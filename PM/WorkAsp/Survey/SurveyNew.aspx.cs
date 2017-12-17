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

namespace EIS.Web.WorkAsp.Survey
{
    public partial class SurveyNew : PageBase
    {
      

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
            DateTime? nullable;
            DateTime? nullable1;
            if (this.EditId != "")
            {
                stringBuilder = new StringBuilder();
                stringBuilder.Append("Update T_OA_Survey_Info set\r\n                    _UpdateTime=@_UpdateTime ,\r\n                    SurTitle=@SurTitle ,\r\n                    SurCode=@SurCode ,\r\n                    Enable=@Enable ,\r\n                    OrderId=@OrderId ,\r\n                    SurScope=@SurScope ,\r\n                    DeptNames=@DeptNames ,\r\n                    DeptCodes=@DeptCodes ,\r\n                    SurMemo=@SurMemo,\r\n                    StartDate=@StartDate,\r\n                    EndDate=@EndDate\r\n\t\t\t    \r\n\t\t\t\twhere _AutoId=@_AutoID\r\n                ");
                sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
                SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, this.EditId);
                SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
                SysDatabase.AddInParameter(sqlStringCommand, "@SurTitle", DbType.String, this.TextBox1.Text);
                SysDatabase.AddInParameter(sqlStringCommand, "@SurCode", DbType.String, this.TextBox2.Text);
                SysDatabase.AddInParameter(sqlStringCommand, "@Enable", DbType.String, this.RadioButtonList2.SelectedValue);
                SysDatabase.AddInParameter(sqlStringCommand, "@SurScope", DbType.String, this.RadioButtonList1.SelectedValue);
                SysDatabase.AddInParameter(sqlStringCommand, "@OrderId", DbType.Int32, int.Parse(this.TextBox4.Text));
                SysDatabase.AddInParameter(sqlStringCommand, "@DeptNames", DbType.String, this.TextBox5.Text);
                SysDatabase.AddInParameter(sqlStringCommand, "@DeptCodes", DbType.String, this.HiddenField1.Value);
                SysDatabase.AddInParameter(sqlStringCommand, "@SurMemo", DbType.String, this.TextBox6.Text);
                nullable = null;
                if (this.txtStart.Text.Trim() != "")
                {
                    nullable = new DateTime?(DateTime.Parse(this.txtStart.Text));
                }
                nullable1 = null;
                if (this.txtEnd.Text.Trim() != "")
                {
                    nullable1 = new DateTime?(DateTime.Parse(this.txtEnd.Text));
                }
                SysDatabase.AddInParameter(sqlStringCommand, "@StartDate", DbType.DateTime, nullable);
                SysDatabase.AddInParameter(sqlStringCommand, "@EndDate", DbType.DateTime, nullable1);
                SysDatabase.ExecuteNonQuery(sqlStringCommand);
            }
            else
            {
                stringBuilder = new StringBuilder();
                stringBuilder.Append("Insert T_OA_Survey_Info (\r\n \t\t\t\t\t    _AutoID,\r\n\t\t\t\t\t    _UserName,\r\n\t\t\t\t\t    _OrgCode,\r\n\t\t\t\t\t    _CreateTime,\r\n\t\t\t\t\t    _UpdateTime,\r\n\t\t\t\t\t    _IsDel,\r\n\t\t\t\t\t    _CompanyId,\r\n\t\t\t\t\t    SurTitle,\r\n\t\t\t\t\t    SurCode,\r\n\t\t\t\t\t    [Enable],\r\n\t\t\t\t\t    OrderId,\r\n\t\t\t\t\t    SurScope,\r\n                        DeptNames,\r\n                        DeptCodes,\r\n                        SurMemo,\r\n                        StartDate,\r\n                        EndDate\r\n\t\t\t    ) values(\r\n\t\t\t\t\t    @_AutoID,\r\n\t\t\t\t\t    @_UserName,\r\n\t\t\t\t\t    @_OrgCode,\r\n\t\t\t\t\t    @_CreateTime,\r\n\t\t\t\t\t    @_UpdateTime,\r\n\t\t\t\t\t    @_IsDel,\r\n\t\t\t\t\t    @_CompanyId,\r\n\t\t\t\t\t    @SurTitle,\r\n\t\t\t\t\t    @SurCode,\r\n\t\t\t\t\t    @Enable,\r\n\t\t\t\t\t    @OrderId,\r\n\t\t\t\t\t    @SurScope,\r\n                        @DeptNames,\r\n                        @DeptCodes,\r\n                        @SurMemo,\r\n                        @StartDate,\r\n                        @EndDate\r\n\t\t\t    )");
                sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
                this.EditId = Guid.NewGuid().ToString();
                SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, this.EditId);
                SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, base.EmployeeID);
                SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, base.OrgCode);
                SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, DateTime.Now);
                SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
                SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, 0);
                SysDatabase.AddInParameter(sqlStringCommand, "@_CompanyId", DbType.String, base.CompanyId);
                SysDatabase.AddInParameter(sqlStringCommand, "@SurTitle", DbType.String, this.TextBox1.Text);
                SysDatabase.AddInParameter(sqlStringCommand, "@Enable", DbType.String, this.RadioButtonList2.SelectedValue);
                SysDatabase.AddInParameter(sqlStringCommand, "@SurScope", DbType.String, this.RadioButtonList1.SelectedValue);
                int num = 0;
                num = (this.TextBox4.Text.Trim() != "" ? int.Parse(this.TextBox4.Text) : this.GetNewOrder());
                SysDatabase.AddInParameter(sqlStringCommand, "@OrderId", DbType.Int32, num);
                string str = num.ToString("d8");
                this.TextBox2.Text = str;
                SysDatabase.AddInParameter(sqlStringCommand, "@SurCode", DbType.String, str);
                SysDatabase.AddInParameter(sqlStringCommand, "@DeptNames", DbType.String, this.TextBox5.Text);
                SysDatabase.AddInParameter(sqlStringCommand, "@DeptCodes", DbType.String, this.HiddenField1.Value);
                SysDatabase.AddInParameter(sqlStringCommand, "@SurMemo", DbType.String, this.TextBox6.Text);
                nullable = null;
                if (this.txtStart.Text.Trim() != "")
                {
                    nullable = new DateTime?(DateTime.Parse(this.txtStart.Text));
                }
                nullable1 = null;
                if (this.txtEnd.Text.Trim() != "")
                {
                    nullable1 = new DateTime?(DateTime.Parse(this.txtEnd.Text));
                }
                SysDatabase.AddInParameter(sqlStringCommand, "@StartDate", DbType.DateTime, nullable);
                SysDatabase.AddInParameter(sqlStringCommand, "@EndDate", DbType.DateTime, nullable1);
                SysDatabase.ExecuteNonQuery(sqlStringCommand);
                this.Button2.Enabled = true;
            }
            base.ClientScript.RegisterStartupScript(base.GetType(), "success", "<script type='text/javascript'>success();</script>");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            base.Response.Redirect(string.Concat("SurveyFrame.aspx?editId=", this.EditId));
        }

        public int GetNewOrder()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select max(OrderId) ");
            stringBuilder.Append(" From T_OA_Survey_Info ");
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
            object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
            return (obj == DBNull.Value ? 1 : Convert.ToInt32(obj) + 1);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dateTime;
            if (!base.IsPostBack)
            {
                this.EditId = base.GetParaValue("editid");
                if (this.EditId != "")
                {
                    string str = string.Format("select * from T_OA_Survey_Info where _autoId='{0}'", this.EditId);
                    DataTable dataTable = SysDatabase.ExecuteTable(str);
                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow item = dataTable.Rows[0];
                        this.RadioButtonList1.SelectedValue = item["SurScope"].ToString();
                        this.RadioButtonList1.SelectedValue = item["Enable"].ToString();
                        this.TextBox1.Text = item["SurTitle"].ToString();
                        this.TextBox2.Text = item["SurCode"].ToString();
                        this.TextBox4.Text = item["OrderId"].ToString();
                        this.TextBox5.Text = item["DeptNames"].ToString();
                        this.HiddenField1.Value = item["DeptCodes"].ToString();
                        this.TextBox6.Text = item["SurMemo"].ToString();
                        if (item["StartDate"].ToString() != "")
                        {
                            TextBox textBox = this.txtStart;
                            dateTime = DateTime.Parse(item["StartDate"].ToString());
                            textBox.Text = dateTime.ToString("yyyy-MM-dd HH:mm");
                        }
                        if (item["EndDate"].ToString() != "")
                        {
                            TextBox str1 = this.txtEnd;
                            dateTime = DateTime.Parse(item["EndDate"].ToString());
                            str1.Text = dateTime.ToString("yyyy-MM-dd HH:mm");
                        }
                    }
                }
                else
                {
                    this.Button2.Enabled = false;
                    this.TextBox4.Text = this.GetNewOrder().ToString();
                }
            }
        }
    }
}