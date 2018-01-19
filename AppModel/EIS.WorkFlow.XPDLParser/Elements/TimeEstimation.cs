using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class TimeEstimation : BaseElement
	{
		private string string_0;

		private string string_1;

		private string string_2;

		public TimeEstimation()
		{
		}

		public string GetDuration()
		{
			return base.GetChildText(this.string_2, out this.string_2, "Duration");
		}

		public string GetWaitingTime()
		{
			return base.GetChildText(this.string_0, out this.string_0, "WaitingTime");
		}

		public string GetWorkingTime()
		{
			return base.GetChildText(this.string_1, out this.string_1, "WorkingTime");
		}
	}
}