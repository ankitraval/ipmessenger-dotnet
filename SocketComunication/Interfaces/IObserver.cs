using System;
using System.Collections.Generic;
using System.Text;

namespace SocketCommunication.Interfaces
{
	public interface IObserver
	{
		void Notify(object data);
	}
}
