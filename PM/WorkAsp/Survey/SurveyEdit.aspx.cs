using EIS.AppBase;
using EIS.DataAccess;
using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.WorkAsp.Survey
{
    public partial class SurveyEdit : PageBase
    {
       

        public string nodeId = "";

        public string surveyId = "";

        public string curNodeId
        {
            get
            {
                return this.ViewState["curNodeId"].ToString();
            }
            set
            {
                this.ViewState["curNodeId"] = value;
            }
        }

        public SurveyEdit()
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.curNodeId = "";
            this.Button1.Enabled = false;
            this.QTitle.Text = "";
            this.QType.SelectedValue = "单选";
            this.QSelect.Text = "";
            this.QMustSel.SelectedValue = "0";
            this.QOrder.Text = this.method_0().ToString();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder;
            DbCommand sqlStringCommand;
            UserContext userInfo;
            string[] strArrays;
            if (this.curNodeId != "")
            {
                stringBuilder = new StringBuilder();
                stringBuilder.Append("update T_OA_Survey_Question set \r\n\t\t\t\t    _UpdateTime=@_UpdateTime,\r\n                    QTitle=@QTitle,\r\n\t\t\t\t    QSelect=@QSelect,\r\n\t\t\t\t    ShowOther=@ShowOther,\r\n\t\t\t\t    QType=@QType,\r\n\t\t\t\t    QOrder=@QOrder,\r\n\t\t\t\t    QMustSel=@QMustSel\r\n                where _AutoID=@_AutoID");
                sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
                userInfo = base.UserInfo;
                SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, this.curNodeId);
                SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
                SysDatabase.AddInParameter(sqlStringCommand, "@QTitle", DbType.String, this.QTitle.Text.Trim());
                SysDatabase.AddInParameter(sqlStringCommand, "@QSelect", DbType.String, this.QSelect.Text);
                SysDatabase.AddInParameter(sqlStringCommand, "@ShowOther", DbType.String, (this.CheckBox1.Checked ? "是" : "否"));
                SysDatabase.AddInParameter(sqlStringCommand, "@QType", DbType.String, this.QType.SelectedValue);
                SysDatabase.AddInParameter(sqlStringCommand, "@QMustSel", DbType.String, this.QMustSel.SelectedValue);
                SysDatabase.AddInParameter(sqlStringCommand, "@QOrder", DbType.Int32, int.Parse(this.QOrder.Text.Trim()));
                SysDatabase.ExecuteNonQuery(sqlStringCommand);
                ClientScriptManager clientScript = base.ClientScript;
                Type type = typeof(SurveyEdit);
                strArrays = new string[] { "changeNode('", this.curNodeId, "|", this.QOrder.Text.Trim(), ".", this.QTitle.Text.Trim(), "');" };
                clientScript.RegisterStartupScript(type, "", string.Concat(strArrays), true);
            }
            else
            {
                stringBuilder = new StringBuilder();
                stringBuilder.Append("Insert T_OA_Survey_Question (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n                    _CompanyId,\r\n                    SurveyId,\r\n\t\t\t\t\tQTitle,\r\n\t\t\t\t\tQSelect,\r\n\t\t\t\t\tShowOther,\r\n\t\t\t\t\tQOrder,\r\n\t\t\t\t\tQType,\r\n\t\t\t\t\tQMustSel\r\n\r\n\t\t\t    ) values(\r\n\t\t\t\t\t    @_AutoID,\r\n\t\t\t\t\t    @_UserName,\r\n\t\t\t\t\t    @_OrgCode,\r\n\t\t\t\t\t    @_CreateTime,\r\n\t\t\t\t\t    @_UpdateTime,\r\n\t\t\t\t\t    @_IsDel,\r\n                        @_CompanyId,\r\n                        @SurveyId,\r\n\t\t\t\t\t    @QTitle,\r\n\t\t\t\t\t    @QSelect,\r\n\t\t\t\t\t    @ShowOther,\r\n\t\t\t\t\t    @QOrder,\r\n\t\t\t\t\t    @QType,\r\n\t\t\t\t\t    @QMustSel\r\n\t\t\t    )");
                sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
                userInfo = base.UserInfo;
                string str = Guid.NewGuid().ToString();
                SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, str);
                SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, userInfo.EmployeeId);
                SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, userInfo.DeptWbs);
                SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, DateTime.Now);
                SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
                SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, 0);
                SysDatabase.AddInParameter(sqlStringCommand, "@_CompanyId", DbType.String, userInfo.CompanyId);
                SysDatabase.AddInParameter(sqlStringCommand, "@SurveyId", DbType.String, this.surveyId);
                SysDatabase.AddInParameter(sqlStringCommand, "@QTitle", DbType.String, this.QTitle.Text.Trim());
                SysDatabase.AddInParameter(sqlStringCommand, "@QSelect", DbType.String, this.QSelect.Text);
                SysDatabase.AddInParameter(sqlStringCommand, "@ShowOther", DbType.String, (this.CheckBox1.Checked ? "是" : "否"));
                SysDatabase.AddInParameter(sqlStringCommand, "@QType", DbType.String, this.QType.SelectedValue);
                SysDatabase.AddInParameter(sqlStringCommand, "@QMustSel", DbType.String, this.QMustSel.SelectedValue);
                SysDatabase.AddInParameter(sqlStringCommand, "@QOrder", DbType.Int32, int.Parse(this.QOrder.Text.Trim()));
                SysDatabase.ExecuteNonQuery(sqlStringCommand);
                ClientScriptManager clientScriptManager = base.ClientScript;
                Type type1 = typeof(SurveyEdit);
                strArrays = new string[] { "addNode('", str, "|", this.QOrder.Text.Trim(), ".", this.QTitle.Text.Trim(), "');" };
                clientScriptManager.RegisterStartupScript(type1, "", string.Concat(strArrays), true);
                this.Button1.Enabled = true;
                this.curNodeId = str;
            }
        }

        private int method_0()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" select max(QOrder) ");
            stringBuilder.Append(" From T_OA_Survey_Question ");
            stringBuilder.Append(string.Concat(" where SurveyId='", this.surveyId, "'"));
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
            object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
            return (obj == DBNull.Value ? 1 : Convert.ToInt32(obj) + 1);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.nodeId = base.GetParaValue("nodeId");
            this.surveyId = base.GetParaValue("surveyId");
            if (!base.IsPostBack)
            {
                string str = string.Concat("select * from T_OA_Survey_Question where _autoId='", this.nodeId, "'");
                DataTable dataTable = SysDatabase.ExecuteTable(str);
                if (dataTable.Rows.Count > 0)
                {
                    this.QTitle.Text = dataTable.Rows[0]["QTitle"].ToString();
                    this.QSelect.Text = dataTable.Rows[0]["QSelect"].ToString();
                    this.QType.SelectedValue = dataTable.Rows[0]["QType"].ToString();
                    this.CheckBox1.Checked = dataTable.Rows[0]["ShowOther"].ToString() == "是";
                    this.QOrder.Text = dataTable.Rows[0]["QOrder"].ToString();
                    this.QMustSel.SelectedValue = dataTable.Rows[0]["QMustSel"].ToString();
                }
                this.curNodeId = this.nodeId;
            }
        }
    }
}