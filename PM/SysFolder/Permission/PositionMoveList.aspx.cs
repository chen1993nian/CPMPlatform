using AjaxPro;
using EIS.AppBase;
using EIS.Permission.Service;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.Permission
{
    public partial class PositionMoveList : AdminPageBase
    {
       

        public string DeptID = "";

       

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
            AjaxPro.Utility.RegisterTypeForAjax(typeof(PositionMoveList));
        }
    }
}