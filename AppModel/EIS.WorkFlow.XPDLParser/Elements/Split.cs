using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Split : BaseElement
	{
		private BaseElement baseElement_0;

		private string string_0;

		private static Hashtable hashtable_0;

		public Split()
		{
			if (Split.hashtable_0 == null)
			{
				Split.hashtable_0 = new Hashtable()
				{
					{ "AND", SplitType.AND },
					{ "XOR", SplitType.XOR }
				};
			}
		}

		public SplitType GetSplitType()
		{
			if (!this.IsTypeSpecified())
			{
				throw new ObjectNotFound("SplitType is not specified");
			}
			return (SplitType)Split.hashtable_0[this.string_0];
		}

		public TransitionRefs GetTransitionRefs()
		{
			TransitionRefs child = (TransitionRefs)base.GetChild(this.baseElement_0, out this.baseElement_0, "TransitionRefs");
			return child;
		}

		public bool IsTypeSpecified()
		{
			return base.GetAttribute(this.string_0, out this.string_0, "Type") != "";
		}
	}
}