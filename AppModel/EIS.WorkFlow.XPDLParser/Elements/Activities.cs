using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Activities : BaseElement
	{
		private IList ilist_0;

		public Activities()
		{
		}

		public IList GetActivities()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(Activity), "Activity");
			return children;
		}

		public Activity GetActivityById(string Id)
		{
			Activity activity;
			if (null == this.ilist_0)
			{
				this.GetActivities();
			}
			IEnumerator enumerator = this.ilist_0.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Activity current = (Activity)enumerator.Current;
					if (!current.GetId().Equals(Id))
					{
						continue;
					}
					activity = current;
					return activity;
				}
				throw new ObjectNotFound(string.Concat("Activity Not Found! Id=", Id));
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return activity;
		}
	}
}