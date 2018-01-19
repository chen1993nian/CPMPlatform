using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Participants : BaseElement
	{
		private IList ilist_0;

		public Participants()
		{
		}

		public Participant GetParticipantById(string Id)
		{
			Participant participant;
			if (null == this.ilist_0)
			{
				this.GetParticipants();
			}
			IEnumerator enumerator = this.ilist_0.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Participant current = (Participant)enumerator.Current;
					if (!current.GetId().Equals(Id))
					{
						continue;
					}
					participant = current;
					return participant;
				}
				throw new ObjectNotFound(string.Concat("Participant Not Found! Id=", Id));
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return participant;
		}

		public IList GetParticipants()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(Participant), "Participant");
			return children;
		}
	}
}