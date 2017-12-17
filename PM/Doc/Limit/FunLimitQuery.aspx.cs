using EIS.AppBase;
using EIS.Permission;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EIS.Web.Doc.Limit
{
    public partial class FunLimitQuery : PageBase
    {
        public string funId = "";

        public string webId = "";

        public string strTreeHtml = "";

        public string strLimitData = "";

        public string strJsonData = "";

        public string strLink = "";

        public bool IsExpan = true;

        public int iExpanClass = 1;

        public string OrderByFieldName = "";

        public string SystemImagePath = "../../img/treeimages/";

        private StringBuilder stringBuilder_0 = new StringBuilder();

        private StringBuilder stringBuilder_1 = new StringBuilder();

        private DataTable dataTable_0 = new DataTable();

        private DataTable dataTable_1 = new DataTable();

        private string string_0 = "0";

      

        public string GetListTree(string PIDValue, string Indent)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string indent = "";
            int num = 0;
            string str = "";
            string str1 = "";
            string str2 = "";
            string[] strArrays = new string[] { "", "", "", "", "", "", "" };
            string[] strArrays1 = strArrays;
            DataRow[] dataRowArray = this.dataTable_0.Select("");
            int length = (int)dataRowArray.Length;
            for (int i = 0; i < length; i++)
            {
                DataRow dataRow = dataRowArray[i];
                str2 = dataRow["employeeId"].ToString();
                string str3 = (dataRow["Limit"].ToString() == "" ? "0000000000" : dataRow["Limit"].ToString());
                string str4 = "";
                if ((int)this.dataTable_1.Select(string.Concat("EmployeeId='", str2, "'")).Length > 0)
                {
                    str1 = this.dataTable_1.Select(string.Concat("EmployeeId='", str2, "'"))[0]["employeename"].ToString();
                    this.dataTable_1.Select(string.Concat("EmployeeId='", str2, "'"))[0]["deptname"].ToString();
                    str4 = this.dataTable_1.Select(string.Concat("EmployeeId='", str2, "'"))[0]["positionname"].ToString();
                }
                else if (str2 == "everyone")
                {
                    str1 = "所有人";
                }
                if (str3.Length >= 7)
                {
                    strArrays1[0] = (str3.Substring(0, 1) == "1" ? "checked" : " ");
                    strArrays1[1] = (str3.Substring(1, 1) == "1" ? "checked" : " ");
                    strArrays1[2] = (str3.Substring(2, 1) == "1" ? "checked" : " ");
                    strArrays1[3] = (str3.Substring(3, 1) == "1" ? "checked" : " ");
                    strArrays1[4] = (str3.Substring(4, 1) == "1" ? "checked" : " ");
                    strArrays1[5] = (str3.Substring(5, 1) == "1" ? "checked" : " ");
                    strArrays1[6] = (str3.Substring(6, 1) == "1" ? "checked" : " ");
                }
                string str5 = "open";
                string str6 = "";
                strArrays = new string[] { "<tr name='tr", str, "' id='tr", str, "' style='", str6, "' state='", str5, "' sonnum=0 ><td class='nodetd'>" };
                stringBuilder.Append(string.Concat(strArrays));
                indent = Indent;
                indent = indent.Replace("0", string.Concat("<img src='", this.SystemImagePath, "white.gif' align=AbsMiddle>"));
                indent = indent.Replace("1", string.Concat("<img src='", this.SystemImagePath, "i.gif' align=AbsMiddle>"));
                stringBuilder.Append(indent);
                num++;
                indent = (num != (int)dataRowArray.Length ? string.Concat("<img src='", this.SystemImagePath, "T.gif' align=AbsMiddle>") : string.Concat("<img src='", this.SystemImagePath, "L.gif' align=AbsMiddle>"));
                stringBuilder.Append(indent);
                stringBuilder.AppendFormat("<a href='../../SysFolder/Common/userInfo.aspx?empId={1}' target='_blank'>{0}</a></td>", string.Concat(str1, "（", str4, "）"), str2);
                stringBuilder.Append(string.Concat("<td >", (strArrays1[0] == "checked" ? "√" : "&nbsp;"), "</td>"));
                stringBuilder.Append(string.Concat("<td >", (strArrays1[1] == "checked" ? "√" : "&nbsp;"), "</td>"));
                stringBuilder.Append(string.Concat("<td >", (strArrays1[2] == "checked" ? "√" : "&nbsp;"), "</td>"));
                stringBuilder.Append(string.Concat("<td >", (strArrays1[3] == "checked" ? "√" : "&nbsp;"), "</td>"));
                stringBuilder.Append(string.Concat("<td >", (strArrays1[4] == "checked" ? "√" : "&nbsp;"), "</td>"));
                stringBuilder.Append(string.Concat("<td >", (strArrays1[5] == "checked" ? "√" : "&nbsp;"), "</td>"));
                stringBuilder.Append(string.Concat("<td >", (strArrays1[6] == "checked" ? "√" : "&nbsp;"), "</td>"));
                stringBuilder.Append("</tr>");
            }
            return stringBuilder.ToString();
        }

        private DataTable method_0(string string_1)
        {
            return EIS.Permission.Utility.GetFunLimitByFunId(string_1);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DepartmentService.GetTopDept();
            _Department __Department = new _Department();
            this.funId = base.GetParaValue("funId");
            this.dataTable_0 = this.method_0(this.funId);
            this.dataTable_1 = (new _DeptEmployee()).GetList(" 1=1 ");
            this.strTreeHtml = this.GetListTree("0", "0");
            this.strLimitData = string.Concat("<root>", this.strLimitData, "</root>");
            this.strJsonData = this.stringBuilder_1.ToString();
        }
    }
}