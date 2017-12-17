using AjaxPro;
using EIS.AppBase;
using EIS.Permission;
using EIS.Permission.Model;
using EIS.Permission.Service;
using EIS.WorkFlow.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;

namespace Studio.JZY.SysFolder.WorkFlow
{
    public partial class FlowMyPart : PageBase
    {
        public string limit = "";

        public string addLimit = "0";

        public string editLimit = "0";

        public string delLimit = "0";

        public string condition = "";

        public string QueryEmpId = "";

        public string funId = "";

        public string bakUrl = "";

        public StringBuilder sbPos = new StringBuilder();



        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void FetchTask(string instanceId)
        {
            EIS.WorkFlow.Engine.Utility.FetchTask(instanceId, base.EmployeeID);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(FlowMyPart));
            this.condition = base.GetParaValue("condition");
            this.QueryEmpId = base.GetParaValue("empId");
            if (this.QueryEmpId == "")
            {
                this.QueryEmpId = base.EmployeeID;
            }
            foreach (DeptEmployee deptEmployeeByEmployeeId in DeptEmployeeService.GetDeptEmployeeByEmployeeId(base.EmployeeID))
            {
                Department model = DepartmentService.GetModel(deptEmployeeByEmployeeId.CompanyID);
                this.sbPos.AppendFormat("{0}|{1},", string.Concat(model.DeptAbbr, "－", deptEmployeeByEmployeeId.PositionName), deptEmployeeByEmployeeId.PositionId);
            }
            this.funId = base.GetParaValue("funId");
            if (this.funId.Length > 0)
            {
                this.bakUrl = EIS.Permission.Utility.GetFunAttrById(this.funId, "FunUrl");
            }
        }
    }
}