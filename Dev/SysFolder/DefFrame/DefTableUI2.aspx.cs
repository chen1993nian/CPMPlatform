using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefTableUI2 : AdminPageBase
    {
        

        public StringBuilder fieldlist1 = new StringBuilder();

        public string tblName = "";

        public string tblNameCn = "";

        public string styleIndex = "";

        public string editor1Html = "";

        public string editor2Html = "";

        public string t = "";

        public string editorLib = "";

       

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string GetFields(string tblName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (FieldInfo field in (new _TableInfo(tblName)).GetFields())
            {
                stringBuilder.AppendFormat("{0},{1}|", field.FieldName, field.FieldNameCn);
            }
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Length = stringBuilder.Length - 1;
            }
            return stringBuilder.ToString();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            this.method_1(this.tblName, this.styleIndex, this.t, this.TextBox1.Text);
        }

        private void method_0(string string_1)
        {
            this.DropDownList1.Items.Clear();
            ListItem listItem = new ListItem("默认样式", "0");
            this.DropDownList1.Items.Add(listItem);
            if (string_1 == "0")
            {
                this.DropDownList1.SelectedIndex = 0;
            }
            int maxIndex = _TableStyle.GetMaxIndex(this.tblName);
            for (int i = 1; i <= maxIndex; i++)
            {
                listItem = new ListItem(string.Concat("样式", i.ToString()), i.ToString())
                {
                    Selected = string_1 == i.ToString()
                };
                this.DropDownList1.Items.Add(listItem);
            }
        }

        private void method_1(string tblName, string style, string string_1, string html)
        {
            _TableInfo __TableInfo = new _TableInfo(tblName);
            if (style != "0")
            {
                EIS.DataModel.Model.TableStyle tableStyle = __TableInfo.GetTableStyle(style);
                tableStyle._UpdateTime = DateTime.Now;
                if (string_1 == "1")
                {
                    tableStyle.FormHtml = html;
                }
                else if (string_1 == "2")
                {
                    tableStyle.FormHtml2 = html;
                }
                else if (string_1 == "3")
                {
                    tableStyle.PrintHtml = html;
                }
                else if (string_1 == "4")
                {
                    tableStyle.DetailHtml = html;
                }
                (new _TableStyle()).Update(tableStyle);
                __TableInfo.SetUpdateTime();
            }
            else
            {
                TableInfo model = __TableInfo.GetModel();
                if (string_1 == "1")
                {
                    model.FormHtml = html;
                }
                else if (string_1 == "2")
                {
                    model.FormHtml2 = html;
                }
                else if (string_1 == "3")
                {
                    model.PrintHtml = html;
                }
                else if (string_1 == "4")
                {
                    model.DetailHtml = html;
                }
                model._UpdateTime = DateTime.Now;
                __TableInfo.Update(model, "");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            List<FieldInfo> tablePhyFields;
            _FieldInfo __FieldInfo;
            FieldInfo tablePhyField = null;
            TableInfo subTable = null;
            TableInfo tableInfo = null;
            CustomDb customDb;
            DbDataReader dbDataReaders;
            DataRow row = null;
            string browser = HttpContext.Current.Request.Browser.Browser;
            if ((browser == "IE" ? false : !(browser == "InternetExplorer")))
            {
                this.editorLib = "tiny_mce.js";
            }
            else if (HttpContext.Current.Request.Browser.MajorVersion <= 8)
            {
                this.editorLib = "tiny_mce_cn.js";
            }
            else
            {
                this.editorLib = "tiny_mce.js";
            }
            this.fileLogger.Info<string, int>("DefTableUI2：browser={0},MajorVer={1}", browser, base.Request.Browser.MajorVersion);
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefTableUI2));
            this.tblName = base.GetParaValue("tblName");
            this.t = base.GetParaValue("t");
            this.styleIndex = base.GetParaValue("styleIndex");
            _TableInfo __TableInfo = new _TableInfo(this.tblName);
            TableInfo model = __TableInfo.GetModel();
            this.tblNameCn = model.TableNameCn;
            if (!base.IsPostBack)
            {
                if (this.styleIndex == "")
                {
                    this.TextBox1.Text = "";
                }
                else if (this.styleIndex != "0")
                {
                    EIS.DataModel.Model.TableStyle tableStyle = __TableInfo.GetTableStyle(this.styleIndex);
                    if (this.t == "1")
                    {
                        this.TextBox1.Text = tableStyle.FormHtml;
                    }
                    else if (this.t == "2")
                    {
                        this.TextBox1.Text = tableStyle.FormHtml2;
                    }
                    else if (this.t == "3")
                    {
                        this.TextBox1.Text = tableStyle.PrintHtml;
                    }
                    else if (this.t == "4")
                    {
                        this.TextBox1.Text = tableStyle.DetailHtml;
                    }
                }
                else if (model != null)
                {
                    if (this.t == "1")
                    {
                        this.TextBox1.Text = model.FormHtml;
                    }
                    else if (this.t == "2")
                    {
                        this.TextBox1.Text = model.FormHtml2;
                    }
                    else if (this.t == "3")
                    {
                        this.TextBox1.Text = model.PrintHtml;
                    }
                    else if (this.t == "4")
                    {
                        this.TextBox1.Text = model.DetailHtml;
                    }
                }
                this.method_0(this.styleIndex);
                if (model.TableType == 1)
                {
                    this.ddlTblName.Items.Add(this.tblName);
                    foreach (TableInfo subTable1 in __TableInfo.GetSubTable())
                    {
                        this.ddlTblName.Items.Add(subTable1.TableName);
                    }
                }
                else if (model.TableType == 2)
                {
                    this.ddlTblName.Items.Add(model.ParentName);
                    foreach (TableInfo tableInfoA in __TableInfo.GetSubTable(model.ParentName))
                    {
                        this.ddlTblName.Items.Add(tableInfoA.TableName);
                    }
                }
                this.ddlTblName.SelectedValue = this.tblName;
            }
            this.editor1Html = this.TextBox1.Text;
            if (model.TableType == 1)
            {
                this.fieldlist1.AppendFormat("<div class='titleZone'><a href='#' ref='{0}'>{0}</a></div>", this.tblName);
                this.fieldlist1.AppendFormat("<div class='fieldZone'>", new object[0]);
                foreach (FieldInfo phyField in __TableInfo.GetPhyFields())
                {
                    this.fieldlist1.AppendFormat("<a href='#' class='fieldattr' ref='{2}|{1}'>[*]</a>&nbsp;<a href='#' class='li-field' ref='{{{2}.{0}}}'>{0}</a>&nbsp;<a href='#' class='li-field' ref='{1}'>{1}</a><br/>", phyField.FieldName, phyField.FieldNameCn, model.TableName);
                }
                this.fieldlist1.AppendFormat("</div>", new object[0]);
                __FieldInfo = new _FieldInfo();
                foreach (TableInfo tableInfo1 in __TableInfo.GetSubTable())
                {
                    tablePhyFields = __FieldInfo.GetTablePhyFields(tableInfo1.TableName);
                    this.fieldlist1.AppendFormat("<div class='titleZone'><a title='点击或拖动插入子表' href='#' class='li-table' ref='{0}'>{0}</a></div>", tableInfo1.TableName);
                    this.fieldlist1.Append("<div class='fieldZone'>");
                    foreach (FieldInfo fieldInfo in tablePhyFields)
                    {
                        this.fieldlist1.AppendFormat("<div class='fieldItem' ref='{0}|{1}'><a href='#' class='fieldattr' ref='{2}|{0}'>[*]</a>&nbsp;<a href='#' class='li-field' ref='{{{0}}}'>{0}</a>&nbsp;<a href='#' class='li-field' ref='{1}'>{1}</a></div>", fieldInfo.FieldName, fieldInfo.FieldNameCn, tableInfo1.TableName);
                    }
                    this.fieldlist1.Append("</div>");
                }
                DataTable dataTable = SysDatabase.ExecuteTable(string.Concat("select * from T_E_Sys_Relation where TableName='", this.tblName, "'"));
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string str = dataRow["subTable"].ToString();
                    tablePhyFields = __FieldInfo.GetTableFields(str);
                    this.fieldlist1.AppendFormat("<div class='titleZone'><a title='点击或拖动插入子表' href='#' class='li-table' ref='{0}'>{0}</a></div>", str);
                    this.fieldlist1.Append("<div class='fieldZone'>");
                    foreach (FieldInfo tablePhyField1 in tablePhyFields)
                    {
                        this.fieldlist1.AppendFormat("<div class='fieldItem' ref='{0}|{1}'><a href='#' class='fieldattr' ref='{2}|{0}'>[*]</a>&nbsp;<a href='#' class='li-field' ref='{{{0}}}'>{0}</a>&nbsp;<a href='#' class='li-field' ref='{1}'>{1}</a></div>", tablePhyField1.FieldName, tablePhyField1.FieldNameCn, str);
                    }
                    this.fieldlist1.Append("</div>");
                }
            }
            else if (model.TableType == 2)
            {
                __FieldInfo = new _FieldInfo();
                tablePhyFields = __TableInfo.GetPhyFields();
                tablePhyFields = __FieldInfo.GetTablePhyFields(this.tblName);
                this.fieldlist1.AppendFormat("<div class='titleZone'><a title='点击或拖动插入子表' href='#' class='li-table' ref='{0}'>{0}</a></div>", this.tblName);
                this.fieldlist1.Append("<div class='fieldZone'>");
                foreach (FieldInfo tablePhyFieldA in tablePhyFields)
                {
                    this.fieldlist1.AppendFormat("<div class='fieldItem' ref='{0}|{1}'><a href='#' class='fieldattr' ref='{2}|{0}'>[*]</a>&nbsp;<a href='#' class='li-field' ref='{{{0}}}'>{0}</a>&nbsp;<a href='#' class='li-field' ref='{1}'>{1}</a></div>", tablePhyFieldA.FieldName, tablePhyFieldA.FieldNameCn, this.tblName);
                }
                this.fieldlist1.Append("</div>");
            }
            else if (model.TableType == 3)
            {
                try
                {
                    DataTable schemaTable = new DataTable();
                    if (model.ConnectionId == "")
                    {
                        schemaTable = SysDatabase.ExecuteReader(model.ListSQL.Replace("|^condition^|", " 1=1 ").Replace("|^sortdir^|", "")).GetSchemaTable();
                    }
                    else
                    {
                        customDb = new CustomDb();
                        customDb.CreateDatabaseByConnectionId(model.ConnectionId);
                        schemaTable = customDb.ExecuteReader(model.ListSQL.Replace("|^condition^|", " 1=1 ").Replace("|^sortdir^|", "")).GetSchemaTable();
                    }
                    foreach (DataRow rowA in schemaTable.Rows)
                    {
                        this.fieldlist1.AppendFormat("<a href='#' class='li-field' ref='{{{0}}}'>[{0}]</a>&nbsp;<br/>", rowA["ColumnName"]);
                    }
                    __FieldInfo = new _FieldInfo();
                    foreach (TableInfo subTableA in __TableInfo.GetSubTable())
                    {
                        TableInfo model1 = (new _TableInfo(subTableA.TableName)).GetModel();
                        this.fieldlist1.Append("<hr/>");
                        if (model1.ConnectionId == "")
                        {
                            dbDataReaders = SysDatabase.ExecuteReader(model1.ListSQL.Replace("|^condition^|", " 1=1 ").Replace("|^sortdir^|", ""));
                            try
                            {
                                schemaTable = dbDataReaders.GetSchemaTable();
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
                            customDb = new CustomDb();
                            customDb.CreateDatabaseByConnectionId(model1.ConnectionId);
                            dbDataReaders = customDb.ExecuteReader(model1.ListSQL.Replace("|^condition^|", " 1=1 ").Replace("|^sortdir^|", ""));
                            try
                            {
                                schemaTable = dbDataReaders.GetSchemaTable();
                            }
                            finally
                            {
                                if (dbDataReaders != null)
                                {
                                    ((IDisposable)dbDataReaders).Dispose();
                                }
                            }
                        }
                        foreach (DataRow row1 in schemaTable.Rows)
                        {
                            this.fieldlist1.AppendFormat("<a href='#' class='li-field' ref='{{{0}}}'>[{0}]</a>&nbsp;<br/>", row1["ColumnName"]);
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void SaveData(string tblName, string style, string string_1, string html)
        {
            this.method_1(tblName, style, string_1, html);
        }
    }
}