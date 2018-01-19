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
	public class _TableScript
	{
		private DbTransaction dbTransaction_0 = null;

		public _TableScript()
		{
		}

		public _TableScript(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(TableScript model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Sys_TableScript (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tTableName,\r\n                    ScriptCode,\r\n\t\t\t\t\tEnable,\r\n\t\t\t\t\tScriptEvent,\r\n\t\t\t\t\tScriptTxt,\t\t\t\t\t\r\n                    ScriptNote\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n                    @TableName,\r\n\t\t\t\t\t@ScriptCode,\r\n\t\t\t\t\t@Enable,\r\n\t\t\t\t\t@ScriptEvent,\r\n                    @ScriptTxt,\r\n\t\t\t\t\t@ScriptNote\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "TableName", DbType.String, model.TableName);
			SysDatabase.AddInParameter(sqlStringCommand, "ScriptCode", DbType.String, model.ScriptCode);
			SysDatabase.AddInParameter(sqlStringCommand, "Enable", DbType.String, model.Enable);
			SysDatabase.AddInParameter(sqlStringCommand, "ScriptEvent", DbType.String, model.ScriptEvent);
			SysDatabase.AddInParameter(sqlStringCommand, "ScriptTxt", DbType.String, model.ScriptTxt);
			SysDatabase.AddInParameter(sqlStringCommand, "ScriptNote", DbType.String, model.ScriptNote);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Sys_TableScript ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			return SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  From T_E_Sys_TableScript ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public TableScript GetModel(string string_0)
		{
			TableScript model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_TableScript ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			TableStyle tableStyle = new TableStyle();
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

		public TableScript GetModel(DataRow dataRow_0)
		{
			TableScript tableScript = new TableScript()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				tableScript._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				tableScript._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				tableScript._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			tableScript.TableName = dataRow_0["TableName"].ToString();
			tableScript.ScriptCode = dataRow_0["ScriptCode"].ToString();
			tableScript.Enable = dataRow_0["Enable"].ToString();
			tableScript.ScriptEvent = dataRow_0["ScriptEvent"].ToString();
			tableScript.ScriptTxt = dataRow_0["ScriptTxt"].ToString();
			tableScript.ScriptNote = dataRow_0["ScriptNote"].ToString();
			return tableScript;
		}

		public List<TableScript> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<TableScript> tableScripts = new List<TableScript>();
			stringBuilder.Append("select *  From T_E_Sys_TableScript ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
			foreach (DataRow row in dataTable.Rows)
			{
				tableScripts.Add(this.GetModel(row));
			}
			return tableScripts;
		}

		public int Update(TableScript model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_TableScript set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\tTableName=@TableName,\r\n\t\t\t\t\tScriptCode=@ScriptCode,\r\n\t\t\t\t\tEnable=@Enable,\r\n\t\t\t\t\tScriptEvent=@ScriptEvent,\r\n\r\n\t\t\t\t\tScriptTxt=@ScriptTxt,\r\n\t\t\t\t\tScriptNote=@ScriptNote\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "TableName", DbType.String, model.TableName);
			SysDatabase.AddInParameter(sqlStringCommand, "ScriptCode", DbType.String, model.ScriptCode);
			SysDatabase.AddInParameter(sqlStringCommand, "Enable", DbType.String, model.Enable);
			SysDatabase.AddInParameter(sqlStringCommand, "ScriptEvent", DbType.String, model.ScriptEvent);
			SysDatabase.AddInParameter(sqlStringCommand, "ScriptTxt", DbType.String, model.ScriptTxt);
			SysDatabase.AddInParameter(sqlStringCommand, "ScriptNote", DbType.String, model.ScriptNote);
			return SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}
	}
}