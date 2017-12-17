using AjaxPro;
using EIS.AppBase;
using EIS.Permission.Service;
using EIS.Web.ModelLib.Model;
using EIS.Web.ModelLib.Service;
using System;
using System.Web;
using System.Web.UI;

namespace EIS.Web.WorkAsp.Calendar
{
    public partial class CalendarEdit : PageBase
    {
        public string sJson = "";

        private string string_0 = "";

        private string string_1 = "";


        private JsonReturnMessages method_0()
        {
            DateTime dateTime;
            DateTime dateTime1;
            JsonReturnMessages jsonReturnMessage;
            JsonReturnMessages message = new JsonReturnMessages();
            string item = base.Request["CalendarTitle"];
            string str = base.Request["CalendarStartTime"];
            string item1 = base.Request["CalendarEndTime"];
            string str1 = base.Request["IsAllDayEvent"];
            int num = Convert.ToInt32(base.Request["timezone"]);
            int timeZone = TimeHelper.GetTimeZone() - num;
            bool flag = DateTime.TryParse(item1, out dateTime);
            if (!DateTime.TryParse(str, out dateTime1))
            {
                message.IsSuccess = false;
                message.Msg = string.Concat("日期格式不正确:", str);
                jsonReturnMessage = message;
            }
            else if (flag)
            {
                try
                {
                    EIS.Web.ModelLib.Model.Calendar calendar = new EIS.Web.ModelLib.Model.Calendar(base.UserInfo)
                    {
                        EmpId = this.string_0,
                        EmpName = this.string_1,
                        Category = "1",
                        CategoryName = "公事",
                        CalendarType = 1,
                        InstanceType = 0,
                        Subject = item,
                        StartTime = dateTime1.AddHours((double)timeZone),
                        EndTime = dateTime.AddHours((double)timeZone),
                        IsAllDayEvent = int.Parse(str1),
                        Location = "",
                        MasterId = new int?(num)
                    };
                    CalendarService.AddCalendar(calendar);
                    message.Data = calendar._AutoID;
                    message.IsSuccess = true;
                    message.Msg = "操作成功!";
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    message.IsSuccess = false;
                    message.Msg = exception.Message;
                }
                jsonReturnMessage = message;
            }
            else
            {
                message.IsSuccess = false;
                message.Msg = string.Concat("日期格式不正确:", item1);
                jsonReturnMessage = message;
            }
            return jsonReturnMessage;
        }

        private JsonReturnMessages method_1()
        {
            DateTime dateTime;
            DateTime dateTime1;
            JsonReturnMessages jsonReturnMessage;
            JsonReturnMessages message = new JsonReturnMessages();
            string item = base.Request["calendarId"];
            string str = base.Request["CalendarStartTime"];
            string item1 = base.Request["CalendarEndTime"];
            int num = Convert.ToInt32(base.Request["timezone"]);
            int timeZone = TimeHelper.GetTimeZone() - num;
            bool flag = DateTime.TryParse(str, out dateTime);
            bool flag1 = DateTime.TryParse(item1, out dateTime1);
            if ((!flag ? false : flag1))
            {
                try
                {
                    EIS.Web.ModelLib.Model.Calendar calendar = CalendarService.GetCalendar(item);
                    calendar.StartTime = dateTime.AddHours((double)timeZone);
                    calendar.EndTime = dateTime1.AddHours((double)timeZone);
                    calendar.MasterId = new int?(num);
                    CalendarService.UpdateCalendar(calendar);
                    message.IsSuccess = true;
                    message.Msg = "操作成功!";
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    message.IsSuccess = false;
                    message.Msg = exception.Message;
                }
                jsonReturnMessage = message;
            }
            else
            {
                message.IsSuccess = false;
                message.Msg = string.Concat("日期格式不正确:", str);
                jsonReturnMessage = message;
            }
            return jsonReturnMessage;
        }

        private JsonReturnMessages method_2()
        {
            JsonReturnMessages jsonReturnMessage;
            JsonReturnMessages message = new JsonReturnMessages();
            string item = base.Request["calendarId"];
            if (!string.IsNullOrEmpty(item))
            {
                try
                {
                    CalendarService.DeleteCalendar(item);
                    message.IsSuccess = true;
                    message.Msg = "操作成功！";
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    message.IsSuccess = false;
                    message.Msg = exception.Message;
                }
                jsonReturnMessage = message;
            }
            else
            {
                message.IsSuccess = false;
                message.Msg = "calendarId参数不能为空";
                jsonReturnMessage = message;
            }
            return jsonReturnMessage;
        }

        private JsonReturnMessages method_3()
        {
            EIS.Web.ModelLib.Model.Calendar item;
            DateTime dateTime;
            string str = base.Request["Id"];
            JsonReturnMessages jsonReturnMessage = new JsonReturnMessages();
            try
            {
                item = (str.Length <= 0 ? new EIS.Web.ModelLib.Model.Calendar(base.UserInfo)
                {
                    _UserName = this.string_0
                } : CalendarService.GetCalendar(str));
                item.Subject = base.Request["Subject"];
                item.Location = base.Request["Location"];
                item.Description = base.Request["Description"];
                item.IsAllDayEvent = (base.Request["IsAllDayEvent"] == "false" ? 0 : 1);
                item.Category = base.Request["colorvalue"];
                string item1 = base.Request["stpartdate"];
                string str1 = base.Request["etpartdate"];
                int num = Convert.ToInt32(base.Request["timezone"]);
                int timeZone = TimeHelper.GetTimeZone() - num;
                if (item.IsAllDayEvent != 1)
                {
                    dateTime = Convert.ToDateTime(item1);
                    item.StartTime = dateTime.AddHours((double)timeZone);
                    dateTime = Convert.ToDateTime(str1);
                    item.EndTime = dateTime.AddHours((double)timeZone);
                }
                else
                {
                    dateTime = Convert.ToDateTime(item1);
                    item.StartTime = dateTime.AddHours((double)timeZone);
                    dateTime = Convert.ToDateTime(str1);
                    dateTime = dateTime.AddHours(23);
                    dateTime = dateTime.AddMinutes(59);
                    dateTime = dateTime.AddSeconds(59);
                    item.EndTime = dateTime.AddHours((double)timeZone);
                }
                if (item.EndTime <= item.StartTime)
                {
                    throw new Exception("结束日期小于开始日期");
                }
                item.CalendarType = 1;
                item.InstanceType = 0;
                item.MasterId = new int?(num);
                if (str.Length <= 0)
                {
                    CalendarService.AddCalendar(item);
                }
                else
                {
                    CalendarService.UpdateCalendar(item);
                }
                jsonReturnMessage.IsSuccess = true;
                jsonReturnMessage.Msg = "操作成功！";
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                jsonReturnMessage.IsSuccess = false;
                jsonReturnMessage.Msg = exception.Message;
            }
            return jsonReturnMessage;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string item = base.Request["act"];
            this.string_0 = (string.IsNullOrEmpty(base.Request["employeeId"]) ? base.EmployeeID : base.Request["employeeId"]);
            this.string_1 = EmployeeService.GetEmployeeName(this.string_0);
            base.Response.ContentType = "application/json; charset=utf-8";
            JsonReturnMessages jsonReturnMessage = new JsonReturnMessages();
            if (item == "add")
            {
                jsonReturnMessage = this.method_0();
            }
            else if (item == "update")
            {
                jsonReturnMessage = this.method_1();
            }
            else if (item == "delete")
            {
                jsonReturnMessage = this.method_2();
            }
            else if (item == "save")
            {
                jsonReturnMessage = this.method_3();
            }
            this.sJson = JavaScriptSerializer.Serialize(jsonReturnMessage);
        }
    }
}