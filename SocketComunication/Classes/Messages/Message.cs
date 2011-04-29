using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace SocketCommunication.Classes.Messages
{
	[Serializable]
	internal abstract class Message : ISerializable
	{
		#region DataMembers

		private MsgHeaderData _HeaderData;

		#endregion

		#region public MsgHeaderData HeaderData
		public MsgHeaderData HeaderData
		{
			get
			{
				return _HeaderData;
			}
		}
		#endregion

		#region protected Message(MsgHeaderData msgHeaderData)
		protected Message(MsgHeaderData msgHeaderData)
		{
			_HeaderData = msgHeaderData;
		}
		#endregion

		#region protected Message(SerializationInfo info, StreamingContext context)
		protected Message(SerializationInfo info, StreamingContext context)
		{
			_HeaderData = new MsgHeaderData(info, context);
		}
		#endregion

		#region Serializtion Methods

		#region protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			_HeaderData.GetObjectData(info, context);
		}
		#endregion

		#endregion

		#region ISerializable Members

		#region private void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}
		#endregion

		#endregion
	}
}
