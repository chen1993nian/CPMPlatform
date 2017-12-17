using EIS.AppBase;
using EIS.WebBase.SysFolder.WorkFlow.UserControl;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using NLog;
using System;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace EIS.WebBase.SysFolder.WorkFlow.Admin
{
    public partial class Admin_Maintain: PageBase
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

        public Instance curInstance = new Instance();

        protected HtmlHead Head1;

        protected HtmlForm form1;

        protected InstanceImg FlowImg;

        protected InstanceLog FlowLog;

        protected EIS.WebBase.SysFolder.WorkFlow.UserControl.UserDealInfo UserDealInfo;

        public Admin_Maintain()
        {
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            #region
            this.tblName = base.GetParaValue("tblName");
            this.InstanceId = base.GetParaValue("InstanceId");
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
            if (!base.IsPostBack)
            {
                this.UserDealInfo.InstanceId = this.InstanceId;
                this.FlowLog.InstanceId = this.InstanceId;
                this.FlowImg.InstanceId = this.InstanceId;
            }
            #endregion

        }
    }
}