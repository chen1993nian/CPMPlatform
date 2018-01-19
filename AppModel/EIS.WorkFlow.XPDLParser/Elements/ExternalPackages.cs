using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class ExternalPackages : BaseElement
	{
		private IList ilist_0;

		public ExternalPackages()
		{
		}

		public ExternalPackage GetExternalPackageByHref(string href)
		{
			ExternalPackage externalPackage;
			if (null == this.ilist_0)
			{
				this.GetExternalPackages();
			}
			IEnumerator enumerator = this.ilist_0.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ExternalPackage current = (ExternalPackage)enumerator.Current;
					if (!current.GetHref().Equals(href))
					{
						continue;
					}
					externalPackage = current;
					return externalPackage;
				}
				throw new ObjectNotFound(string.Concat("ExternalPackage Not Found! href=", href));
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return externalPackage;
		}

		public IList GetExternalPackages()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(ExternalPackage), "ExternalPackage");
			return children;
		}
	}
}