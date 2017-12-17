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
    public partial class FlowTaskDirect : PageBase
    {
        public StringBuilder sbHtml = new StringBuilder();

        public string empNameList = "";

        public string tipClass = "hidden";

        private string string_0 = "";

        public Task curTask = new Task();

     

        protected void Button1_Click(object sender, EventArgs e)
        {
            string text = this.txtReason.Text;
            if (string.IsNullOrEmpty(base.Request["selActId"]))
            {
                this.Session["_syserror"] = "没有选择直送步骤";
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
                        _UserTask __UserTask = new _UserTask(dbTransaction);
                        userTaskByTaskId._UpdateTime = DateTime.Now;
                        userTaskByTaskId.DealTime = new DateTime?(DateTime.Now);
                        userTaskByTaskId.DealAdvice = reason;
                        userTaskByTaskId.DealAction = EnumDescription.GetFieldText(DealAction.DirectTo);
                        userTaskByTaskId.TaskState = 2.ToString();
                        userTaskByTaskId.DealUser = base.EmployeeID;
                        if (userTaskByTaskId.AgentId == base.EmployeeID)
                        {
                            userTaskByTaskId.EmployeeName = string.Concat(userTaskByTaskId.EmployeeName, "（", base.EmployeeName, "代签）");
                        }
                        userTaskByTaskId.PositionId = userInfo.PositionId;
                        userTaskByTaskId.PositionName = userInfo.PositionName;
                        userTaskByTaskId.DeptId = userInfo.DeptId;
                        userTaskByTaskId.DeptName = userInfo.DeptName;
                        __UserTask.UpdateAdvice(userTaskByTaskId);
                        EIS.WorkFlow.Engine.Utility.DeleteUnDoneUserTaskByTaskId(this.curTask.TaskId, dbTransaction);
                        List<Task> tasks = driverEngine.DirectSendTask(this.string_0, targetId, dbTransaction);
                        if (tasks.Count > 0)
                        {
                            _Task __Task = new _Task(dbTransaction);
                            this.curTask.TaskState = 2.ToString();
                            this.curTask._UpdateTime = DateTime.Now;
                            __Task.Update(this.curTask);
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
            Instance instanceById = driverEngine.GetInstanceById(this.curTask.InstanceId);
            Activity activityById = driverEngine.GetActivityById(instanceById, this.curTask.ActivityId);
            if (!base.IsPostBack)
            {
                if (!string.IsNullOrEmpty(base.Request["reason"]))
                {
                    this.txtReason.Text = base.Request["reason"];
                }
                string directScope = activityById.GetDirectScope();
                char[] chrArray = new char[] { '|' };
                string[] strArrays = directScope.Split(chrArray);
                StringCollection stringCollections = new StringCollection();
                if (((int)strArrays.Length <= 1 ? false : directScope.Length > 2))
                {
                    string str = strArrays[1];
                    chrArray = new char[] { ',' };
                    stringCollections.AddRange(str.Split(chrArray));
                    if (stringCollections.Count == 1)
                    {
                        this.method_0(stringCollections[0], this.txtReason.Text);
                    }
                }
                List<Activity> allActivitySith = driverEngine.GetAllActivitySith(instanceById, activityById);
                int num = 1;
                foreach (Activity activity in allActivitySith)
                {
                    string id = activity.GetId();
                    if ((activity.GetNodeType() == ActivityType.Auto ? true : activity.GetNodeType() == ActivityType.End))
                    {
                        continue;
                    }
                    if ((stringCollections.Count <= 0 ? false : !stringCollections.Contains(id)))
                    {
                        continue;
                    }
                    DataRow appData = EIS.WorkFlow.Engine.Utility.GetAppData(instanceById);
                    List<DeptEmployee> activityUser = EIS.WorkFlow.Engine.Utility.GetActivityUser(instanceById, activity, new WFSession(userInfo, instanceById, appData, activityById));
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
                    object[] objArray = new object[] { num, activity.GetId(), activity.GetName(), stringBuilder };
                    stringBuilder1.AppendFormat("\r\n                        <tr>\r\n                        <td style='text-align:center;'>{0}</td>\r\n                        <td><input type=radio value='{1}' name='selActId' id='act{1}' />\r\n                        <label for='act{1}'>{2}</label></td>\r\n                        <td>{3}</td></tr>", objArray);
                    num++;
                }
            }
            List<UserTask> toDoUserTask = TaskService.GetToDoUserTask(instanceById.InstanceId, this.curTask);
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