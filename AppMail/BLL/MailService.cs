using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.AppMail.DAL;
using EIS.AppMail.Model;
using EIS.DataAccess;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace EIS.AppMail.BLL
{
	public class MailService
	{
		private static Logger logger_0;

		static MailService()
		{
			MailService.logger_0 = LogManager.GetCurrentClassLogger();
		}

		public MailService()
		{
		}

        public IList<EIS.DataModel.Model.AppFile> GetMailAttaches(string mailId)
		{
			return (new FileService()).GetFiles(MailInfo.TableName, mailId);
		}

		public string GetNextMail(string employeeId, string folderId, string mailId, string sortdir)
		{
			string str = "";
			DataTable receiveList = (new _MailReceiver()).GetReceiveList(employeeId, folderId, sortdir);
			for (int i = 0; i < receiveList.Rows.Count; i++)
			{
				if (receiveList.Rows[i]["mailId"].ToString() == mailId && i < receiveList.Rows.Count - 1)
				{
					str = receiveList.Rows[i + 1]["mailId"].ToString();
				}
			}
			return str;
		}

		public string GetPrevMail(string employeeId, string folderId, string mailId, string sortdir)
		{
			string str = "";
			DataTable receiveList = (new _MailReceiver()).GetReceiveList(employeeId, folderId, sortdir);
			for (int i = 0; i < receiveList.Rows.Count; i++)
			{
				if (receiveList.Rows[i]["mailId"].ToString() == mailId && i > 0)
				{
					str = receiveList.Rows[i - 1]["mailId"].ToString();
				}
			}
			return str;
		}

		public DataTable GetReceiveMailData(string employeeId, string folderId)
		{
			return (new _MailReceiver()).GetReceiveList(employeeId, folderId);
		}

		public bool MailExist(string mailId)
		{
			return (new _MailMessage()).IsExist(mailId);
		}

		private string method_0(MailInfo mailInfo_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (mailInfo_0.Subject.Trim() == "")
			{
				stringBuilder.Append("<li>邮件主题不能为空");
			}
			if (mailInfo_0.ReceiverIDs.Trim() == "")
			{
				stringBuilder.Append("<li>收件人不能为空");
			}
			if (mailInfo_0.Sender.Trim() == "")
			{
				stringBuilder.Append("<li>邮件发送人不能为空");
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Insert(0, "<ol>");
				stringBuilder.Append("</ol>");
			}
			return stringBuilder.ToString();
		}

		public bool SaveMail(MailInfo model)
		{
			bool flag = true;
			DbConnection dbConnection = SysDatabase.CreateConnection();
			try
			{
				dbConnection.Open();
				DbTransaction dbTransaction = dbConnection.BeginTransaction();
				try
				{
					try
					{
						string str = this.method_0(model);
						if (str != "")
						{
							throw new Exception(str);
						}
						_MailMessage __MailMessage = new _MailMessage(dbTransaction);
						if (!__MailMessage.IsExist(model._AutoID))
						{
							__MailMessage.Add(model);
						}
						else
						{
							__MailMessage.Update(model);
						}
						dbTransaction.Commit();
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						dbTransaction.Rollback();
						flag = false;
						throw exception;
					}
				}
				finally
				{
					dbConnection.Close();
				}
			}
			finally
			{
				if (dbConnection != null)
				{
					((IDisposable)dbConnection).Dispose();
				}
			}
			return flag;
		}

		public bool SendMail(MailInfo model)
		{
			MailReceiver mailReceiver;
			int i;
			bool flag = true;
			DbConnection dbConnection = SysDatabase.CreateConnection();
			try
			{
				dbConnection.Open();
				DbTransaction dbTransaction = dbConnection.BeginTransaction();
				try
				{
					try
					{
						string str = this.method_0(model);
						if (str != "")
						{
							throw new Exception(str);
						}
						_MailMessage __MailMessage = new _MailMessage(dbTransaction);
						if (!__MailMessage.IsExist(model._AutoID))
						{
							__MailMessage.Add(model);
						}
						else
						{
							__MailMessage.Update(model);
						}
						string receiverIDs = model.ReceiverIDs;
						char[] chrArray = new char[] { ',' };
						string[] strArrays = receiverIDs.Split(chrArray);
						string receivers = model.Receivers;
						chrArray = new char[] { ',' };
						string[] strArrays1 = receivers.Split(chrArray);
						_MailReceiver __MailReceiver = new _MailReceiver(dbTransaction);
						for (i = 0; i < (int)strArrays.Length; i++)
						{
							mailReceiver = new MailReceiver()
							{
								_AutoID = Guid.NewGuid().ToString(),
								_OrgCode = "",
								_UserName = "",
								_CreateTime = DateTime.Now,
								_UpdateTime = DateTime.Now,
								_IsDel = 0,
								ReceiverID = strArrays[i],
								ReceiveName = strArrays1[i],
								FolderID = MailFolder.DefaultRecFolderID,
								MailID = model.MailID,
								SendTime = new DateTime?(DateTime.Now),
								SendType = "内部",
								State = 0,
								ReceiveType = "0"
							};
							__MailReceiver.Add(mailReceiver);
						}
						string[] strArrays2 = model.CCIDS.Split(",".ToCharArray());
						string[] strArrays3 = model.CC.Split(",".ToCharArray());
						for (i = 0; i < (int)strArrays2.Length; i++)
						{
							mailReceiver = new MailReceiver()
							{
								_AutoID = Guid.NewGuid().ToString(),
								_UserName = "",
								_OrgCode = "",
								_CreateTime = DateTime.Now,
								_UpdateTime = DateTime.Now,
								_IsDel = 0,
								ReceiverID = strArrays2[i],
								ReceiveName = strArrays3[i],
								FolderID = MailFolder.DefaultRecFolderID,
								MailID = model.MailID,
								SendTime = new DateTime?(DateTime.Now),
								SendType = "内部",
								State = 0,
								ReceiveType = "1"
							};
							__MailReceiver.Add(mailReceiver);
						}
						string[] strArrays4 = model.BCCIDS.Split(",".ToCharArray());
						string[] strArrays5 = model.BCC.Split(",".ToCharArray());
						for (i = 0; i < (int)strArrays4.Length; i++)
						{
							mailReceiver = new MailReceiver()
							{
								_AutoID = Guid.NewGuid().ToString(),
								_UserName = "",
								_OrgCode = "",
								_CreateTime = DateTime.Now,
								_UpdateTime = DateTime.Now,
								_IsDel = 0,
								ReceiverID = strArrays4[i],
								ReceiveName = strArrays5[i],
								FolderID = MailFolder.DefaultRecFolderID,
								MailID = model.MailID,
								SendTime = new DateTime?(DateTime.Now),
								SendType = "内部",
								State = 0,
								ReceiveType = "2"
							};
							__MailReceiver.Add(mailReceiver);
						}
						if (model.OutReceiverIDs.Length > 0)
						{
							MailMessage mailMessage = new MailMessage()
							{
								Subject = model.Subject,
								IsBodyHtml = true
							};
							Type type = typeof(MailPriority);
							int priority = model.Priority;
							mailMessage.Priority = (MailPriority)Enum.Parse(type, priority.ToString());
							mailMessage.Body = model.Body;
							string[] strArrays6 = model.OutReceiverIDs.Split(",".ToCharArray());
							for (int j = 0; j < (int)strArrays6.Length; j++)
							{
								string str1 = strArrays6[j];
								mailMessage.To.Add(str1);
							}
							mailMessage.From = new MailAddress(Class1.smethod_1(model.Sender));
							foreach (AppFile file in (new FileService()).GetFiles(MailInfo.TableName, model.MailID))
							{
								file.FilePath = string.Concat(AppFilePath.GetBasePath(file.BasePath), file.FilePath);
								FileStream fileStream = File.Open(file.FilePath, FileMode.Open);
								mailMessage.Attachments.Add(new Attachment(fileStream, file.FactFileName));
							}
							SmtpClient smtpClient = Class1.smethod_0(model.Sender);
							if (smtpClient.Host == "")
							{
								throw new Exception("默认的外发信箱设置不正确！");
							}
							smtpClient.Send(mailMessage);
							foreach (Attachment attachment in mailMessage.Attachments)
							{
								attachment.ContentStream.Close();
							}
						}
						dbTransaction.Commit();
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						dbTransaction.Rollback();
						flag = false;
						throw exception;
					}
				}
				finally
				{
					dbConnection.Close();
				}
			}
			finally
			{
				if (dbConnection != null)
				{
					((IDisposable)dbConnection).Dispose();
				}
			}
			return flag;
		}

		public static void SendMail(MailMessage mail)
		{
			if (mail.To.Count != 0)
			{
				MailConfig mailConfig = AppSettings.Instance.MailConfig;
				if ((mailConfig.ServerIP == "" ? true : mailConfig.Account == ""))
				{
					throw new Exception("默认的外发信箱设置不正确！");
				}
				mail.From = new MailAddress(mailConfig.Account);
				SmtpClient smtpClient = new SmtpClient()
				{
					Host = mailConfig.ServerIP
				};
				int num = 25;
				int.TryParse(mailConfig.ServerPort, out num);
				smtpClient.Port = num;
				smtpClient.EnableSsl = mailConfig.EnableSSL == "是";
				smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtpClient.UseDefaultCredentials = true;
				string account = mailConfig.Account;
				string passWord = mailConfig.PassWord;
				if (account != "")
				{
					smtpClient.Credentials = new NetworkCredential(account, passWord);
				}
				try
				{
					if (mailConfig.Async != "是")
					{
						smtpClient.Send(mail);
					}
					else
					{
						smtpClient.SendCompleted += new SendCompletedEventHandler(MailService.smethod_0);
						smtpClient.SendAsync(mail, mail);
					}
				}
				catch (Exception exception)
				{
					MailService.logger_0.Error<Exception>(exception);
				}
			}
		}

		private static void smethod_0(object sender, AsyncCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				throw new Exception("用户手动取消了邮件发送");
			}
			if (e.Error != null)
			{
				MailMessage userState = (MailMessage)e.UserState;
				Logger logger0 = MailService.logger_0;
				object[] message = new object[] { e.Error.Message, userState.Subject, userState.Body, userState.To.ToString() };
				logger0.Error("异步发送邮件时失败：{0}，\r\n收件人：{3}\r\n 邮件标题：{1}\r\n邮件内容：{2}", message);
			}
		}
	}
}