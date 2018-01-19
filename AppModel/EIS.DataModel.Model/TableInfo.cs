using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class TableInfo : AppModelBase
	{
		public int ArchiveState
		{
			get;
			set;
		}

		public string CompiledHtml
		{
			get;
			set;
		}

		public string ConnectionId
		{
			get;
			set;
		}

		public int DataLog
		{
			get;
			set;
		}

		public string DataLogTmpl
		{
			get;
			set;
		}

		public int DeleteMode
		{
			get;
			set;
		}

		public string DetailHtml
		{
			get;
			set;
		}

		public string DetailSQL
		{
			get;
			set;
		}

		public string EditMode
		{
			get;
			set;
		}

		public string EditScriptBlock
		{
			get;
			set;
		}

		public string FormHtml
		{
			get;
			set;
		}

		public string FormHtml2
		{
			get;
			set;
		}

		public string FormWidth
		{
			get;
			set;
		}

		public string FormWidthStyle
		{
			get
			{
				string str;
				if (string.IsNullOrEmpty(this.FormWidth))
				{
					str = "";
				}
				else if (this.FormWidth.EndsWith("%"))
				{
					str = string.Concat("width:", this.FormWidth, ";");
				}
				else if (!this.FormWidth.EndsWith("px"))
				{
					str = (!Regex.IsMatch(this.FormWidth, "\\d$") ? string.Concat("width:", this.FormWidth, ";") : string.Concat("width:", this.FormWidth, "px;"));
				}
				else
				{
					str = string.Concat("width:", this.FormWidth, ";");
				}
				return str;
			}
		}

		public int InitRows
		{
			get;
			set;
		}

		public string ListPreProcessFn
		{
			get;
			set;
		}

		public string ListScriptBlock
		{
			get;
			set;
		}

		public string ListSQL
		{
			get;
			set;
		}

		public string OrderField
		{
			get;
			set;
		}

		public int PageRecCount
		{
			get;
			set;
		}

		public string ParentName
		{
			get;
			set;
		}

		public string PrintHtml
		{
			get;
			set;
		}

		public string QueryHtml
		{
			get;
			set;
		}

		public int ShowState
		{
			get;
			set;
		}

		public string TableCat
		{
			get;
			set;
		}

		public string TableName
		{
			get;
			set;
		}

		public string TableNameCn
		{
			get;
			set;
		}

		public int TableType
		{
			get;
			set;
		}

        public Boolean IsShowFldPlaceholder
        {
            get;
            set;
        }

		public TableInfo()
		{
		}
	}
}