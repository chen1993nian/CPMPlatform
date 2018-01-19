using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.Web.ModelLib.Model
{
	public class DaArchive : AppModelBase
	{
		public string AppId
		{
			get;
			set;
		}

		public string AppName
		{
			get;
			set;
		}

		public string Catalog
		{
			get;
			set;
		}

		public string CatalogId
		{
			get;
			set;
		}

		public string CompanyId
		{
			get;
			set;
		}

		public string CompanyName
		{
			get;
			set;
		}

		public string DATCode
		{
			get;
			set;
		}

		public string DATName
		{
			get;
			set;
		}

		public string DeptId
		{
			get;
			set;
		}

		public string DeptName
		{
			get;
			set;
		}

		public string FwCode
		{
			get;
			set;
		}

		public string FwTitle
		{
			get;
			set;
		}

		public string GBTCode
		{
			get;
			set;
		}

		public string GBTName
		{
			get;
			set;
		}

		public string GdName
		{
			get;
			set;
		}

		public DateTime? GdTime
		{
			get;
			set;
		}

		public string InstanceId
		{
			get;
			set;
		}

		public string Location
		{
			get;
			set;
		}

		public string LocationId
		{
			get;
			set;
		}

		public string ProjectWBSCode
		{
			get;
			set;
		}

		public string ProjectWBSName
		{
			get;
			set;
		}

		public string WorkflowId
		{
			get;
			set;
		}

		public DaArchive()
		{
		}

		public DaArchive(UserContext user)
		{
			base._AutoID = Guid.NewGuid().ToString();
			base._UserName = user.EmployeeId;
			base._OrgCode = user.DeptWbs;
			base._IsDel = 0;
			base._CreateTime = DateTime.Now;
			base._UpdateTime = DateTime.Now;
		}
	}
}