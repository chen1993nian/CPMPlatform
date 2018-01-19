using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class TransitionRefs : BaseElement
	{
		private IList ilist_0;

		public TransitionRefs()
		{
		}

		public TransitionRef GetTransitionRefById(string Id)
		{
			TransitionRef transitionRef;
			if (null == this.ilist_0)
			{
				this.GetTransitionRefs();
			}
			IEnumerator enumerator = this.ilist_0.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TransitionRef current = (TransitionRef)enumerator.Current;
					if (!current.GetId().Equals(Id))
					{
						continue;
					}
					transitionRef = current;
					return transitionRef;
				}
				throw new ObjectNotFound(string.Concat("TransitionRef Not Found! Id=", Id));
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return transitionRef;
		}

		public IList GetTransitionRefs()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(TransitionRef), "TransitionRef");
			return children;
		}
	}
}