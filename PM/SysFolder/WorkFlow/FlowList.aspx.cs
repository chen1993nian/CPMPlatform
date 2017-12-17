using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.WorkFlow
{
	public partial class FlowList : PageBase
	{
	

		public StringBuilder stringBuilder_0 = new StringBuilder();

		public StringBuilder stringBuilder_1 = new StringBuilder();

		public StringBuilder stringBuilder_2 = new StringBuilder();

		public StringBuilder sbLast = new StringBuilder();

		public StringBuilder sbFavorite = new StringBuilder();

		private DataTable dataTable_0 = new DataTable();

		public FlowList()
		{
		}

		public bool CheckConfigCondition(string workFlowCode)
		{
			bool flag;
			string str = string.Format("select * from T_E_WF_Config where Enable='是' and WFId='{0}'", workFlowCode, base.CompanyId);
			DataTable dataTable = SysDatabase.ExecuteTable(str);
			if (dataTable.Rows.Count != 0)
			{
				string str1 = dataTable.Rows[0]["condition2"].ToString();
				string str2 = dataTable.Rows[0]["companyId"].ToString();
				if (str2 == "")
				{
					if (str1 != "")
					{
						str1 = base.ReplaceContext(str1);
						flag = (int.Parse(SysDatabase.ExecuteScalar(string.Concat("select count(*) where ", str1)).ToString()) <= 0 ? false : true);
					}
					else
					{
						flag = true;
					}
				}
				else if (str2 != base.CompanyId)
				{
					flag = false;
				}
				else if (str1 != "")
				{
					str1 = base.ReplaceContext(str1);
					flag = (int.Parse(SysDatabase.ExecuteScalar(string.Concat("select count(*) where ", str1)).ToString()) <= 0 ? false : true);
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

		public int GetMaxOrder(string empId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select max(Odr) ");
			stringBuilder.Append(" From T_E_WF_Favorite ");
			stringBuilder.Append(string.Concat(" where _userName='", empId, "'"));
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
			return (obj == DBNull.Value ? 0 : Convert.ToInt32(obj));
		}

		public string getToDoNum(string tblName)
		{
			string str;
			string[] strArrays = new string[] { "select count(*) from ", tblName, " where  _UserName='", base.EmployeeID, "'" };
			string str1 = string.Concat(strArrays);
			try
			{
				string str2 = SysDatabase.ExecuteScalar(str1).ToString();
				str = (str2 == "0" ? "" : str2);
			}
			catch (Exception exception)
			{
				str = "";
			}
			return str;
		}

		private void method_0()
		{
			StringCollection stringCollections = new StringCollection();
			this.sbFavorite.AppendFormat("<dl class='dlstar'><dt class='star'>我收藏的流程</dt>", new object[0]);
			foreach (DataRow row in this.dataTable_0.Rows)
			{
				string str = row["WorkflowCode"].ToString();
				if (!this.CheckConfigCondition(str) || stringCollections.Contains(str))
				{
					continue;
				}
				stringCollections.Add(str);
				StringBuilder stringBuilder = this.sbFavorite;
				object[] item = new object[] { row["WorkflowName"], row["WorkflowId"], row["AppNames"], "", row["Version"], str };
				stringBuilder.AppendFormat("<dd wfcode='{5}'><em class='infav' wfcode='{5}'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</em><a href=\"javascript:newflow('{1}','{2}')\" >{0} V{4}</a><span onclick=\"javascript:openchart('{1}')\" class='flowchart'>(流程图)</span><span title='{3}个待发' onclick=\"javascript:openlist('{2}')\">{3}</span></dd>", item);
			}
			this.sbFavorite.AppendFormat("</dl>", new object[0]);
		}

		private void method_1()
		{
			StringCollection stringCollections = new StringCollection();
			string str = string.Format("select i.WorkflowId,d.WorkflowName,d.WorkflowCode,d.AppNames,d.Version from T_E_WF_Instance i inner join T_E_WF_Define d \r\n                on i.WorkflowId=d._AutoID where i._UserName='{0}' and d.Enabled='是' order by i._CreateTime desc", base.EmployeeID);
			DataTable dataTable = SysDatabase.ExecuteTable(str);
			this.sbLast.AppendFormat("<dl class='dllast'><dt class='clock'>最近使用的流程</dt>", new object[0]);
			int num = 0;
			IEnumerator enumerator = dataTable.Rows.GetEnumerator();
			try
			{
				do
				{
				Label0:
					if (!enumerator.MoveNext())
					{
						break;
					}
					DataRow current = (DataRow)enumerator.Current;
					string str1 = current["WorkflowCode"].ToString();
					if (this.CheckConfigCondition(str1) && !stringCollections.Contains(str1))
					{
						stringCollections.Add(str1);
						StringBuilder stringBuilder = this.sbLast;
						object[] item = new object[] { current["WorkflowName"], current["WorkflowId"], current["AppNames"], "", current["Version"] };
						stringBuilder.AppendFormat("<dd><em>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</em><a href=\"javascript:newflow('{1}','{2}')\" >{0} V{4}</a><span onclick=\"javascript:openchart('{1}')\" class='flowchart'>(流程图)</span><span title='{3}个待发' onclick=\"javascript:openlist('{2}')\">{3}</span></dd>", item);
						num++;
					}
					else
					{
						goto Label0;
					}
				}
				while (num != 20);
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			this.sbLast.AppendFormat("</dl>", new object[0]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			Define define = null;
			object[] workflowName;
			string str;
			AjaxPro.Utility.RegisterTypeForAjax(typeof(FlowList));
			this.method_1();
			string str1 = string.Format("select d._AutoId WorkflowId,d.WorkflowName,d.WorkflowCode,d.AppNames,d.Version from T_E_WF_Favorite f inner join T_E_WF_Define d \r\n                on f.wfcode = d.WorkflowCode where f._UserName='{0}' and d.Enabled='是' order by f.Odr", base.EmployeeID);
			this.dataTable_0 = SysDatabase.ExecuteTable(str1);
			this.method_0();
			int num = 0;
			foreach (Catalog allCatalog in CatalogService.GetAllCatalog())
			{
				string catalogName = allCatalog.CatalogName;
				string catalogId = allCatalog.CatalogId;
				if (allCatalog.IsDisp == 0)
				{
					continue;
				}
				List<Define> workflowDefineModeByCatCode = DefineService.GetWorkflowDefineModeByCatCode(allCatalog.CatalogCode);
				StringBuilder stringBuilder = new StringBuilder();
				if (workflowDefineModeByCatCode.Count <= 0)
				{
					continue;
				}
				if (num % 3 == 0)
				{
					foreach (Define define1 in workflowDefineModeByCatCode)
					{
						if (define1.Enabled == "否" || !this.CheckConfigCondition(define1.WorkflowCode))
						{
							continue;
						}
						str = "nofav";
						if ((int)this.dataTable_0.Select(string.Concat("WorkflowCode='", define1.WorkflowCode, "'")).Length > 0)
						{
							str = "infav";
						}
						string appNames = define1.AppNames;
						workflowName = new object[] { define1.WorkflowName, define1.WorkflowId, define1.AppNames, "", define1.Version, str, define1.WorkflowCode };
						stringBuilder.AppendFormat("<dd><em class='{5}' fav='{5}' wfcode='{6}' wfid='{1}' appname='{2}' wfname='{0} V{4}' title='点击可以收藏'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</em><a href=\"javascript:newflow('{1}','{2}')\" >{0} V{4}</a><span onclick=\"javascript:openchart('{1}')\" class='flowchart'>(流程图)</span><span title='{3}个待发' onclick=\"javascript:openlist('{2}')\">{3}</span></dd>", workflowName);
					}
					if (stringBuilder.Length <= 0)
					{
						continue;
					}
					this.stringBuilder_0.AppendFormat("<dl><dt>{0}</dt>", catalogName, num + 1);
					this.stringBuilder_0.Append(stringBuilder);
					this.stringBuilder_0.AppendFormat("</dl>", new object[0]);
					num++;
				}
				else if (num % 3 != 1)
				{
					if (num % 3 != 2)
					{
						continue;
					}
					foreach (Define defineA in workflowDefineModeByCatCode)
					{
                        if (defineA.Enabled == "否" || !this.CheckConfigCondition(defineA.WorkflowCode))
						{
							continue;
						}
						str = "nofav";
                        if ((int)this.dataTable_0.Select(string.Concat("WorkflowCode='", defineA.WorkflowCode, "'")).Length > 0)
						{
							str = "infav";
						}
                        string appNames1 = defineA.AppNames;
                        workflowName = new object[] { defineA.WorkflowName, defineA.WorkflowId, defineA.AppNames, "", defineA.Version, str, defineA.WorkflowCode };
						stringBuilder.AppendFormat("<dd><em class='{5}' fav='{5}' wfcode='{6}' wfid='{1}' appname='{2}' wfname='{0} V{4}' title='点击可以收藏'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</em><a href=\"javascript:newflow('{1}','{2}')\" >{0} V{4}</a><span onclick=\"javascript:openchart('{1}')\" class='flowchart'>(流程图)</span><span title='{3}个待发' onclick=\"javascript:openlist('{2}')\">{3}</span></dd>", workflowName);
					}
					if (stringBuilder.Length <= 0)
					{
						continue;
					}
					this.stringBuilder_2.AppendFormat("<dl><dt>{0}</dt>", catalogName, num + 1);
					this.stringBuilder_2.Append(stringBuilder);
					this.stringBuilder_2.AppendFormat("</dl>", new object[0]);
					num++;
				}
				else
				{
					foreach (Define define2 in workflowDefineModeByCatCode)
					{
						if (define2.Enabled == "否" || !this.CheckConfigCondition(define2.WorkflowCode))
						{
							continue;
						}
						str = "nofav";
						if ((int)this.dataTable_0.Select(string.Concat("WorkflowCode='", define2.WorkflowCode, "'")).Length > 0)
						{
							str = "infav";
						}
						string appNames2 = define2.AppNames;
						workflowName = new object[] { define2.WorkflowName, define2.WorkflowId, define2.AppNames, "", define2.Version, str, define2.WorkflowCode };
						stringBuilder.AppendFormat("<dd><em class='{5}' fav='{5}' wfcode='{6}' wfid='{1}' appname='{2}' wfname='{0} V{4}' title='点击可以收藏'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</em><a href=\"javascript:newflow('{1}','{2}')\" >{0} V{4}</a><span onclick=\"javascript:openchart('{1}')\" class='flowchart'>(流程图)</span><span title='{3}个待发' onclick=\"javascript:openlist('{2}')\">{3}</span></dd>", workflowName);
					}
					if (stringBuilder.Length <= 0)
					{
						continue;
					}
					this.stringBuilder_1.AppendFormat("<dl><dt>{0}</dt>", catalogName, num + 1);
					this.stringBuilder_1.Append(stringBuilder);
					this.stringBuilder_1.AppendFormat("</dl>", new object[0]);
					num++;
				}
			}
		}

        [AjaxMethod(HttpSessionStateRequirement.Read)]   // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public int UpdateFavorite(string empId, string wfCode, string flag)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			if (flag != "1")
			{
				stringBuilder.AppendFormat("delete T_E_WF_Favorite where _userName='{0}' and wfcode='{1}'", empId, wfCode);
				num = SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
			}
			else
			{
				int maxOrder = 0;
				maxOrder = this.GetMaxOrder(empId) + 1;
				stringBuilder.Append("\r\n                if not exists(select * from T_E_WF_Favorite where _userName=@_UserName and WFCode=@WFCode)\r\n                Insert T_E_WF_Favorite (\r\n \t\t\t\t\t    _AutoID,\r\n\t\t\t\t\t    _UserName,\r\n\t\t\t\t\t    _OrgCode,\r\n\t\t\t\t\t    _CreateTime,\r\n\t\t\t\t\t    _UpdateTime,\r\n\t\t\t\t\t    _IsDel,\r\n                        _CompanyId,\r\n                        WFCode,\r\n\t\t\t\t\t    Odr\r\n\t\t\t    ) values(\r\n\t\t\t\t\t    @_AutoID,\r\n\t\t\t\t\t    @_UserName,\r\n\t\t\t\t\t    @_OrgCode,\r\n\t\t\t\t\t    @_CreateTime,\r\n\t\t\t\t\t    @_UpdateTime,\r\n\t\t\t\t\t    @_IsDel,\r\n                        '',\r\n                        @WFCode,\r\n\t\t\t\t\t    @Odr\r\n\t\t\t    )");
				DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
				string str = Guid.NewGuid().ToString();
				SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, str);
				SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, base.EmployeeID);
				SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, base.OrgCode);
				SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, DateTime.Now);
				SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
				SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, 0);
				SysDatabase.AddInParameter(sqlStringCommand, "@WFCode", DbType.String, wfCode);
				SysDatabase.AddInParameter(sqlStringCommand, "@Odr", DbType.Int32, maxOrder);
				num = SysDatabase.ExecuteNonQuery(sqlStringCommand);
			}
			return num;
		}
	}
}