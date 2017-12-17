using System;
using System.Runtime.CompilerServices;

namespace EIS.Web.WorkAsp.Calendar
{
	public class JsonError
	{
		public string ErrorCode
		{
			get;
		    set;
		}

        public string ErrorMsg
        {
            get;
            set;
        }

		public JsonError(string code, string string_2)
		{
			this.ErrorCode = code;
            this.ErrorMsg = string_2;
		}
	}
}