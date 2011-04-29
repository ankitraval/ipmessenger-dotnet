using System;
using System.Collections.Generic;
using System.Text;
using SocketCommunication.Interfaces;
using SocketCommunication.Classes;
using System.Collections;

namespace SocketCommunication
{
	class Program : IObserver
	{
		#region Data Members

		static ArrayList receivedMessages = new ArrayList();

		#endregion

		#region static void Main(string[] args)
		static void Main(string[] args)
		{
			SocketCommService.SubscribeForUSerInfos(new Program());

			do
			{
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
		#endregion

		#region IObserver Members

		#region public void Notify(object data)
		public void Notify(object data)
		{
			if (data == null)
			{
				return;
			}
			receivedMessages.Add(data);
		}
		#endregion

		#endregion
	}
}
