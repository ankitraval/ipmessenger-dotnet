using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.Interfaces
{
	public interface ISignonManager
	{
		List<string> GetReceivedMessages();
	}
}
