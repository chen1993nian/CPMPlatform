using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.Permission.Model
{
	[Serializable]
	public class DeptType : AppModelBase
	{
		public readonly static int GroupTypeId;

		public readonly static int CompanyTypeId;

		public readonly static int DepartmentTypeId;

		public readonly static int VirtualTypeId;

		public int OrderID
		{
			get;
			set;
		}

		public string TypeName
		{
			get;
			set;
		}

		public int TypeProp
		{
			get;
			set;
		}

		public int TypeState
		{
			get;
			set;
		}

		static DeptType()
		{
			DeptType.GroupTypeId = 0;
			DeptType.CompanyTypeId = 1;
			DeptType.DepartmentTypeId = 2;
			DeptType.VirtualTypeId = 3;
		}

		public DeptType()
		{
		}
	}
}