using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.Web.ModelLib.Model
{
	public class DaPosition : AppModelBase
	{
		public string Note
		{
			get;
			set;
		}

		public int OrderId
		{
			get;
			set;
		}

		public string PositionName
		{
			get;
			set;
		}

		public string PositionPId
		{
			get;
			set;
		}

		public DaPosition()
		{
		}

		public DaPosition(UserContext user)
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