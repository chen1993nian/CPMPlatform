using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.Permission.Model
{
	[Serializable]
	public class RoleEmployee : AppModelBase
	{
		public string EmployeeID
		{
			get;
			set;
		}

		public int RoleEmployeeType
		{
			get;
			set;
		}

		public string RoleID
		{
			get;
			set;
		}

		public RoleEmployee()
		{
		}

		public RoleEmployee(UserContext user)
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