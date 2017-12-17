using EIS.AppBase;
using EIS.DataAccess;

using System;
using System.Data;
using System.Web.UI.HtmlControls;
using WebBase.JZY.Tools;
namespace EIS.Web.WorkAsp.Meeting
{
    public  partial class MeetingInfo : PageBase
    {
        public DataRow hyInfo = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            string paraValue = base.GetParaValue("recId");
            string str = string.Concat("select * from T_OA_HY_Apply where _autoId='", paraValue, "'");
            DataTable dataTable = SysDatabase.ExecuteTable(str);
            if (dataTable.Rows.Count > 0)
            {
                this.hyInfo = dataTable.Rows[0];
            }
            WebTools.UpdateRead(base.EmployeeID, "T_OA_HY_Apply", paraValue);
        }
    }
}