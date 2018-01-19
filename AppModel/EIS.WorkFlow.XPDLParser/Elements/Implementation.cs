using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Implementation : BaseElement
	{
		private IList ilist_0;

		private BaseElement baseElement_0;

		private static string[] string_0;

		static Implementation()
		{
			Implementation.string_0 = new string[] { "Tool", "SubFlow", "No" };
		}

		public Implementation()
		{
		}

		public object GetImplementation()
		{
			object baseElement0;
			if (!this.GetImplementationType().Equals(typeof(Tool)))
			{
				baseElement0 = this.baseElement_0;
			}
			else
			{
				baseElement0 = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(Tool), "Tool");
			}
			return baseElement0;
		}

		public Type GetImplementationType()
		{
			Type type = base.ChoiceChild(this.baseElement_0, out this.baseElement_0, Implementation.string_0).GetType();
			return type;
		}
	}
}