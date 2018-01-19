using System;
using System.Runtime.CompilerServices;

namespace EIS.AppModel
{
	public class DataControl
	{
		public string BizName
		{
			get;
			set;
		}

		public bool? CanRead
		{
			get;
			set;
		}

		public bool CanSee
		{
			get;
			set;
		}

		public bool? CanWrite
		{
			get;
			set;
		}

		private string DataType
		{
			get;
			set;
		}

		public string DefaultType
		{
			get;
			set;
		}

		public string DefaultValue
		{
			get;
			set;
		}

		public string FieldName
		{
			get;
			set;
		}

		public bool? NotNull
		{
			get;
			set;
		}

		public DataControl()
		{
		}

		public DataControl Clone()
		{
			DataControl dataControl = new DataControl()
			{
				BizName = this.BizName,
				FieldName = this.FieldName,
				DefaultType = this.DefaultType,
				DefaultValue = this.DefaultValue,
				DataType = this.DataType,
				CanWrite = this.CanWrite,
				CanRead = this.CanRead,
				CanSee = this.CanSee,
				NotNull = this.NotNull
			};
			return dataControl;
		}
	}
}