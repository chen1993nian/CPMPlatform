using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace EIS.Web.WorkAsp.Calendar
{
	public class JsonCalendarViewData
	{
		public DateTime DateTime_0
		{
			get;
			private set;
		}

		public JsonError error
		{
			get;
			private set;
		}

		public List<object[]> events
		{
			get;
			private set;
		}

		public bool issort
		{
			get;
			private set;
		}

		public DateTime start
		{
			get;
			private set;
		}

		public JsonCalendarViewData(List<object[]> eventList, DateTime startDate, DateTime endDate)
		{
			this.events = eventList;
			this.start = startDate;
			this.DateTime_0 = endDate;
			this.issort = true;
		}

		public JsonCalendarViewData(List<object[]> eventList, DateTime startDate, DateTime endDate, bool isSort)
		{
			this.start = startDate;
			this.DateTime_0 = endDate;
			this.events = eventList;
			this.issort = isSort;
		}

		public JsonCalendarViewData(JsonError jsonError)
		{
			this.error = jsonError;
		}
	}
}