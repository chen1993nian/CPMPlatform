using EIS.AppBase;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.WorkFlow
{
    public partial class Company_Watch_Custom : PageBase
    {
        public string condition = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            condition = base.GetParaValue("condition");
        }
    }
}