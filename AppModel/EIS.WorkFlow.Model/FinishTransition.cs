using System;
using System.Runtime.CompilerServices;

namespace EIS.WorkFlow.Model
{
	[Serializable]
	public class FinishTransition
	{
		public string _AutoID
		{
			get;
			set;
		}

		public DateTime _CreateTime
		{
			get;
			set;
		}

		public int _IsDel
		{
			get;
			set;
		}

		public string _OrgCode
		{
			get;
			set;
		}

		public DateTime _UpdateTime
		{
			get;
			set;
		}

		public string _UserName
		{
			get;
			set;
		}

		public string FromActivity
		{
			get;
			set;
		}

		public string InstanceId
		{
			get;
			set;
		}

		public string ToActivity
		{
			get;
			set;
		}

		public string TransitionId
		{
			get;
			set;
		}

		public FinishTransition()
		{
		}
	}
}