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
	public class _TableStyle
	{
		private DbTransaction dbTransaction_0 = null;

		public _TableStyle()
		{
		}

		public _TableStyle(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(TableStyle model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Sys_TableStyle (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n                    _CompanyId,\r\n\t\t\t\t\tTableName,\r\n                    StyleIndex,\r\n\t\t\t\t\tStyleName,\r\n\t\t\t\t\tFormHtml,\r\n\t\t\t\t\tFormHtml2,\t\t\t\t\t\r\n                    PrintHtml,\r\n\t\t\t\t\tDetailHtml,\r\n\t\t\t\t\tCompiledHtml,\r\n\t\t\t\t\tMemo\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n                    @_CompanyId,\r\n\t\t\t\t\t@TableName,\r\n                    @StyleIndex,\r\n\t\t\t\t\t@StyleName,\r\n\t\t\t\t\t@FormHtml,\r\n\t\t\t\t\t@FormHtml2,\r\n                    @PrintHtml,\r\n\t\t\t\t\t@DetailHtml,\r\n\t\t\t\t\t@CompiledHtml,\r\n\t\t\t\t\t@Memo\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "_CompanyId", DbType.String, model._CompanyId);
			SysDatabase.AddInParameter(sqlStringCommand, "TableName", DbType.String, model.TableName);
			SysDatabase.AddInParameter(sqlStringCommand, "StyleIndex", DbType.Int32, model.StyleIndex);
			SysDatabase.AddInParameter(sqlStringCommand, "StyleName", DbType.String, model.StyleName);
			SysDatabase.AddInParameter(sqlStringCommand, "FormHtml", DbType.String, model.FormHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "FormHtml2", DbType.String, model.FormHtml2);
			SysDatabase.AddInParameter(sqlStringCommand, "PrintHtml", DbType.String, model.PrintHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "DetailHtml", DbType.String, model.DetailHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "CompiledHtml", DbType.String, model.CompiledHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "Memo", DbType.String, model.Memo);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Sys_TableStyle ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			return SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Sys_TableStyle ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public static int GetMaxIndex(string tblName)
		{
			string str = string.Concat("select isnull(max(StyleIndex),0) from T_E_Sys_TableStyle where TableName='", tblName, "'");
			return Convert.ToInt32(SysDatabase.ExecuteScalar(str));
		}

		public TableStyle GetModel(string string_0)
		{
			TableStyle model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_TableStyle ");
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

		public static TableStyle GetModel(string tblName, string sindex)
		{
			TableStyle model;
			_TableStyle __TableStyle = new _TableStyle();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select * from  T_E_Sys_TableStyle ");
			stringBuilder.Append(" where TableName=@TableName and StyleIndex=@StyleIndex");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, tblName);
			SysDatabase.AddInParameter(sqlStringCommand, "@StyleIndex", DbType.String, sindex);
			DataTable dataTable = SysDatabase.ExecuteTable(sqlStringCommand);
			if (dataTable.Rows.Count <= 0)
			{
				model = null;
			}
			else
			{
				model = __TableStyle.GetModel(dataTable.Rows[0]);
			}
			return model;
		}

		public TableStyle GetModel(DataRow dataRow_0)
		{
			TableStyle tableStyle = new TableStyle()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString(),
				_CompanyId = dataRow_0["_CompanyId"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				tableStyle._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				tableStyle._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				tableStyle._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			tableStyle.TableName = dataRow_0["TableName"].ToString();
			if (dataRow_0["StyleIndex"].ToString() != "")
			{
				tableStyle.StyleIndex = int.Parse(dataRow_0["StyleIndex"].ToString());
			}
			tableStyle.StyleName = dataRow_0["StyleName"].ToString();
			tableStyle.FormHtml = dataRow_0["FormHtml"].ToString();
			tableStyle.FormHtml2 = dataRow_0["FormHtml2"].ToString();
			tableStyle.PrintHtml = dataRow_0["PrintHtml"].ToString();
			tableStyle.DetailHtml = dataRow_0["DetailHtml"].ToString();
			tableStyle.CompiledHtml = dataRow_0["CompiledHtml"].ToString();
			tableStyle.Memo = dataRow_0["Memo"].ToString();
			return tableStyle;
		}

		public List<TableStyle> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<TableStyle> tableStyles = new List<TableStyle>();
			stringBuilder.Append("select *  From T_E_Sys_TableStyle ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
			foreach (DataRow row in dataTable.Rows)
			{
				tableStyles.Add(this.GetModel(row));
			}
			return tableStyles;
		}

		public int Update(TableStyle model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_TableStyle set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\tTableName=@TableName,\r\n\t\t\t\t\tStyleName=@StyleName,\r\n\t\t\t\t\tFormHtml=@FormHtml,\r\n\t\t\t\t\tFormHtml2=@FormHtml2,\r\n\r\n\t\t\t\t\tPrintHtml=@PrintHtml,\r\n\t\t\t\t\tDetailHtml=@DetailHtml,\r\n\t\t\t\t\tCompiledHtml=@CompiledHtml,\r\n\t\t\t\t\tMemo=@Memo\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "TableName", DbType.String, model.TableName);
			SysDatabase.AddInParameter(sqlStringCommand, "StyleName", DbType.String, model.StyleName);
			SysDatabase.AddInParameter(sqlStringCommand, "FormHtml", DbType.String, model.FormHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "FormHtml2", DbType.String, model.FormHtml2);
			SysDatabase.AddInParameter(sqlStringCommand, "PrintHtml", DbType.String, model.PrintHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "DetailHtml", DbType.String, model.DetailHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "CompiledHtml", DbType.String, model.CompiledHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "Memo", DbType.String, model.Memo);
			return SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}
	}
}