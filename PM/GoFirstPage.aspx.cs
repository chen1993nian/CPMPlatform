using EIS.AppBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EIS.Web
{
    public partial class GoFirstPage : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string pwbs = base.GetParaValue("pwbs");
            DataTable dtNode = EIS.Permission.Utility.GetAllowedFunNode(base.EmployeeID, pwbs, this.Session["webId"].ToString());
            if ((dtNode != null) && (dtNode.Rows.Count > 0))
            {
                DataRow[] dataRowArray1 = dtNode.Select(string.Concat("FunPWBS like '", pwbs, "%' and LinkFile<>'' "));
                if (dataRowArray1.Length > 0)
                {
                    for (int j = 0; j < (int)dataRowArray1.Length; j++)
                    {
                        Response.Redirect(method_1(dataRowArray1[j]));
                        break;
                    }
                }
                else
                {
                    Response.Redirect("Desktop.aspx?pwbs=" + pwbs);
                }
            }
            else
            {
                Response.Redirect("Desktop.aspx?pwbs=" + pwbs);
            }
        }

        private string method_1(DataRow dataRow_0)
        {
            string str;
            string str1 = dataRow_0["_AutoID"].ToString();
            if (dataRow_0["Encrypt"].ToString() != "是")
            {
                str = dataRow_0["LinkFile"].ToString().Replace("\r\n", "");
            }
            else
            {
                string str2 = dataRow_0["LinkFile"].ToString().Replace("\r\n", "");
                if (str2.Trim() != "")
                {
                    string[] strArrays = str2.Split("?".ToCharArray());
                    if ((int)strArrays.Length != 1)
                    {
                        string[] strArrays1 = new string[] { strArrays[0], "?", base.ReplaceContext(strArrays[1]), "&funid=", str1 };
                        str2 = string.Concat(strArrays1);
                    }
                    else
                    {
                        str2 = string.Concat(str2, "?funid=", str1);
                    }
                    str = EIS.AppBase.Utility.EncryptUrl(str2, base.UserName);
                }
                else
                {
                    str = "";
                }
            }
            return str;
        }


    }
}