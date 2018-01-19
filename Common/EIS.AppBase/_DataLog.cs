using EIS.AppModel;
using EIS.DataAccess;
//using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using EIS.AppCommo;
namespace EIS.AppBase
{
	public class _DataLog
	{
		public _DataLog()
		{
		}

		public static string GenDeleteBizLog(string bizName, string bizId, DataRow data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<{0} status='Deleted' id='{1}'>", bizName, bizId);
            List<EIS.DataModel.Model.FieldInfo> phyFields = (new _C_TableInfo(bizName)).GetPhyFields();
			foreach (DataColumn column in data.Table.Columns)
			{
				string columnName = column.ColumnName;
				if (!AppFields.Contains(columnName))
				{
					string fieldNameCn = "";
					EIS.DataModel.Model.FieldInfo fieldInfo = phyFields.Find((EIS.DataModel.Model.FieldInfo f) => f.FieldName == columnName);
					if (fieldInfo != null)
					{
						fieldNameCn = fieldInfo.FieldNameCn;
						if (fieldInfo.FieldType > 5)
						{
							continue;
						}
					}
					if (data[columnName] != DBNull.Value)
					{
						stringBuilder.AppendFormat("<{0} cn='{1}'><old><![CDATA[{2}]]></old></{0}>", columnName, fieldNameCn, data[columnName]);
					}
				}
			}
			stringBuilder.AppendFormat("</{0}>", bizName);
			return stringBuilder.ToString();
		}

		public static string GenNewBizLog(string bizName, string bizId, DataRow data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<{0} status='Detached' id='{1}'>", bizName, bizId);
            List<EIS.DataModel.Model.FieldInfo> phyFields = (new _C_TableInfo(bizName)).GetPhyFields();
			foreach (DataColumn column in data.Table.Columns)
			{
				string columnName = column.ColumnName;
				if (!AppFields.Contains(columnName))
				{
					string fieldNameCn = "";
					EIS.DataModel.Model.FieldInfo fieldInfo = phyFields.Find((EIS.DataModel.Model.FieldInfo f) => f.FieldName == columnName);
					if (fieldInfo != null)
					{
						fieldNameCn = fieldInfo.FieldNameCn;
						if (fieldInfo.FieldType > 5)
						{
							continue;
						}
					}
					if (data[columnName] != DBNull.Value)
					{
						stringBuilder.AppendFormat("<{0} cn='{1}'><new><![CDATA[{2}]]></new></{0}>", columnName, fieldNameCn, data[columnName]);
					}
				}
			}
			stringBuilder.AppendFormat("</{0}>", bizName);
			return stringBuilder.ToString();
		}

		public static string GenUpdateBizLog(string bizName, string bizId, DataRow oldData, DataRow newData)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<{0} status='Modified' id='{1}'>", bizName, bizId);
            List<EIS.DataModel.Model.FieldInfo> phyFields = (new _C_TableInfo(bizName)).GetPhyFields();
			foreach (DataColumn column in oldData.Table.Columns)
			{
				string columnName = column.ColumnName;
				if (!AppFields.Contains(columnName))
				{
					string fieldNameCn = "";
					EIS.DataModel.Model.FieldInfo fieldInfo = phyFields.Find((EIS.DataModel.Model.FieldInfo f) => f.FieldName == columnName);
					if (fieldInfo != null)
					{
						fieldNameCn = fieldInfo.FieldNameCn;
						if (fieldInfo.FieldType > 5)
						{
							continue;
						}
					}
					if (oldData[columnName].GetType() == typeof(string))
					{
						if (oldData[columnName].ToString() == newData[columnName].ToString())
						{
							continue;
						}
					}
					else if (oldData[columnName].GetType() == typeof(DateTime))
					{
						if ((oldData[columnName] == DBNull.Value ? false : newData[columnName] != DBNull.Value))
						{
							if (Convert.ToDateTime(newData[columnName]).CompareTo(Convert.ToDateTime(oldData[columnName])) == 0)
							{
								continue;
							}
						}
					}
					else if (oldData[columnName].ToString() == newData[columnName].ToString())
					{
						continue;
					}
					object[] item = new object[] { columnName, fieldNameCn, oldData[columnName], newData[columnName] };
					stringBuilder.AppendFormat("<{0} cn='{1}'><old><![CDATA[{2}]]></old><new><![CDATA[{3}]]></new></{0}>", item);
				}
			}
			stringBuilder.AppendFormat("</{0}>", bizName);
			return stringBuilder.ToString();
		}

		public DataLog GetDataLog(string logId)
		{
			DataLog dataLog;
			object obj;
			try
			{
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("select  top 1 * from T_E_Sys_LogData ");
                stringBuilder.Append(" where LogId=@LogId ");
                DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
                SysDatabase.AddInParameter(sqlStringCommand, "LogId", DbType.String, logId);
                DataTable dataTable = SysDatabase.ExecuteTable(sqlStringCommand);
                if (dataTable.Rows.Count <= 0)
                {
                    dataLog = null;
                }
                else
                {
                    dataLog = this.GetModel(dataTable.Rows[0]);
                }
                return dataLog;
			}
			catch (Exception exception)
			{
				Exception innerException = exception.InnerException;
				if (innerException != null)
				{
					throw innerException;
				}
				throw;
			}
			return dataLog;
		}

		private DataLog GetModel(DataRow dataRow_0)
		{
			DataLog dataLog = new DataLog();
			
			try
			{
               
                    dataLog.LogId = dataRow_0["LogId"].ToString();
                    dataLog.LogType = dataRow_0["LogType"].ToString();
                    dataLog.LogUser = dataRow_0["LogUser"].ToString();
                    dataLog.UserName = dataRow_0["UserName"].ToString();
              
                if (dataRow_0["LogTime"].ToString() != "")
                {
                    dataLog.LogTime = DateTime.Parse(dataRow_0["LogTime"].ToString());
                }
                dataLog.AppID = dataRow_0["AppId"].ToString();
                dataLog.AppName = dataRow_0["AppName"].ToString();
                dataLog.ComputeIP = dataRow_0["ComputeIP"].ToString();
                dataLog.ServerIP = dataRow_0["ServerIP"].ToString();
                dataLog.Message = dataRow_0["Message"].ToString();
                dataLog.Data = dataRow_0["Data"].ToString();
                return dataLog;
			}
			catch (Exception exception)
			{
				Exception innerException = exception.InnerException;
				if (innerException != null)
				{
					throw innerException;
				}
				throw;
			}
			return dataLog;
		}

		public void WriteDataLog(DataLog model)
		{
			this.WriteDataLog(model, null);
		}

		public void WriteDataLog(DataLog model, DbTransaction tran)
		{
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("insert into T_E_Sys_LogData(");
            stringBuilder.Append("LogId,LogType,LogUser,UserName,ModuleCode,ModuleName,AppID,AppName,ComputeIP,ServerIP,Message,Data,Browser,Platform,UserAgent)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@LogId,@LogType,@LogUser,@UserName,@ModuleCode,@ModuleName,@AppID,@AppName,@ComputeIP,@ServerIP,@Message,@Data,@Browser,@Platform,@UserAgent)");
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
            if (model.LogId == "")
            {
                model.LogId = Guid.NewGuid().ToString();
            }
            SysDatabase.AddInParameter(sqlStringCommand, "@LogId", DbType.String, model.LogId);
            SysDatabase.AddInParameter(sqlStringCommand, "@LogType", DbType.String, model.LogType);
            SysDatabase.AddInParameter(sqlStringCommand, "@LogUser", DbType.String, model.LogUser);
            SysDatabase.AddInParameter(sqlStringCommand, "@UserName", DbType.String, model.UserName);
            SysDatabase.AddInParameter(sqlStringCommand, "@ModuleCode", DbType.String, model.ModuleCode);
            SysDatabase.AddInParameter(sqlStringCommand, "@ModuleName", DbType.String, model.ModuleName);
            SysDatabase.AddInParameter(sqlStringCommand, "@AppID", DbType.String, model.AppID);
            SysDatabase.AddInParameter(sqlStringCommand, "@AppName", DbType.String, model.AppName);
            SysDatabase.AddInParameter(sqlStringCommand, "@ComputeIP", DbType.String, model.ComputeIP);
            SysDatabase.AddInParameter(sqlStringCommand, "@ServerIP", DbType.String, model.ServerIP);
            SysDatabase.AddInParameter(sqlStringCommand, "@Message", DbType.String, model.Message);
            SysDatabase.AddInParameter(sqlStringCommand, "@Data", DbType.String, model.Data);
            SysDatabase.AddInParameter(sqlStringCommand, "@Browser", DbType.String, model.Browser);
            SysDatabase.AddInParameter(sqlStringCommand, "@Platform", DbType.String, model.Platform);
            SysDatabase.AddInParameter(sqlStringCommand, "@UserAgent", DbType.String, model.UserAgent);
            if (tran != null)
            {
                SysDatabase.ExecuteNonQuery(sqlStringCommand, tran);
            }
            else
            {
                SysDatabase.ExecuteNonQuery(sqlStringCommand);
            }
		}

		public void WriteExceptionLog(DataLog model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into T_E_Sys_LogException(");
			stringBuilder.Append("LogType,LogUser,UserName,ModuleCode,ModuleName,AppID,AppName,ComputeIP,ServerIP,Message,Browser,Platform,UserAgent)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@LogType,@LogUser,@UserName,@ModuleCode,@ModuleName,@AppID,@AppName,@ComputeIP,@ServerIP,@Message,@Browser,@Platform,@UserAgent)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@LogType", DbType.String, model.LogType);
			SysDatabase.AddInParameter(sqlStringCommand, "@LogUser", DbType.String, model.LogUser);
			SysDatabase.AddInParameter(sqlStringCommand, "@UserName", DbType.String, model.UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@ModuleCode", DbType.String, model.ModuleCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@ModuleName", DbType.String, model.ModuleName);
			SysDatabase.AddInParameter(sqlStringCommand, "@AppID", DbType.String, model.AppID);
			SysDatabase.AddInParameter(sqlStringCommand, "@AppName", DbType.String, model.AppName);
			SysDatabase.AddInParameter(sqlStringCommand, "@ComputeIP", DbType.String, model.ComputeIP);
			SysDatabase.AddInParameter(sqlStringCommand, "@ServerIP", DbType.String, model.ServerIP);
			SysDatabase.AddInParameter(sqlStringCommand, "@Message", DbType.String, model.Message);
			SysDatabase.AddInParameter(sqlStringCommand, "@Browser", DbType.String, model.Browser);
			SysDatabase.AddInParameter(sqlStringCommand, "@Platform", DbType.String, model.Platform);
			SysDatabase.AddInParameter(sqlStringCommand, "@UserAgent", DbType.String, model.UserAgent);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public void WriteSecurityLog(DataLog model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into T_E_Sys_LogSecurity(");
			stringBuilder.Append("LogType,LogUser,UserName,ModuleCode,ModuleName,AppID,AppName,ComputeIP,ServerIP,Message,Browser,Platform,UserAgent)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@LogType,@LogUser,@UserName,@ModuleCode,@ModuleName,@AppID,@AppName,@ComputeIP,@ServerIP,@Message,@Browser,@Platform,@UserAgent)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@LogType", DbType.String, model.LogType);
			SysDatabase.AddInParameter(sqlStringCommand, "@LogUser", DbType.String, model.LogUser);
			SysDatabase.AddInParameter(sqlStringCommand, "@UserName", DbType.String, model.UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@ModuleCode", DbType.String, model.ModuleCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@ModuleName", DbType.String, model.ModuleName);
			SysDatabase.AddInParameter(sqlStringCommand, "@AppID", DbType.String, model.AppID);
			SysDatabase.AddInParameter(sqlStringCommand, "@AppName", DbType.String, model.AppName);
			SysDatabase.AddInParameter(sqlStringCommand, "@ComputeIP", DbType.String, model.ComputeIP);
			SysDatabase.AddInParameter(sqlStringCommand, "@ServerIP", DbType.String, model.ServerIP);
			SysDatabase.AddInParameter(sqlStringCommand, "@Message", DbType.String, model.Message);
			SysDatabase.AddInParameter(sqlStringCommand, "@Browser", DbType.String, model.Browser);
			SysDatabase.AddInParameter(sqlStringCommand, "@Platform", DbType.String, model.Platform);
			SysDatabase.AddInParameter(sqlStringCommand, "@UserAgent", DbType.String, model.UserAgent);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}
	}
}