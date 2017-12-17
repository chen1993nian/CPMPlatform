using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.Web.ModelLib.Model;
using EIS.Web.ModelLib.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using EIS.Web.WorkAsp.Calendar;
namespace EIS.Web.WorkAsp.Meeting
{
    public partial class GetMeeting : PageBase
    {
        public string sJson = "";

        public string showCancel = "false";

        private List<object[]> method_0(ICollection<EIS.Web.ModelLib.Model.Meeting> icollection_0)
        {
            List<object[]> objArrays = new List<object[]>();
            if ((icollection_0 == null ? false : icollection_0.Count > 0))
            {
                int timeZone = TimeHelper.GetTimeZone();
                foreach (EIS.Web.ModelLib.Model.Meeting icollection0 in icollection_0)
                {
                    if ((icollection0.HyState != "否" ? false : icollection0._WFState == ""))
                    {
                        continue;
                    }
                    int num = 8 - timeZone;
                    DateTime startTime = icollection0.StartTime;
                    DateTime dateTime = startTime.AddHours((double)num);
                    startTime = icollection0.EndTime;
                    DateTime dateTime1 = startTime.AddHours((double)num);
                    int num1 = 0;
                    if (!(icollection0.HyState == "已取消" ? false : !(icollection0._WFState == "终止")))
                    {
                        num1 = 0;
                        if (this.showCancel == "false")
                        {
                            continue;
                        }
                    }
                    else if (!(DateTime.Now <= dateTime ? true : !(DateTime.Now < dateTime1)))
                    {
                        num1 = 5;
                    }
                    else if (icollection0._WFState == "完成")
                    {
                        num1 = 1;
                    }
                    else if (icollection0._WFState == "处理中")
                    {
                        if (SysDatabase.ExecuteScalar(string.Format("select count(t._AutoID) from T_E_WF_Instance i inner join T_E_WF_Task t on i._AutoID=t.InstanceId\r\n                        where i.AppId='{0}'", icollection0._AutoID)).ToString() == "1")
                        {
                            continue;
                        }
                        num1 = 2;
                    }
                    else if ((icollection0._WFState != "" ? false : icollection0.HyState == "是"))
                    {
                        num1 = 1;
                    }
                    string str = "";
                    List<object[]> objArrays1 = objArrays;
                    object[] endTime = new object[12];
                    endTime[0] = icollection0._AutoID;
                    string[] hyName = new string[] { icollection0.HyName, " - ", icollection0.HyDept, "〔", icollection0.HyJbr, "〕" };
                    endTime[1] = string.Concat(hyName);
                    endTime[2] = icollection0.StartTime;
                    endTime[3] = icollection0.EndTime;
                    endTime[4] = icollection0.IsAllDayEvent;
                    endTime[5] = (dateTime.ToShortDateString() != dateTime1.ToShortDateString() ? 1 : 0);
                    endTime[6] = 0;
                    endTime[7] = num1;
                    endTime[8] = 1;
                    endTime[9] = icollection0.HyAddr;
                    endTime[10] = str;
                    endTime[11] = icollection0.JbrTel;
                    objArrays1.Add(endTime);
                }
            }
            return objArrays;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dateTime;
            string item;
            base.Response.ContentType = "application/json; charset=utf-8";
            CalendarViewType calendarViewType = (CalendarViewType)Enum.Parse(typeof(CalendarViewType), base.Request["viewtype"]);
            string str = base.Request["showdate"];
            int num = Convert.ToInt32(base.Request["timezone"]);
            int timeZone = TimeHelper.GetTimeZone() - num;
            if (!DateTime.TryParse(str, out dateTime))
            {
                base.Response.Write("不合法的日期格式");
            }
            CalendarViewFormat calendarViewFormat = new CalendarViewFormat(calendarViewType, dateTime, DayOfWeek.Monday);
            DateTime startDate = calendarViewFormat.StartDate;
            DateTime dateTime1 = startDate.AddHours((double)timeZone);
            startDate = calendarViewFormat.EndDate;
            DateTime dateTime2 = startDate.AddHours((double)timeZone);
            List<EIS.Web.ModelLib.Model.Meeting> meetings = new List<EIS.Web.ModelLib.Model.Meeting>();
            if (!string.IsNullOrEmpty(base.Request["hysName"]))
            {
                item = base.Request["hysName"];
                meetings = MeetingService.QueryCalendars(dateTime1, dateTime2, item);
            }
            else
            {
                foreach (DataRow row in SysDatabase.ExecuteTable("select * from T_OA_HYS_Info where HysState='可用' order by OrderId,HysName").Rows)
                {
                    item = row["HysName"].ToString();
                    meetings.AddRange(MeetingService.QueryCalendars(dateTime1, dateTime2, item));
                }
            }
            if (!string.IsNullOrEmpty(base.Request["showCancel"]))
            {
                this.showCancel = base.Request["showCancel"];
            }
            JsonCalendarViewData jsonCalendarViewDatum = new JsonCalendarViewData(this.method_0(meetings), calendarViewFormat.StartDate, calendarViewFormat.EndDate);
            this.sJson = JavaScriptSerializer.Serialize(jsonCalendarViewDatum);
        }
    }
}