﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SocketCommunication.Interfaces
{
	public interface ISignonManager
	{
		void SubscribeForUserInfos(IObserver observer);
	}
}
