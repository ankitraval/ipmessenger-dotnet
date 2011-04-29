using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Net;
using System.Collections;

namespace SocketCommunication.Classes
{
	[Serializable]
	public class UserInfo : ISerializable, IComparable
	{
		#region Data Members

		internal string _IPAddress;
		private string _UserName, _Group, _MachineName, _LogOnName;

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

		#region public string UserName
		public string UserName
		{
			get
			{
				return _UserName;
			}
		}
		#endregion

		#region public string Group
		public string Group
		{
			get
			{
				return _Group;
			}
		}
		#endregion

		#region public string MachineName
		public string MachineName
		{
			get
			{
				return _MachineName;
			}
		}
		#endregion

		#region public string LogOnName
		public string LogOnName
		{
			get
			{
				return _LogOnName;
			}
		}
		#endregion

		#endregion

		#region Constructors

		#region internal UserInfo (string ipAddress, string userName, string group, string machineName, string logOnName)
		internal UserInfo(string ipAddress, string userName, string group, string machineName, string logOnName)
		{
			_IPAddress = ipAddress;
			_UserName = userName;
			_Group = group;
			_MachineName = machineName;
			_LogOnName = logOnName;
		}
		#endregion

		#endregion

		#region internal static UserInfo GetCurrentUserInfo()
		internal static UserInfo GetCurrentUserInfo()
		{
			string ipAddress = string.Empty;
			string userName = Environment.UserName;
			string group = string.Empty;
			string machineName = Environment.MachineName;
			string logOnName = Environment.UserName;

			return new UserInfo(string.Empty, logOnName, group, machineName, logOnName);
		}
		#endregion

		#region Serialization Members

		#region public void GetObjectData(SerializationInfo info, StreamingContext context)
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("UserName", _UserName);
			info.AddValue("Group", _Group);
			info.AddValue("MachineName", _MachineName);
			info.AddValue("LogOnName", _LogOnName);
		}
		#endregion

		#region internal UserInfo(SerializationInfo info, StreamingContext context)
		internal UserInfo(SerializationInfo info, StreamingContext context)
		{
			_UserName = info.GetString("UserName");
			_Group = info.GetString("Group");
			_MachineName = info.GetString("MachineName");
			_LogOnName = info.GetString("LogOnName");
		}
		#endregion

		#endregion

		#region public override string ToString()
		public override string ToString()
		{
			string returnString = string.Format("IPAddress:{0} | User Name:{1} | Group:{2} | Machine Name: {3} | Log On Name:{4}", _IPAddress,
				_UserName, _Group, _MachineName, _LogOnName);

			return returnString;
		}
		#endregion

		#region IComparable Members

		public int CompareTo(object obj)
		{
			UserInfo userInfo = obj as UserInfo;
			if (this.IPAddress == userInfo.IPAddress)
			{
				return 0;
			}
			return this.IPAddress.CompareTo(userInfo.IPAddress);
		}

		#endregion
	}
}
