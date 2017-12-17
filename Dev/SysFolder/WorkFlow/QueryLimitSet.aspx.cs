using EIS.AppBase;
using EIS.DataAccess;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.WebBase.SysFolder.WorkFlow
{
    public partial class QueryLimitSet : PageBase
    {
       
        public string insId = "";

        private string EditId
        {
            get
            {
                string str;
                str = (this.ViewState["EditId"] != null ? this.ViewState["EditId"].ToString() : "");
                return str;
            }
            set
            {
                this.ViewState["EditId"] = value;
            }
        }

    

        protected void Button1_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder;
            DbCommand sqlStringCommand;
            if (this.EditId != "")
            {
                stringBuilder = new StringBuilder();
                stringBuilder.Append("Update T_E_WF_Query set\r\n                    _UpdateTime=@_UpdateTime ,\r\n                    PublicType=@PublicType ,\r\n                    DeptNames=@DeptNames ,\r\n                    DeptIds=@DeptIds ,\r\n                    PositionNames=@PositionNames ,\r\n                    PositionIds=@PositionIds ,\r\n                    EmployeeNames=@EmployeeNames ,\r\n                    EmployeeIds=@EmployeeIds\r\n\t\t\t    \r\n\t\t\t\twhere _AutoId=@_AutoID\r\n                ");
                sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
                SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, this.EditId);
                SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
                SysDatabase.AddInParameter(sqlStringCommand, "@PublicType", DbType.Int32, int.Parse(this.RadioButtonList1.SelectedValue));
                SysDatabase.AddInParameter(sqlStringCommand, "@DeptNames", DbType.String, this.TextBox1.Text);
                SysDatabase.AddInParameter(sqlStringCommand, "@DeptIds", DbType.String, this.HiddenField1.Value);
                SysDatabase.AddInParameter(sqlStringCommand, "@PositionNames", DbType.String, this.TextBox2.Text);
                SysDatabase.AddInParameter(sqlStringCommand, "@PositionIds", DbType.String, this.HiddenField2.Value);
                SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeNames", DbType.String, this.TextBox3.Text);
                SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeIds", DbType.String, this.HiddenField3.Value);
                SysDatabase.ExecuteNonQuery(sqlStringCommand);
            }
            else
            {
                stringBuilder = new StringBuilder();
                stringBuilder.Append("Insert T_E_WF_Query (\r\n \t\t\t\t\t    _AutoID,\r\n\t\t\t\t\t    _UserName,\r\n\t\t\t\t\t    _OrgCode,\r\n\t\t\t\t\t    _CreateTime,\r\n\t\t\t\t\t    _UpdateTime,\r\n\t\t\t\t\t    _IsDel,\r\n\t\t\t\t\t    AppId,\r\n\t\t\t\t\t    AppName,\r\n\t\t\t\t\t    InstanceId,\r\n\t\t\t\t\t    PublicType,\r\n                        DeptNames,\r\n                        DeptIds,\r\n                        PositionNames,\r\n                        PositionIds,\r\n                        EmployeeNames,\r\n                        EmployeeIds\r\n\t\t\t    ) values(\r\n\t\t\t\t\t    @_AutoID,\r\n\t\t\t\t\t    @_UserName,\r\n\t\t\t\t\t    @_OrgCode,\r\n\t\t\t\t\t    @_CreateTime,\r\n\t\t\t\t\t    @_UpdateTime,\r\n\t\t\t\t\t    @_IsDel,\r\n\t\t\t\t\t    @AppId,\r\n\t\t\t\t\t    @AppName,\r\n\t\t\t\t\t    @InstanceId,\r\n\t\t\t\t\t    @PublicType,\r\n                        @DeptNames,\r\n                        @DeptIds,\r\n                        @PositionNames,\r\n                        @PositionIds,\r\n                        @EmployeeNames,\r\n                        @EmployeeIds\r\n\t\t\t    )");
                sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
                this.EditId = Guid.NewGuid().ToString();
                SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, this.EditId);
                SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, base.EmployeeID);
                SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, base.OrgCode);
                SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, DateTime.Now);
                SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
                SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, 0);
                Instance instanceById = InstanceService.GetInstanceById(this.insId);
                SysDatabase.AddInParameter(sqlStringCommand, "@InstanceId", DbType.String, this.insId);
                SysDatabase.AddInParameter(sqlStringCommand, "@AppName", DbType.String, instanceById.AppName);
                SysDatabase.AddInParameter(sqlStringCommand, "@AppId", DbType.String, instanceById.AppId);
                SysDatabase.AddInParameter(sqlStringCommand, "@PublicType", DbType.Int32, int.Parse(this.RadioButtonList1.SelectedValue));
                SysDatabase.AddInParameter(sqlStringCommand, "@DeptNames", DbType.String, this.TextBox1.Text);
                SysDatabase.AddInParameter(sqlStringCommand, "@DeptIds", DbType.String, this.HiddenField1.Value);
                SysDatabase.AddInParameter(sqlStringCommand, "@PositionNames", DbType.String, this.TextBox2.Text);
                SysDatabase.AddInParameter(sqlStringCommand, "@PositionIds", DbType.String, this.HiddenField2.Value);
                SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeNames", DbType.String, this.TextBox3.Text);
                SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeIds", DbType.String, this.HiddenField3.Value);
                SysDatabase.ExecuteNonQuery(sqlStringCommand);
            }
            base.ClientScript.RegisterStartupScript(base.GetType(), "success", "<script type='text/javascript'>success();</script>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.insId = base.GetParaValue("insId");
            if (!base.IsPostBack)
            {
                string str = string.Format("select * from T_E_WF_Query where instanceId='{0}'", this.insId);
                DataTable dataTable = SysDatabase.ExecuteTable(str);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow item = dataTable.Rows[0];
                    this.RadioButtonList1.SelectedValue = item["PublicType"].ToString();
                    this.TextBox1.Text = item["DeptNames"].ToString();
                    this.HiddenField1.Value = item["DeptIds"].ToString();
                    this.TextBox2.Text = item["PositionNames"].ToString();
                    this.HiddenField2.Value = item["PositionIds"].ToString();
                    this.TextBox3.Text = item["EmployeeNames"].ToString();
                    this.HiddenField3.Value = item["EmployeeIds"].ToString();
                    this.EditId = item["_AutoId"].ToString();
                }
            }
        }
    }
}