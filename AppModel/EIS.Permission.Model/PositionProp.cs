using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.Permission.Model
{
	[Serializable]
	public class PositionProp : AppModelBase
	{
		public int OrderID
		{
			get;
			set;
		}

		public string PropName
		{
			get;
			set;
		}

		public string SearchScope
		{
			get;
			set;
		}

		public PositionProp()
		{
		}
	}
}