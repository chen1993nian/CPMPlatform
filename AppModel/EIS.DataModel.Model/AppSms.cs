using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class AppSms : AppModelBase
	{
		public string AppId
		{
			get;
			set;
		}

		public string AppName
		{
			get;
			set;
		}

		public string CompanyId
		{
			get;
			set;
		}

		public string Content
		{
			get;
			set;
		}

		public string RecIds
		{
			get;
			set;
		}

		public string RecNames
		{
			get;
			set;
		}

		public string RecPhone
		{
			get;
			set;
		}

		public string SenderId
		{
			get;
			set;
		}

		public string SenderName
		{
			get;
			set;
		}

		public DateTime? SendTime
		{
			get;
			set;
		}

		public string State
		{
			get;
			set;
		}

		public AppSms()
		{
		}
	}
}