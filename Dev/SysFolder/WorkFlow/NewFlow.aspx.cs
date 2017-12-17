using AjaxPro;
using EIS.AppBase;
using EIS.AppModel;
using EIS.AppModel.Workflow;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using EIS.Permission.Model;
using EIS.Permission.Service;

using EIS.WebBase.ModelLib.Service;
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
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using WebBase.JZY.Tools;

namespace EIS.WebBase.SysFolder.WorkFlow
{
    public partial class NewFlow : PageBase
    {
        public StringBuilder sbmodel = new StringBuilder();

        public XmlDocument xmlDoc = new XmlDocument();

        public string tblHTML = "";

        public string xmlData = "";

        public string sIndex = "";

        public string workflowId = "";

        public string editScriptBlock = "";

        public string workflowCode = "";

        public string nodeCode = "";

        public string customScript = "";

        public string formWidth = "";

        public StringBuilder NextActivityList = new StringBuilder();

        private Define define_0 = new Define();



        public string InstanceName
        {
            get
            {
                return this.ViewState["InstanceName"].ToString();
            }
            set
            {
                this.ViewState["InstanceName"] = value;
            }
        }

        public string isNew
        {
            get
            {
                return this.ViewState["isNew"].ToString();
            }
            set
            {
                this.ViewState["isNew"] = value;
            }
        }

        public string MainId
        {
            get
            {
                return this.ViewState["MainId"].ToString();
            }
            set
            {
                this.ViewState["MainId"] = value;
            }
        }

        public string SysFuncEtag
        {
            get
            {
                if (HttpContext.Current.Cache["SysFuncEtag"] == null)
                {
                    string str = HttpContext.Current.Server.MapPath("~/Js/SysFunction.js");
                    FileInfo fileInfo = new FileInfo(str);
                    CacheDependency cacheDependency = new CacheDependency(str);
                    HttpRuntime.Cache.Insert("SysFuncEtag", fileInfo.LastWriteTime.Ticks, cacheDependency);
                }
                return HttpContext.Current.Cache["SysFuncEtag"].ToString();
            }
        }

        public string TblName
        {
            get
            {
                return this.ViewState["tblName"].ToString();
            }
            set
            {
                this.ViewState["tblName"] = value;
            }
        }

        public NewFlow()
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Activity activityById;
            string dealStrategy;
            DriverEngine driverEngine = new DriverEngine(base.UserInfo);
            Instance instance = new Instance()
            {
                InstanceId = Guid.NewGuid().ToString(),
                _UserName = base.EmployeeID,
                _OrgCode = base.OrgCode,
                _CreateTime = DateTime.Now,
                _UpdateTime = DateTime.Now,
                _IsDel = 0,
                InstanceName = this.txtInstanceName.Text.Trim(),
                WorkflowId = this.workflowId,
                DeptId = this.Session["DeptId"].ToString(),
                DeptName = this.Session["DeptName"].ToString(),
                CompanyId = this.Session["CompanyId"].ToString(),
                CompanyName = this.Session["CompanyName"].ToString(),
                EmployeeName = base.EmployeeName,
                AppId = this.MainId,
                AppName = this.TblName,
                Importance = this.selImportance.SelectedValue
            };
            if (this.txtDealline.Text.Length > 0)
            {
                instance.Deadline = new DateTime?(Convert.ToDateTime(this.txtDealline.Text.Trim()));
            }
            instance.Remark = this.txtRemark.Text;
            instance.XPDL = driverEngine.CopyActivityPerformers(this.define_0, base.UserInfo);
            if (!string.IsNullOrEmpty(base.Request["tranact"]))
            {
                StringCollection stringCollections = new StringCollection();
                if (!string.IsNullOrEmpty(base.Request["chkstep"]))
                {
                    string item = base.Request["tranact"];
                    char[] chrArray = new char[] { ',' };
                    string[] strArrays = item.Split(chrArray);
                    StringCollection stringCollections1 = new StringCollection();
                    string str = base.Request["chkstep"];
                    chrArray = new char[] { ',' };
                    stringCollections1.AddRange(str.Split(chrArray));
                    string[] strArrays1 = strArrays;
                    for (int i = 0; i < (int)strArrays1.Length; i++)
                    {
                        string str1 = strArrays1[i];
                        chrArray = new char[] { '|' };
                        string[] strArrays2 = str1.Split(chrArray);
                        string str2 = strArrays2[1];
                        if (strArrays2[2] != "1")
                        {
                            if (!stringCollections1.Contains(str2))
                            {
                                stringCollections.Add(string.Concat(str1, "||0|"));
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(base.Request[string.Concat("pid", str2)]))
                                {
                                    activityById = driverEngine.GetActivityById(instance, str2);
                                    dealStrategy = activityById.GetDealStrategy();
                                    chrArray = new char[] { '|' };
                                    if ((dealStrategy.Split(chrArray)[1] == "1" ? false : activityById.GetNodeType() != ActivityType.Auto) && !this.method_1(activityById))
                                    {
                                        this.Session["_syserror"] = string.Concat("步骤【", activityById.GetName(), "】没有定义处理人");
                                        base.Server.Transfer("FlowErrorInfo.aspx?taskId=", false);
                                    }
                                }
                                stringCollections.Add(string.Concat(str1, "||1|"));
                            }
                        }
                        else if (!stringCollections1.Contains(str2))
                        {
                            stringCollections.Add(string.Concat(str1, "||0|"));
                        }
                        else if (!string.IsNullOrEmpty(base.Request[string.Concat("pid", str2)]))
                        {
                            string item1 = base.Request[string.Concat("pid", str2)];
                            item1 = item1.Trim(",".ToCharArray()).Replace(",,", "");
                            string item2 = base.Request[string.Concat("ppos", str2)];
                            item2 = item2.Trim(",".ToCharArray()).Replace(",,", "");
                            string[] strArrays3 = new string[] { str1, "|", item1, "|1|", item2 };
                            stringCollections.Add(string.Concat(strArrays3));
                        }
                        else
                        {
                            activityById = driverEngine.GetActivityById(instance, str2);
                            dealStrategy = activityById.GetDealStrategy();
                            chrArray = new char[] { '|' };
                            if ((dealStrategy.Split(chrArray)[1] == "1" ? false : activityById.GetNodeType() != ActivityType.Auto))
                            {
                                this.Session["_syserror"] = string.Concat("步骤【", activityById.GetName(), "】没有定义处理人");
                                base.Server.Transfer("FlowErrorInfo.aspx?taskId=", false);
                            }
                            stringCollections.Add(string.Concat(str1, "||1|"));
                        }
                    }
                    EIS.WorkFlow.Engine.Utility.UpdateInstanceUser(instance, stringCollections, false);
                }
                else
                {
                    this.Session["_syserror"] = "请勾选下一步的步骤名称";
                    base.Server.Transfer("FlowErrorInfo.aspx?error=1&taskId=", false);
                }
            }
            if ((string.IsNullOrEmpty(this.MainId) ? false : InstanceService.IsRunAlready(this.TblName, this.MainId)))
            {
                this.Session["_syserror"] = "每条业务数据只能发起一支流程";
                base.Server.Transfer("FlowErrorInfo.aspx?taskId=", false);
            }
            DbConnection dbConnection = SysDatabase.CreateConnection();
            try
            {
                Task task = new Task();
                dbConnection.Open();
                DbTransaction dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        string str3 = this.txtRemark.Text.Trim();
                        if (str3 == "")
                        {
                            str3 = "提交";
                        }
                        driverEngine.NewWorkFlowInstance(instance, dbTransaction);
                        task = driverEngine.StartWorkFlowInstance(instance, str3, dbTransaction);
                        List<Task> tasks = driverEngine.NextTask(instance, task, dbTransaction);
                        if (instance.NeedUpdate)
                        {
                            (new _Instance(dbTransaction)).UpdateXPDL(instance);
                        }
                        if (tasks.Count > 0)
                        {
                            UserTask userTaskByTaskId = EIS.WorkFlow.Engine.Utility.GetUserTaskByTaskId(task.TaskId, base.EmployeeID, dbTransaction);
                            StringCollection stringCollections2 = new StringCollection();
                            foreach (Task task1 in tasks)
                            {
                                TaskService.GetTaskDealUser(task1.TaskId, stringCollections2, dbTransaction);
                            }
                            userTaskByTaskId.RecIds = EIS.AppBase.Utility.GetJoinString(stringCollections2);
                            userTaskByTaskId.RecNames = EmployeeService.GetEmployeeNameList(stringCollections2);
                            (new _UserTask(dbTransaction)).UpdateAdvice(userTaskByTaskId);
                        }
                        this.method_2(instance.InstanceId, dbTransaction);
                        dbTransaction.Commit();
                    }
                    catch (NoUserException noUserException1)
                    {
                        NoUserException noUserException = noUserException1;
                        dbTransaction.Rollback();
                        this.fileLogger.Error<NoUserException>(noUserException);
                        this.Session["_syserror"] = noUserException.Message;
                        this.Session["_syserror_more"] = noUserException.ToString();
                        base.Server.Transfer("FlowErrorInfo.aspx?taskId=", false);
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        dbTransaction.Rollback();
                        this.fileLogger.Error<Exception>(exception);
                        this.Session["_syserror"] = exception.Message;
                        base.Server.Transfer("FlowErrorInfo.aspx?taskId=", false);
                    }
                }
                finally
                {
                    dbConnection.Close();
                }
                base.Server.Transfer(string.Concat("DealFlowAfter.aspx?taskId=", task.TaskId), false);
            }
            finally
            {
                if (dbConnection != null)
                {
                    ((IDisposable)dbConnection).Dispose();
                }
            }
        }

        protected void btnTempSave_Click(object sender, EventArgs e)
        {
            DriverEngine driverEngine = new DriverEngine(base.UserInfo);
            Instance instance = new Instance()
            {
                InstanceId = Guid.NewGuid().ToString(),
                _UserName = base.EmployeeID,
                _OrgCode = base.OrgCode,
                _CreateTime = DateTime.Now,
                _UpdateTime = DateTime.Now,
                _IsDel = 0,
                InstanceName = this.txtInstanceName.Text.Trim(),
                WorkflowId = this.workflowId,
                DeptId = this.Session["DeptId"].ToString(),
                DeptName = this.Session["DeptName"].ToString(),
                CompanyId = this.Session["CompanyId"].ToString(),
                CompanyName = this.Session["CompanyName"].ToString(),
                EmployeeName = base.EmployeeName,
                AppId = this.MainId,
                AppName = this.TblName,
                Importance = this.selImportance.SelectedValue
            };
            if (this.txtDealline.Text.Length > 0)
            {
                instance.Deadline = new DateTime?(Convert.ToDateTime(this.txtDealline.Text.Trim()));
            }
            instance.Remark = this.txtRemark.Text;
            instance.XPDL = driverEngine.CopyActivityPerformers(this.define_0, base.UserInfo);
            if (!string.IsNullOrEmpty(base.Request["tranact"]))
            {
                StringCollection stringCollections = new StringCollection();
                if (!string.IsNullOrEmpty(base.Request["chkstep"]))
                {
                    string item = base.Request["tranact"];
                    char[] chrArray = new char[] { ',' };
                    string[] strArrays = item.Split(chrArray);
                    StringCollection stringCollections1 = new StringCollection();
                    string str = base.Request["chkstep"];
                    chrArray = new char[] { ',' };
                    stringCollections1.AddRange(str.Split(chrArray));
                    string[] strArrays1 = strArrays;
                    for (int i = 0; i < (int)strArrays1.Length; i++)
                    {
                        string str1 = strArrays1[i];
                        chrArray = new char[] { '|' };
                        string[] strArrays2 = str1.Split(chrArray);
                        string str2 = strArrays2[1];
                        if (strArrays2[2] == "1")
                        {
                            if (!stringCollections1.Contains(str2))
                            {
                                stringCollections.Add(string.Concat(str1, "||0|"));
                            }
                            else if (!string.IsNullOrEmpty(base.Request[string.Concat("pid", str2)]))
                            {
                                string item1 = base.Request[string.Concat("pid", str2)];
                                item1 = item1.Trim(",".ToCharArray()).Replace(",,", "");
                                string item2 = base.Request[string.Concat("ppos", str2)];
                                item2 = item2.Trim(",".ToCharArray()).Replace(",,", "");
                                string[] strArrays3 = new string[] { str1, "|", item1, "|1|", item2 };
                                stringCollections.Add(string.Concat(strArrays3));
                            }
                            else
                            {
                                Activity activityById = driverEngine.GetActivityById(instance, str2);
                                string dealStrategy = activityById.GetDealStrategy();
                                chrArray = new char[] { '|' };
                                if ((dealStrategy.Split(chrArray)[1] == "1" ? false : activityById.GetNodeType() != ActivityType.Auto))
                                {
                                    this.Session["_syserror"] = string.Concat("步骤【", activityById.GetName(), "】没有定义处理人");
                                    base.Server.Transfer("FlowErrorInfo.aspx?taskId=", false);
                                }
                                stringCollections.Add(string.Concat(str1, "||1|"));
                            }
                        }
                    }
                    if (stringCollections.Count > 0)
                    {
                        EIS.WorkFlow.Engine.Utility.UpdateInstanceUser(instance, stringCollections, false);
                    }
                }
                else
                {
                    this.Session["_syserror"] = "请勾选下一步的步骤名称";
                    base.Server.Transfer("FlowErrorInfo.aspx?error=1&taskId=", false);
                }
            }
            if ((string.IsNullOrEmpty(this.MainId) ? false : InstanceService.IsRunAlready(this.TblName, this.MainId)))
            {
                this.Session["_syserror"] = "每条业务数据只能发起一支流程";
                base.Server.Transfer("FlowErrorInfo.aspx?taskId=", false);
            }
            Task task = new Task();
            DbConnection dbConnection = SysDatabase.CreateConnection();
            try
            {
                dbConnection.Open();
                DbTransaction dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        driverEngine.NewWorkFlowInstance(instance, dbTransaction);
                        task = driverEngine.StartWorkFlowInstance(instance, this.txtRemark.Text, false, dbTransaction);
                        driverEngine.UpdateAppDataState(instance, InstanceState.Ready, dbTransaction);
                        this.method_2(instance.InstanceId, dbTransaction);
                        dbTransaction.Commit();
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        dbTransaction.Rollback();
                        this.fileLogger.Error<Exception>(exception);
                        this.Session["_syserror"] = exception.Message;
                        base.Server.Transfer("FlowErrorInfo.aspx?taskId=", false);
                    }
                }
                finally
                {
                    dbConnection.Close();
                }
                this.Session["_sysinfo"] = "任务已经保存至桌面待办，下次可以直接从待办列表中发起。";
                base.Server.Transfer(string.Concat("DealFlowAfter.aspx?taskId=", task.TaskId, "&info=1"), false);
            }
            finally
            {
                if (dbConnection != null)
                {
                    ((IDisposable)dbConnection).Dispose();
                }
            }
        }

        [AjaxMethod]
        public int CheckData(string tblName, string mainId)
        {
            string str = string.Format("select count(*) from {0} where _AutoId='{1}'", tblName, mainId);
            object obj = SysDatabase.ExecuteScalar(str);
            return (obj == null ? 0 : Convert.ToInt32(obj));
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void DelAllRecord(string tblName, string parentId)
        {
            StringBuilder stringBuilder = new StringBuilder();
            _TableInfo __TableInfo = new _TableInfo(tblName);
            __TableInfo.GetModel();
            List<TableInfo> subTable = __TableInfo.GetSubTable();
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                string[] tableName = new string[] { "select * from ", tblName, " where _MainID='", parentId, "'" };
                foreach (DataRow row in SysDatabase.ExecuteTable(string.Concat(tableName)).Rows)
                {
                    string str = row["_AutoID"].ToString();
                    foreach (TableInfo tableInfo in subTable)
                    {
                        tableName = new string[] { "delete ", tableInfo.TableName, " where _MainID='", str, "'" };
                        SysDatabase.ExecuteNonQuery(string.Concat(tableName), dbTransaction);
                    }
                }
                stringBuilder.AppendFormat("delete {0}  where _mainId='{1}';", tblName, parentId);
                SysDatabase.ExecuteNonQuery(stringBuilder.ToString(), dbTransaction);
                dbTransaction.Commit();
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                dbTransaction.Rollback();
                throw exception;
            }
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void DelRecord(string mainTbl, string autoid)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (InstanceService.IsRunAlready(mainTbl, autoid))
            {
                if (SysDatabase.ExecuteScalar(string.Format("select count(*) from T_E_WF_Instance i inner join T_E_WF_Task t on i._AutoID=t.InstanceId\r\n                    where i.AppName='{0}' and i.AppId='{1}'", mainTbl, autoid)).ToString() != "1")
                {
                    throw new Exception("本条记录已经存在审批数据，不能删除");
                }
                string str = string.Format("select top 1 _AutoId from T_E_WF_Instance where AppName='{0}' and AppId='{1}'", mainTbl, autoid);
                string str1 = SysDatabase.ExecuteScalar(str).ToString();
                EIS.WorkFlow.Engine.Utility.RemoveInstance(str1, base.UserInfo);
            }
            if (DaArchiveService.IsArchived(mainTbl, autoid))
            {
                throw new Exception("本条记录已经归档，不能删除");
            }
            ModelSave modelSave = new ModelSave(this);
            stringBuilder.AppendFormat("<?xml version=\"1.0\" encoding=\"utf-8\"?><root><Table TableName=\"{0}\">", mainTbl);
            stringBuilder.AppendFormat("<row state=\"Deleted\" id=\"\"><_AutoID><![CDATA[{0}]]></_AutoID></row></Table></root>", autoid);
            modelSave.SaveData(mainTbl, stringBuilder.ToString());
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void DelSubRecord(string subTbl, string autoId)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("delete {0}  where _autoId='{1}';", subTbl, autoId);
            SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string ExecSQL(string scriptCode, string para)
        {
            string str;
            string tableSQLScript = TableService.GetTableSQLScript(scriptCode);
            if (tableSQLScript != "")
            {
                tableSQLScript = base.ReplaceContext(tableSQLScript);
                tableSQLScript = EIS.AppBase.Utility.ReplaceParaValues(tableSQLScript, para);
                object obj = SysDatabase.ExecuteScalar(tableSQLScript);
                str = (obj == null ? "" : obj.ToString());
            }
            else
            {
                str = "";
            }
            return str;
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string GetAutoSn(string fieldId, string para)
        {
            object obj;
            string str;
            DataTable dataTable = SysDatabase.ExecuteTable(string.Format("select TableName,FieldName,FieldType, FieldInDispStyle,FieldInDispStyleTxt from T_E_Sys_FieldInfo where _AutoId='{0}'", fieldId));
            if (dataTable.Rows.Count > 0)
            {
                string str1 = dataTable.Rows[0]["FieldInDispStyleTxt"].ToString();
                string str2 = dataTable.Rows[0]["FieldType"].ToString();
                string str3 = dataTable.Rows[0]["FieldName"].ToString();
                string str4 = dataTable.Rows[0]["TableName"].ToString();
                string str5 = "";
                string str6 = "";
                string str7 = "";
                string str8 = "";
                if (str1.Length <= 0)
                {
                    str = null;
                    return str;
                }
                str1 = base.ReplaceContext(str1);
                string[] strArrays = str1.Split("|".ToCharArray());
                if (str2 == "1")
                {
                    str5 = (strArrays.GetValue(0) == null ? "" : strArrays.GetValue(0).ToString());
                    str6 = strArrays.GetValue(3).ToString();
                    string[] strArrays1 = para.Split(new char[] { '|' });
                    for (int i = 0; i < (int)strArrays1.Length; i++)
                    {
                        string str9 = strArrays1[i];
                        string[] strArrays2 = str9.Split("=".ToCharArray(), 2);
                        str5 = str5.Replace(string.Concat("{", strArrays2[0].Substring(1), "}"), strArrays2[1]);
                        str6 = str6.Replace(string.Concat("{", strArrays2[0].Substring(1), "}"), strArrays2[1]);
                    }
                    DateTime today = DateTime.Today;
                    string str10 = today.ToString("MM");
                    string str11 = today.ToString("dd");
                    str5 = str5.Replace("{年}", today.ToString("yyyy"));
                    str5 = str5.Replace("{年2}", today.ToString("yy"));
                    str5 = str5.Replace("{月}", str10);
                    str5 = str5.Replace("{日}", str11);
                    str6 = str6.Replace("{年}", today.ToString("yyyy"));
                    str6 = str6.Replace("{年2}", today.ToString("yy"));
                    str6 = str6.Replace("{月}", str10);
                    str6 = str6.Replace("{日}", str11);
                    string str12 = string.Concat("select max(", str3, ") from ", str4);
                    string[] strArrays3 = new string[] { str12, " where ", str3, " like '", str5, "%", str6, "'" };
                    string str13 = string.Concat(strArrays3);
                    obj = SysDatabase.ExecuteScalar(SysDatabase.GetSqlStringCommand(str13));
                    int num = 1;
                    if ((obj == null ? false : obj != DBNull.Value))
                    {
                        string str14 = "";
                        str14 = obj.ToString();
                        if (str5 != "")
                        {
                            str14 = str14.Substring(str5.Length, str14.Length - str5.Length).Trim();
                        }
                        if (str6 != "")
                        {
                            str14 = str14.Substring(0, str14.Length - str6.Length).Trim();
                        }
                        num = (!int.TryParse(str14, out num) ? 0 : Convert.ToInt32(str14));
                        num++;
                        str8 = string.Concat(str5, num.ToString(string.Concat("d", strArrays[1])), str6);
                    }
                    else
                    {
                        num = (strArrays[2].Trim() != "" ? Convert.ToInt32(strArrays[2].Trim()) : 1);
                        str8 = (strArrays.GetValue(4).ToString() != "1" ? string.Concat(str5, Convert.ToString(num), str6) : string.Concat(str5, num.ToString(string.Concat("d", strArrays[1])), str6));
                    }
                }
                else if (str2 == "2")
                {
                    str7 = string.Concat("select isNull(max(", str3, "),0) from ", str4);
                    obj = SysDatabase.ExecuteScalar(str7);
                    if (obj != null)
                    {
                        str8 = (Convert.ToInt32(obj) != 0 ? Convert.ToString(Convert.ToInt32(obj) + 1) : strArrays[2]);
                    }
                }
                str = str8;
                return str;
            }
            str = null;
            return str;
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public DataTable GetDataTable(string scriptCode, string para)
        {
            DataTable dataTable;
            string tableSQLScript = TableService.GetTableSQLScript(scriptCode);
            if (tableSQLScript != "")
            {
                if (TableService.HasTableSQLScriptCommandPara(para))
                {
                    DbCommand dbcmmd = SysDatabase.GetSqlStringCommand(tableSQLScript);
                    string[] strArrays = para.Split(new char[] { '|' });
                    for (int i = 0; i < (int)strArrays.Length; i++)
                    {
                        string str = strArrays[i];
                        string[] strArrays1 = str.Split("=".ToCharArray(), 2);
                        if (strArrays1[0].Contains("@int_"))
                        {
                            SysDatabase.AddInParameter(dbcmmd, strArrays1[0], DbType.Int64, Convert.ToInt64(strArrays1[1]));
                        }
                        else if (strArrays1[0].Contains("@str_"))
                        {
                            SysDatabase.AddInParameter(dbcmmd, strArrays1[0], DbType.String, strArrays1[1]);
                        }
                        else if (strArrays1[0].Contains("@date_"))
                        {
                            SysDatabase.AddInParameter(dbcmmd, strArrays1[0], DbType.DateTime, Convert.ToDateTime(strArrays1[1]));
                        }
                        else if (strArrays1[0].Contains("@num_"))
                        {
                            SysDatabase.AddInParameter(dbcmmd, strArrays1[0], DbType.DateTime, Convert.ToDecimal(strArrays1[1]));
                        }
                    }
                    dataTable = SysDatabase.ExecuteTable(dbcmmd);
                }
                else
                {
                    tableSQLScript = base.ReplaceContext(tableSQLScript);
                    tableSQLScript = EIS.AppBase.Utility.ReplaceParaValues(tableSQLScript, para);
                    dataTable = SysDatabase.ExecuteTable(tableSQLScript);
                }
            }
            else
            {
                dataTable = null;
            }
            return dataTable;
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public DataTable GetLinkData(string fieldId, string para)
        {
            DataTable dataTable;
            string str = string.Format("select FieldInDispStyle,FieldInDispStyleTxt from T_E_Sys_FieldInfo where _AutoId='{0}'", fieldId);
            DataTable dataTable1 = SysDatabase.ExecuteTable(str);
            if (dataTable1.Rows.Count > 0)
            {
                string str1 = dataTable1.Rows[0]["FieldInDispStyleTxt"].ToString();
                if (str1.Length <= 0)
                {
                    dataTable = null;
                    return dataTable;
                }
                str = base.ReplaceContext(str1).Replace("[QUOTES]", "'");
                string[] strArrays = para.Split(new char[] { '|' });
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    string str2 = strArrays[i];
                    string[] strArrays1 = str2.Split("=".ToCharArray(), 2);
                    str = str.Replace(string.Concat("{", strArrays1[0].Substring(1), "}"), strArrays1[1]);
                }
                dataTable = SysDatabase.ExecuteTable(str);
                return dataTable;
            }
            dataTable = null;
            return dataTable;
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string GetListHtml(string mainTbl, string subTbl, string parentId, string sIndex)
        {
            ModelBuilder modelBuilder = new ModelBuilder(this)
            {
                Sindex = sIndex,
                DataControl = new List<DataControl>(),
                DataContolFirst = false,
                ReplaceValue = ""
            };
            return modelBuilder.GetRelationList(mainTbl, subTbl, parentId);
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public DataTable GetQuery(string queryId, string keyWord, string condition)
        {
            StringBuilder stringBuilder = new StringBuilder();
            _FieldInfo __FieldInfo = new _FieldInfo();
            TableInfo modelById = (new _TableInfo()).GetModelById(queryId);
            foreach (FieldInfo modelQueryDisp in __FieldInfo.GetModelQueryDisp(modelById.TableName))
            {
                if (stringBuilder.Length <= 0)
                {
                    stringBuilder.AppendFormat("({0} like '%{1}%'", modelQueryDisp.FieldName, keyWord);
                }
                else
                {
                    stringBuilder.AppendFormat(" or {0} like '%{1}%'", modelQueryDisp.FieldName, keyWord);
                }
            }
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(")");
            }
            if (condition.Length > 0)
            {
                stringBuilder.Append(string.Concat(" and ", condition));
            }
            if (stringBuilder.Length == 0)
            {
                stringBuilder.Append(" 1=1 ");
            }
            string str = string.Concat("select ListSQL,ConnectionId from T_E_Sys_TableInfo where tableName='", modelById.TableName, "'");
            DataTable dataTable = SysDatabase.ExecuteTable(str);
            string str1 = dataTable.Rows[0]["ConnectionId"].ToString();
            string str2 = dataTable.Rows[0]["ListSQL"].ToString();
            str2 = str2.Replace("|^condition^|", stringBuilder.ToString()).Replace("|^sortdir^|", "").Replace("\r\n", " ").Replace("\t", "");
            str2 = this.ReplaceContext(str2);
            DataTable dataTable1 = new DataTable();
            if (str1.Trim() == "")
            {
                dataTable1 = SysDatabase.ExecuteTable(str2);
            }
            else
            {
                CustomDb customDb = new CustomDb();
                customDb.CreateDatabaseByConnectionId(str1.Trim());
                dataTable1 = customDb.ExecuteTable(str2);
            }
            if (dataTable1.Rows.Count > 1)
            {
                dataTable1 = new DataTable();
            }
            return dataTable1;
        }

        public string getTblHtml()
        {
            object[] name;
            DataRow mainRow = null;
            XmlDeclaration xmlDeclaration = this.xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            this.xmlDoc.AppendChild(xmlDeclaration);
            try
            {
                ModelBuilder modelBuilder = new ModelBuilder(this)
                {
                    DataContolFirst = true,
                    Sindex = DefineService.GetPrintStyleById(this.workflowId)
                };
                string paraValue = base.GetParaValue("cpro");
                if (paraValue == "")
                {
                    paraValue = base.GetParaValue(string.Concat(this.TblName, "cpro"));
                }
                modelBuilder.DataControl = modelBuilder.GenDataControlFromPara(paraValue, this.TblName);
                Activity startActivity = DriverEngine.GetStartActivity(this.workflowId);
                foreach (DataControl activityDataControl in EIS.WorkFlow.Engine.Utility.GetActivityDataControl(startActivity))
                {
                    if (modelBuilder.DataControl.FindIndex((DataControl dataControl_1) => dataControl_1.FieldName == activityDataControl.FieldName) != -1)
                    {
                        continue;
                    }
                    modelBuilder.DataControl.Add(activityDataControl.Clone());
                }
                modelBuilder.ReplaceValue = "";
                if (startActivity.GetStyleName() != "")
                {
                    modelBuilder.Sindex = startActivity.GetStyleName();
                }
                if (modelBuilder.DataControl.Count > 0)
                {
                    string str = this.method_0(this.workflowCode, modelBuilder.DataControl);
                    if (str.Length > 0)
                    {
                        modelBuilder.Sindex = str;
                    }
                }
                this.sIndex = modelBuilder.Sindex;
                if (this.MainId.Trim() != "")
                {
                    this.tblHTML = modelBuilder.GetTblHtml(this.TblName, string.Concat("_autoid='", this.MainId, "'"), this.sbmodel, this.xmlDoc);
                }
                else
                {
                    this.tblHTML = modelBuilder.GetTblHtml(this.TblName, "", this.sbmodel, this.xmlDoc);
                }
                mainRow = modelBuilder.MainRow;
                this.xmlData = this.xmlDoc.DocumentElement.OuterXml;
                this.MainId = modelBuilder.MainId;
            }
            catch (Exception exception)
            {
                this.tblHTML = exception.Message;
            }
            DriverEngine driverEngine = new DriverEngine(base.UserInfo);
            Instance instance = new Instance()
            {
                InstanceId = Guid.NewGuid().ToString(),
                _UserName = base.EmployeeID,
                _OrgCode = base.OrgCode,
                DeptId = this.Session["DeptId"].ToString(),
                CompanyId = this.Session["CompanyId"].ToString(),
                XPDL = driverEngine.CopyActivityPerformers(this.define_0, base.UserInfo),
                ProcessId = "1",
                InstanceState = EnumDescription.GetFieldText(InstanceState.Processing),
                WorkflowId = this.workflowId,
                AppId = this.MainId,
                AppName = this.TblName
            };
            if (this.isNew != "true")
            {
                string str1 = string.Format("select * from {0} where _autoId='{1}'", this.TblName, this.MainId);
                DataTable dataTable = SysDatabase.ExecuteTable(str1);
                if (dataTable.Rows.Count > 0)
                {
                    mainRow = dataTable.Rows[0];
                }
            }
            Activity activity = driverEngine.GetStartActivity(instance);
            this.nodeCode = activity.GetCode();
            WFSession wFSession = new WFSession(base.UserInfo, instance, mainRow, activity);
            driverEngine.CurSession = wFSession;
            List<Activity> activities = driverEngine.StartNextActivity(instance);
            foreach (Activity activity1 in activities)
            {
                List<DeptEmployee> activityUser = EIS.WorkFlow.Engine.Utility.GetActivityUser(instance, activity1, wFSession);
                string safeOption = activity1.GetSafeOption("CanSelUser");
                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder length = new StringBuilder();
                StringBuilder stringBuilder1 = new StringBuilder();
                foreach (DeptEmployee deptEmployee in activityUser)
                {
                    if (safeOption == "0")
                    {
                        stringBuilder.AppendFormat("{0},", deptEmployee.EmployeeName);
                    }
                    else
                    {
                        stringBuilder.AppendFormat("<span title='点击删除' class='performer' actId='{1}'>{0}<img src='../../img/common/close.png'></span>", deptEmployee.EmployeeName, activity1.GetId());
                    }
                    length.AppendFormat("{0},", deptEmployee.EmployeeID);
                    stringBuilder1.AppendFormat("{0},", deptEmployee.PositionId);
                }
                if (length.Length > 0)
                {
                    length.Length = length.Length - 1;
                }
                if (stringBuilder1.Length > 0)
                {
                    stringBuilder1.Length = stringBuilder1.Length - 1;
                }
                EIS.WorkFlow.Engine.Utility.IsManualTask(activity1);
                bool flag = true;
                bool extendedAttribute = activity.GetExtendedAttribute("CanSelPath") == "0";
                bool flag1 = extendedAttribute;
                if (!extendedAttribute)
                {
                    flag = (activities.Count <= 1 ? true : false);
                }
                if (safeOption == "0")
                {
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Length = stringBuilder.Length - 1;
                    }
                    StringBuilder nextActivityList = this.NextActivityList;
                    name = new object[] { activity1.GetName(), stringBuilder, activity1.GetId(), activity1.SelectPath, null, null, null };
                    name[4] = (flag ? "checked" : "");
                    name[5] = length;
                    name[6] = (flag1 ? "onclick='return false;'" : "");
                    nextActivityList.AppendFormat("<div class='nextTask'><input type='checkbox' {6} {4} value='{2}' name='chkstep' id='chk{2}'/>\r\n                    <input type=text class='hidden' value='{5}' id='pid{2}' name='pid{2}'/><label for='chk{2}'>{0}：</label>\r\n                    <span class='userPanel' id='userPanel{2}'>{1}</span>&nbsp;<input type=hidden value='{3}|{2}|0' name='tranact' id='tranact{2}'/></div>", name);
                }
                else
                {
                    StringBuilder nextActivityList1 = this.NextActivityList;
                    name = new object[] { activity1.GetName(), stringBuilder, activity1.GetId(), activity1.SelectPath, length, null, null, null };
                    name[5] = (flag ? "checked" : "");
                    name[6] = stringBuilder1;
                    name[7] = (flag1 ? "onclick='return false;'" : "");
                    nextActivityList1.AppendFormat("<div class='nextTask'>\r\n                                <input type='checkbox' {7} {5} value='{2}' name='chkstep' id='chk{2}'/><input type=text class='hidden' value='{4}' id='pid{2}' name='pid{2}'/>\r\n                                <input type=text class='hidden' value='{6}' id='ppos{2}' name='ppos{2}' />\r\n                                <input type=text class='hidden' value='' id='pname{2}' name='pname{2}' /><label for='chk{2}'>{0}：</label>\r\n                                <span class='userPanel' id='userPanel{2}'>{1}</span>&nbsp;<input type=hidden value='{3}|{2}|0' name='tranact'  id='tranact{2}'/>\r\n                                <span onclick=\"javascript:seluser('{2}')\" class='seluser'><img title='添加人员' src='../../img/common/add_small.png'></span></div>", name);
                }
            }
            return this.tblHTML;
        }

        private string method_0(string string_0, List<DataControl> list_0)
        {
            string str;
            DataTable dataTable = SysDatabase.ExecuteTable(string.Format("select IsNull(StyleExp,'') StyleExp from T_E_WF_Config where Enable='是' and WFId='{0}'", string_0));
            if (dataTable.Rows.Count != 0)
            {
                string str1 = dataTable.Rows[0]["StyleExp"].ToString();
                if (str1.Length > 0)
                {
                    str1 = str1.Replace("\r\n", "`");
                    char[] chrArray = new char[] { '\u0060' };
                    string[] strArrays = str1.Split(chrArray);
                    int num = 0;
                    while (num < (int)strArrays.Length)
                    {
                        string str2 = strArrays[num].Replace("=>", "`");
                        chrArray = new char[] { '\u0060' };
                        string[] strArrays1 = str2.Split(chrArray);
                        if (((int)strArrays1.Length != 2 || strArrays1[0].Length <= 0 ? false : strArrays1[1].Length > 0))
                        {
                            string str3 = base.ReplaceContext(strArrays1[0]);
                            foreach (DataControl list0 in list_0)
                            {
                                str3 = str3.Replace(string.Concat("{", list0.FieldName, "}"), list0.DefaultValue);
                            }
                            if (int.Parse(SysDatabase.ExecuteScalar(string.Concat("select count(*) where ", str3)).ToString()) <= 0)
                            {
                                str = "";
                                return str;
                            }
                            else
                            {
                                str = strArrays1[1];
                                return str;
                            }
                        }
                        else
                        {
                            num++;
                        }
                    }
                }
                str = "";
            }
            else
            {
                str = "";
            }
            return str;
        }

        private bool method_1(Activity activity_0)
        {
            bool flag = false;
            Performers performers = activity_0.GetPerformers();
            StringCollection stringCollections = new StringCollection();
            char[] chrArray = new char[] { ',' };
            stringCollections.AddRange("04,05,06,07,08,09,32,33,34,35,22,23,24".Split(chrArray));
            IEnumerator enumerator = performers.GetPerformers().GetEnumerator();
            try
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        string description = ((Performer)enumerator.Current).GetDescription();
                        chrArray = new char[] { '|' };
                        string[] strArrays = description.Split(chrArray);
                        this.fileLogger.Trace(strArrays[0]);
                        if (stringCollections.Contains(strArrays[0]))
                        {
                            flag = true;
                            break;
                        }
                        else if ((int)strArrays.Length >= 4 && strArrays[3].Length > 0)
                        {
                            flag = true;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return flag;
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
            Match match = null;
            DateTime now;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(NewFlow));
            this.workflowId = base.GetParaValue("workflowid");
            if (this.workflowId != "")
            {
                this.define_0 = DefineService.GetWorkflowDefineModelById(this.workflowId);
            }
            else
            {
                string paraValue = base.GetParaValue("workflowCode");
                if (paraValue != "")
                {
                    this.define_0 = DefineService.GetWorkflowByCode(paraValue);
                }
            }
            if (this.define_0 == null)
            {
                this.Session["_sysinfo"] = "输入参数（WorkflowId 或者 WorkflowCode）不正确，找不到流程定义！";
                base.Server.Transfer("../AppFrame/AppInfo.aspx?msgType=warning", false);
            }
            this.workflowCode = this.define_0.WorkflowCode;
            this.workflowId = this.define_0.WorkflowId;
            this.customScript = base.GetCustomScript("ref_AppInput");
            if (!base.IsPostBack)
            {
                if (string.IsNullOrEmpty(base.Request["note"]))
                {
                    string str = string.Concat("select top 1 * from T_E_WF_Direction where WFId='", this.define_0.WorkflowCode, "' and enable='是'");
                    DataTable dataTable = SysDatabase.ExecuteTable(str);
                    if (dataTable.Rows.Count > 0 && SysDatabase.ExecuteTable(string.Format("select * from T_OA_Reminded where _CreateTime > '{0}' and AppID='{1}' and EmployeeId='{2}'", Convert.ToDateTime(dataTable.Rows[0]["_UpdateTime"]), this.workflowId, base.EmployeeID)).Rows.Count == 0)
                    {
                        base.Response.Redirect(string.Concat("NewBefore.aspx?", base.Request.QueryString));
                    }
                }
                this.TblName = this.define_0.AppNames;
                this.MainId = base.GetParaValue("AppId");
                this.isNew = (string.IsNullOrEmpty(this.MainId) ? "true" : "false");
                if (!string.IsNullOrEmpty(this.MainId))
                {
                    string str1 = string.Format("select top 1 u._AutoID from T_E_WF_Instance i inner join T_E_WF_UserTask u on i._AutoId=u.InstanceId\r\n                            where i.InstanceState='处理中' and u.TaskState='1' and i.AppId='{0}' and u.OwnerId='{1}'", this.MainId, base.EmployeeID);
                    object obj = SysDatabase.ExecuteScalar(str1);
                    if (obj != null)
                    {
                        base.Response.Redirect(string.Concat("DealFlow.aspx?taskId=", obj.ToString()), true);
                    }
                    else if ((string.IsNullOrEmpty(this.MainId) ? false : InstanceService.IsRunAlready(this.TblName, this.MainId)))
                    {
                        this.Session["_syserror"] = "每条业务数据只能发起一支流程";
                        base.Server.Transfer("FlowErrorInfo.aspx?taskId=", false);
                    }
                }
                this.getTblHtml();
                List<DictItem> dictItems = new List<DictItem>()
                {
                    new DictItem("普通", "1"),
                    new DictItem("重要", "2"),
                    new DictItem("非常重要", "3")
                };
                this.selImportance.DataSource = dictItems;
                this.selImportance.DataTextField = "text";
                this.selImportance.DataValueField = "value";
                this.selImportance.DataBind();
                foreach (DeptEmployee deptEmployeeByEmployeeId in DeptEmployeeService.GetDeptEmployeeByEmployeeId(base.EmployeeID))
                {
                    Department model = DepartmentService.GetModel(deptEmployeeByEmployeeId.CompanyID);
                    string str2 = (model.DeptAbbr == "" ? model.DeptName : model.DeptAbbr);
                    string str3 = (deptEmployeeByEmployeeId.PositionName == "未知" ? "" : string.Concat("－", deptEmployeeByEmployeeId.PositionName));
                    if (base.Session["PositionId"].ToString() != deptEmployeeByEmployeeId.PositionId)
                    {
                        ListItemCollection items = this.PositionList.Items;
                        ListItem listItem = new ListItem()
                        {
                            Text = string.Concat(str2, "－", deptEmployeeByEmployeeId.DeptName, str3),
                            Value = deptEmployeeByEmployeeId._AutoID
                        };
                        items.Add(listItem);
                    }
                    else
                    {
                        ListItemCollection listItemCollections = this.PositionList.Items;
                        ListItem listItem1 = new ListItem()
                        {
                            Text = string.Concat(str2, "－", deptEmployeeByEmployeeId.DeptName, str3),
                            Value = deptEmployeeByEmployeeId._AutoID,
                            Selected = true
                        };
                        listItemCollections.Add(listItem1);
                    }
                }
                string str4 = string.Format("select * from T_E_WF_Config where Enable='是' and WFId='{0}'", this.define_0.WorkflowCode);
                DataTable dataTable1 = SysDatabase.ExecuteTable(str4);
                if (dataTable1.Rows.Count <= 0)
                {
                    this.InstanceName = "";
                }
                else
                {
                    this.InstanceName = dataTable1.Rows[0]["InstanceName"].ToString();
                    this.InstanceName = base.ReplaceContext(this.InstanceName);
                    Regex regex = new Regex("{DateTime:([ymdhs-]+)}", RegexOptions.IgnoreCase);
                    foreach (Match matchA in regex.Matches(this.InstanceName))
                    {
                        string instanceName = this.InstanceName;
                        string value = matchA.Value;
                        now = DateTime.Now;
                        this.InstanceName = instanceName.Replace(value, now.ToString(matchA.Groups[1].Value));
                    }
                    regex = new Regex("\\[([ymdhs\\-\\u5E74\\u6708\\u65E5:/]+)]", RegexOptions.IgnoreCase);
                    foreach (Match match1 in regex.Matches(this.InstanceName))
                    {
                        string instanceName1 = this.InstanceName;
                        string value1 = match1.Value;
                        now = DateTime.Now;
                        this.InstanceName = instanceName1.Replace(value1, now.ToString(match1.Groups[1].Value));
                    }
                }
            }
            _TableInfo __TableInfo = new _TableInfo(this.TblName);
            this.formWidth = __TableInfo.GetModel().FormWidthStyle; //.get_FormWidthStyle();
            this.editScriptBlock = TableService.GetEditScriptBlock(this.TblName);
            if (this.editScriptBlock.Trim().Length > 0)
            {
                string paraValue1 = base.GetParaValue("ReplaceValue");
                if (paraValue1 != "")
                {
                    ModelBuilder modelBuilder = new ModelBuilder(this);
                    this.editScriptBlock = modelBuilder.ReplaceParaValue(paraValue1, this.editScriptBlock);
                    this.editScriptBlock = modelBuilder.GetUbbCode(this.editScriptBlock, "[CRYPT]", "[/CRYPT]", base.UserName);
                }
                this.editScriptBlock = base.ReplaceContext(this.editScriptBlock);
            }
        }

        protected void PositionList_SelectedIndexChanged(object sender, EventArgs e)
        {

            WebTools.ChangePosition(this.PositionList.SelectedValue);
            this.getTblHtml();
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void saveData(string tblNameList, string xmldata)
        {
            (new ModelSave(this)).SaveData(tblNameList, xmldata);
        }
    }
}