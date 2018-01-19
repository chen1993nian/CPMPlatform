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
	public class _FieldEvent
	{
		private DbTransaction dbTransaction_0 = null;

		public _FieldEvent()
		{
		}

		public _FieldEvent(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(FieldEvent model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Sys_FieldEvent (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tFieldID,\r\n                    TableName,\r\n                    FieldName,\r\n\t\t\t\t\tEventType,\r\n\t\t\t\t\tEventScript\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@FieldID,\r\n                    @TableName,\r\n                    @FieldName,\r\n\t\t\t\t\t@EventType,\r\n\t\t\t\t\t@EventScript\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldID", DbType.String, model.FieldID);
			SysDatabase.AddInParameter(sqlStringCommand, "TableName", DbType.String, model.TableName);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldName", DbType.String, model.FieldName);
			SysDatabase.AddInParameter(sqlStringCommand, "EventType", DbType.String, model.EventType);
			SysDatabase.AddInParameter(sqlStringCommand, "EventScript", DbType.String, model.EventScript);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public void Delete(string string_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Sys_FieldEvent ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Sys_FieldEvent ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public FieldEvent GetModel(string string_0)
		{
			FieldEvent model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_FieldEvent ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			FieldEvent fieldEvent = new FieldEvent();
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

		public FieldEvent GetModel(string fieldId, string eventType)
		{
			FieldEvent model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_FieldEvent ");
			stringBuilder.Append(" where fieldId=@fieldId and  eventType=@eventType");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "fieldId", DbType.String, fieldId);
			SysDatabase.AddInParameter(sqlStringCommand, "eventType", DbType.String, eventType);
			FieldEvent fieldEvent = new FieldEvent();
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

		public FieldEvent GetModel(DataRow dataRow_0)
		{
			FieldEvent fieldEvent = new FieldEvent()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				fieldEvent._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				fieldEvent._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				fieldEvent._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			fieldEvent.FieldID = dataRow_0["FieldID"].ToString();
			fieldEvent.TableName = dataRow_0["TableName"].ToString();
			fieldEvent.FieldName = dataRow_0["FieldName"].ToString();
			fieldEvent.EventType = dataRow_0["EventType"].ToString();
			fieldEvent.EventScript = dataRow_0["EventScript"].ToString();
			return fieldEvent;
		}

		public List<FieldEvent> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<FieldEvent> fieldEvents = new List<FieldEvent>();
			stringBuilder.Append("select *  From T_E_Sys_FieldEvent ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			DataTable dataTable = null;
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
			foreach (DataRow row in dataTable.Rows)
			{
				fieldEvents.Add(this.GetModel(row));
			}
			return fieldEvents;
		}

		public List<FieldEvent> GetModelListBy(string fldName, string tblName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<FieldEvent> fieldEvents = new List<FieldEvent>();
			stringBuilder.Append("select *  From T_E_Sys_FieldEvent ");
			string[] strArrays = new string[] { " where FieldName='", fldName, "' and tableName='", tblName, "'" };
			stringBuilder.Append(string.Concat(strArrays));
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				fieldEvents.Add(this.GetModel(row));
			}
			return fieldEvents;
		}

		public void Update(FieldEvent model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_FieldEvent set \r\n\t\t\t\t\t_UserName=@_UserName,\r\n\t\t\t\t\t_OrgCode=@_OrgCode,\r\n\t\t\t\t\t_CreateTime=@_CreateTime,\r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tFieldID=@FieldID,\r\n\t\t\t\t\tTableName=@TableName,\r\n\t\t\t\t\tFieldName=@FieldName,\r\n\t\t\t\t\tEventType=@EventType,\r\n\t\t\t\t\tEventScript=@EventScript\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldID", DbType.String, model.FieldID);
			SysDatabase.AddInParameter(sqlStringCommand, "TableName", DbType.String, model.TableName);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldName", DbType.String, model.FieldName);
			SysDatabase.AddInParameter(sqlStringCommand, "EventType", DbType.String, model.EventType);
			SysDatabase.AddInParameter(sqlStringCommand, "EventScript", DbType.String, model.EventScript);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}
	}
}