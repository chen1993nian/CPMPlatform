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
	public class _TableDoc
	{
		public _TableDoc()
		{
		}

		public int Add(TableDoc model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Sys_TableDoc (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tTableName,\r\n\t\t\t\t\tReportDoc,\r\n\t\t\t\t\tDesignDoc,\r\n\t\t\t\t\tInstructionDoc\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@TableName,\r\n\t\t\t\t\t@ReportDoc,\r\n\t\t\t\t\t@DesignDoc,\r\n\t\t\t\t\t@InstructionDoc\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "TableName", DbType.String, model.TableName);
			SysDatabase.AddInParameter(sqlStringCommand, "ReportDoc", DbType.String, model.ReportDoc);
			SysDatabase.AddInParameter(sqlStringCommand, "DesignDoc", DbType.String, model.DesignDoc);
			SysDatabase.AddInParameter(sqlStringCommand, "InstructionDoc", DbType.String, model.InstructionDoc);
			return SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public int Delete(string string_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Sys_TableDoc ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			return SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  From T_E_Sys_TableDoc ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public TableDoc GetModel(DataRow dataRow_0)
		{
			TableDoc tableDoc = new TableDoc()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				tableDoc._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				tableDoc._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				tableDoc._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			tableDoc.TableName = dataRow_0["TableName"].ToString();
			tableDoc.ReportDoc = dataRow_0["ReportDoc"].ToString();
			tableDoc.DesignDoc = dataRow_0["DesignDoc"].ToString();
			tableDoc.InstructionDoc = dataRow_0["InstructionDoc"].ToString();
			return tableDoc;
		}

		public TableDoc GetModelBytableName(string tableName)
		{
			TableDoc model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_TableDoc ");
			stringBuilder.Append(" where TableName=@TableName ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "TableName", DbType.String, tableName);
			TableDoc tableDoc = new TableDoc();
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

		public IList<TableDoc> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<TableDoc> tableDocs = new List<TableDoc>();
			stringBuilder.Append("select *  From T_E_Sys_TableDoc ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				tableDocs.Add(this.GetModel(row));
			}
			return tableDocs;
		}

		public int Update(TableDoc model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_TableDoc set \r\n\t\t\t\t\t_UserName=@_UserName,\r\n\t\t\t\t\t_OrgCode=@_OrgCode,\r\n\t\t\t\t\t_CreateTime=@_CreateTime,\r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tTableName=@TableName,\r\n\t\t\t\t\tReportDoc=@ReportDoc,\r\n\t\t\t\t\tDesignDoc=@DesignDoc,\r\n\t\t\t\t\tInstructionDoc=@InstructionDoc\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "TableName", DbType.String, model.TableName);
			SysDatabase.AddInParameter(sqlStringCommand, "ReportDoc", DbType.String, model.ReportDoc);
			SysDatabase.AddInParameter(sqlStringCommand, "DesignDoc", DbType.String, model.DesignDoc);
			SysDatabase.AddInParameter(sqlStringCommand, "InstructionDoc", DbType.String, model.InstructionDoc);
			return SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}
	}
}