using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Applications : BaseElement
	{
		private IList ilist_0;

		public Applications()
		{
		}

		public Application GetApplicationById(string Id)
		{
			Application application;
			if (null == this.ilist_0)
			{
				this.GetApplications();
			}
			IEnumerator enumerator = this.ilist_0.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Application current = (Application)enumerator.Current;
					if (!current.GetId().Equals(Id))
					{
						continue;
					}
					application = current;
					return application;
				}
				throw new ObjectNotFound(string.Concat("Applicaton Not Found! Id=", Id));
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return application;
		}

		public IList GetApplications()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(Application), "Application");
			return children;
		}
	}
}