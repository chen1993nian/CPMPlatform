using EIS.DataAccess;
using EIS.Permission.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.Permission.Access
{
	public class _RoleCatalog
	{
		private DbTransaction dbTransaction_0 = null;

		public _RoleCatalog()
		{
		}

		public _RoleCatalog(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(RoleCatalog model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Org_RoleCatalog (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tCatalogName,\r\n\t\t\t\t\tCatalogWBS,\r\n\t\t\t\t\tCatalogPWBS\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@CatalogName,\r\n\t\t\t\t\t@CatalogWBS,\r\n\t\t\t\t\t@CatalogPWBS\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "CatalogName", DbType.String, model.CatalogName);
			SysDatabase.AddInParameter(sqlStringCommand, "CatalogWBS", DbType.String, model.CatalogWBS);
			SysDatabase.AddInParameter(sqlStringCommand, "CatalogPWBS", DbType.String, model.CatalogPWBS);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Org_RoleCatalog ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Org_RoleCatalog ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public List<RoleCatalog> GetListByWbs(string catWbs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<RoleCatalog> roleCatalogs = new List<RoleCatalog>();
			stringBuilder.Append("select *  FROM T_E_Org_RoleCatalog ");
			stringBuilder.Append(string.Concat(" where CatalogWBS like '", catWbs, "%'"));
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				roleCatalogs.Add(this.GetModel(row));
			}
			return roleCatalogs;
		}

		public RoleCatalog GetModel(string string_0)
		{
			RoleCatalog model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Org_RoleCatalog ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			RoleCatalog roleCatalog = new RoleCatalog();
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

		public RoleCatalog GetModel(DataRow dataRow_0)
		{
			RoleCatalog roleCatalog = new RoleCatalog()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				roleCatalog._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				roleCatalog._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				roleCatalog._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			roleCatalog.CatalogName = dataRow_0["CatalogName"].ToString();
			roleCatalog.CatalogWBS = dataRow_0["CatalogWBS"].ToString();
			roleCatalog.CatalogPWBS = dataRow_0["CatalogPWBS"].ToString();
			return roleCatalog;
		}

		public RoleCatalog GetModelByWbs(string catWbs)
		{
			RoleCatalog model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Org_RoleCatalog ");
			stringBuilder.Append(" where CatalogWBS=@CatalogWBS ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "CatalogWBS", DbType.String, catWbs);
			RoleCatalog roleCatalog = new RoleCatalog();
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

		public List<RoleCatalog> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<RoleCatalog> roleCatalogs = new List<RoleCatalog>();
			stringBuilder.Append("select *  FROM T_E_Org_RoleCatalog ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				roleCatalogs.Add(this.GetModel(row));
			}
			return roleCatalogs;
		}

		public string GetNewCode(string pnodewbs)
		{
			string str;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select max(right(CatalogWBS,4))+1 ");
			stringBuilder.Append(" FROM T_E_Org_RoleCatalog ");
			stringBuilder.Append(string.Concat(" where CatalogPWBS='", pnodewbs, "'"));
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
			string str1 = "";
			str1 = (obj == DBNull.Value ? "0001" : obj.ToString());
			if (int.Parse(str1) <= 9999)
			{
				int num = Convert.ToInt32(str1);
				str = string.Concat(pnodewbs, num.ToString("d4"));
			}
			else
			{
				str = "";
			}
			return str;
		}

		public int Update(RoleCatalog model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Org_RoleCatalog set \r\n\t\t\t\t\t_UserName=@_UserName,\r\n\t\t\t\t\t_OrgCode=@_OrgCode,\r\n\t\t\t\t\t_CreateTime=@_CreateTime,\r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tCatalogName=@CatalogName,\r\n\t\t\t\t\tCatalogWBS=@CatalogWBS,\r\n\t\t\t\t\tCatalogPWBS=@CatalogPWBS\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "CatalogName", DbType.String, model.CatalogName);
			SysDatabase.AddInParameter(sqlStringCommand, "CatalogWBS", DbType.String, model.CatalogWBS);
			SysDatabase.AddInParameter(sqlStringCommand, "CatalogPWBS", DbType.String, model.CatalogPWBS);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}