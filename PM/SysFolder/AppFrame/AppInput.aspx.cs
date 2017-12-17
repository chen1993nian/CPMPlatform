using AjaxPro;
using EIS.AppBase;
using EIS.AppModel;
using EIS.AppModel.Workflow;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using EIS.Permission;
using EIS.Permission.Service;
using EIS.WebBase.ModelLib.Service;
using EIS.WorkFlow.Access;
using EIS.WorkFlow.Engine;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace EIS.Web.SysFolder.AppFrame
{
	public partial class AppInput : PageBase
	{
		
		public StringBuilder sbmodel = new StringBuilder();

		public XmlDocument xmlDoc = new XmlDocument();

		public string tblName = "";

		public string fileCount = "";

		public string tblHTML = "";

		public string sIndex = "";

		public string TblNameCn = "";

		public string copyId = "";

		public string fileListUrl = "";

		public string condition = "";

		public string editScriptBlock = "";

		public string isNew = "";

		public string xmlData = "";

		public string workflowCode = "";

		public string startWF = "";

		public string customScript = "";

		public string formWidth = "";

		private Define define_0 = new Define();

		public string mainId
		{
			get
			{
				string str;
				str = (this.ViewState["MainId"] != null ? this.ViewState["MainId"].ToString() : "");
				return str;
			}
			set
			{
				this.ViewState["MainId"] = value;
			}
		}

		public string SysFuncEtag
		{
			get
			{
				if (HttpContext.Current.Cache["SysFuncEtag"] == null)
				{
					string str = HttpContext.Current.Server.MapPath("~/Js/SysFunction.js");
					FileInfo fileInfo = new FileInfo(str);
					CacheDependency cacheDependency = new CacheDependency(str);
					HttpRuntime.Cache.Insert("SysFuncEtag", fileInfo.LastWriteTime.Ticks, cacheDependency);
				}
				return HttpContext.Current.Cache["SysFuncEtag"].ToString();
			}
		}

		public AppInput()
		{
		}

		protected void btnStartWF_Click(object sender, EventArgs e)
		{
			Match match = null;
			DateTime now;
			if (this.workflowCode == "")
			{
				List<Define> defineListByCompanyId = DefineService.GetDefineListByCompanyId(this.tblName, base.CompanyId);
				int num = 0;
				foreach (Define define in defineListByCompanyId)
				{
					if (!this.CheckConfigCondition(define.WorkflowCode, this.tblName, this.mainId))
					{
						continue;
					}
					this.workflowCode = define.WorkflowCode;
					num++;
				}
				if (num == 0)
				{
					this.Session["_sysinfo"] = "找不到符合条件的流程定义信息";
					base.Server.Transfer("AppInfo.aspx", true);
				}
				else if (num > 1)
				{
					base.Response.Redirect(string.Format("../Workflow/SelectWorkFlow.aspx?tblName={0}&mainId={1}", this.tblName, this.mainId));
				}
			}
			this.define_0 = DefineService.GetWorkflowByCode(this.workflowCode);
			if (this.define_0 == null)
			{
				this.Session["_sysinfo"] = string.Format("流程定义已经删除，流程编号为{0}！", this.workflowCode);
				base.Server.Transfer("AppInfo.aspx?msgType=warning", true);
			}
			string str = "";
			string str1 = string.Format("select * from T_E_WF_Config where Enable='是' and WFId='{0}'", this.define_0.WorkflowCode);
			DataTable dataTable = SysDatabase.ExecuteTable(str1);
			if (dataTable.Rows.Count <= 0)
			{
				str = "";
			}
			else
			{
				str = dataTable.Rows[0]["InstanceName"].ToString();
				str = base.ReplaceContext(str);
				Regex regex = new Regex("{DateTime:([ymdhs-]+)}", RegexOptions.IgnoreCase);
				foreach (Match matchA in regex.Matches(str))
				{
                    string value = matchA.Value;
					now = DateTime.Now;
                    str = str.Replace(value, now.ToString(matchA.Groups[1].Value));
				}
				regex = new Regex("\\[([ymdhs\\-\\u5E74\\u6708\\u65E5:/]+)]", RegexOptions.IgnoreCase);
				foreach (Match match1 in regex.Matches(str))
				{
					string value1 = match1.Value;
					now = DateTime.Now;
					str = str.Replace(value1, now.ToString(match1.Groups[1].Value));
				}
				string[] strArrays = new string[] { "select * from ", this.tblName, " where _AutoId='", this.mainId, "'" };
				str1 = string.Format(string.Concat(strArrays), new object[0]);
				DataTable dataTable1 = SysDatabase.ExecuteTable(str1);
				if (dataTable1.Rows.Count == 1)
				{
					str = base.ReplaceWithDataRow(str, dataTable1.Rows[0]);
				}
			}
			DriverEngine driverEngine = new DriverEngine(base.UserInfo);
			Instance instance = new Instance()
			{
				InstanceId = Guid.NewGuid().ToString(),
				_UserName = base.EmployeeID,
				_OrgCode = base.OrgCode,
				_CreateTime = DateTime.Now,
				_UpdateTime = DateTime.Now,
				_IsDel = 0,
				InstanceName = str,
				WorkflowId = this.define_0.WorkflowId,
				DeptId = this.Session["DeptId"].ToString(),
				DeptName = this.Session["DeptName"].ToString(),
				CompanyId = this.Session["CompanyId"].ToString(),
				CompanyName = this.Session["CompanyName"].ToString(),
				EmployeeName = base.EmployeeName,
				AppId = this.mainId,
				AppName = this.tblName,
				Importance = "1",
				Remark = "",
				XPDL = driverEngine.CopyActivityPerformers(this.define_0, base.UserInfo)
			};
			if ((string.IsNullOrEmpty(this.mainId) ? false : InstanceService.IsRunAlready(this.tblName, this.mainId)))
			{
				this.Session["_syserror"] = "每条业务数据只能发起一支流程";
				base.Server.Transfer("FlowErrorInfo.aspx?taskId=", false);
			}
			DbConnection dbConnection = SysDatabase.CreateConnection();
			try
			{
				Task task = new Task();
				dbConnection.Open();
				DbTransaction dbTransaction = dbConnection.BeginTransaction();
				try
				{
					try
					{
						driverEngine.NewWorkFlowInstance(instance, dbTransaction);
						task = driverEngine.StartWorkFlowInstance(instance, "提交", dbTransaction);
						List<Task> tasks = driverEngine.NextTask(instance, task, dbTransaction);
						if (instance.NeedUpdate)
						{
							(new _Instance(dbTransaction)).UpdateXPDL(instance);
						}
						if (tasks.Count > 0)
						{
							UserTask userTaskByTaskId = EIS.WorkFlow.Engine.Utility.GetUserTaskByTaskId(task.TaskId, base.EmployeeID, dbTransaction);
							StringCollection stringCollections = new StringCollection();
							foreach (Task task1 in tasks)
							{
								TaskService.GetTaskDealUser(task1.TaskId, stringCollections, dbTransaction);
							}
							userTaskByTaskId.RecIds = EIS.AppBase.Utility.GetJoinString(stringCollections);
							userTaskByTaskId.RecNames = EmployeeService.GetEmployeeNameList(stringCollections);
							(new _UserTask(dbTransaction)).UpdateAdvice(userTaskByTaskId);
						}
						dbTransaction.Commit();
					}
					catch (NoUserException noUserException1)
					{
						NoUserException noUserException = noUserException1;
						dbTransaction.Rollback();
						this.fileLogger.Error<NoUserException>(noUserException);
						this.Session["_syserror"] = noUserException.Message;
						this.Session["_syserror_more"] = noUserException.ToString();
						base.Server.Transfer("../Workflow/FlowErrorInfo.aspx?taskId=", true);
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						dbTransaction.Rollback();
						this.fileLogger.Error<Exception>(exception);
						this.Session["_syserror"] = exception.Message;
						base.Server.Transfer("../Workflow/FlowErrorInfo.aspx?taskId=", true);
					}
				}
				finally
				{
					dbConnection.Close();
				}
				base.Server.Transfer(string.Concat("../Workflow/DealFlowAfter.aspx?taskId=", task.TaskId), false);
			}
			finally
			{
				if (dbConnection != null)
				{
					((IDisposable)dbConnection).Dispose();
				}
			}
		}

		public bool CheckConfigCondition(string workFlowCode, string TblName, string MainId)
		{
			bool flag;
			string str;
			string str1 = string.Format("select * from T_E_WF_Config where Enable='是' and WFId='{0}'", workFlowCode, base.CompanyId);
			DataTable dataTable = SysDatabase.ExecuteTable(str1);
			if (dataTable.Rows.Count != 0)
			{
				string str2 = dataTable.Rows[0]["condition"].ToString();
				dataTable.Rows[0]["condition2"].ToString();
				string str3 = dataTable.Rows[0]["companyId"].ToString();
				DataTable dataTable1 = SysDatabase.ExecuteTable(string.Format("select * from {0} where _autoid='{1}'", TblName, MainId));
				this.fileLogger.Info(string.Format("select * from {0} where _autoid='{1}'", TblName, MainId));
				if (str3 == "")
				{
					if (str2 != "")
					{
						str2 = base.ReplaceContext(str2);
						this.fileLogger.Info(str2);
						str2 = base.ReplaceWithDataRow(str2, dataTable1.Rows[0]);
						this.fileLogger.Info(str2);
						str = string.Concat("select count(*) where ", str2);
						this.fileLogger.Info(str);
						flag = (int.Parse(SysDatabase.ExecuteScalar(str).ToString()) <= 0 ? false : true);
					}
					else
					{
						flag = true;
					}
				}
				else if (str3 != base.CompanyId)
				{
					flag = false;
				}
				else if (str2 != "")
				{
					str2 = base.ReplaceContext(str2);
					str2 = base.ReplaceWithDataRow(str2, dataTable1.Rows[0]);
					str = string.Concat("select count(*) where ", str2);
					flag = (int.Parse(SysDatabase.ExecuteScalar(str).ToString()) <= 0 ? false : true);
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		 [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void DelAllRecord(string tblName, string parentId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			_TableInfo __TableInfo = new _TableInfo(tblName);
			__TableInfo.GetModel();
			List<TableInfo> subTable = __TableInfo.GetSubTable();
			DbConnection dbConnection = SysDatabase.CreateConnection();
			dbConnection.Open();
			DbTransaction dbTransaction = dbConnection.BeginTransaction();
			try
			{
				string[] tableName = new string[] { "select * from ", tblName, " where _MainID='", parentId, "'" };
				foreach (DataRow row in SysDatabase.ExecuteTable(string.Concat(tableName)).Rows)
				{
					string str = row["_AutoID"].ToString();
					foreach (TableInfo tableInfo in subTable)
					{
						tableName = new string[] { "delete ", tableInfo.TableName, " where _MainID='", str, "'" };
						SysDatabase.ExecuteNonQuery(string.Concat(tableName), dbTransaction);
					}
				}
				stringBuilder.AppendFormat("delete {0}  where _mainId='{1}';", tblName, parentId);
				SysDatabase.ExecuteNonQuery(stringBuilder.ToString(), dbTransaction);
				dbTransaction.Commit();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				dbTransaction.Rollback();
				throw exception;
			}
		}

		 [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void DelRecord(string mainTbl, string autoid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (InstanceService.IsRunAlready(mainTbl, autoid))
			{
				if (SysDatabase.ExecuteScalar(string.Format("select count(*) from T_E_WF_Instance i inner join T_E_WF_Task t on i._AutoID=t.InstanceId\r\n                    where i.AppName='{0}' and i.AppId='{1}'", mainTbl, autoid)).ToString() != "1")
				{
					throw new Exception("本条记录已经存在审批数据，不能删除");
				}
				string str = string.Format("select top 1 _AutoId from T_E_WF_Instance where AppName='{0}' and AppId='{1}'", mainTbl, autoid);
				string str1 = SysDatabase.ExecuteScalar(str).ToString();
				EIS.WorkFlow.Engine.Utility.RemoveInstance(str1, base.UserInfo);
			}
			if (DaArchiveService.IsArchived(mainTbl, autoid))
			{
				throw new Exception("本条记录已经归档，不能删除");
			}
			ModelSave modelSave = new ModelSave(this);
			stringBuilder.AppendFormat("<?xml version=\"1.0\" encoding=\"utf-8\"?><root><Table TableName=\"{0}\">", mainTbl);
			stringBuilder.AppendFormat("<row state=\"Deleted\" id=\"\"><_AutoID><![CDATA[{0}]]></_AutoID></row></Table></root>", autoid);
			modelSave.SaveData(mainTbl, stringBuilder.ToString());
		}

		 [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void DelSubRecord(string subTbl, string autoId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("delete {0}  where _autoId='{1}';", subTbl, autoId);
			SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
		}

		 [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string ExecSQL(string scriptCode, string para)
		{
            string str = "";
            string tableSQLScript = "";

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select ScriptTxt,ConnectionId from T_E_Sys_TableScript ");
            stringBuilder.Append(" where ScriptCode=@ScriptCode and Enable='是' ");
             DbCommand ScriptCmmd = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
            SysDatabase.AddInParameter(ScriptCmmd, "@ScriptCode", DbType.String, scriptCode);
            DataTable dtScript = SysDatabase.ExecuteTable(ScriptCmmd);

            try
            {
                 tableSQLScript = dtScript.Rows[0]["ScriptTxt"].ToString();
                 string connectionId = dtScript.Rows[0]["ConnectionId"].ToString().Trim();
                 if (connectionId != "")
                 {
                     if (tableSQLScript != "")
                     {
                         CustomDb customDb = new CustomDb();
                         customDb.CreateDatabaseByConnectionId(connectionId.Trim());

                         if (tableSQLScript != "")
                         {
                             if (TableService.HasTableSQLScriptCommandPara(para))
                             {
                                 tableSQLScript = base.ReplaceContext(tableSQLScript);
                                 DbCommand dbcmmd = customDb.GetSqlStringCommand(tableSQLScript);
                                 string[] strArrays = para.Split(new char[] { '|' });
                                 for (int i = 0; i < (int)strArrays.Length; i++)
                                 {
                                     str = strArrays[i];
                                     string[] strArrays1 = str.Split("=".ToCharArray(), 2);
                                     if (strArrays1[0].Contains("@int_"))
                                     {
                                         customDb.AddInParameter(dbcmmd, strArrays1[0], DbType.Int64, Convert.ToInt64(strArrays1[1]));
                                     }
                                     else if (strArrays1[0].Contains("@str_"))
                                     {
                                         customDb.AddInParameter(dbcmmd, strArrays1[0], DbType.String, strArrays1[1]);
                                     }
                                     else if (strArrays1[0].Contains("@date_"))
                                     {
                                         customDb.AddInParameter(dbcmmd, strArrays1[0], DbType.DateTime, Convert.ToDateTime(strArrays1[1]));
                                     }
                                     else if (strArrays1[0].Contains("@num_"))
                                     {
                                         customDb.AddInParameter(dbcmmd, strArrays1[0], DbType.DateTime, Convert.ToDecimal(strArrays1[1]));
                                     }
                                 }
                                 object obj = customDb.ExecuteNonQuery(dbcmmd);
                                 str = (obj == null ? "" : obj.ToString());
                             }
                             else
                             {
                                 tableSQLScript = base.ReplaceContext(tableSQLScript);
                                 tableSQLScript = EIS.AppBase.Utility.ReplaceParaValues(tableSQLScript, para);
                                 object obj = customDb.ExecuteScalar(tableSQLScript);
                                 str = (obj == null ? "" : obj.ToString());
                             }
                         }
                     }
                 }
                 else
                 {
                     tableSQLScript = TableService.GetTableSQLScript(scriptCode);
                     if (tableSQLScript != "")
                     {
                         if (TableService.HasTableSQLScriptCommandPara(para))
                         {
                             tableSQLScript = base.ReplaceContext(tableSQLScript);
                             DbCommand dbcmmd = SysDatabase.GetSqlStringCommand(tableSQLScript);
                             string[] strArrays = para.Split(new char[] { '|' });
                             for (int i = 0; i < (int)strArrays.Length; i++)
                             {
                                 str = strArrays[i];
                                 string[] strArrays1 = str.Split("=".ToCharArray(), 2);
                                 if (strArrays1[0].Contains("@int_"))
                                 {
                                     SysDatabase.AddInParameter(dbcmmd, strArrays1[0], DbType.Int64, Convert.ToInt64(strArrays1[1]));
                                 }
                                 else if (strArrays1[0].Contains("@str_"))
                                 {
                                     SysDatabase.AddInParameter(dbcmmd, strArrays1[0], DbType.String, strArrays1[1]);
                                 }
                                 else if (strArrays1[0].Contains("@date_"))
                                 {
                                     SysDatabase.AddInParameter(dbcmmd, strArrays1[0], DbType.DateTime, Convert.ToDateTime(strArrays1[1]));
                                 }
                                 else if (strArrays1[0].Contains("@num_"))
                                 {
                                     SysDatabase.AddInParameter(dbcmmd, strArrays1[0], DbType.DateTime, Convert.ToDecimal(strArrays1[1]));
                                 }
                             }
                             object obj = SysDatabase.ExecuteNonQuery(dbcmmd);
                             str = (obj == null ? "" : obj.ToString());
                         }
                         else
                         {
                             tableSQLScript = base.ReplaceContext(tableSQLScript);
                             tableSQLScript = EIS.AppBase.Utility.ReplaceParaValues(tableSQLScript, para);
                             object obj = SysDatabase.ExecuteScalar(tableSQLScript);
                             str = (obj == null ? "" : obj.ToString());
                         }
                     }
                 }
            }
            catch { }
            finally { }

			return str;
		}

		 [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string GetAutoSn(string fieldId, string para)
		{
			object obj;
			string str;
			DataTable dataTable = SysDatabase.ExecuteTable(string.Format("select TableName,FieldName,FieldType, FieldInDispStyle,FieldInDispStyleTxt from T_E_Sys_FieldInfo where _AutoId='{0}'", fieldId));
			if (dataTable.Rows.Count > 0)
			{
				string str1 = dataTable.Rows[0]["FieldInDispStyleTxt"].ToString();
				string str2 = dataTable.Rows[0]["FieldType"].ToString();
				string str3 = dataTable.Rows[0]["FieldName"].ToString();
				string str4 = dataTable.Rows[0]["TableName"].ToString();
				string str5 = "";
				string str6 = "";
				string str7 = "";
				string str8 = "";
				if (str1.Length <= 0)
				{
					str = null;
					return str;
				}
				str1 = base.ReplaceContext(str1);
				string[] strArrays = str1.Split("|".ToCharArray());
				if (str2 == "1")
				{
					str5 = (strArrays.GetValue(0) == null ? "" : strArrays.GetValue(0).ToString());
					str6 = strArrays.GetValue(3).ToString();
					string[] strArrays1 = para.Split(new char[] { '|' });
					for (int i = 0; i < (int)strArrays1.Length; i++)
					{
						string str9 = strArrays1[i];
						string[] strArrays2 = str9.Split("=".ToCharArray(), 2);
						str5 = str5.Replace(string.Concat("{", strArrays2[0].Substring(1), "}"), strArrays2[1]);
						str6 = str6.Replace(string.Concat("{", strArrays2[0].Substring(1), "}"), strArrays2[1]);
					}
					DateTime today = DateTime.Today;
					string str10 = today.ToString("MM");
					string str11 = today.ToString("dd");
					str5 = str5.Replace("{年}", today.ToString("yyyy"));
					str5 = str5.Replace("{年2}", today.ToString("yy"));
					str5 = str5.Replace("{月}", str10);
					str5 = str5.Replace("{日}", str11);
					str6 = str6.Replace("{年}", today.ToString("yyyy"));
					str6 = str6.Replace("{年2}", today.ToString("yy"));
					str6 = str6.Replace("{月}", str10);
					str6 = str6.Replace("{日}", str11);
					string str12 = string.Concat("select max(", str3, ") from ", str4);
					string[] strArrays3 = new string[] { str12, " where ", str3, " like '", str5, "%", str6, "'" };
					string str13 = string.Concat(strArrays3);
					obj = SysDatabase.ExecuteScalar(SysDatabase.GetSqlStringCommand(str13));
					int num = 1;
					if ((obj == null ? false : obj != DBNull.Value))
					{
						string str14 = "";
						str14 = obj.ToString();
						if (str5 != "")
						{
							str14 = str14.Substring(str5.Length, str14.Length - str5.Length).Trim();
						}
						if (str6 != "")
						{
							str14 = str14.Substring(0, str14.Length - str6.Length).Trim();
						}
						num = (!int.TryParse(str14, out num) ? 0 : Convert.ToInt32(str14));
						num++;
						str8 = string.Concat(str5, num.ToString(string.Concat("d", strArrays[1])), str6);
					}
					else
					{
						num = (strArrays[2].Trim() != "" ? Convert.ToInt32(strArrays[2].Trim()) : 1);
						str8 = (strArrays.GetValue(4).ToString() != "1" ? string.Concat(str5, Convert.ToString(num), str6) : string.Concat(str5, num.ToString(string.Concat("d", strArrays[1])), str6));
					}
				}
				else if (str2 == "2")
				{
					str7 = string.Concat("select isNull(max(", str3, "),0) from ", str4);
					obj = SysDatabase.ExecuteScalar(str7);
					if (obj != null)
					{
						str8 = (Convert.ToInt32(obj) != 0 ? Convert.ToString(Convert.ToInt32(obj) + 1) : strArrays[2]);
					}
				}
				str = str8;
				return str;
			}
			str = null;
			return str;
		}

         [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
         public DataTable GetDataTable(string scriptCode, string para)
         {
             DataTable dataTable = null;
             string tableSQLScript = "";

             StringBuilder stringBuilder = new StringBuilder();
             stringBuilder.Append("select ScriptTxt,ConnectionId from T_E_Sys_TableScript ");
             stringBuilder.Append(" where ScriptCode=@ScriptCode and Enable='是' ");
             DbCommand ScriptCmmd = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
             SysDatabase.AddInParameter(ScriptCmmd, "@ScriptCode", DbType.String, scriptCode);
             DataTable dtScript = SysDatabase.ExecuteTable(ScriptCmmd);

             try
             {
                 tableSQLScript = dtScript.Rows[0]["ScriptTxt"].ToString();
                 string connectionId = dtScript.Rows[0]["ConnectionId"].ToString().Trim();
                 if (connectionId != "")
                 {
                     if (tableSQLScript != "")
                     {
                         CustomDb customDb = new CustomDb();
                         customDb.CreateDatabaseByConnectionId(connectionId.Trim());

                         if (TableService.HasTableSQLScriptCommandPara(para))
                         {
                             tableSQLScript = base.ReplaceContext(tableSQLScript);
                             DbCommand dbcmmd = customDb.GetSqlStringCommand(tableSQLScript);
                             string[] strArrays = para.Split(new char[] { '|' });
                             for (int i = 0; i < (int)strArrays.Length; i++)
                             {
                                 string str = strArrays[i];
                                 string[] strArrays1 = str.Split("=".ToCharArray(), 2);
                                 if (strArrays1[0].Contains("@int_"))
                                 {
                                     customDb.AddInParameter(dbcmmd, strArrays1[0], DbType.Int64, Convert.ToInt64(strArrays1[1]));
                                 }
                                 else if (strArrays1[0].Contains("@str_"))
                                 {
                                     customDb.AddInParameter(dbcmmd, strArrays1[0], DbType.String, strArrays1[1]);
                                 }
                                 else if (strArrays1[0].Contains("@date_"))
                                 {
                                     customDb.AddInParameter(dbcmmd, strArrays1[0], DbType.DateTime, Convert.ToDateTime(strArrays1[1]));
                                 }
                                 else if (strArrays1[0].Contains("@num_"))
                                 {
                                     customDb.AddInParameter(dbcmmd, strArrays1[0], DbType.DateTime, Convert.ToDecimal(strArrays1[1]));
                                 }
                             }
                             dataTable = customDb.ExecuteTable(dbcmmd);
                         }
                         else
                         {
                             tableSQLScript = base.ReplaceContext(tableSQLScript);
                             tableSQLScript = EIS.AppBase.Utility.ReplaceParaValues(tableSQLScript, para);
                             dataTable = customDb.ExecuteTable(tableSQLScript);
                         }
                     }
                 }
                 else
                 {
                     if (tableSQLScript != "")
                     {
                         if (TableService.HasTableSQLScriptCommandPara(para))
                         {
                             tableSQLScript = base.ReplaceContext(tableSQLScript);
                             DbCommand dbcmmd = SysDatabase.GetSqlStringCommand(tableSQLScript);
                             string[] strArrays = para.Split(new char[] { '|' });
                             for (int i = 0; i < (int)strArrays.Length; i++)
                             {
                                 string str = strArrays[i];
                                 string[] strArrays1 = str.Split("=".ToCharArray(), 2);
                                 if (strArrays1[0].Contains("@int_"))
                                 {
                                     SysDatabase.AddInParameter(dbcmmd, strArrays1[0], DbType.Int64, Convert.ToInt64(strArrays1[1]));
                                 }
                                 else if (strArrays1[0].Contains("@str_"))
                                 {
                                     SysDatabase.AddInParameter(dbcmmd, strArrays1[0], DbType.String, strArrays1[1]);
                                 }
                                 else if (strArrays1[0].Contains("@date_"))
                                 {
                                     SysDatabase.AddInParameter(dbcmmd, strArrays1[0], DbType.DateTime, Convert.ToDateTime(strArrays1[1]));
                                 }
                                 else if (strArrays1[0].Contains("@num_"))
                                 {
                                     SysDatabase.AddInParameter(dbcmmd, strArrays1[0], DbType.DateTime, Convert.ToDecimal(strArrays1[1]));
                                 }
                             }
                             dataTable = SysDatabase.ExecuteTable(dbcmmd);
                         }
                         else
                         {
                             tableSQLScript = base.ReplaceContext(tableSQLScript);
                             tableSQLScript = EIS.AppBase.Utility.ReplaceParaValues(tableSQLScript, para);
                             dataTable = SysDatabase.ExecuteTable(tableSQLScript);
                         }
                     }
                 }
             }
             catch { }
             finally { }

             return dataTable;
         }

		 [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public DataTable GetLinkData(string fieldId, string para)
		{
			DataTable dataTable;
			string str = string.Format("select FieldInDispStyle,FieldInDispStyleTxt from T_E_Sys_FieldInfo where _AutoId='{0}'", fieldId);
			DataTable dataTable1 = SysDatabase.ExecuteTable(str);
			if (dataTable1.Rows.Count > 0)
			{
				string str1 = dataTable1.Rows[0]["FieldInDispStyleTxt"].ToString();
				if (str1.Length <= 0)
				{
					dataTable = null;
					return dataTable;
				}
				str = base.ReplaceContext(str1).Replace("[QUOTES]", "'");
				string[] strArrays = para.Split(new char[] { '|' });
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					string str2 = strArrays[i];
					string[] strArrays1 = str2.Split("=".ToCharArray(), 2);
					str = str.Replace(string.Concat("{", strArrays1[0].Substring(1), "}"), strArrays1[1]);
				}
				dataTable = SysDatabase.ExecuteTable(str);
				return dataTable;
			}
			dataTable = null;
			return dataTable;
		}

		 [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string GetListHtml(string mainTbl, string subTbl, string parentId, string sIndex)
		{
			ModelBuilder modelBuilder = new ModelBuilder(this)
			{
				Sindex = sIndex,
				DataControl = new List<DataControl>(),
				DataContolFirst = false,
				ReplaceValue = ""
			};
			return modelBuilder.GetRelationList(mainTbl, subTbl, parentId);
		}

		 [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public DataTable GetQuery(string queryId, string keyWord, string condition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			_FieldInfo __FieldInfo = new _FieldInfo();
			TableInfo modelById = (new _TableInfo()).GetModelById(queryId);
			foreach (FieldInfo modelQueryDisp in __FieldInfo.GetModelQueryDisp(modelById.TableName))
			{
				if (stringBuilder.Length <= 0)
				{
					stringBuilder.AppendFormat("({0} like '%{1}%'", modelQueryDisp.FieldName, keyWord);
				}
				else
				{
					stringBuilder.AppendFormat(" or {0} like '%{1}%'", modelQueryDisp.FieldName, keyWord);
				}
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append(")");
			}
			if (condition.Length > 0)
			{
				stringBuilder.Append(string.Concat(" and ", condition));
			}
			if (stringBuilder.Length == 0)
			{
				stringBuilder.Append(" 1=1 ");
			}
			string str = string.Concat("select ListSQL,ConnectionId from T_E_Sys_TableInfo where tableName='", modelById.TableName, "'");
			DataTable dataTable = SysDatabase.ExecuteTable(str);
			string str1 = dataTable.Rows[0]["ConnectionId"].ToString();
			string str2 = dataTable.Rows[0]["ListSQL"].ToString();
			str2 = str2.Replace("|^condition^|", stringBuilder.ToString()).Replace("|^sortdir^|", "").Replace("\r\n", " ").Replace("\t", "");
			str2 = this.ReplaceContext(str2);
			DataTable dataTable1 = new DataTable();
			if (str1.Trim() == "")
			{
				dataTable1 = SysDatabase.ExecuteTable(str2);
			}
			else
			{
				CustomDb customDb = new CustomDb();
				customDb.CreateDatabaseByConnectionId(str1.Trim());
				dataTable1 = customDb.ExecuteTable(str2);
			}
            if (dataTable1.Rows.Count > 1)
            {
                dataTable1 = new DataTable();
            }
			return dataTable1;
		}

		public string getTblHtml()
		{
			XmlDeclaration xmlDeclaration = this.xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
			this.xmlDoc.AppendChild(xmlDeclaration);
			try
			{
				ModelBuilder modelBuilder = new ModelBuilder(this)
				{
					Sindex = base.GetParaValue("sindex")
				};
				this.sIndex = modelBuilder.Sindex;
                base.AppIndex = this.sIndex;
                string paraValue = base.GetParaValue("cpro");
				if (paraValue == "")
				{
					paraValue = base.GetParaValue(string.Concat(this.tblName, "cpro"));
				}
				modelBuilder.DataControl = modelBuilder.GenDataControlFromPara(paraValue, this.tblName);
				modelBuilder.DataContolFirst = true;
				modelBuilder.CopyId = this.copyId;
				modelBuilder.ReplaceValue = base.GetParaValue("replacevalue");
				this.tblHTML = modelBuilder.GetTblHtml(this.tblName, this.condition, this.sbmodel, this.xmlDoc);
				if (this.tblHTML.Trim().Length > 0)
				{
					XmlElement xmlElement = (XmlElement)this.xmlDoc.SelectSingleNode(string.Concat("//Table[@TableName='", this.tblName, "']/row"));
					if (xmlElement != null)
					{
						this.mainId = modelBuilder.MainId;
						string str = base.GetParaValue("parentId");
						if (str != "")
						{
							XmlNode xmlNodes = xmlElement.SelectSingleNode("_MainID");
							if (xmlNodes != null)
							{
								xmlNodes.InnerText = str;
							}
						}
					}
				}
				this.xmlData = this.xmlDoc.DocumentElement.OuterXml;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				this.fileLogger.Error<Exception>(exception);
				this.tblHTML = exception.Message;
			}
			return this.tblHTML;
		}

		private void method_0(string string_0, DataRow dataRow_0, Define define_1)
		{
			string str = dataRow_0["_AutoID"].ToString();
			if ((string.IsNullOrEmpty(str) ? true : !InstanceService.IsRunAlready(string_0, str)))
			{
				DriverEngine driverEngine = new DriverEngine(base.UserInfo);
				Instance instance = new Instance()
				{
					InstanceId = Guid.NewGuid().ToString(),
					_UserName = base.EmployeeID,
					_OrgCode = base.OrgCode,
					_CreateTime = DateTime.Now,
					_UpdateTime = DateTime.Now,
					_IsDel = 0,
					InstanceName = this.method_1(dataRow_0, define_1),
					WorkflowId = define_1.WorkflowId,
					DeptId = this.Session["DeptId"].ToString(),
					DeptName = this.Session["DeptName"].ToString(),
					CompanyId = this.Session["CompanyId"].ToString(),
					CompanyName = this.Session["CompanyName"].ToString(),
					EmployeeName = base.EmployeeName,
					AppId = str,
					AppName = string_0,
					Importance = "1",
					Remark = "",
					XPDL = driverEngine.CopyActivityPerformers(define_1, base.UserInfo)
				};
				DbConnection dbConnection = SysDatabase.CreateConnection();
				try
				{
					Task task = new Task();
					dbConnection.Open();
					DbTransaction dbTransaction = dbConnection.BeginTransaction();
					try
					{
						try
						{
							driverEngine.NewWorkFlowInstance(instance, dbTransaction);
							task = driverEngine.StartWorkFlowInstance(instance, "", dbTransaction);
							List<Task> tasks = driverEngine.NextTask(instance, task, dbTransaction);
							if (instance.NeedUpdate)
							{
								(new _Instance(dbTransaction)).UpdateXPDL(instance);
							}
							if (tasks.Count > 0)
							{
								UserTask userTaskByTaskId = EIS.WorkFlow.Engine.Utility.GetUserTaskByTaskId(task.TaskId, base.EmployeeID, dbTransaction);
								StringCollection stringCollections = new StringCollection();
								foreach (Task task1 in tasks)
								{
									TaskService.GetTaskDealUser(task1.TaskId, stringCollections, dbTransaction);
								}
								userTaskByTaskId.RecIds = EIS.AppBase.Utility.GetJoinString(stringCollections);
								userTaskByTaskId.RecNames = EmployeeService.GetEmployeeNameList(stringCollections);
								(new _UserTask(dbTransaction)).UpdateAdvice(userTaskByTaskId);
							}
							dbTransaction.Commit();
						}
						catch (Exception exception1)
						{
							Exception exception = exception1;
							dbTransaction.Rollback();
							this.fileLogger.Error<Exception>(exception);
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

		private string method_1(DataRow dataRow_0, Define define_1)
		{
			Match match = null;
			DateTime today;
			string str = "";
			string str1 = string.Format("select * from T_E_WF_Config where Enable='是' and WFId='{0}'", define_1.WorkflowCode);
			DataTable dataTable = SysDatabase.ExecuteTable(str1);
			if (dataTable.Rows.Count <= 0)
			{
				string workflowName = define_1.WorkflowName;
				today = DateTime.Today;
				str = string.Concat(workflowName, "-", today.ToString("yyyy年MM月dd日"));
			}
			else
			{
				str = dataTable.Rows[0]["InstanceName"].ToString();
				str = base.ReplaceContext(str);
				Regex regex = new Regex("{DateTime:([ymdhs-]+)}", RegexOptions.IgnoreCase);
				foreach (Match matchAa in regex.Matches(str))
				{
                    string value = matchAa.Value;
					today = DateTime.Now;
                    str = str.Replace(value, today.ToString(matchAa.Groups[1].Value));
				}
				regex = new Regex("\\[([ymdhs\\-\\u5E74\\u6708\\u65E5:/]+)]", RegexOptions.IgnoreCase);
				foreach (Match match1 in regex.Matches(str))
				{
					string value1 = match1.Value;
					today = DateTime.Now;
					str = str.Replace(value1, today.ToString(match1.Groups[1].Value));
				}
				str = base.ReplaceWithDataRow(str, dataRow_0);
			}
			return str;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			string str;
			object item;
			this.tblName = base.GetParaValue("tblName").Trim();
			this.copyId = base.GetParaValue("copyId").Trim();
			this.workflowCode = base.GetParaValue("workflowCode");
			AjaxPro.Utility.RegisterTypeForAjax(typeof(AppInput));
			this.condition = base.GetParaValue("condition").Replace("[QUOTES]", "'");
            if (!string.IsNullOrEmpty(this.condition))
            {
                this.condition = base.ReplaceContext(this.condition);
            }

			if (string.IsNullOrEmpty(this.condition) && base.GetParaValue("mainId") != "")
			{
				this.mainId = base.GetParaValue("mainId");
				this.condition = string.Concat("_AutoId='", this.mainId, "'");
			}
			this.customScript = base.GetCustomScript("ref_AppInput");
			this.isNew = (this.condition == "" ? "true" : "false");
			_TableInfo __TableInfo = new _TableInfo(this.tblName);
			bool flag = __TableInfo.FieldExists("_wfstate");
			TableInfo model = __TableInfo.GetModel();
			if (model == null)
			{
				this.Session["_sysinfo"] = string.Concat("tblName参数有错，找不到名称为：", this.tblName, "的业务定义");
				base.Server.Transfer("AppInfo.aspx", true);
			}
			if ((!flag ? true : model.ShowState != 1))
			{
				this.startWF = "display:none;";
			//	this.btnStartWF.Style.Add("display", "none");
			}
			else
			{
				this.startWF = "";
				//this.btnStartWF.Style.Add("display", "");
			}
			this.tblName = model.TableName;
			this.TblNameCn = model.TableNameCn;
			this.formWidth = model.FormWidthStyle;
			if (!base.IsPostBack)
			{
				if (string.IsNullOrEmpty(base.GetParaValue("admin")))
				{
					string paraValue = base.GetParaValue("funId");
					if (paraValue.Length > 0)
					{
						string funLimitByEmployeeId = EIS.Permission.Utility.GetFunLimitByEmployeeId(base.EmployeeID, paraValue);
						bool flag1 = funLimitByEmployeeId.Substring(1, 1) == "1";
						bool flag2 = funLimitByEmployeeId.Substring(2, 1) == "1";
                        if (!string.IsNullOrEmpty(this.condition))
                        {
                            flag = !flag1;
                        }
                        else
                        {
                            flag = !flag2;
                        }

                        //(!string.IsNullOrEmpty(this.condition) ? false : !flag1);
                        //(string.IsNullOrEmpty(this.condition) ? false : !flag2);
					}
                    if (!string.IsNullOrEmpty(this.condition))
                    {
                        str = string.Concat("select isnull(_wfstate,'') state , _AutoId from ", this.tblName, " where ", this.condition);
                        DataTable dataTable = SysDatabase.ExecuteTable(str);
                        if (dataTable.Rows.Count > 0)
                        {
                            item = dataTable.Rows[0]["state"];
                            if ((item == null ? false : item != DBNull.Value))
                            {
                                if ((item.ToString() == "" ? false : item.ToString() != EnumDescription.GetFieldText(InstanceState.Ready)))
                                {
                                    string str1 = dataTable.Rows[0]["_AutoId"].ToString();
                                    if (InstanceService.IsRunAlready(this.tblName, str1))
                                    {
                                        this.Session["_sysinfo"] = "本条记录已经有审批数据，不能修改";
                                        base.Server.Transfer("AppInfo.aspx", true);
                                        //base.Server.Transfer(string.Format("AppWorkFlowInfo.aspx?tblName={0}&mainId={1}", this.tblName, str1), false);
                                    }
                                }
                            }
                        }
                        if (__TableInfo.FieldExists("_gdstate"))
                        {
                            str = string.Concat("select isnull(_gdstate,'') from ", this.tblName, " where ", this.condition);
                            item = SysDatabase.ExecuteScalar(str);
                            if (item != null && item.ToString() == "已归档")
                            {
                                this.Session["_sysinfo"] = "本条记录已经归档，不能修改数据";
                                base.Server.Transfer("AppInfo.aspx", true);
                            }
                        }
                    }
				}
				this.getTblHtml();
			}
			this.editScriptBlock = TableService.GetEditScriptBlock(this.tblName);
			if (this.editScriptBlock.Trim().Length > 0)
			{
				string paraValue1 = base.GetParaValue("ReplaceValue");
				if (paraValue1 != "")
				{
					ModelBuilder modelBuilder = new ModelBuilder(this);
					this.editScriptBlock = modelBuilder.ReplaceParaValue(paraValue1, this.editScriptBlock);
					this.editScriptBlock = modelBuilder.GetUbbCode(this.editScriptBlock, "[CRYPT]", "[/CRYPT]", base.UserName);
				}
				this.editScriptBlock = base.ReplaceContext(this.editScriptBlock);
			}
		}

		 [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void saveData(string tblNameList, string xmldata)
		{
            try
            {
                (new ModelSave(this)).SaveData(tblNameList, xmldata);
            }
            catch (Exception exception1)
            {
                if (exception1.Message != "") throw exception1;
                if ((exception1.Message == "") && (exception1.Data.Count > 0))
                {
                    string lastStackTrace = "";
                    foreach (var item in exception1.Data)
                    {
                        var entry = (DictionaryEntry)item;
                        lastStackTrace = exception1.Data[entry.Key].ToString();
                    }
                    AppInputAlertException alertExp = new AppInputAlertException(lastStackTrace);
                    throw alertExp;
                }
            }
		}

		 [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void StartTaskAll(string tblName, string parentId, string wfCode)
		{
			(new _TableInfo(tblName)).GetModel();
			try
			{
				Define workflowByCode = DefineService.GetWorkflowByCode(wfCode);
				if (workflowByCode == null)
				{
					throw new Exception(string.Concat("找不到有效的流程定义（流程编码：", wfCode, "）"));
				}
				string[] strArrays = new string[] { "select * from ", tblName, " where _MainID='", parentId, "'" };
				foreach (DataRow row in SysDatabase.ExecuteTable(string.Concat(strArrays)).Rows)
				{
					this.method_0(tblName, row, workflowByCode);
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

         [AjaxMethod(HttpSessionStateRequirement.Read)]     // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
         public string EncryptPara(string strpara)
         {
             return (Security.Encrypt(strpara));
         }

      
	}
}