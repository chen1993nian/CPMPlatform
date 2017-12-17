using EIS.AppBase;
using System;

namespace EIS.Studio.SysFolder.Permission
{
    public partial class DefDeptFrame : AdminPageBase
    {
        public string leftPath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.leftPath = base.GetParaValue("leftPath");
        }
    }
}