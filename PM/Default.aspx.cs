using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.Permission;
using EIS.Permission.Access;
using EIS.Permission.Service;
using WebBase.JZY.Tools;
using NLog;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.Web
{
    public partial class Default : PageBase
    {

        public string bgWidth = "949px";
        public string bgHeight = "430px";

        public string box_mleft = "-475px";
        public string box_mtop = "-245px";

        public string login_mright = "620px";
        public string login_mtop = "200px";
        public string login_Color = "black";

        public string TipMsg = "";

        public string verifyCss = "display:none;";

        public string LoginTitle = "";

        public Default()
        {
            this.AutoRedirect = false;
        }

        protected virtual void Button1_Click(object sender, EventArgs e)
        {
            string text = this.txtUser.Text;
            string str = this.txtPwd.Text;
            if (!(this.verifyCss != "" ? true : this.Session["VerifyCode"] == null) && this.txtCode.Text.Trim().ToLower() != this.Session["VerifyCode"].ToString().ToLower())
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('验证码不正确');", true);
            }
            else if (this.LoginCheck(text, str) == "")
            {
                this.UpdateCookie(text, str);
                this.GoPage(text);
            }
        }

        protected virtual void Button2_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("WinLogin.aspx");
        }

        protected void GoPage(string account)
        {
            if ((EmployeeService.GetLoginCount(this.Session["EmployeeId"].ToString()) != 1 ? false : SysConfig.GetConfig("ModifyPassFirstLogin", false).ItemValue == "是"))
            {
                FormsAuthentication.SetAuthCookie(account, false);
                base.Response.Redirect("ChangePass.aspx?firstLogin=1");
            }

            string DefaultUrl = FormsAuthentication.DefaultUrl;
            if (DefaultUrl.ToLower().ToString().Contains("webid") && DefaultUrl.ToLower().ToString().Contains("defaultmain.aspx"))
            { 
                string[] arr_p = DefaultUrl.Split('?');
                if (arr_p.Length > 1) {
                    DefaultUrl = arr_p[0] + "?para=" + base.CryptPara(arr_p[1]);
                }
            }

            if (!string.IsNullOrEmpty(base.Request["ReturnUrl"]))
            {
                FormsAuthentication.SetAuthCookie(account, false);
                if (base.Request["ReturnUrl"] == "/")
                {
                    base.Response.Redirect(DefaultUrl);
                }
                else if (base.Request["ReturnUrl"].ToLowerInvariant() == string.Concat(base.Request.ApplicationPath.ToLowerInvariant(), "/"))
                {
                    base.Response.Redirect(DefaultUrl);
                }
                else if (base.Request["ReturnUrl"].ToLowerInvariant() != base.Request.ApplicationPath.ToLowerInvariant())
                {
                    base.Response.Redirect(base.Server.UrlDecode(base.Request["ReturnUrl"]));
                }
                else
                {
                    base.Response.Redirect(DefaultUrl);
                }
            }
            else if (base.Request.UrlReferrer == null)
            {
                FormsAuthentication.RedirectFromLoginPage(account, false);
            }
            else if (base.Request.UrlReferrer.Query.Length <= 0)
            {
                FormsAuthentication.RedirectFromLoginPage(account, false);
            }
            else
            {
                string para = EIS.AppBase.Utility.GetPara(base.Request.UrlReferrer.Query, "ReturnUrl");
                para = base.Server.UrlDecode(para);
                if (para.Length <= 0)
                {
                    FormsAuthentication.RedirectFromLoginPage(account, false);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(account, false);
                    if (para == "/")
                    {
                        base.Response.Redirect(DefaultUrl);
                    }
                    else if (para.ToLowerInvariant() == string.Concat(base.Request.ApplicationPath.ToLowerInvariant(), "/"))
                    {
                        base.Response.Redirect(DefaultUrl);
                    }
                    else if (para.ToLowerInvariant() != base.Request.ApplicationPath.ToLowerInvariant())
                    {
                        base.Response.Redirect(para);
                    }
                    else
                    {
                        base.Response.Redirect(DefaultUrl);
                    }
                }
            }
        }

        protected string LoginCheck(string string_0, string string_1)
        {
            string str = "";
            switch (EIS.Permission.Utility.LoginChk(string_0, string_1))
            {
                case LoginInfoType.Allowed:
                    {
                        str = "";
                        break;
                    }
                case LoginInfoType.NotExist:
                    {
                        str = "用户不存在";
                        break;
                    }
                case LoginInfoType.WrongPwd:
                    {
                        str = "密码不正确";
                        break;
                    }
                case LoginInfoType.IsLocked:
                    {
                        str = "帐户被锁定";
                        break;
                    }
            }
            if (str != "")
            {
                base.WriteSecurityLog("登录失败", str, string_0);
                base.ClientScript.RegisterStartupScript(base.GetType(), "", string.Concat("alert('", str, "');"), true);
            }
            else
            {
                WebTools.OnAuthenticated(string_0);
                this.Session["loginType"] = "1";
                base.WriteSecurityLog("登录成功", "登录成功", string_0);
            }
            return str;
        }

        private void method_0()
        {
            base.Response.Clear();
            base.Response.Status = "401 Unauthorized";
            base.Response.AppendHeader("WWW-Authenticate", "NTLM");
            base.Response.End();
        }

        protected void OnNTLogin()
        {
            string item = base.Request.ServerVariables["LOGON_USER"];
            if (!string.IsNullOrEmpty(item))
            {
                item = item.Split("\\\\".ToCharArray())[1];
                if (EmployeeService.IsADUserExist(item))
                {
                    WebTools.OnAuthenticated(item);
                }
                else
                {
                    HttpCookie httpCookie = base.Request.Cookies["CUR_LOGON_USER"];
                    if ((httpCookie == null ? true : string.Compare(httpCookie.Value, item, true) != 0))
                    {
                        httpCookie = base.Response.Cookies["CUR_LOGON_USER"];
                        if (httpCookie == null)
                        {
                            httpCookie = new HttpCookie("CUR_LOGON_USER");
                            base.Response.Cookies.Add(httpCookie);
                        }
                        httpCookie.Value = item;
                    }
                    else
                    {
                        httpCookie = new HttpCookie("CUR_LOGON_USER", "")
                        {
                            HttpOnly = true,
                            Expires = new DateTime(1999, 10, 12)
                        };
                        base.Response.Cookies.Remove("CUR_LOGON_USER");
                        base.Response.Cookies.Add(httpCookie);
                        this.method_0();
                    }
                }
            }
            else
            {
                this.method_0();
            }
        }

        private void setBackgroundPosition()
        {
            try
            {
                DataSet ds = EIS.Web.CorpLogo.Img.Handlers.CorpLogoServer.getCorpPhotoBg();
                if (ds.Tables.Count > 0) {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Columns.Contains("bgWidth"))
                        {
                            this.bgWidth = ds.Tables[0].Rows[0]["bgWidth"].ToString() + "px";
                            this.bgHeight = ds.Tables[0].Rows[0]["bgHeight"].ToString() + "px";
                            this.box_mleft = ds.Tables[0].Rows[0]["bgLeft"].ToString() + "px";
                            this.box_mtop = ds.Tables[0].Rows[0]["bgTop"].ToString() + "px";
                            this.login_mright = ds.Tables[0].Rows[0]["loginRight"].ToString() + "px";
                            this.login_mtop = ds.Tables[0].Rows[0]["loginTop"].ToString() + "px";
                            this.login_Color = ds.Tables[0].Rows[0]["loginColor"].ToString();
                        }
                    }
                }
            }
            catch { }
            finally { }
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {

            setBackgroundPosition();

            string item;
            DateTime now;
            HttpCookie httpCookie;
            string str;
            string item1;
            if (SysConfig.GetConfig("VerifyCode_Show").ItemValue != "否")
            {
                this.verifyCss = "";
            }
            this.LoginTitle = AppSettings.GetLoginTitle(AppSettings.Instance.WebId);
            if (!base.IsPostBack)
            {
                if (!string.IsNullOrEmpty(base.Request["logout"]))
                {
                    AspxHelper.SignOut();
                    this.Session.Abandon();
                    httpCookie = base.Request.Cookies.Get("UserInfo");
                    if (httpCookie != null)
                    {
                        item = httpCookie.Values["uName"];
                        str = httpCookie.Values["uPass"];
                        item1 = httpCookie.Values["uRememberName"];
                        if (item1 != "1")
                        {
                            HttpCookie httpCookie1 = base.Response.Cookies["UserInfo"];
                            now = DateTime.Now;
                            httpCookie1.Expires = now.AddDays(-1);
                        }
                        else
                        {
                            this.txtUser.Text = item;
                            base.Response.Cookies["UserInfo"]["uName"] = item;
                            base.Response.Cookies["UserInfo"]["uPass"] = "";
                            base.Response.Cookies["UserInfo"]["uRememberName"] = "1";
                            HttpCookie item2 = base.Response.Cookies["UserInfo"];
                            now = DateTime.Now;
                            item2.Expires = now.AddDays(30);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(base.Request["loginkey"]))
                    {
                        string str1 = base.Request["loginkey"];
                        string str2 = Security.Decrypt(base.Server.UrlDecode(str1));
                        bool flag = false;
                        item = Security.GetUrlPara(str2, "u");
                        string urlPara = Security.GetUrlPara(str2, "t");
                        if (!string.IsNullOrEmpty(urlPara))
                        {
                            long num = (long)0;
                            if (long.TryParse(urlPara, out num))
                            {
                                if (num != (long)0)
                                {
                                    DateTime dateTime = new DateTime(Convert.ToInt64(urlPara));
                                    now = DateTime.Now;
                                    TimeSpan timeSpan = new TimeSpan(now.Ticks - dateTime.Ticks);
                                    if (timeSpan.TotalSeconds < 300)
                                    {
                                        if (this.Session["userName"] != null && this.Session["userName"].ToString() == item)
                                        {
                                            flag = true;
                                            this.GoPage(item);
                                        }
                                        if (!flag && (new _Employee()).IsLoginExist(item))
                                        {
                                            WebTools.OnAuthenticated(item);
                                            this.Session["loginType"] = "1";
                                            this.GoPage(item);
                                        }
                                    }
                                }
                                else
                                {
                                    if (this.Session["userName"] != null && this.Session["userName"].ToString() == item)
                                    {
                                        flag = true;
                                        this.GoPage(item);
                                    }
                                    if (!flag)
                                    {
                                        if (!(new _Employee()).IsLoginExist(item))
                                        {
                                            base.WriteSecurityLog("登录失败", "LoginKey登录失败", item);
                                            this.fileLogger.Error<string, string>("通过LoginKey登录失败，u={0},loginkey={1}", item, str1);
                                        }
                                        else
                                        {
                                            WebTools.OnAuthenticated(item);
                                            this.Session["loginType"] = "1";
                                            base.WriteSecurityLog("登录成功", "LoginKey登录成功", item);
                                            this.GoPage(item);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    httpCookie = base.Request.Cookies.Get("UserInfo");
                    if (httpCookie != null)
                    {
                        item = httpCookie.Values["uName"];
                        str = httpCookie.Values["uPass"];
                        item1 = httpCookie.Values["uRememberName"];
                        if (!(string.IsNullOrEmpty(item) ? true : string.IsNullOrEmpty(str)))
                        {
                            try
                            {
                                if (this.LoginCheck(item, Security.Decrypt(base.Server.UrlDecode(str), item)) == "")
                                {
                                    this.GoPage(item);
                                }
                            }
                            catch (Exception exception)
                            {
                                this.txtUser.Text = item;
                            }
                        }
                        else if (item1 == "1")
                        {
                            this.txtUser.Text = item;
                        }
                    }
                }
            }
        }

        protected void UpdateCookie(string string_0, string string_1)
        {
            HttpCookie httpCookie;
            DateTime now;
            if (this.CheckBox2.Checked)
            {
                httpCookie = new HttpCookie("UserInfo");
                httpCookie.Values.Add("uName", string_0);
                string str = base.Server.UrlEncode(Security.EncryptStr(string_1, string_0));
                httpCookie.Values.Add("uPass", str);
                httpCookie.Values.Add("uRememberName", (this.CheckBox1.Checked ? "1" : ""));
                now = DateTime.Now;
                httpCookie.Expires = now.AddDays(30);
                httpCookie.Path = base.Request.ApplicationPath;
                base.Response.Cookies.Add(httpCookie);
            }
            else if (!this.CheckBox1.Checked)
            {
                HttpCookie item = base.Response.Cookies["UserInfo"];
                now = DateTime.Now;
                item.Expires = now.AddDays(-1);
            }
            else
            {
                httpCookie = new HttpCookie("UserInfo");
                httpCookie.Values.Add("uName", string_0);
                httpCookie.Values.Add("uPass", "");
                httpCookie.Values.Add("uRememberName", "1");
                now = DateTime.Now;
                httpCookie.Expires = now.AddDays(30);
                httpCookie.Path = base.Request.ApplicationPath;
                base.Response.Cookies.Add(httpCookie);
            }
        }



     
    }
}