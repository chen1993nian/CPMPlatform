using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.UI;

namespace Studio.JZY.SysFolder.Permission 
{
    public partial class GroupEmployeeRoleSet : AdminPageBase
    {
        public string treedata = "";

        private StringBuilder stringBuilder_0 = new StringBuilder();

        public string roleId = "";

        public string roleName = "";

        private DataTable dataTable_0;

        private List<Department> list_0;

     

        private void GroupEmployeeRoleSet_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(GroupEmployeeRoleSet));
            this.roleId = base.GetParaValue("empId");
            this.roleName = EmployeeService.GetEmployeeName(this.roleId);
            Department topDept = DepartmentService.GetTopDept();
            this.list_0 = DepartmentService.GetAllCompanyList();
            string str = string.Format("select _AutoID,RoleName,CompanyId,orderId,\r\n                    (select COUNT(*) from t_e_org_roleEmployee re where re.EmployeeID='{0}' and re.RoleID=r._AutoId) checked\r\n                    from T_E_Org_Role r", this.roleId);
            this.dataTable_0 = SysDatabase.ExecuteTable(str);
            TreeItem treeItem = new TreeItem()
            {
                id = topDept.DeptWBS,
                text = topDept.DeptName,
                @value = topDept._AutoID,
                isexpand = true
            };
            if (base.LoginType == "0")
            {
                TreeItem treeItem1 = new TreeItem()
                {
                    id = "allright",
                    text = "所有权限角色",
                    @value = "allright",
                    complete = true,
                    hasChildren = false,
                    checkstate = (RoleEmployeeService.IsEmployeeHasAllRight(this.roleId) ? 1 : 0)
                };
                treeItem.Add(treeItem1);
            }
            this.method_0(treeItem);
            this.treedata = treeItem.ToJsonString();
        }

        private void method_0(TreeItem treeItem_0)
        {
            TreeItem treeItem;
            //List<Department> departments = this.list_0.FindAll((Department department_0) => department_0.DeptPWBS == treeItem_0.id);
            //foreach (Department department in departments)
            //{
            //    treeItem = new TreeItem()
            //    {
            //        id = department.DeptWBS
            //    };
            //    int length = (int)this.dataTable_0.Select(string.Concat("checked>0 and CompanyId='", treeItem_0.@value, "'")).Length;
            //    if (length <= 0)
            //    {
            //        treeItem.checkstate = 0;
            //        treeItem.text = department.DeptName;
            //    }
            //    else
            //    {
            //        treeItem.checkstate = 2;
            //        treeItem.text = string.Concat(department.DeptName, "（", length.ToString(), "）");
            //    }
            //    treeItem.@value = department._AutoID;
            //    treeItem.complete = true;
            //    treeItem.hasChildren = true;
            //    treeItem_0.Add(treeItem);
            //    this.method_0(treeItem);
            //}

            foreach(DataRow dataRow in  this.dataTable_0.Rows)
            {
               // DataRow dataRow = dataRowArray1[i];
                treeItem = new TreeItem()
                {
                    id = dataRow["_AutoID"].ToString(),
                    text = dataRow["RoleName"].ToString(),
                    @value = dataRow["_AutoID"].ToString(),
                    complete = true,
                    hasChildren = false
                };
                if (dataRow["checked"].ToString() == "0")
                {
                    treeItem.checkstate = 0;
                }
                else
                {
                    treeItem.checkstate = 1;
                }
                treeItem_0.Add(treeItem);
            }


            //DataRow[] dataRowArray = this.dataTable_0.Select(string.Concat("CompanyId='", treeItem_0.@value, "'"), "orderId");
            //if (treeItem_0.id == "0")
            //{
            //    dataRowArray = this.dataTable_0.Select("Isnull(CompanyId,'')=''", "orderId");
            //}
            //DataRow[] dataRowArray1 = dataRowArray;
            //for (int i = 0; i < (int)dataRowArray1.Length; i++)
            //{
              
            //}
        }

        private void method_1()
        {
            base.Load += new EventHandler(this.GroupEmployeeRoleSet_Load);
        }

        protected override void OnInit(EventArgs eventArgs_0)
        {
            this.method_1();
            base.OnInit(eventArgs_0);
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void SaveRoleSet(string roleList, string empId)
        {
            char[] chrArray = new char[] { ',' };
            RoleEmployeeService.SaveEmployeeRoleSet(roleList.Split(chrArray), empId);
        }
    }
}