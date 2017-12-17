using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.Doc
{
    public partial class DocFolderShare : PageBase
    {
        public string fullPath = "";

        public string folderId = "";



        protected void Button1_Click(object sender, EventArgs e)
        {
            _FolderInfo __FolderInfo = new _FolderInfo();
            FolderInfo model = __FolderInfo.GetModel(this.folderId);
            model.ShareUser = this.txtShare.Text;
            model.ShareUserId = this.hidShareId.Value;
            model.ShareLimit = this.CheckBoxList1.SelectedValue;
            __FolderInfo.Update(model);
            base.ClientScript.RegisterStartupScript(typeof(DocFolderShare), "", "afterSave();", true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.folderId = base.GetParaValue("folderId");
            if (!base.IsPostBack)
            {
                FolderInfo folderModel = FolderService.GetFolderModel(this.folderId);
                this.txtPath.Text = folderModel.FolderName;
                this.txtShare.Text = folderModel.ShareUser;
                this.hidShareId.Value = folderModel.ShareUserId;
                if (folderModel.ShareLimit != "1")
                {
                    this.CheckBoxList1.SelectedValue = "0";
                }
                else
                {
                    this.CheckBoxList1.SelectedValue = "1";
                }
            }
        }
    }
}