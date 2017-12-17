using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EIS.AppBase;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefCatalogFrame: AdminPageBase
    {
        public string rootwbs = "";
        public string rootname = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.rootwbs = base.GetParaValue("rootwbs");
            this.rootname = base.GetParaValue("rootname");
        }
    }
}