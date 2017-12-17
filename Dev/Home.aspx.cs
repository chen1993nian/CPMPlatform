using EIS.AppBase;
using EIS.AppModel.Service;
using EIS.DataAccess;
using EIS.DataModel.Service;
using EIS.Web.ModelLib.Model;
using EIS.Web.ModelLib.Service;
using WebBase.JZY.Tools;
using EIS.WorkFlow.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.SessionState;
using System.Web.UI;

namespace Studio.JZY
{
    public partial class Home : PageBase
    {
        public StringBuilder sbNews = new StringBuilder();

        public StringBuilder sbNote = new StringBuilder();

        public StringBuilder sbNote1 = new StringBuilder();

        public StringBuilder sbNote2 = new StringBuilder();

        public StringBuilder sbToDo = new StringBuilder();

        public StringBuilder sbCalendar = new StringBuilder();

        public StringBuilder sbMsg = new StringBuilder();

        public StringBuilder sbMeeting = new StringBuilder();

        public StringBuilder sbLeaderToDo = new StringBuilder();

        public StringBuilder sbLabel = new StringBuilder();

        public StringBuilder sbSurvey = new StringBuilder();

        public StringBuilder sbFile = new StringBuilder();

        public StringBuilder sbFileLabel = new StringBuilder();

        public string iToDo = "";

        public string iNews = "";

        public string iNote = "";

        public string iNote1 = "";

        public string iNote2 = "";

        public string iMsg = "";

        public string iSchedule = "";

        public string iMeeting = "";

        public string iFile = "";

        public string iSurvey = "";

       

        private string method_0(string string_0, int int_0)
        {
            string str;
            if (string_0 != "")
            {
                str = (string_0.Length <= int_0 ? string_0 : string.Concat(string_0.Substring(0, int_0), "..."));
            }
            else
            {
                str = "---";
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            object[] item;
            DataRow row = null;
            string str = string.Format("select top 10 *,(select count(*) from T_OA_Read where AppName='T_OA_News' and AppId=n._AutoID and EmployeeId = '{0}') isread\r\n            from T_OA_News n where NewsState='是' order by IssueTime desc", base.EmployeeID);
            DataTable toDoUserTaskByEmployeeId = SysDatabase.ExecuteTable(str);
            foreach (DataRow rowA in toDoUserTaskByEmployeeId.Rows)
            {
                StringBuilder stringBuilder = this.sbNews;
                item = new object[] { rowA["_AutoId"], rowA["Title"], rowA["catalog"], null };
                item[3] = (rowA["isread"].ToString() == "0" ? "unread" : "readed");
                stringBuilder.AppendFormat("<div class='item {3}'><a href='Workasp/News/NewsRead.aspx?newsId={0}' target='_blank'>[{2}]&nbsp;{1}</a></div>", item);
            }
            int newsToReadCount = WebTools.GetNewsToReadCount(base.EmployeeID);
            if (newsToReadCount > 0)
            {
                this.iNews = string.Concat("（", newsToReadCount, "）");
            }
            str = string.Format("select top 10 * from ( \r\n            select _AutoId,Title,newsType,IssueTime,IsNull(TopSet,'') TopSet, (select COUNT(*) from T_OA_Read where AppName='T_OA_Note' and AppId=n._AutoID and EmployeeId = '{0}') isread\r\n            from T_OA_Note n where _isdel=0 and  NewsState='是' and ((datalength(ScopeId)=0 and datalength(OrgScopeId)=0) or  patIndex('%{0}%',ScopeId)>0 or patIndex('%{1}%',OrgScopeId)>0) \r\n            ) t order by IsNull(TopSet,'否') desc, IssueTime desc", base.EmployeeID, base.OrgCode);
            toDoUserTaskByEmployeeId = SysDatabase.ExecuteTable(str);
            foreach (DataRow dataRow in toDoUserTaskByEmployeeId.Rows)
            {
                StringBuilder stringBuilder1 = this.sbNote;
                item = new object[] { dataRow["_AutoId"], dataRow["Title"], dataRow["newsType"], null, null, null };
                item[3] = (dataRow["isread"].ToString() == "0" ? "unread" : "readed");
                item[4] = dataRow["IssueTime"];
                item[5] = (dataRow["TopSet"].ToString() == "是" ? "<img class='top' src='img/common/top.gif'/>" : "");
                stringBuilder1.AppendFormat("<div class='item {3}'><a href='Workasp/News/NoteRead.aspx?newsId={0}' target='_blank'>[{2}]&nbsp;{1}</a>&nbsp;<span class='linetime gray'>{4:yyyy-MM-dd}</span>{5}</div>", item);
            }
            newsToReadCount = WebTools.GetNoteToReadCount(base.EmployeeID, base.OrgCode);
            if (newsToReadCount > 0)
            {
                this.iNote = string.Concat("（", newsToReadCount, "）");
            }
            str = string.Format("select top 10 * from ( \r\n            select _AutoId,Title,newsType,IssueTime,IsNull(TopSet,'') TopSet, (select COUNT(*) from T_OA_Read where AppName='T_OA_Note' and AppId=n._AutoID and EmployeeId = '{0}') isread\r\n            from T_OA_Note n where _isdel=0 and  NewsState='是' and newsType='通知' and ((datalength(ScopeId)=0 and datalength(OrgScopeId)=0) or  patIndex('%{0}%',ScopeId)>0 or patIndex('%{1}%',OrgScopeId)>0) \r\n            ) t order by IsNull(TopSet,'否') desc, IssueTime desc", base.EmployeeID, base.OrgCode);
            toDoUserTaskByEmployeeId = SysDatabase.ExecuteTable(str);
            foreach (DataRow row1 in toDoUserTaskByEmployeeId.Rows)
            {
                StringBuilder stringBuilder2 = this.sbNote1;
                item = new object[] { row1["_AutoId"], row1["Title"], row1["newsType"], null, null, null };
                item[3] = (row1["isread"].ToString() == "0" ? "unread" : "readed");
                item[4] = row1["IssueTime"];
                item[5] = (row1["TopSet"].ToString() == "是" ? "<img class='top' src='img/common/top.gif'/>" : "");
                stringBuilder2.AppendFormat("<div class='item {3}'><a href='Workasp/News/NoteRead.aspx?newsId={0}' target='_blank'>[{2}]&nbsp;{1}</a>&nbsp;<span class='linetime gray'>{4:yyyy-MM-dd}</span>{5}</div>", item);
            }
            str = string.Format("select top 10 * from ( \r\n            select _AutoId,Title,newsType,IssueTime,IsNull(TopSet,'') TopSet, (select COUNT(*) from T_OA_Read where AppName='T_OA_Note' and AppId=n._AutoID and EmployeeId = '{0}') isread\r\n            from T_OA_Note n where _isdel=0 and  NewsState='是'  and newsType='公告' and ((datalength(ScopeId)=0 and datalength(OrgScopeId)=0) or  patIndex('%{0}%',ScopeId)>0 or patIndex('%{1}%',OrgScopeId)>0) \r\n            ) t order by IsNull(TopSet,'否') desc, IssueTime desc", base.EmployeeID, base.OrgCode);
            toDoUserTaskByEmployeeId = SysDatabase.ExecuteTable(str);
            foreach (DataRow dataRow1 in toDoUserTaskByEmployeeId.Rows)
            {
                StringBuilder stringBuilder3 = this.sbNote2;
                item = new object[] { dataRow1["_AutoId"], dataRow1["Title"], dataRow1["newsType"], null, null, null };
                item[3] = (dataRow1["isread"].ToString() == "0" ? "unread" : "readed");
                item[4] = dataRow1["IssueTime"];
                item[5] = (dataRow1["TopSet"].ToString() == "是" ? "<img class='top' src='img/common/top.gif'/>" : "");
                stringBuilder3.AppendFormat("<div class='item {3}'><a href='Workasp/News/NoteRead.aspx?newsId={0}' target='_blank'>[{2}]&nbsp;{1}</a>&nbsp;<span class='linetime gray'>{4:yyyy-MM-dd}</span>{5}</div>", item);
            }
            toDoUserTaskByEmployeeId = UserTaskService.GetToDoUserTaskByEmployeeId(base.EmployeeID);
            ArrayList arrayLists = new ArrayList();
            foreach (DataRow row2 in toDoUserTaskByEmployeeId.Rows)
            {
                row2["OwnerId"].ToString();
                string str1 = row2["agentId"].ToString();
                string str2 = row2["isAssign"].ToString();
                string str3 = row2["label"].ToString();
                int count = 0;
                if (arrayLists.Contains(str3))
                {
                    count = arrayLists.IndexOf(str3);
                }
                else
                {
                    count = arrayLists.Count;
                    arrayLists.Add(str3);
                }
                StringBuilder stringBuilder4 = this.sbToDo;
                item = new object[] { row2["uTaskId"], row2["instanceName"], row2["CreateUser"], row2["ArriveTime"], null, null, null, null };
                item[4] = (row2["isread"].ToString() == "0" ? "unread" : "readed");
                item[5] = (str1 == base.EmployeeID ? "[代]&nbsp;" : "");
                item[6] = (str2 == "1" ? "[加]&nbsp;" : "");
                item[7] = string.Concat("label", count.ToString());
                stringBuilder4.AppendFormat("<div class='item {4} {7}'>{5}{6}<a  href='sysfolder/workflow/dealflow.aspx?taskId={0}' target='_blank'>{1}</a>\r\n<span class='lineperson gray'>{2}</span><span class='linetime gray'>{3:yyyy-MM-dd HH:mm}</span>\r\n</div>", item);
            }
            newsToReadCount = toDoUserTaskByEmployeeId.Rows.Count;
            if (newsToReadCount > 0)
            {
                int num = toDoUserTaskByEmployeeId.Rows.Count;
                this.iToDo = string.Concat("（", num.ToString(), "）");
            }
            if (arrayLists.Count > 0)
            {
                this.sbLabel.Append("<span class='bizlabel bizon' title='点击查看全部待办' biz=''>全部待办</span>");
                this.sbLabel.Append("<span class='split'>|</span>");
            }
            for (int i = 0; i < arrayLists.Count; i++)
            {
                string str4 = arrayLists[i].ToString();
                this.sbLabel.AppendFormat("<span class='bizlabel' title='点击查看该分类的待办' biz='label{2}'>{0}<em>({1})</em></span>", str4, (int)toDoUserTaskByEmployeeId.Select(string.Concat("label='", str4, "'")).Length, i);
                if (i < arrayLists.Count - 1)
                {
                    this.sbLabel.AppendFormat("<span class='split'>|</span>", new object[0]);
                }
            }
            DateTime now = DateTime.Now;
            DateTime dateTime = DateTime.Now.AddDays(7);
            List<Calendar> calendars = CalendarService.QueryCalendars(now, dateTime, base.EmployeeID);
            foreach (Calendar calendar in calendars)
            {
                StringBuilder stringBuilder5 = this.sbCalendar;
                item = new object[] { calendar._AutoID, calendar.Subject, calendar.StartTime, null };
                object[] startTime = new object[] { calendar.StartTime, calendar.EndTime, calendar.Subject, calendar.Location };
                item[3] = string.Format("时 间：{0:M月d号}（周） {0:HH:mm}-{1:HH:mm}\r\n事 件：{2}\r\n地 点：{3}", startTime);
                stringBuilder5.AppendFormat("<div class='item'><a  title='{3}' href='Workasp/Calendar/MyCalendar.aspx' target='_self'>{1}</a>\r\n<span class='linetime gray'>{2:yyyy-MM-dd HH:mm}</span>\r\n</div>", item);
            }
            newsToReadCount = calendars.Count;
            if (newsToReadCount > 0)
            {
                this.iSchedule = string.Concat("（", newsToReadCount, "）");
            }
            str = string.Format("select top 6 m._autoid msgid, m.title,m.sender,m._username,m.recids,m.recnames,m.sendtime,m.content,r.isread,r._autoid recid\r\nfrom T_E_App_MsgInfo m inner join T_E_App_MsgRec r on m._AutoID = r.MsgId \r\nwhere r._IsDel=0  and  r.RecId='{0}' and r.isRead=0 order by r.isread, m.sendtime desc", base.EmployeeID);
            toDoUserTaskByEmployeeId = SysDatabase.ExecuteTable(str);
            foreach (DataRow dataRow2 in toDoUserTaskByEmployeeId.Rows)
            {
                StringBuilder stringBuilder6 = this.sbMsg;
                item = new object[] { dataRow2["msgid"], dataRow2["content"], dataRow2["sender"], dataRow2["sendtime"], null, null };
                item[4] = (dataRow2["isread"].ToString() == "0" ? "unread" : "readed");
                item[5] = this.method_0(dataRow2["content"].ToString(), 20);
                stringBuilder6.AppendFormat("<div class='item {4}'><a title='{1}'  href=\"javascript:viewMsg('{0}')\">{5}</a>\r\n                <span class='linetime gray'>{2}&nbsp;{3:MM-dd HH:mm}</span></div>", item);
            }
            newsToReadCount = AppMsgService.GetUnReadMsgCount(base.EmployeeID, "");
            if (newsToReadCount > 0)
            {
                this.iMsg = string.Concat("（", newsToReadCount, "）");
            }
            str = string.Format("select _autoid , HyName,StartTime ,(select count(*) from T_OA_Read where AppName='T_OA_HY_Apply' and AppId=a._AutoID and EmployeeId = '{0}') isread\r\n                from T_OA_HY_Apply a where _IsDel=0  and  \r\n                CharIndex('{0}',HyRyId )>0 and  EndTime>getdate() and HyState='是' order by StartTime", base.EmployeeID);
            toDoUserTaskByEmployeeId = SysDatabase.ExecuteTable(str);
            foreach (DataRow row3 in toDoUserTaskByEmployeeId.Rows)
            {
                StringBuilder stringBuilder7 = this.sbMeeting;
                item = new object[] { row3["_autoid"], row3["HyName"], row3["StartTime"], null };
                item[3] = (row3["isread"].ToString() == "0" ? "unread" : "readed");
                stringBuilder7.AppendFormat("<div class='item {3}'><a title='{1}' target='_blank'  href=\"WorkAsp/Meeting/MeetingInfo.aspx?recId={0}\">{1}</a>\r\n                <span class='linetime gray'>{2:MM月dd日 HH:mm}</span></div>", item);
            }
            newsToReadCount = (int)toDoUserTaskByEmployeeId.Select("isread=0").Length;
            if (newsToReadCount > 0)
            {
                this.iMeeting = string.Concat("（", newsToReadCount, "）");
            }
            StringCollection myLeader2 = EmployeeRelationService.GetMyLeader2(base.EmployeeID);
            if (myLeader2.Count > 0)
            {
                string[] strArrays = myLeader2[0].Split(new char[] { '|' });
                int toDoCount = UserTaskService.GetToDoCount(strArrays[0]);
                this.sbLeaderToDo.AppendFormat("<a href='SysFolder/Workflow/FlowToDo2.aspx?employeeId={0}' target='_self'>{1}</a><span class='itemnum'>（{2}）</span>", strArrays[0], "领导待办", toDoCount);
            }
            str = string.Format("select top 10 i.*,(select count(*) from T_OA_Read where AppName='T_E_WF_Query' and AppId=i.AppId and EmployeeId = '{2}') isread\r\n            from T_E_WF_Instance i inner join T_E_WF_Query q on i._AutoID=q.InstanceId\r\n            where (i.InstanceState='完成' or i.InstanceState='归档' ) and (q.PublicType=1 or (CHARINDEX('{0}',q.DeptIds,0)>0 \r\n            or CHARINDEX('{1}',q.PositionIds,0)>0 or CHARINDEX('{2}',q.EmployeeIds,0)>0)) order by i.FinishTime desc", this.Session["DeptId"], this.Session["PositionId"], base.EmployeeID);
            toDoUserTaskByEmployeeId = SysDatabase.ExecuteTable(str);
            foreach (DataRow dataRow3 in toDoUserTaskByEmployeeId.Rows)
            {
                StringBuilder stringBuilder8 = this.sbFile;
                item = new object[] { dataRow3["AppId"], dataRow3["AppName"], dataRow3["InstanceName"], null, null };
                item[3] = (dataRow3["isread"].ToString() == "0" ? "unread" : "readed");
                item[4] = dataRow3["FinishTime"];
                stringBuilder8.AppendFormat("<div class='item {3}'>\r\n<a  href=\"SysFolder/AppFrame/AppDetail.aspx?tblName={1}&condition=_AutoId=[QUOTES]{0}[QUOTES]&func=1&appId={0}\" target='_blank'>{2}</a>&nbsp;\r\n<span class='linetime gray'>{4:yyyy-MM-dd}</span></div>", item);
            }
            str = string.Format("select top 10 * from T_OA_Survey_Info where [Enable]='是' and getdate() between StartDate and EndDate\r\n                and (SurScope=1 or (SurScope=2 and CHARINDEX('{0}',DeptCodes)>0) or (SurScope=3 and CHARINDEX('{1}',DeptCodes)>0)) order by _CreateTime desc", this.Session["deptId"], this.Session["EmployeeId"]);
            toDoUserTaskByEmployeeId = SysDatabase.ExecuteTable(str);
            foreach (DataRow row4 in toDoUserTaskByEmployeeId.Rows)
            {
                this.sbSurvey.AppendFormat("<div class='item vote'><a  href='Workasp/Survey/SurveyMain.aspx?surveyId={0}' target='_blank'>{1}</a></div>", row4["_AutoId"], row4["SurTitle"]);
            }
        }
    }
}