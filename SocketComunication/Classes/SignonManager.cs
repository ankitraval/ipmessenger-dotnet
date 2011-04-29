using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SocketCommunication.Interfaces;

namespace SocketCommunication.Classes
{
	internal class SignonManager : ISignonManager
	{
		#region Data Members

		private static SignonManager s_Instance = null;
		private List<string> ReceivedMessages = new List<string>();

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

				s_Instance.SendSignOn();
			}
		}
		#endregion

		#region private void SendSignOn()
		private void SendSignOn()
		{
			CommunicationManager.CommMgr.SendSignOn();
		}
		#endregion

		#region private void SendSignonReply(EndPoint remoteEP)
		private void SendSignonReply(EndPoint remoteEP)
		{
			if (remoteEP == null)
			{
				return;
			}
			CommunicationManager.CommMgr.SendSignOnReply(remoteEP);
		}
		#endregion

		#region internal void ProcessSignonMessage(string message)
		internal void ProcessSignonMessage(string message)
		{
			if (message == null || message.Length == 0 || ReceivedMessages.Contains(message))
			{
				return;
			}
			ReceivedMessages.Add(message);
		}
		#endregion

		#region public List<string> GetReceivedMessages()
		public List<string> GetReceivedMessages()
		{
			return ReceivedMessages;
		}
		#endregion
	}
}
