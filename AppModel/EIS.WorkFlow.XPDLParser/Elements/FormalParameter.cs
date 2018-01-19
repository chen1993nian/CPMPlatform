using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	[Serializable]
	public class FormalParameter : BaseElement
	{
		private BaseElement DataType;

		private string Description;

		private string Id;

		private string Index;

		private string mode;

		private static Hashtable modes;

		public FormalParameter()
		{
			if (FormalParameter.modes == null)
			{
				FormalParameter.modes = new Hashtable()
				{
					{ "IN", FormalParameterMode.IN },
					{ "OUT", FormalParameterMode.OUT },
					{ "INOUT", FormalParameterMode.INOUT },
					{ "", FormalParameterMode.NONE }
				};
			}
		}

		public EIS.WorkFlow.XPDLParser.Elements.DataType GetDataType()
		{
			EIS.WorkFlow.XPDLParser.Elements.DataType child = (EIS.WorkFlow.XPDLParser.Elements.DataType)base.GetChild(this.DataType, out this.DataType, "DataType");
			return child;
		}

		public string GetDescription()
		{
			return base.GetChildText(this.Description, out this.Description, "Description");
		}

		public FormalParameterMode GetFormalParameterMode()
		{
			FormalParameterMode item = (FormalParameterMode)FormalParameter.modes[base.GetAttribute(this.mode, out this.mode, "Mode", "IN")];
			return item;
		}

		public string GetId()
		{
			return base.GetAttribute(this.Id, out this.Id, "Id");
		}

		public string GetIndex()
		{
			return base.GetAttribute(this.Index, out this.Index, "Index");
		}
	}
}