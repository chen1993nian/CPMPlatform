using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Service;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using System.Xml;

namespace Studio.JZY.Doc
{
    public class GetList : IHttpHandler, IRequiresSessionState
    {
        private readonly static string string_0;

        private int int_0 = 0;

        private int int_1 = 0;

        private string string_1 = "";

        private string string_2 = "";

        private StringBuilder stringBuilder_0 = new StringBuilder();

        private string string_3 = "";

        private string string_4 = "";

        private string string_5 = "";

        private string string_6 = "";

        private string string_7 = "";

        private XmlNode xmlNode_0 = null;

        private DataTable dataTable_0 = new DataTable();

        private HttpRequest httpRequest_0;

        private HttpResponse httpResponse_0;

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        static GetList()
        {
            GetList.string_0 = Guid.NewGuid().ToString();
        }

    

        private void method_0()
        {
            int i;
            int fileLimit;
            this.string_3 = this.string_3.Replace("|^condition^|", this.string_5).Replace("|^sortdir^|", this.string_6).Replace("\r\n", " ").Replace("\t", "");
            string item = this.httpRequest_0["DefaultValue"];
            if (item != null)
            {
                item = HttpContext.Current.Server.UrlDecode(item);
                foreach (Match match in (new Regex("\"(?<fld>[^,]+?)\":\"?(?<val>[^,\"}]+)\"?")).Matches(item))
                {
                    string value = match.Groups["fld"].Value;
                    string str = match.Groups["val"].Value;
                    this.string_3 = this.string_3.Replace(value, str);
                }
            }
            this.string_3 = this.method_2(this.string_3);
            this.dataTable_0 = SysDatabase.ExecuteTable(this.string_3);
            string str1 = HttpContext.Current.Session["EmployeeId"].ToString();
            DataTable dataTable = this.dataTable_0.Clone();
            for (i = 0; i < this.dataTable_0.Rows.Count; i++)
            {
                DataRow dataRow = this.dataTable_0.Rows[i];
                string str2 = dataRow["filetype"].ToString();
                string str3 = dataRow["fileId"].ToString();
                if (str2 != "")
                {
                    fileLimit = FolderService.GetFileLimit(str3, str1);
                    if (fileLimit >= 1)
                    {
                        dataRow["limit"] = fileLimit;
                        dataTable.LoadDataRow(dataRow.ItemArray, true);
                    }
                }
                else
                {
                    fileLimit = FolderService.GetFolderLimit(str3, str1);
                    if (fileLimit >= 1)
                    {
                        dataRow["limit"] = fileLimit;
                        dataTable.LoadDataRow(dataRow.ItemArray, true);
                    }
                }
            }
            this.stringBuilder_0.AppendFormat("<total>{0}</total>\n", dataTable.Rows.Count);
            this.dataTable_0 = dataTable;
            string string4 = this.string_4;
            i = (this.int_0 - 1) * this.int_1;
            while (true)
            {
                if ((i >= this.dataTable_0.Rows.Count ? true : i >= this.int_0 * this.int_1))
                {
                    break;
                }
                this.stringBuilder_0.Append(this.method_1(string4, this.dataTable_0.Rows[i], i + 1));
                i++;
            }
            if (this.xmlNode_0.FirstChild.SelectNodes("*[@compute]").Count > 0)
            {
                DataRow dataRow1 = this.dataTable_0.NewRow();
                foreach (XmlNode xmlNodes in this.xmlNode_0.FirstChild.SelectNodes("*[@compute]"))
                {
                    string value1 = xmlNodes.Attributes["compute"].Value;
                    string name = xmlNodes.Name;
                    if (!this.dataTable_0.Columns.Contains(name))
                    {
                        continue;
                    }
                    if (xmlNodes.Attributes["dummy"] != null)
                    {
                        dataRow1[name] = value1;
                    }
                    else
                    {
                        dataRow1[name] = this.dataTable_0.Compute(value1, "");
                    }
                }
                this.stringBuilder_0.Append(this.method_1(string4, dataRow1, 0));
            }
        }

        private string method_1(string string_8, DataRow dataRow_0, int int_2)
        {
            Match match = null;
            string value;
            string str;
            DateTime item;
            string str1 = string_8.Replace("[#ROWNUM#]", int_2.ToString());
            foreach (Match matchA in (new Regex("\\[(?<fld>(\\w)+?)]")).Matches(str1))
            {
                value = matchA.Value.Substring(1, matchA.Value.Length - 2);
                str = dataRow_0[value].ToString();
                if ((typeof(DateTime) != dataRow_0.Table.Columns[value].DataType ? false : dataRow_0[value] != DBNull.Value))
                {
                    item = (DateTime)dataRow_0[value];
                    str = item.ToString("yyyy-MM-dd");
                }
                if (!dataRow_0.Table.Columns.Contains(value))
                {
                    continue;
                }
                str1 = str1.Replace(match.Value, str);
            }
            foreach (Match match1 in (new Regex("{(\\w+):?(.*?)}")).Matches(str1))
            {
                value = match1.Groups[1].Value;
                string value1 = match1.Groups[2].Value;
                str = "";
                if (!dataRow_0.Table.Columns.Contains(value))
                {
                    str1 = str1.Replace(match1.Value, "<![CDATA[]]>");
                }
                else
                {
                    str = dataRow_0[value].ToString();
                    if ((typeof(DateTime) != dataRow_0.Table.Columns[value].DataType ? false : dataRow_0[value] != DBNull.Value))
                    {
                        if (value1.Trim() == "")
                        {
                            value1 = "yyyy-MM-dd";
                        }
                        item = (DateTime)dataRow_0[value];
                        str = item.ToString(value1);
                    }
                    str1 = str1.Replace(match1.Value, string.Concat("<![CDATA[", str, "]]>"));
                }
            }
            string str2 = str1.Insert(str1.IndexOf(">") + 1, string.Concat("<rowindex>", (int_2 == 0 ? "" : int_2.ToString()), "</rowindex>"));
            return str2;
        }

        private string method_2(string string_8)
        {
            foreach (Match match in (new Regex("\\[!(.*?)!\\]")).Matches(string_8))
            {
                string value = match.Groups[1].Value;
                try
                {
                    if (HttpContext.Current.Session[value] != null)
                    {
                        string_8 = string_8.Replace(match.Value, HttpContext.Current.Session[value].ToString());
                    }
                }
                catch
                {
                    string_8 = string_8.Replace(match.Value, "");
                }
            }
            return string_8;
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["UserName"] == null)
            {
                throw new Exception("会话过期！");
            }
            string str = context.Session["UserName"].ToString();
            this.httpRequest_0 = context.Request;
            this.httpResponse_0 = context.Response;
            this.int_0 = int.Parse(this.httpRequest_0["page"]);
            this.int_1 = int.Parse(this.httpRequest_0["rp"]);
            this.string_1 = this.httpRequest_0["sortname"];
            this.string_2 = this.httpRequest_0["sortorder"];
            this.string_6 = (string.IsNullOrEmpty(this.string_1) ? "" : string.Concat(" order by ", this.string_1, " ", this.httpRequest_0["sortorder"]));
            if (this.string_6 != "" && this.httpRequest_0["sortname"].ToLower() == "rowindex")
            {
                this.string_6 = "";
            }
            string item = this.httpRequest_0["queryid"];
            if (!string.IsNullOrEmpty(this.httpRequest_0["cryptcond"]))
            {
                this.string_5 = Security.Decrypt(this.httpRequest_0["cryptcond"], str);
            }
            else
            {
                this.string_5 = this.httpRequest_0["condition"];
            }
            this.string_5 = HttpContext.Current.Server.UrlDecode(this.string_5);
            if (!string.IsNullOrEmpty(this.httpRequest_0["query"]))
            {
                if (!string.IsNullOrEmpty(this.string_5))
                {
                    GetList getList = this;
                    getList.string_5 = string.Concat(getList.string_5, " and ", HttpContext.Current.Server.UrlDecode(this.httpRequest_0["query"]));
                }
                else
                {
                    this.string_5 = HttpContext.Current.Server.UrlDecode(this.httpRequest_0["query"]);
                }
            }
            if (!string.IsNullOrEmpty(this.string_5))
            {
                this.string_5 = this.string_5.Replace("[QUOTES]", "'");
            }
            else
            {
                this.string_5 = " 1=1 ";
            }
            this.httpResponse_0.Cache.SetCacheability(HttpCacheability.NoCache);
            this.httpResponse_0.AppendHeader("Pragma", "no-cache");
            this.httpResponse_0.AppendHeader("Content-type", "text/xml");
            XmlDocument xmlDocument = new XmlDocument();
            string str1 = HttpContext.Current.Server.MapPath("~/App_Data/Data.xml");
            if (HttpContext.Current.Cache[GetList.string_0] == null)
            {
                xmlDocument.Load(str1);
                CacheDependency cacheDependency = new CacheDependency(str1);
                HttpRuntime.Cache.Insert(GetList.string_0, xmlDocument, cacheDependency);
            }
            else
            {
                xmlDocument = HttpContext.Current.Cache[GetList.string_0] as XmlDocument;
            }
            XmlElement documentElement = xmlDocument.DocumentElement;
            XmlNode xmlNodes = documentElement.SelectSingleNode(string.Concat("/queryobjs/queryobj[@queryid='", item, "']"));
            if (xmlNodes == null)
            {
                this.httpResponse_0.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?><rows></rows>");
            }
            else
            {
                this.xmlNode_0 = xmlNodes.SelectSingleNode("querylist");
                XmlNode xmlNodes1 = xmlNodes.SelectSingleNode("querysql");
                this.string_3 = xmlNodes1.InnerText;
                this.string_4 = this.xmlNode_0.InnerXml;
                if ((xmlNodes1.Attributes["sortdir"] == null ? false : this.string_6 == ""))
                {
                    this.string_6 = string.Concat(" order by ", xmlNodes1.Attributes["sortdir"].Value);
                }
                if (xmlNodes1.Attributes["distinct"] != null)
                {
                    this.string_7 = xmlNodes1.Attributes["distinct"].Value;
                }
                this.stringBuilder_0.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
                this.stringBuilder_0.Append("<rows>\n");
                this.stringBuilder_0.AppendFormat("<page>{0}</page>\n", this.int_0);
                this.method_0();
                this.stringBuilder_0.Append("</rows>\n");
                this.httpResponse_0.Write(this.stringBuilder_0.ToString());
            }
        }
    }
}