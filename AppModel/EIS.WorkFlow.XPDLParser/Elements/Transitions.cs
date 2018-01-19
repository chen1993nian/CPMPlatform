using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Transitions : BaseElement
	{
		private IList ilist_0;

		public Transitions()
		{
		}

		public Transition GetTransitionById(string Id)
		{
			Transition transition;
			if (null == this.ilist_0)
			{
				this.GetTransitions();
			}
			IEnumerator enumerator = this.ilist_0.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transition current = (Transition)enumerator.Current;
					if (!current.GetId().Equals(Id))
					{
						continue;
					}
					transition = current;
					return transition;
				}
				throw new ObjectNotFound(string.Concat("Transition Not Found! Id=", Id));
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return transition;
		}

		public IList GetTransitions()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(Transition), "Transition");
			return children;
		}
	}
}