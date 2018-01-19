using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	[Serializable]
	public class BasicType : BaseElement
	{
		private string mode;

		private static Hashtable modes;

		public BasicType()
		{
			if (BasicType.modes == null)
			{
				BasicType.modes = new Hashtable()
				{
					{ "STRING", BasicTypeType.STRING },
					{ "FLOAT", BasicTypeType.FLOAT },
					{ "INTEGER", BasicTypeType.INTEGER },
					{ "REFERENCE", BasicTypeType.REFERENCE },
					{ "DATETIME", BasicTypeType.DATETIME },
					{ "BOOLEAN", BasicTypeType.BOOLEAN },
					{ "PERFORMER", BasicTypeType.PERFORMER }
				};
			}
		}

		public BasicTypeType GetBasicTypeType()
		{
			BasicTypeType item = (BasicTypeType)BasicType.modes[base.GetAttribute(this.mode, out this.mode, "Type")];
			return item;
		}
	}
}