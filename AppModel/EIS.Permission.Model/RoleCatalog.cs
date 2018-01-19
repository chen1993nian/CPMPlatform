using System;
using System.ComponentModel;

namespace EIS.Permission.Model
{
	[Serializable]
	public class RoleCatalog
	{
		private string __AutoID;

		private string __UserName;

		private string __OrgCode;

		private DateTime __CreateTime;

		private DateTime __UpdateTime;

		private int __IsDel;

		private string _CatalogName;

		private string _CatalogWBS;

		private string _CatalogPWBS;

		[Description("_AutoID")]
		public string _AutoID
		{
			get
			{
				return this.__AutoID;
			}
			set
			{
				this.__AutoID = value;
			}
		}

		[Description("_CreateTime")]
		public DateTime _CreateTime
		{
			get
			{
				return this.__CreateTime;
			}
			set
			{
				this.__CreateTime = value;
			}
		}

		[Description("_IsDel")]
		public int _IsDel
		{
			get
			{
				return this.__IsDel;
			}
			set
			{
				this.__IsDel = value;
			}
		}

		[Description("_OrgCode")]
		public string _OrgCode
		{
			get
			{
				return this.__OrgCode;
			}
			set
			{
				this.__OrgCode = value;
			}
		}

		[Description("_UpdateTime")]
		public DateTime _UpdateTime
		{
			get
			{
				return this.__UpdateTime;
			}
			set
			{
				this.__UpdateTime = value;
			}
		}

		[Description("_UserName")]
		public string _UserName
		{
			get
			{
				return this.__UserName;
			}
			set
			{
				this.__UserName = value;
			}
		}

		[Description("CatalogName")]
		public string CatalogName
		{
			get
			{
				return this._CatalogName;
			}
			set
			{
				this._CatalogName = value;
			}
		}

		[Description("CatalogPWBS")]
		public string CatalogPWBS
		{
			get
			{
				return this._CatalogPWBS;
			}
			set
			{
				this._CatalogPWBS = value;
			}
		}

		[Description("CatalogWBS")]
		public string CatalogWBS
		{
			get
			{
				return this._CatalogWBS;
			}
			set
			{
				this._CatalogWBS = value;
			}
		}

		public RoleCatalog()
		{
		}
	}
}