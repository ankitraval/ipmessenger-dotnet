using System;
using System.Collections.Generic;
using System.Text;
using SocketCommunication.DefsAndEnums;
using System.Runtime.Serialization;

namespace SocketCommunication.Classes.Messages
{
	[Serializable]
	class ReceiveConfirmationMessage : Message
	{
		#region Data Members

		private int _ReceiveConfirmRefNo;

		#endregion

		#region public int ReceiveConfirmRefNo
		public int ReceiveConfirmRefNo
		{
			get
			{
				return _ReceiveConfirmRefNo;
			}
		}
		#endregion

		#region Constructors

		#region public ReceiveConfirmationMessage(int receiveConfirmRefNo)
		public ReceiveConfirmationMessage(int receiveConfirmRefNo)
			: base(new MsgHeaderData(EMsgDataType.ReceiveConfirmation))
		{
			_ReceiveConfirmRefNo = receiveConfirmRefNo;
		}
		#endregion

		#endregion

		#region Serialization Members

		#region public ReceiveConfirmationMessage(SerializationInfo info, StreamingContext context)
		public ReceiveConfirmationMessage(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			_ReceiveConfirmRefNo = info.GetInt32("ReceiveConfirmRefNo");
		}
		#endregion

		#region protected override void GetObjectData(SerializationInfo info, StreamingContext context)
		protected override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ReceiveConfirmRefNo", _ReceiveConfirmRefNo);
		}
		#endregion

		#endregion
	}
}
