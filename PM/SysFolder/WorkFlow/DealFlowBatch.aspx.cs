using EIS.AppBase;
using EIS.AppModel.Service;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.Permission.Model;
using EIS.Permission.Service;
using WebBase.JZY.Tools;
using EIS.WorkFlow.Access;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using EIS.WorkFlow.XPDLParser.Elements;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.SysFolder.WorkFlow
{
    public partial class DealFlowBatch : PageBase
    {
        public StringBuilder sbToDo = new StringBuilder();

        private string string_0 = "";

        private string string_1 = "";

        private StringDictionary stringDictionary_0 = new StringDictionary();

      

        public string SubmitTaskIdList
        {
            get
            {
                string str;
                str = (this.ViewState["TaskIdList"] != null ? this.ViewState["TaskIdList"].ToString() : "");
                return str;
            }
            set
            {
                this.ViewState["TaskIdList"] = value;
            }
        }

        public DealFlowBatch()
        {
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string item = base.Request["taskchk"];
            if (!string.IsNullOrEmpty(item))
            {
                string[] strArrays = item.Split(new char[] { ',' });
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    this.method_5(strArrays[i], this.txtNewTitle.Text);
                }
            }
            this.method_0(this.string_0);
            base.ClientScript.RegisterStartupScript(base.GetType(), "", "afterSubmit();", true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string item = base.Request["taskchk"];
            if (!string.IsNullOrEmpty(item))
            {
                string[] strArrays = item.Split(new char[] { ',' });
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    string str = strArrays[i];
                    this.method_4(DealAction.Submit, str, this.txtNewTitle.Text);
                }
            }
            this.method_0(this.string_0);
            base.ClientScript.RegisterStartupScript(base.GetType(), "", "afterSubmit();", true);
        }

        private string method_0(string string_2)
        {
            DataTable toDoGroupByEmployeeId = UserTaskService.GetToDoGroupByEmployeeId(base.EmployeeID);
            DataTable toDoUserTaskByEmployeeId = UserTaskService.GetToDoUserTaskByEmployeeId(base.EmployeeID);
            foreach (DataRow row in toDoGroupByEmployeeId.Rows)
            {
                string str = row["WorkflowCode"].ToString();
                string str1 = row["WorkflowName"].ToString();
                string str2 = row["AppName"].ToString();
                if (this.string_1 != "")
                {
                    if (this.string_1 != str2)
                    {
                        continue;
                    }
                }
                else if (string_2 != "" && string_2 != str)
                {
                    continue;
                }
                DataRow[] dataRowArray = toDoUserTaskByEmployeeId.Select(string.Concat("WorkflowCode='", str, "' and IsNull(CanBatch,'')='1'"));
                if ((int)dataRowArray.Length == 0)
                {
                    continue;
                }
                this.sbToDo.AppendFormat("<div class='groupZone'><div class='groupHeader'><input type='checkbox' class='chklist' value='{0}' id='chkall_{0}'><label for='chkall_{0}' title='点击全选'>&nbsp;&nbsp;[{0}]&nbsp;-&nbsp;{1}&nbsp;</label></div>", str, str1);
                bool flag = false;
                DataRow[] dataRowArray1 = dataRowArray;
                for (int i = 0; i < (int)dataRowArray1.Length; i++)
                {
                    DataRow dataRow = dataRowArray1[i];
                    string str3 = dataRow["uTaskId"].ToString();
                    if ((this.SubmitTaskIdList.Length <= 0 ? true : this.SubmitTaskIdList.IndexOf(str3) != -1))
                    {
                        dataRow["OwnerId"].ToString();
                        string str4 = dataRow["AgentId"].ToString();
                        string str5 = dataRow["IsAssign"].ToString();
                        string str6 = dataRow["IsShare"].ToString();
                        string str7 = dataRow["DefineType"].ToString();
                        string str8 = "";
                        if (str7 != "Sign")
                        {
                            str8 = (str6 == "1" ? "共享" : "普通");
                        }
                        else
                        {
                            str8 = "会签";
                        }
                        string str9 = this.method_3(dataRow);
                        StringBuilder stringBuilder = this.sbToDo;
                        object[] item = new object[] { dataRow["uTaskId"], dataRow["instanceName"], dataRow["CreateUser"], dataRow["ArriveTime"], null, null, null, null, null, null, null, null, null };
                        item[4] = (dataRow["IsRead"].ToString() == "0" ? "unread" : "readed");
                        item[5] = (str4 == base.EmployeeID ? "[代]&nbsp;" : "");
                        item[6] = (str5 == "1" ? "[加]&nbsp;" : "");
                        item[7] = (flag ? "alt" : "");
                        item[8] = str8;
                        item[9] = (str9.Length > 8 ? string.Concat(str9.Substring(0, 8), "…") : str9);
                        item[10] = dataRow["taskName"];
                        item[11] = str;
                        item[12] = str9;
                        stringBuilder.AppendFormat("<div class='task {7}'>\r\n                        <table class='taskTbl' width='100%'>\r\n                            <tr>\r\n                                <td width='24'><input type='checkbox' class='taskchk_{11}' value='{0}' name='taskchk' checked='true' /></td>\r\n                                <td width='24'><div class='{4}'></div></td>\r\n                                <td>{5}{6}<a href='dealflow.aspx?taskId={0}' target='_blank'>{1}</a></td>\r\n                                <td width='40'>{8}</td>\r\n                                <td width='60' class='tdActName' title='{10}'>{10}</td>\r\n                                <td width='100' class='tdNextInfo' title='{12}'>{9}</td>\r\n                                <td width='70'><span class='lineuser'>{2}</span></td>\r\n                                <td width='100'><span class='linetime'>{3:yyyy-MM-dd HH:mm}</span></td>\r\n                            </tr>\r\n                        </table>\r\n                        </div>", item);
                        this.sbToDo.Append(this.method_1(str, dataRow["AppId"].ToString()));
                        flag = !flag;
                    }
                }
                this.sbToDo.Append("</div>");
            }
            return this.sbToDo.ToString();
        }

        private string method_1(string wfCode, string appId)
        {
            string str;
            FieldInfo fieldInfo = null;
            StringBuilder stringBuilder = new StringBuilder();
            DataTable dataTable = SysDatabase.ExecuteTable(string.Format("select * from T_E_WF_Config where Enable='是' and WFId='{0}' and IsNull(BatchQuery,'')<>''", wfCode));
            if (dataTable.Rows.Count != 0)
            {
                string str1 = dataTable.Rows[0]["BatchQuery"].ToString();
                List<FieldInfo> modelListDisp = (new _FieldInfo()).GetModelListDisp(str1);
                stringBuilder.Append("<table class='datatbl' width='100%'>");
                stringBuilder.Append("<thead><tr>");
                foreach (FieldInfo fieldInfoA in modelListDisp)
                {
                    if (fieldInfoA.ColumnHidden == 1)
                    {
                        continue;
                    }
                    stringBuilder.AppendFormat("<td align='{2}' width='{1}'>{0}</td>", fieldInfoA.FieldNameCn, fieldInfoA.ColumnWidth, this.method_2(fieldInfoA.ColumnAlign));
                }
                stringBuilder.Append("<td></td>");
                stringBuilder.Append("</tr></thead>");
                string str2 = string.Concat("select ListSQL,ConnectionId,OrderField from T_E_Sys_TableInfo where tablename='", str1, "'");
                DataTable dataTable1 = SysDatabase.ExecuteTable(str2);
                if (dataTable1.Rows.Count > 0)
                {
                    dataTable1.Rows[0]["ConnectionId"].ToString();
                    string str3 = dataTable1.Rows[0]["ListSQL"].ToString();
                    string str4 = string.Concat(" _AutoId = '", appId, "'");
                    str3 = str3.Replace("|^condition^|", string.Concat("(", str4, ")")).Replace("|^sortdir^|", "").Replace("\r\n", " ").Replace("\t", "");
                    DataTable dataTable2 = SysDatabase.ExecuteTable(str3);
                    stringBuilder.Append("<tbody>");
                    foreach (DataRow row in dataTable2.Rows)
                    {
                        stringBuilder.Append("<tr>");
                        foreach (FieldInfo fieldInfo1 in modelListDisp)
                        {
                            if (fieldInfo1.ColumnHidden == 1)
                            {
                                continue;
                            }
                            stringBuilder.AppendFormat("<td align='{1}'>{0}</td>", row[fieldInfo1.FieldName], this.method_2(fieldInfo1.ColumnAlign));
                        }
                        stringBuilder.Append("<td></td>");
                        stringBuilder.Append("</tr>");
                    }
                    stringBuilder.Append("</tbody>");
                }
                stringBuilder.Append("</table>");
                str = stringBuilder.ToString();
            }
            else
            {
                str = "";
            }
            return str;
        }

        private string method_2(string string_2)
        {
            string str;
            if (string_2 == "1")
            {
                str = "left";
            }
            else if (string_2 != "2")
            {
                str = (string_2 != "3" ? "left" : "right");
            }
            else
            {
                str = "center";
            }
            return str;
        }

        private string method_3(DataRow dataRow_0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            UserContext userInfo = base.UserInfo;
            DriverEngine driverEngine = new DriverEngine(userInfo);
            string str = dataRow_0["taskId"].ToString();
            Task taskById = driverEngine.GetTaskById(str);
            Instance instanceById = driverEngine.GetInstanceById(taskById.InstanceId);
            Activity activityById = driverEngine.GetActivityById(instanceById, taskById.ActivityId);
            foreach (Activity activity in driverEngine.NextActivity(instanceById, taskById))
            {
                DataRow appData = EIS.WorkFlow.Engine.Utility.GetAppData(instanceById);
                List<DeptEmployee> activityUser = EIS.WorkFlow.Engine.Utility.GetActivityUser(instanceById, activity, new WFSession(userInfo, instanceById, appData, activityById));
                StringBuilder length = new StringBuilder();
                foreach (DeptEmployee deptEmployee in activityUser)
                {
                    length.AppendFormat("{0},", deptEmployee.EmployeeName);
                }
                if (length.Length > 0)
                {
                    length.Length = length.Length - 1;
                }
                string name = activity.GetName();
                if (activity.GetNodeType() == ActivityType.End)
                {
                    stringBuilder.Append("结束");
                }
                else
                {
                    stringBuilder.AppendFormat("{1}", name, length);
                }
            }
            return stringBuilder.ToString();
        }

        private void method_4(DealAction dealAction_0, string string_2, string string_3)
        {
            UserTask userTaskById = UserTaskService.GetUserTaskById(string_2, null);
            if (userTaskById != null)
            {
                UserContext userInfo = base.UserInfo;
                DeptEmployee deptEmployeeByPositionId = DeptEmployeeService.GetDeptEmployeeByPositionId(userTaskById.OwnerId, userTaskById.PositionId);
                if (deptEmployeeByPositionId != null)
                {
                    userInfo = WebTools.GetUserInfo(deptEmployeeByPositionId._AutoID);
                }
                base.Server.ClearError();
                DriverEngine driverEngine = new DriverEngine(userInfo);
                string taskId = userTaskById.TaskId;
                Task taskById = driverEngine.GetTaskById(taskId);
                Instance instanceById = driverEngine.GetInstanceById(taskById.InstanceId);
                Activity activityById = driverEngine.GetActivityById(instanceById, taskById.ActivityId);
                DbConnection dbConnection = SysDatabase.CreateConnection();
                try
                {
                    dbConnection.Open();
                    DbTransaction dbTransaction = dbConnection.BeginTransaction();
                    try
                    {
                        if ((taskById.DefineType != ActivityType.Normal.ToString() ? false : userTaskById.TaskState == 0.ToString()))
                        {
                            TaskService.ApplyShareTask(taskId, string_2, dbTransaction);
                            taskById.MainPerformer = userTaskById.OwnerId;
                        }
                        userTaskById._UpdateTime = DateTime.Now;
                        userTaskById.ReadTime = new DateTime?(DateTime.Now);
                        userTaskById.IsRead = "1";
                        userTaskById.DealTime = new DateTime?(DateTime.Now);
                        if ((!activityById.IsDecisionNode() ? false : string_3 == ""))
                        {
                            string_3 = "同意";
                        }
                        userTaskById.DealAdvice = string_3;
                        if (dealAction_0 != DealAction.Disagree && taskById.DefineType == ActivityType.Sign.ToString())
                        {
                            dealAction_0 = DealAction.Agree;
                        }
                        userTaskById.DealAction = this.method_6(activityById, userTaskById, dealAction_0);
                        userTaskById.TaskState = 2.ToString();
                        userTaskById.DealUser = base.EmployeeID;
                        if (userTaskById.AgentId == base.EmployeeID && userTaskById.EmployeeName.IndexOf("代签") == -1)
                        {
                            userTaskById.EmployeeName = string.Concat(userTaskById.EmployeeName, "〔", base.EmployeeName, "代签〕");
                        }
                        if (userTaskById.PositionId == "")
                        {
                            userTaskById.PositionId = userInfo.PositionId;
                            userTaskById.PositionName = userInfo.PositionName;
                        }
                        if (userTaskById.DeptId == "")
                        {
                            userTaskById.DeptId = userInfo.DeptId;
                            userTaskById.DeptName = userInfo.DeptName;
                        }
                        _UserTask __UserTask = new _UserTask(dbTransaction);
                        __UserTask.UpdateAdvice(userTaskById);
                        if ((taskById.DefineType == ActivityType.Sign.ToString() ? true : taskById.MainPerformer == userTaskById.OwnerId))
                        {
                            List<Task> tasks = driverEngine.NextTask(instanceById, taskById, dbTransaction);
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
                                userTaskById.RecIds = EIS.AppBase.Utility.GetJoinString(stringCollections);
                                userTaskById.RecNames = EmployeeService.GetEmployeeNameList(stringCollections);
                                __UserTask.UpdateAdvice(userTaskById);
                            }
                            if ((taskById.DefineType != ActivityType.Normal.ToString() ? false : taskById.MainPerformer == userTaskById.OwnerId))
                            {
                                EIS.WorkFlow.Engine.Utility.DeleteUnDoneUserTaskByTaskId(taskById.TaskId, dbTransaction);
                            }
                        }
                        if (userTaskById.IsAssign == "1")
                        {
                            AppMsg appMsg = new AppMsg(base.UserInfo)
                            {
                                Title = string.Concat("加签任务处理完成提醒，来自", base.EmployeeName),
                                MsgType = "",
                                MsgUrl = string.Concat("SysFolder/Workflow/DealFlow.aspx?sysTaskId=", taskById.TaskId),
                                RecIds = userTaskById._UserName,
                                RecNames = EmployeeService.GetEmployeeName(userTaskById._UserName),
                                SendTime = new DateTime?(DateTime.Now),
                                Sender = base.EmployeeName,
                                Content = string.Format("{0}已经在加签任务【{1}】中发表意见。\r\n以下是他（她）的意见：{2}", base.EmployeeName, instanceById.InstanceName, string_3)
                            };
                            AppMsgService.SendMessage(appMsg);
                        }
                        dbTransaction.Commit();
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        dbTransaction.Rollback();
                        base.WriteExceptionLog("错误", this.FormatException(exception, ""));
                        this.fileLogger.Error<Exception>(exception);
                        this.stringDictionary_0.Add(string_2, exception.Message);
                    }
                }
                finally
                {
                    if (dbConnection != null)
                    {
                        ((IDisposable)dbConnection).Dispose();
                    }
                }
            }
        }

        private void method_5(string uTaskId, string reason)
        {
            UserTask userTaskById = UserTaskService.GetUserTaskById(uTaskId, null);
            if (userTaskById != null)
            {
                string taskId = userTaskById.TaskId;
                UserContext userInfo = base.UserInfo;
                DriverEngine driverEngine = new DriverEngine(userInfo);
                Task taskById = driverEngine.GetTaskById(taskId);
                if (taskById.DefineType == ActivityType.Sign.ToString())
                {
                    this.method_4(DealAction.Disagree, uTaskId, reason);
                }
                else if (userTaskById.IsAssign != "1")
                {
                    Instance instanceById = driverEngine.GetInstanceById(taskById.InstanceId);
                    Activity activityById = driverEngine.GetActivityById(instanceById, taskById.ActivityId);
                    DbConnection dbConnection = SysDatabase.CreateConnection();
                    try
                    {
                        dbConnection.Open();
                        DbTransaction dbTransaction = dbConnection.BeginTransaction();
                        try
                        {
                            try
                            {
                                if ((taskById.DefineType != ActivityType.Normal.ToString() ? false : userTaskById.TaskState == 0.ToString()))
                                {
                                    TaskService.ApplyShareTask(taskId, userTaskById.UserTaskId, dbTransaction);
                                    taskById.MainPerformer = userTaskById.OwnerId;
                                }
                                _UserTask __UserTask = new _UserTask(dbTransaction);
                                userTaskById._UpdateTime = DateTime.Now;
                                userTaskById.ReadTime = new DateTime?(DateTime.Now);
                                userTaskById.IsRead = "1";
                                userTaskById.DealTime = new DateTime?(DateTime.Now);
                                userTaskById.DealAdvice = reason;
                                userTaskById.DealAction = EnumDescription.GetFieldText(DealAction.RollBack);
                                userTaskById.TaskState = 2.ToString();
                                userTaskById.DealUser = base.EmployeeID;
                                if (userTaskById.AgentId == base.EmployeeID)
                                {
                                    userTaskById.EmployeeName = string.Concat(userTaskById.EmployeeName, "（", base.EmployeeName, "代签）");
                                }
                                if (userTaskById.PositionId == "")
                                {
                                    userTaskById.PositionId = userInfo.PositionId;
                                    userTaskById.PositionName = userInfo.PositionName;
                                }
                                if (userTaskById.DeptId == "")
                                {
                                    userTaskById.DeptId = userInfo.DeptId;
                                    userTaskById.DeptName = userInfo.DeptName;
                                }
                                __UserTask.UpdateAdvice(userTaskById);
                                EIS.WorkFlow.Engine.Utility.DeleteUnDoneUserTaskByTaskId(taskById.TaskId, dbTransaction);
                                string item = "";
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
                                        item = stringCollections[0];
                                    }
                                }
                                if (item != "")
                                {
                                    List<Task> tasks = driverEngine.RollBackTask(taskId, item, dbTransaction);
                                    if (instanceById.NeedUpdate)
                                    {
                                        (new _Instance(dbTransaction)).UpdateXPDL(instanceById);
                                    }
                                    _Task __Task = new _Task(dbTransaction);
                                    taskById.TaskState = 2.ToString();
                                    taskById._UpdateTime = DateTime.Now;
                                    __Task.Update(taskById);
                                    if (tasks.Count > 0)
                                    {
                                        StringCollection stringCollections1 = new StringCollection();
                                        foreach (Task task in tasks)
                                        {
                                            TaskService.GetTaskDealUser(task.TaskId, stringCollections1, dbTransaction);
                                        }
                                        userTaskById.RecIds = EIS.AppBase.Utility.GetJoinString(stringCollections1);
                                        userTaskById.RecNames = EmployeeService.GetEmployeeNameList(stringCollections1);
                                        __UserTask.UpdateAdvice(userTaskById);
                                    }
                                    dbTransaction.Commit();
                                }
                                else
                                {
                                    return;
                                }
                            }
                            catch (Exception exception1)
                            {
                                Exception exception = exception1;
                                dbTransaction.Rollback();
                                this.stringDictionary_0.Add(uTaskId, exception.Message);
                            }
                        }
                        finally
                        {
                            dbConnection.Close();
                        }
                    }
                    finally
                    {
                        if (dbConnection != null)
                        {
                            ((IDisposable)dbConnection).Dispose();
                        }
                    }
                }
                else
                {
                    this.method_4(DealAction.Submit, uTaskId, reason);
                }
            }
        }

        private string method_6(Activity activity_0, UserTask userTask_0, DealAction dealAction_0)
        {
            string fieldText;
            if (dealAction_0 == DealAction.Submit)
            {
                fieldText = (!activity_0.IsDecisionNode() ? "提交" : "同意");
            }
            else if (dealAction_0 == DealAction.RollBack)
            {
                fieldText = EnumDescription.GetFieldText(dealAction_0);
            }
            else if (dealAction_0 != DealAction.Agree)
            {
                fieldText = (dealAction_0 != DealAction.Disagree ? "提交" : AppSettings.Instance.SignRejectAction);
            }
            else
            {
                fieldText = (userTask_0.DealType != "2" ? EnumDescription.GetFieldText(dealAction_0) : "提交");
            }
            return fieldText;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.string_0 = base.GetParaValue("wfCode");
            this.string_1 = base.GetParaValue("tblName");
            if (!base.IsPostBack)
            {
                this.SubmitTaskIdList = base.GetParaValue("TaskIdList");
                this.method_0(this.string_0);
            }
        }
    }
}