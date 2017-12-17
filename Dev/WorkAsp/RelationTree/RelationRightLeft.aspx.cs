using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Studio.JZY.WorkAsp.RelationTree
{
    public partial class RelationRightLeft : EIS.AppBase.PageBase
    {
        public string RelationName = "左右关联业务";
        public string para = "";
        public string TSqlQueryCondition = "";
        public string SqlQueryCondition = "";
        public string DefQueryValue = "";
        public string DefaultValue = "";
        public string url1 = "";
        public string url2 = "../../welcome.htm";
        public string RelationUIWidth = "300,*";

        protected void Page_Load(object sender, EventArgs e)
        {
            string RelationID = this.GetParaValue("RelationID");

            RelationPageInfo rpage = new RelationPageInfo();
            whRelationPage objR = rpage.GetRelationPageInfoByID(RelationID, this);
            url1 = objR.RelationTreeUrl;
            url2 = objR.AppUrl2;
            RelationUIWidth = objR.RelationUIWidth;
            RelationName = objR.RelationName;


        }
    }
}