using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketCommunication.DefsAndEnums;

namespace SocketCommunication.Classes
{
	[Serializable]
	internal class MsgHeaderData
	{
		#region Data MEmbers

		private EMsgDataType _MsgDataType;
		private int _DataLength;

		#endregion

		#region Properties

		#region public EMsgDataType MsgDataType
		public EMsgDataType MsgDataType
		{
			get
			{
				return _MsgDataType;
			}
		}
		#endregion

		#region public int DataLength
		public int DataLength
		{
			get
			{
				return _DataLength;
			}
		}
		#endregion

		#endregion

		#region Constructors

		public MsgHeaderData(EMsgDataType msgDataType)
		{
			_MsgDataType = msgDataType;
			_DataLength = 0;
		}

		public MsgHeaderData(EMsgDataType msgDataType, int dataLength)
		{
			_MsgDataType = msgDataType;
			_DataLength = dataLength;
		}

		#endregion
	}
}
