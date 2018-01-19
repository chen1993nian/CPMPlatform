using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class TransitionRestrictions : BaseElement
	{
		private IList ilist_0;

		public TransitionRestrictions()
		{
		}

		public IList GetTransitionRestrictions()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(TransitionRestriction), "TransitionRestriction");
			return children;
		}
	}
}