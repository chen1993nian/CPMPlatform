using EIS.AppBase;
using EIS.AppMail.DAL;
using EIS.AppMail.Model;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.Mail
{
    public partial class MailPOP3Edit : PageBase
    {
        

        public string editid = "";

    

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            EIS.AppMail.Model.MailPOP3 mailPOP3 = new EIS.AppMail.Model.MailPOP3();
            _MailPOP3 __MailPOP3 = new _MailPOP3();
            int num = 0;
            if (string.IsNullOrEmpty(base.Request["editid"]))
            {
                mailPOP3._AutoID = Guid.NewGuid().ToString();
                mailPOP3._OrgCode = base.OrgCode;
                mailPOP3._IsDel = 0;
                mailPOP3._UserName = base.EmployeeID;
                mailPOP3._CreateTime = DateTime.Now;
                mailPOP3._UpdateTime = DateTime.Now;
                mailPOP3.Owner = base.EmployeeID;
                mailPOP3.EmailAdrr = this.tb_email.Text;
                mailPOP3.POP3Adrr = this.tb_pop3.Text;
                mailPOP3.POP3Port = int.Parse(this.tb_pop3Port.Text);
                mailPOP3.POP3SSL = (this.chk_pop3SSL.Checked ? 1 : 0);
                mailPOP3.SMTPAdrr = this.tb_smtp.Text;
                mailPOP3.SMTPPort = int.Parse(this.tb_smtpPort.Text);
                mailPOP3.SMTPSSL = (this.chk_smtpSSL.Checked ? 1 : 0);
                mailPOP3.Account = this.tb_account.Text;
                mailPOP3.PassWD = this.tb_passwd.Text;
                mailPOP3.CredentialRequired = int.Parse(this.ddl_needAuthen.SelectedValue);
                mailPOP3.AutoReceive = int.Parse(this.ddl_autoRec.SelectedValue);
                mailPOP3.IsDefault = (this.chk_default.Checked ? 1 : 0);
                mailPOP3.DelAfterRec = (this.chk_delAfterRec.Checked ? 1 : 0);
                num = __MailPOP3.Add(mailPOP3);
            }
            else
            {
                mailPOP3 = __MailPOP3.GetModel(this.editid);
                mailPOP3.EmailAdrr = this.tb_email.Text;
                mailPOP3.POP3Adrr = this.tb_pop3.Text;
                mailPOP3.POP3Port = int.Parse(this.tb_pop3Port.Text);
                mailPOP3.POP3SSL = (this.chk_pop3SSL.Checked ? 1 : 0);
                mailPOP3.SMTPAdrr = this.tb_smtp.Text;
                mailPOP3.SMTPPort = int.Parse(this.tb_smtpPort.Text);
                mailPOP3.SMTPSSL = (this.chk_smtpSSL.Checked ? 1 : 0);
                mailPOP3.Account = this.tb_account.Text;
                mailPOP3.PassWD = this.tb_passwd.Text;
                mailPOP3.CredentialRequired = int.Parse(this.ddl_needAuthen.SelectedValue);
                mailPOP3.AutoReceive = int.Parse(this.ddl_autoRec.SelectedValue);
                mailPOP3.IsDefault = (this.chk_default.Checked ? 1 : 0);
                mailPOP3.DelAfterRec = (this.chk_delAfterRec.Checked ? 1 : 0);
                num = __MailPOP3.Update(mailPOP3);
            }
            if (num <= 0)
            {
                base.alert("保存出错！");
            }
            else
            {
                base.Response.Write("<script>alert('保存成功！');window.opener.app_query();window.close();</script>");
                base.Response.Flush();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.editid = base.GetParaValue("editid");
            if (!base.IsPostBack)
            {
                EIS.AppMail.Model.MailPOP3 model = (new _MailPOP3()).GetModel(this.editid);
                if (model == null)
                {
                    this.tb_pop3Port.Text = "110";
                    this.tb_smtpPort.Text = "25";
                }
                else
                {
                    this.tb_email.Text = model.EmailAdrr;
                    this.tb_pop3.Text = model.POP3Adrr;
                    this.tb_pop3Port.Text = model.POP3Port.ToString();
                    this.chk_pop3SSL.Checked = model.POP3SSL == 1;
                    this.tb_smtp.Text = model.SMTPAdrr;
                    this.tb_smtpPort.Text = model.SMTPPort.ToString();
                    this.chk_smtpSSL.Checked = model.SMTPSSL == 1;
                    this.tb_account.Text = model.Account;
                    this.tb_passwd.Text = model.PassWD;
                    this.ddl_needAuthen.SelectedValue = model.CredentialRequired.ToString();
                    this.ddl_autoRec.SelectedValue = model.AutoReceive.ToString();
                    this.chk_default.Checked = model.IsDefault == 1;
                    this.chk_delAfterRec.Checked = model.DelAfterRec == 1;
                }
            }
        }
    }
}