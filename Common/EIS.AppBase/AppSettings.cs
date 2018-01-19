using EIS.AppBase.Config;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web;
using System.Web.Caching;

namespace EIS.AppBase
{
	public class AppSettings
	{
		private static AppSettings _obj;

		private string _AppFileSavePath = "";

		private string _AppFileBaseCode = "";

		private int _AppFileMaxSize = 104857600;

		private string _WebId = "";

		private StringDictionary _mimemap = new StringDictionary();

		private string _Scheduler_Enable = "0";

		private static Hashtable SitesInfo;

		public string AdminDefaultPass
		{
			get;
			set;
		}

		public string AppFileBaseCode
		{
			get
			{
				if (HttpContext.Current.Cache["AppFileSavePath"] == null)
				{
					string item = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
					string activeBasePath = AppFilePath.GetActiveBasePath(item);
					string[] strArrays = activeBasePath.Split(new char[] { '|' });
					if (!strArrays[1].EndsWith("\\"))
					{
						string[] strArrays1 = strArrays;
						string[] strArrays2 = strArrays1;
						strArrays1[1] = string.Concat(strArrays2[1], "\\");
					}
					this._AppFileSavePath = strArrays[1];
					this._AppFileBaseCode = strArrays[0];
					Cache cache = HttpRuntime.Cache;
					string str = this._AppFileSavePath;
					DateTime now = DateTime.Now;
					cache.Insert("AppFileSavePath", str, null, now.AddHours(1), TimeSpan.Zero);
				}
				return this._AppFileBaseCode;
			}
		}

		public int AppFileMaxSize
		{
			get
			{
				return this._AppFileMaxSize;
			}
		}

		public string AppFileSavePath
		{
			get
			{
				if (HttpContext.Current.Cache["AppFileSavePath"] == null)
				{
					string item = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
					string activeBasePath = AppFilePath.GetActiveBasePath(item);
					string[] strArrays = activeBasePath.Split(new char[] { '|' });
					if (!strArrays[1].EndsWith("\\"))
					{
						string[] strArrays1 = strArrays;
						string[] strArrays2 = strArrays1;
						strArrays1[1] = string.Concat(strArrays2[1], "\\");
					}
					this._AppFileSavePath = strArrays[1];
					this._AppFileBaseCode = strArrays[0];
					Cache cache = HttpRuntime.Cache;
					string str = this._AppFileSavePath;
					DateTime now = DateTime.Now;
					cache.Insert("AppFileSavePath", str, null, now.AddHours(1), TimeSpan.Zero);
				}
				return this._AppFileSavePath;
			}
		}

		public string EmployeeDefaultPass
		{
			get;
			set;
		}

		public static AppSettings Instance
		{
			get
			{
				if (AppSettings._obj == null)
				{
					Mutex mutex = new Mutex();
					mutex.WaitOne();
					if (AppSettings._obj == null)
					{
						AppSettings._obj = new AppSettings();
					}
					mutex.Close();
				}
				return AppSettings._obj;
			}
		}

		public LDAPConfig LdapConfig
		{
			get;
			set;
		}

		public EIS.AppBase.Config.MailConfig MailConfig
		{
			get;
			set;
		}

		public StringDictionary MimeMap
		{
			get
			{
				return this._mimemap;
			}
		}

		public bool Scheduler_Enable
		{
			get
			{
				return this._Scheduler_Enable == "1";
			}
		}

		public string SignRejectAction
		{
			get;
			set;
		}

		public string SignRejectText
		{
			get;
			set;
		}

		public string WebId
		{
			get
			{
				return this._WebId;
			}
			set
			{
				this._WebId = value;
			}
		}

		public bool XPDLInFolder
		{
			get
			{
				return true;
			}
		}

		public string Yun_ResourceRoot
		{
			get;
			set;
		}

		public string Yun_ServiceRoot
		{
			get;
			set;
		}

		static AppSettings()
		{
			AppSettings.SitesInfo = new Hashtable(10);
		}

		private AppSettings()
		{
			char[] chrArray;
			this._mimemap.Add(".bmp", "image/bmp");
			this._mimemap.Add(".doc", "application/msword");
			this._mimemap.Add(".gif", "image/gif");
			this._mimemap.Add(".htm", "text/html");
			this._mimemap.Add(".html", "text/html");
			this._mimemap.Add(".jpe", "image/jpeg");
			this._mimemap.Add(".jpeg", "image/jpeg");
			this._mimemap.Add(".jpg", "image/jpeg");
			this._mimemap.Add(".mid", "audio/mid");
			this._mimemap.Add(".mov", "video/quicktime");
			this._mimemap.Add(".mp3", "audio/mpeg");
			this._mimemap.Add(".mpe", "video/mpeg");
			this._mimemap.Add(".mpeg", "video/mpeg");
			this._mimemap.Add(".mpg", "video/mpeg");
			this._mimemap.Add(".mpp", "application/vnd.ms-project");
			this._mimemap.Add(".mpv2", "video/mpeg");
			this._mimemap.Add(".pdf", "application/pdf");
			this._mimemap.Add(".png", "image/png");
			this._mimemap.Add(".pot", "application/vnd.ms-powerpoint");
			this._mimemap.Add(".pps", "application/vnd.ms-powerpoint");
			this._mimemap.Add(".ppt", "application/vnd.ms-powerpoint");
			this._mimemap.Add(".wav", "audio/wav");
			this._mimemap.Add(".xls", "application/vnd.ms-excel");
			this._mimemap.Add(".xml", "text/xml");
			this._mimemap.Add(".zip", "application/x-zip-compressed");
			this._mimemap.Add(".rar", "application/x-zip-compressed");
			if (ConfigurationManager.AppSettings["WebId"] != null)
			{
				this._WebId = ConfigurationManager.AppSettings["WebId"].ToString();
			}
			if (ConfigurationManager.AppSettings["Scheduler_Enable"] != null)
			{
				this._Scheduler_Enable = ConfigurationManager.AppSettings["Scheduler_Enable"].ToString();
			}
			if (ConfigurationManager.AppSettings["AppFileMaxSize"] != null)
			{
				this._AppFileMaxSize = Convert.ToInt32(ConfigurationManager.AppSettings["AppFileMaxSize"].ToString());
			}
			this.LdapConfig = LDAPConfig.GetConfig();
			this.MailConfig = EIS.AppBase.Config.MailConfig.GetConfig();
			this.EmployeeDefaultPass = SysConfig.GetConfig("EmployeeDefaultPass").ItemValue;
			this.AdminDefaultPass = SysConfig.GetConfig("AdminDefaultPass").ItemValue;
			if (!(SysConfig.GetConfig("Basic_ResourceMethod").ItemValue == "1"))
			{
				chrArray = new char[] { '/' };
				this.Yun_ResourceRoot = "".TrimEnd(chrArray);
			}
			else
			{
				string itemValue = SysConfig.GetConfig("Basic_ResourceRoot").ItemValue;
				chrArray = new char[] { '/' };
				this.Yun_ResourceRoot = itemValue.TrimEnd(chrArray);
			}
			string str = SysConfig.GetConfig("Basic_ServiceRoot").ItemValue;
			chrArray = new char[] { '/' };
			this.Yun_ServiceRoot = str.TrimEnd(chrArray);
			this.SignRejectText = SysConfig.GetConfig("WF_SignRejectText").ItemValue;
			if (string.IsNullOrEmpty(this.SignRejectText))
			{
				this.SignRejectText = "退 回";
			}
			this.SignRejectAction = this.SignRejectText.Replace(" ", "");
		}

		public static string GenAutoLoginUrl(string loginName, string webId, string returnUrl, int t)
		{
			long ticks = DateTime.Now.Ticks;
			string str = Security.EncryptStr(string.Concat("u=", loginName, "&t=", ticks.ToString()), "mytech");
			string str1 = string.Concat(AppSettings.GetRootURI(webId), "/Default.aspx");
			string[] strArrays = new string[] { str1, "?loginKey=", str, "&returnUrl=", HttpUtility.UrlEncode(returnUrl) };
			return string.Concat(strArrays);
		}

		public static string GetLoginTitle(string webId)
		{
			SiteInfo siteInfo;
			string loginTitle = "登录";
			if (!AppSettings.SitesInfo.ContainsKey(webId))
			{
				siteInfo = SiteInfo.GetSiteInfo(webId);
				if (siteInfo != null)
				{
					AppSettings.SitesInfo.Add(webId, siteInfo);
					loginTitle = siteInfo.LoginTitle;
				}
			}
			else
			{
				siteInfo = AppSettings.SitesInfo[webId] as SiteInfo;
				if (siteInfo != null)
				{
					loginTitle = siteInfo.LoginTitle;
				}
			}
			return (loginTitle == "" ? "登录" : loginTitle);
		}

		public static string GetMainTitle(string webId)
		{
			SiteInfo siteInfo;
			string mainTitle = "";
			if (!AppSettings.SitesInfo.ContainsKey(webId))
			{
				siteInfo = SiteInfo.GetSiteInfo(webId);
				if (siteInfo != null)
				{
					AppSettings.SitesInfo.Add(webId, siteInfo);
					mainTitle = siteInfo.MainTitle;
				}
			}
			else
			{
				siteInfo = AppSettings.SitesInfo[webId] as SiteInfo;
				if (siteInfo != null)
				{
					mainTitle = siteInfo.MainTitle;
				}
			}
			return (mainTitle == "" ? "协同办公平台" : mainTitle);
		}

		public static string GetRootURI(string webId)
		{
			SiteInfo siteInfo;
			string rootURI = "";
			if (!AppSettings.SitesInfo.ContainsKey(webId))
			{
				siteInfo = SiteInfo.GetSiteInfo(webId);
				if (siteInfo != null)
				{
					AppSettings.SitesInfo.Add(webId, siteInfo);
					rootURI = siteInfo.RootURI;
				}
			}
			else
			{
				siteInfo = AppSettings.SitesInfo[webId] as SiteInfo;
				if (siteInfo != null)
				{
					rootURI = siteInfo.RootURI;
				}
			}
			return (rootURI.Trim() == "" ? "" : rootURI);
		}

		public static string GetWebName(string webId)
		{
			SiteInfo siteInfo;
			string webName = "";
			if (!AppSettings.SitesInfo.ContainsKey(webId))
			{
				siteInfo = SiteInfo.GetSiteInfo(webId);
				if (siteInfo != null)
				{
					AppSettings.SitesInfo.Add(webId, siteInfo);
					webName = siteInfo.WebName;
				}
			}
			else
			{
				siteInfo = AppSettings.SitesInfo[webId] as SiteInfo;
				if (siteInfo != null)
				{
					webName = siteInfo.WebName;
				}
			}
			return (webName == "" ? "协同办公系统" : webName);
		}

		public static void Reload()
		{
			char[] chrArray;
			SysConfig.Clear();
			AppSettings.Instance.LdapConfig = LDAPConfig.GetConfig();
			AppSettings.Instance.MailConfig = EIS.AppBase.Config.MailConfig.GetConfig();
			AppSettings.Instance.AdminDefaultPass = SysConfig.GetConfig("AdminDefaultPass").ItemValue;
			AppSettings.Instance.EmployeeDefaultPass = SysConfig.GetConfig("EmployeeDefaultPass").ItemValue;
			if (!(SysConfig.GetConfig("Basic_ResourceMethod").ItemValue == "1"))
			{
				AppSettings instance = AppSettings.Instance;
				chrArray = new char[] { '/' };
				instance.Yun_ResourceRoot = "".TrimEnd(chrArray);
			}
			else
			{
				AppSettings appSetting = AppSettings.Instance;
				string itemValue = SysConfig.GetConfig("Basic_ResourceRoot").ItemValue;
				chrArray = new char[] { '/' };
				appSetting.Yun_ResourceRoot = itemValue.TrimEnd(chrArray);
			}
			AppSettings instance1 = AppSettings.Instance;
			string str = SysConfig.GetConfig("Basic_ServiceRoot").ItemValue;
			chrArray = new char[] { '/' };
			instance1.Yun_ServiceRoot = str.TrimEnd(chrArray);
			AppSettings.Instance.SignRejectText = SysConfig.GetConfig("WF_SignRejectText").ItemValue;
			if (string.IsNullOrEmpty(AppSettings.Instance.SignRejectText))
			{
				AppSettings.Instance.SignRejectText = "退 回";
			}
			AppSettings.Instance.SignRejectAction = AppSettings.Instance.SignRejectText.Replace(" ", "");
			AppSettings.SitesInfo.Clear();
			HttpContext.Current.Cache.Remove("AppFileSavePath");
		}
	}
}