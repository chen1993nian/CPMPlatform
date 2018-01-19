using AjaxPro;
using NLog;
using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace EIS.AppBase
{
	public abstract class AdminPageBase : System.Web.UI.Page
	{
		protected bool Logged = false;

		protected bool AutoRedirect = true;

		public _DataLog dblogger = null;

		protected Logger fileLogger = null;

		protected string AppPath
		{
			get
			{
				string str;
				str = (base.Request.ApplicationPath.EndsWith("/") ? base.Request.ApplicationPath : string.Concat(base.Request.ApplicationPath, "/"));
				return str;
			}
		}

		protected string CommonKey
		{
			get
			{
				return "mytech";
			}
		}

        public string PhotoId
        {
            get
            {
                if (HttpContext.Current.Session["PhotoId"] == null)
                {
                    throw new Exception("会话已过期");
                }
                return HttpContext.Current.Session["PhotoId"].ToString();

            }
        }

		public string EmployeeID
		{
			get
			{
				if (HttpContext.Current.Session["EmployeeId"] == null)
				{
					throw new Exception("会话已过期");
				}
				return HttpContext.Current.Session["EmployeeId"].ToString();
			}
		}

		public string EmployeeName
		{
			get
			{
                if (HttpContext.Current.Session["EmployeeName"] == null)
                {
                    throw new Exception("会话已过期");
                }
                return HttpContext.Current.Session["EmployeeName"].ToString();
			}
		}

		public string LoginType
		{
			get
			{
                if (HttpContext.Current.Session["LoginType"] == null)
                {
                    throw new Exception("会话已过期");
                }
                return HttpContext.Current.Session["LoginType"].ToString();
	
			}
		}

		public string OrgCode
		{
			get
			{
				return "";
			}
		}

		public UserContext UserInfo
		{
			get
			{
				UserContext userContext = new UserContext()
				{
					LoginName = this.UserName,
					CompanyId = HttpContext.Current.Session["CompanyId"].ToString(),
					CompanyCode = HttpContext.Current.Session["CompanyCode"].ToString(),
					CompanyWbs = HttpContext.Current.Session["CompanyWbs"].ToString(),
					CompanyName = HttpContext.Current.Session["CompanyName"].ToString(),
					DeptId = "",
					DeptWbs = "",
					DeptName = "",
					EmployeeId = this.EmployeeID,
					EmployeeName = this.UserName,
					PositionId = "",
					PositionName = "",
					WebId = AppSettings.Instance.WebId
				};
				return userContext;
			}
		}

		public string UserName
		{
			get
			{
				if (HttpContext.Current.Session["username"] == null)
				{
					throw new Exception("会话已过期");
				}
				return HttpContext.Current.Session["username"].ToString();
			}
		}

		public string Yun_ResourceRoot
		{
			get
			{
				return AppSettings.Instance.Yun_ResourceRoot;
			}
		}

		public AdminPageBase()
		{
			this.dblogger = new _DataLog();
			this.fileLogger = LogManager.GetCurrentClassLogger();
			AdminPageBase adminPageBase = this;
			base.Load += new EventHandler(adminPageBase.PageBase_Load);
			AdminPageBase adminPageBase1 = this;
			base.Error += new EventHandler(adminPageBase1.PageBase_Error);
		}

		protected void alert(string MsgStr)
		{
			base.Response.Write("<html>");
			base.Response.Write("<head>");
			base.Response.Write("<title>提示</title>");
			base.Response.Write("<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\">");
			base.Response.Write("</head>");
			base.Response.Write("<body>");
			base.Response.Write(string.Concat("<script type=\"text/javascript\">alert(\"", MsgStr.Replace("\r\n", ""), "\");history.back();</script>"));
			base.Response.Write("</body>");
			base.Response.Write("</html>");
		}

		 [AjaxMethod(HttpSessionStateRequirement.Read)]    // The parameters of the attribute are missing, because its assembly was not found
		public string CryptPara(string parastr)
		{
			string str = base.Server.UrlEncode(Security.EncryptStr(parastr, this.UserName));
			return str;
		}

         [AjaxMethod(HttpSessionStateRequirement.Read)]    // The parameters of the attribute are missing, because its assembly was not found
		public string DeCryptPara(string parastr)
		{
			string str = base.Server.UrlDecode(Security.Decrypt(parastr, this.UserName));
			return str;
		}

		protected virtual string FormatException(Exception ex, string catchInfo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (catchInfo != string.Empty)
			{
				stringBuilder.Append(catchInfo).Append("\r\n");
			}
			stringBuilder.Append(ex.Message).Append("\r\n").Append(ex.StackTrace);
			return stringBuilder.ToString();
		}

		public string GetClientIP()
		{
			return Utility.GetClientIP();
		}

		protected string GetOSNameByUserAgent()
		{
			return Utility.GetOSNameByUserAgent();
		}

		public string GetParaValue(string paraname)
		{
			string item;
			if (base.Request.QueryString[paraname] != null)
			{
				item = base.Request.QueryString[paraname];
			}
			else if (base.Request.Form[paraname] != null)
			{
				item = base.Request.Form[paraname];
			}
			else if (string.IsNullOrEmpty(base.Request["para"]))
			{
				item = "";
			}
			else
			{
				string str = base.Request["para"];
				str = Security.Decrypt(str, this.UserName);
				item = Security.GetUrlPara(str, paraname);
			}
			return item;
		}

		public string GetServerIP()
		{
            return HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
			
		}

		protected void Info(string scriptStr)
		{
			base.Response.Write("<html>");
			base.Response.Write("<head>");
			base.Response.Write("<title>提示</title>");
			base.Response.Write("<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\">");
			base.Response.Write("</head>");
			base.Response.Write("<body>");
			base.Response.Write(string.Concat("<script type=\"text/javascript\">", scriptStr, "</script>"));
			base.Response.Write("</body>");
			base.Response.Write("</html>");
		}

		protected virtual void PageBase_Error(object sender, EventArgs e)
		{
			DataLog dataLog = new DataLog()
			{
				ComputeIP = this.GetClientIP(),
				LogType = "错误",
				LogUser = (this.Session["username"] != null ? this.UserName : ""),
				UserName = (this.Session["EmployeeName"] != null ? this.EmployeeName : ""),
				ServerIP = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"],
				Message = this.FormatException(base.Server.GetLastError(), ""),
				Browser = Utility.GetBrowserByUserAgent(),
				Platform = this.GetOSNameByUserAgent(),
				UserAgent = base.Request.UserAgent
			};
			this.dblogger.WriteExceptionLog(dataLog);
			this.fileLogger.Error<Exception>(base.Server.GetLastError());
		}

		protected virtual void PageBase_Load(object sender, EventArgs e)
		{
			if ((!this.Page.User.Identity.IsAuthenticated ? false : this.Page.Session["username"] != null))
			{
				this.Logged = true;
			}
			else
			{
				this.Logged = false;
				if (this.AutoRedirect)
				{
					FormsAuthentication.RedirectToLoginPage();
					base.Response.End();
				}
			}
		}

		public virtual string ReplaceContext(string source)
		{
            foreach (Match match in (new Regex("\\[!(.*?)!\\]")).Matches(source))
            {
                string value = match.Groups[1].Value;
                try
                {
                    if (HttpContext.Current.Session[value] != null)
                    {
                        source = source.Replace(match.Value, HttpContext.Current.Session[value].ToString());
                    }
                }
                catch
                {
                    source = source.Replace(match.Value, "");
                }
            }
            return source;
		}

		protected virtual void ResponseError(Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("\r\n                <div class='ErrorMsg'>{0}</div>\r\n                ", exception.Message);
			base.ClientScript.RegisterStartupScript(base.GetType(), "", stringBuilder.ToString());
		}

		protected void WriteSecurityLog(string logType, string logMsg, string logUser)
		{
            DataLog dataLog = new DataLog()
            {
                AppID = "",
                AppName = "",
                ComputeIP = this.GetClientIP(),
                ServerIP = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"],
                LogType = logType
            };
            if (logUser != "")
            {
                dataLog.LogUser = logUser;
            }
            else
            {
                dataLog.LogUser = (this.Session["username"] != null ? this.UserName : "");
            }
            dataLog.UserName = (this.Session["EmployeeName"] != null ? this.EmployeeName : "");
            dataLog.ModuleCode = "";
            dataLog.ModuleName = "";
            dataLog.Message = logMsg;
            dataLog.Browser = Utility.GetBrowserByUserAgent();
            dataLog.Platform = this.GetOSNameByUserAgent();
            dataLog.UserAgent = base.Request.UserAgent;
            this.dblogger.WriteSecurityLog(dataLog);
		}
	}
}