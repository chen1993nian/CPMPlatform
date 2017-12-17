using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Web.UI.HtmlControls;

namespace EIS.Studio.SysFolder.Permission
{
    public partial class DefRoleLimitLeft : AdminPageBase
    {
      

        public string treedata = "";

        private DataTable dataTable_0;

    

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string GetNewWbs(string catPWBS)
        {
            return RoleCatalogService.GetNewWbs(catPWBS);
        }

        private void method_0(TreeItem treeItem_0)
        {
            TreeItem treeItem;
            //List<Department> departments = this.list_0.FindAll((Department department_0) => department_0.DeptPWBS == treeItem_0.id);
            //foreach (Department department in departments)
            //{
            //    treeItem = new TreeItem()
            //    {
            //        id = department.DeptWBS,
            //        text = department.DeptName,
            //        @value = department._AutoID,
            //        complete = true,
            //        hasChildren = true
            //    };
            //    treeItem_0.Add(treeItem);
            //    this.method_0(treeItem);
            //}
            //DataRow[] dataRowArray = this.dataTable_0.Select(string.Concat("CompanyId='", treeItem_0.@value, "'"), "orderId");
            //if (treeItem_0.id == "0")
            //{
            //    dataRowArray = this.dataTable_0.Select("Isnull(CompanyId,'')=''", "orderId");
            //}
            foreach(DataRow  dataRow in this.dataTable_0.Rows)
            {
                treeItem = new TreeItem()
                {
                    id = dataRow["_AutoID"].ToString(),
                    text = dataRow["RoleName"].ToString(),
                    @value = dataRow["_AutoID"].ToString(),
                    complete = true,
                    hasChildren = false
                };
                treeItem_0.Add(treeItem);
            }
            //DataRow[] dataRowArray1 = dataRowArray;
            //for (int i = 0; i < (int)dataRowArray1.Length; i++)
            //{
            //    DataRow dataRow = dataRowArray1[i];
            //    treeItem = new TreeItem()
            //    {
            //        id = dataRow["_AutoID"].ToString(),
            //        text = dataRow["RoleName"].ToString(),
            //        @value = dataRow["_AutoID"].ToString(),
            //        complete = true,
            //        hasChildren = false
            //    };
            //    treeItem_0.Add(treeItem);
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefRoleLimitLeft));
            Department topDept = DepartmentService.GetTopDept();
         //   this.list_0 = DepartmentService.GetAllCompanyList();
            string str = string.Format("select _AutoID,RoleName,CompanyId,orderId \r\n                    from T_E_Org_Role r", new object[0]);
            this.dataTable_0 = SysDatabase.ExecuteTable(str);
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