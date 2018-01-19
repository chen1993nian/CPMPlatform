using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class ExternalReference : BaseElement
	{
		private string string_0;

		private string string_1;

		private string string_2;

		public ExternalReference()
		{
		}

		public string GetLocation()
		{
			return base.GetAttribute(this.string_1, out this.string_1, "location");
		}

		public string GetName()
		{
			return base.GetAttribute(this.string_2, out this.string_2, "namespace");
		}

		public string GetXref()
		{
			return base.GetAttribute(this.string_0, out this.string_0, "xref");
		}
	}
}