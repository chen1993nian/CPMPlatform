using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Participant : BaseElementWithIdName
	{
		private BaseElement baseElement_0;

		private string string_2;

		private BaseElement baseElement_1;

		private BaseElement baseElement_2;

		public Participant()
		{
		}

		public string GetDescription()
		{
			return base.GetChildText(this.string_2, out this.string_2, "Description");
		}

		public ExtendedAttributes GetExtendedAttributes()
		{
			ExtendedAttributes child = (ExtendedAttributes)base.GetChild(this.baseElement_2, out this.baseElement_2, "ExtendedAttributes");
			return child;
		}

		public ExternalReference GetExternalReference()
		{
			ExternalReference child = (ExternalReference)base.GetChild(this.baseElement_1, out this.baseElement_1, "ExternalReference");
			return child;
		}

		public ParticipantType GetParticipantType()
		{
			ParticipantType child = (ParticipantType)base.GetChild(this.baseElement_0, out this.baseElement_0, "ParticipantType");
			return child;
		}
	}
}