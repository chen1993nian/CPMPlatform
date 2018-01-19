using System;
using System.Runtime.CompilerServices;

namespace EIS.AppBase
{
	public class DictItem
	{
		public string text
		{
			get;
			set;
		}

		public string @value
		{
			get;
			set;
		}

		public DictItem(string _text, string _value)
		{
			this.text = _text;
			this.@value = _value;
		}
	}
}