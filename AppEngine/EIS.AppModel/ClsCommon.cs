using System;

namespace EIS.AppModel
{
	public class ClsCommon
	{
		public ClsCommon()
		{
		}

		public static string AddZero(short numlen, short numini)
		{
			string str = "";
			for (short i = 1; i <= numlen - Convert.ToString(numini).Length; i = (short)(i + 1))
			{
				str = string.Concat(str, "0");
			}
			return string.Concat(str, Convert.ToString(numini));
		}

		public static string AddZero(string addstr, int zeronum)
		{
			for (int i = 0; i < zeronum; i++)
			{
				addstr = string.Concat("0", addstr);
			}
			return addstr;
		}

		public static string DbnullToString(object v)
		{
			string str;
			str = (!(v.GetType().ToString() == "System.DBNull") ? v.ToString() : "");
			return str;
		}

		public static string GetTodayStr(string formatstr)
		{
			string[] strArrays;
			string str = "";
			string str1 = "";
			string str2 = "";
			string str3 = "";
			string str4 = "";
			string str5 = "";
			string str6 = "";
			int year = DateTime.Today.Year;
			str1 = year.ToString();
			year = DateTime.Today.Month;
			str2 = year.ToString();
			year = DateTime.Today.Day;
			str3 = year.ToString();
			year = DateTime.Now.Hour;
			str4 = year.ToString();
			year = DateTime.Now.Minute;
			str5 = year.ToString();
			year = DateTime.Now.Second;
			str6 = year.ToString();
			if (str4.Length == 1)
			{
				str4 = ClsCommon.AddZero(str4, 1);
			}
			if (str5.Length == 1)
			{
				str5 = ClsCommon.AddZero(str5, 1);
			}
			if (str6.Length == 1)
			{
				str6 = ClsCommon.AddZero(str6, 1);
			}
			string str7 = formatstr;
			if (str7 != null)
			{
				switch (str7)
				{
					case "yyyy":
					{
						str = str1;
						break;
					}
					case "yy":
					{
						str = str1.Substring(2, 2);
						break;
					}
					case "mm":
					{
						str = str2;
						break;
					}
					case "m":
					{
						str = str2;
						break;
					}
					case "dd":
					{
						str = str3;
						break;
					}
					case "d":
					{
						str = str3;
						break;
					}
					case "hh:mm:ss":
					{
						strArrays = new string[] { " ", str4, ":", str5, ":", str6 };
						str = string.Concat(strArrays);
						break;
					}
					case "hh时mm分ss":
					{
						strArrays = new string[] { " ", str4, "时", str5, "分", str6, "秒" };
						str = string.Concat(strArrays);
						break;
					}
				}
			}
			return str;
		}

		internal enum ControlType
		{
			TextBoxInChar,
			TextBoxInCharRead,
			TextBoxInDate,
			TextBoxInDateRead,
			TextBoxInOutPage,
			TextBoxInOutPageRead,
			TextBoxInArea,
			TextBoxInAreaRead,
			WebEditor,
			WebEditorRead,
			DropDownIn,
			DropDownInMulti,
			TextBoxFile,
			Radio,
			CheckBox,
			Btn
		}
	}
}