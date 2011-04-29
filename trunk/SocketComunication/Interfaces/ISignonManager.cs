using System;
using System.Collections.Generic;
using System.Text;

namespace SocketCommunication.Interfaces
{
	public interface ISignonManager
	{
		void SubscribeForUserInfos(IObserver observer);

		void UnsubscribeForUserInfos(IObserver observer);

		Classes.UserInfo GetUserInfo(string remoteIPString);

		void Refresh();
	}
}
