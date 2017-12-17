using AjaxPro;
using EIS.AppBase;
using EIS.AppMail.DAL;
using EIS.AppMail.Model;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.Mail
{
	public partial class MailFolderList : PageBase
	{
		

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void ChangeFolder(string editid, string folderName, string order)
		{
			_MailFolder __MailFolder = new _MailFolder();
			MailFolder model = __MailFolder.GetModel(editid);
			model.FolderName = folderName;
			model.SN = int.Parse(order);
			__MailFolder.Update(model);
		}

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void DelFolder(string editid)
		{
			(new _MailFolder()).Delete(editid);
		}

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void NewFolder(string folderName, string order)
		{
			_MailFolder __MailFolder = new _MailFolder();
			MailFolder mailFolder = new MailFolder(base.UserInfo)
			{
				FolderName = folderName,
				Owner = base.EmployeeID,
				SN = int.Parse(order)
			};
			__MailFolder.Add(mailFolder);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(MailFolderList));
		}
	}
}