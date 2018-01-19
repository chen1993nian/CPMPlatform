using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.AppMail.Model
{
	[Serializable]
	public class MailInfo : AppModelBase
	{
		public readonly static string TableName;

		public string BCC
		{
			get;
			set;
		}

		public string BCCIDS
		{
			get;
			set;
		}

		public string Body
		{
			get;
			set;
		}

		public string CC
		{
			get;
			set;
		}

		public string CCIDS
		{
			get;
			set;
		}

		public DateTime CreateTime
		{
			get;
			set;
		}

		public string FolderId
		{
			get;
			set;
		}

		public string MailID
		{
			get;
			set;
		}

		public string OutReceiverIDs
		{
			get;
			set;
		}

		public string OutReceivers
		{
			get;
			set;
		}

		public int Priority
		{
			get;
			set;
		}

		public string ReceiverIDs
		{
			get;
			set;
		}

		public string Receivers
		{
			get;
			set;
		}

		public string Sender
		{
			get;
			set;
		}

		public string SenderName
		{
			get;
			set;
		}

		public int SendFlag
		{
			get;
			set;
		}

		public string Subject
		{
			get;
			set;
		}

		static MailInfo()
		{
			MailInfo.TableName = "T_E_Mail_Message";
		}

		public MailInfo()
		{
			base._IsDel = 0;
			base._CreateTime = DateTime.Now;
		}
	}
}