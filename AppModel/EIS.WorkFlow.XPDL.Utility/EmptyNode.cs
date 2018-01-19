using System;
using System.Collections;

namespace EIS.WorkFlow.XPDL.Utility
{
	[Serializable]
	internal class EmptyNode : Node
	{
		private IDictionary attrs = new Hashtable();

		private IList children = new ArrayList();

		public EmptyNode()
		{
		}

		public string GetAttribute(string attrName)
		{
			return "";
		}

		public IDictionary GetAttributes()
		{
			return this.attrs;
		}

		public IList GetChildren()
		{
			return this.children;
		}

		public string GetName()
		{
			return "";
		}

		public string GetText()
		{
			return "";
		}

		public bool IsEmpty()
		{
			return true;
		}

		public IList SelectNodes(string xPath)
		{
			return this.children;
		}

		public Node SelectSingleNode(string xPath)
		{
			return this;
		}

		public string ToXML()
		{
			return "";
		}
	}
}