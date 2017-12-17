using EIS.AppBase;
using EIS.AppModel.Service;
using EIS.DataModel.Model;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using EIS.WorkFlow.XPDLParser.Elements;
using System;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.SysFolder.WorkFlow
{
    public partial class FlowDelegate : PageBase
    {
       

        public StringBuilder sbHtml = new StringBuilder();

        private string string_0 = "";

        public string TipMsg = "";

        public Task curTask = new Task();

        private Instance instance_0 = null;

        public FlowDelegate()
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string text = this.txtReason.Text;
            if (!string.IsNullOrEmpty(this.txtLeaderId.Value))
            {
                UserTask userTaskByTaskId = EIS.WorkFlow.Engine.Utility.GetUserTaskByTaskId(this.curTask.TaskId, base.EmployeeID);
                if (userTaskByTaskId != null)
                {
                    UserTaskService.UpdateTaskAgent(userTaskByTaskId.UserTaskId, this.txtLeaderId.Value);
                    AppMsg appMsg = new AppMsg(base.UserInfo)
                    {
                        Title = string.Concat("任务委托提醒:", this.instance_0.InstanceName),
                        MsgType = "",
                        MsgUrl = string.Concat("SysFolder/Workflow/DealFlow.aspx?sysTaskId=", this.string_0),
                        RecIds = this.txtLeaderId.Value,
                        RecNames = this.txtLeader.Text,
                        SendTime = new DateTime?(DateTime.Now),
                        Sender = base.EmployeeName,
                        Content = string.Format("{0}把任务【{1}】委托给您，现需要您尽快处理。\r\n以下是他（她的）附言：{2}", base.EmployeeName, this.instance_0.InstanceName, this.txtReason.Text)
                    };
                    AppMsgService.SendMessage(appMsg);
                    this.Session["_sysinfo"] = string.Concat("任务已经委托给：", this.txtLeader.Text, "，同时您也可以继续处理。");
                    base.Server.Transfer(string.Concat("DealFlowAfter.aspx?taskId=", this.string_0, "&info=1"), false);
                }
            }
            else
            {
                this.TipMsg = "<div class='info'>*&nbsp;没有选择代理人</div>";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.string_0 = base.GetParaValue("taskId");
            DriverEngine driverEngine = new DriverEngine(base.UserInfo);
            this.curTask = driverEngine.GetTaskById(this.string_0);
            this.instance_0 = driverEngine.GetInstanceById(this.curTask.InstanceId);
            driverEngine.GetActivityById(this.instance_0, this.curTask.ActivityId);
            if (!base.IsPostBack && !string.IsNullOrEmpty(base.Request["reason"]))
            {
                this.txtReason.Text = base.Request["reason"];
            }
        }
    }
}