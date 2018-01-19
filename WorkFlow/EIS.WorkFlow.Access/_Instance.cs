using EIS.AppBase;
using EIS.DataAccess;
using EIS.WorkFlow.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;

namespace EIS.WorkFlow.Access
{
	public class _Instance
	{
		private DbTransaction _tran = null;

		public _Instance()
		{
		}

		public _Instance(DbTransaction tran)
		{
			this._tran = tran;
		}

		public int Add(Instance model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_WF_Instance (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tEmployeeName,\r\n\t\t\t\t\tDeptId,\r\n\t\t\t\t\tDeptName,\r\n\t\t\t\t\tCompanyName,\r\n\t\t\t\t\tCompanyId,\r\n\t\t\t\t\tInstanceName,\r\n\t\t\t\t\tWorkflowId,\r\n\t\t\t\t\tFinishTime,\r\n\t\t\t\t\tRemark,\r\n\t\t\t\t\tProcessId,\r\n\t\t\t\t\tXPDL,\r\n                    XPDLPath,\r\n                    BasePath,\r\n\t\t\t\t\tAppName,\r\n\t\t\t\t\tAppId,\r\n\t\t\t\t\tInstanceState,\r\n                    Importance,\r\n                    Deadline\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@EmployeeName,\r\n\t\t\t\t\t@DeptId,\r\n\t\t\t\t\t@DeptName,\r\n\t\t\t\t\t@CompanyName,\r\n\t\t\t\t\t@CompanyId,\r\n\t\t\t\t\t@InstanceName,\r\n\t\t\t\t\t@WorkflowId,\r\n\t\t\t\t\t@FinishTime,\r\n\t\t\t\t\t@Remark,\r\n\t\t\t\t\t@ProcessId,\r\n\t\t\t\t\t@XPDL,\r\n                    @XPDLPath,\r\n                    @BasePath,\r\n\t\t\t\t\t@AppName,\r\n\t\t\t\t\t@AppId,\r\n\t\t\t\t\t@InstanceState,\r\n                    @Importance,\r\n                    @Deadline\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model.InstanceId);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "EmployeeName", DbType.String, model.EmployeeName);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptId", DbType.String, model.DeptId);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptName", DbType.String, model.DeptName);
			SysDatabase.AddInParameter(sqlStringCommand, "CompanyName", DbType.String, model.CompanyName);
			SysDatabase.AddInParameter(sqlStringCommand, "CompanyId", DbType.String, model.CompanyId);
			SysDatabase.AddInParameter(sqlStringCommand, "InstanceName", DbType.String, model.InstanceName);
			SysDatabase.AddInParameter(sqlStringCommand, "WorkflowId", DbType.String, model.WorkflowId);
			SysDatabase.AddInParameter(sqlStringCommand, "FinishTime", DbType.DateTime, model.FinishTime);
			SysDatabase.AddInParameter(sqlStringCommand, "Remark", DbType.String, model.Remark);
			SysDatabase.AddInParameter(sqlStringCommand, "ProcessId", DbType.String, model.ProcessId);
			if (!(model.XPDLPath == ""))
			{
				SysDatabase.AddInParameter(sqlStringCommand, "XPDL", DbType.String, "");
			}
			else
			{
				SysDatabase.AddInParameter(sqlStringCommand, "XPDL", DbType.String, model.XPDL);
			}
			SysDatabase.AddInParameter(sqlStringCommand, "XPDLPath", DbType.String, model.XPDLPath);
			SysDatabase.AddInParameter(sqlStringCommand, "BasePath", DbType.String, model.BasePath);
			SysDatabase.AddInParameter(sqlStringCommand, "AppName", DbType.String, model.AppName);
			SysDatabase.AddInParameter(sqlStringCommand, "AppId", DbType.String, model.AppId);
			SysDatabase.AddInParameter(sqlStringCommand, "InstanceState", DbType.String, model.InstanceState);
			SysDatabase.AddInParameter(sqlStringCommand, "Importance", DbType.String, model.Importance);
			SysDatabase.AddInParameter(sqlStringCommand, "Deadline", DbType.String, model.Deadline);
			int num = 0;
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			if ((num <= 0 ? false : model.XPDLPath.Length > 0))
			{
				this.InsertXPDL(model);
			}
			return num;
		}

		public int Delete(string key)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_WF_Instance ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, key);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		private string GetInstanceBasePath(string instanceId)
		{
			string str = string.Concat("select BasePath from T_E_WF_Instance where _autoId='", instanceId, "'");
			return SysDatabase.ExecuteScalar(str).ToString();
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_WF_Instance ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public Instance GetModel(string key)
		{
			Instance model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_WF_Instance ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, key);
			Instance instance = new Instance();
			DataTable dataTable = SysDatabase.ExecuteTable(sqlStringCommand);
			if (dataTable.Rows.Count <= 0)
			{
				model = null;
			}
			else
			{
				model = this.GetModel(dataTable.Rows[0]);
			}
			return model;
		}

		public Instance GetModel(DataRow dr)
		{
			Instance instance = new Instance()
			{
				InstanceId = dr["_AutoID"].ToString(),
				_UserName = dr["_UserName"].ToString(),
				_OrgCode = dr["_OrgCode"].ToString()
			};
			if (dr["_CreateTime"].ToString() != "")
			{
				instance._CreateTime = DateTime.Parse(dr["_CreateTime"].ToString());
			}
			if (dr["_UpdateTime"].ToString() != "")
			{
				instance._UpdateTime = DateTime.Parse(dr["_UpdateTime"].ToString());
			}
			if (dr["_IsDel"].ToString() != "")
			{
				instance._IsDel = int.Parse(dr["_IsDel"].ToString());
			}
			instance.EmployeeName = dr["EmployeeName"].ToString();
			instance.DeptId = dr["DeptId"].ToString();
			instance.DeptName = dr["DeptName"].ToString();
			instance.CompanyName = dr["CompanyName"].ToString();
			instance.CompanyId = dr["CompanyId"].ToString();
			instance.InstanceName = dr["InstanceName"].ToString();
			instance.WorkflowId = dr["WorkflowId"].ToString();
			if (dr["FinishTime"].ToString() != "")
			{
				instance.FinishTime = new DateTime?(DateTime.Parse(dr["FinishTime"].ToString()));
			}
			instance.Remark = dr["Remark"].ToString();
			instance.ProcessId = dr["ProcessId"].ToString();
			instance.XPDL = dr["XPDL"].ToString();
			instance.XPDLPath = dr["XPDLPath"].ToString();
			instance.BasePath = dr["BasePath"].ToString();
			instance.AppName = dr["AppName"].ToString();
			instance.AppId = dr["AppId"].ToString();
			instance.InstanceState = dr["InstanceState"].ToString();
			instance.Importance = dr["Importance"].ToString();
			if (dr["Deadline"].ToString() != "")
			{
				instance.Deadline = new DateTime?(DateTime.Parse(dr["Deadline"].ToString()));
			}
			return instance;
		}

		public List<Instance> GetModelList(string strWhere)
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			List<Instance> instances = new List<Instance>();
			if (!flag)
			{
				stringBuilder.Append("SELECT [_AutoID]\r\n                                ,[_UserName]\r\n                                ,[_OrgCode]\r\n                                ,[_CreateTime]\r\n                                ,[_UpdateTime]\r\n                                ,[_IsDel]\r\n                                ,[EmployeeName]\r\n                                ,[DeptName]\r\n                                ,[CompanyName]\r\n                                ,[CompanyId]\r\n                                ,[InstanceName]\r\n                                ,[WorkflowId]\r\n                                ,[FinishTime]\r\n                                ,[Remark]\r\n                                ,[ProcessId]\r\n                                ,'' [XPDL]\r\n                                ,[XPDLPath]\r\n                                ,[BasePath]\r\n                                ,[AppName]\r\n                                ,[AppId]\r\n                                ,[InstanceState]\r\n                                ,[Importance]\r\n                                ,[Deadline]\r\n                                ,[DeptId]\r\n                            FROM [dbo].[T_E_WF_Instance] ");
			}
			else
			{
				stringBuilder.Append("select *  FROM T_E_WF_Instance ");
			}
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			DataTable dataTable = new DataTable();
			dataTable = (this._tran != null ? SysDatabase.ExecuteTable(stringBuilder.ToString(), this._tran) : SysDatabase.ExecuteTable(stringBuilder.ToString()));
			foreach (DataRow row in dataTable.Rows)
			{
				instances.Add(this.GetModel(row));
			}
			return instances;
		}

		public Instance GetModelWithXPDL(string key)
		{
			Instance instance;
			Instance model = this.GetModel(key);
			if (model == null)
			{
				instance = null;
			}
			else
			{
				model.XPDL = this.GetXPDL(model);
				instance = model;
			}
			return instance;
		}

		public string GetXPDL(Instance ins)
		{
			string str;
			if (!string.IsNullOrEmpty(ins.XPDLPath))
			{
				string basePath = AppFilePath.GetBasePath(ins.BasePath);
				string str1 = string.Concat(basePath, ins.XPDLPath);
				try
				{
					str = File.ReadAllText(str1);
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					throw new Exception(string.Concat("在读取流程XPDL文件包时出错：", exception.Message, "，文件路径：", str1));
				}
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("select  top 1 XPDL from T_E_WF_Instance ");
				stringBuilder.Append(" where _AutoID=@_AutoID ");
				DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
				SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, ins.InstanceId);
				object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
				str = ((obj == null ? false : obj != DBNull.Value) ? obj.ToString() : "");
			}
			return str;
		}

		private void InsertXPDL(Instance ins)
		{
			string basePath = AppFilePath.GetBasePath(ins.BasePath);
			if (string.IsNullOrEmpty(basePath))
			{
				throw new Exception(string.Concat("附件路径配置错误：Code=", ins.BasePath));
			}
			string str = string.Concat(basePath, ins.XPDLPath);
			string directoryName = Path.GetDirectoryName(str);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			File.WriteAllText(str, ins.XPDL);
		}

		public int Update(Instance model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_WF_Instance set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\tEmployeeName=@EmployeeName,\r\n\t\t\t\t\tDeptName=@DeptName,\r\n\t\t\t\t\tCompanyName=@CompanyName,\r\n\t\t\t\t\tCompanyId=@CompanyId,\r\n\t\t\t\t\tInstanceName=@InstanceName,\r\n\t\t\t\t\tFinishTime=@FinishTime,\r\n\t\t\t\t\tAppName=@AppName,\r\n\t\t\t\t\tAppId=@AppId,\r\n\t\t\t\t\tInstanceState=@InstanceState\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model.InstanceId);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "EmployeeName", DbType.String, model.EmployeeName);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptName", DbType.String, model.DeptName);
			SysDatabase.AddInParameter(sqlStringCommand, "CompanyName", DbType.String, model.CompanyName);
			SysDatabase.AddInParameter(sqlStringCommand, "CompanyId", DbType.String, model.CompanyId);
			SysDatabase.AddInParameter(sqlStringCommand, "InstanceName", DbType.String, model.InstanceName);
			SysDatabase.AddInParameter(sqlStringCommand, "WorkflowId", DbType.String, model.WorkflowId);
			SysDatabase.AddInParameter(sqlStringCommand, "FinishTime", DbType.DateTime, model.FinishTime);
			SysDatabase.AddInParameter(sqlStringCommand, "Remark", DbType.String, model.Remark);
			SysDatabase.AddInParameter(sqlStringCommand, "ProcessId", DbType.String, model.ProcessId);
			SysDatabase.AddInParameter(sqlStringCommand, "AppName", DbType.String, model.AppName);
			SysDatabase.AddInParameter(sqlStringCommand, "AppId", DbType.String, model.AppId);
			SysDatabase.AddInParameter(sqlStringCommand, "InstanceState", DbType.String, model.InstanceState);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int UpdateInstanceName(string instanceName, string instanceId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_WF_Instance set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\tInstanceName=@InstanceName\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, instanceId);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, DateTime.Now);
			SysDatabase.AddInParameter(sqlStringCommand, "InstanceName", DbType.String, instanceName);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public void UpdateXPDL(Instance ins)
		{
			StringBuilder stringBuilder;
			DbCommand sqlStringCommand;
			if (string.IsNullOrEmpty(ins.XPDLPath))
			{
				stringBuilder = new StringBuilder();
				stringBuilder.Append("update T_E_WF_Instance set\r\n                    InstanceName=@InstanceName,\r\n\t\t\t\t\tXPDL=@XPDL\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t    ");
				sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
				SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, ins.InstanceId);
				SysDatabase.AddInParameter(sqlStringCommand, "InstanceName", DbType.String, ins.InstanceName);
				SysDatabase.AddInParameter(sqlStringCommand, "XPDL", DbType.String, ins.XPDL);
				if (this._tran == null)
				{
					SysDatabase.ExecuteNonQuery(sqlStringCommand);
				}
				else
				{
					SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran);
				}
			}
			else
			{
				string basePath = ins.BasePath;
				if (string.IsNullOrEmpty(basePath))
				{
					basePath = this.GetInstanceBasePath(ins.InstanceId);
				}
				string str = AppFilePath.GetBasePath(basePath);
				string str1 = string.Concat(str, ins.XPDLPath);
				if (!File.Exists(str1))
				{
					throw new Exception("XPDL文件不存在");
				}
				try
				{
					File.WriteAllText(str1, ins.XPDL);
					stringBuilder = new StringBuilder();
					stringBuilder.Append("update T_E_WF_Instance set\r\n                        InstanceName=@InstanceName\r\n\t\t\t\t\t    where _AutoID=@_AutoID\r\n\t\t\t        ");
					sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
					SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, ins.InstanceId);
					SysDatabase.AddInParameter(sqlStringCommand, "InstanceName", DbType.String, ins.InstanceName);
					if (this._tran == null)
					{
						SysDatabase.ExecuteNonQuery(sqlStringCommand);
					}
					else
					{
						SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran);
					}
				}
				catch (Exception exception)
				{
					throw exception;
				}
			}
		}
	}
}