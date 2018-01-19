using EIS.WorkFlow.XPDL.Utility;
using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class SchemaType : BaseElement
	{
		private IList ilist_0;

		public SchemaType()
		{
		}

		public Node GetNodeByName(string name)
		{
			Node node;
			if (this.ilist_0 == null)
			{
				this.GetNodes();
			}
			IEnumerator enumerator = this.ilist_0.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Node current = (Node)enumerator.Current;
					if (!current.GetName().Equals(name))
					{
						continue;
					}
					node = current;
					return node;
				}
				throw new ObjectNotFound(string.Concat("Node Not Found! Name = ", name));
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return node;
		}

		public IList GetNodes()
		{
			return base.GetChildren(this.ilist_0, out this.ilist_0);
		}
	}
}