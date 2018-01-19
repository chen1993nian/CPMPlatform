using System;

namespace EIS.WorkFlow.XPDL.Utility
{
	public class PDObjectReadOnly : PDException
	{
		public PDObjectReadOnly(string message) : base(message)
		{
		}
	}
}