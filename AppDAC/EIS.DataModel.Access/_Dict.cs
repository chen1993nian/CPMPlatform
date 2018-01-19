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
	public class _Dict
	{
		private DbTransaction dbTransaction_0 = null;

		public _Dict()
		{
		}

		public _Dict(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(Dict model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Sys_Dict (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tDictCode,\r\n\t\t\t\t\tDictName,\r\n\t\t\t\t\tDictCat\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@DictCode,\r\n\t\t\t\t\t@DictName,\r\n\t\t\t\t\t@DictCat\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@DictCode", DbType.String, model.DictCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@DictName", DbType.String, model.DictName);
			SysDatabase.AddInParameter(sqlStringCommand, "@DictCat", DbType.String, model.DictCat);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Sys_Dict ");
			stringBuilder.Append(" where _AutoID=@_AutoID ;");
			stringBuilder.Append("delete T_E_Sys_DictEntry where DictId=@_AutoID ;");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public bool Exists(string dictId)
		{
			bool flag;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from T_E_Sys_Dict");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, dictId);
			flag = (this.dbTransaction_0 == null ? Convert.ToInt32(SysDatabase.ExecuteScalar(sqlStringCommand)) > 0 : Convert.ToInt32(SysDatabase.ExecuteScalar(sqlStringCommand, this.dbTransaction_0)) > 0);
			return flag;
		}

		public bool Exists(string dictId, string dictCode)
		{
			bool flag;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from T_E_Sys_Dict");
			stringBuilder.Append(" where _AutoID<>@_AutoID  and DictCode=@DictCode");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, dictId);
			SysDatabase.AddInParameter(sqlStringCommand, "@DictCode", DbType.String, dictCode);
			flag = (this.dbTransaction_0 == null ? Convert.ToInt32(SysDatabase.ExecuteScalar(sqlStringCommand)) > 0 : Convert.ToInt32(SysDatabase.ExecuteScalar(sqlStringCommand, this.dbTransaction_0)) > 0);
			return flag;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  From T_E_Sys_Dict ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public Dict GetModel(string string_0)
		{
			Dict model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_Dict ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(sqlStringCommand) : SysDatabase.ExecuteTable(sqlStringCommand, this.dbTransaction_0));
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

		public Dict GetModel(DataRow dataRow_0)
		{
			Dict dict = new Dict()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				dict._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				dict._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				dict._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			dict.DictName = dataRow_0["DictName"].ToString();
			dict.DictCode = dataRow_0["DictCode"].ToString();
			dict.DictCat = dataRow_0["DictCat"].ToString();
			return dict;
		}

		public Dict GetModelByCode(string code)
		{
			Dict model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_Dict ");
			stringBuilder.Append(" where DictCode=@DictCode ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@DictCode", DbType.String, code);
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(sqlStringCommand) : SysDatabase.ExecuteTable(sqlStringCommand, this.dbTransaction_0));
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

		public IList<Dict> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Dict> dicts = new List<Dict>();
			stringBuilder.Append("select *  From T_E_Sys_Dict ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				dicts.Add(this.GetModel(row));
			}
			return dicts;
		}

		public int Update(Dict model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_Dict set \r\n\t\t\t\t\t_UserName=@_UserName,\r\n\t\t\t\t\t_OrgCode=@_OrgCode,\r\n\t\t\t\t\t_CreateTime=@_CreateTime,\r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tDictCode=@DictCode,\r\n\t\t\t\t\tDictName=@DictName,\r\n\t\t\t\t\tDictCat=@DictCat\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@DictCode", DbType.String, model.DictCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@DictName", DbType.String, model.DictName);
			SysDatabase.AddInParameter(sqlStringCommand, "@DictCat", DbType.String, model.DictCat);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}