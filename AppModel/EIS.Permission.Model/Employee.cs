using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.Permission.Model
{
	[Serializable]
	public class Employee : AppModelBase
	{
		public string Address
		{
			get;
			set;
		}

		public string BasePath
		{
			get;
			set;
		}

		public DateTime? Birthday
		{
			get;
			set;
		}

		public string Cellphone
		{
			get;
			set;
		}

		public string City
		{
			get;
			set;
		}

		public string DeId
		{
			get;
			set;
		}

		public int DeOrderID
		{
			get;
			set;
		}

		public string DeptName
		{
			get;
			set;
		}

		public string DeptWbs
		{
			get;
			set;
		}

		public int DeType
		{
			get;
			set;
		}

		public string EMail
		{
			get;
			set;
		}

		public string EmployeeCode
		{
			get;
			set;
		}

		public string EmployeeName
		{
			get;
			set;
		}

		public string EmployeeState
		{
			get;
			set;
		}

		public string EmployeeType
		{
			get;
			set;
		}

		public string GraduateSchool
		{
			get;
			set;
		}

		public int HideMobile
		{
			get;
			set;
		}

		public string Homephone
		{
			get;
			set;
		}

		public string IdCard
		{
			get;
			set;
		}

		public string IsLocked
		{
			get;
			set;
		}

		public DateTime? LastLoginTime
		{
			get;
			set;
		}

		public string LockReason
		{
			get;
			set;
		}

		public int LoginCount
		{
			get;
			set;
		}

		public string LoginName
		{
			get;
			set;
		}

		public string LoginPass
		{
			get;
			set;
		}

		public string Marriage
		{
			get;
			set;
		}

		public string Nationality
		{
			get;
			set;
		}

		public string Officephone
		{
			get;
			set;
		}

		public int OrderID
		{
			get;
			set;
		}

		public string OutList
		{
			get;
			set;
		}

		public string PhotoId
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

		public string ReAuthPass
		{
			get;
			set;
		}

		public string ReAuthType
		{
			get;
			set;
		}

		public string Remark
		{
			get;
			set;
		}

		public string Sex
		{
			get;
			set;
		}

		public string SID
		{
			get;
			set;
		}

		public string SignId
		{
			get;
			set;
		}

		public int SysUser
		{
			get;
			set;
		}

		public string UserType
		{
			get;
			set;
		}

		public string ZipCode
		{
			get;
			set;
		}
        /// <summary>
        /// Ä¬ÈÏµÇÂ½¼¶±ð
        /// </summary>
        public string DefaultLogin
        {
            get;
            set;
        }

		public Employee()
		{
			this.HideMobile = 0;
		}
	}
}