using EIS.AppBase;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.UI;

namespace EIS.Web.SysFolder.Common
{
    public partial class PositionTree : PageBase
    {
        private List<Department> list_0;

        public string treedata = "";

        private StringBuilder stringBuilder_0 = new StringBuilder();

        public string selmethod = "";

        public string cid = "";

        public string queryfield = "";

      

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

        private void method_1()
        {
            base.Load += new EventHandler(this.PositionTree_Load);
        }

        protected override void OnInit(EventArgs eventArgs_0)
        {
            this.method_1();
            base.OnInit(eventArgs_0);
        }

        private void PositionTree_Load(object sender, EventArgs e)
        {
            this.selmethod = base.GetParaValue("method");
            this.cid = base.GetParaValue("cid");
            this.queryfield = base.GetParaValue("queryfield");
            Department topDept = DepartmentService.GetTopDept();
            this.list_0 = (new _Department()).GetSubDeptByWbs(topDept.DeptWBS);
            TreeItem treeItem = new TreeItem()
            {
                id = topDept.DeptWBS,
                text = topDept.DeptName,
                @value = topDept._AutoID,
                isexpand = true
            };
            this.method_0(treeItem);
            foreach (Position positionByDeptId in PositionService.GetPositionByDeptId(topDept._AutoID))
            {
                TreeItem treeItem1 = new TreeItem()
                {
                    text = positionByDeptId.PositionName,
                    id = positionByDeptId._AutoID,
                    @value = positionByDeptId.DeptID
                };
                treeItem.Add(treeItem1);
            }
            this.treedata = treeItem.ToJsonString();
        }
    }
}