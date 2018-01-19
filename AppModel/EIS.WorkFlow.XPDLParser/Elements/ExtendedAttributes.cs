using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class ExtendedAttributes : BaseElement
	{
		private IList ilist_0;

		public ExtendedAttributes()
		{
		}

		public ExtendedAttribute GetExtendedAttributeByName(string name)
		{
			ExtendedAttribute extendedAttribute;
			if (null == this.ilist_0)
			{
				this.GetExtendedAttributes();
			}
			foreach (ExtendedAttribute ilist0 in this.ilist_0)
			{
				if (!ilist0.GetName().Equals(name))
				{
					continue;
				}
				extendedAttribute = ilist0;
				return extendedAttribute;
			}
			extendedAttribute = null;
			return extendedAttribute;
		}

		public IList GetExtendedAttributes()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(ExtendedAttribute), "ExtendedAttribute");
			return children;
		}
	}
}