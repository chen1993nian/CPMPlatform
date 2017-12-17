using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.Permission.Service;
using EIS.Web.ModelLib.Model;
using EIS.Web.ModelLib.Service;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.UI;
using EIS.Web.WorkAsp.Calendar;
namespace EIS.Web.WorkAsp.Meeting
{
    public partial class MeetingSave : PageBase
    {
        public string sJson = "";

        private string string_0 = "";

        private string string_1 = "";


        private JsonReturnMessages method_0()
        {
            DateTime dateTime;
            DateTime dateTime1;
            JsonReturnMessages jsonReturnMessage;
            JsonReturnMessages message = new JsonReturnMessages();
            string item = base.Request["HyId"];
            string str = base.Request["MoveTitle"];
            string item1 = base.Request["StartTime"];
            string str1 = base.Request["EndTime"];
            string item2 = base.Request["HysName"];
            string str2 = base.Request["SmsAlert"];
            Convert.ToInt32(base.Request["timezone"]);
            TimeHelper.GetTimeZone();
            bool flag = DateTime.TryParse(str1, out dateTime);
            if (!DateTime.TryParse(item1, out dateTime1))
            {
                message.IsSuccess = false;
                message.Msg = string.Concat("日期格式不正确:", item1);
                jsonReturnMessage = message;
            }
            else if (flag)
            {
                DbConnection dbConnection = SysDatabase.CreateConnection();
                dbConnection.Open();
                DbTransaction dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.Append("update T_OA_HY_Apply set \r\n\t\t\t\t\t    _UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t    HyAddr =@HyAddr,\r\n\t\t\t\t\t    StartTime=@StartTime,\r\n\t\t\t\t\t    EndTime=@EndTime\r\n\t\t\t\t\t    where _AutoID=@_AutoID\r\n\t\t\t     ");
                        DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
                        SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, item);
                        SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
                        SysDatabase.AddInParameter(sqlStringCommand, "@HyAddr", DbType.String, item2);
                        SysDatabase.AddInParameter(sqlStringCommand, "@StartTime", DbType.DateTime, dateTime1);
                        SysDatabase.AddInParameter(sqlStringCommand, "@EndTime", DbType.DateTime, dateTime);
                        SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                        stringBuilder.Length = 0;
                        stringBuilder.Append("Insert T_OA_HY_Apply_TJ (\r\n \t\t\t\t\t    _AutoID,\r\n\t\t\t\t\t    _UserName,\r\n\t\t\t\t\t    _OrgCode,\r\n\t\t\t\t\t    _CreateTime,\r\n\t\t\t\t\t    _UpdateTime,\r\n\t\t\t\t\t    _IsDel,\r\n\t\t\t\t\t    _MainId,\r\n\t\t\t\t\t    _MainTbl,\r\n\t\t\t\t\t    TJR,\r\n\t\t\t\t\t    TJSJ,\r\n\t\t\t\t\t    TJNR\r\n\r\n\t\t\t    ) values(\r\n\t\t\t\t\t    @_AutoID,\r\n\t\t\t\t\t    @_UserName,\r\n\t\t\t\t\t    @_OrgCode,\r\n\t\t\t\t\t    @_CreateTime,\r\n\t\t\t\t\t    @_UpdateTime,\r\n\t\t\t\t\t    @_IsDel,\r\n\t\t\t\t\t    @_MainId,\r\n\t\t\t\t\t    @_MainTbl,\r\n\t\t\t\t\t    @TJR,\r\n\t\t\t\t\t    @TJSJ,\r\n\t\t\t\t\t    @TJNR\r\n\t\t\t    )");
                        string str3 = Guid.NewGuid().ToString();
                        sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
                        SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, str3);
                        SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, base.UserInfo.EmployeeId);
                        SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, base.OrgCode);
                        SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, DateTime.Now);
                        SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
                        SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, 0);
                        SysDatabase.AddInParameter(sqlStringCommand, "@_MainId", DbType.String, item);
                        SysDatabase.AddInParameter(sqlStringCommand, "@_MainTbl", DbType.String, "T_OA_HY_Apply");
                        SysDatabase.AddInParameter(sqlStringCommand, "@TJR", DbType.String, base.EmployeeName);
                        SysDatabase.AddInParameter(sqlStringCommand, "@TJSJ", DbType.DateTime, DateTime.Now);
                        SysDatabase.AddInParameter(sqlStringCommand, "@TJNR", DbType.String, str);
                        SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                        if (str2 == "true")
                        {
                            string str4 = "";
                            string str5 = "";
                            DataTable dataTable = SysDatabase.ExecuteTable(string.Concat("select * from T_OA_HY_Apply where _AutoId='", item, "'"), dbTransaction);
                            if (dataTable.Rows.Count > 0)
                            {
                                str4 = dataTable.Rows[0]["_UserName"].ToString();
                                str5 = dataTable.Rows[0]["HyName"].ToString();
                            }
                            _AppSms _AppSm = new _AppSms(dbTransaction);
                            AppSms appSm = new AppSms()
                            {
                                _AutoID = Guid.NewGuid().ToString(),
                                _UserName = base.UserInfo.EmployeeId,
                                _OrgCode = base.UserInfo.DeptWbs,
                                _CreateTime = DateTime.Now,
                                _UpdateTime = DateTime.Now,
                                _IsDel = 0,
                                SenderId = base.UserInfo.EmployeeId,
                                SenderName = base.UserInfo.EmployeeName,
                                CompanyId = base.UserInfo.CompanyId,
                                AppId = item,
                                AppName = "T_OA_HY_Apply"
                            };
                            string[] strArrays = new string[] { "会议室调剂通知：由您申请的【", str5, "】会议，管理员已经做了调剂，", str, "，请您通知参会人员。" };
                            appSm.Content = string.Concat(strArrays);
                            appSm.State = "0";
                            StringCollection stringCollections = new StringCollection();
                            stringCollections.Add(str4);
                            appSm.RecIds = EIS.AppBase.Utility.GetJoinString(stringCollections);
                            appSm.RecNames = EmployeeService.GetEmployeeNameList(stringCollections);
                            appSm.RecPhone = "";
                            _AppSm.Add(appSm);
                        }
                        dbTransaction.Commit();
                        message.Data = "";
                        message.IsSuccess = true;
                        message.Msg = "操作成功!";
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        dbTransaction.Rollback();
                        message.IsSuccess = false;
                        message.Msg = exception.Message;
                    }
                }
                finally
                {
                    if (dbConnection.State == ConnectionState.Open)
                    {
                        dbConnection.Close();
                    }
                }
                jsonReturnMessage = message;
            }
            else
            {
                message.IsSuccess = false;
                message.Msg = string.Concat("日期格式不正确:", str1);
                jsonReturnMessage = message;
            }
            return jsonReturnMessage;
        }

        private JsonReturnMessages method_1()
        {
            DateTime dateTime;
            DateTime dateTime1;
            JsonReturnMessages jsonReturnMessage;
            JsonReturnMessages message = new JsonReturnMessages();
            string item = base.Request["CalendarTitle"];
            string str = base.Request["CalendarStartTime"];
            string item1 = base.Request["CalendarEndTime"];
            string str1 = base.Request["IsAllDayEvent"];
            int num = Convert.ToInt32(base.Request["timezone"]);
            int timeZone = TimeHelper.GetTimeZone() - num;
            bool flag = DateTime.TryParse(item1, out dateTime);
            if (!DateTime.TryParse(str, out dateTime1))
            {
                message.IsSuccess = false;
                message.Msg = string.Concat("日期格式不正确:", str);
                jsonReturnMessage = message;
            }
            else if (flag)
            {
                try
                {
                    EIS.Web.ModelLib.Model.Calendar calendar = new EIS.Web.ModelLib.Model.Calendar(base.UserInfo)
                    {
                        EmpId = this.string_0,
                        EmpName = this.string_1,
                        Category = "1",
                        CategoryName = "公事",
                        CalendarType = 1,
                        InstanceType = 0,
                        Subject = item,
                        StartTime = dateTime1.AddHours((double)timeZone),
                        EndTime = dateTime.AddHours((double)timeZone),
                        IsAllDayEvent = int.Parse(str1),
                        Location = "",
                        MasterId = new int?(num)
                    };
                    CalendarService.AddCalendar(calendar);
                    message.Data = calendar._AutoID;
                    message.IsSuccess = true;
                    message.Msg = "操作成功!";
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    message.IsSuccess = false;
                    message.Msg = exception.Message;
                }
                jsonReturnMessage = message;
            }
            else
            {
                message.IsSuccess = false;
                message.Msg = string.Concat("日期格式不正确:", item1);
                jsonReturnMessage = message;
            }
            return jsonReturnMessage;
        }

        private JsonReturnMessages method_2()
        {
            DateTime dateTime;
            DateTime dateTime1;
            JsonReturnMessages jsonReturnMessage;
            JsonReturnMessages message = new JsonReturnMessages();
            string item = base.Request["calendarId"];
            string str = base.Request["CalendarStartTime"];
            string item1 = base.Request["CalendarEndTime"];
            int num = Convert.ToInt32(base.Request["timezone"]);
            int timeZone = TimeHelper.GetTimeZone() - num;
            bool flag = DateTime.TryParse(str, out dateTime);
            bool flag1 = DateTime.TryParse(item1, out dateTime1);
            if ((!flag ? false : flag1))
            {
                try
                {
                    EIS.Web.ModelLib.Model.Calendar calendar = CalendarService.GetCalendar(item);
                    calendar.StartTime = dateTime.AddHours((double)timeZone);
                    calendar.EndTime = dateTime1.AddHours((double)timeZone);
                    calendar.MasterId = new int?(num);
                    CalendarService.UpdateCalendar(calendar);
                    message.IsSuccess = true;
                    message.Msg = "操作成功!";
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    message.IsSuccess = false;
                    message.Msg = exception.Message;
                }
                jsonReturnMessage = message;
            }
            else
            {
                message.IsSuccess = false;
                message.Msg = string.Concat("日期格式不正确:", str);
                jsonReturnMessage = message;
            }
            return jsonReturnMessage;
        }

        private JsonReturnMessages method_3()
        {
            JsonReturnMessages jsonReturnMessage;
            JsonReturnMessages message = new JsonReturnMessages();
            string item = base.Request["calendarId"];
            if (!string.IsNullOrEmpty(item))
            {
                try
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("update T_OA_HY_Apply set \r\n\t\t\t\t\t    _UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t    HyState =@HyState\r\n\t\t\t\t\t    where _AutoID=@_AutoID\r\n\t\t\t     ");
                    DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
                    SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, item);
                    SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
                    SysDatabase.AddInParameter(sqlStringCommand, "@HyAddr", DbType.String, "已取消");
                    SysDatabase.ExecuteNonQuery(sqlStringCommand);
                    message.IsSuccess = true;
                    message.Msg = "操作成功！";
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    message.IsSuccess = false;
                    message.Msg = exception.Message;
                }
                jsonReturnMessage = message;
            }
            else
            {
                message.IsSuccess = false;
                message.Msg = "calendarId参数不能为空";
                jsonReturnMessage = message;
            }
            return jsonReturnMessage;
        }

        private JsonReturnMessages method_4()
        {
            EIS.Web.ModelLib.Model.Calendar item;
            DateTime dateTime;
            string str = base.Request["Id"];
            JsonReturnMessages jsonReturnMessage = new JsonReturnMessages();
            try
            {
                item = (str.Length <= 0 ? new EIS.Web.ModelLib.Model.Calendar(base.UserInfo)
                {
                    _UserName = this.string_0
                } : CalendarService.GetCalendar(str));
                item.Subject = base.Request["Subject"];
                item.Location = base.Request["Location"];
                item.Description = base.Request["Description"];
                item.IsAllDayEvent = (base.Request["IsAllDayEvent"] == "false" ? 0 : 1);
                item.Category = base.Request["colorvalue"];
                string item1 = base.Request["stpartdate"];
                string str1 = base.Request["etpartdate"];
                int num = Convert.ToInt32(base.Request["timezone"]);
                int timeZone = TimeHelper.GetTimeZone() - num;
                if (item.IsAllDayEvent != 1)
                {
                    dateTime = Convert.ToDateTime(item1);
                    item.StartTime = dateTime.AddHours((double)timeZone);
                    dateTime = Convert.ToDateTime(str1);
                    item.EndTime = dateTime.AddHours((double)timeZone);
                }
                else
                {
                    dateTime = Convert.ToDateTime(item1);
                    item.StartTime = dateTime.AddHours((double)timeZone);
                    dateTime = Convert.ToDateTime(str1);
                    dateTime = dateTime.AddHours(23);
                    dateTime = dateTime.AddMinutes(59);
                    dateTime = dateTime.AddSeconds(59);
                    item.EndTime = dateTime.AddHours((double)timeZone);
                }
                if (item.EndTime <= item.StartTime)
                {
                    throw new Exception("结束日期小于开始日期");
                }
                item.CalendarType = 1;
                item.InstanceType = 0;
                item.MasterId = new int?(num);
                if (str.Length <= 0)
                {
                    CalendarService.AddCalendar(item);
                }
                else
                {
                    CalendarService.UpdateCalendar(item);
                }
                jsonReturnMessage.IsSuccess = true;
                jsonReturnMessage.Msg = "操作成功！";
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                jsonReturnMessage.IsSuccess = false;
                jsonReturnMessage.Msg = exception.Message;
            }
            return jsonReturnMessage;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string item = base.Request["act"];
            this.string_0 = (string.IsNullOrEmpty(base.Request["employeeId"]) ? base.EmployeeID : base.Request["employeeId"]);
            this.string_1 = EmployeeService.GetEmployeeName(this.string_0);
            base.Response.ContentType = "application/json; charset=utf-8";
            JsonReturnMessages jsonReturnMessage = new JsonReturnMessages();
            if (item == "add")
            {
                jsonReturnMessage = this.method_1();
            }
            else if (item == "update")
            {
                jsonReturnMessage = this.method_2();
            }
            else if (item == "delete")
            {
                jsonReturnMessage = this.method_3();
            }
            else if (item == "save")
            {
                jsonReturnMessage = this.method_4();
            }
            else if (item == "move")
            {
                jsonReturnMessage = this.method_0();
            }
            this.sJson = JavaScriptSerializer.Serialize(jsonReturnMessage);
        }
    }
}