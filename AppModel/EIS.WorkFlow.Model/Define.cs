using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EIS.WorkFlow.Model
{
	[Serializable]
	public class Define
	{
		public readonly static string F__AutoID;

		public readonly static string F__UserName;

		public readonly static string F__OrgCode;

		public readonly static string F__CreateTime;

		public readonly static string F__UpdateTime;

		public readonly static string F__IsDel;

		public readonly static string F_CatalogCode;

		public readonly static string F_WorkflowName;

		public readonly static string F_Version;

		public readonly static string F_Description;

		public readonly static string F_Enabled;

		public readonly static string F_AppNames;

		public readonly static string F_XPDL;

		public readonly static string F_OrderId;

		public readonly static string F_BizType;

		public DateTime _CreateTime
		{
			get;
			set;
		}

		public int _IsDel
		{
			get;
			set;
		}

		public string _OrgCode
		{
			get;
			set;
		}

		public DateTime _UpdateTime
		{
			get;
			set;
		}

		[Description("创建人ID")]
		public string _UserName
		{
			get;
			set;
		}

		public string AppNames
		{
			get;
			set;
		}

		public string BizType
		{
			get;
			set;
		}

		public string CatalogCode
		{
			get;
			set;
		}

		public string CompanyId
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public string Enabled
		{
			get;
			set;
		}

		public DateTime? EndDate
		{
			get;
			set;
		}

		public int OrderId
		{
			get;
			set;
		}

		public string PrintStyle
		{
			get;
			set;
		}

		public string RememberUser
		{
			get;
			set;
		}

		public DateTime? StartDate
		{
			get;
			set;
		}

		public string Version
		{
			get;
			set;
		}

		public string WorkflowCode
		{
			get;
			set;
		}

		[Description("流程ID")]
		public string WorkflowId
		{
			get;
			set;
		}

		public string WorkflowName
		{
			get;
			set;
		}

		public string XPDL
		{
			get;
			set;
		}

		static Define()
		{
			Define.F__AutoID = "_AutoID";
			Define.F__UserName = "_UserName";
			Define.F__OrgCode = "_OrgCode";
			Define.F__CreateTime = "_CreateTime";
			Define.F__UpdateTime = "_UpdateTime";
			Define.F__IsDel = "_IsDel";
			Define.F_CatalogCode = "CatalogCode";
			Define.F_WorkflowName = "WorkflowName";
			Define.F_Version = "Version";
			Define.F_Description = "Description";
			Define.F_Enabled = "Enabled";
			Define.F_AppNames = "AppNames";
			Define.F_XPDL = "XPDL";
			Define.F_OrderId = "OrderId";
			Define.F_BizType = "BizType";
		}

		public Define()
		{
		}
	}
}