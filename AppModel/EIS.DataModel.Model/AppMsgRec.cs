using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class AppMsgRec : AppModelBase
	{
		public int IsRead
		{
			get;
			set;
		}

		public string MsgId
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

		public AppMsgRec()
		{
		}

		public AppMsgRec(UserContext user)
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