using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.WebBase.ModelLib.Model
{
	public class BBSTopic : AppModelBase
	{
		public string AttachId
		{
			get;
			set;
		}

		public string BBSType
		{
			get;
			set;
		}

		public string BizId
		{
			get;
			set;
		}

		public string BizName
		{
			get;
			set;
		}

		public string Content
		{
			get;
			set;
		}

		public string DeptName
		{
			get;
			set;
		}

		public string EmpName
		{
			get;
			set;
		}

		public string Enable
		{
			get;
			set;
		}

		public DateTime? EndTime
		{
			get;
			set;
		}

		public DateTime? LastUpdateTime
		{
			get;
			set;
		}

		public string NmEnable
		{
			get;
			set;
		}

		public int ReplyCount
		{
			get;
			set;
		}

		public DateTime? StartTime
		{
			get;
			set;
		}

		public string State
		{
			get;
			set;
		}

		public string Title
		{
			get;
			set;
		}

		public string ToDeptId
		{
			get;
			set;
		}

		public string ToDeptName
		{
			get;
			set;
		}

		public string ToUserId
		{
			get;
			set;
		}

		public string ToUserName
		{
			get;
			set;
		}

		public BBSTopic()
		{
		}

		public BBSTopic(UserContext user)
		{
			base._AutoID = Guid.NewGuid().ToString();
			base._UserName = user.EmployeeId;
			base._OrgCode = user.DeptWbs;
			base._IsDel = 0;
			base._CreateTime = DateTime.Now;
			base._UpdateTime = DateTime.Now;
			base._CompanyID = user.CompanyId;
		}
	}
}