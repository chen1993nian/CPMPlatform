using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Studio.JZY.Doc
{
    public partial class MyFolderEdit : PageBase
    {
        private _FolderInfo _FolderInfo_0 = new _FolderInfo();

  

        public string folderId
        {
            get
            {
                string str;
                str = (this.ViewState["folderId"] == null ? "" : this.ViewState["folderId"].ToString());
                return str;
            }
            set
            {
                this.ViewState["folderId"] = value;
            }
        }

        public MyFolderEdit()
        {
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            FolderInfo folderInfo;
            if (this.folderId == "")
            {
                string paraValue = base.GetParaValue("folderPId");
                folderInfo = new FolderInfo(base.UserInfo)
                {
                    FolderName = this.TextBox2.Text,
                    FolderWBS = FolderService.GetNewFolderWbsCode(paraValue),
                    Inherit = "1",
                    OrderID = int.Parse((this.TextBox3.Text == "" ? "0" : this.TextBox3.Text)),
                    FolderPID = paraValue
                };
                this._FolderInfo_0.Add(folderInfo);
                this.folderId = folderInfo._AutoID;
                base.ClientScript.RegisterStartupScript(typeof(MyFolderEdit), "", "afterNew();", true);
            }
            else
            {
                folderInfo = this._FolderInfo_0.GetModel(this.folderId);
                folderInfo.FolderName = this.TextBox2.Text;
                folderInfo.OrderID = int.Parse((this.TextBox3.Text == "" ? "0" : this.TextBox3.Text));
                folderInfo._UpdateTime = DateTime.Now;
                this._FolderInfo_0.Update(folderInfo);
                base.ClientScript.RegisterStartupScript(typeof(MyFolderEdit), "", "afterSave();", true);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.folderId = base.GetParaValue("folderId");
                FolderInfo folderModel = FolderService.GetFolderModel(this.folderId);
                if ((folderModel == null ? true : !(this.folderId != "")))
                {
                    TextBox textBox3 = this.TextBox3;
                    int newOrd = this._FolderInfo_0.GetNewOrd(base.GetParaValue("folderPId"));
                    textBox3.Text = newOrd.ToString();
                }
                else
                {
                    this.TextBox2.Text = folderModel.FolderName;
                    this.TextBox3.Text = folderModel.OrderID.ToString();
                }
            }
        }
    }
}