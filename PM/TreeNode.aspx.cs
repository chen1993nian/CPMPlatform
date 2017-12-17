using EIS.AppBase;
using EIS.Permission;
using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI;
using NLog;
namespace EIS.Web
{
    public partial class TreeNode : PageBase
    {
        private DataTable dataTable_0 = null;

        public string treedata = "";
        private  Logger fileLogger;
        	
        private void method_0(TreeItem treeItem_0)
        {
            try{
            string treeItem0 = "0";
            if (treeItem_0.id != "")
            {
                treeItem0 = treeItem_0.id;
            }
            DataRow[] dataRowArray = this.dataTable_0.Select(string.Concat("FunPWBS='", treeItem0, "'"), "orderid");
            if ((int)dataRowArray.Length > 0)
            {
                DataRow[] dataRowArray1 = dataRowArray;
                for (int i = 0; i < (int)dataRowArray1.Length; i++)
                {
                    DataRow dataRow = dataRowArray1[i];
                    TreeItem treeItem = new TreeItem();
                    string str = dataRow["DispStyle"].ToString();
                    string str1 = dataRow["_AutoID"].ToString();
                    treeItem.text = dataRow["FunName"].ToString();
                    treeItem.id = dataRow["FunWBS"].ToString();
                    string str2 = dataRow["LinkFile"].ToString().Trim("\r\n".ToCharArray());
                    if (dataRow["Encrypt"].ToString() != "是")
                    {
                        treeItem.checkstate = 1;
                        treeItem.@value = str2;
                    }
                    else
                    {
                        treeItem.@value = this.method_1(str2, str1);
                    }
                    treeItem.tag = str;
                    treeItem.isexpand = dataRow["IsExpand"].ToString() != "否";
                    treeItem_0.Add(treeItem);
                    this.method_0(treeItem);
                }
            }
            }catch(Exception ex)
            {
                fileLogger.Error<string, string, string>("发生错误 treeNodex:{0},{1},参数列表：{2}", "method_0(TreeItem treeItem_0)", ex.Message, treeItem_0.text);
            
            }
        }

        private string method_1(string path, string funid)
        {
            string str="";
            try{
            if (path.Trim() != "")
            {
                string[] strArrays = path.Split("?".ToCharArray());
                if ((int)strArrays.Length != 1)
                {
                    string[] strArrays1 = new string[] { strArrays[0], "?", base.ReplaceContext(strArrays[1]), "&funid=", funid };
                    path = string.Concat(strArrays1);
                }
                else
                {
                    path = string.Concat(path, "?funid=", funid);
                }
                str = EIS.AppBase.Utility.EncryptUrl(path, base.UserName);
            }
            else
            {
                str = "";
            }
            }catch(Exception ex)
            {
                fileLogger.Error<string, string, string>("发生错误 treeNodex:{0},{1},参数列表：{2}", "method_1(string path, string funid)", ex.Message, path + "->" + funid);
            
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try{
                fileLogger = LogManager.GetCurrentClassLogger();

                string paraValue = base.GetParaValue("parentnode");
                this.dataTable_0 = EIS.Permission.Utility.GetAllowedFunNode(base.EmployeeID, paraValue, this.Session["webId"].ToString());
                if ((this.dataTable_0 != null) && (this.dataTable_0.Rows.Count > 0))
                {
                    TreeItem treeItem = new TreeItem()
                    {
                        id = paraValue,
                        text = "功能菜单",
                        @value = "",
                        isexpand = true,
                    };
                    this.method_0(treeItem);
                    this.treedata = treeItem.ToJsonString(false);
                }
            }catch(Exception ex)
            {

                fileLogger.Error<string, string, string>("发生错误 treeNodex:{0},{1},参数列表：{2}", "Page_Load(object sender, EventArgs e)", ex.Message, "parentnode");

            }
        }
    }
}