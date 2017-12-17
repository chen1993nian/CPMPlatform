using AjaxPro;
using EIS.AppBase;
using EIS.Permission.Service;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.Permission
{
    public partial class DefDeptList : AdminPageBase
    {
     

        public string DeptPWBS = "";

       

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string GetNewDeptWbs(string deptPWBS)
        {
            return DepartmentService.GetNewDeptWbs(deptPWBS);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefDeptList));
            this.DeptPWBS = base.GetParaValue("DeptPWBS");
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void RemoveDept(string deptId)
        {
            DepartmentService.RemoveDepartment(deptId);
        }
    }
}