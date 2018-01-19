using System;
using System.Runtime.CompilerServices;

namespace EIS.WorkFlow.Model
{
	[Serializable]
	public class Instance
	{
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

		public string _UserName
		{
			get;
			set;
		}

		public string AppId
		{
			get;
			set;
		}

		public string AppName
		{
			get;
			set;
		}

		public string BasePath
		{
			get;
			set;
		}

		public string CompanyId
		{
			get;
			set;
		}

		public string CompanyName
		{
			get;
			set;
		}

		public DateTime? Deadline
		{
			get;
			set;
		}

		public string DeptId
		{
			get;
			set;
		}

		public string DeptName
		{
			get;
			set;
		}

		public string EmployeeName
		{
			get;
			set;
		}

		public DateTime? FinishTime
		{
			get;
			set;
		}

		public string Importance
		{
			get;
			set;
		}

		public string InstanceId
		{
			get;
			set;
		}

		public string InstanceName
		{
			get;
			set;
		}

		public string InstanceState
		{
			get;
			set;
		}

		public bool NeedUpdate
		{
			get;
			set;
		}

		public string ProcessId
		{
			get;
			set;
		}

		public string Remark
		{
			get;
			set;
		}

		public string WorkflowId
		{
			get;
			set;
		}

		public string XPDL
		{
			get;
			set;
		}

		public string XPDLPath
		{
			get;
			set;
		}

		public Instance()
		{
		}
	}
}