using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.AppMail.Model
{
	[Serializable]
	public class MailFolder : AppModelBase
	{
		public readonly static string DefaultRecFolderID;

		public string FolderName
		{
			get;
			set;
		}

		public string Owner
		{
			get;
			set;
		}

		public int SN
		{
			get;
			set;
		}

		static MailFolder()
		{
			MailFolder.DefaultRecFolderID = "0";
		}

		public MailFolder()
		{
		}

		public MailFolder(UserContext user)
		{
			base._AutoID = Guid.NewGuid().ToString();
			base._UserName = user.EmployeeId;
			base._OrgCode = user.DeptWbs;
			base._IsDel = 0;
			base._CreateTime = DateTime.Now;
			base._UpdateTime = DateTime.Now;
		}
	}
}