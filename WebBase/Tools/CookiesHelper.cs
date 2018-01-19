using System;
using System.Collections.Specialized;
using System.Web;

namespace WebBase.JZY.Tools
{
	public class CookiesHelper
	{
	

		public static void AddCookie(string string_0, string value)
		{
			CookiesHelper.AddCookie(new HttpCookie(string_0, value));
		}

		public static void AddCookie(string string_0, string value, DateTime expires)
		{
			HttpCookie httpCookie = new HttpCookie(string_0, value)
			{
				Expires = expires
			};
			CookiesHelper.AddCookie(httpCookie);
		}

		public static void AddCookie(string cookieName, string string_0, string value)
		{
			HttpCookie httpCookie = new HttpCookie(cookieName);
			httpCookie.Values.Add(string_0, value);
			CookiesHelper.AddCookie(httpCookie);
		}

		public static void AddCookie(string cookieName, DateTime expires)
		{
			CookiesHelper.AddCookie(new HttpCookie(cookieName)
			{
				Expires = expires
			});
		}

		public static void AddCookie(string cookieName, string string_0, string value, DateTime expires)
		{
			HttpCookie httpCookie = new HttpCookie(cookieName)
			{
				Expires = expires
			};
			httpCookie.Values.Add(string_0, value);
			CookiesHelper.AddCookie(httpCookie);
		}

		public static void AddCookie(HttpCookie cookie)
		{
			HttpResponse response = HttpContext.Current.Response;
			if (response != null)
			{
				cookie.HttpOnly = true;
				cookie.Path = "/";
				response.AppendCookie(cookie);
			}
		}

		public static HttpCookie GetCookie(string cookieName)
		{
			HttpCookie item;
			HttpRequest request = HttpContext.Current.Request;
			if (request == null)
			{
				item = null;
			}
			else
			{
				item = request.Cookies[cookieName];
			}
			return item;
		}

		public static string GetCookieValue(string cookieName)
		{
			return CookiesHelper.GetCookieValue(cookieName, null);
		}

		public static string GetCookieValue(string cookieName, string string_0)
		{
			string str;
			HttpRequest request = HttpContext.Current.Request;
			str = (request == null ? "" : CookiesHelper.GetCookieValue(request.Cookies[cookieName], string_0));
			return str;
		}

		public static string GetCookieValue(HttpCookie cookie, string string_0)
		{
			string str;
			if (cookie == null)
			{
				str = "";
			}
			else
			{
				str = ((string.IsNullOrEmpty(string_0) ? true : !cookie.HasKeys) ? cookie.Value : cookie.Values[string_0]);
			}
			return str;
		}

		public static void RemoveCookie(string cookieName)
		{
			CookiesHelper.RemoveCookie(cookieName, null);
		}

		public static void RemoveCookie(string cookieName, string string_0)
		{
			HttpResponse response = HttpContext.Current.Response;
			if (response != null)
			{
				HttpCookie item = response.Cookies[cookieName];
				if (item != null)
				{
					if ((string.IsNullOrEmpty(string_0) ? true : !item.HasKeys))
					{
						response.Cookies.Remove(cookieName);
					}
					else
					{
						item.Values.Remove(string_0);
					}
				}
			}
		}

		public static void SetCookie(string cookieName, string string_0, string value)
		{
			CookiesHelper.SetCookie(cookieName, string_0, value, null);
		}

		public static void SetCookie(string string_0, string value)
		{
			CookiesHelper.SetCookie(string_0, null, value, null);
		}

		public static void SetCookie(string string_0, string value, DateTime expires)
		{
			CookiesHelper.SetCookie(string_0, null, value, new DateTime?(expires));
		}

		public static void SetCookie(string cookieName, DateTime expires)
		{
			CookiesHelper.SetCookie(cookieName, null, null, new DateTime?(expires));
		}

		public static void SetCookie(string cookieName, string string_0, string value, DateTime? expires)
		{
			HttpResponse response = HttpContext.Current.Response;
			if (response != null)
			{
				HttpCookie item = response.Cookies[cookieName];
				if (item != null)
				{
					if (!(string.IsNullOrEmpty(string_0) ? true : !item.HasKeys))
					{
						item.Values.Set(string_0, value);
					}
					else if (!string.IsNullOrEmpty(value))
					{
						item.Value = value;
					}
					if (expires.HasValue)
					{
						item.Expires = expires.Value;
					}
					response.SetCookie(item);
				}
			}
		}
	}
}