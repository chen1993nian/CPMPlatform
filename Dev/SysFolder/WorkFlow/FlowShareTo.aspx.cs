using EIS.AppBase;
using EIS.AppModel.Service;
using EIS.DataModel.Model;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.XPDLParser.Elements;
using System;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.WebBase.SysFolder.WorkFlow
{
    public partial class FlowShareTo : PageBase
    {
       
        public StringBuilder sbHtml = new StringBuilder();

        private string string_0 = "";

        public string TipMsg = "";

        public Task curTask = new Task();

        public Instance curInstance = null;

        public FlowShareTo()
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string text = this.txtReason.Text;
            if (!string.IsNullOrEmpty(this.txtLeaderId.Value))
            {
                UserContext userInfo = base.UserInfo;
                AppMsg appMsg = new AppMsg(base.UserInfo)
                {
                    Title = string.Concat("共享任务提醒:", this.curInstance.InstanceName),
                    MsgType = "",
                    MsgUrl = string.Concat("SysFolder/AppFrame/AppWorkFlowInfo.aspx?instanceId=", this.curInstance.InstanceId),
                    RecIds = this.txtLeaderId.Value,
                    RecNames = this.txtLeader.Text,
                    SendTime = new DateTime?(DateTime.Now),
                    Sender = base.EmployeeName,
                    Content = string.Format("{0}把【{1}】任务共享给你。\r\n以下是他（她的）附言：{2}", base.EmployeeName, this.curInstance.InstanceName, this.txtReason.Text)
                };
                AppMsgService.SendMessage(appMsg);
                this.Session["_sysinfo"] = string.Concat("任务已经共享给：", this.txtLeader.Text);
                base.Server.Transfer(string.Concat("DealFlowAfter.aspx?taskId=", this.string_0, "&info=1"), false);
            }
            else
            {
                this.TipMsg = "<div class='info'>*&nbsp;没有选择共享人</div>";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.string_0 = base.GetParaValue("taskId");
            DriverEngine driverEngine = new DriverEngine(base.UserInfo);
            this.curTask = driverEngine.GetTaskById(this.string_0);
            this.curInstance = driverEngine.GetInstanceById(this.curTask.InstanceId);
            driverEngine.GetActivityById(this.curInstance, this.curTask.ActivityId);
            if (!base.IsPostBack && !string.IsNullOrEmpty(base.Request["reason"]))
            {
                this.txtReason.Text = base.Request["reason"];
            }
        }
    }
}