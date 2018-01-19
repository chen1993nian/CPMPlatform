using AjaxPro;
using EIS.AppBase.Config;
using EIS.DataAccess;

using EIS.DataModel.Model;
using NLog;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

using EIS.AppBase.Config;
using EIS.AppCommo;
namespace EIS.AppBase
{
	public abstract class PageBase : System.Web.UI.Page
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

		public string CompanyId
		{
			get
			{
				if (HttpContext.Current.Session["CompanyId"] == null)
				{
					throw new Exception("会话已过期");
				}
				return HttpContext.Current.Session["CompanyId"].ToString();
			}
		}

		protected string DecodedPara
		{
			get
			{
				string str;
				if (string.IsNullOrEmpty(base.Request["para"]))
				{
					str = "";
				}
				else
				{
					string item = base.Request["para"];
					str = Security.Decrypt(item, this.UserName);
				}
				return str;
			}
		}

		public string EmployeeID
		{
			get
			{
				if (HttpContext.Current.Session["EmployeeID"] == null)
				{
					throw new Exception("会话已过期");
				}
				return HttpContext.Current.Session["EmployeeID"].ToString();
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

		public string OrgCode
		{
			get
			{
				if (HttpContext.Current.Session["deptwbs"] == null)
				{
					throw new Exception("会话已过期");
				}
				return HttpContext.Current.Session["deptwbs"].ToString();
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
					DeptId = HttpContext.Current.Session["DeptId"].ToString(),
					DeptWbs = this.OrgCode,
					DeptName = HttpContext.Current.Session["DeptName"].ToString(),
					EmployeeId = this.EmployeeID,
					EmployeeName = this.EmployeeName,
					PositionId = HttpContext.Current.Session["PositionId"].ToString(),
					PositionName = HttpContext.Current.Session["PositionName"].ToString(),
					WebId = HttpContext.Current.Session["WebId"].ToString()
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

        public string AppIndex
        {
            get
            {
                if (HttpContext.Current.Items["AppIndex"] == null)
                {
                    return("");
                }
                return HttpContext.Current.Items["AppIndex"].ToString();
            }
            set {
                if (HttpContext.Current.Items["AppIndex"] == null)
                {
                    HttpContext.Current.Items.Add("AppIndex", value);
                }
                else
                {
                    HttpContext.Current.Items["AppIndex"] = value;
                }

                if (HttpContext.Current.Session["AppIndex"] == null)
                {
                    HttpContext.Current.Session.Add("AppIndex", value);
                }
                else
                {
                    HttpContext.Current.Session["AppIndex"] = value;
                }

            }
        }

		public string Yun_ResourceRoot
		{
			get
			{
				return AppSettings.Instance.Yun_ResourceRoot;
			}
		}

		public PageBase()
		{
			this.dblogger = new _DataLog();
			this.fileLogger = LogManager.GetCurrentClassLogger();
			PageBase pageBase = this;
			base.Load += new EventHandler(pageBase.PageBase_Load);
			PageBase pageBase1 = this;
			base.Error += new EventHandler(pageBase1.PageBase_Error);
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

		 [AjaxMethod(HttpSessionStateRequirement.Read)]   // The parameters of the attribute are missing, because its assembly was not found
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
			return Utility.FormatException(ex, catchInfo);
		}

		public string GetClientIP()
		{
			return Utility.GetClientIP();
		}

		protected virtual string GetCustomScript(string pageKey)
		{
            StringBuilder stringBuilder = new StringBuilder();
            string[] strArrays = SysConfig.GetConfig(pageKey, false).ItemValue.Split("\r\n".ToCharArray());
            string[] strArrays1 = strArrays;
            for (int i = 0; i < (int)strArrays1.Length; i++)
            {
                string str = strArrays1[i];
                if (str.ToLower().IndexOf(".css") > 0)
                {
                    stringBuilder.AppendFormat("<link type='text/css' rel='stylesheet' href='{0}' />\r", str);
                }
                else if (str.ToLower().IndexOf(".js") > 0)
                {
                    stringBuilder.AppendFormat("<script type='text/javascript' src='{0}'></script>\r", str);
                }
            }
            return stringBuilder.ToString();
		}

		protected string GetOSNameByUserAgent()
		{
			return Utility.GetOSNameByUserAgent();
		}

		public string GetParaValue(string paraname)
		{
            string item;
            string CryptParaNameStr = ",InitCond,tblname,condition,sindex,cpro,replacevalue,Defaultvalue";
            if (CryptParaNameStr.ToLower().IndexOf(paraname.ToLower()) > 0)
            {
                //必须加密的参数
                if (string.IsNullOrEmpty(base.Request["para"]))
                {
                    item = "";
                }
                else
                {
                    string str = base.Request["para"];
                    try
                    {
                        str = Security.Decrypt(str, this.UserName);
                        item = Security.GetUrlPara(str, paraname);
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        Logger logger = this.fileLogger;
                        object[] userName = new object[] { str, this.UserName, paraname, base.Request.RawUrl, exception.Message };
                        logger.Error("密文：{0}，钥匙：{1}，参数名：{2}，地址：{3}，错误：{4}", userName);
                        userName = new object[] { str, this.UserName, paraname, base.Request.RawUrl };
                        throw new Exception(string.Format("密文：{0}，钥匙：{1}，参数名：{2}，地址：{3}", userName), exception);
                    }
                }
            }
            else
            {
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
                    try
                    {
                        str = Security.Decrypt(str, this.UserName);
                        item = Security.GetUrlPara(str, paraname);
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        Logger logger = this.fileLogger;
                        object[] userName = new object[] { str, this.UserName, paraname, base.Request.RawUrl, exception.Message };
                        logger.Error("密文：{0}，钥匙：{1}，参数名：{2}，地址：{3}，错误：{4}", userName);
                        userName = new object[] { str, this.UserName, paraname, base.Request.RawUrl };
                        throw new Exception(string.Format("密文：{0}，钥匙：{1}，参数名：{2}，地址：{3}", userName), exception);
                    }
                }
            }
            return item;
		}

		protected string GetPathWidthETag(string filePath)
		{
            string str = filePath.GetHashCode().ToString();
            char[] chrArray = new char[] { '-' };
            string str1 = string.Concat("m", str.TrimStart(chrArray));
            if (HttpContext.Current.Cache[str1] == null)
            {
                string str2 = HttpContext.Current.Server.MapPath(filePath);
                FileInfo fileInfo = new FileInfo(str2);
                CacheDependency cacheDependency = new CacheDependency(str2);
                HttpRuntime.Cache.Insert(str1, fileInfo.LastWriteTime.Ticks, cacheDependency);
            }
            string str3 = string.Concat(filePath, "?v=", HttpContext.Current.Cache[str1].ToString());
            return str3;
		}

        [AjaxMethod(HttpSessionStateRequirement.Read)]   // The parameters of the attribute are missing, because its assembly was not found
		public DataTable GetQueryData(string queryCode, string condstr, string replacepara)
		{
            DataTable dataTable = null;
            string str = string.Concat("select TableType,ListSQL,ConnectionId,OrderField from T_E_Sys_TableInfo where tablename='", queryCode, "'");
            DataTable dataTable1 = SysDatabase.ExecuteTable(str);
            if (dataTable1.Rows.Count == 0)
            {
                throw new Exception(string.Concat("在执行GetQueryData时出错，找不到名称为", queryCode, "的定义"));
            }
            string str1 = dataTable1.Rows[0]["ListSQL"].ToString();
            string str2 = dataTable1.Rows[0]["ConnectionId"].ToString();
            str1 = str1.Replace("|^condition^|", string.Concat("(", condstr, ")")).Replace("|^sortdir^|", "").Replace("\r\n", " ").Replace("\t", "");
            if (!string.IsNullOrEmpty(replacepara))
            {
                str1 = Utility.ReplaceParaValues(str1, replacepara);
            }
            str1 = this.ReplaceContext(str1);
            if (str2.Trim() == "")
            {
                dataTable = SysDatabase.ExecuteTable(str1);
            }
            else
            {
                CustomDb customDb = new CustomDb();
                customDb.CreateDatabaseByConnectionId(str2.Trim());
                dataTable = customDb.ExecuteTable(str1, 120);
            }
            return dataTable;
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
				LogUser = (this.Session["username"] != null ? this.EmployeeID : ""),
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
			if ((HttpContext.Current.User.Identity.IsAuthenticated ? true : this.Page.Session["username"] != null))
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
			this.Session["lastOprationTime"] = DateTime.Now.Ticks.ToString();
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

		public string ReplaceWithDataRow(string sourceString, DataRow data)
		{
			foreach (Match match in (new Regex("{(\\w*\\.)?(\\w+)}", RegexOptions.IgnoreCase)).Matches(sourceString))
			{
				string str = "";
				str = (match.Groups.Count <= 2 ? match.Groups[1].Value : match.Groups[2].Value);
				if (data.Table.Columns.Contains(str))
				{
					sourceString = sourceString.Replace(match.Value, data[str].ToString());
				}
			}
			return sourceString;
		}

		protected string ResolveUrlWidthETag(string filePath)
		{
            string str = filePath.GetHashCode().ToString();
            char[] chrArray = new char[] { '-' };
            string str1 = string.Concat("m", str.TrimStart(chrArray));
            if (HttpContext.Current.Cache[str1] == null)
            {
                string str2 = HttpContext.Current.Server.MapPath(filePath);
                FileInfo fileInfo = new FileInfo(str2);
                CacheDependency cacheDependency = new CacheDependency(str2);
                HttpRuntime.Cache.Insert(str1, fileInfo.LastWriteTime.Ticks, cacheDependency);
            }
            string str3 = string.Concat(base.ResolveUrl(filePath), "?v=", HttpContext.Current.Cache[str1].ToString());
            return str3;
		}

		protected virtual void ResponseError(Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("\r\n                <div class='ErrorMsg'>{0}</div>\r\n                ", exception.Message);
			base.ClientScript.RegisterStartupScript(base.GetType(), "", stringBuilder.ToString());
		}

		protected void WriteDataLog(string logType, string bizName, string bizId, DataRow newData)
		{
            DataTable dataTable;
            DataRow item;
            TableInfo model = (new _C_TableInfo(bizName)).GetModel();
            int dataLog = 2;
            string dataLogTmpl = "";
            if (model != null)
            {
                dataLog = model.DataLog;
                dataLogTmpl = model.DataLogTmpl;
                if (model.DataLog == 0)
                {
                    return;
                }
            }
            DataLog dataLog1 = new DataLog()
            {
                AppID = bizId,
                AppName = bizName,
                ComputeIP = this.GetClientIP(),
                ServerIP = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"],
                LogType = logType,
                LogUser = (HttpContext.Current.Session["userName"] != null ? this.EmployeeID : ""),
                UserName = (HttpContext.Current.Session["EmployeeName"] != null ? this.EmployeeName : ""),
                ModuleCode = "",
                ModuleName = "",
                Browser = Utility.GetBrowserByUserAgent(),
                Platform = this.GetOSNameByUserAgent(),
                UserAgent = HttpContext.Current.Request.UserAgent
            };
            if (logType == "新建")
            {
                if (dataLogTmpl != "")
                {
                    dataLog1.Message = this.ReplaceWithDataRow(model.DataLogTmpl, newData);
                    dataLog1.Message = this.ReplaceContext(dataLog1.Message);
                }
                if (dataLog == 2)
                {
                    dataLog1.Data = _DataLog.GenNewBizLog(bizName, bizId, newData);
                }
            }
            else if (logType == "修改")
            {
                dataTable = SysDatabase.ExecuteTable(string.Format("select * from {0} where _AutoId='{1}'", bizName, bizId));
                item = dataTable.Rows[0];
                if (dataLogTmpl != "")
                {
                    dataLog1.Message = this.ReplaceWithDataRow(model.DataLogTmpl, item);
                    dataLog1.Message = this.ReplaceContext(dataLog1.Message);
                }
                if (dataLog == 2)
                {
                    dataLog1.Data = _DataLog.GenUpdateBizLog(bizName, bizId, item, newData);
                }
            }
            else if (logType != "删除")
            {
                dataLog1.Data = "";
            }
            else
            {
                dataTable = SysDatabase.ExecuteTable(string.Format("select * from {0} where _AutoId='{1}'", bizName, bizId));
                item = dataTable.Rows[0];
                if (dataLogTmpl != "")
                {
                    dataLog1.Message = this.ReplaceWithDataRow(model.DataLogTmpl, item);
                    dataLog1.Message = this.ReplaceContext(dataLog1.Message);
                }
                if (dataLog == 2)
                {
                    dataLog1.Data = _DataLog.GenDeleteBizLog(bizName, bizId, item);
                }
            }
            this.dblogger.WriteDataLog(dataLog1);
		}

		protected void WriteExceptionLog(string logType, string logMsg)
		{
            DataLog dataLog = new DataLog()
            {
                AppID = "",
                AppName = "",
                ComputeIP = this.GetClientIP(),
                ServerIP = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"],
                LogType = logType,
                LogUser = (this.Session["username"] != null ? this.EmployeeID : ""),
                UserName = (this.Session["EmployeeName"] != null ? this.EmployeeName : ""),
                ModuleCode = "",
                ModuleName = "",
                Message = logMsg,
                Browser = Utility.GetBrowserByUserAgent(),
                Platform = this.GetOSNameByUserAgent(),
                UserAgent = base.Request.UserAgent
            };
            this.dblogger.WriteExceptionLog(dataLog);
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
                dataLog.LogUser = (this.Session["username"] != null ? this.EmployeeID : "");
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

		private void WriteSecurityLog(string logType, string logMsg)
		{
			this.WriteSecurityLog(logType, logMsg, "");
		}
	}
}