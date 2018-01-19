using System;
using EIS.AppModel;
namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class TypeDeclaration : BaseElementWithIdName
	{
		private BaseElement baseElement_0;

		private string string_2;

		private BaseElement baseElement_1;

		public TypeDeclaration()
		{
		}

		public object GetDataTypes()
		{
			object obj = base.ChoiceChild(this.baseElement_0, out this.baseElement_0, Class0.smethod_0());
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