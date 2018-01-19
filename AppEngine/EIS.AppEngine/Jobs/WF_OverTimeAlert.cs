using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.AppModel.Service;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.Permission.Service;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using EIS.WorkFlow.XPDLParser.Elements;
using NLog;
using Quartz;
using Quartz.Util;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace EIS.AppEngine.Jobs
{
	public class WF_OverTimeAlert : IJob
	{
		private Logger fileLogger = LogManager.GetCurrentClassLogger();

		public WF_OverTimeAlert()
		{
		}

		public void Execute(IJobExecutionContext context)
		{
			string str = context.JobDetail.JobDataMap.GetString("TaskId");
			Task taskById = TaskService.GetTaskById(str);
			if (taskById == null)
			{
				context.Scheduler.DeleteJob(context.JobDetail.Key);
				WF_OverTimeCheck.RemoveTask(str);
			}
			else if (AppWorkDayService.IsWorkTime(taskById.OverTimeCalendarId))
			{
				this.fileLogger.Trace("WF_OverTimeAlert，Execute {0}", str);
				if (!(taskById.TaskState == "2"))
				{
					this.SendOverTimeAlert(taskById);
				}
				else
				{
					this.fileLogger.Info("WF_OverTimeAlert，任务已经完成，TaskState=2 ，自动取消任务,taskId={0}", str);
					context.Scheduler.DeleteJob(context.JobDetail.Key);
					WF_OverTimeCheck.RemoveTask(str);
				}
			}
		}

		private void SendOverTimeAlert(Task task)
		{
			string mail;
			MailMessage mailMessage;
			string webId = AppSettings.Instance.WebId;
			List<UserTask> toDoUserTaskByTaskId = TaskService.GetToDoUserTaskByTaskId(task.TaskId);
			DriverEngine driverEngine = new DriverEngine();
			Instance instanceById = driverEngine.GetInstanceById(task.InstanceId);
			Activity activityById = driverEngine.GetActivityById(instanceById, task.ActivityId);
			string extendedAttribute = activityById.GetExtendedAttribute("OverTimeAlert");
			char[] chrArray = new char[] { '|' };
			string[] strArrays = extendedAttribute.Split(chrArray);
			string str = "1,0,0";
			if ((int)strArrays.Length > 4)
			{
				str = strArrays[4];
			}
			chrArray = new char[] { ',' };
			string[] strArrays1 = str.Split(chrArray);
			this.fileLogger.Trace<int, string, string>("SendOverTimeAlert，返回{0}个用户任务 TaskId={1} ,alertOption={2}", toDoUserTaskByTaskId.Count, task.TaskId, str);
			foreach (UserTask userTask in toDoUserTaskByTaskId)
			{
				if (strArrays1[0] == "1")
				{
					UserContext userContext = new UserContext()
					{
						EmployeeId = "system",
						DeptWbs = "0"
					};
					AppMsg appMsg = new AppMsg(userContext)
					{
						Title = "审批任务超时提醒",
						MsgType = "超时提醒",
						MsgUrl = string.Concat("SysFolder/Workflow/DealFlow.aspx?TaskId=", userTask.UserTaskId),
						RecIds = userTask.OwnerId,
						RecNames = userTask.EmployeeName,
						SendTime = new DateTime?(DateTime.Now),
						Sender = "系统管理员",
						Content = string.Format("审批任务超时提醒：【{0}】即将超时，请尽快发表意见。", instanceById.InstanceName)
					};
					AppMsgService.SendMessage(appMsg);
				}
				this.fileLogger.Info<string, string>("超时提醒,taskId={0},{1}", task.TaskId, userTask.EmployeeName);
				if (strArrays1[1] == "1")
				{
					try
					{
						string str1 = string.Concat("超时提醒:", instanceById.InstanceName);
						string str2 = string.Format("审批任务超时提醒：【{0}】即将超时，请尽快发表意见。", instanceById.InstanceName);
						string str3 = string.Concat(AppSettings.GetRootURI(webId), "/SysFolder/Workflow/DealFlow.aspx?TaskId=", userTask.UserTaskId);
						string itemValue = SysConfig.GetConfig("ReloginLinkInMail").ItemValue;
						string ownerId = userTask.OwnerId;
						if (!(itemValue == "否"))
						{
							mailMessage = new MailMessage()
							{
								Subject = str1,
								IsBodyHtml = true,
								Priority = MailPriority.Normal,
								Body = string.Concat(str2, string.Format("<br/><a href='{0}' target='_blank'>点击处理任务</a>", str3))
							};
							mail = EmployeeService.GetMail(userTask.OwnerId);
							if (mail != "")
							{
								mailMessage.To.Add(mail);
								EIS.WorkFlow.Engine.Utility.SendMail(mailMessage);
							}
						}
						else
						{
							mail = EmployeeService.GetMail(ownerId);
							this.fileLogger.Info("超时邮件提醒,mailAddr={0},relogin=否", mail);
							if (mail != "")
							{
								mailMessage = new MailMessage();
								mailMessage.To.Add(mail);
								mailMessage.Subject = str1;
								mailMessage.IsBodyHtml = true;
								mailMessage.Priority = MailPriority.Normal;
								string str4 = AppSettings.GenAutoLoginUrl(EmployeeService.GetEmployeeAttrById(ownerId, "LoginName"), webId, str3, 0);
								mailMessage.Body = string.Concat(str2, string.Format("<br/><a href='{0}' target='_blank'>点击处理任务</a>", str4));
								EIS.WorkFlow.Engine.Utility.SendMail(mailMessage);
							}
						}
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						this.fileLogger.Error<string, string>("超时提醒发送邮件时出错，taskId={0},{1}", task.TaskId, userTask.EmployeeName);
						this.fileLogger.Error<Exception>(exception);
					}
				}
				if (strArrays1[2] == "1")
				{
					_AppSms _AppSm = new _AppSms();
					AppSms appSm = new AppSms()
					{
						_AutoID = Guid.NewGuid().ToString(),
						_UserName = "system",
						_OrgCode = "",
						_CreateTime = DateTime.Now,
						_UpdateTime = DateTime.Now,
						_IsDel = 0,
						SenderId = "system",
						SenderName = "系统管理员",
						CompanyId = "",
						AppId = instanceById.AppId,
						AppName = instanceById.AppName,
						Content = string.Format("审批任务超时提醒：【{0}】即将超时，请尽快处理。", instanceById.InstanceName),
						State = "0",
						RecIds = userTask.OwnerId,
						RecNames = userTask.EmployeeName,
						RecPhone = ""
					};
					_AppSm.Add(appSm);
				}
			}
		}
	}
}