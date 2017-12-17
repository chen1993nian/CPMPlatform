using EIS.AppBase;
using System;

namespace EIS.SysFolder.Permission
{
    public partial class CompanyPositionFrame  : AdminPageBase
    {
        public string leftPath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.leftPath = base.GetParaValue("leftPath");
        }
    }
}