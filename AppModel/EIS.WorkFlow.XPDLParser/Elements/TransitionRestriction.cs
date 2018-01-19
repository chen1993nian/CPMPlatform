using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class TransitionRestriction : BaseElement
	{
		private BaseElement baseElement_0;

		private BaseElement baseElement_1;

		public TransitionRestriction()
		{
		}

		public Join GetJoin()
		{
			Join child = (Join)base.GetChild(this.baseElement_0, out this.baseElement_0, "Join");
			return child;
		}

		public Split GetSplit()
		{
			Split child = (Split)base.GetChild(this.baseElement_1, out this.baseElement_1, "Split");
			return child;
		}
	}
}