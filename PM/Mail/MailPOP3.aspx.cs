using AjaxPro;
using EIS.AppBase;
using EIS.AppMail;
using EIS.AppMail.DAL;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.Mail
{
	public partial class MailPOP3 : PageBase
	{
		

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void DelRecord(string editid)
		{
			(new _MailPOP3()).Delete(editid);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(MailPOP3));
		}

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void StartPop3()
		{
			POP3Main.Instance.Start();
		}

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void StopPop3()
		{
			POP3Main.Instance.Stop();
		}
	}
}