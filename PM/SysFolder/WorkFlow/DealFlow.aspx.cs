using AjaxPro;
using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.AppCommon;
using EIS.AppModel;
using EIS.AppModel.Service;
using EIS.AppModel.Workflow;
using EIS.Common.Pipe;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using EIS.Permission;
using EIS.Permission.Model;
using EIS.Permission.Service;
using WebBase.JZY.Tools;
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
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using EIS.Web.SysFolder.AppFrame;

namespace EIS.Web.SysFolder.WorkFlow
{
    public partial class DealFlow : PageBase
    {
    

        public StringBuilder sbmodel = new StringBuilder();

        public XmlDocument xmlDoc = new XmlDocument();

        public string fileCount = "";

        public string tblHTML = "";

        public string fileListUrl = "";

        public string xmlData = "";

        public string sIndex = "";

        public string workflowCode = "";

        public string nodeCode = "";

        public string taskId = "";

        public string uTaskId = "";

        public string importance = "";

        public string wokflowName = "";

        public string stepinfo = "display:none";

        public string ReAuthClass = "hidden";

        public Task curTask = new Task();

        public Instance curInstance = new Instance();

        public StringBuilder DealInfo = new StringBuilder();

        public StringBuilder SuggestTmpl = new StringBuilder();

        public StringBuilder NextActivityList = new StringBuilder();

        public StringBuilder CurStepInfo = new StringBuilder();

        public string RollBackClass = "";

        public string DirectClass = "";

        public string AssignClass = "";

        public string RelegateClass = "";

        public string LimitClass = "";

        public string editScriptBlock = "";

        public string customScript = "";

        public string TipMsg = "";

        public string SaveBeforeSubmit = "true";

        private DriverEngine driverEngine_0 = null;

        private UserTask userTask_0 = null;

        private Activity activity_0 = null;

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

        public string tblName
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

     

        protected void btnAgree_Click(object sender, EventArgs e)
        {
            string str = this.txtRemark.Text.Trim();
            if ((str != "" ? false : this.userTask_0.DealType != "2"))
            {
                str = "同意";
            }
            this.method_0(DealAction.Agree, str);
        }

        protected void btnHangUp_Click(object sender, EventArgs e)
        {
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
                        driverEngine.HangUpInstance(this.curInstance, dbTransaction);
                        dbTransaction.Commit();
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        dbTransaction.Rollback();
                        this.Session["_syserror"] = exception.Message;
                        base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?taskId=", this.taskId), false);
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

        protected void btnReject_Click(object sender, EventArgs e)
        {
            this.method_0(DealAction.Disagree, this.txtRemark.Text);
        }

        protected void btnResume_Click(object sender, EventArgs e)
        {
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
                        driverEngine.ResumeInstance(this.curInstance, dbTransaction);
                        dbTransaction.Commit();
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        dbTransaction.Rollback();
                        this.Session["_syserror"] = exception.Message;
                        base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?taskId=", this.taskId), false);
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

        protected void btnShareTask_Click(object sender, EventArgs e)
        {
            base.Server.Transfer(string.Concat("DealShareTask.aspx?uTaskId=", this.uTaskId), true);
        }

        protected void btnStop_Click(object sender, EventArgs e)
        {
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
                        driverEngine.StopInstance(this.curInstance, dbTransaction);
                        dbTransaction.Commit();
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        dbTransaction.Rollback();
                        this.Session["_syserror"] = exception.Message;
                        base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?taskId=", this.taskId), false);
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string text;
            Activity activityById;
            string dealStrategy;
            StringCollection stringCollections = new StringCollection();
            if (string.IsNullOrEmpty(base.Request["tranact"]))
            {
                this.method_0(DealAction.Submit, this.txtRemark.Text);
            }
            else if (this.userTask_0.IsAssign == "1")
            {
                this.method_0(DealAction.Submit, this.txtRemark.Text);
            }
            else if (!string.IsNullOrEmpty(base.Request["chkstep"]))
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
                                activityById = this.driverEngine_0.GetActivityById(this.curInstance, str2);
                                if (activityById.GetNodeType() != ActivityType.End)
                                {
                                    dealStrategy = activityById.GetDealStrategy();
                                    chrArray = new char[] { '|' };
                                    if ((dealStrategy.Split(chrArray)[1] == "1" ? false : activityById.GetNodeType() != ActivityType.Auto))
                                    {
                                        this.Session["_syserror"] = string.Concat("步骤【", activityById.GetName(), "】没有定义处理人");
                                        base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?taskId=", this.taskId), true);
                                    }
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
                        activityById = this.driverEngine_0.GetActivityById(this.curInstance, str2);
                        dealStrategy = activityById.GetDealStrategy();
                        chrArray = new char[] { '|' };
                        if ((dealStrategy.Split(chrArray)[1] == "1" ? false : activityById.GetNodeType() != ActivityType.Auto))
                        {
                            this.Session["_syserror"] = "没有定义处理人";
                            base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?taskId=", this.taskId), true);
                        }
                        stringCollections.Add(string.Concat(str1, "||1|"));
                    }
                }
                EIS.WorkFlow.Engine.Utility.UpdateInstanceUser(this.curInstance, stringCollections, true);
                int toDoUserTaskCountByTaskId = TaskService.GetToDoUserTaskCountByTaskId(this.curTask.TaskId, null);
                if (!(toDoUserTaskCountByTaskId <= 1 ? true : !(this.curTask.MainPerformer == this.userTask_0.OwnerId)))
                {
                    text = this.txtRemark.Text;
                    if ((!this.activity_0.IsDecisionNode() ? false : text == ""))
                    {
                        text = "同意";
                    }
                    base.Server.Transfer(string.Concat("DealAfterAssign.aspx?uTaskId=", this.uTaskId, "&advice=", text), true);
                }
                else if ((this.userTask_0.IsShare != "1" ? true : toDoUserTaskCountByTaskId <= 0))
                {
                    this.method_0(DealAction.Submit, this.txtRemark.Text);
                }
                else
                {
                    text = this.txtRemark.Text;
                    if ((!this.activity_0.IsDecisionNode() ? false : text == ""))
                    {
                        text = "同意";
                    }
                    base.Server.Transfer(string.Concat("DealAfterAssign.aspx?uTaskId=", this.uTaskId, "&advice=", text), true);
                }
            }
            else
            {
                this.Session["_syserror"] = "请勾选下一步的步骤名称";
                base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?taskId=", this.taskId), true);
            }
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
                EIS.WorkFlow.Engine.Utility.RemoveInstance(SysDatabase.ExecuteScalar(str).ToString());
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


        [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
        public string ExecSQL(string scriptCode, string para)
        {
            string str = "";
            string tableSQLScript = "";

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select ScriptTxt,ConnectionId from T_E_Sys_TableScript ");
            stringBuilder.Append(" where ScriptCode=@ScriptCode ");
            DbCommand ScriptCmmd = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
            SysDatabase.AddInParameter(ScriptCmmd, "@ScriptCode", DbType.String, scriptCode);
            DataTable dtScript = SysDatabase.ExecuteTable(ScriptCmmd);

            try
            {
                tableSQLScript = dtScript.Rows[0]["ScriptTxt"].ToString();
                string connectionId = dtScript.Rows[0]["ConnectionId"].ToString().Trim();
                if (connectionId != "")
                {
                    if (tableSQLScript != "")
                    {
                        CustomDb customDb = new CustomDb();
                        customDb.CreateDatabaseByConnectionId(connectionId.Trim());

                        if (tableSQLScript != "")
                        {
                            if (TableService.HasTableSQLScriptCommandPara(para))
                            {
                                tableSQLScript = base.ReplaceContext(tableSQLScript);
                                DbCommand dbcmmd = customDb.GetSqlStringCommand(tableSQLScript);
                                string[] strArrays = para.Split(new char[] { '|' });
                                for (int i = 0; i < (int)strArrays.Length; i++)
                                {
                                    str = strArrays[i];
                                    string[] strArrays1 = str.Split("=".ToCharArray(), 2);
                                    if (strArrays1[0].Contains("@int_"))
                                    {
                                        customDb.AddInParameter(dbcmmd, strArrays1[0], DbType.Int64, Convert.ToInt64(strArrays1[1]));
                                    }
                                    else if (strArrays1[0].Contains("@str_"))
                                    {
                                        customDb.AddInParameter(dbcmmd, strArrays1[0], DbType.String, strArrays1[1]);
                                    }
                                    else if (strArrays1[0].Contains("@date_"))
                                    {
                                        customDb.AddInParameter(dbcmmd, strArrays1[0], DbType.DateTime, Convert.ToDateTime(strArrays1[1]));
                                    }
                                    else if (strArrays1[0].Contains("@num_"))
                                    {
                                        customDb.AddInParameter(dbcmmd, strArrays1[0], DbType.DateTime, Convert.ToDecimal(strArrays1[1]));
                                    }
                                }
                                object obj = customDb.ExecuteNonQuery(dbcmmd);
                                str = (obj == null ? "" : obj.ToString());
                            }
                            else
                            {
                                tableSQLScript = base.ReplaceContext(tableSQLScript);
                                tableSQLScript = EIS.AppBase.Utility.ReplaceParaValues(tableSQLScript, para);
                                object obj = customDb.ExecuteScalar(tableSQLScript);
                                str = (obj == null ? "" : obj.ToString());
                            }
                        }
                    }
                }
                else
                {
                    tableSQLScript = TableService.GetTableSQLScript(scriptCode);
                    if (tableSQLScript != "")
                    {
                        if (TableService.HasTableSQLScriptCommandPara(para))
                        {
                            tableSQLScript = base.ReplaceContext(tableSQLScript);
                            DbCommand dbcmmd = SysDatabase.GetSqlStringCommand(tableSQLScript);
                            string[] strArrays = para.Split(new char[] { '|' });
                            for (int i = 0; i < (int)strArrays.Length; i++)
                            {
                                str = strArrays[i];
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
                            object obj = SysDatabase.ExecuteNonQuery(dbcmmd);
                            str = (obj == null ? "" : obj.ToString());
                        }
                        else
                        {
                            tableSQLScript = base.ReplaceContext(tableSQLScript);
                            tableSQLScript = EIS.AppBase.Utility.ReplaceParaValues(tableSQLScript, para);
                            object obj = SysDatabase.ExecuteScalar(tableSQLScript);
                            str = (obj == null ? "" : obj.ToString());
                        }
                    }
                }
            }
            catch { }
            finally { }

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

        [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
        public DataTable GetDataTable(string scriptCode, string para)
        {
            DataTable dataTable = null;
            string tableSQLScript = "";

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select ScriptTxt,ConnectionId from T_E_Sys_TableScript ");
            stringBuilder.Append(" where ScriptCode=@ScriptCode ");
            DbCommand ScriptCmmd = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
            SysDatabase.AddInParameter(ScriptCmmd, "@ScriptCode", DbType.String, scriptCode);
            DataTable dtScript = SysDatabase.ExecuteTable(ScriptCmmd);

            try
            {
                tableSQLScript = dtScript.Rows[0]["ScriptTxt"].ToString();
                string connectionId = dtScript.Rows[0]["ConnectionId"].ToString().Trim();
                if (connectionId != "")
                {
                    if (tableSQLScript != "")
                    {
                        CustomDb customDb = new CustomDb();
                        customDb.CreateDatabaseByConnectionId(connectionId.Trim());

                        if (TableService.HasTableSQLScriptCommandPara(para))
                        {
                            tableSQLScript = base.ReplaceContext(tableSQLScript);
                            DbCommand dbcmmd = customDb.GetSqlStringCommand(tableSQLScript);
                            string[] strArrays = para.Split(new char[] { '|' });
                            for (int i = 0; i < (int)strArrays.Length; i++)
                            {
                                string str = strArrays[i];
                                string[] strArrays1 = str.Split("=".ToCharArray(), 2);
                                if (strArrays1[0].Contains("@int_"))
                                {
                                    customDb.AddInParameter(dbcmmd, strArrays1[0], DbType.Int64, Convert.ToInt64(strArrays1[1]));
                                }
                                else if (strArrays1[0].Contains("@str_"))
                                {
                                    customDb.AddInParameter(dbcmmd, strArrays1[0], DbType.String, strArrays1[1]);
                                }
                                else if (strArrays1[0].Contains("@date_"))
                                {
                                    customDb.AddInParameter(dbcmmd, strArrays1[0], DbType.DateTime, Convert.ToDateTime(strArrays1[1]));
                                }
                                else if (strArrays1[0].Contains("@num_"))
                                {
                                    customDb.AddInParameter(dbcmmd, strArrays1[0], DbType.DateTime, Convert.ToDecimal(strArrays1[1]));
                                }
                            }
                            dataTable = customDb.ExecuteTable(dbcmmd);
                        }
                        else
                        {
                            tableSQLScript = base.ReplaceContext(tableSQLScript);
                            tableSQLScript = EIS.AppBase.Utility.ReplaceParaValues(tableSQLScript, para);
                            dataTable = customDb.ExecuteTable(tableSQLScript);
                        }
                    }
                }
                else
                {
                    if (tableSQLScript != "")
                    {
                        if (TableService.HasTableSQLScriptCommandPara(para))
                        {
                            tableSQLScript = base.ReplaceContext(tableSQLScript);
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
                }
            }
            catch { }
            finally { }

            return dataTable;
        }

        public string GetInstanceRefers()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (this.curInstance != null)
            {
                foreach (InstanceRefer instanceRefer in InstanceReferService.GetInstanceRefer(this.curInstance.InstanceId))
                {
                    stringBuilder.AppendFormat("<div class='refItem'><a class='refLink' href='../AppFrame/AppWorkFlowInfo.aspx?instanceId={0}' target='_blank'>⊙&nbsp;{1}</a></div>", instanceRefer.ReferId, instanceRefer.ReferName);
                }
            }
            return stringBuilder.ToString();
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
        public string GetListHtml(string mainTbl, string subTbl, string parentId)
        {
            ModelBuilder modelBuilder = new ModelBuilder(this)
            {
                Sindex = "",
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

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string GetSuggestTmpl()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (IdeaTemplate ideaTemplateByEmployeeId in IdeaTemplateService.GetIdeaTemplateByEmployeeId(base.EmployeeID))
            {
                stringBuilder.AppendFormat("{0}|", ideaTemplateByEmployeeId.IdeaName);
            }
            if (stringBuilder.ToString().EndsWith("|"))
            {
                stringBuilder.Length = stringBuilder.Length - 1;
            }
            return stringBuilder.ToString();
        }

        public string getTblHtml()
        {
            object[] id;
            bool flag;
            UserContext userInfo = WebTools.GetUserInfo(this.PositionList.SelectedValue);
            DriverEngine driverEngine = new DriverEngine(userInfo);
            List<Activity> activities = driverEngine.NextActivity(this.curInstance, this.curTask);
            foreach (Activity activity in activities)
            {
                DataRow appData = EIS.WorkFlow.Engine.Utility.GetAppData(this.curInstance);
                List<DeptEmployee> activityUser =EIS.WorkFlow.Engine.Utility.GetActivityUser(this.curInstance, activity, new WFSession(userInfo, this.curInstance, appData, this.activity_0));
                string safeOption = activity.GetSafeOption("CanSelUser");
                if (activity.GetNodeType() == ActivityType.End)
                {
                    safeOption = "0";
                }
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
                        stringBuilder.AppendFormat("<span title='点击删除' class='performer' actId='{1}'>{0}<img src='../../img/common/close.png'></span>", deptEmployee.EmployeeName, activity.GetId());
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
                EIS.WorkFlow.Engine.Utility.IsManualTask(activity);
                bool flag1 = true;
                bool extendedAttribute = this.activity_0.GetExtendedAttribute("CanSelPath") == "0";
                bool flag2 = extendedAttribute;
                if (!extendedAttribute)
                {
                    flag1 = (activities.Count <= 1 ? true : false);
                }
                if (safeOption != "0")
                {
                    flag = false;
                }
                else
                {
                    flag = (activityUser.Count != 0 ? true : activity.GetNodeType() == ActivityType.End);
                }
                if (flag)
                {
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Length = stringBuilder.Length - 1;
                    }
                    string name = activity.GetName();
                    if (activity.GetNodeType() != ActivityType.End)
                    {
                        name = string.Concat(name, "：");
                    }
                    StringBuilder nextActivityList = this.NextActivityList;
                    id = new object[] { name, stringBuilder, activity.GetId(), activity.SelectPath, null, null, null };
                    id[4] = (flag1 ? "checked" : "");
                    id[5] = length;
                    id[6] = (flag2 ? "onclick='return false;'" : "");
                    nextActivityList.AppendFormat("<div class='nextTask'><input type='checkbox' {6} {4} value='{2}' name='chkstep' id='chk{2}'/>\r\n                            <input type=text class='hidden' value='{5}' id='pid{2}' name='pid{2}'/><label for='chk{2}'>{0}</label>\r\n                            <span class='userPanel' id='userPanel{2}'>{1}</span>&nbsp;<input type=hidden value='{3}|{2}|0' name='tranact' id='tranact{2}'/></div>", id);
                }
                else
                {
                    StringBuilder nextActivityList1 = this.NextActivityList;
                    id = new object[] { activity.GetName(), stringBuilder, activity.GetId(), activity.SelectPath, length, null, null, null };
                    id[5] = (flag1 ? "checked" : "");
                    id[6] = stringBuilder1;
                    id[7] = (flag2 ? "onclick='return false;'" : "");
                    nextActivityList1.AppendFormat("<div class='nextTask'>\r\n                            <input type='checkbox' {7} {5} value='{2}' name='chkstep' id='chk{2}'/><input type=text class='hidden' value='{4}' id='pid{2}' name='pid{2}'/><input type=text class='hidden' value='{6}' id='ppos{2}' name='ppos{2}' /><input type=text class='hidden' value='' id='pname{2}' name='pname{2}' /><label for='chk{2}'>{0}：</label>\r\n                            <span class='userPanel' id='userPanel{2}'>{1}</span>&nbsp;<input type=hidden value='{3}|{2}|0' name='tranact' id='tranact{2}'/>\r\n                            <span onclick=\"javascript:seluser('{2}')\" class='seluser'><img title='添加人员' src='../../img/common/add_small.png'></span></div>", id);
                }
            }
            XmlDeclaration xmlDeclaration = this.xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            this.xmlDoc.AppendChild(xmlDeclaration);
            try
            {
                ModelBuilder modelBuilder = new ModelBuilder(this)
                {
                    DataContolFirst = true
                };
                if (this.curInstance._UserName != base.EmployeeID)
                {
                    modelBuilder.SheetEditLimit = this.activity_0.GetExtendedAttribute("DataControl") != "0";
                }
                if ((this.userTask_0.IsAssign != "1" ? false : this.curInstance._UserName != base.EmployeeID))
                {
                    modelBuilder.SheetEditLimit = false;
                }
                this.fileLogger.Debug("SheetEditLimit.Value={0}", modelBuilder.SheetEditLimit);
                modelBuilder.Sindex = DefineService.GetPrintStyleById(this.curInstance.WorkflowId);
                Activity activityById = driverEngine.GetActivityById(this.curInstance, this.curTask.ActivityId);
                modelBuilder.DataControl = EIS.WorkFlow.Engine.Utility.GetActivityDataControl(activityById);
                modelBuilder.ReplaceValue = "";
                if (activityById.GetStyleName() != "")
                {
                    modelBuilder.Sindex = activityById.GetStyleName();
                }
                this.sIndex = modelBuilder.Sindex;
                base.AppIndex = this.sIndex;
                string str = string.Concat("_AutoID='", this.curInstance.AppId, "'");
                this.fileLogger.Trace<string, string, string>("SaveBeforeSubmit={0},tblName={1},cond={2}", this.SaveBeforeSubmit, this.tblName, str);
                if (this.SaveBeforeSubmit != "false")
                {
                    this.fileLogger.Info(str);
                    this.tblHTML = modelBuilder.GetTblHtml(this.tblName, str, this.sbmodel, this.xmlDoc);
                    this.MainId = modelBuilder.MainId;
                }
                else
                {
                    this.tblHTML = modelBuilder.GetDetailHtml(this.tblName, str);
                    this.sbmodel.Append("var _sysModel=[];");
                    XmlElement xmlElement = this.xmlDoc.CreateElement("root");
                    this.xmlDoc.AppendChild(xmlElement);
                }
                this.xmlData = this.xmlDoc.DocumentElement.OuterXml;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                this.tblHTML = string.Concat(exception.Message, "<br/>", exception.StackTrace);
                this.fileLogger.Info(this.tblHTML);
            }
            return this.tblHTML;
        }


        private void method_0(DealAction dealAction_0, string string_0)
        {
            bool taskState;
            base.Server.ClearError();
            UserContext userInfo = WebTools.GetUserInfo(this.PositionList.SelectedValue);
            DriverEngine driverEngine = new DriverEngine(userInfo);
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                try
                {
                    this.method_3(this.curInstance.InstanceId, dbTransaction);
                    if (this.curTask.DefineType != ActivityType.Normal.ToString())
                    {
                        taskState = true;
                    }
                    else
                    {
                        int num = 0;
                        taskState = !(this.userTask_0.TaskState == num.ToString());
                    }
                    if (!taskState)
                    {
                        TaskService.ApplyShareTask(this.taskId, this.uTaskId, dbTransaction);
                        this.curTask.MainPerformer = this.userTask_0.OwnerId;
                    }
                    _UserTask __UserTask = new _UserTask(dbTransaction);
                    this.userTask_0 = UserTaskService.GetUserTaskById(this.uTaskId, dbTransaction);
                    if (this.userTask_0 == null)
                    {
                        this.Session["_syserror"] = "找不到对应的用户任务";
                        base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?taskId=", this.taskId), true);
                    }
                    this.userTask_0._UpdateTime = DateTime.Now;
                    this.userTask_0.DealTime = new DateTime?(DateTime.Now);
                    if ((!(string_0 == "") || !this.activity_0.IsDecisionNode() ? false : this.userTask_0.DealType != "2"))
                    {
                        string_0 = "同意";
                    }
                    this.userTask_0.DealAdvice = string_0;
                    this.userTask_0.DealAction = this.method_1(dealAction_0);
                    this.userTask_0.TaskState = 2.ToString();
                    this.userTask_0.DealUser = base.EmployeeID;
                    if (this.userTask_0.AgentId == base.EmployeeID && this.userTask_0.EmployeeName.IndexOf("代签") == -1)
                    {
                        this.userTask_0.EmployeeName = string.Concat(this.userTask_0.EmployeeName, "〔", base.EmployeeName, "代签〕");
                    }
                    this.userTask_0.PositionId = userInfo.PositionId;
                    this.userTask_0.PositionName = userInfo.PositionName;
                    this.userTask_0.DeptId = userInfo.DeptId;
                    this.userTask_0.DeptName = userInfo.DeptName;
                    __UserTask.UpdateAdvice(this.userTask_0);
                    if ((this.curTask.DefineType == ActivityType.Sign.ToString() ? true : this.curTask.MainPerformer == this.userTask_0.OwnerId))
                    {
                        List<Task> tasks = driverEngine.NextTask(this.curInstance, this.curTask, dbTransaction);
                        if (this.curInstance.NeedUpdate)
                        {
                            (new _Instance(dbTransaction)).UpdateXPDL(this.curInstance);
                        }
                        if (tasks.Count > 0)
                        {
                            StringCollection stringCollections = new StringCollection();
                            foreach (Task task in tasks)
                            {
                                TaskService.GetTaskDealUser(task.TaskId, stringCollections, dbTransaction);
                            }
                            this.userTask_0.RecIds = EIS.AppBase.Utility.GetJoinString(stringCollections);
                            this.userTask_0.RecNames = EmployeeService.GetEmployeeNameList(stringCollections);
                            __UserTask.UpdateAdvice(this.userTask_0);
                            if (!(this.curTask.DefineType != ActivityType.Normal.ToString() ? true : !(this.curTask.MainPerformer == this.userTask_0.OwnerId)))
                            {
                                EIS.WorkFlow.Engine.Utility.DeleteUnDoneUserTaskByTaskId(this.curTask.TaskId, dbTransaction);
                            }
                            else if (this.curTask.DefineType == ActivityType.Sign.ToString())
                            {
                                EIS.WorkFlow.Engine.Utility.DeleteUnDoneUserTaskByTaskId(this.curTask.TaskId, dbTransaction);
                            }
                        }
                    }
                    if (this.userTask_0.IsAssign == "1")
                    {
                        AppMsg appMsg = new AppMsg(base.UserInfo)
                        {
                            Title = string.Concat("加签任务完成提醒，来自", base.EmployeeName),
                            MsgType = "",
                            MsgUrl = string.Concat("SysFolder/Workflow/DealFlow.aspx?sysTaskId=", this.curTask.TaskId),
                            RecIds = this.userTask_0._UserName,
                            RecNames = EmployeeService.GetEmployeeName(this.userTask_0._UserName),
                            SendTime = new DateTime?(DateTime.Now),
                            Sender = base.EmployeeName,
                            Content = string.Format("{0}已经在加签任务【{1}】中发表意见。\r\n以下是他（她）的意见：{2}", base.EmployeeName, this.curInstance.InstanceName, this.txtRemark.Text)
                        };
                        AppMsgService.SendMessage(appMsg);
                    }
                    this.method_2(this.curInstance, dbTransaction);
                    dbTransaction.Commit();
                }
                catch (NoUserException noUserException1)
                {
                    NoUserException noUserException = noUserException1;
                    dbTransaction.Rollback();
                    this.fileLogger.Error<NoUserException>(noUserException);
                    this.Session["_syserror"] = noUserException.Message;
                    this.Session["_syserror_more"] = noUserException.ToString();
                    base.Server.Transfer("FlowErrorInfo.aspx?more=1&taskId=", true);
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    dbTransaction.Rollback();
                    base.WriteExceptionLog("错误", this.FormatException(exception, ""));
                    this.Session["_syserror"] = exception.Message;
                    base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?taskId=", this.taskId), false);
                }
            }
            finally
            {
                dbConnection.Close();
            }
            base.Server.Transfer(string.Concat("DealFlowAfter.aspx?taskId=", this.taskId), false);
        }

        private string method_1(DealAction dealAction_0)
        {
            string fieldText;
            if (dealAction_0 == DealAction.Submit)
            {
                fieldText = (!this.activity_0.IsDecisionNode() ? "提交" : "同意");
            }
            else if (dealAction_0 == DealAction.RollBack)
            {
                fieldText = EnumDescription.GetFieldText(dealAction_0);
            }
            else if (dealAction_0 != DealAction.Agree)
            {
                fieldText = (dealAction_0 != DealAction.Disagree ? "提交" : EnumDescription.GetFieldText(dealAction_0));
            }
            else
            {
                fieldText = (this.userTask_0.DealType != "2" ? EnumDescription.GetFieldText(dealAction_0) : "提交");
            }
            return fieldText;
        }

        private void method_2(Instance instance_0, DbTransaction dbTransaction_0)
        {
            if ((new _TableInfo(instance_0.AppName)).GetModel().TableType == 1)
            {
                string str = string.Format("Update {0} set _WFState='{2}' where _AutoId='{1}'", instance_0.AppName, instance_0.AppId, instance_0.InstanceState);
                SysDatabase.ExecuteNonQuery(str, dbTransaction_0);
            }
        }

        private void method_3(string string_0, DbTransaction dbTransaction_0)
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
           
            string itemValue;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DealFlow));
            this.driverEngine_0 = new DriverEngine(base.UserInfo);
            this.uTaskId = base.GetParaValue("taskId");
            if (!string.IsNullOrEmpty(this.uTaskId))
            {
                this.userTask_0 = UserTaskService.GetUserTaskById(this.uTaskId, null);
            }
            else
            {
                this.taskId = base.GetParaValue("sysTaskId");
                if (this.taskId == "")
                {
                    this.Session["_syserror"] = "参数有错！";
                    base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?taskId=", this.taskId), true);
                }
                else
                {
                    this.userTask_0 = EIS.WorkFlow.Engine.Utility.GetUserTaskByTaskId(this.taskId, base.EmployeeID);
                }
            }
            if (this.userTask_0 == null)
            {
                this.Session["_syserror"] = "本任务已经不存在，或者您不是本步骤的处理人，不能查看任务";
                base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?back=0&taskId=", this.taskId), true);
            }
            this.customScript = base.GetCustomScript("ref_AppInput");
            this.uTaskId = this.userTask_0.UserTaskId;
            this.taskId = this.userTask_0.TaskId;
            this.curTask = this.driverEngine_0.GetTaskById(this.taskId);
            this.curInstance = this.driverEngine_0.GetInstanceById(this.curTask.InstanceId);
            this.wokflowName = DefineService.GetWorkflowName(this.curInstance.WorkflowId);
            this.workflowCode = DefineService.GetWorkflowCode(this.curInstance.WorkflowId);
            this.activity_0 = this.driverEngine_0.GetActivityById(this.curInstance, this.curTask.ActivityId);
            this.nodeCode = this.activity_0.GetCode();
            if (!(this.userTask_0.TaskState == "2" ? false : !(this.userTask_0.TaskState == "3")))
            {
                this.Session["_syserror"] = "已经处理过的任务不能重新提交";
                base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?back=0&taskId=", this.taskId), true);
            }
            else if (!(this.userTask_0.TaskState != "0" ? true : !(this.userTask_0.IsRead == "0")))
            {
                UserTaskService.UpdateReadState(this.userTask_0.UserTaskId, UserTaskReadState.Read);
            }
            else if ((this.userTask_0.TaskState != "1" ? false : this.userTask_0.IsRead == "0"))
            {
                UserTaskService.UpdateReadState(this.userTask_0.UserTaskId, UserTaskReadState.Read);
            }
            if (this.userTask_0.IsShare != "1")
            {
                this.btnShareTask.CssClass = "hidden";
            }
            else if (this.userTask_0.TaskState != "1")
            {
                this.btnShareTask.CssClass = "hidden";
                StringCollection stringCollections = new StringCollection();
                TaskService.GetShareTaskDealUser(this.taskId, stringCollections, null);
                string employeeNameList = EmployeeService.GetEmployeeNameList(stringCollections);
                this.DealInfo.AppendFormat("<div class='tip'>提示：该任务是共享任务，只需要（{0}）其中一个人处理即可。</div>", employeeNameList);
            }
            else
            {
                this.btnShareTask.CssClass = "btn01";
            }
            if (this.userTask_0.IsAssign == "1")
            {
                this.btnSubmit.Text = "提 交";
                if (this.curTask.DefineType.ToLower() != "sign")
                {
                    this.btnSubmit.CssClass = "btn01";
                    this.btnAgree.CssClass = "hidden";
                    this.btnReject.CssClass = "hidden";
                }
                else
                {
                    this.btnSubmit.CssClass = "hidden";
                    this.btnAgree.CssClass = "btn01";
                    this.btnReject.CssClass = "btn01";
                    itemValue = "";
                    itemValue = SysConfig.GetConfig("WF_SignRejectText").ItemValue;
                    this.btnReject.Text = (string.IsNullOrEmpty(itemValue) ? "不同意" : itemValue);
                }
                this.LimitClass = "hidden";
                this.RollBackClass = "hidden";
                this.DirectClass = "hidden";
                this.AssignClass = "hidden";
                this.RelegateClass = "hidden";
                this.btnStop.CssClass = "hidden";
                this.btnHangUp.CssClass = "hidden";
                this.btnResume.CssClass = "hidden";
            }
            else if (this.userTask_0.DealType != "2")
            {
                itemValue = "";
                if (this.activity_0.IsDecisionNode())
                {
                    itemValue = SysConfig.GetConfig("WF_SubmitText").ItemValue;
                    this.btnSubmit.Text = (string.IsNullOrEmpty(itemValue) ? "提 交" : itemValue);
                }
                itemValue = SysConfig.GetConfig("WF_SignRejectText").ItemValue;
                this.btnReject.Text = (string.IsNullOrEmpty(itemValue) ? "不同意" : itemValue);
                if (this.activity_0.GetSafeOption("ReAuth") == "1")
                {
                    this.ReAuthClass = "";
                }
                this.AssignClass = (this.curTask.CanAssign == "1" ? "" : "hidden");
                this.LimitClass = (this.curTask.CanPublic == "1" ? "" : "hidden");
                if ((this.curTask.DefineType.ToLower() == "normal" ? false : !(this.curTask.DefineType.ToLower() == "start")))
                {
                    this.btnSubmit.CssClass = "hidden";
                    this.btnAgree.CssClass = "btn01";
                    this.btnReject.CssClass = "btn01";
                    this.RollBackClass = "hidden";
                    this.DirectClass = "hidden";
                    this.RelegateClass = "hidden";
                    this.btnStop.CssClass = "hidden";
                }
                else
                {
                    this.btnSubmit.CssClass = "btn01";
                    this.btnAgree.CssClass = "hidden";
                    this.btnReject.CssClass = "hidden";
                    this.RollBackClass = (this.curTask.CanRollBack == "1" ? "" : "hidden");
                    this.DirectClass = (this.curTask.CanJump == "1" ? "" : "hidden");
                    if (this.curTask.CanDelegateTo != "1")
                    {
                        this.RelegateClass = "hidden";
                    }
                    else
                    {
                        this.RelegateClass = (this.userTask_0.AgentId == base.EmployeeID ? "hidden" : "");
                    }
                    this.btnStop.CssClass = (this.curTask.CanStop == "1" ? "btn01" : "hidden");
                }
                if (this.curTask.CanHangUp != "1")
                {
                    this.btnHangUp.CssClass = "hidden";
                    this.btnResume.CssClass = "hidden";
                }
                else if (this.curInstance.InstanceState == EnumDescription.GetFieldText(InstanceState.Processing))
                {
                    this.btnHangUp.CssClass = "btn01";
                    this.btnResume.CssClass = "hidden";
                }
                else if (this.curInstance.InstanceState != EnumDescription.GetFieldText(InstanceState.Suspended))
                {
                    this.btnHangUp.CssClass = "hidden";
                    this.btnResume.CssClass = "hidden";
                }
                else
                {
                    this.btnResume.CssClass = "btn01";
                    this.btnHangUp.CssClass = "hidden";
                }
            }
            else
            {
                if (this.curTask.DefineType.ToLower() != "sign")
                {
                    this.btnSubmit.CssClass = "btn01";
                    this.btnSubmit.Text = "提 交";
                    this.btnAgree.CssClass = "hidden";
                    this.btnReject.CssClass = "hidden";
                }
                else
                {
                    this.btnSubmit.CssClass = "hidden";
                    this.btnAgree.CssClass = "btn01";
                    this.btnAgree.Text = "提 交";
                    this.btnReject.CssClass = "hidden";
                }
                this.LimitClass = "hidden";
                this.RollBackClass = "hidden";
                this.DirectClass = "hidden";
                this.AssignClass = "hidden";
                this.RelegateClass = "hidden";
                this.btnStop.CssClass = "hidden";
                this.btnHangUp.CssClass = "hidden";
                this.btnResume.CssClass = "hidden";
            }

            //允许直送
            if (this.curTask.CanJump == "1")
            {
                if (this.driverEngine_0.IsRollBackTask(this.curTask))
                {
                    //是回退任务
                    this.DirectClass = "";
                }
            }

            foreach (IdeaTemplate ideaTemplateByEmployeeId in IdeaTemplateService.GetIdeaTemplateByEmployeeId(base.EmployeeID))
            {
                this.SuggestTmpl.AppendFormat("<a class='tmplitem' href='javascript:'>{0}</a>", ideaTemplateByEmployeeId.IdeaName);
            }
            StringBuilder stringBuilder = new StringBuilder();
            List<UserTask> validUserTask = TaskService.GetValidUserTask(this.curInstance.InstanceId, this.curTask, null);
            if (validUserTask.Count > 1)
            {
                this.stepinfo = "";
                foreach (UserTask userTask in validUserTask)
                {
                    if (userTask.TaskState != 2.ToString())
                    {
                        this.CurStepInfo.AppendFormat("<span class='red'>{0}</span>、", EmployeeService.GetEmployeeName(userTask.OwnerId));
                    }
                    else
                    {
                        stringBuilder.AppendFormat("<span class='green'>{0}</span>、", EmployeeService.GetEmployeeName(userTask.OwnerId));
                    }
                }
                if (stringBuilder.Length > 1)
                {
                    stringBuilder.Insert(0, "，已处理人员（");
                    stringBuilder.Length = stringBuilder.Length - 1;
                    stringBuilder.Append("）");
                }
                if (this.CurStepInfo.Length > 1)
                {
                    this.CurStepInfo.Insert(0, "未处理人员（");
                    this.CurStepInfo.Length = this.CurStepInfo.Length - 1;
                    this.CurStepInfo.Append("）");
                }
                this.CurStepInfo.Append(stringBuilder);
            }
            this.tblName = this.curInstance.AppName;
            this.MainId = this.curInstance.AppId;
            if ((new _TableInfo(this.tblName)).GetModel().TableType == 3)
            {
                this.SaveBeforeSubmit = "false";
            }
       

            if (!base.IsPostBack)
            {
                this.UserDealInfo.InstanceId = this.curInstance.InstanceId;
                if (this.curInstance.Importance == "2")
                {
                    this.importance = "（重要）";
                }
                else if (this.curInstance.Importance == "3")
                {
                    this.importance = "（非常重要）";
                }
                List<DeptEmployee> deptEmployeeByEmployeeId = DeptEmployeeService.GetDeptEmployeeByEmployeeId(this.userTask_0.OwnerId);
                foreach (DeptEmployee deptEmployee in deptEmployeeByEmployeeId)
                {
                    Department model = DepartmentService.GetModel(deptEmployee.CompanyID);
                    string str = (model.DeptAbbr == "" ? model.DeptName : model.DeptAbbr);
                    string str1 = (deptEmployee.PositionName == "未知" ? "" : string.Concat("－", deptEmployee.PositionName));
                    ListItem listItem = new ListItem()
                    {
                        Text = string.Concat(str, "－", deptEmployee.DeptName, str1),
                        Value = deptEmployee._AutoID
                    };
                    ListItem listItem1 = listItem;
                    if (this.userTask_0.PositionId == deptEmployee.PositionId)
                    {
                        listItem1.Selected = true;
                    }
                    this.PositionList.Items.Add(listItem1);
                }
                if ((deptEmployeeByEmployeeId.Count <= 0 ? false : this.PositionList.SelectedValue == ""))
                {
                    this.PositionList.SelectedIndex = 0;
                }
                this.getTblHtml();
            }
            this.editScriptBlock = TableService.GetEditScriptBlock(this.tblName);
            if (this.editScriptBlock.Trim().Length > 0)
            {
                string paraValue = base.GetParaValue("ReplaceValue");
                if (paraValue != "")
                {
                    ModelBuilder modelBuilder = new ModelBuilder(this);
                    this.editScriptBlock = modelBuilder.ReplaceParaValue(paraValue, this.editScriptBlock);
                    this.editScriptBlock = modelBuilder.GetUbbCode(this.editScriptBlock, "[CRYPT]", "[/CRYPT]", base.UserName);
                }
                this.editScriptBlock = base.ReplaceContext(this.editScriptBlock);
            }
            
        }

        protected void PositionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = this.PositionList.SelectedValue;
            this.getTblHtml();
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string ReAuth(string string_0, string code)
        {
            string str = "0";
            if (string_0 == "1")
            {
                str = ( EIS.Permission.Utility.LoginChk(base.UserName, code) == LoginInfoType.Allowed ? "1" : "0");
            }
            else if (HttpContext.Current.Session["VerifyCode"] == null)
            {
                str = "0";
            }
            else
            {
                str = (code == HttpContext.Current.Session["VerifyCode"].ToString() ? "1" : "0");
            }
            return str;
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void saveData(string tblNameList, string xmldata)
        {
            this.fileLogger.Info("saveData保存开始");
            try
            {
                (new ModelSave(this)).SaveData(tblNameList, xmldata);
            }
            catch (Exception exception1)
            {
                if (exception1.Message != "") throw exception1;
                if ((exception1.Message == "") && (exception1.Data.Count > 0))
                {
                    string lastStackTrace = "";
                    foreach (var item in exception1.Data)
                    {
                        var entry = (DictionaryEntry)item;
                        lastStackTrace = exception1.Data[entry.Key].ToString();
                    }
                    AppInputAlertException alertExp = new AppInputAlertException(lastStackTrace);
                    throw alertExp;
                }
            }
            this.fileLogger.Info("saveData保存结束");
        }

        [AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
        public void SendSms()
        {
            string str = (new VryImgGen()).CreateNumCharCode(4);
            HttpContext.Current.Session["VerifyCode"] = str;
            StringBuilder stringBuilder = new StringBuilder();
            string mobilePhone = EmployeeService.GetMobilePhone(base.EmployeeID);
            if (mobilePhone.Trim() == "")
            {
                throw new Exception("系统中尚未登记您的手机号码");
            }
            stringBuilder.AppendFormat("您正在处理任务，校验码为：{1}，本消息来自【{0}】", AppSettings.GetWebName(AppSettings.Instance.WebId), str);
            RemoteMessage.SendSms(mobilePhone, stringBuilder.ToString());
        }
    }
}