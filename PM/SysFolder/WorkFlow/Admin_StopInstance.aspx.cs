using EIS.AppBase;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using System;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace EIS.Web.SysFolder.WorkFlow
{
    public partial class Admin_StopInstance:PageBase
    {
      	public string workflowName = "";

		public string InstanceId = "";

		public Instance curInstance = new Instance();

	

		protected void Button1_Click(object sender, EventArgs e)
		{
			try
			{
				EIS.WorkFlow.Engine.Utility.StopInstance(this.InstanceId, this.txtReason.Text, base.UserInfo);
				base.ClientScript.RegisterStartupScript(base.GetType(), "success", "success();", true);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				this.Session["_syserror"] = string.Concat("终止流程任务时出错：", exception.Message);
				base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?instanceId=", this.InstanceId), true);
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.InstanceId = base.GetParaValue("InstanceId");
			this.curInstance = InstanceService.GetInstanceById(this.InstanceId);
			this.workflowName = DefineService.GetWorkflowName(this.curInstance.WorkflowId);
			if (this.curInstance.InstanceState != EnumDescription.GetFieldText(InstanceState.Processing))
			{
				this.Session["_syserror"] = string.Concat("流程状态为【", this.curInstance.InstanceState, "】，不能终止任务");
				base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?instanceId=", this.InstanceId), true);
			}
		}
    }
}