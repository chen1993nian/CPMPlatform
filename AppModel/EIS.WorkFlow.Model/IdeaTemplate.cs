using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.WorkFlow.Model
{
	[Serializable]
	public class IdeaTemplate : AppModelBase
	{
		public string IdeaName
		{
			get;
			set;
		}

		public string IdeaUser
		{
			get;
			set;
		}

		public int OrderId
		{
			get;
			set;
		}

		public IdeaTemplate()
		{
		}
	}
}