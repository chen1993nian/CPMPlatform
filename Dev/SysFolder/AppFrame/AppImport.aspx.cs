using AjaxPro;
using Aspose.Cells;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.WebBase.SysFolder.AppFrame
{
	public partial class AppImport : PageBase
	{
	

		public string tblName = "";

		public string TipMessage = "";

		public StringBuilder fieldlist1 = new StringBuilder();

		public AppImport()
		{
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string GenTemplate(string tblName, string fldList)
		{
			string str = Guid.NewGuid().ToString();
			try
			{
				_TableInfo __TableInfo = new _TableInfo(tblName);
				TableInfo model = __TableInfo.GetModel();
				List<EIS.DataModel.Model.FieldInfo> phyFields = __TableInfo.GetPhyFields();
				Workbook workbook = new Workbook();
				Worksheet item = workbook.Worksheets[0];
                item.Name = model.TableNameCn;
				int num = 0;
				int num1 = 0;
				int num2 = 0;
				string[] strArrays = fldList.Split(new char[] { '|' });
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					string str1 = strArrays[i];
					EIS.DataModel.Model.FieldInfo fieldInfo = phyFields.Find((EIS.DataModel.Model.FieldInfo fieldInfo_0) => fieldInfo_0.FieldName == str1);
					item.Cells[num, num1].PutValue(fieldInfo.FieldNameCn);
                    item.Cells[num, num1].Style.Font.IsBold = true;
                    item.Cells[num, num1].Style.Font.Name = "宋体";
                    item.Cells[num, num1].Style.Font.Color = Color.Blue;
					item.Cells.SetColumnWidthPixel(num2, Convert.ToInt32(fieldInfo.ColumnWidth) + 10);
					num2++;
					num1++;
				}
				item.Cells.SetRowHeightPixel(0, 30);
				//Style style = workbook.get_Styles().get_Item(workbook.get_Styles().Add());
              //  Style style = workbook.Styles[workbook.Styles.Add()];
                Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];
                style.Font.Name = "Arial";
                style.Font.Size = 10;
				string str2 = string.Format("{0}_{1:yyyy-MM-dd-HH-mm-ss-ffff}.xls", tblName, DateTime.Now);
				string str3 = string.Format("{0}_{1:yyyyMMddHHmm}.xls", model.TableNameCn, DateTime.Now);
				string str4 = DateTime.Now.ToString("yyyy年MM月", DateTimeFormatInfo.InvariantInfo);
				string str5 = string.Concat(AppSettings.Instance.AppFileSavePath, str4);
				if (!Directory.Exists(str5))
				{
					Directory.CreateDirectory(str5);
				}
				string str6 = string.Concat(str5, "\\", str2);
				FileStream fileStream = new FileStream(str6, FileMode.Create);
				long length = (long)0;
				try
				{
                    //workbook.Save(fileStream, 5);
                    workbook.Save(fileStream, FileFormatType.Default);
                    
					fileStream.Flush();
					length = fileStream.Length;
				}
				finally
				{
					fileStream.Close();
				}
				_AppFile __AppFile = new _AppFile();
				AppFile appFile = new AppFile()
				{
					_AutoID = str,
					_UserName = base.EmployeeID,
					_OrgCode = "",
					_CreateTime = DateTime.Now,
					_UpdateTime = DateTime.Now,
					_IsDel = 0,
					FileName = str2,
					FactFileName = str3,
					FilePath = string.Concat(str4, "\\", str2),
					BasePath = AppSettings.Instance.AppFileBaseCode,
					FileSize = (int)length,
					FileType = ".xsl",
					FolderID = "",
					AppId = string.Concat(tblName, "_Template"),
					AppName = tblName
				};
				__AppFile.Add(appFile);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return str;
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string ImportData(string tblName, bool clearBefore)
		{
			string str;
			int i;
			int j;
			DbParameter dbParameter;
			string str1;
			string str2 = "";
			int num = 0;
			AppFile lastFileByAppId = FileService.GetLastFileByAppId(string.Concat(tblName, "_Import"));
			if (lastFileByAppId == null)
			{
				throw new Exception("请先上传数据文件。");
			}
			string basePath = AppFilePath.GetBasePath(lastFileByAppId.BasePath);
			if (!File.Exists(string.Concat(basePath, lastFileByAppId.FilePath)))
			{
				throw new Exception("请先上传数据文件。");
			}
			if (clearBefore)
			{
				SysDatabase.ExecuteNonQuery(string.Concat("delete ", tblName));
			}
			Workbook workbook = new Workbook();
			workbook.Open(string.Concat(basePath, lastFileByAppId.FilePath));
			Cells cells = workbook.Worksheets[0].Cells;
			int maxDataRow = cells.MaxDataRow;
			int maxDataColumn = cells.MaxDataColumn;
			_TableInfo __TableInfo = new _TableInfo(tblName);
			__TableInfo.GetModel();
			List<EIS.DataModel.Model.FieldInfo> phyFields = __TableInfo.GetPhyFields();
			List<EIS.DataModel.Model.FieldInfo> fieldInfos = new List<EIS.DataModel.Model.FieldInfo>();
			for (i = 0; i <= maxDataColumn; i++)
			{
				string str3 = cells[0, i].StringValue.Replace(" ", "");
				EIS.DataModel.Model.FieldInfo fieldInfo = phyFields.Find((EIS.DataModel.Model.FieldInfo fieldInfo_0) => fieldInfo_0.FieldNameCn == str3);
				if (fieldInfo != null)
				{
					fieldInfo.FieldOdr = i;
					fieldInfos.Add(fieldInfo);
				}
			}
			StringCollection stringCollections = new StringCollection();
           stringCollections.Add("_AutoID|newid()");
           // stringCollections.Add("_AutoID|@_AutoID");

			stringCollections.Add(string.Concat("_OrgCode|'", base.OrgCode, "'"));
			stringCollections.Add(string.Concat("_UserName|'", base.EmployeeID, "'"));
			stringCollections.Add("_CreateTime|getdate()");
			stringCollections.Add("_UpdateTime|getdate()");
			stringCollections.Add("_IsDel|0");
			stringCollections.Add("_CompanyId|''");
			string str4 = this.method_0(tblName, fieldInfos, stringCollections);
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(str4);
			if (tblName.ToLower() != "t_e_sys_tabledll")
			{
				string str5 = string.Format("select * from T_E_Sys_TableDll where Enable='是' and TableName='{0}' and ScriptEvent='{1}'", tblName, "ImportBefore");
				DataTable dataTable = SysDatabase.ExecuteTable(str5);
				if (dataTable.Rows.Count <= 0)
				{
					for (j = 1; j <= maxDataRow; j++)
					{
                        //str = Guid.NewGuid().ToString();
                        //if (j == 1)
                        //{
                        //    dbParameter = sqlStringCommand.CreateParameter();
                        //    dbParameter.ParameterName = "_AutoID";
                        //    sqlStringCommand.Parameters.Add(dbParameter);
                        //}
                        //sqlStringCommand.Parameters["_AutoID"].Value = str;
						for (i = 0; i < fieldInfos.Count; i++)
						{
							dbParameter = this.method_1(sqlStringCommand, fieldInfos[i], cells[j, fieldInfos[i].FieldOdr].Value);
							cells[j, fieldInfos[i].FieldOdr].PutValue(dbParameter.Value);
						}
						SysDatabase.ExecuteNonQuery(sqlStringCommand);
						num++;
					}
					if (workbook.Worksheets[0].Pictures.Count > 0)
					{
						string appFileSavePath = AppSettings.Instance.AppFileSavePath;
						int num1 = 0;
						foreach (Picture picture in workbook.Worksheets[0].Pictures)
						{
							int upperLeftColumn = picture.UpperLeftColumn;
							int upperLeftRow = picture.UpperLeftRow;
							int num2 = num1;
							num1 = num2 + 1;
							this.fileLogger.Debug<int, int, int>("图片：{0},col={1},row={2}", num2, upperLeftColumn, upperLeftRow);
							EIS.DataModel.Model.FieldInfo fieldInfo1 = fieldInfos.Find((EIS.DataModel.Model.FieldInfo fieldInfo_0) => fieldInfo_0.FieldOdr == upperLeftColumn);
							if (fieldInfo1 == null)
							{
								continue;
							}
							string fieldInDispStyle = fieldInfo1.FieldInDispStyle;
							if ((fieldInDispStyle == "023" ? false : fieldInDispStyle != "024"))
							{
								continue;
							}
							string stringValue = cells[upperLeftRow, upperLeftColumn].StringValue;
							string str6 = DateTime.Now.ToString("yyyy年MM月", DateTimeFormatInfo.InvariantInfo);
							str6 = tblName;
							if (!Directory.Exists(string.Concat(appFileSavePath, str6)))
							{
								Directory.CreateDirectory(string.Concat(appFileSavePath, str6));
							}
							string str7 = string.Concat(".", picture.ImageFormat.ToString());
							string str8 = Guid.NewGuid().ToString();
							string str9 = string.Concat(str8, str7);
							string str10 = string.Concat(appFileSavePath, str6, "\\", str9);
							FileStream fileStream = new FileStream(str10, FileMode.OpenOrCreate);
							try
							{
								fileStream.Write(picture.Data, 0, (int)picture.Data.Length);
								fileStream.Close();
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
								_AutoID = str8,
                                _UserName = EIS.AppBase.Utility.GetSession("EmployeeID").ToString(),
                                _OrgCode = EIS.AppBase.Utility.GetSession("DeptWbs").ToString(),
								_CreateTime = DateTime.Now,
								_UpdateTime = DateTime.Now,
								_IsDel = 0,
								FileName = str9,
								FactFileName = str9,
								FilePath = string.Concat(str6, "\\", str9),
								BasePath = AppSettings.Instance.AppFileBaseCode,
								DownCount = 0,
								FileSize = (int)picture.Data.Length,
								FileType = str7,
								FolderID = "",
								AppId = stringValue,
								AppName = tblName
							};
							(new _AppFile()).Add(appFile);
						}
					}
					//str5 = string.Format("select * from T_E_Sys_TableDll where Enable='是' and TableName='{0}' and ScriptEvent='{1}'", tblName, "ImportAfter");
					//dataTable = SysDatabase.ExecuteTable(str5);
					//dataTable.Rows.Count > 0;
				}
				else
				{
					DataTable dataTable1 = SysDatabase.ExecuteTable(string.Concat("select * from ", tblName, " where 1=2"));
					for (j = 1; j <= maxDataRow; j++)
					{
						str = Guid.NewGuid().ToString();
						DataRow dataRow = dataTable1.NewRow();
						dataRow["_AutoID"] = str;
						for (i = 0; i < fieldInfos.Count; i++)
						{
							object value = cells[j, fieldInfos[i].FieldOdr].Value;
							if (value != null)
							{
								string str11 = value.ToString();
								switch (fieldInfos[i].FieldType)
								{
									case 1:
									case 5:
									{
										dataRow[fieldInfos[i].FieldName] = str11;
										break;
									}
									case 2:
									{
										if (str11 == "")
										{
											break;
										}
                                        dataRow[fieldInfos[i].FieldName] = cells[j, fieldInfos[i].FieldOdr].IntValue;
										break;
									}
									case 3:
									{
										if (str11 == "")
										{
											break;
										}
										dataRow[fieldInfos[i].FieldName] = cells[j, fieldInfos[i].FieldOdr].DoubleValue;
										break;
									}
									case 4:
									{
										if ((str11 == "" ? true : str11.Length <= 9))
										{
											break;
										}
										dataRow[fieldInfos[i].FieldName] = Convert.ToDateTime(str11.Replace(".", "-"));
										break;
									}
								}
							}
						}
						dataTable1.Rows.Add(dataRow);
					}
					DataRow item = dataTable.Rows[0];
                    string str12 = string.Concat(EIS.AppBase.Utility.GetPhysicalRootPath(), "\\bin\\", item["FilePath"].ToString());
					string str13 = item["ClassName"].ToString();
					string str14 = item["MethodName"].ToString();
					try
					{
						if (!File.Exists(str12))
						{
							throw new Exception(string.Format("执行DLL逻辑时出错，文件[{0}]不存在", str12));
						}
						Type type = Assembly.LoadFrom(str12).GetType(str13);
						object obj = Activator.CreateInstance(type);
						MethodInfo method = type.GetMethod(str14);
						object[] objArray = new object[] { dataTable1, fieldInfos, str2 };
						object obj1 = method.Invoke(obj, objArray);
						str2 = objArray[2].ToString();
						if (Convert.ToBoolean(obj1))
						{
							for (j = 1; j <= maxDataRow; j++)
							{
								str = Guid.NewGuid().ToString();
								if (j == 1)
								{
									dbParameter = sqlStringCommand.CreateParameter();
									dbParameter.ParameterName = "_AutoID";
									sqlStringCommand.Parameters.Add(dbParameter);
								}
								sqlStringCommand.Parameters["_AutoID"].Value = str;
								for (i = 0; i < fieldInfos.Count; i++)
								{
									this.method_1(sqlStringCommand, fieldInfos[i], cells[j, fieldInfos[i].FieldOdr].Value);
								}
								SysDatabase.ExecuteNonQuery(sqlStringCommand);
								num++;
							}
						}
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						if (exception.InnerException == null)
						{
							this.fileLogger.Error<Exception>("调用外部组件出错：{0}", exception);
							throw new Exception(string.Concat("调用外部组件（", str12, "）出错：", exception.Message));
						}
						this.fileLogger.Error<Exception>("调用外部组件出错：{0}", exception.InnerException);
						throw new Exception(string.Concat("调用外部组件（", str12, "）出错：", exception.InnerException.Message));
					}
				}
				str1 = (str2 == "" ? string.Concat("已经成功导入(", num.ToString(), ")条数据") : str2);
			}
			else
			{
				str1 = "";
			}
			return str1;
		}

		private string method_0(string string_0, List<EIS.DataModel.Model.FieldInfo> list_0, StringCollection stringCollection_0)
		{
			int i;
			char[] chrArray;
			EIS.DataModel.Model.FieldInfo list0 = null;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("insert {0} (", string_0);
			for (i = 0; i < stringCollection_0.Count; i++)
			{
				string item = stringCollection_0[i];
				chrArray = new char[] { '|' };
				string str = item.Split(chrArray)[0];
				stringBuilder.AppendFormat("{0},", str);
			}
			foreach (EIS.DataModel.Model.FieldInfo list0A in list_0)
			{
                stringBuilder.AppendFormat("[{0}],", list0A.FieldName);
			}
			stringBuilder.Length = stringBuilder.Length - 1;
			stringBuilder.Append(") values (");
			for (i = 0; i < stringCollection_0.Count; i++)
			{
				string item1 = stringCollection_0[i];
				chrArray = new char[] { '|' };
				string str1 = item1.Split(chrArray)[1];
				stringBuilder.AppendFormat("{0},", str1);
			}
			foreach (EIS.DataModel.Model.FieldInfo fieldInfo in list_0)
			{
				stringBuilder.AppendFormat("@{0},", fieldInfo.FieldName);
			}
			stringBuilder.Length = stringBuilder.Length - 1;
			stringBuilder.Append(");");
			return stringBuilder.ToString();
		}

		private DbParameter method_1(DbCommand dbCommand_0, EIS.DataModel.Model.FieldInfo fieldInfo_0, object object_0)
		{
			DbParameter item = null;
			if (dbCommand_0.Parameters.Contains(fieldInfo_0.FieldName))
			{
				item = dbCommand_0.Parameters[fieldInfo_0.FieldName];
			}
			else
			{
				item = dbCommand_0.CreateParameter();
				item.ParameterName = fieldInfo_0.FieldName;
				dbCommand_0.Parameters.Add(item);
			}
			try
			{
				switch (fieldInfo_0.FieldType)
				{
					case 1:
					{
						item.DbType = DbType.String;
						item.Value = this.method_2(fieldInfo_0, object_0);
						break;
					}
					case 2:
					{
						item.DbType = DbType.Int32;
						if ((object_0 == null ? false : !(object_0.ToString().Trim() == "")))
						{
							item.Value = Convert.ToInt32(object_0);
							break;
						}
						else
						{
							item.Value = DBNull.Value;
							break;
						}
					}
					case 3:
					{
						item.DbType = DbType.Decimal;
						if ((object_0 == null ? false : !(object_0.ToString().Trim() == "")))
						{
							item.Value = Convert.ToDecimal(object_0);
							break;
						}
						else
						{
							item.Value = DBNull.Value;
							break;
						}
					}
					case 4:
					{
						item.DbType = DbType.DateTime;
						if ((object_0 == null ? false : !(object_0.ToString().Trim() == "")))
						{
							item.Value = Convert.ToDateTime(object_0.ToString().Replace(".", "-"));
							break;
						}
						else
						{
							item.Value = DBNull.Value;
							break;
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				if ((object_0 == null ? false : !(object_0.ToString().Trim() == "")))
				{
					throw new Exception(string.Format("在转化字段[{0}]:{1}时出现错误：{2}", fieldInfo_0.FieldNameCn, object_0, exception.Message));
				}
				throw exception;
			}
			return item;
		}

		private object method_2(EIS.DataModel.Model.FieldInfo fieldInfo_0, object object_0)
		{
			object str = null;
			if (fieldInfo_0.FieldType == 1)
			{
				string fieldInDispStyle = fieldInfo_0.FieldInDispStyle;
				if (fieldInDispStyle != null && (fieldInDispStyle == "023" || fieldInDispStyle == "024"))
				{
					str = Guid.NewGuid().ToString();
				}
				else if ((object_0 == null ? false : !(object_0.ToString().Trim() == "")))
				{
					str = object_0;
				}
				else
				{
					str = DBNull.Value;
				}
			}
			return str;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(AppImport));
			this.tblName = base.GetParaValue("tblName");
			foreach (EIS.DataModel.Model.FieldInfo phyField in (new _TableInfo(this.tblName)).GetPhyFields())
			{
				this.fieldlist1.AppendFormat("<li class='ui-state-default' id='{0}'>{1}</li>\n", phyField.FieldName, phyField.FieldNameCn);
			}
		}
	}
}