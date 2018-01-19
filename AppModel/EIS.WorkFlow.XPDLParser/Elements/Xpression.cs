using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Xpression : BaseElement
	{
		private IList ilist_0;

		public Xpression()
		{
		}

		public IList GetNodes()
		{
			return base.GetChildren(this.ilist_0, out this.ilist_0);
		}

		public new string GetText()
		{
			return base.GetText();
		}
	}
}