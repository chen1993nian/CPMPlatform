using AjaxPro;
using EIS.AppBase;
using System;
using System.Web.UI.HtmlControls;


namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefTreeList : AdminPageBase
    {
        public string othercond = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefTreeList));
        }
    }
}