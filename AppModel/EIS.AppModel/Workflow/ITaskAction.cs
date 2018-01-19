using EIS.AppBase;
using EIS.DataModel.Model;
using EIS.Permission.Model;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.XPDLParser.Elements;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net.Mail;

namespace EIS.AppModel.Workflow
{
	public interface ITaskAction
	{
		void Task_Arrive(Instance instance_0, Activity curAct, Task curTask, DataRow dataRow, UserContext user, List<DeptEmployee> list, IDbTransaction dbTran);

		void Task_Finish(Instance instance_0, DataRow dataRow, UserContext user, IDbTransaction dbTran);

		void Task_Rollback(Instance instance_0, Activity curAct, Task curTask, DataRow dataRow, UserContext user, IDbTransaction dbTran);

		void Task_Stop(Instance instance_0, DataRow dataRow, UserContext user, IDbTransaction dbTran);

        void Task_Submit(Instance instance_0, Activity curAct, Task curTask, DataRow dataRow, UserContext user, IDbTransaction dbTran);

        void SendMail(MailMessage mail);

        void SendSms(AppSms appSm, DbTransaction dbTran);
    }
}