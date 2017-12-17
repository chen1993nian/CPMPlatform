using EIS.AppBase;
using EIS.AppCommon;
using EIS.DataAccess;
using EIS.DataModel.Service;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EIS.Web.WorkAsp.WorkLog
{
    public partial class LogLeft : PageBase
    {
        public string treedata = "";

        public string treeId = "";

        protected HtmlHead Head1;

        protected HtmlForm Form1;

     

        private string[] method_0(StringCollection stringCollection_0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder length = new StringBuilder();
            foreach (string stringCollection0 in stringCollection_0)
            {
                string[] strArrays = stringCollection0.Split(new char[] { '|' });
                if (!EmployeeService.CheckedValid(strArrays[0]))
                {
                    continue;
                }
                stringBuilder.AppendFormat("{0},", strArrays[0]);
                length.AppendFormat("{0},", strArrays[1]);
            }
            if (stringBuilder.Length > 1)
            {
                stringBuilder.Length = stringBuilder.Length - 1;
                length.Length = length.Length - 1;
            }
            string[] str = new string[] { stringBuilder.ToString(), length.ToString() };
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int i;
            zTreeNode _zTreeNode;
            zTreeNode _zTreeNode1 = new zTreeNode();
            if (this.treeId == "")
            {
                _zTreeNode1.id = "0";
                _zTreeNode1.name = "工作日志";
                _zTreeNode1.icon = "../../img/doc/home.png";
                _zTreeNode1.@value = "0";
                _zTreeNode1.open = true;
            }
            StringCollection myLeader2 = EmployeeRelationService.GetMyLeader2(base.EmployeeID);
            myLeader2 = EmployeeRelationService.GetMyUnderling(base.EmployeeID, null);
            string[] strArrays = this.method_0(myLeader2);
            string str = strArrays[0];
            char[] chrArray = new char[] { ',' };
            string[] strArrays1 = str.Split(chrArray);
            string str1 = strArrays[1];
            chrArray = new char[] { ',' };
            string[] strArrays2 = str1.Split(chrArray);
            for (i = 0; i < (int)strArrays1.Length; i++)
            {
                if (strArrays1[i].Length != 0)
                {
                    _zTreeNode = new zTreeNode()
                    {
                        icon = "../../img/doc/folder_user.png",
                        name = strArrays2[i],
                        id = strArrays1[i],
                        @value = "1"
                    };
                    _zTreeNode1.Add(_zTreeNode);
                }
            }
            string str2 = string.Format("select _AutoID,DeptName from T_E_Org_Department where PicPositionId in \r\n                (select PositionId from T_E_Org_DeptEmployee de where de.EmployeeID='{0}')\r\n                or UpPositionId in (select PositionId from T_E_Org_DeptEmployee de where de.EmployeeID='{0}')", base.EmployeeID);
            foreach (DataRow row in SysDatabase.ExecuteTable(str2).Rows)
            {
                zTreeNode _zTreeNode2 = new zTreeNode()
                {
                    name = row["DeptName"].ToString(),
                    id = "dept",
                    @value = "0",
                    icon = "../../img/doc/folder_user.png",
                    open = true
                };
                _zTreeNode1.Add(_zTreeNode2);
                List<Employee> employeeByDeptId = DepartmentService.GetEmployeeByDeptId(row["_AutoId"].ToString());
                for (i = 0; i < employeeByDeptId.Count; i++)
                {
                    _zTreeNode = new zTreeNode()
                    {
                        name = employeeByDeptId[i].EmployeeName,
                        id = employeeByDeptId[i]._AutoID,
                        @value = "1"
                    };
                    _zTreeNode2.Add(_zTreeNode);
                }
            }
            this.treedata = _zTreeNode1.ToJsonString(true);
        }
    }
}