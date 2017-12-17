using EIS.AppBase;
using EIS.AppModel;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.Web.SysFolder.WorkFlow.UserControl;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace EIS.Web.SysFolder.AppFrame
{
    public partial class AppWorkFlowInfo : PageBase
    {
       

        public StringBuilder sbmodel = new StringBuilder();

        public XmlDocument xmlDoc = new XmlDocument();

        public string mainId = "";

        public string tblName = "";

        public string fileCount = "";

        public string tblHTML = "";

        public string fileListUrl = "";

        public string workflowName = "";

        public string workflowVer = "";

        public string workflowCode = "";

        public string InstanceId = "";

        public string customScript = "";

        public string formWidth = "";

        public Instance curInstance = new Instance();

        public AppWorkFlowInfo()
        {
            this.AutoRedirect = false;
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

        public string getTblHtml()
        {
            XmlDeclaration xmlDeclaration = this.xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            this.xmlDoc.AppendChild(xmlDeclaration);
            try
            {
                ModelBuilder modelBuilder = new ModelBuilder(this)
                {
                    Sindex = base.GetParaValue("sindex")
                };
                if (this.curInstance != null)
                {
                    string printStyleById = DefineService.GetPrintStyleById(this.curInstance.WorkflowId);
                    if (printStyleById != "")
                    {
                        modelBuilder.Sindex = printStyleById;
                    }
                }
                modelBuilder.DataControl = null;
                modelBuilder.DataContolFirst = false;
                modelBuilder.ReplaceValue = base.GetParaValue("replacevalue");
                this.tblHTML = modelBuilder.GetDetailHtml(this.tblName, string.Concat("_autoid='", this.mainId, "'"));
            }
            catch (Exception exception)
            {
                this.tblHTML = exception.Message;
            }
            return this.tblHTML;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.tblName = base.GetParaValue("tblName");
            this.InstanceId = base.GetParaValue("InstanceId");
            this.customScript = base.GetCustomScript("ref_AppDetail");
            if (!string.IsNullOrEmpty(this.InstanceId))
            {
                this.curInstance = InstanceService.GetInstanceById(this.InstanceId);
                if (this.curInstance == null)
                {
                    this.Session["_sysinfo"] = "系统找不到流程信息";
                    base.Server.Transfer("AppInfo.aspx?msgType=error", true);
                }
                else
                {
                    this.tblName = this.curInstance.AppName;
                    this.mainId = this.curInstance.AppId;
                }
            }
            else
            {
                this.mainId = base.GetParaValue("mainId");
                if (!string.IsNullOrEmpty(this.mainId))
                {
                    this.fileLogger.Debug<string, string>("tblName={0}，mainId={1}", this.tblName, this.mainId);
                    this.curInstance = InstanceService.GetInstanceByAppInfo(this.tblName, this.mainId);
                    this.InstanceId = this.curInstance.InstanceId;
                    this.tblName = this.curInstance.AppName;
                }
                else
                {
                    this.Session["_sysinfo"] = "参数信息错误";
                    base.Server.Transfer("AppInfo.aspx?msgType=error", true);
                }
            }
            Define workflowBasicModelById = DefineService.GetWorkflowBasicModelById(this.curInstance.WorkflowId);
            this.workflowName = workflowBasicModelById.WorkflowName;
            this.workflowCode = workflowBasicModelById.WorkflowCode;
            this.workflowVer = workflowBasicModelById.Version;
            _TableInfo __TableInfo = new _TableInfo(this.tblName);
            this.formWidth = __TableInfo.GetModel().FormWidthStyle;
            if (!base.IsPostBack)
            {
                this.UserDealInfo.InstanceId = this.InstanceId;
                this.FlowLog.InstanceId = this.InstanceId;
                this.FlowImg.InstanceId = this.InstanceId;
                this.getTblHtml();
            }
        }
    }
}