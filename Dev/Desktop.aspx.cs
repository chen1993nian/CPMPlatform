using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission;
using System;
using System.Data;
using System.Text;
using System.Web.SessionState;
using System.Web.UI;

namespace EIS.Studio
{
    public partial class Desktop : AdminPageBase
    {
        public StringBuilder funcIdStr = new StringBuilder();

        public StringBuilder func_array = new StringBuilder();

      
        private string method_0(DataRow dataRow_0)
        {
            string str;
            dataRow_0["_AutoID"].ToString();
            if (dataRow_0["Encrypt"].ToString() != "是")
            {
                str = dataRow_0["LinkFile"].ToString();
            }
            else
            {
                string str1 = dataRow_0["LinkFile"].ToString();
                if (str1.Trim() != "")
                {
                    string[] strArrays = str1.Split("?".ToCharArray());
                    if ((int)strArrays.Length != 1)
                    {
                        str1 = string.Concat(strArrays[0], "?", base.ReplaceContext(strArrays[1]));
                    }
                    str = EIS.AppBase.Utility.EncryptUrl(str1, base.UserName);
                }
                else
                {
                    str = "";
                }
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Session["webId"] = AppSettings.Instance.WebId;
            string str = string.Format("select * from T_E_Sys_FunNode where webId='{0}' and DesktopShow='是' order by DesktopOrder", this.Session["webId"]);
            DataTable dataTable = SysDatabase.ExecuteTable(str);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow item = dataTable.Rows[i];
                string str1 = item["_AutoID"].ToString();
                if ((base.LoginType == "1" ? false : !(base.LoginType == "2")) || EIS.Permission.Utility.GetFunLimitByEmployeeId(base.EmployeeID, str1).StartsWith("1"))
                {
                    string str2 = this.method_0(item);
                    int hashCode = item["_AutoID"].GetHashCode();
                    string str3 = hashCode.ToString().TrimStart(new char[] { '-' });
                    if ((i <= 0 ? false : i % 24 == 0))
                    {
                        this.funcIdStr.Append("|");
                    }
                    this.funcIdStr.Append(string.Concat(str3, ","));
                    StringBuilder funcArray = this.func_array;
                    object[] objArray = new object[] { str3, item["FunName"], str2, item["DesktopImage"], item["DispStyle"] };
                    funcArray.AppendFormat("\r\nfunc_array[\"{0}\"] = [\"{1}\", \"{2}\", \"{3}\", \"{4}\"];", objArray);
                }
            }
        }
    }
}