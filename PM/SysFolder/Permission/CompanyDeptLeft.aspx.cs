using AjaxPro;
using EIS.AppBase;
using EIS.Permission.Access;
using EIS.Permission.Model;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.Permission
{
    public partial class CompanyDeptLeft : AdminPageBase
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
                        @value = department._AutoID
                    };
                    treeItem_0.Add(treeItem);
                    this.method_0(treeItem);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = base.Session["CompanyId"].ToString();
            AjaxPro.Utility.RegisterTypeForAjax(typeof(CompanyDeptLeft));
            _Department __Department = new _Department();
            Department model = __Department.GetModel(str);
            this.list_0 = __Department.GetSubDeptByWbs(model.DeptWBS);
            TreeItem treeItem = new TreeItem()
            {
                id = model.DeptWBS,
                text = model.DeptName,
                @value = model._AutoID,
                isexpand = true
            };
            this.method_0(treeItem);
            this.treedata = treeItem.ToJsonString();
        }
    }
}