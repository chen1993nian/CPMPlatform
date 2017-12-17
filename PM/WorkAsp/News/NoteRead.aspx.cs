using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using EIS.Permission.Service;
using WebBase.JZY.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;

namespace EIS.Web.WorkAsp.News
{
    public partial class NoteRead : PageBase
    {
        public string newstitle = "";

        public string total = "";

        public string newscontent = "";

        public string commentlist = "";

        public string fjlist = "";

        public string newsId = "";

        public int Readed = 0;

        public int Unread = 0;

        public string ReadedList = "";

        public string UnReadList = "";

        public string fileList = "";

        public string showReadBtn = "display:none";
        public string showComment = "";
     

        private void method_0()
        {
            DataTable appRead;
            DataRow row = null;
            string str = string.Concat("select * from T_OA_Note where _Autoid='", this.newsId, "'");
            DataTable dataTable = SysDatabase.ExecuteTable(str);
            if (dataTable.Rows.Count > 0)
            {
                DataRow item = dataTable.Rows[0];
                StringBuilder stringBuilder = new StringBuilder();
                object[] objArray = new object[] { item["Title"], item["Creator"], item["IssueTime"], item["ReadCount"] };
                stringBuilder.AppendFormat("{0}<span>发布人：{1}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;发布日期：{2:yyyy-MM-dd HH:mm}&nbsp;&nbsp;&nbsp;&nbsp;阅读次数：{3}&nbsp;次</span>", objArray);
                this.newstitle = stringBuilder.ToString();
                this.newscontent = item["Content"].ToString();
                stringBuilder.Length = 0;
                string paraValue = base.GetParaValue("newsId");
                string str1 = "T_OA_Note";
                FileService fileService = new FileService();
                IList<AppFile> files = fileService.GetFiles(str1, item["attachId"].ToString());
                stringBuilder.Append("<ul style='list-style-type:disc'>");
                foreach (AppFile file in files)
                {
                    stringBuilder.AppendFormat("<li style='list-style-type:disc'><a href='../../SysFolder/Common/FileDown.aspx?fileId={0}' target='_blank'>{1}</a>({2})</li>", file._AutoID, file.FactFileName, Utility.GetFriendlySize((long)file.FileSize));
                }
                stringBuilder.Append("</ul>");
                this.fjlist = stringBuilder.ToString();
                stringBuilder.Length = 0;
                str = string.Concat("select * from T_OA_Comment where AppID='", this.newsId, "' order by AddTime desc ");
                DataTable dataTable1 = SysDatabase.ExecuteTable(str);
                if ((dataTable1 != null) && (dataTable1.Rows.Count > 0))
                {
                    int num = dataTable1.Rows.Count;
                    foreach (DataRow dataRow in dataTable1.Rows)
                    {
                        objArray = new object[] { dataRow["EmployeeName"], dataRow["AddTime"], dataRow["Content"], null };
                        int num1 = num;
                        num = num1 - 1;
                        objArray[3] = num1;
                        stringBuilder.AppendFormat("<div class=\"c_list\"><span class=\"c_writer\">评论人：{0}</span><span class=\"c_time\">{3}楼 {1}</span><p>{2}</p></div>", objArray);
                    }
                }
                this.commentlist = stringBuilder.ToString();
                stringBuilder.Length = 0;
                string str2 = item["ScopeId"].ToString();
                item["OrgScopeId"].ToString();
                if (str2.Length <= 0)
                {
                    appRead = WebTools.GetAppRead(str1, paraValue);
                    foreach (DataRow rowa in appRead.Rows)
                    {
                        stringBuilder.AppendFormat("<span title='{0}'>{1}</span>，", rowa["_CreateTime"], rowa["EmployeeName"]);
                    }
                    this.Readed = appRead.Rows.Count;
                    if (this.Readed > 0)
                    {
                        stringBuilder.Length = stringBuilder.Length - 1;
                        this.ReadedList = stringBuilder.ToString();
                    }
                    this.total = string.Format("{0}人已读", this.Readed);
                }
                else
                {
                    appRead = WebTools.GetAppRead(str1, paraValue);
                    foreach (DataRow row1 in appRead.Rows)
                    {
                        stringBuilder.AppendFormat("<span title='{0}'>{1}</span>，", row1["_CreateTime"], row1["EmployeeName"]);
                    }
                    this.Readed = appRead.Rows.Count;
                    if (this.Readed > 0)
                    {
                        stringBuilder.Length = stringBuilder.Length - 1;
                        this.ReadedList = stringBuilder.ToString();
                    }
                    stringBuilder.Length = 0;
                    string[] strArrays = str2.Split(new char[] { ',' });
                    for (int i = 0; i < (int)strArrays.Length; i++)
                    {
                        string str3 = strArrays[i];
                        if ((int)appRead.Select(string.Concat("employeeId='", str3, "'")).Length == 0)
                        {
                            stringBuilder.AppendFormat("{0}，", EmployeeService.GetEmployeeName(str3));
                            NoteRead unread = this;
                            unread.Unread = unread.Unread + 1;
                        }
                    }
                    if (this.Unread > 0)
                    {
                        stringBuilder.Length = stringBuilder.Length - 1;
                        this.UnReadList = stringBuilder.ToString();
                    }
                    if (this.Readed + this.Unread > 5)
                    {
                        this.total = string.Format("{0}人已读，{1}人未读", this.Readed, this.Unread);
                    }
                }
                ShowReadBtn();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.newsId = base.GetParaValue("newsId");
            if (!base.IsPostBack)
            {
                this.method_0();
                WebTools.UpdateRead(base.EmployeeID, "T_OA_Note", this.newsId);
                string str = string.Concat("update T_OA_Note set ReadCount= IsNull(ReadCount,0) + 1 where _Autoid='", this.newsId, "'");
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
                AppName = "T_OA_Note"
            };
            WebTools.InsertComment(oAComment);
            this.method_0();
        }

        /// <summary>
        /// 显示【我已完整阅读并同意以上条款】按钮.
        /// </summary>
        private void ShowReadBtn()
        {
            string str = string.Concat("select * from T_OA_Note where _Autoid=@newid");
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
            SysDatabase.AddInParameter(sqlStringCommand, "newid", DbType.String, this.newsId);
            DataTable notedt = SysDatabase.ExecuteTable(sqlStringCommand);
            if ((notedt!=null) && (notedt.Rows.Count>0)){
                if (notedt.Columns.Contains("showReadBtn")){
                    if (notedt.Rows[0]["showReadBtn"].ToString() == "1")
                    {
                        this.showReadBtn = "";

                        string strRead = "select * from T_OA_Comment where AppName='T_OA_Note' and AppID=@newid and _UserName=@EmployeeId";
                        DbCommand readCommand = SysDatabase.GetSqlStringCommand(strRead);
                        SysDatabase.AddInParameter(readCommand, "newid", DbType.String, this.newsId);
                        SysDatabase.AddInParameter(readCommand, "EmployeeId", DbType.String, this.Session["EmployeeId"].ToString());
                        DataTable noteReaddt = SysDatabase.ExecuteTable(readCommand);
                        if ((noteReaddt != null) && (noteReaddt.Rows.Count > 0))
                        {
                            Button1.CssClass = "AllReadBtnGreen";
                        }
                    }
                }
                if (notedt.Columns.Contains("showComment"))
                {
                    showComment = "uhide";
                    if (notedt.Rows[0]["showComment"].ToString() == "1")
                    {
                        showComment = "";
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            OAComment oAComment = new OAComment(base.UserInfo)
            {
                EmployeeName = base.EmployeeName,
                AddTime = new DateTime?(DateTime.Now),
                Content = "我已完整阅读并同意以上条款",
                AppID = this.newsId,
                AppName = "T_OA_Note"
            };
            WebTools.InsertComment(oAComment);
            this.method_0();
            base.ClientScript.RegisterStartupScript(base.GetType(), "", string.Concat("_appClose();"), true);
        }

    }
}