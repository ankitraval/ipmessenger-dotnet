using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketCommunication.Classes
{
	internal static class SocketHelper
	{
		#region internal static Socket GetUdpMulticastSocketForSender(IPEndPoint groupIpEndPoint)
		internal static Socket GetUdpMulticastSocketForSender(IPEndPoint groupIpEndPoint)
		{
			Socket multiCastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			try
			{
				IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 0);
				multiCastSocket.Bind(ipep);

				multiCastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(groupIpEndPoint.Address, IPAddress.Any));
				multiCastSocket.MulticastLoopback = false;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return multiCastSocket;
		}
		#endregion

		#region internal static Socket GetUdpMulticastSocketForReceiver(IPEndPoint groupIpEndPoint)
		internal static Socket GetUdpMulticastSocketForReceiver(IPEndPoint groupIpEndPoint)
		{
			Socket multiCastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			try
			{
				IPEndPoint ipep = new IPEndPoint(IPAddress.Any, groupIpEndPoint.Port);

				multiCastSocket.Bind(ipep);

				multiCastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(groupIpEndPoint.Address, IPAddress.Any));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return multiCastSocket;
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
