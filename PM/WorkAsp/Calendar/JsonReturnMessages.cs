using System;
using System.Runtime.CompilerServices;

namespace EIS.Web.WorkAsp.Calendar
{
	public class JsonReturnMessages
	{
		public object Data
		{
			get;
			set;
		}

		public bool IsSuccess
		{
			get;
			set;
		}

		public string Msg
		{
			get;
			set;
		}

		public JsonReturnMessages()
		{
		}
	}
}