using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class ParticipantType : BaseElement
	{
		private string string_0;

		private static Hashtable hashtable_0;

		public ParticipantType()
		{
			if (ParticipantType.hashtable_0 == null)
			{
				ParticipantType.hashtable_0 = new Hashtable()
				{
					{ "RESOURCE_SET", ParticipantTypeType.RESOURCE_SET },
					{ "RESOURCE", ParticipantTypeType.RESOURCE },
					{ "ROLE", ParticipantTypeType.ROLE },
					{ "ORGANIZATIONAL_UNIT", ParticipantTypeType.ORGANIZATIONAL_UNIT },
					{ "HUMAN", ParticipantTypeType.HUMAN },
					{ "SYSTEM", ParticipantTypeType.SYSTEM }
				};
			}
		}

		public ParticipantTypeType GetParticipantTypeType()
		{
			ParticipantTypeType item = (ParticipantTypeType)ParticipantType.hashtable_0[base.GetAttribute(this.string_0, out this.string_0, "Type")];
			return item;
		}
	}
}