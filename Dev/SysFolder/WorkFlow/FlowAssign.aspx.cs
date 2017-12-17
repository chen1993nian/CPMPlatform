using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.AppModel.Service;
using EIS.DataAccess;
using EIS.DataModel.Model;
using EIS.Permission.Service;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.XPDLParser.Elements;
using NLog;
using System;
using System.Collections.Specialized;
using System.Data.Common;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.WebBase.SysFolder.WorkFlow
{
    public partial class FlowAssign : PageBase
    {
       
        public StringBuilder sbHtml = new StringBuilder();

        private string string_0 = "";

        public string TipMsg = "";

        public Task curTask = new Task();

        public Instance curInstance = null;

        public FlowAssign()
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string stringCollection = null;
            string mail;
            MailMessage mailMessage;
            string text = this.txtReason.Text;
            string str = this.txtLeaderId.Value.Trim();
            char[] chrArray = new char[] { ',' };
            string str1 = str.Trim(chrArray);
            if (!string.IsNullOrEmpty(str1))
            {
                DriverEngine driverEngine = new DriverEngine(base.UserInfo);
                StringCollection stringCollections = new StringCollection();
                StringCollection stringCollections1 = new StringCollection();
                chrArray = new char[] { ',' };
                stringCollections1.AddRange(str1.Split(chrArray));
                string str2 = "";
                DbConnection dbConnection = SysDatabase.CreateConnection();
                try
                {
                    dbConnection.Open();
                    DbTransaction dbTransaction = dbConnection.BeginTransaction();
                    try
                    {
                        try
                        {
                            stringCollections = driverEngine.AssignTask(this.string_0, stringCollections1, dbTransaction);
                            if (this.CheckBox1.Checked)
                            {
                                str2 = "系统消息";
                                AppMsg appMsg = new AppMsg(base.UserInfo)
                                {
                                    Title = string.Concat("加签任务提醒:", this.curInstance.InstanceName),
                                    MsgType = "",
                                    MsgUrl = string.Concat("SysFolder/Workflow/DealFlow.aspx?sysTaskId=", this.string_0),
                                    RecIds = str1,
                                    RecNames = this.txtLeader.Text,
                                    SendTime = new DateTime?(DateTime.Now),
                                    Sender = base.EmployeeName,
                                    Content = string.Format("{0}把您加入到【{1}】任务处理人列表中，现需要您加签处理。\r\n以下是他（她的）附言：{2}", base.EmployeeName, this.curInstance.InstanceName, this.txtReason.Text)
                                };
                                AppMsgService.SendMessage(appMsg);
                            }
                            if (this.CheckBox2.Checked)
                            {
                                str2 = (str2.Length <= 0 ? "电子邮件" : string.Concat(str2, "和电子邮件"));
                                string str3 = string.Concat("加签任务提醒:", this.curInstance.InstanceName);
                                string str4 = string.Format("{0}把您加入到【{1}】任务处理人列表中，现需要您加签处理。\r\n以下是他（她的）附言：{2}", base.EmployeeName, this.curInstance.InstanceName, this.txtReason.Text);
                                string str5 = this.Session["WebId"].ToString();
                                string str6 = string.Concat(AppSettings.GetRootURI(str5), "/SysFolder/Workflow/DealFlow.aspx?sysTaskId=", this.string_0);
                                string itemValue = SysConfig.GetConfig("ReloginLinkInMail").ItemValue;
                                str4 = EIS.WorkFlow.Engine.Utility.FormatMailHtml(str4);
                                if (itemValue != "否")
                                {
                                    mailMessage = new MailMessage()
                                    {
                                        Subject = str3,
                                        IsBodyHtml = true,
                                        Priority = MailPriority.Normal,
                                        Body = string.Concat(str4, string.Format("<br/><a href='{0}' target='_blank'>点击处理任务</a>", str6))
                                    };
                                    foreach (string stringCollectionA in stringCollections)
                                    {
                                        mail = EmployeeService.GetMail(stringCollectionA);
                                        if (mail == "")
                                        {
                                            continue;
                                        }
                                        mailMessage.To.Add(mail);
                                    }
                                    EIS.WorkFlow.Engine.Utility.SendMail(mailMessage);
                                }
                                else
                                {
                                    foreach (string stringCollection1 in stringCollections)
                                    {
                                        mail = EmployeeService.GetMail(stringCollection1);
                                        if (mail == "")
                                        {
                                            continue;
                                        }
                                        mailMessage = new MailMessage();
                                        mailMessage.To.Add(mail);
                                        mailMessage.Subject = str3;
                                        mailMessage.IsBodyHtml = true;
                                        mailMessage.Priority = MailPriority.Normal;
                                        string str7 = AppSettings.GenAutoLoginUrl(EmployeeService.GetEmployeeAttrById(stringCollection1, "LoginName"), str5, str6, 0);
                                        mailMessage.Body = string.Concat(str4, string.Format("<br/><a href='{0}' target='_blank'>点击处理任务</a>", str7));
                                        EIS.WorkFlow.Engine.Utility.SendMail(mailMessage);
                                    }
                                }
                            }
                            dbTransaction.Commit();
                            if (stringCollections.Count <= 0)
                            {
                                this.Session["_sysinfo"] = string.Concat("由于【", this.txtLeader.Text, "】已经是本步骤任务处理人之一，所以未能加签任务给他（们）");
                            }
                            else
                            {
                                string employeeNameList = EmployeeService.GetEmployeeNameList(stringCollections);
                                string[] strArrays = new string[] { "任务已经分配给：【", employeeNameList, "】，并发送", str2, "通知他（们）" };
                                string str8 = string.Concat(strArrays);
                                if (stringCollections.Count != stringCollections1.Count)
                                {
                                    str8 = string.Concat(str8, "，未能加签的是由于他（们)已经是本步骤任务处理人之一");
                                }
                                this.Session["_sysinfo"] = str8;
                            }
                            base.Response.Redirect(string.Concat("DealFlowAfter.aspx?taskId=", this.string_0, "&info=1"), false);
                        }
                        catch (Exception exception1)
                        {
                            Exception exception = exception1;
                            this.fileLogger.Error<Exception>(exception);
                            dbTransaction.Rollback();
                            this.Session["_syserror"] = exception.Message;
                            base.Response.Redirect(string.Concat("FlowErrorInfo.aspx?taskId=", this.string_0), false);
                        }
                    }
                    finally
                    {
                        dbConnection.Close();
                    }
                }
                finally
                {
                    if (dbConnection != null)
                    {
                        ((IDisposable)dbConnection).Dispose();
                    }
                }
            }
            else
            {
                this.TipMsg = "<div class='info'>*&nbsp;没有选择加签人</div>";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.string_0 = base.GetParaValue("taskId");
            DriverEngine driverEngine = new DriverEngine(base.UserInfo);
            this.curTask = driverEngine.GetTaskById(this.string_0);
            this.curInstance = driverEngine.GetInstanceById(this.curTask.InstanceId);
            driverEngine.GetActivityById(this.curInstance, this.curTask.ActivityId);
            if (!base.IsPostBack && !string.IsNullOrEmpty(base.Request["reason"]))
            {
                this.txtReason.Text = base.Request["reason"];
            }
        }
    }
}