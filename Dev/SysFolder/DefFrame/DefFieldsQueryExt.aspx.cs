using EIS.AppBase;
using EIS.AppModel;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace EIS.Studio.SysFolder.DefFrame
{
    public  partial class DefFieldsQueryExt : AdminPageBase
    {
               public string fieldHTML = "";

        public string fieldHTML2 = "";

        public string fieldXML = "";

        public string rowcount = "0";

        public string disp = "";

        public string tblName = "";

  
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            this.tblName = base.GetParaValue("tblname");
            _TableInfo __TableInfo = new _TableInfo(this.tblName);
            __TableInfo.GetModel();
            _TableInfo __TableInfo1 = new _TableInfo(this.tblName);
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                _FieldInfo __FieldInfo = new _FieldInfo(dbTransaction);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(string.Concat("<?xml version='1.0' encoding='utf-8' ?>", base.Request["txtxml"]));
                __TableInfo1.GetFields();
                XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode("root");
                string str = "td";
                if (xmlElement.SelectNodes("TD").Count > 0)
                {
                    str = "TD";
                }
                if ((xmlElement == null ? false : xmlElement.SelectNodes(str).Count > 0))
                {
                    foreach (XmlNode xmlNodes in xmlElement.SelectNodes(string.Concat(str, "[@state='changed']")))
                    {
                        XmlElement xmlElement1 = (XmlElement)xmlNodes;
                        FieldInfo model = __FieldInfo.GetModel(xmlElement1.GetAttribute("fieldid"));
                        model._UpdateTime = DateTime.Now;
                        model.QueryDisp = Convert.ToInt32(xmlElement1.SelectSingleNode("chkquery").InnerText);
                        model.QueryMatchMode = xmlElement1.SelectSingleNode("chkmatch").InnerText;
                        model.QueryDefaultType = xmlElement1.SelectSingleNode("seldeftype").InnerText;
                        model.QueryDefaultValue = xmlElement1.SelectSingleNode("txtdefvalue").InnerText;
                        model.QueryStyle = xmlElement1.SelectSingleNode("txtstyle").InnerText;
                        model.QueryStyleName = xmlElement1.SelectSingleNode("txtstylename").InnerText;
                        model.QueryStyleTxt = xmlElement1.SelectSingleNode("txtstyletxt").InnerText;
                        __FieldInfo.Update(model);
                    }
                }
                __TableInfo.SetUpdateTime();
                dbTransaction.Commit();
                base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>$.noticeAdd({text:'保存成功！',stay:false});</script>");
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                dbTransaction.Rollback();
                base.Response.Write(string.Concat("出现错误:", exception.ToString()));
            }
            this.method_2();
        }

        private string method_0(string val1, string val2)
        {
            return (val1 != val2 ? "" : "selected");
        }

        private string method_1(string val1, string val2)
        {
            return (val1 != val2 ? "" : "checked");
        }

        private void method_2()
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            this.tblName = base.GetParaValue("tblname");
            (new _TableInfo(this.tblName)).GetModel();
            List<FieldInfo> tableFields = (new _FieldInfo()).GetTableFields(this.tblName);
            int num = 1;
            foreach (FieldInfo tableField in tableFields)
            {
                stringBuilder.Append("<tr>\n");
                stringBuilder.Append(string.Concat("\t<td align=\"center\">", num.ToString(), "</td>\n"));
                stringBuilder.Append(string.Concat("\t<td align=\"left\">", tableField.FieldName, "</td>\n"));
                stringBuilder.Append(string.Concat("\t<td align=\"center\">", tableField.FieldNameCn, "</td>\n"));
                string[] str = new string[] { "\t<td align=\"center\"><input id=\"chkquery", num.ToString(), "\" type=\"checkbox\" oindex='", num.ToString(), "' ", null, null };
                int queryDisp = tableField.QueryDisp;
                str[5] = this.method_1("1", queryDisp.ToString());
                str[6] = "  name=\"chkquery\">\n";
                stringBuilder.Append(string.Concat(str));
                stringBuilder.Append("\t<td align=\"center\">");
                if ((tableField.FieldType == 1 ? true : tableField.FieldType == 5))
                {
                    str = new string[] { "<select id=\"chkmatch", num.ToString(), "\" size=\"1\" oindex='", num.ToString(), "'  name=\"chkmatch\">\n" };
                    stringBuilder.Append(string.Concat(str));
                    stringBuilder.Append(string.Concat("\t\t<option value=\"\"", this.method_0("", tableField.QueryMatchMode), "></option>\n"));
                    stringBuilder.Append(string.Concat("\t\t<option value=\"1\"", this.method_0("1", tableField.QueryMatchMode), ">部分匹配</option>\n"));
                    stringBuilder.Append(string.Concat("\t\t<option value=\"2\"", this.method_0("2", tableField.QueryMatchMode), ">完全匹配</option>\n"));
                    stringBuilder.Append("\t\t</select>");
                }
                stringBuilder.Append("</td>\n");
                str = new string[] { "\t<td align=\"left\"><select class='seldeftype' id=\"seldefault", num.ToString(), "\" size=\"1\" oindex='", num.ToString(), "'  name=\"seldeftype\">\n" };
                stringBuilder.Append(string.Concat(str));
                if (!(tableField.FieldType == 2 ? false : tableField.FieldType != 3))
                {
                    stringBuilder.Append("\t\t<option value=\"Custom\">自定义</option>\n");
                }
                else if (!(tableField.FieldType == 1 ? false : tableField.FieldType != 5))
                {
                    stringBuilder.Append(this.method_3(tableField.QueryDefaultType));
                }
                else if (tableField.FieldType == 4)
                {
                    stringBuilder.Append(string.Concat("\t\t<option value=\"Custom\"", this.method_0("Custom", tableField.QueryDefaultType), ">自定义</option>\n"));
                    stringBuilder.Append(string.Concat("\t\t<option value=\"QueryToday\"", this.method_0("QueryToday", tableField.QueryDefaultType), ">当天</option>\n"));
                    stringBuilder.Append(string.Concat("\t\t<option value=\"QueryCurWeek\"", this.method_0("QueryCurWeek", tableField.QueryDefaultType), ">当前周</option>\n"));
                    stringBuilder.Append(string.Concat("\t\t<option value=\"QueryCurMonth\"", this.method_0("QueryCurMonth", tableField.QueryDefaultType), ">当前月</option>\n"));
                    stringBuilder.Append(string.Concat("\t\t<option value=\"QueryCurYear\"", this.method_0("QueryCurYear", tableField.QueryDefaultType), ">当前年</option>\n"));
                    stringBuilder.Append(string.Concat("\t\t<option value=\"QueryLastMonth\"", this.method_0("QueryLastMonth", tableField.QueryDefaultType), ">最近一个月</option>\n"));
                    stringBuilder.Append(string.Concat("\t\t<option value=\"QueryLast3Month\"", this.method_0("QueryLast3Month", tableField.QueryDefaultType), ">最近三个月</option>\n"));
                    stringBuilder.Append(string.Concat("\t\t<option value=\"QueryLastYear\"", this.method_0("QueryLastYear", tableField.QueryDefaultType), ">最近一年</option>\n"));
                }
                stringBuilder.Append("\t\t</select></td>\n");
                str = new string[] { "\t<td align=\"center\"><input class='textbox txtdefvalue' id=\"txtdefvalue", num.ToString(), "\"  type=\"text\"  oindex=", num.ToString(), " name=\"txtdefvalue\" value='", tableField.QueryDefaultValue, "'></td>\n" };
                stringBuilder.Append(string.Concat(str));
                str = new string[] { "\t<td align=\"center\"><input class='textbox txtdispstylename' id=\"txtstylename", num.ToString(), "\"  type=\"text\"  oindex=", num.ToString(), " name=\"txtstylename\" value='", tableField.QueryStyleName, "'></td>\n" };
                stringBuilder.Append(string.Concat(str));
                str = new string[] { "\t<td align=\"center\"><input id=\"txtstyletxt", num.ToString(), "\"  type=\"hidden\"  oindex=", num.ToString(), " name=\"txtstyletxt\" value='", Utility.String2Html(tableField.QueryStyleTxt), "'>" };
                stringBuilder.Append(string.Concat(str));
                str = new string[] { "<input id=\"txtstyle", num.ToString(), "\"  type=\"hidden\"  oindex=", num.ToString(), " name=\"txtstyle\" value='", tableField.QueryStyle, "'>\n" };
                stringBuilder.Append(string.Concat(str));
                str = new string[] { "\t<input value=\"选择\"  type=\"button\" id=\"selbtn", num.ToString(), "\"  oindex='", num.ToString(), "' onclick=\"seldispstyle();\" >" };
                stringBuilder.Append(string.Concat(str));
                stringBuilder.Append("</td>\n</tr>\n");
                stringBuilder2.AppendFormat("<td oindex='{0}' fieldid='{1}' state='unchanged'>", num, tableField._AutoID);
                stringBuilder2.AppendFormat("<txtname><![CDATA[{0}]]></txtname>", tableField.FieldName);
                stringBuilder2.AppendFormat("<chkquery><![CDATA[{0}]]></chkquery>", tableField.QueryDisp);
                stringBuilder2.AppendFormat("<chkmatch><![CDATA[{0}]]></chkmatch>", tableField.QueryMatchMode);
                stringBuilder2.AppendFormat("<seldeftype><![CDATA[{0}]]></seldeftype>", tableField.QueryDefaultType);
                stringBuilder2.AppendFormat("<txtdefvalue><![CDATA[{0}]]></txtdefvalue>", tableField.QueryDefaultValue);
                stringBuilder2.AppendFormat("<txtstyle><![CDATA[{0}]]></txtstyle>", tableField.QueryStyle);
                stringBuilder2.AppendFormat("<txtstylename><![CDATA[{0}]]></txtstylename>", tableField.QueryStyleName);
                stringBuilder2.AppendFormat("<txtstyletxt><![CDATA[{0}]]></txtstyletxt>", tableField.QueryStyleTxt);
                stringBuilder2.Append("</td>");
                num++;
            }
            this.fieldHTML = stringBuilder.ToString();
            this.fieldXML = stringBuilder2.ToString();
            this.rowcount = num.ToString();
        }

        private string method_3(string string_0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> defaultValue in ModelDefaults.DefaultValue)
            {
                if (defaultValue.Key != string_0)
                {
                    stringBuilder.AppendFormat("\t\t<option value=\"{0}\"  title=\"{1}\">{1}</option>\n", defaultValue.Key, defaultValue.Value);
                }
                else
                {
                    stringBuilder.AppendFormat("\t\t<option value=\"{0}\"  title=\"{1}\" selected>{1}</option>\n", defaultValue.Key, defaultValue.Value);
                }
            }
            return stringBuilder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.method_2();
            }
            if (base.Request["t"] == "3")
            {
                this.disp = "display:none";
            }
        }
    }
}