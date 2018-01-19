using NLog;
using Quartz;
using System;

namespace EIS.AppEngine
{
	public class OverTimeCheckListener : IJobListener
	{
		private static Logger fileLogger;

		public virtual string Name
		{
			get
			{
				return "OverTimeCheckListener";
			}
		}

		static OverTimeCheckListener()
		{
			OverTimeCheckListener.fileLogger = LogManager.GetCurrentClassLogger();
		}

		public OverTimeCheckListener()
		{
		}

		public virtual void JobExecutionVetoed(IJobExecutionContext inContext)
		{
			OverTimeCheckListener.fileLogger.Info("OverTimeCheckListener：WF_OverTimeCheck 被否决");
		}

		public virtual void JobToBeExecuted(IJobExecutionContext inContext)
		{
			OverTimeCheckListener.fileLogger.Info("OverTimeCheckListener：WF_OverTimeCheck 即将执行.");
		}

		public virtual void JobWasExecuted(IJobExecutionContext inContext, JobExecutionException inException)
		{
			OverTimeCheckListener.fileLogger.Info("OverTimeCheckListener：WF_OverTimeCheck 已经执行");
		}
	}
}