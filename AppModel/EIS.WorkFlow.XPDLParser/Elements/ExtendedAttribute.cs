using EIS.WorkFlow.XPDL.Utility;
using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class ExtendedAttribute : BaseElement
	{
		private IList ilist_0;

		private Hashtable hashtable_0 = new Hashtable();

		private string string_0;

		private string string_1;

		public ExtendedAttribute()
		{
		}

		public string GetName()
		{
			return base.GetAttribute(this.string_0, out this.string_0, "Name");
		}

		public Node GetNodeByName(string name)
		{
			Node node;
			if (this.ilist_0 == null)
			{
				this.GetNodes();
			}
			foreach (Node ilist0 in this.ilist_0)
			{
				if (!ilist0.GetName().Equals(name))
				{
					continue;
				}
				node = ilist0;
				return node;
			}
			node = null;
			return node;
		}

		public IList GetNodes()
		{
			return base.GetChildren(this.ilist_0, out this.ilist_0);
		}

		public new string GetText()
		{
			return base.GetText();
		}

		public string GetValue()
		{
			return base.GetAttribute(this.string_1, out this.string_1, "Value");
		}

		public string GetValueByName(string attrName)
		{
			string str;
			if (!this.hashtable_0.Contains(attrName))
			{
				string str1 = null;
				base.GetAttribute(str1, out str1, attrName);
				this.hashtable_0.Add(attrName, str1);
				str = str1;
			}
			else
			{
				str = this.hashtable_0[attrName].ToString();
			}
			return str;
		}
	}
}