using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.Permission.Access;
using System;
using System.Collections;
using System.Data;
using System.DirectoryServices;
using System.Text;
using System.Web;
using System.Web.UI;

namespace EIS.Web.SysFolder.Permission
{
    public partial class LdapGetUserXml : AdminPageBase
    {
        public int page = 0;

        public int int_0 = 0;

        public string sortname = "";

        public string sortorder = "";

        private StringBuilder stringBuilder_0 = new StringBuilder();

        public string querysql = "";

        public string querylist = "";

        public string condstr = "";

        public string sortdir = "";

        public string sindex = "";

        public string queryid = "";

        private DataTable dataTable_0 = new DataTable();

     

        protected void Page_Load(object sender, EventArgs e)
        {
            this.page = int.Parse(base.Request["page"]);
            this.int_0 = int.Parse(base.Request["rp"]);
            this.sortname = base.Request["sortname"];
            this.sortorder = base.Request["sortorder"];
            this.sindex = (base.Request["sindex"] == null ? "" : base.Request["sindex"]);
            this.sortdir = (string.IsNullOrEmpty(this.sortname) ? "" : string.Concat(" order by ", this.sortname, " ", base.Request["sortorder"]));
            if (this.sortdir != "" && base.Request["sortname"].ToLower() == "rowindex")
            {
                this.sortdir = "";
            }
            this.queryid = base.Request["queryid"];
            if (string.IsNullOrEmpty(base.Request["cryptcond"]))
            {
                this.condstr = base.Request["condition"];
            }
            this.condstr = base.Server.UrlDecode(this.condstr);
            if (!string.IsNullOrEmpty(base.Request["query"]))
            {
                if (!string.IsNullOrEmpty(this.condstr))
                {
                    LdapGetUserXml ldapGetUserXml = this;
                    ldapGetUserXml.condstr = string.Concat(ldapGetUserXml.condstr, " and ", base.Server.UrlDecode(base.Request["query"]));
                }
                else
                {
                    this.condstr = base.Server.UrlDecode(base.Request["query"]);
                }
            }
            if (string.IsNullOrEmpty(this.condstr))
            {
                this.condstr = " 1=1 ";
            }
            base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            base.Response.AppendHeader("Pragma", "no-cache");
            base.Response.AppendHeader("Content-type", "text/xml");
            LDAPConfig ldapConfig = AppSettings.Instance.LdapConfig;
            string[] serverIP = new string[] { "LDAP://", ldapConfig.ServerIP, ":", ldapConfig.ServerPort, "/", base.Request["oupath"] };
            string str = string.Concat(serverIP);
            DirectoryEntry directoryEntry = new DirectoryEntry(str, ldapConfig.Account, ldapConfig.PassWord);
            if ((directoryEntry == null ? true : directoryEntry.Children == null))
            {
                base.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?><rows></rows>");
            }
            else
            {
                _Employee __Employee = new _Employee();
                this.stringBuilder_0.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
                this.stringBuilder_0.Append("<rows>\n");
                this.stringBuilder_0.AppendFormat("<page>{0}</page>\n", this.page);
                int num = 0;
                DirectorySearcher directorySearcher = new DirectorySearcher();
                try
                {
                    directorySearcher.SearchRoot = directoryEntry;
                    directorySearcher.SearchScope = SearchScope.OneLevel;
                    directorySearcher.Filter = "(objectClass=user)";
                    foreach (SearchResult searchResult in directorySearcher.FindAll())
                    {
                        DirectoryEntry directoryEntry1 = searchResult.GetDirectoryEntry();
                        num++;
                        Guid guid = new Guid((byte[])directoryEntry1.Properties["objectGUID"].Value);
                        string str1 = guid.ToString();
                        StringBuilder stringBuilder0 = this.stringBuilder_0;
                        object[] objArray = new object[] { directoryEntry1.Properties["name"].Value.ToString(), directoryEntry1.Properties["displayName"].Value.ToString(), directoryEntry1.Properties["distinguishedName"].Value.ToString(), str1, num, directoryEntry1.Properties["sAMAccountName"].Value.ToString(), null };
                        objArray[6] = (__Employee.IsADUserExist(str1) ? "是" : "否");
                        stringBuilder0.AppendFormat("<row id=\"{3}\">\r\n                            <rowindex>{4}</rowindex>\r\n                            <cn>{0}</cn>\r\n                            <displayName>{1}</displayName>\r\n                            <distinguishedName>{2}</distinguishedName>\r\n                            <objectGUID>{3}</objectGUID>\r\n                            <sAMAccountName>{5}</sAMAccountName>\r\n                            <exist>{6}</exist>\r\n                        </row>", objArray);
                    }
                }
                finally
                {
                    if (directorySearcher != null)
                    {
                        ((IDisposable)directorySearcher).Dispose();
                    }
                }
                this.stringBuilder_0.AppendFormat("<total>{0}</total>", num);
                this.stringBuilder_0.Append("</rows>\n");
                base.Response.Write(this.stringBuilder_0.ToString());
            }
        }
    }
}