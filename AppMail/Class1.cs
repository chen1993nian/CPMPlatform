using EIS.AppMail.DAL;
using EIS.AppMail.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;

internal class Class1
{
	public static SmtpClient smethod_0(string string_0)
	{
		SmtpClient smtpClient = new SmtpClient();
		_MailPOP3 __MailPOP3 = new _MailPOP3();
		DataTable list = __MailPOP3.GetList(string.Concat("Owner='", string_0, "' and IsDefault=1"));
		if (list.Rows.Count > 0)
		{
			DataRow item = list.Rows[0];
			item["_AutoID"].ToString();
			smtpClient.Host = item["SMTPAdrr"].ToString();
			smtpClient.Port = int.Parse(item["SMTPPort"].ToString());
			smtpClient.EnableSsl = int.Parse(item["SMTPSSL"].ToString()) == 1;
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.UseDefaultCredentials = true;
			string str = item["Account"].ToString();
			string str1 = item["PassWD"].ToString();
			if (str != "")
			{
				smtpClient.Credentials = new NetworkCredential(str, str1);
			}
		}
		return smtpClient;
	}

	public static string smethod_1(string string_0)
	{
		string str;
		_MailPOP3 __MailPOP3 = new _MailPOP3();
		DataTable list = __MailPOP3.GetList(string.Concat("Owner='", string_0, "' and IsDefault=1"));
		if (list.Rows.Count <= 0)
		{
			str = "";
		}
		else
		{
			DataRow item = list.Rows[0];
			str = item["EmailAdrr"].ToString();
		}
		return str;
	}

	public static IList<MailPOP3> smethod_2()
	{
		return (new _MailPOP3()).GetModelList("AutoReceive=1");
	}
}