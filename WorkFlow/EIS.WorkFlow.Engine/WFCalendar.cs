using EIS.AppBase.Config;
using EIS.AppModel.Service;
using EIS.DataAccess;
using EIS.DataModel.Model;
using EIS.Permission.Service;
using System;

namespace EIS.WorkFlow.Engine
{
	public class WFCalendar
	{
		private const float HoursOnDay = 8f;

		public WFCalendar()
		{
		}

		private static DateTime? AdjustStartTime(DateTime startTime, string calendarId)
		{
			DateTime? nullable;
			if (!AppWorkDayService.IsWorkTime(startTime, calendarId))
			{
				AppWorkDay nextWorkDay = AppWorkDayService.GetNextWorkDay(startTime, calendarId);
				if (nextWorkDay != null)
				{
					nullable = new DateTime?(nextWorkDay.StartTime);
				}
				else
				{
					nullable = null;
				}
			}
			else
			{
				nullable = new DateTime?(startTime);
			}
			return nullable;
		}

		public static DateTime? ComputeNextWorkTime(DateTime startTime, TimeSpan ts, string calendarId)
		{
			DateTime? nullable;
			DateTime? nullable1;
			float days = 0f;
			if (ts.Days > 0)
			{
				days = (float)ts.Days * WFCalendar.GetHoursOneDay(calendarId) * 60f;
			}
			if (ts.Hours > 0)
			{
				days = days + (float)(ts.Hours * 60);
			}
			if (ts.Minutes > 0)
			{
				days = days + (float)ts.Minutes;
			}
			ts = TimeSpan.FromMinutes((double)days);
			DateTime? nullable2 = WFCalendar.AdjustStartTime(startTime, calendarId);
			if (nullable2.HasValue)
			{
				startTime = nullable2.Value;
				AppWorkDay workDayByTime = AppWorkDayService.GetWorkDayByTime(startTime, calendarId);
				while (ts.Ticks > (long)0)
				{
					if (workDayByTime.IsWorkDay == "是")
					{
						TimeSpan endTime = workDayByTime.EndTime - startTime;
						if (!(endTime >= ts))
						{
							ts = ts - endTime;
						}
						else
						{
							startTime = startTime + ts;
							break;
						}
					}
					workDayByTime = AppWorkDayService.GetNextWorkDay(workDayByTime.EndTime, calendarId);
					if (workDayByTime != null)
					{
						startTime = workDayByTime.StartTime;
					}
					else
					{
						nullable1 = null;
						nullable = nullable1;
						return nullable;
					}
				}
				nullable = new DateTime?(startTime);
			}
			else
			{
				nullable1 = null;
				nullable = nullable1;
			}
			return nullable;
		}

		public static string GetCalendarIdByDeptId(string deptId)
		{
			string deptWbsById = DepartmentService.GetDeptWbsById(deptId);
			object obj = SysDatabase.ExecuteScalar(string.Format("select top 1 CalendarId from T_E_Org_Department where '{0}' like DeptWBS+'%' and ISNULL(CalendarId,'')<>'' order by DeptWBS desc", deptWbsById));
			return ((obj == null ? false : obj != DBNull.Value) ? obj.ToString() : "");
		}

		public static float GetHoursOneDay(string calendarId)
		{
			float single;
			if (!(calendarId == ""))
			{
				object obj = SysDatabase.ExecuteScalar(string.Format("select HoursOneDay from T_E_App_Calendar where _AutoID='{0}'", calendarId));
				single = ((obj == null ? false : obj != DBNull.Value) ? float.Parse(obj.ToString()) : 8f);
			}
			else
			{
				string itemValue = SysConfig.GetConfig("HoursOneDay").ItemValue;
				single = (!string.IsNullOrEmpty(itemValue) ? float.Parse(itemValue) : 8f);
			}
			return single;
		}
	}
}