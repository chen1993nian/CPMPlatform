using AjaxPro;
using EIS.AppBase;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.Permission
{
    public partial class DefEmployeeLimitLeft : AdminPageBase
    {
        private List<Department> list_0;

        public string treedata = "";
        private void method_0(TreeItem treeItem_0)
        {
         
            List<Department> departments = this.list_0.FindAll((Department department_0) => department_0.DeptPWBS == treeItem_0.id);
            if (departments.Count > 0)
            {
                foreach (Department department in departments)
                {
                    TreeItem treeItem = new TreeItem()
                    {
                        text = department.DeptName,
                        id = department.DeptWBS,
                        @value = department._AutoID,
                        complete = false,
                        hasChildren = true
                    };
                    treeItem_0.Add(treeItem);
                    this.method_0(treeItem);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefEmployeeLimitLeft));
            Department topDept = DepartmentService.GetTopDept();
            if (base.LoginType == "2")
            {
                string str = this.Session["CompanyId"].ToString();
                if (str != "")
                {
                    topDept = DepartmentService.GetModel(str);
                }
            }
            this.list_0 = (new _Department()).GetSubDeptByWbs(topDept.DeptWBS);
            TreeItem treeItem = new TreeItem()
            {
                id = topDept.DeptWBS,
                text = topDept.DeptName,
                @value = topDept._AutoID,
                isexpand = true
            };
            TreeItem treeItem1 = new TreeItem();
            if (base.LoginType != "2")
            {
                treeItem1.id = "everyone";
                treeItem1.text = "全体员工";
                treeItem1.@value = "everyone";
                treeItem1.isexpand = true;
                treeItem.Add(treeItem1);
            }
            this.method_0(treeItem);
            this.treedata = treeItem.ToJsonString();
        }
    }
}