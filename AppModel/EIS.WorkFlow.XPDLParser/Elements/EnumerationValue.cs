using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class EnumerationValue : BaseElement
	{
		private string string_0;

		public EnumerationValue()
		{
		}

		public string GetName()
		{
			return base.GetAttribute(this.string_0, out this.string_0, "Name");
		}
	}
}