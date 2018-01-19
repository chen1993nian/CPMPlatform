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
	public class _DictEntry
	{
		private DbTransaction dbTransaction_0 = null;

		public _DictEntry()
		{
		}

		public _DictEntry(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(DictEntry model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Sys_DictEntry (\r\n\t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tItemName,\r\n\t\t\t\t\tItemCode,\r\n\t\t\t\t\tItemOrder,\r\n\t\t\t\t\tDictID\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@ItemName,\r\n\t\t\t\t\t@ItemCode,\r\n\t\t\t\t\t@ItemOrder,\r\n\t\t\t\t\t@DictID\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "ItemName", DbType.String, model.ItemName);
			SysDatabase.AddInParameter(sqlStringCommand, "ItemCode", DbType.String, model.ItemCode);
			SysDatabase.AddInParameter(sqlStringCommand, "ItemOrder", DbType.Int32, model.ItemOrder);
			SysDatabase.AddInParameter(sqlStringCommand, "DictID", DbType.String, model.DictID);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Sys_DictEntry ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  From T_E_Sys_DictEntry ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public DictEntry GetModel(string string_0)
		{
			DataTable dataTable;
			DictEntry model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_DictEntry ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			DictEntry dictEntry = new DictEntry();
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

		public DictEntry GetModel(DataRow dataRow_0)
		{
			DictEntry dictEntry = new DictEntry()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				dictEntry._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				dictEntry._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				dictEntry._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			dictEntry.ItemName = dataRow_0["ItemName"].ToString();
			dictEntry.ItemCode = dataRow_0["ItemCode"].ToString();
			if (dataRow_0["ItemOrder"].ToString() != "")
			{
				dictEntry.ItemOrder = int.Parse(dataRow_0["ItemOrder"].ToString());
			}
			dictEntry.DictID = dataRow_0["DictID"].ToString();
			return dictEntry;
		}

		public List<DictEntry> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<DictEntry> dictEntries = new List<DictEntry>();
			stringBuilder.Append("select *  From T_E_Sys_DictEntry ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by ItemOrder");
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				dictEntries.Add(this.GetModel(row));
			}
			return dictEntries;
		}

		public List<DictEntry> GetModelListByDictId(string DictId)
		{
			return this.GetModelList(string.Concat(" DictID ='", DictId, "'"));
		}

		public int Update(DictEntry model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_DictEntry set \r\n\t\t\t\t\t_AutoID=@_AutoID,\r\n\t\t\t\t\t_UserName=@_UserName,\r\n\t\t\t\t\t_OrgCode=@_OrgCode,\r\n\t\t\t\t\t_CreateTime=@_CreateTime,\r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tItemName=@ItemName,\r\n\t\t\t\t\tItemCode=@ItemCode,\r\n\t\t\t\t\tItemOrder=@ItemOrder,\r\n\t\t\t\t\tDictID=@DictID\r\n\t\t\t\t\twhere @_AutoID=_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "ItemName", DbType.String, model.ItemName);
			SysDatabase.AddInParameter(sqlStringCommand, "ItemCode", DbType.String, model.ItemCode);
			SysDatabase.AddInParameter(sqlStringCommand, "ItemOrder", DbType.Int32, model.ItemOrder);
			SysDatabase.AddInParameter(sqlStringCommand, "DictID", DbType.String, model.DictID);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}