using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Join : BaseElement
	{
		private string string_0;

		private static Hashtable hashtable_0;

		private BaseElement baseElement_0;

		public Join()
		{
			if (Join.hashtable_0 == null)
			{
				Join.hashtable_0 = new Hashtable()
				{
					{ "AND", JoinType.AND },
					{ "XOR", JoinType.XOR }
				};
			}
		}

		public JoinType GetJoinType()
		{
			if (!this.IsTypeSpecified())
			{
				throw new ObjectNotFound("JoinType is not specified");
			}
			return (JoinType)Join.hashtable_0[this.string_0];
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