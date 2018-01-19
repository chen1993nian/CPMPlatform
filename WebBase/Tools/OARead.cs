using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace WebBase.JZY.Tools
{
	public class OARead : AppModelBase
	{
		public string AppID
		{
			get;
			set;
		}

		public string AppName
		{
			get;
			set;
		}

		public string EmployeeId
		{
			get;
			set;
		}

		public OARead(UserContext user)
		{
			base._AutoID = Guid.NewGuid().ToString();
			base._UserName = user.EmployeeId;
			base._OrgCode = user.DeptWbs;
			base._CreateTime = DateTime.Now;
			base._UpdateTime = DateTime.Now;
			base._IsDel = 0;
		}
	}
}