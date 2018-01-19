using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;

namespace EIS.AppBase
{
	[Serializable]
	public class SmartCommand
	{
		public string CommandText
		{
			get;
			set;
		}

		public System.Data.CommandType CommandType
		{
			get;
			set;
		}

		public List<CommandParameter> Parameters
		{
			get;
			set;
		}

		public SmartCommand()
		{
			this.CommandText = "";
			this.CommandType = System.Data.CommandType.Text;
			this.Parameters = new List<CommandParameter>();
		}
	}
}