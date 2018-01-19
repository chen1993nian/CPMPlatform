using EIS.AppBase;
using EIS.DataAccess;
using EIS.WorkFlow.Model;
using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.WorkFlow.Access
{
	public class _WorkflowLog
	{
		private DbTransaction _tran = null;

		public _WorkflowLog()
		{
		}

		public _WorkflowLog(DbTransaction tran)
		{
			this._tran = tran;
		}

		public int Add(WorkflowLog model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_WF_Log (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tAppName,\r\n\t\t\t\t\tAppId,\r\n\t\t\t\t\tLogTime,\r\n\t\t\t\t\tEmpName,\r\n\t\t\t\t\tLogContent\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@AppName,\r\n\t\t\t\t\t@AppId,\r\n\t\t\t\t\t@LogTime,\r\n\t\t\t\t\t@EmpName,\r\n\t\t\t\t\t@LogContent\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "AppName", DbType.String, model.AppName);
			SysDatabase.AddInParameter(sqlStringCommand, "AppId", DbType.String, model.AppId);
			SysDatabase.AddInParameter(sqlStringCommand, "LogTime", DbType.DateTime, model.LogTime);
			SysDatabase.AddInParameter(sqlStringCommand, "EmpName", DbType.String, model.EmpName);
			SysDatabase.AddInParameter(sqlStringCommand, "LogContent", DbType.String, model.LogContent);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int Delete(string key)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_WF_Log ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, key);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_WF_Log ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public int Update(WorkflowLog model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_WF_Log set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tAppName=@AppName,\r\n\t\t\t\t\tAppId=@AppId,\r\n\t\t\t\t\tLogTime=@LogTime,\r\n\t\t\t\t\tEmpName=@EmpName,\r\n\t\t\t\t\tLogContent=@LogContent\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "AppName", DbType.String, model.AppName);
			SysDatabase.AddInParameter(sqlStringCommand, "AppId", DbType.String, model.AppId);
			SysDatabase.AddInParameter(sqlStringCommand, "LogTime", DbType.DateTime, model.LogTime);
			SysDatabase.AddInParameter(sqlStringCommand, "EmpName", DbType.String, model.EmpName);
			SysDatabase.AddInParameter(sqlStringCommand, "LogContent", DbType.String, model.LogContent);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}
	}
}