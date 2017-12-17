using AjaxPro;
using EIS.AppBase;
using EIS.AppModel;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using EIS.Permission;
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
	public partial class AppQuery : PageBase
	{
		public string colmodel = "";

		public string querymodel = "";

		public string sindex = "";

		public string sortname = "";

		public string sortorder = "";

		public string limit = "";

		public string condLimit = "0";

		public string layoutLimit = "0";

		public string exportLimit = "0";

		public string funId = "";

		public string bakUrl = "";

		public string tblname = "";

		public string listfn = "";

		public string preProcess = "";

		public string InitCond = "";

		public string customScript = "";

        public string PageRecCount = "15";

        public string PageRecOptions = "[10, 15, 20, 25, 30, 40]";

		
		[AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
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
                else {
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

        [AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
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

		protected void Page_Load(object sender, EventArgs e)
		{
			object[] fieldNameCn;
			ModelBuilder modelBuilder;
			AjaxPro.Utility.RegisterTypeForAjax(typeof(AppQuery));
			StringBuilder stringBuilder = new StringBuilder();
			this.tblname = base.GetParaValue("tblname");
			this.sindex = base.GetParaValue("sindex");
            base.AppIndex = this.sindex;
            this.InitCond = base.GetParaValue("InitCond");
			this.funId = base.GetParaValue("funId");
			if (this.funId.Length > 0)
			{
				this.bakUrl = EIS.Permission.Utility.GetFunAttrById(this.funId, "FunUrl");
				this.limit = EIS.Permission.Utility.GetFunLimitByEmployeeId(base.EmployeeID, this.funId);
				this.condLimit = (this.limit.Substring(4, 1) == "1" ? "0" : "1");
				this.layoutLimit = (this.limit.Substring(5, 1) == "1" ? "0" : "1");
				if (base.GetParaValue("hideExport") != "")
				{
					this.exportLimit = base.GetParaValue("hideExport");
				}
				else
				{
					this.exportLimit = (this.limit.Substring(6, 1) == "1" ? "0" : "1");
				}
			}
			this.customScript = base.GetCustomScript("ref_AppQuery");
			_FieldInfo __FieldInfo = new _FieldInfo();
			_FieldInfoExt __FieldInfoExt = new _FieldInfoExt();
			if (this.sindex == "")
			{
				foreach (FieldInfo fieldInfoA in __FieldInfo.GetModelListDisp(this.tblname))
				{
					StringBuilder stringBuilder1 = stringBuilder;
                    fieldNameCn = new object[] { fieldInfoA.FieldNameCn, fieldInfoA.FieldName, fieldInfoA.ColumnWidth, this.method_0(fieldInfoA.ColumnAlign), null, null, null, null };
                    fieldNameCn[4] = (fieldInfoA.ColumnHidden == 1 ? "true" : "false");
                    fieldNameCn[5] = (fieldInfoA.ColumnRender.Trim() == "" ? "false" : fieldInfoA.ColumnRender.Trim());
                    fieldNameCn[6] = fieldInfoA._AutoID;
                    fieldNameCn[7] = (fieldInfoA.FieldType == 5 ? "false" : "true");
					stringBuilder1.AppendFormat("{{display: '{0}', name : '{1}', width : {2}, sortable : {7}, align: '{3}',hide:{4},renderer:{5},fieldid:'{6}'}},", fieldNameCn);
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Length = stringBuilder.Length - 1;
				}
				this.colmodel = stringBuilder.ToString();
				stringBuilder.Length = 0;
				modelBuilder = new ModelBuilder(this);
				foreach (FieldInfo modelQueryDisp in __FieldInfo.GetModelQueryDisp(this.tblname))
				{
					stringBuilder.Append(modelBuilder.GetQueryModel(modelQueryDisp));
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Length = stringBuilder.Length - 1;
				}
				this.querymodel = stringBuilder.ToString();
			}
			else
			{
				foreach (FieldInfoExt modelListDispA in __FieldInfoExt.GetModelListDisp(this.tblname, this.sindex))
				{
					StringBuilder stringBuilder2 = stringBuilder;
                    fieldNameCn = new object[] { modelListDispA.FieldNameCn, modelListDispA.FieldName, modelListDispA.ColumnWidth, this.method_0(modelListDispA.ColumnAlign), null, null, null, null };
                    fieldNameCn[4] = (modelListDispA.ColumnHidden == 1 ? "true" : "false");
                    fieldNameCn[5] = (modelListDispA.ColumnRender.Trim() == "" ? "false" : modelListDispA.ColumnRender.Trim());
                    fieldNameCn[6] = modelListDispA._AutoID;
                    fieldNameCn[7] = (modelListDispA.FieldType == 5 ? "false" : "true");
					stringBuilder2.AppendFormat("{{display: '{0}', name : '{1}', width : {2}, sortable : {7}, align: '{3}',hide:{4},renderer:{5},fieldid:'{6}'}},", fieldNameCn);
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Length = stringBuilder.Length - 1;
				}
				this.colmodel = stringBuilder.ToString();
				stringBuilder.Length = 0;
				foreach (FieldInfoExt fieldInfoExt in __FieldInfoExt.GetModelQueryDisp(this.tblname, this.sindex))
				{
					stringBuilder.AppendFormat("{{display: '{0}', name : '{1}', type: {2}}},", fieldInfoExt.FieldNameCn, fieldInfoExt.FieldName, fieldInfoExt.FieldType);
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Length = stringBuilder.Length - 1;
				}
				this.querymodel = stringBuilder.ToString();
			}
			TableInfo model = (new _TableInfo(this.tblname)).GetModel();
            this.PageRecCount = model.PageRecCount.ToString();
            this.PageRecCount = (model.PageRecCount.ToString() == "0" ? "15" : model.PageRecCount.ToString());
            if (this.PageRecOptions.IndexOf(this.PageRecCount) == -1) this.PageRecOptions = this.PageRecOptions.Replace("40]", "40, " + this.PageRecCount + "]");
            if (model == null)
			{
				this.Session["_sysinfo"] = string.Format("参数 TblName = {0} 输入有错误，系统找不到对应的查询定义。", this.tblname);
				base.Response.Redirect("AppInfo.aspx?msgType=error", true);
			}
			if (base.GetParaValue("sortname") != "")
			{
				this.sortname = base.GetParaValue("sortname");
				this.sortorder = base.GetParaValue("sortorder");
			}
			else if (model.OrderField != "")
			{
				string[] strArrays = model.OrderField.Split(new char[] { ' ' });
				if ((int)strArrays.Length == 2)
				{
					this.sortname = strArrays[0];
					this.sortorder = strArrays[1];
				}
			}
			if (model != null)
			{
				this.listfn = model.ListScriptBlock;
				if (this.listfn.Trim().Length > 0)
				{
					string paraValue = base.GetParaValue("ReplaceValue");
					if (paraValue != "")
					{
						modelBuilder = new ModelBuilder(this);
						this.listfn = modelBuilder.ReplaceParaValue(paraValue, this.listfn);
						this.listfn = modelBuilder.GetUbbCode(this.listfn, "[CRYPT]", "[/CRYPT]", base.UserName);
					}
				}
				this.preProcess = (model.ListPreProcessFn.Trim() == "" ? "false" : model.ListPreProcessFn);
			}
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void saveLayout(ArrayList arrayList_0, string tblname, string sindex, string sortdir)
		{
			int i;
			string[] strArrays;
			object[] objArray;
			StringBuilder stringBuilder = new StringBuilder();
			if (sindex != "")
			{
				for (i = 0; i < arrayList_0.Count; i++)
				{
					strArrays = arrayList_0[i].ToString().Split("=".ToCharArray());
					if (strArrays[0] != "rowindex")
					{
						StringBuilder stringBuilder1 = stringBuilder;
						objArray = new object[] { i, strArrays[1], null, null, null };
						objArray[2] = (strArrays[2] == "none" ? 1 : 0);
						objArray[3] = tblname;
						objArray[4] = strArrays[0];
						stringBuilder1.AppendFormat("update T_E_Sys_FieldInfoExt set ColumnOrder={0} ,ColumnWidth={1}, ColumnHidden={2} where tablename='{3}' and _AutoId='{4}';", objArray);
					}
				}
			}
			else
			{
				for (i = 0; i < arrayList_0.Count; i++)
				{
					strArrays = arrayList_0[i].ToString().Split("=".ToCharArray());
					if (strArrays[0] != "rowindex")
					{
						StringBuilder stringBuilder2 = stringBuilder;
						objArray = new object[] { i, strArrays[1], null, null, null };
						objArray[2] = (strArrays[2] == "none" ? 1 : 0);
						objArray[3] = tblname;
						objArray[4] = strArrays[0];
						stringBuilder2.AppendFormat("update T_E_Sys_FieldInfo set ColumnOrder={0} ,ColumnWidth={1}, ColumnHidden={2} where tablename='{3}' and _AutoId='{4}';", objArray);
					}
				}
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