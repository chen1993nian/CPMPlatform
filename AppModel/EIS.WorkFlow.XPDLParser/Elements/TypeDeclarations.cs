using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class TypeDeclarations : BaseElement
	{
		private IList ilist_0;

		public TypeDeclarations()
		{
		}

		public TypeDeclaration GetTypeDeclarationById(string Id)
		{
			TypeDeclaration typeDeclaration;
			if (null == this.ilist_0)
			{
				this.GetTypeDeclarations();
			}
			IEnumerator enumerator = this.ilist_0.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TypeDeclaration current = (TypeDeclaration)enumerator.Current;
					if (!current.GetId().Equals(Id))
					{
						continue;
					}
					typeDeclaration = current;
					return typeDeclaration;
				}
				throw new ObjectNotFound(string.Concat("TypeDeclaration Not Found! Id=", Id));
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return typeDeclaration;
		}

		public IList GetTypeDeclarations()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(TypeDeclaration), "TypeDeclaration");
			return children;
		}
	}
}