using EIS.AppBase;
using EIS.Web.SysFolder.WorkFlow.UserControl;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using System;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.WorkFlow
{
    public partial class FlowChart : PageBase
    {
        public StringBuilder flowChart = new StringBuilder();

        public int MaxWidth = 0;

        public int MaxHeight = 0;

        public string workflowId = "";

        public string WorkflowName = "";

        public string Creator = "";

        private int int_0 = 32;

        private int int_1 = 32;

        private string string_0 = "#4677BF";

        private int int_2 = 1;

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