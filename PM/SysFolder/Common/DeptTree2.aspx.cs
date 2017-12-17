using EIS.AppBase;
using EIS.AppCommon;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.UI;

namespace EIS.Web.SysFolder.Common
{
    public partial class DeptTree2 : PageBase
    {
        public string treeData = "";

        public string deptTree = "";

        private List<Department> list_0;

        public string selmethod = "";

        public string cid = "";

        public string queryfield = "";



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
                        id = department.DeptWBS
                    };
                    string[] deptCode = new string[] { department._AutoID, "|", department.DeptCode, "|", department.DeptFullName };
                    _zTreeNode.@value = string.Concat(deptCode);
                    zTreeNode_0.Add(_zTreeNode);
                    this.method_0(_zTreeNode);
                }
            }
        }

        private void method_1(TreeItem treeItem_0)
        {
            // treeItem_0.id != "";
            List<Department> departments = this.list_0.FindAll((Department department_0) => department_0.DeptPWBS == treeItem_0.id);
            if (departments.Count > 0)
            {
                foreach (Department department in departments)
                {
                    TreeItem treeItem = new TreeItem()
                    {
                        text = department.DeptName,
                        id = department.DeptWBS
                    };
                    string[] deptCode = new string[] { department._AutoID, "|", department.DeptCode, "|", department.DeptFullName };
                    treeItem.@value = string.Concat(deptCode);
                    treeItem_0.Add(treeItem);
                    this.method_1(treeItem);
                }
            }
        }

        private void method_2()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs eventArgs_0)
        {
            this.method_2();
            base.OnInit(eventArgs_0);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.selmethod = base.GetParaValue("method");
            this.cid = base.GetParaValue("cid");
            this.queryfield = base.GetParaValue("queryfield");
            Department topDept = DepartmentService.GetTopDept();
            this.list_0 = (new _Department()).GetSubDeptByWbs(topDept.DeptWBS);
            TreeItem treeItem = new TreeItem()
            {
                id = topDept.DeptWBS,
                text = topDept.DeptName
            };
            string[] deptCode = new string[] { topDept._AutoID, "|", topDept.DeptCode, "|", topDept.DeptFullName };
            treeItem.@value = string.Concat(deptCode);
            treeItem.isexpand = true;
            this.method_1(treeItem);
            this.treeData = treeItem.ToJsonString();
            zTreeNode _zTreeNode = new zTreeNode()
            {
                id = topDept.DeptWBS,
                name = topDept.DeptName,
                icon = "../../img/common/home.png",
                @value = "",
                open = true
            };
            Dict modelByCode = (new _Dict()).GetModelByCode("_Org_DeptProp");
            if (modelByCode != null)
            {
                foreach (DictEntry modelListByDictId in (new _DictEntry()).GetModelListByDictId(modelByCode._AutoID))
                {
                    zTreeNode _zTreeNode1 = new zTreeNode()
                    {
                        name = modelListByDictId.ItemName,
                        id = "",
                        icon = "../../img/common/site.png",
                        @value = ""
                    };
                    _zTreeNode.Add(_zTreeNode1);
                    string typeIdByCode = DeptTypeService.GetTypeIdByCode("BM");
                    List<Department> departments = this.list_0.FindAll((Department department_0) => (department_0.DeptProp != modelListByDictId.ItemCode ? false : department_0.TypeID == typeIdByCode));
                    departments.Sort((Department department_0, Department department_1) => department_0.DeptName.CompareTo(department_1.DeptName));
                    foreach (Department department in departments)
                    {
                        zTreeNode _zTreeNode2 = new zTreeNode()
                        {
                            name = department.DeptName,
                            id = department.DeptWBS,
                            @value = string.Concat(department._AutoID, "|", department.DeptCode)
                        };
                        _zTreeNode1.Add(_zTreeNode2);
                        this.method_0(_zTreeNode2);
                    }
                }
            }
            this.deptTree = _zTreeNode.ToJsonString(true);
        }

    }
}