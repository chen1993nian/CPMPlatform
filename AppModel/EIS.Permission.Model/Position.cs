using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.Permission.Model
{
	[Serializable]
	public class Position : AppModelBase
	{
		public string DeptID
		{
			get;
			set;
		}

		public string DeptName
		{
			get;
			set;
		}

		public int OrderID
		{
			get;
			set;
		}

		public string ParentPositionId
		{
			get;
			set;
		}

		public string ParentPositionName
		{
			get;
			set;
		}

		public string PositionCode
		{
			get;
			set;
		}

		public string PositionName
		{
			get;
			set;
		}

		public string PropId
		{
			get;
			set;
		}

		public string PropName
		{
			get;
			set;
		}

		public string RankCode
		{
			get;
			set;
		}

		public string RankName
		{
			get;
			set;
		}

		public Position()
		{
		}
	}
}