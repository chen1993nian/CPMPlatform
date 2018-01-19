using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class TransitionRef : BaseElement
	{
		private string string_0;

		public TransitionRef()
		{
		}

		public string GetId()
		{
			return base.GetAttribute(this.string_0, out this.string_0, "Id");
		}
	}
}