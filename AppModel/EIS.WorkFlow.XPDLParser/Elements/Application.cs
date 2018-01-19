using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Application : BaseElementWithIdName
	{
		private string string_2;

		private BaseElement baseElement_0;

		private static string[] string_3;

		private BaseElement baseElement_1;

		static Application()
		{
			Application.string_3 = new string[] { "ExternalReference", "FormalParameters" };
		}

		public Application()
		{
		}

		public object GetDefination()
		{
			object obj = base.ChoiceChild(this.baseElement_0, out this.baseElement_0, Application.string_3);
			return obj;
		}

		public string GetDescription()
		{
			return base.GetChildText(this.string_2, out this.string_2, "Description");
		}

		public ExtendedAttributes GetExtendedAttributes()
		{
			ExtendedAttributes child = (ExtendedAttributes)base.GetChild(this.baseElement_1, out this.baseElement_1, "ExtendedAttributes");
			return child;
		}
	}
}