using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.DataModel.Access
{
	public class _Catalog
	{
		public _Catalog()
		{
		}

		public int Add(Catalog model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Sys_Catalog (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tCatCode,\r\n\t\t\t\t\tCatName,\r\n\t\t\t\t\tCatOdr,\r\n\t\t\t\t\tPCatCode\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@CatCode,\r\n\t\t\t\t\t@CatName,\r\n\t\t\t\t\t@CatOdr,\r\n\t\t\t\t\t@PCatCode\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "CatCode", DbType.String, model.CatCode);
			SysDatabase.AddInParameter(sqlStringCommand, "CatName", DbType.String, model.CatName);
			SysDatabase.AddInParameter(sqlStringCommand, "CatOdr", DbType.Int32, model.CatOdr);
			SysDatabase.AddInParameter(sqlStringCommand, "PCatCode", DbType.String, model.PCatCode);
			return SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public int Delete(string string_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Sys_Catalog ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			return SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public int DeleteByCode(string string_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Sys_Catalog ");
			stringBuilder.Append(" where CatCode=@CatCode ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "CatCode", DbType.String, string_0);
			return SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Sys_Catalog ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public Catalog GetModel(string string_0)
		{
			Catalog model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_Catalog ");
			stringBuilder.Append(" where CatCode=@CatCode ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "CatCode", DbType.String, string_0);
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

		public Catalog GetModel(DataRow dataRow_0)
		{
			Catalog catalog = new Catalog()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				catalog._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				catalog._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				catalog._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			catalog.CatCode = dataRow_0["CatCode"].ToString();
			catalog.CatName = dataRow_0["CatName"].ToString();
			if (dataRow_0["CatOdr"].ToString() != "")
			{
				catalog.CatOdr = int.Parse(dataRow_0["CatOdr"].ToString());
			}
			catalog.PCatCode = dataRow_0["PCatCode"].ToString();
			return catalog;
		}

		public IList<Catalog> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Catalog> catalogs = new List<Catalog>();
			stringBuilder.Append("select *  FROM T_E_Sys_Catalog ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				catalogs.Add(this.GetModel(row));
			}
			return catalogs;
		}

		public List<Catalog> GetModelListByCode(string catCode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Catalog> catalogs = new List<Catalog>();
			stringBuilder.Append("select *  FROM T_E_Sys_Catalog ");
			stringBuilder.Append(string.Concat(" where CatCode like '", catCode, "%' order by PCatCode, CatOdr"));
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				catalogs.Add(this.GetModel(row));
			}
			return catalogs;
		}

		public string GetNewCode(string pnodewbs)
		{
			string str;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select max(right(catcode,4))+1 ");
			stringBuilder.Append(" FROM T_E_Sys_Catalog ");
			if (pnodewbs.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where pcatcode='", pnodewbs, "'"));
			}
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

		public int GetNewOrd(string pnodewbs)
		{
			int num;
			string str = string.Concat("select max(catodr)+1 from T_E_Sys_Catalog where pcatcode='", pnodewbs, "'");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(str.ToString());
			object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
			num = (obj != DBNull.Value ? int.Parse(obj.ToString()) : 1);
			return num;
		}

		public int Update(Catalog model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_Catalog set \r\n\t\t\t\t\t_UserName=@_UserName,\r\n\t\t\t\t\t_OrgCode=@_OrgCode,\r\n\t\t\t\t\t_CreateTime=@_CreateTime,\r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tCatCode=@CatCode,\r\n\t\t\t\t\tCatName=@CatName,\r\n\t\t\t\t\tCatOdr=@CatOdr,\r\n\t\t\t\t\tPCatCode=@PCatCode\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "CatCode", DbType.String, model.CatCode);
			SysDatabase.AddInParameter(sqlStringCommand, "CatName", DbType.String, model.CatName);
			SysDatabase.AddInParameter(sqlStringCommand, "CatOdr", DbType.Int32, model.CatOdr);
			SysDatabase.AddInParameter(sqlStringCommand, "PCatCode", DbType.String, model.PCatCode);
			return SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}
	}
}