using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.Permission.Model
{
	[Serializable]
	public class WebCataLog : AppModelBase
	{
		public readonly static string BizSystem;

		public readonly static string AdminSystem;

		public string WebId
		{
			get;
			set;
		}

		public string WebName
		{
			get;
			set;
		}

		public string WebType
		{
			get;
			set;
		}


        public string ShowState
        {
            get;
            set;
        }


		static WebCataLog()
		{
			WebCataLog.BizSystem = "业务系统";
			WebCataLog.AdminSystem = "管理系统";
		}

		public WebCataLog()
		{
		}
	}
}