using EIS.AppBase;
using EIS.DataAccess;
using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.WorkAsp.WorkLog
{
    public partial class LogEdit : PageBase
    {
        private string string_0 = "";

       
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            this.method_0();
            base.ClientScript.RegisterClientScriptBlock(base.GetType(), "", "<script type='text/javascript'>success();</script>");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            this.method_0();
            base.Response.Redirect("LogEdit.aspx");
        }

        private void method_0()
        {
            DbCommand sqlStringCommand;
            StringBuilder stringBuilder = new StringBuilder();
            if (this.string_0 != "")
            {
                stringBuilder.Append("update T_OA_WorkLog set \r\n\t\t\t\t\t                _UpdateTime = @_UpdateTime,\r\n\t\t\t\t\t                WorkDate = @WorkDate,\r\n\t\t\t\t\t                WorkLog = @WorkLog\r\n\t\t\t                where _AutoID=@_AutoID");
                sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
                SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, this.string_0);
                SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
                SysDatabase.AddInParameter(sqlStringCommand, "@WorkDate", DbType.DateTime, Convert.ToDateTime(this.txtDate.Text));
                SysDatabase.AddInParameter(sqlStringCommand, "@WorkLog", DbType.String, this.txtEditor.Text);
                SysDatabase.ExecuteNonQuery(sqlStringCommand);
            }
            else
            {
                stringBuilder.Append("Insert T_OA_WorkLog (\r\n \t\t\t\t\t                _AutoID,\r\n\t\t\t\t\t                _UserName,\r\n\t\t\t\t\t                _OrgCode,\r\n\t\t\t\t\t                _CreateTime,\r\n\t\t\t\t\t                _UpdateTime,\r\n\t\t\t\t\t                _IsDel,\r\n                                    _CompanyID,\r\n\t\t\t\t\t                EmpID,\r\n\t\t\t\t\t                EmpName,\r\n\t\t\t\t\t                WorkDate,\r\n\t\t\t\t\t                WorkLog\r\n\r\n\t\t\t                ) values(\r\n\t\t\t\t\t                @_AutoID,\r\n\t\t\t\t\t                @_UserName,\r\n\t\t\t\t\t                @_OrgCode,\r\n\t\t\t\t\t                @_CreateTime,\r\n\t\t\t\t\t                @_UpdateTime,\r\n\t\t\t\t\t                @_IsDel,\r\n                                    @_CompanyID,\r\n\t\t\t\t\t                @EmpID,\r\n\t\t\t\t\t                @EmpName,\r\n\t\t\t\t\t                @WorkDate,\r\n\t\t\t\t\t                @WorkLog\r\n\t\t\t                )");
                sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
                Guid guid = Guid.NewGuid();
                SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, guid.ToString());
                SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, base.EmployeeID);
                SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, base.OrgCode);
                SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, DateTime.Now);
                SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
                SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, 0);
                SysDatabase.AddInParameter(sqlStringCommand, "@_CompanyID", DbType.String, base.CompanyId);
                SysDatabase.AddInParameter(sqlStringCommand, "@EmpID", DbType.String, base.EmployeeID);
                SysDatabase.AddInParameter(sqlStringCommand, "@EmpName", DbType.String, base.EmployeeName);
                SysDatabase.AddInParameter(sqlStringCommand, "@WorkDate", DbType.DateTime, Convert.ToDateTime(this.txtDate.Text));
                SysDatabase.AddInParameter(sqlStringCommand, "@WorkLog", DbType.String, this.txtEditor.Text);
                SysDatabase.ExecuteNonQuery(sqlStringCommand);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime today;
            this.string_0 = base.GetParaValue("mainId");
            if (!base.IsPostBack)
            {
                if (this.string_0.Trim() == "")
                {
                    TextBox str = this.txtDate;
                    today = DateTime.Today;
                    str.Text = today.ToString("yyyy-MM-dd");
                    this.txtEmpName.Text = base.EmployeeName;
                }
                else
                {
                    string str1 = string.Format(string.Concat("select * from T_OA_WorkLog where _autoid='", this.string_0, "'"), new object[0]);
                    DataTable dataTable = SysDatabase.ExecuteTable(str1);
                    if (dataTable.Rows.Count > 0)
                    {
                        TextBox textBox = this.txtDate;
                        today = Convert.ToDateTime(dataTable.Rows[0]["WorkDate"]);
                        textBox.Text = today.ToString("yyyy-MM-dd");
                        this.txtEmpName.Text = dataTable.Rows[0]["EmpName"].ToString();
                        this.txtEditor.Text = dataTable.Rows[0]["worklog"].ToString();
                    }
                }
            }
        }
    }
}