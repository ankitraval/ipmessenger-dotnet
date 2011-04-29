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

		#region public static void SubscribeForUSerInfos(IObserver observer)
		public static void SubscribeForUSerInfos(IObserver observer)
		{
			if (observer == null)
			{
				return;
			}
			SignonManagerInstance.SubscribeForUserInfos(observer);
		}
		#endregion
	}
}
