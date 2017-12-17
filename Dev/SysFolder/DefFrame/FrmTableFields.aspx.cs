using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class FrmTableFields : AdminPageBase
    {
        public string hidstate = "0";

        public string rowcount = "0";

        public string fieldHTML = "";

        public string fieldXML = "";

        public string tblName = "";

        public string OpInfo = "";

      

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string str;
            XmlElement xmlElement;
            FieldInfo fieldInfo;
            _TableInfo __TableInfo = new _TableInfo(this.tblName);
            int maxOdr = __TableInfo.GetMaxOdr();
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                try
                {
                    _FieldInfo __FieldInfo = new _FieldInfo(dbTransaction);
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(string.Concat("<?xml version='1.0' encoding='utf-8' ?>", base.Request["txtxml"]));
                    XmlElement xmlElement1 = (XmlElement)xmlDocument.SelectSingleNode("tr");
                    string str1 = "td";
                    if (xmlElement1.SelectNodes("TD").Count > 0)
                    {
                        str1 = "TD";
                    }
                    if (base.Request.Form.GetValues("state")[0] != "1")
                    {
                        if ((xmlElement1 == null ? false : xmlElement1.SelectNodes(str1).Count > 0))
                        {
                            stringBuilder.AppendFormat("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[{0}]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)\r\n                            DROP TABLE [dbo].[{0}] \r\n                            CREATE TABLE [dbo].[{0}] (\r\n                            [_AutoID] [varchar] (50) NOT NULL ,\r\n                            [_UserName] [varchar] (50) NOT  NULL ,\r\n                            [_OrgCode] [varchar] (100) NOT  NULL ,\r\n                            [_CreateTime] [datetime] NOT  NULL ,\r\n                            [_UpdateTime] [datetime] NOT  NULL ,\r\n                            [_IsDel] [int] NOT  NULL ,", this.tblName);
                            if (base.GetParaValue("t") == "1")
                            {
                                stringBuilder.Append("[_CompanyID] [varchar] (50) NOT NULL ,");
                                stringBuilder.Append("[_WFState] [varchar] (50) NULL ,");
                                stringBuilder.Append("[_GDState] [varchar] (50) NULL ,");
                            }
                            if (base.GetParaValue("t") == "2")
                            {
                                stringBuilder.Append("[_MainID] [varchar] (50) NOT NULL ,");
                                stringBuilder.Append("[_MainTbl] [varchar] (100) NOT NULL ,");
                            }
                            foreach (XmlNode xmlNodesA in xmlElement1.SelectNodes(string.Concat(str1, "[@state='add']")))
                            {
                                xmlElement = (XmlElement)xmlNodesA;
                                if (xmlElement.GetAttribute("txtname").Trim() == "")
                                {
                                    continue;
                                }
                                fieldInfo = new FieldInfo()
                                {
                                    _AutoID = Guid.NewGuid().ToString(),
                                    _OrgCode = base.OrgCode,
                                    _UserName = base.EmployeeID,
                                    _CreateTime = DateTime.Now,
                                    _UpdateTime = DateTime.Now,
                                    _IsDel = 0,
                                    TableName = this.tblName,
                                    FieldName = xmlElement.GetAttribute("txtname").Trim(),
                                    FieldNameCn = xmlElement.GetAttribute("txtnamecn").Trim(),
                                    FieldType = Convert.ToInt32(xmlElement.GetAttribute("seltype")),
                                    FieldLength = xmlElement.GetAttribute("txtsize").Trim(),
                                    FieldInDisp = 1,
                                    ListDisp = 1,
                                    ColumnWidth = "80"
                                };
                                int num = maxOdr + 1;
                                maxOdr = num;
                                fieldInfo.FieldOdr = num;
                                fieldInfo.ColumnOrder = fieldInfo.FieldOdr;
                                fieldInfo.IsComput = 0;
                                fieldInfo.IsUnique = Convert.ToInt32(xmlElement.GetAttribute("unique"));
                                fieldInfo.FieldNote = xmlElement.GetAttribute("txtnote");
                                __FieldInfo.Add(fieldInfo);
                                str = "";
                                switch (int.Parse(xmlElement.GetAttribute("seltype")))
                                {
                                    case 1:
                                        {
                                            str = string.Concat(" [nvarchar](", xmlElement.GetAttribute("txtsize").Trim(), ") NULL");
                                            break;
                                        }
                                    case 2:
                                        {
                                            str = " [int] NULL";
                                            break;
                                        }
                                    case 3:
                                        {
                                            str = string.Concat(" [numeric](", xmlElement.GetAttribute("txtsize").Trim(), ") NULL");
                                            break;
                                        }
                                    case 4:
                                        {
                                            str = " [datetime] NULL";
                                            break;
                                        }
                                    case 5:
                                        {
                                            str = " [text] NULL";
                                            break;
                                        }
                                    case 6:
                                        {
                                            str = " [image] NULL";
                                            break;
                                        }
                                }
                                stringBuilder.AppendFormat(" [{0}] {1},", fieldInfo.FieldName, str);
                            }
                            stringBuilder.Length = stringBuilder.Length - 1;
                            stringBuilder.AppendFormat(") ON [PRIMARY] \r\n                            ALTER TABLE [dbo].[{0}] ADD \r\n                             CONSTRAINT [PK_{0}] PRIMARY KEY  NONCLUSTERED\r\n                             ([_AutoID])  ON [PRIMARY]\r\n\r\n                            CREATE CLUSTERED INDEX [IX_{0}] ON [dbo].[{0}] \r\n                            (\r\n\t                            [_CreateTime] ASC\r\n                            ) ON [PRIMARY]\r\n                        ", this.tblName);
                            SysDatabase.ExecuteNonQuery(stringBuilder.ToString(), dbTransaction);
                        }
                        __TableInfo.SetUpdateTime();
                    }
                    else
                    {
                        List<FieldInfo> phyFields = __TableInfo.GetPhyFields();
                        if ((xmlElement1 == null ? false : xmlElement1.SelectNodes(str1).Count > 0))
                        {
                            foreach (XmlNode xmlNodes1 in xmlElement1.SelectNodes(string.Concat(str1, "[@state='add']")))
                            {
                                xmlElement = (XmlElement)xmlNodes1;
                                if (xmlElement.GetAttribute("txtname").Trim() == "")
                                {
                                    continue;
                                }
                                fieldInfo = new FieldInfo()
                                {
                                    _AutoID = Guid.NewGuid().ToString(),
                                    _OrgCode = base.OrgCode,
                                    _UserName = base.EmployeeID,
                                    _CreateTime = DateTime.Now,
                                    _UpdateTime = DateTime.Now,
                                    _IsDel = 0,
                                    FieldInDisp = 1,
                                    ColumnWidth = "80",
                                    TableName = this.tblName,
                                    FieldName = xmlElement.GetAttribute("txtname").Trim(),
                                    FieldNameCn = xmlElement.GetAttribute("txtnamecn").Trim(),
                                    FieldType = Convert.ToInt32(xmlElement.GetAttribute("seltype")),
                                    FieldLength = xmlElement.GetAttribute("txtsize").Trim()
                                };
                                int num1 = maxOdr + 1;
                                maxOdr = num1;
                                fieldInfo.FieldOdr = num1;
                                fieldInfo.ListDisp = 1;
                                fieldInfo.ColumnOrder = fieldInfo.FieldOdr;
                                fieldInfo.IsComput = 0;
                                fieldInfo.IsUnique = Convert.ToInt32(xmlElement.GetAttribute("unique"));
                                fieldInfo.FieldNote = xmlElement.GetAttribute("txtnote");
                                __FieldInfo.Add(fieldInfo);
                                str = "";
                                switch (int.Parse(xmlElement.GetAttribute("seltype")))
                                {
                                    case 1:
                                        {
                                            str = string.Concat(" [nvarchar](", xmlElement.GetAttribute("txtsize").Trim(), ") NULL");
                                            break;
                                        }
                                    case 2:
                                        {
                                            str = " [int] NULL";
                                            break;
                                        }
                                    case 3:
                                        {
                                            str = string.Concat(" [numeric](", xmlElement.GetAttribute("txtsize").Trim(), ") NULL");
                                            break;
                                        }
                                    case 4:
                                        {
                                            str = " [datetime] NULL";
                                            break;
                                        }
                                    case 5:
                                        {
                                            str = " [text] NULL";
                                            break;
                                        }
                                    case 6:
                                        {
                                            str = " [image] NULL";
                                            break;
                                        }
                                }
                                stringBuilder.AppendFormat("alter table {0} add {1} {2};", this.tblName, fieldInfo.FieldName, str);
                            }
                            foreach (XmlNode xmlNodes2 in xmlElement1.SelectNodes(string.Concat(str1, "[@state='changed']")))
                            {
                                XmlElement xmlElement2 = (XmlElement)xmlNodes2;
                                if (xmlElement2.GetAttribute("txtname").Trim() == "")
                                {
                                    continue;
                                }
                                FieldInfo model = __FieldInfo.GetModel(xmlElement2.GetAttribute("fieldid"));
                                model._UpdateTime = DateTime.Now;
                                model.FieldNameCn = xmlElement2.GetAttribute("txtnamecn");
                                model.FieldType = Convert.ToInt32(xmlElement2.GetAttribute("seltype"));
                                model.FieldLength = xmlElement2.GetAttribute("txtsize").Trim();
                                model.IsUnique = Convert.ToInt32(xmlElement2.GetAttribute("unique"));
                                model.FieldNote = xmlElement2.GetAttribute("txtnote");
                                __FieldInfo.Update(model);
                                int fieldType = phyFields.Find((FieldInfo fieldInfo_0) => fieldInfo_0._AutoID == xmlElement2.GetAttribute("fieldid")).FieldType;
                                string str2 = fieldType.ToString();
                                string fieldLength = phyFields.Find((FieldInfo fieldInfo_0) => fieldInfo_0._AutoID == xmlElement2.GetAttribute("fieldid")).FieldLength;
                                if (str2 == xmlElement2.GetAttribute("seltype"))
                                {
                                    if (fieldLength == xmlElement2.GetAttribute("txtsize").Trim())
                                    {
                                        continue;
                                    }
                                    str = "";
                                    switch (int.Parse(xmlElement2.GetAttribute("seltype")))
                                    {
                                        case 1:
                                            {
                                                str = string.Concat(" [nvarchar](", xmlElement2.GetAttribute("txtsize").Trim(), ") NULL");
                                                goto case 2;
                                            }
                                        case 2:
                                            {
                                                stringBuilder1.AppendFormat("alter table {0} alter column {1} {2};", this.tblName, xmlElement2.GetAttribute("txtname"), str);
                                                continue;
                                            }
                                        case 3:
                                            {
                                                str = string.Concat(" [numeric](", xmlElement2.GetAttribute("txtsize").Trim(), ") NULL");
                                                goto case 2;
                                            }
                                        default:
                                            {
                                                goto case 2;
                                            }
                                    }
                                }
                                else
                                {
                                    str = "";
                                    switch (int.Parse(xmlElement2.GetAttribute("seltype")))
                                    {
                                        case 1:
                                            {
                                                str = string.Concat(" [nvarchar](", xmlElement2.GetAttribute("txtsize").Trim(), ") NULL");
                                                break;
                                            }
                                        case 2:
                                            {
                                                str = " [int] NULL";
                                                break;
                                            }
                                        case 3:
                                            {
                                                str = string.Concat(" [numeric](", xmlElement2.GetAttribute("txtsize").Trim(), ") NULL");
                                                break;
                                            }
                                        case 4:
                                            {
                                                str = " [datetime] NULL";
                                                break;
                                            }
                                        case 5:
                                            {
                                                str = " [text] NULL";
                                                break;
                                            }
                                        case 6:
                                            {
                                                str = " [image] NULL";
                                                break;
                                            }
                                    }
                                    stringBuilder1.AppendFormat("alter table {0} alter column {1} {2};", this.tblName, xmlElement2.GetAttribute("txtname"), str);
                                }
                            }
                            foreach (XmlNode xmlNodes3 in xmlElement1.SelectNodes(string.Concat(str1, "[@state='deleted']")))
                            {
                                xmlElement = (XmlElement)xmlNodes3;
                                __FieldInfo.Delete(xmlElement.GetAttribute("fieldid"));
                                stringBuilder2.AppendFormat("alter table {0} drop column {1};", this.tblName, xmlElement.GetAttribute("txtname"));
                            }
                            if (stringBuilder.Length > 0)
                            {
                                SysDatabase.ExecuteNonQuery(stringBuilder.ToString(), dbTransaction);
                            }
                            if (stringBuilder1.Length > 0)
                            {
                                SysDatabase.ExecuteNonQuery(stringBuilder1.ToString(), dbTransaction);
                            }
                            if (stringBuilder2.Length > 0)
                            {
                                SysDatabase.ExecuteNonQuery(stringBuilder2.ToString(), dbTransaction);
                            }
                        }
                        __TableInfo.SetUpdateTime();
                    }
                    dbTransaction.Commit();
                    base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "success();", true);
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    dbTransaction.Rollback();
                    this.OpInfo = string.Concat("<div id='errorInfo' class='tip'>", exception.Message, "</div>");
                }
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
            this.method_2();
        }

        private string method_0(string val1, string val2)
        {
            return (val1 != val2 ? "" : "checked");
        }

        private string method_1(string val1, string val2)
        {
            return (val1 != val2 ? "" : "selected");
        }

        private void method_2()
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder();
            List<FieldInfo> tablePhyFields = (new _FieldInfo()).GetTablePhyFields(this.tblName);
            if (tablePhyFields.Count > 0)
            {
                this.hidstate = "1";
                int num = 1;
                foreach (FieldInfo tablePhyField in tablePhyFields)
                {
                    stringBuilder.Append("<tr>\n");
                    stringBuilder.AppendFormat("\t<td align=\"center\"><input class='form-control input-sm txtname' readonly='readonly' id=\"txtname{0}\"  type=\"text\" oindex=\"{0}\" name=\"txtname\" value='{1}'></td>\n", num, tablePhyField.FieldName);
                    stringBuilder.AppendFormat("\t<td align=\"center\"><input class='form-control input-sm txtnamecn' id=\"txtnamecn{0}\"  type=\"text\"  oindex=\"{0}\" name=\"txtnamecn\" value='{1}'></td>\n", num, tablePhyField.FieldNameCn);
                    stringBuilder.AppendFormat("\t<td align=\"center\"><select class='form-control input-sm' id=\"seltype{0}\" size=\"1\" oindex=\"{0}\" onchange='javascript:checkit(this)' name=\"seltype\">\n", num);
                    int fieldType = tablePhyField.FieldType;
                    stringBuilder.Append(string.Concat("\t\t<option value=\"1\"", this.method_1("1", fieldType.ToString()), ">字符</option>\n"));
                    fieldType = tablePhyField.FieldType;
                    stringBuilder.Append(string.Concat("\t\t<option value=\"2\"", this.method_1("2", fieldType.ToString()), ">整数</option>\n"));
                    fieldType = tablePhyField.FieldType;
                    stringBuilder.Append(string.Concat("\t\t<option value=\"3\"", this.method_1("3", fieldType.ToString()), ">数值</option>\n"));
                    fieldType = tablePhyField.FieldType;
                    stringBuilder.Append(string.Concat("\t\t<option value=\"4\"", this.method_1("4", fieldType.ToString()), ">日期</option>\n"));
                    fieldType = tablePhyField.FieldType;
                    stringBuilder.Append(string.Concat("\t\t<option value=\"5\"", this.method_1("5", fieldType.ToString()), ">大文本</option>\n"));
                    stringBuilder.Append("\t\t</select></td>\n");
                    stringBuilder.AppendFormat("\t<td align=\"center\"><input class='form-control input-sm txtsize' id=\"txtsize{0}\"  type=\"text\"  oindex=\"{0}\" name=\"txtsize\" value='{1}'></td>\n", num, tablePhyField.FieldLength);
                    object obj = num;
                    fieldType = tablePhyField.IsUnique;
                    stringBuilder.AppendFormat("\t<td align=\"center\"><input id=\"unique{0}\" type=\"checkbox\" oindex=\"{0}\" {1}  name=\"unique\">\n", obj, this.method_0("1", fieldType.ToString()));
                    stringBuilder.AppendFormat("\t<td align=\"center\"><input class='form-control input-sm  txtnote' id=\"txtnote{0}\"  type=\"text\"  oindex=\"{0}\" name=\"txtnote\" value='{1}'></td>\n", num, Utility.String2Html(tablePhyField.FieldNote));
                    stringBuilder.Append(string.Concat("\t<td align=\"center\"><input type=\"button\" class='btn btn-link btn-sm' value=\"删除\" onclick=\"dropfield('", num.ToString(), "')\"></td>\n"));
                    stringBuilder.Append("</tr>\n");
                    object[] fieldName = new object[] { num, tablePhyField.FieldName, tablePhyField.FieldNameCn, tablePhyField.FieldType, tablePhyField.FieldLength, tablePhyField._AutoID, tablePhyField.IsUnique, Utility.String2Html(tablePhyField.FieldNote) };
                    stringBuilder1.AppendFormat("<td oindex='{0}' txtname='{1}' txtnamecn='{2}' seltype='{3}' txtsize='{4}' fieldid='{5}' unique='{6}' txtnote='{7}' state='unchanged'/>", fieldName);
                    num++;
                }
                this.rowcount = num.ToString();
                this.fieldHTML = stringBuilder.ToString();
                this.fieldXML = stringBuilder1.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.tblName = base.GetParaValue("tblname");
            if (!base.IsPostBack)
            {
                this.method_2();
            }
        }
    }
}