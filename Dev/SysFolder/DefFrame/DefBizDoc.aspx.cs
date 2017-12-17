using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefBizDoc : AdminPageBase
    {
      

        public StringBuilder fieldlist1 = new StringBuilder();

        public string tblname = "";

        public DefBizDoc()
        {
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            TableDoc tableDoc;
            _TableDoc __TableDoc = new _TableDoc();
            try
            {
                if (this.ViewState["isnew"].ToString() != "0")
                {
                    tableDoc = new TableDoc()
                    {
                        _AutoID = Guid.NewGuid().ToString(),
                        _OrgCode = base.OrgCode,
                        _UserName = base.EmployeeID,
                        _CreateTime = DateTime.Now,
                        _UpdateTime = DateTime.Now,
                        _IsDel = 0,
                        TableName = this.tblname,
                        ReportDoc = this.TextBox1.Text,
                        DesignDoc = this.TextBox2.Text
                    };
                    __TableDoc.Add(tableDoc);
                }
                else
                {
                    tableDoc = __TableDoc.GetModelBytableName(this.tblname);
                    tableDoc.ReportDoc = this.TextBox1.Text;
                    tableDoc.DesignDoc = this.TextBox2.Text;
                    __TableDoc.Update(tableDoc);
                }
                (new _TableInfo(this.tblname)).SetUpdateTime();
            }
            catch (Exception exception)
            {
                base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>alert('保存出错');</script>");
                base.Response.End();
            }
            base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script language=javascript>$.noticeAdd({text:'保存成功！',stay:false});</script>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.tblname = base.GetParaValue("tblname");
            if (!base.IsPostBack)
            {
                TableDoc modelBytableName = (new _TableDoc()).GetModelBytableName(this.tblname);
                if (modelBytableName == null)
                {
                    this.ViewState["isnew"] = "1";
                }
                else
                {
                    this.TextBox1.Text = modelBytableName.ReportDoc;
                    this.TextBox2.Text = modelBytableName.DesignDoc;
                    this.ViewState["isnew"] = "0";
                }
            }
        }
    }
}