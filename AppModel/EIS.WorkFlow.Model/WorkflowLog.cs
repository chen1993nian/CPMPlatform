using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.WorkFlow.Model
{
	[Serializable]
	public class WorkflowLog : AppModelBase
	{
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

		public string EmpName
		{
			get;
			set;
		}

		public string LogContent
		{
			get;
			set;
		}

		public DateTime? LogTime
		{
			get;
			set;
		}

		public WorkflowLog()
		{
		}

		public WorkflowLog(UserContext user)
		{
			if (string.IsNullOrEmpty(base._AutoID))
			{
				base._AutoID = Guid.NewGuid().ToString();
			}
			base._UserName = user.EmployeeId;
			base._OrgCode = user.DeptWbs;
			base._IsDel = 0;
			base._CreateTime = DateTime.Now;
			base._UpdateTime = DateTime.Now;
		}
	}
}