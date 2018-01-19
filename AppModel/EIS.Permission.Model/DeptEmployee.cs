using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.Permission.Model
{
	[Serializable]
	public class DeptEmployee : AppModelBase
	{
		public string CompanyID
		{
			get;
			set;
		}

		public int DataRegion
		{
			get;
			set;
		}

		public string DealType
		{
			get;
			set;
		}

		public int DeptEmployeeType
		{
			get;
			set;
		}

		public string DeptID
		{
			get;
			set;
		}

		public string DeptName
		{
			get;
			set;
		}

		public string EmployeeID
		{
			get;
			set;
		}

		public string EmployeeName
		{
			get;
			set;
		}

		public int IsDefault
		{
			get;
			set;
		}

		public int OrderID
		{
			get;
			set;
		}

		public string PositionId
		{
			get;
			set;
		}

		public string PositionName
		{
			get;
			set;
		}
    
        

		public DeptEmployee()
		{
		}

		public override bool Equals(object object_0)
		{
			bool flag;
			flag = (!(object_0 is DeptEmployee) ? false : base._AutoID.Equals((object_0 as DeptEmployee)._AutoID));
			return flag;
		}

		public override int GetHashCode()
		{
			return base._AutoID.GetHashCode();
		}
	}
}