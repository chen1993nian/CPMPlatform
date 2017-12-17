using EIS.AppBase;
using EIS.DataModel.Service;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.Doc.Limit
{
    public partial class DocLimitTop : PageBase
    {

        public string funId = "";
        public string funName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.funId = base.GetParaValue("funId");
            this.funName = FolderService.GetFullPath(this.funId, "0").Replace("/", " / ");
        }
    }
}