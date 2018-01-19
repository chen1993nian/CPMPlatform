using EIS.AppBase;
using EIS.AppModel.Service;
using EIS.DataAccess;
using EIS.Permission;
using EIS.Permission.Model;
using EIS.Permission.Service;
using EIS.WorkFlow.Access;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using EIS.WorkFlow.XPDLParser.Elements;
using NLog;
using Quartz;
using Quartz.Util;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;

namespace EIS.AppEngine.Jobs
{
	public class WF_OverTimeAction : IJob
	{
		private Logger fileLogger = LogManager.GetCurrentClassLogger();

		public WF_OverTimeAction()
		{
		}

		private void ExecOverTimeAction(Task curTask, UserTask uTask)
		{
			string webId = AppSettings.Instance.WebId;
			DeptEmployee deptEmployeeByPositionId = DeptEmployeeService.GetDeptEmployeeByPositionId(uTask.OwnerId, uTask.PositionId);
			UserContext userInfo = EIS.Permission.Utility.GetUserInfo(deptEmployeeByPositionId._AutoID, webId);
			DriverEngine driverEngine = new DriverEngine(userInfo);
			string taskId = uTask.TaskId;
			Instance instanceById = driverEngine.GetInstanceById(curTask.InstanceId);
			Activity activityById = driverEngine.GetActivityById(instanceById, curTask.ActivityId);
			DbConnection dbConnection = SysDatabase.CreateConnection();
			dbConnection.Open();
			DbTransaction dbTransaction = dbConnection.BeginTransaction();
			try
			{
				try
				{
					_UserTask __UserTask = new _UserTask(dbTransaction);
					uTask._UpdateTime = DateTime.Now;
					uTask.DealTime = new DateTime?(DateTime.Now);
					if (!uTask.ReadTime.HasValue)
					{
						uTask.ReadTime = new DateTime?(DateTime.Now);
					}
					string str = "〔系统自动〕";
					string str1 = "提交";
					if ((!activityById.IsDecisionNode() ? false : curTask.CanReturn != "1"))
					{
						str = "同意〔系统自动〕";
						str1 = "同意";
					}
					uTask.DealAdvice = str;
					uTask.DealAction = str1;
					uTask.TaskState = 2.ToString();
					uTask.DealUser = userInfo.EmployeeId;
					uTask.PositionId = userInfo.PositionId;
					uTask.PositionName = userInfo.PositionName;
					uTask.DeptId = userInfo.DeptId;
					uTask.DeptName = userInfo.DeptName;
					__UserTask.UpdateAdvice(uTask);
					if ((curTask.DefineType == ActivityType.Sign.ToString() ? true : curTask.MainPerformer == uTask.OwnerId))
					{
						List<Task> tasks = driverEngine.NextTask(instanceById, curTask, dbTransaction);
						if (instanceById.NeedUpdate)
						{
							(new _Instance(dbTransaction)).UpdateXPDL(instanceById);
						}
						if (tasks.Count > 0)
						{
							StringCollection stringCollections = new StringCollection();
							foreach (Task task in tasks)
							{
								TaskService.GetTaskDealUser(task.TaskId, stringCollections, dbTransaction);
							}
							uTask.RecIds = EIS.AppBase.Utility.GetJoinString(stringCollections);
							uTask.RecNames = EmployeeService.GetEmployeeNameList(stringCollections);
							__UserTask.UpdateAdvice(uTask);
						}
						if ((curTask.DefineType != ActivityType.Normal.ToString() ? false : curTask.MainPerformer == uTask.OwnerId))
						{
							EIS.WorkFlow.Engine.Utility.DeleteUnDoneUserTaskByTaskId(curTask.TaskId, dbTransaction);
						}
					}
					dbTransaction.Commit();
					this.fileLogger.Info("任务自动提交完成，taskId={0}", uTask.TaskId);
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					dbTransaction.Rollback();
					string str2 = EIS.AppBase.Utility.FormatException(exception, "");
					DataLog dataLog = new DataLog()
					{
						AppID = "",
						AppName = "",
						ComputeIP = "",
						ServerIP = "",
						LogType = "错误",
						LogUser = "system",
						UserName = "system",
						ModuleCode = "",
						ModuleName = "",
						Message = str2,
						Browser = "",
						Platform = "",
						UserAgent = ""
					};
					(new _DataLog()).WriteExceptionLog(dataLog);
				}
			}
			finally
			{
				dbConnection.Close();
			}
		}

		public void Execute(IJobExecutionContext context)
		{
			//context.JobDetail.Key.get_Name();
			string str = context.JobDetail.JobDataMap.GetString("TaskId");
			Task taskById = TaskService.GetTaskById(str);
			if (taskById == null)
			{
				context.Scheduler.DeleteJob(context.JobDetail.Key);
				WF_OverTimeCheck.RemoveTask(str);
			}
			else if (AppWorkDayService.IsWorkTime(taskById.OverTimeCalendarId))
			{
				if (!(taskById.TaskState == "2"))
				{
					foreach (UserTask toDoUserTaskByTaskId in TaskService.GetToDoUserTaskByTaskId(taskById.TaskId))
					{
						try
						{
							string str1 = taskById.OverTimeAction.Substring(0, 1);
							if ((str1 == "2" ? true : str1 == "4"))
							{
								if (str1 == "2")
								{
									taskById.CanReturn = "1";
								}
								this.ExecOverTimeAction(taskById, toDoUserTaskByTaskId);
								context.Scheduler.DeleteJob(context.JobDetail.Key);
								WF_OverTimeCheck.RemoveTask(str);
							}
						}
						catch (Exception exception)
						{
							this.fileLogger.Error<Exception>("在执行ExecOverTimeAction时出错{0}", exception);
						}
					}
				}
				else
				{
					this.fileLogger.Info("WF_OverTimeAlert，任务已经完成，TaskState=2 ，自动取消任务,taskId={0}", str);
					context.Scheduler.DeleteJob(context.JobDetail.Key);
					WF_OverTimeCheck.RemoveTask(str);
				}
			}
		}
	}
}