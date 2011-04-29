using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketCommunication.Interfaces;

namespace SocketCommunication.Classes
{
	public static class SocketCommService
	{
		#region Data Members

		private static ISignonManager SignonManagerInstance = null;
		private static ICommunicationManager CommunicationManagerInstance = null;

		#endregion

		#region static SocketCommService()
		static SocketCommService()
		{
			SignonManagerInstance = SignonManager.SignonMgr as ISignonManager;
			CommunicationManagerInstance = CommunicationManager.CommMgr as ICommunicationManager;
		}
		#endregion

		#region public static ISignonManager GetSignonManager()
		public static ISignonManager GetSignonManager()
		{
			return SignonManagerInstance;
		}
		#endregion

		#region public static ICommunicationManager GetCommunicationManager()
		public static ICommunicationManager GetCommunicationManager()
		{
			return CommunicationManagerInstance;
		}
		#endregion
	}
}
