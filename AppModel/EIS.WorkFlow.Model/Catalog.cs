using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EIS.WorkFlow.Model
{
	[Serializable]
	public class Catalog
	{
		public readonly static string F__AutoID;

		public readonly static string F__UserName;

		public readonly static string F__OrgCode;

		public readonly static string F__CreateTime;

		public readonly static string F__UpdateTime;

		public readonly static string F__IsDel;

		public readonly static string F_CatalogName;

		public readonly static string F_CatalogCode;

		public readonly static string F_OrgWbs;

		public readonly static string F_OrgId;

		public readonly static string F_IsDisp;

		public readonly static string F_OrderId;

		[Description("创建时间")]
		public DateTime _CreateTime
		{
			get;
			set;
		}

		[Description("是否删除")]
		public int _IsDel
		{
			get;
			set;
		}

		[Description("所在部门WBS")]
		public string _OrgCode
		{
			get;
			set;
		}

		[Description("最后修改时间")]
		public DateTime _UpdateTime
		{
			get;
			set;
		}

		[Description("创建人")]
		public string _UserName
		{
			get;
			set;
		}

		[Description("分类编码")]
		public string CatalogCode
		{
			get;
			set;
		}

		[Description("分类Id")]
		public string CatalogId
		{
			get;
			set;
		}

		[Description("分类名称")]
		public string CatalogName
		{
			get;
			set;
		}

		[Description("是否可见")]
		public int IsDisp
		{
			get;
			set;
		}

		[Description("排序")]
		public int OrderId
		{
			get;
			set;
		}

		[Description("所属组织Id")]
		public string OrgId
		{
			get;
			set;
		}

		[Description("PCode")]
		public string PCode
		{
			get;
			set;
		}

		static Catalog()
		{
			Catalog.F__AutoID = "_AutoID";
			Catalog.F__UserName = "_UserName";
			Catalog.F__OrgCode = "_OrgCode";
			Catalog.F__CreateTime = "_CreateTime";
			Catalog.F__UpdateTime = "_UpdateTime";
			Catalog.F__IsDel = "_IsDel";
			Catalog.F_CatalogName = "CatalogName";
			Catalog.F_CatalogCode = "CatalogCode";
			Catalog.F_OrgWbs = "OrgWbs";
			Catalog.F_OrgId = "OrgId";
			Catalog.F_IsDisp = "IsDisp";
			Catalog.F_OrderId = "OrderId";
		}

		public Catalog()
		{
		}
	}
}