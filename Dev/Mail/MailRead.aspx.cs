using AjaxPro;
using EIS.AppBase;
using EIS.AppMail.BLL;
using EIS.AppMail.DAL;
using EIS.AppMail.Model;
using EIS.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web.Mail
{
	public partial class MailRead : PageBase
	{
		public string MailId = "";

		public string BodyHtml = "";

		public string FolderId = "";

		public string sortdir = "";

		public StringBuilder NextPrevBtn = new StringBuilder();

		public StringBuilder mailAttatches = new StringBuilder();

		public MailInfo mailModel;

	
		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void DeleteMail(string mailId)
		{
			(new _MailReceiver()).Delete(mailId, base.EmployeeID);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(MailRead));
			this.MailId = base.GetParaValue("MailId");
			this.FolderId = base.GetParaValue("FolderId");
			this.mailModel = (new _MailMessage()).GetModel(this.MailId);
			this.BodyHtml = this.mailModel.Body;
			_MailReceiver __MailReceiver = new _MailReceiver();
			__MailReceiver.UpdateState(this.MailId, base.EmployeeID, 1);
			MailService mailService = new MailService();
			this.sortdir = base.GetParaValue("sortdir");
			if (this.sortdir == "")
			{
				this.sortdir = "sendtime desc";
			}
			string prevMail = mailService.GetPrevMail(base.EmployeeID, this.FolderId, this.MailId, this.sortdir);
			string nextMail = mailService.GetNextMail(base.EmployeeID, this.FolderId, this.MailId, this.sortdir);
			if (prevMail != "")
			{
				this.NextPrevBtn.AppendFormat("<a id=\"prevmail\" href='MailRead.aspx?FolderId={0}&MailId={1}&sortdir={2}'>上一封</a>&nbsp;", this.FolderId, base.Server.UrlEncode(prevMail), this.sortdir);
			}
			else
			{
				this.NextPrevBtn.AppendFormat("<a style=\"color: #a0a0a0\" id=\"prevmail\" disabled>上一封</a>&nbsp;", new object[0]);
			}
			if (nextMail != "")
			{
				this.NextPrevBtn.AppendFormat("<a id=\"nextmail\" title='下一封'  href='MailRead.aspx?FolderId={0}&MailId={1}&sortdir={2}'>下一封</a>&nbsp;", this.FolderId, base.Server.UrlEncode(nextMail), this.sortdir);
			}
			else
			{
				this.NextPrevBtn.AppendFormat("<a style=\"color: #a0a0a0\" id=\"nextmail\" disabled>下一封</a>&nbsp;", new object[0]);
			}
			IList<AppFile> mailAttaches = mailService.GetMailAttaches(this.MailId);
			this.mailAttatches.AppendFormat("<span>{0}</span>&nbsp;个 <span class=\"tcolor\">(", mailAttaches.Count);
			foreach (AppFile mailAttach in mailAttaches)
			{
				this.mailAttatches.AppendFormat("&nbsp;<a href=\"../SysFolder/Common/FileDown.aspx?para={0}\" target='_blank'>{1}</a><span class=graytext>&nbsp;({2}K)</span><span>；</span>", base.CryptPara(string.Concat("fileid=", mailAttach._AutoID)), mailAttach.FactFileName, mailAttach.FileSize / 1024);
			}
			this.mailAttatches.Append(")</span>");
		}

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void RemoveMail(string mailId)
		{
			(new _MailReceiver()).Remove(mailId, base.EmployeeID);
		}
	}
}