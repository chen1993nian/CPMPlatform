using EIS.AppBase;
using EIS.WebBase.SysFolder.WorkFlow.UserControl;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using System;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EIS.WebBase.SysFolder.WorkFlow
{
    public partial class FlowChart : PageBase
    {
        public StringBuilder flowChart = new StringBuilder();

        public int MaxWidth = 0;

        public int MaxHeight = 0;

        public string workflowId = "";

        public string WorkflowName = "";

        public string Creator = "";


        public Define defModel = new Define();

     

        public FlowChart()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.workflowId = base.GetParaValue("workflowId");
            this.defModel = DefineService.GetWorkflowDefineModelById(this.workflowId);
            this.FlowImg.workflowId = this.workflowId;
        }
    }
}