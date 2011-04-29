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
		#region enum EUIStatus
		enum EUIStatus
		{
			Default,
			ShowingReceicvedMessages,
			ShowingMenu,
			WaitingForUserSelection,
			ProcessingUserSelction,
			WaitingForMessageInput,
		}
		#endregion

		#region Data Members

		static EUIStatus UIStatus = EUIStatus.Default;
		static ArrayList receivedMessages = new ArrayList();
		static Queue<TextMessageData> TextMessageQueue = new Queue<TextMessageData>();

		#endregion

		#region static void Main(string[] args)
		static void Main(string[] args)
		{
			SocketCommService.SubscribeForUSerInfos(new Program());
			SocketCommService.SubscribeForTextMessages(new Program());

			do
			{
				UIStatus = EUIStatus.ShowingReceicvedMessages;
				ShowRecivedMessages();
				UIStatus = EUIStatus.ShowingMenu;
				ShowSelectionMenu();
				UIStatus = EUIStatus.WaitingForUserSelection;
				char userInput = GetUserInput();
				UIStatus = EUIStatus.ProcessingUserSelction;
				ProcessUserInput(userInput);
				Console.Clear();
			}
			while (true);
		}
		#endregion

		#region private static void ShowRecivedMessages()
		private static void ShowRecivedMessages()
		{
			while (TextMessageQueue.Count > 0)
			{
				TextMessageData data = TextMessageQueue.Dequeue();
				Console.WriteLine(data);
			}
		}
		#endregion

		#region private static void ShowSelectionMenu()
		private static void ShowSelectionMenu()
		{
			Console.WriteLine("---------------Menu---------------");
			for (int i = 0; i < receivedMessages.Count; i++)
			{
				Console.WriteLine("{0}: {1}", i + 1, receivedMessages[i]);
			}
			Console.WriteLine("0: Continue");
			Console.WriteLine("----------------------------------");
			Console.Write("Enter your selection: ");
		}
		#endregion

		#region private static char GetUserInput()
		private static char GetUserInput()
		{
			ConsoleKeyInfo keyInfo = Console.ReadKey();
			return keyInfo.KeyChar;
		}
		#endregion

		#region private static void ProcessUserInput(char userInput)
		private static void ProcessUserInput(char userInput)
		{
			if (userInput == '0')
			{
				return;
			}
			int selction;
			if (int.TryParse(userInput.ToString(), out selction) == false)
			{
				return;
			}
			if (receivedMessages.Count < selction)
			{
				return;
			}
			UserInfo selectedUserInfo = receivedMessages[selction - 1] as UserInfo;
			if (selectedUserInfo == null)
			{
				return;
			}
			Console.WriteLine();
			Console.Write("Enter Message : ");
			UIStatus = EUIStatus.WaitingForMessageInput;
			string message = Console.ReadLine();
			CommunicationManager.CommMgr.SendTextMessage(selectedUserInfo.IPAddress, message);
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
			if (data is UserInfo)
			{
				receivedMessages.Add(data);
			}
			else if (data is TextMessageData)
			{
				TextMessageQueue.Enqueue(data as TextMessageData);
			}

			if (UIStatus == EUIStatus.WaitingForUserSelection)
			{
				Console.Clear();
				UIStatus = EUIStatus.ShowingReceicvedMessages;
				ShowRecivedMessages();
				UIStatus = EUIStatus.ShowingMenu;
				ShowSelectionMenu();
				UIStatus = EUIStatus.WaitingForUserSelection;
			}
		}
		#endregion

		#endregion
	}
}
