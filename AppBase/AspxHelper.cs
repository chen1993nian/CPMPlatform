using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Security;

namespace EIS.AppBase
{
	public class AspxHelper
	{
		public static bool IsAuthenticated
		{
			get
			{
				bool isAuthenticated;
				HttpContext current = HttpContext.Current;
				if (!current.User.Identity.IsAuthenticated)
				{
					HttpCookie item = current.Request.Cookies[FormsAuthentication.FormsCookieName];
					if (item == null)
					{
						isAuthenticated = false;
					}
					else
					{
						FormsAuthenticationTicket formsAuthenticationTicket = FormsAuthentication.Decrypt(item.Value);
						isAuthenticated = (new FormsIdentity(formsAuthenticationTicket)).IsAuthenticated;
					}
				}
				else
				{
					isAuthenticated = true;
				}
				return isAuthenticated;
			}
		}

		public static string LoginUserAccount
		{
			get
			{
				string name;
				HttpContext current = HttpContext.Current;
				if (!current.User.Identity.IsAuthenticated)
				{
					HttpCookie item = current.Request.Cookies[FormsAuthentication.FormsCookieName];
					if (item != null)
					{
						try
						{
							name = FormsAuthentication.Decrypt(item.Value).Name;
							return name;
						}
						catch
						{
						}
					}
					name = string.Empty;
				}
				else
				{
					name = current.User.Identity.Name;
				}
				return name;
			}
		}

		public AspxHelper()
		{
		}

		public static string CombinCond(string[] conds)
		{
			string str;
			string empty = string.Empty;
			if ((int)conds.Length != 0)
			{
				string[] strArrays = conds;
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					string str1 = strArrays[i];
					if (!string.IsNullOrEmpty(str1))
					{
						if (!string.IsNullOrEmpty(empty))
						{
							string[] strArrays1 = new string[] { "(", empty, ") AND (", str1, ")" };
							empty = string.Concat(strArrays1);
						}
						else
						{
							empty = str1;
						}
					}
				}
				str = empty;
			}
			else
			{
				str = empty;
			}
			return str;
		}

		public static string DateToString(DateTime date)
		{
			string str;
			if (!(date == DateTime.MinValue))
			{
				str = date.ToString("yyyy-MM-dd");
			}
			else
			{
				str = null;
			}
			return str;
		}

		public static string DateToStringL(DateTime date)
		{
			string str;
			if (!(date == DateTime.MinValue))
			{
				str = date.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo(1033));
			}
			else
			{
				str = null;
			}
			return str;
		}

		public static string EncodeJsString(string s)
		{
			string str;
			if (!string.IsNullOrEmpty(s))
			{
				StringBuilder stringBuilder = new StringBuilder();
				string str1 = s;
				for (int i = 0; i < str1.Length; i++)
				{
					char chr = str1[i];
					char chr1 = chr;
					switch (chr1)
					{
						case '\b':
						{
							stringBuilder.Append("\\b");
							break;
						}
						case '\t':
						{
							stringBuilder.Append("\\t");
							break;
						}
						case '\n':
						{
							stringBuilder.Append("\\n");
							break;
						}
						case '\v':
						{
							int num = chr;
							if ((num < 32 ? false : num <= 127))
							{
								stringBuilder.Append(chr);
							}
							else
							{
								stringBuilder.AppendFormat("\\u{0:X04}", num);
							}
							break;
						}
						case '\f':
						{
							stringBuilder.Append("\\f");
							break;
						}
						case '\r':
						{
							stringBuilder.Append("\\r");
							break;
						}
						default:
						{
							if (chr1 == '\"')
							{
								stringBuilder.Append("\\\"");
								break;
							}
							else if (chr1 == '\\')
							{
								stringBuilder.Append("\\\\");
								break;
							}
							else
							{
								goto case '\v';
							}
						}
					}
				}
				str = stringBuilder.ToString();
			}
			else
			{
				str = s;
			}
			return str;
		}

		public static string HtmlEncode(string s)
		{
			string str;
			if (!string.IsNullOrEmpty(s))
			{
				s = HttpUtility.HtmlEncode(s);
				s = s.Replace("\n", "<br/>");
				str = s;
			}
			else
			{
				str = s;
			}
			return str;
		}

		public static bool IsNumber(string strVar)
		{
			bool flag;
			int length = strVar.Length;
			int num = 0;
			while (true)
			{
				if (num < length)
				{
					char chr = strVar[num];
					if ((chr < '0' ? false : chr <= '9'))
					{
						num++;
					}
					else
					{
						flag = false;
						break;
					}
				}
				else
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		public static void SetAuthCookie(string account)
		{
			FormsAuthentication.SetAuthCookie(account, false);
		}

		public static void SignOut()
		{
			FormsAuthentication.SignOut();
		}

		public static int[] StringToIntArray(string str)
		{
			int num;
			string[] strArrays = str.Split(new char[] { ',' });
			List<int> nums = new List<int>();
			string[] strArrays1 = strArrays;
			for (int i = 0; i < (int)strArrays1.Length; i++)
			{
				string str1 = strArrays1[i];
				if (!string.IsNullOrEmpty(str1))
				{
					if (int.TryParse(str1, out num))
					{
						nums.Add(num);
					}
				}
			}
			return nums.ToArray();
		}
	}
}