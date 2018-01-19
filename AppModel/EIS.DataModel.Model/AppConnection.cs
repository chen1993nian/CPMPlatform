using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class AppConnection : AppModelBase
	{
		public string ConnString
		{
			get;
			set;
		}

		public string DSName
		{
			get;
			set;
		}

		public string DSType
		{
			get;
			set;
		}

		public string State
		{
			get;
			set;
		}

		public AppConnection()
		{
		}

		public AppConnection(UserContext user)
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