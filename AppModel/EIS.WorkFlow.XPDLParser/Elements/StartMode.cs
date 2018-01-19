using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class StartMode : BaseElement
	{
		private BaseElement baseElement_0;

		private static string[] string_0;

		static StartMode()
		{
			StartMode.string_0 = new string[] { "Automatic", "Manual" };
		}

		public StartMode()
		{
		}

		public object GetMode()
		{
			object obj = base.ChoiceChild(this.baseElement_0, out this.baseElement_0, StartMode.string_0);
			return obj;
		}
	}
}