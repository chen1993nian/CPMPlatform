using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Model;
using EIS.DataModel.Service;

using EIS.WebBase.ModelLib.Model;
using EIS.WebBase.ModelLib.Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.WorkAsp.BBS
{
    public partial class TopicMain : PageBase
    {
        public StringBuilder sbTopic = new StringBuilder();

        public StringBuilder RelTopic = new StringBuilder();

        public StringBuilder sbPageNo = new StringBuilder();

        public string Topic = "";

        public string topicType = "";

        public string BizId = "";

        private int int_0 = 0;

        private int int_1 = 20;

      
        public string NewPostId
        {
            get
            {
                string str;
                str = (this.ViewState["NewPostId"] == null ? "" : this.ViewState["NewPostId"].ToString());
                return str;
            }
            set
            {
                this.ViewState["NewPostId"] = value;
            }
        }

        public string TopicId
        {
            get
            {
                string str;
                str = (this.ViewState["TopicId"] == null ? "" : this.ViewState["TopicId"].ToString());
                return str;
            }
            set
            {
                this.ViewState["TopicId"] = value;
            }
        }

        public TopicMain()
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.method_5(this.NewPostId, this.TopicId, this.TopicId, this.txtContent.Text);
            this.method_1();
            this.txtContent.Text = "";
            this.NewPostId = Guid.NewGuid().ToString();
        }

        private void method_0(string string_0)
        {
            BBSReceive bBSReceive;
            _BBSReceive __BBSReceive = new _BBSReceive();
            if (!__BBSReceive.Exists(string_0, base.EmployeeID))
            {
                bBSReceive = new BBSReceive(base.UserInfo)
                {
                    SubjectId = string_0,
                    IsRead = 1,
                    ReadTime = new DateTime?(DateTime.Now),
                    RecId = base.EmployeeID,
                    SubScribe = ""
                };
                __BBSReceive.Add(bBSReceive);
            }
            else
            {
                bBSReceive = __BBSReceive.GetModel(string_0, base.EmployeeID);
                bBSReceive.ReadTime = new DateTime?(DateTime.Now);
                __BBSReceive.Update(bBSReceive);
            }
        }

        private void method_1()
        {
            int i;
            DataRow item;
            string paraValue = base.GetParaValue("pi");
            string str = base.GetParaValue("ps");
            if (paraValue != "")
            {
                this.int_0 = int.Parse(paraValue);
            }
            if (str != "")
            {
                this.int_1 = int.Parse(str);
            }
            string str1 = string.Format("select * from T_BBS_Topic where _AutoId='{0}'", this.TopicId);
            if (this.TopicId == "")
            {
                str1 = string.Format("select * from T_BBS_Topic where BizId='{0}'", this.BizId);
            }
            DataTable dataTable = SysDatabase.ExecuteTable(str1);
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            if (dataTable.Rows.Count <= 0)
            {
                if (this.topicType != "3")
                {
                    throw new Exception("您请求的话题不存在");
                }
                str1 = string.Format("select * from T_OA_Task_Assign where _AutoId='{0}'", this.BizId);
                dataTable = SysDatabase.ExecuteTable(str1);
                if (dataTable.Rows.Count <= 0)
                {
                    throw new Exception("您请求的话题不存在");
                }
                item = dataTable.Rows[0];
                this.Topic = item["TaskName"].ToString();
                str5 = item["FileId"].ToString();
                str3 = item["_userName"].ToString();
                str2 = string.Format("{0:yyyy-MM-dd HH:mm}", item["_CreateTime"]);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("<table class='dtTopic'><tbody>", new object[0]);
                stringBuilder.AppendFormat("<tr><td class='labelTd'>任务标题</td><td>{0}</td></tr>", item["TaskName"]);
                stringBuilder.AppendFormat("<tr><td class='labelTd'>任务编号</td><td>{0}</td></tr>", item["TaskCode"]);
                stringBuilder.AppendFormat("<tr><td class='labelTd'>项目名称</td><td>{0}</td></tr>", item["ProName"]);
                stringBuilder.AppendFormat("<tr><td class='labelTd'>业务分类</td><td>{0}</td></tr>", item["BizType"]);
                stringBuilder.AppendFormat("<tr><td class='labelTd'>任务来源</td><td>{0}</td></tr>", item["TaskLY"]);
                stringBuilder.AppendFormat("<tr><td class='labelTd'>重要程序</td><td>{0}</td></tr>", item["Importance"]);
                stringBuilder.AppendFormat("<tr><td class='labelTd'>任务负责人</td><td>{0}</td></tr>", item["TaskFzMan"]);
                stringBuilder.AppendFormat("<tr><td class='labelTd'>过程检查点</td><td>{0}</td></tr>", item["CheckPoint"]);
                stringBuilder.AppendFormat("<tr><td class='labelTd'>期待完成时间</td><td>{0}</td></tr>", item["ToEndTime"]);
                stringBuilder.AppendFormat("<tr><td class='labelTd'>相关人员</td><td>{0}</td></tr>", item["RelMan"]);
                stringBuilder.AppendFormat("<tr><td class='labelTd'>任务描述</td><td>{0}</td></tr>", item["TaskMemo"]);
                stringBuilder.AppendFormat("</tbody></table>", new object[0]);
                this.method_4(stringBuilder.ToString(), item);
                str4 = stringBuilder.ToString();
            }
            else
            {
                item = dataTable.Rows[0];
                this.TopicId = item["_AutoId"].ToString();
                this.Topic = item["Title"].ToString();
                str4 = item["Content"].ToString();
                str3 = item["_userName"].ToString();
                str5 = item["AttachId"].ToString();
                str2 = string.Format("{0:yyyy-MM-dd HH:mm}", item["_CreateTime"]);
            }
            StringBuilder stringBuilder1 = this.sbTopic;
            object[] objArray = new object[] { this.method_3(str3), str4, str2, this.method_7("T_OA_Task_Assign", str5) };
            stringBuilder1.AppendFormat("<div class='post'>\r\n                <table class='dtPost'><tbody>\r\n                <tr><td class='post_l' rowspan='2'>{0}</td><td class='post_r'>{1}{3}</td></tr>\r\n                <tr><td class='post_other'>\r\n                <span>1楼</span><span>{2}</span><a class='postLink' href='#dtReply'>回&nbsp;复</a>\r\n                </td></tr>\r\n                </tbody></table></div>", objArray);
            str1 = string.Format("select * from T_BBS_Reply where TopicId='{0}' order by ReplyOrder", this.TopicId);
            DataTable dataTable1 = SysDatabase.ExecuteTable(str1);
            DataRow[] dataRowArray = dataTable1.Select(string.Concat("ReferId='", this.TopicId, "'"), "ReplyOrder");
            int length = (int)dataRowArray.Length;
            int int0 = this.int_0 * this.int_1;
            int num = (this.int_0 + 1) * this.int_1;
            num = Math.Min(num, length);
            for (i = int0; i < num; i++)
            {
                DataRow dataRow = dataRowArray[i];
                str3 = dataRow["_userName"].ToString();
                string str6 = dataRow["_AutoId"].ToString();
                StringBuilder stringBuilder2 = this.sbTopic;
                objArray = new object[] { this.method_3(str3), dataRow["ReplyText"], dataRow["ReplyOrder"], dataRow["_CreateTime"], this.method_7("", str6) };
                stringBuilder2.AppendFormat("<div class='post'><table class='dtPost'><tbody>\r\n                    <tr><td class='post_l' rowspan='2'>{0}</td><td class='post_r'>{1}{4}</td></tr>\r\n                    <tr><td class='post_other'><span>{2}楼</span><span>{3:yyyy-MM-dd HH:mm}</span><a class='postLink' href='#dtReply'>回&nbsp;复</a></td></tr></tbody></table>\r\n                    </div>", objArray);
            }
            this.sbTopic.Append("<div class='clear'></div>");
            this.method_2(this.int_0, this.int_1, length);
            str1 = string.Format("select top 10 t._AutoId,t.Title,t.LastUpdateTime,t.ReplyCount,r.ReadTime\r\n                from T_BBS_Topic t left join T_BBS_Receive r on t._AutoID=r.SubjectId and r.RecId='{0}'\r\n                where charIndex('{1}',ToDeptId)>0 or t._UserName='{0}' or charIndex('{0}',ToUserId)>0 \r\n                order by t._createTime desc  ", base.EmployeeID, base.UserInfo.DeptId);
            DataTable dataTable2 = SysDatabase.ExecuteTable(str1);
            for (i = 0; i < dataTable2.Rows.Count; i++)
            {
                item = dataTable2.Rows[i];
                if (item["_AutoId"].ToString() != this.TopicId)
                {
                    DateTime dateTime = Convert.ToDateTime(item["LastUpdateTime"]);
                    bool flag = true;
                    if (item["ReadTime"] != DBNull.Value)
                    {
                        DateTime dateTime1 = Convert.ToDateTime(item["ReadTime"]);
                        flag = dateTime.CompareTo(dateTime1) > 0;
                    }
                    StringBuilder relTopic = this.RelTopic;
                    objArray = new object[] { item["_AutoId"], item["Title"], item["ReplyCount"], null };
                    objArray[3] = (flag ? "<img src='../../Img/new_a.gif'>" : "");
                    relTopic.AppendFormat("<li><a href='TopicMain.aspx?topicId={0}' target='_self'>{1}</a><span>（{2}个回复）{3}</span></li>", objArray);
                }
            }
        }

        private void method_2(int int_2, int int_3, int int_4)
        {
            int num;
            if (int_4 > int_3)
            {
                num = (int_4 % int_3 > 0 ? int_4 / int_3 + 1 : int_4 / int_3);
            }
            else
            {
                num = 1;
            }
            int num1 = num;
            int num2 = (int_2 == 0 ? 0 : int_2 - 1);
            StringBuilder stringBuilder = this.sbPageNo;
            object[] int3 = new object[] { num2, int_3, this.TopicId, this.topicType };
            stringBuilder.AppendFormat("<ul><li class='previous'><a href='TopicMain.aspx?t={3}&topicId={2}&pi={0}&ps={1}' target='_self'>上一页</a></li>", int3);
            for (int i = 0; i < num1; i++)
            {
                StringBuilder stringBuilder1 = this.sbPageNo;
                int3 = new object[] { i + 1, i, int_3, null, null, null };
                int3[3] = (i == int_2 ? " active" : "");
                int3[4] = this.TopicId;
                int3[5] = this.topicType;
                stringBuilder1.AppendFormat("<li class='{3}'><a class='page' href='TopicMain.aspx?t={5}&topicId={4}&pi={1}&ps={2}' target='_self'>{0}</a></li>", int3);
            }
            int num3 = (int_2 == num1 - 1 ? int_2 : int_2 + 1);
            StringBuilder stringBuilder2 = this.sbPageNo;
            int3 = new object[] { num3, int_3, this.TopicId, this.topicType };
            stringBuilder2.AppendFormat("<li class='next'><a href='TopicMain.aspx?t={3}&topicId={2}&pi={0}&ps={1}' target='_self'>下一页</a></li>", int3);
            this.sbPageNo.AppendFormat("<li class='rectotal'>共{0}条</li></ul>", int_4);
        }

        private string method_3(string string_0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<ul class='author'>");
            DataTable dataTable = SysDatabase.ExecuteTable(string.Format("select d.*,e.photo from T_E_Org_DeptEmployee d inner join T_E_Org_Employee e on d.EmployeeId=e._AutoId where d.EmployeeId='{0}'", string_0));
            if (dataTable.Rows.Count > 0)
            {
                DataRow item = dataTable.Rows[0];
                string str = item["EmployeeName"].ToString();
                string str1 = item["photo"].ToString();
                if (str1 != "")
                {
                    stringBuilder.AppendFormat("<li class='icon'><div class='li_relative'><a href='#'><img src='../../Sysfolder/Common/FileDown.aspx?appid={0}'/></a></div></li>", str1);
                }
                else
                {
                    stringBuilder.AppendFormat("<li class='icon'><div class='li_relative'><a href='#'><img src='../../img/bbs/head_80.jpg'/></a></div></li>", new object[0]);
                }
                stringBuilder.AppendFormat("<li class='alias'><a href='#'>{0}（{1}）</a></li>", str, item["DeptName"]);
            }
            stringBuilder.Append("</ul>");
            return stringBuilder.ToString();
        }

        private void method_4(string string_0, DataRow dataRow_0)
        {
            _BBSTopic __BBSTopic = new _BBSTopic();
            BBSTopic bBSTopic = new BBSTopic(base.UserInfo)
            {
                _AutoID = Guid.NewGuid().ToString(),
                _UserName = dataRow_0["_UserName"].ToString(),
                Title = dataRow_0["TaskName"].ToString(),
                EmpName = dataRow_0["SubmitMan"].ToString(),
                Content = string_0,
                AttachId = dataRow_0["FileId"].ToString(),
                ToUserId = dataRow_0["RelManID"].ToString(),
                ToUserName = dataRow_0["RelMan"].ToString(),
                DeptName = dataRow_0["SubmitDept"].ToString(),
                StartTime = new DateTime?(DateTime.Now),
                LastUpdateTime = new DateTime?(DateTime.Now),
                BBSType = "3",
                State = "是",
                Enable = "是",
                ReplyCount = 0,
                BizId = dataRow_0["_AutoID"].ToString(),
                BizName = "T_OA_Task_Assign"
            };
            __BBSTopic.Add(bBSTopic);
            this.TopicId = bBSTopic._AutoID;
        }

        private void method_5(string newPostId, string topicId, string referId, string post)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Insert T_BBS_Reply (\r\n \t\t\t\t\t    _AutoID,\r\n\t\t\t\t\t    _UserName,\r\n\t\t\t\t\t    _OrgCode,\r\n\t\t\t\t\t    _CreateTime,\r\n\t\t\t\t\t    _UpdateTime,\r\n\t\t\t\t\t    _IsDel,\r\n                        _CompanyId,\r\n                        TopicId,\r\n\t\t\t\t\t    ReferId,\r\n                        ReplyText,\r\n                        ReplyOrder\r\n                        \r\n\t\t\t    ) values(\r\n\t\t\t\t\t    @_AutoID,\r\n\t\t\t\t\t    @_UserName,\r\n\t\t\t\t\t    @_OrgCode,\r\n\t\t\t\t\t    @_CreateTime,\r\n\t\t\t\t\t    @_UpdateTime,\r\n\t\t\t\t\t    @_IsDel,\r\n\t\t\t\t\t    @_CompanyId,\r\n\r\n                        @TopicId,\r\n\t\t\t\t\t    @ReferId,\r\n                        @ReplyText,\r\n                        @ReplyOrder\r\n\t\t\t    );\r\n            Update T_BBS_Topic Set LastUpdateTime=getdate(),ReplyCount=IsNull(ReplyCount,0)+1 where _autoId=@TopicId;\r\n            ");
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
            SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, newPostId);
            SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, base.EmployeeID);
            SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, base.OrgCode);
            SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, DateTime.Now);
            SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
            SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, 0);
            SysDatabase.AddInParameter(sqlStringCommand, "@_CompanyId", DbType.String, base.CompanyId);
            SysDatabase.AddInParameter(sqlStringCommand, "@TopicId", DbType.String, topicId);
            SysDatabase.AddInParameter(sqlStringCommand, "@ReferId", DbType.String, referId);
            SysDatabase.AddInParameter(sqlStringCommand, "@ReplyText", DbType.String, post);
            SysDatabase.AddInParameter(sqlStringCommand, "@ReplyOrder", DbType.Int32, this.method_6(referId));
            SysDatabase.ExecuteNonQuery(sqlStringCommand);
        }

        private int method_6(string string_0)
        {
            int num;
            object obj = SysDatabase.ExecuteScalar(string.Format("select max(ReplyOrder) from T_BBS_Reply where TopicId='{0}'", string_0));
            num = (obj == DBNull.Value ? 2 : int.Parse(obj.ToString()) + 1);
            return num;
        }

        private string method_7(string appName, string appId)
        {
            string str;
            StringBuilder stringBuilder = new StringBuilder();
            IList<AppFile> files = (new FileService()).GetFiles(appName, appId);
            if (files.Count != 0)
            {
                stringBuilder.Append("<div class='fileList'><ul style='list-style-type:disc'>");
                int num = 1;
                foreach (AppFile file in files)
                {
                    object[] factFileName = new object[] { file._AutoID, file.FactFileName, Utility.GetFriendlySize((long)file.FileSize), null };
                    int num1 = num;
                    num = num1 + 1;
                    factFileName[3] = num1;
                    stringBuilder.AppendFormat("<li style='list-style-type:disc'><a href='../../SysFolder/Common/FileDown.aspx?fileId={0}' target='_blank'>{3}、{1}</a>&nbsp;({2})</li>", factFileName);
                }
                stringBuilder.Append("</ul></div>");
                str = stringBuilder.ToString();
            }
            else
            {
                str = "";
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.BizId = base.GetParaValue("BizId");
            this.topicType = base.GetParaValue("t");
            if (!base.IsPostBack)
            {
                this.NewPostId = Guid.NewGuid().ToString();
                this.TopicId = base.GetParaValue("topicId");
                this.method_1();
                this.method_0(this.TopicId);
            }
        }

   
    }
}