using EIS.AppBase;
using EIS.DataAccess;
using EIS.Web.ModelLib.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.Web.ModelLib.Access
{
	public class _DaArchive
	{
		private DbTransaction dbTransaction_0 = null;

		public _DaArchive()
		{
		}

		public _DaArchive(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(DaArchive model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_OA_DA_ArchiveInfo (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\r\n\t\t\t\t\tFwCode,\r\n\t\t\t\t\tFwTitle,\r\n\t\t\t\t\tGdName,\r\n\t\t\t\t\tGdTime,\r\n\r\n\t\t\t\t\tLocation,\r\n\t\t\t\t\tLocationId,\r\n\t\t\t\t\tCatalog,\r\n\t\t\t\t\tCatalogId,\r\n\t\t\t\t\tAppId,\r\n\t\t\t\t\tAppName,\r\n\t\t\t\t\tInstanceId,\r\n\t\t\t\t\tWorkflowId,\r\n\r\n\t\t\t\t\tCompanyId,\r\n\t\t\t\t\tCompanyName,\r\n\t\t\t\t\tDeptId,\r\n\t\t\t\t\tDeptName\r\n\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\r\n\r\n\t\t\t\t\t@FwCode,\r\n\t\t\t\t\t@FwTitle,\r\n\t\t\t\t\t@GdName,\r\n\t\t\t\t\t@GdTime,\r\n\r\n\t\t\t\t\t@Location,\r\n\t\t\t\t\t@LocationId,\r\n\t\t\t\t\t@Catalog,\r\n\t\t\t\t\t@CatalogId,\r\n\t\t\t\t\t@AppId,\r\n\t\t\t\t\t@AppName,\r\n\t\t\t\t\t@InstanceId,\r\n\t\t\t\t\t@WorkflowId,\r\n\r\n\t\t\t\t\t@CompanyId,\r\n\t\t\t\t\t@CompanyName,\r\n\t\t\t\t\t@DeptId,\r\n\t\t\t\t\t@DeptName\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@FwCode", DbType.String, model.FwCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@FwTitle", DbType.String, model.FwTitle);
			SysDatabase.AddInParameter(sqlStringCommand, "@GdName", DbType.String, model.GdName);
			SysDatabase.AddInParameter(sqlStringCommand, "@GdTime", DbType.DateTime, model.GdTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@Location", DbType.String, model.Location);
			SysDatabase.AddInParameter(sqlStringCommand, "@LocationId", DbType.String, model.LocationId);
			SysDatabase.AddInParameter(sqlStringCommand, "@Catalog", DbType.String, model.Catalog);
			SysDatabase.AddInParameter(sqlStringCommand, "@CatalogId", DbType.String, model.CatalogId);
			SysDatabase.AddInParameter(sqlStringCommand, "@AppId", DbType.String, model.AppId);
			SysDatabase.AddInParameter(sqlStringCommand, "@AppName", DbType.String, model.AppName);
			SysDatabase.AddInParameter(sqlStringCommand, "@InstanceId", DbType.String, model.InstanceId);
			SysDatabase.AddInParameter(sqlStringCommand, "@WorkflowId", DbType.String, model.WorkflowId);
			SysDatabase.AddInParameter(sqlStringCommand, "@CompanyId", DbType.String, model.CompanyId);
			SysDatabase.AddInParameter(sqlStringCommand, "@CompanyName", DbType.String, model.CompanyName);
			SysDatabase.AddInParameter(sqlStringCommand, "@DeptId", DbType.String, model.DeptId);
			SysDatabase.AddInParameter(sqlStringCommand, "@DeptName", DbType.String, model.DeptName);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_OA_DA_ArchiveInfo ");
			stringBuilder.Append(" where _AutoID=@_AutoID ;");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_OA_DA_ArchiveInfo ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public DaArchive GetModel(string string_0)
		{
			DaArchive model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_OA_DA_ArchiveInfo ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			DataTable dataTable = SysDatabase.ExecuteTable(sqlStringCommand);
			if (dataTable.Rows.Count <= 0)
			{
				model = null;
			}
			else
			{
				model = this.GetModel(dataTable.Rows[0]);
			}
			return model;
		}

		public DaArchive GetModel(DataRow dataRow_0)
		{
			DaArchive daArchive = new DaArchive()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				daArchive._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				daArchive._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				daArchive._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			if (dataRow_0["GdTime"].ToString() != "")
			{
				daArchive.GdTime = new DateTime?(DateTime.Parse(dataRow_0["GdTime"].ToString()));
			}
			daArchive.FwCode = dataRow_0["FwCode"].ToString();
			daArchive.FwTitle = dataRow_0["FwTitle"].ToString();
			daArchive.GdName = dataRow_0["GdName"].ToString();
			daArchive.Location = dataRow_0["Location"].ToString();
			daArchive.LocationId = dataRow_0["LocationId"].ToString();
			daArchive.Catalog = dataRow_0["Catalog"].ToString();
			daArchive.CatalogId = dataRow_0["CatalogId"].ToString();
			daArchive.AppId = dataRow_0["AppId"].ToString();
			daArchive.AppName = dataRow_0["AppName"].ToString();
			daArchive.InstanceId = dataRow_0["InstanceId"].ToString();
			daArchive.WorkflowId = dataRow_0["WorkflowId"].ToString();
			daArchive.CompanyId = dataRow_0["CompanyId"].ToString();
			daArchive.CompanyName = dataRow_0["CompanyName"].ToString();
			daArchive.DeptId = dataRow_0["DeptId"].ToString();
			daArchive.DeptName = dataRow_0["DeptName"].ToString();
			return daArchive;
		}

		public DaArchive GetModelByAppInfo(string appName, string appId)
		{
			DaArchive model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_OA_DA_ArchiveInfo ");
			stringBuilder.Append(" where AppId=@AppId and AppName=@AppName ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@AppId", DbType.String, appId);
			SysDatabase.AddInParameter(sqlStringCommand, "@AppName", DbType.String, appName);
			DataTable dataTable = SysDatabase.ExecuteTable(sqlStringCommand);
			if (dataTable.Rows.Count <= 0)
			{
				model = null;
			}
			else
			{
				model = this.GetModel(dataTable.Rows[0]);
			}
			return model;
		}

		public DaArchive GetModelByInstanceId(string string_0)
		{
			DaArchive model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_OA_DA_ArchiveInfo ");
			stringBuilder.Append(" where InstanceId=@InstanceId ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@InstanceId", DbType.String, string_0);
			DataTable dataTable = SysDatabase.ExecuteTable(sqlStringCommand);
			if (dataTable.Rows.Count <= 0)
			{
				model = null;
			}
			else
			{
				model = this.GetModel(dataTable.Rows[0]);
			}
			return model;
		}

		public List<DaArchive> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<DaArchive> daArchives = new List<DaArchive>();
			stringBuilder.Append("select *  From T_OA_DA_ArchiveInfo ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				daArchives.Add(this.GetModel(row));
			}
			return daArchives;
		}

		public int Update(DaArchive model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_OA_DA_ArchiveInfo set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\r\n\t\t\t\t\tFwCode =@FwCode,\r\n\t\t\t\t\tFwTitle =@FwTitle,\r\n\t\t\t\t\tGdName =@GdName,\r\n\t\t\t\t\tGdTime =@GdTime,\r\n\t\t\t\t\tLocation =@Location,\r\n\t\t\t\t\tCatalog =@Catalog,\r\n\t\t\t\t\tCatalogId =@CatalogId,\r\n\r\n\t\t\t\t\tAppId=@AppId,\r\n\t\t\t\t\tAppName=@AppName,\r\n\t\t\t\t\tInstanceId=@InstanceId,\r\n\r\n\t\t\t\t\tCompanyId=@CompanyId,\r\n\t\t\t\t\tCompanyName=@CompanyName,\r\n\t\t\t\t\tDeptId=@DeptId,\r\n\t\t\t\t\tDeptName=@DeptName\r\n\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@FwCode", DbType.String, model.FwCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@FwTitle", DbType.String, model.FwTitle);
			SysDatabase.AddInParameter(sqlStringCommand, "@GdName", DbType.String, model.GdName);
			SysDatabase.AddInParameter(sqlStringCommand, "@GdTime", DbType.DateTime, model.GdTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@Location", DbType.String, model.Location);
			SysDatabase.AddInParameter(sqlStringCommand, "@Catalog", DbType.String, model.Catalog);
			SysDatabase.AddInParameter(sqlStringCommand, "@CatalogId", DbType.String, model.CatalogId);
			SysDatabase.AddInParameter(sqlStringCommand, "@AppId", DbType.String, model.AppId);
			SysDatabase.AddInParameter(sqlStringCommand, "@AppName", DbType.String, model.AppName);
			SysDatabase.AddInParameter(sqlStringCommand, "@InstanceId", DbType.String, model.InstanceId);
			SysDatabase.AddInParameter(sqlStringCommand, "@CompanyId", DbType.String, model.CompanyId);
			SysDatabase.AddInParameter(sqlStringCommand, "@CompanyName", DbType.String, model.CompanyName);
			SysDatabase.AddInParameter(sqlStringCommand, "@DeptId", DbType.String, model.DeptId);
			SysDatabase.AddInParameter(sqlStringCommand, "@DeptName", DbType.String, model.DeptName);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}