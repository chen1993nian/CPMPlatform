using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Performer : BaseElement
	{
		private BaseElement baseElement_0;

		public Performer()
		{
		}

		public string GetDescription()
		{
			return base.GetText();
		}

		public ExtendedAttributes GetExtendedAttributes()
		{
			ExtendedAttributes child = (ExtendedAttributes)base.GetChild(this.baseElement_0, out this.baseElement_0, "ExtendedAttributes");
			return child;
		}
	}
}