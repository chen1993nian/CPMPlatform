using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class ObjectNotFound : XpdlException
	{
		public ObjectNotFound(string message) : base(message)
		{
		}
	}
}