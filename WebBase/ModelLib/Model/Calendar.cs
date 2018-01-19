using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.Web.ModelLib.Model
{
	public class Calendar : AppModelBase
	{
		public string AttendeeNames
		{
			get;
			set;
		}

		public string Attendees
		{
			get;
			set;
		}

		public int CalendarType
		{
			get;
			set;
		}

		public string Category
		{
			get;
			set;
		}

		public string CategoryName
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public string EmpId
		{
			get;
			set;
		}

		public string EmpName
		{
			get;
			set;
		}

		public DateTime EndTime
		{
			get;
			set;
		}

		public bool HasAttachment
		{
			get;
			set;
		}

		public int InstanceType
		{
			get;
			set;
		}

		public int IsAllDayEvent
		{
			get;
			set;
		}

		public string Location
		{
			get;
			set;
		}

		public int? MasterId
		{
			get;
			set;
		}

		public string OtherAttendee
		{
			get;
			set;
		}

		public string RecurringRule
		{
			get;
			set;
		}

		public DateTime StartTime
		{
			get;
			set;
		}

		public string Subject
		{
			get;
			set;
		}

		public Calendar()
		{
		}

		public Calendar(UserContext user)
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