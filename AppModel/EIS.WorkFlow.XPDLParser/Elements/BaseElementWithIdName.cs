using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class BaseElementWithIdName : BaseElement
	{
		private string string_0;

		private string string_1;

		public BaseElementWithIdName()
		{
		}

		public string GetId()
		{
			return base.GetAttribute(this.string_1, out this.string_1, "Id");
		}

		public string GetName()
		{
			return base.GetAttribute(this.string_0, out this.string_0, "Name");
		}

		public void SetId(string strValue)
		{
			this.string_1 = strValue;
		}

		public void SetName(string strValue)
		{
			this.string_0 = strValue;
		}
	}
}