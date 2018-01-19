using System;
using System.Text;

namespace EIS.AppMail.MIME
{
	public class MimeFieldCodeBase : MimeCode
	{
		public MimeFieldCodeBase()
		{
		}

		public override string DecodeToString(string string_1)
		{
			MimeCode code;
			string str = "";
			int num = 0;
			while (true)
			{
				if (num < string_1.Length)
				{
					int num1 = string_1.IndexOf("=?", num);
					if (num1 == -1)
					{
						str = string.Concat(str, string_1.Substring(num, string_1.Length - num));
						break;
					}
					else
					{
						str = string.Concat(str, string_1.Substring(num, num1 - num));
						int num2 = string_1.IndexOf("?=", num1 + 13);
						if (num2 == -1)
						{
							str = string.Concat(str, string_1.Substring(num1, string_1.Length - num1));
							break;
						}
						else
						{
							num1 = num1 + 2;
							int num3 = string_1.IndexOf('?', num1);
							if ((num3 == -1 ? true : string_1[num3 + 2] != '?'))
							{
								str = string.Concat(str, string_1.Substring(num3, num2 - num3));
							}
							else
							{
								base.Charset = string_1.Substring(num1, num3 - num1);
								string str1 = string_1.Substring(num3 + 3, num2 - num3 - 3);
								if (string_1[num3 + 1] == 'Q')
								{
									code = MimeCodeManager.Instance.GetCode("quoted-printable");
									code.Charset = base.Charset;
									str = string.Concat(str, code.DecodeToString(str1));
								}
								else if (string_1[num3 + 1] != 'B')
								{
									str = string.Concat(str, str1);
								}
								else
								{
									code = MimeCodeManager.Instance.GetCode("base64");
									code.Charset = base.Charset;
									str = string.Concat(str, code.DecodeToString(str1));
								}
							}
							num = num2 + 2;
						}
					}
				}
				else
				{
					break;
				}
			}
			return str;
		}

		protected void EncodeDelimeter(string string_1, StringBuilder stringBuilder_0)
		{
			MimeCode code;
			string[] strArrays = string_1.Split(this.GetDelimeterChars());
			int length = 0;
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				length = length + strArrays[i].Length;
				if (strArrays[i] != null)
				{
					if (base.Charset == null)
					{
						base.Charset = Encoding.Default.BodyName;
					}
					string lower = this.method_0(strArrays[i]).ToLower();
					if (lower != null)
					{
						if (lower == "non")
						{
							stringBuilder_0.Append(strArrays[i]);
						}
						else if (lower == "quoted-printable")
						{
							code = MimeCodeManager.Instance.GetCode("quoted-printable");
							code.Charset = base.Charset;
							stringBuilder_0.AppendFormat("=?{0}?Q?{1}?=", base.Charset, code.EncodeFromString(strArrays[i]));
						}
						else if (lower == "base64")
						{
							code = MimeCodeManager.Instance.GetCode("base64");
							code.Charset = base.Charset;
							stringBuilder_0.AppendFormat("=?{0}?B?{1}?=", base.Charset, code.EncodeFromString(strArrays[i]));
						}
					}
				}
				if (length < string_1.Length)
				{
					stringBuilder_0.Append(string_1.Substring(length, 1));
				}
				length++;
			}
		}

		public override string EncodeFromString(string string_1)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!this.IsNeedDelimeter())
			{
				this.EncodeNoDelimeter(string_1, stringBuilder);
			}
			else
			{
				this.EncodeDelimeter(string_1, stringBuilder);
			}
			if (this.IsAutoFold())
			{
				char[] foldChars = this.GetFoldChars();
				for (int i = 0; i < (int)foldChars.Length; i++)
				{
					string str = foldChars[i].ToString();
					string str1 = string.Concat(str, "\r\n\t");
					stringBuilder.Replace(str, str1);
				}
			}
			return stringBuilder.ToString();
		}

		protected void EncodeNoDelimeter(string string_1, StringBuilder stringBuilder_0)
		{
			MimeCode code;
			if (base.Charset == null)
			{
				base.Charset = Encoding.Default.BodyName;
			}
			string lower = this.method_0(string_1).ToLower();
			if (lower != null)
			{
				if (lower == "non")
				{
					stringBuilder_0.Append(string_1);
				}
				else if (lower == "quoted-printable")
				{
					code = MimeCodeManager.Instance.GetCode("quoted-printable");
					code.Charset = base.Charset;
					stringBuilder_0.AppendFormat("=?{0}?Q?{1}?=", base.Charset, code.EncodeFromString(string_1));
				}
				else if (lower == "base64")
				{
					code = MimeCodeManager.Instance.GetCode("base64");
					code.Charset = base.Charset;
					stringBuilder_0.AppendFormat("=?{0}?B?{1}?=", base.Charset, code.EncodeFromString(string_1));
				}
			}
		}

		protected virtual char[] GetDelimeterChars()
		{
			return null;
		}

		protected virtual char[] GetFoldChars()
		{
			return null;
		}

		protected virtual bool IsAutoFold()
		{
			return false;
		}

		protected virtual bool IsNeedDelimeter()
		{
			return false;
		}

		private string method_0(string string_1)
		{
			string str;
			int num = 0;
			for (int i = 0; i < string_1.Length; i++)
			{
				if (this.method_1(string_1[i]))
				{
					num++;
				}
			}
			if (num != 0)
			{
				str = (string_1.Length + num * 2 <= (string_1.Length + 2) / 3 * 4 || num * 5 <= string_1.Length ? "quoted-printable" : "base64");
			}
			else
			{
				str = "non";
			}
			return str;
		}

		private bool method_1(char char_0)
		{
			return char_0 > 'ÿ';
		}
	}
}