using EIS.AppBase;
using EIS.DataAccess;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web.WorkAsp.Survey
{
    public partial class SurveyMain : PageBase
    {
        private string string_0 = "";

        public string surveyTitle = "";

        public string surveyMemo = "";

        public string Tips = "";

        public string btnClass = "";

        public StringBuilder listItems = new StringBuilder();

       
        private void method_0(string string_1, string string_2, string string_3)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Insert T_OA_Survey_Reply (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n                    _CompanyId,\r\n                    QuesID,\r\n\t\t\t\t\tAnswer,\r\n\t\t\t\t\tInSelect,\r\n\t\t\t\t\tOtherVal\r\n\r\n\t\t\t    ) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n                    @_CompanyId,\r\n                    @QuesID,\r\n\t\t\t\t\t@Answer,\r\n\t\t\t\t\t@InSelect,\r\n\t\t\t\t\t@OtherVal\r\n\t\t\t    )");
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
            UserContext userInfo = base.UserInfo;
            string str = Guid.NewGuid().ToString();
            SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, str);
            SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, userInfo.EmployeeId);
            SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, userInfo.DeptWbs);
            SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, DateTime.Now);
            SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
            SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, 0);
            SysDatabase.AddInParameter(sqlStringCommand, "@_CompanyId", DbType.String, userInfo.CompanyId);
            SysDatabase.AddInParameter(sqlStringCommand, "@QuesID", DbType.String, string_1);
            SysDatabase.AddInParameter(sqlStringCommand, "@Answer", DbType.String, string_2);
            SysDatabase.AddInParameter(sqlStringCommand, "@InSelect", DbType.String, (string_3 == "" ? "是" : "否"));
            SysDatabase.AddInParameter(sqlStringCommand, "@OtherVal", DbType.String, string_3);
            SysDatabase.ExecuteNonQuery(sqlStringCommand);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str;
            DataTable dataTable;
            string str1;
            string str2;
            int i;
            string[] strArrays;
            char[] chrArray;
            string str3;
            DataRow rowA = null;
            string str4;
            string str5;
            string item;
            this.string_0 = base.GetParaValue("surveyId");
            if (base.IsPostBack)
            {
                str = string.Format("select * from T_OA_Survey_Question where SurveyId='{0}' order by QOrder", this.string_0);
                dataTable = SysDatabase.ExecuteTable(str);
                foreach (DataRow rowAC in dataTable.Rows)
                {
                    string str6 = rowAC["_autoId"].ToString();
                    str1 = rowAC["qtype"].ToString();
                    str4 = rowAC["qselect"].ToString();
                    str3 = rowAC["showother"].ToString();
                    str2 = rowAC["qorder"].ToString();
                    if (string.IsNullOrEmpty(base.Request.Form[string.Concat("input", str2)]))
                    {
                        continue;
                    }
                    if (str1 == "单选")
                    {
                        str5 = base.Request.Form[string.Concat("input", str2)].Trim();
                        if (str5 == "")
                        {
                            continue;
                        }
                        item = "";
                        if (str5 == "其它")
                        {
                            item = base.Request.Form[string.Concat("input", str2, "_other")];
                        }
                        this.method_0(str6, str5, item);
                    }
                    else if (str1 != "多选")
                    {
                        str5 = base.Request.Form[string.Concat("input", str2)].Trim();
                        if (str5 == "")
                        {
                            continue;
                        }
                        this.method_0(str6, str5, "");
                    }
                    else
                    {
                        str5 = base.Request.Form[string.Concat("input", str2)].Trim();
                        if (str5 == "")
                        {
                            continue;
                        }
                        str5 = string.Concat(str5, ",");
                        item = "";
                        if (str5.IndexOf("其它") > -1)
                        {
                            item = base.Request.Form[string.Concat("input", str2, "_other")];
                        }
                        this.method_0(str6, str5, item);
                    }
                }
                str = string.Concat("update T_OA_Survey_Info set ReplyNum=isNull(ReplyNum,0)+1 where _AutoId='", this.string_0, "'");
                SysDatabase.ExecuteNonQuery(str);
                base.Response.Redirect(string.Concat("SurveyReport.aspx?surveyId=", this.string_0));
            }
            else
            {
                str = string.Concat("select * from T_OA_Survey_Info where _AutoId='", this.string_0, "'");
                dataTable = SysDatabase.ExecuteTable(str);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow dataRow = dataTable.Rows[0];
                    this.surveyTitle = dataRow["SurTitle"].ToString();
                    this.surveyMemo = dataRow["SurMemo"].ToString();
                    if (dataRow["Enable"].ToString() != "是")
                    {
                        this.Tips = "<div class='tip'>当前调查问卷已经无效，无法使用。</div>";
                        this.btnClass = "hidden";
                    }
                    if ((this.btnClass != "" ? false : dataRow["EndDate"].ToString() != "") && DateTime.Parse(dataRow["EndDate"].ToString()).CompareTo(DateTime.Now) < 0)
                    {
                        this.Tips = "<div class='tip'>当前调查问卷已经过期失效。</div>";
                        this.btnClass = "hidden";
                    }
                    if (this.btnClass == "" && int.Parse(SysDatabase.ExecuteScalar(string.Format("select count(*) from T_OA_Survey_Reply where _userName='{1}' and QuesID in (select _autoId from T_OA_Survey_Question where SurveyId='{0}')", this.string_0, base.EmployeeID)).ToString()) > 0)
                    {
                        this.Tips = string.Concat("<div class='tip'>每个人只能提交一次，请不要重复提交。<a href='SurveyReport.aspx?surveyId=", this.string_0, "' target='_self'>【查看报表】</a></div>");
                        this.btnClass = "hidden";
                    }
                    str = string.Format("select * from T_OA_Survey_Question where SurveyId='{0}' order by QOrder", this.string_0);
                    dataTable = SysDatabase.ExecuteTable(str);
                    foreach (DataRow row1 in dataTable.Rows)
                    {
                        str1 = row1["qtype"].ToString();
                        str4 = row1["qselect"].ToString();
                        str3 = row1["showother"].ToString();
                        str2 = row1["qorder"].ToString();
                        StringBuilder stringBuilder = this.listItems;
                        object[] objArray = new object[] { row1["_AutoId"], str1, str2, row1["QMustSel"].ToString() };
                        stringBuilder.AppendFormat("<div class='question' p='{0}|{1}|{2}|{3}'>", objArray);
                        this.listItems.AppendFormat("<h3>{0}、{1}？<span>{2}</span></h3>", row1["QOrder"], row1["QTitle"], (row1["QMustSel"].ToString() == "1" ? "*" : ""));
                        this.listItems.Append("<div class='list'>");
                        if (str1 == "单选")
                        {
                            chrArray = new char[] { '\r' };
                            strArrays = str4.Split(chrArray);
                            for (i = 0; i < (int)strArrays.Length; i++)
                            {
                                StringBuilder stringBuilder1 = this.listItems;
                                object obj = i;
                                string str7 = strArrays[i];
                                chrArray = new char[] { '\n' };
                                stringBuilder1.AppendFormat("<div class='item'><input type='radio' value='{2}' id='input{0}_{1}' name='input{0}'/><label for='input{0}_{1}'>{2}</label></div>", str2, obj, str7.Trim(chrArray));
                            }
                            if (str3 == "是")
                            {
                                this.listItems.AppendFormat("<div class='item'><input type='radio' value='{2}' id='input{0}_{1}' name='input{0}'/><label for='input{0}_{1}'>{2}</label><input type='text' class='iother' name='input{0}_other'/></div>", str2, (int)strArrays.Length, "其它");
                            }
                        }
                        else if (str1 == "多选")
                        {
                            chrArray = new char[] { '\r' };
                            strArrays = str4.Split(chrArray);
                            for (i = 0; i < (int)strArrays.Length; i++)
                            {
                                StringBuilder stringBuilder2 = this.listItems;
                                object obj1 = i;
                                string str8 = strArrays[i];
                                chrArray = new char[] { '\n' };
                                stringBuilder2.AppendFormat("<div class='item'><input type='checkbox' value='{2}' id='input{0}_{1}' name='input{0}'/><label for='input{0}_{1}'>{2}</label></div>", str2, obj1, str8.Trim(chrArray));
                            }
                            if (str3 == "是")
                            {
                                this.listItems.AppendFormat("<div class='item'><input type='checkbox' value='{2}' id='input{0}_{1}' name='input{0}'/><label for='input{0}_{1}'>{2}</label><input type='text' class='iother' name='input{0}_other'/></div>", str2, (int)strArrays.Length, "其它");
                            }
                        }
                        else if (str1 == "输入")
                        {
                            this.listItems.AppendFormat("<div class='item'><textarea rows='5' name='input{0}'></textarea></div>", str2);
                        }
                        this.listItems.AppendFormat("</div>", new object[0]);
                        this.listItems.AppendFormat("</div>", new object[0]);
                    }
                }
            }
        }
    }
}