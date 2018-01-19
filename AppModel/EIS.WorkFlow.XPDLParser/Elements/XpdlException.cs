using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class XpdlException : ApplicationException
	{
		public XpdlException(string message) : base(message)
		{
		}
	}
}