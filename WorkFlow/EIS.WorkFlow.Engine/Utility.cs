using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.AppModel;
using EIS.AppModel.Service;
using EIS.AppModel.Workflow;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using EIS.WorkFlow.Access;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using EIS.WorkFlow.XPDL.Utility;
using EIS.WorkFlow.XPDLParser.Elements;
using Microsoft.Practices.Unity;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Xml;

namespace EIS.WorkFlow.Engine
{
	public class Utility
	{
		private static Logger fileLogger;

		static Utility()
		{
			EIS.WorkFlow.Engine.Utility.fileLogger = LogManager.GetCurrentClassLogger();
		}

		public Utility()
		{
		}

		public static void DeleteUnDoneUserTaskByTaskId(string taskId, DbTransaction dbTran)
		{
			string str = string.Format("delete t_e_wf_usertask where (TaskState='1') and taskId = '{0}'", taskId);
			SysDatabase.ExecuteNonQuery(str, dbTran);
		}

		public static void DeleteUserTaskByActivityId(string instanceId, string actId, DbTransaction dbTran)
		{
			string str = string.Format("delete t_e_wf_usertask  where (TaskState='0' or TaskState='1') and taskId in \r\n                (select _autoId from T_E_WF_Task where (TaskState='0' or TaskState='1') and  instanceId ='{0}' and ActivityId='{1}')", instanceId, actId);
			SysDatabase.ExecuteNonQuery(str, dbTran);
		}

		public static void FetchInstance(string instanceId)
		{
			DateTime? nullable;
			Instance instanceById = InstanceService.GetInstanceById(instanceId);
			if ((instanceById.InstanceState == EnumDescription.GetFieldText(InstanceState.Finished) ? false : instanceById.InstanceState != EnumDescription.GetFieldText(InstanceState.Stoped)))
			{
				throw new Exception(string.Concat("流程状态为【", instanceById.InstanceState, "】，不能撤回"));
			}
			DataTable dataTable = SysDatabase.ExecuteTable(string.Format("select u._autoid,taskId from t_e_wf_usertask u where  \r\n                        instanceId='{0}' and u._isdel=0 and (TaskState='2') order by _CreateTime desc", instanceId));
			if (dataTable.Rows.Count > 0)
			{
				DbConnection dbConnection = SysDatabase.CreateConnection();
				try
				{
					dbConnection.Open();
					DbTransaction dbTransaction = dbConnection.BeginTransaction();
					try
					{
						try
						{
							string str = dataTable.Rows[0]["TaskId"].ToString();
							_Task __Task = new _Task(dbTransaction);
							_UserTask __UserTask = new _UserTask(dbTransaction);
							if (!(__Task.GetModel(str).DefineType == "End"))
							{
								__UserTask.ResumeUserTaskByInstanceId(instanceId);
							}
							else
							{
								__UserTask.DeleteByTaskId(str);
								__Task.Delete(str);
								string str1 = dataTable.Rows[1]["_AutoId"].ToString();
								UserTask model = __UserTask.GetModel(str1);
								model._UpdateTime = DateTime.Now;
								nullable = null;
								model.DealTime = nullable;
								model.TaskState = "1";
								__UserTask.Update(model);
								string str2 = dataTable.Rows[1]["TaskId"].ToString();
								Task task = __Task.GetModel(str2);
								task.TaskState = 0.ToString();
								__Task.Update(task);
							}
							instanceById.InstanceState = EnumDescription.GetFieldText(InstanceState.Processing);
							nullable = null;
							instanceById.FinishTime = nullable;
							(new _Instance(dbTransaction)).Update(instanceById);
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
				finally
				{
					if (dbConnection != null)
					{
						((IDisposable)dbConnection).Dispose();
					}
				}
			}
		}

		public static string FetchTask(string instanceId, string employeeId)
		{
			DbConnection dbConnection;
			DbTransaction dbTransaction;
			_Task __Task;
			_UserTask __UserTask;
			string str;
			string taskId;
			UserTask model;
			Exception exception;
			string str1;
			DateTime? nullable;
			bool flag = false;
			string str2 = "";
			Instance instanceById = InstanceService.GetInstanceById(instanceId);
			if (instanceById.InstanceState != EnumDescription.GetFieldText(InstanceState.Processing))
			{
				throw new Exception(string.Concat("任务状态为【", instanceById.InstanceState, "】，不能撤回"));
			}
			string str3 = string.Format("select u.* from t_e_wf_task t inner join t_e_wf_usertask u on t._autoid=u.taskId where t.InstanceId='{0}' \r\n                    and (t.TaskState='0' or t.TaskState='1') and u.TaskState='2' and u.dealuser='{1}'", instanceId, employeeId);
			DataTable dataTable = SysDatabase.ExecuteTable(str3);
			if (dataTable.Rows.Count <= 0)
			{
				dataTable = SysDatabase.ExecuteTable(string.Format("select u._autoid,taskId from t_e_wf_usertask u where  \r\n                instanceId='{0}' and u._isdel=0 and (TaskState='0' or TaskState='1')", instanceId));
				if (dataTable.Rows.Count == 0)
				{
					throw new Exception("找不到活动任务，不能撤回任务");
				}
				dbConnection = SysDatabase.CreateConnection();
				try
				{
					dbConnection.Open();
					dbTransaction = dbConnection.BeginTransaction();
					try
					{
						try
						{
							foreach (DataRow row in dataTable.Rows)
							{
								taskId = row["taskId"].ToString();
								__Task = new _Task(dbTransaction);
								__UserTask = new _UserTask(dbTransaction);
								Task taskById = TaskService.GetTaskById(taskId);
								if (!(taskById.TaskState != 0.ToString()))
								{
									Task task = TaskService.GetTaskById(taskById.FromTaskId);
									while (task != null)
									{
										str3 = string.Format("select * from t_e_wf_usertask u where instanceId='{0}' and dealuser='{1}' and taskId='{2}' and u._isdel=0 and TaskState='2' order by _createTime desc", instanceId, employeeId, task.TaskId);
										DataTable dataTable1 = SysDatabase.ExecuteTable(str3, dbTransaction);
										if (!(dataTable1.Rows.Count <= 0 ? true : !(task.IsManualTask == "1")))
										{
											__UserTask.DeleteByTaskId(taskId);
											__Task.Delete(taskId);
											FinishTransitionService.ClearFinishSplitTransition(instanceId, task.ActivityId, dbTransaction);
											str = dataTable1.Rows[0]["_autoid"].ToString();
											str2 = str;
											model = __UserTask.GetModel(str);
											model._UpdateTime = DateTime.Now;
											nullable = null;
											model.DealTime = nullable;
											model.TaskState = "1";
											__UserTask.Update(model);
											task.TaskState = 0.ToString();
											__Task.Update(task);
											flag = true;
											break;
										}
										else if (!(task.IsManualTask == "0"))
										{
											break;
										}
										else
										{
											__UserTask.DeleteByTaskId(taskId);
											__Task.Delete(taskId);
											FinishTransitionService.ClearFinishSplitTransition(instanceId, task.ActivityId, dbTransaction);
											taskId = task.TaskId;
											taskById = task;
											task = __Task.GetModel(task.FromTaskId);
										}
									}
									if (flag)
									{
										break;
									}
								}
							}
							if (!flag)
							{
								dbTransaction.Rollback();
							}
							else
							{
								dbTransaction.Commit();
							}
						}
						catch (Exception exception1)
						{
							exception = exception1;
							dbTransaction.Rollback();
							throw exception;
						}
					}
					finally
					{
						dbConnection.Close();
					}
				}
				finally
				{
					if (dbConnection != null)
					{
						((IDisposable)dbConnection).Dispose();
					}
				}
				if (!flag)
				{
					throw new Exception("后续步骤已经审批，不能撤回");
				}
				str1 = str2;
			}
			else
			{
				dbConnection = SysDatabase.CreateConnection();
				try
				{
					dbConnection.Open();
					dbTransaction = dbConnection.BeginTransaction();
					try
					{
						try
						{
							__Task = new _Task(dbTransaction);
							__UserTask = new _UserTask(dbTransaction);
							str = dataTable.Rows[0]["_autoid"].ToString();
							str2 = str;
							taskId = dataTable.Rows[0]["taskId"].ToString();
							model = __UserTask.GetModel(str);
							model._UpdateTime = DateTime.Now;
							nullable = null;
							model.DealTime = nullable;
							model.TaskState = "1";
							__UserTask.Update(model);
							Task model1 = __Task.GetModel(taskId);
							model1.TaskState = 0.ToString();
							__Task.Update(model1);
							dbTransaction.Commit();
						}
						catch (Exception exception2)
						{
							exception = exception2;
							dbTransaction.Rollback();
							throw exception;
						}
					}
					finally
					{
						dbConnection.Close();
					}
					str1 = str2;
				}
				finally
				{
					if (dbConnection != null)
					{
						((IDisposable)dbConnection).Dispose();
					}
				}
			}
			return str1;
		}
        public static void FinishInstance(string instanceId)
        {
            DriverEngine driverEngine = new DriverEngine();
            Instance instanceById = driverEngine.GetInstanceById(instanceId);
            DbConnection dbConnection = SysDatabase.CreateConnection();
            try
            {
                dbConnection.Open();
                DbTransaction dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        driverEngine.EndInstance(instanceById, dbTransaction);
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
            finally
            {
                if (dbConnection != null)
                {
                    ((IDisposable)dbConnection).Dispose();
                }
            }
        }

		public static void FinishInstance(string instanceId, UserContext userInfo)
		{
			DriverEngine driverEngine = new DriverEngine();
			Instance instanceById = driverEngine.GetInstanceById(instanceId);
			driverEngine.CurSession = new WFSession(userInfo, instanceById, EIS.WorkFlow.Engine.Utility.GetAppData(instanceById), null);
			DbConnection dbConnection = SysDatabase.CreateConnection();
			try
			{
				dbConnection.Open();
				DbTransaction dbTransaction = dbConnection.BeginTransaction();
				try
				{
					try
					{
						driverEngine.EndInstance(instanceById, dbTransaction);
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
			finally
			{
				if (dbConnection != null)
				{
					((IDisposable)dbConnection).Dispose();
				}
			}
		}

		public static string FormatMailHtml(string html)
		{
			html = html.Replace("\r\n", "<br/>").Replace(" ", "&nbsp;").Replace("\t", "&nbsp;&nbsp;");
			return html;
		}

		internal static string GenXPDLPath(Instance ins)
		{
			DateTime dateTime = ins._CreateTime;
			string str = Path.Combine("T_E_WF_Instance", dateTime.ToString("yyyy年MM月"), string.Concat(ins.InstanceId, ".xml"));
			return str;
		}

		public static List<DataControl> GetActivityDataControl(Activity actObj)
		{
			bool? nullable;
			List<DataControl> dataControls = new List<DataControl>();
			ExtendedAttribute extendedAttributeByName = actObj.GetExtendedAttributes().GetExtendedAttributeByName("DataControl");
			if (extendedAttributeByName != null)
			{
				foreach (NonEmptyNode node in extendedAttributeByName.GetNodes())
				{
					bool? nullable1 = null;
					bool? nullable2 = null;
					bool? nullable3 = null;
					if (node.GetAttribute("CanRead").Length > 0)
					{
						nullable1 = new bool?(node.GetAttribute("CanRead") == "True");
						bool? nullable4 = nullable1;
						if (nullable4.HasValue)
						{
							nullable = new bool?(!nullable4.GetValueOrDefault());
						}
						else
						{
							nullable = null;
						}
						nullable2 = nullable;
					}
					if (node.GetAttribute("NotNull").Length > 0)
					{
						nullable3 = new bool?(node.GetAttribute("NotNull") == "True");
					}
					DataControl dataControl = new DataControl()
					{
						BizName = node.GetAttribute("TableName"),
						FieldName = node.GetAttribute("ColumnName"),
						DefaultType = node.GetAttribute("DefaultType"),
						DefaultValue = node.GetAttribute("StartValue"),
						CanWrite = nullable2,
						CanRead = nullable1,
						NotNull = nullable3
					};
					dataControls.Add(dataControl);
				}
			}
			return dataControls;
		}

		public static List<DeptEmployee> GetActivityUser(Instance ins, Activity actObj, WFSession context)
		{
			return EIS.WorkFlow.Engine.Utility.GetActivityUser(ins, actObj, context, null);
		}

		public static List<DeptEmployee> GetActivityUser(Instance ins, Activity actObj, WFSession context, DbTransaction dbTran)
		{
			StringCollection stringCollections = new StringCollection();
			List<DeptEmployee> activityUser = EIS.WorkFlow.Engine.Utility.GetActivityUser(ins, actObj, context, dbTran, ref stringCollections);
			return activityUser;
		}

		public static List<DeptEmployee> GetActivityUser(Instance ins, Activity actObj, WFSession context, DbTransaction dbTran, ref StringCollection retInfo)
		{
			List<DeptEmployee> deptEmployees = new List<DeptEmployee>();
			List<DeptEmployee> performers = new List<DeptEmployee>();
			TaskPerformer2 taskPerformer2 = new TaskPerformer2()
			{
				uc = context.UserInfo,
				ins = ins
			};
            try
            {
                EIS.WorkFlow.Engine.Utility.fileLogger.Debug("GetActivityUser：ins._UserName={0}", ins._UserName);
                if ((actObj.GetNodeType() != ActivityType.Start ? true : !(ins._UserName != "")))
                {
                    Performers performer = actObj.GetPerformers();
                    performers = EIS.WorkFlow.Engine.Utility.GetPerformers(ins, actObj, performer, context, dbTran, ref retInfo);
                    deptEmployees = performers;
                }
                else
                {
                    DeptEmployee initiator = taskPerformer2.GetInitiator(dbTran);
                    if (initiator != null)
                    {
                        performers.Add(initiator);
                    }
                    deptEmployees = performers;
                }
            }
            catch { }
            finally { } 
			return deptEmployees;
		}

		public static string GetAgentId(string workflowId, string employeeId, DbTransaction dbTran)
		{
			string str;
			string workflowBizType = DefineService.GetWorkflowBizType(workflowId, dbTran);
			string str1 = string.Format("select top 1 AgentId from T_E_App_Agent  where EmployeeId ='{0}' and (IsNull(WFType,'')='{1}'  or IsNull(WFType,'')='') \r\n                and Enable='是' and getdate() between StartTime and EndTime order by IsNull(WFType,'') desc", employeeId, workflowBizType);
			object obj = SysDatabase.ExecuteScalar(str1, dbTran);
			if ((obj == null ? true : obj == DBNull.Value))
			{
				str = "";
			}
			else
			{
				str = (!EmployeeService.CheckedValid(obj.ToString()) ? "" : obj.ToString());
			}
			return str;
		}

		public static DataRow GetAppData(Instance ins)
		{
			string str;
			DataRow item = null;
			TableInfo model = (new _TableInfo(ins.AppName)).GetModel();
			if (model.TableType == 1)
			{
				str = string.Format("select * from {0} where _AutoId='{1}'", ins.AppName, ins.AppId);
				item = SysDatabase.ExecuteTable(str).Rows[0];
			}
			else if (model.TableType == 3)
			{
				string str1 = string.Concat(" _AutoID='", ins.AppId, "'");
				str = model.DetailSQL.Replace("|^condition^|", str1);
				CustomDb customDb = new CustomDb();
				customDb.CreateDatabaseByConnectionId(model.ConnectionId);
				item = customDb.ExecuteTable(str).Rows[0];
			}
			return item;
		}

		private static StringCollection GetEmployeeIdList(List<DeptEmployee> list)
		{
			StringCollection stringCollections = new StringCollection();
			foreach (DeptEmployee deptEmployee in list)
			{
				if (deptEmployee != null)
				{
					if (!stringCollections.Contains(deptEmployee.EmployeeID))
					{
						stringCollections.Add(deptEmployee.EmployeeID);
					}
				}
			}
			return stringCollections;
		}

		public static List<DeptEmployee> GetPerformers(Instance ins, Activity actObj, Performers ps, WFSession context, DbTransaction dbTran)
		{
			StringCollection stringCollections = new StringCollection();
			List<DeptEmployee> performers = EIS.WorkFlow.Engine.Utility.GetPerformers(ins, actObj, ps, context, dbTran, ref stringCollections);
			return performers;
		}

		public static List<DeptEmployee> GetPerformers(Instance ins, Activity actObj, Performers ps, WFSession context, DbTransaction dbTran, ref StringCollection retInfo)
		{
			string[] strArrays;
			string[] strArrays1;
			int i;
			string employeeID;
			DeptEmployee deptEmployeeByPositionId;
			string positionId;
			List<DeptEmployee> deptEmployees;
			string str;
			string str1;
			DeptEmployee deptEmployee = null;
			Department defaultDeptById;
			Position model;
			string str2;
			string deptId;
			string str3;
			string queryListSQL;
			DataRow row = null;
			string[] strArrays2;
			List<DeptEmployee> deptEmployees1 = new List<DeptEmployee>();
			TaskPerformer2 taskPerformer2 = new TaskPerformer2()
			{
				uc = context.UserInfo,
				ins = ins
			};
			if (ps != null)
			{
				DriverEngine driverEngine = new DriverEngine(context.UserInfo);
				DataRow appData = context.AppData;
			Label1:
				foreach (Performer performer in ps.GetPerformers())
				{
					string description = performer.GetDescription();
					char[] chrArray = new char[] { '|' };
					string[] strArrays3 = description.Split(chrArray);
					if ((int)strArrays3.Length >= 4)
					{
						string str4 = EIS.AppBase.Utility.Xml2String(strArrays3[3]);
						if (str4.Trim().Length > 0)
						{
							str4 = driverEngine.ReplaceWithDataRow(str4, ins.AppName, context.AppData);
							EIS.WorkFlow.Engine.Utility.fileLogger.Debug<string, string>("InstanceId={0},处理人条件condExpr={1}", ins.InstanceId, str4);
							try
							{
								if (int.Parse(SysDatabase.ExecuteScalar(string.Concat("select count(*) where ", str4)).ToString()) == 0)
								{
									continue;
								}
							}
							catch (Exception exception1)
							{
								Exception exception = exception1;
								EIS.WorkFlow.Engine.Utility.fileLogger.Error<Exception>(exception);
								throw new Exception(string.Concat("验证处理人条件条件时出错：条件表达式=", str4), exception);
							}
						}
					}
					string str5 = "1";
					if ((int)strArrays3.Length >= 5)
					{
						str5 = (strArrays3[4] == "" ? "1" : strArrays3[4]);
					}
					string str6 = strArrays3[0];
					if (str6 != null)
					{
						switch (str6)
						{
							case "01":
							{
								if (strArrays3[2].Length > 0)
								{
									retInfo.Add(description);
									string str7 = strArrays3[2];
									chrArray = new char[] { '=' };
									strArrays = str7.Split(chrArray);
									if ((int)strArrays.Length != 1)
									{
										string str8 = strArrays[0];
										chrArray = new char[] { ',' };
										strArrays1 = str8.Split(chrArray);
										string str9 = strArrays[1];
										chrArray = new char[] { ',' };
										string[] strArrays4 = str9.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											employeeID = strArrays1[i];
											positionId = strArrays4[i];
											deptEmployeeByPositionId = DeptEmployeeService.GetDeptEmployeeByPositionId(employeeID, positionId);
											if (!(deptEmployeeByPositionId == null ? true : !EmployeeService.CheckedValid(employeeID)))
											{
												if (!deptEmployees1.Contains(deptEmployeeByPositionId))
												{
													deptEmployeeByPositionId.DealType = str5;
													deptEmployees1.Add(deptEmployeeByPositionId);
												}
											}
											else if (!EmployeeService.CheckedValid(employeeID))
											{
												deptEmployees = DeptEmployeeService.GetDeptEmployeeByPositionId(positionId);
												if (deptEmployees.Count == 1)
												{
													if (!deptEmployees1.Contains(deptEmployees[0]))
													{
														deptEmployees[0].DealType = str5;
														deptEmployees1.Add(deptEmployees[0]);
													}
												}
											}
											else
											{
												deptEmployeeByPositionId = DeptEmployeeService.GetOrignalDeptEmployee(employeeID);
												if (deptEmployeeByPositionId != null)
												{
													if (!deptEmployees1.Contains(deptEmployeeByPositionId))
													{
														deptEmployeeByPositionId.DealType = str5;
														deptEmployees1.Add(deptEmployeeByPositionId);
													}
												}
											}
										}
									}
									else
									{
										string str10 = strArrays[0];
										chrArray = new char[] { ',' };
										strArrays1 = str10.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											employeeID = strArrays1[i];
											deptEmployeeByPositionId = DeptEmployeeService.GetOrignalDeptEmployee(employeeID);
											if (deptEmployeeByPositionId != null)
											{
												if (!deptEmployees1.Contains(deptEmployeeByPositionId))
												{
													deptEmployeeByPositionId.DealType = str5;
													deptEmployees1.Add(deptEmployeeByPositionId);
												}
											}
										}
									}
								}
								break;
							}
							case "02":
							{
								if (strArrays3[2].Length > 0)
								{
									retInfo.Add(description);
									foreach (DeptEmployee deptEmployeeByPositionIdA in taskPerformer2.FromPositions(description))
									{
                                        if (!deptEmployees1.Contains(deptEmployeeByPositionIdA))
										{
                                            deptEmployeeByPositionIdA.DealType = str5;
                                            deptEmployees1.Add(deptEmployeeByPositionIdA);
										}
									}
								}
								break;
							}
							case "03":
							{
								if (strArrays3[2].Length > 0)
								{
									retInfo.Add(description);
									foreach (DeptEmployee deptEmployee1 in taskPerformer2.FromRoles(description))
									{
										if (!deptEmployees1.Contains(deptEmployee1))
										{
											deptEmployee1.DealType = str5;
											deptEmployees1.Add(deptEmployee1);
										}
									}
								}
								break;
							}
							case "04":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									str = driverEngine.ReplaceWithDataRow(strArrays3[2], ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", str };
									retInfo.Add(string.Concat(strArrays2));
									if (str.Length > 0)
									{
										chrArray = new char[] { ',' };
										strArrays1 = str.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											employeeID = strArrays1[i];
											deptEmployeeByPositionId = DeptEmployeeService.GetOrignalDeptEmployee(employeeID);
											if (deptEmployeeByPositionId != null)
											{
												if (!deptEmployees1.Contains(deptEmployeeByPositionId))
												{
													deptEmployeeByPositionId.DealType = str5;
													deptEmployees1.Add(deptEmployeeByPositionId);
												}
											}
										}
									}
								}
								break;
							}
							case "05":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									str1 = driverEngine.ReplaceWithDataRow(strArrays3[2], ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", str1 };
									retInfo.Add(string.Concat(strArrays2));
									if (str1.Length > 0)
									{
										chrArray = new char[] { ',' };
										strArrays1 = str1.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											positionId = strArrays1[i];
											deptEmployees = DeptEmployeeService.GetDeptEmployeeByPositionId(positionId);
											foreach (DeptEmployee deptEmployeeA in deptEmployees)
											{
                                                if (!deptEmployees1.Contains(deptEmployeeA))
												{
													deptEmployee.DealType = str5;
                                                    deptEmployees1.Add(deptEmployeeA);
												}
											}
										}
									}
								}
								break;
							}
							case "06":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									str = driverEngine.ReplaceWithDataRow(strArrays3[2], ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", str };
									retInfo.Add(string.Concat(strArrays2));
									if (str.Length > 0)
									{
										chrArray = new char[] { ',' };
										strArrays1 = str.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											employeeID = strArrays1[i];
											defaultDeptById = EmployeeService.GetDefaultDeptById(employeeID);
											if ((defaultDeptById == null ? false : !string.IsNullOrEmpty(defaultDeptById.PicPositionId)))
											{
												foreach (DeptEmployee deptEmployeeByPositionId1 in DeptEmployeeService.GetDeptEmployeeByPositionId(defaultDeptById.PicPositionId))
												{
													if (!deptEmployees1.Contains(deptEmployeeByPositionId1))
													{
														deptEmployeeByPositionId1.DealType = str5;
														deptEmployees1.Add(deptEmployeeByPositionId1);
													}
												}
											}
										}
									}
								}
								break;
							}
							case "07":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									str1 = driverEngine.ReplaceWithDataRow(strArrays3[2], ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", str1 };
									retInfo.Add(string.Concat(strArrays2));
									if (str1.Length > 0)
									{
										chrArray = new char[] { ',' };
										strArrays1 = str1.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											positionId = strArrays1[i];
											model = (new _Position()).GetModel(positionId);
											if (model != null)
											{
												if (!string.IsNullOrEmpty(model.ParentPositionId))
												{
													deptEmployees = DeptEmployeeService.GetDeptEmployeeByPositionId(model.ParentPositionId);
													foreach (DeptEmployee deptEmployee2 in deptEmployees)
													{
														if (!deptEmployees1.Contains(deptEmployee2))
														{
															deptEmployee2.DealType = str5;
															deptEmployees1.Add(deptEmployee2);
														}
													}
												}
											}
										}
									}
								}
								break;
							}
							case "08":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									str2 = driverEngine.ReplaceWithDataRow(strArrays3[2], ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", str2 };
									retInfo.Add(string.Concat(strArrays2));
									if (str2.Length > 0)
									{
										EIS.WorkFlow.Engine.Utility.fileLogger.Trace("特定部门[表单指定]的负责人,{0}", str2);
										chrArray = new char[] { ',' };
										strArrays1 = str2.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											deptId = strArrays1[i];
											defaultDeptById = DepartmentService.GetModel(deptId);
											if ((defaultDeptById == null ? false : !string.IsNullOrEmpty(defaultDeptById.PicPositionId)))
											{
												foreach (DeptEmployee deptEmployeeByPositionId2 in DeptEmployeeService.GetDeptEmployeeByPositionId(defaultDeptById.PicPositionId))
												{
													if (!deptEmployees1.Contains(deptEmployeeByPositionId2))
													{
														deptEmployeeByPositionId2.DealType = str5;
														deptEmployees1.Add(deptEmployeeByPositionId2);
													}
												}
											}
										}
									}
								}
								break;
							}
							case "09":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									str2 = driverEngine.ReplaceWithDataRow(strArrays3[2], ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", str2 };
									retInfo.Add(string.Concat(strArrays2));
									if (str2.Length > 0)
									{
										chrArray = new char[] { ',' };
										strArrays1 = str2.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											deptId = strArrays1[i];
											defaultDeptById = DepartmentService.GetModel(deptId);
											if ((defaultDeptById == null ? false : !string.IsNullOrEmpty(defaultDeptById.UpPositionId)))
											{
												deptEmployees = DeptEmployeeService.GetDeptEmployeeByPositionId(defaultDeptById.UpPositionId);
												foreach (DeptEmployee deptEmployee3 in deptEmployees)
												{
													if (!deptEmployees1.Contains(deptEmployee3))
													{
														deptEmployee3.DealType = str5;
														deptEmployees1.Add(deptEmployee3);
													}
												}
											}
										}
									}
								}
								break;
							}
							case "0A":
							{
								if (strArrays3[2].Length > 0)
								{
									deptId = strArrays3[2];
									defaultDeptById = DepartmentService.GetModel(deptId);
									if (!string.IsNullOrEmpty(defaultDeptById.UpPositionId))
									{
										deptEmployees = DeptEmployeeService.GetDeptEmployeeByPositionId(defaultDeptById.UpPositionId);
										foreach (DeptEmployee deptEmployee4 in deptEmployees)
										{
											if (!deptEmployees1.Contains(deptEmployee4))
											{
												deptEmployee4.DealType = str5;
												deptEmployees1.Add(deptEmployee4);
											}
										}
									}
									if (!string.IsNullOrEmpty(defaultDeptById.UpEmployeeId))
									{
										string upEmployeeId = defaultDeptById.UpEmployeeId;
										chrArray = new char[] { ',' };
										strArrays1 = upEmployeeId.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											employeeID = strArrays1[i];
											deptEmployeeByPositionId = DeptEmployeeService.GetOrignalDeptEmployee(employeeID);
											if (deptEmployeeByPositionId != null)
											{
												if (!deptEmployees1.Contains(deptEmployeeByPositionId))
												{
													deptEmployeeByPositionId.DealType = str5;
													deptEmployees1.Add(deptEmployeeByPositionId);
												}
											}
										}
									}
								}
								break;
							}
							case "11":
							{
								DeptEmployee initiator = taskPerformer2.GetInitiator(dbTran);
								if ((initiator == null ? false : !deptEmployees1.Contains(initiator)))
								{
									deptEmployees1.Add(taskPerformer2.GetInitiator(dbTran));
								}
								break;
							}
							case "12":
							{
								retInfo.Add(description);
								foreach (DeptEmployee startDeptLeader in taskPerformer2.GetStartDeptLeader(description))
								{
									if (!deptEmployees1.Contains(startDeptLeader))
									{
										startDeptLeader.DealType = str5;
										deptEmployees1.Add(startDeptLeader);
									}
								}
								break;
							}
							case "13":
							{
								retInfo.Add(description);
								foreach (DeptEmployee upLevelDeptLeader in taskPerformer2.GetUpLevelDeptLeader(description, dbTran))
								{
									if (!deptEmployees1.Contains(upLevelDeptLeader))
									{
										upLevelDeptLeader.DealType = str5;
										deptEmployees1.Add(upLevelDeptLeader);
									}
								}
								break;
							}
							case "14":
							{
								retInfo.Add(description);
								foreach (DeptEmployee topLevelDeptLeader in taskPerformer2.GetTopLevelDeptLeader(description, dbTran))
								{
									if (!deptEmployees1.Contains(topLevelDeptLeader))
									{
										topLevelDeptLeader.DealType = str5;
										deptEmployees1.Add(topLevelDeptLeader);
									}
								}
								break;
							}
							case "21":
							{
								if (strArrays3[2].Length > 0)
								{
									retInfo.Add(description);
									(new DriverEngine()).GetActivityById(ins, strArrays3[2]);
									foreach (DeptEmployee taskDealUser in taskPerformer2.GetTaskDealUser(description, actObj, dbTran,false))
									{
										if (!deptEmployees1.Contains(taskDealUser))
										{
											taskDealUser.DealType = str5;
											deptEmployees1.Add(taskDealUser);
										}
									}
								}
								break;
							}
							case "22":
							{
								if (strArrays3[2].Length > 0)
								{
									retInfo.Add(description);
									if (!(strArrays3[2] == context.CurActivity.GetId()))
									{
										foreach (DeptEmployee positionLeader in taskPerformer2.GetPositionLeader(description, actObj, dbTran))
										{
											if (!deptEmployees1.Contains(positionLeader))
											{
												positionLeader.DealType = str5;
												deptEmployees1.Add(positionLeader);
											}
										}
									}
									else
									{
										positionId = context.UserInfo.PositionId;
										model = (new _Position()).GetModel(positionId);
										if (model != null)
										{
											if (!string.IsNullOrEmpty(model.ParentPositionId))
											{
												deptEmployees = DeptEmployeeService.GetDeptEmployeeByPositionId(model.ParentPositionId);
												foreach (DeptEmployee deptEmployee5 in deptEmployees)
												{
													if (!deptEmployees1.Contains(deptEmployee5))
													{
														deptEmployee5.DealType = str5;
														deptEmployees1.Add(deptEmployee5);
													}
												}
											}
										}
									}
								}
								break;
							}
							case "23":
							{
								if (strArrays3[2].Length > 0)
								{
									retInfo.Add(description);
									if (!(strArrays3[2] == context.CurActivity.GetId()))
									{
										foreach (DeptEmployee taskDeptLeader in taskPerformer2.GetTaskDeptLeader(description, actObj, dbTran))
										{
											if (!deptEmployees1.Contains(taskDeptLeader))
											{
												taskDeptLeader.DealType = str5;
												deptEmployees1.Add(taskDeptLeader);
											}
										}
									}
									else
									{
										deptId = context.UserInfo.DeptId;
										defaultDeptById = DepartmentService.GetModel(deptId);
										if (!string.IsNullOrEmpty(defaultDeptById.PicPositionId))
										{
											deptEmployees = DeptEmployeeService.GetDeptEmployeeByPositionId(defaultDeptById.PicPositionId);
											foreach (DeptEmployee deptEmployee6 in deptEmployees)
											{
												if (!deptEmployees1.Contains(deptEmployee6))
												{
													deptEmployee6.DealType = str5;
													deptEmployees1.Add(deptEmployee6);
												}
											}
										}
									}
								}
								break;
							}
							case "24":
							{
								if (strArrays3[2].Length > 0)
								{
									retInfo.Add(description);
									if (!(strArrays3[2] == context.CurActivity.GetId()))
									{
										foreach (DeptEmployee taskDeptUpLeader in taskPerformer2.GetTaskDeptUpLeader(description, actObj, dbTran))
										{
											if (!deptEmployees1.Contains(taskDeptUpLeader))
											{
												taskDeptUpLeader.DealType = str5;
												deptEmployees1.Add(taskDeptUpLeader);
											}
										}
									}
									else
									{
										deptId = context.UserInfo.DeptId;
										defaultDeptById = DepartmentService.GetModel(deptId);
										if (!string.IsNullOrEmpty(defaultDeptById.UpPositionId))
										{
											deptEmployees = DeptEmployeeService.GetDeptEmployeeByPositionId(defaultDeptById.UpPositionId);
											foreach (DeptEmployee deptEmployee7 in deptEmployees)
											{
												if (!deptEmployees1.Contains(deptEmployee7))
												{
													deptEmployee7.DealType = str5;
													deptEmployees1.Add(deptEmployee7);
												}
											}
										}
										if (!string.IsNullOrEmpty(defaultDeptById.UpEmployeeId))
										{
											string upEmployeeId1 = defaultDeptById.UpEmployeeId;
											chrArray = new char[] { ',' };
											strArrays1 = upEmployeeId1.Split(chrArray);
											for (i = 0; i < (int)strArrays1.Length; i++)
											{
												employeeID = strArrays1[i];
												deptEmployeeByPositionId = DeptEmployeeService.GetOrignalDeptEmployee(employeeID);
												if (deptEmployeeByPositionId != null)
												{
													if (!deptEmployees1.Contains(deptEmployeeByPositionId))
													{
														deptEmployeeByPositionId.DealType = str5;
														deptEmployees1.Add(deptEmployeeByPositionId);
													}
												}
											}
										}
									}
								}
								break;
							}
							case "31":
							{
                                //岗位属性的处理人
								if (strArrays3[2].Length > 0)
								{
									retInfo.Add(description);
									foreach (DeptEmployee employeeByPositionProp in taskPerformer2.GetEmployeeByPositionProp(description))
									{
										if (!deptEmployees1.Contains(employeeByPositionProp))
										{
											employeeByPositionProp.DealType = str5;
											deptEmployees1.Add(employeeByPositionProp);
										}
									}
								}
								break;
							}
							case "32":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									string str11 = strArrays3[2];
									chrArray = new char[] { ';' };
									strArrays = str11.Split(chrArray);
									str3 = strArrays[0];
									queryListSQL = EIS.WorkFlow.Engine.Utility.GetQueryListSQL(str3, strArrays3[2], appData);
									queryListSQL = driverEngine.ReplaceWithDataRow(queryListSQL, ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", queryListSQL };
									retInfo.Add(string.Concat(strArrays2));

                                    DataTable empTbl = new DataTable();
                                    try
                                    {
                                        empTbl = (dbTran == null ? SysDatabase.ExecuteTable(queryListSQL) : SysDatabase.ExecuteTable(queryListSQL, dbTran));
                                    }
                                    catch { }
                                    finally { }

                                    foreach (DataRow dataRow in empTbl.Rows)
									{
										str = dataRow[0].ToString();
										if (str.Length > 0)
										{
											chrArray = new char[] { ',' };
											strArrays1 = str.Split(chrArray);
											for (i = 0; i < (int)strArrays1.Length; i++)
											{
												employeeID = strArrays1[i];
												if (!(employeeID == ""))
												{
													deptEmployeeByPositionId = DeptEmployeeService.GetOrignalDeptEmployee(employeeID);
													if (deptEmployeeByPositionId != null)
													{
														if (!deptEmployees1.Contains(deptEmployeeByPositionId))
														{
															deptEmployeeByPositionId.DealType = str5;
															deptEmployees1.Add(deptEmployeeByPositionId);
														}
													}
												}
											}
										}
									}
								}
								break;
							}
							case "33":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									string str12 = strArrays3[2];
									chrArray = new char[] { ';' };
									strArrays = str12.Split(chrArray);
									str3 = strArrays[0];
									queryListSQL = EIS.WorkFlow.Engine.Utility.GetQueryListSQL(str3, strArrays3[2], appData);
									queryListSQL = driverEngine.ReplaceWithDataRow(queryListSQL, ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", queryListSQL };
									retInfo.Add(string.Concat(strArrays2));
									foreach (DataRow row1 in SysDatabase.ExecuteTable(queryListSQL).Rows)
									{
										str1 = row1[0].ToString();
										if (str1.Length > 0)
										{
											chrArray = new char[] { ',' };
											strArrays1 = str1.Split(chrArray);
											for (i = 0; i < (int)strArrays1.Length; i++)
											{
												positionId = strArrays1[i];
												if (!(positionId == ""))
												{
													deptEmployees = DeptEmployeeService.GetDeptEmployeeByPositionId(positionId);
													foreach (DeptEmployee deptEmployee8 in deptEmployees)
													{
														if (!deptEmployees1.Contains(deptEmployee8))
														{
															deptEmployee8.DealType = str5;
															deptEmployees1.Add(deptEmployee8);
														}
													}
												}
											}
										}
									}
								}
								break;
							}
							case "34":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									string str13 = strArrays3[2];
									chrArray = new char[] { ';' };
									strArrays = str13.Split(chrArray);
									str3 = strArrays[0];
									queryListSQL = EIS.WorkFlow.Engine.Utility.GetQueryListSQL(str3, strArrays3[2], appData);
									queryListSQL = driverEngine.ReplaceWithDataRow(queryListSQL, ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", queryListSQL };
									retInfo.Add(string.Concat(strArrays2));
									foreach (DataRow rowA in SysDatabase.ExecuteTable(queryListSQL).Rows)
									{
                                        str2 = rowA[0].ToString();
										if (str2.Length > 0)
										{
											EIS.WorkFlow.Engine.Utility.fileLogger.Trace("特定部门[SQL指定]的负责人,{0}", str2);
											chrArray = new char[] { ',' };
											strArrays1 = str2.Split(chrArray);
											for (i = 0; i < (int)strArrays1.Length; i++)
											{
												deptId = strArrays1[i];
												if (!(deptId == ""))
												{
													defaultDeptById = DepartmentService.GetModel(deptId);
													if (!string.IsNullOrEmpty(defaultDeptById.PicPositionId))
													{
														foreach (DeptEmployee deptEmployeeByPositionId3 in DeptEmployeeService.GetDeptEmployeeByPositionId(defaultDeptById.PicPositionId))
														{
															if (!deptEmployees1.Contains(deptEmployeeByPositionId3))
															{
																deptEmployeeByPositionId3.DealType = str5;
																deptEmployees1.Add(deptEmployeeByPositionId3);
															}
														}
													}
												}
											}
										}
									}
								}
								break;
							}
							case "35":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									string str14 = strArrays3[2];
									chrArray = new char[] { ';' };
									strArrays = str14.Split(chrArray);
									str3 = strArrays[0];
									queryListSQL = EIS.WorkFlow.Engine.Utility.GetQueryListSQL(str3, strArrays3[2], appData);
									queryListSQL = driverEngine.ReplaceWithDataRow(queryListSQL, ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", queryListSQL };
									retInfo.Add(string.Concat(strArrays2));
									foreach (DataRow dataRow1 in SysDatabase.ExecuteTable(queryListSQL).Rows)
									{
										str2 = dataRow1[0].ToString();
										if (str2.Length > 0)
										{
											chrArray = new char[] { ',' };
											strArrays1 = str2.Split(chrArray);
											for (i = 0; i < (int)strArrays1.Length; i++)
											{
												deptId = strArrays1[i];
												if (!(deptId == ""))
												{
													defaultDeptById = DepartmentService.GetModel(deptId);
													if (!string.IsNullOrEmpty(defaultDeptById.UpPositionId))
													{
														deptEmployees = DeptEmployeeService.GetDeptEmployeeByPositionId(defaultDeptById.UpPositionId);
														foreach (DeptEmployee deptEmployee9 in deptEmployees)
														{
															if (!deptEmployees1.Contains(deptEmployee9))
															{
																deptEmployee9.DealType = str5;
																deptEmployees1.Add(deptEmployee9);
															}
														}
													}
													if (!string.IsNullOrEmpty(defaultDeptById.UpEmployeeId))
													{
														string upEmployeeId2 = defaultDeptById.UpEmployeeId;
														chrArray = new char[] { ',' };
														string[] strArrays5 = upEmployeeId2.Split(chrArray);
														for (int j = 0; j < (int)strArrays5.Length; j++)
														{
															employeeID = strArrays5[j];
															deptEmployeeByPositionId = DeptEmployeeService.GetOrignalDeptEmployee(employeeID);
															if (deptEmployeeByPositionId != null)
															{
																if (!deptEmployees1.Contains(deptEmployeeByPositionId))
																{
																	deptEmployeeByPositionId.DealType = str5;
																	deptEmployees1.Add(deptEmployeeByPositionId);
																}
															}
														}
													}
												}
											}
										}
									}
								}
								break;
							}
							default:
							{
								goto Label1;
							}
						}
					}
					else
					{
						goto Label1;
					}
				}
				StringCollection stringCollections = new StringCollection();
				for (i = deptEmployees1.Count - 1; i > -1; i--)
				{
					employeeID = deptEmployees1[i].EmployeeID;
					if (stringCollections.Contains(employeeID))
					{
						deptEmployees1.RemoveAt(i);
					}
					else if (!EmployeeService.CheckedValid(employeeID))
					{
						deptEmployees1.RemoveAt(i);
					}
					else
					{
						stringCollections.Add(employeeID);
					}
				}
			}
			return deptEmployees1;
		}

		private static List<DeptEmployee> GetPerformers(Instance ins, StringCollection ps, WFSession context, DbTransaction dbTran, ref StringCollection retInfo)
		{
			string[] strArrays;
			string[] strArrays1;
			int i;
			string employeeID;
			DeptEmployee deptEmployeeByPositionId;
			string str;
			List<DeptEmployee> deptEmployees;
			string str1;
			string str2;
			DeptEmployee deptEmployee = null;
			Department defaultDeptById;
			string str3;
			string str4;
			string str5;
			string queryListSQL;
			DataRow row = null;
			string[] strArrays2;
			List<DeptEmployee> deptEmployees1 = new List<DeptEmployee>();
			TaskPerformer2 taskPerformer2 = new TaskPerformer2()
			{
				uc = context.UserInfo,
				ins = ins
			};
			if (ps != null)
			{
				DriverEngine driverEngine = new DriverEngine(context.UserInfo);
				DataRow appData = context.AppData;
			Label1:
				foreach (string p in ps)
				{
					char[] chrArray = new char[] { '|' };
					string[] strArrays3 = p.Split(chrArray);
					if ((int)strArrays3.Length >= 4)
					{
						string str6 = EIS.AppBase.Utility.Xml2String(strArrays3[3]);
						if (str6.Trim().Length > 0)
						{
							str6 = driverEngine.ReplaceWithDataRow(str6, ins.AppName, context.AppData);
							EIS.WorkFlow.Engine.Utility.fileLogger.Debug<string, string>("InstanceId={0},处理人条件condExpr={1}", ins.InstanceId, str6);
							try
							{
								if (int.Parse(SysDatabase.ExecuteScalar(string.Concat("select count(*) where ", str6)).ToString()) == 0)
								{
									continue;
								}
							}
							catch (Exception exception1)
							{
								Exception exception = exception1;
								EIS.WorkFlow.Engine.Utility.fileLogger.Error<Exception>(exception);
								throw new Exception(string.Concat("验证处理人条件条件时出错：条件表达式=", str6), exception);
							}
						}
					}
					string str7 = "1";
					if ((int)strArrays3.Length >= 5)
					{
						str7 = (strArrays3[4] == "" ? "1" : strArrays3[4]);
					}
					string str8 = strArrays3[0];
					if (str8 != null)
					{
						switch (str8)
						{
							case "01":
							{
								if (strArrays3[2].Length > 0)
								{
									retInfo.Add(p);
									string str9 = strArrays3[2];
									chrArray = new char[] { '=' };
									strArrays = str9.Split(chrArray);
									if ((int)strArrays.Length != 1)
									{
										string str10 = strArrays[0];
										chrArray = new char[] { ',' };
										strArrays1 = str10.Split(chrArray);
										string str11 = strArrays[1];
										chrArray = new char[] { ',' };
										string[] strArrays4 = str11.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											employeeID = strArrays1[i];
											str = strArrays4[i];
											deptEmployeeByPositionId = DeptEmployeeService.GetDeptEmployeeByPositionId(employeeID, str);
											if (!(deptEmployeeByPositionId == null ? true : !EmployeeService.CheckedValid(employeeID)))
											{
												if (!deptEmployees1.Contains(deptEmployeeByPositionId))
												{
													deptEmployeeByPositionId.DealType = str7;
													deptEmployees1.Add(deptEmployeeByPositionId);
												}
											}
											else if (!EmployeeService.CheckedValid(employeeID))
											{
												deptEmployees = DeptEmployeeService.GetDeptEmployeeByPositionId(str);
												if (deptEmployees.Count == 1)
												{
													if (!deptEmployees1.Contains(deptEmployees[0]))
													{
														deptEmployees[0].DealType = str7;
														deptEmployees1.Add(deptEmployees[0]);
													}
												}
											}
											else
											{
												deptEmployeeByPositionId = DeptEmployeeService.GetOrignalDeptEmployee(employeeID);
												if (deptEmployeeByPositionId != null)
												{
													if (!deptEmployees1.Contains(deptEmployeeByPositionId))
													{
														deptEmployeeByPositionId.DealType = str7;
														deptEmployees1.Add(deptEmployeeByPositionId);
													}
												}
											}
										}
									}
									else
									{
										string str12 = strArrays[0];
										chrArray = new char[] { ',' };
										strArrays1 = str12.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											employeeID = strArrays1[i];
											deptEmployeeByPositionId = DeptEmployeeService.GetOrignalDeptEmployee(employeeID);
											if (deptEmployeeByPositionId != null)
											{
												if (!deptEmployees1.Contains(deptEmployeeByPositionId))
												{
													deptEmployeeByPositionId.DealType = str7;
													deptEmployees1.Add(deptEmployeeByPositionId);
												}
											}
										}
									}
								}
								break;
							}
							case "02":
							{
								if (strArrays3[2].Length > 0)
								{
									retInfo.Add(p);
									foreach (DeptEmployee deptEmployeeByPositionIdA in taskPerformer2.FromPositions(p))
									{
                                        if (!deptEmployees1.Contains(deptEmployeeByPositionIdA))
										{
                                            deptEmployeeByPositionIdA.DealType = str7;
                                            deptEmployees1.Add(deptEmployeeByPositionIdA);
										}
									}
								}
								break;
							}
							case "03":
							{
								if (strArrays3[2].Length > 0)
								{
									retInfo.Add(p);
									foreach (DeptEmployee deptEmployee1 in taskPerformer2.FromRoles(p))
									{
										if (!deptEmployees1.Contains(deptEmployee1))
										{
											deptEmployee1.DealType = str7;
											deptEmployees1.Add(deptEmployee1);
										}
									}
								}
								break;
							}
							case "04":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									str1 = driverEngine.ReplaceWithDataRow(strArrays3[2], ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", str1 };
									retInfo.Add(string.Concat(strArrays2));
									if (str1.Length > 0)
									{
										chrArray = new char[] { ',' };
										strArrays1 = str1.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											employeeID = strArrays1[i];
											deptEmployeeByPositionId = DeptEmployeeService.GetOrignalDeptEmployee(employeeID);
											if (deptEmployeeByPositionId != null)
											{
												if (!deptEmployees1.Contains(deptEmployeeByPositionId))
												{
													deptEmployeeByPositionId.DealType = str7;
													deptEmployees1.Add(deptEmployeeByPositionId);
												}
											}
										}
									}
								}
								break;
							}
							case "05":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									str2 = driverEngine.ReplaceWithDataRow(strArrays3[2], ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", str2 };
									retInfo.Add(string.Concat(strArrays2));
									if (str2.Length > 0)
									{
										chrArray = new char[] { ',' };
										strArrays1 = str2.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											str = strArrays1[i];
											deptEmployees = DeptEmployeeService.GetDeptEmployeeByPositionId(str);
											foreach (DeptEmployee deptEmployeeA in deptEmployees)
											{
                                                if (!deptEmployees1.Contains(deptEmployeeA))
												{
                                                    deptEmployeeA.DealType = str7;
                                                    deptEmployees1.Add(deptEmployeeA);
												}
											}
										}
									}
								}
								break;
							}
							case "06":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									str1 = driverEngine.ReplaceWithDataRow(strArrays3[2], ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", str1 };
									retInfo.Add(string.Concat(strArrays2));
									if (str1.Length > 0)
									{
										chrArray = new char[] { ',' };
										strArrays1 = str1.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											employeeID = strArrays1[i];
											defaultDeptById = EmployeeService.GetDefaultDeptById(employeeID);
											if ((defaultDeptById == null ? false : !string.IsNullOrEmpty(defaultDeptById.PicPositionId)))
											{
												foreach (DeptEmployee deptEmployeeByPositionId1 in DeptEmployeeService.GetDeptEmployeeByPositionId(defaultDeptById.PicPositionId))
												{
													if (!deptEmployees1.Contains(deptEmployeeByPositionId1))
													{
														deptEmployeeByPositionId1.DealType = str7;
														deptEmployees1.Add(deptEmployeeByPositionId1);
													}
												}
											}
										}
									}
								}
								break;
							}
							case "07":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									str2 = driverEngine.ReplaceWithDataRow(strArrays3[2], ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", str2 };
									retInfo.Add(string.Concat(strArrays2));
									if (str2.Length > 0)
									{
										chrArray = new char[] { ',' };
										strArrays1 = str2.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											str = strArrays1[i];
											Position model = (new _Position()).GetModel(str);
											if (model != null)
											{
												if (!string.IsNullOrEmpty(model.ParentPositionId))
												{
													deptEmployees = DeptEmployeeService.GetDeptEmployeeByPositionId(model.ParentPositionId);
													foreach (DeptEmployee deptEmployee2 in deptEmployees)
													{
														if (!deptEmployees1.Contains(deptEmployee2))
														{
															deptEmployee2.DealType = str7;
															deptEmployees1.Add(deptEmployee2);
														}
													}
												}
											}
										}
									}
								}
								break;
							}
							case "08":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									str3 = driverEngine.ReplaceWithDataRow(strArrays3[2], ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", str3 };
									retInfo.Add(string.Concat(strArrays2));
									if (str3.Length > 0)
									{
										EIS.WorkFlow.Engine.Utility.fileLogger.Trace("特定部门[表单指定]的负责人,{0}", str3);
										chrArray = new char[] { ',' };
										strArrays1 = str3.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											str4 = strArrays1[i];
											defaultDeptById = DepartmentService.GetModel(str4);
											if ((defaultDeptById == null ? false : !string.IsNullOrEmpty(defaultDeptById.PicPositionId)))
											{
												foreach (DeptEmployee deptEmployeeByPositionId2 in DeptEmployeeService.GetDeptEmployeeByPositionId(defaultDeptById.PicPositionId))
												{
													if (!deptEmployees1.Contains(deptEmployeeByPositionId2))
													{
														deptEmployeeByPositionId2.DealType = str7;
														deptEmployees1.Add(deptEmployeeByPositionId2);
													}
												}
											}
										}
									}
								}
								break;
							}
							case "09":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									str3 = driverEngine.ReplaceWithDataRow(strArrays3[2], ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", str3 };
									retInfo.Add(string.Concat(strArrays2));
									if (str3.Length > 0)
									{
										chrArray = new char[] { ',' };
										strArrays1 = str3.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											str4 = strArrays1[i];
											defaultDeptById = DepartmentService.GetModel(str4);
											if ((defaultDeptById == null ? false : !string.IsNullOrEmpty(defaultDeptById.UpPositionId)))
											{
												deptEmployees = DeptEmployeeService.GetDeptEmployeeByPositionId(defaultDeptById.UpPositionId);
												foreach (DeptEmployee deptEmployee3 in deptEmployees)
												{
													if (!deptEmployees1.Contains(deptEmployee3))
													{
														deptEmployee3.DealType = str7;
														deptEmployees1.Add(deptEmployee3);
													}
												}
											}
										}
									}
								}
								break;
							}
							case "0A":
							{
								if (strArrays3[2].Length > 0)
								{
									str4 = strArrays3[2];
									defaultDeptById = DepartmentService.GetModel(str4);
									if (!string.IsNullOrEmpty(defaultDeptById.UpPositionId))
									{
										deptEmployees = DeptEmployeeService.GetDeptEmployeeByPositionId(defaultDeptById.UpPositionId);
										foreach (DeptEmployee deptEmployee4 in deptEmployees)
										{
											if (!deptEmployees1.Contains(deptEmployee4))
											{
												deptEmployee4.DealType = str7;
												deptEmployees1.Add(deptEmployee4);
											}
										}
									}
									if (!string.IsNullOrEmpty(defaultDeptById.UpEmployeeId))
									{
										string upEmployeeId = defaultDeptById.UpEmployeeId;
										chrArray = new char[] { ',' };
										strArrays1 = upEmployeeId.Split(chrArray);
										for (i = 0; i < (int)strArrays1.Length; i++)
										{
											employeeID = strArrays1[i];
											deptEmployeeByPositionId = DeptEmployeeService.GetOrignalDeptEmployee(employeeID);
											if (deptEmployeeByPositionId != null)
											{
												if (!deptEmployees1.Contains(deptEmployeeByPositionId))
												{
													deptEmployeeByPositionId.DealType = str7;
													deptEmployees1.Add(deptEmployeeByPositionId);
												}
											}
										}
									}
								}
								break;
							}
							case "11":
							{
								DeptEmployee initiator = taskPerformer2.GetInitiator(dbTran);
								if ((initiator == null ? false : !deptEmployees1.Contains(initiator)))
								{
									deptEmployees1.Add(taskPerformer2.GetInitiator(dbTran));
								}
								break;
							}
							case "12":
							{
								retInfo.Add(p);
								foreach (DeptEmployee startDeptLeader in taskPerformer2.GetStartDeptLeader(p))
								{
									if (!deptEmployees1.Contains(startDeptLeader))
									{
										startDeptLeader.DealType = str7;
										deptEmployees1.Add(startDeptLeader);
									}
								}
								break;
							}
							case "13":
							{
								retInfo.Add(p);
								foreach (DeptEmployee upLevelDeptLeader in taskPerformer2.GetUpLevelDeptLeader(p, dbTran))
								{
									if (!deptEmployees1.Contains(upLevelDeptLeader))
									{
										upLevelDeptLeader.DealType = str7;
										deptEmployees1.Add(upLevelDeptLeader);
									}
								}
								break;
							}
							case "14":
							{
								retInfo.Add(p);
								foreach (DeptEmployee topLevelDeptLeader in taskPerformer2.GetTopLevelDeptLeader(p, dbTran))
								{
									if (!deptEmployees1.Contains(topLevelDeptLeader))
									{
										topLevelDeptLeader.DealType = str7;
										deptEmployees1.Add(topLevelDeptLeader);
									}
								}
								break;
							}
							case "31":
							{
								if (strArrays3[2].Length > 0)
								{
									retInfo.Add(p);
									foreach (DeptEmployee employeeByPositionProp in taskPerformer2.GetEmployeeByPositionProp(p))
									{
										if (!deptEmployees1.Contains(employeeByPositionProp))
										{
											employeeByPositionProp.DealType = str7;
											deptEmployees1.Add(employeeByPositionProp);
										}
									}
								}
								break;
							}
							case "32":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									string str13 = strArrays3[2];
									chrArray = new char[] { ';' };
									strArrays = str13.Split(chrArray);
									str5 = strArrays[0];
									queryListSQL = EIS.WorkFlow.Engine.Utility.GetQueryListSQL(str5, strArrays3[2], appData);
									queryListSQL = driverEngine.ReplaceWithDataRow(queryListSQL, ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", queryListSQL };
									retInfo.Add(string.Concat(strArrays2));
									foreach (DataRow dataRow in SysDatabase.ExecuteTable(queryListSQL).Rows)
									{
										str1 = dataRow[0].ToString();
										if (str1.Length > 0)
										{
											chrArray = new char[] { ',' };
											strArrays1 = str1.Split(chrArray);
											for (i = 0; i < (int)strArrays1.Length; i++)
											{
												employeeID = strArrays1[i];
												if (!(employeeID == ""))
												{
													deptEmployeeByPositionId = DeptEmployeeService.GetOrignalDeptEmployee(employeeID);
													if (deptEmployeeByPositionId != null)
													{
														if (!deptEmployees1.Contains(deptEmployeeByPositionId))
														{
															deptEmployeeByPositionId.DealType = str7;
															deptEmployees1.Add(deptEmployeeByPositionId);
														}
													}
												}
											}
										}
									}
								}
								break;
							}
							case "33":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									string str14 = strArrays3[2];
									chrArray = new char[] { ';' };
									strArrays = str14.Split(chrArray);
									str5 = strArrays[0];
									queryListSQL = EIS.WorkFlow.Engine.Utility.GetQueryListSQL(str5, strArrays3[2], appData);
									queryListSQL = driverEngine.ReplaceWithDataRow(queryListSQL, ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", queryListSQL };
									retInfo.Add(string.Concat(strArrays2));
									foreach (DataRow row1 in SysDatabase.ExecuteTable(queryListSQL).Rows)
									{
										str2 = row1[0].ToString();
										if (str2.Length > 0)
										{
											chrArray = new char[] { ',' };
											strArrays1 = str2.Split(chrArray);
											for (i = 0; i < (int)strArrays1.Length; i++)
											{
												str = strArrays1[i];
												if (!(str == ""))
												{
													deptEmployees = DeptEmployeeService.GetDeptEmployeeByPositionId(str);
													foreach (DeptEmployee deptEmployee5 in deptEmployees)
													{
														if (!deptEmployees1.Contains(deptEmployee5))
														{
															deptEmployee5.DealType = str7;
															deptEmployees1.Add(deptEmployee5);
														}
													}
												}
											}
										}
									}
								}
								break;
							}
							case "34":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									string str15 = strArrays3[2];
									chrArray = new char[] { ';' };
									strArrays = str15.Split(chrArray);
									str5 = strArrays[0];
									queryListSQL = EIS.WorkFlow.Engine.Utility.GetQueryListSQL(str5, strArrays3[2], appData);
									queryListSQL = driverEngine.ReplaceWithDataRow(queryListSQL, ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", queryListSQL };
									retInfo.Add(string.Concat(strArrays2));
									foreach (DataRow rowB in SysDatabase.ExecuteTable(queryListSQL).Rows)
									{
                                        str3 = rowB[0].ToString();
										if (str3.Length > 0)
										{
											EIS.WorkFlow.Engine.Utility.fileLogger.Trace("特定部门[SQL指定]的负责人,{0}", str3);
											chrArray = new char[] { ',' };
											strArrays1 = str3.Split(chrArray);
											for (i = 0; i < (int)strArrays1.Length; i++)
											{
												str4 = strArrays1[i];
												if (!(str4 == ""))
												{
													defaultDeptById = DepartmentService.GetModel(str4);
													if (!string.IsNullOrEmpty(defaultDeptById.PicPositionId))
													{
														foreach (DeptEmployee deptEmployeeByPositionId3 in DeptEmployeeService.GetDeptEmployeeByPositionId(defaultDeptById.PicPositionId))
														{
															if (!deptEmployees1.Contains(deptEmployeeByPositionId3))
															{
																deptEmployeeByPositionId3.DealType = str7;
																deptEmployees1.Add(deptEmployeeByPositionId3);
															}
														}
													}
												}
											}
										}
									}
								}
								break;
							}
							case "35":
							{
								if ((strArrays3[2].Length <= 0 ? false : appData != null))
								{
									string str16 = strArrays3[2];
									chrArray = new char[] { ';' };
									strArrays = str16.Split(chrArray);
									str5 = strArrays[0];
									queryListSQL = EIS.WorkFlow.Engine.Utility.GetQueryListSQL(str5, strArrays3[2], appData);
									queryListSQL = driverEngine.ReplaceWithDataRow(queryListSQL, ins.AppName, appData);
									strArrays2 = new string[] { strArrays3[0], "|", strArrays3[1], "|", queryListSQL };
									retInfo.Add(string.Concat(strArrays2));
									foreach (DataRow dataRow1 in SysDatabase.ExecuteTable(queryListSQL).Rows)
									{
										str3 = dataRow1[0].ToString();
										if (str3.Length > 0)
										{
											chrArray = new char[] { ',' };
											strArrays1 = str3.Split(chrArray);
											for (i = 0; i < (int)strArrays1.Length; i++)
											{
												str4 = strArrays1[i];
												if (!(str4 == ""))
												{
													defaultDeptById = DepartmentService.GetModel(str4);
													if (!string.IsNullOrEmpty(defaultDeptById.UpPositionId))
													{
														deptEmployees = DeptEmployeeService.GetDeptEmployeeByPositionId(defaultDeptById.UpPositionId);
														foreach (DeptEmployee deptEmployee6 in deptEmployees)
														{
															if (!deptEmployees1.Contains(deptEmployee6))
															{
																deptEmployee6.DealType = str7;
																deptEmployees1.Add(deptEmployee6);
															}
														}
													}
													if (!string.IsNullOrEmpty(defaultDeptById.UpEmployeeId))
													{
														string upEmployeeId1 = defaultDeptById.UpEmployeeId;
														chrArray = new char[] { ',' };
														string[] strArrays5 = upEmployeeId1.Split(chrArray);
														for (int j = 0; j < (int)strArrays5.Length; j++)
														{
															employeeID = strArrays5[j];
															deptEmployeeByPositionId = DeptEmployeeService.GetOrignalDeptEmployee(employeeID);
															if (deptEmployeeByPositionId != null)
															{
																if (!deptEmployees1.Contains(deptEmployeeByPositionId))
																{
																	deptEmployeeByPositionId.DealType = str7;
																	deptEmployees1.Add(deptEmployeeByPositionId);
																}
															}
														}
													}
												}
											}
										}
									}
								}
								break;
							}
							default:
							{
								goto Label1;
							}
						}
					}
					else
					{
						goto Label1;
					}
				}
				StringCollection stringCollections = new StringCollection();
				for (i = deptEmployees1.Count - 1; i > -1; i--)
				{
					employeeID = deptEmployees1[i].EmployeeID;
					if (stringCollections.Contains(employeeID))
					{
						deptEmployees1.RemoveAt(i);
					}
					else if (!EmployeeService.CheckedValid(employeeID))
					{
						deptEmployees1.RemoveAt(i);
					}
					else
					{
						stringCollections.Add(employeeID);
					}
				}
			}
			return deptEmployees1;
		}

		private static string GetQueryListSQL(string tblName, string paramList, DataRow bizData)
		{
			TableInfo model = (new _TableInfo(tblName)).GetModel();
			if (model == null)
			{
				throw new Exception(string.Concat("流程处理人，找不到查询定义：", tblName));
			}
			string listSQL = model.ListSQL;
			foreach (Match match in (new Regex("@\\w+", RegexOptions.None)).Matches(listSQL))
			{
				if (paramList.Length > 0)
				{
					char[] chrArray = new char[] { ';' };
					string[] strArrays = paramList.Split(chrArray);
					for (int i = 1; i < (int)strArrays.Length; i++)
					{
						if (strArrays[i].Length != 0)
						{
							string str = strArrays[i];
							chrArray = new char[] { '=' };
							if (str.Split(chrArray)[0] == match.Value)
							{
								string str1 = strArrays[i];
								chrArray = new char[] { '=' };
								if (!str1.Split(chrArray)[1].StartsWith("[!"))
								{
									string str2 = strArrays[i];
									chrArray = new char[] { '=' };
									string str3 = str2.Split(chrArray)[1];
									listSQL = (!bizData.Table.Columns.Contains(str3) ? listSQL.Replace(match.Value, str3) : listSQL.Replace(match.Value, string.Concat("{", str3, "}")));
								}
							}
						}
					}
				}
			}
			return listSQL;
		}

		private static string getSonText(XmlNode node, string sonName)
		{
			XmlNode xmlNodes = node.SelectSingleNode(sonName);
			return (xmlNodes == null ? "" : xmlNodes.InnerText);
		}

		public static UserTask GetUserTaskByTaskId(string taskId, string employeeId, DbTransaction dbTran)
		{
			UserTask model;
			string str = string.Format("select top 1 u._autoid from t_e_wf_usertask u where _isDel=0 and u.taskid ='{0}' and (ownerId='{1}' or AgentId='{1}')", taskId, employeeId);
			object obj = SysDatabase.ExecuteScalar(str, dbTran);
			if (obj == null)
			{
				model = null;
			}
			else
			{
				model = (new _UserTask(dbTran)).GetModel(obj.ToString());
			}
			return model;
		}

		public static UserTask GetUserTaskByTaskId(string taskId, string employeeId)
		{
			UserTask model;
			string str = string.Format("select top 1 u._autoid from t_e_wf_usertask u where _isDel=0 and u.taskid ='{0}' and (ownerId='{1}' or AgentId='{1}')", taskId, employeeId);
			object obj = SysDatabase.ExecuteScalar(str);
			if (obj == null)
			{
				model = null;
			}
			else
			{
				model = (new _UserTask()).GetModel(obj.ToString());
			}
			return model;
		}

		public static List<UserTask> GetUserTaskList(string employeeId)
		{
			return new List<UserTask>();
		}

		public static bool IsManualTask(Activity actObj)
		{
			bool flag;
			string lower = actObj.GetNodeType().ToString().ToLower();
			StringCollection stringCollections = new StringCollection();
			string[] strArrays = new string[] { "normal", "sign", "start", "end" };
			stringCollections.AddRange(strArrays);
			if (stringCollections.Contains(lower))
			{
				flag = (!(actObj.GetFinishMode().GetMode().GetType() == typeof(Automatic)) ? true : false);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public static bool IsVirtualNode(Activity actObj)
		{
			string lower = actObj.GetNodeType().ToString().ToLower();
			return ((lower == "and" ? false : !(lower == "xor")) ? false : true);
		}

		public static void RemoveInstance(string instanceId, UserContext userInfo)
		{
			DriverEngine driverEngine = new DriverEngine();
			Instance instanceById = driverEngine.GetInstanceById(instanceId);
			driverEngine.CurSession = new WFSession(userInfo, instanceById, EIS.WorkFlow.Engine.Utility.GetAppData(instanceById), null);
			DbConnection dbConnection = SysDatabase.CreateConnection();
			try
			{
				dbConnection.Open();
				DbTransaction dbTransaction = dbConnection.BeginTransaction();
				try
				{
					try
					{
						driverEngine.ExecEventScript(instanceById, "deletebefore", dbTransaction);
						driverEngine.ExecProgram(instanceById, "deletebefore", dbTransaction);
						driverEngine.UpdateAppDataState(instanceById, InstanceState.Ready, dbTransaction);
						(new _FinishTransition(dbTransaction)).DeleteByInstanceId(instanceId);
						(new _UserTask(dbTransaction)).DeleteByInstanceId(instanceId);
						(new _Task(dbTransaction)).DeleteByInstanceId(instanceId);
						(new _Instance(dbTransaction)).Delete(instanceId);
						driverEngine.ExecEventScript(instanceById, "deleteafter", dbTransaction);
						driverEngine.ExecProgram(instanceById, "deleteafter", dbTransaction);
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
			finally
			{
				if (dbConnection != null)
				{
					((IDisposable)dbConnection).Dispose();
				}
			}
		}

        public static void RemoveInstance(string instanceId)
        {
            DriverEngine driverEngine = new DriverEngine();
            Instance instanceById = driverEngine.GetInstanceById(instanceId);
            DbConnection dbConnection = SysDatabase.CreateConnection();
            try
            {
                dbConnection.Open();
                DbTransaction dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    try
                    {
                        driverEngine.UpdateAppDataState(instanceById, InstanceState.Ready, dbTransaction);
                        (new _FinishTransition(dbTransaction)).DeleteByInstanceId(instanceId);
                        (new _UserTask(dbTransaction)).DeleteByInstanceId(instanceId);
                        (new _Task(dbTransaction)).DeleteByInstanceId(instanceId);
                        (new _Instance(dbTransaction)).Delete(instanceId);
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
            finally
            {
                if (dbConnection != null)
                {
                    ((IDisposable)dbConnection).Dispose();
                }
            }
        }

		public static void RemoveInstanceOnStart(string instanceId, string employeeId, UserContext userInfo)
		{
			if (!(SysDatabase.ExecuteScalar(string.Format("select count(*) from T_E_WF_Task where InstanceId='{0}'", instanceId, employeeId)).ToString() == "1"))
			{
				throw new Exception("普通用户只能删除暂存任务，针对已发起任务请使用终止功能");
			}
			EIS.WorkFlow.Engine.Utility.RemoveInstance(instanceId, userInfo);
		}

		public static void SendAlert(Instance ins, Activity newAct, WFSession context, int msgType, DbTransaction dbTran)
		{
			EIS.WorkFlow.Engine.Utility.SendAlert(ins, newAct, null, context, msgType, dbTran);
		}

		public static void SendAlert(Instance ins, Activity newAct, Task sysTask, WFSession context, int msgType, DbTransaction dbTran)
		{
			string option;
			string title;
			string message;
			string[] strArrays;
			string[] strArrays1;
			DriverEngine driverEngine;
			Performers performers;
			List<DeptEmployee> deptEmployees;
			StringCollection employeeIdList;
			string str;
			string str1 = null;
			string mail;
			MailMessage mailMessage;
			string str2;
			TaskPerformer2 taskPerformer2;
			StringCollection stringCollections;
			string str3;
			ITaskAction taskAction;
			char[] chrArray;
			UserContext userInfo = context.UserInfo;
			bool flag = false;
			bool flag1 = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			if (msgType == 1)
			{
				AlertIn alertIn = newAct.GetAlertIn();
				option = alertIn.GetOption();
				title = alertIn.GetTitle();
				message = alertIn.GetMessage();
				chrArray = new char[] { '|' };
				strArrays = option.Split(chrArray);
				if ((int)strArrays.Length > 3)
				{
					flag = strArrays[0] == "1";
					string str4 = strArrays[1];
					chrArray = new char[] { ',' };
					string[] strArrays2 = str4.Split(chrArray);
					flag1 = strArrays2[0] == "1";
					flag2 = strArrays2[1] == "1";
					if (strArrays[2] == "1")
					{
						title = SysConfig.GetConfig("WF_ArriveTitle").ItemValue;
						message = SysConfig.GetConfig("WF_ArriveMsg").ItemValue;
					}
					string str5 = strArrays[3];
					chrArray = new char[] { ',' };
					strArrays1 = str5.Split(chrArray);
					flag3 = strArrays1[0] == "1";
					flag4 = strArrays1[1] == "1";
					flag5 = strArrays1[2] == "1";
				}
				List<DeptEmployee> activityUser = EIS.WorkFlow.Engine.Utility.GetActivityUser(ins, newAct, context, dbTran);
				if (flag)
				{
					driverEngine = new DriverEngine(userInfo);
					title = driverEngine.ReplaceWithDataRow(title, ins.AppName, context.AppData);
					title = driverEngine.ReplaceWithInstance(title, ins, sysTask);
					message = driverEngine.ReplaceWithDataRow(message, ins.AppName, context.AppData);
					message = driverEngine.ReplaceWithInstance(message, ins, sysTask);
					performers = alertIn.GetPerformers();
					deptEmployees = EIS.WorkFlow.Engine.Utility.GetPerformers(ins, newAct, performers, context, dbTran);
					if (flag1)
					{
						employeeIdList = EIS.WorkFlow.Engine.Utility.GetEmployeeIdList(activityUser);
						if (employeeIdList.Count > 0)
						{
							if (flag3)
							{
								str = string.Concat("SysFolder/Workflow/DealFlow.aspx?sysTaskId=", sysTask.TaskId);
								EIS.WorkFlow.Engine.Utility.SendMsg(ins, employeeIdList, title, message, str, userInfo, dbTran);
							}
							if (flag4)
							{
								message = EIS.WorkFlow.Engine.Utility.FormatMailHtml(message);
								str = string.Concat(AppSettings.GetRootURI(userInfo.WebId), "/SysFolder/Workflow/DealFlow.aspx?sysTaskId=", sysTask.TaskId);
								if (!(SysConfig.GetConfig("ReloginLinkInMail").ItemValue == "否"))
								{
									mailMessage = new MailMessage()
									{
										Subject = title,
										IsBodyHtml = true,
										Priority = MailPriority.Normal,
										Body = string.Concat(message, string.Format("<br/><a href='{0}' target='_blank'>点击处理任务</a>", str))
									};
									foreach (string str6 in employeeIdList)
									{
										mail = EmployeeService.GetMail(str6);
										if (mail != "")
										{
											mailMessage.To.Add(mail);
										}
									}
									EIS.WorkFlow.Engine.Utility.SendMail(mailMessage);
								}
								else
								{
									foreach (string str7 in employeeIdList)
									{
										mail = EmployeeService.GetMail(str7);
										if (mail != "")
										{
											mailMessage = new MailMessage();
											mailMessage.To.Add(mail);
											mailMessage.Subject = title;
											mailMessage.IsBodyHtml = true;
											mailMessage.Priority = MailPriority.Normal;
											str2 = AppSettings.GenAutoLoginUrl(EmployeeService.GetEmployeeAttrById(str7, "LoginName"), userInfo.WebId, str, 0);
											mailMessage.Body = string.Concat(message, string.Format("<br/><a href='{0}' target='_blank'>点击处理任务</a>", str2));
											EIS.WorkFlow.Engine.Utility.SendMail(mailMessage);
										}
									}
								}
							}
							if (flag5)
							{
								EIS.WorkFlow.Engine.Utility.SendSms(ins, employeeIdList, message, userInfo, dbTran);
							}
						}
					}
					if (flag2)
					{
						taskPerformer2 = new TaskPerformer2()
						{
							uc = userInfo,
							ins = ins
						};
						deptEmployees.Add(taskPerformer2.GetInitiator(dbTran));
					}
					if (deptEmployees.Count > 0)
					{
						stringCollections = EIS.WorkFlow.Engine.Utility.GetEmployeeIdList(deptEmployees);
						if (flag3)
						{
							str3 = string.Concat("SysFolder/AppFrame/AppWorkFlowInfo.aspx?InstanceId=", ins.InstanceId);
							EIS.WorkFlow.Engine.Utility.SendMsg(ins, stringCollections, title, message, str3, userInfo, dbTran);
						}
						if (flag4)
						{
							message = EIS.WorkFlow.Engine.Utility.FormatMailHtml(message);
							str3 = string.Concat(AppSettings.GetRootURI(userInfo.WebId), "/SysFolder/AppFrame/AppWorkFlowInfo.aspx?InstanceId=", ins.InstanceId);
							if (!(SysConfig.GetConfig("ReloginLinkInMail").ItemValue == "否"))
							{
								mailMessage = new MailMessage()
								{
									Subject = title,
									IsBodyHtml = true,
									Priority = MailPriority.Normal,
									Body = string.Concat(message, string.Format("<br/><a href='{0}' target='_blank'>点击查看任务</a>", str3))
								};
								foreach (string str8 in stringCollections)
								{
									mail = EmployeeService.GetMail(str8);
									if (mail != "")
									{
										mailMessage.To.Add(mail);
									}
								}
								EIS.WorkFlow.Engine.Utility.SendMail(mailMessage);
							}
							else
							{
								foreach (string str9 in stringCollections)
								{
									mail = EmployeeService.GetMail(str9);
									if (mail != "")
									{
										mailMessage = new MailMessage();
										mailMessage.To.Add(mail);
										mailMessage.Subject = title;
										mailMessage.IsBodyHtml = true;
										mailMessage.Priority = MailPriority.Normal;
										str2 = AppSettings.GenAutoLoginUrl(EmployeeService.GetEmployeeAttrById(str9, "LoginName"), userInfo.WebId, str3, 0);
										mailMessage.Body = string.Concat(message, string.Format("<br/><a href='{0}' target='_blank'>点击查看任务</a>", str2));
										EIS.WorkFlow.Engine.Utility.SendMail(mailMessage);
									}
								}
							}
						}
						if (flag5)
						{
							EIS.WorkFlow.Engine.Utility.SendSms(ins, stringCollections, message, userInfo, dbTran);
						}
					}
				}
				string itemValue = SysConfig.GetConfig("Basic_CompanyCode").ItemValue;
				if ((!ins.Deadline.HasValue ? false : itemValue == "BDRMYY"))
				{
					if (DateTime.Today.AddDays(1).CompareTo(ins.Deadline.Value.Date) > 0)
					{
						employeeIdList = EIS.WorkFlow.Engine.Utility.GetEmployeeIdList(activityUser);
						string instanceName = ins.InstanceName;
						DateTime? deadline = ins.Deadline;
						message = string.Format("督办任务提醒：在HRP系统中有一项任务需要您及时处理，标题为：{0},督办期限：{1:yyyy年MM月dd日}", instanceName, deadline.Value);
						EIS.WorkFlow.Engine.Utility.SendSms(ins, employeeIdList, message, userInfo, dbTran);
					}
				}
				try
				{
					taskAction = WFListener.Unity_WorkflowContainer.Resolve<ITaskAction>();
					taskAction.Task_Arrive(ins, newAct, sysTask, context.AppData, context.UserInfo, activityUser, dbTran);
				}
				catch (ResolutionFailedException resolutionFailedException)
				{
					EIS.WorkFlow.Engine.Utility.fileLogger.Debug("依赖注入异常，未找到接口ITaskAction映射的实现类[Task_Arrive]");
				}
				catch (Exception exception)
				{
					EIS.WorkFlow.Engine.Utility.fileLogger.Error<Exception>(exception);
				}
			}
			else if (msgType == 2)
			{
				AlertOut alertOut = newAct.GetAlertOut();
				option = alertOut.GetOption();
				title = alertOut.GetTitle();
				message = alertOut.GetMessage();
				chrArray = new char[] { '|' };
				strArrays = option.Split(chrArray);
				if ((int)strArrays.Length > 3)
				{
					flag = strArrays[0] == "1";
					flag2 = strArrays[1] == "1";
					if (strArrays[2] == "1")
					{
						title = SysConfig.GetConfig("WF_SubmitTitle").ItemValue;
						message = SysConfig.GetConfig("WF_SubmitMsg").ItemValue;
					}
					string str10 = strArrays[3];
					chrArray = new char[] { ',' };
					strArrays1 = str10.Split(chrArray);
					flag3 = strArrays1[0] == "1";
					flag4 = strArrays1[1] == "1";
					flag5 = strArrays1[2] == "1";
				}
				if (flag)
				{
					driverEngine = new DriverEngine(userInfo);
					title = driverEngine.ReplaceWithDataRow(title, ins.AppName, context.AppData);
					title = driverEngine.ReplaceWithInstance(title, ins, sysTask);
					message = driverEngine.ReplaceWithDataRow(message, ins.AppName, context.AppData);
					message = driverEngine.ReplaceWithInstance(message, ins, sysTask);
					performers = alertOut.GetPerformers();
					deptEmployees = EIS.WorkFlow.Engine.Utility.GetPerformers(ins, newAct, performers, context, dbTran);
					if (flag2)
					{
						taskPerformer2 = new TaskPerformer2()
						{
							uc = userInfo,
							ins = ins
						};
						deptEmployees.Add(taskPerformer2.GetInitiator(dbTran));
					}
					if (deptEmployees.Count > 0)
					{
						stringCollections = EIS.WorkFlow.Engine.Utility.GetEmployeeIdList(deptEmployees);
						if (flag3)
						{
							str = string.Concat("SysFolder/AppFrame/AppWorkFlowInfo.aspx?InstanceId=", ins.InstanceId);
							EIS.WorkFlow.Engine.Utility.SendMsg(ins, stringCollections, title, message, str, userInfo, dbTran);
						}
						if (flag4)
						{
							message = EIS.WorkFlow.Engine.Utility.FormatMailHtml(message);
							str = string.Concat(AppSettings.GetRootURI(userInfo.WebId), "/SysFolder/AppFrame/AppWorkFlowInfo.aspx?InstanceId=", ins.InstanceId);
							if (!(SysConfig.GetConfig("ReloginLinkInMail").ItemValue == "否"))
							{
								mailMessage = new MailMessage()
								{
									Subject = title,
									IsBodyHtml = true,
									Priority = MailPriority.Normal,
									Body = string.Concat(message, string.Format("<br/><a href='{0}' target='_blank'>点击处理任务</a>", str))
								};
								foreach (string str11 in stringCollections)
								{
									mail = EmployeeService.GetMail(str11);
									if (mail != "")
									{
										mailMessage.To.Add(mail);
									}
								}
								EIS.WorkFlow.Engine.Utility.SendMail(mailMessage);
							}
							else
							{
								foreach (string str12 in stringCollections)
								{
									mail = EmployeeService.GetMail(str12);
									if (mail != "")
									{
										mailMessage = new MailMessage();
										mailMessage.To.Add(mail);
										mailMessage.Subject = title;
										mailMessage.IsBodyHtml = true;
										mailMessage.Priority = MailPriority.Normal;
										EIS.WorkFlow.Engine.Utility.fileLogger.Debug("SendAlert user.WebId={0}", userInfo.WebId);
										str2 = AppSettings.GenAutoLoginUrl(EmployeeService.GetEmployeeAttrById(str12, "LoginName"), userInfo.WebId, str, 0);
										mailMessage.Body = string.Concat(message, string.Format("<br/><a href='{0}' target='_blank'>点击查看任务</a>", str2));
										EIS.WorkFlow.Engine.Utility.SendMail(mailMessage);
									}
								}
							}
						}
						if (flag5)
						{
							EIS.WorkFlow.Engine.Utility.SendSms(ins, stringCollections, message, userInfo, dbTran);
						}
					}
				}
				try
				{
					taskAction = WFListener.Unity_WorkflowContainer.Resolve<ITaskAction>();
					taskAction.Task_Submit(ins, newAct, sysTask, context.AppData, context.UserInfo, dbTran);
				}
				catch (ResolutionFailedException resolutionFailedException1)
				{
					EIS.WorkFlow.Engine.Utility.fileLogger.Debug("依赖注入异常，未找到接口ITaskAction映射的实现类[Task_Submit]");
				}
				catch (Exception exception1)
				{
					EIS.WorkFlow.Engine.Utility.fileLogger.Error<Exception>(exception1);
				}
			}
			else if (msgType == 3)
			{
				AlertBack alertBack = newAct.GetAlertBack();
				option = alertBack.GetOption();
				title = alertBack.GetTitle();
				message = alertBack.GetMessage();
				chrArray = new char[] { '|' };
				strArrays = option.Split(chrArray);
				if ((int)strArrays.Length > 3)
				{
					flag = strArrays[0] == "1";
					flag2 = strArrays[1] == "1";
					if (strArrays[2] == "1")
					{
						title = SysConfig.GetConfig("WF_BackTitle").ItemValue;
						message = SysConfig.GetConfig("WF_BackMsg").ItemValue;
					}
					string str13 = strArrays[3];
					chrArray = new char[] { ',' };
					strArrays1 = str13.Split(chrArray);
					flag3 = strArrays1[0] == "1";
					flag4 = strArrays1[1] == "1";
					flag5 = strArrays1[2] == "1";
				}
				if (flag)
				{
					driverEngine = new DriverEngine(userInfo);
					title = driverEngine.ReplaceWithDataRow(title, ins.AppName, context.AppData);
					title = driverEngine.ReplaceWithInstance(title, ins, sysTask);
					message = driverEngine.ReplaceWithDataRow(message, ins.AppName, context.AppData);
					message = driverEngine.ReplaceWithInstance(message, ins, sysTask);
					performers = alertBack.GetPerformers();
					deptEmployees = EIS.WorkFlow.Engine.Utility.GetPerformers(ins, newAct, performers, context, dbTran);
					if (flag2)
					{
						taskPerformer2 = new TaskPerformer2()
						{
							uc = userInfo,
							ins = ins
						};
						deptEmployees.Add(taskPerformer2.GetInitiator(dbTran));
					}
					if (deptEmployees.Count > 0)
					{
						stringCollections = EIS.WorkFlow.Engine.Utility.GetEmployeeIdList(deptEmployees);
						if (flag3)
						{
							str = string.Concat("SysFolder/AppFrame/AppWorkFlowInfo.aspx?InstanceId=", ins.InstanceId);
							EIS.WorkFlow.Engine.Utility.SendMsg(ins, stringCollections, title, message, str, userInfo, dbTran);
						}
						if (flag4)
						{
							message = EIS.WorkFlow.Engine.Utility.FormatMailHtml(message);
							str = string.Concat(AppSettings.GetRootURI(userInfo.WebId), "/SysFolder/AppFrame/AppWorkFlowInfo.aspx?InstanceId=", ins.InstanceId);
							if (!(SysConfig.GetConfig("ReloginLinkInMail").ItemValue == "否"))
							{
								mailMessage = new MailMessage()
								{
									Subject = title,
									IsBodyHtml = true,
									Priority = MailPriority.Normal,
									Body = string.Concat(message, string.Format("<br/><a href='{0}' target='_blank'>点击查看任务</a>", str))
								};
								foreach (string str1A in stringCollections)
								{
                                    mail = EmployeeService.GetMail(str1A);
									if (mail != "")
									{
										mailMessage.To.Add(mail);
									}
								}
								EIS.WorkFlow.Engine.Utility.SendMail(mailMessage);
							}
							else
							{
								foreach (string str14 in stringCollections)
								{
									mail = EmployeeService.GetMail(str14);
									if (mail != "")
									{
										mailMessage = new MailMessage();
										mailMessage.To.Add(mail);
										mailMessage.Subject = title;
										mailMessage.IsBodyHtml = true;
										mailMessage.Priority = MailPriority.Normal;
										str2 = AppSettings.GenAutoLoginUrl(EmployeeService.GetEmployeeAttrById(str14, "LoginName"), userInfo.WebId, str, 0);
										mailMessage.Body = string.Concat(message, string.Format("<br/><a href='{0}' target='_blank'>点击查看任务</a>", str2));
										EIS.WorkFlow.Engine.Utility.SendMail(mailMessage);
									}
								}
							}
						}
						if (flag5)
						{
							EIS.WorkFlow.Engine.Utility.SendSms(ins, stringCollections, message, userInfo, dbTran);
						}
					}
				}
				try
				{
					taskAction = WFListener.Unity_WorkflowContainer.Resolve<ITaskAction>();
					taskAction.Task_Rollback(ins, newAct, sysTask, context.AppData, context.UserInfo, dbTran);
				}
				catch (ResolutionFailedException resolutionFailedException2)
				{
					EIS.WorkFlow.Engine.Utility.fileLogger.Debug("依赖注入异常，未找到接口ITaskAction映射的实现类[Task_Rollback]");
				}
				catch (Exception exception2)
				{
					EIS.WorkFlow.Engine.Utility.fileLogger.Error<Exception>(exception2);
				}
			}
		}

		public static void SendAlert(Instance ins, WFSession context, int msgType, DbTransaction dbTran)
		{
			string str;
			string str1 = null;
			string mail;
			MailMessage mailMessage;
			UserContext userInfo = context.UserInfo;
			bool flag = false;
			bool flag1 = false;
			bool flag2 = true;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			if (msgType != 1)
			{
				if (msgType == 2)
				{
					string sonText = "";
					string sonText1 = "";
					string sonText2 = "";
					StringCollection stringCollections = new StringCollection();
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(ins.XPDL);
					XmlNode xmlNodes = xmlDocument.SelectSingleNode("/Package/Alert/AlertStop");
					if (xmlNodes != null)
					{
						sonText = EIS.WorkFlow.Engine.Utility.getSonText(xmlNodes, "Option");
						sonText1 = EIS.WorkFlow.Engine.Utility.getSonText(xmlNodes, "Title");
						sonText2 = EIS.WorkFlow.Engine.Utility.getSonText(xmlNodes, "Message");
						XmlNode xmlNodes1 = xmlNodes.SelectSingleNode("Performers");
						if (xmlNodes1 != null)
						{
							foreach (XmlNode xmlNodes2 in xmlNodes1.SelectNodes("Performer"))
							{
								stringCollections.Add(xmlNodes2.InnerText);
							}
						}
						char[] chrArray = new char[] { '|' };
						string[] strArrays = sonText.Split(chrArray);
						if ((int)strArrays.Length > 3)
						{
							flag = strArrays[0] == "1";
							flag1 = strArrays[1] == "1";
							flag2 = strArrays[2] == "1";
							string str2 = strArrays[3];
							chrArray = new char[] { ',' };
							string[] strArrays1 = str2.Split(chrArray);
							flag3 = strArrays1[0] == "1";
							flag4 = strArrays1[1] == "1";
							flag5 = strArrays1[2] == "1";
						}
						if (flag)
						{
							DriverEngine driverEngine = new DriverEngine(userInfo);
							sonText1 = driverEngine.ReplaceWithDataRow(sonText1, ins.AppName, context.AppData);
							sonText1 = driverEngine.ReplaceWithInstance(sonText1, ins, null);
							sonText2 = driverEngine.ReplaceWithDataRow(sonText2, ins.AppName, context.AppData);
							sonText2 = driverEngine.ReplaceWithInstance(sonText2, ins, null);
							StringCollection stringCollections1 = new StringCollection();
							List<DeptEmployee> performers = EIS.WorkFlow.Engine.Utility.GetPerformers(ins, stringCollections, context, dbTran, ref stringCollections1);
							if (flag1)
							{
								TaskPerformer2 taskPerformer2 = new TaskPerformer2()
								{
									uc = userInfo,
									ins = ins
								};
								performers.Add(taskPerformer2.GetInitiator(dbTran));
							}
							if (performers.Count > 0)
							{
								StringCollection employeeIdList = EIS.WorkFlow.Engine.Utility.GetEmployeeIdList(performers);
								if (flag3)
								{
									str = string.Concat("SysFolder/AppFrame/AppWorkFlowInfo.aspx?InstanceId=", ins.InstanceId);
									EIS.WorkFlow.Engine.Utility.SendMsg(ins, employeeIdList, sonText1, sonText2, str, userInfo, dbTran);
								}
								if (flag4)
								{
									str = string.Concat(AppSettings.GetRootURI(userInfo.WebId), "/SysFolder/AppFrame/AppWorkFlowInfo.aspx?InstanceId=", ins.InstanceId);
									if (!(SysConfig.GetConfig("ReloginLinkInMail").ItemValue == "否"))
									{
										mailMessage = new MailMessage()
										{
											Subject = sonText1,
											IsBodyHtml = true,
											Priority = MailPriority.Normal,
											Body = string.Concat(sonText2, string.Format("<br/><a href='{0}' target='_blank'>点击处理任务</a>", str))
										};
										foreach (string str1A in employeeIdList)
										{
                                            mail = EmployeeService.GetMail(str1A);
											if (mail != "")
											{
												mailMessage.To.Add(mail);
											}
										}
										EIS.WorkFlow.Engine.Utility.SendMail(mailMessage);
									}
									else
									{
										foreach (string str3 in employeeIdList)
										{
											mail = EmployeeService.GetMail(str3);
											if (mail != "")
											{
												mailMessage = new MailMessage();
												mailMessage.To.Add(mail);
												mailMessage.Subject = sonText1;
												mailMessage.IsBodyHtml = true;
												mailMessage.Priority = MailPriority.Normal;
												EIS.WorkFlow.Engine.Utility.fileLogger.Debug("SendAlert user.WebId={0}", userInfo.WebId);
												string str4 = AppSettings.GenAutoLoginUrl(EmployeeService.GetEmployeeAttrById(str3, "LoginName"), userInfo.WebId, str, 0);
												mailMessage.Body = string.Concat(sonText2, string.Format("<br/><a href='{0}' target='_blank'>点击查看任务</a>", str4));
												EIS.WorkFlow.Engine.Utility.SendMail(mailMessage);
											}
										}
									}
								}
								if (flag5)
								{
									EIS.WorkFlow.Engine.Utility.SendSms(ins, employeeIdList, sonText2, userInfo, dbTran);
								}
							}
						}
					}
					try
					{
						ITaskAction taskAction = WFListener.Unity_WorkflowContainer.Resolve<ITaskAction>();
						taskAction.Task_Stop(ins, context.AppData, context.UserInfo, dbTran);
					}
					catch (ResolutionFailedException resolutionFailedException)
					{
						EIS.WorkFlow.Engine.Utility.fileLogger.Debug("依赖注入异常，未找到接口ITaskAction映射的实现类[Task_Stop]");
					}
					catch (Exception exception)
					{
						EIS.WorkFlow.Engine.Utility.fileLogger.Error<Exception>(exception);
					}
				}
				else if (msgType == 3)
				{
				}
			}
		}

		public static void SendMail(MailMessage mail)
		{
			if (mail.To.Count != 0)
			{
				if (!(SysConfig.GetConfig("SysMail_Enable", false).ItemValue == "禁用"))
				{
					MailConfig mailConfig = AppSettings.Instance.MailConfig;
					if ((mailConfig.ServerIP == "" ? true : mailConfig.Account == ""))
					{
						throw new Exception("默认的外发信箱设置不正确！");
					}
					string[] body = new string[] { "<div style='line-height:1.8;'>", mail.Body, "<br/>", EIS.WorkFlow.Engine.Utility.FormatMailHtml(mailConfig.BodySubffix), "</div>" };
					mail.Body = string.Concat(body);
					if (!(mailConfig.NiCheng != ""))
					{
						mail.From = new MailAddress(mailConfig.Account);
					}
					else
					{
						mail.From = new MailAddress(mailConfig.Account, mailConfig.NiCheng);
					}
					SmtpClient smtpClient = new SmtpClient()
					{
						Host = mailConfig.ServerIP
					};
					int num = 25;
					int.TryParse(mailConfig.ServerPort, out num);
					smtpClient.Port = num;
					smtpClient.EnableSsl = mailConfig.EnableSSL == "是";
					smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
					smtpClient.UseDefaultCredentials = true;
					string account = mailConfig.Account;
					string passWord = mailConfig.PassWord;
					if (account != "")
					{
						smtpClient.Credentials = new NetworkCredential(account, passWord);
					}
					try
					{
						if (!(mailConfig.Async == "是"))
						{
							smtpClient.Send(mail);
						}
						else
						{
							smtpClient.SendCompleted += new SendCompletedEventHandler(EIS.WorkFlow.Engine.Utility.SendMail_Completed);
							smtpClient.SendAsync(mail, mail);
						}
					}
					catch (Exception exception)
					{
						EIS.WorkFlow.Engine.Utility.fileLogger.Error<Exception>(exception);
					}
				}
				else
				{
                    try
                    {
                        ITaskAction taskAction = WFListener.Unity_WorkflowContainer.Resolve<ITaskAction>();
                        taskAction.SendMail(mail);
                    }
                    catch {

                    }
                    finally { }
                    //EIS.WorkFlow.Engine.Utility.fileLogger.Warn("没有启用邮件发送服务");
                }
			}
		}

		private static void SendMail_Completed(object sender, AsyncCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				throw new Exception("用户手动取消了邮件发送");
			}
			if (e.Error != null)
			{
				MailMessage userState = (MailMessage)e.UserState;
				Logger logger = EIS.WorkFlow.Engine.Utility.fileLogger;
				object[] message = new object[] { e.Error.Message, userState.Subject, userState.Body, userState.To.ToString() };
				logger.Error("异步发送邮件时失败：{0}，\r\n收件人：{3}\r\n 邮件标题：{1}\r\n邮件内容：{2}", message);
			}
		}

		public static void SendMsg(Instance ins, IList ps, string msgTitle, string Content, string msgUrl, UserContext userInfo, DbTransaction dbTran)
		{
			if (ps.Count != 0)
			{
				AppMsg appMsg = new AppMsg(userInfo)
				{
					_AutoID = Guid.NewGuid().ToString(),
					Title = msgTitle,
					MsgType = "",
					MsgUrl = msgUrl,
					RecIds = EIS.AppBase.Utility.GetJoinString(ps),
					RecNames = EmployeeService.GetEmployeeNameList(ps),
					SendTime = new DateTime?(DateTime.Now),
					Sender = userInfo.EmployeeName,
					Content = Content
				};
				AppMsgService.SendMessage(appMsg, dbTran);
			}
		}

		public static void SendSms(Instance ins, IList ps, string Content, UserContext userInfo, DbTransaction dbTran)
		{
			if (ps.Count != 0)
			{
				_AppSms _AppSm = new _AppSms(dbTran);
				AppSms appSm = new AppSms()
				{
					_AutoID = Guid.NewGuid().ToString(),
					_UserName = userInfo.EmployeeId,
					_OrgCode = userInfo.DeptWbs,
					_CreateTime = DateTime.Now,
					_UpdateTime = DateTime.Now,
					_IsDel = 0,
					SenderId = userInfo.EmployeeId,
					SenderName = userInfo.EmployeeName,
					CompanyId = userInfo.CompanyId,
					AppId = ins.AppId,
					AppName = ins.AppName,
					Content = Content,
					State = "0",
					RecIds = EIS.AppBase.Utility.GetJoinString(ps),
					RecNames = EmployeeService.GetEmployeeNameList(ps),
					RecPhone = ""
				};
				_AppSm.Add(appSm);

                try
                {
                    ITaskAction taskAction = WFListener.Unity_WorkflowContainer.Resolve<ITaskAction>();
                    taskAction.SendSms(appSm, dbTran);
                }
                catch
                {

                }
                finally { }
			}
		}

		public static void StopInstance(string instanceId, string reason, UserContext userInfo)
		{
			DriverEngine driverEngine = new DriverEngine();
			Instance instanceById = driverEngine.GetInstanceById(instanceId);
			driverEngine.CurSession = new WFSession(userInfo, instanceById, EIS.WorkFlow.Engine.Utility.GetAppData(instanceById), null);
			if (instanceById.InstanceState != EnumDescription.GetFieldText(InstanceState.Processing))
			{
				throw new Exception(string.Concat("任务状态为【", instanceById.InstanceState, "】，不能终止"));
			}
			DbConnection dbConnection = SysDatabase.CreateConnection();
			try
			{
				dbConnection.Open();
				DbTransaction dbTransaction = dbConnection.BeginTransaction();
				try
				{
					try
					{
						driverEngine.StopInstance(instanceById, dbTransaction);
						_WorkflowLog __WorkflowLog = new _WorkflowLog(dbTransaction);
						WorkflowLog workflowLog = new WorkflowLog(userInfo)
						{
							EmpName = userInfo.EmployeeName,
							LogTime = new DateTime?(DateTime.Now),
							AppId = instanceId,
							AppName = "T_E_WF_Instance",
							LogContent = string.Format("终止任务：{0}；原因：{1}", instanceById.InstanceName, reason)
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
			finally
			{
				if (dbConnection != null)
				{
					((IDisposable)dbConnection).Dispose();
				}
			}
		}

		public static void UpdateInstanceUser(Instance ins, StringCollection sc, bool saveToDB)
		{
			string str = null;
			string[] strArrays;
			char[] chrArray;
			string[] strArrays1;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(ins.XPDL);
			StringCollection stringCollections = new StringCollection();
			foreach (string strA in sc)
			{
				chrArray = new char[] { '|' };
                strArrays = strA.Split(chrArray);
				if (strArrays[4] == "1")
				{
					string str1 = strArrays[0];
					chrArray = new char[] { '$' };
					stringCollections.AddRange(str1.Split(chrArray));
				}
			}
			foreach (string str2 in sc)
			{
				chrArray = new char[] { '|' };
				strArrays = str2.Split(chrArray);
				if (strArrays[2] == "1")
				{
					XmlNode xmlNodes = xmlDocument.SelectSingleNode(string.Concat("//Activity[@Id=\"", strArrays[1], "\"]"));
					XmlNode xmlNodes1 = xmlNodes.SelectSingleNode("Performers");
					xmlNodes1.InnerXml = "";
					XmlElement xmlElement = xmlDocument.CreateElement("Performer");
					strArrays[3] = strArrays[3].Trim(",".ToCharArray()).Replace(",,", "");
					string str3 = strArrays[3];
					chrArray = new char[] { ',' };
					string employeeNameList = EmployeeService.GetEmployeeNameList(str3.Split(chrArray));
					if ((int)strArrays.Length <= 4)
					{
						strArrays1 = new string[] { "01|", employeeNameList, "|", strArrays[3], "|" };
						xmlElement.InnerText = string.Concat(strArrays1);
					}
					else
					{
						strArrays1 = new string[] { "01|", employeeNameList, "|", strArrays[3], "=", strArrays[5], "|" };
						xmlElement.InnerText = string.Concat(strArrays1);
					}
					xmlNodes1.AppendChild(xmlElement);
				}
				if (strArrays[0].Length > 0)
				{
					string str4 = strArrays[0];
					chrArray = new char[] { '$' };
					string[] strArrays2 = str4.Split(chrArray);
					for (int i = 0; i < (int)strArrays2.Length; i++)
					{
						string str5 = strArrays2[i];
						if (!(str5.Trim() == ""))
						{
							string str6 = (stringCollections.Contains(str5) ? "1" : "0");
							XmlNode xmlNodes2 = xmlDocument.SelectSingleNode(string.Concat("//Transition[@Id=\"", str5, "\"]"));
							XmlNode xmlNodes3 = xmlNodes2.SelectSingleNode("ExtendedAttributes");
							XmlElement xmlElement1 = xmlNodes2.SelectSingleNode("ExtendedAttributes/ExtendedAttribute[@Name=\"State\"]") as XmlElement;
							if (xmlElement1 != null)
							{
								xmlElement1.SetAttribute("Value", str6);
							}
							else
							{
								xmlElement1 = xmlDocument.CreateElement("ExtendedAttribute");
								xmlElement1.SetAttribute("Name", "State");
								xmlElement1.SetAttribute("Value", str6);
								xmlNodes3.AppendChild(xmlElement1);
							}
						}
					}
				}
			}
			if (saveToDB)
			{
				_Instance __Instance = new _Instance();
				Instance model = __Instance.GetModel(ins.InstanceId);
				model.XPDL = xmlDocument.OuterXml;
				model._UpdateTime = DateTime.Now;
				__Instance.UpdateXPDL(model);
			}
			ins.XPDL = xmlDocument.OuterXml;
		}

		public static void UpdateTransitionState(Instance ins, StringCollection sc, bool saveToDB)
		{
			EIS.WorkFlow.Engine.Utility.UpdateTransitionState(ins, sc, saveToDB, null);
		}

		public static void UpdateTransitionState(Instance ins, StringCollection sc, bool saveToDB, DbTransaction dbTran)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(ins.XPDL);
			foreach (string str in sc)
			{
				string[] strArrays = str.Split(new char[] { '|' });
				if (strArrays[0].Length > 0)
				{
					XmlNode xmlNodes = xmlDocument.SelectSingleNode(string.Concat("//Transition[@Id=\"", strArrays[0], "\"]"));
					XmlNode xmlNodes1 = xmlNodes.SelectSingleNode("ExtendedAttributes");
					XmlElement xmlElement = xmlNodes.SelectSingleNode("ExtendedAttributes/ExtendedAttribute[@Name=\"State\"]") as XmlElement;
					if (xmlElement != null)
					{
						xmlElement.SetAttribute("Value", strArrays[1]);
					}
					else
					{
						xmlElement = xmlDocument.CreateElement("ExtendedAttribute");
						xmlElement.SetAttribute("Name", "State");
						xmlElement.SetAttribute("Value", strArrays[1]);
						xmlNodes1.AppendChild(xmlElement);
					}
				}
			}
			if (saveToDB)
			{
				_Instance __Instance = new _Instance(dbTran);
				Instance model = __Instance.GetModel(ins.InstanceId);
				model.XPDL = xmlDocument.OuterXml;
				model._UpdateTime = DateTime.Now;
				__Instance.UpdateXPDL(model);
			}
			ins.XPDL = xmlDocument.OuterXml;
		}
	}
}