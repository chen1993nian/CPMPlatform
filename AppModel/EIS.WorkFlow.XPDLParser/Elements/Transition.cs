using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Transition : BaseElementWithIdName
	{
		private BaseElement baseElement_0;

		private string string_2;

		private BaseElement baseElement_1;

		private string string_3;

		private string string_4;

		public Transition()
		{
		}

		public Condition GetCondition()
		{
			Condition child = (Condition)base.GetChild(this.baseElement_0, out this.baseElement_0, "Condition");
			return child;
		}

		public string GetDescription()
		{
			return base.GetChildText(this.string_2, out this.string_2, "Description");
		}

		public string GetExtendedAttribute(string attrName)
		{
			string str = "";
			try
			{
				ExtendedAttribute extendedAttributeByName = this.GetExtendedAttributes().GetExtendedAttributeByName(attrName);
				str = (extendedAttributeByName != null ? extendedAttributeByName.GetValue() : "");
			}
			catch
			{
			}
			return str;
		}

		public ExtendedAttributes GetExtendedAttributes()
		{
			ExtendedAttributes child = (ExtendedAttributes)base.GetChild(this.baseElement_1, out this.baseElement_1, "ExtendedAttributes");
			return child;
		}

		public string GetFrom()
		{
			return base.GetAttribute(this.string_3, out this.string_3, "From");
		}

		public string GetTo()
		{
			return base.GetAttribute(this.string_4, out this.string_4, "To");
		}
	}
}