using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SocketCommunication.Interfaces;
using System.Collections;

namespace SocketCommunication.Classes
{
	class SignonManager : ISignonManager
	{
		#region Data Members

		private static SignonManager s_Instance = null;
		private List<string> ReceivedSignOnIPs = new List<string>();
		private List<IObserver> Observers = new List<IObserver>();
		private Hashtable UserInfos = new Hashtable();
		private static CommunicationManager CommMgr = CommunicationManager.CommMgr;
		#endregion

		#region private SignonManager()
		private SignonManager()
		{

		}
		#endregion

		#region internal static SignonManager SignonMgr
		internal static SignonManager SignonMgr
		{
			get
			{
				if (s_Instance == null)
				{
					Intialize();
				}
				return s_Instance;
			}
		}
		#endregion

		#region private static void Intialize()
		private static void Intialize()
		{
			if (s_Instance == null)
			{
				s_Instance = new SignonManager();

				CommMgr.SendSignOn();
			}
		}
		#endregion
		
		#region Process Methods

		#region internal void ProcessSignonMessage(IPAddress remoteIP, bool IsReply)
		internal void ProcessSignonMessage(IPAddress remoteIP, bool IsReply)
		{
			if (remoteIP == null || remoteIP == IPAddress.None)
			{
				return;
			}
			string message = remoteIP.ToString();
			lock (ReceivedSignOnIPs)
			{
				if (ReceivedSignOnIPs.Contains(message) == false)
				{
					ReceivedSignOnIPs.Add(message);
				}
			}
			if (IsReply == false)
			{
				CommMgr.SendSignOnReply(remoteIP);
			}
			else
			{
				CommMgr.SendCurrentUserInfoMessage(remoteIP, false);
			}
		}
		#endregion

		#region internal void ProcessUserInfoMessage(UserInfo userInfo, IPAddress remoteIP, bool IsReply)
		internal void ProcessUserInfoMessage(UserInfo userInfo, IPAddress remoteIP, bool IsReply)
		{
			if (userInfo == null)
			{
				return;
			}
			lock (UserInfos.SyncRoot)
			{
				if(UserInfos[userInfo.IPAddress] == null)
				{
					UserInfos[userInfo.IPAddress]  = userInfo;
					NotifyObservers(userInfo);
				}
			}
			if (IsReply == false)
			{
				CommMgr.SendCurrentUserInfoMessage(remoteIP, true);
			}
		}
		#endregion

		#region private void NotifyObservers(UserInfo userInfo)
		private void NotifyObservers(UserInfo userInfo)
		{
			lock (Observers)
			{
				if (Observers == null || Observers.Count <= 0)
				{
					return;
				}
				foreach (IObserver observer in Observers)
				{
					observer.Notify(userInfo);
				}
			}
		}
		#endregion

		#endregion

		#region ISignonManager Members

		#region public void SubscribeForUserInfos(IObserver observer)
		public void SubscribeForUserInfos(IObserver observer)
		{
			lock (Observers)
			{
				if (observer == null || Observers.Contains(observer))
				{
					return;
				}
				Observers.Add(observer);
				lock (UserInfos.SyncRoot)
				{
					foreach (UserInfo userInfo in UserInfos.Values)
					{
						observer.Notify(userInfo);
					}
				}
			}
		}
		#endregion

		#endregion
	}
}
