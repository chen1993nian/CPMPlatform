using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.Web.ModelLib.Model
{
	public class Meeting : AppModelBase
	{
		public DateTime EndTime
		{
			get;
			set;
		}

		public string HyAddr
		{
			get;
			set;
		}

		public string HyDept
		{
			get;
			set;
		}

		public string HyJbr
		{
			get;
			set;
		}

		public string HyName
		{
			get;
			set;
		}

		public string HyState
		{
			get;
			set;
		}

		public int IsAllDayEvent
		{
			get;
			set;
		}

		public string JbrTel
		{
			get;
			set;
		}

		public DateTime StartTime
		{
			get;
			set;
		}

		public Meeting()
		{
		}

		public Meeting(UserContext user)
		{
			base._AutoID = Guid.NewGuid().ToString();
			base._UserName = user.EmployeeId;
			base._OrgCode = user.DeptWbs;
			base._IsDel = 0;
			base._CreateTime = DateTime.Now;
			base._UpdateTime = DateTime.Now;
		}
	}
}