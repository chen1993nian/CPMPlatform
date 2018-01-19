using EIS.AppBase;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EIS.AppMail.Model
{
	[Serializable]
	public class MailPOP3 : AppModelBase
	{
		[Description("登录帐户")]
		public string Account
		{
			get;
			set;
		}

		[Description("自动接收")]
		public int AutoReceive
		{
			get;
			set;
		}

		[Description("需要凭证")]
		public int CredentialRequired
		{
			get;
			set;
		}

		[Description("接收之后删除服务器上邮件")]
		public int DelAfterRec
		{
			get;
			set;
		}

		[Description("邮件地址")]
		public string EmailAdrr
		{
			get;
			set;
		}

		[Description("外发邮件默认邮箱")]
		public int IsDefault
		{
			get;
			set;
		}

		[Description("最大空间M")]
		public int MaxSize
		{
			get;
			set;
		}

		[Description("所有人ID")]
		public string Owner
		{
			get;
			set;
		}

		[Description("账户密码")]
		public string PassWD
		{
			get;
			set;
		}

		[Description("POP3服务器地址")]
		public string POP3Adrr
		{
			get;
			set;
		}

		[Description("POP3服务器端口")]
		public int POP3Port
		{
			get;
			set;
		}

		[Description("POP3是否需要安全连接")]
		public int POP3SSL
		{
			get;
			set;
		}

		[Description("SMTP服务器地址")]
		public string SMTPAdrr
		{
			get;
			set;
		}

		[Description("SMTP服务器端口")]
		public int SMTPPort
		{
			get;
			set;
		}

		[Description("SMTP是否需要安全连接")]
		public int SMTPSSL
		{
			get;
			set;
		}

		public MailPOP3()
		{
		}
	}
}