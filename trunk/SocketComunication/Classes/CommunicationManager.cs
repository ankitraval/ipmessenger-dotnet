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

namespace SocketCommunication.Classes
{
	internal class CommunicationManager : ICommunicationManager
	{
		#region Data Memders

		private static CommunicationManager s_Instance = null;
		private IPEndPoint MulticastGroupIpEndPoint;
		private Thread UdpListenerThread;

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

			ThreadStart udpListenerThreadStart = new ThreadStart(UdpListenerWorkMethod);
			UdpListenerThread = new Thread(udpListenerThreadStart);
			UdpListenerThread.IsBackground = true;
			UdpListenerThread.Name = "UdpListenerThread";
			UdpListenerThread.Priority = ThreadPriority.Normal;
			UdpListenerThread.Start();

		}
		#endregion

		#region private void UdpListenerWorkMethod()
		private void UdpListenerWorkMethod()
		{
			Socket UdpListenerSocket = SocketHelper.GetUdpMulticastSocketForReceiver(MulticastGroupIpEndPoint);

			try
			{
				byte[] dataBuffer = null;
				EndPoint remoteEP = null;

				while (true)
				{
					while (UdpListenerSocket.Available == 0)
					{ }

					remoteEP = new IPEndPoint(IPAddress.Any, 0);
					dataBuffer = new byte[UdpListenerSocket.Available];
					int dataLength = UdpListenerSocket.ReceiveFrom(dataBuffer, ref remoteEP);

					int msgHeaderDataLength;
					MsgHeaderData msgHeaderData = GetMsgHeaderData(dataBuffer, out msgHeaderDataLength);

					if (msgHeaderData == null)
					{
						continue;
					}

					int receivedDataLength = dataLength - msgHeaderDataLength;
					int remainingDataLength = msgHeaderData.DataLength - receivedDataLength;

					if (remainingDataLength > 0)
					{
						continue;
					}

					byte[] data = new byte[msgHeaderData.DataLength];
					Array.Copy(dataBuffer, msgHeaderDataLength, data, 0, msgHeaderData.DataLength);

					ProcessMessage(msgHeaderData.MsgDataType, data, remoteEP);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			UdpListenerSocket.Close();
		}
		#endregion

		#region private MsgHeaderData GetMsgHeaderData(byte[] data, out int dataRead)
		private MsgHeaderData GetMsgHeaderData(byte[] data, out int dataRead)
		{
			MsgHeaderData msgHeaderData = null;

			MemoryStream memStrean = new MemoryStream(data);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			try
			{
				object obj = binaryFormatter.Deserialize(memStrean);
				if (obj != null && obj is MsgHeaderData)
				{
					msgHeaderData = obj as MsgHeaderData;
				}
				dataRead = (int)memStrean.Position;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				dataRead = -1;
			}
			return msgHeaderData;
		}
		#endregion

		#region private byte[] GetByteData(object dataObject)
		private byte[] GetByteData(object dataObject)
		{
			byte[] data = null;
			MemoryStream memStrean = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			try
			{
				binaryFormatter.Serialize(memStrean, dataObject);
				data = memStrean.ToArray();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return data;
		}
		#endregion

		#region private void ProcessMessage(EMsgDataType msgDataType, byte[] data, EndPoint remoteEP)
		private void ProcessMessage(EMsgDataType msgDataType, byte[] data, EndPoint remoteEP)
		{
			switch (msgDataType)
			{
				case EMsgDataType.Signon:
					{
						SignonManager.SignonMgr.ProcessSignonMessage(remoteEP.ToString());
					}
					break;
			}
		}
		#endregion

		#region internal void SendSignOn()
		internal void SendSignOn()
		{
			Socket mcastSenderSocket = SocketHelper.GetUdpMulticastSocketForSender(MulticastGroupIpEndPoint);

			MsgHeaderData msgHeaderData = new MsgHeaderData(EMsgDataType.Signon);

			try
			{
				byte[] data = GetByteData(msgHeaderData);

				mcastSenderSocket.SendTo(data, MulticastGroupIpEndPoint);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			mcastSenderSocket.Close();
		}
		#endregion

		#region internal void SendSignOnReply(EndPoint remoteEP)
		internal void SendSignOnReply(EndPoint remoteEP)
		{
			if (remoteEP == null || (remoteEP is IPEndPoint) == false)
			{
				return;
			}
			IPEndPoint receiverEndPoint = new IPEndPoint((remoteEP as IPEndPoint).Address, MulticastGroupIpEndPoint.Port);

			Socket udpSocket = SocketHelper.GetUdpSocketForSender();

			try
			{
				byte[] data = ASCIIEncoding.ASCII.GetBytes("Hi");

				udpSocket.SendTo(data, receiverEndPoint);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			udpSocket.Close();
		}
		#endregion
	}
}
