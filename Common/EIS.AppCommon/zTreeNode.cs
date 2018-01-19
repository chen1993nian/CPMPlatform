using AjaxPro;
using System;
using System.Collections.Generic;

namespace EIS.AppCommon
{
	public class zTreeNode
	{
		public string id;

		public string name;

		public string @value;

		public string tag;

		public string icon;

		public bool open;

		public bool check;

		public List<zTreeNode> children;

		public bool isParent;

		public zTreeNode()
		{
			this.open = false;
		}

		public void Add(zTreeNode ti)
		{
			if (this.children == null)
			{
				this.children = new List<zTreeNode>();
			}
			this.children.Add(ti);
		}

		public string ToJsonString(bool var)
		{
			string str = JavaScriptSerializer.Serialize(this);
			return string.Concat("[", str, "]");
		}
	}
}