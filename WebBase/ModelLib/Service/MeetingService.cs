using EIS.Web.ModelLib.Access;
using EIS.Web.ModelLib.Model;
using System;
using System.Collections.Generic;

namespace EIS.Web.ModelLib.Service
{
	public class MeetingService
	{
		public MeetingService()
		{
		}

		public static Calendar GetCalendar(string Id)
		{
			return (new _Calendar()).GetModel(Id);
		}

		public static List<Meeting> QueryCalendars(DateTime dateTime_0, DateTime dateTime_1, string hysName)
		{
			_Meeting __Meeting = new _Meeting();
			List<Meeting> modelList = __Meeting.GetModelList(string.Format("HyAddr='{0}' and ((StartTime>='{1}' and StartTime<='{2}') or (EndTime >= '{1}' and EndTime <= '{2}') or (StartTime<='{1}' and EndTime >='{2}'))", hysName, dateTime_0, dateTime_1));
			return modelList;
		}
	}
}