using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefFieldsEvent : AdminPageBase
    {
        public StringBuilder fieldlist1 = new StringBuilder();

        public string tblname = "";

       

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.method_1();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.method_0();
            this.method_1();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedValue = this.DropDownList1.SelectedValue;
                string str = this.DropDownList2.SelectedValue;
                string text = this.DropDownList2.SelectedItem.Text;
                _FieldEvent __FieldEvent = new _FieldEvent();
                List<FieldEvent> modelListBy = __FieldEvent.GetModelListBy(text, this.tblname);
                FieldEvent fieldEvent = modelListBy.Find((FieldEvent fieldEvent_0) => fieldEvent_0.EventType == selectedValue);
                if (fieldEvent == null)
                {
                    fieldEvent = new FieldEvent()
                    {
                        _AutoID = Guid.NewGuid().ToString(),
                        _OrgCode = base.OrgCode,
                        _UserName = base.EmployeeID,
                        _CreateTime = DateTime.Now,
                        _UpdateTime = DateTime.Now,
                        _IsDel = 0,
                        FieldID = str,
                        TableName = this.tblname,
                        FieldName = text,
                        EventType = selectedValue,
                        EventScript = this.TextBox1.Text
                    };
                    __FieldEvent.Add(fieldEvent);
                }
                else
                {
                    fieldEvent.EventScript = this.TextBox1.Text;
                    fieldEvent._UpdateTime = DateTime.Now;
                    __FieldEvent.Update(fieldEvent);
                }
                (new _TableInfo(this.tblname)).SetUpdateTime();
                base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>alert('保存成功！！');</script>");
            }
            catch (Exception ex)
            {
                base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>alert('保存出错');</script>");
            }
        }

        private void method_0()
        {
            string[] strArrays = new string[] { "onclick", "onchange", "onkeyup", "onkeydown", "onblur", "onselect", "onmouseover", "onmouseout" };
            string[] strArrays1 = strArrays;
            this.DropDownList1.Items.Clear();
            string selectedValue = this.DropDownList2.SelectedValue;
            string text = this.DropDownList2.SelectedItem.Text;
            List<FieldEvent> modelListBy = (new _FieldEvent()).GetModelListBy(text, this.tblname);
            string[] strArrays2 = strArrays1;
            for (int i = 0; i < (int)strArrays2.Length; i++)
            {
                string str = strArrays2[i];
                string str1 = "";
                str1 = (modelListBy.FindIndex((FieldEvent fieldEvent_0) => fieldEvent_0.EventType == str) <= -1 ? string.Concat("- ", str) : string.Concat("+ ", str));
                ListItem listItem = new ListItem(str1, str);
                this.DropDownList1.Items.Add(listItem);
            }
        }

        private void method_1()
        {
            string selectedValue = this.DropDownList1.SelectedValue;
            string str = this.DropDownList2.SelectedValue;
            FieldEvent model = (new _FieldEvent()).GetModel(str, selectedValue);
            this.TextBox1.Text = (model == null ? "" : model.EventScript);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            FieldInfo phyField = null;
            this.tblname = base.GetParaValue("tblname");
            List<FieldInfo> phyFields = (new _TableInfo(this.tblname)).GetPhyFields();
            if (!base.IsPostBack)
            {
                foreach (FieldInfo phyFieldA in phyFields)
                {
                    ListItem listItem = new ListItem(phyFieldA.FieldName, phyFieldA._AutoID);
                    this.DropDownList2.Items.Add(listItem);
                }
                this.method_0();
                this.method_1();
            }
            foreach (FieldInfo fieldInfo in phyFields)
            {
                this.fieldlist1.AppendFormat("<a href='#' class='li-field' >{0}</a>.<a href='#' class='li-field' >{1}</a><br>", fieldInfo.FieldOdr, fieldInfo.FieldName);
            }
        }
    }
}