using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.Permission.Model
{
	[Serializable]
	public class FunNode : AppModelBase
	{
		public string DispState
		{
			get;
			set;
		}

		public string DispStyle
		{
			get;
			set;
		}

		public string Encrypt
		{
			get;
			set;
		}

		public string FunName
		{
			get;
			set;
		}

		public string FunPWBS
		{
			get;
			set;
		}

		public string FunWBS
		{
			get;
			set;
		}

		public string IsExpand
		{
			get;
			set;
		}

		public string LinkFile
		{
			get;
			set;
		}

		public int LinkType
		{
			get;
			set;
		}

		public int OrderId
		{
			get;
			set;
		}

		public string WebID
		{
			get;
			set;
		}

		public FunNode()
		{
		}

		public FunNode(UserContext user)
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
			this.Encrypt = "是";
			this.IsExpand = "否";
		}
	}
}