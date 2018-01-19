using EIS.DataAccess;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Xml;

namespace EIS.AppCommon
{
	public class GetChartData : IHttpHandler, IRequiresSessionState
	{
		private string sortname = "";

		private string sortorder = "";

		private string querySQL = "";

		private string connectionId = "";

		private string chartType = "";

		private string condstr = "";

		private string xmlModel = "";

		private string sortdir = "";

		private DataTable retdt = new DataTable();

		private XmlDocument xmldoc = new XmlDocument();

		private HttpRequest Request;

		private HttpResponse Response;

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public GetChartData()
		{
		}

		private void GetList()
		{
			StringBuilder stringBuilder;
			DataRow row = null;
			XmlNode firstChild;
			string outerXml;
			this.querySQL = this.querySQL.Replace("|^condition^|", this.condstr.Replace("[QUOTES]", "'")).Replace("|^sortdir^|", this.sortdir).Replace("\r\n", " ").Replace("\t", "");
			string item = this.Request["DefaultValue"];
			if (item != null)
			{
				item = HttpContext.Current.Server.UrlDecode(item);
				foreach (Match match in (new Regex("\"(?<fld>[^,]+?)\":\"?(?<val>[^,\"}]+)\"?")).Matches(item))
				{
					string value = match.Groups["fld"].Value;
					string str = match.Groups["val"].Value;
					this.querySQL = this.querySQL.Replace(value, str);
				}
			}
			this.querySQL = this.ReplaceContext(this.querySQL);
			if (!(this.connectionId.Trim() != ""))
			{
				this.retdt = SysDatabase.ExecuteTable(this.querySQL);
			}
			else
			{
				CustomDb customDb = new CustomDb();
				customDb.CreateDatabaseByConnectionId(this.connectionId.Trim());
				this.retdt = customDb.ExecuteTable(this.querySQL);
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(this.xmlModel);
			XmlElement documentElement = xmlDocument.DocumentElement;
			if ((documentElement.Attributes["showValues"] == null ? false : !string.IsNullOrEmpty(this.Request["showValues"])))
			{
				documentElement.Attributes["showValues"].Value = this.Request["showValues"];
			}
			XmlNodeList xmlNodeLists = documentElement.SelectNodes("//dataset");
			if (xmlNodeLists.Count <= 0)
			{
				this.xmlModel = xmlDocument.OuterXml;
				firstChild = documentElement.SelectSingleNode("//set");
				outerXml = firstChild.OuterXml;
				stringBuilder = new StringBuilder();
				foreach (DataRow rowA in this.retdt.Rows)
				{
                    stringBuilder.Append(this.ReplaceDataRow(outerXml, rowA));
				}
				this.xmlModel = this.xmlModel.Replace(outerXml, stringBuilder.ToString());
				this.xmlModel = this.xmlModel.Replace("\r\n", " ").Replace("\t", "");
			}
			else
			{
				stringBuilder = new StringBuilder();
				XmlNode xmlNodes = documentElement.SelectSingleNode("//category");
				string value1 = xmlNodes.Attributes["label"].Value;
				value1 = value1.Substring(1, value1.Length - 2);
				StringCollection stringCollections = new StringCollection();
				foreach (DataRow dataRow in this.retdt.Rows)
				{
					string str1 = dataRow[value1].ToString();
					stringBuilder.AppendFormat("<category label=\"{0}\"/>", str1);
				}
				xmlNodes.ParentNode.InnerXml = stringBuilder.ToString();
				for (int i = 0; i < xmlNodeLists.Count; i++)
				{
					stringBuilder.Length = 0;
					firstChild = xmlNodeLists[i].FirstChild;
					outerXml = firstChild.OuterXml;
					string value2 = firstChild.Attributes["value"].Value;
					value2 = value2.Substring(1, value2.Length - 2);
					foreach (DataRow row1 in this.retdt.Rows)
					{
						stringBuilder.Append(this.ReplaceDataRow(outerXml, row1));
					}
					xmlNodeLists[i].InnerXml = stringBuilder.ToString();
				}
				this.xmlModel = xmlDocument.OuterXml;
			}
		}

		public void ProcessRequest(HttpContext context)
		{
			if (context.Session["UserName"] == null)
			{
				throw new Exception("会话过期！");
			}
			context.Session["UserName"].ToString();
			this.Request = context.Request;
			this.Response = context.Response;
			string item = this.Request["chartId"];
			this.chartType = this.Request["chartType"];
			this.condstr = this.Request["condition"];
			this.condstr = HttpContext.Current.Server.UrlDecode(this.condstr);
			if (string.IsNullOrEmpty(this.condstr))
			{
				this.condstr = " 1=1 ";
			}
			this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			this.Response.Charset = "utf-8";
			this.Response.AppendHeader("Pragma", "no-cache");
			this.Response.AppendHeader("Content-type", "text/xml");
			string str = string.Concat("select * from T_E_App_FusionChartsXT where chartId='", item, "'");
			DbDataReader dbDataReaders = SysDatabase.ExecuteReader(str);
			try
			{
				if (!dbDataReaders.Read())
				{
					this.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?><chart></chart>");
					return;
				}
				else
				{
					this.querySQL = dbDataReaders["ChartSQL1"].ToString();
					this.xmlModel = dbDataReaders["XMLData"].ToString();
					dbDataReaders.Close();
				}
			}
			finally
			{
				if (dbDataReaders != null)
				{
					((IDisposable)dbDataReaders).Dispose();
				}
			}
			byte[] numArray = new byte[] { 239, 187, 191 };
			Encoding uTF8Encoding = new UTF8Encoding();
			this.GetList();
			this.Response.Write(string.Concat(uTF8Encoding.GetString(numArray), this.xmlModel));
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

        private string ReplaceDataRow(string tmpl, DataRow dataRow_0)
		{
            foreach (Match match in (new Regex("{(\\w+):?(.*?)}")).Matches(tmpl))
            {
                string value = match.Groups[1].Value;
                string str = match.Groups[2].Value;
                string str1 = dataRow_0[value].ToString();
                if ((typeof(DateTime) != dataRow_0.Table.Columns[value].DataType ? false : dataRow_0[value] != DBNull.Value))
                {
                    str1 = ((DateTime)dataRow_0[value]).ToString("yyyy-MM-dd");
                }
                if (!dataRow_0.Table.Columns.Contains(value))
                {
                    continue;
                }
                tmpl = tmpl.Replace(match.Value, str1);
            }
            return tmpl;
		}
	}
}