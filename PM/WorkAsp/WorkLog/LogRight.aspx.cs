using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web.WorkAsp.WorkLog
{
	public partial class LogRight : PageBase
	{
		public StringBuilder sbLog = new StringBuilder();

		public StringBuilder PreNextLink = new StringBuilder();

		public string emptyStyle = "display:none;";

		public string emptyInfo = "没有找到符合条件的日志";

		public DateTime startDate = DateTime.Today;

		

		protected void Page_Load(object sender, EventArgs e)
		{
			DateTime today;
			DateTime dateTime;
			AjaxPro.Utility.RegisterTypeForAjax(typeof(LogRight));
			string item = "";
			string str = "";
			DataTable dataTable = new DataTable();
			if (!string.IsNullOrEmpty(base.Request["start"]))
			{
				item = base.Request["start"];
				DateTime.Parse(item);
				if (base.Request["mode"] != "week")
				{
					str = string.Format("select top 31 *,(select count(*) from T_OA_Comment where AppId=log._AutoID) c from T_OA_WorkLog log where EmpID='{0}' and WorkDate>='{1}' and datediff(d,'{1}',WorkDate)<31 order by WorkDate", base.EmployeeID, item);
					dataTable = SysDatabase.ExecuteTable(str);
				}
				else
				{
					str = string.Format("select top 7 *,(select count(*) from T_OA_Comment where AppId=log._AutoID) c from T_OA_WorkLog log where EmpID='{0}' and WorkDate>='{1}' and datediff(d,'{1}',WorkDate)<7 order by WorkDate", base.EmployeeID, item);
					dataTable = SysDatabase.ExecuteTable(str);
				}
			}
			else
			{
				today = DateTime.Today;
				dateTime = DateTime.Today;
				DateTime dateTime1 = today.AddDays((double)((int)DayOfWeek.Monday - (int)dateTime.DayOfWeek));
				item = dateTime1.ToString("yyyy-MM-dd");
				str = string.Format("select *,(select count(*) from T_OA_Comment where AppId=log._AutoID) c from T_OA_WorkLog log where EmpID='{0}' and WorkDate>='{1:yyyy-MM-dd}' order by WorkDate", base.EmployeeID, dateTime1);
				dataTable = SysDatabase.ExecuteTable(str);
				if (dataTable.Rows.Count == 0)
				{
					str = string.Format("select top 5 *,(select count(*) from T_OA_Comment where AppId=log._AutoID) c from T_OA_WorkLog log where EmpID='{0}' order by WorkDate desc", base.EmployeeID);
					dataTable = SysDatabase.ExecuteTable(str);
					item = DateTime.Today.ToString("yyyy-MM-dd");
					if (dataTable.Rows.Count == 0)
					{
						this.emptyInfo = "您还没有写过工作日志呢，赶快填写吧！";
					}
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
					DateTime dateTime2 = Convert.ToDateTime(row["WorkDate"]);
					this.sbLog.AppendFormat("<div class='logItem'><div class='itemHeader'><div class='headerLeft'><a title='点击查看' href='LogItem.aspx?id={2}' target='_self' class='on'>{0}</a></div>\r\n                        <div class='headerRight'>\r\n                        <a title='点击查看' href='LogItem.aspx?id={2}' target='_self'>评论[{1}]</a>\r\n                        <a title='点击编辑' href='javascript:' logId='{2}' class='editLink' target='_self'>编辑</a>\r\n                        <a title='点击删除' href='javascript:' logId='{2}' class='removeLink'>删除</a>\r\n                        </div></div>", (dateTime2.Year == DateTime.Today.Year ? dateTime2.ToString("MM月dd日（dddd）") : dateTime2.ToString("yyyy年MM月dd日（dddd）")), row["c"], row["_AutoID"]);
					this.sbLog.AppendFormat("<div class='itemBody'>{0}</div>", row["WorkLog"]);
					this.sbLog.Append("</div>");
				}
			}
			this.startDate = DateTime.Parse(item);
			DateTime dateTime3 = this.startDate.AddMonths(-1);
			dateTime3 = dateTime3.AddDays((double)(1 - dateTime3.Day));
			DateTime dateTime4 = this.startDate.AddMonths(1);
			dateTime4 = dateTime4.AddDays((double)(1 - dateTime4.Day));
			StringBuilder preNextLink = this.PreNextLink;
			today = DateTime.Today;
			dateTime = DateTime.Today;
			preNextLink.AppendFormat("<a href='LogRight.aspx?start={0:yyyy-MM-dd}&mode=week'>本周</a>", today.AddDays((double)((int)DayOfWeek.Monday - (int)dateTime.DayOfWeek)));
			this.PreNextLink.AppendFormat("<a href='LogRight.aspx?start={0:yyyy-MM-dd}&mode=week'>上周</a>", this.startDate.AddDays((double)((int)DayOfWeek.Monday - (int)this.startDate.DayOfWeek - (int)(DayOfWeek.Monday | DayOfWeek.Tuesday | DayOfWeek.Wednesday | DayOfWeek.Thursday | DayOfWeek.Friday | DayOfWeek.Saturday))));
			this.PreNextLink.AppendFormat("<a href='LogRight.aspx?start={0:yyyy-MM-dd}&mode=week'>下周</a>", this.startDate.AddDays((double)((int)(DayOfWeek.Monday | DayOfWeek.Tuesday | DayOfWeek.Wednesday | DayOfWeek.Thursday | DayOfWeek.Friday | DayOfWeek.Saturday) - (int)this.startDate.DayOfWeek + (int)DayOfWeek.Monday)));
			this.PreNextLink.AppendFormat("<a href='LogRight.aspx?start={0:yyyy-MM-dd}&mode=month'>上月</a>", dateTime3);
			this.PreNextLink.AppendFormat("<a href='LogRight.aspx?start={0:yyyy-MM-dd}&mode=month'>下月</a>", dateTime4);
		}

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void RemoveLog(string logId)
		{
			string str = string.Concat("delete T_OA_WorkLog where _autoId='", logId, "'");
			SysDatabase.ExecuteNonQuery(str);
		}
	}
}