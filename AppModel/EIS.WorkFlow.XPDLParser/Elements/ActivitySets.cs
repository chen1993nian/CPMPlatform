using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class ActivitySets : BaseElement
	{
		private IList ilist_0;

		public ActivitySets()
		{
		}

		public ActivitySet GetActivitySetById(string Id)
		{
			ActivitySet activitySet;
			if (null == this.ilist_0)
			{
				this.GetActivitySets();
			}
			IEnumerator enumerator = this.ilist_0.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ActivitySet current = (ActivitySet)enumerator.Current;
					if (!current.GetId().Equals(Id))
					{
						continue;
					}
					activitySet = current;
					return activitySet;
				}
				throw new ObjectNotFound(string.Concat("ActivitySet Not Found! Id=", Id));
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return activitySet;
		}

		public IList GetActivitySets()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(ActivitySet), "ActivitySet");
			return children;
		}
	}
}