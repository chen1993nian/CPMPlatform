using EIS.AppBase;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefQueryStyleLeft : AdminPageBase
    {
        public string treedata = "";

        public string fieldId = "";

        public string tblName = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            this.fieldId = base.GetParaValue("fieldId");
            this.tblName = base.GetParaValue("tblName");
        }
    }
}