using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class BizLogic : BaseElement
	{
		private string string_0;

		private string string_1;

		public BizLogic()
		{
		}

		public string GetEventName()
		{
			return base.GetAttribute(this.string_1, out this.string_1, "EventName");
		}

		public new string GetText()
		{
			return base.GetText();
		}

		public string GetValue()
		{
			return base.GetAttribute(this.string_0, out this.string_0, "Value");
		}
	}
}