using EIS.DataAccess;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Xml;

namespace EIS.Web.SysFolder.Common
{
    public class AutoComplete : IHttpHandler, IRequiresSessionState
    {
        public string sortname = "";

        public string sortorder = "";

        private StringBuilder stringBuilder_0 = new StringBuilder();

        public string querysql = "";

        public string suggestionsdef = "";

        public string datadef = "";

        public string condstr = "";

        public string sortdir = "";

        private DataTable dataTable_0 = new DataTable();

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public AutoComplete()
        {
        }

        public string GetDetailAfterListSub(string loopbody, DataRow dataRow_0, int rnum)
        {
            string str = loopbody.Replace("[#ROWNUM#]", rnum.ToString());
            foreach (Match match in (new Regex("\\{(?<fld>(\\w)+?)}")).Matches(str))
            {
                string str1 = match.Value.Substring(1, match.Value.Length - 2);
                string str2 = dataRow_0[str1].ToString();
                if ((typeof(DateTime) != dataRow_0.Table.Columns[str1].DataType ? false : dataRow_0[str1] != DBNull.Value))
                {
                    str2 = ((DateTime)dataRow_0[str1]).ToString("yyyy-MM-dd");
                }
                if (!dataRow_0.Table.Columns.Contains(str1))
                {
                    continue;
                }
                str = str.Replace(match.Value, str2);
            }
            return str;
        }

        public string GetList(string loopbody)
        {
            StringBuilder stringBuilder = new StringBuilder();
            this.querysql = this.querysql.Replace("|^condition^|", this.condstr).Replace("|^sortdir^|", this.sortdir).Replace("\r\n", " ").Replace("\t", "");
            this.dataTable_0 = SysDatabase.ExecuteTable(this.querysql);
            for (int i = 0; i < this.dataTable_0.Rows.Count; i++)
            {
                stringBuilder.Append(string.Concat(this.GetDetailAfterListSub(loopbody, this.dataTable_0.Rows[i], i + 1), ","));
            }
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Length = stringBuilder.Length - 1;
            }
            return stringBuilder.ToString();
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            this.sortname = request["sortname"];
            this.sortorder = request["sortorder"];
            this.sortdir = (string.IsNullOrEmpty(this.sortname) ? "" : string.Concat(" order by ", this.sortname, " ", request["sortorder"]));
            if (this.sortdir != "" && request["sortname"].ToLower() == "rowindex")
            {
                this.sortdir = "";
            }
            string item = request["queryid"];
            this.condstr = request["condition"];
            this.condstr = context.Server.UrlDecode(this.condstr);
            if (!string.IsNullOrEmpty(request["query"]))
            {
                if (!string.IsNullOrEmpty(this.condstr))
                {
                    AutoComplete autoComplete = this;
                    string str = autoComplete.condstr;
                    string[] strArrays = new string[] { str, " and ", request["querykey"], " like '%", context.Server.UrlDecode(request["query"]), "%'" };
                    autoComplete.condstr = string.Concat(strArrays);
                }
                else
                {
                    this.condstr = string.Concat(request["querykey"], " like '%", context.Server.UrlDecode(request["query"]), "%'");
                }
            }
            if (string.IsNullOrEmpty(this.condstr))
            {
                this.condstr = " 1=1 ";
            }
            string str1 = context.Server.MapPath("AutoCompleteData.xml");
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(str1);
            XmlElement documentElement = xmlDocument.DocumentElement;
            XmlNode xmlNodes = documentElement.SelectSingleNode(string.Concat("/root/queryobj[@queryid='", item, "']"));
            if (xmlNodes == null)
            {
                context.Response.Write(string.Format("{query:'{0}',suggestions:[],data:[]}", request["query"]));
            }
            else
            {
                this.querysql = xmlNodes.SelectSingleNode("querysql").InnerText;
                this.suggestionsdef = xmlNodes.SelectSingleNode("suggestions").InnerText;
                this.datadef = xmlNodes.SelectSingleNode("data").InnerText;
                if ((xmlNodes.SelectSingleNode("querysql").Attributes["sortdir"] == null ? false : this.sortdir == ""))
                {
                    this.sortdir = string.Concat(" order by ", xmlNodes.SelectSingleNode("querysql").Attributes["sortdir"].Value);
                }
                this.stringBuilder_0.AppendFormat("{{query:'{0}',suggestions:[{1}],data:[{2}]}}", request["query"], this.GetList(this.suggestionsdef), this.GetList(this.datadef));
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.ContentType = "application/json";
                context.Response.Write(this.stringBuilder_0.ToString());
            }
        }
    }
}