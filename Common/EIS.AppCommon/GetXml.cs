using EIS.AppBase;
using EIS.DataAccess;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Xml;

namespace EIS.AppCommon
{
	public class GetXml : IHttpHandler, IRequiresSessionState
	{
		private int page = 0;

		private int rp = 0;

		private string sortname = "";

		private string sortorder = "";

		private StringBuilder sb = new StringBuilder();

		private string querysql = "";

		private string connectionId = "";

		private string tblType = "";

		private string condstr = "";

		private string sindex = "";

		private string sortdir = "";

		private string queryid = "";

		private DataTable retdt = new DataTable();

		private DataTable dtFields = null;

		public XmlElement xmlmodel = null;

		private XmlDocument xmldoc = new XmlDocument();

		private HttpRequest Request;

		private HttpResponse Response;

		private bool hasAutoId = false;

		public static StringCollection SysFields;

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		static GetXml()
		{
			GetXml.SysFields = new StringCollection();
			GetXml.SysFields.Add("_autoid");
			GetXml.SysFields.Add("_username");
			GetXml.SysFields.Add("_orgcode");
			GetXml.SysFields.Add("_createtime");
			GetXml.SysFields.Add("_updatetime");
			GetXml.SysFields.Add("_isdel");
			GetXml.SysFields.Add("_companyid");
			GetXml.SysFields.Add("_wfstate");
			GetXml.SysFields.Add("_gdstate");
		}

		public GetXml()
		{
		}

		private string GetDetailAfterListSub(XmlElement loop, DataRow r, int rnum)
		{
			string str;
			XmlElement xmlElement = this.xmldoc.CreateElement("rowindex");
			xmlElement.InnerText = (rnum == 0 ? "" : rnum.ToString());
			loop.AppendChild(xmlElement);
			if (!this.hasAutoId)
			{
				loop.SetAttribute("id", rnum.ToString());
			}
			else
			{
				str = r["_AutoID"].ToString();
				loop.SetAttribute("id", str);
			}
			foreach (XmlElement childNode in loop.ChildNodes)
			{
				string name = childNode.Name;
				if (r.Table.Columns.Contains(name))
				{
					str = r[name].ToString();
					str = Utility.String2Xml(str);
					if ((typeof(DateTime) != r.Table.Columns[name].DataType ? false : r[name] != DBNull.Value))
					{
						string str1 = "yyyy-MM-dd";
						DataRow[] dataRowArray = this.dtFields.Select(string.Concat("fieldName='", name, "' and IsNull(ColFormatExp,'')<>''"));
						if ((int)dataRowArray.Length > 0)
						{
							str1 = dataRowArray[0]["ColFormatExp"].ToString();
						}
						str = ((DateTime)r[name]).ToString(str1);
					}
					childNode.InnerXml = string.Concat("<![CDATA[", str, "]]>");
				}
			}
			return loop.OuterXml;
		}

		private void GetFldModel(string querysql, int t)
		{
			DataTable dataTable;
			string str = querysql.Replace("|^condition^|", " 1=2 ").Replace("|^sortdir^|", this.sortdir).Replace("\r\n", " ").Replace("\t", "");
			string item = this.Request["DefaultValue"];
			if (!string.IsNullOrEmpty(item))
			{
				item = HttpContext.Current.Server.UrlDecode(item);
				foreach (Match match in (new Regex("\"(?<fld>[^,]+?)\":\"?(?<val>[^,\"}]+)\"?")).Matches(item))
				{
					string value = match.Groups["fld"].Value;
					string value1 = match.Groups["val"].Value;
					str = str.Replace(value, value1);
				}
				str = Utility.ReplaceParaValues(str, item);
			}
			str = this.ReplaceContext(str);
			if (!(this.connectionId.Trim() != ""))
			{
				dataTable = SysDatabase.ExecuteTable(str);
			}
			else
			{
				CustomDb customDb = new CustomDb();
				customDb.CreateDatabaseByConnectionId(this.connectionId.Trim());
				dataTable = customDb.ExecuteTable(str);
			}
			this.xmlmodel = this.xmldoc.CreateElement("row");
			this.xmlmodel.SetAttribute("id", "");
			this.hasAutoId = dataTable.Columns.Contains("_AutoID");
			foreach (DataColumn column in dataTable.Columns)
			{
				string columnName = column.ColumnName;
				if (column.ColumnName.ToLower() == "_wfstate")
				{
					columnName = "_wfstate";
				}
				else if (column.ColumnName.ToLower() == "_gdstate")
				{
					columnName = "_gdstate";
				}
				else if (!GetXml.IsAppField(columnName))
				{
					if ((int)this.dtFields.Select(string.Concat("FieldName='", columnName, "'")).Length == 0)
					{
						continue;
					}
				}
				XmlElement xmlElement = this.xmldoc.CreateElement(columnName);
				xmlElement.InnerText = "";
				this.xmlmodel.AppendChild(xmlElement);
			}
		}

		private void GetList()
		{
			int num;
			int num1;
			this.querysql = this.querysql.Replace("|^condition^|", string.Concat("(", this.condstr, ")")).Replace("\r\n", " ").Replace("\t", "");
			string item = this.Request["DefaultValue"];
			if (!string.IsNullOrEmpty(item))
			{
				item = HttpContext.Current.Server.UrlDecode(item);
				foreach (Match match in (new Regex("\"(?<fld>[^,]+?)\":\"?(?<val>[^,\"}]+)\"?")).Matches(item))
				{
					string value = match.Groups["fld"].Value;
					string str = match.Groups["val"].Value;
					this.querysql = this.querysql.Replace(value, str);
				}
				this.querysql = Utility.ReplaceParaValues(this.querysql, item);
			}
			this.querysql = this.ReplaceContext(this.querysql);
			bool flag = this.querysql.IndexOf("|^sortdir^|") > 0;
			string str1 = this.querysql.Replace("|^sortdir^|", "");
			this.querysql = this.querysql.Replace("|^sortdir^|", this.sortdir);
			int num2 = 0;
			if (!(this.connectionId.Trim() != ""))
			{
				num = (this.page - 1) * this.rp + 1;
				num1 = this.page * this.rp;
				if (!flag)
				{
					this.retdt = SysDatabase.ExecuteTable(this.querysql, num, num1, ref num2);
				}
				else
				{
					this.retdt = SysDatabase.ExecuteTable(this.querysql, num, num1);
					num2 = int.Parse(SysDatabase.ExecuteScalar(string.Concat("select count(0) from (", str1, ") _t_")).ToString());
				}
			}
			else
			{
				CustomDb customDb = new CustomDb();
				customDb.CreateDatabaseByConnectionId(this.connectionId.Trim());
				num = (this.page - 1) * this.rp + 1;
				num1 = this.page * this.rp;
				if (!flag)
				{
					this.retdt = customDb.ExecuteTable(this.querysql, num, num1, ref num2);
				}
				else
				{
					this.retdt = customDb.ExecuteTable(this.querysql, num, num1);
					num2 = (!(customDb.dbType == "oracle") ? int.Parse(customDb.ExecuteScalar(string.Concat("select count(*) from (", str1, ") _t_")).ToString()) : int.Parse(customDb.ExecuteScalar(string.Concat("select count(*) from (", str1, ")")).ToString()));
				}
			}
			this.xmlmodel = this.xmldoc.CreateElement("row");
			this.xmlmodel.SetAttribute("id", "");
			this.hasAutoId = this.retdt.Columns.Contains("_AutoID");
			foreach (DataColumn column in this.retdt.Columns)
			{
				string columnName = column.ColumnName;
				if (this.tblType == "1")
				{
					if (column.ColumnName.ToLower() == "_wfstate")
					{
						columnName = "_wfstate";
					}
					else if (column.ColumnName.ToLower() == "_gdstate")
					{
						columnName = "_gdstate";
					}
				}
				XmlElement xmlElement = this.xmldoc.CreateElement(columnName);
				xmlElement.InnerText = "";
				this.xmlmodel.AppendChild(xmlElement);
			}
			this.sb.AppendFormat("<total>{0}</total>\n", num2);
			for (int i = 0; i < this.retdt.Rows.Count; i++)
			{
				XmlElement xmlElement1 = (XmlElement)this.xmlmodel.CloneNode(true);
				int num3 = (this.page - 1) * this.rp + i + 1;
				this.sb.Append(this.GetDetailAfterListSub(xmlElement1, this.retdt.Rows[i], num3));
			}
			XmlElement xmlElement2 = (XmlElement)this.xmlmodel.CloneNode(true);
			DataRow[] dataRowArray = this.dtFields.Select("ISNULL(ColTotalExp,'') <>''");
			DataRow[] dataRowArray1 = dataRowArray;
			for (int j = 0; j < (int)dataRowArray1.Length; j++)
			{
				DataRow dataRow = dataRowArray1[j];
				string str2 = dataRow["ColTotalExp"].ToString();
				string str3 = dataRow["FieldName"].ToString();
				XmlNode xmlNodes = xmlElement2.SelectSingleNode(str3);
				if (xmlNodes != null)
				{
					string str4 = str2;
					if (str2.StartsWith("="))
					{
						str4 = this.retdt.Compute(str2.Substring(1), "").ToString();
					}
					xmlNodes.InnerXml = string.Concat("<![CDATA[", str4, "]]>");
				}
			}
			if ((int)dataRowArray.Length > 0)
			{
				this.sb.Append(xmlElement2.OuterXml);
			}
			this.sb = this.sb.Replace("\r\n", " ").Replace("\t", "");
		}

		public static bool IsAppField(string fieldName)
		{
			return GetXml.SysFields.Contains(fieldName.ToLower());
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
			this.sindex = (this.Request["sindex"] == null ? "" : this.Request["sindex"]);
			this.sortdir = (string.IsNullOrEmpty(this.sortname) ? "" : string.Concat(" order by ", this.sortname, " ", this.Request["sortorder"]));
			if (this.sortdir != "")
			{
				if (this.Request["sortname"].ToLower() == "rowindex")
				{
					this.sortdir = "";
				}
			}
			this.queryid = this.Request["queryid"];
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
					GetXml getXml = this;
					getXml.condstr = string.Concat(getXml.condstr, " and ", HttpContext.Current.Server.UrlDecode(this.Request["query"]));
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
			try
			{
				string str1 = string.Concat("select TableType,ListSQL,ConnectionId,OrderField from T_E_Sys_TableInfo where tablename='", this.queryid, "'");
				DbDataReader dbDataReaders = SysDatabase.ExecuteReader(str1);
				try
				{
					if (!dbDataReaders.Read())
					{
						this.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?><rows></rows>");
						return;
					}
					else
					{
						this.tblType = dbDataReaders["TableType"].ToString();
						this.connectionId = dbDataReaders["ConnectionId"].ToString();
						this.querysql = dbDataReaders["ListSQL"].ToString();
						string str2 = "";
						if (!(this.sindex == ""))
						{
							string[] strArrays = new string[] { "select FieldName,ColTotalExp,ColFormatExp from T_E_Sys_FieldInfoExt where tablename='", this.queryid, "' and StyleIndex=", this.sindex, " and ListDisp=1 order by ColumnOrder" };
							str2 = string.Concat(strArrays);
						}
						else
						{
							str2 = string.Concat("select FieldName,ColTotalExp,ColFormatExp from T_E_Sys_FieldInfo where tablename='", this.queryid, "' and ListDisp=1 order by ColumnOrder,fieldodr ");
						}
						this.dtFields = SysDatabase.ExecuteTable(str2);
						if (this.sortdir == "")
						{
							this.sortdir = dbDataReaders["OrderField"].ToString();
							if (this.sortdir != "")
							{
								this.sortdir = string.Concat(" order by ", this.sortdir);
							}
						}
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
				this.sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
				this.sb.Append("<rows>\n");
				this.sb.AppendFormat("<page>{0}</page>\n", this.page);
				this.GetList();
				this.sb.Append("</rows>\n");
				this.Response.Write(this.sb.ToString());
			}
			catch (Exception exception)
			{
				this.Response.Write(exception.Message);
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