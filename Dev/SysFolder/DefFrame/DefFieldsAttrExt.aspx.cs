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
using EIS.Studio;
using EIS.WebBase;
using WebBase.JZY.Tools;
namespace EIS.Studio.SysFolder.DefFrame
{
    public  partial class DefFieldsAttrExt : AdminPageBase
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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DbDataReader dbDataReaders;
            XmlElement xmlElement;
            string str;
            string attribute;
            _TableInfo __TableInfo = new _TableInfo(this.tblName);
            TableInfo model = __TableInfo.GetModel();
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
            string selectedValue = this.DropDownList1.SelectedValue;
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                _FieldInfoExt __FieldInfoExt = new _FieldInfoExt(dbTransaction);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(string.Concat("<?xml version='1.0' encoding='utf-8' ?>", base.Request["txtxml"]));
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
                        __FieldInfoExt.Update(now);
                    }
                }
                foreach (XmlNode xmlNodes1 in xmlElement1.SelectNodes(string.Concat(str2, "[@state='add']")))
                {
                    xmlElement = (XmlElement)xmlNodes1;
                    attribute = xmlElement.GetAttribute("txtname");
                    str = "1";
                    str = (!dataTable.Columns.Contains("DataTypeName") ? this.TransOracleType(dataTable.Select(string.Concat("ColumnName='", attribute, "'"))[0]["DataType"].ToString()) : this.TransType(dataTable.Select(string.Concat("ColumnName='", attribute, "'"))[0]["DataTypeName"].ToString()));
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
                        StyleIndex = Convert.ToInt32(selectedValue)
                    };
                    __FieldInfoExt.Add(fieldInfoExt);
                }
                foreach (XmlNode xmlNodes2 in xmlElement1.SelectNodes(string.Concat(str2, "[@state='deleted']")))
                {
                    xmlElement = (XmlElement)xmlNodes2;
                    __FieldInfoExt.Delete(xmlElement.GetAttribute("fieldid"));
                }
                __TableInfo.SetUpdateTime();
                dbTransaction.Commit();
                this.method_3();
                base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>$.noticeAdd({text:'保存成功！',stay:false});</script>");
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                dbTransaction.Rollback();
                base.Response.Write(string.Concat("出现错误:", exception.ToString()));
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            DbDataReader dbDataReaders;
            _TableInfo __TableInfo = new _TableInfo(this.tblName);
            TableInfo model = __TableInfo.GetModel();
            string str = "";
            str = (string.IsNullOrEmpty(model.ListSQL) ? string.Concat("select * from ", this.tblName) : model.ListSQL);
            DataTable dataTable = new DataTable();
            if (model.ConnectionId == "")
            {
                dbDataReaders = SysDatabase.ExecuteReader(str.Replace("|^condition^|", " 1=1 ").Replace("|^sortdir^|", ""));
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
                dbDataReaders = customDb.ExecuteReader(str.Replace("|^condition^|", " 1=1 ").Replace("|^sortdir^|", ""));
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
            int num = 0;
            StringBuilder stringBuilder = new StringBuilder();
            int maxIndex = _FieldInfoExt.GetMaxIndex(this.tblName) + 1;
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(string.Concat("<?xml version='1.0' encoding='utf-8' ?>", base.Request["txtxml"]));
                _FieldInfoExt __FieldInfoExt = new _FieldInfoExt(dbTransaction);
                XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode("tr");
                string str1 = "td";
                if (xmlElement.SelectNodes("TD").Count > 0)
                {
                    str1 = "TD";
                }
                foreach (XmlNode xmlNodes in xmlElement.SelectNodes(str1))
                {
                    XmlElement xmlElement1 = (XmlElement)xmlNodes;
                    string attribute = xmlElement1.GetAttribute("txtname");
                    string str2 = this.TransType(dataTable.Select(string.Concat("ColumnName='", attribute, "'"))[0]["DataTypeName"].ToString());
                    FieldInfoExt fieldInfoExt = new FieldInfoExt()
                    {
                        _AutoID = Guid.NewGuid().ToString(),
                        _OrgCode = base.OrgCode,
                        _UserName = base.EmployeeID,
                        _CreateTime = DateTime.Now,
                        _UpdateTime = DateTime.Now,
                        _IsDel = 0,
                        TableName = this.tblName,
                        FieldName = xmlElement1.GetAttribute("txtname"),
                        FieldNameCn = xmlElement1.GetAttribute("txtnamecn"),
                        FieldType = Convert.ToInt32(str2),
                        ListDisp = Convert.ToInt32(xmlElement1.GetAttribute("chklistdisp")),
                        QueryDisp = Convert.ToInt32(xmlElement1.GetAttribute("chkquerydisp")),
                        ColumnAlign = xmlElement1.GetAttribute("selalign").Trim(),
                        ColumnWidth = xmlElement1.GetAttribute("txtwidth").Trim(),
                        ColumnRender = xmlElement1.GetAttribute("txtrender"),
                        ColumnHidden = 0,
                        ColFormatExp = xmlElement1.GetAttribute("txtformatexp"),
                        ColTotalExp = xmlElement1.GetAttribute("txttotalexp")
                    };
                    int num1 = num + 1;
                    num = num1;
                    fieldInfoExt.ColumnOrder = num1;
                    fieldInfoExt.StyleIndex = Convert.ToInt32(maxIndex);
                    __FieldInfoExt.Add(fieldInfoExt);
                }
                __TableInfo.SetUpdateTime();
                dbTransaction.Commit();
                this.method_0(maxIndex);
                this.method_3();
                base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>$.noticeAdd({text:'保存成功！',stay:false});</script>");
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
            int maxIndex = _FieldInfoExt.GetMaxIndex(this.tblName);
            for (int i = 1; i <= maxIndex; i++)
            {
                ListItem listItem = new ListItem(string.Concat("样式", i.ToString()), i.ToString())
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
            DbDataReader dbDataReaders;
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            TableInfo model = (new _TableInfo(this.tblName)).GetModel();
            string str = "";
            str = (string.IsNullOrEmpty(model.ListSQL) ? string.Concat("select * from ", this.tblName) : model.ListSQL);
            DataTable dataTable = new DataTable();
            if (model.ConnectionId == "")
            {
                dbDataReaders = SysDatabase.ExecuteReader(str.Replace("|^condition^|", " 1=1 ").Replace("|^sortdir^|", ""));
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
                dbDataReaders = customDb.ExecuteReader(str.Replace("|^condition^|", " 1=1 ").Replace("|^sortdir^|", ""));
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
            _FieldInfoExt __FieldInfoExt = new _FieldInfoExt();
            int num = Convert.ToInt32((this.DropDownList1.SelectedValue == "" ? "0" : this.DropDownList1.SelectedValue));
            List<FieldInfoExt> tableFields = __FieldInfoExt.GetTableFields(this.tblName, num);
            int num1 = 1;
            if (tableFields.Count > 0)
            {
                foreach (FieldInfoExt tableField in tableFields)
                {
                    stringBuilder.Append("<tr>\n");
                    stringBuilder.Append(string.Concat("\t<td align=\"center\">", num1.ToString(), "</td>\n"));
                    string[] strArrays = new string[] { "\t<td align=\"center\"><select id=\"txtname", num1.ToString(), "\" size=\"1\" oindex='", num1.ToString(), "'  name=\"txtname\">\n" };
                    stringBuilder.Append(string.Concat(strArrays));
                    stringBuilder.Append(this.fieldHTML2.Replace(string.Concat("value='", tableField.FieldName, "'"), string.Concat("value='", tableField.FieldName, "' selected")));
                    stringBuilder.Append("</select></td>\n");
                    strArrays = new string[] { "\t<td align=\"center\"><input class='textbox' id=\"txtnamecn", num1.ToString(), "\"  type=\"text\"  oindex=", num1.ToString(), " name=\"txtnamecn\" value='", tableField.FieldNameCn, "'></td>\n" };
                    stringBuilder.Append(string.Concat(strArrays));
                    strArrays = new string[] { "\t<td align=\"center\"><input id=\"chklistdisp", num1.ToString(), "\" type=\"checkbox\" oindex='", num1.ToString(), "' ", null, null };
                    int listDisp = tableField.ListDisp;
                    strArrays[5] = this.method_2("1", listDisp.ToString());
                    strArrays[6] = "  name=\"chklistdisp\">\n";
                    stringBuilder.Append(string.Concat(strArrays));
                    strArrays = new string[] { "\t<td align=\"center\"><input id=\"chkquerydisp", num1.ToString(), "\" type=\"checkbox\" oindex='", num1.ToString(), "' ", null, null };
                    listDisp = tableField.QueryDisp;
                    strArrays[5] = this.method_2("1", listDisp.ToString());
                    strArrays[6] = "  name=\"chkquerydisp\">\n";
                    stringBuilder.Append(string.Concat(strArrays));
                    strArrays = new string[] { "\t<td align=\"center\"><select id=\"selalign", num1.ToString(), "\" size=\"1\" oindex='", num1.ToString(), "'  name=\"selalign\">\n" };
                    stringBuilder.Append(string.Concat(strArrays));
                    stringBuilder.Append(string.Concat("\t\t<option value=\"1\"", this.method_1("1", tableField.ColumnAlign.ToString()), ">左</option>\n"));
                    stringBuilder.Append(string.Concat("\t\t<option value=\"2\"", this.method_1("2", tableField.ColumnAlign.ToString()), ">中</option>\n"));
                    stringBuilder.Append(string.Concat("\t\t<option value=\"3\"", this.method_1("3", tableField.ColumnAlign.ToString()), ">右</option>\n"));
                    stringBuilder.Append("\t\t</select></td>\n");
                    strArrays = new string[] { "\t<td align=\"center\"><input class='textbox' id=\"txtwidth", num1.ToString(), "\"  type=\"text\"  oindex=", num1.ToString(), " name=\"txtwidth\" value='", tableField.ColumnWidth, "'></td>\n" };
                    stringBuilder.Append(string.Concat(strArrays));
                    strArrays = new string[] { "\t<td align=\"center\"><input class='textbox' id=\"txtrender", num1.ToString(), "\"  type=\"text\"  oindex=", num1.ToString(), " name=\"txtrender\" value='", tableField.ColumnRender, "'></td>\n" };
                    stringBuilder.Append(string.Concat(strArrays));
                    strArrays = new string[] { "\t<td align=\"center\"><input class='textbox' id=\"txtformatexp", num1.ToString(), "\"  type=\"text\"  oindex=", num1.ToString(), " name=\"txtformatexp\" value='", tableField.ColFormatExp, "'></td>\n" };
                    stringBuilder.Append(string.Concat(strArrays));
                    strArrays = new string[] { "\t<td align=\"center\"><input class='textbox' id=\"txttotalexp", num1.ToString(), "\"  type=\"text\"  oindex=", num1.ToString(), " name=\"txttotalexp\" value='", tableField.ColTotalExp, "'></td>\n" };
                    stringBuilder.Append(string.Concat(strArrays));
                    stringBuilder.Append(string.Concat("\t<td align=\"center\"><input id=\"btndel\"  type=\"button\" onclick='dropfield(", num1.ToString(), ");'  name=\"btndel\" value='删除'></td>\n"));
                    stringBuilder.Append("</tr>\n");
                    object[] fieldName = new object[] { num1, tableField.FieldName, tableField.ListDisp, tableField.QueryDisp, tableField.ColumnAlign, tableField.ColumnWidth, tableField.FieldNameCn, tableField.ColumnRender, tableField.ColFormatExp, tableField.ColTotalExp, tableField._AutoID };
                    stringBuilder2.AppendFormat("<td oindex={0} txtname='{1}'  txtnamecn='{6}' chklistdisp='{2}' chkquerydisp='{3}' selalign='{4}' txtwidth='{5}' txtrender='{7}'  txtformatexp='{8}' txttotalexp='{9}' fieldid='{10}'  state='unchanged'/>", fieldName);
                    num1++;
                }
                this.fieldHTML = stringBuilder.ToString();
                this.fieldXML = stringBuilder2.ToString();
            }
            this.rowcount = num1.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.tblName = base.GetParaValue("tblname");
            if (!base.IsPostBack)
            {
                this.method_0(1);
                this.method_3();
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
                if (Class7.dictionary_2 == null)
                {
                    Class7.dictionary_2 = new Dictionary<string, int>(6)
					{
						{ "int", 0 },
						{ "datetime", 1 },
						{ "varchar", 2 },
						{ "nvarchar", 3 },
						{ "decimal", 4 },
						{ "text", 5 }
					};
                }
                if (Class7.dictionary_2.TryGetValue(str1, out num))
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