using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class FolderInfo : AppModelBase
	{
		public string FolderName
		{
			get;
			set;
		}

		public string FolderPID
		{
			get;
			set;
		}

		public string FolderWBS
		{
			get;
			set;
		}

		public string Inherit
		{
			get;
			set;
		}

		public int OrderID
		{
			get;
			set;
		}

		public string ShareLimit
		{
			get;
			set;
		}

		public string ShareUser
		{
			get;
			set;
		}

		public string ShareUserId
		{
			get;
			set;
		}

		public FolderInfo()
		{
		}

		public FolderInfo(UserContext user)
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