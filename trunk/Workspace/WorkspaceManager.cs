using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace Workspace
{
	public class WorkspaceManager
	{
		#region public static void Initialize()
		public static void Initialize()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			CMain.Intialize();
			Application.Run();
		}
		#endregion
	}
}
