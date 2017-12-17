using AjaxPro;
using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
namespace EIS.Studio.SysFolder.Permission
{
    public partial class DefPositionLimitLeft : AdminPageBase
    {
        private _Catalog _Catalog_0 = new _Catalog();

        private List<Department> list_0;

        public string treedata = "";

 

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string GetNewDeptWbs(string deptPWBS)
        {
            return DepartmentService.GetNewDeptWbs(deptPWBS);
        }

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
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefPositionLimitLeft));
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
            this.method_0(treeItem);
            this.treedata = treeItem.ToJsonString();
        }
    }
}