using AjaxPro;
using EIS.AppBase;
using EIS.Web.ModelLib.Model;
using EIS.Web.ModelLib.Service;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace EIS.Web.WorkAsp.Calendar
{
    public partial class GetCalendarData : PageBase
    {
        public string sJson = "";

       

        private List<object[]> method_0(ICollection<EIS.Web.ModelLib.Model.Calendar> icollection_0)
        {
            List<object[]> objArrays = new List<object[]>();
            if ((icollection_0 == null ? false : icollection_0.Count > 0))
            {
                int timeZone = TimeHelper.GetTimeZone();
                foreach (EIS.Web.ModelLib.Model.Calendar icollection0 in icollection_0)
                {
                    int num = (icollection0.MasterId.HasValue ? icollection0.MasterId.Value : 8) - timeZone;
                    DateTime startTime = icollection0.StartTime;
                    DateTime dateTime = startTime.AddHours((double)num);
                    startTime = icollection0.EndTime;
                    DateTime dateTime1 = startTime.AddHours((double)num);
                    string str = string.Concat(icollection0.AttendeeNames, (string.IsNullOrEmpty(icollection0.OtherAttendee) ? "" : string.Concat(",", icollection0.OtherAttendee)));
                    List<object[]> objArrays1 = objArrays;
                    object[] subject = new object[] { icollection0._AutoID, icollection0.Subject, icollection0.StartTime, icollection0.EndTime, icollection0.IsAllDayEvent, null, null, null, null, null, null };
                    subject[5] = (dateTime.ToShortDateString() != dateTime1.ToShortDateString() ? 1 : 0);
                    subject[6] = (icollection0.InstanceType == 2 ? 1 : 0);
                    subject[7] = (string.IsNullOrEmpty(icollection0.Category) ? -1 : Convert.ToInt32(icollection0.Category));
                    subject[8] = 1;
                    subject[9] = icollection0.Location;
                    subject[10] = str;
                    objArrays1.Add(subject);
                }
            }
            return objArrays;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dateTime;
            base.Response.ContentType = "application/json; charset=utf-8";
            CalendarViewType calendarViewType = (CalendarViewType)Enum.Parse(typeof(CalendarViewType), base.Request["viewtype"]);
            string str = (string.IsNullOrEmpty(base.Request["employeeId"]) ? base.EmployeeID : base.Request["employeeId"]);
            string item = base.Request["showdate"];
            int num = Convert.ToInt32(base.Request["timezone"]);
            int timeZone = TimeHelper.GetTimeZone() - num;
            if (!DateTime.TryParse(item, out dateTime))
            {
                base.Response.Write("不合法的日期格式");
            }
            CalendarViewFormat calendarViewFormat = new CalendarViewFormat(calendarViewType, dateTime, DayOfWeek.Monday);
            DateTime startDate = calendarViewFormat.StartDate;
            DateTime dateTime1 = startDate.AddHours((double)timeZone);
            startDate = calendarViewFormat.EndDate;
            DateTime dateTime2 = startDate.AddHours((double)timeZone);
            List<EIS.Web.ModelLib.Model.Calendar> calendars = CalendarService.QueryCalendars(dateTime1, dateTime2, str);
            JsonCalendarViewData jsonCalendarViewDatum = new JsonCalendarViewData(this.method_0(calendars), calendarViewFormat.StartDate, calendarViewFormat.EndDate);
            this.sJson = JavaScriptSerializer.Serialize(jsonCalendarViewDatum);
        }
    }
}