using System;
using System.Runtime.CompilerServices;

namespace EIS.WorkFlow.Model
{
	[Serializable]
	public class UserTask
	{
		private string _advice = "";

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

		public string AgentId
		{
			get;
			set;
		}

		public string DealAction
		{
			get;
			set;
		}

		public string DealAdvice
		{
			get
			{
				return this._advice;
			}
			set
			{
				if (value.Length > 1000)
				{
					throw new Exception("处理意见超过字数过多");
				}
				this._advice = value;
			}
		}

		public DateTime? DealTime
		{
			get;
			set;
		}

		public string DealType
		{
			get;
			set;
		}

		public string DealUser
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

		public string InstanceId
		{
			get;
			set;
		}

		public string IsAssign
		{
			get;
			set;
		}

		public string IsRead
		{
			get;
			set;
		}

		public string IsShare
		{
			get;
			set;
		}

		public string Memo
		{
			get;
			set;
		}

		public string OwnerId
		{
			get;
			set;
		}

		public string PositionId
		{
			get;
			set;
		}

		public string PositionName
		{
			get;
			set;
		}

		public DateTime? ReadTime
		{
			get;
			set;
		}

		public string RecIds
		{
			get;
			set;
		}

		public string RecNames
		{
			get;
			set;
		}

		public string TaskId
		{
			get;
			set;
		}

		public string TaskState
		{
			get;
			set;
		}

		public string UserTaskId
		{
			get;
			set;
		}

		public UserTask()
		{
			this.IsAssign = "0";
		}
	}
}