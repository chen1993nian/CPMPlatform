using System;

namespace EIS.WorkFlow.XPDL.Utility
{
	public class PDObjectNotFound : PDException
	{
		public PDObjectNotFound(string message) : base(message)
		{
		}
	}
}