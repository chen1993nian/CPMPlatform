using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class Dict : AppModelBase
	{
		public string DictCat
		{
			get;
			set;
		}

		public string DictCode
		{
			get;
			set;
		}

		public string DictName
		{
			get;
			set;
		}

		public Dict()
		{
		}
	}
}