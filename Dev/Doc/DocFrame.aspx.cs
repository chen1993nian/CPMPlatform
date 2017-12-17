using EIS.AppBase;
using System;

namespace Studio.JZY.Doc
{
    public partial class DocFrame : PageBase
    {
        public string treeId = "";

        public string treeName = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            this.treeId = base.GetParaValue("treeId");
            this.treeName = base.GetParaValue("treeName");
        }
    }
}