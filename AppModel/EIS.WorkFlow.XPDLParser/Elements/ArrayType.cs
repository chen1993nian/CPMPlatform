using System;
 using EIS.AppModel;
namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class ArrayType : BaseElement
	{
		private BaseElement baseElement_0;

		public string LowerIndex;

		public string UpperIndex;

		public ArrayType()
		{
		}

		public object GetDataTypes()
		{
			object obj = base.ChoiceChild(this.baseElement_0, out this.baseElement_0, Class0.smethod_0());
			return obj;
		}

		public string GetLowerIndex()
		{
			return base.GetAttribute(this.LowerIndex, out this.LowerIndex, "LowerIndex");
		}

		public string GetUpperIndex()
		{
			return base.GetAttribute(this.UpperIndex, out this.UpperIndex, "UpperIndex");
		}
	}
}