using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class AppWorkDay : AppModelBase
	{
		public string CompanyId
		{
			get;
			set;
		}

		public string DayType
		{
			get;
			set;
		}

		public DateTime EndTime
		{
			get;
			set;
		}

		public string Holiday
		{
			get;
			set;
		}

		public string IsWorkDay
		{
			get;
			set;
		}

		public DateTime StartTime
		{
			get;
			set;
		}

		public DateTime WorkDate
		{
			get;
			set;
		}

		public AppWorkDay()
		{
			base._AutoID = Guid.NewGuid().ToString();
			base._UserName = "";
			base._OrgCode = "";
			base._IsDel = 0;
			base._CreateTime = DateTime.Now;
			base._UpdateTime = DateTime.Now;
		}

		public AppWorkDay(UserContext user)
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