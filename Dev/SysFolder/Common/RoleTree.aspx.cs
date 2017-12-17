using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;

namespace EIS.WebBase.SysFolder.Common
{
    public partial class RoleTree : PageBase
    {
        private List<Department> list_0;

        public string treedata = "";

        private StringBuilder stringBuilder_0 = new StringBuilder();

        public string selmethod = "";

        public string cid = "";

        public string queryfield = "";

       
        private void method_0()
        {
            base.Load += new EventHandler(this.RoleTree_Load);
        }

        protected override void OnInit(EventArgs eventArgs_0)
        {
            this.method_0();
            base.OnInit(eventArgs_0);
        }

        private void RoleTree_Load(object sender, EventArgs e)
        {
            this.selmethod = base.GetParaValue("method");
            this.cid = base.GetParaValue("cid");
            this.queryfield = base.GetParaValue("queryfield");
            Department topDept = DepartmentService.GetTopDept();
            this.list_0 = (new _Department()).GetSubDeptByWbs(topDept.DeptWBS);
            TreeItem treeItem = new TreeItem()
            {
                id = "",
                text = topDept.DeptName,
                @value = "",
                isexpand = true
            };
            string str = string.Format("select _AutoID,RoleName from T_E_Org_Role where _IsDel=0 order by RoleName", new object[0]);
            foreach (DataRow row in SysDatabase.ExecuteTable(str).Rows)
            {
                TreeItem treeItem1 = new TreeItem()
                {
                    text = row["roleName"].ToString(),
                    id = row["_AutoID"].ToString(),
                    @value = "1"
                };
                treeItem.Add(treeItem1);
            }
            this.treedata = treeItem.ToJsonString();
        }
    }
}