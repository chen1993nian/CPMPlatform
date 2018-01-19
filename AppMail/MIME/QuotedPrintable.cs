using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace EIS.AppMail.MIME
{
	public class QuotedPrintable
	{
		public const int RFC_1521_MAX_CHARS_PER_LINE = 75;

		public static string Decode(string encoded)
		{
			string str;
			if (encoded == null)
			{
				throw new ArgumentNullException();
			}
			StringWriter stringWriter = new StringWriter();
			StringReader stringReader = new StringReader(encoded);
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
					if (!str2.EndsWith("="))
					{
						stringWriter.WriteLine(QuotedPrintable.smethod_1(str2));
					}
					else
					{
						stringWriter.Write(QuotedPrintable.smethod_1(str2.Substring(0, str2.Length - 1)));
					}
					stringWriter.Flush();
				}
				str = stringWriter.ToString();
			}
			finally
			{
				stringWriter.Close();
				stringReader.Close();
				stringWriter = null;
				stringReader = null;
			}
			return str;
		}

		public static string DecodeFile(string filepath)
		{
			string str;
			if (filepath == null)
			{
				throw new ArgumentNullException();
			}
			string str1 = "";
			FileInfo fileInfo = new FileInfo(filepath);
			if (!fileInfo.Exists)
			{
				throw new FileNotFoundException();
			}
			StreamReader streamReader = fileInfo.OpenText();
			try
			{
				while (true)
				{
					string str2 = streamReader.ReadLine();
					string str3 = str2;
					if (str2 == null)
					{
						break;
					}
					str1 = string.Concat(str1, QuotedPrintable.Decode(str3));
				}
				str = str1;
			}
			finally
			{
				streamReader.Close();
				streamReader = null;
				fileInfo = null;
			}
			return str;
		}

		public static string Encode(string toencode)
		{
			return QuotedPrintable.Encode(toencode, 75);
		}

		public static string Encode(string toencode, int charsperline)
		{
			string str;
			if (toencode == null)
			{
				throw new ArgumentNullException();
			}
			if (charsperline <= 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			string str1 = "";
			StringReader stringReader = new StringReader(toencode);
			try
			{
				while (true)
				{
					string str2 = stringReader.ReadLine();
					string str3 = str2;
					if (str2 == null)
					{
						break;
					}
					str1 = string.Concat(str1, QuotedPrintable.EncodeSmallLine(str3));
				}
				str = QuotedPrintable.FormatEncodedString(str1, charsperline);
			}
			finally
			{
				stringReader.Close();
				stringReader = null;
			}
			return str;
		}

		public static string EncodeFile(string filepath)
		{
			return QuotedPrintable.EncodeFile(filepath, 75);
		}

		public static string EncodeFile(string filepath, int charsperline)
		{
			string str;
			if (filepath == null)
			{
				throw new ArgumentNullException();
			}
			string str1 = "";
			FileInfo fileInfo = new FileInfo(filepath);
			if (!fileInfo.Exists)
			{
				throw new FileNotFoundException();
			}
			StreamReader streamReader = fileInfo.OpenText();
			try
			{
				while (true)
				{
					string str2 = streamReader.ReadLine();
					string str3 = str2;
					if (str2 == null)
					{
						break;
					}
					str1 = string.Concat(str1, QuotedPrintable.EncodeSmallLine(str3));
				}
				str = QuotedPrintable.FormatEncodedString(str1, charsperline);
			}
			finally
			{
				streamReader.Close();
				streamReader = null;
				fileInfo = null;
			}
			return str;
		}

		public static unsafe string EncodeSmall(string string_0)
		{
            //if (string_0 == null)
            //{
            //    throw new ArgumentNullException();
            //}
            //string str = "";
            //fixed (string st = string_0)
            //{
            //    string* offsetToStringData = &st;
            //    if (offsetToStringData != null)
            //    {
            //        offsetToStringData = offsetToStringData + RuntimeHelpers.OffsetToStringData;
            //    }
            //    char* chrPointer = (char*)offsetToStringData;
            //    do
            //    {
            //        int num = (ushort)(*chrPointer);
            //        str = string.Concat(str, string.Format("={0}", num.ToString("X2")));
            //        chrPointer = chrPointer + 2;
            //    }
            //    while ((ushort)(*chrPointer) != 0);
            //}
            //return str;
            return "2222"; 
		}

		public static string EncodeSmallLine(string string_0)
		{
			if (string_0 == null)
			{
				throw new ArgumentNullException();
			}
			return QuotedPrintable.EncodeSmall(string.Concat(string_0, "\r\n"));
		}

		public static unsafe string FormatEncodedString(string qpstr, int maxcharlen)
		{
            //string str;
            //if (qpstr == null)
            //{
            //    throw new ArgumentNullException();
            //}
            //string str1 = "";
            //StringWriter stringWriter = new StringWriter();
            //try
            //{
            //    try
            //    {
            //        fixed (string str2 = qpstr)
            //        {
            //            string* offsetToStringData = &str2;
            //            if (offsetToStringData != null)
            //            {
            //                offsetToStringData = offsetToStringData + RuntimeHelpers.OffsetToStringData;
            //            }
            //            char* chrPointer = (char*)offsetToStringData;
            //            int num = 0;
            //            do
            //            {
            //                str1 = string.Concat(str1, (*chrPointer).ToString());
            //                num++;
            //                if (num == maxcharlen)
            //                {
            //                    stringWriter.WriteLine("{0}=", str1);
            //                    stringWriter.Flush();
            //                    num = 0;
            //                    str1 = "";
            //                }
            //                chrPointer = chrPointer + 2;
            //            }
            //            while ((ushort)(*chrPointer) != 0);
            //        }
            //    }
            //    finally
            //    {
            //        //str2 = null;
            //    }
            //    stringWriter.WriteLine(str1);
            //    stringWriter.Flush();
            //    str = stringWriter.ToString();
            //}
            //finally
            //{
            //    stringWriter.Close();
            //    stringWriter = null;
            //}
			//return str;
            return "";
		}

		private static string smethod_0(Match match_0)
		{
			string value = match_0.Groups[2].Value;
			return ((char)Convert.ToInt32(value, 16)).ToString();
		}

		private static string smethod_1(string string_0)
		{
			if (string_0 == null)
			{
				throw new ArgumentNullException();
			}
			Regex regex = new Regex("(\\=([0-9A-F][0-9A-F]))", RegexOptions.IgnoreCase);
			return regex.Replace(string_0, new MatchEvaluator(QuotedPrintable.smethod_0));
		}
	}
}