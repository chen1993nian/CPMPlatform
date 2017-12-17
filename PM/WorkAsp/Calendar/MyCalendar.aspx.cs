using EIS.AppBase;
using System;
using System.Web;
using System.Web.UI;

namespace EIS.Web.WorkAsp.Calendar
{
    public  partial class MyCalendar : PageBase
    {
        public string employeeId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.employeeId = (string.IsNullOrEmpty(base.Request["employeeId"]) ? base.EmployeeID : base.Request["employeeId"]);
        }
    }
}