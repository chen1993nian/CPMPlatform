using System;
using System.Runtime.CompilerServices;

namespace EIS.Web.WorkAsp.Calendar
{
    public class CalendarViewFormat
    {
        public DateTime EndDate
        {
            get;
            private set;
        }

        public DateTime StartDate
        {
            get;
            private set;
        }

        public CalendarViewFormat(CalendarViewType viewType, DateTime showday, DayOfWeek weekStartDay)
        {
            int hashCode;
            DateTime date;
            int num;
            bool month;
            switch (viewType)
            {
                case CalendarViewType.const_0:
                    {
                        this.StartDate = showday.Date;
                        date = showday.Date;
                        date = date.AddHours(23);
                        date = date.AddMinutes(59);
                        this.EndDate = date.AddSeconds(59);
                        return;
                    }
                case CalendarViewType.week:
                    {
                        num = weekStartDay.GetHashCode();
                        hashCode = num - showday.DayOfWeek.GetHashCode();
                        if (hashCode > 0)
                        {
                            hashCode = hashCode - 7;
                        }
                        date = showday.AddDays((double)hashCode);
                        this.StartDate = date.Date;
                        date = this.StartDate.AddDays(6);
                        date = date.AddHours(23);
                        date = date.AddMinutes(59);
                        this.EndDate = date.AddSeconds(59);
                        return;
                    }
                case CalendarViewType.workweek:
                    {
                        return;
                    }
                case CalendarViewType.month:
                    {
                        DateTime dateTime = new DateTime(showday.Year, showday.Month, 1);
                        num = weekStartDay.GetHashCode();
                        hashCode = num - dateTime.DayOfWeek.GetHashCode();
                        if (hashCode > 0)
                        {
                            hashCode = hashCode - 7;
                        }
                        date = dateTime.AddDays((double)hashCode);
                        this.StartDate = date.Date;
                        date = this.StartDate;
                        this.EndDate = date.AddDays(34);
                        if (this.EndDate.Year != showday.Year || this.EndDate.Month != showday.Month)
                        {
                            month = true;
                        }
                        else
                        {
                            date = this.EndDate.AddDays(1);
                            int month1 = date.Month;
                            date = this.EndDate;
                            month = month1 != date.Month;
                        }
                        if (!month)
                        {
                            date = this.EndDate;
                            this.EndDate = date.AddDays(7);
                        }
                        date = this.EndDate.AddHours(23);
                        date = date.AddMinutes(59);
                        date.AddSeconds(59);
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }
    }
}