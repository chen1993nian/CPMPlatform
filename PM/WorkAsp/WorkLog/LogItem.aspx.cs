using EIS.AppBase;
using EIS.DataAccess;

using WebBase.JZY.Tools;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.WorkAsp.WorkLog
{
    public partial class LogItem : PageBase
    {
       

        public StringBuilder sbLog = new StringBuilder();

        public StringBuilder sbComment = new StringBuilder();

        public StringBuilder PreNextLink = new StringBuilder();

        public string EmpName = "";

        public string emptyStyle = "display:none;";

        public string logId = "";

        public DateTime startDate = DateTime.Today;



        private void method_0()
        {
            string str = "";
            str = string.Format("select top 7 *,(select count(*) from T_OA_Comment where AppId=log._AutoID) c from T_OA_WorkLog log where _autoId='{0}'", this.logId);
            DataTable dataTable = SysDatabase.ExecuteTable(str);
            if (dataTable.Rows.Count > 0)
            {
                DataRow item = dataTable.Rows[0];
                this.sbLog.AppendFormat("<div class='logItem'><div class='itemHeader'><div class='headerLeft'><a title='点击查看' href='LogItem.aspx?id={2}' target='_self' class='on'>{0:yyyy年MM月dd日（dddd）}</a></div>\r\n                        <div class='headerRight'></div></div>", item["WorkDate"], item["c"], item["_AutoID"]);
                this.sbLog.AppendFormat("<div class='itemBody'>{0}</div>", item["WorkLog"]);
                this.sbLog.Append("</div>");
                str = string.Concat("select * from T_OA_Comment where AppID='", this.logId, "' order by AddTime");
                DataTable dataTable1 = SysDatabase.ExecuteTable(str);
                int num = 1;
                foreach (DataRow row in dataTable1.Rows)
                {
                    StringBuilder stringBuilder = this.sbComment;
                    object[] objArray = new object[] { row["EmployeeName"], row["AddTime"], row["Content"], null };
                    int num1 = num;
                    num = num1 + 1;
                    objArray[3] = num1;
                    stringBuilder.AppendFormat("<div class=\"c_list\"><span class=\"c_writer\">评论人：{0}</span><span class=\"c_time\">{3}楼 {1}</span><p>{2}</p></div>", objArray);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.logId = base.GetParaValue("id");
            if (!base.IsPostBack)
            {
                this.method_0();
            }
        }

        protected void Submit1_Click(object sender, EventArgs e)
        {
            OAComment oAComment = new OAComment(base.UserInfo)
            {
                EmployeeName = base.EmployeeName,
                AddTime = new DateTime?(DateTime.Now),
                Content = this.TextBox1.Text,
                AppID = this.logId,
                AppName = "T_OA_WorkLog"
            };
            WebTools.InsertComment(oAComment);
            this.method_0();
        }
    }
}