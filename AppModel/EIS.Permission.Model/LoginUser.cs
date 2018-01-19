using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.Permission.Model
{
	[Serializable]
	public class LoginUser : AppModelBase
	{
		public int CanMultiLogin
		{
			get;
			set;
		}

		public string CompanyId
		{
			get;
			set;
		}

		public int IsLock
		{
			get;
			set;
		}

		public int IsOnline
		{
			get;
			set;
		}

		public string LockReason
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

		public int LoginType
		{
			get;
			set;
		}

        public string Remark
        {
            get;
            set;
        }
        public string EmployeeName
        {
            get;
            set;
        }
        public string Cellphone
        {
            get;
            set;
        }

		public LoginUser()
		{
		}

        public string PhotoId
        {
            get;
            set;
        }


	}
}