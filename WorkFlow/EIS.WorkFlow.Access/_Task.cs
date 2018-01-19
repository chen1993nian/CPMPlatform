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
	public class _Task
	{
		private DbTransaction _tran = null;

		public _Task()
		{
		}

		public _Task(DbTransaction tran)
		{
			this._tran = tran;
		}

		public int Add(Task model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_WF_Task (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tInstanceId,\r\n\t\t\t\t\tTaskName,\r\n\t\t\t\t\tTaskType,\r\n\t\t\t\t\tDefineType,\r\n\t\t\t\t\tActivityId,\r\n\t\t\t\t\tArriveTime,\r\n\t\t\t\t\tFromTaskId,\r\n\t\t\t\t\tIsManualTask,\r\n\t\t\t\t\tCanHangUp,\r\n\t\t\t\t\tCanStop,\r\n\t\t\t\t\tCanRollBack,\r\n\t\t\t\t\tCanDelegateTo,\r\n\t\t\t\t\tCanAssign,\r\n\t\t\t\t\tCanPublic,\r\n\t\t\t\t\tCanJump,\r\n\t\t\t\t\tCanFetch,\r\n\t\t\t\t\tCanReturn,\r\n\t\t\t\t\tCanAttachment,\r\n\t\t\t\t\tCanSelPath,\r\n\t\t\t\t\tCanBatch,\r\n\t\t\t\t\tTaskState,\r\n                    Deadline,\r\n                    OverTimeAlert,\r\n                    OverTimeAlertFirst,\r\n                    OverTimeAlertRepeat,\r\n                    OverTimeAlertInterval,\r\n                    OverTimeAction,\r\n                    DealStrategy,\r\n                    MainPerformer,\r\n                    HideAdviceOnDeal,\r\n                    OverTimeCalendarId,\r\n                    NodeCode,\r\n                    NodeStyle\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@InstanceId,\r\n\t\t\t\t\t@TaskName,\r\n\t\t\t\t\t@TaskType,\r\n\t\t\t\t\t@DefineType,\r\n\t\t\t\t\t@ActivityId,\r\n\t\t\t\t\t@ArriveTime,\r\n\t\t\t\t\t@FromTaskId,\r\n\t\t\t\t\t@IsManualTask,\r\n\t\t\t\t\t@CanHangUp,\r\n\t\t\t\t\t@CanStop,\r\n\t\t\t\t\t@CanRollBack,\r\n\t\t\t\t\t@CanDelegateTo,\r\n\t\t\t\t\t@CanAssign,\r\n\t\t\t\t\t@CanPublic,\r\n\t\t\t\t\t@CanJump,\r\n\t\t\t\t\t@CanFetch,\r\n\t\t\t\t\t@CanReturn,\r\n\t\t\t\t\t@CanAttachment,\r\n                    @CanSelPath,\r\n                    @CanBatch,\r\n\t\t\t\t\t@TaskState,\r\n                    @Deadline,\r\n                    @OverTimeAlert,\r\n                    @OverTimeAlertFirst,\r\n                    @OverTimeAlertRepeat,\r\n                    @OverTimeAlertInterval,\r\n                    @OverTimeAction,\r\n                    @DealStrategy,\r\n                    @MainPerformer,\r\n                    @HideAdviceOnDeal,\r\n                    @OverTimeCalendarId,\r\n                    @NodeCode,\r\n                    @NodeStyle\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model.TaskId);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "InstanceId", DbType.String, model.InstanceId);
			SysDatabase.AddInParameter(sqlStringCommand, "TaskName", DbType.String, model.TaskName);
			SysDatabase.AddInParameter(sqlStringCommand, "TaskType", DbType.String, model.TaskType);
			SysDatabase.AddInParameter(sqlStringCommand, "DefineType", DbType.String, model.DefineType);
			SysDatabase.AddInParameter(sqlStringCommand, "ActivityId", DbType.String, model.ActivityId);
			SysDatabase.AddInParameter(sqlStringCommand, "ArriveTime", DbType.DateTime, model.ArriveTime);
			SysDatabase.AddInParameter(sqlStringCommand, "FromTaskId", DbType.String, model.FromTaskId);
			SysDatabase.AddInParameter(sqlStringCommand, "IsManualTask", DbType.String, model.IsManualTask);
			SysDatabase.AddInParameter(sqlStringCommand, "CanHangUp", DbType.String, model.CanHangUp);
			SysDatabase.AddInParameter(sqlStringCommand, "CanStop", DbType.String, model.CanStop);
			SysDatabase.AddInParameter(sqlStringCommand, "CanRollBack", DbType.String, model.CanRollBack);
			SysDatabase.AddInParameter(sqlStringCommand, "CanDelegateTo", DbType.String, model.CanDelegateTo);
			SysDatabase.AddInParameter(sqlStringCommand, "CanAssign", DbType.String, model.CanAssign);
			SysDatabase.AddInParameter(sqlStringCommand, "CanPublic", DbType.String, model.CanPublic);
			SysDatabase.AddInParameter(sqlStringCommand, "CanJump", DbType.String, model.CanJump);
			SysDatabase.AddInParameter(sqlStringCommand, "CanFetch", DbType.String, model.CanFetch);
			SysDatabase.AddInParameter(sqlStringCommand, "CanReturn", DbType.String, model.CanReturn);
			SysDatabase.AddInParameter(sqlStringCommand, "CanAttachment", DbType.String, model.CanAttachment);
			SysDatabase.AddInParameter(sqlStringCommand, "CanSelPath", DbType.String, model.CanSelPath);
			SysDatabase.AddInParameter(sqlStringCommand, "CanBatch", DbType.String, model.CanBatch);
			SysDatabase.AddInParameter(sqlStringCommand, "TaskState", DbType.String, model.TaskState);
			SysDatabase.AddInParameter(sqlStringCommand, "Deadline", DbType.DateTime, model.Deadline);
			SysDatabase.AddInParameter(sqlStringCommand, "OverTimeAlert", DbType.Int32, model.OverTimeAlert);
			SysDatabase.AddInParameter(sqlStringCommand, "OverTimeAlertFirst", DbType.DateTime, model.OverTimeAlertFirst);
			SysDatabase.AddInParameter(sqlStringCommand, "OverTimeAlertRepeat", DbType.Int32, model.OverTimeAlertRepeat);
			SysDatabase.AddInParameter(sqlStringCommand, "OverTimeAlertInterval", DbType.Int32, model.OverTimeAlertInterval);
			SysDatabase.AddInParameter(sqlStringCommand, "OverTimeAction", DbType.String, model.OverTimeAction);
			SysDatabase.AddInParameter(sqlStringCommand, "DealStrategy", DbType.String, model.DealStrategy);
			SysDatabase.AddInParameter(sqlStringCommand, "MainPerformer", DbType.String, model.MainPerformer);
			SysDatabase.AddInParameter(sqlStringCommand, "HideAdviceOnDeal", DbType.String, model.HideAdviceOnDeal);
			SysDatabase.AddInParameter(sqlStringCommand, "OverTimeCalendarId", DbType.String, model.OverTimeCalendarId);
			SysDatabase.AddInParameter(sqlStringCommand, "NodeCode", DbType.String, model.NodeCode);
			SysDatabase.AddInParameter(sqlStringCommand, "NodeStyle", DbType.String, model.NodeStyle);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int Delete(string key)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_WF_Task ");
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
			stringBuilder.Append("delete from T_E_WF_Task ");
			stringBuilder.Append(" where instanceId=@instanceId ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "instanceId", DbType.String, instanceId);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_WF_Task ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			DataTable dataTable = new DataTable();
			dataTable = (this._tran == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this._tran));
			return dataTable;
		}

		public Task GetModel(string key)
		{
			Task model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_WF_Task ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, key);
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

		public Task GetModel(DataRow dr)
		{
			Task task = new Task()
			{
				TaskId = dr["_AutoID"].ToString(),
				_UserName = dr["_UserName"].ToString(),
				_OrgCode = dr["_OrgCode"].ToString()
			};
			if (dr["_CreateTime"].ToString() != "")
			{
				task._CreateTime = DateTime.Parse(dr["_CreateTime"].ToString());
			}
			if (dr["_UpdateTime"].ToString() != "")
			{
				task._UpdateTime = DateTime.Parse(dr["_UpdateTime"].ToString());
			}
			if (dr["_IsDel"].ToString() != "")
			{
				task._IsDel = int.Parse(dr["_IsDel"].ToString());
			}
			task.InstanceId = dr["InstanceId"].ToString();
			task.TaskName = dr["TaskName"].ToString();
			task.TaskType = dr["TaskType"].ToString();
			task.DefineType = dr["DefineType"].ToString();
			task.ActivityId = dr["ActivityId"].ToString();
			if (dr["ArriveTime"].ToString() != "")
			{
				task.ArriveTime = DateTime.Parse(dr["ArriveTime"].ToString());
			}
			task.FromTaskId = dr["FromTaskId"].ToString();
			task.IsManualTask = dr["IsManualTask"].ToString();
			task.CanHangUp = dr["CanHangUp"].ToString();
			task.CanStop = dr["CanStop"].ToString();
			task.CanRollBack = dr["CanRollBack"].ToString();
			task.CanDelegateTo = dr["CanDelegateTo"].ToString();
			task.CanAssign = dr["CanAssign"].ToString();
			task.CanPublic = dr["CanPublic"].ToString();
			task.CanJump = dr["CanJump"].ToString();
			task.CanFetch = dr["CanFetch"].ToString();
			task.CanReturn = dr["CanReturn"].ToString();
			task.CanAttachment = dr["CanAttachment"].ToString();
			task.CanSelPath = dr["CanSelPath"].ToString();
			task.CanBatch = dr["CanBatch"].ToString();
			task.TaskState = dr["TaskState"].ToString();
			if (dr["Deadline"].ToString() != "")
			{
				task.Deadline = new DateTime?(DateTime.Parse(dr["Deadline"].ToString()));
			}
			if (dr["OverTimeAlert"].ToString() != "")
			{
				task.OverTimeAlert = int.Parse(dr["OverTimeAlert"].ToString());
			}
			if (dr["OverTimeAlertFirst"].ToString() != "")
			{
				task.OverTimeAlertFirst = new DateTime?(DateTime.Parse(dr["OverTimeAlertFirst"].ToString()));
			}
			if (dr["OverTimeAlertRepeat"].ToString() != "")
			{
				task.OverTimeAlertRepeat = int.Parse(dr["OverTimeAlertRepeat"].ToString());
			}
			if (dr["OverTimeAlertInterval"].ToString() != "")
			{
				task.OverTimeAlertInterval = int.Parse(dr["OverTimeAlertInterval"].ToString());
			}
			task.OverTimeAction = dr["OverTimeAction"].ToString();
			task.DealStrategy = dr["DealStrategy"].ToString();
			task.MainPerformer = dr["MainPerformer"].ToString();
			task.HideAdviceOnDeal = dr["HideAdviceOnDeal"].ToString();
			task.OverTimeCalendarId = dr["OverTimeCalendarId"].ToString();
			task.NodeCode = dr["NodeCode"].ToString();
			task.NodeStyle = dr["NodeStyle"].ToString();
			return task;
		}

		public List<Task> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Task> tasks = new List<Task>();
			stringBuilder.Append("select *  From T_E_WF_Task ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			DataTable dataTable = new DataTable();
			dataTable = (this._tran == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this._tran));
			foreach (DataRow row in dataTable.Rows)
			{
				tasks.Add(this.GetModel(row));
			}
			return tasks;
		}

		public int RemoveActiveTask(string instanceId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_WF_Task ");
			stringBuilder.Append(" where instanceId=@instanceId and TaskState='0'");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "instanceId", DbType.String, instanceId);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int Update(Task model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_WF_Task set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\tInstanceId=@InstanceId,\r\n\t\t\t\t\tTaskName=@TaskName,\r\n\t\t\t\t\tTaskType=@TaskType,\r\n\t\t\t\t\tDefineType=@DefineType,\r\n\t\t\t\t\tActivityId=@ActivityId,\r\n\t\t\t\t\tArriveTime=@ArriveTime,\r\n\t\t\t\t\tFromTaskId=@FromTaskId,\r\n\t\t\t\t\tIsManualTask=@IsManualTask,\r\n\t\t\t\t\tCanHangUp=@CanHangUp,\r\n\t\t\t\t\tCanStop=@CanStop,\r\n\t\t\t\t\tCanRollBack=@CanRollBack,\r\n\t\t\t\t\tCanDelegateTo=@CanDelegateTo,\r\n\t\t\t\t\tCanAssign=@CanAssign,\r\n\t\t\t\t\tCanPublic=@CanPublic,\r\n\t\t\t\t\tCanJump=@CanJump,\r\n\t\t\t\t\tCanFetch=@CanFetch,\r\n\t\t\t\t\tCanReturn=@CanReturn,\r\n\t\t\t\t\tCanAttachment=@CanAttachment,\r\n\t\t\t\t\tCanSelPath=@CanSelPath,\r\n                    CanBatch=@CanBatch,\r\n\t\t\t\t\tTaskState=@TaskState,\r\n                    Deadline = @Deadline,\r\n                    OverTimeAlert = @OverTimeAlert,\r\n                    OverTimeAlertFirst = @OverTimeAlertFirst,\r\n                    OverTimeAlertRepeat = @OverTimeAlertRepeat,\r\n                    OverTimeAlertInterval = @OverTimeAlertInterval,\r\n                    OverTimeAction = @OverTimeAction,\r\n                    DealStrategy = @DealStrategy,\r\n                    MainPerformer = @MainPerformer,\r\n                    HideAdviceOnDeal = @HideAdviceOnDeal,\r\n                    OverTimeCalendarId = @OverTimeCalendarId,\r\n                    NodeCode = @NodeCode,\r\n                    NodeStyle = @NodeStyle\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model.TaskId);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "InstanceId", DbType.String, model.InstanceId);
			SysDatabase.AddInParameter(sqlStringCommand, "TaskName", DbType.String, model.TaskName);
			SysDatabase.AddInParameter(sqlStringCommand, "TaskType", DbType.String, model.TaskType);
			SysDatabase.AddInParameter(sqlStringCommand, "DefineType", DbType.String, model.DefineType);
			SysDatabase.AddInParameter(sqlStringCommand, "ActivityId", DbType.String, model.ActivityId);
			SysDatabase.AddInParameter(sqlStringCommand, "ArriveTime", DbType.DateTime, model.ArriveTime);
			SysDatabase.AddInParameter(sqlStringCommand, "FromTaskId", DbType.String, model.FromTaskId);
			SysDatabase.AddInParameter(sqlStringCommand, "IsManualTask", DbType.String, model.IsManualTask);
			SysDatabase.AddInParameter(sqlStringCommand, "CanHangUp", DbType.String, model.CanHangUp);
			SysDatabase.AddInParameter(sqlStringCommand, "CanStop", DbType.String, model.CanStop);
			SysDatabase.AddInParameter(sqlStringCommand, "CanRollBack", DbType.String, model.CanRollBack);
			SysDatabase.AddInParameter(sqlStringCommand, "CanDelegateTo", DbType.String, model.CanDelegateTo);
			SysDatabase.AddInParameter(sqlStringCommand, "CanAssign", DbType.String, model.CanAssign);
			SysDatabase.AddInParameter(sqlStringCommand, "CanPublic", DbType.String, model.CanPublic);
			SysDatabase.AddInParameter(sqlStringCommand, "CanJump", DbType.String, model.CanJump);
			SysDatabase.AddInParameter(sqlStringCommand, "CanFetch", DbType.String, model.CanFetch);
			SysDatabase.AddInParameter(sqlStringCommand, "CanReturn", DbType.String, model.CanReturn);
			SysDatabase.AddInParameter(sqlStringCommand, "CanAttachment", DbType.String, model.CanAttachment);
			SysDatabase.AddInParameter(sqlStringCommand, "CanSelPath", DbType.String, model.CanSelPath);
			SysDatabase.AddInParameter(sqlStringCommand, "CanBatch", DbType.String, model.CanBatch);
			SysDatabase.AddInParameter(sqlStringCommand, "TaskState", DbType.String, model.TaskState);
			SysDatabase.AddInParameter(sqlStringCommand, "Deadline", DbType.DateTime, model.Deadline);
			SysDatabase.AddInParameter(sqlStringCommand, "OverTimeAlert", DbType.Int32, model.OverTimeAlert);
			SysDatabase.AddInParameter(sqlStringCommand, "OverTimeAlertFirst", DbType.DateTime, model.OverTimeAlertFirst);
			SysDatabase.AddInParameter(sqlStringCommand, "OverTimeAlertRepeat", DbType.Int32, model.OverTimeAlertRepeat);
			SysDatabase.AddInParameter(sqlStringCommand, "OverTimeAlertInterval", DbType.Int32, model.OverTimeAlertInterval);
			SysDatabase.AddInParameter(sqlStringCommand, "OverTimeAction", DbType.String, model.OverTimeAction);
			SysDatabase.AddInParameter(sqlStringCommand, "DealStrategy", DbType.String, model.DealStrategy);
			SysDatabase.AddInParameter(sqlStringCommand, "MainPerformer", DbType.String, model.MainPerformer);
			SysDatabase.AddInParameter(sqlStringCommand, "HideAdviceOnDeal", DbType.String, model.HideAdviceOnDeal);
			SysDatabase.AddInParameter(sqlStringCommand, "OverTimeCalendarId", DbType.String, model.OverTimeCalendarId);
			SysDatabase.AddInParameter(sqlStringCommand, "NodeCode", DbType.String, model.NodeCode);
			SysDatabase.AddInParameter(sqlStringCommand, "NodeStyle", DbType.String, model.NodeStyle);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}
	}
}