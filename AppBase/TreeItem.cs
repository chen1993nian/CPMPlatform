using AjaxPro;
using System;
using System.Collections.Generic;
//using System.Web.Script.Serialization;

namespace EIS.AppBase
{
	public class TreeItem
	{
		private bool _hasChildren = false;

		public string id;

		public string text;

		public string @value;

		public bool showcheck;

		public bool isexpand;

		public int checkstate;

		public List<TreeItem> ChildNodes;

		public bool complete;

		public string tag;

		public bool hasChildren
		{
			get
			{
				bool flag;
				if (!this._hasChildren)
				{
					flag = (this.ChildNodes == null ? false : this.ChildNodes.Count > 0);
				}
				else
				{
					flag = true;
				}
				return flag;
			}
			set
			{
				this._hasChildren = value;
			}
		}

		public TreeItem()
		{
			this.showcheck = true;
			this.checkstate = 0;
			this.complete = true;
			this.isexpand = false;
		}

		public List<TreeItem> Add(TreeItem ti)
		{
			if (this.ChildNodes == null)
			{
				this.ChildNodes = new List<TreeItem>();
			}
			this.ChildNodes.Add(ti);
			return this.ChildNodes;
		}
        	public string ToJsonString()
		{
			string str = JavaScriptSerializer.Serialize(this);
			return string.Concat("var treedata=[", str, "];");
		}

            public string ToJsonString(bool var)
            {
                string str = JavaScriptSerializer.Serialize(this);
                return string.Concat("[", str, "]");
            }
        //public string ToJsonString()
        //{
        //    string str = (new JavaScriptSerializer()).Serialize(this);
        //    return string.Concat("var treedata=[", str, "];");
        //}

        //public string ToJsonString(bool var)
        //{
        //    string str = (new JavaScriptSerializer()).Serialize(this);
        //    return string.Concat("[", str, "]");
        //}
	}
}