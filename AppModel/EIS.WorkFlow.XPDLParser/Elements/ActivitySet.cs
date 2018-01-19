using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class ActivitySet : BaseElement
	{
		private BaseElement baseElement_0;

		private BaseElement baseElement_1;

		private string string_0;

		public ActivitySet()
		{
		}

		public Activities GetActivities()
		{
			Activities child = (Activities)base.GetChild(this.baseElement_0, out this.baseElement_0, "Activities");
			return child;
		}

		public string GetId()
		{
			return base.GetAttribute(this.string_0, out this.string_0, "Id");
		}

		public Transitions GetTransitions()
		{
			Transitions child = (Transitions)base.GetChild(this.baseElement_1, out this.baseElement_1, "Transitions");
			return child;
		}
	}
}