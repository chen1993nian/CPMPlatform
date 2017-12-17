using AjaxPro;
using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefTableScript : AdminPageBase
    {
        

        public StringBuilder fieldlist1 = new StringBuilder();

        public string tblName = "";

        public DefTableScript()
        {
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            _TableInfo __TableInfo = new _TableInfo(this.tblName);
            TableInfo model = __TableInfo.GetModel();
            model.EditScriptBlock = this.TextBox1.Text;
            model.ListScriptBlock = this.TextBox2.Text;
            model.ListPreProcessFn = this.TextBox3.Text;
            __TableInfo.UpdateScript(model);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            TableInfo subTable = null;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefTableScript));
            this.tblName = base.GetParaValue("tblname");
            if (!base.IsPostBack)
            {
                _TableInfo __TableInfo = new _TableInfo(this.tblName);
                TableInfo model = __TableInfo.GetModel();
                if (model != null)
                {
                    this.TextBox1.Text = model.EditScriptBlock;
                    this.TextBox2.Text = model.ListScriptBlock;
                    this.TextBox3.Text = model.ListPreProcessFn;
                }
                if (model.TableType == 1)
                {
                    this.ddlTblName.Items.Add(this.tblName);
                    foreach (TableInfo tableInfo in __TableInfo.GetSubTable())
                    {
                        this.ddlTblName.Items.Add(tableInfo.TableName);
                    }
                }
                else if (model.TableType == 2)
                {
                    this.ddlTblName.Items.Add(model.ParentName);
                    foreach (TableInfo subTableA in __TableInfo.GetSubTable(model.ParentName))
                    {
                        this.ddlTblName.Items.Add(subTableA.TableName);
                    }
                }
                this.ddlTblName.SelectedValue = this.tblName;
            }
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void SaveScript(string tblName, string string_0, string string_1, string string_2)
        {
            _TableInfo __TableInfo = new _TableInfo(tblName);
            TableInfo model = __TableInfo.GetModel();
            model.EditScriptBlock = string_0;
            model.ListScriptBlock = string_1;
            model.ListPreProcessFn = string_2;
            __TableInfo.UpdateScript(model);
        }
    }
}