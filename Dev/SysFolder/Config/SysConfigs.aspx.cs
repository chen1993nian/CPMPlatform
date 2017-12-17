using EIS.AppBase;
using EIS.AppBase.Config;
using NLog;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.Config
{
    public partial class SysConfigs : AdminPageBase
    {
       
        public StringBuilder sbMsg = new StringBuilder();

       

        protected void btnBasic_Click(object sender, EventArgs e)
        {
            //企业注册编号
            ConfigItem configItem = new ConfigItem()
            {
                ItemCode = "Basic_CompanyCode",
                ItemValue = this.basic_CompanyCode.Text
            };
            SysConfig.SetConfig(configItem);
            //员工默认桌面
            ConfigItem configItem1 = new ConfigItem()
            {
                ItemCode = "Basic_DeskTop",
                ItemValue = this.basic_DeskTop.SelectedValue
            };
            SysConfig.SetConfig(configItem1);

            //静态资源获取方式
            ConfigItem configItem2 = new ConfigItem()
            {
                ItemCode = "Basic_ResourceMethod",
                ItemValue = this.basic_ResMethod.SelectedValue
            };
            SysConfig.SetConfig(configItem2);
            //每天工作小时数
            ConfigItem configItem3 = new ConfigItem()
            {
                ItemCode = "HoursOneDay",
                ItemValue = this.basic_HoursOneDay.Text
            };
            SysConfig.SetConfig(configItem3);
            //工作时间
            ConfigItem configItem4 = new ConfigItem()
            {
                ItemCode = "AM_StartTime",
                ItemValue = this.basic_StartTime1.Text
            };
            SysConfig.SetConfig(configItem4);

            //上午
            ConfigItem configItem5 = new ConfigItem()
            {
                ItemCode = "AM_EndTime",
                ItemValue = this.basic_EndTime1.Text
            };
            SysConfig.SetConfig(configItem5);
            ConfigItem configItem6 = new ConfigItem()
            {
                ItemCode = "PM_StartTime",
                ItemValue = this.basic_StartTime2.Text
            };
            SysConfig.SetConfig(configItem6);
            ConfigItem configItem7 = new ConfigItem()
            {
                ItemCode = "PM_EndTime",
                ItemValue = this.basic_EndTime2.Text
            };
            SysConfig.SetConfig(configItem7);


            //刷新在线间隔（秒）：
            ConfigItem configItem8 = new ConfigItem()
            {
                ItemCode = "OnlineRefreshInterval",
                ItemValue = this.basic_OnlineRefreshInterval.Text
            };
            SysConfig.SetConfig(configItem8);

            //登录验证码
            ConfigItem configItem9 = new ConfigItem()
            {
                ItemCode = "VerifyCode_Show",
                ItemValue = this.basic_VerifyCode_Show.SelectedValue
            };
            SysConfig.SetConfig(configItem9);

            //首次登录后修改密码
            ConfigItem configItem10 = new ConfigItem()
            {
                ItemCode = "ModifyPassFirstLogin",
                ItemValue = this.basic_ModifyPassFirstLogin.SelectedValue
            };
            SysConfig.SetConfig(configItem10);

            //查看链接自动登录
            ConfigItem configItem11 = new ConfigItem()
            {
                ItemCode = "ReloginLinkInMail",
                ItemValue = this.basic_ReloginLinkInMail.SelectedValue
            };
            SysConfig.SetConfig(configItem11);
            //员工初始化密码
            ConfigItem configItem12 = new ConfigItem()
            {
                ItemCode = "EmployeeDefaultPass",
                ItemValue = this.basic_EmployeeDefaultPass.Text
            };
            SysConfig.SetConfig(configItem12);

            //管理员初始化密码
            ConfigItem configItem13 = new ConfigItem()
            {
                ItemCode = "AdminDefaultPass",
                ItemValue = this.basic_AdminDefaultPass.Text
            };
            SysConfig.SetConfig(configItem13);
            //管理员手机
            ConfigItem configItem14 = new ConfigItem()
            {
                ItemCode = "AdminDTel",
                ItemValue = this.txtAdminTel.Text
            };
            SysConfig.SetConfig(configItem14);

            //;测试人员&nbsp;-&nbsp;手机：
            ConfigItem configItem15 = new ConfigItem()
            {
                ItemCode = "TestTel",
                ItemValue = this.txtTestTel.Text
            };
            SysConfig.SetConfig(configItem15);

            //;管理员&nbsp;-&nbsp;邮箱：
            ConfigItem configItem16 = new ConfigItem()
            {
                ItemCode = "AdminEmail",
                ItemValue = this.txtAdminEmail.Text
            };
            SysConfig.SetConfig(configItem16);

            //;管理员&nbsp;-&nbsp;邮箱：
            ConfigItem configItem17 = new ConfigItem()
            {
                ItemCode = "TestEmail",
                ItemValue = this.txtTestEmail.Text
            };
            SysConfig.SetConfig(configItem17);

            //系统维护提示语
            ConfigItem configItem18 = new ConfigItem()
            {
                ItemCode = "txtSound",
                ItemValue = this.TextBox5.Text
            };
            SysConfig.SetConfig(configItem18);

            //系统关闭
            ConfigItem configItem19 = new ConfigItem()
            {
                ItemCode = "Basic_SysSwich",
                ItemValue = this.RadioButtonList2.SelectedValue
            };
            SysConfig.SetConfig(configItem19);


            base.ClientScript.RegisterStartupScript(base.GetType(), "", "<script language=javascript>alert(\"保存设置成功！\");</script>");
        }

        protected void btnDs_Click(object sender, EventArgs e)
        {
            LDAPConfig ldapConfig = AppSettings.Instance.LdapConfig;
            ldapConfig.ServerIP = this.ds_ServerIP.Text;
            ldapConfig.ServerPort = this.ds_ServerPort.Text;
            ldapConfig.State = this.ds_State.SelectedValue;
            ldapConfig.RootPath = this.ds_Root.Text;
            ldapConfig.Account = this.ds_Account.Text;
            ldapConfig.PassWord = this.ds_Pass.Text;
            LDAPConfig.SetConfig(ldapConfig);
            base.ClientScript.RegisterStartupScript(base.GetType(), "", "<script language=javascript>alert(\"保存设置成功！\");</script>");
        }

        protected void btnMail_Click(object sender, EventArgs e)
        {
            MailConfig mailConfig = AppSettings.Instance.MailConfig;
            mailConfig.ServerIP = this.mail_Server.Text;
            mailConfig.ServerPort = this.mail_Port.Text;
            mailConfig.EnableSSL = this.mail_SSL.SelectedValue;
            mailConfig.Async = this.mail_Async.SelectedValue;
            mailConfig.Account = this.mail_Account.Text;
            mailConfig.NiCheng = this.mail_NiCheng.Text;
            mailConfig.Enable = this.mail_Enable.SelectedValue;
            mailConfig.BodySubffix = this.mail_Subffix.Text;
            if (this.mail_PassWord.Text != "")
            {
                mailConfig.PassWord = this.mail_PassWord.Text;
            }
            MailConfig.SetConfig(mailConfig);
        }

        protected void btnRef_Click(object sender, EventArgs e)
        {
            ConfigItem configItem = new ConfigItem()
            {
                ItemCode = "Ref_DefaultMain",
                ItemValue = this.ref_DefaultMain.Text
            };
            SysConfig.SetConfig(configItem);
            ConfigItem configItem1 = new ConfigItem()
            {
                ItemCode = "Ref_AppDefault",
                ItemValue = this.ref_AppDefault.Text
            };
            SysConfig.SetConfig(configItem1);
            ConfigItem configItem2 = new ConfigItem()
            {
                ItemCode = "Ref_AppQuery",
                ItemValue = this.ref_AppQuery.Text
            };
            SysConfig.SetConfig(configItem2);
            ConfigItem configItem3 = new ConfigItem()
            {
                ItemCode = "Ref_AppInput",
                ItemValue = this.ref_AppInput.Text
            };
            SysConfig.SetConfig(configItem3);
            ConfigItem configItem4 = new ConfigItem()
            {
                ItemCode = "Ref_AppDetail",
                ItemValue = this.ref_AppDetail.Text
            };
            SysConfig.SetConfig(configItem4);
            ConfigItem configItem5 = new ConfigItem()
            {
                ItemCode = "Ref_AppPrint",
                ItemValue = this.ref_AppPrint.Text
            };
            SysConfig.SetConfig(configItem5);
            base.ClientScript.RegisterStartupScript(base.GetType(), "", "<script language=javascript>alert(\"保存设置成功！\");</script>");
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            MailConfig mailConfig = AppSettings.Instance.MailConfig;
            mailConfig.ServerIP = this.mail_Server.Text;
            mailConfig.ServerPort = this.mail_Port.Text;
            mailConfig.EnableSSL = this.mail_SSL.SelectedValue;
            mailConfig.Async = this.mail_Async.SelectedValue;
            mailConfig.Account = this.mail_Account.Text;
            mailConfig.Enable = this.mail_Enable.SelectedValue;
            mailConfig.BodySubffix = this.mail_Subffix.Text;
            if (this.mail_PassWord.Text != "")
            {
                mailConfig.NiCheng = this.mail_NiCheng.Text;
            }
            mailConfig.PassWord = this.mail_PassWord.Text;
            MailConfig.SetConfig(mailConfig);
            MailMessage mailMessage = new MailMessage();
            DateTime now = DateTime.Now;
            mailMessage.Subject = string.Concat("系统测试邮件-", now.ToString("MM月dd日 HH:mm"));
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;
            mailMessage.Body = "这是一封系统测试邮件，无需回复。";
            try
            {
                mailMessage.To.Add(this.mail_TestAccount.Text);
                if ((mailConfig.ServerIP == "" ? true : mailConfig.Account == ""))
                {
                    throw new Exception("默认的外发信箱设置不正确！");
                }
                if (mailConfig.NiCheng == "")
                {
                    mailMessage.From = new MailAddress(mailConfig.Account);
                }
                else
                {
                    mailMessage.From = new MailAddress(mailConfig.Account, mailConfig.NiCheng);
                }
                SmtpClient smtpClient = new SmtpClient()
                {
                    Host = mailConfig.ServerIP
                };
                int num = 25;
                int.TryParse(mailConfig.ServerPort, out num);
                smtpClient.Port = num;
                smtpClient.EnableSsl = mailConfig.EnableSSL == "是";
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = true;
                string account = mailConfig.Account;
                string passWord = mailConfig.PassWord;
                if (account != "")
                {
                    smtpClient.Credentials = new NetworkCredential(account, passWord);
                }
                smtpClient.Send(mailMessage);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                this.fileLogger.Error<Exception>(exception);
                this.sbMsg.AppendFormat("<div class='tip' >{0}</div>", exception.Message);
            }
        }

        protected void btnWF_Click(object sender, EventArgs e)
        {
            ConfigItem configItem = new ConfigItem()
            {
                ItemCode = "WF_SubmitText",
                ItemValue = this.wf_WF_SubmitText.Text
            };
            SysConfig.SetConfig(configItem);
            ConfigItem configItem1 = new ConfigItem()
            {
                ItemCode = "WF_SignRejectText",
                ItemValue = this.wf_WF_SignRejectText.Text
            };
            SysConfig.SetConfig(configItem1);
            ConfigItem configItem2 = new ConfigItem()
            {
                ItemCode = "WF_SignPrintStyle",
                ItemValue = this.wf_WF_SignPrintStyle.Text
            };
            SysConfig.SetConfig(configItem2);
            ConfigItem configItem3 = new ConfigItem()
            {
                ItemCode = "WF_ArriveTitle",
                ItemValue = this.wf_ArriveTitle.Text
            };
            SysConfig.SetConfig(configItem3);
            ConfigItem configItem4 = new ConfigItem()
            {
                ItemCode = "WF_ArriveMsg",
                ItemValue = this.wf_ArriveMsg.Text
            };
            SysConfig.SetConfig(configItem4);
            ConfigItem configItem5 = new ConfigItem()
            {
                ItemCode = "WF_SubmitTitle",
                ItemValue = this.wf_SubmitTitle.Text
            };
            SysConfig.SetConfig(configItem5);
            ConfigItem configItem6 = new ConfigItem()
            {
                ItemCode = "WF_SubmitMsg",
                ItemValue = this.wf_SubmitMsg.Text
            };
            SysConfig.SetConfig(configItem6);
            ConfigItem configItem7 = new ConfigItem()
            {
                ItemCode = "WF_BackTitle",
                ItemValue = this.wf_BackTitle.Text
            };
            SysConfig.SetConfig(configItem7);
            ConfigItem configItem8 = new ConfigItem()
            {
                ItemCode = "WF_BackMsg",
                ItemValue = this.wf_BackMsg.Text
            };
            SysConfig.SetConfig(configItem8);
            ConfigItem configItem9 = new ConfigItem()
            {
                ItemCode = "WF_OverTimeCheck",
                ItemValue = this.wf_OverTimeCheck.SelectedValue
            };
            SysConfig.SetConfig(configItem9);
            ConfigItem configItem10 = new ConfigItem()
            {
                ItemCode = "WF_OverTimeInterval",
                ItemValue = this.wf_OverTimeInterval.Text
            };
            SysConfig.SetConfig(configItem10);
            ConfigItem configItem11 = new ConfigItem()
            {
                ItemCode = "WF_EnforceAgree",
                ItemValue = this.wf_EnforceAgree.SelectedValue
            };
            SysConfig.SetConfig(configItem11);
            base.ClientScript.RegisterStartupScript(base.GetType(), "", "<script language=javascript>alert(\"保存设置成功！\");</script>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                LDAPConfig config = LDAPConfig.GetConfig();
                MailConfig mailConfig = MailConfig.GetConfig();
                this.mail_Server.Text = mailConfig.ServerIP;
                this.mail_Port.Text = mailConfig.ServerPort;
                this.mail_SSL.SelectedValue = mailConfig.EnableSSL;
                this.mail_Async.SelectedValue = mailConfig.Async;
                this.mail_Account.Text = mailConfig.Account;
                this.mail_NiCheng.Text = mailConfig.NiCheng;
                this.mail_PassWord.Text = mailConfig.PassWord;
                this.mail_Enable.SelectedValue = (mailConfig.Enable == "禁用" ? "禁用" : "启用");
                this.mail_Subffix.Text = mailConfig.BodySubffix;
                this.ds_ServerIP.Text = config.ServerIP;
                this.ds_ServerPort.Text = config.ServerPort;
                this.ds_State.SelectedValue = config.State;
                this.ds_Root.Text = config.RootPath;
                this.ds_Account.Text = config.Account;
                this.ds_Pass.Text = config.PassWord;
                this.basic_CompanyCode.Text = SysConfig.GetConfig("Basic_CompanyCode", false).ItemValue;
                this.basic_DeskTop.SelectedValue = SysConfig.GetConfig("Basic_DeskTop", false).ItemValue;
                this.basic_HoursOneDay.Text = SysConfig.GetConfig("HoursOneDay", false).ItemValue;
                this.basic_StartTime1.Text = SysConfig.GetConfig("AM_StartTime", false).ItemValue;
                this.basic_EndTime1.Text = SysConfig.GetConfig("AM_EndTime", false).ItemValue;
                this.basic_StartTime2.Text = SysConfig.GetConfig("PM_StartTime", false).ItemValue;
                this.basic_EndTime2.Text = SysConfig.GetConfig("PM_EndTime", false).ItemValue;
                this.basic_OnlineRefreshInterval.Text = SysConfig.GetConfig("OnlineRefreshInterval", false).ItemValue;
                this.basic_VerifyCode_Show.SelectedValue = SysConfig.GetConfig("VerifyCode_Show", false).ItemValue;
                this.basic_ModifyPassFirstLogin.SelectedValue = SysConfig.GetConfig("ModifyPassFirstLogin", false).ItemValue;
                this.basic_ReloginLinkInMail.SelectedValue = SysConfig.GetConfig("ReloginLinkInMail", false).ItemValue;
                this.basic_ResMethod.SelectedValue = SysConfig.GetConfig("Basic_ResourceMethod", false).ItemValue;
                this.basic_EmployeeDefaultPass.Text = SysConfig.GetConfig("EmployeeDefaultPass", false).ItemValue;
                this.basic_AdminDefaultPass.Text = SysConfig.GetConfig("AdminDefaultPass", false).ItemValue;
                this.RadioButtonList2.SelectedValue = SysConfig.GetConfig("Basic_SysSwich", false).ItemValue;
                this.wf_WF_SubmitText.Text = SysConfig.GetConfig("WF_SubmitText", false).ItemValue;
                this.wf_WF_SignRejectText.Text = SysConfig.GetConfig("WF_SignRejectText", false).ItemValue;
                this.wf_WF_SignPrintStyle.Text = SysConfig.GetConfig("WF_SignPrintStyle", false).ItemValue;
                this.wf_ArriveTitle.Text = SysConfig.GetConfig("WF_ArriveTitle", false).ItemValue;
                this.wf_ArriveMsg.Text = SysConfig.GetConfig("WF_ArriveMsg", false).ItemValue;
                this.wf_SubmitTitle.Text = SysConfig.GetConfig("WF_SubmitTitle", false).ItemValue;
                this.wf_SubmitMsg.Text = SysConfig.GetConfig("WF_SubmitMsg", false).ItemValue;
                this.wf_BackTitle.Text = SysConfig.GetConfig("WF_BackTitle", false).ItemValue;
                this.wf_BackMsg.Text = SysConfig.GetConfig("WF_BackMsg", false).ItemValue;
                this.wf_OverTimeCheck.SelectedValue = SysConfig.GetConfig("WF_OverTimeCheck", false).ItemValue;
                this.wf_OverTimeInterval.Text = SysConfig.GetConfig("WF_OverTimeInterval", false).ItemValue;
                if (SysConfig.GetConfig("WF_EnforceAgree", false).ItemValue != "")
                {
                    this.wf_EnforceAgree.SelectedValue = SysConfig.GetConfig("WF_EnforceAgree", false).ItemValue;
                }
                this.ref_DefaultMain.Text = SysConfig.GetConfig("Ref_DefaultMain", false).ItemValue;
                this.ref_AppDefault.Text = SysConfig.GetConfig("Ref_AppDefault", false).ItemValue;
                this.ref_AppQuery.Text = SysConfig.GetConfig("Ref_AppQuery", false).ItemValue;
                this.ref_AppInput.Text = SysConfig.GetConfig("Ref_AppInput", false).ItemValue;
                this.ref_AppDetail.Text = SysConfig.GetConfig("Ref_AppDetail", false).ItemValue;
                this.ref_AppPrint.Text = SysConfig.GetConfig("Ref_AppPrint", false).ItemValue;


                this.txtAdminTel.Text = SysConfig.GetConfig("AdminDTel", false).ItemValue;
                this.txtTestTel.Text = SysConfig.GetConfig("TestTel", false).ItemValue;
                this.txtAdminEmail.Text = SysConfig.GetConfig("AdminEmail", false).ItemValue;
                this.txtTestEmail.Text = SysConfig.GetConfig("TestEmail", false).ItemValue;
                this.TextBox5.Text = SysConfig.GetConfig("txtSound", false).ItemValue;

               
     


            }
        }
    }
}