using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Text;
using EIS.AppDAC;
namespace EIS.DataModel.Service
{
	public class TableService
	{
		public TableService()
		{
		}

		public static string GetEditScriptBlock(string tblName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select EditScriptBlock from T_E_Sys_TableInfo ");
			stringBuilder.Append(" where TableName=@TableName ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, tblName);
			object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
			return (obj == null ? "" : obj.ToString());
		}

		public static string GetTableSQLScript(string scriptCode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select ScriptTxt from T_E_Sys_TableScript ");
            stringBuilder.Append(" where ScriptCode=@ScriptCode and Enable='是' ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@ScriptCode", DbType.String, scriptCode);
			object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
			return (obj == null ? "" : obj.ToString());
		}

		public static bool ImportTable(string tblName, string catCode, string empId, string orgCode)
		{
			bool flag = true;
			StringDictionary stringDictionaries = new StringDictionary()
			{
				{ "_autoid", "_autoid" },
				{ "_username", "_username" },
				{ "_orgcode", "_orgcode" },
				{ "_createtime", "_createtime" },
				{ "_updatetime", "_updatetime" },
				{ "_isdel", "_isdel" }
			};
			string str = string.Concat("select * from ", tblName);
			DataTable schemaTable = SysDatabase.ExecuteReader(str).GetSchemaTable();
			DbConnection dbConnection = SysDatabase.CreateConnection();
			dbConnection.Open();
			DbTransaction dbTransaction = dbConnection.BeginTransaction();
			try
			{
				try
				{
					_TableInfo __TableInfo = new _TableInfo(tblName, dbTransaction);
					if (__TableInfo.Exists())
					{
						throw new Exception(string.Concat(tblName, "已经存在"));
					}
					TableInfo tableInfo = new TableInfo()
					{
						_AutoID = Guid.NewGuid().ToString(),
						_UserName = empId,
						_OrgCode = orgCode,
						_CreateTime = DateTime.Now,
						_UpdateTime = DateTime.Now,
						_IsDel = 0,
						TableName = tblName,
						TableNameCn = tblName,
                        ListSQL = string.Concat("select * from ", tblName, " where |^condition^| |^sortdir^|"),
                        DetailSQL = string.Concat("select * from ", tblName, " where |^condition^|"),
						DataLog = 1,
						DataLogTmpl = "",
						ShowState = 1,
						DeleteMode = 0,
						TableCat = catCode,
						ParentName = "",
						TableType = 1
					};
					tableInfo.DataLog = 0;
					__TableInfo.Add(tableInfo);
					int num = 0;
					_FieldInfo __FieldInfo = new _FieldInfo(dbTransaction);
					foreach (DataRow row in schemaTable.Rows)
					{
						string str1 = row["ColumnName"].ToString();
						string str2 = TableService.smethod_0(schemaTable.Select(string.Concat("ColumnName='", str1, "'"))[0]["DataTypeName"].ToString());
						if (stringDictionaries.ContainsKey(str1.ToLower()))
						{
							continue;
						}
						FieldInfo fieldInfo = new FieldInfo()
						{
							_AutoID = Guid.NewGuid().ToString(),
							_OrgCode = orgCode,
							_UserName = empId,
							_CreateTime = DateTime.Now,
							_UpdateTime = DateTime.Now,
							_IsDel = 0,
							TableName = tblName,
							FieldDValueType = "0",
							FieldDValue = "",
							FieldInDisp = 1,
							IsUnique = 0,
							FieldName = str1,
							FieldNameCn = str1,
							FieldType = Convert.ToInt32(str2),
							FieldLength = TableService.smethod_1(row),
                            ListDisp = (Convert.ToInt32(str2) < 5 ? 1 : 0),
                            QueryDisp = (Convert.ToInt32(str2) == 1 || Convert.ToInt32(str2) == 4 ? 1 : 0),
							ColumnAlign = "left",
							ColumnWidth = "80",
							ColumnRender = "",
							ColFormatExp = "",
							ColTotalExp = ""
						};
						int num1 = num + 1;
						num = num1;
						fieldInfo.FieldOdr = num1;
						fieldInfo.IsComput = 0;
						__FieldInfo.Add(fieldInfo);
					}
					dbTransaction.Commit();
					flag = true;
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					dbTransaction.Rollback();
					flag = false;
					throw exception;
				}
			}
			finally
			{
				dbConnection.Close();
			}
			return flag;
		}

        public static Boolean HasTableSQLScriptCommandPara(string tableSQLPara)
        {
            Boolean IsPara1 = false;
            string[] strArrays = tableSQLPara.Split(new char[] { '|' });
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                string str = strArrays[i];
                string[] strArrays1 = str.Split("=".ToCharArray(), 2);
                IsPara1 = (strArrays1[0].Contains("@int_") || strArrays1[0].Contains("@str_") || strArrays1[0].Contains("@date_") || strArrays1[0].Contains("@num_"));
                if (IsPara1) break;
            }
            return (IsPara1);
        }

		private static string smethod_0(string string_0)
		{
			string str;
			int num;
			string string0 = string_0;
			if (string0 != null)
			{
				if (Class0.dictionary_0 == null)
				{
					Class0.dictionary_0 = new Dictionary<string, int>(6)
					{
						{ "int", 0 },
						{ "datetime", 1 },
						{ "varchar", 2 },
						{ "nvarchar", 3 },
						{ "decimal", 4 },
						{ "text", 5 }
					};
				}
				if (Class0.dictionary_0.TryGetValue(string0, out num))
				{
					switch (num)
					{
						case 0:
						{
							str = "2";
							break;
						}
						case 1:
						{
							str = "4";
							break;
						}
						case 2:
						case 3:
						{
							str = "1";
							break;
						}
						case 4:
						{
							str = "3";
							break;
						}
						case 5:
						{
							str = "5";
							break;
						}
						default:
						{
							str = "1";
							return str;
						}
					}
				}
				else
				{
					str = "1";
					return str;
				}
			}
			else
			{
				str = "1";
				return str;
			}
			return str;
		}

		private static string smethod_1(DataRow dataRow_0)
		{
			string str;
			int num;
			string str1 = dataRow_0["DataTypeName"].ToString();
			if (str1 != null)
			{
				if (Class0.dictionary_1 == null)
				{
					Class0.dictionary_1 = new Dictionary<string, int>(6)
					{
						{ "int", 0 },
						{ "datetime", 1 },
						{ "varchar", 2 },
						{ "nvarchar", 3 },
						{ "decimal", 4 },
						{ "text", 5 }
					};
				}
				if (Class0.dictionary_1.TryGetValue(str1, out num))
				{
					switch (num)
					{
						case 0:
						{
							str = "";
							break;
						}
						case 1:
						{
							str = "";
							break;
						}
						case 2:
						case 3:
						{
							str = dataRow_0["ColumnSize"].ToString();
							break;
						}
						case 4:
						{
							str = string.Concat(dataRow_0["NumericPrecision"].ToString(), ",", dataRow_0["NumericScale"].ToString());
							break;
						}
						case 5:
						{
							str = "";
							break;
						}
						default:
						{
							str = "";
							return str;
						}
					}
				}
				else
				{
					str = "";
					return str;
				}
			}
			else
			{
				str = "";
				return str;
			}
			return str;
		}
	}
}