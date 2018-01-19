using System;
using System.Runtime.CompilerServices;

namespace EIS.AppBase.Config
{
	public class ConfigItem
	{
		public string ItemCode
		{
			get;
			set;
		}

		public string ItemName
		{
			get;
			set;
		}

		public string ItemValue
		{
			get;
			set;
		}

		public ConfigItem()
		{
			this.ItemCode = "";
			this.ItemValue = "";
			this.ItemName = "";
		}
	}
}