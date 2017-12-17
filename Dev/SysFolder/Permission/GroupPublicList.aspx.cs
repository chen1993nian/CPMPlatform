using AjaxPro;
using EIS.AppBase;
using System;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using System.Text;
using EIS.DataAccess;
namespace Studio.JZY.SysFolder.Permission
{
    public partial class GroupPublicList : AdminPageBase
    {
        public string DeptPWBS = "";

        public string CompanyId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(GroupPublicList));
            this.CompanyId = base.Session["CompanyId"].ToString();
        }
          [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void DelRecord(string tablename, string strid)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" delete from {0} where _AutoID='{1}'",tablename,strid);

            SysDatabase.ExecuteNonQuery(sb.ToString());

        }

    }
}