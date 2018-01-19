using System;

namespace EIS.WorkFlow.XPDL.Utility
{
	public class PDException : ApplicationException
	{
		public PDException(string message) : base(message)
		{
		}
	}
}