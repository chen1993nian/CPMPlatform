using EIS.Web.ModelLib.Model;
using System;

namespace EIS.Web.WorkAsp.Calendar
{
    public class TimeHelper
    {
    
        public static int CheckIsCrossEvent(EIS.Web.ModelLib.Model.Calendar calendar)
        {
            int timeZone = TimeHelper.GetTimeZone();
            int num = (calendar.MasterId.HasValue ? calendar.MasterId.Value : 8) - timeZone;
            DateTime startTime = calendar.StartTime;
            DateTime dateTime = startTime.AddHours((double)num);
            startTime = calendar.EndTime;
            DateTime dateTime1 = startTime.AddHours((double)num);
            return (dateTime.ToShortDateString() != dateTime1.ToShortDateString() ? 1 : 0);
        }

        public static int GetTimeZone()
        {
            DateTime now = DateTime.Now;
            return (now - now.ToUniversalTime()).Hours;
        }

        public static long MilliTimeStamp(DateTime theDate)
        {
            DateTime dateTime = new DateTime(1970, 1, 1);
            DateTime universalTime = theDate.ToUniversalTime();
            TimeSpan timeSpan = new TimeSpan(universalTime.Ticks - dateTime.Ticks);
            return (long)timeSpan.TotalMilliseconds;
        }
    }
}