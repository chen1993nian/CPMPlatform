using EIS.AppBase;
using EIS.AppModel.Service;
using EIS.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.WebBase.WorkAsp.Settings
{
    public partial class SetCalendar : PageBase
    {
        public string calendarId = "";

       

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.txtQueryDate.Text != "")
            {
                DateTime today = DateTime.Today;
                DateTime.TryParse(this.txtQueryDate.Text, out today);
                this.Calendar1.VisibleDate = today;
            }
        }

        protected void Calendar1_DataBinding(object sender, EventArgs e)
        {
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            object obj;
            object obj1;
            CalendarDay day = e.Day;
            TableCell cell = e.Cell;
            if (!day.IsOtherMonth)
            {
                List<AppWorkDay> dataByDay = AppWorkDayService.GetDataByDay(day.Date, this.calendarId);
                if (dataByDay.Count == 2)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    DateTime date = day.Date;
                    stringBuilder.AppendFormat("<a href=\"javascript:updateDay('{1}','{2:yyyy-MM-dd}')\">{0}</a>&nbsp;", date.Day, this.calendarId, day.Date);
                    if ((dataByDay[0].IsWorkDay == "是" ? false : !(dataByDay[1].IsWorkDay == "是")))
                    {
                        stringBuilder.AppendFormat("<img  style='vertical-align:middle' src='../../img/common/reddot.gif' alt='节假日'/>", new object[0]);
                        stringBuilder.AppendFormat("<div class='holiday'>{0}</div>", (dataByDay[0].DayType == "节假日" ? dataByDay[0].Holiday : ""));
                    }
                    else
                    {
                        stringBuilder.Append("<img style='vertical-align:middle' src='../../img/common/greendot.gif' alt='工作日'/>");
                        stringBuilder.Append("<div class='tip'>");
                        StringBuilder stringBuilder1 = stringBuilder;
                        if (dataByDay[0].IsWorkDay == "是")
                        {
                            date = dataByDay[0].StartTime;
                            string shortTimeString = date.ToShortTimeString();
                            date = dataByDay[0].EndTime;
                            obj = string.Concat(shortTimeString, "-", date.ToShortTimeString());
                        }
                        else
                        {
                            obj = " ---- ";
                        }
                        stringBuilder1.AppendFormat("上午:{0} ", obj);
                        StringBuilder stringBuilder2 = stringBuilder;
                        if (dataByDay[1].IsWorkDay == "是")
                        {
                            date = dataByDay[1].StartTime;
                            string str = date.ToShortTimeString();
                            date = dataByDay[1].EndTime;
                            obj1 = string.Concat(str, "-", date.ToShortTimeString());
                        }
                        else
                        {
                            obj1 = " ---- ";
                        }
                        stringBuilder2.AppendFormat("<br/>下午:{0} ", obj1);
                        stringBuilder.Append("</div>");
                    }
                    cell.Text = stringBuilder.ToString();
                }
            }
            else
            {
                cell.Controls.Clear();
            }
        }

        protected void Calendar1_Init(object sender, EventArgs e)
        {
        }

        protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.calendarId = base.GetParaValue("nodeId");
        }
    }
}