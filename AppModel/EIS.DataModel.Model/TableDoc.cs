using System;
using System.ComponentModel;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class TableDoc
	{
		public readonly static string F__AutoID;

		public readonly static string F__UserName;

		public readonly static string F__OrgCode;

		public readonly static string F__CreateTime;

		public readonly static string F__UpdateTime;

		public readonly static string F__IsDel;

		public readonly static string F_TableName;

		public readonly static string F_ReportDoc;

		public readonly static string F_DesignDoc;

		public readonly static string F_InstructionDoc;

		private string __AutoID;

		private string __UserName;

		private string __OrgCode;

		private DateTime __CreateTime;

		private DateTime __UpdateTime;

		private int __IsDel;

		private string _TableName;

		private string _ReportDoc;

		private string _DesignDoc;

		private string _InstructionDoc;

		[Description("_AutoID")]
		public string _AutoID
		{
			get
			{
				return this.__AutoID;
			}
			set
			{
				this.__AutoID = value;
			}
		}

		[Description("_CreateTime")]
		public DateTime _CreateTime
		{
			get
			{
				return this.__CreateTime;
			}
			set
			{
				this.__CreateTime = value;
			}
		}

		[Description("_IsDel")]
		public int _IsDel
		{
			get
			{
				return this.__IsDel;
			}
			set
			{
				this.__IsDel = value;
			}
		}

		[Description("_OrgCode")]
		public string _OrgCode
		{
			get
			{
				return this.__OrgCode;
			}
			set
			{
				this.__OrgCode = value;
			}
		}

		[Description("_UpdateTime")]
		public DateTime _UpdateTime
		{
			get
			{
				return this.__UpdateTime;
			}
			set
			{
				this.__UpdateTime = value;
			}
		}

		[Description("_UserName")]
		public string _UserName
		{
			get
			{
				return this.__UserName;
			}
			set
			{
				this.__UserName = value;
			}
		}

		[Description("DesignDoc")]
		public string DesignDoc
		{
			get
			{
				return this._DesignDoc;
			}
			set
			{
				this._DesignDoc = value;
			}
		}

		[Description("InstructionDoc")]
		public string InstructionDoc
		{
			get
			{
				return this._InstructionDoc;
			}
			set
			{
				this._InstructionDoc = value;
			}
		}

		[Description("ReportDoc")]
		public string ReportDoc
		{
			get
			{
				return this._ReportDoc;
			}
			set
			{
				this._ReportDoc = value;
			}
		}

		[Description("TableName")]
		public string TableName
		{
			get
			{
				return this._TableName;
			}
			set
			{
				this._TableName = value;
			}
		}

		static TableDoc()
		{
			TableDoc.F__AutoID = "_AutoID";
			TableDoc.F__UserName = "_UserName";
			TableDoc.F__OrgCode = "_OrgCode";
			TableDoc.F__CreateTime = "_CreateTime";
			TableDoc.F__UpdateTime = "_UpdateTime";
			TableDoc.F__IsDel = "_IsDel";
			TableDoc.F_TableName = "TableName";
			TableDoc.F_ReportDoc = "ReportDoc";
			TableDoc.F_DesignDoc = "DesignDoc";
			TableDoc.F_InstructionDoc = "InstructionDoc";
		}

		public TableDoc()
		{
		}
	}
}