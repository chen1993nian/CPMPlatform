using EIS.AppBase;
using EIS.DataAccess;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace EIS.Web.WorkAsp.Meeting
{
    public partial class MeetingCancel : PageBase
    {
        public string workflowName = "";

        public string _HyName = "";

        public string _EmployeeName = "";

        public string _CreateTime = "";

        public string _InstanceState = "";

        public string InstanceId = "";

        public Instance curInstance = new Instance();

        private DataRow dataRow_0;

      

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.method_0();
                base.ClientScript.RegisterStartupScript(base.GetType(), "success", "success();", true);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                this.Session["_sysinfo"] = string.Concat("取消会议时出错：", exception.Message);
                base.Server.Transfer("../../SysFolder/AppFrame/AppInfo.aspx?msgType=error", true);
            }
        }

        private void method_0()
        {
            string str = this.dataRow_0["_autoId"].ToString();
            string str1 = string.Concat("会议取消：", this.txtReason.Text);
            DbConnection dbConnection = SysDatabase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                try
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("update T_OA_HY_Apply set \r\n\t\t\t\t\t    _UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t    HyState =@HyState\r\n\t\t\t\t\t    where _AutoID=@_AutoID\r\n\t\t\t     ");
                    DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
                    SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, str);
                    SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
                    SysDatabase.AddInParameter(sqlStringCommand, "@HyState", DbType.String, "已取消");
                    SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                    stringBuilder.Length = 0;
                    stringBuilder.Append("Insert T_OA_HY_Apply_TJ (\r\n \t\t\t\t\t    _AutoID,\r\n\t\t\t\t\t    _UserName,\r\n\t\t\t\t\t    _OrgCode,\r\n\t\t\t\t\t    _CreateTime,\r\n\t\t\t\t\t    _UpdateTime,\r\n\t\t\t\t\t    _IsDel,\r\n\t\t\t\t\t    _MainId,\r\n\t\t\t\t\t    _MainTbl,\r\n\t\t\t\t\t    TJR,\r\n\t\t\t\t\t    TJSJ,\r\n\t\t\t\t\t    TJNR\r\n\r\n\t\t\t    ) values(\r\n\t\t\t\t\t    @_AutoID,\r\n\t\t\t\t\t    @_UserName,\r\n\t\t\t\t\t    @_OrgCode,\r\n\t\t\t\t\t    @_CreateTime,\r\n\t\t\t\t\t    @_UpdateTime,\r\n\t\t\t\t\t    @_IsDel,\r\n\t\t\t\t\t    @_MainId,\r\n\t\t\t\t\t    @_MainTbl,\r\n\t\t\t\t\t    @TJR,\r\n\t\t\t\t\t    @TJSJ,\r\n\t\t\t\t\t    @TJNR\r\n\t\t\t    )");
                    string str2 = Guid.NewGuid().ToString();
                    sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
                    SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, str2);
                    SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, base.UserInfo.EmployeeId);
                    SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, base.OrgCode);
                    SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, DateTime.Now);
                    SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
                    SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, 0);
                    SysDatabase.AddInParameter(sqlStringCommand, "@_MainId", DbType.String, str);
                    SysDatabase.AddInParameter(sqlStringCommand, "@_MainTbl", DbType.String, "T_OA_HY_Apply");
                    SysDatabase.AddInParameter(sqlStringCommand, "@TJR", DbType.String, base.EmployeeName);
                    SysDatabase.AddInParameter(sqlStringCommand, "@TJSJ", DbType.DateTime, DateTime.Now);
                    SysDatabase.AddInParameter(sqlStringCommand, "@TJNR", DbType.String, str1);
                    SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                    dbTransaction.Commit();
                }
                catch (Exception exception)
                {
                    dbTransaction.Rollback();
                }
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InstanceId = base.GetParaValue("InstanceId");
            if (this.InstanceId == "")
            {
                string paraValue = base.GetParaValue("hyId");
                this.curInstance = InstanceService.GetInstanceByAppInfo("T_OA_HY_Apply", paraValue);
                if (this.curInstance != null)
                {
                    this.InstanceId = this.curInstance.InstanceId;
                    this.dataRow_0 = EIS.WorkFlow.Engine.Utility.GetAppData(this.curInstance);
                    this._InstanceState = this.curInstance.InstanceState;
                }
                else
                {
                    DataTable dataTable = SysDatabase.ExecuteTable(string.Concat("select * from T_OA_HY_Apply where _AutoId='", paraValue, "'"));
                    if (dataTable.Rows.Count <= 0)
                    {
                        this.Session["_sysinfo"] = "找不到对应的会议！";
                        base.Server.Transfer("../../SysFolder/AppFrame/AppInfo.aspx?msgType=error", true);
                    }
                    else
                    {
                        this.dataRow_0 = dataTable.Rows[0];
                    }
                }
            }
            else
            {
                this.curInstance = InstanceService.GetInstanceById(this.InstanceId);
                this.dataRow_0 = EIS.WorkFlow.Engine.Utility.GetAppData(this.curInstance);
                this._InstanceState = this.curInstance.InstanceState;
            }
            string str = this.dataRow_0["hyState"].ToString();
            this._HyName = this.dataRow_0["HyName"].ToString();
            this._EmployeeName = this.dataRow_0["HyJbr"].ToString();
            DateTime dateTime = Convert.ToDateTime(this.dataRow_0["ApplyTime"]);
            this._CreateTime = dateTime.ToString("yyyy-MM-dd HH:mm");
            if (str != "是")
            {
                if (str != "否")
                {
                    if (str == "已取消")
                    {
                        this.Session["_sysinfo"] = "会议已经取消！";
                        base.Server.Transfer("../../SysFolder/AppFrame/AppInfo.aspx?msgType=info", true);
                    }
                }
                else if (this.curInstance == null)
                {
                    this.Session["_sysinfo"] = "会议还未生效，无需取消！";
                    base.Server.Transfer("../../SysFolder/AppFrame/AppInfo.aspx?msgType=info", true);
                }
                else if (this.curInstance.InstanceState == EnumDescription.GetFieldText(InstanceState.Processing))
                {
                    base.Response.Redirect(string.Concat("../../SysFolder/Workflow/Admin_StopInstance.aspx?instanceId=", this.InstanceId));
                }
                else if (this.curInstance.InstanceState == EnumDescription.GetFieldText(InstanceState.Stoped))
                {
                    this.Session["_sysinfo"] = "会议已经取消！";
                    base.Server.Transfer("../../SysFolder/AppFrame/AppInfo.aspx?msgType=info", true);
                }
            }
        }
    }
}