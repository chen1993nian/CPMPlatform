using EIS.DataAccess;
using NLog;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;

namespace EIS.AppBase
{
	public class AppFilePath
	{
		private static Logger fileLogger;

		private static Hashtable itemHash;

		static AppFilePath()
		{
			AppFilePath.fileLogger = LogManager.GetCurrentClassLogger();
			AppFilePath.itemHash = new Hashtable(40);
		}

		public AppFilePath()
		{
		}

		public static string GetActiveBasePath(string serverIP)
		{
			object obj = SysDatabase.ExecuteScalar(string.Format("Select top 1 BaseCode+'|'+BasePath from T_E_File_Config \r\n            where Enable='是' and (ServerIP='' or ServerIP='{0}') order by ServerIP desc", serverIP));
			if ((obj == null ? true : obj == DBNull.Value))
			{
				AppFilePath.fileLogger.Error("找不到附件目录配置项");
				throw new Exception("找不到附件目录配置项");
			}
			AppFilePath.fileLogger.Trace("GetActiveBasePath:{0}", obj.ToString());
			return obj.ToString();
		}

		public static string GetBasePath(string baseCode)
		{
			string item = "";
			if (HttpContext.Current != null)
			{
				item = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
			}
			return AppFilePath.GetBasePath(item, baseCode, true);
		}

		public static string GetBasePath(string serverIP, string baseCode, bool cacheFirst)
		{
			string str;
			if (cacheFirst)
			{
				if (AppFilePath.itemHash.ContainsKey(baseCode))
				{
					str = AppFilePath.itemHash[baseCode].ToString();
					return str;
				}
			}
			string str1 = string.Format("Select top 1 basePath from T_E_File_Config where baseCode ='{0}' \r\n                    and (ServerIP='' or ServerIP='{1}') order by ServerIP desc", baseCode, serverIP);
			object obj = SysDatabase.ExecuteScalar(str1);
			if ((obj == null ? true : obj == DBNull.Value))
			{
				str = "";
			}
			else
			{
				string str2 = obj.ToString();
				if (!str2.EndsWith("\\"))
				{
					str2 = string.Concat(str2, "\\");
				}
				AppFilePath.itemHash[baseCode] = str2;
				str = str2;
			}
			return str;
		}
	}
}