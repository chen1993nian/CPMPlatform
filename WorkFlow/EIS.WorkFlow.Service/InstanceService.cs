using EIS.AppBase;
using EIS.DataAccess;
using EIS.WorkFlow.Access;
using EIS.WorkFlow.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace EIS.WorkFlow.Service
{
	public class InstanceService
	{
		public InstanceService()
		{
		}

		public static Instance GetInstanceByAppInfo(string appName, string appId)
		{
			Instance item;
			_Instance __Instance = new _Instance();
			List<Instance> instances = new List<Instance>();
			if (!string.IsNullOrEmpty(appName))
			{
				string[] strArrays = new string[] { "appName='", appName, "' and appId='", appId, "'" };
				instances = __Instance.GetModelList(string.Concat(strArrays));
			}
			else
			{
				instances = __Instance.GetModelList(string.Concat("appId='", appId, "'"));
			}
			if (instances.Count <= 0)
			{
				item = null;
			}
			else
			{
				item = instances[0];
			}
			return item;
		}

		public static Instance GetInstanceById(string instanceId)
		{
			return (new _Instance()).GetModel(instanceId);
		}

		public static string GetInstanceName(string instanceId)
		{
			string str = string.Concat("select instanceName from T_E_WF_Instance where _autoId='", instanceId, "'");
			return SysDatabase.ExecuteScalar(str).ToString();
		}

		public static DataTable GetUserDealInfo(string instanceId)
		{
			string str = string.Concat("select t.TaskName,t.ArriveTime,u.*,\r\n                (select EmployeeName from t_e_org_employee e where e._autoid=u.ownerId) EmployeeName \r\n                from t_e_wf_task t inner join t_e_wf_usertask u \r\n                on t._autoid=u.taskid where u._isdel=0 and u.TaskState='2' and t.instanceId='", instanceId, "' order by u._CreateTime ");
			return SysDatabase.ExecuteTable(str);
		}

		public static DataTable GetUserDealState(string instanceId)
		{
            return SysDatabase.ExecuteTable(string.Format(@"select * from (
	            select TaskName =case u.isAssign when '1' then t.TaskName+'〔加签〕' else t.TaskName end
	            ,t.ArriveTime	,u.TaskId,u._AutoID,u._CreateTime, u.OwnerId, u.AgentId, u.TaskState
	            , DealAdvice = case when IsNull(HideAdviceOnDeal,'')='1' and u.TaskState='2' and t.TaskState ='1' then '******' else u.DealAdvice end
	            , DealAction = case when IsNull(HideAdviceOnDeal,'')='1' and u.TaskState='2' and t.TaskState ='1' then '****' else u.DealAction end
	            , u.DealTime, u.PositionName, u.DeptName,
	            u.EmployeeName, u.ReadTime, u.DealUser, u.IsAssign,IsRead2 =case u.IsRead when '0' then '否' else '是' end ,t.ActivityId
	            ,(select count(*) from T_E_File_File where appId=u._autoId) fileCount,t.TaskState tState,u.Memo, t.NodeCode
	            from t_e_wf_task t 
	            inner join t_e_wf_usertask u on t._autoid=u.taskid
	            where t.instanceId='{0}' and u._isdel=0 and (u.TaskState='2' or u.TaskState ='1' ) and t.DefineType<>'End'
	            union
	            select t.TaskName,t.ArriveTime,t._AutoID,'',t.ArriveTime, '' OwnerId, '' AgentId, '0' TaskState
	            ,'' DealAdvice,'' DealAction, null DealTime, '' PositionName, '' DeptName
	            , dbo.WF_GetShareTaskEmployee(t._AutoID) EmployeeName, null ReadTime, '' DealUser, '0' IsAssign
	            ,'否',t.ActivityId,0 fileCount,t.TaskState tState,'' Memo , t.NodeCode
	            from t_e_wf_task t 
	            where t.instanceId='{0}' and  _AutoID in (select taskId from t_e_wf_usertask u where u._isdel=0 and (u.TaskState ='0' ))
	            ) tbl 
            Order by _CreateTime", instanceId));
		}

		public static bool IsRunAlready(string appName, string appId)
		{
			_Instance __Instance = new _Instance();
			string[] strArrays = new string[] { "appName='", appName, "' and appId='", appId, "'" };
			List<Instance> modelList = __Instance.GetModelList(string.Concat(strArrays));
			return modelList.Count > 0;
		}

		public static bool IsRunAlready(string appName, string appId, DbTransaction tran)
		{
			_Instance __Instance = new _Instance(tran);
			string[] strArrays = new string[] { "appName='", appName, "' and appId='", appId, "'" };
			List<Instance> modelList = __Instance.GetModelList(string.Concat(strArrays));
			return modelList.Count > 0;
		}

		public static void UpdateInstanceName(string instanceName, string instanceId, UserContext userInfo)
		{
			DbConnection dbConnection = SysDatabase.CreateConnection();
			dbConnection.Open();
			DbTransaction dbTransaction = dbConnection.BeginTransaction();
			try
			{
				try
				{
					_Instance __Instance = new _Instance(dbTransaction);
					Instance model = __Instance.GetModel(instanceId);
					__Instance.UpdateInstanceName(instanceName, instanceId);
					_WorkflowLog __WorkflowLog = new _WorkflowLog(dbTransaction);
					WorkflowLog workflowLog = new WorkflowLog(userInfo)
					{
						EmpName = userInfo.EmployeeName,
						AppId = instanceId,
						LogTime = new DateTime?(DateTime.Now),
						AppName = "T_E_WF_Instance",
						LogContent = string.Format("修改任务名称：{0}；{1}", model.InstanceName, instanceName)
					};
					__WorkflowLog.Add(workflowLog);
					dbTransaction.Commit();
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					dbTransaction.Rollback();
					throw exception;
				}
			}
			finally
			{
				dbConnection.Close();
			}
		}
	}
}