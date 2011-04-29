using System;
using System.Collections.Generic;
using System.Text;
using SocketCommunication.Interfaces;
using SocketCommunication.Classes;

namespace SocketCommunication
{
	class Program
	{
		static void Main(string[] args)
		{
			ISignonManager signonMgr = SocketCommService.GetSignonManager();

			do
			{
				List<String> receivedMessages = signonMgr.GetReceivedMessages();
				if (receivedMessages.Count > 0)
				{
					for (int i = 0; i < receivedMessages.Count; i++)
					{
						Console.WriteLine(receivedMessages[i]);
					}
					receivedMessages.Clear();
				}
			}
			while (true);
		}
	}
}
