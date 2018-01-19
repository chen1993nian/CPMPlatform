using EIS.DataAccess;
using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace EIS.AppBase
{
	internal class SiteInfo
	{
		public string LoginTitle
		{
			get;
			set;
		}

		public string MainTitle
		{
			get;
			set;
		}

		public string RootURI
		{
			get;
			set;
		}

		public string WebId
		{
			get;
			set;
		}

		public string WebName
		{
			get;
			set;
		}

		public SiteInfo()
		{
		}

		public static SiteInfo GetSiteInfo(string webId)
		{
			SiteInfo siteInfo;
			DataTable dataTable = SysDatabase.ExecuteTable(string.Concat("Select * from T_E_Sys_WebId where webId=", webId));
			if (dataTable.Rows.Count <= 0)
			{
				siteInfo = null;
			}
			else
			{
				SiteInfo siteInfo1 = new SiteInfo()
				{
					WebId = webId,
					WebName = dataTable.Rows[0]["WebName"].ToString(),
					LoginTitle = dataTable.Rows[0]["LoginTitle"].ToString(),
					MainTitle = dataTable.Rows[0]["MainTitle"].ToString(),
					RootURI = dataTable.Rows[0]["SiteURI"].ToString()
				};
				if (siteInfo1.RootURI.Length > 0)
				{
					string rootURI = siteInfo1.RootURI;
					char[] chrArray = new char[] { '/' };
					siteInfo1.RootURI = rootURI.TrimEnd(chrArray);
				}
				siteInfo = siteInfo1;
			}
			return siteInfo;
		}
	}
}