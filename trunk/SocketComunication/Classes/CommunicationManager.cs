using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using SocketCommunication.DefsAndEnums;
using SocketCommunication.Interfaces;
using System.IO.Compression;
using SevenZip;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization;
using SocketCommunication.Classes.Messages;

namespace SocketCommunication.Classes
{
	class CommunicationManager : ICommunicationManager
	{
		#region private struct UdpSenderWorkMethodParams
		private struct UdpSenderWorkMethodParams
		{
			#region Data Members

			private Socket _Socket;
			private IPEndPoint _RemoteEP;
			private Message _Message;

			#endregion

			#region public Socket Socket
			public Socket Socket
			{
				get
				{
					return _Socket;
				}
			}
			#endregion

			#region public IPEndPoint RemoteEP
			public IPEndPoint RemoteEP
			{
				get
				{
					return _RemoteEP;
				}
			}
			#endregion

			#region public Message Message
			public Message Message
			{
				get
				{
					return _Message;
				}
			}
			#endregion

			#region Constructors

			#region public UdpSenderWorkMethodParams(Socket socket, IPEndPoint remoteEP, Message message)
			public UdpSenderWorkMethodParams(Socket socket, IPEndPoint remoteEP, Message message)
			{
				_Socket = socket;
				_RemoteEP = remoteEP;
				_Message = message;
			}
			#endregion

			#endregion
		}
		#endregion

		#region Data Members

		private static CommunicationManager s_Instance = null;
		private IPEndPoint MulticastGroupIpEndPoint;
		private Thread UdpListenerThread;
		private Thread UdpSenderThread;
		private List<int> ReceivedConfirmationList;

		#endregion

		#region Constructors
		private CommunicationManager()
		{
		}
		#endregion

		#region Properties

		internal static CommunicationManager CommMgr
		{
			get
			{
				if (s_Instance == null)
				{
					Initialize();
				}
				return s_Instance;
			}
		}

		#endregion

		#region private static void Initialize()
		private static void Initialize()
		{
			if (s_Instance == null)
			{
				s_Instance = new CommunicationManager();

				s_Instance.PopulateMulticastGroupIPEndPoint();

				s_Instance.StartUdpListener();
			}
		}
		#endregion

		#region private void PopulateMulticastGroupIPEndPoint()
		private void PopulateMulticastGroupIPEndPoint()
		{
			IPAddress multicastGroupIp = IPAddress.Parse(SocketCommDefs.MulticastGroupIP);
			int multicastGroupIpPort = SocketCommDefs.MulticastGroupPort;
			MulticastGroupIpEndPoint = new IPEndPoint(multicastGroupIp, multicastGroupIpPort);
		}
		#endregion

		#region private void StartUdpListener()
		private void StartUdpListener()
		{
			if (UdpListenerThread == null)
			{
				ThreadStart udpListenerThreadStart = new ThreadStart(UdpListenerWorkMethod);
				UdpListenerThread = new Thread(udpListenerThreadStart);
				UdpListenerThread.IsBackground = true;
				UdpListenerThread.Name = "UdpListenerThread";
				UdpListenerThread.Priority = ThreadPriority.Normal;
			}
			if ((UdpListenerThread.ThreadState & ThreadState.Unstarted) == ThreadState.Unstarted ||
				(UdpListenerThread.ThreadState & ThreadState.Stopped) == ThreadState.Stopped)
			{
				UdpListenerThread.Start();
			}
		}
		#endregion

		#region private void UdpListenerWorkMethod()
		private void UdpListenerWorkMethod()
		{
			List<Socket> udpListenerSockets = SocketHelper.GetUdpMulticastSocketForReceiver(MulticastGroupIpEndPoint);

			try
			{
				byte[] dataBuffer = null;
				EndPoint remoteEP = null;

				while (true)
				{
					Socket udpListenerSocket = null;
					while (true)
					{
						bool dataAvailable = false;
						for (int i = 0; i < udpListenerSockets.Count; i++)
						{
							udpListenerSocket = udpListenerSockets[i];

							if (udpListenerSocket != null && udpListenerSocket.Available > 0)
							{
								dataAvailable = true;
								break;
							}
						}
						if (dataAvailable == true)
						{
							break;
						}
					}

					remoteEP = new IPEndPoint(IPAddress.Any, 0);
					dataBuffer = new byte[udpListenerSocket.Available];
					int dataLength = udpListenerSocket.ReceiveFrom(dataBuffer, ref remoteEP);

					dataBuffer = SevenZipHelper.Decompress(dataBuffer);

					int msgHeaderDataLength;
					Message message = GetMessageFromBytes(dataBuffer, out msgHeaderDataLength);

					if (message == null || message.HeaderData == null || msgHeaderDataLength < 0)
					{
						continue;
					}

					IPAddress ipAddress = IPAddress.None;

					if (remoteEP is IPEndPoint)
					{
						ipAddress = (remoteEP as IPEndPoint).Address;
					}

					if (message.HeaderData.MsgDataType != EMsgDataType.ReceiveConfirmation)
					{
						SendReceiveConfirmationMessage(message.HeaderData.RefNo, ipAddress);
					}

					ProcessMessage(message, ipAddress);

					dataBuffer = null;
					remoteEP = null;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			for (int i = 0; i < udpListenerSockets.Count; i++)
			{
				if (udpListenerSockets[i] != null)
				{
					udpListenerSockets[i].Close();
				}
			}
		}
		#endregion

		#region private void UdpSenderWorkMethod(object obj)
		private void UdpSenderWorkMethod(object obj)
		{
			if ((obj is UdpSenderWorkMethodParams) == false)
			{
				return;
			}

			UdpSenderWorkMethodParams udpSenderWorkMethodParams = (UdpSenderWorkMethodParams)obj;
			if (udpSenderWorkMethodParams.Socket == null || udpSenderWorkMethodParams.RemoteEP == null ||
				udpSenderWorkMethodParams.Message == null)
			{
				return;
			}

			try
			{
				byte[] data = ConvertMessageToBytes(udpSenderWorkMethodParams.Message);

				data = SevenZipHelper.Compress(data);

				if (udpSenderWorkMethodParams.Message.HeaderData.MsgDataType != EMsgDataType.ReceiveConfirmation)
				{
					int refNo = udpSenderWorkMethodParams.Message.HeaderData.RefNo;

					bool receiveConfirmationReceived = false;

					//Send message max three times. Stop if confirmation is received.
					for (int i = 0; i < 3; i++)
					{
						udpSenderWorkMethodParams.Socket.SendTo(data, udpSenderWorkMethodParams.RemoteEP);

						//Check for confirmation for 5 sec
						int j = 0;
						while (true)
						{
							if (ReceivedConfirmationList != null && ReceivedConfirmationList.Contains(refNo))
							{
								receiveConfirmationReceived = true;
								break;
							}
							//Wait for 10 micro sec
							Thread.Sleep(1000);
							j++;
							if (j == 5000)
							{
								receiveConfirmationReceived = false;
								break;
							}
						}

						if (receiveConfirmationReceived)
						{
							break;
						}
					}

					if (receiveConfirmationReceived == false)
					{
						NotifyFailedTransmission(udpSenderWorkMethodParams);
					}
					else
					{
						lock (ReceivedConfirmationList)
						{
							ReceivedConfirmationList.Remove(refNo);
						}
					}
				}
				else
				{
					udpSenderWorkMethodParams.Socket.SendTo(data, udpSenderWorkMethodParams.RemoteEP);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			udpSenderWorkMethodParams.Socket.Close();
		}
		#endregion

		#region Byte Conversion Methods

		#region private Message GetMessageFromBytes(byte[] data, out int dataRead)
		private Message GetMessageFromBytes(byte[] data, out int dataRead)
		{
			Message message = null;

			MemoryStream memStream = new MemoryStream(data);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			try
			{
				object obj = binaryFormatter.Deserialize(memStream);
				if (obj != null && obj is Message)
				{
					message = obj as Message;
				}
				dataRead = (int)memStream.Position;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				dataRead = -1;
			}
			return message;
		}
		#endregion

		#region private byte[] ConvertMessageToBytes(Message message)
		private byte[] ConvertMessageToBytes(Message message)
		{
			byte[] data = null;
			MemoryStream memStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			try
			{
				binaryFormatter.Serialize(memStream, message);
				data = memStream.ToArray();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return data;
		}
		#endregion

		#endregion

		#region Send Methods

		#region private void SendMessage(Socket socket, IPEndPoint remoteEP, Message message)
		private void SendMessage(Socket socket, IPEndPoint remoteEP, Message message)
		{
			ParameterizedThreadStart udpListenerThreadStart = new ParameterizedThreadStart(UdpSenderWorkMethod);
			UdpSenderThread = new Thread(udpListenerThreadStart);
			UdpSenderThread.IsBackground = true;
			UdpSenderThread.Name = "UdpSenderThread";
			UdpSenderThread.Priority = ThreadPriority.Normal;

			UdpSenderWorkMethodParams udpSenderWorkMethodParams = new UdpSenderWorkMethodParams(socket, remoteEP, message);
			UdpSenderThread.Start(udpSenderWorkMethodParams);
		}
		#endregion

		#region internal void SendSignOn()
		internal void SendSignOn()
		{
			List<Socket> mcastSenderSockets = SocketHelper.GetUdpMulticastSocketForSender(MulticastGroupIpEndPoint);

			if (mcastSenderSockets == null || mcastSenderSockets.Count == 0)
			{
				return;
			}

			Message message = new SignonMessage();
			foreach (Socket mcastSenderSocket in mcastSenderSockets)
			{
				if (mcastSenderSocket == null)
				{
					continue;
				}
				SendMessage(mcastSenderSocket, MulticastGroupIpEndPoint, message);
			}
		}
		#endregion

		#region internal void SendSignOnReply(IPAddress remoteIP)
		internal void SendSignOnReply(IPAddress remoteIP)
		{
			if (remoteIP == null || remoteIP == IPAddress.None)
			{
				return;
			}
			IPEndPoint receiverEndPoint = new IPEndPoint(remoteIP, MulticastGroupIpEndPoint.Port);

			Message message = new SignonMessage();
			message.HeaderData.IsReply = true;

			Socket udpSocket = SocketHelper.GetUdpSocketForSender();

			SendMessage(udpSocket, receiverEndPoint, message);
		}
		#endregion

		#region private void SendReceiveConfirmationMessage(int refNo,IPAddress remoteIP)
		private void SendReceiveConfirmationMessage(int refNo, IPAddress remoteIP)
		{
			if (remoteIP == null || remoteIP == IPAddress.None)
			{
				return;
			}
			IPEndPoint receiverEndPoint = new IPEndPoint(remoteIP, MulticastGroupIpEndPoint.Port);

			Message message = new ReceiveConfirmationMessage(refNo);

			Socket udpSocket = SocketHelper.GetUdpSocketForSender();

			SendMessage(udpSocket, receiverEndPoint, message);
		}
		#endregion

		#region internal void SendCurrentUserInfoMessage(IPAddress remoteIP, bool IsReply)
		internal void SendCurrentUserInfoMessage(IPAddress remoteIP, bool IsReply)
		{
			if (remoteIP == null || remoteIP == IPAddress.None)
			{
				return;
			}
			IPEndPoint receiverEndPoint = new IPEndPoint(remoteIP, MulticastGroupIpEndPoint.Port);

			UserInfo currentUserInfo = UserInfo.GetCurrentUserInfo();

			Message message = new UserInfoMessage(currentUserInfo);
			message.HeaderData.IsReply = IsReply;

			Socket udpSocket = SocketHelper.GetUdpSocketForSender();

			SendMessage(udpSocket, receiverEndPoint, message);
		}
		#endregion

		#endregion

		#region Process Methdods

		#region private void ProcessMessage(Message message, IPAddress remoteIP)
		private void ProcessMessage(Message message, IPAddress remoteIP)
		{
			if (message == null || message.HeaderData == null)
			{
				return;
			}
			switch (message.HeaderData.MsgDataType)
			{
				case EMsgDataType.Signon:
					{
						SignonManager.SignonMgr.ProcessSignonMessage(remoteIP, message.HeaderData.IsReply);
					}
					break;
				case EMsgDataType.ReceiveConfirmation:
					{
						if ((message is ReceiveConfirmationMessage) == false)
						{
							break;
						}
						ReceiveConfirmationMessage receiveConfirmationMessage = message as ReceiveConfirmationMessage;
						if (receiveConfirmationMessage == null)
						{
							break;
						}
						if (ReceivedConfirmationList == null)
						{
							ReceivedConfirmationList = new List<int>();
						}
						lock (ReceivedConfirmationList)
						{
							ReceivedConfirmationList.Add(receiveConfirmationMessage.ReceiveConfirmRefNo);
						}
					}
					break;
				case EMsgDataType.UserInfo:
					{
						if ((message is UserInfoMessage) == false || remoteIP == IPAddress.None)
						{
							break;
						}
						UserInfoMessage userInfoMessage = message as UserInfoMessage;
						if (userInfoMessage == null || userInfoMessage.UserInfo == null)
						{
							break;
						}
						userInfoMessage.UserInfo._IPAddress = remoteIP.ToString();
						SignonManager.SignonMgr.ProcessUserInfoMessage(userInfoMessage.UserInfo, remoteIP, message.HeaderData.IsReply);
					}
					break;
			}
		}
		#endregion

		#region private void NotifyFailedTransmission(UdpSenderWorkMethodParams udpSenderWorkMethodParams)
		private void NotifyFailedTransmission(UdpSenderWorkMethodParams udpSenderWorkMethodParams)
		{
			MsgHeaderData msgHeaderData = udpSenderWorkMethodParams.Message.HeaderData;
			Console.WriteLine("Transmission failed for {0} message - RefNo : {1}", msgHeaderData.MsgDataType, msgHeaderData.RefNo);
		}
		#endregion

		#endregion
	}
}
