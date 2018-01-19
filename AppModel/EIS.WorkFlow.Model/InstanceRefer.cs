using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.WorkFlow.Model
{
	[Serializable]
	public class InstanceRefer : AppModelBase
	{
		public string InstanceId
		{
			get;
			set;
		}

		public int OrderID
		{
			get;
			set;
		}

		public string ReferId
		{
			get;
			set;
		}

		public string ReferName
		{
			get;
			set;
		}

		public InstanceRefer()
		{
		}

		public InstanceRefer(UserContext user)
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