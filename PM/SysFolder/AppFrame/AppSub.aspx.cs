using AjaxPro;
using EIS.AppBase;
using EIS.AppModel;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace EIS.Web.SysFolder.AppFrame
{
	public  partial class AppSub : PageBase
	{
		

		public StringBuilder sbModel = new StringBuilder();

		public XmlDocument xmlDoc = new XmlDocument();

		public string tblName = "";

		public string mainTblName = "";

		public string tblHTML = "";

		public string rowId = "";

		public string sIndex = "";

		public string copyId = "";

		public string editScriptBlock = "";

		public string mainId = "";

		public string subId = "";

		public string isNew = "";

		public string xmlData = "";

		public string customScript = "";

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

		public AppSub()
		{
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]   // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string ExecSQL(string scriptCode, string para)
		{
			string str;
			string tableSQLScript = TableService.GetTableSQLScript(scriptCode);
			if (tableSQLScript != "")
			{
				tableSQLScript = base.ReplaceContext(tableSQLScript);
				tableSQLScript = EIS.AppBase.Utility.ReplaceParaValues(tableSQLScript, para);
				object obj = SysDatabase.ExecuteScalar(tableSQLScript);
				str = (obj == null ? "" : obj.ToString());
			}
			else
			{
				str = "";
			}
			return str;
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]   // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
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

		[AjaxMethod(HttpSessionStateRequirement.Read)]   // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
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

		[AjaxMethod(HttpSessionStateRequirement.Read)]   // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
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

		[AjaxMethod(HttpSessionStateRequirement.Read)]   // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
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

		protected void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(AppSub));
			this.tblName = base.GetParaValue("tblName").Trim();
			this.rowId = base.GetParaValue("rowId").Trim();
			this.mainId = base.GetParaValue("mainId").Trim();
			this.subId = base.GetParaValue("subId").Trim();
			this.copyId = base.GetParaValue("copyId").Trim();
			this.sIndex = base.GetParaValue("sIndex");
			this.customScript = base.GetCustomScript("ref_AppInput");
			this.isNew = (this.subId.Trim() == "" ? "true" : "false");
			XmlDeclaration xmlDeclaration = this.xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
			this.xmlDoc.AppendChild(xmlDeclaration);
			ModelBuilder modelBuilder = new ModelBuilder(this)
			{
				Sindex = this.sIndex,
				DataControl = new List<DataControl>(),
				DataContolFirst = false,
				ReplaceValue = ""
			};
			this.tblHTML = modelBuilder.GetSubHtml(this.tblName, this.mainId, this.subId, this.copyId, this.sbModel, this.xmlDoc);
			if (modelBuilder.MainRow != null)
			{
				this.subId = modelBuilder.MainRow["_AutoID"].ToString();
			}
			this.xmlData = this.xmlDoc.DocumentElement.OuterXml;
			this.editScriptBlock = TableService.GetEditScriptBlock(this.tblName);
			if (this.editScriptBlock.Trim().Length > 0)
			{
				string paraValue = base.GetParaValue("ReplaceValue");
				if (paraValue != "")
				{
					ModelBuilder modelBuilder1 = new ModelBuilder(this);
					this.editScriptBlock = modelBuilder1.ReplaceParaValue(paraValue, this.editScriptBlock);
					this.editScriptBlock = modelBuilder1.GetUbbCode(this.editScriptBlock, "[CRYPT]", "[/CRYPT]", base.UserName);
				}
				this.editScriptBlock = base.ReplaceContext(this.editScriptBlock);
			}
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]   // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void saveData(string tblNameList, string mainId, string xmldata)
		{
			try
			{
				(new ModelSave(this)).SaveSub(tblNameList, mainId, xmldata);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				this.fileLogger.Error<Exception>(exception);
				throw exception;
			}
		}
	}
}