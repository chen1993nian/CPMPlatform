using EIS.AppBase;
using EIS.DataAccess;
using System;
using System.Data;
using System.Text;
using System.Web.SessionState;
using System.Web.UI;
namespace EIS.Web.SysFolder.Permission
{
    public partial class CompanyEmployeeLimitLeft : AdminPageBase
    {
        public string treedata = "";

        private StringBuilder stringBuilder_0 = new StringBuilder();

        public string roleId = "";

        public string roleName = "";

        private DataTable dataTable_0;

       
        private void CompanyEmployeeLimitLeft_Load(object sender, EventArgs e)
        {
            string str = base.Session["CompanyWbs"].ToString();
            string str1 = base.Session["companyId"].ToString();
            string str2 = base.Session["companyName"].ToString();
            string str3 = string.Format("select _autoid,deptwbs,deptpwbs,deptname,OrderID ,9 as DeptEmployeeType from T_E_Org_Department \r\n                            where deptwbs like '{1}%'\r\n                            union\r\n                            select de.employeeid,de.employeeid,d.DeptWBS, de.EmployeeName,de.OrderID,de.DeptEmployeeType\r\n                            from T_E_Org_DeptEmployee de inner join T_E_Org_Department d  on de.DeptID=d._AutoID where d.deptwbs like '{1}%'\r\n\r\n                            ", this.roleId, str);
            this.dataTable_0 = SysDatabase.ExecuteTable(str3);
            TreeItem treeItem = new TreeItem()
            {
                id = str,
                text = str2,
                @value = str1,
                isexpand = true
            };
            this.method_0(treeItem);
            this.treedata = treeItem.ToJsonString();
        }

        private void method_0(TreeItem treeItem_0)
        {
            DataRow[] dataRowArray = this.dataTable_0.Select(string.Concat("deptpwbs='", treeItem_0.id, "'"), "orderid");
            if ((int)dataRowArray.Length > 0)
            {
                DataRow[] dataRowArray1 = dataRowArray;
                for (int i = 0; i < (int)dataRowArray1.Length; i++)
                {
                    DataRow dataRow = dataRowArray1[i];
                    TreeItem treeItem = new TreeItem()
                    {
                        id = dataRow["DeptWbs"].ToString()
                    };
                    if (dataRow["DeptEmployeeType"].ToString() == "9")
                    {
                        treeItem.text = dataRow["DeptName"].ToString();
                    }
                    else if (dataRow["DeptEmployeeType"].ToString() != "1")
                    {
                        treeItem.text = dataRow["DeptName"].ToString();
                    }
                    else
                    {
                        treeItem.text = string.Concat(dataRow["DeptName"].ToString(), "（兼）");
                    }
                    treeItem.@value = dataRow["_AutoID"].ToString();
                    treeItem.complete = true;
                    treeItem.hasChildren = dataRow["DeptEmployeeType"].ToString() == "9";
                    treeItem_0.Add(treeItem);
                    this.method_0(treeItem);
                }
            }
        }

        private void method_1()
        {
            base.Load += new EventHandler(this.CompanyEmployeeLimitLeft_Load);
        }

        protected override void OnInit(EventArgs eventArgs_0)
        {
            this.method_1();
            base.OnInit(eventArgs_0);
        }
    }
}