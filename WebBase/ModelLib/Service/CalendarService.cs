using EIS.Web.ModelLib.Access;
using EIS.Web.ModelLib.Model;
using System;
using System.Collections.Generic;

namespace EIS.Web.ModelLib.Service
{
	public class CalendarService
	{
		public CalendarService()
		{
		}

		public static int AddCalendar(Calendar data)
		{
			return (new _Calendar()).Add(data);
		}

		public static int DeleteCalendar(string Id)
		{
			return (new _Calendar()).Delete(Id);
		}

		public static Calendar GetCalendar(string Id)
		{
			return (new _Calendar()).GetModel(Id);
		}

		public static List<Calendar> QueryCalendars(DateTime dateTime_0, DateTime dateTime_1, string empId)
		{
			_Calendar __Calendar = new _Calendar();
			List<Calendar> modelList = __Calendar.GetModelList(string.Format("empId='{0}' and ((StartTime>'{1}' and StartTime<'{2}') or (EndTime >= '{1}' and EndTime < '{2}') or (StartTime<'{1}' and EndTime >'{2}'))", empId, dateTime_0, dateTime_1));
			return modelList;
		}

		public static int UpdateCalendar(Calendar data)
		{
			return (new _Calendar()).Update(data);
		}
	}
}