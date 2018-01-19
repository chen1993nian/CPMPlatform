using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Xml;
using EIS.AppBase;

namespace EIS.AppBase
{
	public class Utility
	{
		public Utility()
		{
		}

		public static void AddListFromDict(ListItemCollection list, string dictname, string selvalue)
		{
			list.Clear();
		}

		public static void AddShuiYinPic(string Path, string Path_syp, string Path_sypf)
		{
			System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
			System.Drawing.Image image1 = System.Drawing.Image.FromFile(Path_sypf);
			Graphics graphic = Graphics.FromImage(image);
			graphic.DrawImage(image1, new Rectangle(image.Width - image1.Width, image.Height - image1.Height, image1.Width, image1.Height), 0, 0, image1.Width, image1.Height, GraphicsUnit.Pixel);
			graphic.Dispose();
			image.Save(Path_syp);
			image.Dispose();
		}

		public static void AddShuiYinWord(string Path, string Path_sy)
		{
			string str = "测试水印";
			System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
			Graphics graphic = Graphics.FromImage(image);
			graphic.DrawImage(image, 0, 0, image.Width, image.Height);
			Font font = new Font("Verdana", 16f);
			Brush solidBrush = new SolidBrush(Color.Blue);
			graphic.DrawString(str, font, solidBrush, 15f, 15f);
			graphic.Dispose();
			image.Save(Path_sy);
			image.Dispose();
		}

		public static string ContentTypeLookup(string fileextension)
		{
			string str = "";
			try
			{
				RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(fileextension);
				str = registryKey.GetValue("Content Type", "application/unknown").ToString();
				registryKey = null;
			}
			catch
			{
				str = "application/unknown";
			}
			return str;
		}

		public static string DealCommandBySeesion(string strcmd)
		{
			foreach (Match match in (new Regex("\\[!(.*?)!\\]")).Matches(strcmd))
			{
				string value = match.Groups[1].Value;
				try
				{
					if (HttpContext.Current.Session[value] != null)
					{
						strcmd = strcmd.Replace(match.Value, HttpContext.Current.Session[value].ToString());
					}
				}
				catch
				{
					strcmd = strcmd.Replace(match.Value, "");
				}
			}
			return strcmd;
		}

		public static string DecodeBase64(string base64String, Encoding desEncoding)
		{
			int num;
			int num1;
			byte num2;
			byte num3;
			byte num4;
			int num5;
			byte num6;
			byte num7;
			string str;
			int[] numArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 62, 0, 0, 0, 63, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 0, 0, 0, 0, 0, 0, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51 };
			byte[] bytes = Encoding.ASCII.GetBytes(base64String);
			if (base64String.Length % 4 == 0)
			{
				MemoryStream memoryStream = new MemoryStream();
				for (int i = 0; i < base64String.Length; i = i + 4)
				{
					byte num8 = bytes[i];
					byte num9 = bytes[i + 1];
					byte num10 = bytes[i + 2];
					byte num11 = bytes[i + 3];
					if (num11 != 61)
					{
						num = numArray[num8];
						num1 = numArray[num9];
						num5 = numArray[num10];
						int num12 = numArray[num11];
						num2 = Convert.ToByte(num);
						num3 = Convert.ToByte(num1);
						num6 = Convert.ToByte(num5);
						byte num13 = Convert.ToByte(num12);
						num4 = Convert.ToByte((int)((byte)(num2 << 2) | (byte)(num3 >> 4 & 3)));
						num7 = Convert.ToByte((int)((byte)(num3 << 4) | (byte)(num6 >> 2 & 15)));
						byte num14 = Convert.ToByte((int)((byte)(num6 << 6) | (byte)(num13 & 63)));
						memoryStream.WriteByte(num4);
						memoryStream.WriteByte(num7);
						memoryStream.WriteByte(num14);
					}
					else if (num10 != 61)
					{
						num = numArray[num8];
						num1 = numArray[num9];
						num5 = numArray[num10];
						num2 = Convert.ToByte(num);
						num3 = Convert.ToByte(num1);
						num6 = Convert.ToByte(num5);
						num4 = Convert.ToByte((int)((byte)(num2 << 2) | (byte)(num3 >> 4 & 3)));
						num7 = Convert.ToByte((int)((byte)(num3 << 4) | (byte)(num6 >> 2 & 15)));
						memoryStream.WriteByte(num4);
						memoryStream.WriteByte(num7);
					}
					else
					{
						num = numArray[num8];
						num1 = numArray[num9];
						num2 = Convert.ToByte(num);
						num3 = Convert.ToByte(num1);
						num4 = Convert.ToByte((int)((byte)(num2 << 2) | (byte)(num3 >> 4 & 3)));
						memoryStream.WriteByte(num4);
					}
				}
				str = desEncoding.GetString(memoryStream.ToArray());
			}
			else
			{
				str = "";
			}
			return str;
		}

		public static object DeserializeToObject(byte[] bytes)
		{
			object obj = null;
			if (bytes != null)
			{
				MemoryStream memoryStream = new MemoryStream(bytes)
				{
					Position = (long)0
				};
				obj = (new BinaryFormatter()).Deserialize(memoryStream);
				memoryStream.Close();
			}
			return obj;
		}

		public static T DeserializeToObject<T>(byte[] bytes)
		{
			return (T)Utility.DeserializeToObject(bytes);
		}

		public static string EncodeBase64(string srcString, Encoding srcEncoding)
		{
			byte num;
			byte num1;
			char chr;
			char chr1;
			char chr2;
			char[] charArray = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".ToCharArray();
			StringBuilder stringBuilder = new StringBuilder();
			byte[] bytes = srcEncoding.GetBytes(srcString);
			int num2 = 0;
			int length = (int)bytes.Length / 3;
			int length1 = (int)bytes.Length % 3;
			for (int i = 0; i < length; i++)
			{
				num = bytes[i * 3];
				num1 = bytes[i * 3 + 1];
				byte num3 = bytes[i * 3 + 2];
				chr = charArray[Convert.ToInt16(num >> 2)];
				chr1 = charArray[Convert.ToInt16(num << 4 | num1 >> 4) & 63];
				chr2 = charArray[Convert.ToInt16((num1 << 2 | num3 >> 6) & 63)];
				char chr3 = charArray[Convert.ToInt16(num3 & 63)];
				stringBuilder.Append(chr);
				stringBuilder.Append(chr1);
				stringBuilder.Append(chr2);
				stringBuilder.Append(chr3);
				num2 = num2 + 3;
			}
			if (length1 == 1)
			{
				num = bytes[num2];
				chr = charArray[Convert.ToInt16(num >> 2)];
				chr1 = charArray[Convert.ToInt16((num & 3) << 4)];
				stringBuilder.Append(chr);
				stringBuilder.Append(chr1);
				stringBuilder.Append("=");
				stringBuilder.Append("=");
			}
			else if (length1 == 2)
			{
				num = bytes[num2];
				num1 = bytes[num2 + 1];
				chr = charArray[Convert.ToInt16(num >> 2)];
				chr1 = charArray[Convert.ToInt16((num << 4 | num1 >> 4) & 63)];
				chr2 = charArray[Convert.ToInt16((num1 & 15) << 2)];
				stringBuilder.Append(chr);
				stringBuilder.Append(chr1);
				stringBuilder.Append(chr2);
				stringBuilder.Append("=");
			}
			return stringBuilder.ToString();
		}

		public static string EncryptUrl(string url, string key)
		{
			string str;
			if (!(url.Trim() == ""))
			{
				string[] strArrays = url.Split("?".ToCharArray());
				if ((int)strArrays.Length == 1)
				{
					str = strArrays[0];
				}
				else if ((int)strArrays.Length != 2)
				{
					str = "";
				}
				else
				{
					string str1 = "";
					string str2 = strArrays[1];
					string subStr = "";
					while (str2.IndexOf("[UNCRYPT]") != -1)
					{
						subStr = Utility.GetSubStr(str2, "[UNCRYPT]", "[/UNCRYPT]", out str2);
						if (subStr.StartsWith("&"))
						{
							subStr = subStr.Remove(0, 1);
						}
						if (subStr.EndsWith("&"))
						{
							subStr = subStr.Remove(subStr.Length - 1, 1);
						}
						str1 = string.Concat(str1, "&", subStr);
					}
					str = string.Concat(strArrays[0], "?para=", Security.EncryptStr(str2, key), str1);
				}
			}
			else
			{
				str = "";
			}
			return str;
		}

		public static List<string> ExcelSheetName(string fileName)
		{
			List<string> strs = new List<string>();
			OleDbConnection oleDbConnection = new OleDbConnection(string.Concat("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=", fileName, ";Extended Properties=Excel 8.0;"));
			oleDbConnection.Open();
			Guid tables = OleDbSchemaGuid.Tables;
			object[] objArray = new object[] { null, null, null, "TABLE" };
			DataTable oleDbSchemaTable = oleDbConnection.GetOleDbSchemaTable(tables, objArray);
			oleDbConnection.Close();
			foreach (DataRow row in oleDbSchemaTable.Rows)
			{
				strs.Add(row[2].ToString());
			}
			return strs;
		}

		public static string FormatException(Exception ex, string catchInfo)
		{
			string str;
			if (ex != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (catchInfo != string.Empty)
				{
					stringBuilder.Append(catchInfo).Append("\r\n");
				}
				stringBuilder.Append(ex.Message).Append("\r\n").Append(ex.StackTrace);
				str = stringBuilder.ToString();
			}
			else
			{
				str = "";
			}
			return str;
		}

		public static string GetBrowserByUserAgent()
		{
			string str;
			string userAgent = HttpContext.Current.Request.UserAgent;
			if (userAgent.Contains("360SE"))
			{
				str = "360SE";
			}
			else if (userAgent.Contains("MetaSr"))
			{
				str = "Sogou";
			}
			else if (!userAgent.Contains("Maxthon"))
			{
				str = (!userAgent.Contains("TencentTraveler") ? HttpContext.Current.Request.Browser.Type : "TencentTraveler");
			}
			else
			{
				str = "Maxthon";
			}
			return str;
		}

		public static string GetCheckedValues(ListItemCollection list)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ListItem listItem in list)
			{
				if (listItem.Selected)
				{
					stringBuilder.Append(listItem.Value);
					stringBuilder.Append(",");
				}
			}
			return stringBuilder.ToString();
		}

		public static string GetClientIP()
		{
			string item = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
			if ((item == null ? true : item == string.Empty))
			{
				item = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
			}
			if ((item == null ? true : item == string.Empty))
			{
				item = HttpContext.Current.Request.UserHostAddress;
			}
			if (item == "::1")
			{
				item = "127.0.0.1";
			}
			return item;
		}

		public static string GetFieldValueList(DataTable dataSource, string colName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DataRow row in dataSource.Rows)
			{
				stringBuilder.AppendFormat("{0},", row[colName]);
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length = stringBuilder.Length - 1;
			}
			return stringBuilder.ToString();
		}

		public static string GetFriendlySize(long size)
		{
			string str;
			if (size < (long)1073741824)
			{
				str = (size < (long)1048576 ? string.Format("{0:f2}K", size / (long)1024) : string.Format("{0:f2}M", size / (long)1048576));
			}
			else
			{
				str = string.Format("{0:f2}G", size / (long)1073741824);
			}
			return str;
		}

		public static string GetJoinString(string spliter, IList sc)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in sc)
			{
				stringBuilder.AppendFormat("{0}{1}", obj, spliter);
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length = stringBuilder.Length - 1;
			}
			return stringBuilder.ToString();
		}

		public static string GetJoinString(IList sc)
		{
			return Utility.GetJoinString(",", sc);
		}

		public static string GetOSNameByUserAgent()
		{
			string userAgent = HttpContext.Current.Request.UserAgent;
			string str = "未知";
			if (userAgent.Contains("NT 6.3"))
			{
				str = "Windows 8.1";
			}
			else if (userAgent.Contains("NT 6.2"))
			{
				str = "Windows 8";
			}
			else if (userAgent.Contains("NT 6.1"))
			{
				str = "Windows 7";
			}
			else if (userAgent.Contains("NT 6.0"))
			{
				str = "Windows Vista/Server 2008";
			}
			else if (userAgent.Contains("NT 5.2"))
			{
				str = "Windows Server 2003";
			}
			else if (userAgent.Contains("NT 5.1"))
			{
				str = "Windows XP";
			}
			else if (userAgent.Contains("NT 5"))
			{
				str = "Windows 2000";
			}
			else if (userAgent.Contains("NT 4"))
			{
				str = "Windows NT4";
			}
			else if (userAgent.Contains("Me"))
			{
				str = "Windows Me";
			}
			else if (userAgent.Contains("98"))
			{
				str = "Windows 98";
			}
			else if (userAgent.Contains("95"))
			{
				str = "Windows 95";
			}
			else if (userAgent.Contains("iPad"))
			{
				str = "iPad";
			}
			else if (userAgent.Contains("iPhone"))
			{
				str = "iPhone";
			}
			else if (userAgent.Contains("Mac"))
			{
				str = "Mac";
			}
			else if (userAgent.Contains("Unix"))
			{
				str = "UNIX";
			}
			else if (userAgent.Contains("Android"))
			{
				str = "Android";
			}
			else if (userAgent.Contains("Linux"))
			{
				str = "Linux";
			}
			else if (userAgent.Contains("SunOS"))
			{
				str = "SunOS";
			}
			else if (userAgent.Contains("SymbianOS"))
			{
				str = "SymbianOS";
			}
			return str;
		}

		public static string GetPara(string ourl, string param)
		{
			string str;
			Regex regex = new Regex(string.Concat(param, "=([^&]*)"), RegexOptions.IgnoreCase);
			Match match = regex.Match(ourl);
			if (!match.Success)
			{
				str = "";
			}
			else
			{
				str = (match.Value.Length <= param.Length ? "" : match.Groups[1].Value);
			}
			return str;
		}

		public static string GetPhysicalRootPath()
		{
			string baseDirectory = "";
			HttpContext current = HttpContext.Current;
			if (current == null)
			{
				baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
				if (Regex.Match(baseDirectory, "\\\\$", RegexOptions.Compiled).Success)
				{
					baseDirectory = baseDirectory.Substring(0, baseDirectory.Length - 1);
				}
			}
			else
			{
				baseDirectory = current.Server.MapPath("~");
			}
			return baseDirectory;
		}

        public static string GetRootURI()
        {
            string str = "";
            if (ConfigurationManager.AppSettings["WebAppRoot"] != null) str = ConfigurationManager.AppSettings["WebAppRoot"].ToString();
            if (str == "")
            {
                HttpContext current = HttpContext.Current;
                if (current != null)
                {
                    HttpRequest request = current.Request;
                    string leftPart = request.Url.GetLeftPart(UriPartial.Authority);
                    str = ((request.ApplicationPath == null ? false : !(request.ApplicationPath == "/")) ? string.Concat(leftPart, request.ApplicationPath) : leftPart);
                }
            }
            return str;
        }

		public static object GetSession(string key)
		{
			object obj;
			obj = (HttpContext.Current.Session[key] == null ? null : HttpContext.Current.Session[key]);
			return obj;
		}

		public static string GetSession(string key, string defValue)
		{
			string str;
			str = (HttpContext.Current.Session[key] == null ? defValue : HttpContext.Current.Session[key].ToString());
			return str;
		}

		public static string GetSplitQuoteString(string sourceString, string spliter)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] strArrays = sourceString.Split(spliter.ToCharArray());
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				stringBuilder.AppendFormat("'{0}',", strArrays[i]);
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length = stringBuilder.Length - 1;
			}
			return stringBuilder.ToString();
		}

		public static string GetSplitQuoteString(IList list)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in list)
			{
				stringBuilder.AppendFormat("'{0}',", obj);
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length = stringBuilder.Length - 1;
			}
			return stringBuilder.ToString();
		}

		public static string GetSubStr(string originalStr, string startStr, string endStr, out string returnStr)
		{
			string str = "";
			int num = originalStr.IndexOf(startStr);
			int num1 = originalStr.IndexOf(endStr);
			if (num1 - num < 0)
			{
				returnStr = "";
			}
			else
			{
				str = originalStr.Substring(num + startStr.Length, num1 - num - startStr.Length);
				returnStr = originalStr.Remove(num, num1 - num + endStr.Length);
			}
			return str;
		}

		public static string GetValue(string path, string AppKey, string attr)
		{
			string str;
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.Load(path);
				XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode(AppKey);
				str = (xmlElement == null ? "" : xmlElement.GetAttribute(attr));
			}
			catch (Exception exception)
			{
				str = "";
			}
			return str;
		}

		public static DataSet GetXls(string fileName, string sheetName)
		{
			DataSet dataSet = null;
			string str = "";
			if (fileName.EndsWith(".xls"))
			{
				str = string.Concat("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=", fileName, ";Extended Properties=Excel 8.0");
			}
			else if (fileName.EndsWith(".xlsx"))
			{
				str = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'", fileName);
			}
			if (str.Length > 0)
			{
				OleDbConnection oleDbConnection = null;
				dataSet = new DataSet();
				try
				{
					oleDbConnection = new OleDbConnection(str);
					OleDbCommand oleDbCommand = new OleDbCommand(string.Concat("select * from ", sheetName), oleDbConnection);
					OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter()
					{
						SelectCommand = oleDbCommand
					};
					oleDbDataAdapter.Fill(dataSet);
				}
				catch (Exception exception)
				{
					throw exception;
				}
			}
			return dataSet;
		}

		public static string HtmlTransForXML(string p_sStr)
		{
			string str = p_sStr.Replace("&amp;", "&");
			str = str.Replace("&lt;", "<");
			str = str.Replace("&gt;", ">");
			str = str.Replace("&quot;", "\"\"");
			str = str.Replace("&apos;", "'");
			return str.Replace("&nbsp;;", " ");
		}

		public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
		{
			System.Drawing.Image bitmap;
			Graphics graphic;
			System.Drawing.Image image = System.Drawing.Image.FromFile(originalImagePath);
			int num = width;
			int num1 = height;
			int num2 = 0;
			int num3 = 0;
			int num4 = image.Width;
			int num5 = image.Height;
			string str = mode;
			if (str == null)
			{
			}
			else if (str != "HW")
			{
				if (str == "W")
				{
					num1 = image.Height * width / image.Width;
				}
				else if (str == "H")
				{
					num = image.Width * height / image.Height;
				}
				else
				{
					if (str != "Cut")
					{
						bitmap = new Bitmap(num, num1);
						graphic = Graphics.FromImage(bitmap);
						graphic.InterpolationMode = InterpolationMode.High;
						graphic.SmoothingMode = SmoothingMode.HighQuality;
						graphic.Clear(Color.Transparent);
						graphic.DrawImage(image, new Rectangle(0, 0, num, num1), new Rectangle(num2, num3, num4, num5), GraphicsUnit.Pixel);
						try
						{
							try
							{
								bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
							}
							catch (Exception exception)
							{
								throw exception;
							}
						}
						finally
						{
							image.Dispose();
							bitmap.Dispose();
							graphic.Dispose();
						}
						return;
					}
					if ((double)image.Width / (double)image.Height <= (double)num / (double)num1)
					{
						num4 = image.Width;
						num5 = image.Width * height / num;
						num2 = 0;
						num3 = (image.Height - num5) / 2;
					}
					else
					{
						num5 = image.Height;
						num4 = image.Height * num / num1;
						num3 = 0;
						num2 = (image.Width - num4) / 2;
					}
				}
			}
			bitmap = new Bitmap(num, num1);
			graphic = Graphics.FromImage(bitmap);
			graphic.InterpolationMode = InterpolationMode.High;
			graphic.SmoothingMode = SmoothingMode.HighQuality;
			graphic.Clear(Color.Transparent);
			graphic.DrawImage(image, new Rectangle(0, 0, num, num1), new Rectangle(num2, num3, num4, num5), GraphicsUnit.Pixel);
			try
			{
				try
				{
					bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
				}
				catch (Exception exception)
				{
					throw exception;
				}
			}
			finally
			{
				image.Dispose();
				bitmap.Dispose();
				graphic.Dispose();
			}
			return;
			bitmap = new Bitmap(num, num1);
			graphic = Graphics.FromImage(bitmap);
			graphic.InterpolationMode = InterpolationMode.High;
			graphic.SmoothingMode = SmoothingMode.HighQuality;
			graphic.Clear(Color.Transparent);
			graphic.DrawImage(image, new Rectangle(0, 0, num, num1), new Rectangle(num2, num3, num4, num5), GraphicsUnit.Pixel);
			try
			{
				try
				{
					bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
				}
				catch (Exception exception)
				{
					throw exception;
				}
			}
			finally
			{
				image.Dispose();
				bitmap.Dispose();
				graphic.Dispose();
			}
		}

		public static string ReplacePara(string ourl, string param, string newValue)
		{
			string str = Regex.Replace(ourl, string.Concat(param, "=([^&#]*)"), string.Concat(param, "=", newValue), RegexOptions.IgnoreCase);
			return str;
		}

		public static string ReplaceParaValues(string input, string para)
		{
			if ((input.Length <= 0 ? false : para.Length > 0))
			{
				string[] strArrays = para.Split(new char[] { '|' });
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					string str = strArrays[i];
					string[] strArrays1 = str.Split("=".ToCharArray(), 2);
					input = input.Replace(strArrays1[0], strArrays1[1]);
				}
			}
			return input;
		}

		public static string ReplaceWithDataRow(string sourceString, DataRow data)
		{
			string str = Utility.DealCommandBySeesion(sourceString);
			foreach (Match match in (new Regex("\\{(.*?)\\}", RegexOptions.IgnoreCase)).Matches(sourceString))
			{
				string value = match.Groups[1].Value;
				if (data.Table.Columns.Contains(value))
				{
					str = (data.RowState != DataRowState.Deleted ? str.Replace(match.Value, data[value].ToString()) : str.Replace(match.Value, data[value, DataRowVersion.Original].ToString()));
				}
			}
			return str;
		}

		public static byte[] SerializeToBinary(object obj)
		{
            byte[] numArray;
            if (obj != null)
            {
                MemoryStream memoryStream = new MemoryStream();
                (new BinaryFormatter()).Serialize(memoryStream, obj);
                memoryStream.Position = (long)0;
                byte[] numArray1 = new byte[memoryStream.Length];
                memoryStream.Read(numArray1, 0, (int)numArray1.Length);
                memoryStream.Close();
                numArray = numArray1;
            }
            else
            {
                numArray = null;
            }
            return numArray;
        
		}

		public static void SetCheckedValues(ListItemCollection list, string value)
		{
			char[] chrArray = new char[] { ',' };
			string[] strArrays = value.Split(chrArray, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				ListItem listItem = list.FindByValue(strArrays[i]);
				if (listItem != null)
				{
					listItem.Selected = true;
				}
			}
		}

		public static void SetSession(string key, object content)
		{
			HttpContext.Current.Session[key] = content;
		}

		public static void SetValue(string path, string AppKey, string attr, string AppValue)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(path);
			XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode(AppKey);
			if (xmlElement != null)
			{
				xmlElement.SetAttribute(attr, AppValue);
			}
			xmlDocument.Save(path);
		}

		public static string String2Html(string strData)
		{
			string str;
			if (strData != null)
			{
				strData = strData.Replace("&", "&#38;");
				strData = strData.Replace("<", "&#60;");
				strData = strData.Replace(">", "&#62;");
				strData = strData.Replace("'", "&#39;");
				strData = strData.Replace("\"", "&#34;");
				str = strData;
			}
			else
			{
				str = "";
			}
			return str;
		}
   

		public static string String2Xml(string strData)
		{
			string str;
			if (strData != null)
			{
				strData = strData.Replace("&", "&amp;");
				strData = strData.Replace("<", "&lt;");
				strData = strData.Replace(">", "&gt;");
				strData = strData.Replace("'", "&apos;");
				strData = strData.Replace("\"", "&quot;");
				str = strData;
			}
			else
			{
				str = "";
			}
			return str;
		}

		public static string TransferCDATA(string orgString)
		{
			return orgString.Replace("]]>", "]]]]><![CDATA[>");
		}

		public static string TrimPara(string ourl, string param)
		{
			string str = Regex.Replace(ourl, string.Concat(param, "=([^&#]*)&{0,1}"), "", RegexOptions.IgnoreCase);
			return str;
		}

		public static string Xml2String(string xmlData)
		{
			string str;
			if (xmlData != null)
			{
				xmlData = xmlData.Replace("&amp;", "&");
				xmlData = xmlData.Replace("&lt;", "<");
				xmlData = xmlData.Replace("&gt;", ">");
				xmlData = xmlData.Replace("&apos;", "'");
				xmlData = xmlData.Replace("&quot;", "\"");
				str = xmlData;
			}
			else
			{
				str = "";
			}
			return str;
		}


        /// <summary>  
        /// 根据GUID获取16位的唯一字符串  
        /// </summary>  
        /// <param name=\"guid\"></param>  
        /// <returns></returns>  
        public static string GuidTo16String()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            { i *= ((int)b + 1); }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>  
        public static long GuidToLongID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
     
	}
}