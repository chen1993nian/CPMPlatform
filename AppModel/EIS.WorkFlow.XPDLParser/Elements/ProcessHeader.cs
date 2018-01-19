using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class ProcessHeader : BaseElement
	{
		private string string_0;

		private string string_1;

		private string string_2;

		private string string_3;

		private string string_4;

		private string string_5;

		private BaseElement baseElement_0;

		private string string_6;

		private static Hashtable hashtable_0;

		public ProcessHeader()
		{
			if (ProcessHeader.hashtable_0 == null)
			{
				ProcessHeader.hashtable_0 = new Hashtable()
				{
					{ "D", ProcessHeaderDurationUnit.D },
					{ "h", ProcessHeaderDurationUnit.const_3 },
					{ "M", ProcessHeaderDurationUnit.M },
					{ "m", ProcessHeaderDurationUnit.const_4 },
					{ "s", ProcessHeaderDurationUnit.const_5 },
					{ "Y", ProcessHeaderDurationUnit.Y }
				};
			}
		}

		public string GetCreated()
		{
			return base.GetChildText(this.string_0, out this.string_0, "Created");
		}

		public string GetDescription()
		{
			return base.GetChildText(this.string_1, out this.string_1, "Description");
		}

		public ProcessHeaderDurationUnit GetDurationUnit()
		{
			ProcessHeaderDurationUnit item = (ProcessHeaderDurationUnit)ProcessHeader.hashtable_0[base.GetAttribute(this.string_6, out this.string_6, "DurationUnit")];
			return item;
		}

		public string GetLimit()
		{
			return base.GetChildText(this.string_3, out this.string_3, "Limit");
		}

		public string GetPriority()
		{
			return base.GetChildText(this.string_2, out this.string_2, "Priority");
		}

		public TimeEstimation GetTimeEstimation()
		{
			TimeEstimation child = (TimeEstimation)base.GetChild(this.baseElement_0, out this.baseElement_0, "TimeEstimation");
			return child;
		}

		public string GetValidFrom()
		{
			return base.GetChildText(this.string_4, out this.string_4, "ValidFrom");
		}

		public string GetValidTo()
		{
			return base.GetChildText(this.string_5, out this.string_5, "ValidTo");
		}

		public bool IsDurationUnitSpecified()
		{
			return base.GetAttribute(this.string_6, out this.string_6, "DurationUnits") != "";
		}
	}
}