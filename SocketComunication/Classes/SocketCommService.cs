using System;
using System.Collections.Generic;
using System.Text;
using SocketCommunication.Interfaces;

namespace SocketCommunication.Classes
{
	public static class SocketCommService
	{
		#region Data Members

		private static ISignonManager SignonManagerInstance = null;
		private static ICommunicationManager CommunicationManagerInstance = null;
		public static readonly object ObserverClearCacheMessageData = "Clear";

		#endregion

		#region static SocketCommService()
		static SocketCommService()
		{
			SignonManagerInstance = SignonManager.SignonMgr as ISignonManager;
			CommunicationManagerInstance = CommunicationManager.CommMgr as ICommunicationManager;
		}
		#endregion

		#region internal static ISignonManager GetSignonManager()
		internal static ISignonManager GetSignonManager()
		{
			return SignonManagerInstance;
		}
		#endregion

		#region internal static ICommunicationManager GetCommunicationManager()
		internal static ICommunicationManager GetCommunicationManager()
		{
			return CommunicationManagerInstance;
		}
		#endregion

		#region public static void SubscribeForUserInfos(IObserver observer)
		public static void SubscribeForUserInfos(IObserver observer)
		{
			if (observer == null)
			{
				return;
			}
			SignonManagerInstance.SubscribeForUserInfos(observer);
		}
		#endregion

		#region public static void UnsubscribeForUserInfos(IObserver observer)
		public static void UnsubscribeForUserInfos(IObserver observer)
		{
			if (observer == null)
			{
				return;
			}
			SignonManagerInstance.UnsubscribeForUserInfos(observer);
		}
		#endregion

		#region public static void SubscribeForTextMessages(IObserver observer)
		public static void SubscribeForTextMessages(IObserver observer)
		{
			if (observer == null)
			{
				return;
			}
			CommunicationManagerInstance.SubscribeForTextMessages(observer);
		}
		#endregion

		#region public static void UnsubscribeForTextMessages(IObserver observer)
		public static void UnsubscribeForTextMessages(IObserver observer)
		{
			if (observer == null)
			{
				return;
			}
			CommunicationManagerInstance.UnsubscribeForTextMessages(observer);
		}
		#endregion

		#region public static void SendTextMessage(string remoteIPString, string text)
		public static void SendTextMessage(string remoteIPString, string text)
		{
			CommunicationManagerInstance.SendTextMessage(remoteIPString, text);
		}
		#endregion

		#region public static void RefreshSignonService()
		public static void RefreshSignonService()
		{
			SignonManagerInstance.Refresh();
		}
		#endregion

		#region public static UserInfo GetUserInfo(string remoteIPString)
		public static UserInfo GetUserInfo(string remoteIPString)
		{
			return SignonManagerInstance.GetUserInfo(remoteIPString);
		}
		#endregion

		#region public static int GetUserInfoCount()
		public static int GetUserInfoCount()
		{
			return SignonManagerInstance.GetUserInfoCount();
		}
		#endregion

	}
}
