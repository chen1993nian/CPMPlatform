using EIS.AppBase;
using EIS.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.AppBase.Config
{
	public class SysConfig
	{
		private static Hashtable itemHash;

		static SysConfig()
		{
			SysConfig.itemHash = new Hashtable(40);
		}

		public SysConfig()
		{
		}

		public static void Clear()
		{
			SysConfig.itemHash.Clear();
		}

		public static List<ConfigItem> GetConfig(IList keys)
		{
			string str = string.Concat("Select * from T_E_Sys_Config where ItemCode in (", Utility.GetSplitQuoteString(keys), ")");
			DataTable dataTable = SysDatabase.ExecuteTable(str);
			List<ConfigItem> configItems = new List<ConfigItem>();
			foreach (DataRow row in dataTable.Rows)
			{
				ConfigItem configItem = new ConfigItem()
				{
					ItemCode = row["ItemCode"].ToString(),
					ItemName = row["ItemName"].ToString(),
					ItemValue = row["ItemValue"].ToString()
				};
				configItems.Add(configItem);
			}
			return configItems;
		}

		public static ConfigItem GetConfig(string key)
		{
			return SysConfig.GetConfig(key, true);
		}

		public static ConfigItem GetConfig(string key, bool cacheFirst)
		{
			ConfigItem item;
			if (cacheFirst)
			{
				if (SysConfig.itemHash.ContainsKey(key))
				{
					item = (ConfigItem)SysConfig.itemHash[key];
					return item;
				}
			}
			string str = string.Concat("Select * from T_E_Sys_Config where ItemCode ='", key, "'");
			DataTable dataTable = SysDatabase.ExecuteTable(str);
			ConfigItem configItem = new ConfigItem();
			if (dataTable.Rows.Count > 0)
			{
				configItem.ItemCode = dataTable.Rows[0]["ItemCode"].ToString();
				configItem.ItemName = dataTable.Rows[0]["ItemName"].ToString();
				configItem.ItemValue = dataTable.Rows[0]["ItemValue"].ToString();
			}
			SysConfig.itemHash[key] = configItem;
			item = configItem;
			return item;
		}

        public static Boolean VerifyToken(string Token)
        {
            //为了安全起见，每个下载源代码的朋友，必须修改一下自己的Token
            //建议定期修改,或者分开发Token,测试Token,正式Token。
            string str = @"Select * from T_E_Sys_TokenConfig 
                where Token = @Token
                and DATEDIFF(second , startDate , GETDATE())>0
                and DATEDIFF(second  , GETDATE() , EndDate )>0";
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
            SysDatabase.AddInParameter(sqlStringCommand, "@Token", DbType.String, Token);
            DataSet ds = SysDatabase.ExecuteDataSet(sqlStringCommand);
            if ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0)) return (true);
            return (false);
        }

		public static bool IsExsits(string key)
		{
			string str = string.Concat("Select count(*) from T_E_Sys_Config where ItemCode ='", key, "'");
			object obj = SysDatabase.ExecuteScalar(str);
			return Convert.ToInt32(obj) > 0;
		}

		public static void SetConfig(ConfigItem config)
		{
			DbCommand sqlStringCommand;
			if (!SysConfig.IsExsits(config.ItemCode))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Insert T_E_Sys_Config (\r\n \t\t\t\t\t    _AutoID,\r\n\t\t\t\t\t    _UserName,\r\n\t\t\t\t\t    _OrgCode,\r\n\t\t\t\t\t    _CreateTime,\r\n\t\t\t\t\t    _UpdateTime,\r\n\t\t\t\t\t    _IsDel,\r\n\t\t\t\t\t    ItemCode,\r\n\t\t\t\t\t    ItemValue\r\n\t\t\t    ) values(\r\n\t\t\t\t\t    @_AutoID,\r\n\t\t\t\t\t    @_UserName,\r\n\t\t\t\t\t    @_OrgCode,\r\n\t\t\t\t\t    @_CreateTime,\r\n\t\t\t\t\t    @_UpdateTime,\r\n\t\t\t\t\t    @_IsDel,\r\n\t\t\t\t\t    @ItemCode,\r\n\t\t\t\t\t    @ItemValue\r\n\t\t\t    )");
				sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
				Guid guid = Guid.NewGuid();
				SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, guid.ToString());
				SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, "");
				SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, "");
				SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, DateTime.Now);
				SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
				SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, 0);
				SysDatabase.AddInParameter(sqlStringCommand, "@ItemCode", DbType.String, config.ItemCode);
				SysDatabase.AddInParameter(sqlStringCommand, "@ItemValue", DbType.String, config.ItemValue);
				SysDatabase.ExecuteNonQuery(sqlStringCommand);
			}
			else
			{
				sqlStringCommand = SysDatabase.GetSqlStringCommand("update T_E_Sys_Config set ItemValue=@ItemValue where ItemCode=@ItemCode");
				SysDatabase.AddInParameter(sqlStringCommand, "@ItemCode", DbType.String, config.ItemCode);
				SysDatabase.AddInParameter(sqlStringCommand, "@ItemValue", DbType.String, config.ItemValue);
				SysDatabase.ExecuteNonQuery(sqlStringCommand);
			}
			SysConfig.itemHash[config.ItemCode] = config;
			if (config.ItemCode == "EmployeeDefaultPass")
			{
				AppSettings.Instance.EmployeeDefaultPass = config.ItemValue;
			}
			if (config.ItemCode == "AdminDefaultPass")
			{
				AppSettings.Instance.AdminDefaultPass = config.ItemValue;
			}
		}
	}
}