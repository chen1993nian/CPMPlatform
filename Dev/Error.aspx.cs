using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Studio
{
    public partial class Error : System.Web.UI.Page
    {
        public string ErrorMsg = "系统出现未知错误！";

        public string DetailMsg = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.Server.GetLastError() != null)
            {
                this.ErrorMsg = string.Concat("系统出现错误：", base.Server.GetLastError().Message);
                if (base.Server.GetLastError().InnerException != null)
                {
                    EIS.Studio.Error error = this;
                    error.ErrorMsg = string.Concat(error.ErrorMsg, base.Server.GetLastError().InnerException.Message);
                }
                this.DetailMsg = base.Server.GetLastError().ToString();
            }
        }
    }
}