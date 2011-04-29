using System;
using System.Collections.Generic;
using System.Text;
using SocketCommunication.DefsAndEnums;
using System.Runtime.Serialization;

namespace SocketCommunication.Classes.Messages
{
	[Serializable]
	class UserInfoMessage : Message
	{
		#region Data Members

		private UserInfo _UserInfo;

		#endregion

		#region public UserInfo UserInfo
		public UserInfo UserInfo
		{
			get
			{
				return _UserInfo;
			}
		}
		#endregion

		#region Constructors

		#region public UserInfoMessage(UserInfo userInfo)
		public UserInfoMessage(UserInfo userInfo)
			: base(new MsgHeaderData(EMsgDataType.UserInfo))
		{
			_UserInfo = userInfo;
		}
		#endregion

		#endregion

		#region Serialization Members

		#region public UserInfoMessage(SerializationInfo info, StreamingContext context)
		public UserInfoMessage(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			_UserInfo = info.GetValue("UserInfo", typeof(UserInfo)) as UserInfo;
		}
		#endregion

		#region protected override void GetObjectData(SerializationInfo info, StreamingContext context)
		protected override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("UserInfo", _UserInfo);
		}
		#endregion

		#endregion
	}
}
