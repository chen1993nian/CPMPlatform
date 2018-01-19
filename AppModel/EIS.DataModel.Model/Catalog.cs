using EIS.AppBase;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class Catalog : AppModelBase
	{
		[Description("CatCode")]
		public string CatCode
		{
			get;
			set;
		}

		[Description("CatName")]
		public string CatName
		{
			get;
			set;
		}

		[Description("CatOdr")]
		public int CatOdr
		{
			get;
			set;
		}

		[Description("PCatCode")]
		public string PCatCode
		{
			get;
			set;
		}

		public Catalog()
		{
		}

		public Catalog(UserContext user)
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