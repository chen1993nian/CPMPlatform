using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.AppMail.Model
{
	[Serializable]
	public class MailReceiver : AppModelBase
	{
		public string FolderID
		{
			get;
			set;
		}

		public string MailID
		{
			get;
			set;
		}

		public DateTime? ReadTime
		{
			get;
			set;
		}

		public string ReceiveName
		{
			get;
			set;
		}

		public string ReceiverID
		{
			get;
			set;
		}

		public string ReceiveType
		{
			get;
			set;
		}

		public DateTime? SendTime
		{
			get;
			set;
		}

		public string SendType
		{
			get;
			set;
		}

		public int State
		{
			get;
			set;
		}

		public MailReceiver()
		{
		}
	}
}