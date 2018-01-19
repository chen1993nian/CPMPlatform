using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace EIS.AppModel
{
	public class ModelSave
	{
		private DbConnection _conn = null;

		private PageBase _page = null;

		private Logger fileLogger = null;

		public ModelSave(PageBase p)
		{
			this.fileLogger = LogManager.GetCurrentClassLogger();
			this._page = p;
		}

		private bool DeleteData(string tblname, string mainid)
		{
			_TableInfo __TableInfo = new _TableInfo(tblname);
			TableInfo tableInfo = new TableInfo();
			tableInfo = __TableInfo.GetModel();
			(new _FieldInfo()).GetTablePhyFields(tableInfo.TableName);
			List<TableInfo> subTable = __TableInfo.GetSubTable();
			if (tableInfo == null)
			{
				throw new Exception("找不到业务定义");
			}
			this._conn = SysDatabase.CreateConnection();
			this._conn.Open();
			DbTransaction dbTransaction = this._conn.BeginTransaction();
			try
			{
				try
				{
					DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(this.GenDeleteCmd(tblname, mainid));
					SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
					foreach (TableInfo tableInfo1 in subTable)
					{
						string tableName = tableInfo1.TableName;
						string[] strArrays = new string[] { "delete ", tableName, " where mainid='", mainid, "'" };
						sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
						SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
					}
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
				this._conn.Close();
			}
			return true;
		}

		private bool ExecCode(TableInfo model, string method, object[] args)
		{
			return true;
		}

		private void ExecScript(string tblName, int iEvent, DataRow bizData, DbTransaction dbTran)
		{
			object[] objArray;
			if (!(tblName.ToLower() == "t_e_sys_tabledll"))
			{
				string str = "";
				int num = 1;
				if (iEvent == 1)
				{
					str = "SaveBefore";
					num = 1;
				}
				else if (iEvent == 2)
				{
					str = "SaveAfter";
					num = 1;
				}
				else if (iEvent == 3)
				{
					str = "SaveBefore";
					num = 2;
				}
				else if (iEvent == 4)
				{
					str = "SaveAfter";
					num = 2;
				}
				else if (iEvent == 5)
				{
					str = "DeleteBefore";
				}
				else if (iEvent == 6)
				{
					str = "DeleteAfter";
				}
				string str1 = string.Format("select * from T_E_Sys_TableDll where Enable='是' and TableName='{0}' and ScriptEvent='{1}'", tblName, str);
				DataTable dataTable = SysDatabase.ExecuteTable(str1);
				if (dataTable.Rows.Count != 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						string str2 = string.Concat(Utility.GetPhysicalRootPath(), "\\bin\\", row["FilePath"].ToString());
						string str3 = row["ClassName"].ToString();
						string str4 = row["MethodName"].ToString();
                        try
                        {
                            if (!File.Exists(str2))
                            {
                                throw new Exception(string.Format("执行DLL逻辑时出错，文件[{0}]不存在", str2));
                            }
                            Type type = Assembly.LoadFrom(str2).GetType(str3);
                            object obj = Activator.CreateInstance(type);
                            MethodInfo method = type.GetMethod(str4);
                            if (str == "SaveBefore")
                            {
                                objArray = new object[] { tblName, bizData, num, dbTran };
                                method.Invoke(obj, objArray);
                            }
                            else if (str == "SaveAfter")
                            {
                                objArray = new object[] { tblName, bizData, num, dbTran };
                                method.Invoke(obj, objArray);
                            }
                            else if (str == "DeleteBefore")
                            {
                                objArray = new object[] { tblName, bizData, dbTran };
                                method.Invoke(obj, objArray);
                            }
                            else if (str == "DeleteAfter")
                            {
                                objArray = new object[] { tblName, bizData, dbTran };
                                method.Invoke(obj, objArray);
                            }
                        }
                        catch (Exception exception1)
                        {
                            Exception exception = exception1;
                            if (exception.InnerException == null)
                            {
                                this.fileLogger.Error(string.Concat("调用外部组件（", str2, "）出错：", exception.Message));
                                this.fileLogger.Error<Exception>(exception);
                                throw new Exception(exception.Message);
                            }
                            this.fileLogger.Error(string.Concat("调用外部组件（", str2, "）出错：", exception.InnerException.Message));
                            this.fileLogger.Error<Exception>(exception.InnerException);
                            throw exception.InnerException;
                        }
					}
				}
			}
		}

		private string FormatData(object data)
		{
			string str;
			str = (!(data.GetType().FullName == "System.DateTime") ? data.ToString() : Convert.ToDateTime(data).ToString("yyyy-MM-dd"));
			return str;
		}

		private string GenDeleteCmd(string tblname, string autoid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("delete {0} where _AutoID='{1}' ", tblname, autoid);
			return stringBuilder.ToString();
		}

		private string GenInsertCmd(string tblName, List<EIS.DataModel.Model.FieldInfo> dtfields, StringCollection paraHash)
		{
			int i;
			EIS.DataModel.Model.FieldInfo dtfield = null;
			char[] chrArray;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("insert {0} (", tblName);
			for (i = 0; i < paraHash.Count; i++)
			{
				string item = paraHash[i];
				chrArray = new char[] { '|' };
				string str = item.Split(chrArray)[0];
				stringBuilder.AppendFormat("{0},", str);
			}
			foreach (EIS.DataModel.Model.FieldInfo dtfieldA in dtfields)
			{
                stringBuilder.AppendFormat("[{0}],", dtfieldA.FieldName);
			}
			stringBuilder.Length = stringBuilder.Length - 1;
			stringBuilder.Append(") values (");
			for (i = 0; i < paraHash.Count; i++)
			{
				string item1 = paraHash[i];
				chrArray = new char[] { '|' };
				string str1 = item1.Split(chrArray)[1];
				stringBuilder.AppendFormat("{0},", str1);
			}
			foreach (EIS.DataModel.Model.FieldInfo fieldInfo in dtfields)
			{
				stringBuilder.AppendFormat("@{0},", fieldInfo.FieldName);
			}
			stringBuilder.Length = stringBuilder.Length - 1;
			stringBuilder.Append(");");
			return stringBuilder.ToString();
		}

		private DbParameter GenParameter(DbCommand command, EIS.DataModel.Model.FieldInfo rowdef, XmlElement fldnode)
		{
			DbParameter fieldName = command.CreateParameter();
			fieldName.ParameterName = rowdef.FieldName;
			switch (rowdef.FieldType)
			{
				case 1:
				{
					fieldName.DbType = DbType.String;
					fieldName.Value = fldnode.InnerText;
					break;
				}
				case 2:
				{
					fieldName.DbType = DbType.Int32;
					if (!(fldnode.InnerText.Trim() != ""))
					{
						fieldName.Value = DBNull.Value;
					}
					else
					{
						fieldName.Value = Convert.ToInt32(fldnode.InnerText);
					}
					break;
				}
				case 3:
				{
					fieldName.DbType = DbType.Decimal;
					if (!(fldnode.InnerText.Trim() != ""))
					{
						fieldName.Value = DBNull.Value;
					}
					else
					{
						fieldName.Value = Convert.ToDecimal(fldnode.InnerText);
					}
					break;
				}
				case 4:
				{
					fieldName.DbType = DbType.DateTime;
					if (!(fldnode.InnerText.Trim() == ""))
					{
						fieldName.Value = Convert.ToDateTime(fldnode.InnerText);
					}
					else
					{
						fieldName.Value = DBNull.Value;
					}
					break;
				}
				case 5:
				{
					fieldName.DbType = DbType.String;
					fieldName.Value = fldnode.InnerText;
					break;
				}
			}
			command.Parameters.Add(fieldName);
			return fieldName;
		}

		private string GenUpdateCmd(string tblname, List<EIS.DataModel.Model.FieldInfo> dtfields, string autoid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("update {0} set ", tblname);
			foreach (EIS.DataModel.Model.FieldInfo dtfield in dtfields)
			{
				stringBuilder.AppendFormat("[{0}]=@{0},", dtfield.FieldName);
			}
			stringBuilder.AppendFormat("{0}={1},", "_UpdateTime", "getdate()");
			stringBuilder.Length = stringBuilder.Length - 1;
			stringBuilder.AppendFormat(" where _AutoID='{0}'", autoid);
			return stringBuilder.ToString();
		}

		private string GetCmpFromRow(List<EIS.DataModel.Model.FieldInfo> dtfields, DataRow olddata, string nodename)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<{0} status='Deleted' id='{1}'>", nodename, olddata["_AutoID"]);
			foreach (EIS.DataModel.Model.FieldInfo dtfield in dtfields)
			{
				if (dtfield.FieldType < 5)
				{
					stringBuilder.AppendFormat("<{0} cn='{1}'><old><![CDATA[{2}]]></old></{0}>", dtfield.FieldName, dtfield.FieldNameCn, olddata[dtfield.FieldName]);
				}
			}
			stringBuilder.AppendFormat("</{0}>", nodename);
			return stringBuilder.ToString();
		}

		private string GetCmpFromXml(List<EIS.DataModel.Model.FieldInfo> dtfields, XmlElement newdata, string nodename, string autoId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<{0} status='Detached' id='{1}'>", nodename, autoId);
			foreach (EIS.DataModel.Model.FieldInfo dtfield in dtfields)
			{
				string innerText = newdata.SelectSingleNode(dtfield.FieldName).InnerText;
				if (dtfield.FieldType < 5)
				{
					stringBuilder.AppendFormat("<{0} cn='{1}'><new><![CDATA[{2}]]></new></{0}>", dtfield.FieldName, dtfield.FieldNameCn, innerText);
				}
			}
			stringBuilder.AppendFormat("</{0}>", nodename);
			return stringBuilder.ToString();
		}

		private string GetCmpLog(List<EIS.DataModel.Model.FieldInfo> dtfields, XmlElement newdata, DataRow olddata, string nodename)
		{
			string str;
			object[] item;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<{0} status='Modified' id='{1}'>", nodename, olddata["_autoid"]);
			bool flag = false;
			foreach (EIS.DataModel.Model.FieldInfo dtfield in dtfields)
			{
				string fieldName = dtfield.FieldName;
				string fieldNameCn = dtfield.FieldNameCn;
				int fieldType = dtfield.FieldType;
				string innerText = newdata.SelectSingleNode(fieldName).InnerText;
				if (fieldType < 5)
				{
					if (fieldType == 4)
					{
						try
						{
							DateTime dateTime = Convert.ToDateTime(innerText);
							if (olddata[fieldName] == DBNull.Value)
							{
								item = new object[] { fieldName, fieldNameCn, "", dateTime };
								stringBuilder.AppendFormat("<{0} cn='{1}'><old><![CDATA[{2}]]></old><new><![CDATA[{3}]]></new></{0}>", item);
								flag = true;
							}
							else if (dateTime.CompareTo(olddata[fieldName]) != 0)
							{
								item = new object[] { fieldName, fieldNameCn, olddata[fieldName], innerText };
								stringBuilder.AppendFormat("<{0} cn='{1}'><old><![CDATA[{2}]]></old><new><![CDATA[{3}]]></new></{0}>", item);
								flag = true;
							}
						}
						catch (FormatException formatException)
						{
							if (olddata[fieldName] != DBNull.Value)
							{
								item = new object[] { fieldName, fieldNameCn, olddata[fieldName], "" };
								stringBuilder.AppendFormat("<{0} cn='{1}'><old><![CDATA[{2}]]></old><new><![CDATA[{3}]]></new></{0}>", item);
								flag = true;
							}
						}
					}
					else if (olddata[fieldName].ToString() != innerText)
					{
						item = new object[] { fieldName, fieldNameCn, olddata[fieldName], innerText };
						stringBuilder.AppendFormat("<{0} cn='{1}'><old><![CDATA[{2}]]></old><new><![CDATA[{3}]]></new></{0}>", item);
						flag = true;
					}
				}
			}
			if (!flag)
			{
				str = "";
			}
			else
			{
				stringBuilder.AppendFormat("</{0}>", nodename);
				str = stringBuilder.ToString();
			}
			return str;
		}

		private string GetDbValue(object val)
		{
			return (val == DBNull.Value ? "" : val.ToString());
		}

		private int getMaxOrder(string fldName, string tblName, string condition)
		{
			string str = string.Format("select IsNull(Max({1}),0)+1 from {0} where {2}", tblName, fldName, condition);
			return Convert.ToInt32(SysDatabase.ExecuteScalar(str));
		}

		private XmlElement GetXmlNodeByFieldName(XmlElement row, string fldName)
		{
			XmlElement xmlElement = (XmlElement)row.SelectSingleNode(fldName);
			if (xmlElement == null)
			{
				xmlElement = (XmlElement)row.SelectSingleNode(string.Concat("*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz') = '", fldName.ToLower(), "']"));
			}
			return xmlElement;
		}

		private string PreSqlTrigger(string sql, DataRow data)
		{
			string str = Utility.DealCommandBySeesion(sql);
			foreach (Match match in (new Regex("\\{(.*?)\\}", RegexOptions.IgnoreCase)).Matches(str))
			{
				string value = match.Groups[1].Value;
				if (data.Table.Columns.Contains(value))
				{
					str = str.Replace(match.Value, data[value].ToString());
				}
			}
			return str;
		}

		private string PreSqlTrigger(string sql, XmlElement data)
		{
			string str = Utility.DealCommandBySeesion(sql);
			foreach (Match match in (new Regex("\\{(.*?)\\}", RegexOptions.IgnoreCase)).Matches(str))
			{
				string value = match.Groups[1].Value;
				XmlElement xmlElement = (XmlElement)data.SelectSingleNode(value);
				if (xmlElement != null)
				{
					str = str.Replace(match.Value, xmlElement.InnerText);
				}
			}
			return str;
		}

		public bool SaveData(string tblName, string xmldata)
		{
			bool flag=true;
            DbCommand sqlStringCommand;
            string innerText;
            TableInfo tableInfo = null;
            EIS.DataModel.Model.FieldInfo fieldInfo = null;
            string fieldName;
            string fieldNameCn;
            XmlElement xmlNodeByFieldName;
            DbTransaction dbTransaction;
            string tableName;
            XmlNodeList xmlNodeLists;
            XmlNode xmlNodes = null;
            StringCollection stringCollections;
            XmlElement xmlElement;
            DataRow row = null;
            DataLog dataLog;
            Exception exception;
            DataRow item;
            DataTable sQLTriger;
            DataTable dataTable;
            DbCommand dbCommand;
            string[] strArrays;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmldata);
            _TableInfo __TableInfo = new _TableInfo(tblName);
            TableInfo model = new TableInfo();
            model = __TableInfo.GetModel();
            Exception inputExp = null;

            List<EIS.DataModel.Model.FieldInfo> phyFields = __TableInfo.GetPhyFields();
            List<TableInfo> subTable = __TableInfo.GetSubTable();
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder();
            string dbValue = "";
            string str = "";
            if (model == null) throw new Exception("找不到业务定义");
            XmlElement xmlElement1 = (XmlElement)xmlDocument.SelectSingleNode(string.Concat("//Table[@TableName='", tblName, "']/row"));
            string attribute = xmlElement1.GetAttribute("state");
            if (attribute == "Detached")
            {
                #region 增加记录
                innerText = xmlElement1.SelectSingleNode("_AutoID").InnerText;
                strArrays = new string[] { "select count(*) from ", tblName, " where _AutoID='", innerText, "'" };
                if (Convert.ToInt32(SysDatabase.ExecuteScalar(string.Concat(strArrays))) > 0)
                {
                    StringBuilder stringBuilder2 = new StringBuilder();
                    stringBuilder2.AppendFormat("delete {0} where _AutoID='{1}' ;", tblName, innerText);
                    foreach (TableInfo tableInfo1 in subTable)
                    {
                        strArrays = new string[] { "delete ", tableInfo1.TableName, " where _MainID='", innerText, "';" };
                        stringBuilder2.Append(string.Concat(strArrays));
                    }
                    SysDatabase.ExecuteNonQuery(stringBuilder2.ToString());
                }
                foreach (EIS.DataModel.Model.FieldInfo fieldInfo1 in phyFields.FindAll((EIS.DataModel.Model.FieldInfo f) => f.IsUnique == 1))
                {
                    fieldName = fieldInfo1.FieldName;
                    fieldNameCn = fieldInfo1.FieldNameCn;
                    xmlNodeByFieldName = this.GetXmlNodeByFieldName(xmlElement1, fieldName);
                    strArrays = new string[] { "select count(*) from ", tblName, " where ", fieldName, "=@", fieldName };
                    sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
                    this.GenParameter(sqlStringCommand, fieldInfo1, xmlNodeByFieldName);
                    if (int.Parse(SysDatabase.ExecuteScalar(sqlStringCommand).ToString()) <= 0)
                    {
                        continue;
                    }
                    throw new Exception(string.Concat(fieldNameCn, "已经存在值为", xmlNodeByFieldName.InnerText, "的记录！"));
                }
                DataTable dataTable1 = SysDatabase.ExecuteTable(string.Concat("select * from ", tblName, " where 1=2"));
                DataRow orgCode = dataTable1.NewRow();
                orgCode["_AutoID"] = innerText;
                orgCode["_OrgCode"] = this._page.OrgCode;
                orgCode["_UserName"] = this._page.EmployeeID;
                orgCode["_CreateTime"] = DateTime.Now;
                orgCode["_UpdateTime"] = DateTime.Now;
                orgCode["_IsDel"] = 0;
                StringCollection stringCollections1 = new StringCollection();
                stringCollections1.Add(string.Concat("_AutoID|'", innerText, "'"));
                stringCollections1.Add(string.Concat("_OrgCode|'", this._page.OrgCode, "'"));
                stringCollections1.Add(string.Concat("_UserName|'", this._page.EmployeeID, "'"));
                stringCollections1.Add("_CreateTime|getdate()");
                stringCollections1.Add("_UpdateTime|getdate()");
                stringCollections1.Add("_IsDel|0");
                if (__TableInfo.FieldExists("_CompanyId"))
                {
                    stringCollections1.Add(string.Concat("_CompanyId|'", this._page.CompanyId, "'"));
                    orgCode["_CompanyId"] = this._page.CompanyId;
                }
                str = this.GenInsertCmd(tblName, phyFields, stringCollections1);
                sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
                foreach (EIS.DataModel.Model.FieldInfo phyField in phyFields)
                {
                    xmlNodeByFieldName = this.GetXmlNodeByFieldName(xmlElement1, phyField.FieldName);
                    this.fileLogger.Trace("生成参数：FieldName={0}", phyField.FieldName);
                    DbParameter dbParameter = this.GenParameter(sqlStringCommand, phyField, xmlNodeByFieldName);
                    orgCode[phyField.FieldName] = dbParameter.Value;
                }
                this._conn = SysDatabase.CreateConnection();
                this._conn.Open();
                dbTransaction = this._conn.BeginTransaction();
                try
                {
                    try
                    {
                        this.ExecScript(tblName, 1, orgCode, dbTransaction);
                        DataTable sQLTriger1 = __TableInfo.GetSQLTriger("AddA");
                        SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                        if (model.DataLog == 2)
                        {
                            stringBuilder.Append(this.GetCmpFromXml(phyFields, xmlElement1, tblName, innerText));
                        }
                        foreach (TableInfo tableInfo2 in subTable)
                        {
                            tableName = tableInfo2.TableName;
                            phyFields = (new _TableInfo(tableName)).GetPhyFields();
                            xmlNodeLists = xmlDocument.SelectNodes(string.Concat("//Table[@TableName='", tableName, "']/row"));
                            foreach (XmlNode xmlNodes1 in xmlNodeLists)
                            {
                                foreach (EIS.DataModel.Model.FieldInfo fieldInfo2 in phyFields.FindAll((EIS.DataModel.Model.FieldInfo f) => f.IsUnique == 1))
                                {
                                    fieldName = fieldInfo2.FieldName;
                                    fieldNameCn = fieldInfo2.FieldNameCn;
                                    xmlNodeByFieldName = (XmlElement)xmlNodes1.SelectSingleNode(fieldName);
                                    strArrays = new string[] { "select count(*) from ", tblName, " where ", fieldName, "=@", fieldName };
                                    sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
                                    this.GenParameter(sqlStringCommand, fieldInfo2, xmlNodeByFieldName);
                                    if (int.Parse(SysDatabase.ExecuteScalar(sqlStringCommand).ToString()) <= 0)
                                    {
                                        continue;
                                    }
                                    throw new Exception(string.Concat(fieldNameCn, "已经存在值为", xmlNodeByFieldName.InnerText, "的记录！"));
                                }
                                stringCollections = new StringCollection();
                                string str1 = Guid.NewGuid().ToString();
                                stringCollections.Add(string.Concat("_AutoID|'", str1, "'"));
                                stringCollections.Add(string.Concat("_OrgCode|'", this._page.OrgCode, "'"));
                                stringCollections.Add(string.Concat("_UserName|'", this._page.EmployeeID, "'"));
                                stringCollections.Add("_CreateTime|getdate()");
                                stringCollections.Add("_UpdateTime|getdate()");
                                stringCollections.Add("_IsDel|0");
                                stringCollections.Add(string.Concat("_MainID|'", innerText, "'"));
                                stringCollections.Add(string.Concat("_MainTbl|'", tblName, "'"));
                                string str2 = this.GenInsertCmd(tableName, phyFields, stringCollections);
                                sqlStringCommand = SysDatabase.GetSqlStringCommand(str2);
                                foreach (EIS.DataModel.Model.FieldInfo phyField1 in phyFields)
                                {
                                    xmlElement = (XmlElement)xmlNodes1.SelectSingleNode(phyField1.FieldName);
                                    this.GenParameter(sqlStringCommand, phyField1, xmlElement);
                                }
                                SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                                if (model.DataLog != 2)
                                {
                                    continue;
                                }
                                stringBuilder.Append(this.GetCmpFromXml(phyFields, (XmlElement)xmlNodes1, tableName, str1));
                            }
                        }
                        strArrays = new string[] { "select * from ", tblName, " where _AutoID='", innerText, "'" };
                        sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
                        DataRow dataRow = SysDatabase.ExecuteTable(sqlStringCommand, dbTransaction).Rows[0];
                        foreach (DataRow row1 in sQLTriger1.Rows)
                        {
                            dbValue = this.GetDbValue(row1["ScriptTxt"]);
                            if (dbValue == "")
                            {
                                continue;
                            }
                            str = string.Concat(this.PreSqlTrigger(dbValue, dataRow), ";");
                            sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
                            SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                        }
                        try
                        {
                            this.ExecScript(tblName, 2, dataRow, dbTransaction);
                        }
                        catch (Exception exception1)
                        {
                            inputExp = exception1;
                        }
                        if ((inputExp == null) || (inputExp.Message == ""))
                        {
                            dbTransaction.Commit();
                            if (model.DataLog == 1)
                            {
                                dataLog = new DataLog()
                                {
                                    AppID = innerText,
                                    AppName = tblName,
                                    ComputeIP = this._page.GetClientIP(),
                                    ServerIP = this._page.GetServerIP(),
                                    UserName = this._page.EmployeeName,
                                    LogType = "新建",
                                    LogUser = this._page.EmployeeID,
                                    ModuleCode = "",
                                    ModuleName = "",
                                    Message = ""
                                };
                                if (model.DataLogTmpl != "")
                                {
                                    dataLog.Message = this._page.ReplaceWithDataRow(model.DataLogTmpl, dataRow);
                                    dataLog.Message = this._page.ReplaceContext(dataLog.Message);
                                }
                                dataLog.Data = "";
                                this._page.dblogger.WriteDataLog(dataLog);
                            }
                            else if (model.DataLog == 2)
                            {
                                dataLog = new DataLog()
                                {
                                    AppID = innerText,
                                    AppName = tblName,
                                    ComputeIP = this._page.GetClientIP(),
                                    ServerIP = this._page.GetServerIP(),
                                    UserName = this._page.EmployeeName,
                                    LogType = "新建",
                                    LogUser = this._page.EmployeeID,
                                    ModuleCode = "",
                                    ModuleName = "",
                                    Message = ""
                                };
                                if (model.DataLogTmpl != "")
                                {
                                    dataLog.Message = this._page.ReplaceWithDataRow(model.DataLogTmpl, dataRow);
                                    dataLog.Message = this._page.ReplaceContext(dataLog.Message);
                                }
                                dataLog.Data = stringBuilder.ToString();
                                this._page.dblogger.WriteDataLog(dataLog);
                            }
                        }
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        dbTransaction.Rollback();
                        if (exception.InnerException == null)
                        {
                            throw exception;
                        }
                        throw exception.InnerException;
                    }
                }
                finally
                {
                    this._conn.Close();
                }
                #endregion
            }
            else if (attribute != "Deleted")
            {
                #region 修改数据
                this.fileLogger.Info("进入修改代码段");
                innerText = xmlElement1.SelectSingleNode("_AutoID").InnerText;
                this.fileLogger.Info<string, string>("修改记录：[{0}][{1}]", tblName, innerText);
                this._conn = SysDatabase.CreateConnection();
                this._conn.Open();
                dbTransaction = this._conn.BeginTransaction();
                try
                {
                    try
                    {
                        strArrays = new string[] { "select * from ", tblName, " where _AutoID='", innerText, "'" };
                        sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
                        item = SysDatabase.ExecuteTable(sqlStringCommand, dbTransaction).Rows[0];
                        this.ExecScript(tblName, 3, item, dbTransaction);
                        sQLTriger = __TableInfo.GetSQLTriger("ChangeB");
                        dataTable = __TableInfo.GetSQLTriger("ChangeA");
                        foreach (DataRow rows in sQLTriger.Rows)
                        {
                            dbValue = this.GetDbValue(rows["ScriptTxt"]);
                            if (dbValue == "")
                            {
                                continue;
                            }
                            str = string.Concat(this.PreSqlTrigger(dbValue, item), ";");
                            sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
                            SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                        }
                        if (attribute == "Modified")
                        {
                            //Convert.ToDateTime(xmlElement1.SelectSingleNode("_UpdateTime").InnerText).CompareTo(Convert.ToDateTime(item["_UpdateTime"].ToString())) != 0;
                            foreach (EIS.DataModel.Model.FieldInfo fieldInfoA in phyFields.FindAll((EIS.DataModel.Model.FieldInfo f) => f.IsUnique == 1))
                            {
                                fieldName = fieldInfoA.FieldName;
                                fieldNameCn = fieldInfoA.FieldNameCn;
                                xmlNodeByFieldName = this.GetXmlNodeByFieldName(xmlElement1, fieldName);
                                if (item[fieldName].ToString() == xmlNodeByFieldName.InnerText)
                                {
                                    continue;
                                }
                                strArrays = new string[] { "select count(*) from ", tblName, " where ", fieldName, "=@", fieldName };
                                sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
                                this.GenParameter(sqlStringCommand, fieldInfoA, xmlNodeByFieldName);
                                if (int.Parse(SysDatabase.ExecuteScalar(sqlStringCommand).ToString()) <= 0)
                                {
                                    continue;
                                }
                                throw new Exception(string.Concat(fieldNameCn, "已经存在值为", xmlNodeByFieldName.InnerText, "的记录！"));
                            }
                            stringBuilder.Append(this.GetCmpLog(phyFields, xmlElement1, item, tblName));
                            str = this.GenUpdateCmd(tblName, phyFields, innerText);
                            sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
                            foreach (EIS.DataModel.Model.FieldInfo phyField2 in phyFields)
                            {
                                xmlNodeByFieldName = (XmlElement)xmlElement1.SelectSingleNode(phyField2.FieldName);
                                this.GenParameter(sqlStringCommand, phyField2, xmlNodeByFieldName);
                            }
                            SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                        }
                        foreach (EIS.DataModel.Model.TableInfo tableInfoA in subTable)
                        {
                            tableName = tableInfoA.TableName;
                            phyFields = (new _TableInfo(tableName)).GetPhyFields();
                            xmlNodeLists = xmlDocument.SelectNodes(string.Concat("//Table[@TableName='", tableName, "']/row"));
                            foreach (XmlNode xmlNodesA in xmlNodeLists)
                            {
                                XmlElement xmlElement2 = (XmlElement)xmlNodesA;
                                string innerText1 = "";
                                string attribute1 = xmlElement2.GetAttribute("state");
                                if (attribute1 == null)
                                {
                                    continue;
                                }
                                if (attribute1 == "Detached")
                                {
                                    foreach (EIS.DataModel.Model.FieldInfo fieldInfo3 in phyFields.FindAll((EIS.DataModel.Model.FieldInfo f) => f.IsUnique == 1))
                                    {
                                        fieldName = fieldInfo3.FieldName;
                                        fieldNameCn = fieldInfo3.FieldNameCn;
                                        xmlNodeByFieldName = (XmlElement)xmlElement2.SelectSingleNode(fieldName);
                                        strArrays = new string[] { "select count(*) from ", tableName, " where ", fieldName, "=@", fieldName };
                                        dbCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
                                        this.GenParameter(dbCommand, fieldInfo3, xmlNodeByFieldName);
                                        if (int.Parse(SysDatabase.ExecuteScalar(dbCommand).ToString()) <= 0)
                                        {
                                            continue;
                                        }
                                        throw new Exception(string.Concat(fieldNameCn, "已经存在值为", xmlNodeByFieldName.InnerText, "的记录！"));
                                    }
                                    stringCollections = new StringCollection();
                                    innerText1 = Guid.NewGuid().ToString();
                                    stringCollections.Add(string.Concat("_AutoID|'", innerText1, "'"));
                                    stringCollections.Add(string.Concat("_OrgCode|'", this._page.OrgCode, "'"));
                                    stringCollections.Add(string.Concat("_UserName|'", this._page.EmployeeID, "'"));
                                    stringCollections.Add("_CreateTime|getdate()");
                                    stringCollections.Add("_UpdateTime|getdate()");
                                    stringCollections.Add("_IsDel|0");
                                    stringCollections.Add(string.Concat("_MainID|'", innerText, "'"));
                                    stringCollections.Add(string.Concat("_MainTbl|'", tblName, "'"));
                                    string str3 = this.GenInsertCmd(tableName, phyFields, stringCollections);
                                    DbCommand sqlStringCommand1 = SysDatabase.GetSqlStringCommand(str3);
                                    foreach (EIS.DataModel.Model.FieldInfo phyField3 in phyFields)
                                    {
                                        xmlElement = (XmlElement)xmlNodesA.SelectSingleNode(phyField3.FieldName);
                                        this.GenParameter(sqlStringCommand1, phyField3, xmlElement);
                                    }
                                    stringBuilder.Append(this.GetCmpFromXml(phyFields, xmlElement2, tableName, innerText1));
                                    SysDatabase.ExecuteNonQuery(sqlStringCommand1, dbTransaction);
                                }
                                else if (attribute1 == "Modified")
                                {
                                    innerText1 = xmlElement2.SelectSingleNode("_AutoID").InnerText;
                                    strArrays = new string[] { "select * from ", tableName, " where _AutoID='", innerText1, "'" };
                                    sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
                                    DataRow item1 = SysDatabase.ExecuteTable(sqlStringCommand, dbTransaction).Rows[0];
                                    foreach (EIS.DataModel.Model.FieldInfo fieldInfo4 in phyFields.FindAll((EIS.DataModel.Model.FieldInfo f) => f.IsUnique == 1))
                                    {
                                        fieldName = fieldInfo4.FieldName;
                                        fieldNameCn = fieldInfo4.FieldNameCn;
                                        xmlNodeByFieldName = (XmlElement)xmlElement2.SelectSingleNode(fieldName);
                                        if (item1[fieldName].ToString() == xmlNodeByFieldName.InnerText)
                                        {
                                            continue;
                                        }
                                        strArrays = new string[] { "select count(*) from ", tableName, " where ", fieldName, "=@", fieldName };
                                        dbCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
                                        this.GenParameter(dbCommand, fieldInfo4, xmlNodeByFieldName);
                                        if (int.Parse(SysDatabase.ExecuteScalar(dbCommand).ToString()) <= 0)
                                        {
                                            continue;
                                        }
                                        throw new Exception(string.Concat(fieldNameCn, "已经存在值为", xmlNodeByFieldName.InnerText, "的记录！"));
                                    }
                                    stringBuilder.Append(this.GetCmpLog(phyFields, xmlElement2, item1, tableName));
                                    string str4 = this.GenUpdateCmd(tableName, phyFields, innerText1);
                                    DbCommand dbCommand1 = SysDatabase.GetSqlStringCommand(str4);
                                    foreach (EIS.DataModel.Model.FieldInfo phyField4 in phyFields)
                                    {
                                        xmlElement = (XmlElement)xmlNodesA.SelectSingleNode(phyField4.FieldName);
                                        this.GenParameter(dbCommand1, phyField4, xmlElement);
                                    }
                                    SysDatabase.ExecuteNonQuery(dbCommand1, dbTransaction);
                                }
                                else
                                {
                                    if (attribute1 != "Deleted")
                                    {
                                        continue;
                                    }
                                    innerText1 = xmlElement2.SelectSingleNode("_AutoID").InnerText;
                                    strArrays = new string[] { "select * from ", tableName, " where _AutoID='", innerText1, "'" };
                                    sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
                                    DataRow dataRow1 = SysDatabase.ExecuteTable(sqlStringCommand, dbTransaction).Rows[0];
                                    stringBuilder.Append(this.GetCmpFromRow(phyFields, dataRow1, tableName));
                                    string str5 = this.GenDeleteCmd(tableName, innerText1);
                                    SysDatabase.ExecuteNonQuery(SysDatabase.GetSqlStringCommand(str5), dbTransaction);
                                }
                            }
                        }
                        strArrays = new string[] { "select * from ", tblName, " where _AutoID='", innerText, "'" };
                        sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
                        DataRow item2 = SysDatabase.ExecuteTable(sqlStringCommand, dbTransaction).Rows[0];
                        foreach (DataRow row2 in dataTable.Rows)
                        {
                            dbValue = this.GetDbValue(row2["ScriptTxt"]);
                            if (dbValue == "")
                            {
                                continue;
                            }
                            str = string.Concat(this.PreSqlTrigger(dbValue, item2), ";");
                            sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
                            SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                        }

                        try
                        {
                            this.ExecScript(tblName, 4, item2, dbTransaction);
                        }
                        catch (Exception exception1)
                        {
                            inputExp = exception1;
                        }
                        if ((inputExp == null) || (inputExp.Message == ""))
                        {
                            dbTransaction.Commit();
                            if (stringBuilder.Length > 0)
                            {
                                if (model.DataLog == 1)
                                {
                                    dataLog = new DataLog()
                                    {
                                        AppID = innerText,
                                        AppName = tblName,
                                        ComputeIP = this._page.GetClientIP(),
                                        ServerIP = this._page.GetServerIP(),
                                        UserName = this._page.EmployeeName,
                                        LogType = "修改",
                                        LogUser = this._page.EmployeeID,
                                        ModuleCode = "",
                                        ModuleName = "",
                                        Message = ""
                                    };
                                    if (model.DataLogTmpl != "")
                                    {
                                        dataLog.Message = this._page.ReplaceWithDataRow(model.DataLogTmpl, item);
                                        dataLog.Message = this._page.ReplaceContext(dataLog.Message);
                                    }
                                    dataLog.Data = "";
                                    this._page.dblogger.WriteDataLog(dataLog);
                                }
                                else if (model.DataLog == 2)
                                {
                                    dataLog = new DataLog()
                                    {
                                        AppID = innerText,
                                        AppName = tblName,
                                        ComputeIP = this._page.GetClientIP(),
                                        ServerIP = this._page.GetServerIP(),
                                        UserName = this._page.EmployeeName,
                                        LogType = "修改",
                                        LogUser = this._page.EmployeeID,
                                        ModuleCode = "",
                                        ModuleName = "",
                                        Message = ""
                                    };
                                    if (model.DataLogTmpl != "")
                                    {
                                        dataLog.Message = this._page.ReplaceWithDataRow(model.DataLogTmpl, item);
                                        dataLog.Message = this._page.ReplaceContext(dataLog.Message);
                                    }
                                    dataLog.Data = stringBuilder.ToString();
                                    this._page.dblogger.WriteDataLog(dataLog);
                                }
                            }
                        }
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        dbTransaction.Rollback();
                        this.fileLogger.Error<Exception>(exception);
                        if (exception.InnerException == null)
                        {
                            throw exception;
                        }
                        throw exception.InnerException;
                    }
                }
                finally
                {
                    this._conn.Close();
                }
                #endregion
            }
            else
            {
                #region 删除数据
                innerText = xmlElement1.SelectSingleNode("_AutoID").InnerText;
                this._conn = SysDatabase.CreateConnection();
                this._conn.Open();
                dbTransaction = this._conn.BeginTransaction();
                try
                {
                    try
                    {
                        strArrays = new string[] { "select * from ", tblName, " where _AutoID='", innerText, "'" };
                        sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
                        item = SysDatabase.ExecuteTable(sqlStringCommand, dbTransaction).Rows[0];
                        if (model.DataLog == 2)
                        {
                            stringBuilder.Append(this.GetCmpFromRow(phyFields, item, tblName));
                        }
                        sQLTriger = __TableInfo.GetSQLTriger("DeleteB");
                        dataTable = __TableInfo.GetSQLTriger("DeleteA");
                        foreach (DataRow dataRow2 in sQLTriger.Rows)
                        {
                            dbValue = this.GetDbValue(dataRow2["ScriptTxt"]);
                            if (dbValue == "")
                            {
                                continue;
                            }
                            str = string.Concat(this.PreSqlTrigger(dbValue, item), ";");
                            sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
                            SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                        }
                        this.ExecScript(tblName, 5, item, dbTransaction);
                        if (model.DeleteMode != 0)
                        {
                            strArrays = new string[] { "update ", tblName, " set _IsDel=1 where _AutoID='", innerText, "'" };
                            sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
                            SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                            foreach (TableInfo tableInfo3 in subTable)
                            {
                                tableName = tableInfo3.TableName;
                                strArrays = new string[] { "update ", tableName, " set _IsDel=1 where _MainID='", innerText, "'" };
                                sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
                                SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                            }
                        }
                        else
                        {
                            sqlStringCommand = SysDatabase.GetSqlStringCommand(this.GenDeleteCmd(tblName, innerText));
                            SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                            foreach (TableInfo tableInfo4 in subTable)
                            {
                                tableName = tableInfo4.TableName;
                                strArrays = new string[] { "delete ", tableName, " where _MainID='", innerText, "'" };
                                sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
                                SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                            }
                        }
                        foreach (DataRow row3 in dataTable.Rows)
                        {
                            dbValue = this.GetDbValue(row3["ScriptTxt"]);
                            if (dbValue == "")
                            {
                                continue;
                            }
                            str = string.Concat(this.PreSqlTrigger(dbValue, item), ";");
                            sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
                            SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                        }

                        try
                        {
                            this.ExecScript(tblName, 6, item, dbTransaction);
                        }
                        catch (Exception exception1)
                        {
                            inputExp = exception1;
                        }
                        if ((inputExp == null) || (inputExp.Message == ""))
                        {
                            dbTransaction.Commit();
                            if (model.DataLog == 1)
                            {
                                dataLog = new DataLog()
                                {
                                    AppID = innerText,
                                    AppName = tblName,
                                    ComputeIP = this._page.GetClientIP(),
                                    ServerIP = this._page.GetServerIP(),
                                    UserName = this._page.EmployeeName,
                                    LogType = "删除",
                                    LogUser = this._page.EmployeeID,
                                    ModuleCode = "",
                                    ModuleName = "",
                                    Message = ""
                                };
                                if (model.DataLogTmpl != "")
                                {
                                    dataLog.Message = this._page.ReplaceWithDataRow(model.DataLogTmpl, item);
                                    dataLog.Message = this._page.ReplaceContext(dataLog.Message);
                                }
                                dataLog.Data = "";
                                this._page.dblogger.WriteDataLog(dataLog);
                            }
                            else if (model.DataLog == 2)
                            {
                                dataLog = new DataLog()
                                {
                                    AppID = innerText,
                                    AppName = tblName,
                                    ComputeIP = this._page.GetClientIP(),
                                    ServerIP = this._page.GetServerIP(),
                                    UserName = this._page.EmployeeName,
                                    LogType = "删除",
                                    LogUser = this._page.EmployeeID,
                                    ModuleCode = "",
                                    ModuleName = "",
                                    Message = ""
                                };
                                if (model.DataLogTmpl != "")
                                {
                                    dataLog.Message = this._page.ReplaceWithDataRow(model.DataLogTmpl, item);
                                    dataLog.Message = this._page.ReplaceContext(dataLog.Message);
                                }
                                dataLog.Data = stringBuilder.ToString();
                                this._page.dblogger.WriteDataLog(dataLog);
                            }
                        }
                    }
                    catch (Exception exception3)
                    {
                        exception = exception3;
                        dbTransaction.Rollback();
                        if (exception.InnerException == null)
                        {
                            throw exception;
                        }
                        throw exception.InnerException;
                    }
                }
                finally
                {
                    this._conn.Close();
                }
                #endregion
            }
            if (inputExp != null) throw inputExp;
            return true;
		}

		private void SaveLog2File(string appName, string logId, string logData, DbTransaction dbTran)
		{
			DateTime now = DateTime.Now;
			string str = string.Concat(now.ToString("yyyy-MM-dd-HH-mm-ss-ffff", DateTimeFormatInfo.InvariantInfo), ".xml");
			now = DateTime.Now;
			string str1 = now.ToString("yyyy年MM月", DateTimeFormatInfo.InvariantInfo);
			if (!string.IsNullOrEmpty(appName))
			{
				str1 = string.Concat("T_E_Sys_LogData\\", appName, "\\", str1);
			}
			try
			{
				string appFileSavePath = AppSettings.Instance.AppFileSavePath;
				if (!Directory.Exists(string.Concat(appFileSavePath, str1)))
				{
					Directory.CreateDirectory(string.Concat(appFileSavePath, str1));
				}
				string str2 = string.Concat(appFileSavePath, str1, "\\", str);
				int length = 0;
				FileStream fileStream = new FileStream(str2, FileMode.Create);
				try
				{
					byte[] bytes = Encoding.UTF8.GetBytes(logData);
					length = (int)bytes.Length;
					fileStream.Write(bytes, 0, length);
					fileStream.Flush();
				}
				finally
				{
					if (fileStream != null)
					{
						((IDisposable)fileStream).Dispose();
					}
				}
				AppFile appFile = new AppFile()
				{
					_AutoID = logId,
					_UserName = Utility.GetSession("EmployeeID").ToString(),
					_OrgCode = Utility.GetSession("DeptWbs").ToString(),
					_CreateTime = DateTime.Now,
					_UpdateTime = DateTime.Now,
					_IsDel = 0,
					FileName = str,
					FactFileName = str,
					FilePath = string.Concat(str1, "\\", str),
					BasePath = AppSettings.Instance.AppFileBaseCode,
					DownCount = 0,
					FileSize = length,
					FileType = ".xml",
					FolderID = "",
					AppId = logId,
					AppName = "T_E_Sys_LogData"
				};
				(new _AppFile(dbTran)).Add(appFile);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				this.fileLogger.Error("写入数据日志时出错：{0}", exception.Message);
				throw exception;
			}
		}

		public bool SaveSub(string tblName, string mainId, string xmlData)
		{
			DbCommand sqlStringCommand;
			EIS.DataModel.Model.FieldInfo fieldInfo = null;
			string fieldName;
			string fieldNameCn;
			XmlElement xmlNodeByFieldName;
			DbTransaction dbTransaction;
			DataRow row = null;
			DataLog dataLog;
			Exception exception;
			string innerText;
			DataRow item;
			DataTable sQLTriger;
			DataTable dataTable;
			string[] strArrays;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xmlData);
			_TableInfo __TableInfo = new _TableInfo(tblName);
			TableInfo tableInfo = new TableInfo();
			tableInfo = __TableInfo.GetModel();
			List<EIS.DataModel.Model.FieldInfo> phyFields = __TableInfo.GetPhyFields();
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder1 = new StringBuilder();
			string dbValue = "";
			string str = "";
			if (tableInfo == null)
			{
				throw new Exception("找不到业务定义");
			}
			XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode(string.Concat("//Table[@TableName='", tblName, "']/row"));
			string attribute = xmlElement.GetAttribute("state");
			if (attribute == "Detached")
			{
				string innerText1 = xmlElement.SelectSingleNode("_AutoID").InnerText;
				foreach (EIS.DataModel.Model.FieldInfo fieldInfo1 in phyFields.FindAll((EIS.DataModel.Model.FieldInfo f) => f.IsUnique == 1))
				{
					fieldName = fieldInfo1.FieldName;
					fieldNameCn = fieldInfo1.FieldNameCn;
					xmlNodeByFieldName = this.GetXmlNodeByFieldName(xmlElement, fieldName);
					strArrays = new string[] { "select count(*) from ", tblName, " where ", fieldName, "=@", fieldName };
					sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
					this.GenParameter(sqlStringCommand, fieldInfo1, xmlNodeByFieldName);
					if (int.Parse(SysDatabase.ExecuteScalar(sqlStringCommand).ToString()) > 0)
					{
						strArrays = new string[] { "【", fieldNameCn, "】已经存在值为“", xmlNodeByFieldName.InnerText, "”的记录！" };
						throw new Exception(string.Concat(strArrays));
					}
				}
				DataTable dataTable1 = SysDatabase.ExecuteTable(string.Concat("select * from ", tblName, " where 1=2"));
				DataRow orgCode = dataTable1.NewRow();
				orgCode["_AutoID"] = innerText1;
				orgCode["_OrgCode"] = this._page.OrgCode;
				orgCode["_UserName"] = this._page.EmployeeID;
				orgCode["_CreateTime"] = DateTime.Now;
				orgCode["_UpdateTime"] = DateTime.Now;
				orgCode["_IsDel"] = 0;
				orgCode["_MainID"] = mainId;
				orgCode["_MainTbl"] = tableInfo.ParentName;
				StringCollection stringCollections = new StringCollection();
				stringCollections.Add(string.Concat("_AutoID|'", innerText1, "'"));
				stringCollections.Add(string.Concat("_OrgCode|'", this._page.OrgCode, "'"));
				stringCollections.Add(string.Concat("_UserName|'", this._page.EmployeeID, "'"));
				stringCollections.Add("_CreateTime|getdate()");
				stringCollections.Add("_UpdateTime|getdate()");
				stringCollections.Add("_IsDel|0");
				stringCollections.Add(string.Concat("_MainID|'", mainId, "'"));
				stringCollections.Add(string.Concat("_MainTbl|'", tableInfo.ParentName, "'"));
				str = this.GenInsertCmd(tblName, phyFields, stringCollections);
				sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
				foreach (EIS.DataModel.Model.FieldInfo phyField in phyFields)
				{
					xmlNodeByFieldName = this.GetXmlNodeByFieldName(xmlElement, phyField.FieldName);
					this.fileLogger.Trace("生成参数：FieldName={0}", phyField.FieldName);
					DbParameter dbParameter = this.GenParameter(sqlStringCommand, phyField, xmlNodeByFieldName);
					orgCode[phyField.FieldName] = dbParameter.Value;
				}
				this._conn = SysDatabase.CreateConnection();
				this._conn.Open();
				dbTransaction = this._conn.BeginTransaction();
				try
				{
					try
					{
						this.ExecScript(tblName, 1, orgCode, dbTransaction);
						DataTable sQLTriger1 = __TableInfo.GetSQLTriger("AddA");
						SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
						if (tableInfo.DataLog == 2)
						{
							stringBuilder.Append(this.GetCmpFromXml(phyFields, xmlElement, tblName, innerText1));
						}
						strArrays = new string[] { "select * from ", tblName, " where _AutoID='", innerText1, "'" };
						sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
						DataRow dataRow = SysDatabase.ExecuteTable(sqlStringCommand, dbTransaction).Rows[0];
						foreach (DataRow row1 in sQLTriger1.Rows)
						{
							dbValue = this.GetDbValue(row1["ScriptTxt"]);
							if (dbValue != "")
							{
								str = string.Concat(this.PreSqlTrigger(dbValue, dataRow), ";");
								sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
								SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
							}
						}
						this.ExecScript(tblName, 2, dataRow, dbTransaction);
						dbTransaction.Commit();
						if (tableInfo.DataLog == 1)
						{
							dataLog = new DataLog()
							{
								AppID = innerText1,
								AppName = tblName,
								ComputeIP = this._page.GetClientIP(),
								ServerIP = this._page.GetServerIP(),
								UserName = this._page.EmployeeName,
								LogType = "新建",
								LogUser = this._page.EmployeeID,
								ModuleCode = "",
								ModuleName = "",
								Message = ""
							};
							if (tableInfo.DataLogTmpl != "")
							{
								dataLog.Message = this._page.ReplaceWithDataRow(tableInfo.DataLogTmpl, dataRow);
								dataLog.Message = this._page.ReplaceContext(dataLog.Message);
							}
							dataLog.Data = "";
							this._page.dblogger.WriteDataLog(dataLog);
						}
						else if (tableInfo.DataLog == 2)
						{
							dataLog = new DataLog()
							{
								AppID = innerText1,
								AppName = tblName,
								ComputeIP = this._page.GetClientIP(),
								ServerIP = this._page.GetServerIP(),
								UserName = this._page.EmployeeName,
								LogType = "新建",
								LogUser = this._page.EmployeeID,
								ModuleCode = "",
								ModuleName = "",
								Message = ""
							};
							if (tableInfo.DataLogTmpl != "")
							{
								dataLog.Message = this._page.ReplaceWithDataRow(tableInfo.DataLogTmpl, dataRow);
								dataLog.Message = this._page.ReplaceContext(dataLog.Message);
							}
							dataLog.Data = stringBuilder.ToString();
							this._page.dblogger.WriteDataLog(dataLog);
						}
					}
					catch (Exception exception1)
					{
						exception = exception1;
						dbTransaction.Rollback();
						if (exception.InnerException == null)
						{
							throw exception;
						}
						throw exception.InnerException;
					}
				}
				finally
				{
					this._conn.Close();
				}
			}
			else if (!(attribute == "Deleted"))
			{
				this.fileLogger.Info("进入修改代码段");
				innerText = xmlElement.SelectSingleNode("_AutoID").InnerText;
				this.fileLogger.Info<string, string>("修改记录：[{0}][{1}]", tblName, innerText);
				this._conn = SysDatabase.CreateConnection();
				this._conn.Open();
				dbTransaction = this._conn.BeginTransaction();
				try
				{
					try
					{
						strArrays = new string[] { "select * from ", tblName, " where _AutoID='", innerText, "'" };
						sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
						item = SysDatabase.ExecuteTable(sqlStringCommand, dbTransaction).Rows[0];
						this.ExecScript(tblName, 3, item, dbTransaction);
						sQLTriger = __TableInfo.GetSQLTriger("ChangeB");
						dataTable = __TableInfo.GetSQLTriger("ChangeA");
						foreach (DataRow rowC in sQLTriger.Rows)
						{
                            dbValue = this.GetDbValue(rowC["ScriptTxt"]);
							if (dbValue != "")
							{
								str = string.Concat(this.PreSqlTrigger(dbValue, item), ";");
								sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
								SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
							}
						}
						if (attribute == "Modified")
						{
							if (Convert.ToDateTime(xmlElement.SelectSingleNode("_UpdateTime").InnerText).CompareTo(Convert.ToDateTime(item["_UpdateTime"].ToString())) != 0)
							{
							}
							foreach (EIS.DataModel.Model.FieldInfo fieldInfoA in phyFields.FindAll((EIS.DataModel.Model.FieldInfo f) => f.IsUnique == 1))
							{
                                fieldName = fieldInfoA.FieldName;
                                fieldNameCn = fieldInfoA.FieldNameCn;
								xmlNodeByFieldName = this.GetXmlNodeByFieldName(xmlElement, fieldName);
								if (!(item[fieldName].ToString() == xmlNodeByFieldName.InnerText))
								{
									strArrays = new string[] { "select count(*) from ", tblName, " where ", fieldName, "=@", fieldName };
									sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
                                    this.GenParameter(sqlStringCommand, fieldInfoA, xmlNodeByFieldName);
									if (int.Parse(SysDatabase.ExecuteScalar(sqlStringCommand).ToString()) > 0)
									{
										strArrays = new string[] { "【", fieldNameCn, "】已经存在值为“", xmlNodeByFieldName.InnerText, "”的记录！" };
										throw new Exception(string.Concat(strArrays));
									}
								}
							}
							stringBuilder.Append(this.GetCmpLog(phyFields, xmlElement, item, tblName));
							str = this.GenUpdateCmd(tblName, phyFields, innerText);
							sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
							foreach (EIS.DataModel.Model.FieldInfo phyField1 in phyFields)
							{
								xmlNodeByFieldName = (XmlElement)xmlElement.SelectSingleNode(phyField1.FieldName);
								this.GenParameter(sqlStringCommand, phyField1, xmlNodeByFieldName);
							}
							SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
						}
						strArrays = new string[] { "select * from ", tblName, " where _AutoID='", innerText, "'" };
						sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
						DataRow item1 = SysDatabase.ExecuteTable(sqlStringCommand, dbTransaction).Rows[0];
						foreach (DataRow dataRow1 in dataTable.Rows)
						{
							dbValue = this.GetDbValue(dataRow1["ScriptTxt"]);
							if (dbValue != "")
							{
								str = string.Concat(this.PreSqlTrigger(dbValue, item1), ";");
								sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
								SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
							}
						}
						this.ExecScript(tblName, 4, item1, dbTransaction);
						dbTransaction.Commit();
						if (stringBuilder.Length > 0)
						{
							if (tableInfo.DataLog == 1)
							{
								dataLog = new DataLog()
								{
									AppID = innerText,
									AppName = tblName,
									ComputeIP = this._page.GetClientIP(),
									ServerIP = this._page.GetServerIP(),
									UserName = this._page.EmployeeName,
									LogType = "修改",
									LogUser = this._page.EmployeeID,
									ModuleCode = "",
									ModuleName = "",
									Message = ""
								};
								if (tableInfo.DataLogTmpl != "")
								{
									dataLog.Message = this._page.ReplaceWithDataRow(tableInfo.DataLogTmpl, item);
									dataLog.Message = this._page.ReplaceContext(dataLog.Message);
								}
								dataLog.Data = "";
								this._page.dblogger.WriteDataLog(dataLog);
							}
							else if (tableInfo.DataLog == 2)
							{
								dataLog = new DataLog()
								{
									AppID = innerText,
									AppName = tblName,
									ComputeIP = this._page.GetClientIP(),
									ServerIP = this._page.GetServerIP(),
									UserName = this._page.EmployeeName,
									LogType = "修改",
									LogUser = this._page.EmployeeID,
									ModuleCode = "",
									ModuleName = "",
									Message = ""
								};
								if (tableInfo.DataLogTmpl != "")
								{
									dataLog.Message = this._page.ReplaceWithDataRow(tableInfo.DataLogTmpl, item);
									dataLog.Message = this._page.ReplaceContext(dataLog.Message);
								}
								dataLog.Data = stringBuilder.ToString();
								this._page.dblogger.WriteDataLog(dataLog);
							}
						}
					}
					catch (Exception exception2)
					{
						exception = exception2;
						dbTransaction.Rollback();
						this.fileLogger.Error<Exception>(exception);
						if (exception.InnerException == null)
						{
							throw exception;
						}
						throw exception.InnerException;
					}
				}
				finally
				{
					this._conn.Close();
				}
			}
			else
			{
				innerText = xmlElement.SelectSingleNode("_AutoID").InnerText;
				this._conn = SysDatabase.CreateConnection();
				this._conn.Open();
				dbTransaction = this._conn.BeginTransaction();
				try
				{
					try
					{
						strArrays = new string[] { "select * from ", tblName, " where _AutoID='", innerText, "'" };
						sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
						item = SysDatabase.ExecuteTable(sqlStringCommand, dbTransaction).Rows[0];
						if (tableInfo.DataLog == 2)
						{
							stringBuilder.Append(this.GetCmpFromRow(phyFields, item, tblName));
						}
						sQLTriger = __TableInfo.GetSQLTriger("DeleteB");
						dataTable = __TableInfo.GetSQLTriger("DeleteA");
						foreach (DataRow row2 in sQLTriger.Rows)
						{
							dbValue = this.GetDbValue(row2["ScriptTxt"]);
							if (dbValue != "")
							{
								str = string.Concat(this.PreSqlTrigger(dbValue, item), ";");
								sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
								SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
							}
						}
						this.ExecScript(tblName, 5, item, dbTransaction);
						if (tableInfo.DeleteMode != 0)
						{
							strArrays = new string[] { "update ", tblName, " set _IsDel=1 where _AutoID='", innerText, "'" };
							sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(strArrays));
							SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
						}
						else
						{
							sqlStringCommand = SysDatabase.GetSqlStringCommand(this.GenDeleteCmd(tblName, innerText));
							SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
						}
						foreach (DataRow dataRow2 in dataTable.Rows)
						{
							dbValue = this.GetDbValue(dataRow2["ScriptTxt"]);
							if (dbValue != "")
							{
								str = string.Concat(this.PreSqlTrigger(dbValue, item), ";");
								sqlStringCommand = SysDatabase.GetSqlStringCommand(str);
								SysDatabase.ExecuteNonQuery(sqlStringCommand, dbTransaction);
							}
						}
						this.ExecScript(tblName, 6, item, dbTransaction);
						dbTransaction.Commit();
						if (tableInfo.DataLog == 1)
						{
							dataLog = new DataLog()
							{
								AppID = innerText,
								AppName = tblName,
								ComputeIP = this._page.GetClientIP(),
								ServerIP = this._page.GetServerIP(),
								UserName = this._page.EmployeeName,
								LogType = "删除",
								LogUser = this._page.EmployeeID,
								ModuleCode = "",
								ModuleName = "",
								Message = ""
							};
							if (tableInfo.DataLogTmpl != "")
							{
								dataLog.Message = this._page.ReplaceWithDataRow(tableInfo.DataLogTmpl, item);
								dataLog.Message = this._page.ReplaceContext(dataLog.Message);
							}
							dataLog.Data = "";
							this._page.dblogger.WriteDataLog(dataLog);
						}
						else if (tableInfo.DataLog == 2)
						{
							dataLog = new DataLog()
							{
								AppID = innerText,
								AppName = tblName,
								ComputeIP = this._page.GetClientIP(),
								ServerIP = this._page.GetServerIP(),
								UserName = this._page.EmployeeName,
								LogType = "删除",
								LogUser = this._page.EmployeeID,
								ModuleCode = "",
								ModuleName = "",
								Message = ""
							};
							if (tableInfo.DataLogTmpl != "")
							{
								dataLog.Message = this._page.ReplaceWithDataRow(tableInfo.DataLogTmpl, item);
								dataLog.Message = this._page.ReplaceContext(dataLog.Message);
							}
							dataLog.Data = stringBuilder.ToString();
							this._page.dblogger.WriteDataLog(dataLog);
						}
					}
					catch (Exception exception3)
					{
						exception = exception3;
						dbTransaction.Rollback();
						if (exception.InnerException == null)
						{
							throw exception;
						}
						throw exception.InnerException;
					}
				}
				finally
				{
					this._conn.Close();
				}
			}
			return true;
		}
	}
}