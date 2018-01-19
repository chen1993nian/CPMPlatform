using EIS.DataAccess;
using EIS.WorkFlow.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.WorkFlow.Access
{
	public class _UserTask
	{
		private DbTransaction _tran = null;

		public _UserTask()
		{
		}

		public _UserTask(DbTransaction tran)
		{
			this._tran = tran;
		}

		public int Add(UserTask model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_WF_UserTask (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tInstanceId,\r\n\t\t\t\t\tTaskId,\r\n\t\t\t\t\tOwnerId,\r\n\t\t\t\t\tAgentId,\r\n                    DealUser,\r\n\t\t\t\t\tTaskState,\r\n\t\t\t\t\tDealAdvice,\r\n\t\t\t\t\tDealTime,\r\n\t\t\t\t\tDealAction,\r\n\t\t\t\t\tIsShare,\r\n\t\t\t\t\tIsAssign,\r\n\t\t\t\t\tDealType,\r\n                    EmployeeName,\r\n                    PositionId,\r\n                    PositionName,\r\n                    DeptId,\r\n                    DeptName,\r\n                    IsRead,\r\n                    ReadTime,\r\n                    RecNames,\r\n                    RecIds\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@InstanceId,\r\n\t\t\t\t\t@TaskId,\r\n\t\t\t\t\t@OwnerId,\r\n\t\t\t\t\t@AgentId,\r\n\t\t\t\t\t@DealUser,\r\n\t\t\t\t\t@TaskState,\r\n\t\t\t\t\t@DealAdvice,\r\n\t\t\t\t\t@DealTime,\r\n\t\t\t\t\t@DealAction,\r\n\t\t\t\t\t@IsShare,\r\n\t\t\t\t\t@IsAssign,\r\n\t\t\t\t\t@DealType,\r\n                    @EmployeeName,\r\n                    @PositionId,\r\n                    @PositionName,\r\n                    @DeptId,\r\n                    @DeptName,\r\n                    @IsRead,\r\n                    @ReadTime,\r\n                    @RecNames,\r\n                    @RecIds\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model.UserTaskId);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "InstanceId", DbType.String, model.InstanceId);
			SysDatabase.AddInParameter(sqlStringCommand, "TaskId", DbType.String, model.TaskId);
			SysDatabase.AddInParameter(sqlStringCommand, "OwnerId", DbType.String, model.OwnerId);
			SysDatabase.AddInParameter(sqlStringCommand, "AgentId", DbType.String, model.AgentId);
			SysDatabase.AddInParameter(sqlStringCommand, "DealUser", DbType.String, model.DealUser);
			SysDatabase.AddInParameter(sqlStringCommand, "TaskState", DbType.String, model.TaskState);
			SysDatabase.AddInParameter(sqlStringCommand, "DealAdvice", DbType.String, model.DealAdvice);
			SysDatabase.AddInParameter(sqlStringCommand, "DealTime", DbType.DateTime, model.DealTime);
			SysDatabase.AddInParameter(sqlStringCommand, "DealAction", DbType.String, model.DealAction);
			SysDatabase.AddInParameter(sqlStringCommand, "IsShare", DbType.String, model.IsShare);
			SysDatabase.AddInParameter(sqlStringCommand, "IsAssign", DbType.String, model.IsAssign);
			SysDatabase.AddInParameter(sqlStringCommand, "DealType", DbType.String, model.DealType);
			SysDatabase.AddInParameter(sqlStringCommand, "EmployeeName", DbType.String, model.EmployeeName);
			SysDatabase.AddInParameter(sqlStringCommand, "PositionId", DbType.String, model.PositionId);
			SysDatabase.AddInParameter(sqlStringCommand, "PositionName", DbType.String, model.PositionName);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptId", DbType.String, model.DeptId);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptName", DbType.String, model.DeptName);
			SysDatabase.AddInParameter(sqlStringCommand, "IsRead", DbType.String, model.IsRead);
			SysDatabase.AddInParameter(sqlStringCommand, "ReadTime", DbType.DateTime, model.ReadTime);
			SysDatabase.AddInParameter(sqlStringCommand, "RecNames", DbType.String, model.RecNames);
			SysDatabase.AddInParameter(sqlStringCommand, "RecIds", DbType.String, model.RecIds);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int Delete(string key)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_WF_UserTask ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, key);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int DeleteByInstanceId(string instanceId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_WF_UserTask ");
			stringBuilder.Append(" where instanceId=@instanceId ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "instanceId", DbType.String, instanceId);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int DeleteByTaskId(string taskId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_WF_UserTask ");
			stringBuilder.Append(" where taskId=@taskId ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "taskId", DbType.String, taskId);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public UserTask GetLastUserTaskByEmployeeId(string instanceId, string employeeId)
		{
			UserTask model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select top 1 * from T_E_WF_UserTask where _IsDel=0 and TaskState='2' and OwnerId='{0}' \r\n                and InstanceId ='{1}' and isnull(IsAssign,'0')='0' order by DealTime desc", employeeId, instanceId);
			DataTable dataTable = null;
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			dataTable = (this._tran == null ? SysDatabase.ExecuteTable(sqlStringCommand) : SysDatabase.ExecuteTable(sqlStringCommand, this._tran));
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

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_WF_UserTask ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public UserTask GetModel(string key)
		{
			UserTask model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_WF_UserTask ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, key);
			UserTask userTask = new UserTask();
			DataTable dataTable = new DataTable();
			dataTable = (this._tran == null ? SysDatabase.ExecuteTable(sqlStringCommand) : SysDatabase.ExecuteTable(sqlStringCommand, this._tran));
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

		public UserTask GetModel(DataRow dr)
		{
			UserTask userTask = new UserTask()
			{
				UserTaskId = dr["_AutoID"].ToString(),
				_UserName = dr["_UserName"].ToString(),
				_OrgCode = dr["_OrgCode"].ToString()
			};
			if (dr["_CreateTime"].ToString() != "")
			{
				userTask._CreateTime = DateTime.Parse(dr["_CreateTime"].ToString());
			}
			if (dr["_UpdateTime"].ToString() != "")
			{
				userTask._UpdateTime = DateTime.Parse(dr["_UpdateTime"].ToString());
			}
			if (dr["_IsDel"].ToString() != "")
			{
				userTask._IsDel = int.Parse(dr["_IsDel"].ToString());
			}
			userTask.InstanceId = dr["InstanceId"].ToString();
			userTask.TaskId = dr["TaskId"].ToString();
			userTask.OwnerId = dr["OwnerId"].ToString();
			userTask.AgentId = dr["AgentId"].ToString();
			userTask.DealUser = dr["DealUser"].ToString();
			userTask.TaskState = dr["TaskState"].ToString();
			userTask.DealAdvice = dr["DealAdvice"].ToString();
			if (dr["DealTime"].ToString() != "")
			{
				userTask.DealTime = new DateTime?(DateTime.Parse(dr["DealTime"].ToString()));
			}
			userTask.DealAction = dr["DealAction"].ToString();
			userTask.IsShare = dr["IsShare"].ToString();
			userTask.IsAssign = dr["IsAssign"].ToString();
			userTask.DealType = dr["DealType"].ToString();
			userTask.EmployeeName = dr["EmployeeName"].ToString();
			userTask.PositionId = dr["PositionId"].ToString();
			userTask.PositionName = dr["PositionName"].ToString();
			userTask.DeptId = dr["DeptId"].ToString();
			userTask.DeptName = dr["DeptName"].ToString();
			userTask.IsRead = dr["IsRead"].ToString();
			if (dr["ReadTime"].ToString() != "")
			{
				userTask.ReadTime = new DateTime?(DateTime.Parse(dr["ReadTime"].ToString()));
			}
			userTask.RecNames = dr["RecNames"].ToString();
			userTask.RecIds = dr["RecIds"].ToString();
			return userTask;
		}

		public List<UserTask> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<UserTask> userTasks = new List<UserTask>();
			stringBuilder.Append("select *  From T_E_WF_UserTask ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by _createTime");
			DataTable dataTable = null;
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			dataTable = (this._tran == null ? SysDatabase.ExecuteTable(sqlStringCommand) : SysDatabase.ExecuteTable(sqlStringCommand, this._tran));
			foreach (DataRow row in dataTable.Rows)
			{
				userTasks.Add(this.GetModel(row));
			}
			return userTasks;
		}

		public UserTask GetStartUserTask(string instanceId)
		{
			UserTask model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select top 1 u.* from T_E_WF_Task t inner join T_E_WF_UserTask u on t._AutoID=u.TaskId ");
			stringBuilder.AppendFormat(" where t.InstanceId='{0}' and t.DefineType='Start' order by u._CreateTime", instanceId);
			DataTable dataTable = null;
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			dataTable = (this._tran == null ? SysDatabase.ExecuteTable(sqlStringCommand) : SysDatabase.ExecuteTable(sqlStringCommand, this._tran));
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

		public int HangUpUserTaskByInstanceId(string instanceId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_WF_UserTask Set _IsDel=2");
			stringBuilder.Append(" where instanceId=@instanceId and (TaskState='0' or TaskState='1') and _IsDel=0");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "instanceId", DbType.String, instanceId);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int RemoveActiveUserTask(string instanceId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_WF_UserTask ");
			stringBuilder.Append(" where instanceId=@instanceId and (TaskState='0' or TaskState='1')");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "instanceId", DbType.String, instanceId);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int ResumeUserTaskByInstanceId(string instanceId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_WF_UserTask Set _IsDel=0");
			stringBuilder.Append(" where instanceId=@instanceId and _IsDel=2");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "instanceId", DbType.String, instanceId);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int StopUserTaskByInstanceId(string instanceId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update from T_E_WF_UserTask Set _IsDel=1");
			stringBuilder.Append(" where instanceId=@instanceId and (TaskState='0' or TaskState='1')");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "instanceId", DbType.String, instanceId);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int Update(UserTask model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_WF_UserTask set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tInstanceId=@InstanceId,\r\n\t\t\t\t\tTaskId=@TaskId,\r\n\t\t\t\t\tAgentId=@AgentId,\r\n\t\t\t\t\tDealUser=@DealUser,\r\n\t\t\t\t\tTaskState=@TaskState,\r\n\t\t\t\t\tDealAdvice=@DealAdvice,\r\n\t\t\t\t\tMemo=@Memo,\r\n\t\t\t\t\tDealTime=@DealTime,\r\n\t\t\t\t\tDealAction=@DealAction,\r\n\t\t\t\t\tIsShare=@IsShare,\r\n                    \r\n                    EmployeeName=@EmployeeName,\r\n                    PositionId=@PositionId,\r\n                    PositionName=@PositionName,\r\n                    DeptId=@DeptId,\r\n                    DeptName=@DeptName,\r\n\r\n                    IsRead=@IsRead,\r\n                    ReadTime=@ReadTime,\r\n                    RecNames=@RecNames,\r\n                    RecIds=@RecIds,\r\n                    DealType=@DealType\r\n\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model.UserTaskId);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "InstanceId", DbType.String, model.InstanceId);
			SysDatabase.AddInParameter(sqlStringCommand, "TaskId", DbType.String, model.TaskId);
			SysDatabase.AddInParameter(sqlStringCommand, "OwnerId", DbType.String, model.OwnerId);
			SysDatabase.AddInParameter(sqlStringCommand, "AgentId", DbType.String, model.AgentId);
			SysDatabase.AddInParameter(sqlStringCommand, "DealUser", DbType.String, model.DealUser);
			SysDatabase.AddInParameter(sqlStringCommand, "TaskState", DbType.String, model.TaskState);
			SysDatabase.AddInParameter(sqlStringCommand, "DealAdvice", DbType.String, model.DealAdvice);
			SysDatabase.AddInParameter(sqlStringCommand, "Memo", DbType.String, model.Memo);
			SysDatabase.AddInParameter(sqlStringCommand, "DealTime", DbType.DateTime, model.DealTime);
			SysDatabase.AddInParameter(sqlStringCommand, "DealAction", DbType.String, model.DealAction);
			SysDatabase.AddInParameter(sqlStringCommand, "IsShare", DbType.String, model.IsShare);
			SysDatabase.AddInParameter(sqlStringCommand, "EmployeeName", DbType.String, model.EmployeeName);
			SysDatabase.AddInParameter(sqlStringCommand, "PositionId", DbType.String, model.PositionId);
			SysDatabase.AddInParameter(sqlStringCommand, "PositionName", DbType.String, model.PositionName);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptId", DbType.String, model.DeptId);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptName", DbType.String, model.DeptName);
			SysDatabase.AddInParameter(sqlStringCommand, "IsRead", DbType.String, model.IsRead);
			SysDatabase.AddInParameter(sqlStringCommand, "ReadTime", DbType.DateTime, model.ReadTime);
			SysDatabase.AddInParameter(sqlStringCommand, "RecNames", DbType.String, model.RecNames);
			SysDatabase.AddInParameter(sqlStringCommand, "RecIds", DbType.String, model.RecIds);
			SysDatabase.AddInParameter(sqlStringCommand, "DealType", DbType.String, model.DealType);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int UpdateAdvice(UserTask model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_WF_UserTask set \r\n                    _UpdateTime =@_UpdateTime,\r\n\t\t\t\t\tDealUser=@DealUser,\r\n\t\t\t\t\tTaskState=@TaskState,\r\n\t\t\t\t\tDealAdvice=@DealAdvice,\r\n\t\t\t\t\tMemo=@Memo,\r\n\t\t\t\t\tDealTime=@DealTime,\r\n\t\t\t\t\tReadTime=@ReadTime,\r\n\t\t\t\t\tDealAction=@DealAction,\r\n                    PositionId=@PositionId,\r\n                    PositionName=@PositionName,\r\n                    DeptId=@DeptId,\r\n                    DeptName=@DeptName,\r\n                    EmployeeName=@EmployeeName,\r\n                    IsRead=@IsRead,\r\n                    RecNames=@RecNames,\r\n                    RecIds=@RecIds\r\n\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model.UserTaskId);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "DealUser", DbType.String, model.DealUser);
			SysDatabase.AddInParameter(sqlStringCommand, "TaskState", DbType.String, model.TaskState);
			SysDatabase.AddInParameter(sqlStringCommand, "DealAdvice", DbType.String, model.DealAdvice);
			SysDatabase.AddInParameter(sqlStringCommand, "Memo", DbType.String, model.Memo);
			SysDatabase.AddInParameter(sqlStringCommand, "DealTime", DbType.DateTime, model.DealTime);
			SysDatabase.AddInParameter(sqlStringCommand, "ReadTime", DbType.DateTime, model.ReadTime);
			SysDatabase.AddInParameter(sqlStringCommand, "DealAction", DbType.String, model.DealAction);
			SysDatabase.AddInParameter(sqlStringCommand, "PositionId", DbType.String, model.PositionId);
			SysDatabase.AddInParameter(sqlStringCommand, "PositionName", DbType.String, model.PositionName);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptId", DbType.String, model.DeptId);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptName", DbType.String, model.DeptName);
			SysDatabase.AddInParameter(sqlStringCommand, "EmployeeName", DbType.String, model.EmployeeName);
			SysDatabase.AddInParameter(sqlStringCommand, "IsRead", DbType.String, model.IsRead);
			SysDatabase.AddInParameter(sqlStringCommand, "RecNames", DbType.String, model.RecNames);
			SysDatabase.AddInParameter(sqlStringCommand, "RecIds", DbType.String, model.RecIds);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}
	}
}