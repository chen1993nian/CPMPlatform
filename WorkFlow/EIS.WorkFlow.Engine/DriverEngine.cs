using EIS.AppBase;
using EIS.AppModel.Workflow;
using EIS.DataAccess;

using EIS.DataModel.Model;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using EIS.WorkFlow.Access;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using EIS.WorkFlow.XPDL.Utility;
using EIS.WorkFlow.XPDLParser;
using EIS.WorkFlow.XPDLParser.Elements;
using Microsoft.Practices.Unity;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Xml;

namespace EIS.WorkFlow.Engine
{
	public class DriverEngine
	{
		public UserContext UserInfo = null;

		private WFSession _curSession = null;

		private Logger fileLogger = null;

		private List<Activity> tempList = new List<Activity>();

		private StringCollection scTranId = new StringCollection();

		private DataRow appData = null;

		public WFSession CurSession
		{
			get
			{
				return this._curSession;
			}
			set
			{
				this.UserInfo = value.UserInfo;
				this.appData = value.AppData;
				this._curSession = value;
			}
		}

		public DriverEngine()
		{
			this.fileLogger = LogManager.GetCurrentClassLogger();
		}

		public DriverEngine(UserContext user)
		{
			this.UserInfo = user;
			this.fileLogger = LogManager.GetCurrentClassLogger();
		}

		public void ArchiveInstance(Instance instance, DbTransaction tran)
		{
			instance._UpdateTime = DateTime.Now;
			instance.InstanceState = EnumDescription.GetFieldText(InstanceState.Archived);
			(new _Instance(tran)).Update(instance);
			this.UpdateAppDataState(instance, InstanceState.Archived, tran);
		}

		public StringCollection AssignTask(string taskId, IList ps, bool assignTask, DbTransaction tran)
		{
			UserTask userTask;
			string p = null;
			StringCollection stringCollections;
			DateTime? nullable;
			Task taskById = this.GetTaskById(taskId);
			Instance instanceById = this.GetInstanceById(taskById.InstanceId);
			Activity activityById = this.GetActivityById(instanceById, taskById.ActivityId);
			if (this.CurSession == null)
			{
				this.CurSession = new WFSession(this.UserInfo, instanceById, this.GetAppData(instanceById), activityById);
			}
			_Task __Task = new _Task(tran);
			_UserTask __UserTask = new _UserTask(tran);
			StringCollection stringCollections1 = new StringCollection();
			switch (activityById.GetNodeType())
			{
				case ActivityType.Start:
				case ActivityType.Normal:
				{
					if (ps.Count == 0)
					{
						throw new Exception("没有定义加签人");
					}
					if (taskById.DealStrategy == "1")
					{
						userTask = new UserTask()
						{
							TaskId = taskById.TaskId,
							UserTaskId = Guid.NewGuid().ToString(),
							_UserName = this.UserInfo.EmployeeId,
							_OrgCode = this.UserInfo.DeptWbs,
							_CreateTime = DateTime.Now,
							_UpdateTime = DateTime.Now,
							_IsDel = 0,
							InstanceId = instanceById.InstanceId,
							OwnerId = ps[0].ToString(),
                            //EmployeeName = EmployeeService.GetEmployeeName(userTask.OwnerId),
                            EmployeeName = EmployeeService.GetEmployeeName(ps[0].ToString()),
                            AgentId = EIS.WorkFlow.Engine.Utility.GetAgentId(instanceById.WorkflowId, ps[0].ToString(), tran),
                            //AgentId = EIS.WorkFlow.Engine.Utility.GetAgentId(instanceById.WorkflowId, userTask.OwnerId, tran),
							IsShare = "0",
							DealAdvice = "",
							DealAction = ""
						};
						nullable = null;
						userTask.DealTime = nullable;
						userTask.TaskState = 1.ToString();
						userTask.IsRead = "0";
						userTask.IsAssign = (assignTask ? "1" : "0");
						__UserTask.Add(userTask);
					}
					else if ((taskById.DealStrategy == "2" ? false : !(taskById.DealStrategy == "")))
					{
						if (!(taskById.DealStrategy == "3"))
						{
							throw new Exception(string.Concat("【", taskById.TaskName, "】步骤处理人策略定义有错，其值为：", taskById.DealStrategy));
						}
						foreach (string pA in ps)
						{
                            if ((stringCollections1.Contains(pA) ? false : !UserTaskService.CheckEmployeeInActivity(taskById.InstanceId, taskById.ActivityId, pA, tran)))
							{
                                stringCollections1.Add(pA);
								userTask = new UserTask()
								{
									TaskId = taskById.TaskId,
									UserTaskId = Guid.NewGuid().ToString(),
									_UserName = this.UserInfo.EmployeeId,
									_OrgCode = this.UserInfo.DeptWbs,
									_CreateTime = DateTime.Now,
									_UpdateTime = DateTime.Now,
									_IsDel = 0,
									InstanceId = instanceById.InstanceId,
                                    OwnerId = pA,
                                    EmployeeName = EmployeeService.GetEmployeeName(pA),
                                    AgentId = EIS.WorkFlow.Engine.Utility.GetAgentId(instanceById.WorkflowId, pA, tran),
									IsShare = "0",
									DealAdvice = "",
									DealAction = ""
								};
								nullable = null;
								userTask.DealTime = nullable;
								userTask.TaskState = 0.ToString();
								userTask.IsRead = "0";
								userTask.IsAssign = (assignTask ? "1" : "0");
								__UserTask.Add(userTask);
							}
						}
					}
					else
					{
						foreach (string str in ps)
						{
							if ((stringCollections1.Contains(str) ? false : !UserTaskService.CheckEmployeeInActivity(taskById.InstanceId, taskById.ActivityId, str, tran)))
							{
								stringCollections1.Add(str);
								userTask = new UserTask()
								{
									TaskId = taskById.TaskId,
									UserTaskId = Guid.NewGuid().ToString(),
									_UserName = this.UserInfo.EmployeeId,
									_OrgCode = this.UserInfo.DeptWbs,
									_CreateTime = DateTime.Now,
									_UpdateTime = DateTime.Now,
									_IsDel = 0,
									InstanceId = instanceById.InstanceId,
									OwnerId = str,
									EmployeeName = EmployeeService.GetEmployeeName(str),
									AgentId = EIS.WorkFlow.Engine.Utility.GetAgentId(instanceById.WorkflowId, str, tran),
									IsShare = "0",
									DealAdvice = "",
									DealAction = ""
								};
								nullable = null;
								userTask.DealTime = nullable;
								userTask.TaskState = 1.ToString();
								userTask.IsRead = "0";
								userTask.IsAssign = (assignTask ? "1" : "0");
								__UserTask.Add(userTask);
							}
						}
					}
					stringCollections = stringCollections1;
					return stringCollections;
				}
				case ActivityType.End:
				{
					stringCollections = stringCollections1;
					return stringCollections;
				}
				case ActivityType.Sign:
				{
					if (ps.Count == 0)
					{
						throw new Exception("没有定义加签人");
					}
					foreach (string p1 in ps)
					{
						if ((stringCollections1.Contains(p1) ? false : !UserTaskService.CheckEmployeeInActivity(taskById.InstanceId, taskById.ActivityId, p1, tran)))
						{
							stringCollections1.Add(p1);
							userTask = new UserTask()
							{
								TaskId = taskById.TaskId,
								UserTaskId = Guid.NewGuid().ToString(),
								_UserName = this.UserInfo.EmployeeId,
								_OrgCode = this.UserInfo.DeptWbs,
								_CreateTime = DateTime.Now,
								_UpdateTime = DateTime.Now,
								_IsDel = 0,
								InstanceId = instanceById.InstanceId,
								OwnerId = p1,
								EmployeeName = EmployeeService.GetEmployeeName(p1),
								AgentId = EIS.WorkFlow.Engine.Utility.GetAgentId(instanceById.WorkflowId, p1, tran),
								IsShare = "0",
								DealAdvice = "",
								DealAction = ""
							};
							nullable = null;
							userTask.DealTime = nullable;
							userTask.TaskState = 1.ToString();
							userTask.IsRead = "0";
							userTask.IsAssign = (assignTask ? "1" : "0");
							__UserTask.Add(userTask);
						}
					}
					stringCollections = stringCollections1;
					return stringCollections;
				}
				default:
				{
					stringCollections = stringCollections1;
					return stringCollections;
				}
			}
		}

		public StringCollection AssignTask(string taskId, IList ps, DbTransaction tran)
		{
			return this.AssignTask(taskId, ps, true, tran);
		}

		private bool CheckActivityState(Activity act, Instance ins, DbTransaction dbTran)
		{
			bool flag;
			bool flag1 = false;
			string lower = act.GetJoinType().ToLower();
			IList joinTransitions = this.GetJoinTransitions(act, ins);
			if (!(lower == "and"))
			{
				flag1 = true;
			}
			else
			{
				if (joinTransitions.Count > 1)
				{
					goto Label1;
				}
				flag1 = true;
			}
			flag = flag1;
			return flag;
		Label1:
			act.GetNodeType();
			int finishJoinTransitionCount = FinishTransitionService.GetFinishJoinTransitionCount(ins.InstanceId, act.GetId(), dbTran);
			Activity matchAndActivity = this.GetMatchAndActivity(act, ins, true);
			if (matchAndActivity == null)
			{
				flag = false;
				return flag;
			}
			else
			{
				matchAndActivity.GetId();
				int num = 0;
				IList splitTransitions = this.GetSplitTransitions(matchAndActivity, ins);
				for (int i = 0; i < splitTransitions.Count; i++)
				{
					Transition item = (Transition)splitTransitions[i];
					ExtendedAttribute extendedAttributeByName = item.GetExtendedAttributes().GetExtendedAttributeByName("State");
					if (extendedAttributeByName != null)
					{
						if (extendedAttributeByName.GetValue() == "1")
						{
							num++;
						}
					}
				}
				flag = finishJoinTransitionCount == num;
				return flag;
			}
		}

		private bool CheckActivityState(Activity act, Instance ins)
		{
			bool flag;
			bool flag1 = false;
			string lower = act.GetJoinType().ToLower();
			IList joinTransitions = this.GetJoinTransitions(act, ins);
			if (!(lower == "and"))
			{
				flag1 = true;
			}
			else
			{
				if (joinTransitions.Count > 1)
				{
					goto Label1;
				}
				flag1 = true;
			}
			flag = flag1;
			return flag;
		Label1:
			act.GetNodeType();
			int finishJoinTransitionCount = FinishTransitionService.GetFinishJoinTransitionCount(ins.InstanceId, act.GetId());
			Activity matchAndActivity = this.GetMatchAndActivity(act, ins, true);
			if (matchAndActivity == null)
			{
				flag = false;
				return flag;
			}
			else
			{
				matchAndActivity.GetId();
				int num = 0;
				IList splitTransitions = this.GetSplitTransitions(matchAndActivity, ins);
				for (int i = 0; i < splitTransitions.Count; i++)
				{
					Transition item = (Transition)splitTransitions[i];
					ExtendedAttribute extendedAttributeByName = item.GetExtendedAttributes().GetExtendedAttributeByName("State");
					if (extendedAttributeByName != null)
					{
						if (extendedAttributeByName.GetValue() == "1")
						{
							num++;
						}
					}
				}
				flag = finishJoinTransitionCount == num - 1;
				return flag;
			}
		}

		private bool CheckTransitionCondition(Instance instance, Transition tran, DbTransaction dbTran)
		{
			bool flag;
			bool flag1 = false;
			Condition condition = tran.GetCondition();
			string conditionTypeString = condition.GetConditionTypeString();
			string text = condition.GetText();
			text = EIS.AppBase.Utility.Xml2String(text);
			Logger logger = this.fileLogger;
			object[] instanceId = new object[] { instance.InstanceId, instance.InstanceName, conditionTypeString, text };
			logger.Debug("验证条件：InstanceId={0}，instanceName={1}，condType={2}，condExpr={3}", instanceId);
			if ((conditionTypeString == "" ? false : !(conditionTypeString == "condition")))
			{
				flag1 = false;
			}
			else
			{
				ExtendedAttribute extendedAttributeByName = tran.GetExtendedAttributes().GetExtendedAttributeByName("State");
				if (extendedAttributeByName != null)
				{
					if (extendedAttributeByName.GetValue() != "1")
					{
						flag = false;
						return flag;
					}
					if (!(text == ""))
					{
						text = this.ReplaceWithDataRow(text, instance.AppName, this.CurSession.AppData);
						this.fileLogger.Debug<string, string>("InstanceId={0},计算后的condExpr={1}", instance.InstanceId, text);
						try
						{
							string connectionId = condition.GetConnectionId();
							flag1 = (int.Parse(this.ExecuteScalar(string.Concat("select count(*) where ", text), connectionId, dbTran).ToString()) <= 0 ? false : true);
						}
						catch (Exception exception1)
						{
							Exception exception = exception1;
							this.fileLogger.Error<Exception>(exception);
							throw new Exception(string.Concat("验证分支条件时出错：条件表达式=", text), exception);
						}
					}
					else
					{
						flag1 = true;
					}
				}
				else
				{
					flag = false;
					return flag;
				}
			}
			flag = flag1;
			return flag;
		}

		private bool CheckTransitionCondition(Instance instance, Transition tran)
		{
			bool flag;
			bool flag1 = false;
			Condition condition = tran.GetCondition();
			string conditionTypeString = condition.GetConditionTypeString();
			string text = condition.GetText();
			text = EIS.AppBase.Utility.Xml2String(text);
			Logger logger = this.fileLogger;
			object[] instanceId = new object[] { instance.InstanceId, instance.InstanceName, conditionTypeString, text };
			logger.Debug("验证条件：InstanceId={0}，instanceName={1}，condType={2}，condExpr={3}", instanceId);
			if ((conditionTypeString == "" ? false : !(conditionTypeString == "condition")))
			{
				flag1 = false;
			}
			else
			{
				ExtendedAttribute extendedAttributeByName = tran.GetExtendedAttributes().GetExtendedAttributeByName("State");
				if (extendedAttributeByName == null)
				{
					flag = false;
					return flag;
				}
				else if (extendedAttributeByName.GetValue() != "1")
				{
					flag = false;
					return flag;
				}
				else if (!(text == ""))
				{
					text = this.ReplaceWithDataRow(text, instance.AppName, this.appData);
					this.fileLogger.Debug<string, string>("InstanceId={0},计算后的condExpr={1}", instance.InstanceId, text);
					try
					{
						if (this.appData == null)
						{
							if ((new Regex("{(\\w*\\.)?(\\w+)}", RegexOptions.IgnoreCase)).Matches(text).Count > 0)
							{
								flag = true;
								return flag;
							}
						}
						flag1 = (int.Parse(this.ExecuteScalar(string.Concat("select count(*) where ", text), condition.GetConnectionId(), null).ToString()) <= 0 ? false : true);
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						this.fileLogger.Error<Exception>(exception);
						throw new Exception(string.Concat("验证分支条件时出错：条件表达式=", text), exception);
					}
				}
				else
				{
					flag1 = true;
				}
			}
			flag = flag1;
			return flag;
		}

		private bool CheckTransitionConditionNoState(Instance instance, Transition tran)
		{
			bool flag = false;
			Condition condition = tran.GetCondition();
			string conditionTypeString = condition.GetConditionTypeString();
			string text = condition.GetText();
			text = EIS.AppBase.Utility.Xml2String(text);
			Logger logger = this.fileLogger;
			object[] instanceId = new object[] { instance.InstanceId, instance.InstanceName, conditionTypeString, text };
			logger.Debug("验证条件：InstanceId={0}，instanceName={1}，condType={2}，condExpr={3}", instanceId);
			if ((conditionTypeString == "" ? false : !(conditionTypeString == "condition")))
			{
				flag = false;
			}
			else if (!(text == ""))
			{
				text = this.ReplaceWithDataRow(text, instance.AppName, this.appData);
				this.fileLogger.Debug<string, string>("InstanceId={0},计算后的condExpr={1}", instance.InstanceId, text);
				string connectionId = condition.GetConnectionId();
				try
				{
					flag = (int.Parse(this.ExecuteScalar(string.Concat("select count(*) where ", text), connectionId, null).ToString()) <= 0 ? false : true);
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					this.fileLogger.Error<Exception>(exception);
					throw new Exception(string.Concat("验证分支条件时出错：条件表达式=", text), exception);
				}
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		private bool CheckTransitionOtherCondition(Instance instance, Transition tran, DbTransaction dbTran)
		{
			bool flag = false;
			Condition condition = tran.GetCondition();
			condition.GetConditionTypeString();
			string text = condition.GetText();
			text = EIS.AppBase.Utility.Xml2String(text);
			if (!(text == ""))
			{
				text = this.ReplaceWithDataRow(text, instance.AppName, this.CurSession.AppData);
				string connectionId = condition.GetConnectionId();
				flag = (int.Parse(this.ExecuteScalar(string.Concat("select count(*) where ", text), connectionId, dbTran).ToString()) <= 0 ? false : true);
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		public string CopyActivityPerformers(Define model, UserContext userInfo)
		{
			string outerXml;
			if (!(model.RememberUser != "是"))
			{
				string str = string.Format("select top 1 * from T_E_WF_Instance where deptId='{0}' and workFlowId='{1}' order by _CreateTime desc", userInfo.DeptId, model.WorkflowId);
				object obj = SysDatabase.ExecuteScalar(str);
				if (obj != null)
				{
					string str1 = obj.ToString();
					this.fileLogger.Trace("CopyActivityPerformers 找到参考 insId={0}, ", str1);
					Instance modelWithXPDL = (new _Instance()).GetModelWithXPDL(str1);
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(modelWithXPDL.XPDL);
					XmlNode xmlNodes = xmlDocument.SelectSingleNode("//Activities");
					XmlDocument xmlDocument1 = new XmlDocument();
					xmlDocument1.LoadXml(model.XPDL);
					foreach (XmlNode xmlNodes1 in xmlDocument1.SelectNodes("//Activity"))
					{
						string innerText = xmlNodes1.SelectSingleNode("Icon").InnerText;
						string value = xmlNodes1.Attributes["Id"].Value;
						if ((innerText == "2" ? true : innerText == "3"))
						{
							if (xmlNodes1.SelectSingleNode("ExtendedAttributes/ExtendedAttribute[@Name='CanRememberUser']").Attributes["Value"].Value == "1")
							{
								XmlNode innerXml = xmlNodes1.SelectSingleNode("Performers");
								XmlNode xmlNodes2 = xmlNodes.SelectSingleNode(string.Concat("Activity[@Id='", value, "']"));
								if (xmlNodes2 != null)
								{
									innerXml.InnerXml = xmlNodes2.SelectSingleNode("Performers").InnerXml;
									this.fileLogger.Trace<XmlNode, string>("CopyActivityPerformers ActId={0},定义={1} ", xmlNodes2, xmlNodes2.OuterXml);
								}
							}
						}
					}
					outerXml = xmlDocument1.OuterXml;
				}
				else
				{
					outerXml = model.XPDL;
				}
			}
			else
			{
				outerXml = model.XPDL;
			}
			return outerXml;
		}

		public List<Task> DirectSendTask(string fromTaskId, string toId, DbTransaction tran)
		{
			List<Task> tasks = new List<Task>();
			Task taskById = this.GetTaskById(fromTaskId);
			Instance instanceById = this.GetInstanceById(taskById.InstanceId);
			Activity activityById = this.GetActivityById(instanceById, taskById.ActivityId);
			Activity activity = this.GetActivityById(instanceById, toId);
			if (this.CurSession == null)
			{
				this.CurSession = new WFSession(this.UserInfo, instanceById, this.GetAppData(instanceById), activityById);
			}
			EIS.WorkFlow.Engine.Utility.DeleteUserTaskByActivityId(instanceById.InstanceId, fromTaskId, tran);
			return this.NewTask(instanceById, activity, taskById, tran);
		}

		public void EndInstance(Instance instance, DbTransaction tran)
		{
			this.ExecEventScript(instance, "endbefore", tran);
			this.ExecProgram(instance, "endbefore", tran);
			instance._UpdateTime = DateTime.Now;
			instance.FinishTime = new DateTime?(DateTime.Now);
			instance.InstanceState = EnumDescription.GetFieldText(InstanceState.Finished);
			(new _Instance(tran)).Update(instance);
			(new _UserTask(tran)).RemoveActiveUserTask(instance.InstanceId);
			(new _Task(tran)).RemoveActiveTask(instance.InstanceId);
			this.UpdateAppDataState(instance, InstanceState.Finished, tran);
			this.ExecEventScript(instance, "endafter", tran);
			this.ExecProgram(instance, "endafter", tran);
			try
			{
				ITaskAction taskAction = WFListener.Unity_WorkflowContainer.Resolve<ITaskAction>();
				taskAction.Task_Finish(instance, this.CurSession.AppData, this.CurSession.UserInfo, tran);
			}
			catch (ResolutionFailedException resolutionFailedException)
			{
				this.fileLogger.Debug("依赖注入异常，未找到接口ITaskAction映射的实现类[Task_Stop]");
			}
			catch (Exception exception)
			{
				this.fileLogger.Error<Exception>(exception);
			}
		}

		private void ExecEventScript(Instance ins, Activity actObj, string eventName, DbTransaction dbTran)
		{
			string str = "";
			ExtendedAttribute extendedAttributeByName = actObj.GetExtendedAttributes().GetExtendedAttributeByName("EventScript");
			if (extendedAttributeByName != null)
			{
				foreach (NonEmptyNode node in extendedAttributeByName.GetNodes())
				{
					if ((node.GetAttribute("EventName") != eventName ? false : !string.IsNullOrWhiteSpace(node.GetText())))
					{
						string attribute = node.GetAttribute("ConnectionId");
						str = this.ReplaceWithDataRow(node.GetText(), ins.AppName, this.CurSession.AppData);
						Logger logger = this.fileLogger;
						object[] message = new object[] { eventName, node.GetText(), str, attribute };
						logger.Debug("ExecEventScript,EventName={0},OrgEventSQL={1},EventSQL={2},connectionId={3}", message);
						try
						{
							if (string.IsNullOrWhiteSpace(attribute))
							{
								SysDatabase.ExecuteNonQuery(str, dbTran);
							}
							else
							{
								CustomDb customDb = new CustomDb();
								customDb.CreateDatabaseByConnectionId(attribute.Trim());
								customDb.ExecuteNonQuery(str);
							}
						}
						catch (SqlException sqlException1)
						{
							SqlException sqlException = sqlException1;
							Logger logger1 = this.fileLogger;
							message = new object[] { sqlException.Message, ins.InstanceId, ins.InstanceName, eventName, actObj.GetName(), str, this.UserInfo.EmployeeName };
							logger1.Error("EmpName={6},InstanceId={1},InstanceName={2},eventName={3},actName={4},EventSQL={5}\r\n执行事件脚本时发生错误：\r\n{0}", message);
							throw new Exception(string.Concat("执行事件脚本时发生错误：", sqlException.Message));
						}
						catch (DbException dbException1)
						{
							DbException dbException = dbException1;
							this.fileLogger.Error<DbException>("执行事件脚本时发生错误：{0}", dbException);
							throw new Exception(string.Concat("执行事件脚本时发生错误：", dbException.Message));
						}
						catch (Exception exception1)
						{
							Exception exception = exception1;
							this.fileLogger.Error<Exception>("执行事件脚本时发生错误：{0}", exception);
							throw new Exception(string.Concat("执行事件脚本时发生错误：", exception.Message));
						}
					}
				}
			}
		}

		internal void ExecEventScript(Instance ins, string eventName, DbTransaction dbTran)
		{
			string str = "";
			InstanceEventScripts eventScripts = XpdlModel.GetPackageFromText(ins.XPDL).GetEventScripts();
			if (eventScripts != null)
			{
				InstanceEventScript eventScript = eventScripts.GetEventScript(eventName.ToLower());
				if (eventScript != null)
				{
					string text = eventScript.GetText();
					if (!string.IsNullOrWhiteSpace(text))
					{
						string connectionId = eventScript.GetConnectionId();
						str = this.ReplaceWithDataRow(text, ins.AppName, this.CurSession.AppData);
						Logger logger = this.fileLogger;
						object[] message = new object[] { eventName, text, str, connectionId };
						logger.Debug("ExecEventScript,EventName={0},OrgEventSQL={1},EventSQL={2},connectionId={3}", message);
						try
						{
							if (string.IsNullOrWhiteSpace(connectionId))
							{
								SysDatabase.ExecuteNonQuery(str, dbTran);
							}
							else
							{
								CustomDb customDb = new CustomDb();
								customDb.CreateDatabaseByConnectionId(connectionId.Trim());
								customDb.ExecuteNonQuery(str);
							}
						}
						catch (SqlException sqlException1)
						{
							SqlException sqlException = sqlException1;
							Logger logger1 = this.fileLogger;
							message = new object[] { sqlException.Message, ins.InstanceId, ins.InstanceName, eventName, str, this.UserInfo.EmployeeName };
							logger1.Error("EmpName={5},InstanceId={1},InstanceName={2},eventName={3},EventSQL={4}\r\n执行事件脚本时发生错误：\r\n{0}", message);
							throw new Exception(string.Concat("执行流程事件脚本时发生错误：", sqlException.Message));
						}
						catch (DbException dbException1)
						{
							DbException dbException = dbException1;
							this.fileLogger.Error<DbException>("执行流程事件脚本时发生错误：{0}", dbException);
							throw new Exception(string.Concat("执行流程事件脚本时发生错误：", dbException.Message));
						}
						catch (Exception exception1)
						{
							Exception exception = exception1;
							this.fileLogger.Error<Exception>("执行流程事件脚本时发生错误：{0}", exception);
							throw new Exception(string.Concat("执行流程事件脚本时发生错误：", exception.Message));
						}
					}
				}
			}
		}

		private string ExecNodeBizLogic(Instance ins, string eventName, Task task, DataRow bizData, DbTransaction dbTran)
		{
			string str;
			Activity activityById = this.GetActivityById(ins, task.ActivityId);
			ExtendedAttribute extendedAttributeByName = activityById.GetExtendedAttributes().GetExtendedAttributeByName("BizLogic");
			if (extendedAttributeByName != null)
			{
				foreach (NonEmptyNode node in extendedAttributeByName.GetNodes())
				{
					eventName = eventName.ToLower();
					if ((node.GetAttribute("EventName") != eventName ? false : !string.IsNullOrWhiteSpace(node.GetAttribute("Value"))))
					{
						string[] strArrays = node.GetAttribute("Value").Split(new char[] { '|' });
						activityById.GetName();
						if ((int)strArrays.Length < 3)
						{
							str = "";
							return str;
						}
						else if ((strArrays[0].Trim() == "" || strArrays[1].Trim() == "" ? false : !(strArrays[2].Trim() == "")))
						{
							string str1 = string.Concat(EIS.AppBase.Utility.GetPhysicalRootPath(), "\\bin\\", strArrays[0]);
							string str2 = strArrays[1];
							string str3 = strArrays[2];
							try
							{
								Type type = Assembly.LoadFrom(str1).GetType(str2);
								object obj = Activator.CreateInstance(type);
								MethodInfo method = type.GetMethod(str3);
								object[] objArray = new object[] { ins, task, bizData, dbTran };
								str = (string)method.Invoke(obj, objArray);
								return str;
							}
							catch (Exception exception1)
							{
								Exception exception = exception1;
								this.fileLogger.Error(string.Concat("ExecNodeBizLogic过程调用外部组件（", strArrays[0], "）出错：", exception.Message));
								this.fileLogger.Error<Exception>(exception);
								throw new ArgumentException(exception.Message);
							}
						}
						else
						{
							str = "";
							return str;
						}
					}
				}
			}
			str = "";
			return str;
		}

        private string ExecProgram(Instance ins, Activity actObj, Task curTask, DataRow bizData, DbTransaction dbTran)
        {
            string str;
            string extendedAttribute = actObj.GetExtendedAttribute("ProgramInfo");
            string name = actObj.GetName();
            string[] strArrays = extendedAttribute.Split(new char[] { '|' });
            if (((int)strArrays.Length < 3 || strArrays[0] == "" || strArrays[1] == "" ? true : strArrays[2] == ""))
            {
                throw new Exception(string.Concat("程序集节点【", name, "】定义有误"));
            }
            string str1 = string.Concat(EIS.AppBase.Utility.GetPhysicalRootPath(), "\\bin\\", strArrays[0]);
            string str2 = strArrays[1];
            string str3 = strArrays[2];
            Type type = null;
            object obj = null;

            try
            {
                type = Assembly.LoadFrom(str1).GetType(str2);
                obj = Activator.CreateInstance(type);
            }
            catch (Exception exception1)
            {
                string errMsg = string.Format("程序集节点【{0}】定义有误,程序集[{1}]类名[{2}]。错误：{3}。", name, str1, str2, exception1.Message);
                throw new Exception(errMsg);
            }
            finally { }

            try
            {
                MethodInfo method = type.GetMethod(str3);
                object[] objArray = new object[] { ins, actObj, bizData, dbTran };
                if (str3 == "Task_Arrive") objArray = new object[] { ins, actObj, curTask, bizData, null,null, dbTran };
                else if (str3 == "Task_Finish") objArray = new object[] { ins, bizData, null, dbTran };
                else if (str3 == "Task_Rollback") objArray = new object[] { ins, actObj, curTask, bizData,null, dbTran };
                else if (str3 == "Task_Stop") objArray = new object[] { ins, bizData, null, dbTran };
                else if (str3 == "Task_Submit") objArray = new object[] { ins, actObj, curTask, bizData, null, dbTran };
                str = (string)method.Invoke(obj, objArray);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                string msg = exception.Message;
                if ((exception1.InnerException != null) && (exception1.InnerException.Message != ""))
                {
                    msg += "\r\n" + exception1.InnerException.Message;
                }
                this.fileLogger.Error(string.Concat("调用外部组件（", strArrays[0], "）出错：", msg));
                this.fileLogger.Error<Exception>(exception);
                throw new ArgumentException(msg);
            }
            return str;
        }


        private string ExecProgram(Instance ins, Activity actObj, DataRow bizData, DbTransaction dbTran)
        {
            string str;
            string extendedAttribute = actObj.GetExtendedAttribute("ProgramInfo");
            string name = actObj.GetName();
            string[] strArrays = extendedAttribute.Split(new char[] { '|' });
            if (((int)strArrays.Length < 3 || strArrays[0] == "" || strArrays[1] == "" ? true : strArrays[2] == ""))
            {
                throw new Exception(string.Concat("程序集节点【", name, "】定义有误"));
            }
            string str1 = string.Concat(EIS.AppBase.Utility.GetPhysicalRootPath(), "\\bin\\", strArrays[0]);
            string str2 = strArrays[1];
            string str3 = strArrays[2];
            try
            {
                Type type = Assembly.LoadFrom(str1).GetType(str2);
                object obj = Activator.CreateInstance(type);
                MethodInfo method = type.GetMethod(str3);
                object[] objArray = new object[] { ins, actObj, bizData, dbTran };
                str = (string)method.Invoke(obj, objArray);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                this.fileLogger.Error(string.Concat("调用外部组件（", strArrays[0], "）出错：", exception.Message));
                this.fileLogger.Error<Exception>(exception);
                throw new ArgumentException(exception.Message);
            }
            return str;
        }

		internal string ExecProgram(Instance ins, string eventName, DbTransaction dbTran)
		{
			string str;
			BizLogics bizLogics = XpdlModel.GetPackageFromText(ins.XPDL).GetBizLogics();
			if (bizLogics == null)
			{
				str = "";
			}
			else
			{
				BizLogic bizLogic = bizLogics.GetBizLogic(eventName.ToLower());
				if (bizLogic != null)
				{
					string value = bizLogic.GetValue();
					if (value.Length >= 3)
					{
						string[] strArrays = value.Split(new char[] { '|' });
						if (((int)strArrays.Length < 3 || strArrays[0] == "" || strArrays[1] == "" ? true : strArrays[2] == ""))
						{
							throw new Exception(string.Format("流程[{0}]事件程序集定义有误", eventName));
						}
						string str1 = string.Concat(EIS.AppBase.Utility.GetPhysicalRootPath(), "\\bin\\", strArrays[0]);
						string str2 = strArrays[1];
						string str3 = strArrays[2];
						try
						{
							Type type = Assembly.LoadFrom(str1).GetType(str2);
							object obj = Activator.CreateInstance(type);
							MethodInfo method = type.GetMethod(str3);
							DataRow appData = this.CurSession.AppData;
							object[] objArray = new object[] { ins, appData, dbTran };
							str = (string)method.Invoke(obj, objArray);
						}
						catch (Exception exception1)
						{
							Exception exception = exception1;
							this.fileLogger.Error(string.Concat("调用外部组件（", strArrays[0], "）出错：", exception.Message));
							this.fileLogger.Error<Exception>(exception);
							throw new ArgumentException(exception.Message);
						}
					}
					else
					{
						str = "";
					}
				}
				else
				{
					str = "";
				}
			}
			return str;
		}

		private object ExecuteScalar(string script, string connectionId, DbTransaction dbTran)
		{
			object obj;
			if (string.IsNullOrWhiteSpace(connectionId))
			{
				obj = (dbTran == null ? SysDatabase.ExecuteScalar(script) : SysDatabase.ExecuteScalar(script, dbTran));
			}
			else
			{
				CustomDb customDb = new CustomDb();
				customDb.CreateDatabaseByConnectionId(connectionId.Trim());
				obj = customDb.ExecuteScalar(script);
			}
			return obj;
		}

		private DataTable ExecuteTable(string script, string connectionId, DbTransaction dbTran)
		{
			DataTable dataTable;
			if (string.IsNullOrWhiteSpace(connectionId))
			{
				dataTable = (dbTran == null ? SysDatabase.ExecuteTable(script) : SysDatabase.ExecuteTable(script, dbTran));
			}
			else
			{
				CustomDb customDb = new CustomDb();
				customDb.CreateDatabaseByConnectionId(connectionId.Trim());
				dataTable = customDb.ExecuteTable(script);
			}
			return dataTable;
		}

		private bool FindNodePrev(Instance ins, Transition tran, string targetId)
		{
			bool flag;
			string extendedAttribute = tran.GetExtendedAttribute("From");
			if (!(extendedAttribute == targetId))
			{
				Activity activityById = this.GetActivityById(ins, extendedAttribute);
				if (activityById.GetNodeType() != ActivityType.Start)
				{
					IList joinTransitions = this.GetJoinTransitions(activityById, ins);
					bool flag1 = false;
					foreach (Transition joinTransition in joinTransitions)
					{
						extendedAttribute = joinTransition.GetExtendedAttribute("From");
						flag1 = (!(extendedAttribute == targetId) ? this.FindNodePrev(ins, joinTransition, targetId) : true);
						if (flag1)
						{
							break;
						}
					}
					flag = flag1;
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		public Activity GetActivityById(Instance instance, string actId)
		{
			Activity item = null;
			Package packageFromText = XpdlModel.GetPackageFromText(instance.XPDL);
			if (instance.ProcessId == null)
			{
				instance.ProcessId = "1";
			}
			IList activities = packageFromText.GetWorkflowProcesses().GetWorkflowProcessById(instance.ProcessId).GetActivities().GetActivities();
			for (int i = 0; i < activities.Count; i++)
			{
				item = (Activity)activities[i];
				if (item.GetId() == actId)
				{
					return item;
				}
			}
			throw new Exception("没有找到相应的节点");
		}

		public List<Activity> GetAllActivityPre(Instance instance, Activity activity)
		{
			this.GetAllActivityPreInternal(instance, activity);
			return this.tempList;
		}

		private void GetAllActivityPreInternal(Instance instance, Activity activity)
		{
			if (activity.GetNodeType() != ActivityType.Start)
			{
				IList joinTransitions = this.GetJoinTransitions(activity, instance);
				string from = "";
				for (int i = 0; i < joinTransitions.Count; i++)
				{
					from = ((Transition)joinTransitions[i]).GetFrom();
					if (this.tempList.FindIndex((Activity a) => a.GetId() == from) == -1)
					{
						Activity activityById = this.GetActivityById(instance, from);
						this.tempList.Add(activityById);
						this.GetAllActivityPreInternal(instance, activityById);
					}
				}
			}
		}

		public List<Activity> GetAllActivitySith(Instance instance, Activity activity)
		{
			this.GetAllActivitySithInternal(instance, activity);
			return this.tempList;
		}

		private void GetAllActivitySithInternal(Instance instance, Activity activity)
		{
			if (activity.GetNodeType() != ActivityType.End)
			{
				IList splitTransitions = this.GetSplitTransitions(activity, instance);
				string to = "";
				for (int i = 0; i < splitTransitions.Count; i++)
				{
					to = ((Transition)splitTransitions[i]).GetTo();
					if (this.tempList.FindIndex((Activity a) => a.GetId() == to) == -1)
					{
						Activity activityById = this.GetActivityById(instance, to);
						this.tempList.Add(activityById);
						this.GetAllActivitySithInternal(instance, activityById);
					}
				}
			}
		}

		public int GetAndActivityType(Activity act, Instance ins)
		{
			int num = 0;
			IList joinTransitionRefs = this.GetJoinTransitionRefs(act, ins);
			if ((this.GetSplitTransitionRefs(act, ins).Count <= 1 ? true : !(act.GetSplitType() == "AND")))
			{
				num = ((joinTransitionRefs.Count <= 1 ? true : !(act.GetJoinType() == "AND")) ? 0 : 2);
			}
			else
			{
				num = 1;
			}
			return num;
		}

		public DataRow GetAppData(Instance ins, DbTransaction dbTran)
		{
			string str;
			if (this.appData == null)
			{
				TableInfo model = (new EIS.DataModel.Access._TableInfo(ins.AppName)).GetModel();
				if (model != null)
				{
					if (model.TableType == 1)
					{
						str = string.Format("select * from {0} where _AutoId='{1}'", ins.AppName, ins.AppId);
                        this.appData = this.ExecuteTable(str, "", dbTran).Rows[0];
					}
					else if (model.TableType == 3)
					{
						string str1 = string.Concat(" _AutoID='", ins.AppId, "'");
						str = model.DetailSQL.Replace("|^condition^|", str1);
                        this.appData = this.ExecuteTable(str, model.ConnectionId, dbTran).Rows[0];
					}
				}
			}
			return this.appData;
		}

		public DataRow GetAppData(Instance ins)
		{
			string str;
			DataTable dataTable;
			if (this.appData == null)
			{
				TableInfo model = (new EIS.DataModel.Access._TableInfo(ins.AppName)).GetModel();
				if (model.TableType == 1)
				{
					str = string.Format("select * from {0} where _AutoId='{1}'", ins.AppName, ins.AppId);
					dataTable = this.ExecuteTable(str, "", null);
					if (dataTable.Rows.Count > 0)
					{
						this.appData = dataTable.Rows[0];
					}
				}
				else if (model.TableType == 3)
				{
					string str1 = string.Concat(" _AutoID='", ins.AppId, "'");
					str = model.DetailSQL.Replace("|^condition^|", str1);
					dataTable = this.ExecuteTable(str, model.ConnectionId, null);
					if (dataTable.Rows.Count > 0)
					{
						this.appData = dataTable.Rows[0];
					}
				}
			}
			return this.appData;
		}

		public DataFields GetDataFields(Instance ins, DbTransaction dbTran)
		{
			return new DataFields();
		}

		public Instance GetInstanceById(string instanceId)
		{
			Instance instance = new Instance();
			return (new _Instance()).GetModelWithXPDL(instanceId);
		}

		public IList GetJoinTransitionRefs(Activity act, Instance ins)
		{
			IList transitionRefs = new List<TransitionRef>();
			TransitionRestriction item = (TransitionRestriction)act.GetTransitionRestrictions().GetTransitionRestrictions()[0];
			if (item.GetJoin().IsTypeSpecified())
			{
				transitionRefs = item.GetJoin().GetTransitionRefs().GetTransitionRefs();
			}
			return transitionRefs;
		}

		public IList GetJoinTransitions(Activity act, Instance ins)
		{
			IList transitions = new List<Transition>();
			TransitionRestriction item = (TransitionRestriction)act.GetTransitionRestrictions().GetTransitionRestrictions()[0];
			if (item.GetJoin().IsTypeSpecified())
			{
				foreach (TransitionRef transitionRef in item.GetJoin().GetTransitionRefs().GetTransitionRefs())
				{
					string id = transitionRef.GetId();
					transitions.Add(this.GetTransitionById(ins, id));
				}
			}
			return transitions;
		}

		public Activity GetMatchAndActivity(Activity act, Instance ins, bool searchForward)
		{
			Activity activity;
			if (!searchForward)
			{
				activity = null;
			}
			else
			{
				activity = this.SearchForward(act, ins);
			}
			return activity;
		}

		public List<Activity> GetNextRealActivities(Instance instance, Transition tran, string path)
		{
			List<Activity> activities = new List<Activity>();
			Activity activityById = this.GetActivityById(instance, tran.GetTo());
			ActivityType nodeType = activityById.GetNodeType();
			if (!(nodeType == ActivityType.And || nodeType == ActivityType.Xor ? false : EIS.WorkFlow.Engine.Utility.IsManualTask(activityById)))
			{
				foreach (Transition splitTransition in this.GetSplitTransitions(activityById, instance))
				{
					if (this.CheckTransitionConditionNoState(instance, splitTransition))
					{
						activities.AddRange(this.GetNextRealActivities(instance, splitTransition, string.Concat(path, "$", tran.GetId())));
					}
				}
			}
			else if (!activities.Contains(activityById))
			{
				activityById.SelectPath = string.Concat(path, "$", tran.GetId());
				activities.Add(activityById);
			}
			return activities;
		}

		public string GetPreActivityId(Instance instance, Activity activity)
		{
			string from;
			if (activity.GetNodeType() != ActivityType.Start)
			{
				IList joinTransitions = this.GetJoinTransitions(activity, instance);
				if (joinTransitions.Count == 1)
				{
					from = ((Transition)joinTransitions[0]).GetFrom();
					return from;
				}
			}
			from = "";
			return from;
		}

		public IList GetSplitTransitionRefs(Activity act, Instance ins)
		{
			IList transitionRefs = new List<TransitionRef>();
			TransitionRestriction item = (TransitionRestriction)act.GetTransitionRestrictions().GetTransitionRestrictions()[0];
			if (item.GetSplit().IsTypeSpecified())
			{
				transitionRefs = item.GetSplit().GetTransitionRefs().GetTransitionRefs();
			}
			return transitionRefs;
		}

		public IList GetSplitTransitions(Activity act, Instance ins)
		{
			IList transitions = new List<Transition>();
			TransitionRestriction item = (TransitionRestriction)act.GetTransitionRestrictions().GetTransitionRestrictions()[0];
			if (item.GetSplit().IsTypeSpecified())
			{
				foreach (TransitionRef transitionRef in item.GetSplit().GetTransitionRefs().GetTransitionRefs())
				{
					string id = transitionRef.GetId();
					transitions.Add(this.GetTransitionById(ins, id));
				}
			}
			return transitions;
		}

		public Activity GetStartActivity(Instance instance)
		{
			Activity item = null;
			Package packageFromText = XpdlModel.GetPackageFromText(instance.XPDL);
			IList activities = packageFromText.GetWorkflowProcesses().GetWorkflowProcessById(instance.ProcessId).GetActivities().GetActivities();
			for (int i = 0; i < activities.Count; i++)
			{
				item = (Activity)activities[i];
				if (item.GetNodeType() == ActivityType.Start)
				{
					return item;
				}
			}
			throw new Exception("流程定义错误，没有找到开始结点");
		}

		public static Activity GetStartActivity(string workflowId)
		{
			Activity activity;
			Define workflowDefineModelById = DefineService.GetWorkflowDefineModelById(workflowId);
			Activity item = null;
			Package packageFromText = XpdlModel.GetPackageFromText(workflowDefineModelById.XPDL);
			IList activities = packageFromText.GetWorkflowProcesses().GetWorkflowProcessById("1").GetActivities().GetActivities();
			int num = 0;
			while (true)
			{
				if (num < activities.Count)
				{
					item = (Activity)activities[num];
					if (item.GetNodeType() != ActivityType.Start)
					{
						num++;
					}
					else
					{
						activity = item;
						break;
					}
				}
				else
				{
					activity = null;
					break;
				}
			}
			return activity;
		}

		private string GetStyleFromConfig()
		{
			string str;
			string str1 = string.Format("select IsNull(c.StyleExp,'') StyleExp from T_E_WF_Config c inner join T_E_WF_Define d\r\n                on c.WFId=d.WorkflowCode where c.Enable='是' and d._AutoID ='{0}'", this.CurSession.CurInstance.WorkflowId);
			DataTable dataTable = SysDatabase.ExecuteTable(str1);
			if (dataTable.Rows.Count != 0)
			{
				string str2 = dataTable.Rows[0]["StyleExp"].ToString();
				if (str2.Length > 0)
				{
					str2 = str2.Replace("\r\n", "`");
					char[] chrArray = new char[] { '\u0060' };
					string[] strArrays = str2.Split(chrArray);
					int num = 0;
					while (num < (int)strArrays.Length)
					{
						string str3 = strArrays[num].Replace("=>", "`");
						chrArray = new char[] { '\u0060' };
						string[] strArrays1 = str3.Split(chrArray);
						if (((int)strArrays1.Length != 2 || strArrays1[0].Length <= 0 ? true : strArrays1[1].Length <= 0))
						{
							num++;
						}
						else if (int.Parse(SysDatabase.ExecuteScalar(string.Concat("select count(*) where ", this.ReplaceWithDataRow(strArrays1[0], this.CurSession.CurInstance.AppName, this.CurSession.AppData))).ToString()) <= 0)
						{
							str = "";
							return str;
						}
						else
						{
							str = strArrays1[1];
							return str;
						}
					}
				}
				str = "";
			}
			else
			{
				str = "";
			}
			return str;
		}

		public Task GetTaskById(string taskId)
		{
			Task task = new Task();
			return (new _Task()).GetModel(taskId);
		}

		public Task GetTaskById(string taskId, DbTransaction dbTran)
		{
			Task task = new Task();
			return (new _Task(dbTran)).GetModel(taskId);
		}

        public Boolean IsRollBackTask(Task task)
        {
            string whereStr = string.Format(" InstanceId='{0}' and ActivityId='{1}' and _AutoID <> '{2}'" , task.InstanceId,task.ActivityId,task.TaskId);
            List<Task> taskList = (new _Task()).GetModelList(whereStr);
            if (taskList.Count > 0) return (true);
            return (false);
        }

		public Transition GetTransitionById(Instance instance, string tranId)
		{
			Transition transitionById = null;
			Package packageFromText = XpdlModel.GetPackageFromText(instance.XPDL);
			transitionById = packageFromText.GetWorkflowProcesses().GetWorkflowProcessById(instance.ProcessId).GetTransitions().GetTransitionById(tranId);
			return transitionById;
		}

		private void GetTransitionIdListToEnd(Instance ins, string startNodeId)
		{
			Activity activityById = this.GetActivityById(ins, startNodeId);
			if (activityById.GetNodeType() != ActivityType.End)
			{
				IList splitTransitions = this.GetSplitTransitions(activityById, ins);
				string to = "";
				for (int i = 0; i < splitTransitions.Count; i++)
				{
					string id = ((Transition)splitTransitions[i]).GetId();
					if (!this.scTranId.Contains(id))
					{
						this.scTranId.Add(id);
						to = ((Transition)splitTransitions[i]).GetTo();
						this.GetTransitionIdListToEnd(ins, to);
					}
				}
			}
		}

		public void HangUpInstance(Instance instance, DbTransaction tran)
		{
			instance._UpdateTime = DateTime.Now;
			instance.InstanceState = EnumDescription.GetFieldText(InstanceState.Suspended);
			(new _Instance(tran)).Update(instance);
			(new _UserTask(tran)).HangUpUserTaskByInstanceId(instance.InstanceId);
			this.UpdateAppDataState(instance, InstanceState.Suspended, tran);
		}

		public List<Task> NewTask(Instance instance, Activity actObj, Task oldTask, DbTransaction tran)
		{
			return this.NewTask(instance, actObj, oldTask, TaskType.Normal, tran);
		}

		public List<Task> NewTask(Instance instance, Activity actObj, Task oldTask, TaskType taskType, DbTransaction tran)
		{
			Task task;
			_Task __Task;
			_UserTask __UserTask;
			List<DeptEmployee> activityUser;
			UserTask userTask;
			bool flag;
			DeptEmployee deptEmployee = null;
			List<Task> tasks;
			DateTime? deadline;
			List<Task> tasks1 = new List<Task>();
			ActivityType nodeType = actObj.GetNodeType();
			if (!TaskService.CheckTaskReSubmit(oldTask.TaskId, actObj.GetId(), tran))
			{
				task = new Task();
				__Task = new _Task(tran);
				__UserTask = new _UserTask(tran);
				task.TaskId = Guid.NewGuid().ToString();
				task._UserName = this.UserInfo.EmployeeId;
				task._OrgCode = this.UserInfo.DeptWbs;
				task._CreateTime = DateTime.Now;
				task._UpdateTime = DateTime.Now;
				task._IsDel = 0;
				task.InstanceId = instance.InstanceId;
				task.TaskName = actObj.GetName();
				task.ActivityId = actObj.GetId();
				task.DefineType = actObj.GetNodeType().ToString();
				task.TaskType = taskType.ToString();
				task.ArriveTime = DateTime.Now;
				task.FromTaskId = oldTask.TaskId;
				task.NodeCode = actObj.GetCode();
				task.NodeStyle = actObj.GetStyleName();
				if (task.NodeStyle.Trim() == "")
				{
					string styleFromConfig = this.GetStyleFromConfig();
					if (styleFromConfig != "")
					{
						task.NodeStyle = styleFromConfig;
					}
				}
				if (!(task.DefineType == "End"))
				{
					task.IsManualTask = (EIS.WorkFlow.Engine.Utility.IsManualTask(actObj) ? "1" : "0");
				}
				else
				{
					task.IsManualTask = "0";
				}
				task.TaskState = 0.ToString();
				task.CanFetch = actObj.GetSafeOption("CanFetch");
				task.CanRollBack = actObj.GetSafeOption("CanRollBack");
				task.CanDelegateTo = actObj.GetSafeOption("CanDelegateTo");
				task.CanAssign = actObj.GetSafeOption("CanAssign");
				task.CanPublic = actObj.GetSafeOption("CanPublic");
				task.CanJump = actObj.GetSafeOption("CanJump");
				task.CanHangUp = actObj.GetSafeOption("CanHangUp");
				task.CanStop = actObj.GetSafeOption("CanStop");
				task.CanReturn = actObj.GetSafeOption("CanReturn");
				task.CanSelPath = actObj.GetSafeOption("CanSelPath");
				task.CanBatch = actObj.GetSafeOption("CanBatch");
				StringCollection stringCollections = new StringCollection();
				activityUser = EIS.WorkFlow.Engine.Utility.GetActivityUser(instance, actObj, this.CurSession, tran, ref stringCollections);
				string dealStrategy = actObj.GetDealStrategy();
				char[] chrArray = new char[] { '|' };
				string[] strArrays = dealStrategy.Split(chrArray);
				if ((strArrays[1] != "1" ? true : activityUser.Count != 0))
				{
					string extendedAttribute = actObj.GetExtendedAttribute("OverTimeDate");
					chrArray = new char[] { '|' };
					string[] strArrays1 = extendedAttribute.Split(chrArray);
					if (strArrays1[0] == "2")
					{
						string calendarIdByDeptId = "";
						string str = actObj.GetExtendedAttribute("OverTimeCalendar");
						if (str.Length > 0)
						{
							chrArray = new char[] { '|' };
							string[] strArrays2 = str.Split(chrArray);
							if (!(strArrays2[0] != "1" ? true : activityUser.Count <= 0))
							{
								calendarIdByDeptId = WFCalendar.GetCalendarIdByDeptId(activityUser[0].DeptID);
							}
							else if (strArrays2[0] == "2")
							{
								calendarIdByDeptId = WFCalendar.GetCalendarIdByDeptId(instance.DeptId);
							}
							else if (strArrays2[0] == "3")
							{
								calendarIdByDeptId = strArrays2[1];
							}
						}
						string str1 = strArrays1[1];
						chrArray = new char[] { ',' };
						string[] strArrays3 = str1.Split(chrArray);
						float hoursOneDay = WFCalendar.GetHoursOneDay(calendarIdByDeptId);
						double num = (double)(hoursOneDay * float.Parse(strArrays3[0]) + float.Parse(strArrays3[1]));
						TimeSpan timeSpan = TimeSpan.FromHours(num);
						task.Deadline = WFCalendar.ComputeNextWorkTime(task.ArriveTime, timeSpan, calendarIdByDeptId);
						deadline = task.Deadline;
						if (deadline.HasValue)
						{
							string extendedAttribute1 = actObj.GetExtendedAttribute("OverTimeAlert");
							chrArray = new char[] { '|' };
							string[] strArrays4 = extendedAttribute1.Split(chrArray);
							if ((int)strArrays4.Length >= 4)
							{
								if (!(strArrays4[0] == "1"))
								{
									task.OverTimeAlert = 0;
									deadline = null;
									task.OverTimeAlertFirst = deadline;
								}
								else
								{
									task.OverTimeAlert = 1;
									string str2 = strArrays4[1];
									chrArray = new char[] { ',' };
									strArrays3 = str2.Split(chrArray);
									TimeSpan timeSpan1 = new TimeSpan(int.Parse(strArrays3[0]), int.Parse(strArrays3[1]), 0);
									if (!(timeSpan > timeSpan1))
									{
										task.OverTimeAlert = 0;
										deadline = null;
										task.OverTimeAlertFirst = deadline;
									}
									else
									{
										timeSpan = timeSpan - timeSpan1;
										task.OverTimeAlertFirst = WFCalendar.ComputeNextWorkTime(task.ArriveTime, timeSpan, calendarIdByDeptId);
										task.OverTimeAlertRepeat = int.Parse(strArrays4[2]);
										string str3 = strArrays4[3];
										chrArray = new char[] { ',' };
										string[] strArrays5 = str3.Split(chrArray);
										task.OverTimeAlertInterval = 60 * int.Parse(strArrays5[0]) + int.Parse(strArrays5[1]);
									}
								}
							}
							task.OverTimeAction = actObj.GetExtendedAttribute("OverTimeAction");
							task.OverTimeCalendarId = calendarIdByDeptId;
						}
					}
					string signStrategy = actObj.GetSignStrategy();
					chrArray = new char[] { '|' };
					string[] strArrays6 = signStrategy.Split(chrArray);
					if ((int)strArrays6.Length <= 3)
					{
						task.HideAdviceOnDeal = "0";
					}
					else
					{
						task.HideAdviceOnDeal = (strArrays6[3] == "1" ? "1" : "0");
					}
					string extendedAttribute2 = actObj.GetExtendedAttribute("AutoSkip");
					if (extendedAttribute2.Trim() == "")
					{
						extendedAttribute2 = "0|1|0";
					}
					chrArray = new char[] { '|' };
					string[] strArrays7 = extendedAttribute2.Split(chrArray);
					if (activityUser.Count == 1)
					{
						if ((oldTask.DefineType == ActivityType.Start.ToString() ? true : oldTask.DefineType == ActivityType.Normal.ToString()))
						{
							if (!(strArrays7[1] != "1" ? true : !activityUser[0].EmployeeID.Equals(this.UserInfo.EmployeeId)))
							{
                                if (!HttpContext.Current.Request.Url.ToString().Contains("FlowTaskRollBack"))
                                {
                                    //不是回退
                                    task.TaskState = 2.ToString();
                                    task.IsManualTask = "0";
                                    __Task.Add(task);
                                    tasks1.Add(task);
                                    userTask = new UserTask()
                                    {
                                        TaskId = task.TaskId,
                                        UserTaskId = Guid.NewGuid().ToString(),
                                        _UserName = this.UserInfo.EmployeeId,
                                        _OrgCode = this.UserInfo.DeptWbs,
                                        _CreateTime = DateTime.Now,
                                        _UpdateTime = DateTime.Now,
                                        _IsDel = 0,
                                        InstanceId = instance.InstanceId,
                                        OwnerId = activityUser[0].EmployeeID,
                                        DealUser = activityUser[0].EmployeeID,
                                        //EmployeeName = EmployeeService.GetEmployeeName(userTask.OwnerId)
                                        EmployeeName = EmployeeService.GetEmployeeName(activityUser[0].EmployeeID)
                                    };
                                    UserTask userTaskByTaskId = EIS.WorkFlow.Engine.Utility.GetUserTaskByTaskId(oldTask.TaskId, this.UserInfo.EmployeeId, tran);
                                    userTask.AgentId = EIS.WorkFlow.Engine.Utility.GetAgentId(instance.WorkflowId, userTask.OwnerId, tran);
                                    userTask.IsShare = "0";
                                    if (userTaskByTaskId == null)
                                    {
                                        userTask.DealAdvice = "意见同上";
                                        userTask.DealAction = "";
                                    }
                                    else
                                    {
                                        userTask.DealAdvice = userTaskByTaskId.DealAdvice;
                                        if (!actObj.IsDecisionNode())
                                        {
                                            userTask.DealAction = "提交";
                                        }
                                        else
                                        {
                                            userTask.DealAction = "同意";
                                        }
                                    }
                                    userTask.DealTime = new DateTime?(DateTime.Now);
                                    userTask.TaskState = 2.ToString();
                                    userTask.IsRead = "1";
                                    userTask.ReadTime = new DateTime?(DateTime.Now);
                                    userTask.PositionId = activityUser[0].PositionId;
                                    userTask.PositionName = activityUser[0].PositionName;
                                    userTask.DeptId = activityUser[0].DeptID;
                                    userTask.DeptName = activityUser[0].DeptName;
                                    userTask.RecIds = this.UserInfo.EmployeeId;
                                    userTask.RecNames = this.UserInfo.EmployeeName;
                                    __UserTask.Add(userTask);
                                    this.ExecEventScript(instance, actObj, "OnTaskSubmit", tran);
                                    EIS.WorkFlow.Engine.Utility.SendAlert(instance, actObj, task, this.CurSession, 2, tran);
                                    this.ExecNodeBizLogic(instance, "OnTaskArrive", task, this.CurSession.AppData, tran);
                                    this.ExecNodeBizLogic(instance, "OnTaskSubmit", task, this.CurSession.AppData, tran);
                                    tasks1 = this.NextTask(instance, task, tran);
                                    tasks = tasks1;
                                    return tasks;
                                }
							}
							else if (strArrays7[2] == "1")
							{
								flag = UserTaskService.IsLastTaskConfirmed(instance.InstanceId, activityUser[0].EmployeeID, tran);
								if (flag)
								{
									goto Label1;
								}
							}
						}
					}
					switch (nodeType)
					{
						case ActivityType.Start:
						case ActivityType.Normal:
						case ActivityType.Auto:
						{
							if (!(actObj.GetFinishMode().GetMode().GetType() == typeof(Automatic)))
							{
								if (activityUser.Count == 0)
								{
									throw new NoUserException(actObj, stringCollections, this.CurSession.AppData);
								}
								if (strArrays[0] == "1")
								{
									task.MainPerformer = activityUser[0].EmployeeID;
									userTask = new UserTask()
									{
										TaskId = task.TaskId,
										UserTaskId = Guid.NewGuid().ToString(),
										_UserName = this.UserInfo.EmployeeId,
										_OrgCode = this.UserInfo.DeptWbs,
										_CreateTime = DateTime.Now,
										_UpdateTime = DateTime.Now,
										_IsDel = 0,
										InstanceId = instance.InstanceId,
										OwnerId = activityUser[0].EmployeeID,
										EmployeeName = activityUser[0].EmployeeName,
										PositionId = activityUser[0].PositionId,
										PositionName = activityUser[0].PositionName,
										DeptId = activityUser[0].DeptID,
										DeptName = activityUser[0].DeptName
									};
									userTask.EmployeeName = EmployeeService.GetEmployeeName(userTask.OwnerId);
									userTask.AgentId = EIS.WorkFlow.Engine.Utility.GetAgentId(instance.WorkflowId, userTask.OwnerId, tran);
									userTask.IsShare = "0";
									userTask.DealAdvice = "";
									userTask.DealAction = "";
									deadline = null;
									userTask.DealTime = deadline;
									userTask.TaskState = 1.ToString();
									userTask.IsRead = "0";
									userTask.IsAssign = "0";
									userTask.DealType = "1";
									__UserTask.Add(userTask);
								}
								else if (!(strArrays[0] == "2"))
								{
									if (!(strArrays[0] == "3"))
									{
										throw new Exception(string.Concat("【", task.TaskName, "】步骤处理人策略定义有错，其值为：", strArrays[0]));
									}
									task.MainPerformer = activityUser[0].EmployeeID;
									foreach (DeptEmployee deptEmployeeA in activityUser)
									{
                                        if (!UserTaskService.CheckEmployeeInActivity(task.InstanceId, task.ActivityId, deptEmployeeA.EmployeeID, tran))
										{
											userTask = new UserTask()
											{
												TaskId = task.TaskId,
												UserTaskId = Guid.NewGuid().ToString(),
												_UserName = this.UserInfo.EmployeeId,
												_OrgCode = this.UserInfo.DeptWbs,
												_CreateTime = DateTime.Now,
												_UpdateTime = DateTime.Now,
												_IsDel = 0,
												InstanceId = instance.InstanceId,
                                                OwnerId = deptEmployeeA.EmployeeID,
                                                EmployeeName = deptEmployeeA.EmployeeName,
                                                PositionId = deptEmployeeA.PositionId,
                                                PositionName = deptEmployeeA.PositionName,
                                                DeptId = deptEmployeeA.DeptID,
                                                DeptName = deptEmployeeA.DeptName,
                                                AgentId = EIS.WorkFlow.Engine.Utility.GetAgentId(instance.WorkflowId, deptEmployeeA.EmployeeID, tran),
												IsShare = "0",
												DealAdvice = "",
												DealAction = ""
											};
											deadline = null;
											userTask.DealTime = deadline;
											userTask.TaskState = 1.ToString();
											userTask.IsRead = "0";
											userTask.IsAssign = "0";
											userTask.DealType = "1";
											__UserTask.Add(userTask);
										}
									}
								}
								else if (activityUser.Count > 1)
								{
									foreach (DeptEmployee deptEmployee1 in activityUser)
									{
										if (!UserTaskService.CheckEmployeeInActivity(task.InstanceId, task.ActivityId, deptEmployee1.EmployeeID, tran))
										{
											userTask = new UserTask()
											{
												TaskId = task.TaskId,
												UserTaskId = Guid.NewGuid().ToString(),
												_UserName = this.UserInfo.EmployeeId,
												_OrgCode = this.UserInfo.DeptWbs,
												_CreateTime = DateTime.Now,
												_UpdateTime = DateTime.Now,
												_IsDel = 0,
												InstanceId = instance.InstanceId,
												OwnerId = deptEmployee1.EmployeeID,
												EmployeeName = deptEmployee1.EmployeeName,
												PositionId = deptEmployee1.PositionId,
												PositionName = deptEmployee1.PositionName,
												DeptId = deptEmployee1.DeptID,
												DeptName = deptEmployee1.DeptName,
												AgentId = EIS.WorkFlow.Engine.Utility.GetAgentId(instance.WorkflowId, deptEmployee1.EmployeeID, tran),
												IsShare = "1",
												DealAdvice = "",
												DealAction = ""
											};
											deadline = null;
											userTask.DealTime = deadline;
											userTask.TaskState = 0.ToString();
											userTask.IsRead = "0";
											userTask.IsAssign = "0";
											userTask.DealType = "1";
											__UserTask.Add(userTask);
										}
									}
								}
								else if (!UserTaskService.CheckEmployeeInActivity(task.InstanceId, task.ActivityId, activityUser[0].EmployeeID, tran))
								{
									task.MainPerformer = activityUser[0].EmployeeID;
									userTask = new UserTask()
									{
										TaskId = task.TaskId,
										UserTaskId = Guid.NewGuid().ToString(),
										_UserName = this.UserInfo.EmployeeId,
										_OrgCode = this.UserInfo.DeptWbs,
										_CreateTime = DateTime.Now,
										_UpdateTime = DateTime.Now,
										_IsDel = 0,
										InstanceId = instance.InstanceId,
										OwnerId = activityUser[0].EmployeeID,
										EmployeeName = activityUser[0].EmployeeName,
										PositionId = activityUser[0].PositionId,
										PositionName = activityUser[0].PositionName,
										DeptId = activityUser[0].DeptID,
										DeptName = activityUser[0].DeptName,
                                        //AgentId = EIS.WorkFlow.Engine.Utility.GetAgentId(instance.WorkflowId, userTask.OwnerId, tran),
                                        AgentId = EIS.WorkFlow.Engine.Utility.GetAgentId(instance.WorkflowId, activityUser[0].EmployeeID, tran),
										IsShare = "0",
										DealAdvice = "",
										DealAction = ""
									};
									deadline = null;
									userTask.DealTime = deadline;
									userTask.TaskState = 1.ToString();
									userTask.IsRead = "0";
									userTask.IsAssign = "0";
									userTask.DealType = "1";
									__UserTask.Add(userTask);
								}
								task.DealStrategy = strArrays[0];
								__Task.Add(task);
								tasks1.Add(task);
								EIS.WorkFlow.Engine.Utility.SendAlert(instance, actObj, task, this.CurSession, 1, tran);
								this.ExecEventScript(instance, actObj, "OnTaskArrive", tran);
								this.ExecNodeBizLogic(instance, "OnTaskArrive", task, this.CurSession.AppData, tran);
							}
							else
							{
								task.TaskState = 2.ToString();
								__Task.Add(task);
								this.ExecNodeBizLogic(instance, "OnTaskArrive", task, this.CurSession.AppData, tran);
								this.ExecNodeBizLogic(instance, "OnTaskSubmit", task, this.CurSession.AppData, tran);
								tasks1 = this.NextTask(instance, task, tran);
							}
							goto case ActivityType.SubWorkflow;
						}
						case ActivityType.End:
						{
							oldTask.TaskState = 2.ToString();
							oldTask._UpdateTime = DateTime.Now;
							__Task.Update(oldTask);
							task.TaskState = 2.ToString();
							__Task.Add(task);
							tasks1.Add(task);
							UserTask userTask1 = new UserTask()
							{
								TaskId = task.TaskId,
								UserTaskId = Guid.NewGuid().ToString(),
								_UserName = this.UserInfo.EmployeeId,
								_OrgCode = this.UserInfo.DeptWbs,
								_CreateTime = DateTime.Now.AddSeconds(1),
                                //_UpdateTime = userTask1._CreateTime,
                                _UpdateTime = DateTime.Now,
								_IsDel = 0,
								InstanceId = instance.InstanceId,
								OwnerId = this.UserInfo.EmployeeId,
								EmployeeName = this.UserInfo.EmployeeName,
								PositionId = this.UserInfo.PositionId,
								PositionName = this.UserInfo.PositionName,
								DeptId = this.UserInfo.DeptId,
								DeptName = this.UserInfo.DeptName,
								AgentId = "",
								IsShare = "0",
								DealAdvice = "完成",
								DealAction = "提交",
                                DealTime = new DateTime?(DateTime.Now),
                                ReadTime = new DateTime?(DateTime.Now),
                                //DealTime = new DateTime?(userTask1._CreateTime),
                                //ReadTime = new DateTime?(userTask1._CreateTime),
								TaskState = 2.ToString(),
								IsRead = "1",
								IsAssign = "0",
								DealType = "1"
							};
							__UserTask.Add(userTask1);
							this.ExecEventScript(instance, actObj, "OnTaskArrive", tran);
							this.ExecEventScript(instance, actObj, "OnTaskSubmit", tran);
							this.ExecNodeBizLogic(instance, "OnTaskArrive", task, this.CurSession.AppData, tran);
							this.ExecNodeBizLogic(instance, "OnTaskSubmit", task, this.CurSession.AppData, tran);
							EIS.WorkFlow.Engine.Utility.SendAlert(instance, actObj, task, this.CurSession, 1, tran);
							EIS.WorkFlow.Engine.Utility.SendAlert(instance, actObj, task, this.CurSession, 2, tran);
							this.EndInstance(instance, tran);
							goto case ActivityType.SubWorkflow;
						}
						case ActivityType.Sign:
						{
							if (activityUser.Count == 0)
							{
								throw new NoUserException(actObj, stringCollections, this.CurSession.AppData);
							}
							task.MainPerformer = "";
							__Task.Add(task);
							tasks1.Add(task);
							foreach (DeptEmployee deptEmployee2 in activityUser)
							{
								if (!UserTaskService.CheckEmployeeInActivity(task.InstanceId, task.ActivityId, deptEmployee2.EmployeeID, tran))
								{
									userTask = new UserTask()
									{
										TaskId = task.TaskId,
										UserTaskId = Guid.NewGuid().ToString(),
										_UserName = this.UserInfo.EmployeeId,
										_OrgCode = this.UserInfo.DeptWbs,
										_CreateTime = DateTime.Now,
										_UpdateTime = DateTime.Now,
										_IsDel = 0,
										InstanceId = instance.InstanceId,
										OwnerId = deptEmployee2.EmployeeID,
										EmployeeName = deptEmployee2.EmployeeName,
										PositionId = deptEmployee2.PositionId,
										PositionName = deptEmployee2.PositionName,
										DeptId = deptEmployee2.DeptID,
										DeptName = deptEmployee2.DeptName,
										AgentId = EIS.WorkFlow.Engine.Utility.GetAgentId(instance.WorkflowId, deptEmployee2.EmployeeID, tran),
										IsShare = "0",
										DealAdvice = "",
										DealAction = ""
									};
									deadline = null;
									userTask.DealTime = deadline;
									userTask.TaskState = 1.ToString();
									userTask.IsRead = "0";
									userTask.IsAssign = "0";
									userTask.DealType = deptEmployee2.DealType;
									__UserTask.Add(userTask);
								}
							}
							EIS.WorkFlow.Engine.Utility.SendAlert(instance, actObj, task, this.CurSession, 1, tran);
							this.ExecEventScript(instance, actObj, "OnTaskArrive", tran);
							this.ExecNodeBizLogic(instance, "OnTaskArrive", task, this.CurSession.AppData, tran);
							goto case ActivityType.SubWorkflow;
						}
						case ActivityType.Mail:
						{
							this.SendMail(instance, actObj, task, this.CurSession.AppData, tran);
							task.TaskState = 2.ToString();
							__Task.Add(task);
							tasks1 = this.NextTask(instance, task, tran);
							goto case ActivityType.SubWorkflow;
						}
						case ActivityType.Dll:
						{
							this.ExecProgram(instance, actObj,task, this.CurSession.AppData, tran);
							task.TaskState = 2.ToString();
							__Task.Add(task);
							tasks1 = this.NextTask(instance, task, tran);
							goto case ActivityType.SubWorkflow;
						}
						case ActivityType.And:
						case ActivityType.Xor:
						case ActivityType.Block:
						case ActivityType.SubWorkflow:
						{
							tasks = tasks1;
							break;
						}
						default:
						{
							goto case ActivityType.SubWorkflow;
						}
					}
				}
				else
				{
					task.TaskState = 2.ToString();
					task.IsManualTask = "0";
					__Task.Add(task);
					this.ExecNodeBizLogic(instance, "OnTaskArrive", task, this.CurSession.AppData, tran);
					this.ExecNodeBizLogic(instance, "OnTaskSubmit", task, this.CurSession.AppData, tran);
					tasks1 = this.NextTask(instance, task, tran);
					tasks = tasks1;
				}
			}
			else
			{
				tasks = tasks1;
			}
			return tasks;
		Label1:
			task.TaskState = 2.ToString();
			task.IsManualTask = "0";
			__Task.Add(task);
			tasks1.Add(task);
			userTask = new UserTask()
			{
				TaskId = task.TaskId,
				UserTaskId = Guid.NewGuid().ToString(),
				_UserName = this.UserInfo.EmployeeId,
				_OrgCode = this.UserInfo.DeptWbs,
				_CreateTime = DateTime.Now,
				_UpdateTime = DateTime.Now,
				_IsDel = 0,
				InstanceId = instance.InstanceId,
				OwnerId = activityUser[0].EmployeeID,
				DealUser = activityUser[0].EmployeeID,
                //EmployeeName = EmployeeService.GetEmployeeName(userTask.OwnerId),
                EmployeeName = EmployeeService.GetEmployeeName(activityUser[0].EmployeeID),
				AgentId = "",
				IsShare = "0"
			};
			if (!flag)
			{
				userTask.DealAdvice = "意见同上";
				userTask.DealAction = "";
			}
			else
			{
				userTask.DealAdvice = "〔系统自动〕";
				if (!actObj.IsDecisionNode())
				{
					userTask.DealAction = "提交";
				}
				else
				{
					userTask.DealAction = "同意";
				}
			}
			userTask.DealTime = new DateTime?(DateTime.Now);
			userTask.TaskState = 2.ToString();
			userTask.IsRead = "1";
			userTask.ReadTime = new DateTime?(DateTime.Now);
			userTask.PositionId = activityUser[0].PositionId;
			userTask.PositionName = activityUser[0].PositionName;
			userTask.DeptId = activityUser[0].DeptID;
			userTask.DeptName = activityUser[0].DeptName;
			userTask.RecIds = this.UserInfo.EmployeeId;
			userTask.RecNames = this.UserInfo.EmployeeName;
			__UserTask.Add(userTask);
			this.ExecEventScript(instance, actObj, "OnTaskSubmit", tran);
			EIS.WorkFlow.Engine.Utility.SendAlert(instance, actObj, task, this.CurSession, 2, tran);
			this.ExecNodeBizLogic(instance, "OnTaskArrive", task, this.CurSession.AppData, tran);
			this.ExecNodeBizLogic(instance, "OnTaskSubmit", task, this.CurSession.AppData, tran);
			tasks1 = this.NextTask(instance, task, tran);
			tasks = tasks1;
			return tasks;
		}

		public int NewWorkFlowInstance(Instance instance, DbTransaction tran)
		{
			_Instance __Instance = new _Instance(tran);
			Define workflowDefineModelById = DefineService.GetWorkflowDefineModelById(instance.WorkflowId);
			if (string.IsNullOrEmpty(instance.XPDL))
			{
				instance.XPDL = workflowDefineModelById.XPDL;
			}
			instance.ProcessId = "1";
			instance.InstanceState = EnumDescription.GetFieldText(InstanceState.Processing);
			if (!AppSettings.Instance.XPDLInFolder)
			{
				instance.XPDLPath = "";
			}
			else
			{
				instance.XPDLPath = EIS.WorkFlow.Engine.Utility.GenXPDLPath(instance);
				instance.BasePath = AppSettings.Instance.AppFileBaseCode;
			}
			return __Instance.Add(instance);
		}

		public List<Activity> NextActivity(Instance ins, Task oldTask)
		{
			Task taskById;
			Activity activityById;
			Transition transition;
			int i;
			string id;
			Transition transitionById;
			Activity activity;
			Transition splitTransition = null;
			string lower;
			Transition transitionById1;
			List<Activity> activities;
			List<Activity> activities1 = new List<Activity>();
			XpdlModel.GetPackageFromText(ins.XPDL);
			Activity activityById1 = this.GetActivityById(ins, oldTask.ActivityId);
			if (this.CurSession == null)
			{
				this.CurSession = new WFSession(this.UserInfo, ins, this.GetAppData(ins), activityById1);
			}
			IList splitTransitionRefs = this.GetSplitTransitionRefs(activityById1, ins);
			bool canReturn = activityById1.GetCanReturn();
			string fromTaskId = oldTask.FromTaskId;
			string str = activityById1.GetSplitType().ToLower();
			switch (activityById1.GetNodeType())
			{
				case ActivityType.Start:
				case ActivityType.Normal:
				case ActivityType.Mail:
				case ActivityType.Dll:
				case ActivityType.Auto:
				{
					if (str == "xor")
					{
						if (!canReturn)
						{
							i = 0;
							while (i < splitTransitionRefs.Count)
							{
								id = ((TransitionRef)splitTransitionRefs[i]).GetId();
								transitionById = this.GetTransitionById(ins, id);
								if (!this.CheckTransitionConditionNoState(ins, transitionById))
								{
									i++;
								}
								else
								{
									activity = this.GetActivityById(ins, transitionById.GetTo());
									if (!EIS.WorkFlow.Engine.Utility.IsManualTask(activity))
									{
										foreach (Transition splitTransition1 in this.GetSplitTransitions(activity, ins))
										{
											if (this.CheckTransitionConditionNoState(ins, splitTransition1))
											{
												activities1.AddRange(this.GetNextRealActivities(ins, splitTransition1, transitionById.GetId()));
											}
										}
									}
									else if (this.CheckActivityState(activity, ins))
									{
										activity.SelectPath = transitionById.GetId();
										activities1.Add(activity);
									}
									activities = activities1;
									return activities;
								}
							}
							if (activities1.Count == 0)
							{
								i = 0;
								while (i < splitTransitionRefs.Count)
								{
									id = ((TransitionRef)splitTransitionRefs[i]).GetId();
									transitionById = this.GetTransitionById(ins, id);
									lower = transitionById.GetCondition().GetConditionTypeString().ToLower();
									if (!(lower == "otherwise"))
									{
										i++;
									}
									else
									{
										activity = this.GetActivityById(ins, transitionById.GetTo());
										if (!EIS.WorkFlow.Engine.Utility.IsManualTask(activity))
										{
											foreach (Transition transition1 in this.GetSplitTransitions(activity, ins))
											{
												if (this.CheckTransitionConditionNoState(ins, transition1))
												{
													activities1.AddRange(this.GetNextRealActivities(ins, transition1, transitionById.GetId()));
												}
											}
										}
										else if (this.CheckActivityState(activity, ins))
										{
											activity.SelectPath = transitionById.GetId();
											activities1.Add(activity);
										}
										break;
									}
								}
							}
						}
						else
						{
							taskById = TaskService.GetTaskById(oldTask.FromTaskId);
							activityById = this.GetActivityById(ins, taskById.ActivityId);
							activityById.SelectPath = "";
							transition = new Transition();
							activities1.Add(activityById);
							activities = activities1;
							break;
						}
					}
					else if (!canReturn)
					{
						try
						{
							for (i = 0; i < splitTransitionRefs.Count; i++)
							{
								id = ((TransitionRef)splitTransitionRefs[i]).GetId();
								transitionById = this.GetTransitionById(ins, id);
								if (this.CheckTransitionConditionNoState(ins, transitionById))
								{
									activity = this.GetActivityById(ins, transitionById.GetTo());
									if (!EIS.WorkFlow.Engine.Utility.IsManualTask(activity))
									{
										foreach (Transition splitTransitionA in this.GetSplitTransitions(activity, ins))
										{
                                            if (this.CheckTransitionConditionNoState(ins, splitTransitionA))
											{
                                                activities1.AddRange(this.GetNextRealActivities(ins, splitTransitionA, transitionById.GetId()));
											}
										}
									}
									else if (this.CheckActivityState(activity, ins))
									{
										activity.SelectPath = transitionById.GetId();
										activities1.Add(activity);
									}
								}
							}
							if (activities1.Count == 0)
							{
								i = 0;
								while (i < splitTransitionRefs.Count)
								{
									id = ((TransitionRef)splitTransitionRefs[i]).GetId();
									transitionById = this.GetTransitionById(ins, id);
									lower = transitionById.GetCondition().GetConditionTypeString().ToLower();
									if (!(lower == "otherwise"))
									{
										i++;
									}
									else
									{
										activity = this.GetActivityById(ins, transitionById.GetTo());
										if (!EIS.WorkFlow.Engine.Utility.IsManualTask(activity))
										{
											foreach (Transition splitTransition2 in this.GetSplitTransitions(activity, ins))
											{
												if (this.CheckTransitionConditionNoState(ins, splitTransition2))
												{
													activities1.AddRange(this.GetNextRealActivities(ins, splitTransition2, transitionById.GetId()));
												}
											}
										}
										else if (this.CheckActivityState(activity, ins))
										{
											activity.SelectPath = transitionById.GetId();
											activities1.Add(activity);
										}
										break;
									}
								}
							}
						}
						catch (Exception exception)
						{
							throw exception;
						}
					}
					else
					{
						taskById = TaskService.GetTaskById(oldTask.FromTaskId);
						activityById = this.GetActivityById(ins, taskById.ActivityId);
						activityById.SelectPath = "";
						transition = new Transition();
						activities1.Add(activityById);
						activities = activities1;
						break;
					}
					goto case ActivityType.Xor;
				}
				case ActivityType.End:
				case ActivityType.And:
				case ActivityType.Xor:
				{
				Label0:
					activities = activities1;
					break;
				}
				case ActivityType.Sign:
				{
					string[] strArrays = activityById1.GetSignStrategy().Split(new char[] { '|' });
					bool flag = strArrays[2].ToLower() == "true";
					string str1 = strArrays[0];
					decimal num = decimal.Parse(strArrays[1]);
					int finishedUserTaskCount = TaskService.GetFinishedUserTaskCount(ins.InstanceId, oldTask);
					List<UserTask> userTask = TaskService.GetUserTask(ins.InstanceId, oldTask);
					int count = userTask.Count;
					int count1 = userTask.FindAll((UserTask t) => t.DealAction == EnumDescription.GetFieldText(DealAction.Agree)).Count;
					int num1 = userTask.FindAll((UserTask t) => t.DealAction == AppSettings.Instance.SignRejectAction).Count;
					bool flag1 = false; //变量
					if (str1 == "1")
					{
						if (!flag)
						{
							if (count1 >= num)
							{
								i = 0;
								while (i < splitTransitionRefs.Count)
                                {
                                    #region
                                    id = ((TransitionRef)splitTransitionRefs[i]).GetId();
									transitionById1 = this.GetTransitionById(ins, id);
									lower = transitionById1.GetCondition().GetConditionTypeString().ToLower();
									if ((lower == "agree" ? false : !(lower == "")))
									{
										i++;
									}
									else
                                    {
                                        #region
                                            activity = this.GetActivityById(ins, transitionById1.GetTo());
										    if (!EIS.WorkFlow.Engine.Utility.IsManualTask(activity))
										    {
											    foreach (Transition transition2 in this.GetSplitTransitions(activity, ins))
											    {
												    if (this.CheckTransitionConditionNoState(ins, transition2))
												    {
													    activities1.AddRange(this.GetNextRealActivities(ins, transition2, transitionById1.GetId()));
												    }
											    }
										    }
										    else if (this.CheckActivityState(activity, ins))
										    {
											    activity.SelectPath = transitionById1.GetId();
											    activities1.Add(activity);
										    }
										    flag1 = true;
										    break;
                                        #endregion
                                    }
                                    #endregion
                                }
							}
							else if (!(num1 > (count - num)))
							{
								activities = activities1;
								break;
							}
							else
							{
								i = 0;
								while (i < splitTransitionRefs.Count)
								{
									id = ((TransitionRef)splitTransitionRefs[i]).GetId();
									transitionById1 = this.GetTransitionById(ins, id);
									if (!(transitionById1.GetCondition().GetConditionTypeString().ToLower() == "reject"))
									{
										i++;
									}
									else
									{
										activity = this.GetActivityById(ins, transitionById1.GetTo());
										if (!EIS.WorkFlow.Engine.Utility.IsManualTask(activity))
										{
											foreach (Transition splitTransition3 in this.GetSplitTransitions(activity, ins))
											{
												if (this.CheckTransitionConditionNoState(ins, splitTransition3))
												{
													activities1.AddRange(this.GetNextRealActivities(ins, splitTransition3, transitionById1.GetId()));
												}
											}
										}
										else if (this.CheckActivityState(activity, ins))
										{
											activity.SelectPath = transitionById1.GetId();
											activities1.Add(activity);
										}
										flag1 = true;
										break;
									}
								}
							}
							if (!flag1)
							{
							}
						}
						else if (finishedUserTaskCount >= count)
						{
							if (!(count1 >= num))
							{
								i = 0;
								while (i < splitTransitionRefs.Count)
								{
									id = ((TransitionRef)splitTransitionRefs[i]).GetId();
									transitionById1 = this.GetTransitionById(ins, id);
									if (!(transitionById1.GetCondition().GetConditionTypeString().ToLower() == "reject"))
									{
										i++;
									}
									else
									{
										activity = this.GetActivityById(ins, transitionById1.GetTo());
										if (!EIS.WorkFlow.Engine.Utility.IsManualTask(activity))
										{
											foreach (Transition transition3 in this.GetSplitTransitions(activity, ins))
											{
												if (this.CheckTransitionConditionNoState(ins, transition3))
												{
													activities1.AddRange(this.GetNextRealActivities(ins, transition3, transitionById1.GetId()));
												}
											}
										}
										else if (this.CheckActivityState(activity, ins))
										{
											activity.SelectPath = transitionById1.GetId();
											activities1.Add(activity);
										}
										flag1 = true;
										break;
									}
								}
							}
							else
							{
								i = 0;
								while (i < splitTransitionRefs.Count)
								{
									id = ((TransitionRef)splitTransitionRefs[i]).GetId();
									transitionById1 = this.GetTransitionById(ins, id);
									lower = transitionById1.GetCondition().GetConditionTypeString().ToLower();
									if ((lower == "agree" ? false : !(lower == "")))
									{
										i++;
									}
									else
									{
										activity = this.GetActivityById(ins, transitionById1.GetTo());
										if (!EIS.WorkFlow.Engine.Utility.IsManualTask(activity))
										{
											foreach (Transition splitTransition4 in this.GetSplitTransitions(activity, ins))
											{
												if (this.CheckTransitionConditionNoState(ins, splitTransition4))
												{
													activities1.AddRange(this.GetNextRealActivities(ins, splitTransition4, transitionById1.GetId()));
												}
											}
										}
										else if (this.CheckActivityState(activity, ins))
										{
											activity.SelectPath = transitionById1.GetId();
											activities1.Add(activity);
										}
										flag1 = true;
										break;
									}
								}
							}
							if (!flag1)
							{
							}
						}
						else
						{
							activities = activities1;
							break;
						}
					}
					else if (!flag)
					{
						if (count1 / count >= (num / new decimal(100)))
						{
							i = 0;
							while (i < splitTransitionRefs.Count)
							{
								id = ((TransitionRef)splitTransitionRefs[i]).GetId();
								transitionById1 = this.GetTransitionById(ins, id);
								if (!(transitionById1.GetCondition().GetConditionTypeString().ToLower() == "agree"))
								{
									i++;
								}
								else
								{
									activity = this.GetActivityById(ins, transitionById1.GetTo());
									if (!EIS.WorkFlow.Engine.Utility.IsManualTask(activity))
									{
										foreach (Transition transition4 in this.GetSplitTransitions(activity, ins))
										{
											if (this.CheckTransitionConditionNoState(ins, transition4))
											{
												activities1.AddRange(this.GetNextRealActivities(ins, transition4, transitionById1.GetId()));
											}
										}
									}
									else if (this.CheckActivityState(activity, ins))
									{
										activity.SelectPath = transitionById1.GetId();
										activities1.Add(activity);
									}
									flag1 = true;
									break;
								}
							}
						}
						else if (!(num1 / count > (new decimal(1) - (num / new decimal(100)))))
						{
							activities = activities1;
							break;
						}
						else
						{
							i = 0;
							while (i < splitTransitionRefs.Count)
							{
								id = ((TransitionRef)splitTransitionRefs[i]).GetId();
								transitionById1 = this.GetTransitionById(ins, id);
								if (!(transitionById1.GetCondition().GetConditionTypeString().ToLower() == "reject"))
								{
									i++;
								}
								else
								{
									activity = this.GetActivityById(ins, transitionById1.GetTo());
									if (!EIS.WorkFlow.Engine.Utility.IsManualTask(activity))
									{
										foreach (Transition splitTransition5 in this.GetSplitTransitions(activity, ins))
										{
											if (this.CheckTransitionConditionNoState(ins, splitTransition5))
											{
												activities1.AddRange(this.GetNextRealActivities(ins, splitTransition5, transitionById1.GetId()));
											}
										}
									}
									else if (this.CheckActivityState(activity, ins))
									{
										activity.SelectPath = transitionById1.GetId();
										activities1.Add(activity);
									}
									flag1 = true;
									break;
								}
							}
						}
						if (!flag1)
						{
						}
					}
					else if (finishedUserTaskCount >= count)
					{
						if (!(count1 / count >= (num / new decimal(100))))
						{
							i = 0;
							while (i < splitTransitionRefs.Count)
							{
								id = ((TransitionRef)splitTransitionRefs[i]).GetId();
								transitionById1 = this.GetTransitionById(ins, id);
								if (!(transitionById1.GetCondition().GetConditionTypeString().ToLower() == "reject"))
								{
									i++;
								}
								else
								{
									activity = this.GetActivityById(ins, transitionById1.GetTo());
									if (!EIS.WorkFlow.Engine.Utility.IsManualTask(activity))
									{
										foreach (Transition transition5 in this.GetSplitTransitions(activity, ins))
										{
											if (this.CheckTransitionConditionNoState(ins, transition5))
											{
												activities1.AddRange(this.GetNextRealActivities(ins, transition5, transitionById1.GetId()));
											}
										}
									}
									else if (this.CheckActivityState(activity, ins))
									{
										activity.SelectPath = transitionById1.GetId();
										activities1.Add(activity);
									}
									flag1 = true;
									break;
								}
							}
						}
						else
						{
							i = 0;
							while (i < splitTransitionRefs.Count)
							{
								id = ((TransitionRef)splitTransitionRefs[i]).GetId();
								transitionById1 = this.GetTransitionById(ins, id);
								if (!(transitionById1.GetCondition().GetConditionTypeString().ToLower() == "agree"))
								{
									i++;
								}
								else
								{
									activity = this.GetActivityById(ins, transitionById1.GetTo());
									if (!EIS.WorkFlow.Engine.Utility.IsManualTask(activity))
									{
										foreach (Transition splitTransition6 in this.GetSplitTransitions(activity, ins))
										{
											if (this.CheckTransitionConditionNoState(ins, splitTransition6))
											{
												activities1.AddRange(this.GetNextRealActivities(ins, splitTransition6, transitionById1.GetId()));
											}
										}
									}
									else if (this.CheckActivityState(activity, ins))
									{
										activity.SelectPath = transitionById1.GetId();
										activities1.Add(activity);
									}
									flag1 = true;
									break;
								}
							}
						}
						if (!flag1)
						{
						}
					}
					else
					{
						activities = activities1;
						break;
					}
					goto case ActivityType.Xor;
				}
				case ActivityType.SubWorkflow:
				{
					goto case ActivityType.Xor;
				}
				default:
				{
					goto case ActivityType.Xor;
				}
			}
			return activities;
		}

		

		public string ReplaceSession(string source)
		{
			foreach (Match match in (new Regex("\\[!(.*?)!\\]")).Matches(source))
			{
				string value = match.Groups[1].Value;
				try
				{
					if (HttpContext.Current.Session[value] != null)
					{
						source = source.Replace(match.Value, HttpContext.Current.Session[value].ToString());
					}
				}
				catch
				{
					source = source.Replace(match.Value, "");
				}
			}
			return source;
		}

		public string ReplaceWithDataRow(string sourceString, string appName, DataRow data)
		{
			Regex regex = new Regex("{(\\w*\\.)?(\\w+)(:(.*?))?}", RegexOptions.IgnoreCase);
			if (data != null)
			{
				foreach (Match match in regex.Matches(sourceString))
				{
					string value = "";
					value = match.Groups[2].Value;
					if (data.Table.Columns.Contains(value))
					{
						if (data[value] == DBNull.Value)
						{
							if ((typeof(decimal) == data[value].GetType() ? false : !(typeof(int) == data[value].GetType())))
							{
								sourceString = sourceString.Replace(match.Value, "");
							}
							else
							{
								sourceString = sourceString.Replace(match.Value, "0");
							}
						}
						else if (typeof(decimal) == data[value].GetType())
						{
							sourceString = sourceString.Replace(match.Value, data[value].ToString().Replace(",", ""));
						}
						else if (!(typeof(DateTime) == data[value].GetType()))
						{
							sourceString = sourceString.Replace(match.Value, data[value].ToString());
						}
						else
						{
							string str = "yyyy-MM-dd HH:mm";
							if (match.Groups[4].Value.Length > 0)
							{
								str = match.Groups[4].Value;
							}
							string value1 = match.Value;
							DateTime dateTime = Convert.ToDateTime(data[value]);
							sourceString = sourceString.Replace(value1, dateTime.ToString(str));
						}
					}
				}
			}
			sourceString = this.ReplaceSession(sourceString);
			return sourceString;
		}

		public string ReplaceWithInstance(string sourceString, Instance ins, Task task)
		{
			sourceString = sourceString.Replace("{Instance.InstanceName}", ins.InstanceName).Replace("{Instance.DeptId}", ins.DeptId).Replace("{Instance.DeptName}", ins.DeptName).Replace("{Instance.CompanyId}", ins.CompanyId).Replace("{Instance.CompanyName}", ins.CompanyName).Replace("{Instance.EmployeeId}", ins._UserName).Replace("{Instance.EmployeeName}", ins.EmployeeName);
			if (task != null)
			{
				sourceString = sourceString.Replace("{Task.TaskName}", task.TaskName);
			}
			return sourceString;
		}

		public void ResumeInstance(Instance instance, DbTransaction tran)
		{
			instance._UpdateTime = DateTime.Now;
			instance.InstanceState = EnumDescription.GetFieldText(InstanceState.Processing);
			(new _Instance(tran)).Update(instance);
			(new _UserTask(tran)).ResumeUserTaskByInstanceId(instance.InstanceId);
			this.UpdateAppDataState(instance, InstanceState.Processing, tran);
		}

		public List<Task> RollBackTask(string fromTaskId, string toId, DbTransaction trans)
		{
			Activity activity = null;
			List<Task> tasks = new List<Task>();
			Task taskById = this.GetTaskById(fromTaskId, trans);
			Instance instanceById = this.GetInstanceById(taskById.InstanceId);
			Activity activityById = this.GetActivityById(instanceById, taskById.ActivityId);
			Activity activityById1 = this.GetActivityById(instanceById, toId);
			if (this.CurSession == null)
			{
				this.CurSession = new WFSession(this.UserInfo, instanceById, this.GetAppData(instanceById), activityById);
			}
			List<Activity> allActivitySith = this.GetAllActivitySith(instanceById, activityById1);
			try
			{
				StringCollection stringCollections = new StringCollection();
				foreach (Activity activityA in allActivitySith)
				{
                    EIS.WorkFlow.Engine.Utility.DeleteUserTaskByActivityId(instanceById.InstanceId, activityA.GetId(), trans);
                    FinishTransitionService.ClearFinishSplitTransition(instanceById.InstanceId, activityA.GetId(), trans);
                    foreach (TransitionRef splitTransitionRef in this.GetSplitTransitionRefs(activityA, instanceById))
					{
						if (!stringCollections.Contains(splitTransitionRef.GetId()))
						{
							stringCollections.Add(splitTransitionRef.GetId());
						}
					}
				}
				foreach (Activity activity1 in allActivitySith)
				{
				}
				for (int i = 0; i < stringCollections.Count; i++)
				{
					stringCollections[i] = string.Concat(stringCollections[i], "|1");
				}
				EIS.WorkFlow.Engine.Utility.UpdateTransitionState(instanceById, stringCollections, true, trans);
				EIS.WorkFlow.Engine.Utility.SendAlert(instanceById, activityById, taskById, this.CurSession, 3, trans);
				tasks = this.NewTask(instanceById, activityById1, taskById, TaskType.RollBack, trans);
				this.ExecEventScript(instanceById, activityById, "OnTaskRollBack", trans);
				this.ExecNodeBizLogic(instanceById, "OnTaskRollBack", taskById, this.CurSession.AppData, trans);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return tasks;
		}

		private void SaveAndJoinTransition(Activity act, string targetId, DbTransaction dbTran)
		{
			if (act.GetJoinType().ToLower() == "and")
			{
				foreach (Transition joinTransition in this.GetJoinTransitions(act, this.CurSession.CurInstance))
				{
					joinTransition.GetId();
					if (this.FindNodePrev(this.CurSession.CurInstance, joinTransition, targetId))
					{
						this.SaveCheckedTransition(this.CurSession.CurInstance, joinTransition, dbTran);
					}
				}
			}
		}

		private void SaveCheckedTransition(Instance instance, Transition tran, DbTransaction dbTran)
		{
			_FinishTransition __FinishTransition = new _FinishTransition(dbTran);
			if (!__FinishTransition.IsExist(instance.InstanceId, tran.GetId()))
			{
				FinishTransition finishTransition = new FinishTransition()
				{
					_AutoID = Guid.NewGuid().ToString(),
					_UserName = this.UserInfo.EmployeeId,
					_OrgCode = this.UserInfo.DeptWbs,
					_CreateTime = DateTime.Now,
					_UpdateTime = DateTime.Now,
					_IsDel = 0,
					InstanceId = instance.InstanceId,
					TransitionId = tran.GetId(),
					FromActivity = tran.GetFrom(),
					ToActivity = tran.GetTo()
				};
				__FinishTransition.Add(finishTransition);
			}
		}

		private Activity SearchForward(Activity act, Instance ins)
		{
			int num = 1;
			IList joinTransitionRefs = this.GetJoinTransitionRefs(act, ins);
			string id = ((TransitionRef)joinTransitionRefs[0]).GetId();
			Transition transitionById = this.GetTransitionById(ins, id);
			Activity activityById = null;
			while (num != 0)
			{
				activityById = this.GetActivityById(ins, transitionById.GetFrom());
				if (this.GetAndActivityType(activityById, ins) == 2)
				{
					num++;
				}
				else if (this.GetAndActivityType(activityById, ins) == 1)
				{
					num--;
				}
				if (num != 0)
				{
					joinTransitionRefs = this.GetJoinTransitionRefs(activityById, ins);
					id = ((TransitionRef)joinTransitionRefs[0]).GetId();
					transitionById = this.GetTransitionById(ins, id);
				}
				else
				{
					break;
				}
			}
			return activityById;
		}

		private void SendMail(Instance ins, Activity actObj, Task task, DataRow bizData, DbTransaction dbTran)
		{
			ExtendedAttribute extendedAttributeByName = actObj.GetExtendedAttributes().GetExtendedAttributeByName("MailInfo");
			if (extendedAttributeByName != null)
			{
				try
				{
					string text = "";
					string str = "";
					string text1 = "";
					NonEmptyNode nodeByName = (NonEmptyNode)extendedAttributeByName.GetNodeByName("MailList");
					if (nodeByName != null)
					{
						text = nodeByName.GetText();
					}
					NonEmptyNode nonEmptyNode = (NonEmptyNode)extendedAttributeByName.GetNodeByName("MailTitle");
					if (nonEmptyNode != null)
					{
						str = nonEmptyNode.GetText();
					}
					NonEmptyNode nodeByName1 = (NonEmptyNode)extendedAttributeByName.GetNodeByName("MailBody");
					if (nodeByName1 != null)
					{
						text1 = nodeByName1.GetText();
					}
					str = this.ReplaceWithDataRow(str, ins.AppName, this.CurSession.AppData);
					str = this.ReplaceWithInstance(str, ins, task);
					text1 = this.ReplaceWithDataRow(text1, ins.AppName, this.CurSession.AppData);
					text1 = this.ReplaceWithInstance(text1, ins, task);
					MailMessage mailMessage = new MailMessage()
					{
						Subject = str,
						IsBodyHtml = true,
						Priority = MailPriority.Normal,
						Body = EIS.WorkFlow.Engine.Utility.FormatMailHtml(text1)
					};
					string[] strArrays = text.Split(";".ToCharArray());
					for (int i = 0; i < (int)strArrays.Length; i++)
					{
						string str1 = strArrays[i];
						mailMessage.To.Add(str1);
					}
					EIS.WorkFlow.Engine.Utility.SendMail(mailMessage);
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					this.fileLogger.Error<Exception>("执行外发邮件时发生错误：{0}", exception);
					throw new Exception(string.Concat("执行外发邮件时发生错误：", exception.Message));
				}
			}
		}

		public List<Activity> StartNextActivity(Instance ins)
		{
			int i;
			string id;
			Transition transitionById;
			Activity activityById;
			List<Activity> activities;
			XpdlModel.GetPackageFromText(ins.XPDL);
			Activity startActivity = this.GetStartActivity(ins);
			if (this.CurSession == null)
			{
				this.CurSession = new WFSession(this.UserInfo, ins, this.GetAppData(ins), startActivity);
			}
			List<Activity> activities1 = new List<Activity>();
			IList splitTransitionRefs = this.GetSplitTransitionRefs(startActivity, ins);
			if (!(startActivity.GetSplitType().ToLower() == "xor"))
			{
				try
				{
					for (i = 0; i < splitTransitionRefs.Count; i++)
					{
						id = ((TransitionRef)splitTransitionRefs[i]).GetId();
						transitionById = this.GetTransitionById(ins, id);
						if (this.CheckTransitionCondition(ins, transitionById))
						{
							activityById = this.GetActivityById(ins, transitionById.GetTo());
							if (!EIS.WorkFlow.Engine.Utility.IsManualTask(activityById))
							{
								foreach (Transition splitTransition in this.GetSplitTransitions(activityById, ins))
								{
									activities1.AddRange(this.GetNextRealActivities(ins, splitTransition, transitionById.GetId()));
								}
							}
							else if (this.CheckActivityState(activityById, ins))
							{
								activityById.SelectPath = transitionById.GetId();
								activities1.Add(activityById);
							}
						}
					}
				}
				catch (Exception exception)
				{
					throw exception;
				}
			}
			else
			{
				i = 0;
				while (i < splitTransitionRefs.Count)
				{
					id = ((TransitionRef)splitTransitionRefs[i]).GetId();
					transitionById = this.GetTransitionById(ins, id);
					if (!this.CheckTransitionCondition(ins, transitionById))
					{
						i++;
					}
					else
					{
						activityById = this.GetActivityById(ins, transitionById.GetTo());
						if (this.CheckActivityState(activityById, ins))
						{
							activityById.SelectPath = transitionById.GetId();
							activities1.Add(activityById);
						}
						activities = activities1;
						return activities;
					}
				}
			}
			activities = activities1;
			return activities;
		}

		public Task StartWorkFlowInstance(Instance instance, string advice, bool startTaskFinished, bool updateAppDataState, DbTransaction tran)
		{
			Task task = new Task();
			Activity startActivity = this.GetStartActivity(instance);
			_Task __Task = new _Task(tran);
			task.TaskId = Guid.NewGuid().ToString();
			task._UserName = instance._UserName;
			task._OrgCode = instance._OrgCode;
			task._CreateTime = DateTime.Now;
			task._UpdateTime = DateTime.Now;
			task._IsDel = 0;
			task.InstanceId = instance.InstanceId;
			task.TaskName = startActivity.GetName();
			task.ActivityId = startActivity.GetId();
			task.DefineType = ActivityType.Start.ToString();
			task.TaskType = TaskType.Normal.ToString();
			task.ArriveTime = DateTime.Now;
			task.FromTaskId = "";
			task.IsManualTask = "1";
			task.CanFetch = startActivity.GetSafeOption("CanFetch");
			task.CanRollBack = startActivity.GetSafeOption("CanRollBack");
			task.CanDelegateTo = startActivity.GetSafeOption("CanDelegateTo");
			task.CanAssign = startActivity.GetSafeOption("CanAssign");
			task.CanPublic = startActivity.GetSafeOption("CanPublic");
			task.CanJump = startActivity.GetSafeOption("CanJump");
			task.CanHangUp = startActivity.GetSafeOption("CanHangUp");
			task.CanStop = startActivity.GetSafeOption("CanStop");
			task.CanReturn = startActivity.GetSafeOption("CanReturn");
			task.CanSelPath = startActivity.GetSafeOption("CanSelPath");
			task.CanBatch = startActivity.GetSafeOption("CanBatch");
			if (!startTaskFinished)
			{
				task.TaskState = 0.ToString();
			}
			else
			{
				task.TaskState = 2.ToString();
			}
			task.MainPerformer = instance._UserName;
			task.DealStrategy = "2";
			__Task.Add(task);
			_UserTask __UserTask = new _UserTask(tran);
			UserTask userTask = new UserTask()
			{
				TaskId = task.TaskId,
				UserTaskId = Guid.NewGuid().ToString(),
				_UserName = instance._UserName,
				_OrgCode = instance._OrgCode,
				_CreateTime = DateTime.Now,
				_UpdateTime = DateTime.Now,
				_IsDel = 0,
				InstanceId = instance.InstanceId,
				OwnerId = instance._UserName,
				AgentId = "",
				DealUser = this.UserInfo.EmployeeId,
				IsShare = "0",
				DealAdvice = advice
			};
			if (!startTaskFinished)
			{
				userTask.DealAction = "";
				DateTime? nullable = null;
				userTask.DealTime = nullable;
				userTask.TaskState = 1.ToString();
				userTask.IsRead = "0";
				nullable = null;
				userTask.ReadTime = nullable;
			}
			else
			{
				userTask.DealAction = EnumDescription.GetFieldText(DealAction.Submit);
				userTask.DealTime = new DateTime?(DateTime.Now);
				userTask.TaskState = 2.ToString();
				userTask.IsRead = "1";
				userTask.ReadTime = new DateTime?(DateTime.Now);
			}
			userTask.EmployeeName = this.UserInfo.EmployeeName;
			userTask.PositionId = this.UserInfo.PositionId;
			userTask.PositionName = this.UserInfo.PositionName;
			userTask.DeptId = this.UserInfo.DeptId;
			userTask.DeptName = this.UserInfo.DeptName;
			userTask.IsAssign = "0";
			__UserTask.Add(userTask);
			if (updateAppDataState)
			{
                this.UpdateAppDataState(instance, InstanceState.Processing, tran);
			}
			return task;
		}

		public Task StartWorkFlowInstance(Instance instance, string advice, bool startTaskFinished, DbTransaction tran)
		{
			return this.StartWorkFlowInstance(instance, advice, startTaskFinished, true, tran);
		}

		public Task StartWorkFlowInstance(Instance instance, string advice, DbTransaction tran)
		{
			return this.StartWorkFlowInstance(instance, advice, true, tran);
		}

		public Task StartWorkFlowInstance(string wfCode, string appId, bool startTaskFinished, DbTransaction tran)
		{
			DateTime today;
			Define workflowByCode = DefineService.GetWorkflowByCode(wfCode);
			string appNames = workflowByCode.AppNames;
			string workflowId = workflowByCode.WorkflowId;
			this.fileLogger.Debug<string, string>("StartWorkFlowInstance：appName={0},appId={1}", appNames, appId);
			if ((string.IsNullOrEmpty(appId) ? false : InstanceService.IsRunAlready(appNames, appId, tran)))
			{
				throw new Exception("每条业务数据只能发起一支流程");
			}
			Instance instance = new Instance();
			try
			{
				instance.InstanceId = Guid.NewGuid().ToString();
				instance.AppId = appId;
				instance.AppName = appNames;
				instance.WorkflowId = workflowId;
				instance.XPDL = workflowByCode.XPDL;
				instance.ProcessId = "1";
				instance._UserName = "";
				string str = string.Format("select * from {0} where _AutoId='{1}'", instance.AppName, instance.AppId);
				DataTable dataTable = this.ExecuteTable(str, "", tran);
				if (dataTable.Rows.Count == 0)
				{
					throw new Exception(string.Concat("找不到业务数据：", str));
				}
				DataRow item = dataTable.Rows[0];
				Activity startActivity = this.GetStartActivity(instance);
				this.CurSession = new WFSession(this.UserInfo, instance, item, startActivity);
				StringCollection stringCollections = new StringCollection();
				List<DeptEmployee> activityUser = EIS.WorkFlow.Engine.Utility.GetActivityUser(instance, startActivity, this.CurSession, tran, ref stringCollections);
				if (activityUser.Count == 0)
				{
					this.fileLogger.Debug("StartWorkFlowInstance：wfCode={0},开始节点没有处理人", wfCode);
					throw new NoUserException(startActivity, stringCollections, this.CurSession.AppData);
				}
				Logger logger = this.fileLogger;
				object[] objArray = new object[] { wfCode, activityUser[0].EmployeeID, activityUser[0].PositionId, activityUser.Count };
				logger.Debug("StartWorkFlowInstance：wfCode={0},处理人信息({3}) EmployeeID={1},PositionId={2}", objArray);
				UserContext userContext = new UserContext();
				_Employee __Employee = new _Employee();
				Employee model = __Employee.GetModel(activityUser[0].EmployeeID);
				DeptEmployee deptEmployeeByPositionId = DeptEmployeeService.GetDeptEmployeeByPositionId(activityUser[0].EmployeeID, activityUser[0].PositionId);
				Department department = DepartmentService.GetModel(deptEmployeeByPositionId.DeptID);
				userContext.LoginName = model.LoginName;
				userContext.EmployeeId = model._AutoID;
				userContext.EmployeeName = model.EmployeeName;
				Department companyByDeptId = DepartmentService.GetCompanyByDeptId(department._AutoID);
				userContext.CompanyId = companyByDeptId._AutoID;
				userContext.CompanyCode = companyByDeptId.DeptCode;
				userContext.CompanyWbs = companyByDeptId.DeptWBS;
				userContext.CompanyName = companyByDeptId.DeptName;
				userContext.DeptId = department._AutoID;
				userContext.DeptWbs = department.DeptWBS;
				userContext.DeptName = department.DeptName;
				userContext.PositionId = deptEmployeeByPositionId.PositionId;
				userContext.PositionName = deptEmployeeByPositionId.PositionName;
				userContext.WebId = AppSettings.Instance.WebId;
				this.UserInfo = userContext;
				this.CurSession.UserInfo = userContext;
				string str1 = "";
				string str2 = string.Format("select * from T_E_WF_Config where Enable='是' and WFId='{0}'", workflowByCode.WorkflowCode);
				DataTable dataTable1 = SysDatabase.ExecuteTable(str2);
				if (dataTable1.Rows.Count <= 0)
				{
					string workflowName = workflowByCode.WorkflowName;
					today = DateTime.Today;
					str1 = string.Concat(workflowName, today.ToShortDateString());
				}
				else
				{
					str1 = dataTable1.Rows[0]["InstanceName"].ToString();
					str1 = this.ReplaceWithDataRow(str1, appNames, this.CurSession.AppData);
					foreach (Match match in (new Regex("\\[([ymdhs\\-\\u5E74\\u6708\\u65E5:/]+)]", RegexOptions.IgnoreCase)).Matches(str1))
					{
						string value = match.Value;
						today = DateTime.Now;
						str1 = str1.Replace(value, today.ToString(match.Groups[1].Value));
					}
				}
				instance._UserName = model._AutoID;
				instance._OrgCode = userContext.DeptWbs;
				instance._CreateTime = DateTime.Now;
				instance._UpdateTime = DateTime.Now;
				instance._IsDel = 0;
				instance.InstanceName = str1;
				instance.DeptId = userContext.DeptId;
				instance.DeptName = userContext.DeptName;
				instance.CompanyId = userContext.CompanyId;
				instance.CompanyName = userContext.CompanyName;
				instance.EmployeeName = model.EmployeeName;
				instance.Importance = "1";
				instance.Remark = "";
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				this.fileLogger.Error("流程发起人报错：{0}", exception.Message);
				this.fileLogger.Error<Exception>(exception);
				throw exception;
			}
			Task task = new Task();
			try
			{
				this.fileLogger.Debug<string, string>("StartWorkFlowInstance 开始发起，wfCode={0}，InstanceId={1}", wfCode, instance.InstanceId);
				this.NewWorkFlowInstance(instance, tran);
				task = this.StartWorkFlowInstance(instance, "", startTaskFinished, false, tran);
				this.UpdateAppDataState(instance, InstanceState.Processing, tran);
				this.fileLogger.Debug<string, string>("StartWorkFlowInstance 开始结束，wfCode={0}，InstanceId={1}", wfCode, instance.InstanceId);
			}
			catch (Exception exception3)
			{
				Exception exception2 = exception3;
				this.fileLogger.Error("StartWorkFlowInstance 流程发起报错：{0}", exception2.Message);
				this.fileLogger.Error<Exception>(exception2);
				throw exception2;
			}
			return task;
		}

		public void StopInstance(Instance instance, DbTransaction tran)
		{
			if (this.CurSession == null)
			{
				this.CurSession = new WFSession(this.UserInfo, instance, this.GetAppData(instance, tran), null);
			}
			this.ExecEventScript(instance, "stopbefore", tran);
			this.ExecProgram(instance, "stopbefore", tran);
			instance._UpdateTime = DateTime.Now;
			instance.FinishTime = new DateTime?(DateTime.Now);
			instance.InstanceState = EnumDescription.GetFieldText(InstanceState.Stoped);
			(new _Instance(tran)).Update(instance);
			(new _UserTask(tran)).HangUpUserTaskByInstanceId(instance.InstanceId);
			this.UpdateAppDataState(instance, InstanceState.Stoped, tran);
			this.ExecEventScript(instance, "stopafter", tran);
			this.ExecProgram(instance, "stopafter", tran);
			EIS.WorkFlow.Engine.Utility.SendAlert(instance, this.CurSession, 2, tran);
		}

		public void StopInstance2(Instance instance, DbTransaction tran)
		{
			instance._UpdateTime = DateTime.Now;
			instance.FinishTime = new DateTime?(DateTime.Now);
			instance.InstanceState = EnumDescription.GetFieldText(InstanceState.Stoped);
			(new _Instance(tran)).Update(instance);
			(new _UserTask(tran)).StopUserTaskByInstanceId(instance.InstanceId);
			this.UpdateAppDataState(instance, InstanceState.Stoped, tran);
		}

		public void UpdateAppDataState(Instance ins, InstanceState state, DbTransaction dbTran)
		{
            if ((new EIS.DataModel.Access._TableInfo(ins.AppName)).GetModel().TableType == 1)
			{
				string fieldText = EnumDescription.GetFieldText(state);
				string str = string.Format("update {0} set _wfstate='{2}' where _AutoId='{1}'", ins.AppName, ins.AppId, fieldText);
				if (dbTran != null)
				{
					SysDatabase.ExecuteNonQuery(str, dbTran);
				}
				else
				{
					SysDatabase.ExecuteNonQuery(str);
				}
			}
		}
        public List<Task> NextTask(Instance instance_0, Task oldTask, DbTransaction dbTran)
        {
            Task taskById;
            Activity activityById;
            List<Task> tasks;
            StringCollection stringCollections;
            int i;
            string id;
            Transition transitionById;
            Activity item;
            IEnumerator enumerator;
            Transition current;
            IDisposable disposable;
            string lower;
            string dealStrategy;
            string[] strArrays;
            Transition transition;
            string rollBackScope;
            string[] strArrays1;
            StringCollection stringCollections1;
            string preActivityId;
            bool flag;
            bool flag1;
            List<Task> tasks1 = new List<Task>();
            XpdlModel.GetPackageFromText(instance_0.XPDL);
            Activity activity = this.GetActivityById(instance_0, oldTask.ActivityId);
            if (this.CurSession == null)
            {
                this.CurSession = new WFSession(this.UserInfo, instance_0, this.GetAppData(instance_0, dbTran), activity);
            }
            this.fileLogger.Trace<string, string>("进入NextTask：InstanceId={0},InstanceName={1}", instance_0.InstanceId, instance_0.InstanceName);
            IList splitTransitionRefs = this.GetSplitTransitionRefs(activity, instance_0);
            ActivityType nodeType = activity.GetNodeType();
            bool canReturn = oldTask.CanReturn == "1";
            bool safeOption = activity.GetSafeOption("CanSelPath") == "1";
            string fromTaskId = oldTask.FromTaskId;
            bool flag2 = false;
            string str = activity.GetSplitType().ToLower();
            _Task __Task = new _Task(dbTran);
            switch (nodeType)
            {
                case ActivityType.Start:
                case ActivityType.Normal:
                case ActivityType.Mail:
                case ActivityType.Dll:
                case ActivityType.Auto:
                    {
                        if (str != "xor")
                        {
                            oldTask.TaskState = 2.ToString();
                            oldTask._UpdateTime = DateTime.Now;
                            __Task.Update(oldTask);
                            if (!canReturn)
                            {
                                try
                                {
                                    this.ExecEventScript(instance_0, activity, "OnTaskSubmit", dbTran);
                                    Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                    stringCollections = new StringCollection();
                                    for (i = 0; i < splitTransitionRefs.Count; i++)
                                    {
                                        id = ((TransitionRef)splitTransitionRefs[i]).GetId();
                                        transitionById = this.GetTransitionById(instance_0, id);
                                        this.fileLogger.Debug<string, string, string>("开始验证条件，InstanceId={0}，TransId={1}，TransName={2}", instance_0.InstanceId, transitionById.GetId(), transitionById.GetName());
                                        if (this.CheckTransitionCondition(instance_0, transitionById, dbTran))
                                        {
                                            this.fileLogger.Debug<string, string, string>("条件验证通过，InstanceId={0}，TransId={1}，TransName={2}", instance_0.InstanceId, transitionById.GetId(), transitionById.GetName());
                                            this.SaveCheckedTransition(instance_0, transitionById, dbTran);
                                            flag2 = true;
                                            item = this.GetActivityById(instance_0, transitionById.GetTo());
                                            if (Utility.IsVirtualNode(item))
                                            {
                                                enumerator = this.GetSplitTransitions(item, instance_0).GetEnumerator();
                                                try
                                                {
                                                    while (true)
                                                    {
                                                        if (enumerator.MoveNext())
                                                        {
                                                            current = (Transition)enumerator.Current;
                                                            if (this.CheckTransitionCondition(instance_0, current))
                                                            {
                                                                item = this.GetNextRealActivities(instance_0, current, transitionById.GetId())[0];
                                                                this.SaveAndJoinTransition(item, oldTask.ActivityId, dbTran);
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            break;
                                                        }
                                                    }
                                                }
                                                finally
                                                {
                                                    disposable = enumerator as IDisposable;
                                                    if (disposable != null)
                                                    {
                                                        disposable.Dispose();
                                                    }
                                                }
                                            }
                                            if (this.CheckActivityState(item, instance_0, dbTran))
                                            {
                                                tasks1.AddRange(this.NewTask(instance_0, item, oldTask, dbTran));
                                            }
                                        }
                                        else if (!safeOption)
                                        {
                                            stringCollections.Add(string.Concat(id, "|0"));
                                        }
                                    }
                                    if (stringCollections.Count > 0)
                                    {
                                        Utility.UpdateTransitionState(instance_0, stringCollections, false);
                                        instance_0.NeedUpdate = true;
                                    }
                                    if (!flag2)
                                    {
                                        i = 0;
                                        while (true)
                                        {
                                            if (i < splitTransitionRefs.Count)
                                            {
                                                id = ((TransitionRef)splitTransitionRefs[i]).GetId();
                                                transitionById = this.GetTransitionById(instance_0, id);
                                                lower = transitionById.GetCondition().GetConditionTypeString().ToLower();
                                                if (lower == "otherwise")
                                                {
                                                    flag2 = true;
                                                    this.SaveCheckedTransition(instance_0, transitionById, dbTran);
                                                    item = this.GetActivityById(instance_0, transitionById.GetTo());
                                                    if (Utility.IsVirtualNode(item))
                                                    {
                                                        enumerator = this.GetSplitTransitions(item, instance_0).GetEnumerator();
                                                        try
                                                        {
                                                            while (true)
                                                            {
                                                                if (enumerator.MoveNext())
                                                                {
                                                                    current = (Transition)enumerator.Current;
                                                                    if (this.CheckTransitionCondition(instance_0, current))
                                                                    {
                                                                        item = this.GetNextRealActivities(instance_0, current, transitionById.GetId())[0];
                                                                        this.SaveAndJoinTransition(item, oldTask.ActivityId, dbTran);
                                                                        break;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        finally
                                                        {
                                                            disposable = enumerator as IDisposable;
                                                            if (disposable != null)
                                                            {
                                                                disposable.Dispose();
                                                            }
                                                        }
                                                    }
                                                    tasks1.AddRange(this.NewTask(instance_0, item, oldTask, dbTran));
                                                    break;
                                                }
                                                else
                                                {
                                                    i++;
                                                }
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        if (!flag2)
                                        {
                                            throw new Exception("流转下一步时的找不到路径");
                                        }
                                    }
                                    goto case ActivityType.SubWorkflow;
                                }
                                catch (Exception exception1)
                                {
                                    Exception exception = exception1;
                                    this.fileLogger.Error<Exception>("流程下一步时发生错误", exception);
                                    throw exception;
                                }
                            }
                            else
                            {
                                this.ExecEventScript(instance_0, activity, "OnTaskSubmit", dbTran);
                                Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                taskById = TaskService.GetTaskById(oldTask.FromTaskId, dbTran);
                                activityById = this.GetActivityById(instance_0, taskById.ActivityId);
                                tasks1.AddRange(this.NewTask(instance_0, activityById, oldTask, dbTran));
                                tasks = tasks1;
                                break;
                            }
                        }
                        else
                        {
                            oldTask.TaskState = 2.ToString();
                            oldTask._UpdateTime = DateTime.Now;
                            __Task.Update(oldTask);
                            if (!canReturn)
                            {
                                stringCollections = new StringCollection();
                                i = 0;
                                while (i < splitTransitionRefs.Count)
                                {
                                    id = ((TransitionRef)splitTransitionRefs[i]).GetId();
                                    transitionById = this.GetTransitionById(instance_0, id);
                                    this.fileLogger.Debug<string, string, string>("开始验证条件，InstanceId={0}，TransId={1}，TransName={2}", instance_0.InstanceId, transitionById.GetId(), transitionById.GetName());
                                    if (this.CheckTransitionCondition(instance_0, transitionById, dbTran))
                                    {
                                        this.fileLogger.Debug<string, string, string>("条件验证通过，InstanceId={0}，TransId={1}，TransName={2}", instance_0.InstanceId, transitionById.GetId(), transitionById.GetName());
                                        this.SaveCheckedTransition(instance_0, transitionById, dbTran);
                                        flag2 = true;
                                        this.ExecEventScript(instance_0, activity, "OnTaskSubmit", dbTran);
                                        Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                        item = this.GetActivityById(instance_0, transitionById.GetTo());
                                        if (Utility.IsVirtualNode(item))
                                        {
                                            enumerator = this.GetSplitTransitions(item, instance_0).GetEnumerator();
                                            try
                                            {
                                                while (true)
                                                {
                                                    if (enumerator.MoveNext())
                                                    {
                                                        current = (Transition)enumerator.Current;
                                                        if (this.CheckTransitionCondition(instance_0, current))
                                                        {
                                                            item = this.GetNextRealActivities(instance_0, current, transitionById.GetId())[0];
                                                            this.SaveAndJoinTransition(item, oldTask.ActivityId, dbTran);
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                            finally
                                            {
                                                disposable = enumerator as IDisposable;
                                                if (disposable != null)
                                                {
                                                    disposable.Dispose();
                                                }
                                            }
                                        }
                                        if (this.CheckActivityState(item, instance_0, dbTran))
                                        {
                                            tasks1.AddRange(this.NewTask(instance_0, item, oldTask, dbTran));
                                        }
                                        tasks = tasks1;
                                        return tasks;
                                    }
                                    else
                                    {
                                        if (!safeOption)
                                        {
                                            stringCollections.Add(string.Concat(id, "|0"));
                                        }
                                        i++;
                                    }
                                }
                                if (stringCollections.Count > 0)
                                {
                                    Utility.UpdateTransitionState(instance_0, stringCollections, false);
                                    instance_0.NeedUpdate = true;
                                }
                                if (flag2)
                                {
                                    goto case ActivityType.SubWorkflow;
                                }
                                i = 0;
                                while (true)
                                {
                                    if (i < splitTransitionRefs.Count)
                                    {
                                        id = ((TransitionRef)splitTransitionRefs[i]).GetId();
                                        transitionById = this.GetTransitionById(instance_0, id);
                                        lower = transitionById.GetCondition().GetConditionTypeString().ToLower();
                                        if (lower == "otherwise")
                                        {
                                            this.SaveCheckedTransition(instance_0, transitionById, dbTran);
                                            flag2 = true;
                                            this.ExecEventScript(instance_0, activity, "OnTaskSubmit", dbTran);
                                            Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                            item = this.GetActivityById(instance_0, transitionById.GetTo());
                                            if (Utility.IsVirtualNode(item))
                                            {
                                                enumerator = this.GetSplitTransitions(item, instance_0).GetEnumerator();
                                                try
                                                {
                                                    while (true)
                                                    {
                                                        if (enumerator.MoveNext())
                                                        {
                                                            current = (Transition)enumerator.Current;
                                                            if (this.CheckTransitionCondition(instance_0, current))
                                                            {
                                                                item = this.GetNextRealActivities(instance_0, current, transitionById.GetId())[0];
                                                                this.SaveAndJoinTransition(item, oldTask.ActivityId, dbTran);
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            break;
                                                        }
                                                    }
                                                }
                                                finally
                                                {
                                                    disposable = enumerator as IDisposable;
                                                    if (disposable != null)
                                                    {
                                                        disposable.Dispose();
                                                    }
                                                }
                                            }
                                            if (!this.CheckActivityState(item, instance_0, dbTran))
                                            {
                                                break;
                                            }
                                            tasks1.AddRange(this.NewTask(instance_0, item, oldTask, dbTran));
                                            break;
                                        }
                                        else
                                        {
                                            i++;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (flag2)
                                {
                                    goto case ActivityType.SubWorkflow;
                                }
                                throw new Exception("流转下一步时的找不到路径");
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(oldTask.FromTaskId))
                                {
                                    throw new Exception("任务返回时发生错误，找不到上一步任务（FromTaskId）");
                                }
                                this.ExecEventScript(instance_0, activity, "OnTaskSubmit", dbTran);
                                Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                taskById = TaskService.GetTaskById(oldTask.FromTaskId, dbTran);
                                activityById = this.GetActivityById(instance_0, taskById.ActivityId);
                                tasks1.AddRange(this.NewTask(instance_0, activityById, oldTask, dbTran));
                                tasks = tasks1;
                                break;
                            }
                        }
                        break;
                    }
                case ActivityType.End:
                case ActivityType.And:
                case ActivityType.Xor:
                case ActivityType.Block:
                case ActivityType.SubWorkflow:
                    {
                        tasks = tasks1;
                        break;
                    }
                case ActivityType.Sign: //会签
                    {
                        #region
                        string signStrategy = activity.GetSignStrategy();
                        char[] chrArray = new char[] { '|' };
                        string[] strArrays2 = signStrategy.Split(chrArray);
                        bool lower1 = strArrays2[2].ToLower() == "true"; //
                        string str1 = strArrays2[0];  // 
                        float single = float.Parse(strArrays2[1]);
                        int finishedUserTaskCount = TaskService.GetFinishedUserTaskCount(instance_0.InstanceId, oldTask, dbTran);
                        List<UserTask> userTask = TaskService.GetUserTask(instance_0.InstanceId, oldTask, dbTran);
                        float count = (float)userTask.Count;  //任务人数总量
                        float count1 = (float)userTask.FindAll((UserTask userTask_0) => userTask_0.DealType == "2").Count;
                        float single1 = count - count1;
                        float count2 = (float)userTask.FindAll((UserTask userTask_0) => userTask_0.DealAction == EnumDescription.GetFieldText(DealAction.Agree)).Count; //同意数量
                        float single2 = (float)userTask.FindAll((UserTask userTask_0) => userTask_0.DealAction == EnumDescription.GetFieldText(DealAction.Disagree)).Count;
                        bool flag3 = false;
                        bool flag4 = false;
                        Logger logger0 = this.fileLogger;
                        object[] objArray = new object[] { lower1, count, count2, single2 };
                        logger0.Info("会签信息：chkAll={0},allCount={1},agreeCount={2},rejectCount={3}", objArray);
                        if (str1 != "1")
                        {
                            if (!lower1)
                            {
                                #region
                                this.fileLogger.Info("会签百分比NotchkAll");
                                if (count2 / single1 < single / 100f)
                                {
                                    oldTask.TaskState = 2.ToString(); 
                                    oldTask._UpdateTime = DateTime.Now;
                                    __Task.Update(oldTask);
                                    #region
                                    if (single2 / single1 <= 1f - single / 100f)
                                    {
                                        #region
                                        oldTask.TaskState = 1.ToString();
                                        oldTask._UpdateTime = DateTime.Now;
                                        __Task.Update(oldTask);
                                        tasks = tasks1;
                                        break;
                                        #endregion
                                    }
                                    else if (!canReturn)
                                    {
                                 
                                        flag4 = false;
                                        this.fileLogger.Info("会签百分比永远通不过");
                                        i = 0;
                                        while (i < splitTransitionRefs.Count)
                                        {
                                            #region goto Label2;
                                            id = ((TransitionRef)splitTransitionRefs[i]).GetId();
                                            transition = this.GetTransitionById(instance_0, id);
                                            if (transition.GetCondition().GetConditionTypeString().ToLower() == "reject")
                                            {
                                                this.SaveCheckedTransition(instance_0, transition, dbTran);
                                                item = this.GetActivityById(instance_0, transition.GetTo());
                                                #region
                                                if (Utility.IsVirtualNode(item))
                                                {
                                                    enumerator = this.GetSplitTransitions(item, instance_0).GetEnumerator();
                                                    try
                                                    {
                                                        #region
                                                        while (true)
                                                        {
                                                            if (enumerator.MoveNext())
                                                            {
                                                                current = (Transition)enumerator.Current;
                                                                if (this.CheckTransitionCondition(instance_0, current))
                                                                {
                                                                    item = this.GetNextRealActivities(instance_0, current, id)[0];
                                                                    this.SaveAndJoinTransition(item, oldTask.ActivityId, dbTran);
                                                                    break;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        #endregion
                                                    }
                                                    finally
                                                    {
                                                        disposable = enumerator as IDisposable;
                                                        if (disposable != null)
                                                        {
                                                            disposable.Dispose();
                                                        }
                                                    }
                                                }
                                                #endregion
                                                if (this.CheckActivityState(item, instance_0, dbTran))
                                                {
                                                    tasks1.AddRange(this.NewTask(instance_0, item, oldTask, dbTran));
                                                }
                                                flag3 = true;
                                                goto Label2;
                                            }
                                            else
                                            {
                                                i++;
                                            }
                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        #region
                                        this.ExecEventScript(instance_0, activity, "OnTaskRollBack", dbTran);
                                        Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                        taskById = TaskService.GetTaskById(oldTask.FromTaskId, dbTran);
                                        activityById = this.GetActivityById(instance_0, taskById.ActivityId);
                                        tasks1.AddRange(this.NewTask(instance_0, activityById, oldTask, dbTran));
                                        tasks = tasks1;
                                        break;
                                        #endregion
                                    }
                                    #endregion
                                }
                                else
                                {
                                    oldTask.TaskState = 2.ToString();
                                    oldTask._UpdateTime = DateTime.Now;
                                    __Task.Update(oldTask);
                                    #region
                                    if (!canReturn)
                                    {
                                        flag4 = true;
                                        i = 0;
                                        #region
                                        while (i < splitTransitionRefs.Count)
                                        {
                                            id = ((TransitionRef)splitTransitionRefs[i]).GetId();
                                            transition = this.GetTransitionById(instance_0, id);
                                            lower = transition.GetCondition().GetConditionTypeString().ToLower();
                                            if ((lower == "agree" ? true : lower == ""))
                                            {
                                                #region  goto Label2;
                                                this.ExecEventScript(instance_0, activity, "OnTaskSubmit", dbTran);
                                                Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                                this.SaveCheckedTransition(instance_0, transition, dbTran);
                                                item = this.GetActivityById(instance_0, transition.GetTo());
                                                if (Utility.IsVirtualNode(item))
                                                {
                                                    #region
                                                    enumerator = this.GetSplitTransitions(item, instance_0).GetEnumerator();
                                                    try
                                                    {
                                                        #region
                                                        while (true)
                                                        {
                                                            if (enumerator.MoveNext())
                                                            {
                                                                current = (Transition)enumerator.Current;
                                                                if (this.CheckTransitionCondition(instance_0, current))
                                                                {
                                                                    item = this.GetNextRealActivities(instance_0, current, id)[0];
                                                                    this.SaveAndJoinTransition(item, oldTask.ActivityId, dbTran);
                                                                    break;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        #endregion
                                                    }
                                                    finally
                                                    {
                                                        disposable = enumerator as IDisposable;
                                                        if (disposable != null)
                                                        {
                                                            disposable.Dispose();
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                if (this.CheckActivityState(item, instance_0, dbTran))
                                                {
                                                    tasks1.AddRange(this.NewTask(instance_0, item, oldTask, dbTran));
                                                }
                                                flag3 = true;
                                                goto Label2;
                                                #endregion
                                            }
                                            else
                                            {
                                                i++;
                                            }
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region
                                        this.ExecEventScript(instance_0, activity, "OnTaskSubmit", dbTran);
                                        Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                        taskById = TaskService.GetTaskById(oldTask.FromTaskId, dbTran);
                                        activityById = this.GetActivityById(instance_0, taskById.ActivityId);
                                        tasks1.AddRange(this.NewTask(instance_0, activityById, oldTask, dbTran));
                                        tasks = tasks1;
                                        break;
                                        #endregion
                                    }
                                    #endregion
                                }
                            Label2:
                                #region
                                if (flag3)
                                {
                                    goto case ActivityType.SubWorkflow;
                                }
                                if (flag4)
                                {
                                    throw new Exception("没有找到合适的分支");
                                }
                                this.fileLogger.Info("如果没有找到合适的条件,查找【退回】路径");
                                rollBackScope = activity.GetRollBackScope();
                                chrArray = new char[] { '|' };
                                strArrays1 = rollBackScope.Split(chrArray);
                                stringCollections1 = new StringCollection();
                                if (!((int)strArrays1.Length <= 1 ? true : rollBackScope.Length <= 2))
                                {
                                    this.ExecEventScript(instance_0, activity, "OnTaskRollBack", dbTran);
                                    Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                    string str2 = strArrays1[1];
                                    chrArray = new char[] { ',' };
                                    stringCollections1.AddRange(str2.Split(chrArray));
                                    this.fileLogger.Info("只有一个回退节点，直接退回");
                                    tasks1 = this.RollBackTask(oldTask.TaskId, stringCollections1[0], dbTran);
                                    goto case ActivityType.SubWorkflow;
                                }
                                else if (oldTask.TaskType != "Normal")
                                {
                                    preActivityId = this.GetPreActivityId(instance_0, activity);
                                    if (string.IsNullOrEmpty(preActivityId))
                                    {
                                        throw new Exception("没有找到合适的回退分支");
                                    }
                                    this.ExecEventScript(instance_0, activity, "OnTaskRollBack", dbTran);
                                    Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                    tasks1 = this.RollBackTask(oldTask.TaskId, preActivityId, dbTran);
                                    tasks = tasks1;
                                    break;
                                }
                                else
                                {
                                    this.ExecEventScript(instance_0, activity, "OnTaskRollBack", dbTran);
                                    Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                    taskById = TaskService.GetTaskById(oldTask.FromTaskId, dbTran);
                                    activityById = this.GetActivityById(instance_0, taskById.ActivityId);
                                    tasks1.AddRange(this.NewTask(instance_0, activityById, oldTask, dbTran));
                                    tasks = tasks1;
                                    break;
                                }
                                #endregion
                                #endregion
                            }
                            else
                            {
                                #region
                                this.fileLogger.Info("会签百分比chkAll");
                                if ((float)finishedUserTaskCount >= count)
                                {
                                    #region
                                    oldTask.TaskState = 2.ToString();
                                    oldTask._UpdateTime = DateTime.Now;
                                    __Task.Update(oldTask);
                                    if (!canReturn)
                                    {
                                        dealStrategy = activity.GetDealStrategy();
                                        chrArray = new char[] { '|' };
                                        strArrays = dealStrategy.Split(chrArray);
                                        if (count2 / single1 >= single / 100f)
                                        {
                                            flag = false;
                                        }
                                        else
                                        {
                                            flag = (strArrays[1] != "1" ? true : userTask.Count != 0);
                                        }
                                        if (flag)
                                        {
                                            flag4 = false;
                                            i = 0;
                                            #region goto Label3;
                                            while (i < splitTransitionRefs.Count)
                                            {
                                                id = ((TransitionRef)splitTransitionRefs[i]).GetId();
                                                transition = this.GetTransitionById(instance_0, id);
                                                if (transition.GetCondition().GetConditionTypeString().ToLower() == "reject")
                                                {
                                                    this.SaveCheckedTransition(instance_0, transition, dbTran);
                                                    item = this.GetActivityById(instance_0, transition.GetTo());
                                                    if (this.CheckActivityState(item, instance_0, dbTran))
                                                    {
                                                        tasks1.AddRange(this.NewTask(instance_0, item, oldTask, dbTran));
                                                    }
                                                    flag3 = true;
                                                    goto Label3;
                                                }
                                                else
                                                {
                                                    i++;
                                                }
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            #region
                                            flag4 = true;
                                            i = 0;
                                            while (i < splitTransitionRefs.Count)
                                            {
                                                id = ((TransitionRef)splitTransitionRefs[i]).GetId();
                                                transition = this.GetTransitionById(instance_0, id);
                                                lower = transition.GetCondition().GetConditionTypeString().ToLower();
                                                if ((lower == "agree" ? true : lower == ""))
                                                {
                                                    #region goto Label3;
                                                    this.ExecEventScript(instance_0, activity, "OnTaskSubmit", dbTran);
                                                    Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                                    this.SaveCheckedTransition(instance_0, transition, dbTran);
                                                    item = this.GetActivityById(instance_0, transition.GetTo());
                                                    if (Utility.IsVirtualNode(item))
                                                    {
                                                        #region
                                                        enumerator = this.GetSplitTransitions(item, instance_0).GetEnumerator();
                                                        try
                                                        {
                                                            #region
                                                            while (true)
                                                            {
                                                                if (enumerator.MoveNext())
                                                                {
                                                                    current = (Transition)enumerator.Current;
                                                                    if (this.CheckTransitionCondition(instance_0, current))
                                                                    {
                                                                        item = this.GetNextRealActivities(instance_0, current, id)[0];
                                                                        this.SaveAndJoinTransition(item, oldTask.ActivityId, dbTran);
                                                                        break;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                        }
                                                        finally
                                                        {
                                                            disposable = enumerator as IDisposable;
                                                            if (disposable != null)
                                                            {
                                                                disposable.Dispose();
                                                            }
                                                        }
                                                        #endregion
                                                    }
                                                    if (this.CheckActivityState(item, instance_0, dbTran))
                                                    {
                                                        tasks1.AddRange(this.NewTask(instance_0, item, oldTask, dbTran));
                                                    }
                                                    flag3 = true;
                                                    goto Label3;
                                                    #endregion
                                                }
                                                else
                                                {
                                                    i++;
                                                }
                                            }
                                            #endregion
                                        }
                                    Label3:
                                        #region
                                        if (flag3)
                                        {
                                            goto case ActivityType.SubWorkflow;
                                        }
                                        if (flag4)
                                        {
                                            throw new Exception("没有找到合适的分支");
                                        }
                                        rollBackScope = activity.GetRollBackScope();
                                        chrArray = new char[] { '|' };
                                        strArrays1 = rollBackScope.Split(chrArray);
                                        stringCollections1 = new StringCollection();
                                        if (!((int)strArrays1.Length <= 1 ? true : rollBackScope.Length <= 2))
                                        {
                                            this.ExecEventScript(instance_0, activity, "OnTaskRollBack", dbTran);
                                            Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                            string str3 = strArrays1[1];
                                            chrArray = new char[] { ',' };
                                            stringCollections1.AddRange(str3.Split(chrArray));
                                            tasks1 = this.RollBackTask(oldTask.TaskId, stringCollections1[0], dbTran);
                                            goto case ActivityType.SubWorkflow;
                                        }
                                        else if (oldTask.TaskType != "Normal")
                                        {
                                            preActivityId = this.GetPreActivityId(instance_0, activity);
                                            if (string.IsNullOrEmpty(preActivityId))
                                            {
                                                throw new Exception("没有找到合适的回退分支");
                                            }
                                            this.ExecEventScript(instance_0, activity, "OnTaskRollBack", dbTran);
                                            Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                            tasks1 = this.RollBackTask(oldTask.TaskId, preActivityId, dbTran);
                                            tasks = tasks1;
                                            break;
                                        }
                                        else
                                        {
                                            this.ExecEventScript(instance_0, activity, "OnTaskRollBack", dbTran);
                                            Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                            taskById = TaskService.GetTaskById(oldTask.FromTaskId, dbTran);
                                            activityById = this.GetActivityById(instance_0, taskById.ActivityId);
                                            tasks1.AddRange(this.NewTask(instance_0, activityById, oldTask, dbTran));
                                            tasks = tasks1;
                                            break;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        this.ExecEventScript(instance_0, activity, "OnTaskSubmit", dbTran);
                                        Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                        taskById = TaskService.GetTaskById(oldTask.FromTaskId, dbTran);
                                        activityById = this.GetActivityById(instance_0, taskById.ActivityId);
                                        tasks1.AddRange(this.NewTask(instance_0, activityById, oldTask, dbTran));
                                        tasks = tasks1;
                                        break;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    oldTask.TaskState = 1.ToString();
                                    oldTask._UpdateTime = DateTime.Now;
                                    __Task.Update(oldTask);
                                    tasks = tasks1;
                                    break;
                                }
                                #endregion

                            }
                        }
                        else if (!lower1)
                        {
                            #region
                            oldTask.TaskState = 2.ToString();
                            oldTask._UpdateTime = DateTime.Now;
                            __Task.Update(oldTask);
                            if (count2 >= single)
                            {
                                #region
                                if (!canReturn)
                                {
                                    flag4 = true;
                                    i = 0;
                                    #region
                                    while (i < splitTransitionRefs.Count)
                                    {
                                        id = ((TransitionRef)splitTransitionRefs[i]).GetId();
                                        transition = this.GetTransitionById(instance_0, id);
                                        lower = transition.GetCondition().GetConditionTypeString().ToLower();
                                        if ((lower == "agree" ? true : lower == ""))
                                        {
                                            this.ExecEventScript(instance_0, activity, "OnTaskSubmit", dbTran);
                                            Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                            this.SaveCheckedTransition(instance_0, transition, dbTran);
                                            item = this.GetActivityById(instance_0, transition.GetTo());
                                            if (Utility.IsVirtualNode(item))
                                            {
                                                #region
                                                enumerator = this.GetSplitTransitions(item, instance_0).GetEnumerator();
                                                try
                                                {
                                                    while (true)
                                                    {
                                                        if (enumerator.MoveNext())
                                                        {
                                                            current = (Transition)enumerator.Current;
                                                            if (this.CheckTransitionCondition(instance_0, current))
                                                            {
                                                                item = this.GetNextRealActivities(instance_0, current, id)[0];
                                                                this.SaveAndJoinTransition(item, oldTask.ActivityId, dbTran);
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            break;
                                                        }
                                                    }
                                                }
                                                finally
                                                {
                                                    disposable = enumerator as IDisposable;
                                                    if (disposable != null)
                                                    {
                                                        disposable.Dispose();
                                                    }
                                                }
                                                #endregion
                                            }
                                            if (this.CheckActivityState(item, instance_0, dbTran))
                                            {
                                                tasks1.AddRange(this.NewTask(instance_0, item, oldTask, dbTran));
                                            }
                                            flag3 = true;
                                            goto Label4;
                                        }
                                        else
                                        {
                                            i++;
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    this.ExecEventScript(instance_0, activity, "OnTaskSubmit", dbTran);
                                    Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                    taskById = TaskService.GetTaskById(oldTask.FromTaskId, dbTran);
                                    activityById = this.GetActivityById(instance_0, taskById.ActivityId);
                                    tasks1.AddRange(this.NewTask(instance_0, activityById, oldTask, dbTran));
                                    tasks = tasks1;
                                    break;
                                }
                                #endregion
                            }
                            else if (single2 <= single1 - single)
                            {
                                oldTask.TaskState = 1.ToString();
                                oldTask._UpdateTime = DateTime.Now;
                                __Task.Update(oldTask);
                                tasks = tasks1;
                                break;
                            }
                            else if (!canReturn)
                            {
                                #region goto Label4;
                                flag4 = false;
                                i = 0;
                                while (i < splitTransitionRefs.Count)
                                {
                                    id = ((TransitionRef)splitTransitionRefs[i]).GetId();
                                    transition = this.GetTransitionById(instance_0, id);
                                    if (transition.GetCondition().GetConditionTypeString().ToLower() == "reject")
                                    {
                                        this.SaveCheckedTransition(instance_0, transition, dbTran);
                                        item = this.GetActivityById(instance_0, transition.GetTo());
                                        if (Utility.IsVirtualNode(item))
                                        {
                                            #region
                                            enumerator = this.GetSplitTransitions(item, instance_0).GetEnumerator();
                                            try
                                            {
                                                while (true)
                                                {
                                                    if (enumerator.MoveNext())
                                                    {
                                                        current = (Transition)enumerator.Current;
                                                        if (this.CheckTransitionCondition(instance_0, current))
                                                        {
                                                            item = this.GetNextRealActivities(instance_0, current, id)[0];
                                                            this.SaveAndJoinTransition(item, oldTask.ActivityId, dbTran);
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                            finally
                                            {
                                                disposable = enumerator as IDisposable;
                                                if (disposable != null)
                                                {
                                                    disposable.Dispose();
                                                }
                                            }
                                            #endregion
                                        }
                                        if (this.CheckActivityState(item, instance_0, dbTran))
                                        {
                                            tasks1.AddRange(this.NewTask(instance_0, item, oldTask, dbTran));
                                        }
                                        flag3 = true;
                                        goto Label4;
                                    }
                                    else
                                    {
                                        i++;
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                this.ExecEventScript(instance_0, activity, "OnTaskRollBack", dbTran);
                                Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                taskById = TaskService.GetTaskById(oldTask.FromTaskId, dbTran);
                                activityById = this.GetActivityById(instance_0, taskById.ActivityId);
                                tasks1.AddRange(this.NewTask(instance_0, activityById, oldTask, dbTran));
                                tasks = tasks1;
                                break;
                            }
                        Label4:
                            #region
                            if (flag3)
                            {
                                goto case ActivityType.SubWorkflow;
                            }
                            if (flag4)
                            {
                                throw new Exception("没有找到合适的分支");
                            }
                            rollBackScope = activity.GetRollBackScope();
                            chrArray = new char[] { '|' };
                            strArrays1 = rollBackScope.Split(chrArray);
                            stringCollections1 = new StringCollection();
                            if (!((int)strArrays1.Length <= 1 ? true : rollBackScope.Length <= 2))
                            {
                                this.ExecEventScript(instance_0, activity, "OnTaskRollBack", dbTran);
                                Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                string str4 = strArrays1[1];
                                chrArray = new char[] { ',' };
                                stringCollections1.AddRange(str4.Split(chrArray));
                                tasks1 = this.RollBackTask(oldTask.TaskId, stringCollections1[0], dbTran);
                                goto case ActivityType.SubWorkflow;
                            }
                            else if (oldTask.TaskType != "Normal")
                            {
                                preActivityId = this.GetPreActivityId(instance_0, activity);
                                if (string.IsNullOrEmpty(preActivityId))
                                {
                                    throw new Exception("没有找到合适的回退分支");
                                }
                                this.ExecEventScript(instance_0, activity, "OnTaskRollBack", dbTran);
                                Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                tasks1 = this.RollBackTask(oldTask.TaskId, preActivityId, dbTran);
                                tasks = tasks1;
                                break;
                            }
                            else
                            {
                                this.ExecEventScript(instance_0, activity, "OnTaskRollBack", dbTran);
                                Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                taskById = TaskService.GetTaskById(oldTask.FromTaskId, dbTran);
                                activityById = this.GetActivityById(instance_0, taskById.ActivityId);
                                tasks1.AddRange(this.NewTask(instance_0, activityById, oldTask, dbTran));
                                tasks = tasks1;
                                break;
                            }
                            #endregion
                            #endregion
                        }
                        else if ((float)finishedUserTaskCount >= count)
                        {
                            #region
                            oldTask.TaskState = 2.ToString();
                            oldTask._UpdateTime = DateTime.Now;
                            __Task.Update(oldTask);
                            if (!canReturn)
                            {
                                #region
                                dealStrategy = activity.GetDealStrategy();
                                chrArray = new char[] { '|' };
                                strArrays = dealStrategy.Split(chrArray);
                                if (count2 >= single)
                                {
                                    flag1 = false;
                                }
                                else
                                {
                                    flag1 = (strArrays[1] != "1" ? true : userTask.Count != 0);
                                }
                                if (flag1)
                                {
                                    #region
                                    flag4 = false;
                                    i = 0;
                                    while (i < splitTransitionRefs.Count)
                                    {
                                        id = ((TransitionRef)splitTransitionRefs[i]).GetId();
                                        transition = this.GetTransitionById(instance_0, id);
                                        if (transition.GetCondition().GetConditionTypeString().ToLower() == "reject")
                                        {
                                            #region
                                            this.SaveCheckedTransition(instance_0, transition, dbTran);
                                            item = this.GetActivityById(instance_0, transition.GetTo());
                                            if (Utility.IsVirtualNode(item))
                                            {
                                                #region
                                                enumerator = this.GetSplitTransitions(item, instance_0).GetEnumerator();
                                                try
                                                {
                                                    while (true)
                                                    {
                                                        if (enumerator.MoveNext())
                                                        {
                                                            current = (Transition)enumerator.Current;
                                                            if (this.CheckTransitionCondition(instance_0, current))
                                                            {
                                                                item = this.GetNextRealActivities(instance_0, current, id)[0];
                                                                this.SaveAndJoinTransition(item, oldTask.ActivityId, dbTran);
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            break;
                                                        }
                                                    }
                                                }
                                                finally
                                                {
                                                    disposable = enumerator as IDisposable;
                                                    if (disposable != null)
                                                    {
                                                        disposable.Dispose();
                                                    }
                                                }
                                                #endregion
                                            }
                                            if (this.CheckActivityState(item, instance_0, dbTran))
                                            {
                                                tasks1.AddRange(this.NewTask(instance_0, item, oldTask, dbTran));
                                            }
                                            flag3 = true;
                                            goto Label5;
                                            #endregion
                                        }
                                        else
                                        {
                                            i++;
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region
                                    flag4 = true;
                                    i = 0;
                                    while (i < splitTransitionRefs.Count)
                                    {
                                        id = ((TransitionRef)splitTransitionRefs[i]).GetId();
                                        transition = this.GetTransitionById(instance_0, id);
                                        lower = transition.GetCondition().GetConditionTypeString().ToLower();
                                        if ((lower == "agree" ? true : lower == ""))
                                        {
                                            #region goto Label5;
                                            this.SaveCheckedTransition(instance_0, transition, dbTran);
                                            this.ExecEventScript(instance_0, activity, "OnTaskSubmit", dbTran);
                                            Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                            item = this.GetActivityById(instance_0, transition.GetTo());
                                            if (Utility.IsVirtualNode(item))
                                            {
                                                #region
                                                enumerator = this.GetSplitTransitions(item, instance_0).GetEnumerator();
                                                try
                                                {
                                                    while (true)
                                                    {
                                                        if (enumerator.MoveNext())
                                                        {
                                                            current = (Transition)enumerator.Current;
                                                            if (this.CheckTransitionCondition(instance_0, current))
                                                            {
                                                                item = this.GetNextRealActivities(instance_0, current, id)[0];
                                                                this.SaveAndJoinTransition(item, oldTask.ActivityId, dbTran);
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            break;
                                                        }
                                                    }
                                                }
                                                finally
                                                {
                                                    disposable = enumerator as IDisposable;
                                                    if (disposable != null)
                                                    {
                                                        disposable.Dispose();
                                                    }
                                                }
                                                #endregion
                                            }
                                            if (this.CheckActivityState(item, instance_0, dbTran))
                                            {
                                                tasks1.AddRange(this.NewTask(instance_0, item, oldTask, dbTran));
                                            }
                                            flag3 = true;
                                            goto Label5;
                                            #endregion
                                        }
                                        else
                                        {
                                            i++;
                                        }
                                    }
                                    #endregion
                                }
                            Label5:
                                #region
                                if (!flag3)
                                {
                                    if (flag4)
                                    {
                                        throw new Exception("没有找到合适的分支");
                                    }
                                    rollBackScope = activity.GetRollBackScope();
                                    chrArray = new char[] { '|' };
                                    strArrays1 = rollBackScope.Split(chrArray);
                                    stringCollections1 = new StringCollection();
                                    if (!((int)strArrays1.Length <= 1 ? true : rollBackScope.Length <= 2))
                                    {
                                        this.ExecEventScript(instance_0, activity, "OnTaskRollBack", dbTran);
                                        Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                        string str5 = strArrays1[1];
                                        chrArray = new char[] { ',' };
                                        stringCollections1.AddRange(str5.Split(chrArray));
                                        tasks1 = this.RollBackTask(oldTask.TaskId, stringCollections1[0], dbTran);
                                    }
                                    else if (oldTask.TaskType != "Normal")
                                    {
                                        preActivityId = this.GetPreActivityId(instance_0, activity);
                                        if (string.IsNullOrEmpty(preActivityId))
                                        {
                                            throw new Exception("没有找到合适的回退分支");
                                        }
                                        this.ExecEventScript(instance_0, activity, "OnTaskRollBack", dbTran);
                                        Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                        tasks1 = this.RollBackTask(oldTask.TaskId, preActivityId, dbTran);
                                        tasks = tasks1;
                                        break;
                                    }
                                    else
                                    {
                                        this.ExecEventScript(instance_0, activity, "OnTaskRollBack", dbTran);
                                        Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                        taskById = TaskService.GetTaskById(oldTask.FromTaskId, dbTran);
                                        activityById = this.GetActivityById(instance_0, taskById.ActivityId);
                                        tasks1.AddRange(this.NewTask(instance_0, activityById, oldTask, dbTran));
                                        tasks = tasks1;
                                        break;
                                    }

                                }
                                #endregion
                                oldTask.TaskState = 2.ToString();
                                oldTask._UpdateTime = DateTime.Now;
                                __Task.Update(oldTask);
                                goto case ActivityType.SubWorkflow;
                                #endregion
                            }
                            else
                            {
                                this.ExecEventScript(instance_0, activity, "OnTaskSubmit", dbTran);
                                Utility.SendAlert(instance_0, activity, oldTask, this.CurSession, 2, dbTran);
                                taskById = TaskService.GetTaskById(oldTask.FromTaskId, dbTran);
                                activityById = this.GetActivityById(instance_0, taskById.ActivityId);
                                tasks1.AddRange(this.NewTask(instance_0, activityById, oldTask, dbTran));
                                tasks = tasks1;
                                break;
                            }
                            #endregion
                        }
                        else
                        {
                            oldTask.TaskState = 1.ToString();
                            oldTask._UpdateTime = DateTime.Now;
                            __Task.Update(oldTask);
                            tasks = tasks1;
                            break;
                        }
                        #endregion
                    }
                default:
                    {
                        goto case ActivityType.SubWorkflow;
                    }
            }
            return tasks;
        }
   
	}
}