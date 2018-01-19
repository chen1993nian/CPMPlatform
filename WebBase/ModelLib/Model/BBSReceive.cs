using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.WebBase.ModelLib.Model
{
	public class BBSReceive : AppModelBase
	{
		public int IsRead
		{
			get;
			set;
		}

		public DateTime? ReadTime
		{
			get;
			set;
		}

		public string RecId
		{
			get;
			set;
		}

		public string SubjectId
		{
			get;
			set;
		}

		public string SubScribe
		{
			get;
			set;
		}

		public BBSReceive()
		{
		}

		public BBSReceive(UserContext user)
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