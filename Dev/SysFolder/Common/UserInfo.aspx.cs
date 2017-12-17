using EIS.AppBase;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.WebBase.SysFolder.Common
{
    public partial class UserInfo : PageBase
    {
        

        public string companyId = "";

        public string deptId = "";

        public string deptName = "";

        public string signPath = "";

        public string Birthday = "";

        public StringBuilder TipMessage = new StringBuilder();

        public StringBuilder sbPos = new StringBuilder();

        public Employee model = null;

        public string EditEmployeeId
        {
            get
            {
                return this.ViewState["EmployeeId"].ToString();
            }
            set
            {
                this.ViewState["EmployeeId"] = value;
            }
        }

      

        protected void Page_Load(object sender, EventArgs e)
        {
            this.EditEmployeeId = base.GetParaValue("empId");
            this.model = EmployeeService.GetModel(this.EditEmployeeId);
            if (this.model != null)
            {
                if (this.model.Birthday.HasValue)
                {
                    DateTime value = this.model.Birthday.Value;
                    this.Birthday = value.ToString("yyyy-MM-dd");
                }
                foreach (DeptEmployee deptEmployeeByEmployeeId in DeptEmployeeService.GetDeptEmployeeByEmployeeId(this.EditEmployeeId))
                {
                    string str = "";
                    string departmentName = DepartmentService.GetDepartmentName(deptEmployeeByEmployeeId.CompanyID);
                    if (departmentName != deptEmployeeByEmployeeId.DeptName)
                    {
                        string[] deptName = new string[] { departmentName, "－", deptEmployeeByEmployeeId.DeptName, "－", deptEmployeeByEmployeeId.PositionName };
                        str = string.Concat(deptName);
                    }
                    else
                    {
                        str = string.Concat(deptEmployeeByEmployeeId.DeptName, "－", deptEmployeeByEmployeeId.PositionName);
                    }
                    this.sbPos.AppendFormat("<li class='posItem'>{0}</li>", str);
                }
            }
            else
            {
                this.model = new Employee();
                this.TipMessage.Append("<div class='tip'>提示：找不到该员工的信息，请确认参数正确</div>");
            }
        }
    }
}