using AjaxPro;
using EIS.AppBase;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.Doc
{
    public partial class MyDocReceive : PageBase
    {
      

        public string folderPId = "";

        public string folderId = "";

       
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(MyDocList));
            this.folderId = string.Concat("myReceive_", base.EmployeeID);
        }
    }
}