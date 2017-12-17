using EIS.AppBase;
using System;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web.Mail
{
    public partial class MailAfterSend : PageBase
    {
       

        public string msgInfo = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["_sysinfo"] != null)
            {
                this.msgInfo = this.Session["_sysinfo"].ToString();
            }
        }
    }
}