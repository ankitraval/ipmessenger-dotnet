using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SocketCommunication.Interfaces
{
	public interface ICommunicationManager
	{
		void SubscribeForTextMessages(IObserver observer);

		void UnsubscribeForTextMessages(IObserver observer);

		void SendTextMessage(string remoteIPString, string text);
	}
}
