using EIS.AppBase;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using System;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace EIS.WebBase.SysFolder.WorkFlow
{
    public partial class Admin_UpdateTitle : PageBase
    {
        public string workflowName = "";

        public Instance curInstance = new Instance();

        private string string_0 = "";

        public Admin_UpdateTitle()
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                InstanceService.UpdateInstanceName(this.txtNewTitle.Text.Trim(), this.string_0, base.UserInfo);
                base.ClientScript.RegisterStartupScript(base.GetType(), "success", "success();", true);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                this.Session["_syserror"] = string.Concat("修改任务名称时出错：", exception.Message);
                base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?instanceId=", this.string_0), true);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.string_0 = base.GetParaValue("instanceId");
            this.curInstance = InstanceService.GetInstanceById(this.string_0);
            this.workflowName = DefineService.GetWorkflowName(this.curInstance.WorkflowId);
            if (!base.IsPostBack)
            {
                if (this.curInstance == null)
                {
                    this.Session["_syserror"] = "instanceId参数有错";
                    base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?instanceId=", this.string_0), true);
                }
                else
                {
                    this.txtNewTitle.Text = this.curInstance.InstanceName;
                }
            }
        }
    }
}