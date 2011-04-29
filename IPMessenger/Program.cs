using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Workspace;

namespace IPMessenger
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			WorkspaceManager.Initialize();
		}
	}
}
