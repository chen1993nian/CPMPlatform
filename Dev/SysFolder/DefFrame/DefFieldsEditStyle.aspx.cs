using EIS.AppBase;
using EIS.AppModel;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;


namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefFieldsEditStyle : AdminPageBase
    {
       

        public string fieldHTML = "";

        public string fieldHTML2 = "";

        public string fieldXML = "";

        public string tblName = "";


        protected void ddlStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.method_2();
        }

        //保存
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            XmlNode xmlNodes = null;
            XmlNode xmlNodes1;
            this.tblName = base.GetParaValue("tblName");
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(string.Concat("<?xml version='1.0' encoding='utf-8' ?>", this.txtxml.Text));
                _FieldInfo __FieldInfo = new _FieldInfo(dbTransaction);
                _FieldStyle __FieldStyle = new _FieldStyle(dbTransaction);
                XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode("root");
                if ((xmlElement == null ? false : xmlElement.SelectNodes("td").Count > 0))
                {
                    string selectedValue = this.ddlStyle.SelectedValue;
                    if (selectedValue != "")
                    {
                        foreach (XmlNode xmlNodesA in xmlElement.SelectNodes("td[@fieldid='']"))
                        {
                            XmlElement xmlElement1 = (XmlElement)xmlNodesA;
                            FieldStyle fieldStyle = new FieldStyle()
                            {
                                _AutoID = Guid.NewGuid().ToString(),
                                _OrgCode = base.OrgCode,
                                _UserName = base.EmployeeID,
                                _CreateTime = DateTime.Now,
                                _UpdateTime = DateTime.Now,
                                _IsDel = 0,
                                TableName = this.tblName,
                                FieldName = xmlNodes.SelectSingleNode("txtname").InnerText
                            };
                            xmlNodes1 = xmlNodes.SelectSingleNode("chkindisp");
                            fieldStyle.FieldInDisp = Convert.ToInt32(xmlNodes1.InnerText);
                            fieldStyle.FieldRead = Convert.ToInt32(xmlNodes.SelectSingleNode("chkread").InnerText);
                            fieldStyle.FieldNull = Convert.ToInt32(xmlNodes.SelectSingleNode("chknull").InnerText);
                            fieldStyle.FieldWidth = xmlNodes.SelectSingleNode("txtfieldw").InnerText;
                            fieldStyle.FieldHeight = xmlNodes.SelectSingleNode("txtfieldh").InnerText;
                            fieldStyle.FieldInDispStyle = xmlNodes.SelectSingleNode("txtdispstyle").InnerText;
                            fieldStyle.FieldInDispStyleName = xmlNodes.SelectSingleNode("txtdispstylename").InnerText;
                            fieldStyle.FieldInDispStyleTxt = xmlNodes.SelectSingleNode("txtdispstyletxt").InnerText;
                            fieldStyle.FieldDValueType = xmlNodes.SelectSingleNode("txtdeftype").InnerText;
                            fieldStyle.FieldDValue = xmlNodes.SelectSingleNode("txtdefvalue").InnerText;
                            fieldStyle.StyleIndex = Convert.ToInt32(selectedValue);
                            __FieldStyle.Add(fieldStyle);
                        }
                        foreach (XmlNode xmlNodes2 in xmlElement.SelectNodes("td[@state='changed']"))
                        {
                            string attribute = ((XmlElement)xmlNodes2).GetAttribute("fieldid");
                            if (attribute == "")
                            {
                                continue;
                            }
                            FieldStyle model = __FieldStyle.GetModel(attribute);
                            model._UpdateTime = DateTime.Now;
                            xmlNodes1 = xmlNodes2.SelectSingleNode("chkindisp");
                            model.FieldInDisp = Convert.ToInt32(xmlNodes1.InnerText);
                            model.FieldRead = Convert.ToInt32(xmlNodes2.SelectSingleNode("chkread").InnerText);
                            model.FieldNull = Convert.ToInt32(xmlNodes2.SelectSingleNode("chknull").InnerText);
                            model.FieldWidth = xmlNodes2.SelectSingleNode("txtfieldw").InnerText;
                            model.FieldHeight = xmlNodes2.SelectSingleNode("txtfieldh").InnerText;
                            model.FieldInDispStyle = xmlNodes2.SelectSingleNode("txtdispstyle").InnerText;
                            model.FieldInDispStyleName = xmlNodes2.SelectSingleNode("txtdispstylename").InnerText;
                            model.FieldInDispStyleTxt = xmlNodes2.SelectSingleNode("txtdispstyletxt").InnerText;
                            model.FieldDValueType = xmlNodes2.SelectSingleNode("txtdeftype").InnerText;
                            model.FieldDValue = xmlNodes2.SelectSingleNode("txtdefvalue").InnerText;
                            __FieldStyle.Update(model);
                        }
                    }
                    else
                    {
                        foreach (XmlNode xmlNodes3 in xmlElement.SelectNodes("td[@state='changed']"))
                        {
                            FieldInfo now = __FieldInfo.GetModel(((XmlElement)xmlNodes3).GetAttribute("fieldid"));
                            now._UpdateTime = DateTime.Now;
                            xmlNodes1 = xmlNodes3.SelectSingleNode("chkindisp");
                            now.FieldInDisp = Convert.ToInt32(xmlNodes1.InnerText);
                            now.FieldRead = Convert.ToInt32(xmlNodes3.SelectSingleNode("chkread").InnerText);
                            now.FieldNull = Convert.ToInt32(xmlNodes3.SelectSingleNode("chknull").InnerText);
                            now.FieldWidth = xmlNodes3.SelectSingleNode("txtfieldw").InnerText;
                            now.FieldHeight = xmlNodes3.SelectSingleNode("txtfieldh").InnerText;
                            now.FieldInDispStyle = xmlNodes3.SelectSingleNode("txtdispstyle").InnerText;
                            now.FieldInDispStyleName = xmlNodes3.SelectSingleNode("txtdispstylename").InnerText;
                            now.FieldInDispStyleTxt = xmlNodes3.SelectSingleNode("txtdispstyletxt").InnerText;
                            now.FieldDValueType = xmlNodes3.SelectSingleNode("txtdeftype").InnerText;
                            now.FieldDValue = xmlNodes3.SelectSingleNode("txtdefvalue").InnerText;
                            __FieldInfo.Update(now);
                        }
                    }
                }
                (new _TableInfo(this.tblName, dbTransaction)).SetUpdateTime();
                dbTransaction.Commit();
                base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>$.noticeAdd({text:'保存成功！',stay:false});</script>");
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                dbTransaction.Rollback();
                this.fileLogger.Error<Exception>(exception);
                base.Response.Write(string.Concat("出现错误:", exception.ToString()));
            }
            this.method_2();
        }

        //新建风格
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            int maxIndex = _FieldStyle.GetMaxIndex(this.tblName) + 1;
            _TableInfo __TableInfo = new _TableInfo(this.tblName);
            List<FieldInfo> tablePhyFields = (new _FieldInfo()).GetTablePhyFields(this.tblName);
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                _FieldStyle __FieldStyle = new _FieldStyle();
                foreach (FieldInfo tablePhyField in tablePhyFields)
                {
                    FieldStyle fieldStyle = new FieldStyle()
                    {
                        _AutoID = Guid.NewGuid().ToString(),
                        _OrgCode = base.OrgCode,
                        _UserName = base.EmployeeID,
                        _CreateTime = DateTime.Now,
                        _UpdateTime = DateTime.Now,
                        _IsDel = 0,
                        TableName = this.tblName,
                        FieldName = tablePhyField.FieldName,
                        FieldInDisp = tablePhyField.FieldInDisp,
                        FieldRead = tablePhyField.FieldRead,
                        FieldNull = tablePhyField.FieldNull,
                        FieldWidth = tablePhyField.FieldWidth,
                        FieldHeight = tablePhyField.FieldHeight,
                        FieldDValueType = tablePhyField.FieldDValueType,
                        FieldDValue = tablePhyField.FieldDValue,
                        FieldInDispStyle = tablePhyField.FieldInDispStyle,
                        FieldInDispStyleTxt = tablePhyField.FieldInDispStyleTxt,
                        FieldInDispStyleName = tablePhyField.FieldInDispStyleName,
                        StyleIndex = maxIndex
                    };
                    __FieldStyle.Add(fieldStyle);
                }
                __TableInfo.SetUpdateTime();
                dbTransaction.Commit();
                this.method_0(maxIndex);
                this.method_2();
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
            this.ddlStyle.Items.Clear();
            ListItem listItem = new ListItem("默认风格", "");
            this.ddlStyle.Items.Add(listItem);
            if (int_0 == 0)
            {
                this.ddlStyle.SelectedIndex = 0;
            }
            int maxIndex = _FieldStyle.GetMaxIndex(this.tblName);
            for (int i = 1; i <= maxIndex; i++)
            {
                listItem = new ListItem(string.Concat("风格", i.ToString()), i.ToString())
                {
                    Selected = int_0 == i
                };
                this.ddlStyle.Items.Add(listItem);
            }
        }

        private string method_1(string val1, string val2)
        {
            return (val1 != val2 ? "" : "checked");
        }

        private void method_2()
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder();
            string selectedValue = this.ddlStyle.SelectedValue;
            _FieldInfo __FieldInfo = new _FieldInfo();
            List<FieldInfo> fieldInfos = new List<FieldInfo>();
            if (selectedValue != "")
            {
                _FieldStyle __FieldStyle = new _FieldStyle();
                List<FieldStyle> tableFields = __FieldStyle.GetTableFields(this.tblName, int.Parse(selectedValue));
                fieldInfos = __FieldInfo.GetFieldsStyleMerged(this.tblName, int.Parse(selectedValue));
                foreach (FieldInfo fieldInfo in fieldInfos)
                {
                    if (tableFields.FindIndex((FieldStyle fieldStyle_0) => fieldStyle_0.FieldName == fieldInfo.FieldName) > -1)
                    {
                        continue;
                    }
                    FieldStyle fieldStyle = new FieldStyle()
                    {
                        _AutoID = Guid.NewGuid().ToString(),
                        _OrgCode = base.OrgCode,
                        _UserName = base.EmployeeID,
                        _CreateTime = DateTime.Now,
                        _UpdateTime = DateTime.Now,
                        _IsDel = 0,
                        TableName = this.tblName,
                        FieldName = fieldInfo.FieldName,
                        FieldInDisp = fieldInfo.FieldInDisp,
                        FieldRead = fieldInfo.FieldRead,
                        FieldNull = fieldInfo.FieldNull,
                        FieldWidth = fieldInfo.FieldWidth,
                        FieldHeight = fieldInfo.FieldHeight,
                        FieldDValueType = fieldInfo.FieldDValueType,
                        FieldDValue = fieldInfo.FieldDValue,
                        FieldInDispStyle = fieldInfo.FieldInDispStyle,
                        FieldInDispStyleTxt = fieldInfo.FieldInDispStyleTxt,
                        FieldInDispStyleName = fieldInfo.FieldInDispStyleName,
                        StyleIndex = int.Parse(selectedValue)
                    };
                    __FieldStyle.Add(fieldStyle);
                    fieldInfo.FieldStyleId = fieldStyle._AutoID;
                }
            }
            else
            {
                fieldInfos = __FieldInfo.GetTablePhyFields(this.tblName);
            }
            int num = 1;
            foreach (FieldInfo fieldInfo1 in fieldInfos)
            {
                stringBuilder.Append("<tr>\n");
                int fieldOdr = fieldInfo1.FieldOdr;
                stringBuilder.Append(string.Concat("\t<td align=\"center\">", fieldOdr.ToString(), "</td>\n"));
                stringBuilder.Append(string.Concat("\t<td align=\"left\">", fieldInfo1.FieldName, "</td>\n"));
                stringBuilder.Append(string.Concat("\t<td align=\"left\">", fieldInfo1.FieldNameCn, "</td>\n"));
                string[] str = new string[] { "\t<td align=\"center\"><input id=\"chkindisp", num.ToString(), "\" type=\"checkbox\" oindex='", num.ToString(), "' ", null, null };
                fieldOdr = fieldInfo1.FieldInDisp;
                str[5] = this.method_1("1", fieldOdr.ToString());
                str[6] = "  name=\"chkindisp\">\n";
                stringBuilder.Append(string.Concat(str));
                str = new string[] { "\t<td align=\"center\"><input id=\"chkread", num.ToString(), "\" type=\"checkbox\" oindex='", num.ToString(), "' ", null, null };
                fieldOdr = fieldInfo1.FieldRead;
                str[5] = this.method_1("1", fieldOdr.ToString());
                str[6] = "  name=\"chkread\">\n";
                stringBuilder.Append(string.Concat(str));
                str = new string[] { "\t<td align=\"center\"><input id=\"chknull", num.ToString(), "\" type=\"checkbox\" oindex='", num.ToString(), "' ", null, null };
                fieldOdr = fieldInfo1.FieldNull;
                str[5] = this.method_1("1", fieldOdr.ToString());
                str[6] = "  name=\"chknull\">\n";
                stringBuilder.Append(string.Concat(str));
                str = new string[] { "\t<td align=\"center\"><input class='textbox txtfieldw' id=\"txtfieldw", num.ToString(), "\"  type=\"text\"  oindex=", num.ToString(), " name=\"txtfieldw\" value='", fieldInfo1.FieldWidth, "'></td>\n" };
                stringBuilder.Append(string.Concat(str));
                str = new string[] { "\t<td align=\"center\"><input class='textbox txtfieldh' id=\"txtfieldh", num.ToString(), "\"  type=\"text\"  oindex=", num.ToString(), " name=\"txtfieldh\" value='", fieldInfo1.FieldHeight, "'></td>\n" };
                stringBuilder.Append(string.Concat(str));
                stringBuilder.Append("\t<td>");
                if (fieldInfo1.FieldInDispStyleName != "")
                {
                    stringBuilder.AppendFormat("<a href=\"javascript:\" class=\"selbtn\" id='selbtn{0}' oindex='{0}' >{1}</a>", num, fieldInfo1.FieldInDispStyleName);
                }
                else
                {
                    stringBuilder.AppendFormat("<a href=\"javascript:\" class=\"selbtn\" id='selbtn{0}' oindex='{0}' >【选择】</a>", num);
                }
                str = new string[] { "    <input class='txtdispstylename' id=\"txtdispstylename", num.ToString(), "\"  type=\"hidden\"  oindex=", num.ToString(), " name=\"txtdispstylename\" value='", fieldInfo1.FieldInDispStyleName, "'>" };
                stringBuilder.Append(string.Concat(str));
                str = new string[] { "\t<input class='txtdispstyletxt' id=\"txtdispstyletxt", num.ToString(), "\"  type=\"hidden\"  oindex=", num.ToString(), " name=\"txtdispstyletxt\" value='", this.method_3(fieldInfo1.FieldInDispStyleTxt), "'>" };
                stringBuilder.Append(string.Concat(str));
                str = new string[] { "\t<input class='hidden' id=\"txtdispstyle", num.ToString(), "\"  type=\"hidden\"  oindex=", num.ToString(), " name=\"txtdispstyle\" value='", fieldInfo1.FieldInDispStyle, "'>\n" };
                stringBuilder.Append(string.Concat(str));
                stringBuilder.Append("\t</td>\n");
                stringBuilder.Append("<td align=\"center\">");
                if (!(fieldInfo1.FieldDValueType == "" ? true : !(fieldInfo1.FieldDValueType != "0")))
                {
                    stringBuilder.AppendFormat("<a id=\"defbtn{0}\" href=\"javascript:\" class=\"defbtn\"  oindex='{0}' >{1}</a>", num, ModelDefaults.DefaultValue[fieldInfo1.FieldDValueType]);
                }
                else if (fieldInfo1.FieldDValue == "")
                {
                    stringBuilder.AppendFormat("<a id=\"defbtn{0}\" href=\"javascript:\" class=\"defbtn\"  oindex='{0}' >【设置】</a>", num);
                }
                else
                {
                    stringBuilder.AppendFormat("<a id=\"defbtn{0}\" href=\"javascript:\" class=\"defbtn\"  oindex='{0}' >{1}</a>", num, fieldInfo1.FieldDValue);
                }
                stringBuilder.AppendFormat("<input id=\"txtdeftype{0}\"  type=\"hidden\"  oindex=\"{0}\" name=\"txtdeftype\" value='{1}'>", num, fieldInfo1.FieldDValueType);
                stringBuilder.AppendFormat("<input id=\"txtdefvalue{0}\"  type=\"hidden\"  oindex=\"{0}\" name=\"txtdefvalue\" value='{1}'>", num, fieldInfo1.FieldDValue);
                stringBuilder.Append("</td>\n");
                stringBuilder.Append("</tr>\n");
                stringBuilder1.AppendFormat("<td oindex='{0}' fieldid='{1}' state='unchanged'>", num, (selectedValue == "" ? fieldInfo1._AutoID : fieldInfo1.FieldStyleId));
                stringBuilder1.AppendFormat("<txtname><![CDATA[{0}]]></txtname>", fieldInfo1.FieldName);
                stringBuilder1.AppendFormat("<chkindisp><![CDATA[{0}]]></chkindisp>", fieldInfo1.FieldInDisp);
                stringBuilder1.AppendFormat("<chkread><![CDATA[{0}]]></chkread>", fieldInfo1.FieldRead);
                stringBuilder1.AppendFormat("<chknull><![CDATA[{0}]]></chknull>", fieldInfo1.FieldNull);
                stringBuilder1.AppendFormat("<txtdeftype><![CDATA[{0}]]></txtdeftype>", fieldInfo1.FieldDValueType);
                stringBuilder1.AppendFormat("<txtdefvalue>{0}</txtdefvalue>", this.method_3(fieldInfo1.FieldDValue));
                stringBuilder1.AppendFormat("<txtfieldw><![CDATA[{0}]]></txtfieldw>", fieldInfo1.FieldWidth);
                stringBuilder1.AppendFormat("<txtfieldh><![CDATA[{0}]]></txtfieldh>", fieldInfo1.FieldHeight);
                stringBuilder1.AppendFormat("<txtdispstyle><![CDATA[{0}]]></txtdispstyle>", fieldInfo1.FieldInDispStyle);
                stringBuilder1.AppendFormat("<txtdispstylename><![CDATA[{0}]]></txtdispstylename>", fieldInfo1.FieldInDispStyleName);
                stringBuilder1.AppendFormat("<txtdispstyletxt>{0}</txtdispstyletxt>", this.method_3(fieldInfo1.FieldInDispStyleTxt));
                stringBuilder1.AppendFormat("<txtfilter><![CDATA[{0}]]></txtfilter>", fieldInfo1.FieldInFilter);
                stringBuilder1.Append("</td>");
                num++;
            }
            this.fieldHTML2 = stringBuilder.ToString();
            this.fieldXML = stringBuilder1.ToString();
        }

        private string method_3(string string_0)
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
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefFieldsEditStyle));
            this.tblName = base.GetParaValue("tblName");
            if (!base.IsPostBack)
            {
                this.method_0(0);
                this.method_2();
            }
        }
    }
}