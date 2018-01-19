using EIS.DataAccess;
using EIS.WorkFlow.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.WorkFlow.Access
{
	public class _Catalog
	{
		private DbTransaction _tran = null;

		public _Catalog()
		{
		}

		public int Add(Catalog model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_WF_Catalog (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tCatalogName,\r\n\t\t\t\t\tCatalogCode,\r\n\t\t\t\t\tPCode,\r\n\t\t\t\t\tOrgId,\r\n\t\t\t\t\tIsDisp,\r\n\t\t\t\t\tOrderId\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@CatalogName,\r\n\t\t\t\t\t@CatalogCode,\r\n\t\t\t\t\t@PCode,\r\n\t\t\t\t\t@OrgId,\r\n\t\t\t\t\t@IsDisp,\r\n\t\t\t\t\t@OrderId\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model.CatalogId);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "CatalogName", DbType.String, model.CatalogName);
			SysDatabase.AddInParameter(sqlStringCommand, "CatalogCode", DbType.String, model.CatalogCode);
			SysDatabase.AddInParameter(sqlStringCommand, "PCode", DbType.String, model.PCode);
			SysDatabase.AddInParameter(sqlStringCommand, "OrgId", DbType.String, model.OrgId);
			SysDatabase.AddInParameter(sqlStringCommand, "IsDisp", DbType.Int32, model.IsDisp);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderId", DbType.Int32, model.OrderId);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int Delete(string key)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_WF_Catalog ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, key);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_WF_Catalog ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by orderId");
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public Catalog GetModel(string key)
		{
			Catalog model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_WF_Catalog ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, key);
			Catalog catalog = new Catalog();
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

		public Catalog GetModel(DataRow dr)
		{
			Catalog catalog = new Catalog()
			{
				CatalogId = dr["_AutoID"].ToString(),
				_UserName = dr["_UserName"].ToString(),
				_OrgCode = dr["_OrgCode"].ToString()
			};
			if (dr["_CreateTime"].ToString() != "")
			{
				catalog._CreateTime = DateTime.Parse(dr["_CreateTime"].ToString());
			}
			if (dr["_UpdateTime"].ToString() != "")
			{
				catalog._UpdateTime = DateTime.Parse(dr["_UpdateTime"].ToString());
			}
			if (dr["_IsDel"].ToString() != "")
			{
				catalog._IsDel = int.Parse(dr["_IsDel"].ToString());
			}
			catalog.CatalogName = dr["CatalogName"].ToString();
			catalog.CatalogCode = dr["CatalogCode"].ToString();
			catalog.PCode = dr["PCode"].ToString();
			catalog.OrgId = dr["OrgId"].ToString();
			if (dr["IsDisp"].ToString() != "")
			{
				catalog.IsDisp = int.Parse(dr["IsDisp"].ToString());
			}
			if (dr["OrderId"].ToString() != "")
			{
				catalog.OrderId = int.Parse(dr["OrderId"].ToString());
			}
			return catalog;
		}

		public Catalog GetModelByCode(string code)
		{
			Catalog model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_WF_Catalog ");
			stringBuilder.Append(" where CatalogCode=@CatalogCode ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "CatalogCode", DbType.String, code);
			Catalog catalog = new Catalog();
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

		public List<Catalog> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Catalog> catalogs = new List<Catalog>();
			stringBuilder.Append("select *  FROM T_E_WF_Catalog ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by orderId");
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				catalogs.Add(this.GetModel(row));
			}
			return catalogs;
		}

		public string GetNewCode(string pnodewbs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select max(CatalogCode) ");
			stringBuilder.Append(" from T_E_WF_Catalog ");
			if (pnodewbs.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where PCode = '", pnodewbs, "'"));
			}
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
			int num = 0;
			num = (obj != DBNull.Value ? Convert.ToInt32(obj.ToString().Substring(pnodewbs.Length + 1)) + 1 : 1);
			return string.Concat(pnodewbs, ".", num.ToString());
		}

		public int GetNewOrd(string pnodewbs)
		{
			int num;
			string str = string.Concat("select max(OrderId)+1 from T_E_WF_Catalog where PCode = '", pnodewbs, "'");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(str.ToString());
			object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
			num = (obj != DBNull.Value ? int.Parse(obj.ToString()) : 1);
			return num;
		}

		public int GetSonCountByCode(string catCode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  count(*) from T_E_WF_Catalog ");
			stringBuilder.Append(string.Concat(" where CatalogCode like '", catCode, ".%' "));
			object obj = SysDatabase.ExecuteScalar(stringBuilder.ToString());
			return Convert.ToInt32(obj);
		}

		public int Update(Catalog model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_WF_Catalog set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tCatalogName=@CatalogName,\r\n\t\t\t\t\tCatalogCode=@CatalogCode,\r\n\t\t\t\t\tPCode=@PCode,\r\n\t\t\t\t\tOrgId=@OrgId,\r\n\t\t\t\t\tIsDisp=@IsDisp,\r\n\t\t\t\t\tOrderId=@OrderId\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model.CatalogId);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "CatalogName", DbType.String, model.CatalogName);
			SysDatabase.AddInParameter(sqlStringCommand, "CatalogCode", DbType.String, model.CatalogCode);
			SysDatabase.AddInParameter(sqlStringCommand, "PCode", DbType.String, model.PCode);
			SysDatabase.AddInParameter(sqlStringCommand, "OrgId", DbType.String, model.OrgId);
			SysDatabase.AddInParameter(sqlStringCommand, "IsDisp", DbType.Int32, model.IsDisp);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderId", DbType.Int32, model.OrderId);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}
	}
}