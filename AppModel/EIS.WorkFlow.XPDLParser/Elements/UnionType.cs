using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class UnionType : BaseElement
	{
		private IList ilist_0;

		public UnionType()
		{
		}

		public IList GetMembers()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(Member), "Member");
			return children;
		}
	}
}