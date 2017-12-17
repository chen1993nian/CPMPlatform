using EIS.AppBase;
using System;

namespace EIS.Web.SysFolder.Permission
{
    public partial class CompanyDeptFrame : AdminPageBase
    {
        public string leftPath = "";

     
        protected void Page_Load(object sender, EventArgs e)
        {
            this.leftPath = base.GetParaValue("leftPath");
        }
    }
}