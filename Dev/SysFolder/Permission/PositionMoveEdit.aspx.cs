using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.Permission
{
    public partial class PositionMoveEdit : AdminPageBase
    {
       
        public string deptId = "";

        public string deptName = "";

        public StringBuilder TipMessage = new StringBuilder();

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

    

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.EditEmployeeId))
            {
                DbConnection dbConnection = SysDatabase.CreateConnection();
                dbConnection.Open();
                DbTransaction dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        _Employee __Employee = new _Employee(dbTransaction);
                        _DeptEmployee __DeptEmployee = new _DeptEmployee(dbTransaction);
                        Employee model = EmployeeService.GetModel(this.EditEmployeeId);
                        DeptEmployee value = __DeptEmployee.GetModel(model.DeId);
                        if (value != null)
                        {
                            value.OrderID = (this.txtOrder.Text == "" ? 0 : Convert.ToInt32(this.txtOrder.Text));
                            value.PositionId = this.HiddenField1.Value;
                            value.PositionName = this.txtPositionName2.Text;
                            Department department = DepartmentService.GetModel(this.HiddenField2.Value.Trim());
                            value.DeptID = department._AutoID;
                            value.DeptName = department.DeptName;
                            value.CompanyID = department.CompanyID;
                            value.EmployeeName = model.EmployeeName;
                            __DeptEmployee.Update(value);
                        }
                        dbTransaction.Commit();
                        base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>$.noticeAdd({text:'保存成功！',stay:false});try { window.opener.app_query();} catch (e) {} window.close();</script>");
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        dbTransaction.Rollback();
                        this.TipMessage.AppendFormat("<div class=\"ErrorMsg\">{0}</div>", string.Concat("出现错误:保存失败！ 【新岗位名称，只能选有项】"));
                    }
                }
                finally
                {
                    if (dbConnection.State == ConnectionState.Open)
                    {
                        dbConnection.Close();
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(PositionMoveEdit));
            if (!base.IsPostBack)
            {
                this.EditEmployeeId = base.GetParaValue("employeeId");
                if (!string.IsNullOrEmpty(this.EditEmployeeId))
                {
                    Employee model = EmployeeService.GetModel(this.EditEmployeeId);
                    this.txtEmpName.Text = model.EmployeeName;
                    this.txtDeptName.Text = model.DeptName;
                    this.txtPositionName.Text = model.PositionName;
                }
            }
        }
    }
}