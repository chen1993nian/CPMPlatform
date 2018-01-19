using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Script : BaseElement
	{
		private string string_0;

		private string string_1;

		private string string_2;

		public Script()
		{
		}

		public string GetGrammar()
		{
			return base.GetAttribute(this.string_2, out this.string_2, "Grammar");
		}

		public string GetSciptType()
		{
			return base.GetAttribute(this.string_0, out this.string_0, "Type");
		}

		public string GetVersion()
		{
			return base.GetAttribute(this.string_1, out this.string_1, "Version");
		}
	}
}