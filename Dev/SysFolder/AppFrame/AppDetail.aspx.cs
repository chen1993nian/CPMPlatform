using AjaxPro;
using EIS.AppBase;
using EIS.AppModel;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using WebBase.JZY.Tools;
using NLog;
using System;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace EIS.WebBase.SysFolder.AppFrame
{
    public partial class AppDetail : PageBase
    {
        public StringBuilder sbmodel = new StringBuilder();

        public XmlDocument xmlDoc = new XmlDocument();

        public string mainId = "";

        public string tblName = "";

        public string fileCount = "";

        public string tblHTML = "";

        public string fileListUrl = "";

        public string customScript = "";

        public string formWidth = "";

       

       

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
                string str = base.GetParaValue("condition").Replace("[QUOTES]", "'");
                if (string.IsNullOrEmpty(str) && base.GetParaValue("mainId") != "")
                {
                    this.mainId = base.GetParaValue("mainId");
                    str = string.Concat("_AutoId='", this.mainId, "'");
                }
                this.tblHTML = modelBuilder.GetDetailHtml(this.tblName, str);
                this.mainId = modelBuilder.MainId;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                this.fileLogger.Error<Exception>(exception);
                this.tblHTML = exception.Message;
                this.Session["_sysinfo"] = exception.Message;
                base.Server.Transfer("AppInfo.aspx", true);
            }
            return this.tblHTML;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.tblName = base.GetParaValue("tblName");

            AjaxPro.Utility.RegisterTypeForAjax(typeof(AppInput));
            
            this.customScript = base.GetCustomScript("ref_AppDetail");
            if (!base.IsPostBack)
            {
                _TableInfo __TableInfo = new _TableInfo(this.tblName);
                this.formWidth = __TableInfo.GetModel().FormWidthStyle;
                this.getTblHtml();
                if (base.GetParaValue("func") == "1")
                {
                    string paraValue = base.GetParaValue("appId");
                    WebTools.UpdateRead(base.EmployeeID, "T_E_WF_Query", paraValue);
                }
            }
        }
    }
}