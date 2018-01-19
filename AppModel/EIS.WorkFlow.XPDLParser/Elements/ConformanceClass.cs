using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class ConformanceClass : BaseElement
	{
		private string string_0;

		private static Hashtable hashtable_0;

		public ConformanceClass()
		{
			if (ConformanceClass.hashtable_0 == null)
			{
				ConformanceClass.hashtable_0 = new Hashtable()
				{
					{ "FULL_BLOCKED", ConformanceClassGraphConformance.FULL_BLOCKED },
					{ "LOOP_BLOCKED", ConformanceClassGraphConformance.LOOP_BLOCKED },
					{ "NON_BLOCKED", ConformanceClassGraphConformance.NON_BLOCKED }
				};
			}
		}

		public ConformanceClassGraphConformance GetGraphConformance()
		{
			if (!this.IsGraphConformanceSpecified())
			{
				throw new ObjectNotFound("GraphConformance is not specified");
			}
			return (ConformanceClassGraphConformance)ConformanceClass.hashtable_0[this.string_0];
		}

		public bool IsGraphConformanceSpecified()
		{
			return base.GetAttribute(this.string_0, out this.string_0, "GraphConformance") != "";
		}
	}
}