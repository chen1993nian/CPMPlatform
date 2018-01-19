using EIS.AppBase;
using EIS.AppMail.BLL;
using EIS.AppMail.DAL;
using EIS.AppMail.MIME;
using EIS.AppMail.Model;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.Permission.Access;
using EIS.Permission.Model;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;

namespace EIS.AppMail
{
	public class POP3Main
	{
		protected POP3Main.service_state state = POP3Main.service_state.STARTED;

		protected System.Timers.Timer timer;

		protected int FetchIntervalInMinutes = 15;

		protected ArrayList websites;

		protected string Pop3Server;

		protected string Pop3Port;

		protected string Pop3UseSSL;

		protected int ReadInputStreamCharByChar = 0;

		private static Regex regex_0;

		private Thread thread_0;

		private static Logger logger_0;

		private static POP3Main pop3Main_0;

		public static POP3Main Instance
		{
			get
			{
				if (POP3Main.pop3Main_0 == null)
				{
					POP3Main.pop3Main_0 = new POP3Main();
				}
				return POP3Main.pop3Main_0;
			}
		}

		static POP3Main()
		{
			POP3Main.regex_0 = new Regex("\\|");
			POP3Main.logger_0 = LogManager.GetCurrentClassLogger();
			POP3Main.pop3Main_0 = null;
		}

		private POP3Main()
		{
			ServicePointManager.CertificatePolicy = new Class0();
			POP3Main.logger_0 = LogManager.GetCurrentClassLogger();
			POP3Main.logger_0.Debug("开始初始化线程");
			this.thread_0 = new Thread(new ThreadStart(this.ThreadProc));
			this.thread_0.Start();
			POP3Main.logger_0.Debug("获取邮件线程启动");
		}

		public void DoWork(object sender, ElapsedEventArgs e)
		{
			if (this.state == POP3Main.service_state.STARTED)
			{
				foreach (MailPOP3 mailPOP3 in Class1.smethod_2())
				{
					if (this.state != POP3Main.service_state.STARTED)
					{
						break;
					}
					try
					{
						POP3Main.logger_0.Debug(string.Concat("开始从", mailPOP3.Account, "获取邮件"));
						this.FetchMessages(mailPOP3);
						POP3Main.logger_0.Debug(string.Concat("从", mailPOP3.Account, "获取邮件完成"));
					}
					catch (Exception exception)
					{
						POP3Main.logger_0.Error<Exception>(exception);
						this.state = POP3Main.service_state.STOPPED;
					}
				}
			}
			this.Resume();
		}

		protected void FetchMessages(MailPOP3 model)
		{
			string[] strArrays = null;
			Regex regex = new Regex("\r\n");
			POP3client pOP3client = null;
			try
			{
				pOP3client = new POP3client(this.ReadInputStreamCharByChar);
				bool flag = false;
				if (this.Pop3UseSSL != "")
				{
					flag = (model.POP3SSL == 1 ? true : false);
				}
				pOP3client.connect(model.POP3Adrr, model.POP3Port, flag);
				pOP3client.USER(model.Account);
				pOP3client.PASS(model.PassWD);
				pOP3client.STAT();
				strArrays = regex.Split(pOP3client.LIST());
			}
			catch (Exception exception)
			{
				throw exception;
			}
			int num = 0;
			int length = (int)strArrays.Length - 1;
			for (int i = 1; i < length && this.state == POP3Main.service_state.STARTED; i++)
			{
				int num1 = strArrays[i].IndexOf(" ");
				num = Convert.ToInt32(strArrays[i].Substring(0, num1));
				string str = pOP3client.UIDL(num);
				string str1 = str.Split(" ".ToCharArray())[2];
				if (!(new MailService()).MailExist(str1))
				{
					string str2 = pOP3client.RETR(num);
					Employee employee = (new _Employee()).GetModel(model.Owner);
					if (employee != null)
					{
						try
						{
							POP3Main.logger_0.Debug(string.Concat("开始解析邮件：", str1));
							this.GetMailInfo(str2, str1, employee);
							POP3Main.logger_0.Debug(string.Concat("结束解析邮件：", str1));
						}
						catch (Exception exception2)
						{
							Exception exception1 = exception2;
							POP3Main.logger_0.Error(string.Concat("在解析邮件：", str1, "时发生错误"));
							POP3Main.logger_0.Error<Exception>(exception1);
							pOP3client.QUIT();
							throw exception1;
						}
					}
				}
			}
			pOP3client.QUIT();
		}

        protected void GetMailInfo(string message, string mailId, EIS.Permission.Model.Employee user)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder1 = new StringBuilder();
			MimeMessage mimeMessage = new MimeMessage();
			mimeMessage.LoadBody(message);
			DbConnection dbConnection = SysDatabase.CreateConnection();
			try
			{
				dbConnection.Open();
				DbTransaction dbTransaction = dbConnection.BeginTransaction();
				MailInfo mailInfo = new MailInfo()
				{
					MailID = mailId,
					FolderId = "0",
					CreateTime = DateTime.Now,
					BCC = "",
					BCCIDS = "",
					SendFlag = 1,
					Subject = mimeMessage.GetSubject(),
					Sender = mimeMessage.GetFrom(),
					Receivers = mimeMessage.GetTo(),
					CC = mimeMessage.GetCC()
				};
				mailInfo.BCC = mimeMessage.GetBCC();
				ArrayList arrayLists = new ArrayList();
				mimeMessage.GetBodyPartList(arrayLists);
				for (int i = 0; i < arrayLists.Count; i++)
				{
					MimeBody item = (MimeBody)arrayLists[i];
					if (item.IsText())
					{
						string lower = item.GetContentSubType().ToLower();
						string text = item.GetText();
						if (lower != "plain")
						{
							stringBuilder1.Append(text);
						}
						else
						{
							stringBuilder.Append(text);
						}
					}
				}
				mailInfo.Body = (stringBuilder1.Length == 0 ? stringBuilder.ToString() : stringBuilder1.ToString());
				try
				{
					try
					{
						(new _MailMessage(dbTransaction)).Add(mailInfo);
						_MailReceiver __MailReceiver = new _MailReceiver(dbTransaction);
						MailReceiver mailReceiver = new MailReceiver()
						{
							_AutoID = Guid.NewGuid().ToString(),
							_IsDel = 0,
							_UserName = user._AutoID,
							_CreateTime = DateTime.Now,
							_UpdateTime = DateTime.Now,
							ReceiverID = user._AutoID,
							ReceiveName = "",
							FolderID = MailFolder.DefaultRecFolderID,
							MailID = mailInfo.MailID,
							SendTime = new DateTime?(DateTime.Now),
							SendType = "外部",
							State = 0
						};
						__MailReceiver.Add(mailReceiver);
						this.method_0(arrayLists, mailId, dbTransaction, user);
						dbTransaction.Commit();
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						dbTransaction.Rollback();
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
		}

        private void method_0(ArrayList arrayList_0, string string_0, DbTransaction dbTransaction_0, EIS.Permission.Model.Employee employee_0)
		{
			for (int i = 0; i < arrayList_0.Count; i++)
			{
				MimeBody item = (MimeBody)arrayList_0[i];
				if (item.IsAttachment())
				{
					string name = item.GetName();
					string lower = Path.GetExtension(name).ToLower();
					DateTime now = DateTime.Now;
					string str = string.Concat(now.ToString("yyyy-MM-dd-HH-mm-ss-ffff", DateTimeFormatInfo.InvariantInfo), lower);
					now = DateTime.Now;
					string str1 = now.ToString("yyyy年MM月", DateTimeFormatInfo.InvariantInfo);
					string appFileSavePath = AppSettings.Instance.AppFileSavePath;
					try
					{
						if (!Directory.Exists(string.Concat(appFileSavePath, str1)))
						{
							Directory.CreateDirectory(string.Concat(appFileSavePath, str1));
						}
						string str2 = string.Concat(appFileSavePath, str1, "\\", str);
						int file = item.WriteToFile(str2);
						AppFile appFile = new AppFile()
						{
							_AutoID = Guid.NewGuid().ToString(),
							_UserName = employee_0._AutoID,
							_OrgCode = employee_0.DeptWbs,
							_CreateTime = DateTime.Now,
							_UpdateTime = DateTime.Now,
							_IsDel = 0,
							FileName = str,
							FactFileName = name,
							FilePath = string.Concat(str1, "\\", str),
							FileSize = file,
							FileType = lower,
							FolderID = "",
							AppId = string_0,
							AppName = MailInfo.TableName
						};
						(new _AppFile(dbTransaction_0)).Add(appFile);
					}
					catch (Exception exception)
					{
						throw exception;
					}
				}
			}
		}

		public void Pause()
		{
			this.state = POP3Main.service_state.PAUSED;
		}

		public void Resume()
		{
			POP3Main.logger_0.Debug("进入重新设置计时器");
			if (this.timer != null)
			{
				this.timer.Stop();
				this.timer.Dispose();
				POP3Main.logger_0.Debug("停止计时器");
			}
			this.timer = new System.Timers.Timer()
			{
				AutoReset = false
			};
			this.timer.Elapsed += new ElapsedEventHandler(this.DoWork);
			this.timer.Interval = (double)(0xea60 * this.FetchIntervalInMinutes);
			this.timer.Enabled = true;
			POP3Main.logger_0.Debug("退出重新设置计时器");
		}

		public void Start()
		{
			this.state = POP3Main.service_state.STARTED;
		}

		public void Stop()
		{
			this.state = POP3Main.service_state.STOPPED;
		}

		public void ThreadProc()
		{
			this.DoWork(null, null);
			while (true)
			{
				Thread.Sleep(0x7d0);
				if (this.state == POP3Main.service_state.STOPPED)
				{
					break;
				}
			}
			this.timer.Enabled = false;
			POP3Main.logger_0.Debug("退出获取邮件线程");
		}

		protected enum service_state
		{
			STARTED,
			PAUSED,
			STOPPED
		}
	}
}