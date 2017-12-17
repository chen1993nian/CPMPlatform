using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Model;
using EIS.Permission.Service;
using EIS.WorkFlow.Access;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using EIS.WorkFlow.XPDLParser.Elements;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.SysFolder.WorkFlow
{
    public partial class FlowTaskRollBack : PageBase
    {
        public StringBuilder sbHtml = new StringBuilder();

        public string empNameList = "";

        public string tipClass = "hidden";

        private string string_0 = "";

        public Task curTask = new Task();

        private Instance instance_0 = null;



        protected void Button1_Click(object sender, EventArgs e)
        {
            string text = this.txtReason.Text;
            if (string.IsNullOrEmpty(base.Request["selActId"]))
            {
                this.Session["_syserror"] = "没有选择退回步骤";
                base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?taskId=", this.string_0), false);
            }
            this.method_0(base.Request["selActId"], text);
        }

        private void method_0(string targetId, string reason)
        {
            UserContext userInfo = base.UserInfo;
            DriverEngine driverEngine = new DriverEngine(userInfo);
            DbConnection dbConnection = SysDatabase.CreateConnection();
            try
            {
                dbConnection.Open();
                DbTransaction dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        UserTask userTaskByTaskId = EIS.WorkFlow.Engine.Utility.GetUserTaskByTaskId(this.string_0, base.EmployeeID, dbTransaction);
                        if (userTaskByTaskId == null)
                        {
                            throw new Exception("找不到对应的用户任务");
                        }
                        if ((this.curTask.DefineType != ActivityType.Normal.ToString() ? false : userTaskByTaskId.TaskState == 0.ToString()))
                        {
                            TaskService.ApplyShareTask(this.string_0, userTaskByTaskId.UserTaskId, dbTransaction);
                            this.curTask.MainPerformer = userTaskByTaskId.OwnerId;
                        }
                        _UserTask __UserTask = new _UserTask(dbTransaction);
                        userTaskByTaskId._UpdateTime = DateTime.Now;
                        userTaskByTaskId.DealTime = new DateTime?(DateTime.Now);
                        userTaskByTaskId.DealAdvice = reason;
                        userTaskByTaskId.DealAction = EnumDescription.GetFieldText(DealAction.RollBack);
                        userTaskByTaskId.TaskState = 2.ToString();
                        userTaskByTaskId.DealUser = base.EmployeeID;
                        if (userTaskByTaskId.AgentId == base.EmployeeID)
                        {
                            userTaskByTaskId.EmployeeName = string.Concat(userTaskByTaskId.EmployeeName, "（", base.EmployeeName, "代签）");
                        }
                        if (userTaskByTaskId.PositionId == "")
                        {
                            userTaskByTaskId.PositionId = userInfo.PositionId;
                            userTaskByTaskId.PositionName = userInfo.PositionName;
                        }
                        if (userTaskByTaskId.DeptId == "")
                        {
                            userTaskByTaskId.DeptId = userInfo.DeptId;
                            userTaskByTaskId.DeptName = userInfo.DeptName;
                        }
                        __UserTask.UpdateAdvice(userTaskByTaskId);
                        EIS.WorkFlow.Engine.Utility.DeleteUnDoneUserTaskByTaskId(this.curTask.TaskId, dbTransaction);
                        List<Task> tasks = driverEngine.RollBackTask(this.string_0, targetId, dbTransaction);
                        if (this.instance_0.NeedUpdate)
                        {
                            (new _Instance(dbTransaction)).UpdateXPDL(this.instance_0);
                        }
                        _Task __Task = new _Task(dbTransaction);
                        this.curTask.TaskState = 2.ToString();
                        this.curTask._UpdateTime = DateTime.Now;
                        __Task.Update(this.curTask);
                        if (tasks.Count > 0)
                        {
                            StringCollection stringCollections = new StringCollection();
                            foreach (Task task in tasks)
                            {
                                TaskService.GetTaskDealUser(task.TaskId, stringCollections, dbTransaction);
                            }
                            userTaskByTaskId.RecIds = EIS.AppBase.Utility.GetJoinString(stringCollections);
                            userTaskByTaskId.RecNames = EmployeeService.GetEmployeeNameList(stringCollections);
                            __UserTask.UpdateAdvice(userTaskByTaskId);
                        }
                        dbTransaction.Commit();
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        dbTransaction.Rollback();
                        this.Session["_syserror"] = exception.Message;
                        base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?taskId=", this.string_0), false);
                    }
                }
                finally
                {
                    dbConnection.Close();
                }
                base.Server.Transfer(string.Concat("DealFlowAfter.aspx?taskId=", this.string_0), false);
            }
            finally
            {
                if (dbConnection != null)
                {
                    ((IDisposable)dbConnection).Dispose();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.string_0 = base.GetParaValue("taskId");
            UserContext userInfo = base.UserInfo;
            DriverEngine driverEngine = new DriverEngine(userInfo);
            this.curTask = driverEngine.GetTaskById(this.string_0);
            this.instance_0 = driverEngine.GetInstanceById(this.curTask.InstanceId);
            Activity activityById = driverEngine.GetActivityById(this.instance_0, this.curTask.ActivityId);
            if (this.curTask.TaskState == "2")
            {
                this.Session["_syserror"] = "已经处理过的任务不能退回";
                base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?taskId=", this.string_0), false);
            }
            if (!base.IsPostBack)
            {
                if (!string.IsNullOrEmpty(base.Request["reason"]))
                {
                    this.txtReason.Text = base.Request["reason"];
                }
                string rollBackScope = activityById.GetRollBackScope();
                char[] chrArray = new char[] { '|' };
                string[] strArrays = rollBackScope.Split(chrArray);
                StringCollection stringCollections = new StringCollection();
                if (((int)strArrays.Length <= 1 ? false : rollBackScope.Length > 2))
                {
                    string str = strArrays[1];
                    chrArray = new char[] { ',' };
                    stringCollections.AddRange(str.Split(chrArray));
                    if (stringCollections.Count == 1)
                    {
                        this.method_0(stringCollections[0], this.txtReason.Text);
                    }
                }
                List<Activity> allActivityPre = driverEngine.GetAllActivityPre(this.instance_0, activityById);
                int num = 1;
                foreach (Activity activity in allActivityPre)
                {
                    string id = activity.GetId();
                    if (activity.GetNodeType() == ActivityType.Auto)
                    {
                        continue;
                    }
                    if ((stringCollections.Count <= 0 ? false : !stringCollections.Contains(id)))
                    {
                        continue;
                    }
                    DataRow appData = EIS.WorkFlow.Engine.Utility.GetAppData(this.instance_0);
                    List<DeptEmployee> activityUser = EIS.WorkFlow.Engine.Utility.GetActivityUser(this.instance_0, activity, new WFSession(userInfo, this.instance_0, appData, activityById));
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (DeptEmployee deptEmployee in activityUser)
                    {
                        stringBuilder.AppendFormat("{0}，", deptEmployee.EmployeeName);
                    }
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Length = stringBuilder.Length - 1;
                    }
                    StringBuilder stringBuilder1 = this.sbHtml;
                    object[] name = new object[] { num, id, activity.GetName(), stringBuilder };
                    stringBuilder1.AppendFormat("\r\n                        <tr>\r\n                        <td>{0}</td>\r\n                        <td><input type=radio value='{1}' name='selActId' id='act{1}' />\r\n                        <label for='act{1}'>{2}</label></td>\r\n                        <td>{3}</td></tr>", name);
                    num++;
                }
            }
            List<UserTask> toDoUserTask = TaskService.GetToDoUserTask(this.instance_0.InstanceId, this.curTask);
            if (toDoUserTask.Count > 1)
            {
                StringCollection stringCollections1 = new StringCollection();
                foreach (UserTask userTask in toDoUserTask)
                {
                    if ((userTask.OwnerId == base.EmployeeID ? true : userTask.AgentId == base.EmployeeID))
                    {
                        continue;
                    }
                    stringCollections1.Add(userTask.OwnerId);
                }
                this.empNameList = EmployeeService.GetEmployeeNameList(stringCollections1);
                this.tipClass = "tip";
            }
        }
    }
}