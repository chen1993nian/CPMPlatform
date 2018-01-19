using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace EIS.AppModel.Service
{
	public class AppWorkDayService
	{
		public AppWorkDayService()
		{
		}

		public static List<AppWorkDay> GetDataByDay(DateTime date)
		{
			return (new _AppWorkDay()).GetModelsByDate(date);
		}

		public static List<AppWorkDay> GetDataByDay(DateTime date, string calendarId)
		{
			return (new _AppWorkDay()).GetModelsByDate(date, calendarId);
		}

		public static List<AppWorkDay> GetDataByMonth(int year, int month, string calendarId)
		{
			_AppWorkDay __AppWorkDay = new _AppWorkDay();
			List<AppWorkDay> modelList = __AppWorkDay.GetModelList(string.Format("year(WorkDate)={0} and month(WorkDate)={1} and isnull(CompanyId,'')='{2}'", year, month, calendarId));
			return modelList;
		}

		public static AppWorkDay GetNextWorkDay(DateTime startTime, string calendarId)
		{
			return (new _AppWorkDay()).GetNextWorkDay(startTime, calendarId);
		}

		public static AppWorkDay GetWorkDayByTime(DateTime testTime, string calendarId)
		{
			return (new _AppWorkDay()).GetWorkDayByTime(testTime, calendarId);
		}

		public static void InitWorkDay(int year, string am_startTime, string am_endTime, string pm_startTime, string pm_endTime, bool[] flags, string calendarId)
		{
			DbConnection dbConnection = SysDatabase.CreateConnection();
			dbConnection.Open();
			DbTransaction dbTransaction = dbConnection.BeginTransaction();
			try
			{
				try
				{
					_AppWorkDay __AppWorkDay = new _AppWorkDay(dbTransaction);
					string[] str = new string[] { " year(WorkDate)=", year.ToString(), " and companyId='", calendarId, "'" };
					__AppWorkDay.RemoveByCondition(string.Concat(str));
					DateTime dateTime = new DateTime(year, 1, 1);
					DateTime dateTime1 = new DateTime(year + 1, 1, 1);
					while (dateTime < dateTime1)
					{
						AppWorkDay appWorkDay = new AppWorkDay()
						{
							CompanyId = calendarId,
							WorkDate = dateTime
						};
						if (!flags[(int)dateTime.DayOfWeek])
						{
							appWorkDay.IsWorkDay = "否";
							appWorkDay.DayType = "周末";
						}
						else
						{
							appWorkDay.IsWorkDay = "是";
							appWorkDay.DayType = "工作日";
						}
						appWorkDay.StartTime = Convert.ToDateTime(string.Concat(dateTime.ToString("yyyy-MM-dd "), am_startTime));
						appWorkDay.EndTime = Convert.ToDateTime(string.Concat(dateTime.ToString("yyyy-MM-dd "), am_endTime));
						__AppWorkDay.Add(appWorkDay);
						AppWorkDay appWorkDay1 = new AppWorkDay()
						{
							CompanyId = calendarId,
							WorkDate = dateTime
						};
						if (!flags[(int)dateTime.DayOfWeek])
						{
							appWorkDay1.IsWorkDay = "否";
							appWorkDay.DayType = "周末";
						}
						else
						{
							appWorkDay1.IsWorkDay = "是";
							appWorkDay.DayType = "工作日";
						}
						appWorkDay1.StartTime = Convert.ToDateTime(string.Concat(dateTime.ToString("yyyy-MM-dd "), pm_startTime));
						appWorkDay1.EndTime = Convert.ToDateTime(string.Concat(dateTime.ToString("yyyy-MM-dd "), pm_endTime));
						__AppWorkDay.Add(appWorkDay1);
						dateTime = dateTime.AddDays(1);
					}
					dbTransaction.Commit();
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					dbTransaction.Rollback();
					throw exception;
				}
			}
			finally
			{
				dbConnection.Close();
			}
		}

		public static bool IsWorkTime(string calendarId)
		{
			return AppWorkDayService.IsWorkTime(DateTime.Now, calendarId);
		}

		public static bool IsWorkTime(DateTime time, string calendarId)
		{
			bool flag;
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand("select count(*) from T_E_App_WorkDay where  IsWorkDay='是' and isnull(CompanyId,'')=@calendarId and @time between StartTime and EndTime");
			SysDatabase.AddInParameter(sqlStringCommand, "@time", DbType.DateTime, time);
			SysDatabase.AddInParameter(sqlStringCommand, "@calendarId", DbType.String, calendarId);
			object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
			flag = ((obj == DBNull.Value ? false : obj != null) ? Convert.ToInt32(obj) > 0 : false);
			return flag;
		}
	}
}