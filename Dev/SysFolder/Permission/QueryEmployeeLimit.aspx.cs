using EIS.AppBase;
using EIS.Permission;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.Permission
{
    public partial class QueryEmployeeLimit : AdminPageBase
    {
       

        public string roleid = "";

        public string webId = "";

        public StringBuilder strTreeHtml = new StringBuilder();

        public StringBuilder strLimitData = new StringBuilder();

        public string strLink = "";

        public bool IsExpan = true;

        public int iExpanClass = 0;

        public string OrderByFieldName = "";

        public string SystemImagePath = "../../img/treeimages/";

        public string IDFldName = "_AutoID";

        public string SIDFldName = "FunWBS";

        public string PIDFldName = "FunpWBS";

        public string CaptionFldName = "FunName";

        private StringBuilder stringBuilder_0 = new StringBuilder();

        private DataTable dataTable_0 = new DataTable();

        private string string_0 = "0";

  
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dataTable_0 = EIS.Permission.Utility.GetAllLimitDataByEmployeeId(this.roleid, this.webId);
            this.strLimitData.Append("<root>");
            this.GetListTree("0", "0");
            this.strLimitData.Append("</root>");
        }

        public void GetListTree(string PIDValue, string Indent)
        {
            string indent = "";
            int num = 0;
            string str = "";
            string str1 = "";
            string str2 = "";
            string str3 = "";
            string[] string0 = new string[] { "", "", "", "", "", "", "" };
            string[] strArrays = string0;
            str3 = string.Concat(this.PIDFldName, "='", PIDValue, "'");
            DataRow[] dataRowArray = this.dataTable_0.Select(str3);
            int length = (int)dataRowArray.Length;
            for (int i = 0; i < length; i++)
            {
                DataRow dataRow = dataRowArray[i];
                dataRow[this.IDFldName].ToString();
                str2 = dataRow[this.SIDFldName].ToString();
                string str4 = (dataRow["Limit"].ToString() == "" ? "0000000000" : dataRow["Limit"].ToString());
                StringBuilder stringBuilder = this.strLimitData;
                string0 = new string[] { "<tn id=\"", str2, "\" limit=\"", str4, "\" EditFlag=\"", this.string_0, "\" >" };
                stringBuilder.Append(string.Concat(string0));
                if (str4.Length >= 5)
                {
                    strArrays[0] = (str4.Substring(0, 1) == "1" ? "checked" : " ");
                    strArrays[1] = (str4.Substring(1, 1) == "1" ? "checked" : " ");
                    strArrays[2] = (str4.Substring(2, 1) == "1" ? "checked" : " ");
                    strArrays[3] = (str4.Substring(3, 1) == "1" ? "checked" : " ");
                    strArrays[4] = (str4.Substring(4, 1) == "1" ? "checked" : " ");
                    strArrays[5] = (str4.Substring(5, 1) == "1" ? "checked" : " ");
                    strArrays[6] = (str4.Substring(6, 1) == "1" ? "checked" : " ");
                }
                int length1 = (int)this.dataTable_0.Select(string.Concat(this.PIDFldName, "='", dataRow[this.SIDFldName].ToString(), "'")).Length;
                this.strTreeHtml.Append(string.Concat("\n\n<tr name='tr", dataRow[this.SIDFldName].ToString()));
                StringBuilder stringBuilder1 = this.strTreeHtml;
                object[] objArray = new object[] { "' id='tr", dataRow[this.SIDFldName].ToString(), "' state='open' sonnum=", length1, " ><td class='nodetd'>" };
                stringBuilder1.Append(string.Concat(objArray));
                indent = Indent;
                indent = indent.Replace("0", string.Concat("<img src='", this.SystemImagePath, "white.gif' align=AbsMiddle>"));
                indent = indent.Replace("1", string.Concat("<img src='", this.SystemImagePath, "i.gif' align=AbsMiddle>"));
                this.strTreeHtml.Append(indent);
                num++;
                str1 = "Lplus.gif";
                if (this.IsExpan)
                {
                    str1 = "Lminus.gif";
                }
                else if (this.iExpanClass > Indent.Length)
                {
                    str1 = "Lminus.gif";
                }
                if (length1 <= 0)
                {
                    indent = (num != (int)dataRowArray.Length ? string.Concat("<img src='", this.SystemImagePath, "T.gif' align=AbsMiddle>") : string.Concat("<img src='", this.SystemImagePath, "L.gif' align=AbsMiddle>"));
                }
                else if (num != (int)dataRowArray.Length)
                {
                    str = string.Concat(Indent, "1");
                    string0 = new string[] { "<img id='img", str2, "' style=\"cursor:hand\" src='", this.SystemImagePath, str1, "' align=absBottom \n  onclick=\"javascript:ShowHide(this,'", dataRow[this.SIDFldName].ToString(), "');\">" };
                    indent = string.Concat(string0);
                }
                else
                {
                    str = string.Concat(Indent, "0");
                    string0 = new string[] { "<img id='img", str2, "' style=\"cursor:hand\" src='", this.SystemImagePath, str1, "' align=absBottom \n  onclick=\"javascript:ShowHide(this,'", dataRow[this.SIDFldName].ToString(), "');\">" };
                    indent = string.Concat(string0);
                }
                this.strTreeHtml.Append(indent);
                this.strTreeHtml.Append(string.Concat(dataRow[this.CaptionFldName].ToString(), "</td>"));
                this.strTreeHtml.Append(string.Concat("<td >", (strArrays[0] == "checked" ? "√" : "&nbsp;"), "</td>"));
                this.strTreeHtml.Append(string.Concat("<td >", (strArrays[1] == "checked" ? "√" : "&nbsp;"), "</td>"));
                this.strTreeHtml.Append(string.Concat("<td >", (strArrays[2] == "checked" ? "√" : "&nbsp;"), "</td>"));
                this.strTreeHtml.Append(string.Concat("<td >", (strArrays[3] == "checked" ? "√" : "&nbsp;"), "</td>"));
                this.strTreeHtml.Append(string.Concat("<td >", (strArrays[4] == "checked" ? "√" : "&nbsp;"), "</td>"));
                this.strTreeHtml.Append(string.Concat("<td >", (strArrays[5] == "checked" ? "√" : "&nbsp;"), "</td>"));
                this.strTreeHtml.Append(string.Concat("<td >", (strArrays[6] == "checked" ? "√" : "&nbsp;"), "</td>"));
                this.strTreeHtml.Append("</tr>");
                PIDValue = dataRow[this.SIDFldName].ToString();
                this.GetListTree(PIDValue, str);
                this.strLimitData.Append("</tn>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.roleid = base.Request["roleid"];
            if (!base.IsPostBack)
            {
                foreach (WebCataLog cataLogList in WebCataLogService.GetCataLogList(WebCataLog.BizSystem))
                {
                    ListItem listItem = new ListItem(cataLogList.WebName, cataLogList.WebId);
                    if (this.Session["DefaultWebSite"] != null && listItem.Value == this.Session["DefaultWebSite"].ToString())
                    {
                        listItem.Selected = true;
                    }
                    this.DropDownList1.Items.Add(listItem);
                }
                this.webId = this.DropDownList1.SelectedValue;
                this.dataTable_0 =EIS.Permission.Utility.GetAllLimitDataByEmployeeId(this.roleid, this.webId);
                this.strLimitData.Append("<root>");
                this.GetListTree("0", "0");
                this.strLimitData.Append("</root>");
            }
            this.webId = this.DropDownList1.SelectedValue;
        }
    }
}