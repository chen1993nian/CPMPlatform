using AjaxPro;
using EIS.AppBase;
using EIS.AppModel;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.AppFrame
{
	public partial class AppOutSelect : PageBase
	{
		

		public string tblName = "";

		public string colmodel = "";

		public string listfn = "";

		public string querymodel = "";

		public string sortname = "";

		public string sortorder = "";

		public string queryid = "";

		public string InitCond = "";

		public string customScript = "";

        public string PageRecCount = "15";

        public string PageRecOptions = "[10, 15, 20, 25, 30, 40]";

		public AppOutSelect()
		{
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string ExecSQL(string scriptCode, string para)
		{
			string str;
			string tableSQLScript = TableService.GetTableSQLScript(scriptCode);
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
			else
			{
				str = "";
			}
			return str;
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public DataTable GetDataTable(string scriptCode, string para)
		{
			DataTable dataTable;
			string tableSQLScript = TableService.GetTableSQLScript(scriptCode);
			if (tableSQLScript != "")
			{
                if (TableService.HasTableSQLScriptCommandPara(para))
                {
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
			else
			{
				dataTable = null;
			}
			return dataTable;
		}

		private string method_0(string string_0)
		{
			string str;
			if (string_0 == "1")
			{
				str = "left";
			}
			else if (string_0 != "2")
			{
				str = (string_0 != "3" ? "left" : "right");
			}
			else
			{
				str = "center";
			}
			return str;
		}

		protected virtual void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(AppOutSelect));
			this.queryid = base.GetParaValue("queryid");
			this.InitCond = base.GetParaValue("InitCond");
			TableInfo tableInfo = new TableInfo();
			if (string.IsNullOrEmpty(this.queryid))
			{
				this.tblName = base.GetParaValue("tblName");
				tableInfo = (new _TableInfo(this.tblName)).GetModel();
				if (tableInfo == null)
				{
					this.Session["_sysinfo"] = string.Format("参数 tblName = {0} 有错误，系统找不到对应的业务定义。", this.tblName);
					base.Response.Redirect("AppInfo.aspx?msgType=error", true);
				}
                this.PageRecCount = tableInfo.PageRecCount.ToString();
                if (this.PageRecOptions.IndexOf(this.PageRecCount) == -1) this.PageRecOptions = this.PageRecOptions.Replace("40]", "40, " + this.PageRecCount + "]");
            }
			else
			{
				tableInfo = (new _TableInfo()).GetModelById(this.queryid);
				if (tableInfo == null)
				{
					this.Session["_sysinfo"] = string.Format("参数 QueryId = {0} 有错误，系统找不到对应的业务定义。", this.queryid);
					base.Response.Redirect("AppInfo.aspx?msgType=error", true);
				}
				this.tblName = tableInfo.TableName;
                this.PageRecCount = tableInfo.PageRecCount.ToString();
                this.PageRecCount = (tableInfo.PageRecCount.ToString() == "0" ? "15" : tableInfo.PageRecCount.ToString());
                if (this.PageRecOptions.IndexOf(this.PageRecCount) == -1) this.PageRecOptions = this.PageRecOptions.Replace("40]", "40, " + this.PageRecCount + "]");
            }


			this.customScript = base.GetCustomScript("ref_AppQuery");
			if (tableInfo.OrderField != "")
			{
				string[] strArrays = tableInfo.OrderField.Split(new char[] { ' ' });
				if ((int)strArrays.Length == 2)
				{
					this.sortname = strArrays[0];
					this.sortorder = strArrays[1];
				}
			}
			AjaxPro.Utility.RegisterTypeForAjax(typeof(AppOutSelect));
			StringBuilder stringBuilder = new StringBuilder();
			_FieldInfo __FieldInfo = new _FieldInfo();
			foreach (FieldInfo modelListDispA in __FieldInfo.GetModelListDisp(this.tblName))
			{
				StringBuilder stringBuilder1 = stringBuilder;
				object[] fieldNameCn = new object[] { modelListDispA.FieldNameCn, modelListDispA.FieldName, modelListDispA.ColumnWidth, this.method_0(modelListDispA.ColumnAlign), modelListDispA.ColumnHidden, null, null };
				fieldNameCn[5] = (modelListDispA.ColumnRender.Trim() == "" ? "false" : modelListDispA.ColumnRender.Trim());
				fieldNameCn[6] = modelListDispA._AutoID;
				stringBuilder1.AppendFormat("{{display: '{0}', name : '{1}', width : {2}, sortable : true, align: '{3}',hide:{4},renderer:{5},fieldid:'{6}'}},", fieldNameCn);
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length = stringBuilder.Length - 1;
			}
			this.colmodel = stringBuilder.ToString();
			stringBuilder.Length = 0;
			ModelBuilder modelBuilder = new ModelBuilder(this);
			foreach (FieldInfo modelQueryDisp in __FieldInfo.GetModelQueryDisp(this.tblName))
			{
				stringBuilder.Append(modelBuilder.GetQueryModel(modelQueryDisp));
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length = stringBuilder.Length - 1;
			}
			this.querymodel = stringBuilder.ToString();
			if (tableInfo != null)
			{
				this.listfn = tableInfo.ListScriptBlock;
				if (this.listfn.Trim().Length > 0)
				{
					string paraValue = base.GetParaValue("ReplaceValue");
					if (paraValue != "")
					{
						this.listfn = modelBuilder.ReplaceParaValue(paraValue, this.listfn);
						this.listfn = modelBuilder.GetUbbCode(this.listfn, "[CRYPT]", "[/CRYPT]", base.UserName);
					}
				}
			}
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void saveLayout(ArrayList arrayList_0, string tblname, string sortdir)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < arrayList_0.Count; i++)
			{
				string[] strArrays = arrayList_0[i].ToString().Split("=".ToCharArray());
				StringBuilder stringBuilder1 = stringBuilder;
				object[] objArray = new object[] { i, strArrays[1], null, null, null };
				objArray[2] = (strArrays[2] == "none" ? 1 : 0);
				objArray[3] = tblname;
				objArray[4] = strArrays[0];
				stringBuilder1.AppendFormat("update T_E_Sys_FieldInfo set ColumnOrder={0} ,ColumnWidth={1}, ColumnHidden={2} where tablename='{3}' and _autoid='{4}';", objArray);
			}
			if (sortdir != "")
			{
				stringBuilder.AppendFormat("update T_E_Sys_TableInfo set OrderField='{1}' where tableName='{0}'", tblname, sortdir);
			}
			if (stringBuilder.Length > 0)
			{
				SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
			}
		}
	}
}