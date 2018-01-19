using System;

namespace EIS.WorkFlow.XPDL.Utility
{
	public class PDObjectNotSupport : PDException
	{
		public PDObjectNotSupport(string message) : base(message)
		{
		}
	}
}