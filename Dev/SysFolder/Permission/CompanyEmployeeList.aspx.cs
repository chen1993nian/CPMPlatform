using AjaxPro;
using EIS.AppBase;
using EIS.Permission.Service;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Studio.SysFolder.Permission
{
    public partial class CompanyEmployeeList : AdminPageBase
    {
       

        public string DeptID = "";

        public string Condition = "";

    

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void DelRecord(string employeeId)
        {
            if (!EmployeeService.DeleteEmployee(employeeId))
            {
                throw new Exception("在删除员工过程中出错错误，删除失败");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(CompanyEmployeeList));
            this.DeptID = base.GetParaValue("DeptID");
            this.Condition = base.GetParaValue("Condition");
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void RemoveEmployee(string employeeId)
        {
            if (!EmployeeService.RemoveEmployee(employeeId))
            {
                throw new Exception("在删除员工过程中出错错误，删除失败");
            }
        }
    }
}