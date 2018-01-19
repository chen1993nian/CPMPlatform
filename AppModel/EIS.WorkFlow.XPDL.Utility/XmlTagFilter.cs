using System;

namespace EIS.WorkFlow.XPDL.Utility
{
	public class XmlTagFilter
	{
		public static string toEsc(string normal)
		{
			string str = normal.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
			return str;
		}

		public static string toMessageEsc(string normal)
		{
			string str = normal.Replace("<", "nucleusxiaoyuhao").Replace(">", "nucleusdayuhao").Replace("\"", "nucleusshuangyinhao");
			return str;
		}

		public static string toMessageNormal(string string_0)
		{
			string str = string_0.Replace("nucleusxiaoyuhao", "<").Replace("nucleusdayuhao", ">").Replace("nucleusshuangyinhao", "\"");
			return str;
		}

		public static string toNormal(string string_0)
		{
			string str = string_0.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"");
			return str;
		}
	}
}