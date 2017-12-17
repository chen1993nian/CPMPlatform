using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace EIS.Studio.SysFolder.Limit
{
    public partial class FunLimitExclude : AdminPageBase
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

        private string string_0 = "0";

  
        public string GetListTree(string PIDValue, string Indent)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string indent = "";
            int num = 0;
            string str = "";
            string str1 = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string[] string0 = new string[] { "", "", "", "", "", "", "" };
            string[] strArrays = string0;
            DataRow[] dataRowArray = this.dataTable_0.Select(string.Concat("deptPWBS='", PIDValue, "'"), "orderId");
            int length = (int)dataRowArray.Length;
            for (int i = 0; i < length; i++)
            {
                DataRow dataRow = dataRowArray[i];
                str4 = dataRow["deptid"].ToString();
                str2 = dataRow["deptwbs"].ToString();
                str3 = dataRow["deptname"].ToString();
                string str5 = (dataRow["Limit"].ToString() == "" ? "0000000000" : dataRow["Limit"].ToString());
                StringBuilder stringBuilder0 = this.stringBuilder_0;
                string0 = new string[] { "<tn id=\"", str2, "\" bizId=\"", str4, "\" limit=\"", str5, "\" EditFlag=\"", this.string_0, "\" >" };
                stringBuilder0.Append(string.Concat(string0));
                if (str5.Length >= 7)
                {
                    strArrays[0] = (str5.Substring(0, 1) == "1" ? "checked" : " ");
                    strArrays[1] = (str5.Substring(1, 1) == "1" ? "checked" : " ");
                    strArrays[2] = (str5.Substring(2, 1) == "1" ? "checked" : " ");
                    strArrays[3] = (str5.Substring(3, 1) == "1" ? "checked" : " ");
                    strArrays[4] = (str5.Substring(4, 1) == "1" ? "checked" : " ");
                    strArrays[5] = (str5.Substring(5, 1) == "1" ? "checked" : " ");
                    strArrays[6] = (str5.Substring(6, 1) == "1" ? "checked" : " ");
                }
                int length1 = (int)this.dataTable_0.Select(string.Concat("deptPWBS='", str2, "'")).Length;
                string str6 = "open";
                string str7 = "";
                if (Indent.Length - 1 > this.iExpanClass)
                {
                    str6 = "close";
                    str7 = "display:none;";
                }
                string str8 = "";
                if (str2.Length == 36)
                {
                    str8 = "pos";
                }
                object[] objArray = new object[] { "<tr name='tr", str2, "' id='tr", str2, "' style='", str7, "' state='", str6, "' sonnum=", length1, " ><td class='nodetd ", str8, "'>" };
                stringBuilder.Append(string.Concat(objArray));
                indent = Indent;
                indent = indent.Replace("0", string.Concat("<img src='", this.SystemImagePath, "white.gif' align=AbsMiddle>"));
                indent = indent.Replace("1", string.Concat("<img src='", this.SystemImagePath, "i.gif' align=AbsMiddle>"));
                stringBuilder.Append(indent);
                num++;
                str1 = "Lminus.gif";
                if (Indent.Length > this.iExpanClass)
                {
                    str1 = "Lplus.gif";
                }
                if (length1 <= 0)
                {
                    indent = (num != (int)dataRowArray.Length ? string.Concat("<img src='", this.SystemImagePath, "T.gif' align=AbsMiddle>") : string.Concat("<img src='", this.SystemImagePath, "L.gif' align=AbsMiddle>"));
                }
                else if (num != (int)dataRowArray.Length)
                {
                    str = string.Concat(Indent, "1");
                    string0 = new string[] { "<img id='img", str2, "' style=\"cursor:hand\" src='", this.SystemImagePath, str1, "' align=absBottom \n  onclick=\"javascript:ShowHide(this,'", str2, "');\">" };
                    indent = string.Concat(string0);
                }
                else
                {
                    str = string.Concat(Indent, "0");
                    string0 = new string[] { "<img id='img", str2, "' style=\"cursor:hand\" src='", this.SystemImagePath, str1, "' align=absBottom \n  onclick=\"javascript:ShowHide(this,'", str2, "');\">" };
                    indent = string.Concat(string0);
                }
                stringBuilder.Append(indent);
                stringBuilder.AppendFormat("<input type=checkbox id='chk0{0}' name='chk{0}' ><label for='chk0{0}'>{1}</label></td>", str2, str3);
                string0 = new string[] { "<td ><input type=checkbox id='chk1", str2, "' name='chk", str2, "'  ", strArrays[0], "></td>" };
                stringBuilder.Append(string.Concat(string0));
                string0 = new string[] { "<td ><input type=checkbox id='chk2", str2, "' name='chk", str2, "'  ", strArrays[1], "></td>" };
                stringBuilder.Append(string.Concat(string0));
                string0 = new string[] { "<td ><input type=checkbox id='chk3", str2, "' name='chk", str2, "'  ", strArrays[2], "></td>" };
                stringBuilder.Append(string.Concat(string0));
                string0 = new string[] { "<td ><input type=checkbox id='chk4", str2, "' name='chk", str2, "'  ", strArrays[3], "></td>" };
                stringBuilder.Append(string.Concat(string0));
                string0 = new string[] { "<td ><input type=checkbox id='chk5", str2, "' name='chk", str2, "'  ", strArrays[4], "></td>" };
                stringBuilder.Append(string.Concat(string0));
                string0 = new string[] { "<td ><input type=checkbox id='chk6", str2, "' name='chk", str2, "'  ", strArrays[5], "></td>" };
                stringBuilder.Append(string.Concat(string0));
                string0 = new string[] { "<td ><input type=checkbox id='chk7", str2, "' name='chk", str2, "'  ", strArrays[6], "></td>" };
                stringBuilder.Append(string.Concat(string0));
                stringBuilder.Append("</tr>");
                stringBuilder.Append(this.GetListTree(str2, str));
                this.stringBuilder_0.Append("</tn>");
            }
            this.strLimitData = this.stringBuilder_0.ToString();
            return stringBuilder.ToString();
        }

        public void GetModelXML(string PIDValue)
        {
            string str = "";
            string str1 = "";
            DataRow[] dataRowArray = this.dataTable_0.Select(string.Concat("deptPWBS='", PIDValue, "'"));
            int length = (int)dataRowArray.Length;
            for (int i = 0; i < length; i++)
            {
                DataRow dataRow = dataRowArray[i];
                str1 = dataRow["deptId"].ToString();
                str = dataRow["deptwbs"].ToString();
                string str2 = (dataRow["Limit"].ToString() == "" ? "0000000000" : dataRow["Limit"].ToString());
                StringBuilder stringBuilder0 = this.stringBuilder_0;
                string[] strArrays = new string[] { "<tn id=\"", str, "\" bizId=\"", str1, "\" limit=\"", str2, "\" EditFlag=\"0\" >" };
                stringBuilder0.Append(string.Concat(strArrays));
                this.GetModelXML(str);
                this.stringBuilder_0.Append("</tn>");
            }
        }

        private DataTable method_0(string string_1)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("SELECT _AutoID DeptId, DeptName, DeptWBS ,DeptPWBS ,(OrderId+100) OrderId,'' funid,'' Limit FROM T_E_Org_Department where _IsDel=0\r\n                 union select r.EmployeeID, r.EmployeeName+'（'+r.PositionName+'）', r.EmployeeID ,d.DeptWBS ,d.OrderId,m.funid,m.Limit \r\n                 FROM T_E_Org_DeptEmployee AS r inner join T_E_Org_Department d on r.DeptID=d._AutoID\r\n                 LEFT OUTER JOIN T_E_Org_ExcludeLimit AS m \r\n                 ON r.EmployeeID = m.EmployeeID \r\n                 and m.FunID = '{0}' where r.DeptEmployeeType=0", string_1);
            return SysDatabase.ExecuteTable(stringBuilder.ToString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(FunLimitExclude));
            DepartmentService.GetTopDept();
            _Department __Department = new _Department();
            this.funId = base.GetParaValue("funId");
            this.dataTable_0 = this.method_0(this.funId);
            this.strTreeHtml = this.GetListTree("0", "0");
            this.strLimitData = string.Concat("<root>", this.strLimitData, "</root>");
            this.strJsonData = this.stringBuilder_1.ToString();
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string SaveLimit(string funId, string limitxml)
        {
            this.dataTable_0 = this.method_0(funId);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(limitxml);
            string str = "";
            try
            {
                string funWbsById = FunNodeService.GetFunWbsById(funId);
                if (funWbsById.Trim() == "")
                {
                    throw new Exception("保存权限时出错：节点编号（FunWBS）为空");
                }
                foreach (XmlNode xmlNodes in xmlDocument.SelectNodes("//tn[@EditFlag='1']"))
                {
                    if (xmlNodes.Attributes.GetNamedItem("id").Value.Length != 36)
                    {
                        continue;
                    }
                    string value = xmlNodes.Attributes.GetNamedItem("limit").Value;
                    string value1 = xmlNodes.Attributes.GetNamedItem("bizId").Value;
                    DataTable dataTable0 = this.dataTable_0;
                    string[] strArrays = new string[] { "FunID='", funId, "' and DeptId='", value1, "'" };
                    if ((int)dataTable0.Select(string.Concat(strArrays)).Length != 0)
                    {
                        strArrays = new string[] { "update dbo.T_E_Org_ExcludeLimit set Limit ='", value, "',funwbs='", funWbsById, "' where funid='", funId, "' and employeeId='", value1, "'" };
                        str = string.Concat(strArrays);
                    }
                    else if (value == "0000000000")
                    {
                        str = "";
                    }
                    else
                    {
                        object[] objArray = new object[] { Guid.NewGuid(), base.EmployeeID, base.OrgCode, DateTime.Now, DateTime.Now, 0, funId, value1, value, funWbsById };
                        str = string.Format("insert into dbo.T_E_Org_ExcludeLimit\r\n                                (_AutoID,_UserName,_OrgCode,_CreateTime,_UpdateTime,_IsDel,FunID,employeeId,Limit,FunWBS) \r\n                                values('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}','{8}','{9}')", objArray);
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
            this.dataTable_0 = this.method_0(funId);
            this.GetModelXML("0");
            return string.Concat("<root>", this.stringBuilder_0.ToString(), "</root>");
        }
    }
}