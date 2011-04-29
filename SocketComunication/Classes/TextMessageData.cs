using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Net;
using System.Collections;

namespace SocketCommunication.Classes
{
	[Serializable]
	public class TextMessageData : ISerializable
	{
		#region Data Members

		internal string _IPAddress;
		internal DateTime _ReceiptTime;
		private string _Text;

		#endregion

		#region Properties

		#region public string IPAddress
		public string IPAddress
		{
			get
			{
				return _IPAddress;
			}
		}
		#endregion

		#region public DateTime ReceiptTime
		public DateTime ReceiptTime
		{
			get
			{
				return _ReceiptTime;
			}
		}
		#endregion

		#region public string Text
		public string Text
		{
			get
			{
				return _Text;
			}
		}
		#endregion

		#endregion

		#region Constructors

		#region internal TextMessageData (string ipAddress, string text)
		internal TextMessageData(string ipAddress, string text)
		{
			_IPAddress = ipAddress;
			_Text = text;
		}
		#endregion

		#endregion

		#region Serialization Members

		#region public void GetObjectData(SerializationInfo info, StreamingContext context)
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Text", _Text);
		}
		#endregion

		#region internal TextMessageData(SerializationInfo info, StreamingContext context)
		internal TextMessageData(SerializationInfo info, StreamingContext context)
		{
			_Text = info.GetString("Text");
		}
		#endregion

		#endregion

		#region public override string ToString()
		public override string ToString()
		{
			string returnString = string.Format("IPAddress:{0} | Text:{1}", _IPAddress, _Text);

			return returnString;
		}
		#endregion
	}
}
