using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using WebBase.JZY.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.WorkAsp.News
{
    public partial class NewsRead : PageBase
    {
      

        public string newsTitle = "新闻详细页";

        public string newsHeader = "";

        public string newscontent = "";

        public string commentlist = "";

        public string fjlist = "";

        public string newsId = "";

        public NewsRead()
        {
            this.AutoRedirect = false;
        }

        private void method_0()
        {
            string str = string.Concat("select * from T_OA_News where _Autoid='", this.newsId, "'");
            DataTable dataTable = SysDatabase.ExecuteTable(str);
            if (dataTable.Rows.Count > 0)
            {
                DataRow item = dataTable.Rows[0];
                StringBuilder stringBuilder = new StringBuilder();
                this.newsTitle = item["Title"].ToString();
                object[] objArray = new object[] { item["Title"], item["Creator"], item["IssueTime"], item["ReadCount"] };
                stringBuilder.AppendFormat("{0}<span>发布人：{1}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;发布日期：{2}&nbsp;&nbsp;&nbsp;&nbsp;阅读次数：{3}&nbsp;次</span>", objArray);
                this.newsHeader = stringBuilder.ToString();
                this.newscontent = item["Content"].ToString();
                stringBuilder.Length = 0;
                string paraValue = base.GetParaValue("newsId");
                IList<AppFile> files = (new FileService()).GetFiles("T_OA_News", paraValue);
                stringBuilder.Append("<ul style='list-style-type:disc'>");
                foreach (AppFile file in files)
                {
                    stringBuilder.AppendFormat("<li style='list-style-type:disc'><a href='../../SysFolder/Common/FileDown.aspx?fileId={0}' target='_blank'>{1}</a>({2})</li>", file._AutoID, file.FactFileName, Utility.GetFriendlySize((long)file.FileSize));
                }
                stringBuilder.Append("</ul>");
                this.fjlist = stringBuilder.ToString();
                stringBuilder.Length = 0;
                str = string.Concat("select * from T_OA_Comment where AppID='", this.newsId, "' order by AddTime");
                DataTable dataTable1 = SysDatabase.ExecuteTable(str);
                int num = 1;
                foreach (DataRow row in dataTable1.Rows)
                {
                    objArray = new object[] { row["EmployeeName"], row["AddTime"], row["Content"], null };
                    int num1 = num;
                    num = num1 + 1;
                    objArray[3] = num1;
                    stringBuilder.AppendFormat("<div class=\"c_list\"><span class=\"c_writer\">评论人：{0}</span><span class=\"c_time\">{3}楼 {1}</span><p>{2}</p></div>", objArray);
                }
                this.commentlist = stringBuilder.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.newsId = base.GetParaValue("newsId");
            if (!base.IsPostBack)
            {
                this.method_0();
                if (!this.Logged)
                {
                    this.Submit1.Disabled = true;
                }
                else
                {
                    WebTools.UpdateRead(base.EmployeeID, "T_OA_News", this.newsId);
                }
                string str = string.Concat("update T_OA_News set ReadCount=IsNull(ReadCount,0) + 1 where _Autoid='", this.newsId, "'");
                SysDatabase.ExecuteNonQuery(str);
            }
        }

        protected void Submit1_ServerClick(object sender, EventArgs e)
        {
            OAComment oAComment = new OAComment(base.UserInfo)
            {
                EmployeeName = base.EmployeeName,
                AddTime = new DateTime?(DateTime.Now),
                Content = this.TextBox1.Text,
                AppID = this.newsId,
                AppName = "T_OA_News"
            };
            WebTools.InsertComment(oAComment);
            this.method_0();
        }
    }
}