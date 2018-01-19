using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.Permission.Model
{
	[Serializable]
	public class Role : AppModelBase
	{
		public string CatalogID
		{
			get;
			set;
		}

		public string CompanyId
		{
			get;
			set;
		}

		public int OrderID
		{
			get;
			set;
		}

		public string RoleName
		{
			get;
			set;
		}

		public string RoleNotes
		{
			get;
			set;
		}

		public int RoleState
		{
			get;
			set;
		}

		public string RoleType
		{
			get;
			set;
		}

		public string SearchScope
		{
			get;
			set;
		}

		public Role()
		{
		}
	}
}