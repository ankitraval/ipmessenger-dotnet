using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketCommunication.Classes
{
	internal static class SocketHelper
	{
		#region internal static List<Socket> GetUdpMulticastSocketForSender(IPEndPoint groupIpEndPoint)
		internal static List<Socket> GetUdpMulticastSocketForSender(IPEndPoint groupIpEndPoint)
		{
			List<Socket> multiCastSockets = new List<Socket>();

			try
			{
				string hostName = Dns.GetHostName();
				IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
				if (hostEntry.AddressList != null && hostEntry.AddressList.Length > 0)
				{
					for (int i = 0; i < hostEntry.AddressList.Length; i++)
					{
						IPAddress localIP = hostEntry.AddressList[i];
						if (localIP.AddressFamily == AddressFamily.InterNetwork)
						{
							Socket multiCastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

							IPEndPoint ipep = new IPEndPoint(localIP, 0);
							multiCastSocket.Bind(ipep);

							multiCastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(groupIpEndPoint.Address, localIP));
							//multiCastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastLoopback, false);
							multiCastSockets.Add(multiCastSocket);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return multiCastSockets;
		}
		#endregion

		#region internal static List<Socket> GetUdpMulticastSocketForReceiver(IPEndPoint groupIpEndPoint)
		internal static List<Socket> GetUdpMulticastSocketForReceiver(IPEndPoint groupIpEndPoint)
		{
			List<Socket> multiCastSockets = new List<Socket>();

			try
			{
				string hostName;
				IPHostEntry hostEntry;

				hostName = Dns.GetHostName();
				hostEntry = Dns.GetHostEntry(hostName);

				if (hostEntry.AddressList != null && hostEntry.AddressList.Length > 0)
				{
					for (int i = 0; i < hostEntry.AddressList.Length; i++)
					{
						IPAddress localIP = hostEntry.AddressList[i];
						if (localIP.AddressFamily == AddressFamily.InterNetwork)
						{
							Socket multiCastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

							IPEndPoint ipep = new IPEndPoint(localIP, groupIpEndPoint.Port);

							multiCastSocket.Bind(ipep);

							multiCastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(groupIpEndPoint.Address, localIP));

							multiCastSockets.Add(multiCastSocket);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return multiCastSockets;
		}
		#endregion

		#region internal static Socket GetUdpSocketForSender()
		internal static Socket GetUdpSocketForSender()
		{
			Socket udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			return udpSocket;
		}
		#endregion
	}
}
