using EIS.AppBase;
using EIS.AppBase.Config;
using NLog;
using Quartz;
using Quartz.Impl;
using System;
using System.Runtime.CompilerServices;
using EIS.AppEngine.Jobs;
namespace EIS.AppEngine
{
	public class Scheduler
	{
		public static IScheduler sched;

		private static Logger fileLogger;

		static Scheduler()
		{
			Scheduler.sched = null;
			Scheduler.fileLogger = LogManager.GetCurrentClassLogger();
		}

		public Scheduler()
		{
		}

		public static void Pasuse()
		{
			Scheduler.sched.PauseAll();
		}

		public static void Resume()
		{
			Scheduler.sched.ResumeAll();
		}

		public static void ShutDown()
		{
			Scheduler.fileLogger.Info("关掉调度器");
			Scheduler.sched.Shutdown(true);
		}

		public static void Start()
		{
			try
			{
				if (!AppSettings.Instance.Scheduler_Enable)
				{
					Scheduler.fileLogger.Info("调度器没有启动，Scheduler_Enable=0");
				}
				else
				{
					Scheduler.fileLogger.Info("开始启动调度器");
					Scheduler.sched = (new StdSchedulerFactory()).GetScheduler();
					Scheduler.startOverTimeCheck();
					Scheduler.sched.Start();
					Scheduler.fileLogger.Info("启动调度器成功,IsStarted：{0}", Scheduler.sched.IsStarted);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				Scheduler.fileLogger.Error("启动调度器时出现错误");
				Scheduler.fileLogger.Error<Exception>(exception);
			}
		}

		private static void startOverTimeCheck()
		{
			string itemValue = SysConfig.GetConfig("WF_OverTimeCheck").ItemValue;
			string str = SysConfig.GetConfig("WF_OverTimeInterval").ItemValue;
			if (!(itemValue == "是"))
			{
				Scheduler.fileLogger.Info<string, string>("没有启动定时检查任务，配置信息：WF_OverTimeCheck={0}，WF_OverTimeInterval={1}", itemValue, str);
			}
			else
			{
				int num = 1;
				int.TryParse(str, out num);
				IJobDetail jobDetail = JobBuilder.Create<WF_OverTimeCheck>().WithIdentity("WF_OverTimeCheck", "wf_overtime").Build();
				DateTimeOffset dateTimeOffset = DateBuilder.EvenMinuteDate(new DateTimeOffset?(DateTimeOffset.UtcNow));
				ISimpleTrigger simpleTrigger = (ISimpleTrigger)SimpleScheduleTriggerBuilderExtensions.WithSimpleSchedule(TriggerBuilder.Create().WithIdentity("WF_OverTimeCheck_Trigger", "wf_overtime").StartAt(dateTimeOffset), (SimpleScheduleBuilder x) => x.WithIntervalInMinutes(num).RepeatForever()).Build();
				DateTimeOffset? nullable = new DateTimeOffset?(Scheduler.sched.ScheduleJob(jobDetail, simpleTrigger));
				Scheduler.fileLogger.Info<string, string>("已经启动定时检查任务，配置信息：WF_OverTimeCheck={0}，WF_OverTimeInterval={1}", itemValue, str);
				Scheduler.fileLogger.Info<DateTimeOffset>("第一次任务检查开始于：StartAt={0}", nullable.Value.ToLocalTime());
			}
		}
	}
}