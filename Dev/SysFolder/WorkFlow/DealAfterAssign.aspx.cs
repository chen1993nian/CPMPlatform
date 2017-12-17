using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.AppModel.Service;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.Permission.Service;

using EIS.WorkFlow.Access;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using EIS.WorkFlow.XPDLParser.Elements;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebBase.JZY.Tools;
namespace EIS.WebBase.SysFolder.WorkFlow
{
    public partial class DealAfterAssign : PageBase
    {
       

        public StringBuilder DealInfo = new StringBuilder();

        public string uTaskId = "";

        public string perror = "";

        public string empNameList = "";

        public Task curTask = new Task();

        public UserTask uTask = new UserTask();

        public Instance curInstance = new Instance();

        private Activity activity_0 = null;

        public string Advice
        {
            get
            {
                return (this.ViewState["Advice"] == null ? "" : this.ViewState["Advice"].ToString());
            }
            set
            {
                this.ViewState["Advice"] = value;
            }
        }

        public string RelationId
        {
            get
            {
                return this.ViewState["RelationId"].ToString();
            }
            set
            {
                this.ViewState["RelationId"] = value;
            }
        }

        public DealAfterAssign()
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.Advice = base.Request["txtRemark"];
            if (string.IsNullOrEmpty(this.Advice))
            {
                this.Advice = base.Request["Advice"];
            }
            string advice = "";
            bool itemValue = SysConfig.GetConfig("WF_EnforceAgree").ItemValue == "是";
            if (this.activity_0.IsDecisionNode())
            {
                if (itemValue)
                {
                    if (this.Advice != "同意")
                    {
                        advice = this.Advice;
                    }
                    this.Advice = "同意";
                }
                else if (this.Advice == "")
                {
                    this.Advice = "同意";
                }
            }
            this.method_0(DealAction.Submit, this.Advice, advice);
        }

        private void method_0(DealAction dealAction_0, string string_0, string string_1)
        {
            base.Server.ClearError();
           
            UserContext userInfo =WebTools.GetUserInfo(this.RelationId);
            DriverEngine driverEngine = new DriverEngine(userInfo);
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                try
                {
                    this.method_2(this.curInstance.InstanceId, dbTransaction);
                    _UserTask __UserTask = new _UserTask(dbTransaction);
                    this.uTask._UpdateTime = DateTime.Now;
                    this.uTask.DealTime = new DateTime?(DateTime.Now);
                    this.uTask.DealAdvice = string_0;
                    this.uTask.Memo = string_1;
                    string str = "提交";
                    if (driverEngine.GetActivityById(this.curInstance, this.curTask.ActivityId).IsDecisionNode())
                    {
                        str = "同意";
                    }
                    this.uTask.DealAction = str;
                    this.uTask.TaskState = 2.ToString();
                    this.uTask.DealUser = base.EmployeeID;
                    if (this.uTask.AgentId == base.EmployeeID)
                    {
                        this.uTask.EmployeeName = string.Concat(this.uTask.EmployeeName, "〔", base.EmployeeName, "代签〕");
                    }
                    this.uTask.PositionId = userInfo.PositionId;
                    this.uTask.PositionName = userInfo.PositionName;
                    this.uTask.DeptId = userInfo.DeptId;
                    this.uTask.DeptName = userInfo.DeptName;
                    __UserTask.UpdateAdvice(this.uTask);
                    if ((this.curTask.DefineType == ActivityType.Sign.ToString() ? true : this.curTask.MainPerformer == this.uTask.OwnerId))
                    {
                        List<Task> tasks = driverEngine.NextTask(this.curInstance, this.curTask, dbTransaction);
                        if (this.curInstance.NeedUpdate)
                        {
                            (new _Instance(dbTransaction)).UpdateXPDL(this.curInstance);
                        }
                        _Task __Task = new _Task(dbTransaction);
                        this.curTask._UpdateTime = DateTime.Now;
                        __Task.Update(this.curTask);
                        if (tasks.Count > 0)
                        {
                            StringCollection stringCollections = new StringCollection();
                            foreach (Task task in tasks)
                            {
                                TaskService.GetTaskDealUser(task.TaskId, stringCollections, dbTransaction);
                            }
                            this.uTask.RecIds = EIS.AppBase.Utility.GetJoinString(stringCollections);
                            this.uTask.RecNames = EmployeeService.GetEmployeeNameList(stringCollections);
                            __UserTask.UpdateAdvice(this.uTask);
                        }
                        if ((this.curTask.DefineType != ActivityType.Normal.ToString() ? false : this.curTask.MainPerformer == this.uTask.OwnerId))
                        {
                            EIS.WorkFlow.Engine.Utility.DeleteUnDoneUserTaskByTaskId(this.curTask.TaskId, dbTransaction);
                        }
                    }
                    if (this.uTask.IsAssign == "1")
                    {
                        AppMsg appMsg = new AppMsg(base.UserInfo)
                        {
                            Title = string.Concat("加签任务完成提醒，来自", base.EmployeeName),
                            MsgType = "",
                            MsgUrl = string.Concat("SysFolder/Workflow/DealFlow.aspx?sysTaskId=", this.curTask.TaskId),
                            RecIds = this.uTask._UserName,
                            RecNames = EmployeeService.GetEmployeeName(this.uTask._UserName),
                            SendTime = new DateTime?(DateTime.Now),
                            Sender = base.EmployeeName,
                            Content = string.Format("{0}已经在加签任务【{1}】中发表意见。\r\n以下是他（她）的意见：{2}", base.EmployeeName, this.curInstance.InstanceName, this.Advice)
                        };
                        AppMsgService.SendMessage(appMsg);
                    }
                    this.method_1(this.curInstance, dbTransaction);
                    dbTransaction.Commit();
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    dbTransaction.Rollback();
                    base.WriteExceptionLog("错误", this.FormatException(exception, ""));
                    this.Session["_syserror"] = exception.Message;
                    base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?taskId=", this.curTask.TaskId), true);
                }
            }
            finally
            {
                dbConnection.Close();
            }
            base.Server.Transfer(string.Concat("DealFlowAfter.aspx?taskId=", this.curTask.TaskId), false);
        }

        private void method_1(Instance instance_0, DbTransaction dbTransaction_0)
        {
            if ((new _TableInfo(instance_0.AppName)).GetModel().TableType == 1)
            {
                string str = string.Format("Update {0} set _WFState='{2}' where _AutoId='{1}'", instance_0.AppName, instance_0.AppId, instance_0.InstanceState);
                SysDatabase.ExecuteNonQuery(str, dbTransaction_0);
            }
        }

        private void method_2(string string_0, DbTransaction dbTransaction_0)
        {
            if (!string.IsNullOrEmpty(base.Request["wfRefer"]))
            {
                string item = base.Request["wfRefer"];
                char[] chrArray = new char[] { ',' };
                string[] strArrays = item.Split(chrArray);
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    string str = strArrays[i];
                    chrArray = new char[] { '|' };
                    string[] strArrays1 = str.Split(chrArray);
                    InstanceRefer instanceRefer = new InstanceRefer(base.UserInfo)
                    {
                        InstanceId = string_0,
                        ReferId = strArrays1[0],
                        ReferName = string.Concat(strArrays1[1], "（", strArrays1[2], "）"),
                        OrderID = i
                    };
                    (new _InstanceRefer(dbTransaction_0)).Add(instanceRefer);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.uTaskId = base.GetParaValue("uTaskId");
            DriverEngine driverEngine = new DriverEngine(base.UserInfo);
            this.uTask = UserTaskService.GetUserTaskById(this.uTaskId, null);
            this.curTask = driverEngine.GetTaskById(this.uTask.TaskId);
            this.curInstance = driverEngine.GetInstanceById(this.curTask.InstanceId);
            this.activity_0 = driverEngine.GetActivityById(this.curInstance, this.curTask.ActivityId);
            if (!string.IsNullOrEmpty(base.GetParaValue("info")))
            {
                if (this.Session["_sysinfo"] != null)
                {
                    this.DealInfo.Append(this.Session["_sysinfo"].ToString());
                }
            }
            else if (!base.IsPostBack)
            {
                List<UserTask> toDoUserTask = TaskService.GetToDoUserTask(this.curInstance.InstanceId, this.curTask);
                if (toDoUserTask.Count > 0)
                {
                    StringCollection stringCollections = new StringCollection();
                    foreach (UserTask userTask in toDoUserTask)
                    {
                        if ((userTask.OwnerId == base.EmployeeID ? true : userTask.AgentId == base.EmployeeID))
                        {
                            continue;
                        }
                        stringCollections.Add(userTask.OwnerId);
                    }
                    this.empNameList = EmployeeService.GetEmployeeNameList(stringCollections);
                }
                this.RelationId = base.Request["PositionList"];
            }
        }
    }
}