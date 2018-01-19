using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Tool : BaseElement
	{
		private BaseElement baseElement_0;

		private string string_0;

		private string string_1;

		private BaseElement baseElement_1;

		private string string_2;

		private static Hashtable hashtable_0;

		public Tool()
		{
			if (Tool.hashtable_0 == null)
			{
				Tool.hashtable_0 = new Hashtable()
				{
					{ "APPLICATION", ToolType.APPLICATION },
					{ "PROCEDURE", ToolType.PROCEDURE }
				};
			}
		}

		public ActualParameters GetActualParameters()
		{
			ActualParameters child = (ActualParameters)base.GetChild(this.baseElement_0, out this.baseElement_0, "ActualParameters");
			return child;
		}

		public string GetDescription()
		{
			return base.GetChildText(this.string_1, out this.string_1, "Description");
		}

		public ExtendedAttributes GetExtendedAttributes()
		{
			ExtendedAttributes child = (ExtendedAttributes)base.GetChild(this.baseElement_1, out this.baseElement_1, "ExtendedAttributes");
			return child;
		}

		public string GetId()
		{
			return base.GetAttribute(this.string_0, out this.string_0, "Id");
		}

		public ToolType GetToolType()
		{
			ToolType item = (ToolType)Tool.hashtable_0[base.GetAttribute(this.string_2, out this.string_2, "Type")];
			return item;
		}

		public bool IsTypeSpecified()
		{
			return base.GetAttribute(this.string_2, out this.string_2, "Type") != "";
		}
	}
}