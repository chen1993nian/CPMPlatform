using EIS.AppEngine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using NLog;
using Quartz;
using Quartz.Util;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Runtime.CompilerServices;

namespace EIS.AppEngine.Jobs
{
	public class WF_OverTimeCheck : IJob
	{
		private static StringCollection scTask;

		private Logger fileLogger = LogManager.GetCurrentClassLogger();

		static WF_OverTimeCheck()
		{
			WF_OverTimeCheck.scTask = new StringCollection();
		}

		public WF_OverTimeCheck()
		{
		}

		public void Execute(IJobExecutionContext context)
		{
			DataTable overTimeTasks = TaskService.GetOverTimeTasks();
			this.fileLogger.Trace("进入WF_OverTimeCheck ,取回{0}条数据", overTimeTasks.Rows.Count);
			foreach (DataRow row in overTimeTasks.Rows)
			{
				string str = row["_AutoId"].ToString();
				if (!WF_OverTimeCheck.scTask.Contains(str))
				{
					try
					{
						this.ScheduleTask(row);
						WF_OverTimeCheck.scTask.Add(str);
					}
					catch (Exception exception)
					{
						this.fileLogger.Error<Exception>("执行ScheduleTask发生错误", exception);
					}
				}
			}
		}

		public static void RemoveTask(string taskId)
		{
			if (WF_OverTimeCheck.scTask.Contains(taskId))
			{
				WF_OverTimeCheck.scTask.Remove(taskId);
			}
		}

		private void ScheduleTask(DataRow data)
		{
			DateTime value;
			IJobDetail jobDetail;
			ISimpleTrigger simpleTrigger;
			bool flag;
			DateTimeOffset? nullable;
			string str = data["_AutoId"].ToString();
			Task taskById = TaskService.GetTaskById(str);
			if (taskById != null)
			{
				Logger logger = this.fileLogger;
				object[] overTimeAlert = new object[] { str, taskById.OverTimeAlert, null, null, null, null };
				DateTime? deadline = taskById.Deadline;
				overTimeAlert[2] = deadline.Value;
				overTimeAlert[3] = DateTime.Now;
				deadline = taskById.Deadline;
				DateTime dateTime = deadline.Value;
				overTimeAlert[4] = dateTime.CompareTo(DateTime.Now);
				overTimeAlert[5] = taskById.OverTimeAction;
				logger.Trace("taskId={0}，OverTimeAlert={1}，Deadline({2})-Now({3}) ={4},OverTimeAction={5}", overTimeAlert);
				if (taskById.OverTimeAlert != 1)
				{
					flag = true;
				}
				else
				{
					deadline = taskById.Deadline;
					dateTime = deadline.Value;
					flag = dateTime.CompareTo(DateTime.Now) <= 0;
				}
				if (!flag)
				{
					deadline = taskById.OverTimeAlertFirst;
					if (deadline.HasValue)
					{
						deadline = taskById.OverTimeAlertFirst;
						value = deadline.Value;
						DateTime? deadline1 = taskById.Deadline;
						jobDetail = JobBuilder.Create<WF_OverTimeAlert>().WithIdentity(string.Concat("wf_overtime_alert_", str), "wf_overtime").Build();
						jobDetail.JobDataMap.Put("TaskId", str);
						if (taskById.OverTimeAlertRepeat != 1)
						{
							this.fileLogger.Trace("taskId={0}，超时提醒，通知一次", str);
							simpleTrigger = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity(string.Concat("wf_overtime_alert_tr_", str), "wf_overtime").StartAt(value).Build();
							Scheduler.sched.ScheduleJob(jobDetail, simpleTrigger);
						}
						else
						{
							this.fileLogger.Trace("taskId={0}，超时提醒，OverTimeAlertRepeat = 1", str);
							int overTimeAlertInterval = taskById.OverTimeAlertInterval;
							TimeSpan timeSpan = new TimeSpan(0, overTimeAlertInterval, 0);
							TriggerBuilder triggerBuilder = TriggerBuilder.Create().WithIdentity(string.Concat("wf_overtime_alert_tr_", str), "wf_overtime").StartAt(value);
							deadline = deadline1;
							if (deadline.HasValue)
							{
								nullable = new DateTimeOffset?(deadline.GetValueOrDefault());
							}
							else
							{
								nullable = null;
							}
							simpleTrigger = (ISimpleTrigger)SimpleScheduleTriggerBuilderExtensions.WithSimpleSchedule(triggerBuilder.EndAt(nullable), (SimpleScheduleBuilder x) => x.WithIntervalInMinutes(overTimeAlertInterval).RepeatForever()).Build();
							DateTimeOffset? nullable1 = new DateTimeOffset?(Scheduler.sched.ScheduleJob(jobDetail, simpleTrigger));
						}
					}
				}
				if (taskById.OverTimeAction != "1")
				{
					this.fileLogger.Trace("taskId={0}，超时采取措施", str);
					deadline = taskById.Deadline;
					value = deadline.Value;
					jobDetail = JobBuilder.Create<WF_OverTimeAction>().WithIdentity(string.Concat("wf_overtime_action_", str), "wf_overtime").Build();
					jobDetail.JobDataMap.Put("TaskId", str);
					if (DateTime.Now.CompareTo(value) > 0)
					{
						value = DateTime.Now;
					}
					simpleTrigger = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity(string.Concat("wf_overtime_action_tr_", str), "wf_overtime").StartAt(value).Build();
					Scheduler.sched.ScheduleJob(jobDetail, simpleTrigger);
				}
			}
		}
	}
}