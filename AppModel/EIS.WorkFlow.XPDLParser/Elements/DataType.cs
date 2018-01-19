using System;
using EIS.AppModel;
namespace EIS.WorkFlow.XPDLParser.Elements
{
	[Serializable]
	public class DataType : BaseElement
	{
		private BaseElement Item;

		public DataType()
		{
		}

		public object GetDataTypes()
		{
			object obj = base.ChoiceChild(this.Item, out this.Item, Class0.smethod_0());
			return obj;
		}
	}
}