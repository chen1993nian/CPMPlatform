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
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefTableUI : AdminPageBase
    {
       

        public StringBuilder fieldlist1 = new StringBuilder();

        public string tblName = "";

        public string styleIndex = "";

        public string editor1Html = "";

        public string editor2Html = "";

        public string t = "";

     
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
            TableInfo subTable = null;
            _FieldInfo __FieldInfo;
            FieldInfo phyField = null;
            CustomDb customDb;
            DbDataReader dbDataReaders;
            DataRow row = null;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefTableUI));
            this.tblName = base.GetParaValue("tblname");
            this.t = base.GetParaValue("t");
            this.styleIndex = base.GetParaValue("styleIndex");
            _TableInfo __TableInfo = new _TableInfo(this.tblName);
            TableInfo model = __TableInfo.GetModel();
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
            }
            this.editor1Html = this.TextBox1.Text;
            if (model.TableType == 1)
            {
                foreach (FieldInfo fieldInfo in __TableInfo.GetPhyFields())
                {
                    this.fieldlist1.AppendFormat("<a href='#' class='li-field' ref='{{{2}.{0}}}'>[{0}]</a>&nbsp;<a href='#' class='li-field' ref='{1}'>{1}</a><br>", fieldInfo.FieldName, fieldInfo.FieldNameCn, model.TableName);
                }
                __FieldInfo = new _FieldInfo();
                foreach (TableInfo tableInfo in __TableInfo.GetSubTable())
                {
                    List<FieldInfo> tablePhyFields = __FieldInfo.GetTablePhyFields(tableInfo.TableName);
                    this.fieldlist1.Append("<hr/>");
                    foreach (FieldInfo tablePhyField in tablePhyFields)
                    {
                        this.fieldlist1.AppendFormat("<a href='#' class='li-field' ref='{{{0}}}'>[{0}]</a>&nbsp;<a href='#' class='li-field' ref='{1}'>{1}</a><br>", tablePhyField.FieldName, tablePhyField.FieldNameCn, tableInfo.TableName);
                    }
                }
            }
            else if (model.TableType == 2)
            {
                foreach (FieldInfo phyFieldA in __TableInfo.GetPhyFields())
                {
                    this.fieldlist1.AppendFormat("<a href='#' class='li-field' ref='{{{2}.{0}}}'>[{0}]</a>&nbsp;<a href='#' class='li-field' ref='{1}'>{1}</a><br>", phyFieldA.FieldName, phyFieldA.FieldNameCn, model.TableName);
                }
            }
            else if (model.TableType == 3)
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    if (model.ConnectionId == "")
                    {
                        dbDataReaders = SysDatabase.ExecuteReader(model.ListSQL.Replace("|^condition^|", " 1=1 ").Replace("|^sortdir^|", ""));
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
                        customDb = new CustomDb();
                        customDb.CreateDatabaseByConnectionId(model.ConnectionId);
                        dbDataReaders = customDb.ExecuteReader(model.ListSQL.Replace("|^condition^|", " 1=1 ").Replace("|^sortdir^|", ""));
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
                    foreach (DataRow rowA in dataTable.Rows)
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
                            customDb = new CustomDb();
                            customDb.CreateDatabaseByConnectionId(model1.ConnectionId);
                            dbDataReaders = customDb.ExecuteReader(model1.ListSQL.Replace("|^condition^|", " 1=1 ").Replace("|^sortdir^|", ""));
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
                        foreach (DataRow dataRowA in dataTable.Rows)
                        {
                            this.fieldlist1.AppendFormat("<a href='#' class='li-field' ref='{{{0}}}'>[{0}]</a>&nbsp;<br/>", dataRowA["ColumnName"]);
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