using EIS.AppBase;
using EIS.AppCommon;
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

namespace EIS.Web.SysFolder.Permission
{
    public partial class DefEmployeeLeft : AdminPageBase
    {
        private _Catalog _Catalog_0 = new _Catalog();

        private List<Department> list_0;

        public string treedata = "";

    
        private void method_0(zTreeNode zTreeNode_0)
        {
          
            List<Department> departments = this.list_0.FindAll((Department department_0) => department_0.DeptPWBS == zTreeNode_0.id);
            if (departments.Count > 0)
            {
                foreach (Department department in departments)
                {
                    zTreeNode _zTreeNode = new zTreeNode()
                    {
                        name = department.DeptName,
                        id = department.DeptWBS,
                        @value = department._AutoID
                    };
                    zTreeNode_0.Add(_zTreeNode);
                    this.method_0(_zTreeNode);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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
            zTreeNode _zTreeNode = new zTreeNode()
            {
                id = topDept.DeptWBS,
                name = topDept.DeptName,
                @value = topDept._AutoID,
                open = true
            };
            this.method_0(_zTreeNode);
            this.treedata = _zTreeNode.ToJsonString(true);
        }
    }
}