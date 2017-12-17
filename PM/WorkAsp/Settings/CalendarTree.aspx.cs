using AjaxPro;
using EIS.AppBase;
using EIS.AppCommon;
using EIS.DataAccess;
using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EIS.Web.WorkAsp.Settings
{
    public partial class CalendarTree : PageBase
    {
      
        private DataTable dataTable_0;

        public string treedata = "";

      

        [AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
        public string AddSonNode(string nodeName, string PID)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Insert T_E_App_Calendar (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tCalendarName,\r\n\t\t\t\t\tCalendarPID,\r\n                    HoursOneDay,\r\n                    TimeZone,\r\n\t\t\t\t\tOrderId,\r\n\t\t\t\t\tNote\r\n\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@CalendarName,\r\n\t\t\t\t\t@CalendarPID,\r\n                    @HoursOneDay,\r\n                    @TimeZone,\r\n\t\t\t\t\t@OrderId,\r\n\t\t\t\t\t@Note\r\n\t\t\t)");
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
            UserContext userInfo = base.UserInfo;
            string str = Guid.NewGuid().ToString();
            SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, str);
            SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, userInfo.EmployeeId);
            SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, userInfo.DeptWbs);
            SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, DateTime.Now);
            SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
            SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, 0);
            SysDatabase.AddInParameter(sqlStringCommand, "@CalendarName", DbType.String, nodeName);
            SysDatabase.AddInParameter(sqlStringCommand, "@CalendarPID", DbType.String, PID);
            SysDatabase.AddInParameter(sqlStringCommand, "@TimeZone", DbType.String, TimeZoneInfo.Local.Id);
            SysDatabase.AddInParameter(sqlStringCommand, "@OrderId", DbType.Int32, this.method_1(PID));
            SysDatabase.AddInParameter(sqlStringCommand, "@HoursOneDay", DbType.Single, 9);
            SysDatabase.AddInParameter(sqlStringCommand, "@Note", DbType.String, "");
            SysDatabase.ExecuteNonQuery(sqlStringCommand);
            return str;
        }

        private void method_0(zTreeNode zTreeNode_0)
        {
            DataRow[] dataRowArray = this.dataTable_0.Select(string.Concat("CalendarPID='", zTreeNode_0.id, "'"), "OrderId");
            if ((int)dataRowArray.Length > 0)
            {
                DataRow[] dataRowArray1 = dataRowArray;
                for (int i = 0; i < (int)dataRowArray1.Length; i++)
                {
                    DataRow dataRow = dataRowArray1[i];
                    zTreeNode _zTreeNode = new zTreeNode()
                    {
                        name = dataRow["CalendarName"].ToString(),
                        id = dataRow["_AutoID"].ToString(),
                        icon = "../../img/common/calendar.gif",
                        @value = ""
                    };
                    zTreeNode_0.Add(_zTreeNode);
                    this.method_0(_zTreeNode);
                }
            }
        }

        private int method_1(string string_0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" select max(OrderId) ");
            stringBuilder.Append(" From T_E_App_Calendar ");
            stringBuilder.Append(string.Concat(" where CalendarPID='", string_0, "'"));
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
            object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
            return (obj == DBNull.Value ? 1 : Convert.ToInt32(obj) + 1);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(CalendarTree));
            this.dataTable_0 = SysDatabase.ExecuteTable("select * from T_E_App_Calendar");
            zTreeNode _zTreeNode = new zTreeNode()
            {
                name = "工作日历列表",
                id = "root",
                @value = "",
                icon = "../../img/common/home.png",
                open = true
            };
            zTreeNode _zTreeNode1 = new zTreeNode()
            {
                name = "默认日历",
                id = "",
                @value = "",
                icon = "../../img/common/calendar0.png"
            };
            _zTreeNode.Add(_zTreeNode1);
            this.method_0(_zTreeNode);
            this.treedata = _zTreeNode.ToJsonString(true);
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
        public int RemoveNode(string nodeId)
        {
            if ((nodeId == "" ? true : nodeId == "root"))
            {
                throw new Exception("根结点或者默认日历不可删除！");
            }
            return SysDatabase.ExecuteNonQuery(string.Format("delete T_E_App_Calendar where _autoId='{0}'", nodeId));
        }
    }
}