using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class RecordType : BaseElement
	{
		private IList ilist_0;

		public RecordType()
		{
		}

		public IList GetMembers()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(Member), "Member");
			return children;
		}
	}
}