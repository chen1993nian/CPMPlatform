using AjaxPro;
using EIS.AppBase;
using System;
using System.Web.UI.HtmlControls;

namespace Studio.JZY.Doc
{
    public partial class MyDocFavorite : PageBase
    {
        public string folderPId = "";

        public string folderId = "";

      

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(MyDocList));
            //AjaxPro.Utility.RegisterTypeForAjax(typeof(MyDocList));
            this.folderId = string.Concat("myFavorite_", base.EmployeeID);
        }
    }
}