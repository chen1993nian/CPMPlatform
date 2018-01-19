using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

using EIS.AppCommo;
//从 EIS.ApppDAC 中迁移过来，解决 Comm和DAC 相互引用问题 _TableInfo

namespace EIS.AppCommo
{
	public class _C_TableInfo
	{
		private DbTransaction dbTransaction_0 = null;

		private string string_0 = "";

		public _C_TableInfo(string tblName)
		{
			this.string_0 = tblName;
		}

		public _C_TableInfo(string tblName, DbTransaction dbTran)
		{
			this.string_0 = tblName;
			this.dbTransaction_0 = dbTran;
		}

		public _C_TableInfo()
		{
		}

        public _C_TableInfo(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(TableInfo model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Sys_TableInfo (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tTableName,\r\n\t\t\t\t\tTableNameCn,\r\n\t\t\t\t\tParentName,\r\n\t\t\t\t\tTableCat,\r\n\t\t\t\t\tFormHtml,\r\n\t\t\t\t\tFormHtml2,\r\n\t\t\t\t\tCompiledHtml,\r\n\t\t\t\t\tQueryHtml,\r\n\t\t\t\t\tPrintHtml,\r\n\t\t\t\t\tDetailHtml,\r\n\t\t\t\t\tListSQL,\r\n\t\t\t\t\tDetailSQL,\r\n\t\t\t\t\tPageRecCount,\r\n\t\t\t\t\tTableType,\r\n\t\t\t\t\tListScriptBlock,\r\n\t\t\t\t\tListPreProcessFn,\r\n\t\t\t\t\tEditScriptBlock,\r\n\t\t\t\t\tDataLog,\r\n\t\t\t\t\tDataLogTmpl,\r\n\t\t\t\t\tDeleteMode,\r\n\t\t\t\t\tShowState,\r\n\t\t\t\t\tArchiveState,\r\n\t\t\t\t\tOrderField,\r\n                    ConnectionId,\r\n                    EditMode,\r\n                    InitRows,\r\n                    FormWidth\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@TableName,\r\n\t\t\t\t\t@TableNameCn,\r\n\t\t\t\t\t@ParentName,\r\n\t\t\t\t\t@TableCat,\r\n\t\t\t\t\t@FormHtml,\r\n\t\t\t\t\t@FormHtml2,\r\n\t\t\t\t\t@CompiledHtml,\r\n\t\t\t\t\t@QueryHtml,\r\n\t\t\t\t\t@PrintHtml,\r\n\t\t\t\t\t@DetailHtml,\r\n\t\t\t\t\t@ListSQL,\r\n                    @DetailSQL,\r\n\t\t\t\t\t@PageRecCount,\r\n\t\t\t\t\t@TableType,\r\n\t\t\t\t\t@ListScriptBlock,\r\n\t\t\t\t\t@ListPreProcessFn,\r\n\t\t\t\t\t@EditScriptBlock,\r\n\t\t\t\t\t@DataLog,\r\n\t\t\t\t\t@DataLogTmpl,\r\n\t\t\t\t\t@DeleteMode,\r\n\t\t\t\t\t@ShowState,\r\n\t\t\t\t\t@ArchiveState,\r\n\t\t\t\t\t@OrderField,\r\n                    @ConnectionId,\r\n                    @EditMode,\r\n                    @InitRows,\r\n                    @FormWidth\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "TableName", DbType.String, model.TableName);
			SysDatabase.AddInParameter(sqlStringCommand, "TableNameCn", DbType.String, model.TableNameCn);
			SysDatabase.AddInParameter(sqlStringCommand, "ParentName", DbType.String, model.ParentName);
			SysDatabase.AddInParameter(sqlStringCommand, "TableCat", DbType.String, model.TableCat);
			SysDatabase.AddInParameter(sqlStringCommand, "FormHtml", DbType.String, model.FormHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "FormHtml2", DbType.String, model.FormHtml2);
			SysDatabase.AddInParameter(sqlStringCommand, "CompiledHtml", DbType.String, model.CompiledHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryHtml", DbType.String, model.QueryHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "PrintHtml", DbType.String, model.PrintHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "DetailHtml", DbType.String, model.DetailHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "ListSQL", DbType.String, model.ListSQL);
			SysDatabase.AddInParameter(sqlStringCommand, "DetailSQL", DbType.String, model.DetailSQL);
			SysDatabase.AddInParameter(sqlStringCommand, "PageRecCount", DbType.Int32, model.PageRecCount);
			SysDatabase.AddInParameter(sqlStringCommand, "TableType", DbType.Int32, model.TableType);
			SysDatabase.AddInParameter(sqlStringCommand, "ListScriptBlock", DbType.String, model.ListScriptBlock);
			SysDatabase.AddInParameter(sqlStringCommand, "ListPreProcessFn", DbType.String, model.ListPreProcessFn);
			SysDatabase.AddInParameter(sqlStringCommand, "EditScriptBlock", DbType.String, model.EditScriptBlock);
			SysDatabase.AddInParameter(sqlStringCommand, "DataLog", DbType.Int32, model.DataLog);
			SysDatabase.AddInParameter(sqlStringCommand, "DataLogTmpl", DbType.String, model.DataLogTmpl);
			SysDatabase.AddInParameter(sqlStringCommand, "DeleteMode", DbType.Int32, model.DeleteMode);
			SysDatabase.AddInParameter(sqlStringCommand, "ShowState", DbType.Int32, model.ShowState);
			SysDatabase.AddInParameter(sqlStringCommand, "ArchiveState", DbType.String, model.ArchiveState);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderField", DbType.String, model.OrderField);
			SysDatabase.AddInParameter(sqlStringCommand, "ConnectionId", DbType.String, model.ConnectionId);
			SysDatabase.AddInParameter(sqlStringCommand, "EditMode", DbType.String, model.EditMode);
			SysDatabase.AddInParameter(sqlStringCommand, "InitRows", DbType.Int32, model.InitRows);
			SysDatabase.AddInParameter(sqlStringCommand, "FormWidth", DbType.String, model.FormWidth);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int AddTableStyle(string strhtml, int sindex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert T_E_Sys_TableStyle (TableName,stylename,FormHtml ) ");
			stringBuilder.Append("values (@TableName,@sindex,@FormHtml)  ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, this.string_0);
			SysDatabase.AddInParameter(sqlStringCommand, "@FormHtml", DbType.String, strhtml);
			SysDatabase.AddInParameter(sqlStringCommand, "@sindex", DbType.Int32, sindex);
			return SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public int Delete()
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Sys_TableInfo ");
			stringBuilder.Append(" where TableName=@TableName ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, this.string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_1)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Sys_TableInfo ");
			stringBuilder.Append(" where TableName=@TableName ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, string_1);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public void DropTable()
		{
			StringBuilder stringBuilder = new StringBuilder();
			DbConnection dbConnection = SysDatabase.CreateConnection();
			try
			{
				dbConnection.Open();
				DbTransaction dbTransaction = dbConnection.BeginTransaction();
				try
				{
					try
					{
						stringBuilder.AppendFormat("delete T_E_Sys_TableInfo  where tablename='{0}';", this.string_0);
						stringBuilder.AppendFormat("delete T_E_Sys_FieldInfo  where tablename='{0}';", this.string_0);
						stringBuilder.AppendFormat("delete T_E_Sys_FieldInfoExt  where tablename='{0}';", this.string_0);
						stringBuilder.AppendFormat("delete T_E_Sys_FieldStyle  where tablename='{0}';", this.string_0);
						stringBuilder.AppendFormat("delete T_E_Sys_TableScript  where tablename='{0}';", this.string_0);
						stringBuilder.AppendFormat("delete T_E_Sys_TableStyle  where tablename='{0}';", this.string_0);
						stringBuilder.AppendFormat("delete T_E_Sys_FieldEvent  where tablename='{0}';", this.string_0);
						stringBuilder.AppendFormat("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.[{0}]') AND type in (N'U')) ", this.string_0);
						stringBuilder.AppendFormat(" DROP TABLE [dbo].[{0}];", this.string_0);
						foreach (TableInfo subTable in this.GetSubTable())
						{
							string tableName = subTable.TableName;
							stringBuilder.AppendFormat("delete T_E_Sys_FieldInfo  where tablename='{0}';", tableName);
							stringBuilder.AppendFormat("delete T_E_Sys_FieldStyle  where tablename='{0}';", tableName);
							stringBuilder.AppendFormat("delete T_E_Sys_TableInfo  where tablename='{0}';", tableName);
							stringBuilder.AppendFormat("delete T_E_Sys_FieldEvent  where tablename='{0}';", tableName);
							stringBuilder.AppendFormat("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}]') AND type in (N'U')) ", tableName);
							stringBuilder.AppendFormat(" DROP TABLE [dbo].[{0}];", tableName);
						}
						DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
						SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
						dbTransaction.Commit();
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						dbTransaction.Rollback();
						throw exception;
					}
				}
				finally
				{
					dbConnection.Close();
				}
			}
			finally
			{
				if (dbConnection != null)
				{
					((IDisposable)dbConnection).Dispose();
				}
			}
		}

		public void DropTableOuterTrans(bool dropTable)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("delete T_E_Sys_TableInfo  where tablename='{0}';", this.string_0);
			stringBuilder.AppendFormat("delete T_E_Sys_FieldInfo  where tablename='{0}';", this.string_0);
			stringBuilder.AppendFormat("delete T_E_Sys_FieldInfoExt  where tablename='{0}';", this.string_0);
			stringBuilder.AppendFormat("delete T_E_Sys_FieldStyle  where tablename='{0}';", this.string_0);
			stringBuilder.AppendFormat("delete T_E_Sys_TableScript  where tablename='{0}';", this.string_0);
			stringBuilder.AppendFormat("delete T_E_Sys_TableStyle  where tablename='{0}';", this.string_0);
			stringBuilder.AppendFormat("delete T_E_Sys_FieldEvent  where tablename='{0}';", this.string_0);
			if (dropTable)
			{
				stringBuilder.AppendFormat("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.[{0}]') AND type in (N'U')) ", this.string_0);
				stringBuilder.AppendFormat(" DROP TABLE [dbo].[{0}];", this.string_0);
			}
			foreach (TableInfo subTable in this.GetSubTable())
			{
				string tableName = subTable.TableName;
				stringBuilder.AppendFormat("delete T_E_Sys_FieldInfo  where tablename='{0}';", tableName);
				stringBuilder.AppendFormat("delete T_E_Sys_FieldStyle  where tablename='{0}';", tableName);
				stringBuilder.AppendFormat("delete T_E_Sys_TableInfo  where tablename='{0}';", tableName);
				stringBuilder.AppendFormat("delete T_E_Sys_FieldEvent  where tablename='{0}';", tableName);
				if (!dropTable)
				{
					continue;
				}
				stringBuilder.AppendFormat("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}]') AND type in (N'U')) ", tableName);
				stringBuilder.AppendFormat(" DROP TABLE [dbo].[{0}];", tableName);
			}
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			if (this.dbTransaction_0 == null)
			{
				SysDatabase.ExecuteNonQuery(sqlStringCommand);
			}
			else
			{
				SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0);
			}
		}

		public bool Exists()
		{
			bool flag;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from T_E_Sys_TableInfo");
			stringBuilder.Append(" where TableName=@TableName ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, this.string_0);
			flag = (this.dbTransaction_0 == null ? Convert.ToInt32(SysDatabase.ExecuteScalar(sqlStringCommand)) > 0 : Convert.ToInt32(SysDatabase.ExecuteScalar(sqlStringCommand, this.dbTransaction_0)) > 0);
			return flag;
		}

		public bool Exists(string tblName)
		{
			bool flag;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from T_E_Sys_TableInfo");
			stringBuilder.Append(" where TableName=@TableName ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, tblName);
			flag = (this.dbTransaction_0 == null ? Convert.ToInt32(SysDatabase.ExecuteScalar(sqlStringCommand)) > 0 : Convert.ToInt32(SysDatabase.ExecuteScalar(sqlStringCommand, this.dbTransaction_0)) > 0);
			return flag;
		}

		public bool FieldExists(string fldName)
		{
			bool flag;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(*) from syscolumns where id = object_id(@TableName) and name = @fldName");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, this.string_0);
			SysDatabase.AddInParameter(sqlStringCommand, "@fldName", DbType.String, fldName);
			flag = (this.dbTransaction_0 == null ? Convert.ToInt32(SysDatabase.ExecuteScalar(sqlStringCommand)) > 0 : Convert.ToInt32(SysDatabase.ExecuteScalar(sqlStringCommand, this.dbTransaction_0)) > 0);
			return flag;
		}

		public List<FieldInfo> GetFields()
		{
            return (new _C_FieldInfo(this.dbTransaction_0)).GetTableFields(this.string_0);
		}

		public List<FieldInfo> GetFieldsByTableId(string tableId)
		{
            _C_FieldInfo __FieldInfo = new _C_FieldInfo();
			return __FieldInfo.GetModelList(string.Concat("tableName=(select top 1 TableName from T_E_Sys_TableInfo where _AutoID='", tableId, "')"));
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  From T_E_Sys_TableInfo ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public int GetMaxOdr()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select isnull(max(FieldOdr),0) FROM T_E_Sys_FieldInfo ");
			stringBuilder.Append(" where TableName=@TableName ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, this.string_0);
			return Convert.ToInt32(SysDatabase.ExecuteScalar(sqlStringCommand));
		}

		public TableInfo GetModel()
		{
			TableInfo model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_TableInfo ");
			stringBuilder.Append(" where TableName=@TableName ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, this.string_0);
			TableInfo tableInfo = new TableInfo();
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 != null ? SysDatabase.ExecuteTable(sqlStringCommand, this.dbTransaction_0) : SysDatabase.ExecuteTable(sqlStringCommand));
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

		public TableInfo GetModel(DataRow dataRow_0)
		{
			TableInfo tableInfo = new TableInfo()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				tableInfo._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				tableInfo._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				tableInfo._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			tableInfo.TableName = dataRow_0["TableName"].ToString();
			tableInfo.TableNameCn = dataRow_0["TableNameCn"].ToString();
			tableInfo.ParentName = dataRow_0["ParentName"].ToString();
			tableInfo.TableCat = dataRow_0["TableCat"].ToString();
			tableInfo.FormHtml = dataRow_0["FormHtml"].ToString();
			tableInfo.FormHtml2 = dataRow_0["FormHtml2"].ToString();
			tableInfo.CompiledHtml = dataRow_0["CompiledHtml"].ToString();
			tableInfo.QueryHtml = dataRow_0["QueryHtml"].ToString();
			tableInfo.PrintHtml = dataRow_0["PrintHtml"].ToString();
			tableInfo.DetailHtml = dataRow_0["DetailHtml"].ToString();
			tableInfo.ListSQL = dataRow_0["ListSQL"].ToString();
			tableInfo.DetailSQL = dataRow_0["DetailSQL"].ToString();
			if (dataRow_0["PageRecCount"].ToString() != "")
			{
				tableInfo.PageRecCount = int.Parse(dataRow_0["PageRecCount"].ToString());
			}
			if (dataRow_0["TableType"].ToString() != "")
			{
				tableInfo.TableType = int.Parse(dataRow_0["TableType"].ToString());
			}
			tableInfo.ListScriptBlock = dataRow_0["ListScriptBlock"].ToString();
			tableInfo.ListPreProcessFn = dataRow_0["ListPreProcessFn"].ToString();
			tableInfo.EditScriptBlock = dataRow_0["EditScriptBlock"].ToString();
			if (dataRow_0["DataLog"].ToString() != "")
			{
				tableInfo.DataLog = int.Parse(dataRow_0["DataLog"].ToString());
			}
			tableInfo.DataLogTmpl = dataRow_0["DataLogTmpl"].ToString();
			if (dataRow_0["DeleteMode"].ToString() != "")
			{
				tableInfo.DeleteMode = int.Parse(dataRow_0["DeleteMode"].ToString());
			}
			if (dataRow_0["ShowState"].ToString() != "")
			{
				tableInfo.ShowState = int.Parse(dataRow_0["ShowState"].ToString());
			}
			if (dataRow_0["ArchiveState"].ToString() != "")
			{
				tableInfo.ArchiveState = int.Parse(dataRow_0["ArchiveState"].ToString());
			}
			tableInfo.OrderField = dataRow_0["OrderField"].ToString();
			tableInfo.ConnectionId = dataRow_0["ConnectionId"].ToString();
			if (dataRow_0["InitRows"].ToString() != "")
			{
				tableInfo.InitRows = int.Parse(dataRow_0["InitRows"].ToString());
			}
			tableInfo.EditMode = dataRow_0["EditMode"].ToString();
			tableInfo.FormWidth = dataRow_0["FormWidth"].ToString();
			return tableInfo;
		}

		public TableInfo GetModelById(string tableId)
		{
			TableInfo model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_TableInfo ");
			stringBuilder.Append(" where _AutoId=@_AutoId or TableName=@_AutoId ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoId", DbType.String, tableId);
			TableInfo tableInfo = new TableInfo();
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

		public List<TableInfo> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<TableInfo> tableInfos = new List<TableInfo>();
			stringBuilder.Append("select *  FROM T_E_Sys_TableInfo ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				tableInfos.Add(this.GetModel(row));
			}
			return tableInfos;
		}

		public List<FieldInfo> GetPhyFields()
		{
            return (new _C_FieldInfo()).GetTablePhyFields(this.string_0);
		}

		public DataTable GetSQLTriger(string eventType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  from  T_E_Sys_TableScript ");
			stringBuilder.Append(" where TableName=@TableName and [Enable]='是' and ScriptEvent=@ScriptEvent");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, this.string_0);
			SysDatabase.AddInParameter(sqlStringCommand, "@ScriptEvent", DbType.String, eventType);
			return SysDatabase.ExecuteDataSet(sqlStringCommand).Tables[0];
		}

		public List<TableInfo> GetSubTable()
		{
			return this.GetSubTable(this.string_0);
		}

		public List<TableInfo> GetSubTable(string tName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Concat("select * from T_E_Sys_TableInfo where parentName='", tName, "' order by _CreateTime"));
			StringBuilder stringBuilder1 = new StringBuilder();
			List<TableInfo> tableInfos = new List<TableInfo>();
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
			foreach (DataRow row in dataTable.Rows)
			{
				tableInfos.Add(this.GetModel(row));
			}
			return tableInfos;
		}

		public TableStyle GetTableStyle(string sindex)
		{
            return _C_TableStyle.GetModel(this.string_0, sindex);
		}

		public string GetTableStyle(string sindex, int htmlType)
		{
			string formHtml;
			try
			{
				if (htmlType == 0)
				{
                    formHtml = _C_TableStyle.GetModel(this.string_0, sindex).FormHtml;
				}
				else if (htmlType == 1)
				{
                    formHtml = _C_TableStyle.GetModel(this.string_0, sindex).FormHtml2;
				}
				else if (htmlType != 2)
				{
                    formHtml = (htmlType != 3 ? _C_TableStyle.GetModel(this.string_0, sindex).FormHtml : _C_TableStyle.GetModel(this.string_0, sindex).DetailHtml);
				}
				else
				{
                    formHtml = _C_TableStyle.GetModel(this.string_0, sindex).PrintHtml;
				}
			}
			catch (NullReferenceException nullReferenceException)
			{
				formHtml = "";
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				throw new Exception(string.Format("在查询表单样式（TblName={0},sIndex={1}）时，发生错误：{2}", this.string_0, sindex, exception.Message));
			}
			return formHtml;
		}

		public int SetUpdateTime()
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_TableInfo set ");
			stringBuilder.Append(" _UpdateTime=@UpdateTime ");
			stringBuilder.Append(" where TableName=@TableName ");
			stringBuilder.Append("update T_E_Sys_TableInfo set ");
			stringBuilder.Append(" _UpdateTime=@UpdateTime ");
			stringBuilder.Append(" where TableName=(select ParentName from T_E_Sys_TableInfo where TableName=@TableName) ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, this.string_0);
			SysDatabase.AddInParameter(sqlStringCommand, "@UpdateTime", DbType.DateTime, DateTime.Now);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Update(TableInfo model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_TableInfo set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tTableName=@TableName,\r\n\t\t\t\t\tTableNameCn=@TableNameCn,\r\n\t\t\t\t\tParentName=@ParentName,\r\n\t\t\t\t\tTableCat=@TableCat,\r\n\t\t\t\t\tFormHtml=@FormHtml,\r\n\t\t\t\t\tFormHtml2=@FormHtml2,\r\n\t\t\t\t\tCompiledHtml=@CompiledHtml,\r\n\t\t\t\t\tQueryHtml=@QueryHtml,\r\n\t\t\t\t\tPrintHtml=@PrintHtml,\r\n\t\t\t\t\tDetailHtml=@DetailHtml,\r\n\t\t\t\t\tListSQL=@ListSQL,\r\n\t\t\t\t\tDetailSQL=@DetailSQL,\r\n\t\t\t\t\tPageRecCount=@PageRecCount,\r\n\t\t\t\t\tTableType=@TableType,\r\n\t\t\t\t\tListScriptBlock=@ListScriptBlock,\r\n\t\t\t\t\tListPreProcessFn=@ListPreProcessFn,\r\n\t\t\t\t\tEditScriptBlock=@EditScriptBlock,\r\n\t\t\t\t\tDataLog=@DataLog,\r\n\t\t\t\t\tDataLogTmpl=@DataLogTmpl,\r\n\t\t\t\t\tDeleteMode=@DeleteMode,\r\n\t\t\t\t\tShowState=@ShowState,\r\n\t\t\t\t\tArchiveState=@ArchiveState,\r\n\t\t\t\t\tOrderField=@OrderField,\r\n\t\t\t\t\tConnectionId=@ConnectionId,\r\n                    EditMode=@EditMode,\r\n\t\t\t\t\tInitRows=@InitRows,\r\n\t\t\t\t\tFormWidth=@FormWidth\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, DateTime.Now);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "TableName", DbType.String, model.TableName);
			SysDatabase.AddInParameter(sqlStringCommand, "TableNameCn", DbType.String, model.TableNameCn);
			SysDatabase.AddInParameter(sqlStringCommand, "ParentName", DbType.String, model.ParentName);
			SysDatabase.AddInParameter(sqlStringCommand, "TableCat", DbType.String, model.TableCat);
			SysDatabase.AddInParameter(sqlStringCommand, "FormHtml", DbType.String, model.FormHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "FormHtml2", DbType.String, model.FormHtml2);
			SysDatabase.AddInParameter(sqlStringCommand, "CompiledHtml", DbType.String, model.CompiledHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryHtml", DbType.String, model.QueryHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "PrintHtml", DbType.String, model.PrintHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "DetailHtml", DbType.String, model.DetailHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "ListSQL", DbType.String, model.ListSQL);
			SysDatabase.AddInParameter(sqlStringCommand, "DetailSQL", DbType.String, model.DetailSQL);
			SysDatabase.AddInParameter(sqlStringCommand, "PageRecCount", DbType.Int32, model.PageRecCount);
			SysDatabase.AddInParameter(sqlStringCommand, "TableType", DbType.Int32, model.TableType);
			SysDatabase.AddInParameter(sqlStringCommand, "ListScriptBlock", DbType.String, model.ListScriptBlock);
			SysDatabase.AddInParameter(sqlStringCommand, "ListPreProcessFn", DbType.String, model.ListPreProcessFn);
			SysDatabase.AddInParameter(sqlStringCommand, "EditScriptBlock", DbType.String, model.EditScriptBlock);
			SysDatabase.AddInParameter(sqlStringCommand, "DataLog", DbType.Int32, model.DataLog);
			SysDatabase.AddInParameter(sqlStringCommand, "DataLogTmpl", DbType.String, model.DataLogTmpl);
			SysDatabase.AddInParameter(sqlStringCommand, "DeleteMode", DbType.Int32, model.DeleteMode);
			SysDatabase.AddInParameter(sqlStringCommand, "ShowState", DbType.Int32, model.ShowState);
			SysDatabase.AddInParameter(sqlStringCommand, "ArchiveState", DbType.String, model.ArchiveState);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderField", DbType.String, model.OrderField);
			SysDatabase.AddInParameter(sqlStringCommand, "ConnectionId", DbType.String, model.ConnectionId);
			SysDatabase.AddInParameter(sqlStringCommand, "FormWidth", DbType.String, model.FormWidth);
			SysDatabase.AddInParameter(sqlStringCommand, "EditMode", DbType.String, model.EditMode);
			SysDatabase.AddInParameter(sqlStringCommand, "InitRows", DbType.Int32, model.InitRows);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public void Update(TableInfo model, string stylename)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_TableInfo set ");
			stringBuilder.Append("FormHtml=@FormHtml,");
			stringBuilder.Append("FormHtml2=@FormHtml2,");
			stringBuilder.Append("CompiledHtml=@CompiledHtml,");
			stringBuilder.Append("PrintHtml=@PrintHtml,");
			stringBuilder.Append("DetailHtml=@DetailHtml,");
			stringBuilder.Append("_UpdateTime=@UpdateTime");
			stringBuilder.Append(" where TableName=@TableName ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, model.TableName);
			SysDatabase.AddInParameter(sqlStringCommand, "@FormHtml", DbType.String, model.FormHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "@FormHtml2", DbType.String, model.FormHtml2);
			SysDatabase.AddInParameter(sqlStringCommand, "@CompiledHtml", DbType.String, model.CompiledHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "@PrintHtml", DbType.String, model.PrintHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "@DetailHtml", DbType.String, model.DetailHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "@UpdateTime", DbType.DateTime, DateTime.Now);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public void UpdateScript(TableInfo model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_TableInfo set ");
			stringBuilder.Append("ListScriptBlock=@ListScriptBlock,");
			stringBuilder.Append("ListPreProcessFn=@ListPreProcessFn,");
			stringBuilder.Append("EditScriptBlock=@EditScriptBlock,");
			stringBuilder.Append(" _UpdateTime=@UpdateTime ");
			stringBuilder.Append(" where TableName=@TableName ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, model.TableName);
			SysDatabase.AddInParameter(sqlStringCommand, "@ListScriptBlock", DbType.String, model.ListScriptBlock);
			SysDatabase.AddInParameter(sqlStringCommand, "@ListPreProcessFn", DbType.String, model.ListPreProcessFn);
			SysDatabase.AddInParameter(sqlStringCommand, "@EditScriptBlock", DbType.String, model.EditScriptBlock);
			SysDatabase.AddInParameter(sqlStringCommand, "@UpdateTime", DbType.DateTime, DateTime.Now);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public int UpdateTableStyle(TableInfo model, string sindex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_TableStyle set ");
			stringBuilder.Append("_UpdateTime=@_UpdateTime, ");
			stringBuilder.Append("FormHtml=@FormHtml, ");
			stringBuilder.Append("FormHtml2=@FormHtml2 ");
			stringBuilder.Append(" where TableName=@TableName  and styleIndex=@sindex;");
			stringBuilder.Append("update T_E_Sys_TableInfo set ");
			stringBuilder.Append(" _UpdateTime=@_UpdateTime ");
			stringBuilder.Append(" where TableName=@TableName ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
			SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, this.string_0);
			SysDatabase.AddInParameter(sqlStringCommand, "@FormHtml", DbType.String, model.FormHtml);
			SysDatabase.AddInParameter(sqlStringCommand, "@FormHtml2", DbType.String, model.FormHtml2);
			SysDatabase.AddInParameter(sqlStringCommand, "@sindex", DbType.Int32, int.Parse(sindex));
			return SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}
	}
}