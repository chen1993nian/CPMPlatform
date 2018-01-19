using System;
using System.IO;
using System.Text;

namespace EIS.AppMail.MIME
{
	public class MimeCodeQP : MimeCode
	{
		public MimeCodeQP()
		{
		}

		public override byte[] DecodeToBytes(string string_1)
		{
			byte[] array;
			if (string_1 == null)
			{
				throw new ArgumentNullException();
			}
			MemoryStream memoryStream = new MemoryStream();
			StringReader stringReader = new StringReader(string_1);
			try
			{
				while (true)
				{
					string str = stringReader.ReadLine();
					string str1 = str;
					if (str == null)
					{
						break;
					}
					this.method_0(memoryStream, str1);
				}
				array = memoryStream.ToArray();
			}
			finally
			{
				memoryStream.Close();
				stringReader.Close();
				memoryStream = null;
				stringReader = null;
			}
			return array;
		}

		public override string EncodeFromBytes(byte[] inArray, int offset, int length)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException();
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < length; i++)
			{
				byte num = inArray[offset + i];
				if (((num < 33 || num > 126 || num == 61) && num != 9 && num != 32 && num != 13 ? num == 10 : true))
				{
					stringBuilder.Append(Convert.ToChar(num));
				}
				else
				{
					int num1 = num;
					stringBuilder.AppendFormat("={0}", num1.ToString("X2"));
				}
			}
			stringBuilder.Replace(" \r", "=20\r", 0, stringBuilder.Length);
			stringBuilder.Replace("\t\r", "=09\r", 0, stringBuilder.Length);
			stringBuilder.Replace("\r\n.\r\n", "\r\n=2E\r\n", 0, stringBuilder.Length);
			stringBuilder.Replace(" ", "=20", stringBuilder.Length - 1, 1);
			return this.method_2(stringBuilder.ToString());
		}

		private void method_0(MemoryStream memoryStream_0, string string_1)
		{
			byte num;
			if ((memoryStream_0 == null ? true : string_1 == null))
			{
				throw new ArgumentNullException();
			}
			int num1 = 0;
			int num2 = 0;
			while (num1 < string_1.Length)
			{
				if (string_1[num1] != '=')
				{
					num = Convert.ToByte(string_1[num1]);
				}
				else
				{
					if (num1 + 2 > string_1.Length)
					{
						break;
					}
					if ((!this.method_1(string_1[num1 + 1]) ? true : !this.method_1(string_1[num1 + 2])))
					{
						int num3 = num1 + 1;
						num1 = num3;
						num = Convert.ToByte(string_1[num3]);
					}
					else
					{
						string str = string_1.Substring(num1 + 1, 2);
						num = Convert.ToByte(str, 16);
						num1 = num1 + 2;
					}
				}
				memoryStream_0.WriteByte(num);
				num1++;
				num2++;
			}
			if (!string_1.EndsWith("="))
			{
				memoryStream_0.WriteByte(13);
				memoryStream_0.WriteByte(10);
			}
		}

		private bool method_1(char char_0)
		{
			bool flag;
			if ((char_0 < '0' || char_0 > '9') && (char_0 < 'A' || char_0 > 'F'))
			{
				flag = (char_0 < 'a' ? true : char_0 > 'f');
			}
			else
			{
				flag = false;
			}
			return (flag ? false : true);
		}

		private string method_2(string string_1)
		{
			string str;
			StringReader stringReader = new StringReader(string_1);
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				while (true)
				{
					string str1 = stringReader.ReadLine();
					string str2 = str1;
					if (str1 == null)
					{
						break;
					}
					int num = 75;
					int num1 = 0;
					while (num < str2.Length)
					{
						if ((!this.method_1(str2[num]) || !this.method_1(str2[num - 1]) ? false : str2[num - 2] == '='))
						{
							num = num - 2;
						}
						stringBuilder.Append(str2.Substring(num1, num - num1));
						stringBuilder.Append("=\r\n");
						num1 = num;
						num = num + 75;
					}
					stringBuilder.Append(str2.Substring(num1, str2.Length - num1));
					stringBuilder.Append("\r\n");
				}
				str = stringBuilder.ToString();
			}
			finally
			{
				stringReader.Close();
				stringReader = null;
				stringBuilder = null;
			}
			return str;
		}
	}
}