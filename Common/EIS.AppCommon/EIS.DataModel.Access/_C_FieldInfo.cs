using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text;
using EIS.AppCommo;
//从 EIS.ApppDAC 中迁移过来，解决 Comm和DAC 相互引用问题 _FieldInfo

namespace EIS.AppCommo
{
	public class _C_FieldInfo
	{
		private DbTransaction dbTransaction_0 = null;

		public _C_FieldInfo()
		{
		}

        public _C_FieldInfo(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(FieldInfo model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Sys_FieldInfo (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tTableName,\r\n\t\t\t\t\tFieldOdr,\r\n\t\t\t\t\tFieldName,\r\n\t\t\t\t\tFieldNameCn,\r\n\t\t\t\t\tFieldType,\r\n\t\t\t\t\tFieldLength,\r\n\t\t\t\t\tFieldInDisp,\r\n\t\t\t\t\tFieldRead,\r\n\t\t\t\t\tFieldNull,\r\n\t\t\t\t\tFieldWidth,\r\n\t\t\t\t\tFieldHeight,\r\n\t\t\t\t\tFieldDValueType,\r\n\t\t\t\t\tFieldDValue,\r\n\t\t\t\t\tFieldInDispStyle,\r\n\t\t\t\t\tFieldInDispStyleTxt,\r\n\t\t\t\t\tFieldInDispStyleName,\r\n\t\t\t\t\tFieldInFilter,\r\n\t\t\t\t\tFieldNote,\r\n\t\t\t\t\tIsComput,\r\n\t\t\t\t\tIsUnique,\r\n\t\t\t\t\tListDisp,\r\n\t\t\t\t\tQueryDisp,\r\n\t\t\t\t\tColumnAlign,\r\n\t\t\t\t\tColumnWidth,\r\n\t\t\t\t\tColumnOrder,\r\n\t\t\t\t\tColumnHidden,\r\n\t\t\t\t\tQueryOrder,\r\n                    QueryMatchMode,\r\n                    QueryDefaultType,\r\n                    QueryDefaultValue,\r\n                    QueryStyle,\r\n                    QueryStyleName,\r\n                    QueryStyleTxt,\r\n\t\t\t\t\tColumnRender,\r\n\t\t\t\t\tColFormatExp,\r\n\t\t\t\t\tColTotalExp\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@TableName,\r\n\t\t\t\t\t@FieldOdr,\r\n\t\t\t\t\t@FieldName,\r\n\t\t\t\t\t@FieldNameCn,\r\n\t\t\t\t\t@FieldType,\r\n\t\t\t\t\t@FieldLength,\r\n\t\t\t\t\t@FieldInDisp,\r\n\t\t\t\t\t@FieldRead,\r\n\t\t\t\t\t@FieldNull,\r\n\t\t\t\t\t@FieldWidth,\r\n\t\t\t\t\t@FieldHeight,\r\n\t\t\t\t\t@FieldDValueType,\r\n\t\t\t\t\t@FieldDValue,\r\n\t\t\t\t\t@FieldInDispStyle,\r\n\t\t\t\t\t@FieldInDispStyleTxt,\r\n\t\t\t\t\t@FieldInDispStyleName,\r\n\t\t\t\t\t@FieldInFilter,\r\n\t\t\t\t\t@FieldNote,\r\n\t\t\t\t\t@IsComput,\r\n\t\t\t\t\t@IsUnique,\r\n\t\t\t\t\t@ListDisp,\r\n\t\t\t\t\t@QueryDisp,\r\n\t\t\t\t\t@ColumnAlign,\r\n\t\t\t\t\t@ColumnWidth,\r\n\t\t\t\t\t@ColumnOrder,\r\n\t\t\t\t\t@ColumnHidden,\r\n\t\t\t\t\t@QueryOrder,\r\n                    @QueryMatchMode,\r\n                    @QueryDefaultType,\r\n                    @QueryDefaultValue,\r\n                    @QueryStyle,\r\n                    @QueryStyleName,\r\n                    @QueryStyleTxt,\r\n\t\t\t\t\t@ColumnRender,\r\n\t\t\t\t\t@ColFormatExp,\r\n\t\t\t\t\t@ColTotalExp\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "TableName", DbType.String, model.TableName);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldOdr", DbType.Int32, model.FieldOdr);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldName", DbType.String, model.FieldName);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldNameCn", DbType.String, model.FieldNameCn);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldType", DbType.Int32, model.FieldType);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldLength", DbType.String, model.FieldLength);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldInDisp", DbType.Int32, model.FieldInDisp);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldRead", DbType.Int32, model.FieldRead);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldNull", DbType.Int32, model.FieldNull);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldWidth", DbType.String, model.FieldWidth);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldHeight", DbType.String, model.FieldHeight);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldDValueType", DbType.String, model.FieldDValueType);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldDValue", DbType.String, model.FieldDValue);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldInDispStyle", DbType.String, model.FieldInDispStyle);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldInDispStyleTxt", DbType.String, model.FieldInDispStyleTxt);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldInDispStyleName", DbType.String, model.FieldInDispStyleName);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldInFilter", DbType.String, model.FieldInFilter);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldNote", DbType.String, model.FieldNote);
			SysDatabase.AddInParameter(sqlStringCommand, "IsComput", DbType.Int32, model.IsComput);
			SysDatabase.AddInParameter(sqlStringCommand, "IsUnique", DbType.Int32, model.IsUnique);
			SysDatabase.AddInParameter(sqlStringCommand, "ListDisp", DbType.Int32, model.ListDisp);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryDisp", DbType.Int32, model.QueryDisp);
			SysDatabase.AddInParameter(sqlStringCommand, "ColumnAlign", DbType.String, model.ColumnAlign);
			SysDatabase.AddInParameter(sqlStringCommand, "ColumnWidth", DbType.String, model.ColumnWidth);
			SysDatabase.AddInParameter(sqlStringCommand, "ColumnOrder", DbType.Int32, model.ColumnOrder);
			SysDatabase.AddInParameter(sqlStringCommand, "ColumnHidden", DbType.Int32, model.ColumnHidden);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryOrder", DbType.Int32, model.QueryOrder);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryMatchMode", DbType.String, model.QueryMatchMode);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryDefaultType", DbType.String, model.QueryDefaultType);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryDefaultValue", DbType.String, model.QueryDefaultValue);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryStyle", DbType.String, model.QueryStyle);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryStyleName", DbType.String, model.QueryStyleName);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryStyleTxt", DbType.String, model.QueryStyleTxt);
			SysDatabase.AddInParameter(sqlStringCommand, "ColumnRender", DbType.String, model.ColumnRender);
			SysDatabase.AddInParameter(sqlStringCommand, "ColFormatExp", DbType.String, model.ColFormatExp);
			SysDatabase.AddInParameter(sqlStringCommand, "ColTotalExp", DbType.String, model.ColTotalExp);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Sys_FieldInfo ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public bool ExistWfStateField(string tblName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Concat("select count(*)  From T_E_Sys_FieldInfo where TableName='", tblName, "' and FieldName='_wfstate'"));
			return Convert.ToInt32(SysDatabase.ExecuteScalar(stringBuilder.ToString())) > 0;
		}

		public List<FieldInfoExt> GetDefaultFieldsExt(string tblName)
		{
			List<FieldInfoExt> fieldInfoExts = new List<FieldInfoExt>();
			foreach (FieldInfo modelList in this.GetModelList(string.Concat("TableName='", tblName, "'")))
			{
				FieldInfoExt fieldInfoExt = new FieldInfoExt()
				{
					_AutoID = modelList._AutoID,
					_UserName = modelList._UserName,
					_OrgCode = modelList._OrgCode,
					_CreateTime = modelList._CreateTime,
					_UpdateTime = modelList._UpdateTime,
					_IsDel = modelList._IsDel,
					TableName = modelList.TableName,
					FieldName = modelList.FieldName,
					FieldType = modelList.FieldType,
					FieldNameCn = modelList.FieldNameCn,
					StyleIndex = 0,
					IsComput = modelList.IsComput,
					ListDisp = modelList.ListDisp,
					QueryDisp = modelList.QueryDisp,
					ColumnAlign = modelList.ColumnAlign,
					ColumnWidth = modelList.ColumnWidth,
					ColumnRender = modelList.ColumnRender,
					ColumnHidden = modelList.ColumnHidden,
					ColumnOrder = modelList.ColumnOrder,
					QueryOrder = modelList.QueryOrder,
					ColFormatExp = modelList.ColFormatExp,
					ColTotalExp = modelList.ColTotalExp,
					QueryMatchMode = modelList.QueryMatchMode,
					QueryDefaultType = modelList.QueryDefaultType,
					QueryDefaultValue = modelList.QueryDefaultValue,
					QueryStyle = modelList.QueryStyle,
					QueryStyleName = modelList.QueryStyleName,
					QueryStyleTxt = modelList.QueryStyleTxt
				};
				fieldInfoExts.Add(fieldInfoExt);
			}
			return fieldInfoExts;
		}

		public List<FieldInfo> GetFieldsStyleMerged(string tblName, int styleIndex)
		{
			List<FieldInfo> tablePhyFields = this.GetTablePhyFields(tblName);
			List<FieldStyle> fieldStyles = new List<FieldStyle>();
            fieldStyles = (this.dbTransaction_0 == null ? (new _C_FieldStyle()).GetFieldsStyle(tblName, styleIndex) : (new _C_FieldStyle(this.dbTransaction_0)).GetFieldsStyle(tblName, styleIndex));
			foreach (FieldStyle fieldStyle in fieldStyles)
			{
				FieldInfo fieldInDisp = tablePhyFields.Find((FieldInfo fieldInfo_0) => fieldInfo_0.FieldName == fieldStyle.FieldName);
				if (fieldInDisp == null)
				{
					continue;
				}
				fieldInDisp.FieldInDisp = fieldStyle.FieldInDisp;
				fieldInDisp.FieldRead = fieldStyle.FieldRead;
				fieldInDisp.FieldNull = fieldStyle.FieldNull;
				fieldInDisp.FieldWidth = fieldStyle.FieldWidth;
				fieldInDisp.FieldHeight = fieldStyle.FieldHeight;
				fieldInDisp.FieldDValueType = fieldStyle.FieldDValueType;
				fieldInDisp.FieldDValue = fieldStyle.FieldDValue;
				fieldInDisp.FieldInDispStyle = fieldStyle.FieldInDispStyle;
				fieldInDisp.FieldInDispStyleTxt = fieldStyle.FieldInDispStyleTxt;
				fieldInDisp.FieldInDispStyleName = fieldStyle.FieldInDispStyleName;
				fieldInDisp.FieldStyleId = fieldStyle._AutoID;
			}
			return tablePhyFields;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Sys_FieldInfo ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public FieldInfo GetModel(string string_0)
		{
			FieldInfo model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_FieldInfo ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			FieldInfo fieldInfo = new FieldInfo();
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(sqlStringCommand) : SysDatabase.ExecuteTable(sqlStringCommand, this.dbTransaction_0));
			if (dataTable.Rows.Count <= 0)
			{
				model = null;
			}
			else
			{
				model = this.GetModel(dataTable.Rows[0]);
			}
			return model;
		}

		public FieldInfo GetModel(DataRow dataRow_0)
		{
			FieldInfo fieldInfo = new FieldInfo()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				fieldInfo._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				fieldInfo._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				fieldInfo._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			fieldInfo.TableName = dataRow_0["TableName"].ToString();
			if (dataRow_0["FieldOdr"].ToString() != "")
			{
				fieldInfo.FieldOdr = int.Parse(dataRow_0["FieldOdr"].ToString());
			}
			fieldInfo.FieldName = dataRow_0["FieldName"].ToString();
			fieldInfo.FieldNameCn = dataRow_0["FieldNameCn"].ToString();
			if (dataRow_0["FieldType"].ToString() != "")
			{
				fieldInfo.FieldType = int.Parse(dataRow_0["FieldType"].ToString());
			}
			fieldInfo.FieldLength = dataRow_0["FieldLength"].ToString();
			if (dataRow_0["FieldInDisp"].ToString() != "")
			{
				fieldInfo.FieldInDisp = int.Parse(dataRow_0["FieldInDisp"].ToString());
			}
			if (dataRow_0["FieldRead"].ToString() != "")
			{
				fieldInfo.FieldRead = int.Parse(dataRow_0["FieldRead"].ToString());
			}
			if (dataRow_0["FieldNull"].ToString() != "")
			{
				fieldInfo.FieldNull = int.Parse(dataRow_0["FieldNull"].ToString());
			}
			fieldInfo.FieldWidth = dataRow_0["FieldWidth"].ToString();
			fieldInfo.FieldHeight = dataRow_0["FieldHeight"].ToString();
			fieldInfo.FieldDValueType = dataRow_0["FieldDValueType"].ToString();
			fieldInfo.FieldDValue = dataRow_0["FieldDValue"].ToString();
			fieldInfo.FieldInDispStyle = dataRow_0["FieldInDispStyle"].ToString();
			fieldInfo.FieldInDispStyleTxt = dataRow_0["FieldInDispStyleTxt"].ToString();
			fieldInfo.FieldInDispStyleName = dataRow_0["FieldInDispStyleName"].ToString();
			fieldInfo.FieldInFilter = dataRow_0["FieldInFilter"].ToString();
			fieldInfo.FieldNote = dataRow_0["FieldNote"].ToString();
			if (dataRow_0["IsComput"].ToString() != "")
			{
				fieldInfo.IsComput = int.Parse(dataRow_0["IsComput"].ToString());
			}
			if (dataRow_0["IsUnique"].ToString() != "")
			{
				fieldInfo.IsUnique = int.Parse(dataRow_0["IsUnique"].ToString());
			}
			if (dataRow_0["ListDisp"].ToString() != "")
			{
				fieldInfo.ListDisp = int.Parse(dataRow_0["ListDisp"].ToString());
			}
			if (dataRow_0["QueryDisp"].ToString() != "")
			{
				fieldInfo.QueryDisp = int.Parse(dataRow_0["QueryDisp"].ToString());
			}
			fieldInfo.ColumnAlign = dataRow_0["ColumnAlign"].ToString();
			fieldInfo.ColumnWidth = dataRow_0["ColumnWidth"].ToString();
			if (dataRow_0["ColumnOrder"].ToString() != "")
			{
				fieldInfo.ColumnOrder = int.Parse(dataRow_0["ColumnOrder"].ToString());
			}
			if (dataRow_0["ColumnHidden"].ToString() != "")
			{
				fieldInfo.ColumnHidden = int.Parse(dataRow_0["ColumnHidden"].ToString());
			}
			if (dataRow_0["QueryOrder"].ToString() != "")
			{
				fieldInfo.QueryOrder = int.Parse(dataRow_0["QueryOrder"].ToString());
			}
			fieldInfo.QueryMatchMode = dataRow_0["QueryMatchMode"].ToString();
			fieldInfo.QueryDefaultType = dataRow_0["QueryDefaultType"].ToString();
			fieldInfo.QueryDefaultValue = dataRow_0["QueryDefaultValue"].ToString();
			fieldInfo.QueryStyle = dataRow_0["QueryStyle"].ToString();
			fieldInfo.QueryStyleName = dataRow_0["QueryStyleName"].ToString();
			fieldInfo.QueryStyleTxt = dataRow_0["QueryStyleTxt"].ToString();
			fieldInfo.ColumnRender = dataRow_0["ColumnRender"].ToString();
			fieldInfo.ColFormatExp = dataRow_0["ColFormatExp"].ToString();
			fieldInfo.ColTotalExp = dataRow_0["ColTotalExp"].ToString();
			return fieldInfo;
		}

		public List<FieldInfo> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<FieldInfo> fieldInfos = new List<FieldInfo>();
			stringBuilder.Append("select *  From T_E_Sys_FieldInfo ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by FieldOdr");
			DataTable dataTable = null;
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
			foreach (DataRow row in dataTable.Rows)
			{
				fieldInfos.Add(this.GetModel(row));
			}
			return fieldInfos;
		}

		public List<FieldInfo> GetModelListDisp(string tblName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<FieldInfo> fieldInfos = new List<FieldInfo>();
			stringBuilder.Append(string.Concat("select * From T_E_Sys_FieldInfo where TableName='", tblName, "' and ListDisp=1 order by ColumnOrder"));
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				fieldInfos.Add(this.GetModel(row));
			}
			return fieldInfos;
		}

		public List<FieldInfo> GetModelQueryDisp(string tblName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<FieldInfo> fieldInfos = new List<FieldInfo>();
			stringBuilder.Append(string.Concat("select *  FROM T_E_Sys_FieldInfo where TableName='", tblName, "' and QueryDisp=1 order by QueryOrder"));
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				fieldInfos.Add(this.GetModel(row));
			}
			return fieldInfos;
		}

		public List<FieldInfo> GetTableFields(string tblName)
		{
			return this.GetModelList(string.Concat("TableName='", tblName, "'"));
		}

		public List<FieldInfo> GetTablePhyFields(string tblName)
		{
			return this.GetModelList(string.Concat("TableName='", tblName, "' and isComput=0"));
		}

		public int Update(FieldInfo model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_FieldInfo set \r\n\t\t\t\t\t_UserName=@_UserName,\r\n\t\t\t\t\t_OrgCode=@_OrgCode,\r\n\t\t\t\t\t_CreateTime=@_CreateTime,\r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tTableName=@TableName,\r\n\t\t\t\t\tFieldOdr=@FieldOdr,\r\n\t\t\t\t\tFieldName=@FieldName,\r\n\t\t\t\t\tFieldNameCn=@FieldNameCn,\r\n\t\t\t\t\tFieldType=@FieldType,\r\n\t\t\t\t\tFieldLength=@FieldLength,\r\n\t\t\t\t\tFieldInDisp=@FieldInDisp,\r\n\t\t\t\t\tFieldRead=@FieldRead,\r\n\t\t\t\t\tFieldNull=@FieldNull,\r\n\t\t\t\t\tFieldWidth=@FieldWidth,\r\n\t\t\t\t\tFieldHeight=@FieldHeight,\r\n\t\t\t\t\tFieldDValueType=@FieldDValueType,\r\n\t\t\t\t\tFieldDValue=@FieldDValue,\r\n\t\t\t\t\tFieldInDispStyle=@FieldInDispStyle,\r\n\t\t\t\t\tFieldInDispStyleTxt=@FieldInDispStyleTxt,\r\n\t\t\t\t\tFieldInDispStyleName=@FieldInDispStyleName,\r\n\t\t\t\t\tFieldInFilter=@FieldInFilter,\r\n\t\t\t\t\tFieldNote=@FieldNote,\r\n\t\t\t\t\tIsComput=@IsComput,\r\n\t\t\t\t\tIsUnique=@IsUnique,\r\n\t\t\t\t\tListDisp=@ListDisp,\r\n\t\t\t\t\tQueryDisp=@QueryDisp,\r\n\t\t\t\t\tColumnAlign=@ColumnAlign,\r\n\t\t\t\t\tColumnWidth=@ColumnWidth,\r\n\t\t\t\t\tColumnOrder=@ColumnOrder,\r\n\t\t\t\t\tColumnHidden=@ColumnHidden,\r\n\t\t\t\t\tQueryOrder=@QueryOrder,\r\n                    QueryMatchMode=@QueryMatchMode,\r\n                    QueryDefaultType=@QueryDefaultType,\r\n                    QueryDefaultValue=@QueryDefaultValue,\r\n                    QueryStyle=@QueryStyle,\r\n                    QueryStyleName=@QueryStyleName,\r\n                    QueryStyleTxt=@QueryStyleTxt,\r\n\t\t\t\t\tColumnRender=@ColumnRender,\r\n\t\t\t\t\tColFormatExp=@ColFormatExp,\r\n\t\t\t\t\tColTotalExp=@ColTotalExp\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "TableName", DbType.String, model.TableName);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldOdr", DbType.Int32, model.FieldOdr);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldName", DbType.String, model.FieldName);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldNameCn", DbType.String, model.FieldNameCn);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldType", DbType.Int32, model.FieldType);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldLength", DbType.String, model.FieldLength);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldInDisp", DbType.Int32, model.FieldInDisp);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldRead", DbType.Int32, model.FieldRead);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldNull", DbType.Int32, model.FieldNull);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldWidth", DbType.String, model.FieldWidth);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldHeight", DbType.String, model.FieldHeight);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldDValueType", DbType.String, model.FieldDValueType);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldDValue", DbType.String, model.FieldDValue);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldInDispStyle", DbType.String, model.FieldInDispStyle);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldInDispStyleTxt", DbType.String, model.FieldInDispStyleTxt);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldInDispStyleName", DbType.String, model.FieldInDispStyleName);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldInFilter", DbType.String, model.FieldInFilter);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldNote", DbType.String, model.FieldNote);
			SysDatabase.AddInParameter(sqlStringCommand, "IsComput", DbType.Int32, model.IsComput);
			SysDatabase.AddInParameter(sqlStringCommand, "IsUnique", DbType.Int32, model.IsUnique);
			SysDatabase.AddInParameter(sqlStringCommand, "ListDisp", DbType.Int32, model.ListDisp);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryDisp", DbType.Int32, model.QueryDisp);
			SysDatabase.AddInParameter(sqlStringCommand, "ColumnAlign", DbType.String, model.ColumnAlign);
			SysDatabase.AddInParameter(sqlStringCommand, "ColumnWidth", DbType.String, model.ColumnWidth);
			SysDatabase.AddInParameter(sqlStringCommand, "ColumnOrder", DbType.Int32, model.ColumnOrder);
			SysDatabase.AddInParameter(sqlStringCommand, "ColumnHidden", DbType.Int32, model.ColumnHidden);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryOrder", DbType.Int32, model.QueryOrder);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryMatchMode", DbType.String, model.QueryMatchMode);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryDefaultType", DbType.String, model.QueryDefaultType);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryDefaultValue", DbType.String, model.QueryDefaultValue);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryStyle", DbType.String, model.QueryStyle);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryStyleName", DbType.String, model.QueryStyleName);
			SysDatabase.AddInParameter(sqlStringCommand, "QueryStyleTxt", DbType.String, model.QueryStyleTxt);
			SysDatabase.AddInParameter(sqlStringCommand, "ColumnRender", DbType.String, model.ColumnRender);
			SysDatabase.AddInParameter(sqlStringCommand, "ColFormatExp", DbType.String, model.ColFormatExp);
			SysDatabase.AddInParameter(sqlStringCommand, "ColTotalExp", DbType.String, model.ColTotalExp);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}