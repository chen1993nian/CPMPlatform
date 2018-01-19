using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class WorkflowProcess : BaseElementWithIdName
	{
		private BaseElement baseElement_0;

		private BaseElement baseElement_1;

		private BaseElement baseElement_2;

		private BaseElement baseElement_3;

		private BaseElement baseElement_4;

		private BaseElement baseElement_5;

		private BaseElement baseElement_6;

		private BaseElement baseElement_7;

		private BaseElement baseElement_8;

		private BaseElement baseElement_9;

		private string string_2;

		private static Hashtable hashtable_0;

		public WorkflowProcess()
		{
			if (WorkflowProcess.hashtable_0 == null)
			{
				WorkflowProcess.hashtable_0 = new Hashtable()
				{
					{ "PUBLIC", WorkflowProcessAccessLevel.PUBLIC },
					{ "PRIVATE", WorkflowProcessAccessLevel.PRIVATE }
				};
			}
		}

		public WorkflowProcessAccessLevel GetAccessLevel()
		{
			WorkflowProcessAccessLevel item = (WorkflowProcessAccessLevel)WorkflowProcess.hashtable_0[base.GetAttribute(this.string_2, out this.string_2, "AccessLevel")];
			return item;
		}

		public Activities GetActivities()
		{
			Activities child = (Activities)base.GetChild(this.baseElement_7, out this.baseElement_7, "Activities");
			return child;
		}

		public ActivitySets GetActivitySets()
		{
			ActivitySets child = (ActivitySets)base.GetChild(this.baseElement_6, out this.baseElement_6, "ActivitySets");
			return child;
		}

		public Applications GetApplications()
		{
			Applications child = (Applications)base.GetChild(this.baseElement_5, out this.baseElement_5, "Applications");
			return child;
		}

		public DataFields GetDataFields()
		{
			DataFields child = (DataFields)base.GetChild(this.baseElement_3, out this.baseElement_3, "DataFields");
			return child;
		}

		public ExtendedAttributes GetExtendedAttributes()
		{
			ExtendedAttributes child = (ExtendedAttributes)base.GetChild(this.baseElement_9, out this.baseElement_9, "ExtendedAttributes");
			return child;
		}

		public FormalParameters GetFormalParameters()
		{
			FormalParameters child = (FormalParameters)base.GetChild(this.baseElement_2, out this.baseElement_2, "FormalParameters");
			return child;
		}

		public Participants GetParticipants()
		{
			Participants child = (Participants)base.GetChild(this.baseElement_4, out this.baseElement_4, "Participants");
			return child;
		}

		public ProcessHeader GetProcessHeader()
		{
			ProcessHeader child = (ProcessHeader)base.GetChild(this.baseElement_0, out this.baseElement_0, "ProcessHeader");
			return child;
		}

		public RedefinableHeader GetRedefinableHeader()
		{
			RedefinableHeader child = (RedefinableHeader)base.GetChild(this.baseElement_1, out this.baseElement_1, "RedefinableHeader");
			return child;
		}

		public Transitions GetTransitions()
		{
			Transitions child = (Transitions)base.GetChild(this.baseElement_8, out this.baseElement_8, "Transitions");
			return child;
		}

		public bool IsAccessLevelSpecified()
		{
			return base.GetAttribute(this.string_2, out this.string_2, "AccessLevel") != "";
		}
	}
}