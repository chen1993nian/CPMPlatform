using AjaxPro;
using EIS.AppBase;
using EIS.AppModel.Service;
using EIS.DataAccess;
using EIS.Permission;
using EIS.Web.ModelLib.Model;
using EIS.Web.ModelLib.Service;
using WebBase.JZY.Tools;
using EIS.WorkFlow.Service;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using NLog;
using EIS.AppBase.Config;
namespace EIS.Web
{
    public partial class Desktop : PageBase
    {
        public StringBuilder funcIdStr = new StringBuilder();

        public StringBuilder func_array = new StringBuilder();

        public int int_0 = 0;

        public string pwbs = "";
        public string IsTextIcon = "false";

        public DataTable dtNode = null;

        private Logger fileLogger;

        [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
        public string GetCounts(string idList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{");
            int toDoCount = 0;
            string[] strArrays = idList.Split(new char[] { ',' });
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                string str = strArrays[i];
                if (str == "todo")
                {
                    toDoCount = UserTaskService.GetToDoCount(base.EmployeeID);
                    stringBuilder.AppendFormat("'{0}':{1},", str, toDoCount);
                }
                else if (str == "news")
                {
                    toDoCount = WebTools.GetNewsToReadCount(base.EmployeeID);
                    stringBuilder.AppendFormat("'{0}':{1},", str, toDoCount);
                }
                else if (str == "notice")
                {
                    toDoCount = WebTools.GetNoteToReadCount(base.EmployeeID, base.OrgCode);
                    stringBuilder.AppendFormat("'{0}':{1},", str, toDoCount);
                }
                else if (str == "msg")
                {
                    toDoCount = AppMsgService.GetUnReadMsgCount(base.EmployeeID);
                    stringBuilder.AppendFormat("'{0}':{1},", str, toDoCount);
                }
                else if (str == "calendar")
                {
                    DateTime now = DateTime.Now;
                    DateTime dateTime = DateTime.Now.AddDays(7);
                    List<Calendar> calendars = CalendarService.QueryCalendars(now, dateTime, base.EmployeeID);
                    stringBuilder.AppendFormat("'{0}':{1},", str, calendars.Count);
                }
                else if (str == "meeting")
                {
                    string str1 = string.Format(@"select count(*) from T_OA_HY_Apply a where _IsDel=0  and  
                            CharIndex('{0}',HyRyId )>0 and  EndTime>getdate() and HyState='是'", base.EmployeeID);
                    object obj = SysDatabase.ExecuteScalar(str1);
                    stringBuilder.AppendFormat("'{0}':{1},", str, obj.ToString());
                }
                else
                {
                    try
                    {
                        string str1 = string.Format(@"select * from T_E_Sys_FunNode where CountSQL<>'' and  _AutoID='{0}'", str);
                        DataSet ds = SysDatabase.ExecuteDataSet(str1);
                        if ((ds != null) && (ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
                        {
                            string countSql = ds.Tables[0].Rows[0]["CountSQL"].ToString();
                            if (countSql != "")
                            {
                                countSql = base.ReplaceContext(countSql);
                                object obj = SysDatabase.ExecuteScalar(countSql);
                                stringBuilder.AppendFormat("'{0}':{1},", str, obj.ToString());
                            }
                        }
                    }
                    catch { }
                    finally { }
                }
            }
            stringBuilder.Length = stringBuilder.Length - 1;
            stringBuilder.Append("};");
            return stringBuilder.ToString();
        }

        private void method_0(DataRow dataRow_0)
        {
            string str = dataRow_0["FunWbs"].ToString();
            string str1 = dataRow_0["_AutoID"].ToString();
            string str2 = this.method_1(dataRow_0);
            if (str2 == "")
            {
                if (((int)this.dtNode.Select(string.Concat("FunPWBS='", str, "'")).Length <= 0 ? true : str.Length != 13))
                {
                    return;
                }
                str2 = string.Concat("SysFolder/AppFrame/AppFrame.aspx?menu=", str);
            }
            string str3 = str1;
            if ((this.int_0 <= 0 ? false : this.int_0 % 24 == 0))
            {
                this.funcIdStr.Append("|");
            }
            Desktop int0 = this;
            int0.int_0 = int0.int_0 + 1;
            this.funcIdStr.Append(string.Concat(str3, ","));
            StringBuilder funcArray = this.func_array;
            object[] item = new object[] { str3, dataRow_0["FunName"], str2, dataRow_0["DesktopImage"], dataRow_0["DispStyle"] };
            funcArray.AppendFormat("\r\nfunc_array[\"{0}\"] = [\"{1}\", \"{2}\", \"{3}\", \"{4}\"];", item);
        }

        private string method_1(DataRow dataRow_0)
        {
            string str;
            string str1 = dataRow_0["_AutoID"].ToString();
            if (dataRow_0["Encrypt"].ToString() != "是")
            {
                str = dataRow_0["LinkFile"].ToString().Replace("\r\n", "");
            }
            else
            {
                string str2 = dataRow_0["LinkFile"].ToString().Replace("\r\n", "");
                if (str2.Trim() != "")
                {
                    string[] strArrays = str2.Split("?".ToCharArray());
                    if ((int)strArrays.Length != 1)
                    {
                        string[] strArrays1 = new string[] { strArrays[0], "?", base.ReplaceContext(strArrays[1]), "&funid=", str1 };
                        str2 = string.Concat(strArrays1);
                    }
                    else
                    {
                        str2 = string.Concat(str2, "?funid=", str1);
                    }
                    str = EIS.AppBase.Utility.EncryptUrl(str2, base.UserName);
                }
                else
                {
                    str = "";
                }
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable allowedFunNode;
            int j;
            DataRow[] dataRowArray;
            DataRow item;
            string str;
            object[] objArray;
            string str1;
            string str2;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Desktop));


            IsTextIcon = (SysConfig.GetConfig("Basic_DeskTop").ItemValue == "1" ? "true" : "false");

            try{
            this.pwbs = base.GetParaValue("pwbs");
            string str3 = "";
            if (this.pwbs == "")
            {
                fileLogger = LogManager.GetCurrentClassLogger();
                str3 = string.Format("select f.funId, n.* from T_E_App_Favorite f inner join T_E_Sys_FunNode n on f.funId=n._autoId where f._userName='{0}' order by f.funOrder", base.EmployeeID);
                allowedFunNode = SysDatabase.ExecuteTable(str3);
                this.int_0 = 1;
                StringCollection stringCollections = new StringCollection();
                char[] chrArray = new char[] { ',' };
                stringCollections.AddRange("home,todo,news,notice,msg,calendar,meeting".Split(chrArray));
                this.funcIdStr.Append("home,todo,news,notice,msg,calendar,meeting,");
                this.func_array.AppendFormat("\r\nfunc_array[\"home\"] = [\"我的桌面\", \"home.aspx\", \"home.png\", \"\"];", new object[0]);
                this.func_array.AppendFormat("\r\nfunc_array[\"todo\"] = [\"待办事项\", \"home.aspx\", \"default.png\", \"\"];", new object[0]);
                this.func_array.AppendFormat("\r\nfunc_array[\"news\"] = [\"新闻中心\", \"home.aspx\", \"news.png\", \"\"];", new object[0]);
                this.func_array.AppendFormat("\r\nfunc_array[\"notice\"] = [\"通知公告\", \"home.aspx\", \"notify.png\", \"\"];", new object[0]);
                this.func_array.AppendFormat("\r\nfunc_array[\"msg\"] = [\"系统消息\", \"home.aspx\", \"email.png\", \"\"];", new object[0]);
                this.func_array.AppendFormat("\r\nfunc_array[\"calendar\"] = [\"日程安排\", \"home.aspx\", \"calendar.png\", \"\"];", new object[0]);
                this.func_array.AppendFormat("\r\nfunc_array[\"meeting\"] = [\"我的会议\", \"home.aspx\", \"meeting.png\", \"\"];", new object[0]);
                for (int i = 0; i < allowedFunNode.Rows.Count; i++)
                {
                    item = allowedFunNode.Rows[i];
                    if (!stringCollections.Contains(item["funId"].ToString()))
                    {
                        str2 = item["funId"].ToString();
                        str1 = this.method_1(item);
                        if (EIS.Permission.Utility.GetFunLimitByEmployeeId(base.EmployeeID, str2).StartsWith("1"))
                        {
                            str = str2;
                            if ((this.int_0 <= 0 ? false : this.int_0 % 24 == 0))
                            {
                                this.funcIdStr.Append("|");
                            }
                            Desktop int0 = this;
                            int0.int_0 = int0.int_0 + 1;
                            this.funcIdStr.Append(string.Concat(str, ","));
                            StringBuilder funcArray = this.func_array;
                            objArray = new object[] { str, item["FunName"], str1, item["DesktopImage"], item["DispStyle"] };
                            funcArray.AppendFormat("\r\nfunc_array[\"{0}\"] = [\"{1}\", \"{2}\", \"{3}\", \"{4}\"];", objArray);
                        }
                    }
                }
            }
            else if (this.pwbs.ToLower() == "switch")
            {
                allowedFunNode = EIS.Permission.Utility.GetAllowedFunNode(base.EmployeeID, "0", "-1");
                dataRowArray = allowedFunNode.Select("FunPWBS='0'", "orderid");
                j = 0;
                while (j < (int)dataRowArray.Length)
                {
                    item = dataRowArray[j];
                    str2 = item["_AutoID"].ToString();
                    str1 = this.method_1(item);
                    if (str1 == "")
                    {
                        return;
                    }
                    else
                    {
                        str = str2;
                        if ((this.int_0 <= 0 ? false : this.int_0 % 24 == 0))
                        {
                            this.funcIdStr.Append("|");
                        }
                        Desktop desktop = this;
                        desktop.int_0 = desktop.int_0 + 1;
                        this.funcIdStr.Append(string.Concat(str, ","));
                        StringBuilder stringBuilder = this.func_array;
                        objArray = new object[] { str, item["FunName"], str1, item["DesktopImage"] };
                        stringBuilder.AppendFormat("\r\nfunc_array[\"{0}\"] = [\"{1}\", \"{2}\", \"{3}\", \"3\"];", objArray);
                        j++;
                    }
                }
            }
            else if (this.pwbs.Length > 0)
            {
                this.dtNode = EIS.Permission.Utility.GetAllowedFunNode(base.EmployeeID, this.pwbs, this.Session["webId"].ToString());
                DataRow[] dataRowArray1 = this.dtNode.Select(string.Concat("FunPWBS='", this.pwbs, "'"), "orderid");
                dataRowArray = dataRowArray1;
                for (j = 0; j < (int)dataRowArray.Length; j++)
                {
                    item = dataRowArray[j];
                    this.method_0(item);
                    string str4 = item["FunWbs"].ToString();
                    DataRow[] dataRowArray2 = this.dtNode.Select(string.Concat("FunPWBS='", str4, "'"), "orderid");
                    DataRow[] dataRowArray3 = dataRowArray2;
                    for (int k = 0; k < (int)dataRowArray3.Length; k++)
                    {
                        this.method_0(dataRowArray3[k]);
                    }
                }
            }
            }catch(Exception ex)
            {
                fileLogger.Error<string, string, string>("发生错误 DeskTop:{0},{1},参数列表：{2}", "Page_Load(object sender, EventArgs e)", ex.Message, this.pwbs);
            }
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
        public void SaveOrder(string idList)
        {
            string[] strArrays = idList.Split(new char[] { ',' });
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                string str = string.Format("Update T_E_App_Favorite set funOrder={1} where funId ='{0}' and _userName='{2}'", strArrays[i], i, base.EmployeeID);
                SysDatabase.ExecuteNonQuery(str);
            }
        }
    }
}