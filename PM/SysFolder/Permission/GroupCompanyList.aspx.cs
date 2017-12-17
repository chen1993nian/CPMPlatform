using AjaxPro;
using EIS.AppBase;
using EIS.Permission.Service;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.Permission
{
    public partial class GroupCompanyList : AdminPageBase
    {
        public string DeptPWBS = "";

       

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(GroupCompanyList));
            this.DeptPWBS = base.GetParaValue("DeptPWBS");
        }
        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void RemoveDept(string deptId)
        {
            DepartmentService.RemoveDepartment(deptId);
        }
        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void DelRecord(string DeptName, string deptId)
        {
            DepartmentService.RemoveDepartment(deptId);
        }
    }
}