using System;
using System.Text;
using System.Text.RegularExpressions;

namespace EIS.AppMail.MIME
{
	public class MimeCode
	{
		private string string_0;

		public string Charset
		{
			get
			{
				return this.string_0;
			}
			set
			{
				if (value != null)
				{
					this.string_0 = Regex.Match(value, "([\\w|-]*)").Value;
				}
			}
		}

		public MimeCode()
		{
		}

		public virtual byte[] DecodeToBytes(string string_1)
		{
			byte[] bytes;
			if (string_1 == null)
			{
				throw new ArgumentNullException();
			}
			if (this.string_0 == null)
			{
				this.string_0 = Encoding.Default.BodyName;
				bytes = Encoding.Default.GetBytes(string_1);
			}
			else
			{
				bytes = Encoding.GetEncoding(this.string_0.ToLower()).GetBytes(string_1);
			}
			return bytes;
		}

		public virtual string DecodeToString(string string_1)
		{
			string str;
			byte[] bytes = this.DecodeToBytes(string_1);
			if (this.string_0 == null)
			{
				this.string_0 = Encoding.Default.BodyName;
				str = Encoding.Default.GetString(bytes);
			}
			else
			{
				str = Encoding.GetEncoding(this.string_0).GetString(bytes);
			}
			return str;
		}

		public virtual string EncodeFromBytes(byte[] inArray, int offset, int length)
		{
			string str;
			if (inArray == null)
			{
				throw new ArgumentNullException();
			}
			if (this.string_0 == null)
			{
				this.string_0 = Encoding.Default.BodyName;
				str = Encoding.Default.GetString(inArray, offset, length);
			}
			else
			{
				str = Encoding.GetEncoding(this.string_0).GetString(inArray, offset, length);
			}
			return str;
		}

		public string EncodeFromBytes(byte[] inArray)
		{
			return this.EncodeFromBytes(inArray, 0, (int)inArray.Length);
		}

		public virtual string EncodeFromString(string string_1)
		{
			byte[] bytes;
			if (string_1 == null)
			{
				throw new ArgumentNullException();
			}
			if (this.string_0 == null)
			{
				this.string_0 = Encoding.Default.BodyName;
				bytes = Encoding.Default.GetBytes(string_1);
			}
			else
			{
				bytes = Encoding.GetEncoding(this.string_0).GetBytes(string_1);
			}
			return this.EncodeFromBytes(bytes);
		}
	}
}