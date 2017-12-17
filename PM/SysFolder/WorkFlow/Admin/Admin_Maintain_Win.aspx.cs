using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission;
using EIS.Permission.Model;
using EIS.Permission.Service;
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
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.SysFolder.WorkFlow.Admin
{
    public partial class Admin_Maintain_Win : PageBase
    {
        protected HtmlForm form1;

        protected TextBox TextBox1;

        protected HiddenField HiddenField1;

        protected HiddenField HiddenField1_PosId;

        protected Button Button1;

        protected TextBox TextBox2;

        protected HiddenField HiddenField2;

        protected Button Button2;

        protected TextBox TextBox3;

        protected HiddenField HiddenField_uTaskId;

        protected Button Button3;

        protected TextBox TextBox4;

        protected HiddenField HiddenField4;

        protected HiddenField HiddenField4_PosId;

        protected Button Button4;

        protected TextBox TextBox5;

        protected HiddenField HiddenField5;

        protected HiddenField HiddenField5_PosId;

        protected Button Button5;

        protected CheckBox CheckBox1;

        protected CheckBox CheckBox2;

        protected CheckBox CheckBox3;

        protected Button Button6;

        public string insId = "";

        public string nodeId = "";

        public string nodeName = "";

        public string tState = "-1";

        public string nodeType = "";

        public string tipClass = "hidden";

        public string tipInfo = "";

        public StringBuilder dealList = new StringBuilder();

        private DataTable dataTable_0;

        private Instance instance_0;

        public Admin_Maintain_Win()
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.method_0();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string str = "";
            string str1 = string.Format("select top 1 _AutoId from T_E_WF_Task where (TaskState='0' or TaskState='1') and instanceId ='{0}' and ActivityId='{1}' order by ArriveTime desc", this.insId, this.nodeId);
            object obj = SysDatabase.ExecuteScalar(str1);
            if (obj != DBNull.Value)
            {
                str = obj.ToString();
                DriverEngine driverEngine = new DriverEngine(base.UserInfo);
                DbConnection dbConnection = SysDatabase.CreateConnection();
                try
                {
                    dbConnection.Open();
                    DbTransaction dbTransaction = dbConnection.BeginTransaction();
                    try
                    {
                        try
                        {
                            StringCollection stringCollections = new StringCollection();
                            string value = this.HiddenField2.Value;
                            char[] chrArray = new char[] { ',' };
                            stringCollections.AddRange(value.Split(chrArray));
                            driverEngine.AssignTask(str, stringCollections, dbTransaction);
                            dbTransaction.Commit();
                        }
                        catch (Exception exception1)
                        {
                            Exception exception = exception1;
                            this.tipClass = "";
                            this.tipInfo = exception.Message;
                            this.fileLogger.Error<Exception>(exception);
                            dbTransaction.Rollback();
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
                this.dataTable_0 = InstanceService.GetUserDealState(this.insId);
                this.method_0();
            }
            else
            {
                this.tipClass = "";
                this.tipInfo = "当前节点为非活动节点，不能加签任务";
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string value = this.HiddenField_uTaskId.Value;
            if (value != "")
            {
                UserTask userTaskById = UserTaskService.GetUserTaskById(value, null);
                SysDatabase.ExecuteNonQuery(string.Format("update T_E_WF_UserTask set _IsDel=1 where _AutoId='{0}'", value));
                _WorkflowLog __WorkflowLog = new _WorkflowLog();
                WorkflowLog workflowLog = new WorkflowLog(base.UserInfo)
                {
                    EmpName = base.EmployeeName,
                    AppId = this.insId,
                    LogTime = new DateTime?(DateTime.Now),
                    AppName = "T_E_WF_Instance",
                    LogContent = string.Format("撤销用户任务：{0}；{1}；原因：{2}", userTaskById.EmployeeName, value, this.TextBox3.Text)
                };
                __WorkflowLog.Add(workflowLog);
                this.dataTable_0 = InstanceService.GetUserDealState(this.insId);
                this.method_0();
            }
            else
            {
                this.tipClass = "";
                this.tipInfo = "请选择要撤销的用户任务";
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string value = this.HiddenField_uTaskId.Value;
            if (value != "")
            {
                UserTask userTaskById = UserTaskService.GetUserTaskById(value, null);
                UserTaskService.UpdateTaskOwner(value, this.HiddenField4.Value, this.HiddenField4_PosId.Value);
                _WorkflowLog __WorkflowLog = new _WorkflowLog();
                WorkflowLog workflowLog = new WorkflowLog(base.UserInfo)
                {
                    EmpName = base.EmployeeName,
                    AppId = this.insId,
                    LogTime = new DateTime?(DateTime.Now),
                    AppName = "T_E_WF_Instance",
                    LogContent = string.Format("变更任务处理人：{0}->{1}；{2}", userTaskById.EmployeeName, this.TextBox4.Text, value)
                };
                __WorkflowLog.Add(workflowLog);
                this.dataTable_0 = InstanceService.GetUserDealState(this.insId);
                this.method_0();
            }
            else
            {
                this.tipClass = "";
                this.tipInfo = "请选择要变更处理人的任务";
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            string value = this.HiddenField5.Value;
            if (value != "")
            {
                StringCollection stringCollections = new StringCollection();
                string[] strArrays = new string[] { "|", this.nodeId, "|1|", value, "|0|", this.HiddenField5_PosId.Value };
                stringCollections.Add(string.Concat(strArrays));
                EIS.WorkFlow.Engine.Utility.UpdateInstanceUser(this.instance_0, stringCollections, true);
                this.dataTable_0 = InstanceService.GetUserDealState(this.insId);
                this.method_0();
            }
            else
            {
                this.tipClass = "";
                this.tipInfo = "请选择该步骤新的处理人";
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            this.dataTable_0 = InstanceService.GetUserDealState(this.insId);
            this.method_0();
        }

        private void method_0()
        {
            DataRow[] dataRowArray = this.dataTable_0.Select(string.Concat("ActivityId='", this.nodeId, "'"), "ArriveTime desc");
            if ((int)dataRowArray.Length > 0)
            {
                this.tState = dataRowArray[0]["tState"].ToString();
            }
            int num = 1;
            foreach (DataRow row in this.dataTable_0.Rows)
            {
                if (row["ActivityId"].ToString() != this.nodeId)
                {
                    continue;
                }
                string str = row["TaskState"].ToString();
                StringBuilder stringBuilder = this.dealList;
                object[] item = new object[12];
                int num1 = num;
                num = num1 + 1;
                item[0] = num1;
                item[1] = row["TaskName"];
                item[2] = row["EmployeeName"];
                item[3] = row["DealAction"];
                item[4] = row["DealAdvice"];
                item[5] = row["ReadTime"];
                item[6] = row["DealTime"];
                item[7] = (str == "2" ? " yes" : " no");
                item[8] = row["_AutoId"];
                item[9] = row["TaskId"];
                item[10] = str;
                item[11] = row["isAssign"];
                stringBuilder.AppendFormat("<tr p='{8}|{9}|{10}|{2}|{11}'><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5:yyyy-MM-dd HH:mm}</td><td>{6:yyyy-MM-dd HH:mm}</td><td class='deltd{7}'>&nbsp;</td></tr>\r\n", item);
            }
        }

        private UserContext method_1(DataTable dataTable_1)
        {
            UserContext userInfo;
            DataRow[] dataRowArray = dataTable_1.Select("(TaskState='0' or TaskState='1') and IsNull(isAssign,'')=''", "ArriveTime desc");
            if ((int)dataRowArray.Length > 0)
            {
                UserTask userTaskById = UserTaskService.GetUserTaskById(dataRowArray[0]["_AutoID"].ToString(), null);
                DeptEmployee deptEmployeeByPositionId = DeptEmployeeService.GetDeptEmployeeByPositionId(userTaskById.OwnerId, userTaskById.PositionId);
                if (deptEmployeeByPositionId == null)
                {
                    userInfo = null;
                    return userInfo;
                }
                userInfo = EIS.Permission.Utility.GetUserInfo(deptEmployeeByPositionId._AutoID, HttpContext.Current.Session["webId"].ToString());
                return userInfo;
            }
            userInfo = null;
            return userInfo;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.insId = base.GetParaValue("instanceId");
            this.nodeId = base.GetParaValue("nodeId");
            DriverEngine driverEngine = new DriverEngine(base.UserInfo);
            this.instance_0 = driverEngine.GetInstanceById(this.insId);
            Activity activityById = driverEngine.GetActivityById(this.instance_0, this.nodeId);
            this.nodeName = activityById.GetName();
            this.nodeType = activityById.GetNodeType().ToString();
            if (!base.IsPostBack)
            {
                this.dataTable_0 = InstanceService.GetUserDealState(this.insId);
                this.method_0();
                this.CheckBox1.Checked = activityById.GetExtendedAttribute("DataControl") != "0";
                this.CheckBox2.Checked = activityById.GetSafeOption("CanSelUser") != "0";
                this.CheckBox3.Checked = activityById.GetSafeOption("CanRollBack") == "1";
                DataRow appData = EIS.WorkFlow.Engine.Utility.GetAppData(this.instance_0);
                UserContext userContext = this.method_1(this.dataTable_0);
                List<DeptEmployee> activityUser = EIS.WorkFlow.Engine.Utility.GetActivityUser(this.instance_0, activityById, new WFSession(userContext, this.instance_0, appData, activityById));
                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder length = new StringBuilder();
                foreach (DeptEmployee deptEmployee in activityUser)
                {
                    stringBuilder.AppendFormat("{0},", deptEmployee.EmployeeName);
                    length.AppendFormat("{0},", deptEmployee.EmployeeID);
                }
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Length = stringBuilder.Length - 1;
                }
                if (length.Length > 0)
                {
                    length.Length = length.Length - 1;
                }
                this.TextBox5.Text = stringBuilder.ToString();
                this.HiddenField5.Value = length.ToString();
            }
        }
    }
}