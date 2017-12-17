using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace EIS.Web.SysFolder.Permission
{
    public partial class DefEmployeeLimitEdit : AdminPageBase
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

        public string IDFldName = "FunID";

        public string SIDFldName = "FunWBS";

        public string PIDFldName = "FunpWBS";

        public string CaptionFldName = "FunName";

        private StringBuilder stringBuilder_0 = new StringBuilder();

        private DataTable dataTable_0 = new DataTable();

        private DataTable dataTable_1 = new DataTable();

        private string string_0 = "0";

     

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.method_0();
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
                string str5 = dataRow[this.CaptionFldName].ToString();
                this.strTreeHtml.AppendFormat("<input type=checkbox id='chk0{0}' name='chk{0}' ><label for='chk0{0}'>{1}</label></td>", str2, str5);
                StringBuilder stringBuilder2 = this.strTreeHtml;
                string0 = new string[] { "<td ><input type=checkbox id='chk1", str2, "' name='chk", str2, "'  ", strArrays[0], "></td>" };
                stringBuilder2.Append(string.Concat(string0));
                StringBuilder stringBuilder3 = this.strTreeHtml;
                string0 = new string[] { "<td ><input type=checkbox id='chk2", str2, "' name='chk", str2, "'  ", strArrays[1], "></td>" };
                stringBuilder3.Append(string.Concat(string0));
                StringBuilder stringBuilder4 = this.strTreeHtml;
                string0 = new string[] { "<td ><input type=checkbox id='chk3", str2, "' name='chk", str2, "'  ", strArrays[2], "></td>" };
                stringBuilder4.Append(string.Concat(string0));
                StringBuilder stringBuilder5 = this.strTreeHtml;
                string0 = new string[] { "<td ><input type=checkbox id='chk4", str2, "' name='chk", str2, "'  ", strArrays[3], "></td>" };
                stringBuilder5.Append(string.Concat(string0));
                StringBuilder stringBuilder6 = this.strTreeHtml;
                string0 = new string[] { "<td ><input type=checkbox id='chk5", str2, "' name='chk", str2, "'  ", strArrays[4], "></td>" };
                stringBuilder6.Append(string.Concat(string0));
                StringBuilder stringBuilder7 = this.strTreeHtml;
                string0 = new string[] { "<td ><input type=checkbox id='chk6", str2, "' name='chk", str2, "'  ", strArrays[5], "></td>" };
                stringBuilder7.Append(string.Concat(string0));
                StringBuilder stringBuilder8 = this.strTreeHtml;
                string0 = new string[] { "<td ><input type=checkbox id='chk7", str2, "' name='chk", str2, "'  ", strArrays[6], "></td>" };
                stringBuilder8.Append(string.Concat(string0));
                this.strTreeHtml.Append("</tr>");
                PIDValue = dataRow[this.SIDFldName].ToString();
                this.GetListTree(PIDValue, str);
                this.strLimitData.Append("</tn>");
            }
        }

        public void GetModelXML(string PIDValue)
        {
            string str = "";
            string str1 = "";
            str1 = string.Concat(this.PIDFldName, "='", PIDValue, "'");
            DataRow[] dataRowArray = this.dataTable_0.Select(str1);
            int length = (int)dataRowArray.Length;
            for (int i = 0; i < length; i++)
            {
                DataRow dataRow = dataRowArray[i];
                dataRow[this.IDFldName].ToString();
                str = dataRow[this.SIDFldName].ToString();
                string str2 = (dataRow["Limit"].ToString() == "" ? "0000000000" : dataRow["Limit"].ToString());
                StringBuilder stringBuilder = this.strLimitData;
                string[] strArrays = new string[] { "<tn id=\"", str, "\" limit=\"", str2, "\" EditFlag=\"0\" >" };
                stringBuilder.Append(string.Concat(strArrays));
                PIDValue = dataRow[this.SIDFldName].ToString();
                this.GetModelXML(PIDValue);
                this.strLimitData.Append("</tn>");
            }
        }

        private void method_0()
        {
            this.Session["DefaultWebSite"] = this.webId;
            this.dataTable_0 = EmployeeLimitService.GetEmployeeLimitDataById(this.webId, this.roleid);
            this.strLimitData.Append("<root>");
            this.GetListTree("0", "0");
            this.strLimitData.Append("</root>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefEmployeeLimitEdit));
            this.roleid = base.Request["roleid"];
            if (base.IsPostBack)
            {
                this.webId = this.DropDownList1.SelectedValue;
            }
            else
            {
                foreach (WebCataLog cataLogList in WebCataLogService.GetCataLogList(WebCataLog.BizSystem))
                {
                    if (!string.IsNullOrEmpty(cataLogList.ShowState) && cataLogList.ShowState.ToString()=="1")
                    {
                        ListItem listItem = new ListItem(cataLogList.WebName, cataLogList.WebId);
                        if (this.Session["DefaultWebSite"] != null && listItem.Value == this.Session["DefaultWebSite"].ToString())
                        {
                            listItem.Selected = true;
                        }
                        this.DropDownList1.Items.Add(listItem);
                    }
                }
                string paraValue = base.GetParaValue("webId");
                if (paraValue.Trim() == "")
                {
                    this.webId = this.DropDownList1.SelectedValue;
                }
                else
                {
                    this.webId = paraValue;
                    this.DropDownList1.SelectedValue = paraValue;
                    this.DropDownList1.Enabled = false;
                }
                this.method_0();
            }
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string SaveLimit(string roleid, string limitxml, string WebId)
        {
            this.dataTable_0 = EmployeeLimitService.GetEmployeeLimitDataById(WebId, roleid);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(limitxml);
            string str = "";
            try
            {
                foreach (XmlNode xmlNodes in xmlDocument.SelectNodes("//tn[@EditFlag='1']"))
                {
                    string value = xmlNodes.Attributes.GetNamedItem("id").Value;
                    if (value.Trim() == "")
                    {
                        throw new Exception("保存权限时出错：节点编号（FunWBS）为空");
                    }
                    string str1 = this.dataTable_0.Select(string.Concat("FunWBS='", value, "'"))[0]["FunID"].ToString();
                    string value1 = xmlNodes.Attributes.GetNamedItem("limit").Value;
                    DataTable dataTable0 = this.dataTable_0;
                    string[] strArrays = new string[] { "FunWBS='", value, "' and EmployeeID='", roleid, "'" };
                    if ((int)dataTable0.Select(string.Concat(strArrays)).Length != 0)
                    {
                        strArrays = new string[] { "update dbo.T_E_Org_EmployeeLimit set Limit ='", value1, "',funwbs='", value, "' where funid='", str1, "' and EmployeeID='", roleid, "'" };
                        str = string.Concat(strArrays);
                    }
                    else if (value1 == "0000000000")
                    {
                        str = "";
                    }
                    else
                    {
                        object[] objArray = new object[] { Guid.NewGuid(), base.EmployeeID, base.OrgCode, DateTime.Now, DateTime.Now, 0, str1, roleid, value1, value };
                        str = string.Format("insert into dbo.T_E_Org_EmployeeLimit\r\n                                (_AutoID,_UserName,_OrgCode,_CreateTime,_UpdateTime,_IsDel,FunID,EmployeeID,Limit,FunWBS) \r\n                                values('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}','{8}','{9}')", objArray);
                    }
                    if (str == "")
                    {
                        continue;
                    }
                    SysDatabase.ExecuteNonQuery(str);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            this.dataTable_0 = EmployeeLimitService.GetEmployeeLimitDataById(WebId, roleid);
            this.GetModelXML("0");
            return string.Concat("<root>", this.strLimitData.ToString(), "</root>");
        }
    }
}