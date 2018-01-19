using EIS.DataAccess;
using EIS.Permission.Model;
using EIS.Permission.Service;
using EIS.WorkFlow.Access;
using EIS.WorkFlow.Model;
using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.WorkFlow.Service
{
	public class UserTaskService
	{
		public UserTaskService()
		{
		}

		public static bool CheckEmployeeInActivity(string instanceId, string actId, string empId, DbTransaction dbTran)
		{
			bool flag;
			string str = string.Format("select count(*) from t_e_wf_task t inner join t_e_wf_usertask u on t._AutoId=u.taskid\r\n                where u._isDel=0 and u.taskstate='1' and t.instanceId ='{1}' and t.activityId='{2}' and (u.ownerId='{0}')", empId, instanceId, actId);
			object obj = null;
			obj = (dbTran == null ? SysDatabase.ExecuteScalar(str) : SysDatabase.ExecuteScalar(str, dbTran));
			flag = ((obj == null ? false : obj != DBNull.Value) ? Convert.ToInt32(obj.ToString()) > 0 : false);
			return flag;
		}

		public static bool CheckEmployeeInTask(string taskId, string empId, DbTransaction dbTran)
		{
			bool flag;
			string str = string.Format("select count(*) from t_e_wf_task t inner join t_e_wf_usertask u on t._AutoId=u.taskid\r\n                where u._isDel=0 and u.taskstate='1' and t._autoId ='{1}' and (u.ownerId='{0}' or u.AgentId='{0}')", empId, taskId);
			object obj = null;
			obj = (dbTran == null ? SysDatabase.ExecuteScalar(str) : SysDatabase.ExecuteScalar(str, dbTran));
			flag = ((obj == null ? false : obj != DBNull.Value) ? Convert.ToInt32(obj.ToString()) > 0 : false);
			return flag;
		}

		public static int GetToDoCount(string employeeId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(*) From T_E_WF_UserTask u inner join T_E_WF_Instance i on u.InstanceId=i._autoId ");
			stringBuilder.AppendFormat(" where i.InstanceState='处理中' and u._isdel=0 and (u.ownerId='{0}' or u.agentId='{0}') and (u.taskstate='0' or u.taskstate='1')", employeeId);
			int num = Convert.ToInt32(SysDatabase.ExecuteScalar(stringBuilder.ToString()));
			return num;
		}

		public static DataTable GetToDoGroupByEmployeeId(string employeeId)
		{
			return SysDatabase.ExecuteTable(string.Format("select d.WorkflowCode,d.WorkflowName,i.AppName,COUNT(*) num from T_E_WF_UserTask u \r\n                inner join T_E_WF_Task t on u.TaskId=t._AutoID \r\n                inner join T_E_WF_Instance i on t.InstanceId=i._AutoID \r\n                inner join T_E_WF_Define d on i.WorkflowId=d._AutoID\r\n                where u._isDel=0 and t.TaskState<>'2' and (u.TaskState in ('0', '1') and (u.ownerId='{0}'  or u.AgentId='{0}'))\r\n                group by d.WorkflowCode,d.WorkflowName,i.AppName order by d.WorkflowCode", employeeId));
		}

		public static DataTable GetToDoUserTaskByEmployeeId(string employeeId)
		{
			return SysDatabase.ExecuteTable(string.Format("select i.employeeName as CreateUser,i._CreateTime,i.DeptName,i.instanceName,i._autoid as instanceId,i.AppName,i.AppId\r\n                ,t._AutoId as taskId,u._autoId uTaskId,t.TaskName,t.ArriveTime,u.OwnerId,u.agentId,u.isshare,u.taskState,u.isread,t.CanBatch\r\n                ,isnull(u.isAssign,'0') isAssign,(select c.CatalogName from T_E_WF_Catalog c where d.CatalogCode=c.CatalogCode) label\r\n                ,d.WorkflowCode,t.DefineType from T_E_WF_Instance i \r\n                inner join T_E_WF_Define d on i.WorkflowId = d._AutoID\r\n                inner join t_e_wf_task t on i._AutoId=t.instanceId\r\n                inner join t_e_wf_usertask u on t._AutoId=u.taskid\r\n                where u._isDel=0 and t.TaskState<>'2' and (u.TaskState in ('0', '1') and (u.ownerId='{0}'  or u.AgentId='{0}'))  \r\n                order by t.arrivetime desc", employeeId));
		}

		public static UserTask GetUserTaskById(string uTaskId, DbTransaction dbTran)
		{
			return (new _UserTask(dbTran)).GetModel(uTaskId);
		}

		internal static bool IsLastTaskConfirmed(string instanceId, string empId, DbTransaction dbTran)
		{
			bool flag;
			UserTask lastUserTaskByEmployeeId = (new _UserTask(dbTran)).GetLastUserTaskByEmployeeId(instanceId, empId);
			if (lastUserTaskByEmployeeId != null)
			{
				flag = (lastUserTaskByEmployeeId.DealAction == "" || lastUserTaskByEmployeeId.DealAction == "提交" ? true : lastUserTaskByEmployeeId.DealAction == "同意");
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public static void UpdateReadState(string userTaskId, UserTaskReadState state)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("update T_E_WF_UserTask set IsRead='{0}' , ReadTime='{1:yyyy-MM-dd HH:mm:ss}' ", (int)state, DateTime.Now);
			stringBuilder.AppendFormat(" where _AutoID='{0}' ", userTaskId);
			SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
		}

		public static void UpdateTaskAgent(string userTaskId, string agentId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(" update T_E_WF_UserTask set agentId='{0}' ", agentId);
			stringBuilder.AppendFormat(" where _AutoID='{0}' ", userTaskId);
			SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
		}

		public static void UpdateTaskOwner(string userTaskId, string ownerId, string posId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			UserTask model = (new _UserTask()).GetModel(userTaskId);
			DeptEmployee deptEmployeeByPositionId = DeptEmployeeService.GetDeptEmployeeByPositionId(ownerId, posId);
			object[] objArray = new object[] { ownerId, deptEmployeeByPositionId.EmployeeName, deptEmployeeByPositionId.DeptID, deptEmployeeByPositionId.DeptName, deptEmployeeByPositionId.PositionId, deptEmployeeByPositionId.PositionName };
			stringBuilder.AppendFormat(" update T_E_WF_UserTask set ownerId='{0}' ,employeeName='{1}',deptId='{2}',deptName='{3}',positionId='{4}',positionName='{5}' ", objArray);
			stringBuilder.AppendFormat(" where _AutoID='{0}' ;", userTaskId);
			stringBuilder.AppendFormat("update T_E_WF_Task set MainPerformer='{0}' ", ownerId);
			stringBuilder.AppendFormat(" where _AutoID='{0}' and MainPerformer='{1}'; ", model.TaskId, model.OwnerId);
			SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
		}
	}
}