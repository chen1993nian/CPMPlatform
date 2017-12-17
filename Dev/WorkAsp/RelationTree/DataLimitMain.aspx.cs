using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Studio.JZY.WorkAsp.DataLimit
{
    public partial class DataLimitMain : System.Web.UI.Page
    {
        public string LimitUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //LimitType = 0人员权限；1角色权限；2岗位权限；3部门权限；
            int LimitType = 1;
            string urlPre = "RelationMain.aspx?";
            this.LimitUrl = urlPre + Request.QueryString.ToString();

            if (Request["LimitType"] != null) LimitType = Convert.ToInt16(Request["LimitType"].ToString());
            string[] arrLimitUrl = new string[] { "BB4D6FF4-9711-4357-A521-AF6B6613F38A", "C2661DED-2FBB-47CF-8269-F63D5E3C5D58", "1E5E6448-21D7-4D3F-A5CC-4E4B306FD687", "4FBE5F3B-949C-4D34-9BB9-E7F22E333564" };
            if (Request.QueryString.ToString().Trim() != "")
                this.LimitUrl = this.LimitUrl + "&RelationID=" + arrLimitUrl[LimitType].ToString();
            else
                this.LimitUrl = this.LimitUrl + "RelationID=" + arrLimitUrl[LimitType].ToString();
        }
    }
}