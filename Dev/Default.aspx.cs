using EIS.AppBase;
using EIS.Permission;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EIS.Common;
using EIS.AppCommon;
namespace EIS.Studio
{
    public partial class Default : AdminPageBase
    {
      

        public string TipMsg = "";

        public Default()
        {
            this.AutoRedirect = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string text = this.txtUser.Text;
            string str = this.txtPwd.Text;
            string str1 = "";
            switch (UserService.LoginChk(text, str))
            {
                case LoginInfoType.Allowed:
                    {
                        str1 = "";
                        break;
                    }
                case LoginInfoType.NotExist:
                    {
                        str1 = "用户不存在";
                        break;
                    }
                case LoginInfoType.WrongPwd:
                    {
                        str1 = "密码不正确";
                        break;
                    }
                case LoginInfoType.IsLocked:
                    {
                        str1 = "帐户被锁定";
                        break;
                    }
            }
            if (txtCode.Text == "") str1 = "验证码不正确";
            if (txtCode.Text != Session["Validate_Identify"].ToString()) str1 = "验证码不正确";

            if (str1 != "")
            {
                base.WriteSecurityLog("登录失败", str1, text);
                base.ClientScript.RegisterStartupScript(base.GetType(), "", string.Concat("alert('", str1, "');"), true);
            }
            else
            {
                this.UpdateCookie(text, str);
                this.method_0(text);
                base.WriteSecurityLog("登录成功", "登录成功", text);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("WinLogin.aspx");
        }

        private void method_0(string string_0)
        {
            HttpContext.Current.Session["UserName"] = string_0;
            LoginUser model = (new _User()).GetModel(string_0);
            HttpContext.Current.Session["LoginType"] = model.LoginType;
            Department department = DepartmentService.GetModel(model.CompanyId);
            if (department == null)
            {
                HttpContext.Current.Session["CompanyId"] = "";
                HttpContext.Current.Session["CompanyCode"] = "";
                HttpContext.Current.Session["CompanyWbs"] = "";
                HttpContext.Current.Session["CompanyAbbr"] = "";
                HttpContext.Current.Session["CompanyName"] = "";
            }
            else
            {  //strdeptid
                HttpContext.Current.Session["CompanyId"] = department._AutoID;
                HttpContext.Current.Session["CompanyCode"] = department.DeptCode;
                HttpContext.Current.Session["CompanyWbs"] = department.DeptWBS;
                HttpContext.Current.Session["CompanyAbbr"] = department.DeptAbbr;
                HttpContext.Current.Session["CompanyName"] = department.DeptName;
            }
            HttpContext.Current.Session["DeptId"] = "";
            HttpContext.Current.Session["DeptCode"] = "";
            HttpContext.Current.Session["DeptName"] = "";
            HttpContext.Current.Session["DeptWbs"] = "";
            HttpContext.Current.Session["TopDeptId"] = "";
            HttpContext.Current.Session["TopDeptCode"] = "";
            HttpContext.Current.Session["TopDeptName"] = "";
            HttpContext.Current.Session["TopDeptWbs"] = "";
            HttpContext.Current.Session["EmployeeId"] = model._AutoID;
            HttpContext.Current.Session["EmployeeCode"] = "";
            HttpContext.Current.Session["EmployeeName"] = model.EmployeeName;
            HttpContext.Current.Session["PositionId"] = "";
            HttpContext.Current.Session["PositionName"] = "";
            HttpContext.Current.Session["PhotoId"] = model.PhotoId;


            if (!string.IsNullOrEmpty(base.Request["ReturnUrl"]))
            {
                FormsAuthentication.SetAuthCookie(string_0, false);
                if (base.Request["ReturnUrl"] == "/")
                {
                    base.Response.Redirect(FormsAuthentication.DefaultUrl);
                }
                if (base.Request["ReturnUrl"].ToLowerInvariant() == string.Concat(base.Request.ApplicationPath.ToLowerInvariant(), "/"))
                {
                    base.Response.Redirect(FormsAuthentication.DefaultUrl);
                }
                else if (base.Request["ReturnUrl"].ToLowerInvariant() != base.Request.ApplicationPath.ToLowerInvariant())
                {
                    base.Response.Redirect(base.Server.UrlDecode(base.Request["ReturnUrl"]));
                }
                else
                {
                    base.Response.Redirect(FormsAuthentication.DefaultUrl);
                }
            }
            else if (base.Request.UrlReferrer == null)
            {
                FormsAuthentication.RedirectFromLoginPage(string_0, false);
            }
            else if (base.Request.UrlReferrer.Query.Length <= 0)
            {
                FormsAuthentication.RedirectFromLoginPage(string_0, false);
            }
            else
            {
                string para = EIS.AppBase.Utility.GetPara(base.Request.UrlReferrer.Query, "ReturnUrl");
                para = base.Server.UrlDecode(para);
                if (para.Length <= 0)
                {
                    FormsAuthentication.RedirectFromLoginPage(string_0, false);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(string_0, false);
                    if (para == "/")
                    {
                        base.Response.Redirect(FormsAuthentication.DefaultUrl);
                    }
                    else if (para.ToLowerInvariant() == string.Concat(base.Request.ApplicationPath.ToLowerInvariant(), "/"))
                    {
                        base.Response.Redirect(FormsAuthentication.DefaultUrl);
                    }
                    else if (para.ToLowerInvariant() != base.Request.ApplicationPath.ToLowerInvariant())
                    {
                        base.Response.Redirect(para);
                    }
                    else
                    {
                        base.Response.Redirect(FormsAuthentication.DefaultUrl);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (!string.IsNullOrEmpty(base.Request["logout"]))
                {
                    AspxHelper.SignOut();
                    this.Session.Abandon();
                }
                else
                {
                    HttpCookie httpCookie = base.Request.Cookies.Get("_UserInfo");
                    if (httpCookie != null)
                    {
                        string item = httpCookie.Values["uName"];
                        string str = httpCookie.Values["uPass"];
                        string item1 = httpCookie.Values["uRememberName"];
                        if (!string.IsNullOrEmpty(item))
                        {
                            this.txtUser.Text = item;
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
                httpCookie = new HttpCookie("_UserInfo");
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
                HttpCookie item = base.Response.Cookies["_UserInfo"];
                now = DateTime.Now;
                item.Expires = now.AddDays(-1);
            }
            else
            {
                httpCookie = new HttpCookie("_UserInfo");
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