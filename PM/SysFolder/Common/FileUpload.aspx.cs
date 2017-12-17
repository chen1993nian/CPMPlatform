using EIS.AppBase;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.Common
{
    public partial class FileUpload : PageBase
    {
       

        private string string_0 = "";

        private string string_1 = "";

        public string AppId
        {
            get
            {
                return this.string_0;
            }
        }

        public string AppName
        {
            get
            {
                return this.string_1;
            }
        }

        public string folderId
        {
            get
            {
                return base.GetParaValue("folderId");
            }
        }

        public FileUpload()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.string_0 = base.GetParaValue("appId");
            this.string_1 = base.GetParaValue("appName");
        }
    }
}