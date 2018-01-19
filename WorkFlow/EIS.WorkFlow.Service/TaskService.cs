using EIS.DataAccess;
using EIS.WorkFlow.Access;
using EIS.WorkFlow.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.WorkFlow.Service
{
	public class TaskService
	{
		public TaskService()
		{
		}

		public static void ApplyShareTask(string taskId, string uTaskId)
		{
			TaskService.ApplyShareTask(taskId, uTaskId, null);
		}

		public static void ApplyShareTask(string taskId, string uTaskId, DbTransaction tran)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("update T_E_WF_UserTask set TaskState='1' ", new object[0]);
			stringBuilder.AppendFormat(" where _AutoId='{0}' and IsShare=1; ", uTaskId);
			stringBuilder.AppendFormat("update T_E_WF_UserTask set TaskState='3' ", new object[0]);
			stringBuilder.AppendFormat(" where TaskId='{0}' and IsShare=1 and _AutoId<>'{1}' ; ", taskId, uTaskId);
			UserTask userTaskById = UserTaskService.GetUserTaskById(uTaskId, tran);
			stringBuilder.AppendFormat("update T_E_WF_Task set MainPerformer='{1}' where _AutoID='{0}'; ", taskId, userTaskById.OwnerId);
			if (tran != null)
			{
				SysDatabase.ExecuteNonQuery(stringBuilder.ToString(), tran);
			}
			else
			{
				SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
			}
		}

		public static void BackShareTask(string taskId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("update T_E_WF_UserTask set TaskState='0' ", new object[0]);
			stringBuilder.AppendFormat(" where TaskId='{0}'  and IsShare=1", taskId);
			stringBuilder.AppendFormat("update T_E_WF_Task set MainPerformer='' ", new object[0]);
			stringBuilder.AppendFormat(" where _AutoID='{0}' ", taskId);
			SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
		}

		public static bool CheckTaskReSubmit(string fromTaskId, string toActId, DbTransaction dbTran)
		{
			bool flag;
			string str = string.Format("select count(*) from T_E_WF_Task where FromTaskId='{0}' and activityId='{1}'", fromTaskId, toActId);
			flag = (dbTran != null ? Convert.ToInt32(SysDatabase.ExecuteScalar(str, dbTran)) > 0 : Convert.ToInt32(SysDatabase.ExecuteScalar(str)) > 0);
			return flag;
		}

		public static int GetActivityState(string instanceId, string actId)
		{
			int num;
			_Task __Task = new _Task();
			string[] strArrays = new string[] { "InstanceId='", instanceId, "' and activityId='", actId, "'" };
			DataTable list = __Task.GetList(string.Concat(strArrays));
			if ((int)list.Select("taskstate='0' or taskstate='1'").Length <= 0)
			{
				num = ((int)list.Select("taskstate='2'").Length <= 0 ? 0 : 2);
			}
			else
			{
				num = 1;
			}
			return num;
		}

		public static int GetFinishedUserTaskCount(string instanceId, Task task, DbTransaction dbTran)
		{
			_UserTask __UserTask = new _UserTask(dbTran);
			string str = string.Format(" TaskId='{1}' and InstanceId='{0}' and TaskState='2'", instanceId, task.TaskId);
			return __UserTask.GetModelList(str).Count;
		}

		public static int GetFinishedUserTaskCount(string instanceId, Task task)
		{
			_UserTask __UserTask = new _UserTask();
			string str = string.Format(" _isDel=0 and TaskId='{1}' and InstanceId='{0}' and TaskState='2'", instanceId, task.TaskId);
			return __UserTask.GetModelList(str).Count;
		}

		public static List<Task> GetNextManualTask(string taskId)
		{
			List<Task> tasks = new List<Task>();
			foreach (Task modelList in (new _Task()).GetModelList(string.Format(" FromTaskId='{0}'", taskId)))
			{
				if (!(modelList.IsManualTask == "1"))
				{
					tasks.AddRange(TaskService.GetNextManualTask(modelList.TaskId));
				}
				else
				{
					tasks.Add(modelList);
				}
			}
			return tasks;
		}

		public static List<Task> GetNextTask(string taskId)
		{
			_Task __Task = new _Task();
			return __Task.GetModelList(string.Format(" FromTaskId='{0}'", taskId));
		}

		public static DataTable GetOverTimeTasks()
		{
			return SysDatabase.ExecuteTable("select * from T_E_WF_Task\r\n                    where _IsDel=0 and (OverTimeAlertFirst is null or getdate() > OverTimeAlertFirst) and Deadline is not null \r\n                    and (TaskState='0' or TaskState='1') and isnull(OverTimeAction,'')<>''");
		}

		public static IList GetShareTaskDealUser(string taskId, StringCollection list, DbTransaction dbTran)
		{
			StringCollection stringCollections = new StringCollection();
			_UserTask __UserTask = new _UserTask(dbTran);
			foreach (UserTask modelList in __UserTask.GetModelList(string.Format(" _isDel=0 and TaskId='{0}' and IsShare=1 and IsNull(IsAssign,0)=0", taskId)))
			{
				stringCollections.Add(modelList.OwnerId);
				if (list != null)
				{
					list.Add(modelList.OwnerId);
				}
			}
			return stringCollections;
		}

		public static Task GetTaskById(string taskId, DbTransaction dbTran)
		{
			return (new _Task(dbTran)).GetModel(taskId);
		}

		public static Task GetTaskById(string taskId)
		{
			return (new _Task()).GetModel(taskId);
		}

		public static IList GetTaskDealUser(string taskId, StringCollection list, DbTransaction dbTran)
		{
			StringCollection stringCollections = new StringCollection();
			_UserTask __UserTask = new _UserTask(dbTran);
			foreach (UserTask modelList in __UserTask.GetModelList(string.Format(" _isDel=0 and TaskId='{0}' ", taskId)))
			{
				stringCollections.Add(modelList.OwnerId);
				if (list != null)
				{
					list.Add(modelList.OwnerId);
				}
			}
			return stringCollections;
		}

		public static List<UserTask> GetToDoUserTask(string instanceId, Task task)
		{
			_UserTask __UserTask = new _UserTask();
			string str = string.Format(" _isdel=0 and TaskId='{1}' and InstanceId='{0}' and TaskState='1'", instanceId, task.TaskId);
			return __UserTask.GetModelList(str);
		}

		public static List<UserTask> GetToDoUserTaskByTaskId(string taskId, bool allTask)
		{
			_UserTask __UserTask = new _UserTask();
			string str = string.Format(" _isdel=0 and TaskId='{0}' and TaskState='1'", taskId);
			if (allTask)
			{
				str = string.Format(" _isdel=0 and TaskId='{0}' and (TaskState='1' or TaskState='0')", taskId);
			}
			return __UserTask.GetModelList(str);
		}

		public static List<UserTask> GetToDoUserTaskByTaskId(string taskId)
		{
			return TaskService.GetToDoUserTaskByTaskId(taskId, false);
		}

		public static int GetToDoUserTaskCountByTaskId(string taskId, DbTransaction dbTran)
		{
			int num;
			_UserTask __UserTask = new _UserTask();
			string str = string.Format("select count(*) from T_E_WF_UserTask where _isdel=0 and TaskId='{0}' and TaskState='1'", taskId);
			num = (dbTran != null ? Convert.ToInt32(SysDatabase.ExecuteScalar(str, dbTran)) : Convert.ToInt32(SysDatabase.ExecuteScalar(str)));
			return num;
		}

		public static List<UserTask> GetUserTask(string instanceId, Task task, DbTransaction dbTran)
		{
			_UserTask __UserTask = new _UserTask(dbTran);
			string str = string.Format(" _isDel=0 and TaskId='{1}' and InstanceId='{0}'", instanceId, task.TaskId);
			return __UserTask.GetModelList(str);
		}

		public static List<UserTask> GetUserTask(string instanceId, Task task)
		{
			_UserTask __UserTask = new _UserTask();
			string str = string.Format(" _isDel=0 and TaskId='{1}' and InstanceId='{0}'", instanceId, task.TaskId);
			return __UserTask.GetModelList(str);
		}

		public static List<UserTask> GetValidUserTask(string instanceId, Task task, DbTransaction dbTran)
		{
			_UserTask __UserTask = new _UserTask(dbTran);
			string str = string.Format(" _isDel=0 and TaskId='{1}' and InstanceId='{0}' and (TaskState='1' or TaskState='2')", instanceId, task.TaskId);
			return __UserTask.GetModelList(str);
		}
	}
}