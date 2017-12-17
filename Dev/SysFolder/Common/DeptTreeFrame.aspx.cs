using EIS.AppBase;
using EIS.AppCommon;
using EIS.DataAccess;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Caching;

namespace EIS.WebBase.SysFolder.Common
{
    public partial class DeptTreeFrame : PageBase
    {
        public string deptTree = "";

        private List<Department> list_0;

    

        private void method_0()
        {
            Department topDept = DepartmentService.GetTopDept();
            this.list_0 = (new _Department()).GetSubDeptByWbs(topDept.DeptWBS);
            zTreeNode _zTreeNode = new zTreeNode()
            {
                id = topDept.DeptWBS,
                name = topDept.DeptName,
                icon = "../../img/common/home.png",
                @value = "",
                open = true
            };
            this.method_1(_zTreeNode);
            this.deptTree = _zTreeNode.ToJsonString(true);
        }

        private void method_1(zTreeNode zTreeNode_0)
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
                        @value = string.Concat(department._AutoID, "|", department.DeptCode)
                    };
                    zTreeNode_0.Add(_zTreeNode);
                    this.method_1(_zTreeNode);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime now;
            string str = "DeptTree";
            if (HttpContext.Current.Cache[str] == null)
            {
                this.method_0();
                HttpContext.Current.Cache[str] = this.deptTree;
                System.Web.Caching.Cache cache = HttpContext.Current.Cache;
                string str1 = string.Concat(str, "_time");
                now = DateTime.Now;
                cache[str1] = now.ToString();
            }
            else if (SysDatabase.ExecuteScalar(string.Concat("select count(*) from T_E_Org_Department where _UpdateTime>'", HttpContext.Current.Cache[string.Concat(str, "_time")].ToString(), "'")).ToString() != "0")
            {
                this.method_0();
                HttpContext.Current.Cache[str] = this.deptTree;
                System.Web.Caching.Cache caches = HttpContext.Current.Cache;
                string str2 = string.Concat(str, "_time");
                now = DateTime.Now;
                caches[str2] = now.ToString();
            }
            else
            {
                this.deptTree = HttpContext.Current.Cache[str].ToString();
            }
        }
    }
}