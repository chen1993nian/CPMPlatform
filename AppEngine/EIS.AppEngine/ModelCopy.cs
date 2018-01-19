using EIS.AppBase;
using EIS.AppModel;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;
using System.Text;

namespace EIS.AppEngine
{
	public class ModelCopy
	{
		public ModelCopy()
		{
		}

		public static void CopyTables(StringCollection tableList, UserContext user)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string str = "";
			string str1 = "";
			DbConnection dbConnection = SysDatabase.CreateConnection();
			try
			{
				dbConnection.Open();
				DbTransaction dbTransaction = dbConnection.BeginTransaction();
				try
				{
					foreach (string str2 in tableList)
					{
						char[] chrArray = new char[] { ',' };
						string[] strArrays = str2.Split(chrArray);
						string str3 = strArrays[0];
						string str4 = strArrays[1];
						string str5 = strArrays[2];
						_TableInfo __TableInfo = new _TableInfo(str3, dbTransaction);
						if (!__TableInfo.Exists(str4))
						{
							TableInfo model = __TableInfo.GetModel();
							if ((str != "" ? false : model.TableType == 1))
							{
								str = str3;
								str1 = str4;
							}
							model._AutoID = Guid.NewGuid().ToString();
							model._UserName = user.EmployeeId;
							model._CreateTime = DateTime.Now;
							model._UpdateTime = DateTime.Now;
							model.TableName = str4;
							model.TableNameCn = str5;
							model.ListSQL = model.ListSQL.Replace(str3, str4);
							model.DetailSQL = model.DetailSQL.Replace(str3, str4);
							if (model.TableType == 2)
							{
								model.ParentName = str1;
							}
							if (model.TableType != 1)
							{
								model.FormHtml = model.FormHtml.Replace(string.Concat("{", str3, "."), string.Concat("{", str4, "."));
								model.FormHtml2 = model.FormHtml2.Replace(string.Concat("{", str3, "."), string.Concat("{", str4, "."));
								model.DetailHtml = model.DetailHtml.Replace(string.Concat("{", str3, "."), string.Concat("{", str4, "."));
								model.PrintHtml = model.PrintHtml.Replace(string.Concat("{", str3, "."), string.Concat("{", str4, "."));
							}
							else
							{
								model.FormHtml = model.FormHtml.Replace(string.Concat("{", str3, "."), string.Concat("{", str4, "."));
								model.FormHtml2 = model.FormHtml2.Replace(string.Concat("{", str3, "."), string.Concat("{", str4, "."));
								model.DetailHtml = model.DetailHtml.Replace(string.Concat("{", str3, "."), string.Concat("{", str4, "."));
								model.PrintHtml = model.PrintHtml.Replace(string.Concat("{", str3, "."), string.Concat("{", str4, "."));
								if (tableList.Count > 1)
								{
									for (int i = 1; i < tableList.Count; i++)
									{
										string item = tableList[i];
										chrArray = new char[] { ',' };
										string[] strArrays1 = item.Split(chrArray);
										string str6 = strArrays1[0];
										string str7 = strArrays1[1];
										model.FormHtml = model.FormHtml.Replace(str6, str7);
										model.FormHtml2 = model.FormHtml2.Replace(str6, str7);
										model.DetailHtml = model.DetailHtml.Replace(str6, str7);
										model.PrintHtml = model.PrintHtml.Replace(str6, str7);
									}
								}
							}
							__TableInfo.Add(model);
							_FieldInfo __FieldInfo = new _FieldInfo(dbTransaction);
							List<FieldInfo> fields = __TableInfo.GetFields();
							foreach (FieldInfo field in fields)
							{
								field._AutoID = Guid.NewGuid().ToString();
								field._UserName = user.EmployeeId;
								field._CreateTime = DateTime.Now;
								field._UpdateTime = DateTime.Now;
								field.TableName = str4;
								__FieldInfo.Add(field);
							}
							_FieldStyle __FieldStyle = new _FieldStyle(dbTransaction);
							foreach (FieldStyle tableField in __FieldStyle.GetTableFields(str3))
							{
								tableField._AutoID = Guid.NewGuid().ToString();
								tableField._UserName = user.EmployeeId;
								tableField._CreateTime = DateTime.Now;
								tableField._UpdateTime = DateTime.Now;
								tableField.TableName = str4;
								__FieldStyle.Add(tableField);
							}
							_FieldInfoExt __FieldInfoExt = new _FieldInfoExt(dbTransaction);
							foreach (FieldInfoExt employeeId in __FieldInfoExt.GetTableFields(str3))
							{
								employeeId._AutoID = Guid.NewGuid().ToString();
								employeeId._UserName = user.EmployeeId;
								employeeId._CreateTime = DateTime.Now;
								employeeId._UpdateTime = DateTime.Now;
								employeeId.TableName = str4;
								__FieldInfoExt.Add(employeeId);
							}
							_FieldEvent __FieldEvent = new _FieldEvent(dbTransaction);
							foreach (FieldEvent modelList in __FieldEvent.GetModelList(string.Concat("TableName='", str3, "'")))
							{
								modelList._AutoID = Guid.NewGuid().ToString();
								modelList._UserName = user.EmployeeId;
								modelList._CreateTime = DateTime.Now;
								modelList._UpdateTime = DateTime.Now;
								modelList.FieldID = "";
								modelList.TableName = str4;
								__FieldEvent.Add(modelList);
							}
							_TableStyle __TableStyle = new _TableStyle(dbTransaction);
							foreach (TableStyle now in __TableStyle.GetModelList(string.Concat("TableName='", str3, "'")))
							{
								now._AutoID = Guid.NewGuid().ToString();
								now._UserName = user.EmployeeId;
								now._CreateTime = DateTime.Now;
								now._UpdateTime = DateTime.Now;
								now.TableName = str4;
								now.FormHtml = now.FormHtml.Replace(string.Concat("{", str3, "."), string.Concat("{", str4, "."));
								now.FormHtml2 = now.FormHtml2.Replace(string.Concat("{", str3, "."), string.Concat("{", str4, "."));
								now.DetailHtml = now.DetailHtml.Replace(string.Concat("{", str3, "."), string.Concat("{", str4, "."));
								now.PrintHtml = now.PrintHtml.Replace(string.Concat("{", str3, "."), string.Concat("{", str4, "."));
								__TableStyle.Add(now);
							}
							_TableScript __TableScript = new _TableScript(dbTransaction);
							foreach (TableScript tableScript in __TableScript.GetModelList(string.Concat("TableName='", str3, "'")))
							{
								tableScript._AutoID = Guid.NewGuid().ToString();
								tableScript._UserName = user.EmployeeId;
								tableScript._CreateTime = DateTime.Now;
								tableScript._UpdateTime = DateTime.Now;
								tableScript.TableName = str4;
								tableScript.ScriptCode = tableScript.ScriptCode.Replace(str3, str4);
								__TableScript.Add(tableScript);
							}
							if ((model.TableType == 1 ? true : model.TableType == 2))
							{
								stringBuilder.Append(ModelCopy.GenTblSQL(model, fields));
							}
						}
						else
						{
							return;
						}
					}
					if (stringBuilder.Length > 0)
					{
						SysDatabase.ExecuteNonQuery(stringBuilder.ToString(), dbTransaction);
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
				if (dbConnection != null)
				{
					((IDisposable)dbConnection).Dispose();
				}
			}
		}

		private static string GenTblSQL(TableInfo Model, List<FieldInfo> list)
		{
			string str;
			StringBuilder stringBuilder = new StringBuilder();
			string tableName = Model.TableName;
			string str1 = Model.TableType.ToString();
			if ((str1 == "1" ? true : str1 == "2"))
			{
				if (list.Count > 0)
				{
					stringBuilder.AppendFormat(" if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[{0}]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)\r\n                            DROP TABLE [dbo].[{0}] \r\n                            CREATE TABLE [dbo].[{0}] (\r\n                            [_AutoID] [varchar] (50) NOT NULL DEFAULT (newid()),\r\n                            [_UserName] [varchar] (50) NOT  NULL DEFAULT (''),\r\n                            [_OrgCode] [varchar] (100) NOT  NULL DEFAULT (''),\r\n                            [_CreateTime] [datetime] NOT  NULL DEFAULT (getdate()),\r\n                            [_UpdateTime] [datetime] NOT  NULL DEFAULT (getdate()),\r\n                            [_IsDel] [int] NOT  NULL DEFAULT (0),", tableName);
					if (str1 == "1")
					{
						stringBuilder.Append("[_CompanyID] [varchar] (50) NULL DEFAULT (''),");
						stringBuilder.Append("[_WFState] [varchar] (50) NULL ,");
						stringBuilder.Append("[_GDState] [varchar] (50) NULL ,");
					}
					if (str1 == "2")
					{
						stringBuilder.Append("[_MainID] [varchar] (50) NOT NULL ,");
						stringBuilder.Append("[_MainTbl] [varchar] (100) NOT NULL ,");
					}
					foreach (FieldInfo fieldInfo in list)
					{
						int isComput = fieldInfo.IsComput;
						if (isComput != 1)
						{
							string fieldName = fieldInfo.FieldName;
							if ((isComput != 0 ? true : !AppFields.Contains(fieldName)))
							{
								string str2 = "";
								switch (fieldInfo.FieldType)
								{
									case 1:
									{
										str2 = string.Concat(" [nvarchar](", fieldInfo.FieldLength, ") NULL");
										break;
									}
									case 2:
									{
										str2 = " [int] NULL";
										break;
									}
									case 3:
									{
										str2 = string.Concat(" [numeric](", fieldInfo.FieldLength, ") NULL");
										break;
									}
									case 4:
									{
										str2 = " [datetime] NULL";
										break;
									}
									case 5:
									{
										str2 = " [text] NULL";
										break;
									}
									case 6:
									{
										str2 = " [image] NULL";
										break;
									}
								}
								stringBuilder.AppendFormat(" [{0}] {1},", fieldName, str2);
							}
						}
					}
					stringBuilder.Length = stringBuilder.Length - 1;
					stringBuilder.AppendFormat(") ON [PRIMARY] \r\n                            ALTER TABLE [dbo].[{0}] ADD \r\n                             CONSTRAINT [PK_{0}] PRIMARY KEY  NONCLUSTERED\r\n                             ([_AutoID])  ON [PRIMARY]\r\n\r\n                            CREATE CLUSTERED INDEX [IX_{0}] ON [dbo].[{0}] \r\n                            (\r\n\t                            [_CreateTime] ASC\r\n                            ) ON [PRIMARY] \r\n                        ", tableName);
				}
				str = stringBuilder.ToString();
			}
			else
			{
				str = "";
			}
			return str;
		}
	}
}