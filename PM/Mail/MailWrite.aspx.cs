using EIS.AppBase;
using EIS.AppMail.BLL;
using EIS.AppMail.DAL;
using EIS.AppMail.Model;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.Mail
{
    public partial class MailWrite : PageBase
    {
       

        public StringBuilder msgInfo = new StringBuilder();

        public string mailId
        {
            get
            {
                return this.ViewState["mailId"].ToString();
            }
            set
            {
                this.ViewState["mailId"] = value;
            }
        }

     
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string text = this.TO_ID.Text;
            string str = this.TO_NAME.Text;
            string text1 = this.CC_ID.Text;
            string str1 = this.CC_Name.Text;
            string text2 = this.BCC_ID.Text;
            string str2 = this.BCC_Name.Text;
            string text3 = this.Out_ID.Text;
            string str3 = this.Out_Name.Text;
            string text4 = this.Subject.Text;
            string str4 = this.MailBody.Text;
            try
            {
                MailInfo mailInfo = new MailInfo()
                {
                    _AutoID = this.mailId,
                    _CreateTime = DateTime.Now,
                    _UpdateTime = DateTime.Now,
                    _UserName = base.EmployeeID,
                    _OrgCode = base.OrgCode,
                    _IsDel = 0,
                    MailID = this.mailId,
                    Priority = 0,
                    ReceiverIDs = text,
                    Receivers = str,
                    CCIDS = text1,
                    CC = str1,
                    BCCIDS = text2,
                    BCC = str2,
                    OutReceiverIDs = text3,
                    OutReceivers = str3
                };
                mailInfo.ReceiverIDs = text;
                mailInfo.ReceiverIDs = text;
                mailInfo.Subject = text4;
                mailInfo.Body = str4;
                mailInfo.CreateTime = DateTime.Now;
                mailInfo.SendFlag = 1;
                mailInfo.Sender = base.EmployeeID;
                mailInfo.SenderName = base.EmployeeName;
                (new MailService()).SendMail(mailInfo);
                this.Session["_sysinfo"] = "邮件已经发送成功！";
                base.Response.Redirect(string.Concat("MailAfterSend.aspx?mailId=", this.mailId));
            }
            catch (Exception exception)
            {
                this.msgInfo.AppendFormat("<div class='tip'>{0}</div>", exception.Message);
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string text = this.TO_ID.Text;
            string str = this.TO_NAME.Text;
            string text1 = this.CC_ID.Text;
            string str1 = this.CC_Name.Text;
            string text2 = this.BCC_ID.Text;
            string str2 = this.BCC_Name.Text;
            string text3 = this.Out_ID.Text;
            string str3 = this.Out_Name.Text;
            string text4 = this.Subject.Text;
            string str4 = this.MailBody.Text;
            try
            {
                MailInfo mailInfo = new MailInfo()
                {
                    _AutoID = this.mailId,
                    _CreateTime = DateTime.Now,
                    _UpdateTime = DateTime.Now,
                    _UserName = base.EmployeeID,
                    _OrgCode = base.OrgCode,
                    _IsDel = 0,
                    Priority = 0,
                    ReceiverIDs = text,
                    Receivers = str,
                    CCIDS = text1,
                    CC = str1,
                    BCCIDS = text2,
                    BCC = str2,
                    OutReceiverIDs = text3,
                    OutReceivers = str3
                };
                mailInfo.ReceiverIDs = text;
                mailInfo.ReceiverIDs = text;
                mailInfo.Subject = text4;
                mailInfo.Body = str4;
                mailInfo.SendFlag = 0;
                mailInfo.Sender = base.EmployeeID;
                mailInfo.SenderName = base.EmployeeName;
                mailInfo.CreateTime = DateTime.Now;
                (new MailService()).SaveMail(mailInfo);
                this.Session["_sysinfo"] = "邮件已经保存到草稿箱了！";
                base.Response.Redirect(string.Concat("MailAfterSend.aspx?mailId=", this.mailId));
            }
            catch (Exception exception)
            {
                this.msgInfo.AppendFormat("<div class='tip'>{0}</div>", exception.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string paraValue = base.GetParaValue("act");
                string mailID = base.GetParaValue("MailId");
                if (mailID == "")
                {
                    string str = base.GetParaValue("recId");
                    if (str != "")
                    {
                        mailID = (new _MailReceiver()).GetModel(str).MailID;
                    }
                }
                if ((string.IsNullOrEmpty(paraValue) ? true : !(mailID != "")))
                {
                    this.mailId = Guid.NewGuid().ToString();
                }
                else
                {
                    MailInfo model = (new _MailMessage()).GetModel(mailID);
                    if (paraValue == "0")
                    {
                        this.mailId = mailID;
                        this.TO_NAME.Text = model.Receivers;
                        this.TO_ID.Text = model.ReceiverIDs;
                        this.CC_Name.Text = model.CC;
                        this.CC_ID.Text = model.CCIDS;
                        this.BCC_Name.Text = model.BCC;
                        this.BCC_ID.Text = model.BCCIDS;
                        this.Subject.Text = model.Subject;
                        this.MailBody.Text = model.Body;
                    }
                    else if (paraValue == "1")
                    {
                        this.mailId = Guid.NewGuid().ToString();
                        this.Subject.Text = string.Concat("回复：", model.Subject);
                        this.TO_NAME.Text = model.SenderName;
                        this.TO_ID.Text = model.Sender;
                        this.CC_Name.Text = model.CC;
                        this.CC_ID.Text = model.CCIDS;
                        this.BCC_Name.Text = model.BCC;
                        this.BCC_ID.Text = model.BCCIDS;
                        this.MailBody.Text = string.Concat("<br/><br/>---------邮件原文---------<br/><br/>", model.Body);
                    }
                    else if (paraValue == "2")
                    {
                        this.mailId = Guid.NewGuid().ToString();
                        this.Subject.Text = string.Concat("转发：", model.Subject);
                        FileService fileService = new FileService();
                        _AppFile __AppFile = new _AppFile();
                        foreach (AppFile file in fileService.GetFiles("T_E_Mail_Message", mailID))
                        {
                            file.AppId = this.mailId;
                            file.AppName = "T_E_Mail_Message";
                            file._AutoID = Guid.NewGuid().ToString();
                            __AppFile.Add(file);
                        }
                        this.MailBody.Text = string.Concat("<br/><br/>---------邮件原文---------<br/><br/>", model.Body);
                    }
                }
            }
        }
    }
}