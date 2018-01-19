using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using EIS.Permission.Service;
using EIS.WorkFlow.Model;
using EIS.WorkFlow.Service;
using HtmlAgilityPack;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Xml;

namespace EIS.AppModel
{
	public class ModelBuilder
	{
        /// <summary>
        /// 请求页面连接
        /// </summary>
		private PageBase _page = null;

		public bool SheetEditLimit = true;

		public bool IsNew = true;

		private DataRow _mainRow;
        /// <summary>
        /// 字段事件脚本块
        /// </summary>
		private StringBuilder FeScriptBlock = new StringBuilder();
        /// <summary>
        /// 记录到文件的logger
        /// </summary>
		private Logger fileLogger = null;

		private DataTable adviceList;

		private DataRow curRow;

		private int maxAdviceLen = 0x3e8;

		private string signPrintStyle = "";

		private StringCollection idCodeHash = new StringCollection();

		private DataTable dtFieldEvent = null;

		public string CopyId
		{
			get;
			set;
		}
        /// <summary>
        /// 编辑记录时，数据控制项优先
        /// </summary>
		public bool DataContolFirst
		{
			get;
			set;
		}
        /// <summary>
        /// 默认值参数值
        /// </summary>
		public List<EIS.AppModel.DataControl> DataControl
		{
			get;
			set;
		}
        /// <summary>
        /// SQL替换参数值，参数字符串，用“|”分割，通常为“@dd=23|@rr=e|@tt=34”
        /// </summary>
		public string DefaultValue
		{
			get;
			set;
		}

		public string MainId
		{
			get;
			private set;
		}

		public DataRow MainRow
		{
			get
			{
				return this._mainRow;
			}
		}
        /// <summary>
        /// 内容替换参数值，参数字符串，用“|”分割，通常为“@dd=23|@rr=e|@tt=34”
        /// </summary>
		public string ReplaceValue
		{
			get;
			set;
		}
        /// <summary>
        /// 多样式索引
        /// </summary>
		public string Sindex
		{
			get;
			set;
		}
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p">当前请求页面</param>
		public ModelBuilder(PageBase p)
		{
			this.fileLogger = LogManager.GetCurrentClassLogger();
			this._page = p;
			this.CopyId = "";
		}

		private string CheckBoxCustom(string cName, EIS.DataModel.Model.FieldInfo field, string[] arrLayout, DataTable dt, StringCollection scData, string eventStr)
		{
			int i;
			DataRow item;
			int num;
			int num1;
			int j;
			int k;
			int num2;
			object[] objArray;
			int num3;
			object obj;
			object obj1;
			object obj2;
			object obj3;
			object obj4;
			object obj5;
			StringBuilder stringBuilder = new StringBuilder();
			if (field.FieldRead == 2)
			{
				if ((int)arrLayout.Length == 1)
				{
					for (i = 0; i < dt.Rows.Count; i++)
					{
						item = dt.Rows[i];
						StringBuilder stringBuilder1 = stringBuilder;
						objArray = new object[] { i, cName, item[0], null, null, null };
						objArray[3] = (scData.Contains(item[0].ToString()) ? "checked" : "");
						objArray[4] = item[1];
						objArray[5] = string.Concat(field.TableName, "_", field.FieldName);
						stringBuilder1.AppendFormat("<input type='checkbox' class='printcheck {5}' {3} value='{2}' name='{1}' id='{1}_{0}' onclick='javascript:return false;' /><label for='{1}_{0}'>{4}</label>", objArray);
					}
				}
				else if (!(arrLayout[0] == "" ? false : !(arrLayout[0] == "0")))
				{
					for (i = 0; i < dt.Rows.Count; i++)
					{
						item = dt.Rows[i];
						StringBuilder stringBuilder2 = stringBuilder;
						objArray = new object[] { i, cName, item[0], null, null, null, null };
						objArray[3] = (scData.Contains(item[0].ToString()) ? "checked" : "");
						objArray[4] = item[1];
						objArray[5] = string.Concat(field.TableName, "_", field.FieldName);
						object[] objArray1 = objArray;
						if (arrLayout[3] == "1")
						{
							num3 = i + 1;
							obj5 = string.Concat(num3.ToString(), ".&nbsp;");
						}
						else
						{
							obj5 = "";
						}
						objArray1[6] = obj5;
						stringBuilder2.AppendFormat("<input type='checkbox' class='printcheck {5}' {3} value='{2}' name='{1}' id='{1}_{0}'  onclick='javascript:return false;'/><label for='{1}_{0}'>{6}{4}</label>", objArray);
					}
				}
				else if (arrLayout[0] == "1")
				{
					num = 1;
					if (arrLayout[2] != "")
					{
						num = Convert.ToInt32(arrLayout[2]);
					}
					stringBuilder.AppendFormat("<table class='noborder checktbl'>", new object[0]);
					if (!(arrLayout[1] == "1"))
					{
						num2 = (int)Math.Ceiling((double)dt.Rows.Count / (double)num);
						for (j = 0; j < num; j++)
						{
							stringBuilder.Append("<tr>");
							for (k = 0; k < num2; k++)
							{
								if (k * num + j >= dt.Rows.Count)
								{
									stringBuilder.Append("<td></td>");
								}
								else
								{
									item = dt.Rows[k * num + j];
									StringBuilder stringBuilder3 = stringBuilder;
									objArray = new object[] { j * num + k, cName, item[0], null, null, null, null };
									objArray[3] = (scData.Contains(item[0].ToString()) ? "checked" : "");
									objArray[4] = item[1];
									objArray[5] = string.Concat(field.TableName, "_", field.FieldName);
									object[] objArray2 = objArray;
									if (arrLayout[3] == "1")
									{
										num3 = k * num + j + 1;
										obj3 = string.Concat(num3.ToString(), ".&nbsp;");
									}
									else
									{
										obj3 = "";
									}
									objArray2[6] = obj3;
									stringBuilder3.AppendFormat("<td><input type='checkbox' class='{5}'  {3} value='{2}' name='{1}' id='{1}_{0}' onclick='javascript:return false;' />&nbsp;{6}{4}&nbsp;</td>", objArray);
								}
							}
							stringBuilder.Append("<tr>");
						}
					}
					else
					{
						num1 = (int)Math.Ceiling((double)dt.Rows.Count / (double)num);
						for (j = 0; j < num1; j++)
						{
							stringBuilder.Append("<tr>");
							for (k = 0; k < num; k++)
							{
								if (j * num + k >= dt.Rows.Count)
								{
									stringBuilder.Append("<td></td>");
								}
								else
								{
									item = dt.Rows[j * num + k];
									StringBuilder stringBuilder4 = stringBuilder;
									objArray = new object[] { j * num + k, cName, item[0], null, null, null, null };
									objArray[3] = (scData.Contains(item[0].ToString()) ? "checked" : "");
									objArray[4] = item[1];
									objArray[5] = string.Concat(field.TableName, "_", field.FieldName);
									object[] objArray3 = objArray;
									if (arrLayout[3] == "1")
									{
										num3 = j * num + k + 1;
										obj4 = string.Concat(num3.ToString(), ".&nbsp;");
									}
									else
									{
										obj4 = "";
									}
									objArray3[6] = obj4;
									stringBuilder4.AppendFormat("<td><input type='checkbox' class='{5}'  {3} value='{2}' name='{1}' id='{1}_{0}' onclick='javascript:return false;' />&nbsp;{6}{4}&nbsp;</td>", objArray);
								}
							}
							stringBuilder.Append("<tr>");
						}
					}
					stringBuilder.AppendFormat("</table>", new object[0]);
				}
			}
			else if ((int)arrLayout.Length == 1)
			{
				for (i = 0; i < dt.Rows.Count; i++)
				{
					StringBuilder stringBuilder5 = stringBuilder;
					objArray = new object[] { i, cName, dt.Rows[i][0], null, null, null, null, null };
					objArray[3] = (scData.Contains(dt.Rows[i][0].ToString()) ? "checked" : "");
					objArray[4] = dt.Rows[i][1];
					objArray[5] = eventStr;
					objArray[6] = (field.FieldInDisp == 1 ? this.GetClassName(ClsCommon.ControlType.CheckBox) : "hidden");
					objArray[7] = (field.FieldRead == 1 ? "disabled" : "");
					stringBuilder5.AppendFormat("<input type='checkbox' class='{6}' {5} {7}   {3} value='{2}' name='{1}' id='{1}_{0}' /><label for='{1}_{0}'>{4}</label>", objArray);
				}
			}
			else if (!(arrLayout[0] == "" ? false : !(arrLayout[0] == "0")))
			{
				for (i = 0; i < dt.Rows.Count; i++)
				{
					StringBuilder stringBuilder6 = stringBuilder;
					objArray = new object[] { i, cName, dt.Rows[i][0], null, null, null, null, null, null, null };
					objArray[3] = (scData.Contains(dt.Rows[i][0].ToString()) ? "checked" : "");
					objArray[4] = dt.Rows[i][1];
					objArray[5] = eventStr;
					objArray[6] = (field.FieldInDisp == 1 ? this.GetClassName(ClsCommon.ControlType.CheckBox) : "hidden");
					objArray[7] = (field.FieldRead == 1 ? "disabled" : "");
					objArray[8] = string.Concat(field.TableName, "_", field.FieldName);
					object[] objArray4 = objArray;
					if (arrLayout[3] == "1")
					{
						num3 = i + 1;
						obj2 = string.Concat(num3.ToString(), ".&nbsp;");
					}
					else
					{
						obj2 = "";
					}
					objArray4[9] = obj2;
					stringBuilder6.AppendFormat("<input type='checkbox' class='{6} {8}' {5} {7}   {3} value='{2}' name='{1}' id='{1}_{0}' /><label for='{1}_{0}'>{9}{4}</label>", objArray);
				}
			}
			else if (arrLayout[0] == "1")
			{
				num = 1;
				if (arrLayout[2] != "")
				{
					num = Convert.ToInt32(arrLayout[2]);
				}
				stringBuilder.AppendFormat("<table class='noborder checktbl{0}'>", (field.FieldInDisp == 1 ? "" : " hidden"));
				if (!(arrLayout[1] == "1"))
				{
					num2 = (int)Math.Ceiling((double)dt.Rows.Count / (double)num);
					for (j = 0; j < num; j++)
					{
						stringBuilder.Append("<tr>");
						for (k = 0; k < num2; k++)
						{
							if (k * num + j >= dt.Rows.Count)
							{
								stringBuilder.Append("<td></td>");
							}
							else
							{
								item = dt.Rows[k * num + j];
								StringBuilder stringBuilder7 = stringBuilder;
								objArray = new object[] { k * num + j, cName, item[0], null, null, null, null, null, null, null };
								objArray[3] = (scData.Contains(item[0].ToString()) ? "checked" : "");
								objArray[4] = item[1];
								objArray[5] = eventStr;
								objArray[6] = (field.FieldInDisp == 1 ? this.GetClassName(ClsCommon.ControlType.CheckBox) : "hidden");
								objArray[7] = (field.FieldRead == 1 ? "disabled" : "");
								objArray[8] = string.Concat(field.TableName, "_", field.FieldName);
								object[] objArray5 = objArray;
								if (arrLayout[3] == "1")
								{
									num3 = k * num + j + 1;
									obj = string.Concat(num3.ToString(), ".&nbsp;");
								}
								else
								{
									obj = "";
								}
								objArray5[9] = obj;
								stringBuilder7.AppendFormat("<td><input type='checkbox' class='{6} {8}' {5} {7} {3} value='{2}' name='{1}' id='{1}_{0}' /><label for='{1}_{0}'>{9}{4}</label></td>", objArray);
							}
						}
						stringBuilder.Append("<tr>");
					}
				}
				else
				{
					num1 = (int)Math.Ceiling((double)dt.Rows.Count / (double)num);
					for (j = 0; j < num1; j++)
					{
						stringBuilder.Append("<tr>");
						for (k = 0; k < num; k++)
						{
							if (j * num + k >= dt.Rows.Count)
							{
								stringBuilder.Append("<td></td>");
							}
							else
							{
								item = dt.Rows[j * num + k];
								StringBuilder stringBuilder8 = stringBuilder;
								objArray = new object[] { j * num + k, cName, item[0], null, null, null, null, null, null, null };
								objArray[3] = (scData.Contains(item[0].ToString()) ? "checked" : "");
								objArray[4] = item[1];
								objArray[5] = eventStr;
								objArray[6] = (field.FieldInDisp == 1 ? this.GetClassName(ClsCommon.ControlType.CheckBox) : "hidden");
								objArray[7] = (field.FieldRead == 1 ? "disabled" : "");
								objArray[8] = string.Concat(field.TableName, "_", field.FieldName);
								object[] objArray6 = objArray;
								if (arrLayout[3] == "1")
								{
									num3 = j * num + k + 1;
									obj1 = string.Concat(num3.ToString(), ".&nbsp;");
								}
								else
								{
									obj1 = "";
								}
								objArray6[9] = obj1;
								stringBuilder8.AppendFormat("<td><input type='checkbox' class='{6} {8}' {5} {7} {3} value='{2}' name='{1}' id='{1}_{0}' /><label for='{1}_{0}'>{9}{4}</label></td>", objArray);
							}
						}
						stringBuilder.Append("<tr>");
					}
				}
				stringBuilder.AppendFormat("</table>", new object[0]);
			}
			return stringBuilder.ToString();
		}
        /// <summary>
        /// 处理数据控制参数
        /// </summary>
        /// <param name="tblname">表名</param>
        /// <param name="data">数据行</param>
        /// <param name="dtFields">字段信息</param>
		public void DealDataControlPara(string tblName, DataRow data, List<EIS.DataModel.Model.FieldInfo> dtFields)
		{
			if (this.DataControl != null)
			{
				foreach (EIS.AppModel.DataControl defaultValue in this.DataControl.FindAll((EIS.AppModel.DataControl f) => f.BizName == tblName))
				{
					if (!AppFields.Contains(defaultValue.FieldName))
					{
						if (data.Table.Columns.Contains(defaultValue.FieldName))
						{
							EIS.DataModel.Model.FieldInfo defaultType = dtFields.Find((EIS.DataModel.Model.FieldInfo f) => f.FieldName.ToLower() == defaultValue.FieldName.ToLower());
							if (defaultValue.CanRead.HasValue)
							{
								defaultType.FieldRead = (defaultValue.CanRead.Value ? 1 : 0);
							}
							if (defaultValue.NotNull.HasValue)
							{
								defaultType.FieldNull = (defaultValue.NotNull.Value ? 1 : 0);
							}
							this.fileLogger.Trace<string, string, string>("DataControl {0}：{1}|{2}", defaultValue.FieldName, defaultValue.DefaultType, defaultValue.DefaultValue);
							if (!string.IsNullOrEmpty(defaultValue.DefaultType))
							{
								defaultType.FieldDValueType = defaultValue.DefaultType;
								defaultType.FieldDValue = defaultValue.DefaultValue;
								switch (defaultType.FieldType)
								{
									case 1:
									case 5:
									{
										data[defaultValue.FieldName] = this.GetDefaultValue(defaultValue.DefaultType, defaultValue.DefaultValue);
										break;
									}
									case 2:
									{
										if (defaultValue.DefaultValue.Trim() != "")
										{
											data[defaultValue.FieldName] = Convert.ToInt32(this.GetDefaultValue(defaultValue.DefaultType, defaultValue.DefaultValue));
										}
										break;
									}
									case 3:
									{
										if (defaultValue.DefaultValue.Trim() != "")
										{
											data[defaultValue.FieldName] = Convert.ToDecimal(this.GetDefaultValue(defaultValue.DefaultType, defaultValue.DefaultValue));
										}
										break;
									}
									case 4:
									{
										data[defaultValue.FieldName] = Convert.ToDateTime(this.GetDefaultValue(defaultValue.DefaultType, defaultValue.DefaultValue));
										break;
									}
									default:
									{
										data[defaultValue.FieldName] = this.GetDefaultValue(defaultValue.DefaultType, defaultValue.DefaultValue);
										break;
									}
								}
							}
						}
					}
				}
			}
		}
        /// <summary>
        /// 返回流程对应的业务数据 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
		private DataTable ExecuteTable(string script, string connectionId)
		{
			DataTable dataTable;
			if (string.IsNullOrWhiteSpace(connectionId))
			{
				dataTable = SysDatabase.ExecuteTable(script);
			}
			else
			{
				CustomDb customDb = new CustomDb();
				customDb.CreateDatabaseByConnectionId(connectionId.Trim());
				dataTable = customDb.ExecuteTable(script);
			}
			return dataTable;
		}
        /// <summary>
        /// 格式化数据输出
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="fieldDef">字段定义</param>
        /// <returns></returns>
		private string FormatData(object data, EIS.DataModel.Model.FieldInfo fieldDef)
		{
			string str;
			DateTime dateTime;
			string fieldInDispStyle = fieldDef.FieldInDispStyle;
			if (!(data.GetType().FullName == "System.DateTime"))
			{
				string str1 = "";
				if (fieldDef.FieldType != 3)
				{
					str1 = data.ToString();
				}
				else
				{
					string fieldLength = fieldDef.FieldLength;
					char[] chrArray = new char[] { ',' };
					string str2 = fieldLength.Split(chrArray)[1];
					if ((data == null ? false : data != DBNull.Value))
					{
						str1 = data.ToString();
						decimal num = Convert.ToDecimal(str1);
						str1 = num.ToString(string.Concat("N", str2));
					}
					else
					{
						str1 = "";
					}
				}
				str = str1;
			}
			else
			{
				if (fieldInDispStyle.Length > 0)
				{
					if (fieldInDispStyle.Substring(0, 3) == "001")
					{
						goto Label1;
					}
				}
				dateTime = Convert.ToDateTime(data);
				str = dateTime.ToString("yyyy-MM-dd");
			}
			return str;
		Label1:
			string[] strArrays = fieldDef.FieldInDispStyleTxt.Split("|".ToCharArray());
			if (strArrays[0].Length <= 0)
			{
				dateTime = Convert.ToDateTime(data);
				str = dateTime.ToString("yyyy-MM-dd");
				return str;
			}
			else
			{
				dateTime = Convert.ToDateTime(data);
				str = dateTime.ToString(strArrays[0]);
				return str;
			}
		}
        /// <summary>
        /// 生成数据控制项,字段名1=值^1|字段名2=值^0，其中的1代表只读,0代表可写
        /// </summary>
        /// <param name="cproval">参数值，字段名1=值^1|字段名2=值^0|...</param>
        /// <param name="tblName">表名</param>
        /// <returns></returns>
		public List<EIS.AppModel.DataControl> GenDataControlFromPara(string cproval, string tblName)
		{
			List<EIS.AppModel.DataControl> dataControls;
			List<EIS.AppModel.DataControl> dataControls1 = new List<EIS.AppModel.DataControl>();
			if (cproval.Length != 0)
			{
				string[] strArrays = cproval.Split("|".ToCharArray());
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					string[] strArrays1 = strArrays[i].Split("=".ToCharArray());
					if (((int)strArrays1.Length <= 1 ? false : strArrays1[1].IndexOf("^") > 0))
					{
						string[] strArrays2 = strArrays1[1].Split("=^".ToCharArray());
						EIS.AppModel.DataControl dataControl = new EIS.AppModel.DataControl()
						{
							BizName = tblName,
							FieldName = strArrays1[0],
							DefaultType = "custom",
							DefaultValue = strArrays2[0],
							CanWrite = new bool?(strArrays2[1] == "0"),
							CanRead = new bool?(strArrays2[1] == "1")
						};
						dataControls1.Add(dataControl);
					}
				}
				dataControls = dataControls1;
			}
			else
			{
				dataControls = dataControls1;
			}
			return dataControls;
		}

		private void GenDefaultValue(string tblname, DataRow data, List<EIS.DataModel.Model.FieldInfo> dtfields)
		{
			if (data.RowState == DataRowState.Detached)
			{
				foreach (EIS.DataModel.Model.FieldInfo dtfield in dtfields)
				{
					string fieldName = dtfield.FieldName;
					string fieldInDispStyle = dtfield.FieldInDispStyle;
					string str = (data == null ? "" : this.FormatData(data[fieldName], dtfield));
					try
					{
						string defaultValue = this.GetDefaultValue(dtfield.FieldDValueType, dtfield.FieldDValue);
						if (defaultValue.Length > 0)
						{
							if (str == "")
							{
								str = defaultValue;
							}
							else if ((dtfield.FieldType == 2 ? true : dtfield.FieldType == 3))
							{
								str = defaultValue;
							}
							data[fieldName] = str;
						}
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						Logger logger = this.fileLogger;
						object[] objArray = new object[] { tblname, fieldName, fieldInDispStyle, dtfield.FieldInDispStyleName, dtfield.FieldInDispStyleTxt, exception.Message };
						logger.Error("在生成字段[{0}.{1}]默认值时发生错误：fInStyle=[{2}：{3}：{4}],{5}", objArray);
						objArray = new object[] { tblname, fieldName, fieldInDispStyle, dtfield.FieldInDispStyleName, dtfield.FieldInDispStyleTxt, exception.Message };
						throw new Exception(string.Format("在生成字段[{0}.{1}]默认值时发生错误：fInStyle=[{2}：{3}：{4}],{5}", objArray));
					}
				}
			}
		}

        /// <summary>
        /// 产生Id和Code的对应关系 code:Id
        /// </summary>
        /// <param name="workflowModel"></param>
		private void GenIdCodeRelation(Define workflowModel)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(workflowModel.XPDL);
			XmlNodeList xmlNodeLists = xmlDocument.DocumentElement.SelectNodes("/Package/WorkflowProcesses/WorkflowProcess/Activities/Activity");
			if (xmlNodeLists != null)
			{
				foreach (XmlNode xmlNodes in xmlNodeLists)
				{
					if (xmlNodes.Attributes["Code"] != null)
					{
						if (xmlNodes.Attributes["Code"].Value.Trim() != "")
						{
							this.idCodeHash.Add(string.Concat(xmlNodes.Attributes["Code"].Value, "|", xmlNodes.Attributes["Id"].Value));
						}
					}
				}
			}
		}

		private string GetAdviceList(DataTable dtAdvice, string para)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			stringBuilder.AppendFormat("<table class='dealInfoTbl' border='1'><thead><tr>\r\n<th width='40'>序号</th><th width='120'>步骤名称</th><th width='50'>处理人</th><th width='40'>审批</th><th>处理意见</th><th width='140'>处理时间</th>\r\n</tr></thead><tbody>", new object[0]);
			foreach (DataRow row in dtAdvice.Rows)
			{
				object[] item = new object[6];
				int num1 = num;
				num = num1 + 1;
				item[0] = num1;
				item[1] = row["TaskName"];
				item[2] = row["EmployeeName"];
				item[3] = row["DealAction"];
				item[4] = row["DealAdvice"];
				item[5] = row["DealTime"];
				stringBuilder.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5:yyyy年MM月dd日 HH:mm}</td></tr>", item);
			}
			stringBuilder.Append("</tbody></table>");
			return stringBuilder.ToString();
		}
        /// <summary>
        /// 生成自动编号格式的字符串
        /// </summary>
		private string GetAutoStr(EIS.DataModel.Model.FieldInfo field, string cName, string fldval)
		{
			int num;
			Match match = null;
			string value;
			string str;
			string str1;
			object[] objArray;
			string str2 = "";
			string str3 = "";
			string str4 = "";
			string str5 = "";
			string str6 = "";
			string styleStr = "";
			string str7 = "";
			int num1 = 0;
			string str8 = ClsCommon.DbnullToString(field.FieldWidth);
			string str9 = ClsCommon.DbnullToString(field.FieldHeight);
			styleStr = this.GetStyleStr(str8, str9, true);
			if (field.FieldRead != 2)
			{
				object obj = null;
				int num2 = Convert.ToInt32(field.FieldType.ToString());
				string fieldName = field.FieldName;
				field.FieldInDispStyleTxt = Utility.DealCommandBySeesion(field.FieldInDispStyleTxt);
				Regex regex = new Regex("{(\\w+)}", RegexOptions.IgnoreCase);
				StringCollection stringCollections = new StringCollection();
				string[] tableName = new string[] { "年", "年2", "月", "日" };
				stringCollections.AddRange(tableName);
				if ((!string.IsNullOrEmpty(fldval) ? true : !this.IsNew))
				{
					switch (num2)
					{
						case 1:
						{
							num1 = Convert.ToInt16(field.FieldLength);
							foreach (Match matchA in regex.Matches(field.FieldInDispStyleTxt))
							{
                                value = matchA.Groups[1].Value;
								if (!stringCollections.Contains(value))
								{
									str1 = str7;
									tableName = new string[] { str1, "|", field.TableName, "&", value };
									str7 = string.Concat(tableName);
								}
							}
							if (str7.Length > 0)
							{
								objArray = new object[] { field._AutoID, "&", field.FieldOdr, str7 };
								str7 = string.Concat(objArray);
							}
							break;
						}
						case 2:
						{
							num1 = 10;
							break;
						}
						case 3:
						{
							num1 = Convert.ToInt16(field.FieldLength.Split(",".ToCharArray()).GetValue(0).ToString());
							break;
						}
					}
					str5 = fldval;
				}
				else
				{
					string fieldInDispStyleTxt = field.FieldInDispStyleTxt;
					foreach (Match match1 in regex.Matches(fieldInDispStyleTxt))
					{
						value = match1.Groups[1].Value;
						if (!stringCollections.Contains(value))
						{
							if (this.MainRow.Table.Columns.Contains(value))
							{
								fieldInDispStyleTxt = fieldInDispStyleTxt.Replace(match1.Value, this.MainRow[value].ToString());
							}
						}
					}
					string[] strArrays = fieldInDispStyleTxt.Split("|".ToCharArray());
					if (num2 == 1)
					{
						str3 = (strArrays.GetValue(0) == null ? "" : strArrays.GetValue(0).ToString());
						str4 = strArrays.GetValue(3).ToString();
						DateTime today = DateTime.Today;
						string str10 = today.ToString("MM");
						string str11 = today.ToString("dd");
						str3 = str3.Replace("{年}", today.ToString("yyyy"));
						str3 = str3.Replace("{年2}", today.ToString("yy"));
						str3 = str3.Replace("{月}", str10);
						str3 = str3.Replace("{日}", str11);
						str4 = str4.Replace("{年}", today.ToString("yyyy"));
						str4 = str4.Replace("{年2}", today.ToString("yy"));
						str4 = str4.Replace("{月}", str10);
						str4 = str4.Replace("{日}", str11);
						str2 = string.Concat("select max(", fieldName, ") from ", field.TableName);
						str1 = str2;
						tableName = new string[] { str1, " where ", fieldName, " like '", str3, "%", str4, "'" };
						str2 = string.Concat(tableName);
						obj = SysDatabase.ExecuteScalar(SysDatabase.GetSqlStringCommand(str2));
						if ((obj == null ? false : obj != DBNull.Value))
						{
							string str12 = "";
							str12 = obj.ToString();
							if (str3 != "")
							{
								str12 = str12.Substring(str3.Length, str12.Length - str3.Length).Trim();
							}
							if (str4 != "")
							{
								str12 = str12.Substring(0, str12.Length - str4.Length).Trim();
							}
							num = (!int.TryParse(str12, out num) ? 0 : Convert.ToInt32(str12));
							num++;
							str5 = string.Concat(str3, num.ToString(string.Concat("d", strArrays[1])), str4);
						}
						else
						{
							num = (!(strArrays[2].Trim() == "") ? Convert.ToInt32(strArrays[2].Trim()) : 1);
							str5 = (!(strArrays.GetValue(4).ToString() == "1") ? string.Concat(str3, Convert.ToString(num), str4) : string.Concat(str3, num.ToString(string.Concat("d", strArrays[1])), str4));
						}
						num1 = Convert.ToInt16(field.FieldLength);
						foreach (Match match2 in regex.Matches(field.FieldInDispStyleTxt))
						{
							value = match2.Groups[1].Value;
							if (!stringCollections.Contains(value))
							{
								str1 = str7;
								tableName = new string[] { str1, "|", field.TableName, "&", value };
								str7 = string.Concat(tableName);
							}
						}
						if (str7.Length > 0)
						{
							objArray = new object[] { field._AutoID, "&", field.FieldOdr, str7 };
							str7 = string.Concat(objArray);
						}
					}
					else if ((num2 == 2 ? true : num2 == 3))
					{
						str2 = string.Concat("select isnull(max(", fieldName, "),0) from ", field.TableName);
						obj = SysDatabase.ExecuteScalar(str2);
						if (obj != null)
						{
							str5 = (Convert.ToInt32(obj) != 0 ? Convert.ToString(Convert.ToInt32(obj) + 1) : strArrays[2]);
						}
						if (num2 != 2)
						{
							num1 = Convert.ToInt16(field.FieldLength.Split(",".ToCharArray()).GetValue(0).ToString());
						}
						else
						{
							num1 = 10;
						}
					}
				}
				str6 = string.Concat("系统自动生成的自动编号，该编号最大长度为", num1);
				StringBuilder stringBuilder = new StringBuilder();
				if (!Convert.ToBoolean(field.FieldInDisp))
				{
					stringBuilder.AppendFormat("<input  class='hidden {0}'", string.Concat(field.TableName, "_", field.FieldName));
				}
				else if (!Convert.ToBoolean(field.FieldRead))
				{
					stringBuilder.AppendFormat(string.Concat("<input class='autosn {0} ", this.GetClassName(ClsCommon.ControlType.TextBoxInChar), "' ", styleStr), string.Concat(field.TableName, "_", field.FieldName));
				}
				else
				{
					stringBuilder.AppendFormat(string.Concat("<input class='autosn {0} ", this.GetClassName(ClsCommon.ControlType.TextBoxInCharRead), "'  readonly ", styleStr), string.Concat(field.TableName, "_", field.FieldName));
				}
				if (str7.Length <= 0)
				{
					stringBuilder.AppendFormat(" autosn='' ", new object[0]);
				}
				else
				{
					stringBuilder.AppendFormat(" autosn='{0}' ", str7);
				}
				objArray = new object[] { " maxlength='", num1, "' type='text'  title=\"", str6, "\" " };
				stringBuilder.Append(string.Concat(objArray));
				tableName = new string[] { " id='", cName, "' name='", cName, "' value=\"", str5, "\"  {FieldEventDef} >" };
				stringBuilder.Append(string.Concat(tableName));
				str = stringBuilder.ToString();
			}
			else
			{
				str = fldval;
			}
			return str;
		}
        /// <summary>
        /// 生成CheckBox列表相关格式的字符串
        /// </summary>
		private string GetCheckBoxStr(EIS.DataModel.Model.FieldInfo field, string cName, string fielddata)
		{
			DataTable dataTable;
			string str = "";
			string styleStr = "";
			string styleStr1 = "";
			string str1 = " {FieldEventDef} ";
			StringBuilder stringBuilder = new StringBuilder();
			string str2 = ClsCommon.DbnullToString(field.FieldWidth);
			string str3 = ClsCommon.DbnullToString(field.FieldHeight);
			styleStr = this.GetStyleStr(str2, str3, true);
			styleStr1 = this.GetStyleStr(str2, str3, false);
			StringCollection stringCollections = new StringCollection();
			char[] chrArray = new char[] { ',' };
			stringCollections.AddRange(fielddata.Split(chrArray));
			string[] strArrays = new string[1];
			string fieldInDispStyle = field.FieldInDispStyle;
			if (fieldInDispStyle != null)
			{
				if (fieldInDispStyle == "050")
				{
					string fieldInDispStyleTxt = field.FieldInDispStyleTxt;
					chrArray = new char[] { '|' };
					string[] strArrays1 = fieldInDispStyleTxt.Split(chrArray);
					str = string.Concat(str, "select ItemCode,ItemName from T_E_Sys_DictEntry where DictID='", strArrays1[1]);
					str = string.Concat(str, "' order by  Itemorder");
					dataTable = SysDatabase.ExecuteTable(str);
					if ((int)strArrays1.Length > 2)
					{
						string str4 = strArrays1[2];
						chrArray = new char[] { ',' };
						strArrays = str4.Split(chrArray);
					}
					stringBuilder.Append(this.CheckBoxCustom(cName, field, strArrays, dataTable, stringCollections, str1));
				}
				else if (fieldInDispStyle == "051")
				{
					string fieldInDispStyleTxt1 = field.FieldInDispStyleTxt;
					chrArray = new char[] { '\u0060' };
					string[] strArrays2 = fieldInDispStyleTxt1.Split(chrArray);
					string str5 = strArrays2[0];
					chrArray = new char[] { ',' };
					string[] strArrays3 = str5.Split(chrArray);
					if ((int)strArrays2.Length > 1)
					{
						if (strArrays2[1].IndexOf(",") <= -1)
						{
							string str6 = strArrays2[1].Replace("\r\n", "^");
							chrArray = new char[] { '\u005E' };
							strArrays3 = str6.Split(chrArray);
						}
						else
						{
							string str7 = strArrays2[1];
							chrArray = new char[] { ',' };
							strArrays3 = str7.Split(chrArray);
						}
						string str8 = strArrays2[0];
						chrArray = new char[] { ',' };
						strArrays = str8.Split(chrArray);
					}
					DataTable dataTable1 = new DataTable();
					dataTable1.Columns.Add("c");
					dataTable1.Columns.Add("n");
					for (int i = 0; i < (int)strArrays3.Length; i++)
					{
						string str9 = strArrays3[i];
						chrArray = new char[] { '|' };
						string[] strArrays4 = str9.Split(chrArray);
						string str10 = strArrays4[0];
						string str11 = ((int)strArrays4.Length > 1 ? strArrays4[1] : str10);
						if (str11.Length != 0)
						{
							DataRow dataRow = dataTable1.NewRow();
							dataRow[0] = str11;
							dataRow[1] = str10;
							dataTable1.Rows.Add(dataRow);
						}
					}
					stringBuilder.Append(this.CheckBoxCustom(cName, field, strArrays, dataTable1, stringCollections, str1));
				}
				else if (fieldInDispStyle == "052")
				{
					string fieldInDispStyleTxt2 = field.FieldInDispStyleTxt;
					chrArray = new char[] { '\u0060' };
					string[] strArrays5 = fieldInDispStyleTxt2.Split(chrArray);
					if ((int)strArrays5.Length <= 1)
					{
						str = this.ReplaceParaValue(this.GetParaValue("DropValueSqlPara"), field.FieldInDispStyleTxt);
					}
					else
					{
						str = this.ReplaceParaValue(this.GetParaValue("DropValueSqlPara"), strArrays5[1]);
						string str12 = strArrays5[0];
						chrArray = new char[] { ',' };
						strArrays = str12.Split(chrArray);
					}
					dataTable = SysDatabase.ExecuteTable(str);
					stringBuilder.Append(this.CheckBoxCustom(cName, field, strArrays, dataTable, stringCollections, str1));
				}
			}
			return stringBuilder.ToString();
		}



        /// <summary>
        /// 取得不同控件对应的class名称
        /// </summary>
        /// <param name="ctype">控件类型</param>
        /// <returns>返回class名称</returns>
		private string GetClassName(ClsCommon.ControlType ctype)
		{
			string str = "";
			switch (ctype)
			{
				case ClsCommon.ControlType.TextBoxInChar:
				{
					str = "TextBoxInChar";
					break;
				}
				case ClsCommon.ControlType.TextBoxInCharRead:
				{
					str = "TextBoxInChar Read";
					break;
				}
				case ClsCommon.ControlType.TextBoxInDate:
				{
					str = "TextBoxInDate";
					break;
				}
				case ClsCommon.ControlType.TextBoxInDateRead:
				{
					str = "TextBoxInDate DateRead Read";
					break;
				}
				case ClsCommon.ControlType.TextBoxInOutPage:
				{
					str = "TextBoxInOutPage";
					break;
				}
				case ClsCommon.ControlType.TextBoxInOutPageRead:
				{
					str = "TextBoxInOutPage Read";
					break;
				}
				case ClsCommon.ControlType.TextBoxInArea:
				{
					str = "TextBoxInArea";
					break;
				}
				case ClsCommon.ControlType.TextBoxInAreaRead:
				{
					str = "TextBoxInArea Read";
					break;
				}
				case ClsCommon.ControlType.WebEditor:
				{
					str = "WebEditor";
					break;
				}
				case ClsCommon.ControlType.WebEditorRead:
				{
					str = "WebEditor Read";
					break;
				}
				case ClsCommon.ControlType.DropDownIn:
				{
					str = "DropDownIn";
					break;
				}
				case ClsCommon.ControlType.DropDownInMulti:
				{
					str = "DropDownInMulti";
					break;
				}
				case ClsCommon.ControlType.TextBoxFile:
				{
					str = "TextBoxFile";
					break;
				}
				case ClsCommon.ControlType.Radio:
				{
					str = "Radio";
					break;
				}
				case ClsCommon.ControlType.CheckBox:
				{
					str = "CheckBox";
					break;
				}
				case ClsCommon.ControlType.Btn:
				{
					str = "btn";
					break;
				}
			}
			return str;
		}


        /// <summary>
        /// 把DataRow转化能XML
        /// </summary>
        /// <param name="ParentNode">父结点</param>
        /// <param name="data">DataRow数据</param>
        /// <param name="rowid">生成XML结点的ID</param>
        /// <param name="fieldList">业务字段模型</param>
        /// <returns></returns>
		protected virtual XmlElement GetDataRowXml(XmlElement ParentNode, DataRow data, string rowid, List<EIS.DataModel.Model.FieldInfo> fieldList)
		{
			XmlElement xmlElement = ParentNode.OwnerDocument.CreateElement("row");
			foreach (DataColumn column in data.Table.Columns)
			{
				XmlElement xmlElement1 = ParentNode.OwnerDocument.CreateElement(column.ColumnName);
				if (!AppFields.Contains(column.ColumnName))
				{
					EIS.DataModel.Model.FieldInfo fieldInfo = fieldList.Find((EIS.DataModel.Model.FieldInfo p) => p.FieldName == column.ColumnName);
					if (fieldInfo != null)
					{
						string str = (data == null ? "" : this.FormatData(data[column.ColumnName], fieldInfo).Replace("\r", "&#x0D;").Replace("\n", "&#x0A;"));
						string defaultValue = this.GetDefaultValue(fieldInfo.FieldDValueType, fieldInfo.FieldDValue);
						if ((data.RowState != DataRowState.Detached ? false : str == ""))
						{
							str = defaultValue;
						}
						str = Utility.String2Xml(str);
						xmlElement1.AppendChild(ParentNode.OwnerDocument.CreateCDataSection(str));
					}
				}
				else
				{
					string str1 = data[column.ColumnName].ToString();
					if ((column.ColumnName.ToLower() != "_autoid" ? false : str1.Trim() == ""))
					{
						str1 = Guid.NewGuid().ToString();
					}
					xmlElement1.AppendChild(ParentNode.OwnerDocument.CreateCDataSection(str1));
				}
				xmlElement.AppendChild(xmlElement1);
			}
			xmlElement.SetAttribute("state", data.RowState.ToString());
			xmlElement.SetAttribute("id", rowid);
			ParentNode.AppendChild(xmlElement);
			return xmlElement;
		}

        /// <summary>
        /// 生成日期相关格式的字符串
        /// </summary>
		private string GetDateStr(EIS.DataModel.Model.FieldInfo field, string cName, string fldval)
		{
			string str;
			string[] strArrays;
			if (field.FieldRead != 2)
			{
				string[] strArrays1 = field.FieldInDispStyleTxt.Split("|".ToCharArray());
				string styleStr = "";
				string styleStr1 = "";
				StringBuilder stringBuilder = new StringBuilder();
				string str1 = ClsCommon.DbnullToString(field.FieldWidth);
				string str2 = ClsCommon.DbnullToString(field.FieldHeight);
				styleStr = this.GetStyleStr(str1, str2, true);
				styleStr1 = this.GetStyleStr(str1, str2, false);
				StringBuilder stringBuilder1 = new StringBuilder();
				if (strArrays1[0].Length > 0)
				{
					stringBuilder1.AppendFormat(" dateFmt:'{0}'", strArrays1[0]);
				}
				if (strArrays1[1].Length > 0)
				{
					stringBuilder1.AppendFormat(",minDate:'{0}'", strArrays1[1]);
				}
				if (strArrays1[2].Length > 0)
				{
					stringBuilder1.AppendFormat(",maxDate:'{0}'", strArrays1[2]);
				}
				if (strArrays1[3].Length > 0)
				{
					stringBuilder1.AppendFormat(",doubleCalendar:{0}", (strArrays1[3] == "1" ? "true" : "false"));
				}
				stringBuilder.Append("<input ");
				if (!Convert.ToBoolean(field.FieldInDisp))
				{
					stringBuilder.Append(string.Concat(styleStr1, " "));
				}
				else if (!Convert.ToBoolean(field.FieldRead))
				{
					strArrays = new string[] { "class='Wdate {0} {1}' ", styleStr, " onfocus=\"WdatePicker({{", stringBuilder1.ToString(), ",onpicked:_datePicked}});\" " };
					stringBuilder.AppendFormat(string.Concat(strArrays), this.GetClassName(ClsCommon.ControlType.TextBoxInDate), string.Concat(field.TableName, "_", field.FieldName));
				}
				else
				{
					stringBuilder.AppendFormat(string.Concat("class='Wdate {0} {1}' ", styleStr, "  readOnly "), this.GetClassName(ClsCommon.ControlType.TextBoxInDateRead), string.Concat(field.TableName, "_", field.FieldName));
				}
				strArrays = new string[] { "type='text' value='", fldval, "' id='", cName, "' name='", cName, "'  {FieldEventDef} />" };
				stringBuilder.Append(string.Concat(strArrays));
				str = stringBuilder.ToString();
			}
			else
			{
				str = fldval;
			}
			return str;
		}



        /// <summary>
        /// 生成默认录入样式日期格式的字符串
        /// </summary>
		private string GetDefaultDateStr(EIS.DataModel.Model.FieldInfo field, string cName, string fldval)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string styleStr = "";
			string str = "";
			string str1 = ClsCommon.DbnullToString(field.FieldWidth);
			string str2 = ClsCommon.DbnullToString(field.FieldHeight);
			styleStr = this.GetStyleStr(str1, str2, true);
			str = this.GetStyleStr(str1, str2, false);
			stringBuilder.Append("<input maxlength='10' title=\"只能输入如yyyy-mm-dd格式的日期字符串\" ");
			if (!Convert.ToBoolean(field.FieldInDisp))
			{
				stringBuilder.Append(string.Concat(str, " "));
			}
			else if (!Convert.ToBoolean(field.FieldRead))
			{
				stringBuilder.AppendFormat(string.Concat("class='Wdate {0} {1}' ", styleStr, " onFocus='WdatePicker({{onpicked:_datePicked}});' "), this.GetClassName(ClsCommon.ControlType.TextBoxInDate), string.Concat(field.TableName, "_", field.FieldName));
			}
			else
			{
				stringBuilder.AppendFormat(string.Concat("class='Wdate {0} {1}' ", styleStr, "  readOnly "), this.GetClassName(ClsCommon.ControlType.TextBoxInDateRead), string.Concat(field.TableName, "_", field.FieldName));
			}
            string[] strArrays = new string[] { "type='text' placeholder='请输入",field.FieldNameCn ,"' value='", fldval, "' id='", cName, "' name='", cName, "'  {FieldEventDef} />" };
			stringBuilder.Append(string.Concat(strArrays));
			return stringBuilder.ToString();
		}



        /// <summary>
        /// 生成默认录入样式多行文本格式的字符串
        /// </summary>
		private string GetDefaultMultiTxtStr(EIS.DataModel.Model.FieldInfo field, string cName, string fldval)
		{
			string styleStr = "";
			string str = "";
			string str1 = " {FieldEventDef} ";
			string str2 = ClsCommon.DbnullToString(field.FieldWidth);
			string str3 = ClsCommon.DbnullToString(field.FieldHeight);
			styleStr = this.GetStyleStr(str2, str3, true);
			str = this.GetStyleStr(str2, str3, false);
			StringBuilder stringBuilder = new StringBuilder();
			if (!Convert.ToBoolean(field.FieldInDisp))
			{
				stringBuilder.Append(string.Concat("<textarea  ", str));
			}
			else if (!Convert.ToBoolean(field.FieldRead))
			{
				stringBuilder.AppendFormat("<textarea class='{0} {2}' {1} ", this.GetClassName(ClsCommon.ControlType.TextBoxInArea), styleStr, string.Concat(field.TableName, "_", field.FieldName));
			}
			else
			{
				stringBuilder.AppendFormat("<textarea class='{0} {2}' {1} readonly ", this.GetClassName(ClsCommon.ControlType.TextBoxInAreaRead), styleStr, string.Concat(field.TableName, "_", field.FieldName));
			}
			string[] strArrays = new string[] { " rows=10 name='", cName, "' id='", cName, "' placeholder='请输入",field.FieldNameCn ,"'"};
			stringBuilder.Append(string.Concat(strArrays));
			stringBuilder.Append(string.Concat(" title=\"请输入", field.FieldNameCn, "\" "));
			stringBuilder.Append(string.Concat(str1, ">", fldval, "</textarea>"));
			return stringBuilder.ToString();
		}
        /// <summary>
        /// 生成默认录入样式字符格式的字符串
        /// </summary>
		private string GetDefaultTxtStr(EIS.DataModel.Model.FieldInfo field, string cName, string fldval)
		{
			object[] fieldLength;
			string styleStr = "";
			string str = "";
			string str1 = ClsCommon.DbnullToString(field.FieldWidth);
			string str2 = ClsCommon.DbnullToString(field.FieldHeight);
			styleStr = this.GetStyleStr(str1, str2, true);
			str = this.GetStyleStr(str1, str2, false);
			StringBuilder stringBuilder = new StringBuilder();
			int num = Convert.ToInt32(field.FieldType.ToString());
			if (!Convert.ToBoolean(field.FieldInDisp))
			{
				stringBuilder.AppendFormat(string.Concat("<input class='{0}' ", str), string.Concat(field.TableName, "_", field.FieldName));
			}
			else if (!Convert.ToBoolean(field.FieldRead))
			{
				stringBuilder.AppendFormat("<input class='{0} {2}' {1} ", this.GetClassName(ClsCommon.ControlType.TextBoxInChar), styleStr, string.Concat(field.TableName, "_", field.FieldName));
			}
			else
			{
				stringBuilder.AppendFormat("<input class='{0} {2}' {1} readonly ", this.GetClassName(ClsCommon.ControlType.TextBoxInCharRead), styleStr, string.Concat(field.TableName, "_", field.FieldName));
			}
			string[] strArrays = new string[] { " type='text' name='", cName, "' id='", cName, "'" };
			stringBuilder.Append(string.Concat(strArrays));
			if (num == 1)
			{
				fieldLength = new object[] { " maxlength='", field.FieldLength, "' title=\"最多输入", Convert.ToInt16(field.FieldLength), "个字符\" " };
				stringBuilder.Append(string.Concat(fieldLength));
			}
			else if (num == 2)
			{
				stringBuilder.Append(" data-type='int' title=\"必须输入整数\" ");
			}
			else if (num != 3)
			{
				fieldLength = new object[] { " maxlength=", field.FieldLength, " title=\"最多输入", Convert.ToInt16(field.FieldLength), "个字符\" " };
				stringBuilder.Append(string.Concat(fieldLength));
			}
			else
			{
				string[] strArrays1 = field.FieldLength.Split(new char[] { ',' });
				int num1 = int.Parse(strArrays1[0]) + (int.Parse(strArrays1[0]) - int.Parse(strArrays1[1])) / 3;
				strArrays = new string[] {" data-type='float' precision='", strArrays1[1], "' maxlength='", num1.ToString(), "' title=\"" };
				stringBuilder.Append(string.Concat(strArrays));
				stringBuilder.Append(string.Concat("必须输入数值，数值长度为", strArrays1[0]));
				stringBuilder.Append(string.Concat("位,小数点位数为", strArrays1[1], "位\""));
			}
            stringBuilder.Append(string.Concat(" placeholder='请输入", field.FieldNameCn, "' value=\"", fldval, "\" {FieldEventDef} />"));
			return stringBuilder.ToString();
		}


        /// <summary>
        /// 取得字段的缺省值
        /// </summary>
        /// <param name="dtype">缺省值类型</param>
        /// <param name="dvalue">缺省值</param>
        /// <returns>返回缺省值</returns>
		private string GetDefaultValue(string dtype, string dvalue)
		{
			string str;
			DateTime today;
			DateTime dateTime;
			string employeeID = "";
			string str1 = "";
			dvalue = dvalue.Trim();
			string lower = dtype.ToLower();
			if (lower != null)
			{
				switch (lower)
				{
					case "custom":
					{
						employeeID = dvalue;
						break;
					}
					case "session":
					{
						if (this._page.Session[dvalue] != null)
						{
							employeeID = this._page.Session[dvalue].ToString();
						}
						break;
					}
					case "date":
					{
						today = DateTime.Today;
						employeeID = today.ToString((dvalue == "" ? "yyyy-MM-dd" : dvalue));
						break;
					}
					case "datetime":
					{
						today = DateTime.Now;
						employeeID = today.ToString((dvalue == "" ? "yyyy-MM-dd HH:mm" : dvalue));
						break;
					}
					case "employeeid":
					{
						employeeID = this._page.EmployeeID;
						break;
					}
					case "employeename":
					{
						employeeID = this._page.EmployeeName;
						break;
					}
					case "loginname":
					{
						employeeID = this._page.UserName;
						break;
					}
					case "deptid":
					{
						employeeID = Utility.GetSession("deptid", "");
						break;
					}
					case "deptcode":
					{
						employeeID = Utility.GetSession("deptcode", "");
						break;
					}
					case "deptname":
					{
						employeeID = Utility.GetSession("deptname", "");
						break;
					}
					case "deptfullname":
					{
						employeeID = Utility.GetSession("deptfullname", "");
						break;
					}
					case "topdeptid":
					{
						employeeID = Utility.GetSession("topdeptid", "");
						break;
					}
					case "topdeptcode":
					{
						employeeID = Utility.GetSession("topdeptcode", "");
						break;
					}
					case "topdeptname":
					{
						employeeID = Utility.GetSession("topdeptname", "");
						break;
					}
					case "topdeptfullname":
					{
						employeeID = Utility.GetSession("topdeptfullname", "");
						break;
					}
					case "companyid":
					{
						employeeID = this._page.Session["companyid"].ToString();
						break;
					}
					case "companycode":
					{
						employeeID = this._page.Session["companycode"].ToString();
						break;
					}
					case "companyname":
					{
						employeeID = this._page.Session["companyname"].ToString();
						break;
					}
					case "positionid":
					{
						employeeID = this._page.Session["positionid"].ToString();
						break;
					}
					case "positionname":
					{
						employeeID = this._page.Session["positionname"].ToString();
						break;
					}
					case "loginip":
					{
						employeeID = this._page.Request.UserHostAddress.ToString();
						break;
					}
					case "dbfunction":
					{
						dvalue = this._page.ReplaceContext(dvalue).Replace("[QUOTES]", "'");
						str1 = string.Concat("select ", this.ReplaceParaValue(this.GetParaValue("DValueFuncPara").ToString(), dvalue));
						try
						{
							employeeID = SysDatabase.ExecuteScalar(str1).ToString();
						}
						catch
						{
							employeeID = "自定义函数值生成错误，请确定函数的正确性！";
						}
						break;
					}
					case "dbsql":
					{
						str1 = this.ReplaceParaValue(this.GetParaValue("DValueSqlPara").ToString(), dvalue);
						str1 = this._page.ReplaceContext(str1).Replace("[QUOTES]", "'");
						try
						{
							employeeID = SysDatabase.ExecuteScalar(str1).ToString();
						}
						catch
						{
							employeeID = "";
						}
						break;
					}
					case "currentyear":
					{
						employeeID = System.DateTime.Now.Year.ToString();
						break;
					}
					case "currentmonth":
					{
                        employeeID = System.DateTime.Now.Month.ToString();
						break;
					}
					case "currentday":
					{
                        employeeID = System.DateTime.Now.Day.ToString();
						break;
					}
					case "querytoday":
					{
						today = DateTime.Today;
						employeeID = today.ToString("yyyy-MM-dd,yyyy-MM-dd");
						break;
					}
					case "querycurweek":
					{
						today = DateTime.Today;
						dateTime = DateTime.Today;
						employeeID = string.Format("{0:yyyy-MM-dd},{1:yyyy-MM-dd}", today.AddDays(-(double)dateTime.DayOfWeek), DateTime.Today);
						break;
					}
					case "querycurmonth":
					{
						today = DateTime.Today;
						dateTime = DateTime.Today;
						DateTime dateTime1 = today.AddDays(1 - (double)dateTime.Day);
						today = DateTime.Today;
						int year = today.Year;
						today = DateTime.Today;
						int num = DateTime.DaysInMonth(year, today.Month);
						object obj2 = dateTime1;
						today = DateTime.Today;
						int year1 = today.Year;
						today = DateTime.Today;
						employeeID = string.Format("{0:yyyy-MM-dd},{1:yyyy-MM-dd}", obj2, new DateTime(year1, today.Month, num));
						break;
					}
					case "querycuryear":
					{
						today = DateTime.Today;
						employeeID = string.Format("{0}-1-1,{1:yyyy-MM-dd}", today.Year, DateTime.Today);
						break;
					}
					case "querylastmonth":
					{
						today = DateTime.Today;
						employeeID = string.Format("{0:yyyy-MM-dd},{1:yyyy-MM-dd}", today.AddMonths(-1), DateTime.Today);
						break;
					}
					case "querylast3month":
					{
						today = DateTime.Today;
						employeeID = string.Format("{0:yyyy-MM-dd},{1:yyyy-MM-dd}", today.AddMonths(-3), DateTime.Today);
						break;
					}
					case "querylastyear":
					{
						today = DateTime.Today;
						employeeID = string.Format("{0:yyyy-MM-dd},{1:yyyy-MM-dd}", today.AddYears(-1), DateTime.Today);
						break;
					}
					default:
					{
						str = dvalue;
						return str;
					}
				}
				str = employeeID;
				return str;
			}
			str = dvalue;
			return str;
		}
     

        /// <summary>
        /// 生成下拉列表相关格式的字符串
        /// </summary>
		private string GetDropStr(EIS.DataModel.Model.FieldInfo field, string cName, string fielddata, DataRow rowData)
		{
			DataTable dataTable;
			string str;
			StringBuilder stringBuilder = new StringBuilder();
			string str1 = "";
			string str2 = "";
			string styleStr = "";
			string styleStr1 = "";
			string str3 = " {FieldEventDef} ";
			string str4 = "";
			string str5 = ClsCommon.DbnullToString(field.FieldWidth);
			string str6 = ClsCommon.DbnullToString(field.FieldHeight);
			styleStr = this.GetStyleStr(str5, str6, true);
			styleStr1 = this.GetStyleStr(str5, str6, false);
			string fieldInDispStyle = field.FieldInDispStyle;
			string str7 = "";
			string str8 = fieldInDispStyle;
			if (str8 != null)
			{
				if (str8 == "010")
				{
					if (field.FieldRead != 2)
					{
						DateTime today = DateTime.Today;
						int year = today.Year - 20;
						today = DateTime.Today;
						str1 = string.Concat(str1, this.GetLoopOptionStr(year, today.Year + 20, fielddata));
					}
					else
					{
						str4 = fielddata;
					}
				}
				else if (str8 == "011")
				{
					if (field.FieldRead != 2)
					{
						str1 = string.Concat(str1, this.GetLoopOptionStr(1, 13, fielddata));
					}
					else
					{
						str4 = fielddata;
					}
				}
				else if (str8 == "012")
				{
					str2 = string.Concat(str2, "select ItemCode,ItemName from T_E_Sys_DictEntry where DictID='", field.FieldInDispStyleTxt.Split("|".ToCharArray())[1]);
					str2 = string.Concat(str2, "' order by  Itemorder");
					if (field.FieldRead != 2)
					{
						str1 = string.Concat(str1, this.GetLoopOptionStr(str2, fielddata));
					}
					else
					{
						dataTable = SysDatabase.ExecuteTable(str2);
						str4 = ((int)dataTable.Select(string.Concat("ItemCode='", fielddata, "'")).Length <= 0 ? fielddata : dataTable.Select(string.Concat("ItemCode='", fielddata, "'"))[0]["ItemName"].ToString());
					}
				}
				else if (str8 == "013")
				{
					string fieldInDispStyleTxt = field.FieldInDispStyleTxt;
					char[] chrArray = new char[] { ',' };
					string[] strArrays = fieldInDispStyleTxt.Split(chrArray);
					if (field.FieldRead != 2)
					{
						str1 = string.Concat(str1, this.GetLoopOptionStr(strArrays, fielddata));
					}
					else
					{
						int num = 0;
						while (num < (int)strArrays.Length)
						{
							string str9 = strArrays[num];
							chrArray = new char[] { '|' };
							string[] strArrays1 = str9.Split(chrArray);
							if (!(fielddata == ((int)strArrays1.Length > 1 ? strArrays1[1] : strArrays1[0])))
							{
								num++;
							}
							else
							{
								str4 = strArrays1[0];
								break;
							}
						}
						if (str4 == "")
						{
							str4 = fielddata;
						}
					}
				}
				else if (str8 == "014")
				{
					str2 = this.ReplaceParaValue(this.GetParaValue("DropValueSqlPara"), field.FieldInDispStyleTxt);
					string str10 = this._page.Session["employeeId"].ToString();
					if ((!this.IsNew ? true : field.FieldRead == 2))
					{
						str10 = rowData["_UserName"].ToString();
					}
					str2 = str2.Replace("{_UserName}", str10);
					str2 = this._page.ReplaceContext(str2).Replace("[QUOTES]", "'");
					if (field.FieldRead != 2)
					{
						foreach (Match match in (new Regex("{(\\w+)}", RegexOptions.IgnoreCase)).Matches(field.FieldInDispStyleTxt))
						{
							string value = match.Groups[1].Value;
							if (rowData != null)
							{
								if (rowData.Table.Columns.Contains(value))
								{
									string str11 = rowData[value].ToString();
									if (str11 != "")
									{
										str2 = str2.Replace(match.Value, str11);
									}
								}
							}
							string str12 = str7;
							string[] tableName = new string[] { str12, "|", field.TableName, "&", value };
							str7 = string.Concat(tableName);
						}
						if (str7.Length > 0)
						{
							object[] objArray = new object[] { field._AutoID, "&", field.FieldOdr, str7 };
							str7 = string.Concat(objArray);
						}
						str1 = string.Concat(str1, this.GetLoopOptionStr(str2, fielddata));
					}
					else
					{
						dataTable = SysDatabase.ExecuteTable(str2);
						string columnName = dataTable.Columns[0].ColumnName;
						if ((int)dataTable.Select(string.Concat(columnName, "='", fielddata, "'")).Length <= 0)
						{
							str4 = fielddata;
						}
						else
						{
							str4 = (dataTable.Columns.Count != 1 ? dataTable.Select(string.Concat(columnName, "='", fielddata, "'"))[0][1].ToString() : dataTable.Select(string.Concat(columnName, "='", fielddata, "'"))[0][0].ToString());
						}
					}
				}
			}
			if (field.FieldRead != 2)
			{
				str1 = (str1.IndexOf("selected") == -1 ? string.Concat("<option selected value=\"\">请选择---</option>", str1) : string.Concat("<option value=\"\">请选择---</option>", str1));
				if (!(fieldInDispStyle.Substring(0, 2) == "01"))
				{
					stringBuilder.Append("<select multiple=true ");
				}
				else
				{
					stringBuilder.Append("<select ");
				}
				if (str7.Length > 0)
				{
					stringBuilder.AppendFormat("dLink='{0}'", str7);
				}
				stringBuilder.AppendFormat(" id='{0}' name='{0}' {1}", cName, str3);
				if (!Convert.ToBoolean(field.FieldInDisp))
				{
					stringBuilder.Append(string.Concat(" ", styleStr1));
				}
				else if (!(fieldInDispStyle.Substring(0, 2) == "01"))
				{
					stringBuilder.AppendFormat(string.Concat(" class='{0}{1} {2}' ", styleStr), this.GetClassName(ClsCommon.ControlType.DropDownInMulti), (str7.Length > 0 ? " dLink" : ""), string.Concat(field.TableName, "_", field.FieldName));
				}
				else
				{
					stringBuilder.AppendFormat(string.Concat(" class='{0}{1} {2}' ", styleStr), this.GetClassName(ClsCommon.ControlType.DropDownIn), (str7.Length > 0 ? " dLink" : ""), string.Concat(field.TableName, "_", field.FieldName));
				}
				if (Convert.ToBoolean(field.FieldRead))
				{
					stringBuilder.Append(" disabled ");
				}
				stringBuilder.AppendFormat(">{0}</select>", str1);
				str = stringBuilder.ToString();
			}
			else
			{
				str = str4;
			}
			return str;
		}

		private string GetDropStr(EIS.DataModel.Model.FieldInfo field)
		{
			int i;
			DataRow row = null;
			string str = "";
			StringBuilder stringBuilder = new StringBuilder();
			string fieldInDispStyle = field.FieldInDispStyle;
			if (fieldInDispStyle != null)
			{
				if (fieldInDispStyle == "010")
				{
					for (i = DateTime.Today.Year - 20; i < DateTime.Today.Year + 20; i++)
					{
						stringBuilder.AppendFormat("{0}|{0},", i);
					}
				}
				else if (fieldInDispStyle == "011")
				{
					for (i = 1; i < 13; i++)
					{
						stringBuilder.AppendFormat("{0}|{0},", i);
					}
				}
				else if (fieldInDispStyle == "012")
				{
					str = string.Concat(str, "select ItemCode,ItemName from T_E_Sys_DictEntry where DictID='", field.FieldInDispStyleTxt.Split("|".ToCharArray())[1]);
					str = string.Concat(str, "' order by  Itemorder");
					foreach (DataRow rowB in SysDatabase.ExecuteTable(str).Rows)
					{
                        stringBuilder.AppendFormat("{0}|{1},", rowB["ItemName"], rowB["ItemCode"]);
					}
				}
				else if (fieldInDispStyle == "013")
				{
					stringBuilder.Append(field.FieldInDispStyleTxt);
				}
				else if (fieldInDispStyle == "014")
				{
					str = this.ReplaceParaValue(this.GetParaValue("DropValueSqlPara"), field.FieldInDispStyleTxt);
					str = this._page.ReplaceContext(str);
					DataTable dataTable = SysDatabase.ExecuteTable(str);
					foreach (DataRow dataRow in dataTable.Rows)
					{
						if (dataTable.Columns.Count != 1)
						{
							stringBuilder.AppendFormat("{0}|{1},", dataRow[1], dataRow[0]);
						}
						else
						{
							stringBuilder.AppendFormat("{0}|{1},", dataRow[0], dataRow[0]);
						}
					}
				}
			}
			return stringBuilder.ToString();
		}
        /// <summary>
        /// 得到字段的事件字符串
        /// </summary>
        /// <param name="fd">字段对象</param>
        /// <param name="cName">控件名称</param>
        /// <returns></returns>
		private string GetFieldEventScript(EIS.DataModel.Model.FieldInfo fd, string cName)
		{
			if (this.dtFieldEvent == null)
			{
				this.dtFieldEvent = SysDatabase.ExecuteTable(string.Concat("select EventScript,EventType,FieldName from T_E_Sys_FieldEvent where TableName='", fd.TableName, "'"));
				this.dtFieldEvent.TableName = fd.TableName;
			}
			else if (this.dtFieldEvent.TableName != fd.TableName)
			{
				this.dtFieldEvent = SysDatabase.ExecuteTable(string.Concat("select EventScript,EventType,FieldName from T_E_Sys_FieldEvent where TableName='", fd.TableName, "'"));
				this.dtFieldEvent.TableName = fd.TableName;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string str = cName;
			if (!cName.StartsWith("input"))
			{
				string[] strArrays = cName.Split("_".ToCharArray());
				str = string.Concat(strArrays[0], strArrays[2]);
			}
			DataRow[] dataRowArray = this.dtFieldEvent.Select(string.Concat("FieldName='", fd.FieldName, "'"));
			for (int i = 0; i < (int)dataRowArray.Length; i++)
			{
				DataRow dataRow = dataRowArray[i];
				string str1 = string.Concat(str, "_", dataRow["EventType"].ToString());
				stringBuilder.AppendFormat(" {0}='{2}_{0}({1})' ", dataRow["EventType"], cName, str);
				if (this.FeScriptBlock.ToString().IndexOf(string.Concat("function ", str1)) == -1)
				{
					this.FeScriptBlock.AppendFormat("\r\nfunction {1}_{0}(ctl){{\r\n {2}\r\n}}", dataRow["EventType"], str, dataRow["EventScript"]);
				}
			}
			return stringBuilder.ToString();
		}

        /// <summary>
        /// 生成业务字体模型
        /// </summary>
        /// <param name="fieldList">业务字段模型对象</param>
        /// <returns>业务模型字符串</returns>
		protected virtual string GetFieldModel(List<EIS.DataModel.Model.FieldInfo> fieldList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (EIS.DataModel.Model.FieldInfo fieldInfo in fieldList)
			{
				object[] fieldName = new object[] { fieldInfo.FieldName, fieldInfo.FieldType, fieldInfo.FieldLength, fieldInfo.FieldOdr, fieldInfo.FieldNull, fieldInfo.FieldNameCn, fieldInfo.FieldInDispStyle };
				stringBuilder.AppendFormat("\n\t{{'name' : '{0}', 'type' : '{1}', 'length' : '{2}', 'order': '{3}','empty':'{4}','namecn' : '{5}','dispstyle' : '{6}'}},", fieldName);
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length = stringBuilder.Length - 1;
			}
			return stringBuilder.ToString();
		}


        /// <summary>
        /// 返回多附件的打印列表
        /// </summary>
        /// <param name="appName">业务名称</param>
        /// <param name="appId">业务Id</param>
        /// <returns></returns>
		private string GetFileList(string appName, string appId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			IList<AppFile> files = (new FileService()).GetFiles(appName, appId);
			int num = 1;
			foreach (AppFile file in files)
			{
				stringBuilder.AppendFormat("<div class='fileItem'>", new object[0]);
				StringBuilder stringBuilder1 = stringBuilder;
				object[] factFileName = new object[7];
				int num1 = num;
				num = num1 + 1;
				factFileName[0] = num1;
				factFileName[1] = file.FactFileName;
				factFileName[2] = file.FileSize / 0x400;
				factFileName[3] = this._page.CryptPara(string.Concat("fileId=", file._AutoID));
				factFileName[4] = EmployeeService.GetEmployeeName(file._UserName);
				factFileName[5] = (file._CreateTime.Year == DateTime.Now.Year ? file._CreateTime.ToString("M月d日 HH:mm") : file._CreateTime.ToString("yy年M月d日 HH:mm"));
                factFileName[6] = EIS.AppBase.Utility.GetRootURI();
				stringBuilder1.AppendFormat("{0}、<a href='{6}/SysFolder/Common/FileDown.aspx?para={3}' _target='_blank'>{1}</a>（{4}&nbsp;{5}&nbsp;{2}K）", factFileName);
				stringBuilder.AppendFormat("</div>", new object[0]);
			}
			return stringBuilder.ToString();
		}
        /// <summary>
        /// 拼装下拉控件中下拉数据的html字符串，采用循环方式增加下拉值
        /// </summary>
        /// <param name="startvalue">下拉起始值</param>
        /// <param name="endvalue">下拉终止值</param>
        /// <param name="dvalue">缺省值</param>
        /// <returns>返回下拉控件中下拉数据的html字符串</returns>
		private string GetLoopOptionStr(int startvalue, int endvalue, string dvalue)
		{
			string str = "";
			for (int i = startvalue; i < endvalue; i++)
			{
				str = (!(dvalue == i.ToString()) ? string.Concat(str, "<option ") : string.Concat(str, "<option selected "));
				string str1 = str;
				string[] strArrays = new string[] { str1, "value=", i.ToString(), ">", i.ToString(), "</option>" };
				str = string.Concat(strArrays);
			}
			return str;
		}
        /// <summary>
        /// 拼装下拉控件中下拉数据的html字符串，采用sql语句方式增加下拉值（重载）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="dvalue">缺省值</param>
        /// <returns>返回下拉控件中下拉数据的html字符串</returns>
		private string GetLoopOptionStr(string sql, string dvalue)
		{
			string str = "";
			DataTable dataTable = SysDatabase.ExecuteTable(sql);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				str = (!(dvalue == dataTable.Rows[i][0].ToString()) ? string.Concat(str, "<option ") : string.Concat(str, "<option selected "));
				str = string.Concat(str, "value='", dataTable.Rows[i][0].ToString(), "'>");
				str = (dataTable.Columns.Count != 1 ? string.Concat(str, dataTable.Rows[i][1].ToString(), "</option>") : string.Concat(str, dataTable.Rows[i][0].ToString(), "</option>"));
			}
			dataTable.Dispose();
			return str;
		}

        /// <summary>
        /// 拼装下拉控件中下拉数据的html字符串，采用自定义值方式增加下拉值（重载）
        /// </summary>
        /// <param name="strarr">自定义值的数据库值和显示值的字符数组</param>
        /// <param name="dvalue">缺省值</param>
        /// <returns>返回下拉控件中下拉数据的html字符串</returns>

		private string GetLoopOptionStr(string[] strarr, string dvalue)
		{
			string str = "";
			for (int i = 0; i < (int)strarr.Length; i++)
			{
				string[] strArrays = strarr[i].Split(new char[] { '|' });
				string str1 = ((int)strArrays.Length > 1 ? strArrays[1] : strArrays[0]);
				str = (!(dvalue == str1) ? string.Concat(str, "<option ") : string.Concat(str, "<option selected "));
				str = string.Concat(str, "value='", str1, "'>");
				str = string.Concat(str, strArrays[0], "</option>");
			}
			return str;
		}
        /// <summary>
        /// 拼装Radio控件中html字符串，采用sql语句方式增加下拉值（重载）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="dvalue">缺省值</param>
        /// <returns>返回下拉控件中下拉数据的html字符串</returns>
		private string GetLoopRadioStr(string sql, string dvalue)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DataTable dataTable = SysDatabase.ExecuteTable(sql);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				stringBuilder.AppendFormat("<input type='radio' {} value='' name='' id='' /><label for='{}'>{}</label>", new object[0]);
			}
			dataTable.Dispose();
			return stringBuilder.ToString();
		}

		private string GetMaxOrder(string subName, string fldName, string mainId)
		{
			string str = string.Format("select IsNull( max(cast([{1}] as int))+1,1) from {0} where _MainId='{2}'", subName, fldName, mainId);
			return SysDatabase.ExecuteScalar(str).ToString();
		}

	


        /// <summary>
        /// 生成多附件上传
        /// </summary>
        /// <param name="field">字段信息</param>
        /// <param name="cName">中文名称</param>
        /// <param name="fldval"></param>
        /// <returns></returns>
		private string GetMultiAttach(EIS.DataModel.Model.FieldInfo field, string cName, string fldval)
		{
			object[] fieldOdr;
			if (field.FieldInDispStyleTxt.Trim() == "")
			{
				field.FieldInDispStyleTxt = "||1|";
			}
			field.FieldInDispStyleTxt.Split("|".ToCharArray());
			string styleStr = "";
			string str = "";
			string str1 = ClsCommon.DbnullToString(field.FieldWidth);
			string str2 = ClsCommon.DbnullToString(field.FieldHeight);
			styleStr = this.GetStyleStr(str1, str2, true);
			str = this.GetStyleStr(str1, str2, false);
			StringBuilder stringBuilder = new StringBuilder();
			fldval = (fldval == "" ? Guid.NewGuid().ToString() : fldval);
            if (fldval != "")
            {
                if (field.FieldHeight == "")
                {
                    IList<AppFile> files = (new FileService()).GetFiles(field.TableName, fldval);
                    field.FieldHeight = ((files.Count + 2) * 30).ToString();
                }
            }
			if (!cName.StartsWith("input"))
			{
				StringBuilder stringBuilder1 = stringBuilder;
				fieldOdr = new object[] { field.FieldOdr, fldval, null, null, null, null, null, null, null };
				fieldOdr[2] = (field.FieldWidth == "" ? "100%" : field.FieldWidth);
				fieldOdr[3] = (field.FieldHeight == "" ? "30px" : field.FieldHeight);
				fieldOdr[4] = field.TableName;
				fieldOdr[5] = field.FieldRead;
				fieldOdr[6] = HttpContext.Current.Server.UrlEncode(Security.EncryptStr(string.Concat("set=", field.FieldInDispStyleTxt.Trim()), this._page.UserName));
				fieldOdr[7] = cName;
				fieldOdr[8] = (cName.IndexOf("_srkjdslABHSAS_") > -1 ? string.Concat("{", field.FieldName, "}") : fldval);
				stringBuilder1.AppendFormat("<iframe id=\"frm_{7}\" class='attachFrame' frameborder=\"0\" scrolling=\"auto\"  src=\"../Common/FileListMini.aspx?para={6}&appName={4}&appId={8}&read={5}\" width=\"{2}\" height=\"{3}\"></iframe>", fieldOdr);
			}
			else
			{
				StringBuilder stringBuilder2 = stringBuilder;
				fieldOdr = new object[] { field.FieldOdr, fldval, null, null, null, null, null };
				fieldOdr[2] = (field.FieldWidth == "" ? "100%" : field.FieldWidth);
				fieldOdr[3] = (field.FieldHeight == "" ? "100px" : field.FieldHeight);
				fieldOdr[4] = field.TableName;
				fieldOdr[5] = field.FieldRead;
				fieldOdr[6] = HttpContext.Current.Server.UrlEncode(Security.EncryptStr(string.Concat("set=", field.FieldInDispStyleTxt.Trim()), this._page.UserName));
				stringBuilder2.AppendFormat("<iframe id=\"frm_{0}\" class='attachFrame' frameborder=\"0\" scrolling=\"auto\"  src=\"../Common/FileListFrame.aspx?para={6}&appName={4}&appId={1}&read={5}\" width=\"{2}\" height=\"{3}\"></iframe>", fieldOdr);
			}
			stringBuilder.AppendFormat("<input type='hidden' id='{0}' name='{0}' value='{1}'/>", cName, fldval);
			return stringBuilder.ToString();
		}

		private string GetMultiTxtStr(EIS.DataModel.Model.FieldInfo field, string cName, string fielddata)
		{
			string str;
			string[] fieldInDispStyleTxt;
			string str1 = " {FieldEventDef} ";
			string str2 = ClsCommon.DbnullToString(field.FieldWidth);
			string str3 = ClsCommon.DbnullToString(field.FieldHeight);
			string styleStr = this.GetStyleStr(str2, str3, true);
			string styleStr1 = this.GetStyleStr(str2, str3, false);
			string fieldInDispStyle = field.FieldInDispStyle;
			StringBuilder stringBuilder = new StringBuilder();
			if (fieldInDispStyle == "020")
			{
				if (field.FieldRead == 2)
				{
					str = fielddata.Replace("\r\n", "<br/>");
					return str;
				}
				if (!Convert.ToBoolean(field.FieldInDisp))
				{
					stringBuilder.Append(string.Concat("<textarea  ", styleStr1));
				}
				else if (!Convert.ToBoolean(field.FieldRead))
				{
					stringBuilder.AppendFormat("<textarea class='{0} {2}' {1} ", this.GetClassName(ClsCommon.ControlType.TextBoxInArea), styleStr, string.Concat(field.TableName, "_", field.FieldName));
				}
				else
				{
					stringBuilder.AppendFormat("<textarea class='{0} {2}' {1} readonly ", this.GetClassName(ClsCommon.ControlType.TextBoxInAreaRead), styleStr, string.Concat(field.TableName, "_", field.FieldName));
				}
                fieldInDispStyleTxt = new string[] { " name='", cName, "' id='", cName, "' placeholder='请输入", field.FieldNameCn, "'" };
				stringBuilder.Append(string.Concat(fieldInDispStyleTxt));
				stringBuilder.Append(string.Concat(" title=请输入", field.FieldNameCn, " "));
				fieldInDispStyleTxt = new string[] { str1, " rows=", field.FieldInDispStyleTxt, ">", fielddata, "</textarea>" };
				stringBuilder.Append(string.Concat(fieldInDispStyleTxt));
				str = stringBuilder.ToString();
				return str;
			}
			else if (fieldInDispStyle == "021")
			{
				if (field.FieldRead == 2)
				{
					str = fielddata;
					return str;
				}
				if (!Convert.ToBoolean(field.FieldInDisp))
				{
					stringBuilder.Append("<textarea  ");
				}
				else if (!Convert.ToBoolean(field.FieldRead))
				{
					stringBuilder.Append(string.Concat("<textarea class='", this.GetClassName(ClsCommon.ControlType.WebEditor), "' "));
				}
				else
				{
					fieldInDispStyleTxt = new string[] { "<textarea class='", this.GetClassName(ClsCommon.ControlType.WebEditorRead), "'  title=请输入", field.FieldNameCn, " readonly " };
					stringBuilder.Append(string.Concat(fieldInDispStyleTxt));
				}
				StringBuilder stringBuilder1 = stringBuilder;
                fieldInDispStyleTxt = new string[] { " style='display:none;width:{0};height:{1}px;' rows=3 name='", cName, "' id='", cName, "' placeholder='请输入", field.FieldNameCn, "'>", fielddata.Replace("{", "{{").Replace("}", "}}"), "</textarea> \r\n" };
				stringBuilder1.AppendFormat(string.Concat(fieldInDispStyleTxt), (str2 == "" ? "100%" : str2), str3);
				str = stringBuilder.ToString();
				return str;
			}
			else if (fieldInDispStyle == "022")
			{
				str = this.GetWebOffice(field, cName, fielddata);
			}
			else if (!(fieldInDispStyle == "023"))
			{
				if (fieldInDispStyle != "024")
				{
					str = stringBuilder.ToString();
					return str;
				}
				str = this.GetSingleImage(field, cName, fielddata);
			}
			else
			{
				str = this.GetMultiAttach(field, cName, fielddata);
			}
			return str;
		}

        /// <summary>
        /// 生成弹出页面相关格式的字符串
        /// </summary>	
		private string GetOutPageStr(EIS.DataModel.Model.FieldInfo field, string cName, string fielddata, string prefix)
		{
			char[] chrArray;
			string[] strArrays;
			int i;
			string[] strArrays1;
			StringBuilder stringBuilder = new StringBuilder();
			string str = "";
			string str1 = "";
			string str2 = " {FieldEventDef} ";
			string str3 = ClsCommon.DbnullToString(field.FieldWidth);
			string str4 = ClsCommon.DbnullToString(field.FieldHeight);
			string styleStr = this.GetStyleStr(str3, str4, true);
			this.GetStyleStr(str3, str4, false);
			string fieldInDispStyle = field.FieldInDispStyle;
			string str5 = "";
			string str6 = "";
			StringBuilder stringBuilder1 = new StringBuilder();
			bool flag = true;
			bool flag1 = true;
			if (!Convert.ToBoolean(field.FieldInDisp))
			{
				stringBuilder.AppendFormat("<input type='text' style='display:none;' id='{0}' name='{0}' class='{1}' ", cName, string.Concat(field.TableName, "_", field.FieldName));
                stringBuilder.Append(string.Concat(str2, " value=\"", fielddata, "\"  placeholder='请输入", field.FieldNameCn, "' />"));
			}
			else
			{
				_TableInfo __TableInfo = new _TableInfo(field.TableName);
				List<EIS.DataModel.Model.FieldInfo> phyFields = __TableInfo.GetPhyFields();
				string[] strArrays2 = field.FieldInDispStyleTxt.Split("|".ToCharArray());
				if (!(field.FieldType != 1 ? true : field.FieldHeight.Length <= 0))
				{
					int num = 0;
					if (int.TryParse(field.FieldHeight, out num))
					{
						if (num > 30)
						{
							flag = false;
						}
					}
				}
				else if (field.FieldType == 5)
				{
					flag = false;
				}
				string str7 = fieldInDispStyle;
				if (str7 != null)
				{
					switch (str7)
					{
						case "030":
						{
							str = this.ReplaceParaValue(this.GetParaValue("OutPagePara").ToString(), strArrays2[0]);
							break;
						}
						case "031":
						case "032":
						{
							str = string.Concat("../AppFrame/AppOutSelect.aspx?queryid=", strArrays2[0], "&queryfield=", strArrays2[2]);
							string str8 = strArrays2[1];
							chrArray = new char[] { ',' };
							strArrays = str8.Split(chrArray);
							for (i = 0; i < (int)strArrays.Length; i++)
							{
								string str9 = strArrays[i];
								stringBuilder1.AppendFormat(string.Concat(prefix, "{0},"), phyFields.Find((EIS.DataModel.Model.FieldInfo fobj) => fobj.FieldName == str9).FieldOdr);
							}
							str1 = stringBuilder1.ToString();
							str1 = str1.TrimEnd(",".ToCharArray());
							strArrays2[1] = str1;
							str5 = "EnterSearch";
							StringBuilder stringBuilder2 = new StringBuilder();
							_FieldInfo __FieldInfo = new _FieldInfo();
							TableInfo modelById = __TableInfo.GetModelById(strArrays2[0]);
							foreach (EIS.DataModel.Model.FieldInfo modelQueryDisp in __FieldInfo.GetModelQueryDisp(modelById.TableName))
							{
								if (stringBuilder2.Length <= 0)
								{
									stringBuilder2.AppendFormat("{0}", modelQueryDisp.FieldName);
								}
								else
								{
									stringBuilder2.AppendFormat(",{0}", modelQueryDisp.FieldName);
								}
							}
							if ((int)strArrays2.Length != 3)
							{
								strArrays2[3] = this.tranExpression(strArrays2[3]);
								str6 = string.Format(" display=\"{0}\" ", string.Concat(string.Join("|", strArrays2), "|", stringBuilder2.ToString()));
							}
							else
							{
								strArrays2[2] = this.tranExpression(strArrays2[2]);
								str6 = string.Format(" display=\"{0}\" ", string.Concat(string.Join("|", strArrays2), "||", stringBuilder2.ToString()));
							}
							flag1 = false;
							break;
						}
						case "033":
						{
							string str10 = strArrays2[1];
							chrArray = new char[] { ',' };
							strArrays = str10.Split(chrArray);
							for (i = 0; i < (int)strArrays.Length; i++)
							{
								string str11 = strArrays[i];
								stringBuilder1.AppendFormat(string.Concat(prefix, "{0},"), phyFields.Find((EIS.DataModel.Model.FieldInfo fobj) => fobj.FieldName == str11).FieldOdr);
							}
							str1 = stringBuilder1.ToString();
							str = string.Concat("../Common/UserTree.aspx?method=", strArrays2[0], "&queryfield=", strArrays2[2]);
							break;
						}
						case "034":
						{
							string str12 = strArrays2[1];
							chrArray = new char[] { ',' };
							strArrays = str12.Split(chrArray);
							for (i = 0; i < (int)strArrays.Length; i++)
							{
								string str13 = strArrays[i];
								stringBuilder1.AppendFormat(string.Concat(prefix, "{0},"), phyFields.Find((EIS.DataModel.Model.FieldInfo fobj) => fobj.FieldName == str13).FieldOdr);
							}
							str1 = stringBuilder1.ToString();
							str = string.Concat("../Common/PositionTree.aspx?method=1&queryfield=", strArrays2[2]);
							break;
						}
						case "035":
						{
							string str14 = strArrays2[1];
							chrArray = new char[] { ',' };
							strArrays = str14.Split(chrArray);
							for (i = 0; i < (int)strArrays.Length; i++)
							{
								string str15 = strArrays[i];
								stringBuilder1.AppendFormat(string.Concat(prefix, "{0},"), phyFields.Find((EIS.DataModel.Model.FieldInfo fobj) => fobj.FieldName == str15).FieldOdr);
							}
							str1 = stringBuilder1.ToString();
							str = string.Concat("../Common/DeptTree.aspx?method=1&queryfield=", strArrays2[2]);
							break;
						}
						case "036":
						{
							string str16 = strArrays2[1];
							chrArray = new char[] { ',' };
							strArrays = str16.Split(chrArray);
							for (i = 0; i < (int)strArrays.Length; i++)
							{
								string str17 = strArrays[i];
								stringBuilder1.AppendFormat(string.Concat(prefix, "{0},"), phyFields.Find((EIS.DataModel.Model.FieldInfo fobj) => fobj.FieldName == str17).FieldOdr);
							}
							str1 = stringBuilder1.ToString();
							str = string.Concat("../Common/DeptTree2.aspx?method=1&queryfield=", strArrays2[2]);
							break;
						}
					}
				}
				if (str1.Length > 1)
				{
					str1 = str1.TrimEnd(",".ToCharArray());
					str = (str.IndexOf("?") == -1 ? string.Concat(str, "?cid=", str1) : string.Concat(str, "&cid=", str1));
				}
				if (!flag)
				{
					strArrays1 = new string[] { "<textarea  type=text id='", cName, "' name='", cName, "' " };
					stringBuilder.Append(string.Concat(strArrays1));
					if (!Convert.ToBoolean(field.FieldRead))
					{
						strArrays1 = new string[] { " class='{0} {1} ", str5, " emptytip' ", styleStr, " " };
						stringBuilder.AppendFormat(string.Concat(strArrays1), this.GetClassName(ClsCommon.ControlType.TextBoxInOutPage), string.Concat(field.TableName, "_", field.FieldName));
					}
					else
					{
						strArrays1 = new string[] { " class='{0} {1} ", str5, " emptytip' ", styleStr, " readonly " };
						stringBuilder.AppendFormat(string.Concat(strArrays1), this.GetClassName(ClsCommon.ControlType.TextBoxInOutPageRead), string.Concat(field.TableName, "_", field.FieldName));
					}
					stringBuilder.Append(str6);
					stringBuilder.Append(string.Concat(" ", str2, " title=\"请双击选择\" emptytip='<请双击选择>'"));
					if (flag1)
					{
						stringBuilder.Append(string.Concat(" ondblclick=\"javascript:_openPage('", str.Replace("'", "[QUOTES]"), "');\""));
					}
                    stringBuilder.Append(string.Concat(" placeholder='请输入", field.FieldNameCn, "' >", fielddata, "</textarea>"));
				}
				else
				{
					strArrays1 = new string[] { "<input  type=text id='", cName, "' name='", cName, "' " };
					stringBuilder.Append(string.Concat(strArrays1));
					if (!Convert.ToBoolean(field.FieldRead))
					{
						stringBuilder.AppendFormat(string.Concat(" class='{0} {1} ", str5, " emptytip' ", styleStr), this.GetClassName(ClsCommon.ControlType.TextBoxInChar), string.Concat(field.TableName, "_", field.FieldName));
					}
					else
					{

						strArrays1 = new string[] { " class='{0} {1} ", str5, " emptytip' ", styleStr, " readonly " };
						stringBuilder.AppendFormat(string.Concat(strArrays1), this.GetClassName(ClsCommon.ControlType.TextBoxInCharRead), string.Concat(field.TableName, "_", field.FieldName));
					}
					strArrays1 = new string[] { " ", str2, " title=\"请双击选择\" emptytip='<请双击选择>'  value=\"", fielddata, "\" " };
					stringBuilder.Append(string.Concat(strArrays1));
					stringBuilder.Append(str6);
					if (flag1)
					{
						stringBuilder.Append(string.Concat(" ondblclick=\"javascript:_openPage('", str.Replace("'", "[QUOTES]"), "');\" "));
					}
                    stringBuilder.Append(string.Concat("  placeholder='请输入", field.FieldNameCn, "' />"));
				}
			}
			return stringBuilder.ToString();
		}

        /// <summary>
        /// 取得url中对应参数名的参数值，如果传入的是经过加密的字符串，那么解密，否则，根据参数名直接给出参数。
        /// </summary>
        /// <param name="paraname">参数名</param>
        /// <returns>返回参数值</returns>
		private string GetParaValue(string paraname)
		{
			return this._page.GetParaValue(paraname);
		}
        /// <summary>
        /// 生成密码相关格式的字符串
        /// </summary>
		private string GetPwdStr(EIS.DataModel.Model.FieldInfo field, string cName, string fielddata)
		{
			string str;
			string[] className;
			string str1 = "";
			string str2 = "";
			string styleStr = "";
			string styleStr1 = "";
			string str3 = " {FieldEventDef} ";
			string str4 = ClsCommon.DbnullToString(field.FieldWidth);
			string str5 = ClsCommon.DbnullToString(field.FieldHeight);
			styleStr = this.GetStyleStr(str4, str5, true);
			styleStr1 = this.GetStyleStr(str4, str5, false);
			if (!Convert.ToBoolean(field.FieldInDisp))
			{
				str1 = string.Concat(str1, "<input  ", styleStr1);
			}
			else if (!Convert.ToBoolean(field.FieldRead))
			{
				str = str1;
				className = new string[] { str, "<input class='", this.GetClassName(ClsCommon.ControlType.TextBoxInChar), "' ", styleStr };
				str1 = string.Concat(className);
			}
			else
			{
				str = str1;
				className = new string[] { str, "<input class='", this.GetClassName(ClsCommon.ControlType.TextBoxInCharRead), "' ", styleStr, " readonly " };
				str1 = string.Concat(className);
			}
			str2 = string.Concat("最多输入", field.FieldLength, "个字符");
			str = str1;
            className = new string[] { str, " maxlength=", field.FieldLength, " type='password' placeholder='请输入", field.FieldNameCn, "'", str3, " title=\"", str2, "\" " };
			str1 = string.Concat(className);
			str = str1;
			className = new string[] { str, "id=", cName, " name=", cName, " value=\"", fielddata, "\" />" };
			str1 = string.Concat(className);
			return str1;
		}
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="queryCode">查询编码</param>
        /// <param name="strWhere">条件</param>
        /// <param name="option">选项，PageSize|PageIndex|SortDir</param>
        /// <param name="pageUrl">页码连接地址</param>
        /// <returns></returns>
		public string GetQueryList(string queryCode, string strWhere, string option, string pageUrl)
		{
			string str;
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			string formHtml = "";
			TableInfo model = (new _TableInfo(queryCode)).GetModel();
			string[] strArrays = option.Split(new char[] { '|' });
			int num1 = int.Parse(strArrays[0]);
			int num2 = int.Parse(strArrays[1]);
			string str1 = strArrays[2];
			if (str1 != "")
			{
				str1 = string.Concat(" order by ", str1);
			}
			if (this.Sindex != "")
			{
				TableStyle tableStyle = _TableStyle.GetModel(queryCode, this.Sindex);
				if (tableStyle != null)
				{
					formHtml = tableStyle.FormHtml;
				}
			}
			if (formHtml == "")
			{
				formHtml = model.FormHtml;
			}
			formHtml = formHtml.Replace("\r\n", "");
			if (strWhere.Trim() == "")
			{
				strWhere = " 1=1 ";
			}
			this.fileLogger.Debug("查询[{0}]开始替换参数", queryCode);
			string str2 = model.ListSQL.Replace("|^condition^|", strWhere.Replace("[QUOTES]", "'")).Replace("|^sortdir^|", str1).Replace("\r\n", " ").Replace("\t", "");
			if (!string.IsNullOrEmpty(this.DefaultValue))
			{
				str2 = Utility.ReplaceParaValues(str2, this.DefaultValue);
			}
			str2 = Utility.DealCommandBySeesion(str2);
			this.fileLogger.Debug<string, string>("查询[{0}]ListSQL计算后：[{1}]", queryCode, str2);
			DataTable dataTable = this.ExecuteTable(str2, model.ConnectionId);
			this.fileLogger.Debug<string, int>("查询[{0}]返回数据：[{1}]", queryCode, dataTable.Rows.Count);
			HtmlDocument htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(formHtml);
			HtmlNode elementbyId = htmlDocument.GetElementbyId(queryCode);
			if (elementbyId != null)
			{
				elementbyId.SelectSingleNode("thead");
				HtmlNode htmlNode = elementbyId.SelectSingleNode("tbody");
				string outerHtml = htmlNode.OuterHtml;
				string innerHtml = htmlNode.InnerHtml;
				StringBuilder stringBuilder1 = new StringBuilder();
				if (dataTable.Rows.Count <= 0)
				{
					formHtml = htmlDocument.DocumentNode.OuterHtml.Replace(outerHtml, "");
				}
				else
				{
					int num3 = num2 * num1;
					int count = (num2 + 1) * num1;
					if (count > dataTable.Rows.Count)
					{
						count = dataTable.Rows.Count;
					}
                    double dou = dataTable.Rows.Count / num1;
                    int num4 = (int)Math.Ceiling(dou);
					for (int i = num3; i < count; i++)
					{
						DataRow item = dataTable.Rows[i];
						stringBuilder1.Append(this.ReplaceWithDataRow(innerHtml, item, i + 1));
					}
					formHtml = htmlDocument.DocumentNode.OuterHtml.Replace(outerHtml, stringBuilder1.ToString());
					StringBuilder stringBuilder2 = new StringBuilder();
					if (num2 == 0)
					{
						if (num4 > 1)
						{
							num = num4 - 1;
							stringBuilder2.AppendFormat("<a href='{0}' target='_self'>下页</a>&nbsp;<a href='{1}' target='_self'>尾页</a>", Utility.EncryptUrl(pageUrl.Replace("{i}", "1"), this._page.UserName), Utility.EncryptUrl(pageUrl.Replace("{i}", num.ToString()), this._page.UserName));
						}
					}
					else if (!(num2 <= 0 ? true : num2 >= num4 - 1))
					{
						object[] objArray = new object[] { Utility.EncryptUrl(pageUrl.Replace("{i}", "0"), this._page.UserName), null, null, null };
						num = num2 - 1;
						objArray[1] = Utility.EncryptUrl(pageUrl.Replace("{i}", num.ToString()), this._page.UserName);
						num = num2 + 1;
						objArray[2] = Utility.EncryptUrl(pageUrl.Replace("{i}", num.ToString()), this._page.UserName);
						num = num4 - 1;
						objArray[3] = Utility.EncryptUrl(pageUrl.Replace("{i}", num.ToString()), this._page.UserName);
						stringBuilder2.AppendFormat("<a href='{0}' target='_self'>首页</a>&nbsp;\r\n                        <a href='{1}' target='_self'>上页</a>&nbsp;\r\n                        <a href='{2}' target='_self'>下页</a>&nbsp;\r\n                        <a href='{3}' target='_self'>尾页</a>", objArray);
					}
					else if (num2 == num4 - 1)
					{
						num = num2 - 1;
						stringBuilder2.AppendFormat("<a href='{0}' target='_self'>首页</a>&nbsp;<a href='{1}' target='_self'>上页</a>", Utility.EncryptUrl(pageUrl.Replace("{i}", "0"), this._page.UserName), Utility.EncryptUrl(pageUrl.Replace("{i}", num.ToString()), this._page.UserName));
					}
					stringBuilder2.AppendFormat("<span class='total'>（共{0}项，{1}/{2}）</span>", dataTable.Rows.Count, num2 + 1, num4);
					formHtml = string.Concat(formHtml, "<div class='pagerbody'>", stringBuilder2.ToString(), "</div>");
				}
				str = formHtml;
			}
			else
			{
				str = "";
			}
			return str;
		}

		public string GetQueryModel(EIS.DataModel.Model.FieldInfo field)
		{
			object[] fieldNameCn;
			if (field.QueryStyle.Trim() != "")
			{
				field.FieldInDispStyle = field.QueryStyle;
				field.FieldInDispStyleTxt = field.QueryStyleTxt;
			}
			string fieldInDispStyle = field.FieldInDispStyle;
			StringBuilder stringBuilder = new StringBuilder();
			string dropStr = "";
			if (fieldInDispStyle.Length < 3)
			{
				fieldNameCn = new object[] { field.FieldNameCn, field.FieldName, field.FieldType, this.GetDefaultValue(field.QueryDefaultType, field.QueryDefaultValue) };
				stringBuilder.AppendFormat("{{display: '{0}', name : '{1}', type: {2},defvalue:'{3}'}},", fieldNameCn);
			}
			else if (fieldInDispStyle.Substring(0, 2) == "01")
			{
				dropStr = this.GetDropStr(field);
				fieldNameCn = new object[] { field.FieldNameCn, field.FieldName, field.FieldType, dropStr, this.GetDefaultValue(field.QueryDefaultType, field.QueryDefaultValue) };
				stringBuilder.AppendFormat("{{display: '{0}', name : '{1}', type: {2}, edit:'select',data:'{3}',defvalue:'{4}'}},", fieldNameCn);
			}
			else if (!(fieldInDispStyle.Substring(0, 2) == "04"))
			{
				fieldNameCn = new object[] { field.FieldNameCn, field.FieldName, field.FieldType, this.GetDefaultValue(field.QueryDefaultType, field.QueryDefaultValue) };
				stringBuilder.AppendFormat("{{display: '{0}', name : '{1}', type: {2},defvalue:'{3}'}},", fieldNameCn);
			}
			else
			{
				dropStr = this.GetRadioStr(field);
				fieldNameCn = new object[] { field.FieldNameCn, field.FieldName, field.FieldType, dropStr, this.GetDefaultValue(field.QueryDefaultType, field.QueryDefaultValue) };
				stringBuilder.AppendFormat("{{display: '{0}', name : '{1}', type: {2}, edit:'radio',data:'{3}',defvalue:'{4}'}},", fieldNameCn);
			}
			return stringBuilder.ToString();
		}
        /// <summary>
        /// 生成Radio列表相关格式的字符串
        /// </summary>
		private string GetRadioStr(EIS.DataModel.Model.FieldInfo field, string cName, string fielddata)
		{
			DataTable dataTable;
			StringBuilder stringBuilder = new StringBuilder();
			string str = "";
			string styleStr = "";
			string styleStr1 = "";
			string str1 = " {FieldEventDef} ";
			string str2 = ClsCommon.DbnullToString(field.FieldWidth);
			string str3 = ClsCommon.DbnullToString(field.FieldHeight);
			styleStr = this.GetStyleStr(str2, str3, true);
			styleStr1 = this.GetStyleStr(str2, str3, false);
			StringCollection stringCollections = new StringCollection();
			char[] chrArray = new char[] { ',' };
			stringCollections.AddRange(fielddata.Split(chrArray));
			string[] strArrays = new string[1];
			string fieldInDispStyle = field.FieldInDispStyle;
			if (fieldInDispStyle != null)
			{
				if (fieldInDispStyle == "040")
				{
					string fieldInDispStyleTxt = field.FieldInDispStyleTxt;
					chrArray = new char[] { '|' };
					string[] strArrays1 = fieldInDispStyleTxt.Split(chrArray);
					str = string.Concat(str, "select ItemCode,ItemName from T_E_Sys_DictEntry where DictID='", strArrays1[1]);
					str = string.Concat(str, "' order by  Itemorder");
					dataTable = SysDatabase.ExecuteTable(str);
					if ((int)strArrays1.Length > 2)
					{
						string str4 = strArrays1[2];
						chrArray = new char[] { ',' };
						strArrays = str4.Split(chrArray);
					}
					stringBuilder.Append(this.RadioCustom(cName, field, strArrays, dataTable, stringCollections, str1));
				}
				else if (fieldInDispStyle == "041")
				{
					string fieldInDispStyleTxt1 = field.FieldInDispStyleTxt;
					chrArray = new char[] { '\u0060' };
					string[] strArrays2 = fieldInDispStyleTxt1.Split(chrArray);
					string str5 = strArrays2[0];
					chrArray = new char[] { ',' };
					string[] strArrays3 = str5.Split(chrArray);
					if ((int)strArrays2.Length > 1)
					{
						if (strArrays2[1].IndexOf(",") <= -1)
						{
							string str6 = strArrays2[1].Replace("\r\n", "^");
							chrArray = new char[] { '\u005E' };
							strArrays3 = str6.Split(chrArray);
						}
						else
						{
							string str7 = strArrays2[1];
							chrArray = new char[] { ',' };
							strArrays3 = str7.Split(chrArray);
						}
						string str8 = strArrays2[0];
						chrArray = new char[] { ',' };
						strArrays = str8.Split(chrArray);
					}
					DataTable dataTable1 = new DataTable();
					dataTable1.Columns.Add("c");
					dataTable1.Columns.Add("n");
					for (int i = 0; i < (int)strArrays3.Length; i++)
					{
						string str9 = strArrays3[i];
						chrArray = new char[] { '|' };
						string[] strArrays4 = str9.Split(chrArray);
						string str10 = strArrays4[0];
						string str11 = ((int)strArrays4.Length > 1 ? strArrays4[1] : str10);
						if (str11.Length != 0)
						{
							DataRow dataRow = dataTable1.NewRow();
							dataRow[0] = str11;
							dataRow[1] = str10;
							dataTable1.Rows.Add(dataRow);
						}
					}
					stringBuilder.Append(this.RadioCustom(cName, field, strArrays, dataTable1, stringCollections, str1));
				}
				else if (fieldInDispStyle == "042")
				{
					string fieldInDispStyleTxt2 = field.FieldInDispStyleTxt;
					chrArray = new char[] { '\u0060' };
					string[] strArrays5 = fieldInDispStyleTxt2.Split(chrArray);
					if ((int)strArrays5.Length <= 1)
					{
						str = this.ReplaceParaValue(this.GetParaValue("DropValueSqlPara"), field.FieldInDispStyleTxt);
					}
					else
					{
						str = this.ReplaceParaValue(this.GetParaValue("DropValueSqlPara"), strArrays5[1]);
						string str12 = strArrays5[0];
						chrArray = new char[] { ',' };
						strArrays = str12.Split(chrArray);
					}
					dataTable = SysDatabase.ExecuteTable(str);
					stringBuilder.Append(this.RadioCustom(cName, field, strArrays, dataTable, stringCollections, str1));
				}
			}
			return stringBuilder.ToString();
		}
        /// <summary>
        /// 生成Radio列表相关格式的字符串
        /// </summary>
		private string GetRadioStr(EIS.DataModel.Model.FieldInfo field)
		{
			DataRow row = null;
			string str;
			char[] chrArray;
			StringBuilder stringBuilder = new StringBuilder();
			string str1 = "";
			string fieldInDispStyle = field.FieldInDispStyle;
			if (fieldInDispStyle != null)
			{
				if (fieldInDispStyle == "040")
				{
					str1 = string.Concat(str1, "select ItemCode,ItemName from T_E_Sys_DictEntry where DictID='", field.FieldInDispStyleTxt.Split("|".ToCharArray())[1]);
					str1 = string.Concat(str1, "' order by  Itemorder");
					foreach (DataRow rowa in SysDatabase.ExecuteTable(str1).Rows)
					{
                        stringBuilder.AppendFormat("{0}|{1},", rowa["ItemName"], rowa["ItemCode"]);
					}
				}
				else if (fieldInDispStyle == "041")
				{
					string fieldInDispStyleTxt = field.FieldInDispStyleTxt;
					chrArray = new char[] { '\u0060' };
					string[] strArrays = fieldInDispStyleTxt.Split(chrArray);
					if ((int)strArrays.Length <= 1)
					{
						str = strArrays[0].Replace("\r\n", ",");
						stringBuilder.Append(strArrays[0]);
					}
					else
					{
						str = strArrays[1].Replace("\r\n", ",");
						stringBuilder.Append(str);
					}
				}
				else if (fieldInDispStyle == "042")
				{
					string fieldInDispStyleTxt1 = field.FieldInDispStyleTxt;
					chrArray = new char[] { '\u0060' };
					string[] strArrays1 = fieldInDispStyleTxt1.Split(chrArray);
					str1 = ((int)strArrays1.Length <= 1 ? this.ReplaceParaValue(this.GetParaValue("DropValueSqlPara"), field.FieldInDispStyleTxt) : this.ReplaceParaValue(this.GetParaValue("DropValueSqlPara"), strArrays1[1]));
					str1 = this.ReplaceParaValue(this.GetParaValue("DropValueSqlPara"), str1);
					str1 = this._page.ReplaceContext(str1);
					foreach (DataRow dataRow in SysDatabase.ExecuteTable(str1).Rows)
					{
						stringBuilder.AppendFormat("{0}|{1},", dataRow[1], dataRow[0]);
					}
				}
			}
			return stringBuilder.ToString();
		}

		public string GetRelationList(string mainTbl, string subName, string parentId)
		{
			string str;
			StringBuilder stringBuilder = new StringBuilder();
			_TableInfo __TableInfo = new _TableInfo(mainTbl);
			string formHtml = __TableInfo.GetModel().FormHtml;
			if (this.Sindex != "")
			{
				string tableStyle = __TableInfo.GetTableStyle(this.Sindex, 0);
				if (tableStyle != "")
				{
					formHtml = tableStyle;
				}
			}
			HtmlDocument htmlDocument = new HtmlDocument()
			{
				OptionOutputOriginalCase = true
			};
			htmlDocument.LoadHtml(formHtml);
			HtmlNode elementbyId = htmlDocument.GetElementbyId(subName);
			if (elementbyId != null)
			{
				string str1 = "";
				HtmlNode htmlNode = elementbyId.SelectSingleNode("thead");
				HtmlNode htmlNode1 = elementbyId.SelectSingleNode("tbody");
				str1 = (htmlNode == null ? elementbyId.FirstChild.OuterHtml : htmlNode.OuterHtml);
				HtmlNode htmlNode2 = htmlNode1.SelectSingleNode("tr");
				htmlNode2.SetAttributeValue("Id", "[#BODYID#]");
				htmlNode2.SetAttributeValue("class", "dataRow");
				string outerHtml = elementbyId.OuterHtml;
				string outerHtml1 = htmlNode1.OuterHtml;
				string outerHtml2 = htmlNode2.OuterHtml;
				string[] strArrays = new string[] { "select * from ", subName, " where _MainID='", parentId, "' order by _CreateTime" };
				string str2 = string.Concat(strArrays);
				DataTable dataTable = new DataTable();
				dataTable = SysDatabase.ExecuteTable(str2);
				StringBuilder stringBuilder1 = new StringBuilder();
				stringBuilder1.AppendFormat("<tbody>", new object[0]);
				if (dataTable.Rows.Count > 0)
				{
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						string str3 = this.ReplaceWithDataRow(outerHtml2, dataTable.Rows[i], i);
						string str4 = dataTable.Rows[i]["_AutoID"].ToString();
						str3 = str3.Replace("[#BODYID#]", str4);
						strArrays = new string[] { "<a class='subdelbtn' title='删除行' href='javascript:' onclick=\"_fnSubDelConfirm('", subName, "','", str4, "')\">&nbsp;</a>" };
						string str5 = string.Concat(strArrays);
						str3 = str3.Replace("[#DELBTN#]", str5).Replace("[#DEL#]", str5);
						strArrays = new string[] { "<a class='subeditbtn' title='编辑行' href='javascript:' onclick=\"_fnSubEdit('", subName, "','", str4, "')\">&nbsp;</a>" };
						str3 = str3.Replace("[#EDIT#]", string.Concat(strArrays));
						strArrays = new string[] { "<a class='subcopybtn' title='复制行' href='javascript:' onclick=\"_fnSubCopy('", subName, "','", str4, "')\">&nbsp;</a>" };
						string str6 = string.Concat(strArrays);
						stringBuilder1.Append(str3.Replace("[#COPY#]", str6));
					}
				}
				stringBuilder1.Append("</tbody>");
				string str7 = "操作";
				formHtml = outerHtml.Replace(outerHtml1, stringBuilder1.ToString()).Replace("[#ADDBTN#]", str7).Replace("[#ADD#]", str7);
				str = formHtml;
			}
			else
			{
				str = "";
			}
			return str;
		}

		private string GetSignPrintStyle()
		{
			if (this.signPrintStyle == "")
			{
				this.signPrintStyle = SysConfig.GetConfig("WF_SignPrintStyle").ItemValue;
			}
			return this.signPrintStyle;
		}
        /// <summary>
        /// 生成单个图片上传
        /// </summary>
        /// <param name="field">字段信息</param>
        /// <param name="cName">中文名称</param>
        /// <param name="fldval"></param>
        /// <returns></returns>
		private string GetSingleImage(EIS.DataModel.Model.FieldInfo field, string cName, string fldval)
		{
			string str;
			if (field.FieldInDispStyleTxt.Trim() == "")
			{
				field.FieldInDispStyleTxt = "||1||";
			}
			string[] strArrays = field.FieldInDispStyleTxt.Split("|".ToCharArray());
			string styleStr = "";
			string styleStr1 = "";
			string str1 = ClsCommon.DbnullToString(field.FieldWidth);
			string str2 = ClsCommon.DbnullToString(field.FieldHeight);
			styleStr = this.GetStyleStr(str1, str2, true);
			styleStr1 = this.GetStyleStr(str1, str2, false);
			if (field.FieldRead != 2)
			{
				StringBuilder stringBuilder = new StringBuilder();
				fldval = (fldval == "" ? Guid.NewGuid().ToString() : fldval);
				StringBuilder stringBuilder1 = stringBuilder;
				object[] fieldOdr = new object[] { field.FieldOdr, fldval, null, null, null, null, null };
				fieldOdr[2] = (field.FieldWidth == "" ? "100%" : field.FieldWidth);
				fieldOdr[3] = (field.FieldHeight == "" ? "80px" : field.FieldHeight);
				fieldOdr[4] = field.TableName;
				fieldOdr[5] = field.FieldRead;
				fieldOdr[6] = HttpContext.Current.Server.UrlEncode(Security.EncryptStr(string.Concat("set=", field.FieldInDispStyleTxt), this._page.UserName));
				stringBuilder1.AppendFormat("<iframe id=\"frm_{0}\" class='imageFrame' frameborder=\"0\" scrolling=\"auto\"  src=\"../Common/AvatarUpload.aspx?para={6}&appName={4}&appId={1}&read={5}\" width=\"{2}\" height=\"{3}\"></iframe>", fieldOdr);
				stringBuilder.AppendFormat("<input type='hidden' id='{0}' name='{0}' value='{1}'/>", cName, fldval);
				str = stringBuilder.ToString();
			}
			else if (!(fldval == ""))
			{
				string str3 = strArrays[2];
				string str4 = strArrays[3];
				str = string.Format("<img src='../common/filedown.aspx?appId={0}' alt='{1}' style='{2}' />", fldval, str3, str4);
			}
			else
			{
				str = "";
			}
			return str;
		}
        /// <summary>
        /// 取得style字符串
        /// </summary>
        /// <param name="width">控件宽度</param>
        /// <param name="height">控件高度</param>
        /// <param name="isdisp">是否显示</param>
        /// <returns>返回style字符串</returns>
		private string GetStyleStr(string width, string height, bool isdisp)
		{
			string str = "";
			if (width != "")
			{
				width = string.Concat("style=\"width:", (width.EndsWith("%") ? width : string.Concat(width, "px;")));
				if (!(height == "" ? true : isdisp))
				{
					str = string.Concat(width, ";height:", (height.EndsWith("%") ? height : string.Concat(height, "px;")), ";display:none\"");
				}
				else if (!(height != "" ? true : isdisp))
				{
					str = string.Concat(width, ";display:none\"");
				}
				else if (!(height == "" ? true : !isdisp))
				{
					str = string.Concat(width, ";height:", (height.EndsWith("%") ? height : string.Concat(height, "px;")), "\"");
				}
				else if ((height != "" ? false : isdisp))
				{
					str = string.Concat(width, "\"");
				}
			}
			else if (!(height == "" ? true : isdisp))
			{
				str = string.Concat("style=\"height:", height, ";display:none\"");
			}
			else if (!(height != "" ? true : isdisp))
			{
				str = "style=\"display:none\"";
			}
			else if (!(height == "" ? true : !isdisp))
			{
				str = string.Concat("style=\"height:", (height.EndsWith("%") ? height : string.Concat(height, "px;")), "\"");
			}
			else if ((height != "" ? false : isdisp))
			{
				str = "";
			}
			return str;
		}

	
		private string GetTaskDealInfo(string actId, Match m)
		{
			DataRow row;
			string str;
			DataRow[] dataRowArray;
			int i;
			string value = m.Groups[1].Value;
			StringBuilder stringBuilder = new StringBuilder();
			DataRow[] dataRowArray1 = this.adviceList.Select(string.Concat("ActivityId='", actId, "' and TaskState='2'"), " DealTime asc");
			if ((int)dataRowArray1.Length != 0)
			{
				DataRow dataRow = dataRowArray1[(int)dataRowArray1.Length - 1];
				string str1 = dataRow["DealTime"].ToString().Trim();
				if (!(str1 == ""))
				{
					string value1 = "";
					StringCollection stringCollections = new StringCollection();
					string lower = value.ToLower();
					if (lower != null)
					{
						switch (lower)
						{
							case "advice":
							{
								value1 = "last";
								if (m.Groups.Count > 3)
								{
									value1 = m.Groups[4].Value;
								}
								if (!(value1.ToLower() == "all"))
								{
									stringBuilder.Append(dataRow["DealAdvice"].ToString().Replace("\r\n", "<br>"));
									stringBuilder.AppendFormat("〔{0}〕", dataRow["EmployeeName"]);
								}
								else
								{
									dataRowArray = dataRowArray1;
									for (i = 0; i < (int)dataRowArray.Length; i++)
									{
										row = dataRowArray[i];
										string str2 = row["DealAdvice"].ToString();
										stringBuilder.Append(str2.Replace("\r\n", "<br>"));
										stringBuilder.AppendFormat("〔{0}〕", row["EmployeeName"]);
									}
								}
								break;
							}
							case "action":
							{
								stringBuilder.Append(dataRow["DealAction"].ToString());
								break;
							}
							case "employee":
							{
								stringBuilder.Append(dataRow["EmployeeName"].ToString());
								break;
							}
							case "employees":
							{
								dataRowArray = dataRowArray1;
								for (i = 0; i < (int)dataRowArray.Length; i++)
								{
									row = dataRowArray[i];
									string str3 = row["OwnerId"].ToString();
									if (!stringCollections.Contains(str3))
									{
										stringCollections.Add(str3);
										stringBuilder.Append(string.Concat(row["EmployeeName"].ToString(), "&nbsp;"));
									}
								}
								break;
							}
							case "position":
							{
								stringBuilder.Append(dataRow["positionName"].ToString());
								break;
							}
							case "deptname":
							{
								stringBuilder.Append(dataRow["deptname"].ToString());
								break;
							}
							case "companyname":
							{
								stringBuilder.Append(dataRow["companyname"].ToString());
								break;
							}
							case "signature":
							{
								string str4 = dataRow["OwnerId"].ToString();
								string employeeName = EmployeeService.GetEmployeeName(str4);
								string str5 = string.Format("<img class='signimg' src='../Common/SignImage.aspx?uid={0}' alt='{1}'/>", str4, employeeName);
								stringBuilder.Append(str5);
								break;
							}
							case "signatures":
							{
								dataRowArray = dataRowArray1;
								for (i = 0; i < (int)dataRowArray.Length; i++)
								{
									row = dataRowArray[i];
									string str6 = row["OwnerId"].ToString();
									if (!stringCollections.Contains(str6))
									{
										stringCollections.Add(str6);
										string employeeName1 = EmployeeService.GetEmployeeName(str6);
										string str7 = string.Format("<img class='signimg' src='../Common/SignImage.aspx?uid={0}' alt='{1}'/>", str6, employeeName1);
										stringBuilder.Append(str7);
									}
								}
								break;
							}
							case "time":
							{
								value1 = "yyyy-MM-dd HH:mm";
								if (m.Groups.Count > 3)
								{
									value1 = m.Groups[4].Value;
								}
								stringBuilder.Append((str1 == "" ? "" : Convert.ToDateTime(str1).ToString(value1)));
								break;
							}
							case "sign":
							{
								Regex regex = new Regex("{(Advice|Employee|Signature|Signatures|Time|Position|DeptName|CompanyName)(:(.*?))?}", RegexOptions.IgnoreCase);
								string signPrintStyle = this.GetSignPrintStyle();
								DataView defaultView = this.adviceList.DefaultView;
								defaultView.Sort = "DealTime asc";
								defaultView.RowFilter = string.Concat("ActivityId='", actId, "'");
								foreach (DataRow rowA in defaultView.ToTable().Rows)
								{
                                    this.curRow = rowA;
                                    if (rowA.RowState != DataRowState.Deleted)
									{
										stringBuilder.Append(regex.Replace(signPrintStyle, new MatchEvaluator(this.GetTaskDealInfo)));
									}
								}
								break;
							}
							case "block":
							{
								break;
							}
							default:
							{
								goto Label1;
							}
						}
					}
					else
					{
					}
				Label1:
					str = stringBuilder.ToString();
				}
				else
				{
					str = "";
				}
			}
			else
			{
				str = "";
			}
			return str;
		}

		private string GetTaskDealInfo(Match m)
		{
			string str;
			DataRow dataRow = this.curRow;
			string value = m.Groups[1].Value;
			StringBuilder stringBuilder = new StringBuilder();
			string str1 = dataRow["DealTime"].ToString().Trim();
			if (!(str1 == ""))
			{
				string lower = value.ToLower();
				if (lower != null)
				{
					switch (lower)
					{
						case "advice":
						{
							string str2 = dataRow["DealAdvice"].ToString();
							stringBuilder.Append((str2.Length > this.maxAdviceLen ? "意见附后" : str2.Replace("\r\n", "<br>")));
							break;
						}
						case "position":
						{
							stringBuilder.Append(dataRow["positionName"].ToString());
							break;
						}
						case "deptname":
						{
							stringBuilder.Append(dataRow["deptname"].ToString());
							break;
						}
						case "companyname":
						{
							stringBuilder.Append(dataRow["companyname"].ToString());
							break;
						}
						case "employee":
						{
							stringBuilder.Append(dataRow["employeeName"].ToString());
							break;
						}
						case "signature":
						{
							string str3 = dataRow["OwnerId"].ToString();
							string employeeName = EmployeeService.GetEmployeeName(str3);
							string str4 = string.Format("<img class='signimg' src='../Common/SignImage.aspx?uid={0}' alt='{1}'>", str3, employeeName);
							stringBuilder.Append(str4);
							break;
						}
						case "time":
						{
							string value1 = "yyyy-MM-dd HH:mm";
							if (m.Groups.Count > 3)
							{
								value1 = m.Groups[3].Value;
							}
							stringBuilder.Append((str1 == "" ? "" : Convert.ToDateTime(str1).ToString(value1)));
							break;
						}
						default:
						{
							goto Label1;
						}
					}
				}
				else
				{
				}
			Label1:
				str = stringBuilder.ToString();
			}
			else
			{
				str = "";
			}
			return str;
		}


        /// <summary>
        /// 开始替换用来将指定字符进行加密的[CRYPT][/CRYPT]标记
        /// </summary>
        /// <param name="oldstr">旧字符串</param>
        /// <param name="beginstr">开始字符串</param>
        /// <param name="endstr">结束字符串</param>
        /// <param name="crypt_key">加密串</param>
        /// <returns>返回新的字符串</returns>
		public string GetUbbCode(string oldstr, string beginstr, string endstr, string crypt_key)
		{
			StringBuilder stringBuilder = new StringBuilder();
			MatchCollection matchCollections = Regex.Matches(oldstr, "\\[CRYPT\\](.*)\\[/CRYPT\\]");
			int index = 0;
			foreach (Match match in matchCollections)
			{
				stringBuilder.Append(oldstr.Substring(index, match.Index - index));
				string str = Security.EncryptStr(match.Groups[1].Value, crypt_key);
				stringBuilder.Append(string.Concat("Para=", str));
				index = match.Index + match.Length;
			}
			if (index < oldstr.Length)
			{
				stringBuilder.Append(oldstr.Substring(index));
			}
			return stringBuilder.ToString();
		}

        /// <summary>
        /// 生成WebOffice
        /// </summary>
        /// <param name="field">字段信息</param>
        /// <param name="cName">中文名称</param>
        /// <param name="fldval"></param>
        /// <returns></returns>
		private string GetWebOffice(EIS.DataModel.Model.FieldInfo field, string cName, string fldval)
		{
			string[] strArrays = field.FieldInDispStyleTxt.Split("|".ToCharArray());
			string styleStr = "";
			string str = "";
			string str1 = ClsCommon.DbnullToString(field.FieldWidth);
			string str2 = ClsCommon.DbnullToString(field.FieldHeight);
			styleStr = this.GetStyleStr(str1, str2, true);
			str = this.GetStyleStr(str1, str2, false);
			StringBuilder stringBuilder = new StringBuilder();
			fldval = (fldval == "" ? Guid.NewGuid().ToString() : fldval);
			string paraValue = "";
			if (!(strArrays[1] != "1" ? true : strArrays[2].Length <= 0))
			{
				paraValue = this._page.GetParaValue(strArrays[2]);
			}
			else if (strArrays[1] == "2")
			{
				paraValue = strArrays[2];
			}
			StringBuilder stringBuilder1 = stringBuilder;
			object[] fieldOdr = new object[] { field.FieldOdr, fldval, null, null, null, null, null, null };
			fieldOdr[2] = (field.FieldWidth == "" ? "100%" : field.FieldWidth);
			fieldOdr[3] = (field.FieldHeight == "" ? "400px" : field.FieldHeight);
			fieldOdr[4] = field.TableName;
			fieldOdr[5] = strArrays[0];
			fieldOdr[6] = field.FieldRead;
			fieldOdr[7] = paraValue;
			stringBuilder1.AppendFormat("<iframe id=\"editoffice_{0}\" frameborder=\"0\" scrolling=\"no\"  src=\"../Common/WebOffice.aspx?folderName={4}&fileId={1}&newofficetype={5}&read={6}&mburl={7}\" width=\"{2}\" height=\"{3}\"></iframe>", fieldOdr);
			stringBuilder.AppendFormat("<input type='hidden' id='{0}' value='{1}'/>", cName, fldval);
			return stringBuilder.ToString();
		}

		public void MergerDataControl(string tblName, List<EIS.DataModel.Model.FieldInfo> dtFields)
		{
			if (this.DataControl != null)
			{
				try
				{
					foreach (EIS.AppModel.DataControl dataControl in this.DataControl.FindAll((EIS.AppModel.DataControl f) => f.BizName == tblName))
					{
						if (!AppFields.Contains(dataControl.FieldName))
						{
							EIS.DataModel.Model.FieldInfo defaultType = dtFields.Find((EIS.DataModel.Model.FieldInfo f) => f.FieldName.ToLower() == dataControl.FieldName.ToLower());
							if (defaultType != null)
							{
								if (dataControl.CanRead.HasValue)
								{
									defaultType.FieldRead = (dataControl.CanRead.Value ? 1 : 0);
								}
								if (dataControl.NotNull.HasValue)
								{
									defaultType.FieldNull = (dataControl.NotNull.Value ? 1 : 0);
								}
								if (dataControl.DefaultValue.Trim() != "")
								{
									defaultType.FieldDValueType = dataControl.DefaultType;
									defaultType.FieldDValue = dataControl.DefaultValue;
								}
							}
						}
					}
				}
				catch (Exception exception)
				{
					this.fileLogger.Error<Exception>(exception);
					throw new Exception("处理数据控制参数DataControl时出现异常");
				}
			}
		}
       
		private string RadioCustom(string cName, EIS.DataModel.Model.FieldInfo field, string[] arrLayout, DataTable dt, StringCollection scData, string eventStr)
		{
			int i;
			int num;
			int num1;
			int j;
			int k;
			DataRow item;
			int num2;
			object[] objArray;
			int num3;
			object obj;
			object obj1;
			object obj2;
			object obj3;
			object obj4;
			object obj5;
			StringBuilder stringBuilder = new StringBuilder();
			if (field.FieldRead == 2)
			{
				if ((int)arrLayout.Length == 1)
				{
					for (i = 0; i < dt.Rows.Count; i++)
					{
						StringBuilder stringBuilder1 = stringBuilder;
						objArray = new object[] { i, cName, dt.Rows[i][0], null, null, null, null, null };
						objArray[3] = (scData.Contains(dt.Rows[i][0].ToString()) ? "checked" : "");
						objArray[4] = dt.Rows[i][1];
						objArray[5] = "";
						objArray[6] = (field.FieldInDisp == 1 ? this.GetClassName(ClsCommon.ControlType.Radio) : "hidden");
						objArray[7] = (field.FieldRead == 1 ? "disabled" : "");
						stringBuilder1.AppendFormat("<input type='radio' class='{6}' {5} {7}   {3} value='{2}' name='{1}' id='{1}_{0}' /><label for='{1}_{0}'>{4}</label>&nbsp;", objArray);
					}
				}
				else if (!(arrLayout[0] == "" ? false : !(arrLayout[0] == "0")))
				{
					for (i = 0; i < dt.Rows.Count; i++)
					{
						StringBuilder stringBuilder2 = stringBuilder;
						objArray = new object[] { i, cName, dt.Rows[i][0], null, null, null, null, null, null, null };
						objArray[3] = (scData.Contains(dt.Rows[i][0].ToString()) ? "checked" : "");
						objArray[4] = dt.Rows[i][1];
						objArray[5] = "";
						objArray[6] = (field.FieldInDisp == 1 ? this.GetClassName(ClsCommon.ControlType.Radio) : "hidden");
						objArray[7] = (field.FieldRead == 1 ? "disabled" : "");
						objArray[8] = string.Concat(field.TableName, "_", field.FieldName);
						object[] objArray1 = objArray;
						if (arrLayout[3] == "1")
						{
							num3 = i + 1;
							obj5 = string.Concat(num3.ToString(), ".&nbsp;");
						}
						else
						{
							obj5 = "";
						}
						objArray1[9] = obj5;
						stringBuilder2.AppendFormat("<input type='radio' class='{6} {8}' {5} {7}   {3} value='{2}' name='{1}' id='{1}_{0}' /><label for='{1}_{0}'>{9}{4}</label>&nbsp;", objArray);
					}
				}
				else if (arrLayout[0] == "1")
				{
					num = 1;
					if (arrLayout[2] != "")
					{
						num = Convert.ToInt32(arrLayout[2]);
					}
					stringBuilder.AppendFormat("<table class='noborder checktbl{0}'>", (field.FieldInDisp == 1 ? "" : " hidden"));
					if (!(arrLayout[1] == "1"))
					{
						num2 = (int)Math.Ceiling((double)dt.Rows.Count / (double)num);
						for (j = 0; j < num; j++)
						{
							stringBuilder.Append("<tr>");
							for (k = 0; k < num2; k++)
							{
								if (k * num + j >= dt.Rows.Count)
								{
									stringBuilder.Append("<td></td>");
								}
								else
								{
									item = dt.Rows[k * num + j];
									StringBuilder stringBuilder3 = stringBuilder;
									objArray = new object[] { k * num + j, cName, item[0], null, null, null, null, null, null, null };
									objArray[3] = (scData.Contains(item[0].ToString()) ? "checked" : "");
									objArray[4] = item[1];
									objArray[5] = "";
									objArray[6] = (field.FieldInDisp == 1 ? this.GetClassName(ClsCommon.ControlType.Radio) : "hidden");
									objArray[7] = (field.FieldRead == 1 ? "disabled" : "");
									objArray[8] = string.Concat(field.TableName, "_", field.FieldName);
									object[] objArray2 = objArray;
									if (arrLayout[3] == "1")
									{
										num3 = k * num + j + 1;
										obj3 = string.Concat(num3.ToString(), ".&nbsp;");
									}
									else
									{
										obj3 = "";
									}
									objArray2[9] = obj3;
									stringBuilder3.AppendFormat("<td><input type='radio' class='{6} {8}' {5} {7} {3} value='{2}' name='{1}' id='{1}_{0}' onclick='javascript:return false;' /><label for='{1}_{0}'>{9}{4}</label>&nbsp;</td>", objArray);
								}
							}
							stringBuilder.Append("<tr>");
						}
					}
					else
					{
						num1 = (int)Math.Ceiling((double)dt.Rows.Count / (double)num);
						for (j = 0; j < num1; j++)
						{
							stringBuilder.Append("<tr>");
							for (k = 0; k < num; k++)
							{
								if (j * num + k >= dt.Rows.Count)
								{
									stringBuilder.Append("<td></td>");
								}
								else
								{
									item = dt.Rows[j * num + k];
									StringBuilder stringBuilder4 = stringBuilder;
									objArray = new object[] { j * num + k, cName, item[0], null, null, null, null, null, null, null };
									objArray[3] = (scData.Contains(item[0].ToString()) ? "checked" : "");
									objArray[4] = item[1];
									objArray[5] = "";
									objArray[6] = (field.FieldInDisp == 1 ? this.GetClassName(ClsCommon.ControlType.Radio) : "hidden");
									objArray[7] = (field.FieldRead == 1 ? "disabled" : "");
									objArray[8] = string.Concat(field.TableName, "_", field.FieldName);
									object[] objArray3 = objArray;
									if (arrLayout[3] == "1")
									{
										num3 = j * num + k + 1;
										obj4 = string.Concat(num3.ToString(), ".&nbsp;");
									}
									else
									{
										obj4 = "";
									}
									objArray3[9] = obj4;
									stringBuilder4.AppendFormat("<td><input type='radio' class='{6} {8}' {5} {7} {3} value='{2}' name='{1}' id='{1}_{0}' onclick='javascript:return false;' /><label for='{1}_{0}'>{9}{4}</label>&nbsp;</td>", objArray);
								}
							}
							stringBuilder.Append("<tr>");
						}
					}
					stringBuilder.AppendFormat("</table>", new object[0]);
				}
			}
			else if ((int)arrLayout.Length == 1)
			{
				for (i = 0; i < dt.Rows.Count; i++)
				{
					StringBuilder stringBuilder5 = stringBuilder;
					objArray = new object[] { i, cName, dt.Rows[i][0], null, null, null, null, null };
					objArray[3] = (scData.Contains(dt.Rows[i][0].ToString()) ? "checked" : "");
					objArray[4] = dt.Rows[i][1];
					objArray[5] = eventStr;
					objArray[6] = (field.FieldInDisp == 1 ? this.GetClassName(ClsCommon.ControlType.Radio) : "hidden");
					objArray[7] = (field.FieldRead == 1 ? "disabled" : "");
					stringBuilder5.AppendFormat("<input type='radio' class='{6}' {5} {7}   {3} value='{2}' name='{1}' id='{1}_{0}' /><label for='{1}_{0}'>{4}</label>&nbsp;", objArray);
				}
			}
			else if (!(arrLayout[0] == "" ? false : !(arrLayout[0] == "0")))
			{
				for (i = 0; i < dt.Rows.Count; i++)
				{
					StringBuilder stringBuilder6 = stringBuilder;
					objArray = new object[] { i, cName, dt.Rows[i][0], null, null, null, null, null, null, null };
					objArray[3] = (scData.Contains(dt.Rows[i][0].ToString()) ? "checked" : "");
					objArray[4] = dt.Rows[i][1];
					objArray[5] = eventStr;
					objArray[6] = (field.FieldInDisp == 1 ? this.GetClassName(ClsCommon.ControlType.Radio) : "hidden");
					objArray[7] = (field.FieldRead == 1 ? "disabled" : "");
					objArray[8] = string.Concat(field.TableName, "_", field.FieldName);
					object[] objArray4 = objArray;
					if (arrLayout[3] == "1")
					{
						num3 = i + 1;
						obj2 = string.Concat(num3.ToString(), ".&nbsp;");
					}
					else
					{
						obj2 = "";
					}
					objArray4[9] = obj2;
					stringBuilder6.AppendFormat("<input type='radio' class='{6} {8}' {5} {7}   {3} value='{2}' name='{1}' id='{1}_{0}' /><label for='{1}_{0}'>{9}{4}</label>&nbsp;", objArray);
				}
			}
			else if (arrLayout[0] == "1")
			{
				num = 1;
				if (arrLayout[2] != "")
				{
					num = Convert.ToInt32(arrLayout[2]);
				}
				stringBuilder.AppendFormat("<table class='noborder checktbl{0}'>", (field.FieldInDisp == 1 ? "" : " hidden"));
				if (!(arrLayout[1] == "1"))
				{
					num2 = (int)Math.Ceiling((double)dt.Rows.Count / (double)num);
					for (j = 0; j < num; j++)
					{
						stringBuilder.Append("<tr>");
						for (k = 0; k < num2; k++)
						{
							if (k * num + j >= dt.Rows.Count)
							{
								stringBuilder.Append("<td></td>");
							}
							else
							{
								item = dt.Rows[k * num + j];
								StringBuilder stringBuilder7 = stringBuilder;
								objArray = new object[] { k * num + j, cName, item[0], null, null, null, null, null, null, null };
								objArray[3] = (scData.Contains(item[0].ToString()) ? "checked" : "");
								objArray[4] = item[1];
								objArray[5] = eventStr;
								objArray[6] = (field.FieldInDisp == 1 ? this.GetClassName(ClsCommon.ControlType.Radio) : "hidden");
								objArray[7] = (field.FieldRead == 1 ? "disabled" : "");
								objArray[8] = string.Concat(field.TableName, "_", field.FieldName);
								object[] objArray5 = objArray;
								if (arrLayout[3] == "1")
								{
									num3 = k * num + j + 1;
									obj = string.Concat(num3.ToString(), ".&nbsp;");
								}
								else
								{
									obj = "";
								}
								objArray5[9] = obj;
								stringBuilder7.AppendFormat("<td><input type='radio' class='{6} {8}' {5} {7} {3} value='{2}' name='{1}' id='{1}_{0}' /><label for='{1}_{0}'>{9}{4}</label>&nbsp;</td>", objArray);
							}
						}
						stringBuilder.Append("<tr>");
					}
				}
				else
				{
					num1 = (int)Math.Ceiling((double)dt.Rows.Count / (double)num);
					for (j = 0; j < num1; j++)
					{
						stringBuilder.Append("<tr>");
						for (k = 0; k < num; k++)
						{
							if (j * num + k >= dt.Rows.Count)
							{
								stringBuilder.Append("<td></td>");
							}
							else
							{
								item = dt.Rows[j * num + k];
								StringBuilder stringBuilder8 = stringBuilder;
								objArray = new object[] { j * num + k, cName, item[0], null, null, null, null, null, null, null };
								objArray[3] = (scData.Contains(item[0].ToString()) ? "checked" : "");
								objArray[4] = item[1];
								objArray[5] = eventStr;
								objArray[6] = (field.FieldInDisp == 1 ? this.GetClassName(ClsCommon.ControlType.Radio) : "hidden");
								objArray[7] = (field.FieldRead == 1 ? "disabled" : "");
								objArray[8] = string.Concat(field.TableName, "_", field.FieldName);
								object[] objArray6 = objArray;
								if (arrLayout[3] == "1")
								{
									num3 = j * num + k + 1;
									obj1 = string.Concat(num3.ToString(), ".&nbsp;");
								}
								else
								{
									obj1 = "";
								}
								objArray6[9] = obj1;
								stringBuilder8.AppendFormat("<td><input type='radio' class='{6} {8}' {5} {7} {3} value='{2}' name='{1}' id='{1}_{0}' /><label for='{1}_{0}'>{9}{4}</label>&nbsp;</td>", objArray);
							}
						}
						stringBuilder.Append("<tr>");
					}
				}
				stringBuilder.AppendFormat("</table>", new object[0]);
			}
			return stringBuilder.ToString();
		}

		private string ReplaceMatch(Match m)
		{
			string value = m.Groups[2].Value;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string str in this.idCodeHash)
			{
				if (str.StartsWith(string.Concat(value, "|")))
				{
					char[] chrArray = new char[] { '|' };
					string str1 = str.Split(chrArray)[1];
					stringBuilder.Append(this.GetTaskDealInfo(str1, m));
				}
			}
			return stringBuilder.ToString();
		}
        /// <summary>
        /// 替换字符串中的参数为参数字符串中对应的值
        /// </summary>
        /// <param name="parastr">参数字符串，用“|”分割，通常为“@dd=23|@rr=e|@tt=34”</param>
        /// <param name="replacestr">要替换的字符串，通常包含参数名，如“getstring('@dd')”</param>
        /// <returns>返回替换后的字符串，如上例中替换后的字符串为“getstring('23')”</returns>
		public string ReplaceParaValue(string parastr, string replacestr)
		{
			string[] strArrays;
			string[] strArrays1 = parastr.Split("|".ToCharArray());
			string str = "";
			string str1 = "";
			string str2 = "";
			string str3 = "";
			str1 = replacestr;
			for (int i = 0; i < (int)strArrays1.Length; i++)
			{
				if (strArrays1[i] != "")
				{
					str2 = strArrays1[i].Split("=".ToCharArray())[0].Trim();
					str = strArrays1[i].Substring(str2.Length + 1, strArrays1[i].Length - str2.Length - 1);
					while (str.IndexOf("[!") != str.IndexOf("!]"))
					{
						str3 = str.Substring(str.IndexOf("[!") + 2, str.IndexOf("!]") - str.IndexOf("[!") - 2);
						str = (this._page.Session[str3] == null ? str.Replace(string.Concat("[!", str3, "!]"), "") : str.Replace(string.Concat("[!", str3, "!]"), this._page.Session[str3].ToString()));
					}
					string str4 = str;
					if (str4 != null)
					{
						switch (str4)
						{
							case "{username}":
							{
								str = this._page.Session["username"].ToString();
								break;
							}
							case "{employeename}":
							{
								str = this._page.Session["employeename"].ToString();
								break;
							}
							case "{deptname}":
							{
								str = this._page.Session["deptname"].ToString();
								break;
							}
							case "{deptcode}":
							{
								str = this._page.Session["deptcode"].ToString();
								break;
							}
							case "{deptid}":
							{
								str = this._page.Session["deptid"].ToString();
								break;
							}
							case "{employeeid}":
							{
								str = this._page.Session["employeeid"].ToString();
								break;
							}
							case "{yyyy}":
							{
								str = "2015";
								break;
							}
							case "{mm}":
							{
								str = "4";
								break;
							}
							case "{dd}":
							{
								str = "21";
								break;
							}
							case "{date}":
							{
								strArrays = new string[] { "2015", "-", "4", "-", "21" };
								str = string.Concat(strArrays);
								break;
							}
							case "{now}":
							{
								strArrays = new string[] { "2015", "-", "4", "-", "21", " 11:07:58" };
								str = string.Concat(strArrays);
								break;
							}
						}
					}
					str1 = str1.Replace(strArrays1[i].Split("=".ToCharArray())[0], str);
				}
			}
			return str1;
		}
        /// <summary>
        /// 替换数据
        /// </summary>
        /// <param name="tmpl">模板</param>
        /// <param name="data">数据</param>
        /// <param name="rowIndex">索引</param>
        /// <returns></returns>
		protected string ReplaceWithDataRow(string tmpl, DataRow data, int rowIndex)
		{
			int num;
			decimal num1;
			foreach (Match match in (new Regex("{([_\\w]*\\.)?([_\\w]+):?(.*?)}", RegexOptions.IgnoreCase)).Matches(tmpl))
			{
				string value = match.Groups[2].Value;
				string str = match.Groups[3].Value;
				if (data.Table.Columns.Contains(value))
				{
					string str1 = data[value].ToString();
					Type dataType = data.Table.Columns[value].DataType;
					if (!(typeof(DateTime) != dataType ? true : data[value] == DBNull.Value))
					{
						if (str.Trim() == "")
						{
							str = "yyyy-MM-dd";
						}
						str1 = ((DateTime)data[value]).ToString(str);
					}
					else if (typeof(int) == dataType)
					{
						if (data[value] != DBNull.Value)
						{
							num = Convert.ToInt32(str1);
							str1 = num.ToString("N0");
						}
					}
					else if (typeof(decimal) == dataType)
					{
						if (data[value] != DBNull.Value)
						{
							if (str1.IndexOf(".") <= 0)
							{
								num1 = Convert.ToDecimal(str1);
								str1 = num1.ToString("N");
							}
							else
							{
								char[] chrArray = new char[] { '.' };
								num = str1.Split(chrArray)[1].Length;
								string str2 = num.ToString();
								num1 = Convert.ToDecimal(str1);
								str1 = num1.ToString(string.Concat("N", str2));
							}
						}
					}
					tmpl = tmpl.Replace(match.Value, str1);
				}
			}
			tmpl = tmpl.Replace("[#ROWNUM#]", rowIndex.ToString());
			return tmpl;
		}

		public string[] splitarray(string oldval, string findval)
		{
			int num = 0;
			int length = -findval.Length;
			int num1 = 0;
			while (length != -1)
			{
				length = oldval.IndexOf(findval, length + findval.Length);
				num++;
			}
			string[] strArrays = new string[num];
			length = -findval.Length;
			num = 0;
			while (length != -1)
			{
				num1 = length;
				length = oldval.IndexOf(findval, length + findval.Length);
				if (length == -1)
				{
					strArrays[num] = oldval.Substring(num1 + findval.Length);
				}
				else
				{
					strArrays[num] = oldval.Substring(num1 + findval.Length, length - num1 - findval.Length);
				}
				num++;
			}
			return strArrays;
		}







        public string GetSubHtml(string subName, string mainId, string subId, string copyId, StringBuilder sbModel, XmlDocument xmlDoc)
        {
            List<EIS.DataModel.Model.FieldInfo> tablePhyFields;
            string str;
            Guid guid;
            this.FeScriptBlock.Length = 0;
            string formHtml = "";
            this.MainId = mainId;
            sbModel.Append("var _sysModel=");
            StringBuilder stringBuilder = new StringBuilder();
            XmlElement xmlElement = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(xmlElement);
            _TableInfo __TableInfo = new _TableInfo(subName);
            _FieldInfo __FieldInfo = new _FieldInfo();
            string str1 = "";
            TableInfo tableInfo = new TableInfo();
            try
            {
                tableInfo = __TableInfo.GetModel();
                if (tableInfo == null)
                {
                    throw new Exception(string.Format("找不到业务定义：tblName=[{0}]", subName));
                }
                if (!(tableInfo.FormHtml.Trim() == ""))
                {
                    formHtml = tableInfo.FormHtml;
                    if (!(this.Sindex != ""))
                    {
                        tablePhyFields = __FieldInfo.GetTablePhyFields(tableInfo.TableName);
                    }
                    else
                    {
                        tablePhyFields = __FieldInfo.GetFieldsStyleMerged(tableInfo.TableName, int.Parse(this.Sindex));
                        string tableStyle = __TableInfo.GetTableStyle(this.Sindex, 0);
                        if (tableStyle != "")
                        {
                            formHtml = tableStyle;
                        }
                    }
                    XmlElement xmlElement1 = xmlDoc.CreateElement("Table");
                    xmlElement1.SetAttribute("TableName", subName);
                    xmlElement.AppendChild(xmlElement1);
                    DataTable dataTable = null;
                    if (!(subId == ""))
                    {
                        str1 = string.Format(string.Concat("select * from ", subName, " where _AutoID='{0}'"), subId);
                        dataTable = SysDatabase.ExecuteTable(str1);
                        this.IsNew = false;
                        this._mainRow = dataTable.Rows[0];
                        if (this.DataContolFirst)
                        {
                            this.DealDataControlPara(subName, this._mainRow, tablePhyFields);
                        }
                    }
                    else
                    {
                        if (!(copyId != ""))
                        {
                            str1 = string.Format(string.Concat("select * from ", subName, " where 1=2"), new object[0]);
                            dataTable = SysDatabase.ExecuteTable(str1);
                            this._mainRow = dataTable.NewRow();
                            DataRow dataRow = this._mainRow;
                            guid = Guid.NewGuid();
                            dataRow["_AutoID"] = guid.ToString();
                        }
                        else
                        {
                            str1 = string.Format(string.Concat("select * from ", subName, " where _AutoID='{0}'"), copyId);
                            dataTable = SysDatabase.ExecuteTable(str1);
                            DataRow item = dataTable.Rows[0];
                            this._mainRow = dataTable.NewRow();
                            foreach (DataColumn column in dataTable.Columns)
                            {
                                this._mainRow[column.ColumnName] = item[column.ColumnName];
                            }
                            DataRow dataRow1 = this._mainRow;
                            guid = Guid.NewGuid();
                            dataRow1["_AutoID"] = guid.ToString();
                            this._mainRow["_CreateTime"] = DateTime.Now;
                            this._mainRow["_UpdateTime"] = DateTime.Now;
                        }
                        this.IsNew = true;
                        this.GenDefaultValue(subName, this._mainRow, tablePhyFields);
                        this.DealDataControlPara(subName, this._mainRow, tablePhyFields);
                    }
                    tableInfo.EditMode = "0";
                    formHtml = this.toEditHtml(tableInfo, formHtml, this._mainRow, "input0", tablePhyFields);
                    this.GetDataRowXml(xmlElement1, this._mainRow, "", tablePhyFields);
                    sbModel.AppendFormat("{{'tablename':'{0}','fields':[{1}]}}", subName, this.GetFieldModel(tablePhyFields));
                    stringBuilder.Append(formHtml);
                    foreach (EIS.DataModel.Model.FieldInfo fieldInfo in tablePhyFields.FindAll((EIS.DataModel.Model.FieldInfo f) => f.FieldInDispStyle == "004"))
                    {
                        string[] strArrays = fieldInfo.FieldInDispStyleTxt.Split(new char[] { '|' });
                        if (strArrays[0] == "3")
                        {
                            Regex regex = new Regex("{(\\w+)}", RegexOptions.IgnoreCase);
                            string str2 = strArrays[2];
                            foreach (Match match in regex.Matches(str2))
                            {
                                string value = match.Groups[1].Value;
                                EIS.DataModel.Model.FieldInfo fieldInfo1 = tablePhyFields.Find((EIS.DataModel.Model.FieldInfo fo) => fo.FieldName.ToLower() == value.ToLower());
                                if (fieldInfo1 == null)
                                {
                                    string[] fieldNameCn = new string[] { "主表字段[", fieldInfo.FieldNameCn, "]的计算公式设置错误，公式", str2, " 中的[", value, "]字段不存在" };
                                    throw new Exception(string.Concat(fieldNameCn));
                                }
                                this.FeScriptBlock.AppendFormat("jQuery('#input0{0}').live('change',function(){{ jQuery('#input0{2}').val(_computeExp('{1}')).change();}});", fieldInfo1.FieldOdr, str2, fieldInfo.FieldOdr);
                            }
                        }
                    }
                    string ubbCode = this.ReplaceParaValue(this.ReplaceValue, stringBuilder.ToString());
                    ubbCode = this.GetUbbCode(ubbCode, "[CRYPT]", "[/CRYPT]", this._page.UserName);
                    ubbCode = string.Concat(ubbCode, "\r\n<script type='text/javascript'>\r\n", this.FeScriptBlock.ToString(), "\r\n</script>");
                    str = ubbCode;
                }
                else
                {
                    sbModel.Append("{};");
                    str = "默认样式为空，请调整默认样式";
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                this.fileLogger.Error<Exception>(exception);
                throw exception;
            }
            return str;
        }




        /// <summary>
        /// 获取编辑界面
        /// </summary>
        /// <param name="tblName">业务名称</param>
        /// <param name="strWhere">记录查询条件</param>
        /// <param name="sbmodel">业务字段模型</param>
        /// <param name="xmlDoc">业务数据模型<

        public string GetTblHtml(string tblName, string strWhere, StringBuilder sbModel, XmlDocument xmlDoc)
        {
            string str = "";

            List<EIS.DataModel.Model.FieldInfo> tablePhyFields;
            Guid guid;
            this.FeScriptBlock.Length = 0;
            string formHtml = "";
            sbModel.Append("var _sysModel=");
            StringBuilder stringBuilder = new StringBuilder();
            XmlElement xmlElement = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(xmlElement);
            _TableInfo __TableInfo = new _TableInfo(tblName);
            _FieldInfo __FieldInfo = new _FieldInfo();
            string str1 = "";
            TableInfo tableInfo = new TableInfo();
            try
            {

                tableInfo = __TableInfo.GetModel();
                if (tableInfo == null)
                {
                    throw new Exception(string.Format("找不到业务定义：tblName=[{0}]", tblName));
                }
                if (!(tableInfo.FormHtml.Trim() == ""))
                {
                    formHtml = tableInfo.FormHtml;
                    if (!(this.Sindex != ""))
                    {
                        tablePhyFields = __FieldInfo.GetTablePhyFields(tableInfo.TableName);
                    }
                    else
                    {
                        tablePhyFields = __FieldInfo.GetFieldsStyleMerged(tableInfo.TableName, int.Parse(this.Sindex));
                        string tableStyle = __TableInfo.GetTableStyle(this.Sindex, 0);
                        if (tableStyle != "")
                        {
                            formHtml = tableStyle;
                        }
                    }
                    XmlElement xmlElement1 = xmlDoc.CreateElement("Table");
                    xmlElement1.SetAttribute("TableName", tblName);
                    xmlElement.AppendChild(xmlElement1);

                    DataTable dataTable = null;

                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        tableInfo.EditMode = "1";
                        //条件查询
                        str1 = string.Format("select * from {0} where {1}", tblName, strWhere);
                    }
                    else
                    {
                        tableInfo.EditMode = "0";
                        //第一次
                        str1 = string.Format(string.Concat("select * from ", tblName, " where 1=2"), new object[0]);
                    }
                    dataTable = SysDatabase.ExecuteTable(str1);


                    //查询条件
                    if (!string.IsNullOrEmpty(strWhere) && dataTable != null && dataTable.Rows.Count > 0)
                    {
                        tableInfo._AutoID = dataTable.Rows[0]["_AutoID"].ToString();
                        this._mainRow = dataTable.Rows[0];
                        this.MainId = dataTable.Rows[0]["_AutoID"].ToString();
                    }
                    else
                    {
                        guid = Guid.NewGuid();
                        this.MainId = guid.ToString();
                        this._mainRow = dataTable.NewRow();
                        this._mainRow["_AutoID"] = guid.ToString();
                    }


                    this.GenDefaultValue(tblName, this._mainRow, tablePhyFields);
                    this.DealDataControlPara(tblName, this._mainRow, tablePhyFields);

                    //从主表获取 HTML,替换
                    formHtml = this.toEditHtml(tableInfo, formHtml, this._mainRow, "input0", tablePhyFields);
                    //获取主表XML
                    this.GetDataRowXml(xmlElement1, this._mainRow, "", tablePhyFields);
                    // SysModel
                    sbModel.Append("[");
                    sbModel.AppendFormat("{{'tablename':'{0}','fields':[{1}],", tblName, this.GetFieldModel(tablePhyFields));

                    #region 公式表达式主表
                    foreach (EIS.DataModel.Model.FieldInfo fieldInfo in tablePhyFields.FindAll((EIS.DataModel.Model.FieldInfo f) => f.FieldInDispStyle == "004"))
                    {
                        string[] strArrays = fieldInfo.FieldInDispStyleTxt.Split(new char[] { '|' });
                        if (strArrays[0] == "1") //1.主表字段表达式：{货款}+{运费}
                        {
                            Regex regex = new Regex("{(\\w+)}", RegexOptions.IgnoreCase);
                            string str2 = strArrays[2];
                            foreach (Match match in regex.Matches(str2))
                            {
                                string value = match.Groups[1].Value;
                                EIS.DataModel.Model.FieldInfo fieldInfo1 = tablePhyFields.Find((EIS.DataModel.Model.FieldInfo fo) => fo.FieldName.ToLower() == value.ToLower());
                                if (fieldInfo1 == null)
                                {
                                    string[] fieldNameCn = new string[] { "主表字段[", fieldInfo.FieldNameCn, "]的计算公式设置错误，公式", str2, " 中的[", value, "]字段不存在" };
                                    throw new Exception(string.Concat(fieldNameCn));
                                }
                                this.FeScriptBlock.AppendFormat("\t jQuery('#input0{0}').live('change',function(){{ jQuery('#input0{2}').val(_computeExp('{1}')).change();}}); \n", fieldInfo1.FieldOdr, str2, fieldInfo.FieldOdr);
                            }
                        }
                        if (strArrays[0] == "2")  //2.主表合计子表字段(表达式)：首选选中对应的子表，然后只需填写子表表达式，如{数量}*{单价}
                        {
                            Regex regex = new Regex("{(\\w+)}", RegexOptions.IgnoreCase);
                            string str2 = strArrays[2];

                            List<EIS.DataModel.Model.FieldInfo> tablePhyFields_Exp = __FieldInfo.GetTablePhyFields(strArrays[1].ToString());

                            //变量标题
                            string varTemp = "_exp_" + AppBase.Utility.GuidTo16String();
                            this.FeScriptBlock.AppendFormat("\t var {0}=\"{1}\"; \n", varTemp, str2);
                            foreach (Match match in regex.Matches(str2))
                            {
                                string value = match.Groups[1].Value;
                                EIS.DataModel.Model.FieldInfo fieldInfo1 = tablePhyFields_Exp.Find((EIS.DataModel.Model.FieldInfo fo) => fo.FieldName.ToLower() == value.ToLower());
                                if (fieldInfo1 == null)
                                {
                                    string[] fieldNameCn = new string[] { "主表字段[", fieldInfo.FieldNameCn, "]的计算公式设置错误，公式", str2, " 中的[", value, "]字段不存在" };
                                    throw new Exception(string.Concat(fieldNameCn));
                                }

                                this.FeScriptBlock.AppendFormat("\t jQuery('.{0}_{1}').live('change',function(){{ jQuery('#input0{2}').val(_sumSubField('{3}',{4})).change();}}); \n", strArrays[1], fieldInfo1.FieldName, fieldInfo.FieldOdr, strArrays[1], varTemp);
                            }
                            string strRowName = strArrays[1] + "_RowChange";
                            this.FeScriptBlock.Append("\t if(typeof(" + strRowName + ")=='undefined'){ \n");
                            this.FeScriptBlock.Append("\t " + strRowName + "=jQuery({}); \n");
                            this.FeScriptBlock.Append("\t}  \n");
                            this.FeScriptBlock.Append("\t " + strRowName + ".bind('rowchange',function(){ \n");
                            this.FeScriptBlock.AppendFormat("\t jQuery('#input0{0}').val(_sumSubField('{1}',{2})).change();  \n", fieldInfo.FieldOdr, strArrays[1], varTemp);
                            this.FeScriptBlock.Append("\t });  \n");


                        }

                    }
                    #endregion
                    sbModel.Append("\n");
                    sbModel.Append("'subtbls':[");
                    sbModel.Append("\n");

                    //获取子表数量
                    List<TableInfo> subTable = __TableInfo.GetSubTable();
                    string outerHtml = null; //存原始表格
                    string outerHtmltbody = null;  //存原始表格tbody
                    string outerHtmlthead = null;
                    int num = 0;
                    foreach (TableInfo tableInfo1 in subTable)
                    {
                        stringBuilder = new StringBuilder();
                        string tableName = tableInfo1.TableName;
                        HtmlDocument htmlDocument = new HtmlDocument();
                        htmlDocument.LoadHtml(formHtml);
                        HtmlNode elementbyId = htmlDocument.GetElementbyId(tableName);
                        if (elementbyId != null)
                        {
                            #region 取子表字段集合
                            if (!(this.Sindex != ""))
                            {
                                tablePhyFields = __FieldInfo.GetTablePhyFields(tableInfo1.TableName);
                            }
                            else
                            {
                                tablePhyFields = __FieldInfo.GetFieldsStyleMerged(tableInfo1.TableName, int.Parse(this.Sindex));
                            }

                            #endregion
                            if (tablePhyFields.Count > 0)
                            {
                                #region 公式表达式主表
                                foreach (EIS.DataModel.Model.FieldInfo fieldInfosub in tablePhyFields.FindAll((EIS.DataModel.Model.FieldInfo f) => f.FieldInDispStyle == "004"))
                                {
                                    string[] strArraysSub = fieldInfosub.FieldInDispStyleTxt.Split(new char[] { '|' });
                                    if (strArraysSub[0] == "3")  //3.子表字段表达式：{数量}*{单价}
                                    {
                                        Regex regex = new Regex("{(\\w+)}", RegexOptions.IgnoreCase);
                                        string str2 = strArraysSub[2];
                                        foreach (Match match in regex.Matches(str2))
                                        {
                                            string subvalue = match.Groups[1].Value;
                                            EIS.DataModel.Model.FieldInfo fieldInfosubValue = tablePhyFields.Find((EIS.DataModel.Model.FieldInfo fo) => fo.FieldName.ToLower() == subvalue.ToLower());
                                            if (fieldInfosubValue == null)
                                            {
                                                string[] fieldNameCn = new string[] { "主表字段[", fieldInfosub.FieldNameCn, "]的计算公式设置错误，公式", str2, " 中的[", subvalue, "]字段不存在" };
                                                throw new Exception(string.Concat(fieldNameCn));
                                            }

                                            string strID = "." + tableInfo1.TableName + "_" + subvalue;
                                            string subTblId = "#SubTbl0" + num.ToString();
                                            //this.FeScriptBlock.Append("\t jQuery('" + strID + "').live('change',function(){ \n");
                                            this.FeScriptBlock.Append("\t jQuery('" + subTblId + "').on('change','" + strID + "',function(){ \n");
                                            this.FeScriptBlock.Append("\t\t var row=this.id.split('_')[1]; \n");
                                            this.FeScriptBlock.Append("\t\t var p=this.id.split('_')[0]; \n");
                                            this.FeScriptBlock.AppendFormat("\t\t var v=_computeSubExp('{0}','{2}',row,\"{1}\"); \n", tableInfo1.TableName, str2, fieldInfosub.FieldName);
                                            this.FeScriptBlock.Append("\t }); \n");
                                            //jQuery('.test_Sub_cprice').live('change',function(){
                                            //    var row=this.id.split('_')[1];
                                            //    var p=this.id.split('_')[0];
                                            //    var v=_computeSubExp('test_Sub','sumoney',row,"{cprice}*{cnum}");
                                            //});
                                        }
                                    }
                                }
                                #endregion




                                #region 标题
                                string strFalg = "[#" + tableInfo1.TableName + "#]";
                                htmlDocument.DocumentNode.InnerHtml = htmlDocument.DocumentNode.OuterHtml.Replace(elementbyId.OuterHtml, strFalg);
                                //原始表格HTML
                                outerHtml = elementbyId.OuterHtml;
                                // 原始表格 thead
                                HtmlNode htmlNodeThead = elementbyId.SelectSingleNode("thead");
                                outerHtmlthead = (htmlNodeThead == null ? elementbyId.FirstChild.OuterHtml : htmlNodeThead.OuterHtml);  //标题
                                // 原始表格body
                                HtmlNode htmlNode = elementbyId.SelectSingleNode("tbody");
                                outerHtmltbody = htmlNode.OuterHtml;

                                //取Body下的 tr 用于替换
                                HtmlNode htmlBackNodeTr = htmlNode.SelectSingleNode("tr");

                                //取tbody 赋值，替换 添加行 隐藏列
                                HtmlNode htmlNodeTbody = elementbyId.SelectSingleNode("tbody");
                                htmlNodeTbody.SetAttributeValue("style", "display:none");
                                HtmlNode htmlNodeTr = htmlNodeTbody.SelectSingleNode("tr");
                                htmlNodeTr.SetAttributeValue("class", "dataRow");
                                string trid = "SubTbl0" + num.ToString() + "_srkjdslABHSAS_";
                                htmlNodeTr.SetAttributeValue("id", trid);

                                #endregion

                                #region 子表有字段,取子表集合
                                if (!string.IsNullOrEmpty(strWhere))
                                {   //添加查询，编辑
                                    str1 = string.Format("select * from {0} where _MainID='{1}' order by _CreateTime ", tableInfo1.TableName, tableInfo._AutoID);
                                    tableInfo1.EditMode = "1";
                                }
                                else
                                {
                                    tableInfo1.EditMode = "0"; //新建
                                    str1 = string.Format(string.Concat("select * from ", tableInfo1.TableName, " where 1=2"), new object[0]);
                                }
                                //获取数据源
                                dataTable = SysDatabase.ExecuteTable(str1);
                                this._mainRow = dataTable.NewRow();

                                this.GenDefaultValue(tblName, this._mainRow, tablePhyFields);
                                this.DealDataControlPara(tblName, this._mainRow, tablePhyFields);
                                #endregion
                                int intRowcount = 0;
                                //子表HTML
                                StringBuilder sb = new StringBuilder();
                                #region 将替换表格，格式化
                                sb.Append(this.toEditSubHtml(tableInfo1, htmlNodeTbody.OuterHtml, this._mainRow, trid, tablePhyFields));
                                #endregion
                                string subTrId = "SubTbl0" + num.ToString();
                                sb.Append("<tbody id='" + subTrId + "'>");
                                #region 模型字段
                                XmlElement xmTemp = xmlDoc.CreateElement("Table");
                                xmTemp.SetAttribute("TableName", tableInfo1.TableName);
                                xmlElement.AppendChild(xmTemp);
                                #endregion

                                #region 子表替换数据
                                if (dataTable != null && dataTable.Rows.Count > 0)
                                {
                                    sbModel.AppendFormat(" \n {{'tablename':'{0}','maxorder':{2},'mode':'{3}','fields':[{1}]}} ,", tableInfo1.TableName, this.GetFieldModel(tablePhyFields), dataTable.Rows.Count.ToString(), tableInfo1.EditMode.ToString());

                                    #region 编辑
                                    int k = 0;
                                    foreach (DataRow drRow in dataTable.Rows)
                                    {
                                        this._mainRow = drRow;
                                        HtmlNode NewEditTr = htmlBackNodeTr;
                                        NewEditTr.SetAttributeValue("class", "dataRow");
                                        string edittrid = "SubTbl0" + num.ToString() + "_" + k.ToString() + "_";
                                        NewEditTr.SetAttributeValue("id", edittrid);
                                        sb.Append(this.toEditSubHtml(tableInfo1, NewEditTr.OuterHtml, this._mainRow, edittrid, tablePhyFields));
                                        k++;
                                        this.GetDataRowXml(xmTemp, this._mainRow, edittrid, tablePhyFields);
                                    }
                                    #endregion
                                }
                                else
                                {
                                    //sbModel.AppendFormat(" \n {{'tablename':'{0}','maxorder':{2},'mode':'{3}','fields':[{1}]}} ,", tableInfo1.TableName, this.GetFieldModel(tablePhyFields), "0".ToString(), tableInfo1.EditMode.ToString());
                                    sbModel.AppendFormat(" \n {{'tablename':'{0}','maxorder':{2},'mode':'{3}','fields':[{1}]}} ,", tableInfo1.TableName, this.GetFieldModel(tablePhyFields), tableInfo1.InitRows.ToString(), tableInfo1.EditMode.ToString());
                                    // 新增
                                    #region 新增

                                    for (int j = 0; j < tableInfo1.InitRows; j++)
                                    {
                                        this._mainRow = dataTable.NewRow();
                                        this._mainRow["_AutoID"] = Guid.NewGuid().ToString();
                                        HtmlNode NewTr = htmlBackNodeTr;
                                        NewTr.SetAttributeValue("class", "dataRow");
                                        string Newtrid = "SubTbl0" + num.ToString() + "_" + j.ToString() + "_";
                                        NewTr.SetAttributeValue("id", Newtrid);
                                        this.GenDefaultValue(tblName, this._mainRow, tablePhyFields);
                                        this.DealDataControlPara(tblName, this._mainRow, tablePhyFields);
                                        sb.Append(this.toEditSubHtml(tableInfo1, NewTr.OuterHtml, this._mainRow, Newtrid, tablePhyFields));
                                        this.GetDataRowXml(xmTemp, this._mainRow, Newtrid, tablePhyFields);

                                    }
                                    #endregion
                                }
                                #endregion



                                sb.Append("</tbody>");
                                string strAdd = "<a class='subaddbtn' title='添加行' href='javascript:' onclick=\"_fnSubAdd('" + tableInfo1.TableName + "')\">&nbsp;</a>";
                                string stringTemp = outerHtml.Replace(outerHtmltbody, sb.ToString().Replace("[#DELBTN#]", strAdd).Replace("[#ADD#]", strAdd).Replace("[#ADDBTN#]", strAdd)).Replace("[#DELBTN#]", strAdd).Replace("[#ADD#]", strAdd).Replace("[#ADDBTN#]", strAdd);

                                formHtml = htmlDocument.DocumentNode.OuterHtml.Replace(strFalg, stringTemp);


                            }
                            else
                            {
                                // 子表位创建，没有字段
                                sbModel.AppendFormat(" \n {{'tablename':'{0}','maxorder':0,'fields':[]}},", tableInfo1.TableName);
                            }
                        }
                        else
                        {
                            sbModel.AppendFormat(" \n {{'tablename':'{0}','maxorder':{2},'mode':'{3}','fields':[{1}]}},", tableInfo1.TableName, this.GetFieldModel(tablePhyFields), tableInfo1.InitRows.ToString(), tableInfo1.EditMode.ToString());
                        }
                        num++;
                    }
                    sbModel.Remove(sbModel.Length - 1, 1);

                    stringBuilder.Append(formHtml);

                    sbModel.Append(" \n ]}]");
                    string ubbCode = this.ReplaceParaValue(this.ReplaceValue, stringBuilder.ToString());
                    ubbCode = this.GetUbbCode(ubbCode, "[CRYPT]", "[/CRYPT]", this._page.UserName);
                    ubbCode = string.Concat(ubbCode, "\r\n<script type='text/javascript'>\r\n", this.FeScriptBlock.ToString(), "\r\n</script>");
                    str = ubbCode;

                }
                else
                {
                    sbModel.Append("{};");
                    str = "默认样式为空，请调整默认样式";
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                this.fileLogger.Error<Exception>(exception);
                throw exception;
            }
            return str;

        }
        /// <summary>
        /// 子表替换
        /// </summary>
        /// <param name="tblModel"></param>
        /// <param name="tmpl"></param>
        /// <param name="data"></param>
        /// <param name="prefix"></param>
        /// <param name="dtfields"></param>
        /// <returns></returns>
        private string toEditSubHtml(TableInfo tblModel, string tmpl, DataRow data, string prefix, List<EIS.DataModel.Model.FieldInfo> dtfields)
        {
            string[] strArrays;
            string str;
            try
            {
                string tableName = tblModel.TableName;
                foreach (EIS.DataModel.Model.FieldInfo dtfield in dtfields)
                {
                    string fieldInDispStyle = dtfield.FieldInDispStyle;
                    string fieldName = dtfield.FieldName;
                    string maxOrder = (data == null ? "" : this.FormatData(data[fieldName], dtfield));
                    int fieldOdr = dtfield.FieldOdr;
                    string str1 = string.Concat(prefix, fieldOdr.ToString());
                    this.fileLogger.Trace<string, string, string>("解析字段：tblName=[{0}]，fldname=[{1}]，fldvalue=[{2}]", tableName, fieldName, maxOrder);
                    try
                    {
                        EIS.AppModel.DataControl dataControl = this.DataControl.Find((EIS.AppModel.DataControl f) => (f.BizName.ToLower() != tableName.ToLower() ? false : f.FieldName.ToLower() == fieldName.ToLower()));
                        if (dataControl == null)
                        {
                            if (!this.SheetEditLimit)
                            {
                                dtfield.FieldRead = 1;
                            }
                        }
                        else if (dataControl.CanRead.HasValue)
                        {
                            dtfield.FieldRead = (dataControl.CanRead.Value ? 1 : 0);
                        }
                        else if (!this.SheetEditLimit)
                        {
                            dtfield.FieldRead = 1;
                        }
                        maxOrder = Utility.String2Html(maxOrder);
                        string autoStr = "";
                        if (fieldInDispStyle == "000")
                        {
                            autoStr = this.GetAutoStr(dtfield, str1, maxOrder);
                        }
                        else if (fieldInDispStyle.Length < 3)
                        {
                            if (!(fieldInDispStyle == "04"))
                            {
                                switch (dtfield.FieldType)
                                {
                                    case 4:
                                        {
                                            autoStr = this.GetDefaultDateStr(dtfield, str1, maxOrder);
                                            break;
                                        }
                                    case 5:
                                        {
                                            autoStr = this.GetDefaultMultiTxtStr(dtfield, str1, maxOrder);
                                            break;
                                        }
                                    default:
                                        {
                                            autoStr = this.GetDefaultTxtStr(dtfield, str1, maxOrder);
                                            break;
                                        }
                                }
                            }
                        }
                        else if (fieldInDispStyle.Substring(0, 3) == "001")
                        {
                            autoStr = this.GetDateStr(dtfield, str1, maxOrder);
                        }
                        else if (fieldInDispStyle == "002")
                        {
                            autoStr = this.GetPwdStr(dtfield, str1, maxOrder);
                        }
                        else if (fieldInDispStyle == "003")
                        {
                            if (string.IsNullOrEmpty(maxOrder))
                            {
                                    int num = 0;
                                    char[] chrArray = new char[] { '\u005F' };
                                    if (int.TryParse(prefix.Split(chrArray)[1], out num))
                                    {
                                        maxOrder = (num + 1).ToString();
                                    }
                                
                            }
                            dtfield.FieldRead = 1;
                            autoStr = this.GetDefaultTxtStr(dtfield, str1, maxOrder);
                        }
                        else if (fieldInDispStyle == "004")
                        {
                            autoStr = this.GetDefaultTxtStr(dtfield, str1, maxOrder);
                        }
                        else if (fieldInDispStyle == "005")
                        {
                            autoStr = this.GetDefaultTxtStr(dtfield, str1, maxOrder);
                            if (dtfield.FieldInDispStyleTxt.Trim().Length > 0)
                            {
                                EIS.DataModel.Model.FieldInfo fieldInfo = dtfields.Find((EIS.DataModel.Model.FieldInfo f) => f.FieldName.ToLower() == dtfield.FieldInDispStyleTxt.ToLower());
                                if (fieldInfo != null)
                                {
                                    this.FeScriptBlock.AppendFormat("jQuery('#input0{1}').change(function(){{jQuery('#{0}').val(_a2c(this.value));}});", str1, fieldInfo.FieldOdr);
                                }
                            }
                        }
                        else if (fieldInDispStyle.Substring(0, 2) == "01")
                        {
                            autoStr = this.GetDropStr(dtfield, str1, maxOrder, data);
                        }
                        else if (fieldInDispStyle.Substring(0, 2) == "03")
                        {
                            autoStr = this.GetOutPageStr(dtfield, str1, maxOrder, prefix);
                        }
                        else if (fieldInDispStyle.Substring(0, 2) == "04")
                        {
                            autoStr = this.GetRadioStr(dtfield, str1, maxOrder);
                        }
                        else if (fieldInDispStyle.Substring(0, 2) == "05")
                        {
                            autoStr = this.GetCheckBoxStr(dtfield, str1, maxOrder);
                        }
                        else if (fieldInDispStyle.Substring(0, 2) == "02")
                        {
                            autoStr = this.GetMultiTxtStr(dtfield, str1, maxOrder);
                        }
                        string fieldEventScript = this.GetFieldEventScript(dtfield, str1);
                        //子表字段不添加预期值的提示信息
                        string placeholderStr = string.Concat(" placeholder='请输入", dtfield.FieldNameCn, "'");
                        autoStr = autoStr.Replace(placeholderStr, "");
                        autoStr = autoStr.Replace("{FieldEventDef}", fieldEventScript);

                        if ((dtfield.FieldNull != 1 || !(fieldInDispStyle != "021") || dtfield.FieldInDisp != 1 ? false : prefix.StartsWith("input0")))
                        {
                            string str2 = "100%";
                            if (dtfield.FieldWidth.Trim() != "")
                            {
                                if (!dtfield.FieldWidth.EndsWith("%"))
                                {
                                    fieldOdr = int.Parse(dtfield.FieldWidth.Trim()) + 16;
                                    str2 = string.Concat(fieldOdr.ToString(), "px");
                                }
                            }
                            strArrays = new string[] { "<table border=0 width='", str2, "' class='RequiredTbl'><tr><td>", autoStr, "</td><td class='RequiredStar'>*</td></tr></table>" };
                            autoStr = string.Concat(strArrays);
                        }
                        if (!prefix.StartsWith("input"))
                        {
                            tmpl = tmpl.Replace(string.Concat("{", tableName, ".", fieldName, "}"), autoStr);
                            tmpl = tmpl.Replace(string.Concat("{", fieldName, "}"), autoStr);
                            if ((tblModel.TableType != 2 ? true : !(tblModel.EditMode == "1")))
                            {
                                autoStr = string.Format("<p class='valp' id='{1}_p'>{0}</p>", maxOrder, str1);
                                tmpl = tmpl.Replace(string.Concat("[", tableName, ".", fieldName, "]"), autoStr);
                                tmpl = tmpl.Replace(string.Concat("[", fieldName, "]"), autoStr);
                            }
                            else
                            {
                                autoStr = string.Format("<p class='valp' id='{1}_p'>{0}</p><p style='display:none;'>{2}</p>", maxOrder, str1, autoStr);
                                tmpl = tmpl.Replace(string.Concat("[", tableName, ".", fieldName, "]"), autoStr);
                                tmpl = tmpl.Replace(string.Concat("[", fieldName, "]"), autoStr);

                            }
                        


                            tmpl = tmpl.Replace("[#BODYID#]", prefix);
                            strArrays = new string[] { "<a class='subdelbtn' title='删除行' href='javascript:' onclick=\"_fnSubDelConfirm('", tableName, "','", prefix, "')\">&nbsp;</a>" };
                            string str3 = string.Concat(strArrays);
                            tmpl = tmpl.Replace("[#DELBTN#]", str3).Replace("[#DEL#]", str3);
                            strArrays = new string[] { "<a class='subeditbtn' title='编辑行' href='javascript:' onclick=\"_fnSubEdit('", tableName, "','", prefix, "')\">&nbsp;</a>" };
                            tmpl = tmpl.Replace("[#EDIT#]", string.Concat(strArrays));
                            strArrays = new string[] { "<a class='subcopybtn' title='复制行' href='javascript:' onclick=\"_fnSubCopy('", tableName, "','", prefix, "')\">&nbsp;</a>" };
                            tmpl = tmpl.Replace("[#COPY#]", string.Concat(strArrays));
                        }
                    
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        Logger logger = this.fileLogger;
                        object[] fieldInDispStyleName = new object[] { tableName, fieldName, fieldInDispStyle, dtfield.FieldInDispStyleName, dtfield.FieldInDispStyleTxt };
                        logger.Error<Exception>(string.Format("在解析字段[{0}.{1}]时发生错误：fInStyle=[{2}：{3}：{4}]", fieldInDispStyleName), exception);
                        fieldInDispStyleName = new object[] { tableName, fieldName, fieldInDispStyle, dtfield.FieldInDispStyleName, dtfield.FieldInDispStyleTxt };
                        throw new Exception(string.Format("在解析字段[{0}.{1}]时发生错误：fInStyle=[{2}：{3}：{4}]", fieldInDispStyleName), exception);
                    }
                }
                str = tmpl;
            }
            finally
            {
                //u003216296805.u003370177019(null, MethodBase.GetCurrentMethod(), false);
            }
            return str;
        }

		private string toEditHtml(TableInfo tblModel, string tmpl, DataRow data, string prefix, List<EIS.DataModel.Model.FieldInfo> dtfields)
		{
			string[] strArrays;
			string str;
			//u003216296805.u003370177019(null, MethodBase.GetCurrentMethod(), true);
			try
			{
				string tableName = tblModel.TableName;
				foreach (EIS.DataModel.Model.FieldInfo dtfield in dtfields)
				{
					string fieldInDispStyle = dtfield.FieldInDispStyle;
					string fieldName = dtfield.FieldName;
					string maxOrder = (data == null ? "" : this.FormatData(data[fieldName], dtfield));
					int fieldOdr = dtfield.FieldOdr;
					string str1 = string.Concat(prefix, fieldOdr.ToString());
					this.fileLogger.Trace<string, string, string>("解析字段：tblName=[{0}]，fldname=[{1}]，fldvalue=[{2}]", tableName, fieldName, maxOrder);
					try
					{
						EIS.AppModel.DataControl dataControl = this.DataControl.Find((EIS.AppModel.DataControl f) => (f.BizName.ToLower() != tableName.ToLower() ? false : f.FieldName.ToLower() == fieldName.ToLower()));
						if (dataControl == null)
						{
							if (!this.SheetEditLimit)
							{
								dtfield.FieldRead = 1;
							}
						}
						else if (dataControl.CanRead.HasValue)
						{
							dtfield.FieldRead = (dataControl.CanRead.Value ? 1 : 0);
						}
						else if (!this.SheetEditLimit)
						{
							dtfield.FieldRead = 1;
						}
						maxOrder = Utility.String2Html(maxOrder);
						string autoStr = "";
						if (fieldInDispStyle == "000")
						{
							autoStr = this.GetAutoStr(dtfield, str1, maxOrder);
						}
						else if (fieldInDispStyle.Length < 3)
						{
							if (!(fieldInDispStyle == "04"))
							{
								switch (dtfield.FieldType)
								{
									case 4:
									{
										autoStr = this.GetDefaultDateStr(dtfield, str1, maxOrder);
										break;
									}
									case 5:
									{
										autoStr = this.GetDefaultMultiTxtStr(dtfield, str1, maxOrder);
										break;
									}
									default:
									{
										autoStr = this.GetDefaultTxtStr(dtfield, str1, maxOrder);
										break;
									}
								}
							}
						}
						else if (fieldInDispStyle.Substring(0, 3) == "001")
						{
							autoStr = this.GetDateStr(dtfield, str1, maxOrder);
						}
						else if (fieldInDispStyle == "002")
						{
							autoStr = this.GetPwdStr(dtfield, str1, maxOrder);
						}
						else if (fieldInDispStyle == "003")
						{
							if (string.IsNullOrEmpty(maxOrder))
							{
								if (!prefix.StartsWith("SubTbl0"))
								{
									maxOrder = this.GetMaxOrder(tableName, dtfield.FieldName, this.MainId);
								}
								else
								{
									int num = 0;
									char[] chrArray = new char[] { '\u005F' };
									if (int.TryParse(prefix.Split(chrArray)[1], out num))
									{
										maxOrder = (num + 1).ToString();
									}
								}
							}
							dtfield.FieldRead = 1;
							autoStr = this.GetDefaultTxtStr(dtfield, str1, maxOrder);
						}
						else if (fieldInDispStyle == "004")
						{
							autoStr = this.GetDefaultTxtStr(dtfield, str1, maxOrder);
						}
						else if (fieldInDispStyle == "005")
						{
							autoStr = this.GetDefaultTxtStr(dtfield, str1, maxOrder);
							if (dtfield.FieldInDispStyleTxt.Trim().Length > 0)
							{
								EIS.DataModel.Model.FieldInfo fieldInfo = dtfields.Find((EIS.DataModel.Model.FieldInfo f) => f.FieldName.ToLower() == dtfield.FieldInDispStyleTxt.ToLower());
								if (fieldInfo != null)
								{
									this.FeScriptBlock.AppendFormat("jQuery('#input0{1}').change(function(){{jQuery('#{0}').val(_a2c(this.value));}});", str1, fieldInfo.FieldOdr);
								}
							}
						}
						else if (fieldInDispStyle.Substring(0, 2) == "01")
						{
							autoStr = this.GetDropStr(dtfield, str1, maxOrder, data);
						}
						else if (fieldInDispStyle.Substring(0, 2) == "03")
						{
							autoStr = this.GetOutPageStr(dtfield, str1, maxOrder, prefix);
						}
						else if (fieldInDispStyle.Substring(0, 2) == "04")
						{
							autoStr = this.GetRadioStr(dtfield, str1, maxOrder);
						}
						else if (fieldInDispStyle.Substring(0, 2) == "05")
						{
							autoStr = this.GetCheckBoxStr(dtfield, str1, maxOrder);
						}
						else if (fieldInDispStyle.Substring(0, 2) == "02")
						{
							autoStr = this.GetMultiTxtStr(dtfield, str1, maxOrder);
						}
						string fieldEventScript = this.GetFieldEventScript(dtfield, str1);
                        //字段不添加预期值的提示信息
                        if (!tblModel.IsShowFldPlaceholder)
                        {
                            string placeholderStr = string.Concat(" placeholder='请输入", dtfield.FieldNameCn, "'");
                            autoStr = autoStr.Replace(placeholderStr, "");
                        }
						autoStr = autoStr.Replace("{FieldEventDef}", fieldEventScript);
						if ((dtfield.FieldNull != 1 || !(fieldInDispStyle != "021") || dtfield.FieldInDisp != 1 ? false : prefix.StartsWith("input0")))
						{
							string str2 = "100%";
							if (dtfield.FieldWidth.Trim() != "")
							{
								if (!dtfield.FieldWidth.EndsWith("%"))
								{
									fieldOdr = int.Parse(dtfield.FieldWidth.Trim()) + 16;
									str2 = string.Concat(fieldOdr.ToString(), "px");
								}
							}
							strArrays = new string[] { "<table border=0 width='", str2, "' class='RequiredTbl'><tr><td>", autoStr, "</td><td class='RequiredStar'>*</td></tr></table>" };
							autoStr = string.Concat(strArrays);
						}
						if (!prefix.StartsWith("input"))
						{
							tmpl = tmpl.Replace(string.Concat("{", fieldName, "}"), autoStr);
							if ((tblModel.TableType != 2 ? true : !(tblModel.EditMode == "1")))
							{
								autoStr = string.Format("<p class='valp' id='{1}_p'>{0}</p>", maxOrder, str1);
								tmpl = tmpl.Replace(string.Concat("[", fieldName, "]"), autoStr);
							}
							else
							{
								autoStr = string.Format("<p class='valp' id='{1}_p'>{0}</p><p style='display:none;'>{2}</p>", maxOrder, str1, autoStr);
								tmpl = tmpl.Replace(string.Concat("[", fieldName, "]"), autoStr);
							}
							tmpl = tmpl.Replace("[#BODYID#]", prefix);
							strArrays = new string[] { "<a class='subdelbtn' title='删除行' href='javascript:' onclick=\"_fnSubDelConfirm('", tableName, "','", prefix, "')\">&nbsp;</a>" };
							string str3 = string.Concat(strArrays);
							tmpl = tmpl.Replace("[#DELBTN#]", str3).Replace("[#DEL#]", str3);
							strArrays = new string[] { "<a class='subeditbtn' title='编辑行' href='javascript:' onclick=\"_fnSubEdit('", tableName, "','", prefix, "')\">&nbsp;</a>" };
							tmpl = tmpl.Replace("[#EDIT#]", string.Concat(strArrays));
							strArrays = new string[] { "<a class='subcopybtn' title='复制行' href='javascript:' onclick=\"_fnSubCopy('", tableName, "','", prefix, "')\">&nbsp;</a>" };
							tmpl = tmpl.Replace("[#COPY#]", string.Concat(strArrays));
						}
						else
						{
							strArrays = new string[] { "{", tableName, ".", fieldName, "}" };
							tmpl = tmpl.Replace(string.Concat(strArrays), autoStr);
							strArrays = new string[] { "[", tableName, ".", fieldName, "]" };
							tmpl = tmpl.Replace(string.Concat(strArrays), maxOrder);
                            //tmpl = tmpl.Replace(string.Concat("{", fieldName, "}"), autoStr);  
                            //tmpl = tmpl.Replace(string.Concat("[", fieldName, "]"), maxOrder);
						}
                      
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						Logger logger = this.fileLogger;
						object[] fieldInDispStyleName = new object[] { tableName, fieldName, fieldInDispStyle, dtfield.FieldInDispStyleName, dtfield.FieldInDispStyleTxt };
						logger.Error<Exception>(string.Format("在解析字段[{0}.{1}]时发生错误：fInStyle=[{2}：{3}：{4}]", fieldInDispStyleName), exception);
						fieldInDispStyleName = new object[] { tableName, fieldName, fieldInDispStyle, dtfield.FieldInDispStyleName, dtfield.FieldInDispStyleTxt };
						throw new Exception(string.Format("在解析字段[{0}.{1}]时发生错误：fInStyle=[{2}：{3}：{4}]", fieldInDispStyleName), exception);
					}
				}
              
                
				str = tmpl;
			}
			finally
			{
				//u003216296805.u003370177019(null, MethodBase.GetCurrentMethod(), false);
			}
			return str;
		}

		private string toPrintHtml(string tblName, List<EIS.DataModel.Model.FieldInfo> dtFields, string tmpl, DataRow data)
		{
			string str;
			string str1 = "input0";
			string[] strArrays = new string[] { "\\{", tblName, ".([a-zA-Z0-9_]+)\\}|\\{([a-zA-Z0-9_]+)\\}|\\[([a-zA-Z0-9_]+)\\]|\\[", tblName, ".([a-zA-Z0-9_]+)\\]" };
			Regex regex = new Regex(string.Concat(strArrays), RegexOptions.IgnoreCase);
			foreach (Match match in regex.Matches(tmpl))
			{
				string str2 = match.Value.Trim("{}[]".ToCharArray());
				if (str2.IndexOf(".") > 0)
				{
					char[] chrArray = new char[] { '.' };
					str = str2.Split(chrArray)[1];
				}
				else
				{
					str = str2;
				}
				string str3 = str;
				if (data.Table.Columns.Contains(str3))
				{
					EIS.DataModel.Model.FieldInfo fieldInfo = dtFields.Find((EIS.DataModel.Model.FieldInfo f) => f.FieldName == str3);
					if (fieldInfo == null)
					{
						tmpl = tmpl.Replace(match.Value, data[str3].ToString());
					}
					else
					{
						string fieldInDispStyle = fieldInfo.FieldInDispStyle;
						string fieldName = fieldInfo.FieldName;
						string str4 = (data == null ? "" : this.FormatData(data[fieldName], fieldInfo));
                        
						int fieldOdr = fieldInfo.FieldOdr;
						string str5 = string.Concat(str1, fieldOdr.ToString());
						fieldInfo.FieldRead = 2;

                        //将隐藏字段替换成空
                        if (!Convert.ToBoolean(fieldInfo.FieldInDisp))
                        {
                           str4="";
                        }

						tmpl = tmpl.Replace(string.Concat("[", str2, "]"), str4);
						string autoStr = "";
						if (fieldInDispStyle == "000")
						{
							autoStr = this.GetAutoStr(fieldInfo, str5, str4);
						}
						else if (fieldInDispStyle.Length < 3)
						{
							if (!(fieldInDispStyle == "04"))
							{
								autoStr = str4;
							}
						}
						else if (fieldInDispStyle.Substring(0, 3) == "001")
						{
							autoStr = this.GetDateStr(fieldInfo, str5, str4);
						}
						else if (fieldInDispStyle == "002")
						{
							autoStr = "******";
						}
						else if (fieldInDispStyle.Substring(0, 2) == "01")
						{
							autoStr = this.GetDropStr(fieldInfo, str5, str4, data);
						}
						else if (fieldInDispStyle.Substring(0, 2) == "03")
						{
							autoStr = str4;
						}
						else if (fieldInDispStyle.Substring(0, 2) == "04")
						{
							autoStr = this.GetRadioStr(fieldInfo, str5, str4);
						}
						else if (fieldInDispStyle.Substring(0, 2) == "05")
						{
							autoStr = this.GetCheckBoxStr(fieldInfo, str5, str4);
						}
						else if (!(fieldInDispStyle.Substring(0, 2) == "02"))
						{
							autoStr = str4;
						}
						else
						{
							autoStr = (!(fieldInDispStyle == "023") ? this.GetMultiTxtStr(fieldInfo, str5, str4) : this.GetFileList(fieldInfo.TableName, str4));
						}
						if (!str1.StartsWith("input"))
						{
							tmpl = tmpl.Replace(string.Concat("{", fieldName, "}"), autoStr);
							tmpl = tmpl.Replace(string.Concat("[", fieldName, "]"), autoStr);
                     
							tmpl = tmpl.Replace("[#BODYID#]", str1);
                            tmpl = tmpl.Replace("<td style=\"text-align: center;\">[#ADDBTN#]</td>", "");
                            tmpl = tmpl.Replace("<td style=\"text-align: center;\">[#DELBTN#]</td>", "");
                            tmpl = tmpl.Replace("<td style=\"text-align: center;\">[#DEL#]</td>", "");
                             tmpl = tmpl.Replace("<td align=\"center\" style=\"text-align: center;\">[#DELBTN#]</td>", "");
                          
                            tmpl = tmpl.Replace("<td class=\"ADDBTN\">[#ADDBTN#]</td>", "");
                            tmpl = tmpl.Replace("<td class=\"DELBTN\">[#DELBTN#]</td>", "");
							tmpl = tmpl.Replace("[#DELBTN#]", "").Replace("[#DEL#]", "");
						}
						else
						{
							tmpl = tmpl.Replace(match.Value, autoStr);
						}
					}
				}
			}
			return tmpl;
		}

        /// <summary>
        /// 打印流程，带签名
        /// </summary>
        /// <param name="tblName"></param>
        /// <param name="strwhere"></param>
        /// <returns></returns>
        public string PrintWorkflow(string tblName, string strwhere)
        {
            string tableName;
            HtmlDocument htmlDocument;
            HtmlNode elementbyId;
            int i;
            HtmlNode htmlNode;
            HtmlNode htmlNode1;
            string outerHtml;
            HtmlNode htmlNode2;
            string str;
            string str1;
            string printHtml = "";
            _TableInfo __TableInfo = new _TableInfo(tblName);
            _FieldInfo __FieldInfo = new _FieldInfo();
            string str2 = "";
            TableInfo tableInfo = new TableInfo();
            try
            {
                tableInfo = __TableInfo.GetModel();
                if (tableInfo == null)
                {
                    throw new Exception(string.Concat("找不到业务定义[", tblName, "]"));
                }
                tblName = tableInfo.TableName;
                List<EIS.DataModel.Model.FieldInfo> tablePhyFields = __FieldInfo.GetTablePhyFields(tableInfo.TableName);
                DataTable dataTable = new DataTable();
                str2 = (!(tableInfo.DetailSQL.Trim() == "") ? tableInfo.DetailSQL.Replace("|^condition^|", strwhere) : string.Concat("select * from ", tblName, " where ", strwhere));
                dataTable = this.ExecuteTable(str2, tableInfo.ConnectionId);
                if (dataTable.Rows.Count <= 0)
                {
                    throw new Exception("找不到对应的数据记录！");
                }
                this.MainId = dataTable.Rows[0]["_AutoID"].ToString();
                Instance instanceByAppInfo = InstanceService.GetInstanceByAppInfo(tblName, this.MainId);
                if (instanceByAppInfo == null)
                {
                    throw new Exception("找不到对应的流程审批信息！");
                }
                Define workflowDefineModelById = DefineService.GetWorkflowDefineModelById(instanceByAppInfo.WorkflowId);
                if (!(workflowDefineModelById.PrintStyle != ""))
                {
                    printHtml = tableInfo.PrintHtml;
                }
                else
                {
                    string tableStyle = __TableInfo.GetTableStyle(workflowDefineModelById.PrintStyle, 2);
                    printHtml = (!(tableStyle != "") ? tableInfo.PrintHtml : tableStyle);
                }
                List<TableInfo> subTable = __TableInfo.GetSubTable();
                int num = 0;
                foreach (TableInfo tableInfo1 in subTable)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    tableName = tableInfo1.TableName;
                    htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(printHtml);
                    elementbyId = htmlDocument.GetElementbyId(tableName);
                    if (elementbyId != null)
                    {
                        DataTable dataTable1 = new DataTable();
                        List<EIS.DataModel.Model.FieldInfo> fieldInfos = new List<EIS.DataModel.Model.FieldInfo>();
                        if (tableInfo.TableType == 1)
                        {
                            fieldInfos = __FieldInfo.GetTablePhyFields(tableName);
                            str2 = string.Concat("select * from ", tableName, " where ", (dataTable.Rows.Count > 0 ? string.Concat(" _MainID='", this.MainId, "'") : "1=2"));
                            dataTable1 = SysDatabase.ExecuteTable(SysDatabase.GetSqlStringCommand(str2));
                        }
                        else if (tableInfo.TableType == 3)
                        {
                            str2 = tableInfo1.ListSQL.Replace("@_MainID", this.MainId);
                            dataTable1 = this.ExecuteTable(str2, tableInfo1.ConnectionId);
                        }
                        string outerHtml1 = elementbyId.OuterHtml;
                        if (!(tableInfo1.EditMode == "1"))
                        {
                            htmlNode = elementbyId.SelectSingleNode("thead");
                            htmlNode1 = elementbyId.SelectSingleNode("tbody");
                            outerHtml = htmlNode1.OuterHtml;
                            htmlNode2 = htmlNode1.SelectSingleNode("tr");
                            str = htmlNode2.OuterHtml;
                            stringBuilder.AppendFormat("<tbody id='{0}'>", string.Concat("SubTbl0", num.ToString()));
                            if (dataTable1.Rows.Count <= 0)
                            {
                                stringBuilder.Append("");
                            }
                            else
                            {
                                for (i = 0; i < dataTable1.Rows.Count; i++)
                                {
                                    stringBuilder.Append(this.toPrintHtml(tableName, fieldInfos, str, dataTable1.Rows[i]));
                                }
                            }
                            stringBuilder.Append("</tbody>");
                            printHtml = htmlDocument.DocumentNode.OuterHtml.Replace(outerHtml, stringBuilder.ToString()).Replace("[#ADDBTN#]", "").Replace("[#ADD#]", "");
                        }
                        else
                        {
                            if (dataTable1.Rows.Count <= 0)
                            {
                                stringBuilder.Append("");
                            }
                            else
                            {
                                for (i = 0; i < dataTable1.Rows.Count; i++)
                                {
                                    stringBuilder.Append(this.toPrintHtml(tableName, fieldInfos, outerHtml1, dataTable1.Rows[i]));
                                }
                            }
                            printHtml = printHtml.Replace(outerHtml1, stringBuilder.ToString());
                        }
                        num++;
                    }
                }
                printHtml = this.toPrintHtml(tblName, tablePhyFields, printHtml, dataTable.Rows[0]);
                DataTable dataTable2 = SysDatabase.ExecuteTable(string.Concat("select * from T_E_Sys_Relation where TableName='", tblName, "'"));
                foreach (DataRow row in dataTable2.Rows)
                {
                    tableName = row["subTable"].ToString();
                    __FieldInfo.GetTableFields(tableName);
                    htmlDocument = new HtmlDocument()
                    {
                        OptionOutputOriginalCase = true
                    };
                    htmlDocument.LoadHtml(printHtml);
                    elementbyId = htmlDocument.GetElementbyId(tableName);
                    if (elementbyId != null)
                    {
                        string str3 = "";
                        htmlNode = elementbyId.SelectSingleNode("thead");
                        htmlNode1 = elementbyId.SelectSingleNode("tbody");
                        str3 = (htmlNode == null ? elementbyId.FirstChild.OuterHtml : htmlNode.OuterHtml);
                        htmlNode2 = htmlNode1.SelectSingleNode("tr");
                        htmlNode2.SetAttributeValue("Id", "[#BODYID#]");
                        htmlNode2.SetAttributeValue("class", "dataRow");
                        string outerHtml2 = elementbyId.OuterHtml;
                        outerHtml = htmlNode1.OuterHtml;
                        str = htmlNode2.OuterHtml;
                        string[] mainId = new string[] { "select * from ", tableName, " where _MainID='", this.MainId, "' order by _CreateTime" };
                        DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(string.Concat(mainId));
                        DataTable dataTable3 = new DataTable();
                        dataTable3 = SysDatabase.ExecuteTable(sqlStringCommand);
                        StringBuilder stringBuilder1 = new StringBuilder();
                        stringBuilder1.AppendFormat("<tbody>", new object[0]);
                        if (dataTable3.Rows.Count > 0)
                        {
                            for (i = 0; i < dataTable3.Rows.Count; i++)
                            {
                                string str4 = this.ReplaceWithDataRow(str, dataTable3.Rows[i], i);
                                string str5 = dataTable3.Rows[i]["_AutoID"].ToString();
                                str4 = str4.Replace("[#BODYID#]", str5);
                                str4 = str4.Replace("[#DELBTN#]", "").Replace("[#DEL#]", "");
                                str4 = str4.Replace("[#EDIT#]", "");
                                stringBuilder1.Append(str4.Replace("[#COPY#]", ""));
                            }
                        }
                        stringBuilder1.Append("</tbody>");
                        string str6 = "操作";
                        printHtml = htmlDocument.DocumentNode.OuterHtml.Replace(outerHtml2, outerHtml2.Replace(outerHtml, stringBuilder1.ToString()).Replace("[#ADDBTN#]", str6).Replace("[#ADD#]", str6));
                    }
                }
                this.GenIdCodeRelation(workflowDefineModelById);
                printHtml = Regex.Replace(printHtml, string.Concat("{(", tblName, "\\.)?\\w+}"), "", RegexOptions.IgnoreCase);
                this.adviceList = InstanceService.GetUserDealState(instanceByAppInfo.InstanceId);
                Regex regex = new Regex("{(Advice|Action|Employee|Employees|Signature|Signatures|Time|Sign|Block|Position|DeptName|CompanyName):(\\w+)(:(.*?))?}", RegexOptions.IgnoreCase);
                printHtml = regex.Replace(printHtml, new MatchEvaluator(this.ReplaceMatch));
                Regex regex1 = new Regex("{AdviceList:(.*)}", RegexOptions.IgnoreCase);
                printHtml = regex1.Replace(printHtml, this.GetAdviceList(this.adviceList, ""));
                string str7 = this.ReplaceParaValue(this.ReplaceValue, printHtml);
                str7 = str7.Replace("[#DELBTN#]", "").Replace("[#DEL#]", "");
                str1 = str7;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str1;
        }
        public string GetMobileHtml(string tblName, string strwhere)
        {
            string str;
            string formHtml2 = "";
            _TableInfo __TableInfo = new _TableInfo(tblName);
            _FieldInfo __FieldInfo = new _FieldInfo();
            string str1 = "";
            TableInfo tableInfo = new TableInfo();
            try
            {
                tableInfo = __TableInfo.GetModel();
                if (tableInfo == null)
                {
                    throw new Exception(string.Concat("找不到业务定义[", tblName, "]"));
                }
                DataTable dataTable = new DataTable();
                str1 = ((tableInfo.DetailSQL.Trim() != "" ? true : tableInfo.TableType != 1) ? tableInfo.DetailSQL.Replace("|^condition^|", strwhere) : string.Concat("select * from ", tblName, " where ", strwhere));
                if (str1.Trim() == "")
                {
                    throw new Exception(string.Concat(tblName, "的单条SQL语句不能空"));
                }
                this.fileLogger.Trace(str1);
                dataTable = this.ExecuteTable(str1, tableInfo.ConnectionId);
                if (dataTable.Rows.Count <= 0)
                {
                    throw new Exception(string.Concat("找不到对应的业务数据！查询SQL：[", str1, "]"));
                }
                if (!dataTable.Columns.Contains("_AutoID"))
                {
                    throw new Exception(string.Concat("对应的主查询应该返回_AutoID列！主查询SQL：[", str1, "]"));
                }
                this.MainId = dataTable.Rows[0]["_AutoID"].ToString();
                if (!string.IsNullOrEmpty(this.MainId))
                {
                    str1 = string.Format("select top 1  IsNull(t.NodeStyle,'') from T_E_WF_Task t inner join T_E_WF_UserTask u \r\n                            on t._AutoID=u.TaskId and (u.DealUser='{2}' or u.OwnerId='{2}') and IsNull(u.IsAssign,'0') = '0'\r\n                            inner join T_E_WF_Instance i on i._AutoID=t.InstanceId \r\n                            where i.AppName='{0}' and i.AppId='{1}' order by t._CreateTime desc", tblName, this.MainId, this._page.EmployeeID);
                    object obj = SysDatabase.ExecuteScalar(str1);
                    if (obj != null)
                    {
                        if (obj.ToString() != "")
                        {
                            this.Sindex = obj.ToString();
                        }
                    }
                }
                formHtml2 = tableInfo.FormHtml2;
                formHtml2 = formHtml2.Replace("\r\n", "");
                if (this.Sindex != "")
                {
                    string tableStyle = __TableInfo.GetTableStyle(this.Sindex, 1);
                    if (tableStyle != "")
                    {
                        formHtml2 = tableStyle;
                    }
                }
                if (!(formHtml2.Trim() == ""))
                {
                    List<EIS.DataModel.Model.FieldInfo> tablePhyFields = __FieldInfo.GetTablePhyFields(tableInfo.TableName);
                    List<TableInfo> subTable = __TableInfo.GetSubTable();
                    if (subTable.Count > 0)
                    {
                        int num = 0;
                        foreach (TableInfo tableInfo1 in subTable)
                        {
                            StringBuilder stringBuilder = new StringBuilder();
                            string tableName = tableInfo1.TableName;
                            HtmlDocument htmlDocument = new HtmlDocument();
                            htmlDocument.LoadHtml(formHtml2);
                            HtmlNode elementbyId = htmlDocument.GetElementbyId(tableName);
                            if (elementbyId != null)
                            {
                                DataTable dataTable1 = new DataTable();
                                string outerHtml = elementbyId.OuterHtml;
                                elementbyId.SelectSingleNode("thead");
                                HtmlNode htmlNode = elementbyId.SelectSingleNode("tbody");
                                string outerHtml1 = htmlNode.OuterHtml;
                                string outerHtml2 = htmlNode.SelectSingleNode("tr").OuterHtml;
                                List<EIS.DataModel.Model.FieldInfo> fieldInfos = new List<EIS.DataModel.Model.FieldInfo>();
                                if (tableInfo.TableType == 1)
                                {
                                    fieldInfos = __FieldInfo.GetTablePhyFields(tableName);
                                    str1 = string.Concat("select * from ", tableName, " where ", (dataTable.Rows.Count > 0 ? string.Concat(" _MainID='", this.MainId, "'") : "1=2"));
                                    dataTable1 = SysDatabase.ExecuteTable(SysDatabase.GetSqlStringCommand(str1));
                                }
                                else if (tableInfo.TableType == 3)
                                {
                                    str1 = tableInfo1.ListSQL.Replace("@_MainID", this.MainId);
                                    this.fileLogger.Trace<string, string>("strcmd={0}，ConnectionId={1}", str1, tableInfo1.ConnectionId);
                                    dataTable1 = this.ExecuteTable(str1, tableInfo1.ConnectionId);
                                    this.fileLogger.Trace("datasub.Count={0}", dataTable1.Rows.Count);
                                }
                                stringBuilder.AppendFormat("<tbody id='{0}'>", string.Concat("SubTbl0", num.ToString()));
                                if (dataTable1.Rows.Count <= 0)
                                {
                                    stringBuilder.Append("");
                                }
                                else
                                {
                                    this.fileLogger.Trace<string, string>("subname={0},innerhtml={1}", tableName, outerHtml2);
                                    for (int i = 0; i < dataTable1.Rows.Count; i++)
                                    {
                                        stringBuilder.Append(this.toPrintHtml(tableName, fieldInfos, outerHtml2, dataTable1.Rows[i]));
                                    }
                                }
                                stringBuilder.Append("</tbody>");
                                formHtml2 = htmlDocument.DocumentNode.OuterHtml.Replace(outerHtml1, stringBuilder.ToString()).Replace("[#ADDBTN#]", "").Replace("[#ADD#]", "");
                                num++;
                            }
                        }
                    }
                    formHtml2 = this.toPrintHtml(tblName, tablePhyFields, formHtml2, dataTable.Rows[0]);
                    string ubbCode = this.ReplaceParaValue(this.ReplaceValue, formHtml2);
                    ubbCode = ubbCode.Replace("[#DELBTN#]", "").Replace("[#DEL#]", "");
                    ubbCode = this.GetUbbCode(ubbCode, "[CRYPT]", "[/CRYPT]", this._page.UserName);
                    ubbCode = string.Concat(ubbCode, "\r\n<script type='text/javascript'>", this.FeScriptBlock.ToString(), "\r\n</script>");
                    foreach (Match match in (new Regex("<img[^>]*?src=\"([^>\"]*)\"", RegexOptions.IgnoreCase)).Matches(ubbCode))
                    {
                        string userName = this._page.UserName;
                        long ticks = DateTime.Now.Ticks;
                        string str2 = Security.EncryptStr(string.Concat("u=", userName, "&t=", ticks.ToString()), "mytech");
                        string str3 = string.Concat(match.Groups[1].Value, "&loginkey=", str2);
                        ubbCode = ubbCode.Replace(match.Groups[1].Value, str3);
                    }
                    str = ubbCode;
                }
                else
                {
                    str = "打印样式为空";
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str;
        }
        /// <summary>
        /// 获取打印界面
        /// </summary>
        /// <param name="tblName"></param>
        /// <param name="strwhere"></param>
        /// <returns></returns>
        public string GetDetailHtml(string tblName, string strWhere)
        {
            string str;
            string str1;
            try
            {
                List<EIS.DataModel.Model.FieldInfo> tablePhyFields;
                HtmlNode htmlNode;
                int i;
                HtmlNode htmlNode1;
                string outerHtml;

                string outerHtml1;

                int num;
                string detailHtml = "";
                _TableInfo __TableInfo = new _TableInfo(tblName);
                _FieldInfo __FieldInfo = new _FieldInfo();
                string str2 = "";
                TableInfo tableInfo = new TableInfo();

                tableInfo = __TableInfo.GetModel();
                if (tableInfo == null)
                {
                    throw new Exception(string.Concat("找不到业务定义[", tblName, "]"));
                }
                tblName = tableInfo.TableName;
                Instance instance = null;
                DataTable dataTable = new DataTable();
                str2 = ((tableInfo.DetailSQL.Trim() != "" ? true : tableInfo.TableType != 1) ? tableInfo.DetailSQL : string.Concat("select * from ", tblName, " where ", strWhere));
                if (str2.Trim() == "")
                {
                    throw new Exception(string.Concat(tblName, "的单条SQL语句不能空"));
                }
                this.fileLogger.Trace(str2);
                str2 = str2.Replace("|^condition^|", strWhere.Replace("[QUOTES]", "'")).Replace("|^sortdir^|", "");
                if (!string.IsNullOrEmpty(this.DefaultValue))
                {
                    str2 = Utility.ReplaceParaValues(str2, this.DefaultValue);
                }
                str2 = Utility.DealCommandBySeesion(str2);
                dataTable = this.ExecuteTable(str2, tableInfo.ConnectionId);
                if (dataTable.Rows.Count <= 0)
                {
                    throw new Exception(string.Concat("找不到对应的业务数据！查询SQL：[", str2, "]"));
                }
                if (dataTable.Columns.Contains("_AutoID"))
                {
                    this.MainId = dataTable.Rows[0]["_AutoID"].ToString();
                }
                if (!string.IsNullOrEmpty(this.MainId))
                {
                    str2 = string.Format("select top 1  IsNull(t.NodeStyle,'') from T_E_WF_Task t inner join T_E_WF_UserTask u \r\n                            on t._AutoID=u.TaskId and (u.DealUser='{2}' or u.OwnerId='{2}') and IsNull(u.IsAssign,'0') = '0'\r\n                            inner join T_E_WF_Instance i on i._AutoID=t.InstanceId \r\n                            where i.AppName='{0}' and i.AppId='{1}' order by t._CreateTime desc", tblName, this.MainId, this._page.EmployeeID);
                    object obj = SysDatabase.ExecuteScalar(str2);
                    if (obj != null)
                    {
                        if (obj.ToString() != "")
                        {
                            this.Sindex = obj.ToString();
                        }
                    }
                }
                detailHtml = tableInfo.DetailHtml;
                if (!(this.Sindex != ""))
                {
                    tablePhyFields = __FieldInfo.GetTablePhyFields(tableInfo.TableName);
                }
                else
                {
                    tablePhyFields = __FieldInfo.GetFieldsStyleMerged(tableInfo.TableName, int.Parse(this.Sindex));
                    string tableStyle = __TableInfo.GetTableStyle(this.Sindex, 3);
                    if (tableStyle != "")
                    {
                        detailHtml = tableStyle;
                    }
                }
                if (detailHtml.Trim() == "")
                {
                    detailHtml = tableInfo.FormHtml;
                }
                List<TableInfo> subTable = __TableInfo.GetSubTable();
                if (subTable.Count > 0)
                {
                    if (string.IsNullOrEmpty(this.MainId))
                    {
                        throw new Exception(string.Concat("对应的主查询应该返回_AutoID列！主查询SQL：[", str2, "]"));
                    }
                    int num1 = 0;
                    foreach (TableInfo tableInfo1 in subTable)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        string tableName = tableInfo1.TableName;
                        HtmlDocument htmlDocument = new HtmlDocument();
                        htmlDocument.LoadHtml(detailHtml);
                        HtmlNode elementbyId = htmlDocument.GetElementbyId(tableName);
                        if (elementbyId != null)
                        {
                            List<EIS.DataModel.Model.FieldInfo> fieldInfos = new List<EIS.DataModel.Model.FieldInfo>();
                            DataTable dataTable1 = new DataTable();
                            if (tableInfo.TableType == 1)
                            {
                                fieldInfos = (!(this.Sindex != "") ? __FieldInfo.GetTablePhyFields(tableName) : __FieldInfo.GetFieldsStyleMerged(tableName, int.Parse(this.Sindex)));
                                str2 = string.Concat("select * from ", tableName, " where ", (dataTable.Rows.Count > 0 ? string.Concat(" _MainID='", this.MainId, "'") : "1=2"));
                                dataTable1 = SysDatabase.ExecuteTable(SysDatabase.GetSqlStringCommand(str2));
                            }
                            else if (tableInfo.TableType == 3)
                            {
                                str2 = tableInfo1.ListSQL.Replace("@_MainID", this.MainId);
                                dataTable1 = this.ExecuteTable(str2, tableInfo1.ConnectionId);
                            }
                            if (!(tableInfo1.EditMode == "1"))
                            {
                                htmlNode1 = elementbyId.SelectSingleNode("thead");
                                htmlNode = elementbyId.SelectSingleNode("tbody");
                                outerHtml = elementbyId.OuterHtml;
                                str = htmlNode.OuterHtml;
                                outerHtml1 = htmlNode.SelectSingleNode("tr").OuterHtml;
                                stringBuilder.AppendFormat("<tbody id='{0}'>", string.Concat("SubTbl0", num1.ToString()));
                                if (dataTable1.Rows.Count <= 0)
                                {
                                    stringBuilder.Append("");
                                }
                                else
                                {
                                    for (i = 0; i < dataTable1.Rows.Count; i++)
                                    {
                                        num = i + 1;
                                        stringBuilder.Append(this.toPrintHtml(tableName, fieldInfos, outerHtml1, dataTable1.Rows[i]).Replace("[#SN#]", num.ToString()));
                                    }
                                }
                                stringBuilder.Append("</tbody>");

                           

                                detailHtml = htmlDocument.DocumentNode.OuterHtml.Replace(str, stringBuilder.ToString()).Replace("<td style=\"text-align: center;\">[#ADDBTN#]</td>", "").Replace("<td style=\"text-align: center;\">[#DELBTN#]</td>", "");
                                detailHtml= detailHtml.Replace("<td style=\"text-align: center;\">[#DEL#]</td>", "").Replace("<td class=\"ADDBTN\">[#ADDBTN#]</td>", "");
                                detailHtml= detailHtml.Replace("<td class=\"DELBTN\">[#DELBTN#]</td>", "") .Replace("[#ADDBTN#]", "").Replace("[#ADD#]", "");
                                   
                            }
                            else
                            {
                                htmlNode = elementbyId.SelectSingleNode("tbody");
                                if (htmlNode.SelectNodes("tr").Count <= 1)
                                {
                                    htmlNode1 = elementbyId.SelectSingleNode("thead");
                                    outerHtml = elementbyId.OuterHtml;
                                    str = htmlNode.OuterHtml;
                                    outerHtml1 = htmlNode.SelectSingleNode("tr").OuterHtml;
                                    stringBuilder.AppendFormat("<tbody id='{0}'>", string.Concat("SubTbl0", num1.ToString()));
                                    if (dataTable1.Rows.Count <= 0)
                                    {
                                        stringBuilder.Append("");
                                    }
                                    else
                                    {
                                        for (i = 0; i < dataTable1.Rows.Count; i++)
                                        {
                                            num = i + 1;
                                            stringBuilder.Append(this.toPrintHtml(tableName, fieldInfos, outerHtml1, dataTable1.Rows[i]).Replace("[#SN#]", num.ToString()));
                                        }
                                    }
                                    stringBuilder.Append("</tbody>");
                                   // detailHtml = htmlDocument.DocumentNode.OuterHtml.Replace(str, stringBuilder.ToString()).Replace("[#ADDBTN#]", "").Replace("[#ADD#]", "");
                                    detailHtml = htmlDocument.DocumentNode.OuterHtml.Replace(str, stringBuilder.ToString()).Replace("<td style=\"text-align: center;\">[#ADDBTN#]</td>", "").Replace("<td style=\"text-align: center;\">[#DELBTN#]</td>", "");
                                    detailHtml = detailHtml.Replace("<td style=\"text-align: center;\">[#DEL#]</td>", "").Replace("<td class=\"ADDBTN\">[#ADDBTN#]</td>", "");
                                    detailHtml = detailHtml.Replace("<td class=\"DELBTN\">[#DELBTN#]</td>", "").Replace("[#ADDBTN#]", "").Replace("[#ADD#]", "");
                                }
                                else
                                {
                                    string outerHtml2 = elementbyId.OuterHtml;
                                    if (dataTable1.Rows.Count <= 0)
                                    {
                                        stringBuilder.Append("");
                                    }
                                    else
                                    {
                                        for (i = 0; i < dataTable1.Rows.Count; i++)
                                        {
                                            stringBuilder.Append(this.toPrintHtml(tableName, fieldInfos, outerHtml2, dataTable1.Rows[i]));
                                        }
                                    }
                                    detailHtml = detailHtml.Replace(outerHtml2, stringBuilder.ToString());
                                }
                            }
                            num1++;
                        }
                    }
                }
                detailHtml = this.toPrintHtml(tblName, tablePhyFields, detailHtml, dataTable.Rows[0]);
                if (!string.IsNullOrEmpty(this.MainId))
                {
                    if (instance != null)
                    {
                        this.GenIdCodeRelation(DefineService.GetWorkflowDefineModelById(instance.WorkflowId));
                        detailHtml = Regex.Replace(detailHtml, string.Concat("{(", tblName, "\\.)?\\w+}"), "", RegexOptions.IgnoreCase);
                        this.adviceList = InstanceService.GetUserDealState(instance.InstanceId);
                        Regex regex = new Regex("{(Advice|Action|Employee|Signature|Signatures|Time|Sign|Block|Position|DeptName|CompanyName):(\\w+)(:(.*?))?}", RegexOptions.IgnoreCase);
                        detailHtml = regex.Replace(detailHtml, new MatchEvaluator(this.ReplaceMatch));
                    }
                }
                string ubbCode = this.ReplaceParaValue(this.ReplaceValue, detailHtml.ToString());
                ubbCode = ubbCode.Replace("[#DELBTN#]", "").Replace("[#DEL#]", "").Replace("[#EDIT#]", "").Replace("[#COPY#]", "");
                ubbCode = this.GetUbbCode(ubbCode, "[CRYPT]", "[/CRYPT]", this._page.UserName);
                ubbCode = string.Concat(ubbCode, "\r\n<script language='javascript'>", this.FeScriptBlock.ToString(), "\r\n</script>");
                str1 = ubbCode;
            }
            catch (Exception exception)
            {
                this.fileLogger.Error<string>("ModelBuilder:GetDetailHtml2016:" + exception.ToString());
                throw exception;
            }
            return str1;
        }

        private string GetDetailSub(string tblName, DataRow dtMain, string strHtml)
        {
            int i;
            string str;
            string str1 = dtMain["_AutoId"].ToString();
            _TableInfo __TableInfo = new _TableInfo(tblName);
            _FieldInfo __FieldInfo = new _FieldInfo();
            string str2 = "";
            TableInfo tableInfo = new TableInfo();
            try
            {
                tableInfo = __TableInfo.GetModel();
                if (tableInfo == null)
                {
                    throw new Exception(string.Concat("找不到业务定义[", tblName, "]"));
                }
                tblName = tableInfo.TableName;
                List<TableInfo> subTable = __TableInfo.GetSubTable();
                if (subTable.Count > 0)
                {
                    int num = 0;
                    foreach (TableInfo tableInfo1 in subTable)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        string tableName = tableInfo1.TableName;
                        HtmlDocument htmlDocument = new HtmlDocument();
                        htmlDocument.LoadHtml(strHtml);
                        HtmlNode elementbyId = htmlDocument.GetElementbyId(tableName);
                        if (elementbyId != null)
                        {
                            List<EIS.DataModel.Model.FieldInfo> fieldInfos = new List<EIS.DataModel.Model.FieldInfo>();
                            DataTable dataTable = new DataTable();
                            if (tableInfo.TableType == 1)
                            {
                                fieldInfos = (!(this.Sindex != "") ? __FieldInfo.GetTablePhyFields(tableName) : __FieldInfo.GetFieldsStyleMerged(tableName, int.Parse(this.Sindex)));
                                string[] strArrays = new string[] { "select * from ", tableName, " where  _MainID='", str1, "'" };
                                str2 = string.Concat(strArrays);
                                dataTable = SysDatabase.ExecuteTable(SysDatabase.GetSqlStringCommand(str2));
                            }
                            if (!(tableInfo1.EditMode == "1"))
                            {
                                elementbyId.SelectSingleNode("thead");
                                HtmlNode htmlNode = elementbyId.SelectSingleNode("tbody");
                                string outerHtml = elementbyId.OuterHtml;
                                string outerHtml1 = htmlNode.OuterHtml;
                                string outerHtml2 = htmlNode.SelectSingleNode("tr").OuterHtml;
                                stringBuilder.AppendFormat("<tbody id='{0}'>", string.Concat("SubTbl0", num.ToString()));
                                if (dataTable.Rows.Count <= 0)
                                {
                                    stringBuilder.Append("");
                                }
                                else
                                {
                                    for (i = 0; i < dataTable.Rows.Count; i++)
                                    {
                                        stringBuilder.Append(this.toPrintHtml(tableName, fieldInfos, outerHtml2, dataTable.Rows[i]));
                                    }
                                }
                                stringBuilder.Append("</tbody>");
                                strHtml = htmlDocument.DocumentNode.OuterHtml.Replace(outerHtml1, stringBuilder.ToString()).Replace("[#ADDBTN#]", "").Replace("[#ADD#]", "");
                            }
                            else
                            {
                                string outerHtml3 = elementbyId.OuterHtml;
                                if (dataTable.Rows.Count <= 0)
                                {
                                    stringBuilder.Append("");
                                }
                                else
                                {
                                    for (i = 0; i < dataTable.Rows.Count; i++)
                                    {
                                        stringBuilder.Append(this.toPrintHtml(tableName, fieldInfos, outerHtml3, dataTable.Rows[i]));
                                    }
                                }
                                strHtml = strHtml.Replace(outerHtml3, stringBuilder.ToString());
                            }
                            num++;
                        }
                    }
                }
                List<EIS.DataModel.Model.FieldInfo> tablePhyFields = __FieldInfo.GetTablePhyFields(tableInfo.TableName);
                strHtml = this.toPrintHtml(tblName, tablePhyFields, strHtml, dtMain);
                string ubbCode = this.ReplaceParaValue(this.ReplaceValue, strHtml.ToString());
                ubbCode = ubbCode.Replace("[#DELBTN#]", "").Replace("[#DEL#]", "").Replace("[#EDIT#]", "").Replace("[#COPY#]", "");
                ubbCode = this.GetUbbCode(ubbCode, "[CRYPT]", "[/CRYPT]", this._page.UserName);
                ubbCode = string.Concat(ubbCode, "\r\n<script language='javascript'>", this.FeScriptBlock.ToString(), "\r\n</script>");
                str = ubbCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str;
        }
		private string tranExpression(string exp)
		{
			foreach (Match match in (new Regex("\\{([a-zA-Z0-9_]+)\\}", RegexOptions.IgnoreCase)).Matches(exp))
			{
				exp = exp.Replace(match.Value, string.Concat("{!", match.Groups[1].Value, "!}"));
			}
			return exp;
		}
	}
}