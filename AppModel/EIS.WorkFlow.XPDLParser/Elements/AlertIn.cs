using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class AlertIn : BaseElement
	{
		private string string_0;

		private string string_1;

		private string string_2;

		private BaseElement baseElement_0;

		public AlertIn()
		{
		}

		public string GetMessage()
		{
			return base.GetChildText(this.string_2, out this.string_2, "Message");
		}

		public string GetOption()
		{
			return base.GetChildText(this.string_0, out this.string_0, "Option");
		}

		public Performers GetPerformers()
		{
			Performers child = (Performers)base.GetChild(this.baseElement_0, out this.baseElement_0, "Performers");
			return child;
		}

		public string GetTitle()
		{
			return base.GetChildText(this.string_1, out this.string_1, "Title");
		}
	}
}