using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Service;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web.WorkAsp.WorkLog
{
    public partial class LogView : PageBase
    {
        

        public StringBuilder sbLog = new StringBuilder();

        public StringBuilder PreNextLink = new StringBuilder();

        public string EmpName = "";

        public string emptyStyle = "display:none;";

        public DateTime startDate = DateTime.Today;

       

        protected void Page_Load(object sender, EventArgs e)
        {
            string item = "";
            string str = "";
            string paraValue = "";
            paraValue = base.GetParaValue("empId");
            this.EmpName = EmployeeService.GetEmployeeName(paraValue);
            DataTable dataTable = new DataTable();
            if (!string.IsNullOrEmpty(base.Request["start"]))
            {
                item = base.Request["start"];
                str = string.Format("select top 7 *,(select count(*) from T_OA_Comment where AppId=log._AutoID) c from T_OA_WorkLog log where EmpID='{0}' and WorkDate>='{1}' order by WorkDate", paraValue, item);
                dataTable = SysDatabase.ExecuteTable(str);
            }
            else
            {
                DateTime today = DateTime.Today;
                DateTime dateTime = DateTime.Today;
                DateTime dateTime1 = today.AddDays((double)((int)DayOfWeek.Monday - (int)dateTime.DayOfWeek));
                item = dateTime1.ToString("yyyy-MM-dd");
                str = string.Format("select *,(select count(*) from T_OA_Comment where AppId=log._AutoID) c from T_OA_WorkLog log where EmpID='{0}' and WorkDate>='{1:yyyy-MM-dd}' order by WorkDate", paraValue, dateTime1);
                dataTable = SysDatabase.ExecuteTable(str);
                if (dataTable.Rows.Count == 0)
                {
                    dataTable = SysDatabase.ExecuteTable(string.Format("select top 7 *,(select count(*) from T_OA_Comment where AppId=log._AutoID) c from T_OA_WorkLog log where EmpID='{0}' order by WorkDate", paraValue));
                    item = DateTime.Today.ToString("yyyy-MM-dd");
                }
            }
            if (dataTable.Rows.Count <= 0)
            {
                this.emptyStyle = "display:block;";
            }
            else
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    this.sbLog.AppendFormat("<div class='logItem'><div class='itemHeader'><div class='headerLeft'><a title='点击查看' href='LogItem.aspx?id={2}' target='_self' class='on'>{0:yyyy年MM月dd日（dddd）}</a></div>\r\n                        <div class='headerRight'>\r\n                        <a title='点击查看' href='LogItem.aspx?id={2}' target='_self'>评论[{1}]</a></div></div>", row["WorkDate"], row["c"], row["_AutoID"]);
                    this.sbLog.AppendFormat("<div class='itemBody'>{0}</div>", row["WorkLog"]);
                    this.sbLog.Append("</div>");
                }
            }
            this.startDate = DateTime.Parse(item);
            DateTime dateTime2 = this.startDate.AddMonths(-1);
            dateTime2 = dateTime2.AddDays((double)(1 - dateTime2.Day));
            DateTime dateTime3 = this.startDate.AddMonths(1);
            dateTime3 = dateTime3.AddDays((double)(1 - dateTime3.Day));
            this.PreNextLink.AppendFormat("<a href='LogView.aspx?start={0:yyyy-MM-dd}&empId={1}'>上周</a>", this.startDate.AddDays((double)((int)DayOfWeek.Monday - (int)this.startDate.DayOfWeek - (int)(DayOfWeek.Monday | DayOfWeek.Tuesday | DayOfWeek.Wednesday | DayOfWeek.Thursday | DayOfWeek.Friday | DayOfWeek.Saturday))), paraValue);
            this.PreNextLink.AppendFormat("<a href='LogView.aspx?start={0:yyyy-MM-dd}&empId={1}'>下周</a>", this.startDate.AddDays((double)((int)(DayOfWeek.Monday | DayOfWeek.Tuesday | DayOfWeek.Wednesday | DayOfWeek.Thursday | DayOfWeek.Friday | DayOfWeek.Saturday) - (int)this.startDate.DayOfWeek + (int)DayOfWeek.Monday)), paraValue);
            this.PreNextLink.AppendFormat("<a href='LogView.aspx?start={0:yyyy-MM-dd}&empId={1}'>上月</a>", dateTime2, paraValue);
            this.PreNextLink.AppendFormat("<a href='LogView.aspx?start={0:yyyy-MM-dd}&empId={1}'>下月</a>", dateTime3, paraValue);
        }
    }
}