using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.AppModel;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using EIS.WebBase.SysFolder.WorkFlow.UserControl;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using System;
using System.Text;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace EIS.WebBase.SysFolder.AppFrame
{
    public partial class AppWorkFlowInfo2 : PageBase
    {
        public StringBuilder sbmodel = new StringBuilder();

        public XmlDocument xmlDoc = new XmlDocument();

        public string mainId = "";

        public string tblName = "";

        public string fileCount = "";

        public string tblHTML = "";

        public string fileListUrl = "";

        public string workflowName = "";

        public string InstanceId = "";

        public string customScript = "";

        public string formWidth = "";

        public Instance curInstance = new Instance();

        public AppWorkFlowInfo2()
        {
            this.AutoRedirect = false;
        }

        public string getTblHtml()
        {
            XmlDeclaration xmlDeclaration = this.xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            this.xmlDoc.AppendChild(xmlDeclaration);
            try
            {
                ModelBuilder modelBuilder = new ModelBuilder(this)
                {
                    Sindex = base.GetParaValue("sindex"),
                    DataControl = null,
                    DataContolFirst = false,
                    ReplaceValue = base.GetParaValue("replacevalue")
                };
                this.tblHTML = modelBuilder.GetDetailHtml(this.tblName, string.Concat("_autoid='", this.mainId, "'"));
                FileService fileService = new FileService();
                this.fileCount = fileService.GetFilesCount(this.tblName, this.mainId);
                this.fileListUrl = base.CryptPara(string.Concat("appName=", this.tblName, "&appId=", this.mainId));
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
                this.tblName = this.curInstance.AppName;
                this.mainId = this.curInstance.AppId;
            }
            else
            {
                this.mainId = base.GetParaValue("mainId");
                if (string.IsNullOrEmpty(this.mainId))
                {
                    throw new Exception("参数错误");
                }
                this.curInstance = InstanceService.GetInstanceByAppInfo(this.tblName, this.mainId);
                this.InstanceId = this.curInstance.InstanceId;
                this.tblName = this.curInstance.AppName;
            }
            string itemValue = SysConfig.GetConfig("Code_QueryURI").ItemValue;
            this.Session["QRCode"] = string.Concat(itemValue, "?t=1&k=", this.InstanceId);
            this.workflowName = DefineService.GetWorkflowName(this.curInstance.WorkflowId);
            if (!base.IsPostBack)
            {
                _TableInfo __TableInfo = new _TableInfo(this.tblName);
                this.formWidth = __TableInfo.GetModel().FormWidthStyle;
                this.UserDealInfo.InstanceId = this.InstanceId;
                this.getTblHtml();
            }
        }
    }
}