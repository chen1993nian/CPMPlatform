using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class AppChart : AppModelBase
	{
		public string ChartHeight
		{
			get;
			set;
		}

		public string ChartTitle
		{
			get;
			set;
		}

		public string ChartType
		{
			get;
			set;
		}

		public string ChartWidth
		{
			get;
			set;
		}

		public string QueryCode
		{
			get;
			set;
		}

		public string ShowValue
		{
			get;
			set;
		}

		public string xAxisName
		{
			get;
			set;
		}

		public string xAxisTitle
		{
			get;
			set;
		}

		public string yAxisName
		{
			get;
			set;
		}

		public string yAxisTitle
		{
			get;
			set;
		}

		public AppChart()
		{
		}

		public AppChart(UserContext user)
		{
			if (string.IsNullOrEmpty(base._AutoID))
			{
				base._AutoID = Guid.NewGuid().ToString();
			}
			base._UserName = user.EmployeeId;
			base._OrgCode = user.DeptWbs;
			base._IsDel = 0;
			base._CreateTime = DateTime.Now;
			base._UpdateTime = DateTime.Now;
		}
	}
}