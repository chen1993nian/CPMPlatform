using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class EnumerationType : BaseElement
	{
		private IList ilist_0;

		public EnumerationType()
		{
		}

		public EnumerationValue GetEnumerationValueByName(string name)
		{
			EnumerationValue enumerationValue;
			if (null == this.ilist_0)
			{
				this.GetEnumerationValues();
			}
			IEnumerator enumerator = this.ilist_0.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					EnumerationValue current = (EnumerationValue)enumerator.Current;
					if (!current.GetName().Equals(name))
					{
						continue;
					}
					enumerationValue = current;
					return enumerationValue;
				}
				throw new ObjectNotFound(string.Concat("EnumerationValue Not Found! Name=", name));
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return enumerationValue;
		}

		public IList GetEnumerationValues()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(EnumerationValue), "EnumerationValue");
			return children;
		}
	}
}