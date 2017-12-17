using EIS.AppBase;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using System;
using System.Text;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.WorkFlow
{
    public partial class FlowErrorInfo : PageBase
    {
        public StringBuilder DealInfo = new StringBuilder();

        public string taskId = "";

        public string perror = "";

        public string moreInfo = "";

        public string backCls = "";

        public string moreCls = "hidden";

        public Task curTask = new Task();

        public Instance curInstance = new Instance();

     
        public FlowErrorInfo()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.taskId = base.GetParaValue("taskId");
            string paraValue = base.GetParaValue("instanceId");
            this.backCls = (base.GetParaValue("back") != "0" ? "" : "hidden");
            this.moreCls = (base.GetParaValue("more") == "1" ? "" : "hidden");
            if (!string.IsNullOrEmpty(this.taskId))
            {
                DriverEngine driverEngine = new DriverEngine(base.UserInfo);
                this.curTask = driverEngine.GetTaskById(this.taskId);
                this.curInstance = driverEngine.GetInstanceById(this.curTask.InstanceId);
            }
            else if (!string.IsNullOrEmpty(paraValue))
            {
                this.curInstance = InstanceService.GetInstanceById(paraValue);
            }
            if (this.Session["_syserror"] != null)
            {
                this.DealInfo.Append(this.Session["_syserror"].ToString());
            }
            if (this.Session["_syserror_more"] != null)
            {
                this.moreInfo = this.Session["_syserror_more"].ToString();
            }
        }
    }
}