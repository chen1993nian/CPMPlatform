using System;

namespace EIS.WorkFlow.XPDL.Utility
{
	public class PDObjectNotSpecified : PDException
	{
		public PDObjectNotSpecified(string message) : base(message)
		{
		}
	}
}