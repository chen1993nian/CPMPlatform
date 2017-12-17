using EIS.AppBase;
using EIS.Permission;
using EIS.Permission.Service;
using System;
using System.Data;

namespace EIS.Studio
{
    public partial class TreeNode : AdminPageBase
    {
        private DataTable dataTable_0 = null;

        public string treedata = "";


        private void method_0(TreeItem treeItem_0)
        {
            DataRow[] dataRowArray = this.dataTable_0.Select(string.Concat("FunPWBS='", treeItem_0.id, "'"), "orderid");
            if ((int)dataRowArray.Length > 0)
            {
                DataRow[] dataRowArray1 = dataRowArray;
                for (int i = 0; i < (int)dataRowArray1.Length; i++)
                {
                    DataRow dataRow = dataRowArray1[i];
                    if (dataRow["DispState"].ToString() != "否")
                    {
                        TreeItem treeItem = new TreeItem();
                        string str = dataRow["_AutoID"].ToString();
                        treeItem.text = dataRow["FunName"].ToString();
                        treeItem.id = dataRow["FunWBS"].ToString();
                        string str1 = dataRow["LinkFile"].ToString().Trim("\r\n".ToCharArray());
                        if (dataRow["Encrypt"].ToString() != "是")
                        {
                            treeItem.checkstate = 1;
                            treeItem.@value = str1;
                        }
                        else
                        {
                            treeItem.@value = this.method_1(str1, str);
                        }
                        treeItem.isexpand = dataRow["IsExpand"].ToString() == "是";
                        treeItem_0.Add(treeItem);
                        this.method_0(treeItem);
                    }
                }
            }
        }

        private string method_1(string path, string funid)
        {
            string str;
            if (path.Trim() != "")
            {
                string[] strArrays = path.Split("?".ToCharArray());
                if ((int)strArrays.Length != 1)
                {
                    path = string.Concat(strArrays[0], "?", base.ReplaceContext(strArrays[1]));
                }
                str = EIS.AppBase.Utility.EncryptUrl(path, base.UserName);
            }
            else
            {
                str = "";
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string paraValue = base.GetParaValue("parentnode");
            if ((base.LoginType == "1") || (base.LoginType == "2"))
            {
                string str = "";
                if (base.LoginType == "1")
                {
                    str = "_GroupAdmin";
                }
                else if (base.LoginType == "2")
                {
                    str = "_CompanyAdmin";
                }
                string roleId = RoleService.GetRoleId(str);
                this.dataTable_0 = EIS.Permission.Utility.GetAllowedFunNodeByRole(roleId, paraValue, AppSettings.Instance.WebId);
            }
            else
            {
                this.dataTable_0 = FunNodeService.GetAllFunNodeByWebId(AppSettings.Instance.WebId);
            }
            TreeItem treeItem = new TreeItem()
            {
                id = paraValue,
                text = "功能菜单",
                @value = "",
                isexpand = true
            };
            this.method_0(treeItem);
            this.treedata = treeItem.ToJsonString(false);
        }
    }
}