using EIS.AppBase;
using EIS.Permission.Service;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.WebBase.SysFolder.WorkFlow
{
    public partial class DealShareTask : PageBase
    {
      
        public StringBuilder DealInfo = new StringBuilder();

        public string taskId = "";

        public string uTaskId = "";

        public string perror = "";

        public Task curTask = new Task();

        public Instance curInstance = new Instance();

        public DealShareTask()
        {
        }

        protected void btnApplyTask_Click(object sender, EventArgs e)
        {
            TaskService.ApplyShareTask(this.taskId, this.uTaskId);
            base.Response.Redirect(string.Concat("DealFlow.aspx?taskId=", this.uTaskId), true);
        }

        protected void btnShareTask_Click(object sender, EventArgs e)
        {
            TaskService.BackShareTask(this.taskId);
            if (UserTaskService.GetUserTaskById(this.uTaskId, null).TaskState == "0")
            {
                this.btnShareTask.CssClass = "hidden";
                this.btnApplyTask.CssClass = "";
                StringCollection stringCollections = new StringCollection();
                TaskService.GetShareTaskDealUser(this.taskId, stringCollections, null);
                string employeeNameList = EmployeeService.GetEmployeeNameList(stringCollections);
                this.DealInfo.AppendFormat("该任务是共享任务，需要（{0}）其中一个人处理", employeeNameList);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.uTaskId = base.GetParaValue("uTaskId");
            if (string.IsNullOrEmpty(this.uTaskId))
            {
                this.Session["_syserror"] = "参数有错，缺少uTaskId参数！";
                base.Server.Transfer(string.Concat("FlowErrorInfo.aspx?taskId=", this.taskId), true);
            }
            UserTask userTaskById = UserTaskService.GetUserTaskById(this.uTaskId, null);
            this.taskId = userTaskById.TaskId;
            DriverEngine driverEngine = new DriverEngine(base.UserInfo);
            this.curInstance = driverEngine.GetInstanceById(userTaskById.InstanceId);
            if (!base.IsPostBack)
            {
                StringCollection stringCollections = new StringCollection();
                TaskService.GetShareTaskDealUser(this.taskId, stringCollections, null);
                string employeeNameList = EmployeeService.GetEmployeeNameList(stringCollections);
                if (!(userTaskById.IsShare != "1" ? true : !(userTaskById.TaskState == "0")))
                {
                    this.DealInfo.AppendFormat("该任务是共享任务，需要（{0}）其中一个人处理，点击【申请任务】即可办理", employeeNameList);
                    this.btnShareTask.CssClass = "hidden";
                }
                else if ((userTaskById.IsShare != "1" ? false : userTaskById.TaskState == "1"))
                {
                    this.DealInfo.Append("该任务是共享任务，点击【退还任务】，可以让其它人来处理");
                    this.btnApplyTask.CssClass = "hidden";
                }
            }
        }
    }
}