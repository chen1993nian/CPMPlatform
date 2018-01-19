using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class ExternalPackage : BaseElement
	{
		private BaseElement baseElement_0;

		private string string_0;

		public ExternalPackage()
		{
		}

		public ExtendedAttributes GetExtendedAttributes()
		{
			ExtendedAttributes child = (ExtendedAttributes)base.GetChild(this.baseElement_0, out this.baseElement_0, "ExtendedAttributes");
			return child;
		}

		public string GetHref()
		{
			return base.GetAttribute(this.string_0, out this.string_0, "href");
		}
	}
}