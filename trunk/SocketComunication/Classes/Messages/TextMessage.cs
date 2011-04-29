using System;
using System.Collections.Generic;
using System.Text;
using SocketCommunication.DefsAndEnums;
using System.Runtime.Serialization;

namespace SocketCommunication.Classes.Messages
{
	[Serializable]
	class TextMessage : Message
	{
		#region Data Members

		private TextMessageData _TextMessageData;

		#endregion

		#region public TextMessageData TextMessageData
		public TextMessageData TextMessageData
		{
			get
			{
				return _TextMessageData;
			}
		}
		#endregion

		#region Constructors

		#region public TextMessage(string text)
		public TextMessage(string text)
			: base(new MsgHeaderData(EMsgDataType.Text))
		{
			_TextMessageData = new TextMessageData(null, text);
		}
		#endregion

		#endregion

		#region Serialization Members

		#region public TextMessage(SerializationInfo info, StreamingContext context)
		public TextMessage(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			_TextMessageData = info.GetValue("TextMessageData", typeof(TextMessageData)) as TextMessageData;
		}
		#endregion

		#region protected override void GetObjectData(SerializationInfo info, StreamingContext context)
		protected override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("TextMessageData", _TextMessageData);
		}
		#endregion

		#endregion
	}
}
