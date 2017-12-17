using EIS.DataAccess;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace EIS.WebBase.SysFolder.WorkFlow.UserControl
{
    public partial class InstanceLog : System.Web.UI.UserControl
    {
        protected GridView LogGridView1;

        public string InstanceId
        {
            get;
            set;
        }

   
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.InstanceId))
            {
                string str = string.Concat("select * from T_E_WF_Log where AppId='", this.InstanceId, "' order by _CreateTime");
                DataTable dataTable = SysDatabase.ExecuteTable(str);
                this.LogGridView1.DataSource = dataTable;
                this.LogGridView1.DataBind();
            }

        }
    }
}