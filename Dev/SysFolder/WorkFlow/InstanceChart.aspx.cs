using EIS.AppBase;
using EIS.WebBase.SysFolder.WorkFlow.UserControl;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using System;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EIS.WebBase.SysFolder.WorkFlow
{
    public partial class InstanceChart : PageBase
    {
        public StringBuilder flowChart = new StringBuilder();

        public int MaxWidth = 0;

        public int MaxHeight = 0;

        public string instanceId = "";

        public string instanceName = "";

        public string workflowName = "";

        public Instance defModel = new Instance();

     


 

        protected void Page_Load(object sender, EventArgs e)
        {
            DriverEngine driverEngine = new DriverEngine(base.UserInfo);
            this.instanceId = base.GetParaValue("instanceId");
            this.defModel = driverEngine.GetInstanceById(this.instanceId);
            this.instanceName = this.defModel.InstanceName;
            this.workflowName = DefineService.GetWorkflowName(this.defModel.WorkflowId);
            this.FlowImg.InstanceId = this.instanceId;
        }
    }
}