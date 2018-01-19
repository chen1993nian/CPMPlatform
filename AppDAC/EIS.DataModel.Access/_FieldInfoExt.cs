using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.DataModel.Access
{
	public class _FieldInfoExt
	{
		private DbTransaction dbTransaction_0 = null;

		public _FieldInfoExt()
		{
		}

		public _FieldInfoExt(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(FieldInfoExt model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Sys_FieldInfoExt (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tTableName,\r\n\t\t\t\t\tStyleIndex,\r\n\t\t\t\t\tFieldName,\r\n\t\t\t\t\tFieldNameCn,\r\n\t\t\t\t\tListDisp,\r\n\t\t\t\t\tQueryDisp,\r\n\t\t\t\t\tColumnAlign,\r\n\t\t\t\t\tColumnWidth,\r\n\t\t\t\t\tColumnOrder,\r\n\t\t\t\t\tColumnHidden,\r\n\t\t\t\t\tQueryOrder,\r\n                    QueryMatchMode,\r\n                    QueryDefaultType,\r\n                    QueryDefaultValue,\r\n                    QueryStyle,\r\n                    QueryStyleName,\r\n                    QueryStyleTxt,\r\n\t\t\t\t\tColumnRender,\r\n\t\t\t\t\tColFormatExp,\r\n\t\t\t\t\tColTotalExp,\r\n\t\t\t\t\tFieldType\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@TableName,\r\n\t\t\t\t\t@StyleIndex,\r\n\t\t\t\t\t@FieldName,\r\n\t\t\t\t\t@FieldNameCn,\r\n\t\t\t\t\t@ListDisp,\r\n\t\t\t\t\t@QueryDisp,\r\n\t\t\t\t\t@ColumnAlign,\r\n\t\t\t\t\t@ColumnWidth,\r\n\t\t\t\t\t@ColumnOrder,\r\n\t\t\t\t\t@ColumnHidden,\r\n\t\t\t\t\t@QueryOrder,\r\n                    @QueryMatchMode,\r\n                    @QueryDefaultType,\r\n                    @QueryDefaultValue,\r\n                    @QueryStyle,\r\n                    @QueryStyleName,\r\n                    @QueryStyleTxt,\r\n\t\t\t\t\t@ColumnRender,\r\n\t\t\t\t\t@ColFormatExp,\r\n\t\t\t\t\t@ColTotalExp,\r\n\t\t\t\t\t@FieldType\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "TableName", DbType.String, model.TableName);
			SysDatabase.AddInParameter(sqlStringCommand, "StyleIndex", DbType.Int32, model.StyleIndex);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldName", DbType.String, model.FieldName);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldNameCn", DbType.String, model.FieldNameCn);
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
			SysDatabase.AddInParameter(sqlStringCommand, "FieldType", DbType.Int32, model.FieldType);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Sys_FieldInfoExt ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Sys_FieldInfoExt ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public static int GetMaxIndex(string tblName)
		{
			string str = string.Concat("select isnull(max(styleindex),0) from T_E_Sys_FieldInfoExt where TableName='", tblName, "'");
			return Convert.ToInt32(SysDatabase.ExecuteScalar(str));
		}

		public FieldInfoExt GetModel(string string_0)
		{
			FieldInfoExt model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_FieldInfoExt ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			FieldInfoExt fieldInfoExt = new FieldInfoExt();
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

		public FieldInfoExt GetModel(DataRow dataRow_0)
		{
			FieldInfoExt fieldInfoExt = new FieldInfoExt()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				fieldInfoExt._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				fieldInfoExt._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				fieldInfoExt._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			fieldInfoExt.TableName = dataRow_0["TableName"].ToString();
			if (dataRow_0["StyleIndex"].ToString() != "")
			{
				fieldInfoExt.StyleIndex = int.Parse(dataRow_0["StyleIndex"].ToString());
			}
			fieldInfoExt.FieldName = dataRow_0["FieldName"].ToString();
			fieldInfoExt.FieldNameCn = dataRow_0["FieldNameCn"].ToString();
			if (dataRow_0["ListDisp"].ToString() != "")
			{
				fieldInfoExt.ListDisp = int.Parse(dataRow_0["ListDisp"].ToString());
			}
			if (dataRow_0["QueryDisp"].ToString() != "")
			{
				fieldInfoExt.QueryDisp = int.Parse(dataRow_0["QueryDisp"].ToString());
			}
			fieldInfoExt.ColumnAlign = dataRow_0["ColumnAlign"].ToString();
			fieldInfoExt.ColumnWidth = dataRow_0["ColumnWidth"].ToString();
			if (dataRow_0["ColumnOrder"].ToString() != "")
			{
				fieldInfoExt.ColumnOrder = int.Parse(dataRow_0["ColumnOrder"].ToString());
			}
			if (dataRow_0["ColumnHidden"].ToString() != "")
			{
				fieldInfoExt.ColumnHidden = int.Parse(dataRow_0["ColumnHidden"].ToString());
			}
			if (dataRow_0["QueryOrder"].ToString() != "")
			{
				fieldInfoExt.QueryOrder = int.Parse(dataRow_0["QueryOrder"].ToString());
			}
			fieldInfoExt.QueryMatchMode = dataRow_0["QueryMatchMode"].ToString();
			fieldInfoExt.QueryDefaultType = dataRow_0["QueryDefaultType"].ToString();
			fieldInfoExt.QueryDefaultValue = dataRow_0["QueryDefaultValue"].ToString();
			fieldInfoExt.QueryStyle = dataRow_0["QueryStyle"].ToString();
			fieldInfoExt.QueryStyleName = dataRow_0["QueryStyleName"].ToString();
			fieldInfoExt.QueryStyleTxt = dataRow_0["QueryStyleTxt"].ToString();
			fieldInfoExt.ColumnRender = dataRow_0["ColumnRender"].ToString();
			fieldInfoExt.ColFormatExp = dataRow_0["ColFormatExp"].ToString();
			fieldInfoExt.ColTotalExp = dataRow_0["ColTotalExp"].ToString();
			if (dataRow_0["FieldType"].ToString() != "")
			{
				fieldInfoExt.FieldType = int.Parse(dataRow_0["FieldType"].ToString());
			}
			return fieldInfoExt;
		}

		public List<FieldInfoExt> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<FieldInfoExt> fieldInfoExts = new List<FieldInfoExt>();
			stringBuilder.Append("select *  FROM T_E_Sys_FieldInfoExt ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
			foreach (DataRow row in dataTable.Rows)
			{
				fieldInfoExts.Add(this.GetModel(row));
			}
			return fieldInfoExts;
		}

		public List<FieldInfoExt> GetModelListDisp(string tblName, string styleIndex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  * from T_E_Sys_FieldInfoExt ");
			stringBuilder.Append(" where TableName=@TableName and ListDisp=1 and StyleIndex=@StyleIndex  order by ColumnOrder");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, tblName);
			SysDatabase.AddInParameter(sqlStringCommand, "@StyleIndex", DbType.Int32, int.Parse(styleIndex));
			List<FieldInfoExt> fieldInfoExts = new List<FieldInfoExt>();
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(sqlStringCommand) : SysDatabase.ExecuteTable(sqlStringCommand, this.dbTransaction_0));
			foreach (DataRow row in dataTable.Rows)
			{
				fieldInfoExts.Add(this.GetModel(row));
			}
			return fieldInfoExts;
		}

		public List<FieldInfoExt> GetModelQueryDisp(string tblName, string styleIndex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  * from T_E_Sys_FieldInfoExt ");
			stringBuilder.Append(" where TableName=@TableName and QueryDisp=1 and StyleIndex=@StyleIndex  order by QueryOrder");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@TableName", DbType.String, tblName);
			SysDatabase.AddInParameter(sqlStringCommand, "@StyleIndex", DbType.Int32, int.Parse(styleIndex));
			List<FieldInfoExt> fieldInfoExts = new List<FieldInfoExt>();
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(sqlStringCommand) : SysDatabase.ExecuteTable(sqlStringCommand, this.dbTransaction_0));
			foreach (DataRow row in dataTable.Rows)
			{
				fieldInfoExts.Add(this.GetModel(row));
			}
			return fieldInfoExts;
		}

		public List<FieldInfoExt> GetTableFields(string tblName, int styleIndex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<FieldInfoExt> fieldInfoExts = new List<FieldInfoExt>();
			stringBuilder.Append(" select *  from T_E_Sys_FieldInfoExt ");
			stringBuilder.Append(string.Concat(" where TableName='", tblName, "' and StyleIndex=", styleIndex.ToString()));
			stringBuilder.Append(" order by ColumnOrder");
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
			foreach (DataRow row in dataTable.Rows)
			{
				fieldInfoExts.Add(this.GetModel(row));
			}
			return fieldInfoExts;
		}

		public List<FieldInfoExt> GetTableFields(string tblName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<FieldInfoExt> fieldInfoExts = new List<FieldInfoExt>();
			stringBuilder.Append(" select *  from T_E_Sys_FieldInfoExt ");
			stringBuilder.Append(string.Concat(" where TableName='", tblName, "'"));
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
			foreach (DataRow row in dataTable.Rows)
			{
				fieldInfoExts.Add(this.GetModel(row));
			}
			return fieldInfoExts;
		}

		public int GetTableFieldsCount(string tblName, string styleIndex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<FieldInfoExt> fieldInfoExts = new List<FieldInfoExt>();
			stringBuilder.Append(" select count(*)  from T_E_Sys_FieldInfoExt ");
			stringBuilder.Append(string.Concat(" where TableName='", tblName, "' and StyleIndex=", styleIndex.ToString()));
			object obj = SysDatabase.ExecuteScalar(stringBuilder.ToString());
			return int.Parse(obj.ToString());
		}

		public int Update(FieldInfoExt model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_FieldInfoExt set \r\n\t\t\t\t\t_UserName=@_UserName,\r\n\t\t\t\t\t_OrgCode=@_OrgCode,\r\n\t\t\t\t\t_CreateTime=@_CreateTime,\r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tTableName=@TableName,\r\n\t\t\t\t\tStyleIndex=@StyleIndex,\r\n\t\t\t\t\tFieldName=@FieldName,\r\n\t\t\t\t\tFieldNameCn=@FieldNameCn,\r\n\t\t\t\t\tListDisp=@ListDisp,\r\n\t\t\t\t\tQueryDisp=@QueryDisp,\r\n\t\t\t\t\tColumnAlign=@ColumnAlign,\r\n\t\t\t\t\tColumnWidth=@ColumnWidth,\r\n\t\t\t\t\tColumnOrder=@ColumnOrder,\r\n\t\t\t\t\tColumnHidden=@ColumnHidden,\r\n\t\t\t\t\tQueryOrder=@QueryOrder,\r\n                    QueryMatchMode=@QueryMatchMode,\r\n                    QueryDefaultType=@QueryDefaultType,\r\n                    QueryDefaultValue=@QueryDefaultValue,\r\n                    QueryStyle=@QueryStyle,\r\n                    QueryStyleName=@QueryStyleName,\r\n                    QueryStyleTxt=@QueryStyleTxt,\r\n\t\t\t\t\tColumnRender=@ColumnRender,\r\n\t\t\t\t\tColFormatExp=@ColFormatExp,\r\n\t\t\t\t\tColTotalExp=@ColTotalExp,\r\n\t\t\t\t\tFieldType=@FieldType\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "TableName", DbType.String, model.TableName);
			SysDatabase.AddInParameter(sqlStringCommand, "StyleIndex", DbType.Int32, model.StyleIndex);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldName", DbType.String, model.FieldName);
			SysDatabase.AddInParameter(sqlStringCommand, "FieldNameCn", DbType.String, model.FieldNameCn);
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
			SysDatabase.AddInParameter(sqlStringCommand, "FieldType", DbType.Int32, model.FieldType);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}