using System;
using System.Collections.Generic;
using System.Text;
using SocketCommunication.DefsAndEnums;
using System.Runtime.Serialization;

namespace SocketCommunication.Classes.Messages
{
	[Serializable]
	class SignonMessage : Message
	{
		#region Constructors

		public SignonMessage()
			: base(new MsgHeaderData(EMsgDataType.Signon))
		{
		}

		public SignonMessage(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		#endregion
	}
}
