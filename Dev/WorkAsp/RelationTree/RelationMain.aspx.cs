using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EIS.DataAccess;

namespace Studio.JZY.WorkAsp.RelationTree
{
    public partial class RelationMain : EIS.AppBase.PageBase
    {
        public RelationMain()
        {
            AutoRedirect = false;     
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            string RelationID = this.GetParaValue("RelationID");

            RelationPageInfo rpage = new RelationPageInfo();
            whRelationPage objR = rpage.GetRelationPageInfoByID(RelationID, this);
            Response.Redirect(objR.RelationUIUrl);


        }







    }
}