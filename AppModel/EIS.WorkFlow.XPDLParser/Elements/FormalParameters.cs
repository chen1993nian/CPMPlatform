using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class FormalParameters : BaseElement
	{
		private IList ilist_0;

		public FormalParameters()
		{
		}

		public FormalParameter GetFormalParameterById(string Id)
		{
			FormalParameter formalParameter;
			if (null == this.ilist_0)
			{
				this.GetFormalParameters();
			}
			IEnumerator enumerator = this.ilist_0.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					FormalParameter current = (FormalParameter)enumerator.Current;
					if (!current.GetId().Equals(Id))
					{
						continue;
					}
					formalParameter = current;
					return formalParameter;
				}
				throw new ObjectNotFound(string.Concat("FormalParameter Not Found! Id=", Id));
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return formalParameter;
		}

		public IList GetFormalParameters()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(FormalParameter), "FormalParameter");
			return children;
		}
	}
}