using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class SimulationInformation : BaseElement
	{
		private string string_0;

		private BaseElement baseElement_0;

		private string string_1;

		private static Hashtable hashtable_0;

		public SimulationInformation()
		{
			if (SimulationInformation.hashtable_0 == null)
			{
				SimulationInformation.hashtable_0 = new Hashtable()
				{
					{ "MULTIPLE", SimulationInformationInstantiation.MULTIPLE },
					{ "ONCE", SimulationInformationInstantiation.ONCE }
				};
			}
		}

		public string GetCost()
		{
			return base.GetChildText(this.string_0, out this.string_0, "Cost");
		}

		public SimulationInformationInstantiation GetInstantiation()
		{
			if (!this.IsInstantiationSpecified())
			{
				throw new ObjectNotFound("Instantiation is not specified");
			}
			return (SimulationInformationInstantiation)SimulationInformation.hashtable_0[this.string_1];
		}

		public TimeEstimation GetTimeEstimation()
		{
			TimeEstimation child = (TimeEstimation)base.GetChild(this.baseElement_0, out this.baseElement_0, "TimeEstimation");
			return child;
		}

		public bool IsInstantiationSpecified()
		{
			return base.GetAttribute(this.string_1, out this.string_1, "Instantiation") != "";
		}
	}
}