using EIS.AppBase;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace EIS.AppBase.Config
{
	public class LDAPConfig
	{
		public string State = "";

		public string ServerIP = "";

		public string ServerPort = "";

		public string Account
		{
			get;
			set;
		}

		public string PassWord
		{
			get;
			set;
		}

		public string RootPath
		{
			get;
			set;
		}

		public LDAPConfig()
		{
		}

		public static LDAPConfig GetConfig()
		{
			LDAPConfig lDAPConfig = new LDAPConfig();
			StringCollection stringCollections = new StringCollection();
			string[] strArrays = new string[] { "DirectoryService_State", "DirectoryService_ServerIP", "DirectoryService_ServerPort", "DirectoryService_Root", "DirectoryService_Account", "DirectoryService_Pass" };
			stringCollections.AddRange(strArrays);
			List<ConfigItem> config = SysConfig.GetConfig(stringCollections);
			ConfigItem configItem = config.Find((ConfigItem c) => c.ItemCode == "DirectoryService_State");
			if (configItem != null)
			{
				lDAPConfig.State = configItem.ItemValue;
			}
			configItem = config.Find((ConfigItem c) => c.ItemCode == "DirectoryService_Root");
			if (configItem != null)
			{
				lDAPConfig.RootPath = configItem.ItemValue;
			}
			configItem = config.Find((ConfigItem c) => c.ItemCode == "DirectoryService_ServerIP");
			if (configItem != null)
			{
				lDAPConfig.ServerIP = configItem.ItemValue;
			}
			configItem = config.Find((ConfigItem c) => c.ItemCode == "DirectoryService_ServerPort");
			if (configItem != null)
			{
				lDAPConfig.ServerPort = configItem.ItemValue;
			}
			configItem = config.Find((ConfigItem c) => c.ItemCode == "DirectoryService_Account");
			if (configItem != null)
			{
				lDAPConfig.Account = configItem.ItemValue;
			}
			configItem = config.Find((ConfigItem c) => c.ItemCode == "DirectoryService_Pass");
			if (configItem != null)
			{
				lDAPConfig.PassWord = configItem.ItemValue;
			}
			return lDAPConfig;
		}

		public static void SetConfig(LDAPConfig config)
		{
			ConfigItem configItem = new ConfigItem()
			{
				ItemCode = "DirectoryService_ServerIP",
				ItemValue = config.ServerIP
			};
			SysConfig.SetConfig(configItem);
			ConfigItem configItem1 = new ConfigItem()
			{
				ItemCode = "DirectoryService_ServerPort",
				ItemValue = config.ServerPort
			};
			SysConfig.SetConfig(configItem1);
			ConfigItem configItem2 = new ConfigItem()
			{
				ItemCode = "DirectoryService_Root",
				ItemValue = config.RootPath
			};
			SysConfig.SetConfig(configItem2);
			ConfigItem configItem3 = new ConfigItem()
			{
				ItemCode = "DirectoryService_State",
				ItemValue = config.State
			};
			SysConfig.SetConfig(configItem3);
			ConfigItem configItem4 = new ConfigItem()
			{
				ItemCode = "DirectoryService_Account",
				ItemValue = config.Account
			};
			SysConfig.SetConfig(configItem4);
			ConfigItem configItem5 = new ConfigItem()
			{
				ItemCode = "DirectoryService_Pass",
				ItemValue = config.PassWord
			};
			SysConfig.SetConfig(configItem5);
			AppSettings.Instance.LdapConfig = config;
		}
	}
}