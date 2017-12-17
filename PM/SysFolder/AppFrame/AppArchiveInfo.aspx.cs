using EIS.AppBase;
using EIS.AppModel;
using EIS.Web.ModelLib.Access;
using EIS.Web.ModelLib.Model;
using EIS.Web.SysFolder.WorkFlow.UserControl;
using System;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace EIS.Web.SysFolder.AppFrame
{
    public partial class AppArchiveInfo : PageBase
    {
      
       

        public StringBuilder sbmodel = new StringBuilder();

        public string fileCount = "";

        public string tblHTML = "";

        public string daHTML = "";

        public string fileListUrl = "";

        public string InstanceId = "";

        public DaArchive archiveModel = null;

        public AppArchiveInfo()
        {
            this.AutoRedirect = false;
        }

        public string getTblHtml(string appName, string appId)
        {
            string detailHtml = "";
            XmlDocument xmlDocument = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDocument.AppendChild(xmlDeclaration);
            try
            {
                ModelBuilder modelBuilder = new ModelBuilder(this)
                {
                    Sindex = base.GetParaValue("sindex"),
                    DataControl = null,
                    DataContolFirst = false,
                    ReplaceValue = base.GetParaValue("replacevalue")
                };
                detailHtml = modelBuilder.GetDetailHtml(appName, string.Concat("_autoid='", appId, "'"));
            }
            catch (Exception exception)
            {
                detailHtml = exception.Message;
            }
            return detailHtml;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string paraValue = "";
            string appId = "";
            string appName = "";
            paraValue = base.GetParaValue("daId");
            this.InstanceId = base.GetParaValue("InstanceId");
            appId = base.GetParaValue("appId");
            appName = base.GetParaValue("appName");
            _DaArchive __DaArchive = new _DaArchive();
            if (!string.IsNullOrEmpty(paraValue))
            {
                this.archiveModel = __DaArchive.GetModel(paraValue);
            }
            else if (this.InstanceId != "")
            {
                this.archiveModel = __DaArchive.GetModelByInstanceId(this.InstanceId);
            }
            else if ((appId == "" ? false : appName != ""))
            {
                this.archiveModel = __DaArchive.GetModelByAppInfo(appName, appId);
            }
            if (this.archiveModel == null)
            {
                this.Session["_sysinfo"] = "参数信息错误";
                base.Server.Transfer("AppInfo.aspx?msgType=error", true);
            }
            if (this.archiveModel.InstanceId.Length > 0)
            {
                this.UserDealInfo.InstanceId = this.archiveModel.InstanceId;
            }
            appId = this.archiveModel.AppId;
            appName = this.archiveModel.AppName;
            this.daHTML = this.getTblHtml("T_OA_DA_ArchiveInfo", this.archiveModel._AutoID);
            this.tblHTML = this.getTblHtml(appName, appId);
        }
    }
}