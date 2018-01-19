using System;
using System.Globalization;

namespace EIS.AppMail.MIME
{
	public class MimeMessage : MimeBody
	{
		public MimeMessage()
		{
		}

		public string GetBCC()
		{
			return base.GetFieldValue("BCC");
		}

		public string GetCC()
		{
			return base.GetFieldValue("CC");
		}

		public string GetDate()
		{
			return base.GetFieldValue("Date");
		}

		public string GetFrom()
		{
			return base.GetFieldValue("From");
		}

		public string GetSubject()
		{
			return base.GetFieldValue("Subject");
		}

		public string GetTo()
		{
			return base.GetFieldValue("To");
		}

		public void SetBCC(string string_1, string charset)
		{
			base.SetFieldValue("BCC", string_1, charset);
		}

		public void SetCC(string string_1, string charset)
		{
			base.SetFieldValue("CC", string_1, charset);
		}

		public void SetDate(string date, string charset)
		{
			base.SetFieldValue("Date", date, charset);
		}

		public void SetDate()
		{
			DateTime now = DateTime.Now;
			string str = now.ToString("r", DateTimeFormatInfo.InvariantInfo);
			now = DateTime.Now;
			str = str.Replace("GMT", string.Concat(now.ToString("zz", DateTimeFormatInfo.InvariantInfo), "00"));
			base.SetFieldValue("Date", str, null);
		}

		public void SetFrom(string from, string charset)
		{
			base.SetFieldValue("From", from, charset);
		}

		public void SetSubject(string subject, string charset)
		{
			base.SetFieldValue("Subject", subject, charset);
		}

		public void SetTo(string string_1, string charset)
		{
			base.SetFieldValue("To", string_1, charset);
		}

		public void Setversion()
		{
			base.SetFieldValue("MIME-Version", "1.0", null);
		}
	}
}