using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class AppTree : AppModelBase
	{
		public string CatCode
		{
			get;
			set;
		}

		public string Connection
		{
			get;
			set;
		}

		public string ConnectionId
		{
			get;
			set;
		}

		public string RootPara
		{
			get;
			set;
		}

		public string RootValue
		{
			get;
			set;
		}

		public string TreeName
		{
			get;
			set;
		}

		public string TreeScript
		{
			get;
			set;
		}

		public string TreeSQL
		{
			get;
			set;
		}

		public AppTree()
		{
		}

		public AppTree(UserContext user)
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