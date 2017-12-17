using EIS.AppBase;
using EIS.DataAccess;
using WebBase.JZY.Tools;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace EIS.Web.SysFolder.WorkFlow
{
    public partial class NewBefore : PageBase
    {
      

        public string TblName = "";

        public string MainId = "";

        public string workflowId = "";

        public string workflowCode = "";

        public string Infos = "";

        public Define defModel = new Define();

        public DateTime updateTime;


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.CheckBox1.Checked)
            {
               
                WebTools.DeleteRemind(base.EmployeeID, "", this.workflowId);
                WebTools.UpdateRemind(base.EmployeeID, "", this.workflowId);
            }
            base.Response.Redirect(string.Concat("NewFlow.aspx?note=1&", base.Request.QueryString));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.workflowId = base.GetParaValue("workflowid");
            this.workflowCode = base.GetParaValue("workflowCode");
            if (this.workflowId != "")
            {
                this.defModel = DefineService.GetWorkflowDefineModelById(this.workflowId);
            }
            else if (this.workflowCode != "")
            {
                this.defModel = DefineService.GetWorkflowByCode(this.workflowCode);
            }
            if (!base.IsPostBack)
            {
                string str = string.Concat("select top 1 * from T_E_WF_Direction where WFId='", this.defModel.WorkflowCode, "' and enable='是'");
                DataTable dataTable = SysDatabase.ExecuteTable(str);
                if (dataTable.Rows.Count > 0)
                {
                    if (base.GetParaValue("view") != "1")
                    {
                        this.updateTime = Convert.ToDateTime(dataTable.Rows[0]["_UpdateTime"]);
                        if (SysDatabase.ExecuteTable(string.Format("select * from T_OA_Reminded where _CreateTime > '{0}' and AppID='{1}' and EmployeeId='{2}'", this.updateTime, this.workflowId, base.EmployeeID)).Rows.Count <= 0)
                        {
                            this.Infos = dataTable.Rows[0]["Note"].ToString();
                        }
                        else
                        {
                            base.Response.Redirect(string.Concat("NewFlow.aspx?note=1&", base.Request.QueryString));
                        }
                    }
                    else
                    {
                        this.Button1.Enabled = false;
                        this.Infos = dataTable.Rows[0]["Note"].ToString();
                    }
                }
                else if (base.GetParaValue("view") != "1")
                {
                    base.Response.Redirect(string.Concat("NewFlow.aspx?note=1&", base.Request.QueryString));
                }
                else
                {
                    this.Infos = "暂无填报须知";
                    this.Button1.Enabled = false;
                }
            }
        }
    }
}