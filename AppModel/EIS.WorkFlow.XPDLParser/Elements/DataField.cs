using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class DataField : BaseElementWithIdName
	{
		private BaseElement baseElement_0;

		private string string_2;

		private string string_3;

		private string string_4;

		private BaseElement baseElement_1;

		private string string_5;

		private static Hashtable hashtable_0;

		public DataField()
		{
			if (DataField.hashtable_0 == null)
			{
				DataField.hashtable_0 = new Hashtable()
				{
					{ "FALSE", DataFieldIsArray.FALSE },
					{ "TRUE", DataFieldIsArray.TRUE }
				};
			}
		}

		public DataFieldIsArray GetDataFieldIsArray()
		{
			DataFieldIsArray item = (DataFieldIsArray)DataField.hashtable_0[base.GetAttribute(this.string_5, out this.string_5, "Mode", "FALSE")];
			return item;
		}

		public DataType GetDataType()
		{
			DataType child = (DataType)base.GetChild(this.baseElement_0, out this.baseElement_0, "DataType");
			return child;
		}

		public string GetDescription()
		{
			return base.GetChildText(this.string_4, out this.string_4, "Description");
		}

		public ExtendedAttributes GetExtendedAttributes()
		{
			ExtendedAttributes child = (ExtendedAttributes)base.GetChild(this.baseElement_1, out this.baseElement_1, "ExtendedAttributes");
			return child;
		}

		public string GetInitialValue()
		{
			return base.GetChildText(this.string_2, out this.string_2, "InitialValue");
		}

		public string GetLength()
		{
			return base.GetChildText(this.string_3, out this.string_3, "Length");
		}
	}
}