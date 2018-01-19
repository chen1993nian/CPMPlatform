using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.WebBase.ModelLib.Model
{
	public class BBSReply : AppModelBase
	{
		public string ReferId
		{
			get;
			set;
		}

		public int ReplyOrder
		{
			get;
			set;
		}

		public string ReplyText
		{
			get;
			set;
		}

		public string TopicId
		{
			get;
			set;
		}

		public BBSReply()
		{
		}

		public BBSReply(UserContext user)
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