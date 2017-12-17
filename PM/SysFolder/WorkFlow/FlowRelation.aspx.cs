using EIS.AppBase;
using EIS.Permission;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.WorkFlow
{
    public partial class FlowRelation : PageBase
    {
        public string deptClass = "hidden";

        public string companyClass = "hidden";

        protected void Page_Load(object sender, EventArgs e)
        {
            string funLimitByFunCode = EIS.Permission.Utility.GetFunLimitByFunCode(base.EmployeeID, "WF_DeptStart");
            string str = EIS.Permission.Utility.GetFunLimitByFunCode(base.EmployeeID, "WF_CompanyStart");
            if (funLimitByFunCode.StartsWith("1"))
            {
                this.deptClass = "";
            }
            if (str.StartsWith("1"))
            {
                this.companyClass = "";
            }
        }
    }
}