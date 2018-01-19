using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class SubFlow : BaseElement
	{
		private BaseElement baseElement_0;

		private string string_0;

		private string string_1;

		private static Hashtable hashtable_0;

		public SubFlow()
		{
			if (SubFlow.hashtable_0 == null)
			{
				SubFlow.hashtable_0 = new Hashtable()
				{
					{ "ASYNCHR", SubFlowExecution.ASYNCHR },
					{ "SYNCHR", SubFlowExecution.SYNCHR }
				};
			}
		}

		public ActualParameters GetActualParameters()
		{
			ActualParameters child = (ActualParameters)base.GetChild(this.baseElement_0, out this.baseElement_0, "ActualParameters");
			return child;
		}

		public string GetId()
		{
			return base.GetAttribute(this.string_0, out this.string_0, "Id");
		}

		public SubFlowExecution GetSubFlowExecution()
		{
			SubFlowExecution item = (SubFlowExecution)SubFlow.hashtable_0[base.GetAttribute(this.string_1, out this.string_1, "Execution")];
			return item;
		}

		public bool IsExecutionSpecified()
		{
			return base.GetAttribute(this.string_1, out this.string_1, "Execution") != "";
		}
	}
}