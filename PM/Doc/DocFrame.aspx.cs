using EIS.AppBase;
using System;

namespace EIS.Web.Doc
{
    public partial class DocFrame : PageBase
    {
        public string treeId = "";

        public string treeName = "";


        public string treedata = "";

         protected void Page_Load(object sender, EventArgs e)
        {
            this.treeId = base.GetParaValue("treeId");
            this.treeName = base.GetParaValue("treeName");
        }
    }
}