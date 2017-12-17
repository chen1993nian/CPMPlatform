using EIS.AppBase;
using EIS.AppMail.DAL;
using System;
using System.Collections;
using System.Data;
using System.Text;

namespace EIS.Web.Mail
{
    public partial class MailFrame : PageBase
    {
        public StringBuilder sbFolder = new StringBuilder();

     

        protected void Page_Load(object sender, EventArgs e)
        {
            _MailFolder __MailFolder = new _MailFolder();
            DataTable list = __MailFolder.GetList(string.Concat("Owner='", base.EmployeeID, "'"));
            foreach (DataRow row in list.Rows)
            {
                this.sbFolder.AppendFormat("<li class='menu subfolder' url='MailReceive.aspx?folderId={0}' folderid=\"{0}\" >{1}</li>", row["_autoId"], row["FolderName"]);
            }
        }
    }
}