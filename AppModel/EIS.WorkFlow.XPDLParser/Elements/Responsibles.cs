using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Responsibles : BaseElement
	{
		private IList ilist_0;

		public Responsibles()
		{
		}

		public IList GetResponsibles()
		{
			if (this.ilist_0 == null)
			{
				this.ilist_0 = base.GetChildrenTexts();
			}
			return this.ilist_0;
		}
	}
}