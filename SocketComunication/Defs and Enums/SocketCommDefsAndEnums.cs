using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.DefsAndEnums
{
	internal class SocketCommDefs
	{
		public const string MulticastGroupIP = "224.168.100.2";
		public const int MulticastGroupPort = 19880;
	}

	internal enum EMsgDataType
	{
		Default,
		Signon,
		SignonReply,
		ReceiveConfirmation,
	}
}
