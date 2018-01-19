using System;
using System.Diagnostics;

namespace EIS.WorkFlow.XPDL.Utility
{
	public class SystemFunction
	{
		public static bool AlreadyStarted(string processName)
		{
			return (int)Process.GetProcessesByName(processName).Length > 1;
		}
	}
}