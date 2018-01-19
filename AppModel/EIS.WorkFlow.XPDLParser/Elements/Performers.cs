using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Performers : BaseElement
	{
		private string string_0;

		private IList ilist_0;

		public Performers()
		{
		}

		public IList GetPerformers()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(Performer), "Performer");
			return children;
		}

		public string GetStrategy()
		{
			return base.GetAttribute(this.string_0, out this.string_0, "Strategy");
		}
	}
}