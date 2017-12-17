using EIS.AppBase;
using EIS.DataModel.Service;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.WorkAsp.Calendar
{
    public partial class LeaderCalendar : PageBase
    {
        public string employeeId = "";

        public string read = "false";

      
       

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.employeeId = this.DropDownList1.SelectedValue;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.employeeId = (string.IsNullOrEmpty(base.Request["employeeId"]) ? base.EmployeeID : base.Request["employeeId"]);
            if (!base.IsPostBack)
            {
                StringCollection myLeader2 = EmployeeRelationService.GetMyLeader2(base.EmployeeID);
                foreach (string str in myLeader2)
                {
                    string[] strArrays = str.Split(new char[] { '|' });
                    this.DropDownList1.Items.Add(new ListItem(strArrays[1], strArrays[0]));
                }
                if (myLeader2.Count <= 0)
                {
                    this.employeeId = Guid.NewGuid().ToString();
                    this.read = "true";
                }
                else
                {
                    this.employeeId = this.DropDownList1.Items[0].Value;
                }
            }
        }
    }
}