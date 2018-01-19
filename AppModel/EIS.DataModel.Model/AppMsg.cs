using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class AppMsg : AppModelBase
	{
		public string Content
		{
			get;
			set;
		}

		public string MsgType
		{
			get;
			set;
		}

		public string MsgUrl
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

		public string ReplyId
		{
			get;
			set;
		}

		public string Sender
		{
			get;
			set;
		}

		public DateTime? SendTime
		{
			get;
			set;
		}

		public string Title
		{
			get;
			set;
		}

		public AppMsg()
		{
		}

		public AppMsg(UserContext user)
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