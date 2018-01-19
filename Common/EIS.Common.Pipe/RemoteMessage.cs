using NLog;
using System;
using System.Collections.Specialized;
using System.Configuration;

namespace EIS.Common.Pipe
{
	public class RemoteMessage
	{
		private static Logger fileLogger;

		static RemoteMessage()
		{
			RemoteMessage.fileLogger = LogManager.GetCurrentClassLogger();
		}

		public RemoteMessage()
		{
		}

		public static void SendSms(string phoneList, string msg)
		{
			string str = "";
			str = (ConfigurationManager.AppSettings["SmsPipeServer"] == null ? ".;SmsPipe" : ConfigurationManager.AppSettings["SmsPipeServer"].ToString());
			string[] strArrays = str.Split(new char[] { ';' });
			try
			{
				NamedPipeClient namedPipeClient = new NamedPipeClient(strArrays[0], strArrays[1]);
				try
				{
					if (namedPipeClient.Send(string.Concat(phoneList, ";=;", msg)) != "1")
					{
						throw new Exception("发送短信时发生未知错误");
					}
				}
				finally
				{
					if (namedPipeClient != null)
					{
						((IDisposable)namedPipeClient).Dispose();
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				RemoteMessage.fileLogger.Error(string.Concat("发送短信时出错：", exception.Message));
				throw new Exception(string.Concat("发送短信时出错：", exception.Message));
			}
		}
	}
}