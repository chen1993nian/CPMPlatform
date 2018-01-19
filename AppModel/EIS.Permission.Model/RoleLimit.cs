using System;
using System.ComponentModel;

namespace EIS.Permission.Model
{
	[Serializable]
	public class RoleLimit
	{
		private string __AutoID;

		private string __UserName;

		private string __OrgCode;

		private DateTime __CreateTime;

		private DateTime __UpdateTime;

		private int __IsDel;

		private string _FunID;

		private string _RoleID;

		private string _FunLimit;

		private int _DeptLimit;

		private int _IsDealOwen;

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

		[Description("DeptLimit")]
		public int DeptLimit
		{
			get
			{
				return this._DeptLimit;
			}
			set
			{
				this._DeptLimit = value;
			}
		}

		[Description("FunID")]
		public string FunID
		{
			get
			{
				return this._FunID;
			}
			set
			{
				this._FunID = value;
			}
		}

		[Description("FunLimit")]
		public string FunLimit
		{
			get
			{
				return this._FunLimit;
			}
			set
			{
				this._FunLimit = value;
			}
		}

		[Description("IsDealOwen")]
		public int IsDealOwen
		{
			get
			{
				return this._IsDealOwen;
			}
			set
			{
				this._IsDealOwen = value;
			}
		}

		[Description("RoleID")]
		public string RoleID
		{
			get
			{
				return this._RoleID;
			}
			set
			{
				this._RoleID = value;
			}
		}

		public RoleLimit()
		{
		}
	}
}