using AjaxPro;
using EIS.AppBase;
using EIS.Permission.Access;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Studio.SysFolder.Permission
{
    public partial class GroupPartTimeList : AdminPageBase
    {
       

        public string DeptPWBS = "";

        public string CompanyId = "";

      

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void DelRecord(string recId)
        {
            (new _DeptEmployee()).Remove(recId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(GroupPartTimeList));
        }
    }
}