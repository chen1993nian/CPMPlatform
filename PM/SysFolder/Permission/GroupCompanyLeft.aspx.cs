using AjaxPro;
using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.Permission
{
    public partial class GroupCompanyLeft : AdminPageBase
    {


        private _Catalog _Catalog_0 = new _Catalog();

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
                        @value = department._AutoID
                    };
                    treeItem_0.Add(treeItem);
                    this.method_0(treeItem);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefDeptLeft));
            Department topDept = DepartmentService.GetTopDept();
            this.list_0 = DepartmentService.GetAllCompanyList();
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