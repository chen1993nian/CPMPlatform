using EIS.AppBase;
using EIS.Permission.Service;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.WorkFlow
{
    public partial class DealFlowAfter : PageBase
    {
       

        public StringBuilder DealInfo = new StringBuilder();

        public string taskId = "";

        public string perror = "";

        public Task curTask = new Task();

        public Instance curInstance = new Instance();

        public DealFlowAfter()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            StringCollection stringCollections;
            string employeeNameList;
            UserTask userTask = null;
            this.taskId = base.GetParaValue("taskId");
            DriverEngine driverEngine = new DriverEngine(base.UserInfo);
            this.curTask = driverEngine.GetTaskById(this.taskId);
            this.curInstance = driverEngine.GetInstanceById(this.curTask.InstanceId);
            if (string.IsNullOrEmpty(base.GetParaValue("info")))
            {
                List<UserTask> toDoUserTask = TaskService.GetToDoUserTask(this.curInstance.InstanceId, this.curTask);
                if (toDoUserTask.Count <= 0)
                {
                    List<Task> nextManualTask = TaskService.GetNextManualTask(this.taskId);
                    if (nextManualTask.Count > 0)
                    {
                        this.DealInfo.Append("任务提交到");
                        foreach (Task task in nextManualTask)
                        {
                            List<UserTask> userTasks = TaskService.GetUserTask(this.curInstance.InstanceId, task);
                            if (userTasks.Count <= 0)
                            {
                                continue;
                            }
                            stringCollections = new StringCollection();
                            foreach (UserTask userTaskA in userTasks)
                            {
                                stringCollections.Add(userTaskA.OwnerId);
                            }
                            employeeNameList = EmployeeService.GetEmployeeNameList(stringCollections);
                            if ((employeeNameList != "") && (employeeNameList.Length<200))
                            {
                                this.DealInfo.AppendFormat("【{0}】、", employeeNameList);
                            }
                            else
                            {
                                this.DealInfo.AppendFormat("【{0}】、", task.TaskName);
                            }
                        }
                        if (this.DealInfo.Length > 1)
                        {
                            this.DealInfo.Length = this.DealInfo.Length - 1;
                        }
                    }
                    else if (this.curInstance.InstanceState != EnumDescription.GetFieldText(InstanceState.Finished))
                    {
                        this.DealInfo.Append("任务已经成功提交！");
                    }
                    else
                    {
                        this.DealInfo.Append("任务已经成功结束！");
                    }
                }
                else
                {
                    stringCollections = new StringCollection();
                    foreach (UserTask userTask1 in toDoUserTask)
                    {
                        stringCollections.Add(userTask1.OwnerId);
                    }
                    employeeNameList = EmployeeService.GetEmployeeNameList(stringCollections);
                    //this.DealInfo.AppendFormat("任务提交成功，但该步骤还有【{0}】没有处理,待他们处理过后才会转到下一步", employeeNameList);
                    this.DealInfo.AppendFormat("任务提交成功，但该步骤还有【{0}】尚未处理...", employeeNameList);
                }
            }
            else if (this.Session["_sysinfo"] != null)
            {
                this.DealInfo.Append(this.Session["_sysinfo"].ToString());
            }
        }
    }
}