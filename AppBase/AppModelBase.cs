using System;
using System.Runtime.CompilerServices;

namespace EIS.AppBase
{
	[Serializable]
	public abstract class AppModelBase
	{
		public string _AutoID
		{
			get;
			set;
		}

		public string _CompanyID
		{
			get;
			set;
		}

		public DateTime _CreateTime
		{
			get;
			set;
		}

		public int _IsDel
		{
			get;
			set;
		}

		public string _OrgCode
		{
			get;
			set;
		}

		public DateTime _UpdateTime
		{
			get;
			set;
		}

		public string _UserName
		{
			get;
			set;
		}

		public string _WFState
		{
			get;
			set;
		}

		protected AppModelBase()
		{
		}
	}
}