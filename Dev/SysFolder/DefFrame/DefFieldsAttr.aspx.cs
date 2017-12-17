using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using EIS.WebBase;
using WebBase.JZY.Tools;
namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefFieldsAttr : AdminPageBase
    {
      

        public string fieldHTML = "";

        public string fieldHTML2 = "";

        public string fieldXML = "";

        public string rowcount = "0";

        public string disp = "";

        public string tblName = "";

    

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.method_3();
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string GetFieldNameCn(string tblName, string fldName)
        {
            string str = "";
            object obj = SysDatabase.ExecuteScalar(string.Concat("select top 1 FieldNameCn from (select FieldNameCn,count(*) c from T_E_Sys_FieldInfo \r\n                where fieldName='", fldName, "' group by FieldNameCn) t order by c desc"));
            if (obj != null)
            {
                str = obj.ToString();
            }
            return str;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DbDataReader dbDataReaders;
            XmlElement xmlElement;
            string attribute;
            string str;
            string selectedValue = this.DropDownList1.SelectedValue;
            TableInfo model = (new _TableInfo(this.tblName)).GetModel();
            string str1 = "";
            str1 = (string.IsNullOrEmpty(model.ListSQL) ? string.Concat("select * from ", this.tblName) : model.ListSQL);
            DataTable dataTable = new DataTable();
            if (model.ConnectionId == "")
            {
                dbDataReaders = SysDatabase.ExecuteReader(str1.Replace("|^condition^|", " 1=1 ").Replace("|^sortdir^|", ""));
                try
                {
                    dataTable = dbDataReaders.GetSchemaTable();
                }
                finally
                {
                    if (dbDataReaders != null)
                    {
                        ((IDisposable)dbDataReaders).Dispose();
                    }
                }
            }
            else
            {
                CustomDb customDb = new CustomDb();
                customDb.CreateDatabaseByConnectionId(model.ConnectionId);
                dbDataReaders = customDb.ExecuteReader(str1.Replace("|^condition^|", " 1=1 ").Replace("|^sortdir^|", ""));
                try
                {
                    dataTable = dbDataReaders.GetSchemaTable();
                }
                finally
                {
                    if (dbDataReaders != null)
                    {
                        ((IDisposable)dbDataReaders).Dispose();
                    }
                }
            }
            _TableInfo __TableInfo = new _TableInfo(this.tblName);
            int maxOdr = __TableInfo.GetMaxOdr();
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                _FieldInfo __FieldInfo = new _FieldInfo(dbTransaction);
                _FieldInfoExt __FieldInfoExt = new _FieldInfoExt(dbTransaction);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(string.Concat("<?xml version='1.0' encoding='utf-8' ?>", base.Request["txtxml"]));
                __TableInfo.GetFields();
                XmlElement xmlElement1 = (XmlElement)xmlDocument.SelectSingleNode("tr");
                string str2 = "td";
                if (xmlElement1.SelectNodes("TD").Count > 0)
                {
                    str2 = "TD";
                }
                if ((xmlElement1 == null ? false : xmlElement1.SelectNodes(str2).Count > 0))
                {
                    foreach (XmlNode xmlNodesA in xmlElement1.SelectNodes(string.Concat(str2, "[@state='changed']")))
                    {
                        xmlElement = (XmlElement)xmlNodesA;
                        attribute = xmlElement.GetAttribute("txtname");
                        str = "1";
                        str = (!dataTable.Columns.Contains("DataTypeName") ? this.TransOracleType(dataTable.Select(string.Concat("ColumnName='", attribute, "'"))[0]["DataType"].ToString()) : this.TransType(dataTable.Select(string.Concat("ColumnName='", attribute, "'"))[0]["DataTypeName"].ToString()));
                        if (selectedValue != "")
                        {
                            FieldInfoExt now = __FieldInfoExt.GetModel(xmlElement.GetAttribute("fieldid"));
                            now._UpdateTime = DateTime.Now;
                            now.FieldName = xmlElement.GetAttribute("txtname");
                            now.FieldNameCn = xmlElement.GetAttribute("txtnamecn");
                            now.ListDisp = Convert.ToInt32(xmlElement.GetAttribute("chklistdisp"));
                            now.QueryDisp = Convert.ToInt32(xmlElement.GetAttribute("chkquerydisp"));
                            now.ColumnAlign = xmlElement.GetAttribute("selalign").Trim();
                            now.ColumnWidth = xmlElement.GetAttribute("txtwidth").Trim();
                            now.ColumnRender = xmlElement.GetAttribute("txtrender");
                            now.ColFormatExp = xmlElement.GetAttribute("txtformatexp");
                            now.ColTotalExp = xmlElement.GetAttribute("txttotalexp");
                            now.FieldType = Convert.ToInt32(str);
                            now.QueryStyle = xmlElement.GetAttribute("querystyle");
                            now.QueryStyleName = xmlElement.GetAttribute("querystylename");
                            now.QueryStyleTxt = xmlElement.GetAttribute("querystyletxt");
                            now.QueryMatchMode = xmlElement.GetAttribute("querymatch");
                            now.QueryDefaultType = xmlElement.GetAttribute("deftype");
                            now.QueryDefaultValue = xmlElement.GetAttribute("defvalue");
                            __FieldInfoExt.Update(now);
                        }
                        else
                        {
                            FieldInfo num = __FieldInfo.GetModel(xmlElement.GetAttribute("fieldid"));
                            num._UpdateTime = DateTime.Now;
                            num.FieldName = xmlElement.GetAttribute("txtname");
                            num.FieldNameCn = xmlElement.GetAttribute("txtnamecn");
                            num.FieldType = Convert.ToInt32(str);
                            num.ListDisp = Convert.ToInt32(xmlElement.GetAttribute("chklistdisp"));
                            num.QueryDisp = Convert.ToInt32(xmlElement.GetAttribute("chkquerydisp"));
                            num.ColumnAlign = xmlElement.GetAttribute("selalign").Trim();
                            num.ColumnWidth = xmlElement.GetAttribute("txtwidth").Trim();
                            num.ColumnRender = xmlElement.GetAttribute("txtrender");
                            num.ColFormatExp = xmlElement.GetAttribute("txtformatexp");
                            num.ColTotalExp = xmlElement.GetAttribute("txttotalexp");
                            num.QueryStyle = xmlElement.GetAttribute("querystyle");
                            num.QueryStyleName = xmlElement.GetAttribute("querystylename");
                            num.QueryStyleTxt = xmlElement.GetAttribute("querystyletxt");
                            num.QueryMatchMode = xmlElement.GetAttribute("querymatch");
                            num.QueryDefaultType = xmlElement.GetAttribute("deftype");
                            num.QueryDefaultValue = xmlElement.GetAttribute("defvalue");
                            __FieldInfo.Update(num);
                        }
                    }
                }
                foreach (XmlNode xmlNodes1 in xmlElement1.SelectNodes(string.Concat(str2, "[@state='add']")))
                {
                    xmlElement = (XmlElement)xmlNodes1;
                    attribute = xmlElement.GetAttribute("txtname");
                    str = "1";
                    str = (!dataTable.Columns.Contains("DataTypeName") ? this.TransOracleType(dataTable.Select(string.Concat("ColumnName='", attribute, "'"))[0]["DataType"].ToString()) : this.TransType(dataTable.Select(string.Concat("ColumnName='", attribute, "'"))[0]["DataTypeName"].ToString()));
                    if (selectedValue != "")
                    {
                        FieldInfoExt fieldInfoExt = new FieldInfoExt()
                        {
                            _AutoID = Guid.NewGuid().ToString(),
                            _OrgCode = base.OrgCode,
                            _UserName = base.EmployeeID,
                            _CreateTime = DateTime.Now,
                            _UpdateTime = DateTime.Now,
                            _IsDel = 0,
                            TableName = this.tblName,
                            FieldName = xmlElement.GetAttribute("txtname"),
                            FieldNameCn = xmlElement.GetAttribute("txtnamecn"),
                            FieldType = Convert.ToInt32(str),
                            ListDisp = Convert.ToInt32(xmlElement.GetAttribute("chklistdisp")),
                            QueryDisp = Convert.ToInt32(xmlElement.GetAttribute("chkquerydisp")),
                            ColumnAlign = xmlElement.GetAttribute("selalign").Trim(),
                            ColumnWidth = xmlElement.GetAttribute("txtwidth").Trim(),
                            ColumnRender = xmlElement.GetAttribute("txtrender"),
                            ColumnHidden = 0,
                            ColFormatExp = xmlElement.GetAttribute("txtformatexp"),
                            ColTotalExp = xmlElement.GetAttribute("txttotalexp"),
                            ColumnOrder = Convert.ToInt32(xmlElement.GetAttribute("oindex")),
                            StyleIndex = Convert.ToInt32(selectedValue),
                            QueryStyle = xmlElement.GetAttribute("querystyle"),
                            QueryStyleName = xmlElement.GetAttribute("querystylename"),
                            QueryStyleTxt = xmlElement.GetAttribute("querystyletxt"),
                            QueryMatchMode = xmlElement.GetAttribute("querymatch"),
                            QueryDefaultType = xmlElement.GetAttribute("deftype"),
                            QueryDefaultValue = xmlElement.GetAttribute("defvalue")
                        };
                        __FieldInfoExt.Add(fieldInfoExt);
                    }
                    else
                    {
                        FieldInfo fieldInfo = new FieldInfo()
                        {
                            _AutoID = Guid.NewGuid().ToString(),
                            _OrgCode = base.OrgCode,
                            _UserName = base.EmployeeID,
                            _CreateTime = DateTime.Now,
                            _UpdateTime = DateTime.Now,
                            _IsDel = 0,
                            TableName = this.tblName,
                            FieldName = xmlElement.GetAttribute("txtname"),
                            FieldNameCn = xmlElement.GetAttribute("txtnamecn"),
                            FieldType = Convert.ToInt32(str),
                            ListDisp = Convert.ToInt32(xmlElement.GetAttribute("chklistdisp")),
                            QueryDisp = Convert.ToInt32(xmlElement.GetAttribute("chkquerydisp")),
                            ColumnAlign = xmlElement.GetAttribute("selalign").Trim(),
                            ColumnWidth = xmlElement.GetAttribute("txtwidth").Trim(),
                            ColumnRender = xmlElement.GetAttribute("txtrender"),
                            ColFormatExp = xmlElement.GetAttribute("txtformatexp"),
                            ColTotalExp = xmlElement.GetAttribute("txttotalexp"),
                            QueryStyle = xmlElement.GetAttribute("querystyle"),
                            QueryStyleName = xmlElement.GetAttribute("querystylename"),
                            QueryStyleTxt = xmlElement.GetAttribute("querystyletxt"),
                            QueryMatchMode = xmlElement.GetAttribute("querymatch"),
                            QueryDefaultType = xmlElement.GetAttribute("deftype"),
                            QueryDefaultValue = xmlElement.GetAttribute("defvalue")
                        };
                        int num1 = maxOdr + 1;
                        maxOdr = num1;
                        fieldInfo.FieldOdr = num1;
                        fieldInfo.IsComput = 1;
                        __FieldInfo.Add(fieldInfo);
                    }
                }
                foreach (XmlNode xmlNodes2 in xmlElement1.SelectNodes(string.Concat(str2, "[@state='deleted']")))
                {
                    xmlElement = (XmlElement)xmlNodes2;
                    if (selectedValue != "")
                    {
                        __FieldInfoExt.Delete(xmlElement.GetAttribute("fieldid"));
                    }
                    else
                    {
                        __FieldInfo.Delete(xmlElement.GetAttribute("fieldid"));
                    }
                }
                __TableInfo.SetUpdateTime();
                dbTransaction.Commit();
                base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "$.noticeAdd({text:'保存成功！',stay:false});", true);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                dbTransaction.Rollback();
                base.Response.Write(string.Concat("出现错误:", exception.ToString()));
            }
            this.method_3();
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            int maxIndex = _FieldInfoExt.GetMaxIndex(this.tblName) + 1;
            _TableInfo __TableInfo = new _TableInfo(this.tblName);
            List<FieldInfo> tableFields = (new _FieldInfo()).GetTableFields(this.tblName);
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                _FieldInfoExt __FieldInfoExt = new _FieldInfoExt(dbTransaction);
                foreach (FieldInfo tableField in tableFields)
                {
                    FieldInfoExt fieldInfoExt = new FieldInfoExt()
                    {
                        _AutoID = Guid.NewGuid().ToString(),
                        _OrgCode = base.OrgCode,
                        _UserName = base.EmployeeID,
                        _CreateTime = DateTime.Now,
                        _UpdateTime = DateTime.Now,
                        _IsDel = 0,
                        TableName = this.tblName,
                        FieldName = tableField.FieldName,
                        FieldNameCn = tableField.FieldNameCn,
                        FieldType = tableField.FieldType,
                        ListDisp = tableField.ListDisp,
                        QueryDisp = tableField.QueryDisp,
                        ColumnAlign = tableField.ColumnAlign,
                        ColumnWidth = tableField.ColumnWidth,
                        ColumnRender = tableField.ColumnRender,
                        ColumnHidden = 0,
                        ColFormatExp = tableField.ColFormatExp,
                        ColTotalExp = tableField.ColTotalExp,
                        ColumnOrder = tableField.ColumnOrder,
                        StyleIndex = maxIndex
                    };
                    __FieldInfoExt.Add(fieldInfoExt);
                }
                __TableInfo.SetUpdateTime();
                dbTransaction.Commit();
                this.method_0(maxIndex);
                this.method_3();
                base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "$.noticeAdd({text:'保存成功！',stay:false});", true);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                dbTransaction.Rollback();
                base.Response.Write(string.Concat("出现错误:", exception.ToString()));
            }
        }

        private void method_0(int int_0)
        {
            this.DropDownList1.Items.Clear();
            ListItem listItem = new ListItem("默认列表", "");
            this.DropDownList1.Items.Add(listItem);
            if (int_0 == 0)
            {
                this.DropDownList1.SelectedIndex = 0;
            }
            int maxIndex = _FieldInfoExt.GetMaxIndex(this.tblName);
            for (int i = 1; i <= maxIndex; i++)
            {
                listItem = new ListItem(string.Concat("列表", i.ToString()), i.ToString())
                {
                    Selected = int_0 == i
                };
                this.DropDownList1.Items.Add(listItem);
            }
        }

        private string method_1(string val1, string val2)
        {
            return (val1 != val2 ? "" : "selected");
        }

        private string method_2(string val1, string val2)
        {
            return (val1 != val2 ? "" : "checked");
        }

        private void method_3()
        {

            //StringBuilder sbsql = new StringBuilder();
            //sbsql.AppendFormat("SELECT  count(*) FROM dbo.SysObjects WHERE ID = object_id(N'[{0}]') AND OBJECTPROPERTY(ID, 'IsTable') = 1",this.tblName);
            //DataTable dtExits = new DataTable();
            //dtExits=SysDatabase.ExecuteTable(sbsql.ToString());

            //if(dtExits.Rows[0][0].ToString()=="0" )
            //     return ;


            try
            {

            DbDataReader dbDataReaders;
            string[] str;
            string selectedValue = this.DropDownList1.SelectedValue;
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            TableInfo model = (new _TableInfo(this.tblName)).GetModel();
            string str1 = "";
            str1 = (string.IsNullOrEmpty(model.ListSQL) ? string.Concat("select * from ", this.tblName) : model.ListSQL);
            DataTable dataTable = new DataTable();
            if (model.ConnectionId == "")
            {
                dbDataReaders = SysDatabase.ExecuteReader(str1.Replace("|^condition^|", " 1=1 ").Replace("|^sortdir^|", ""));
                try
                {
                    dataTable = dbDataReaders.GetSchemaTable();
                }
                finally
                {
                    if (dbDataReaders != null)
                    {
                        ((IDisposable)dbDataReaders).Dispose();
                    }
                }
            }
            else
            {
                CustomDb customDb = new CustomDb();
                customDb.CreateDatabaseByConnectionId(model.ConnectionId);
                dbDataReaders = customDb.ExecuteReader(str1.Replace("|^condition^|", " 1=1 ").Replace("|^sortdir^|", ""));
                try
                {
                    dataTable = dbDataReaders.GetSchemaTable();
                }
                finally
                {
                    if (dbDataReaders != null)
                    {
                        ((IDisposable)dbDataReaders).Dispose();
                    }
                }
            }
            foreach (DataRow row in dataTable.Rows)
            {
                stringBuilder1.AppendFormat("<option value='{0}'>{0}</option>", row["ColumnName"]);
            }
            this.fieldHTML2 = stringBuilder1.ToString();
            List<FieldInfoExt> fieldInfoExts = new List<FieldInfoExt>();
            if (selectedValue != "")
            {
                _FieldInfoExt __FieldInfoExt = new _FieldInfoExt();
                int num = Convert.ToInt32((selectedValue == "" ? "0" : this.DropDownList1.SelectedValue));
                fieldInfoExts = __FieldInfoExt.GetTableFields(this.tblName, num);
            }
            else
            {
                fieldInfoExts = (new _FieldInfo()).GetDefaultFieldsExt(this.tblName);
            }
            int num1 = 1;
            foreach (FieldInfoExt fieldInfoExt in fieldInfoExts)
            {
                stringBuilder.Append("<tr>\n");
                stringBuilder.Append(string.Concat("\t<td align=\"center\">", num1.ToString(), "</td>\n"));
                if (fieldInfoExt.IsComput != 1)
                {
                    stringBuilder.Append(string.Concat("\t<td align=\"left\" class='_fieldname'>", fieldInfoExt.FieldName, "</td>\n"));
                }
                else
                {
                    str = new string[] { "\t<td align=\"left\"><select id=\"txtname", num1.ToString(), "\" size=\"1\" oindex='", num1.ToString(), "'  name=\"txtname\">\n" };
                    stringBuilder.Append(string.Concat(str));
                    stringBuilder.Append(this.fieldHTML2.Replace(string.Concat("value='", fieldInfoExt.FieldName, "'"), string.Concat("value='", fieldInfoExt.FieldName, "' selected")));
                    stringBuilder.Append("</select></td>\n");
                }
                str = new string[] { "\t<td align=\"center\"><input class='textbox' id=\"txtnamecn", num1.ToString(), "\"  type=\"text\"  oindex=", num1.ToString(), " name=\"txtnamecn\" value='", fieldInfoExt.FieldNameCn, "'></td>\n" };
                stringBuilder.Append(string.Concat(str));
                str = new string[] { "\t<td align=\"center\"><input id=\"chklistdisp", num1.ToString(), "\" type=\"checkbox\" oindex='", num1.ToString(), "' ", null, null };
                int listDisp = fieldInfoExt.ListDisp;
                str[5] = this.method_2("1", listDisp.ToString());
                str[6] = "  name=\"chklistdisp\">\n";
                stringBuilder.Append(string.Concat(str));
                object[] fieldName = new object[] { num1, null, null, null, null, null, null, null, null };
                listDisp = fieldInfoExt.QueryDisp;
                fieldName[1] = this.method_2("1", listDisp.ToString());
                fieldName[2] = selectedValue;
                fieldName[3] = fieldInfoExt.FieldName;
                fieldName[4] = fieldInfoExt.FieldNameCn;
                fieldName[5] = fieldInfoExt.FieldType;
                fieldName[6] = fieldInfoExt.QueryMatchMode;
                fieldName[7] = fieldInfoExt.QueryDefaultType;
                fieldName[8] = fieldInfoExt.QueryDefaultValue;
                stringBuilder.AppendFormat("<td align='center'><input id='chkquerydisp{0}' name='chkquerydisp' type='checkbox' oindex='{0}' {1} />\r\n                    <input type='hidden' oindex='{0}' id='querymatch{0}' name='querymatch' value='{6}'/>\r\n                    <input type='hidden' oindex='{0}' id='deftype{0}' name='deftype' value='{7}'/>\r\n                    <input type='hidden' oindex='{0}' id='defvalue{0}' name='defvalue' value='{8}'/>\r\n                    <a class='querySet' href='javascript:' attr='{0}|{3}|{4}|{5}'>【设置】</a>", fieldName);
                stringBuilder.Append("\t<td>");
                if (fieldInfoExt.QueryStyleName != "")
                {
                    stringBuilder.AppendFormat("<a href=\"javascript:\" class=\"querybtn\" id='querybtn{0}' oindex='{0}' >{1}</a>", num1, fieldInfoExt.QueryStyleName);
                }
                else
                {
                    stringBuilder.AppendFormat("<a href=\"javascript:\" class=\"querybtn\" id='querybtn{0}' oindex='{0}' >【选择】</a>", num1);
                }
                str = new string[] { "<input class='querystylename' id=\"querystylename", num1.ToString(), "\"  type=\"hidden\"  oindex=", num1.ToString(), " name=\"querystylename\" value='", fieldInfoExt.QueryStyleName, "'>" };
                stringBuilder.Append(string.Concat(str));
                str = new string[] { "<input class='querystyletxt' id=\"querystyletxt", num1.ToString(), "\"  type=\"hidden\"  oindex=", num1.ToString(), " name=\"querystyletxt\" value='", this.method_4(fieldInfoExt.QueryStyleTxt), "'>" };
                stringBuilder.Append(string.Concat(str));
                str = new string[] { "<input class='hidden' id=\"querystyle", num1.ToString(), "\"  type=\"hidden\"  oindex=", num1.ToString(), " name=\"querystyle\" value='", fieldInfoExt.QueryStyle, "'>\n" };
                stringBuilder.Append(string.Concat(str));
                stringBuilder.Append("\t</td>\n");
                str = new string[] { "\t<td align=\"center\"><select id=\"selalign", num1.ToString(), "\" size=\"1\" oindex='", num1.ToString(), "'  name=\"selalign\">\n" };
                stringBuilder.Append(string.Concat(str));
                stringBuilder.Append(string.Concat("\t\t<option value=\"1\"", this.method_1("1", fieldInfoExt.ColumnAlign.ToString()), ">左</option>\n"));
                stringBuilder.Append(string.Concat("\t\t<option value=\"2\"", this.method_1("2", fieldInfoExt.ColumnAlign.ToString()), ">中</option>\n"));
                stringBuilder.Append(string.Concat("\t\t<option value=\"3\"", this.method_1("3", fieldInfoExt.ColumnAlign.ToString()), ">右</option>\n"));
                stringBuilder.Append("\t\t</select></td>\n");
                str = new string[] { "\t<td align=\"center\"><input class='textbox' id=\"txtwidth", num1.ToString(), "\"  type=\"text\"  oindex=", num1.ToString(), " name=\"txtwidth\" value='", fieldInfoExt.ColumnWidth, "'></td>\n" };
                stringBuilder.Append(string.Concat(str));
                str = new string[] { "\t<td align=\"center\"><input class='textbox' id=\"txtrender", num1.ToString(), "\"  type=\"text\"  oindex=", num1.ToString(), " name=\"txtrender\" value='", fieldInfoExt.ColumnRender, "'></td>\n" };
                stringBuilder.Append(string.Concat(str));
                str = new string[] { "\t<td align=\"center\"><input class='textbox' id=\"txtformatexp", num1.ToString(), "\"  type=\"text\"  oindex=", num1.ToString(), " name=\"txtformatexp\" value='", fieldInfoExt.ColFormatExp, "'></td>\n" };
                stringBuilder.Append(string.Concat(str));
                str = new string[] { "\t<td align=\"center\"><input class='textbox' id=\"txttotalexp", num1.ToString(), "\"  type=\"text\"  oindex=", num1.ToString(), " name=\"txttotalexp\" value='", fieldInfoExt.ColTotalExp, "'></td>\n" };
                stringBuilder.Append(string.Concat(str));
                if (fieldInfoExt.IsComput != 1)
                {
                    stringBuilder.Append("\t<td align=\"center\">&nbsp;</td>\n");
                }
                else
                {
                    stringBuilder.Append(string.Concat("\t<td align=\"center\"><input id=\"btndel\"  type=\"button\" onclick='dropfield(", num1.ToString(), ");'  name=\"btndel\" value='删除'></td>\n"));
                }
                stringBuilder.Append("</tr>\n");
                fieldName = new object[] { num1, fieldInfoExt.FieldName, fieldInfoExt.ListDisp, fieldInfoExt.QueryDisp, fieldInfoExt.ColumnAlign, fieldInfoExt.ColumnWidth, fieldInfoExt.FieldNameCn, fieldInfoExt.ColumnRender, fieldInfoExt.ColFormatExp, fieldInfoExt.ColTotalExp, fieldInfoExt._AutoID, fieldInfoExt.QueryStyle, fieldInfoExt.QueryStyleName, this.method_4(fieldInfoExt.QueryStyleTxt), fieldInfoExt.QueryMatchMode, fieldInfoExt.QueryDefaultType, fieldInfoExt.QueryDefaultValue };
                stringBuilder2.AppendFormat("<td oindex={0} txtname='{1}'  txtnamecn='{6}' chklistdisp='{2}' chkquerydisp='{3}' selalign='{4}' txtwidth='{5}' txtrender='{7}'  txtformatexp='{8}' txttotalexp='{9}' fieldid='{10}'  querystyle='{11}' querystylename='{12}' querystyletxt='{13}' querymatch='{14}' deftype='{15}' defvalue='{16}' state='unchanged'/>", fieldName);
                num1++;
            }
            this.fieldHTML = stringBuilder.ToString();
            this.fieldXML = stringBuilder2.ToString();
            this.rowcount = num1.ToString();
            }catch(Exception ex)
            {
                base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>$.noticeAdd({text:'表或查询业务不存在！',stay:false});</script>");
            }
        }

        private string method_4(string string_0)
        {
            string string0;
            if (string_0 != null)
            {
                string_0 = string_0.Replace("&", "&amp;");
                string_0 = string_0.Replace("<", "&lt;");
                string_0 = string_0.Replace(">", "&gt;");
                string_0 = string_0.Replace("'", "&apos;");
                string_0 = string_0.Replace("\"", "&quot;");
                string_0 = string_0.Replace("\r", "&#x000D;");
                string_0 = string_0.Replace("\n", "&#x000A;");
                string_0 = string_0.Replace(" ", "&#x0020;");
                string_0 = string_0.Replace("\t", "&#x0009;");
                string0 = string_0;
            }
            else
            {
                string0 = "";
            }
            return string0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefFieldsAttr));
            this.tblName = base.GetParaValue("tblName");
            if (!base.IsPostBack)
            {
                this.method_0(0);
                this.method_3();
            }
            if (base.Request["t"] == "3")
            {
                this.disp = "display:none";
            }
        }

        public string TransOracleType(string dbtype)
        {
            string str;
            string lower = dbtype.ToLower();
            if (lower != null)
            {
                if (lower == "system.int32")
                {
                    str = "2";
                    return str;
                }
                else if (lower == "system.datetime")
                {
                    str = "4";
                    return str;
                }
                else
                {
                    if (lower != "system.decimal")
                    {
                        str = "1";
                        return str;
                    }
                    str = "3";
                    return str;
                }
            }
            str = "1";
            return str;
        }

        public string TransType(string nettype)
        {
            string str;
            int num;
            string str1 = nettype;
            if (str1 != null)
            {
                if (Class7.dictionary_0 == null)
                {
                    Class7.dictionary_0 = new Dictionary<string, int>(6)
					{
						{ "int", 0 },
						{ "datetime", 1 },
						{ "varchar", 2 },
						{ "nvarchar", 3 },
						{ "decimal", 4 },
						{ "text", 5 }
					};
                }
                if (Class7.dictionary_0.TryGetValue(str1, out num))
                {
                    switch (num)
                    {
                        case 0:
                            {
                                str = "2";
                                break;
                            }
                        case 1:
                            {
                                str = "4";
                                break;
                            }
                        case 2:
                        case 3:
                            {
                                str = "1";
                                break;
                            }
                        case 4:
                            {
                                str = "3";
                                break;
                            }
                        case 5:
                            {
                                str = "5";
                                break;
                            }
                        default:
                            {
                                str = "1";
                                return str;
                            }
                    }
                }
                else
                {
                    str = "1";
                    return str;
                }
            }
            else
            {
                str = "1";
                return str;
            }
            return str;
        }
    }
}