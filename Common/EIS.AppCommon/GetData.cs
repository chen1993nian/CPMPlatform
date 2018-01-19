using EIS.AppBase;
using EIS.DataAccess;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using System.Xml;

namespace EIS.AppCommon
{
	public class GetData : IHttpHandler, IRequiresSessionState
	{
		private int page = 0;

		private int rp = 0;

		private string sortname = "";

		private string sortorder = "";

		private StringBuilder sb = new StringBuilder();

		private string querysql = "";

		private string querylist = "";

		private string condstr = "";

		private string sortdir = "";

		private string distinct = "";

		private XmlNode querylistnode = null;

		private DataTable retdt = new DataTable();

		private HttpRequest Request;

		private HttpResponse Response;

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

	

		private string GetDetailAfterListSub(string loopbody, DataRow r, int rnum)
		{
			Match match = null;
			string value;
			string str;
			DateTime item;
			string str1 = loopbody.Replace("[#ROWNUM#]", rnum.ToString());
			foreach (Match matchA in (new Regex("\\[(?<fld>(\\w)+?)]")).Matches(str1))
			{
                value = matchA.Value.Substring(1, matchA.Value.Length - 2);
				str = r[value].ToString();
				if ((typeof(DateTime) != r.Table.Columns[value].DataType ? false : r[value] != DBNull.Value))
				{
					item = (DateTime)r[value];
					str = item.ToString("yyyy-MM-dd");
				}
				if (r.Table.Columns.Contains(value))
				{
                    str1 = str1.Replace(matchA.Value, str);
				}
			}
			foreach (Match match1 in (new Regex("{(\\w+):?(.*?)}")).Matches(str1))
			{
				value = match1.Groups[1].Value;
				string value1 = match1.Groups[2].Value;
				str = "";
				if (!r.Table.Columns.Contains(value))
				{
					str1 = str1.Replace(match1.Value, "<![CDATA[]]>");
				}
				else
				{
					str = r[value].ToString();
					if ((typeof(DateTime) != r.Table.Columns[value].DataType ? false : r[value] != DBNull.Value))
					{
						if (value1.Trim() == "")
						{
							value1 = "yyyy-MM-dd";
						}
						item = (DateTime)r[value];
						str = item.ToString(value1);
					}
					str1 = str1.Replace(match1.Value, string.Concat("<![CDATA[", str, "]]>"));
				}
			}
			string str2 = str1.Insert(str1.IndexOf(">") + 1, string.Concat("<rowindex>", (rnum == 0 ? "" : rnum.ToString()), "</rowindex>"));
			return str2;
		}

		private void GetList()
		{
			int i;
			this.querysql = this.querysql.Replace("|^condition^|", this.condstr).Replace("|^sortdir^|", this.sortdir).Replace("\r\n", " ").Replace("\t", "");
			string item = this.Request["DefaultValue"];
			if (item != null)
			{
				item = HttpContext.Current.Server.UrlDecode(item);
				foreach (Match match in (new Regex("\"(?<fld>[^,]+?)\":\"?(?<val>[^,\"}]+)\"?")).Matches(item))
				{
					string value = match.Groups["fld"].Value;
					string str = match.Groups["val"].Value;
					this.querysql = this.querysql.Replace(value, str);
				}
			}
			this.querysql = this.ReplaceContext(this.querysql);
			this.retdt = SysDatabase.ExecuteTable(this.querysql);
			if (this.distinct.Length <= 0)
			{
				this.sb.AppendFormat("<total>{0}</total>\n", this.retdt.Rows.Count);
			}
			else
			{
				DataTable dataTable = this.retdt.Clone();
				StringCollection stringCollections = new StringCollection();
				int num = 0;
				for (i = 0; i < this.retdt.Rows.Count; i++)
				{
					DataRow dataRow = this.retdt.Rows[i];
					if (!stringCollections.Contains(dataRow[this.distinct].ToString()))
					{
						if (num <= this.page * this.rp)
						{
							dataTable.LoadDataRow(dataRow.ItemArray, true);
						}
						stringCollections.Add(dataRow[this.distinct].ToString());
						num++;
					}
				}
				this.sb.AppendFormat("<total>{0}</total>\n", num);
				this.retdt = dataTable;
			}
			string str1 = this.querylist;
			i = (this.page - 1) * this.rp;
			while (true)
			{
				if ((i >= this.retdt.Rows.Count ? true : i >= this.page * this.rp))
				{
					break;
				}
				this.sb.Append(this.GetDetailAfterListSub(str1, this.retdt.Rows[i], i + 1));
				i++;
			}
			if (this.querylistnode.FirstChild.SelectNodes("*[@compute]").Count > 0)
			{
				DataRow dataRow1 = this.retdt.NewRow();
				foreach (XmlNode xmlNodes in this.querylistnode.FirstChild.SelectNodes("*[@compute]"))
				{
					string value1 = xmlNodes.Attributes["compute"].Value;
					string name = xmlNodes.Name;
					if (this.retdt.Columns.Contains(name))
					{
						if (xmlNodes.Attributes["dummy"] != null)
						{
							dataRow1[name] = value1;
						}
						else
						{
							dataRow1[name] = this.retdt.Compute(value1, "");
						}
					}
				}
				this.sb.Append(this.GetDetailAfterListSub(str1, dataRow1, 0));
			}
		}

		public void ProcessRequest(HttpContext context)
		{
			if (context.Session["UserName"] == null)
			{
				throw new Exception("会话过期！");
			}
			string str = context.Session["UserName"].ToString();
			this.Request = context.Request;
			this.Response = context.Response;
			this.page = int.Parse(this.Request["page"]);
			this.rp = int.Parse(this.Request["rp"]);
			this.sortname = this.Request["sortname"];
			this.sortorder = this.Request["sortorder"];
			this.sortdir = (string.IsNullOrEmpty(this.sortname) ? "" : string.Concat(" order by ", this.sortname, " ", this.Request["sortorder"]));
			if (this.sortdir != "")
			{
				if (this.Request["sortname"].ToLower() == "rowindex")
				{
					this.sortdir = "";
				}
			}
			string item = this.Request["queryid"];
			if (!string.IsNullOrEmpty(this.Request["cryptcond"]))
			{
				this.condstr = Security.Decrypt(this.Request["cryptcond"], str);
			}
			else
			{
				this.condstr = this.Request["condition"];
			}
			if (!string.IsNullOrEmpty(this.Request["query"]))
			{
				if (!string.IsNullOrEmpty(this.condstr))
				{
					GetData getDatum = this;
					getDatum.condstr = string.Concat(getDatum.condstr, " and ", HttpContext.Current.Server.UrlDecode(this.Request["query"]));
				}
				else
				{
					this.condstr = HttpContext.Current.Server.UrlDecode(this.Request["query"]);
				}
			}
			if (!string.IsNullOrEmpty(this.condstr))
			{
				this.condstr = this.condstr.Replace("[QUOTES]", "'");
			}
			else
			{
				this.condstr = " 1=1 ";
			}
			this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			this.Response.AppendHeader("Pragma", "no-cache");
			this.Response.AppendHeader("Content-type", "text/xml");
			XmlDocument xmlDocument = new XmlDocument();
			string str1 = "Data";
			if (!string.IsNullOrEmpty(this.Request["ds"]))
			{
				str1 = this.Request["ds"].ToString();
			}
			string str2 = HttpContext.Current.Server.MapPath(string.Concat("~/App_Data/", str1, ".xml"));
			string str3 = string.Concat("App_Data_", str1);
			if (HttpContext.Current.Cache[str3] == null)
			{
				xmlDocument.Load(str2);
				CacheDependency cacheDependency = new CacheDependency(str2);
				HttpRuntime.Cache.Insert(str3, xmlDocument, cacheDependency);
			}
			else
			{
				xmlDocument = HttpContext.Current.Cache[str3] as XmlDocument;
			}
			XmlElement documentElement = xmlDocument.DocumentElement;
			XmlNode xmlNodes = documentElement.SelectSingleNode(string.Concat("/queryobjs/queryobj[@queryid='", item, "']"));
			if (xmlNodes == null)
			{
				this.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?><rows></rows>");
			}
			else
			{
				this.querylistnode = xmlNodes.SelectSingleNode("querylist");
				XmlNode xmlNodes1 = xmlNodes.SelectSingleNode("querysql");
				this.querysql = xmlNodes1.InnerText;
				this.querylist = this.querylistnode.InnerXml;
				if ((xmlNodes1.Attributes["sortdir"] == null ? false : this.sortdir == ""))
				{
					this.sortdir = string.Concat(" order by ", xmlNodes1.Attributes["sortdir"].Value);
				}
				if (xmlNodes1.Attributes["distinct"] != null)
				{
					this.distinct = xmlNodes1.Attributes["distinct"].Value;
				}
				this.sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
				this.sb.Append("<rows>\n");
				this.sb.AppendFormat("<page>{0}</page>\n", this.page);
				this.GetList();
				this.sb.Append("</rows>\n");
				this.Response.Write(this.sb.ToString());
			}
		}

		private string ReplaceContext(string source)
		{
			foreach (Match match in (new Regex("\\[!(.*?)!\\]")).Matches(source))
			{
				string value = match.Groups[1].Value;
				try
				{
					if (HttpContext.Current.Session[value] != null)
					{
						source = source.Replace(match.Value, HttpContext.Current.Session[value].ToString());
					}
				}
				catch
				{
					source = source.Replace(match.Value, "");
				}
			}
			return source;
		}
	}
}