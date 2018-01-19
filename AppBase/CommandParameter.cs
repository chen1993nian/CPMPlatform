using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace EIS.AppBase
{
	[Serializable]
	public class CommandParameter
	{
		public System.Data.DbType DbType
		{
			get;
			set;
		}

		public ParameterDirection Direction
		{
			get;
			set;
		}

		public bool IsNullable
		{
			get;
			set;
		}

		public string ParameterName
		{
			get;
			set;
		}

		public int Size
		{
			get;
			set;
		}

		public string SourceColumn
		{
			get;
			set;
		}

		public bool SourceColumnNullMapping
		{
			get;
			set;
		}

		public DataRowVersion SourceVersion
		{
			get;
			set;
		}

		public object Value
		{
			get;
			set;
		}

		public CommandParameter()
		{
		}
	}
}