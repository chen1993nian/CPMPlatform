using EIS.AppBase;
using EIS.AppModel;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;
using System.Text;
using System.Xml;

namespace EIS.AppEngine
{
	public class ModelIO
	{
		public ModelIO()
		{
		}

		private static bool CheckFieldExists(string tblName, string fieldName)
		{
			string str = string.Format("select count(*) from syscolumns where id = object_id(N'{0}') and name = N'{1}'", tblName, fieldName);
			bool num = Convert.ToInt32(SysDatabase.ExecuteScalar(str)) > 0;
			return num;
		}

		private static bool CheckTableExists(string tblName, DbTransaction Trans)
		{
			string str = string.Format("select count(*) from sysobjects where [name] = '{0}' and xtype='U'", tblName);
			bool num = Convert.ToInt32(SysDatabase.ExecuteScalar(str, Trans)) > 0;
			return num;
		}

		private static XmlElement ExportDict(string dictId, XmlDocument xmlDoc)
		{
			XmlElement xmlElement = xmlDoc.CreateElement("Dict");
			Dict model = (new _Dict()).GetModel(dictId);
			xmlElement.SetAttribute("_AutoID", model._AutoID);
			xmlElement.SetAttribute("DictCode", model.DictCode);
			xmlElement.SetAttribute("DictName", model.DictName);
			xmlElement.SetAttribute("DictCat", model.DictCat);
			foreach (DictEntry modelListByDictId in (new _DictEntry()).GetModelListByDictId(dictId))
			{
				xmlElement.AppendChild(modelListByDictId.ToXml(xmlDoc));
			}
			return xmlElement;
		}

		private static XmlElement ExportSubTbl(TableInfo subModel, bool relationObjs, StringCollection sbRelation, XmlDocument xmlDoc)
		{
			FieldInfo field = null;
			XmlElement xmlElement = xmlDoc.CreateElement("bizModel");
			_TableInfo __TableInfo = new _TableInfo(subModel.TableName);
			xmlElement.SetAttribute("_AutoID", subModel._AutoID);
			xmlElement.SetAttribute("TableName", subModel.TableName);
			xmlElement.SetAttribute("TableNameCn", subModel.TableNameCn);
			xmlElement.SetAttribute("ParentName", subModel.ParentName);
			xmlElement.SetAttribute("TableCat", subModel.TableCat);
			int tableType = subModel.TableType;
			xmlElement.SetAttribute("TableType", tableType.ToString());
			tableType = subModel.DataLog;
			xmlElement.SetAttribute("DataLog", tableType.ToString());
			xmlElement.SetAttribute("ConnectionId", subModel.ConnectionId);
			xmlElement.SetAttribute("EditMode", subModel.EditMode);
			tableType = subModel.InitRows;
			xmlElement.SetAttribute("InitRows", tableType.ToString());
			XmlElement xmlElement1 = xmlDoc.CreateElement("FormHtml");
			xmlElement1.InnerXml = string.Concat("<![CDATA[", Utility.TransferCDATA(subModel.FormHtml), "]]>");
			xmlElement.AppendChild(xmlElement1);
			XmlElement xmlElement2 = xmlDoc.CreateElement("FormHtml2");
			xmlElement2.InnerXml = string.Concat("<![CDATA[", Utility.TransferCDATA(subModel.FormHtml2), "]]>");
			xmlElement.AppendChild(xmlElement2);
			XmlElement xmlElement3 = xmlDoc.CreateElement("PrintHtml");
			xmlElement3.InnerXml = string.Concat("<![CDATA[", Utility.TransferCDATA(subModel.PrintHtml), "]]>");
			xmlElement.AppendChild(xmlElement3);
			XmlElement xmlElement4 = xmlDoc.CreateElement("DetailHtml");
			xmlElement4.InnerXml = string.Concat("<![CDATA[", Utility.TransferCDATA(subModel.DetailHtml), "]]>");
			xmlElement.AppendChild(xmlElement4);
			_FieldStyle __FieldStyle = new _FieldStyle();
			int maxIndex = _FieldStyle.GetMaxIndex(subModel.TableName);
			for (int i = 1; i <= maxIndex; i++)
			{
				XmlElement xmlElement5 = xmlDoc.CreateElement("FieldStyles");
				xmlElement5.SetAttribute("Index", i.ToString());
				List<FieldStyle> tableFields = __FieldStyle.GetTableFields(subModel.TableName, i);
				foreach (FieldStyle tableField in tableFields)
				{
					xmlElement5.AppendChild(tableField.ToXml(xmlDoc));
				}
				if (tableFields.Count > 0)
				{
					xmlElement.AppendChild(xmlElement5);
				}
			}
			XmlElement xmlElement6 = xmlDoc.CreateElement("Fields");
			List<FieldInfo> fields = __TableInfo.GetFields();
			foreach (FieldInfo fieldB in fields)
			{
                xmlElement6.AppendChild(fieldB.ToXml(xmlDoc));
				if (relationObjs)
				{
                    string[] strArrays = fieldB.FieldInDispStyleTxt.Split("|".ToCharArray());
                    if ((fieldB.FieldInDispStyle == "012" || fieldB.FieldInDispStyle == "040" ? true : fieldB.FieldInDispStyle == "050"))
					{
						if ((int)strArrays.Length > 1)
						{
							string str = strArrays[1];
							if (!sbRelation.Contains(string.Concat("dict|", str)))
							{
								sbRelation.Add(string.Concat("dict|", str));
							}
						}
					}
                    if (fieldB.FieldInDispStyle == "032")
					{
						string str1 = strArrays[0];
						if (!sbRelation.Contains(string.Concat("query|", str1)))
						{
							sbRelation.Add(string.Concat("query|", str1));
						}
					}
				}
			}
			xmlElement.AppendChild(xmlElement6);
			XmlElement xmlElement7 = xmlDoc.CreateElement("FieldEvents");
			_FieldEvent __FieldEvent = new _FieldEvent();
			foreach (FieldInfo fieldInfo in fields)
			{
				foreach (FieldEvent modelListBy in __FieldEvent.GetModelListBy(fieldInfo.FieldName, fieldInfo.TableName))
				{
					xmlElement7.AppendChild(modelListBy.ToXml(xmlDoc));
				}
			}
			xmlElement.AppendChild(xmlElement7);
			XmlElement xmlElement8 = xmlDoc.CreateElement("TableStyles");
			_TableStyle __TableStyle = new _TableStyle();
			List<TableStyle> modelList = __TableStyle.GetModelList(string.Concat("TableName='", subModel.TableName, "'"));
			foreach (TableStyle tableStyle in modelList)
			{
				xmlElement8.AppendChild(tableStyle.ToXml(xmlDoc));
			}
			xmlElement.AppendChild(xmlElement8);
			return xmlElement;
		}

		public static void ExportTable(string tblNameOption, StringCollection scFlag, XmlDocument xmlDoc)
		{
			FieldInfo field = null;
			int i;
			XmlElement xmlElement;
			char[] chrArray = new char[] { '|' };
			string[] strArrays = tblNameOption.Split(chrArray);
			string str = strArrays[0];
			bool lower = strArrays[1].ToLower() == "true";
			StringCollection stringCollections = new StringCollection();
			if (!scFlag.Contains(str))
			{
				scFlag.Add(str);
				_TableInfo __TableInfo = new _TableInfo(str);
				if (!__TableInfo.Exists())
				{
					throw new Exception(string.Format("业务名称：{0} 不存在", str));
				}
				TableInfo model = __TableInfo.GetModel();
				XmlElement documentElement = xmlDoc.DocumentElement;
				XmlElement xmlElement1 = xmlDoc.CreateElement("bizModel");
				xmlElement1.SetAttribute("_AutoID", model._AutoID);
				xmlElement1.SetAttribute("TableName", model.TableName);
				xmlElement1.SetAttribute("TableNameCn", model.TableNameCn);
				xmlElement1.SetAttribute("ParentName", model.ParentName);
                xmlElement1.SetAttribute("TableCat", model.TableCat);
                xmlElement1.SetAttribute("PageRecCount", model.PageRecCount.ToString());
                int tableType = model.TableType;
				xmlElement1.SetAttribute("TableType", tableType.ToString());
				tableType = model.DataLog;
				xmlElement1.SetAttribute("DataLog", tableType.ToString());
				xmlElement1.SetAttribute("ConnectionId", model.ConnectionId);
				xmlElement1.SetAttribute("DataLogTmpl", model.DataLogTmpl);
				tableType = model.DeleteMode;
				xmlElement1.SetAttribute("DeleteMode", tableType.ToString());
				tableType = model.ShowState;
				xmlElement1.SetAttribute("ShowState", tableType.ToString());
				tableType = model.ArchiveState;
				xmlElement1.SetAttribute("ArchiveState", tableType.ToString());
				xmlElement1.SetAttribute("OrderField", model.OrderField);
				xmlElement1.SetAttribute("FormWidth", model.FormWidth);
				xmlElement1.SetAttribute("EditMode", model.EditMode);
				tableType = model.InitRows;
				xmlElement1.SetAttribute("InitRows", tableType.ToString());
				XmlElement xmlElement2 = xmlDoc.CreateElement("ListSQL");
				xmlElement2.InnerXml = string.Concat("<![CDATA[", model.ListSQL, "]]>");
				xmlElement1.AppendChild(xmlElement2);
				XmlElement xmlElement3 = xmlDoc.CreateElement("DetailSQL");
				xmlElement3.InnerXml = string.Concat("<![CDATA[", model.DetailSQL, "]]>");
				xmlElement1.AppendChild(xmlElement3);
				XmlElement xmlElement4 = xmlDoc.CreateElement("ListScriptBlock");
				xmlElement4.InnerXml = string.Concat("<![CDATA[", model.ListScriptBlock, "]]>");
				xmlElement1.AppendChild(xmlElement4);
				XmlElement xmlElement5 = xmlDoc.CreateElement("ListPreProcessFn");
				xmlElement5.InnerXml = string.Concat("<![CDATA[", model.ListPreProcessFn, "]]>");
				xmlElement1.AppendChild(xmlElement5);
				XmlElement xmlElement6 = xmlDoc.CreateElement("EditScriptBlock");
				xmlElement6.InnerXml = string.Concat("<![CDATA[", model.EditScriptBlock, "]]>");
				xmlElement1.AppendChild(xmlElement6);
				XmlElement xmlElement7 = xmlDoc.CreateElement("FormHtml");
				xmlElement7.InnerXml = string.Concat("<![CDATA[", Utility.TransferCDATA(model.FormHtml), "]]>");
				xmlElement1.AppendChild(xmlElement7);
				XmlElement xmlElement8 = xmlDoc.CreateElement("FormHtml2");
				xmlElement8.InnerXml = string.Concat("<![CDATA[", Utility.TransferCDATA(model.FormHtml2), "]]>");
				xmlElement1.AppendChild(xmlElement8);
				XmlElement xmlElement9 = xmlDoc.CreateElement("PrintHtml");
				xmlElement9.InnerXml = string.Concat("<![CDATA[", Utility.TransferCDATA(model.PrintHtml), "]]>");
				xmlElement1.AppendChild(xmlElement9);
				XmlElement xmlElement10 = xmlDoc.CreateElement("DetailHtml");
				xmlElement10.InnerXml = string.Concat("<![CDATA[", Utility.TransferCDATA(model.DetailHtml), "]]>");
				xmlElement1.AppendChild(xmlElement10);
				XmlElement xmlElement11 = xmlDoc.CreateElement("Fields");
				List<FieldInfo> fields = __TableInfo.GetFields();
				foreach (FieldInfo fieldA in fields)
				{
                    xmlElement11.AppendChild(fieldA.ToXml(xmlDoc));
					if (lower)
					{
                        string[] strArrays1 = fieldA.FieldInDispStyleTxt.Split("|".ToCharArray());
                        if ((fieldA.FieldInDispStyle == "012" || fieldA.FieldInDispStyle == "040" ? true : fieldA.FieldInDispStyle == "050"))
						{
							if ((int)strArrays1.Length > 1)
							{
								string str1 = strArrays1[1];
								if (!stringCollections.Contains(string.Concat("dict|", str1)))
								{
									stringCollections.Add(string.Concat("dict|", str1));
								}
							}
						}
                        if (fieldA.FieldInDispStyle == "032")
						{
							string str2 = strArrays1[0];
							if (!stringCollections.Contains(string.Concat("query|", str2)))
							{
								stringCollections.Add(string.Concat("query|", str2));
							}
						}
					}
				}
				xmlElement1.AppendChild(xmlElement11);
				_FieldStyle __FieldStyle = new _FieldStyle();
				int maxIndex = _FieldStyle.GetMaxIndex(str);
				for (i = 1; i <= maxIndex; i++)
				{
					xmlElement = xmlDoc.CreateElement("FieldStyles");
					xmlElement.SetAttribute("Index", i.ToString());
					List<FieldStyle> tableFields = __FieldStyle.GetTableFields(str, i);
					foreach (FieldStyle tableField in tableFields)
					{
						xmlElement.AppendChild(tableField.ToXml(xmlDoc));
					}
					if (tableFields.Count > 0)
					{
						xmlElement1.AppendChild(xmlElement);
					}
				}
				_FieldInfoExt __FieldInfoExt = new _FieldInfoExt();
				int num = _FieldInfoExt.GetMaxIndex(str);
				for (i = 1; i <= num; i++)
				{
					xmlElement = xmlDoc.CreateElement("FieldExts");
					xmlElement.SetAttribute("Index", i.ToString());
					List<FieldInfoExt> fieldInfoExts = __FieldInfoExt.GetTableFields(str, i);
					foreach (FieldInfoExt fieldInfoExt in fieldInfoExts)
					{
						xmlElement.AppendChild(fieldInfoExt.ToXml(xmlDoc));
					}
					if (fieldInfoExts.Count > 0)
					{
						xmlElement1.AppendChild(xmlElement);
					}
				}
				XmlElement xmlElement12 = xmlDoc.CreateElement("FieldEvents");
				_FieldEvent __FieldEvent = new _FieldEvent();
				foreach (FieldInfo fieldInfo in fields)
				{
					foreach (FieldEvent modelListBy in __FieldEvent.GetModelListBy(fieldInfo.FieldName, fieldInfo.TableName))
					{
						xmlElement12.AppendChild(modelListBy.ToXml(xmlDoc));
					}
				}
				xmlElement1.AppendChild(xmlElement12);
				XmlElement xmlElement13 = xmlDoc.CreateElement("TableStyles");
				_TableStyle __TableStyle = new _TableStyle();
				foreach (TableStyle modelList in __TableStyle.GetModelList(string.Concat("TableName='", str, "'")))
				{
					xmlElement13.AppendChild(modelList.ToXml(xmlDoc));
				}
				xmlElement1.AppendChild(xmlElement13);
				XmlElement xmlElement14 = xmlDoc.CreateElement("TableScripts");
				_TableScript __TableScript = new _TableScript();
				foreach (TableScript tableScript in __TableScript.GetModelList(string.Concat("TableName='", str, "'")))
				{
					xmlElement14.AppendChild(tableScript.ToXml(xmlDoc));
				}
				xmlElement1.AppendChild(xmlElement14);
				documentElement.AppendChild(xmlElement1);
				if (model.TableType == 1)
				{
					foreach (TableInfo subTable in __TableInfo.GetSubTable())
					{
						xmlElement1.AppendChild(ModelIO.ExportSubTbl(subTable, lower, stringCollections, xmlDoc));
					}
				}
				foreach (string stringCollection in stringCollections)
				{
					chrArray = new char[] { '|' };
					string[] strArrays2 = stringCollection.Split(chrArray);
					if (strArrays2[0] == "dict")
					{
						if (!scFlag.Contains(strArrays2[1]))
						{
							scFlag.Add(strArrays2[1]);
							documentElement.AppendChild(ModelIO.ExportDict(strArrays2[1], xmlDoc));
						}
						else
						{
							continue;
						}
					}
					else if (strArrays2[0] == "query")
					{
						TableInfo modelById = (new _TableInfo()).GetModelById(strArrays2[1]);
						if (modelById != null)
						{
							string str3 = string.Concat(modelById.TableName, "|0|0");
							ModelIO.ExportTable(str3, scFlag, xmlDoc);
						}
					}
				}
			}
		}

		private static string GenTblSQL(XmlElement xmlModel)
		{
			string str;
			StringBuilder stringBuilder = new StringBuilder();
			string attribute = xmlModel.GetAttribute("TableName");
			string attribute1 = xmlModel.GetAttribute("TableType");
			if ((attribute1 == "1" ? true : attribute1 == "2"))
			{
				XmlNode xmlNodes = xmlModel.SelectSingleNode("Fields");
				if ((xmlNodes == null ? false : xmlNodes.SelectNodes("Field").Count > 0))
				{
					stringBuilder.AppendFormat(" if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[{0}]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)\r\n                            DROP TABLE [dbo].[{0}] \r\n                            CREATE TABLE [dbo].[{0}] (\r\n                            [_AutoID] [varchar] (50) NOT NULL DEFAULT (newid()),\r\n                            [_UserName] [varchar] (50) NOT  NULL DEFAULT (''),\r\n                            [_OrgCode] [varchar] (100) NOT  NULL DEFAULT (''),\r\n                            [_CreateTime] [datetime] NOT  NULL DEFAULT (getdate()),\r\n                            [_UpdateTime] [datetime] NOT  NULL DEFAULT (getdate()),\r\n                            [_IsDel] [int] NOT  NULL DEFAULT (0),", attribute);
					if (attribute1 == "1")
					{
						stringBuilder.Append("[_CompanyID] [varchar] (50) NULL DEFAULT (''),");
						stringBuilder.Append("[_WFState] [varchar] (50) NULL ,");
						stringBuilder.Append("[_GDState] [varchar] (50) NULL ,");
						stringBuilder.Append("[_WFCurNode] [nvarchar] (200) NULL ,");
						stringBuilder.Append("[_WFCurUser] [nvarchar] (200) NULL ,");
					}
					if (attribute1 == "2")
					{
						stringBuilder.Append("[_MainID] [varchar] (50) NOT NULL ,");
						stringBuilder.Append("[_MainTbl] [varchar] (100) NOT NULL ,");
					}
					foreach (XmlNode xmlNodes1 in xmlNodes.SelectNodes("Field"))
					{
						XmlElement xmlElement = (XmlElement)xmlNodes1;
						string str1 = xmlElement.GetAttribute("IsComput");
						if (!(str1 == "1"))
						{
							string str2 = xmlElement.GetAttribute("FieldName").Trim();
							if ((str1 != "0" ? true : !AppFields.Contains(str2)))
							{
								string str3 = "";
								switch (int.Parse(xmlElement.GetAttribute("FieldType")))
								{
									case 1:
									{
										str3 = string.Concat(" [nvarchar](", xmlElement.GetAttribute("FieldLength").Trim(), ") NULL");
										break;
									}
									case 2:
									{
										str3 = " [int] NULL";
										break;
									}
									case 3:
									{
										str3 = string.Concat(" [numeric](", xmlElement.GetAttribute("FieldLength").Trim(), ") NULL");
										break;
									}
									case 4:
									{
										str3 = " [datetime] NULL";
										break;
									}
									case 5:
									{
										str3 = " [text] NULL";
										break;
									}
									case 6:
									{
										str3 = " [image] NULL";
										break;
									}
								}
								stringBuilder.AppendFormat(" [{0}] {1},", str2, str3);
							}
						}
					}
					stringBuilder.Length = stringBuilder.Length - 1;
					stringBuilder.AppendFormat(") ON [PRIMARY] \r\n                            ALTER TABLE [dbo].[{0}] ADD \r\n                             CONSTRAINT [PK_{0}] PRIMARY KEY  NONCLUSTERED\r\n                             ([_AutoID])  ON [PRIMARY]\r\n\r\n                            CREATE CLUSTERED INDEX [IX_{0}] ON [dbo].[{0}] \r\n                            (\r\n\t                            [_CreateTime] ASC\r\n                            ) ON [PRIMARY] \r\n                        ", attribute);
				}
				str = stringBuilder.ToString();
			}
			else
			{
				str = "";
			}
			return str;
		}

		public static void ImportDict(XmlElement xmlModel, string catCode, bool reWrite, DbTransaction Trans)
		{
			string attribute = xmlModel.GetAttribute("_AutoID");
			string str = xmlModel.GetAttribute("DictCode");
			string attribute1 = xmlModel.GetAttribute("DictName");
			string str1 = xmlModel.GetAttribute("DictCat");
			_Dict __Dict = new _Dict(Trans);
			if (__Dict.Exists(attribute))
			{
				if (!reWrite)
				{
					throw new Exception(string.Format("字典：{0} 已经存在，不能导入", attribute1));
				}
				__Dict.Delete(attribute);
			}
			Dict dict = new Dict()
			{
				_AutoID = attribute,
				_UserName = "",
				_OrgCode = "",
				_IsDel = 0,
				_CreateTime = DateTime.Now,
				_UpdateTime = DateTime.Now,
				DictCode = str,
				DictName = attribute1,
				DictCat = (catCode == "" ? str1 : catCode)
			};
			__Dict.Add(dict);
			_DictEntry __DictEntry = new _DictEntry(Trans);
			foreach (XmlNode xmlNodes in xmlModel.SelectNodes("DictEntry"))
			{
				__DictEntry.Add(new DictEntry((XmlElement)xmlNodes));
			}
		}

		private static void ImportSubTbl(XmlElement xmlModel, bool reWrite, DbTransaction Trans)
		{
			XmlNode childNode = null;
			string attribute = xmlModel.GetAttribute("TableName");
			xmlModel.GetAttribute("TableType");
			_TableInfo __TableInfo = new _TableInfo(attribute, Trans);
			if (__TableInfo.Exists())
			{
				throw new Exception(string.Format("业务名称：{0} 已经存在，不能导入", attribute));
			}
			TableInfo tableInfo = new TableInfo()
			{
				_AutoID = xmlModel.GetAttribute("_AutoID"),
				_UserName = "",
				_OrgCode = "",
				_CreateTime = DateTime.Now,
				_UpdateTime = DateTime.Now,
				_IsDel = 0,
				TableName = attribute,
				TableNameCn = xmlModel.GetAttribute("TableNameCn"),
				ParentName = xmlModel.GetAttribute("ParentName"),
				TableCat = xmlModel.GetAttribute("TableCat"),
				TableType = int.Parse(xmlModel.GetAttribute("TableType")),
				DataLog = int.Parse(xmlModel.GetAttribute("DataLog")),
				ConnectionId = "",
				EditMode = xmlModel.GetAttribute("EditMode")
			};
			string str = xmlModel.GetAttribute("InitRows");
			tableInfo.InitRows = (str == "" ? 0 : int.Parse(str));
			tableInfo.ListSQL = "";
			tableInfo.DetailSQL = "";
			tableInfo.ListScriptBlock = "";
			tableInfo.ListPreProcessFn = "";
			tableInfo.EditScriptBlock = "";
			if (xmlModel.SelectSingleNode("FormHtml") != null)
			{
				tableInfo.FormHtml = xmlModel.SelectSingleNode("FormHtml").InnerText;
			}
			if (xmlModel.SelectSingleNode("FormHtml2") != null)
			{
				tableInfo.FormHtml2 = xmlModel.SelectSingleNode("FormHtml2").InnerText;
			}
			if (xmlModel.SelectSingleNode("PrintHtml") != null)
			{
				tableInfo.PrintHtml = xmlModel.SelectSingleNode("PrintHtml").InnerText;
			}
			if (xmlModel.SelectSingleNode("DetailHtml") != null)
			{
				tableInfo.DetailHtml = xmlModel.SelectSingleNode("DetailHtml").InnerText;
			}
			__TableInfo.Add(tableInfo);
			_FieldInfo __FieldInfo = new _FieldInfo(Trans);
			foreach (XmlNode childNodeA in xmlModel.SelectSingleNode("Fields").ChildNodes)
			{
                FieldInfo fieldInfo = new FieldInfo((XmlElement)childNodeA);
				if ((fieldInfo.IsComput != 0 ? true : !AppFields.Contains(fieldInfo.FieldName)))
				{
					__FieldInfo.Add(fieldInfo);
				}
			}
			if (tableInfo.TableType == 2)
			{
				try
				{
					_FieldStyle __FieldStyle = new _FieldStyle(Trans);
					foreach (XmlNode xmlNodes in xmlModel.SelectNodes("FieldStyles"))
					{
						foreach (XmlNode childNode1 in xmlNodes.ChildNodes)
						{
							__FieldStyle.Add(new FieldStyle((XmlElement)childNode1));
						}
					}
				}
				catch (Exception exception)
				{
					throw new Exception("导入扩展字段风格时出错", exception);
				}
				try
				{
					_FieldEvent __FieldEvent = new _FieldEvent(Trans);
					foreach (XmlNode xmlNodes1 in xmlModel.SelectSingleNode("FieldEvents").ChildNodes)
					{
						__FieldEvent.Add(new FieldEvent((XmlElement)xmlNodes1));
					}
				}
				catch (Exception exception1)
				{
					throw new Exception("导入字段事件时出错", exception1);
				}
			}
			try
			{
				_TableStyle __TableStyle = new _TableStyle(Trans);
				XmlNode xmlNodes2 = xmlModel.SelectSingleNode("TableStyles");
				if (xmlNodes2 != null)
				{
					foreach (XmlNode childNode2 in xmlNodes2.ChildNodes)
					{
						__TableStyle.Add(new TableStyle((XmlElement)childNode2));
					}
				}
			}
			catch (Exception exception2)
			{
				throw new Exception("导入设计界面时出错", exception2);
			}
		}

		public static void ImportTable(XmlElement xmlModel, string catCode, bool reWrite, DbTransaction Trans)
		{
			XmlNode childNode = null;
			XmlNode xmlNodes = null;
			XmlNode childNode1 = null;
			XmlNode xmlNodes1 = null;
			XmlElement xmlElement;
			string attribute;
			string str;
			string str1;
			string attribute1 = xmlModel.GetAttribute("TableName");
			string attribute2 = xmlModel.GetAttribute("TableType");
			StringBuilder stringBuilder = new StringBuilder();
			_TableInfo __TableInfo = new _TableInfo(attribute1, Trans);
			if (__TableInfo.Exists())
			{
				__TableInfo.DropTableOuterTrans(reWrite);
			}
			TableInfo tableInfo = new TableInfo()
			{
				_AutoID = xmlModel.GetAttribute("_AutoID"),
				_UserName = "",
				_OrgCode = "",
				_CreateTime = DateTime.Now,
				_UpdateTime = DateTime.Now,
				_IsDel = 0,
				TableName = attribute1,
				TableNameCn = xmlModel.GetAttribute("TableNameCn"),
				ParentName = xmlModel.GetAttribute("ParentName"),
                PageRecCount = 15
			};
			if (!(catCode == ""))
			{
				tableInfo.TableCat = catCode;
			}
			else
			{
				tableInfo.TableCat = xmlModel.GetAttribute("TableCat");
			}
            try
            {
                tableInfo.PageRecCount = int.Parse(xmlModel.GetAttribute("PageRecCount"));
            }
            catch { }
            finally { }
            tableInfo.TableType = int.Parse(xmlModel.GetAttribute("TableType"));
            tableInfo.DataLog = int.Parse(xmlModel.GetAttribute("DataLog"));
			tableInfo.ConnectionId = xmlModel.GetAttribute("ConnectionId");
			tableInfo.DataLogTmpl = xmlModel.GetAttribute("DataLogTmpl");
			tableInfo.OrderField = xmlModel.GetAttribute("OrderField");
			tableInfo.DeleteMode = int.Parse(xmlModel.GetAttribute("DeleteMode"));
			tableInfo.ShowState = int.Parse(xmlModel.GetAttribute("ShowState"));
			tableInfo.ArchiveState = int.Parse(xmlModel.GetAttribute("ArchiveState"));
			tableInfo.FormWidth = xmlModel.GetAttribute("FormWidth");
			tableInfo.EditMode = xmlModel.GetAttribute("EditMode");
			string str2 = xmlModel.GetAttribute("InitRows");
			tableInfo.InitRows = (str2 == "" ? 0 : int.Parse(str2));
			tableInfo.ListSQL = xmlModel.SelectSingleNode("ListSQL").InnerText;
			tableInfo.DetailSQL = xmlModel.SelectSingleNode("DetailSQL").InnerText;
			tableInfo.ListScriptBlock = xmlModel.SelectSingleNode("ListScriptBlock").InnerText;
			tableInfo.ListPreProcessFn = xmlModel.SelectSingleNode("ListPreProcessFn").InnerText;
			tableInfo.EditScriptBlock = xmlModel.SelectSingleNode("EditScriptBlock").InnerText;
			tableInfo.FormHtml = xmlModel.SelectSingleNode("FormHtml").InnerText;
			tableInfo.FormHtml2 = xmlModel.SelectSingleNode("FormHtml2").InnerText;
			tableInfo.PrintHtml = xmlModel.SelectSingleNode("PrintHtml").InnerText;
			tableInfo.DetailHtml = xmlModel.SelectSingleNode("DetailHtml").InnerText;
			__TableInfo.Add(tableInfo);
			_FieldInfo __FieldInfo = new _FieldInfo(Trans);
			XmlNode xmlNodes2 = xmlModel.SelectSingleNode("Fields");
			foreach (XmlNode childNodeA in xmlNodes2.ChildNodes)
			{
                FieldInfo fieldInfo = new FieldInfo((XmlElement)childNodeA);
				if ((fieldInfo.IsComput != 0 ? true : !AppFields.Contains(fieldInfo.FieldName)))
				{
					__FieldInfo.Add(fieldInfo);
				}
			}
			_FieldStyle __FieldStyle = new _FieldStyle(Trans);
			foreach (XmlNode xmlNodesA in xmlModel.SelectNodes("FieldStyles"))
			{
                foreach (XmlNode childNode2 in xmlNodesA.ChildNodes)
				{
					__FieldStyle.Add(new FieldStyle((XmlElement)childNode2));
				}
			}
			_FieldInfoExt __FieldInfoExt = new _FieldInfoExt(Trans);
			foreach (XmlNode xmlNodes3 in xmlModel.SelectNodes("FieldExts"))
			{
				foreach (XmlNode childNode3 in xmlNodes3.ChildNodes)
				{
					__FieldInfoExt.Add(new FieldInfoExt((XmlElement)childNode3));
				}
			}
			if (tableInfo.TableType == 1)
			{
				_FieldEvent __FieldEvent = new _FieldEvent(Trans);
				foreach (XmlNode childNode4 in xmlModel.SelectSingleNode("FieldEvents").ChildNodes)
				{
					__FieldEvent.Add(new FieldEvent((XmlElement)childNode4));
				}
			}
			_TableStyle __TableStyle = new _TableStyle(Trans);
			foreach (XmlNode childNodeA1 in xmlModel.SelectSingleNode("TableStyles").ChildNodes)
			{
                __TableStyle.Add(new TableStyle((XmlElement)childNodeA1));
			}
			_TableScript __TableScript = new _TableScript(Trans);
			foreach (XmlNode xmlNodes4 in xmlModel.SelectSingleNode("TableScripts").ChildNodes)
			{
				__TableScript.Add(new TableScript((XmlElement)xmlNodes4));
			}
			if (reWrite)
			{
				stringBuilder.Append(ModelIO.GenTblSQL(xmlModel));
			}
			else if (attribute2 == "1")
			{
				if ((xmlNodes2 == null ? false : xmlNodes2.SelectNodes("Field").Count > 0))
				{
					foreach (XmlNode xmlNodes1a in xmlNodes2.SelectNodes("Field"))
					{
                        xmlElement = (XmlElement)xmlNodes1a;
						attribute = xmlElement.GetAttribute("FieldName").Trim();
						str = xmlElement.GetAttribute("IsComput");
						if (!(str == "1"))
						{
							if ((str != "0" ? true : !AppFields.Contains(attribute)))
							{
								if (!ModelIO.CheckFieldExists(attribute1, attribute))
								{
									str1 = "";
									switch (int.Parse(xmlElement.GetAttribute("FieldType")))
									{
										case 1:
										{
											str1 = string.Concat(" [nvarchar](", xmlElement.GetAttribute("FieldLength").Trim(), ") NULL");
											break;
										}
										case 2:
										{
											str1 = " [int] NULL";
											break;
										}
										case 3:
										{
											str1 = string.Concat(" [numeric](", xmlElement.GetAttribute("FieldLength").Trim(), ") NULL");
											break;
										}
										case 4:
										{
											str1 = " [datetime] NULL";
											break;
										}
										case 5:
										{
											str1 = " [text] NULL";
											break;
										}
									}
									stringBuilder.AppendFormat("alter table {0} add [{1}] {2};", attribute1, attribute, str1);
								}
							}
						}
					}
				}
			}
			foreach (XmlNode xmlNodes5 in xmlModel.SelectNodes("bizModel"))
			{
				XmlElement xmlElement1 = (XmlElement)xmlNodes5;
				attribute2 = xmlElement1.GetAttribute("TableType");
				ModelIO.ImportSubTbl(xmlElement1, reWrite, Trans);
				string attribute3 = xmlElement1.GetAttribute("TableName");
				if (!ModelIO.CheckTableExists(attribute3, Trans))
				{
					stringBuilder.Append(ModelIO.GenTblSQL(xmlElement1));
				}
				else if (reWrite)
				{
					stringBuilder.Append(ModelIO.GenTblSQL(xmlElement1));
				}
				else if (attribute2 == "2")
				{
					XmlNode xmlNodes6 = xmlElement1.SelectSingleNode("Fields");
					if ((xmlNodes6 == null ? false : xmlNodes6.SelectNodes("Field").Count > 0))
					{
						foreach (XmlNode xmlNodes7 in xmlNodes6.SelectNodes("Field"))
						{
							xmlElement = (XmlElement)xmlNodes7;
							attribute = xmlElement.GetAttribute("FieldName");
							str = xmlElement.GetAttribute("IsComput");
							if (!(str == "1"))
							{
								if ((str != "0" ? true : !AppFields.Contains(attribute)))
								{
									if (!ModelIO.CheckFieldExists(attribute3, attribute))
									{
										str1 = "";
										switch (int.Parse(xmlElement.GetAttribute("FieldType")))
										{
											case 1:
											{
												str1 = string.Concat(" [nvarchar](", xmlElement.GetAttribute("FieldLength").Trim(), ") NULL");
												break;
											}
											case 2:
											{
												str1 = " [int] NULL";
												break;
											}
											case 3:
											{
												str1 = string.Concat(" [numeric](", xmlElement.GetAttribute("FieldLength").Trim(), ") NULL");
												break;
											}
											case 4:
											{
												str1 = " [datetime] NULL";
												break;
											}
											case 5:
											{
												str1 = " [text] NULL";
												break;
											}
										}
										stringBuilder.AppendFormat("alter table {0} add [{1}] {2};", attribute3, attribute, str1);
									}
								}
							}
						}
					}
				}
			}
			if (stringBuilder.Length > 0)
			{
				SysDatabase.ExecuteNonQuery(stringBuilder.ToString(), Trans);
			}
		}
	}
}