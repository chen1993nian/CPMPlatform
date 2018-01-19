using System;
using EIS.AppModel;
namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class ListType : BaseElement
	{
		private BaseElement baseElement_0;

		public ListType()
		{
		}

		public object GetDataTypes()
		{
			object obj = base.ChoiceChild(this.baseElement_0, out this.baseElement_0, Class0.smethod_0());
			return obj;
		}
	}
}