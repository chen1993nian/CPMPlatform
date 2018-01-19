using EIS.AppBase;
using EIS.DataModel.Model;
using EIS.Permission.Model;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.XPDLParser.Elements;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace EIS.Web.SysFolder.WorkFlow
{
    public class WorkflowEvent : EIS.AppModel.Workflow.ITaskAction
    {
        private static Logger fileLogger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 任务到达事件
        /// </summary>
        /// <param name="ins">流程实例</param>
        /// <param name="curAct">目标节点</param>
        /// <param name="curTask">当前任务</param>
        /// <param name="dataRow">业务数据行</param>
        /// <param name="user">操作人信息</param>
        /// <param name="list">处理人信息</param>
        /// <param name="dbTran">事务对象</param>
        public void Task_Arrive(Instance ins, Activity toAct, Task curTask, DataRow dataRow, UserContext user, List<DeptEmployee> list, IDbTransaction dbTran)
        {

        }


        /// <summary>
        /// 任务提交事件
        /// </summary>
        /// <param name="ins">流程实例</param>
        /// <param name="curAct">目标节点</param>
        /// <param name="curTask">当前任务</param>
        /// <param name="dataRow">业务数据行</param>
        /// <param name="user">操作人信息</param>
        /// <param name="dbTran">事务对象</param>
        public void Task_Submit(Instance ins, Activity curAct, Task curTask, DataRow dataRow, UserContext user, IDbTransaction dbTran)
        {

        }

        /// <summary>
        /// 任务回退事件
        /// </summary>
        /// <param name="ins">流程实例</param>
        /// <param name="curAct">目标节点</param>
        /// <param name="curTask">当前任务</param>
        /// <param name="dataRow">业务数据行</param>
        /// <param name="user">操作人信息</param>
        /// <param name="dbTran">事务对象</param>
        public void Task_Rollback(Instance ins, Activity curAct, Task curTask, DataRow dataRow, UserContext user, IDbTransaction dbTran)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance_0"></param>
        /// <param name="dataRow"></param>
        /// <param name="user"></param>
        /// <param name="dbTran"></param>
        public void Task_Finish(Instance instance_0, DataRow dataRow, UserContext user, IDbTransaction dbTran)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance_0"></param>
        /// <param name="dataRow"></param>
        /// <param name="user"></param>
        /// <param name="dbTran"></param>
        public void Task_Stop(Instance instance_0, DataRow dataRow, UserContext user, IDbTransaction dbTran)
        {

        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mail"></param>
        public void SendMail(MailMessage mail)
        {

        }

        /// <summary>
        /// 发短信
        /// </summary>
        /// <param name="appSm"></param>
        /// <param name="dbTran"></param>
        public void SendSms(AppSms appSm, DbTransaction dbTran)
        {

        }




    }
}