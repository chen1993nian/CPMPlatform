using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EIS.AppBase;
using EIS.AppBase.Config;

namespace EIS.Web
{
    public partial class ShowSession : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.GetParaValue("reload") == "1")
            {
                AppSettings.Reload();
            }
            base.Response.Write("会话信息");
            base.Response.Write("<br/>==================================================================<br/>");
            foreach (string key in this.Session.Keys)
            {
                if (this.Session[key] == null)
                {
                    base.Response.Write(string.Concat(key, "：null<br/>"));
                }
                else
                {
                    base.Response.Write(string.Concat(key, "：", this.Session[key].ToString(), "<br/>"));
                }
            }
            base.Response.Write("<br/>系统变量");
            base.Response.Write("<br/>==================================================================<br/>");
            base.Response.Write(string.Format("AdminDefaultPass：{0}<br/>", AppSettings.Instance.AdminDefaultPass));
            base.Response.Write(string.Format("EmployeeDefaultPass：{0}<br/>", AppSettings.Instance.EmployeeDefaultPass));
            base.Response.Write(string.Format("AppFileSavePath：{0}<br/>", AppSettings.Instance.AppFileSavePath));
            base.Response.Write(string.Format("VerifyCode_Show：{0}<br/>", SysConfig.GetConfig("VerifyCode_Show").ItemValue));
            base.Response.Write(string.Format("OnlineRefreshInterval：{0}<br/>", SysConfig.GetConfig("OnlineRefreshInterval").ItemValue));
            base.Response.Write(string.Format("AM_StartTime：{0}<br/>", SysConfig.GetConfig("AM_StartTime").ItemValue));
            base.Response.Write(string.Format("AM_EndTime：{0}<br/>", SysConfig.GetConfig("AM_EndTime").ItemValue));
            base.Response.Write(string.Format("PM_StartTime：{0}<br/>", SysConfig.GetConfig("PM_StartTime").ItemValue));
            base.Response.Write(string.Format("PM_EndTime：{0}<br/>", SysConfig.GetConfig("PM_EndTime").ItemValue));
            base.Response.Write(string.Format("Basic_CompanyCode：{0}<br/>", SysConfig.GetConfig("Basic_CompanyCode").ItemValue));
            base.Response.Write(string.Format("Basic_DeskTop：{0}<br/>", SysConfig.GetConfig("Basic_DeskTop").ItemValue));
            base.Response.Write(string.Format("Basic_ResourceMethod：{0}<br/>", SysConfig.GetConfig("Basic_ResourceMethod").ItemValue));
            base.Response.Write(string.Format("WF_SubmitText：{0}<br/>", SysConfig.GetConfig("WF_SubmitText").ItemValue));
            base.Response.Write(string.Format("WF_SignRejectText：{0}<br/>", SysConfig.GetConfig("WF_SignRejectText").ItemValue));
            base.Response.Write(string.Format("WF_EnforceAgree：{0}<br/>", SysConfig.GetConfig("WF_EnforceAgree").ItemValue));
            base.Response.Write(string.Format("WF_OverTimeCheck：{0}<br/>", SysConfig.GetConfig("WF_OverTimeCheck").ItemValue));
            base.Response.Write(string.Format("WF_OverTimeInterval：{0}<br/>", SysConfig.GetConfig("WF_OverTimeInterval").ItemValue));
            string str = this.Session["WebId"].ToString();
            base.Response.Write(string.Format("WebName：{0}<br/>", AppSettings.GetWebName(str)));
            base.Response.Write(string.Format("LoginTitle：{0}<br/>", AppSettings.GetLoginTitle(str)));
            base.Response.Write(string.Format("MainTitle：{0}<br/>", AppSettings.GetMainTitle(str)));
            base.Response.Write(string.Format("RootURI：{0}<br/>", AppSettings.GetRootURI(str)));
            base.Response.Write(string.Format("SysMail_Server：{0}<br/>", AppSettings.Instance.MailConfig.ServerIP));
            base.Response.Write(string.Format("SysMail_Port：{0}<br/>", AppSettings.Instance.MailConfig.ServerPort));
            base.Response.Write(string.Format("SysMail_SSL：{0}<br/>", AppSettings.Instance.MailConfig.EnableSSL));
            base.Response.Write(string.Format("SysMail_Async：{0}<br/>", AppSettings.Instance.MailConfig.Async));
            base.Response.Write(string.Format("SysMail_Account：{0}<br/>", AppSettings.Instance.MailConfig.Account));
            base.Response.Write(string.Format("SysMail_NiCheng：{0}<br/>", AppSettings.Instance.MailConfig.NiCheng));
            base.Response.Write(string.Format("SysMail_Enable：{0}<br/>", AppSettings.Instance.MailConfig.Enable));
            base.Response.Write(string.Format("SysMail_Subffix：{0}<br/>", AppSettings.Instance.MailConfig.BodySubffix));
        }
    }
}