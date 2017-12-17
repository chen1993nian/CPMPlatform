using EIS.AppBase;
using EIS.Permission.Service;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.WorkAsp.Calendar
{
    public partial class ViewRight : PageBase
    {
        public string employeeId = "";

        public string read = "";

        public string view = "";

        public string personName = "";

  

        protected void Page_Load(object sender, EventArgs e)
        {
            this.employeeId = base.GetParaValue("employeeId");
            this.read = base.GetParaValue("read");
            this.view = base.GetParaValue("view");
            this.personName = EmployeeService.GetEmployeeName(this.employeeId);
        }
    }
}