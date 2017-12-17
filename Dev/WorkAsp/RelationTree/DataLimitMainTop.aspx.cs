using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Studio.JZY.WorkAsp.DataLimit.WorkAsp.RelationTree
{
    public partial class DataLimitMainTop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Studio.JZY.WorkAsp.DataLimit.ClsDataLimit1 clsDL = new ClsDataLimit1();
            clsDL.SetAllDeptDataLimit();
            Response.Write("<script langauge=\"javascript\">");
            Response.Write("window.open(\"RelationMain.aspx?RelationID=4FBE5F3B-949C-4D34-9BB9-E7F22E333564\",\"DataLimitContents\");");
            Response.Write("</script>");
        }
    }
}