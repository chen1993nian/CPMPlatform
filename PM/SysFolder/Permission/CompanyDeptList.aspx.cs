using AjaxPro;
using EIS.AppBase;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Web.UI.HtmlControls;


namespace EIS.Web.SysFolder.Permission
{
    public partial class CompanyDeptList : AdminPageBase
    {
       
        public string DeptPWBS = "";

        public string CompanyId = "";

        
        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string GetNewDeptWbs(string deptPWBS)
        {
            return DepartmentService.GetNewDeptWbs(deptPWBS);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(CompanyDeptList));
            this.DeptPWBS = base.GetParaValue("DeptPWBS");
            this.CompanyId = DepartmentService.GetModelByWbs(this.DeptPWBS).CompanyID;
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void RemoveDept(string deptId)
        {
            DepartmentService.RemoveDepartment(deptId);
        }
    }
}