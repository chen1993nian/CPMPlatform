using System;
using System.Text;

namespace EIS.AppMail.MIME
{
	public class MimeField
	{
		private string string_0;

		private string string_1;

		private string string_2;

		public MimeField()
		{
		}

		public void Clear()
		{
			this.string_0 = "";
			this.string_1 = "";
			this.string_2 = "";
		}

		public string GetCharset()
		{
			return this.string_2;
		}

		public string GetName()
		{
			return this.string_0;
		}

		public string GetParameter(string pszAttr)
		{
			int length;
			string str;
			string[] strArrays = this.string_1.Split(new char[] { ';' });
			if (strArrays != null)
			{
				int num = 0;
				while (num < (int)strArrays.Length)
				{
					if (strArrays[num].IndexOf(pszAttr, 0) != -1)
					{
						int num1 = strArrays[num].IndexOf('=');
						if (num1 + 1 != strArrays[num].Length)
						{
							if (strArrays[num][num1 + 1] != '\"')
							{
								num1++;
								length = strArrays[num].IndexOf('\r', num1);
								if (length == -1)
								{
									length = strArrays[num].Length;
								}
							}
							else
							{
								num1 = num1 + 2;
								length = strArrays[num].IndexOf('\"', num1);
							}
							str = strArrays[num].Substring(num1, length - num1);
							return str;
						}
						else
						{
							str = null;
							return str;
						}
					}
					else
					{
						num++;
					}
				}
			}
			str = null;
			return str;
		}

		public string GetValue()
		{
			return this.string_1;
		}

		public void LoadField(string strData)
		{
			if (strData == null)
			{
				throw new ArgumentNullException();
			}
			int num = strData.IndexOf(':');
			if (num == -1)
			{
				this.string_0 = "";
			}
			else
			{
				this.string_0 = strData.Substring(0, num);
			}
			num++;
			this.string_1 = strData.Substring(num, strData.Length - num).Trim();
			MimeCode code = MimeCodeManager.Instance.GetCode(this.GetName());
			if (code != null)
			{
				this.string_1 = code.DecodeToString(this.string_1);
				this.string_2 = code.Charset;
			}
		}

		public void SetCharset(string pszCharset)
		{
			this.string_2 = pszCharset;
		}

		public void SetName(string pszName)
		{
			this.string_0 = pszName;
		}

		public void SetParameter(string pszAttr, string pszValue)
		{
			MimeField mimeField;
			string str;
			string str1 = string.Concat(pszAttr, "=", pszValue);
			string[] strArrays = this.string_1.Split(new char[] { ';' });
			bool flag = false;
			int num = 0;
			if (strArrays != null)
			{
				while (num < (int)strArrays.Length)
				{
					if (strArrays[num].IndexOf(pszAttr, 0) != -1)
					{
						flag = true;
						if (!flag)
						{
							mimeField = this;
							mimeField.string_1 = string.Concat(mimeField.string_1, ";", str1);
						}
						else
						{
							str = this.string_1.Replace(strArrays[num], str1);
						}
						return;
					}
					else
					{
						num++;
					}
				}
			}
			if (!flag)
			{
				mimeField = this;
				mimeField.string_1 = string.Concat(mimeField.string_1, ";", str1);
			}
			else
			{
				str = this.string_1.Replace(strArrays[num], str1);
			}
		}

		public void SetValue(string pszValue)
		{
			this.string_1 = pszValue;
		}

		public void Store(StringBuilder stringBuilder_0)
		{
			if (stringBuilder_0 == null)
			{
				throw new ArgumentNullException();
			}
			MimeCode code = MimeCodeManager.Instance.GetCode(this.GetName());
			if (code == null)
			{
				stringBuilder_0.AppendFormat("{0}: {1}\r\n", this.string_0, this.string_1);
			}
			else
			{
				code.Charset = this.string_2;
				stringBuilder_0.AppendFormat("{0}: {1}\r\n", this.string_0, code.EncodeFromString(this.string_1));
			}
		}
	}
}