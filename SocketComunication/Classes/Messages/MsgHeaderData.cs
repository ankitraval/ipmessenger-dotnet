using System;
using System.Collections.Generic;
using System.Text;
using SocketCommunication.DefsAndEnums;
using System.Runtime.Serialization;

namespace SocketCommunication.Classes
{
	[Serializable]
	class MsgHeaderData : ISerializable
	{
		#region Data Members

		private static int RefNoCount = 0;
		private int _RefNo;
		private EMsgDataType _MsgDataType;
		private bool _IsReply;

		#endregion

		#region Properties

		#region internal EMsgDataType MsgDataType
		internal EMsgDataType MsgDataType
		{
			get
			{
				return _MsgDataType;
			}
		}
		#endregion

		#region internal int RefNo
		internal int RefNo
		{
			get
			{
				return _RefNo;
			}
		}
		#endregion

		#region internal bool IsReply
		internal bool IsReply
		{
			get
			{
				return _IsReply;
			}
			set
			{
				_IsReply = value;
			}
		}
		#endregion

		#endregion

		#region Constructors

		internal MsgHeaderData(EMsgDataType msgDataType)
		{
			_MsgDataType = msgDataType;
			_RefNo = RefNoCount;
			RefNoCount++;
			IsReply = false;
		}

		#endregion

		#region Serialization Members

		#region internal MsgHeaderData(SerializationInfo info, StreamingContext context)
		internal MsgHeaderData(SerializationInfo info, StreamingContext context)
		{
			_MsgDataType = (EMsgDataType)info.GetValue("MsgDataTye", typeof(EMsgDataType));
			_RefNo = info.GetInt32("RefNo");
			_IsReply = info.GetBoolean("IsReply");
		}
		#endregion

		#region public void GetObjectData(SerializationInfo info, StreamingContext context)
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("MsgDataTye", _MsgDataType);
			info.AddValue("RefNo", _RefNo);
			info.AddValue("IsReply", _IsReply);
		}
		#endregion

		#endregion
	}
}
