using System;
using System.Collections.Generic;
using System.Text;

namespace SocketCommunication.DefsAndEnums
{
	class SocketCommDefs
	{
		public const string MulticastGroupIP = "224.101.19.88";
		public const int MulticastGroupPort = 19880;
	}

	enum EMsgDataType
	{
		Default,
		Signon,
		ReceiveConfirmation,
		UserInfo,
		Text,
	}
}
