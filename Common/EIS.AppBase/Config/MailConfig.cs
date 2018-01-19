using EIS.AppBase;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace EIS.AppBase.Config
{
	public class MailConfig
	{
		public string EnableSSL = "";

		public string Async = "";

		public string ServerIP = "";

		public string ServerPort = "";

		public string Enable = "启用";

		public string BodySubffix = "";

		public string Account
		{
			get;
			set;
		}

		public string NiCheng
		{
			get;
			set;
		}

		public string PassWord
		{
			get;
			set;
		}

		public MailConfig()
		{
		}

		public static MailConfig GetConfig()
		{
			MailConfig mailConfig = new MailConfig();
			StringCollection stringCollections = new StringCollection();
			string[] strArrays = new string[] { "SysMail_Server", "SysMail_Port", "SysMail_SSL", "SysMail_Async", "SysMail_Account", "SysMail_NiCheng", "SysMail_PassWord", "SysMail_Enable", "SysMail_Subffix" };
			stringCollections.AddRange(strArrays);
			List<ConfigItem> config = SysConfig.GetConfig(stringCollections);
			ConfigItem configItem = config.Find((ConfigItem c) => c.ItemCode == "SysMail_SSL");
			if (configItem != null)
			{
				mailConfig.EnableSSL = configItem.ItemValue;
			}
			configItem = config.Find((ConfigItem c) => c.ItemCode == "SysMail_Async");
			if (configItem != null)
			{
				mailConfig.Async = configItem.ItemValue;
			}
			configItem = config.Find((ConfigItem c) => c.ItemCode == "SysMail_Server");
			if (configItem != null)
			{
				mailConfig.ServerIP = configItem.ItemValue;
			}
			configItem = config.Find((ConfigItem c) => c.ItemCode == "SysMail_Port");
			if (configItem != null)
			{
				mailConfig.ServerPort = configItem.ItemValue;
			}
			configItem = config.Find((ConfigItem c) => c.ItemCode == "SysMail_Account");
			if (configItem != null)
			{
				mailConfig.Account = configItem.ItemValue;
			}
			configItem = config.Find((ConfigItem c) => c.ItemCode == "SysMail_NiCheng");
			if (configItem != null)
			{
				mailConfig.NiCheng = configItem.ItemValue;
			}
			configItem = config.Find((ConfigItem c) => c.ItemCode == "SysMail_PassWord");
			if (configItem != null)
			{
				mailConfig.PassWord = configItem.ItemValue;
			}
			configItem = config.Find((ConfigItem c) => c.ItemCode == "SysMail_Enable");
			if (configItem != null)
			{
				mailConfig.Enable = configItem.ItemValue;
			}
			configItem = config.Find((ConfigItem c) => c.ItemCode == "SysMail_Subffix");
			if (configItem != null)
			{
				mailConfig.BodySubffix = configItem.ItemValue;
			}
			return mailConfig;
		}

		public static void SetConfig(MailConfig config)
		{
			ConfigItem configItem = new ConfigItem()
			{
				ItemCode = "SysMail_Server",
				ItemValue = config.ServerIP
			};
			SysConfig.SetConfig(configItem);
			ConfigItem configItem1 = new ConfigItem()
			{
				ItemCode = "SysMail_Port",
				ItemValue = config.ServerPort
			};
			SysConfig.SetConfig(configItem1);
			ConfigItem configItem2 = new ConfigItem()
			{
				ItemCode = "SysMail_SSL",
				ItemValue = config.EnableSSL
			};
			SysConfig.SetConfig(configItem2);
			ConfigItem configItem3 = new ConfigItem()
			{
				ItemCode = "SysMail_Async",
				ItemValue = config.Async
			};
			SysConfig.SetConfig(configItem3);
			ConfigItem configItem4 = new ConfigItem()
			{
				ItemCode = "SysMail_Account",
				ItemValue = config.Account
			};
			SysConfig.SetConfig(configItem4);
			ConfigItem configItem5 = new ConfigItem()
			{
				ItemCode = "SysMail_NiCheng",
				ItemValue = config.NiCheng
			};
			SysConfig.SetConfig(configItem5);
			ConfigItem configItem6 = new ConfigItem()
			{
				ItemCode = "SysMail_PassWord",
				ItemValue = config.PassWord
			};
			SysConfig.SetConfig(configItem6);
			ConfigItem configItem7 = new ConfigItem()
			{
				ItemCode = "SysMail_Enable",
				ItemValue = config.Enable
			};
			SysConfig.SetConfig(configItem7);
			ConfigItem configItem8 = new ConfigItem()
			{
				ItemCode = "SysMail_Subffix",
				ItemValue = config.BodySubffix
			};
			SysConfig.SetConfig(configItem8);
			AppSettings.Instance.MailConfig = config;
		}
	}
}