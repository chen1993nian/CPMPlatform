using EIS.AppBase;
using EIS.DataAccess;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.SysFolder.WorkFlow
{
    public partial class DealFlowByGroup : PageBase
    {
    
        public string WFCode = string.Empty;

        public string WFName = string.Empty;

        public StringBuilder sbToDo = new StringBuilder();

        public StringBuilder sbMsg = new StringBuilder();

        public DealFlowByGroup()
        {
        }

        protected void btnBatch_Click(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.WFCode = base.GetParaValue("wfCode");
            Define workflowByCode = DefineService.GetWorkflowByCode(this.WFCode);
            string str = string.Format("select i._AutoID instanceId,i.InstanceName,i.EmployeeName as CreateUser,i.DeadLine\r\n                ,i.DeptName, u._AutoId uTaskId,u.TaskId,t.TaskName,t.ArriveTime,u.isread,u.OwnerId,u.agentId,isnull(u.isAssign,'0') isAssign \r\n                from T_E_WF_UserTask u inner join T_E_WF_Task t on u.TaskId=t._AutoID \r\n                inner join T_E_WF_Instance i on t.InstanceId=i._AutoID inner join T_E_WF_Define d on i.WorkflowId=d._AutoID\r\n                where u._isDel=0 and t.TaskState<>'2' and (u.TaskState in ('0', '1') and (u.ownerId='{0}'  or u.AgentId='{0}'))\r\n                and d.WorkflowCode='{1}' order by t.ArriveTime desc", base.EmployeeID, this.WFCode);
            DataTable dataTable = SysDatabase.ExecuteTable(str);
            this.WFName = string.Format("{0}<em>（{1}）</em>", workflowByCode.WorkflowName, dataTable.Rows.Count);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow item = dataTable.Rows[i];
                item["OwnerId"].ToString();
                string str1 = item["agentId"].ToString();
                string str2 = item["isAssign"].ToString();
                StringBuilder stringBuilder = this.sbToDo;
                object[] objArray = new object[] { item["uTaskId"], item["instanceName"], item["CreateUser"], item["ArriveTime"], null, null, null, null, null, null };
                objArray[4] = (item["isread"].ToString() == "0" ? "unread" : "readed");
                objArray[5] = (str1 == base.EmployeeID ? "[代办]&nbsp;" : "");
                objArray[6] = (str2 == "1" ? "[加签]&nbsp;" : "");
                objArray[7] = i + 1;
                objArray[8] = item["TaskName"];
                objArray[9] = (item["DeadLine"] == DBNull.Value ? "" : "duban");
                stringBuilder.AppendFormat("<tr><td><input type='checkbox' class='taskchk' value='{0}' /></td><td class='taskLink'><a class='{9}' href='DealFlow.aspx?taskId={0}' target='_blank'>{5}{6}{1}</a></td>\r\n                <td>{8}</td><td>{2}</td><td>{3:yyyy-MM-dd HH:mm}</td></tr>", objArray);
            }
        }
    }
}