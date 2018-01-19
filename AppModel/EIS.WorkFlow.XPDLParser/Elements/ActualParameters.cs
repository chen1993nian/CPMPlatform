using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class ActualParameters : BaseElement
	{
		private IList ilist_0;

		public ActualParameters()
		{
		}

		public IList GetActualParameters()
		{
			if (this.ilist_0 == null)
			{
				this.ilist_0 = base.GetChildrenTexts();
			}
			return this.ilist_0;
		}
	}
}