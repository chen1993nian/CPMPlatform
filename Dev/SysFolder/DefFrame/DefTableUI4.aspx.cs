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
    public partial class DefTableUI4 : AdminPageBase
    {
        public StringBuilder fieldlist1 = new StringBuilder();

        public string tblName = "";

        public string styleIndex = "";

        public string editor1Html = "";

        public string editor2Html = "";

        public string t = "";

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            _TableInfo __TableInfo = new _TableInfo(this.tblName);
            string text = this.TextBox1.Text;
            if (this.styleIndex != "0")
            {
                EIS.DataModel.Model.TableStyle tableStyle = __TableInfo.GetTableStyle(this.styleIndex);
                tableStyle._UpdateTime = DateTime.Now;
                if (this.t == "1")
                {
                    tableStyle.FormHtml = text;
                }
                else if (this.t == "2")
                {
                    tableStyle.FormHtml2 = text;
                }
                else if (this.t == "3")
                {
                    tableStyle.PrintHtml = text;
                }
                else if (this.t == "4")
                {
                    tableStyle.DetailHtml = text;
                }
                (new _TableStyle()).Update(tableStyle);
                __TableInfo.SetUpdateTime();
            }
            else
            {
                TableInfo model = __TableInfo.GetModel();
                if (this.t == "1")
                {
                    model.FormHtml = text;
                }
                else if (this.t == "2")
                {
                    model.FormHtml2 = text;
                }
                else if (this.t == "3")
                {
                    model.PrintHtml = text;
                }
                else if (this.t == "4")
                {
                    model.DetailHtml = text;
                }
                model._UpdateTime = DateTime.Now;
                __TableInfo.Update(model, "");
            }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            FieldInfo phyField = null;
            CustomDb customDb;
            DbDataReader dbDataReaders;
            DataRow row = null;
            TableInfo subTable = null;
            _FieldInfo __FieldInfo;
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
            }
            this.editor1Html = this.TextBox1.Text;
            if (model.TableType == 1)
            {
                foreach (FieldInfo phyFieldA in __TableInfo.GetPhyFields())
                {
                    this.fieldlist1.AppendFormat("<a href='#' class='li-field' ref='{1}：{{{0}}}'>[*]</a>&nbsp;<a href='#' class='li-field' ref='{{{0}}}'>[{0}]</a>&nbsp;<a href='#' class='li-field' ref='{1}'>{1}</a><br/>", phyFieldA.FieldName, phyFieldA.FieldNameCn, model.TableName);
                }
                __FieldInfo = new _FieldInfo();
                foreach (TableInfo tableInfo in __TableInfo.GetSubTable())
                {
                    List<FieldInfo> tablePhyFields = __FieldInfo.GetTablePhyFields(tableInfo.TableName);
                    this.fieldlist1.Append("<div class='subzone'>");
                    this.fieldlist1.AppendFormat("<a href='#' class='li-table' ref='{0}'>{0}</a>", tableInfo.TableName);
                    foreach (FieldInfo tablePhyField in tablePhyFields)
                    {
                        this.fieldlist1.AppendFormat("<div class='fieldItem' ref='{0}|{1}'><a href='#' class='li-field' ref='{1}：{{{2}.{0}}}'>[*]</a>&nbsp;<a href='#' class='li-field' ref='{{{0}}}'>[{0}]</a>&nbsp;<a href='#' class='li-field' ref='{1}'>{1}</a></div>", tablePhyField.FieldName, tablePhyField.FieldNameCn, tableInfo.TableName);
                    }
                    this.fieldlist1.Append("</div>");
                }
                this.method_0(this.styleIndex);
            }
            else if (model.TableType == 3)
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    if (model.ConnectionId == "")
                    {
                        dataTable = SysDatabase.ExecuteReader(model.ListSQL.Replace("|^condition^|", " 1=1 ").Replace("|^sortdir^|", "")).GetSchemaTable();
                    }
                    else
                    {
                        customDb = new CustomDb();
                        customDb.CreateDatabaseByConnectionId(model.ConnectionId);
                        dataTable = customDb.ExecuteReader(model.ListSQL.Replace("|^condition^|", " 1=1 ").Replace("|^sortdir^|", "")).GetSchemaTable();
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
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            this.fieldlist1.AppendFormat("<a href='#' class='li-field' ref='{{{0}}}'>[{0}]</a>&nbsp;<br/>", dataRow["ColumnName"]);
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }
        }
    }
}