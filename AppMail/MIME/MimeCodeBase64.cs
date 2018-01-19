using System;
using System.Text;

namespace EIS.AppMail.MIME
{
	public class MimeCodeBase64 : MimeCode
	{
		public MimeCodeBase64()
		{
		}

		public override byte[] DecodeToBytes(string string_1)
		{
			if (string_1 == null)
			{
				throw new ArgumentNullException();
			}
			return Convert.FromBase64String(string_1);
		}

		public override string EncodeFromBytes(byte[] inArray, int offset, int length)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException();
			}
			return this.method_0(Convert.ToBase64String(inArray, offset, length));
		}

		private string method_0(string string_1)
		{
			if (string_1 == null)
			{
				throw new ArgumentNullException();
			}
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			while (num + 76 < string_1.Length)
			{
				stringBuilder.AppendFormat("{0}\r\n", string_1.Substring(num, 76));
				num = num + 76;
			}
			stringBuilder.AppendFormat("{0}", string_1.Substring(num, string_1.Length - num));
			return stringBuilder.ToString();
		}
	}
}