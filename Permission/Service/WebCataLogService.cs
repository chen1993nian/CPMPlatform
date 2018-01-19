using EIS.DataAccess;
using EIS.Permission.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EIS.Permission.Service
{
	public class WebCataLogService
	{
		public WebCataLogService()
		{
		}

		public static List<WebCataLog> GetCataLogList(string webType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select * from t_e_sys_webid ");
			if (webType != "")
			{
				stringBuilder.AppendFormat(" where webType='{0}'", webType);
			}
			DataTable dataTable = SysDatabase.ExecuteTable(stringBuilder.ToString());
			List<WebCataLog> webCataLogs = new List<WebCataLog>();
			foreach (DataRow row in dataTable.Rows)
			{
				WebCataLog webCataLog = new WebCataLog()
				{
					WebId = row["webid"].ToString(),
					WebName = row["webName"].ToString(),
                    WebType = row["webType"].ToString(),
                    ShowState = row["ShowState"].ToString()
				};
				webCataLogs.Add(webCataLog);
			}
			return webCataLogs;
		}
	}
}