using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

//从 EIS.ApppDAC 中迁移过来，解决 Comm和DAC 相互引用问题 _FieldStyle
namespace EIS.AppCommo
{
	public class _C_FieldStyle
	{
		private DbTransaction dbTransaction_0 = null;

		public _C_FieldStyle()
		{
		}

        public _C_FieldStyle(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(FieldStyle model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Sys_FieldStyle (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tTableName,\r\n\t\t\t\t\tStyleIndex,\r\n\t\t\t\t\tFieldName,\r\n\t\t\t\t\tFieldInDisp,\r\n\t\t\t\t\tFieldRead,\r\n\t\t\t\t\tFieldNull,\r\n\t\t\t\t\tFieldWidth,\r\n\t\t\t\t\tFieldHeight,\r\n\t\t\t\t\tFieldDValueType,\r\n\t\t\t\t\tFieldDValue,\r\n\t\t\t\t\tFieldInDispStyle,\r\n\t\t\t\t\tFieldInDispStyleTxt,\r\n\t\t\t\t\tFieldInDispStyleName\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@TableName,\r\n\t\t\t\t\t@StyleIndex,\r\n\t\t\t\t\t@FieldName,\r\n\t\t\t\t\t@FieldInDisp,\r\n\t\t\t\t\t@FieldRead,\r\n\t\t\t\t\t@FieldNull,\r\n\t\t\t\t\t@FieldWidth,\r\n\t\t\t\t\t@FieldHeight,\r\n\t\t\t\t\t@FieldDValueType,\r\n\t\t\t\t\t@FieldDValue,\r\n\t\t\t\t\t@FieldInDispStyle,\r\n\t\t\t\t\t@FieldInDispStyleTxt,\r\n\t\t\t\t\t@FieldInDispStyleName\r\n\t\t\t)");
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
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Sys_FieldStyle ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public List<FieldStyle> GetFieldsStyle(string tblName, int styleIndex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<FieldStyle> fieldStyles = new List<FieldStyle>();
			stringBuilder.Append(" select *  from T_E_Sys_FieldStyle ");
			stringBuilder.Append(string.Concat(" where TableName='", tblName, "' and StyleIndex=", styleIndex.ToString()));
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
			foreach (DataRow row in dataTable.Rows)
			{
				fieldStyles.Add(this.GetModel(row));
			}
			return fieldStyles;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  From T_E_Sys_FieldStyle ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public static int GetMaxIndex(string tblName)
		{
			string str = string.Concat("select isnull(max(styleIndex),0) from T_E_Sys_FieldStyle where TableName='", tblName, "'");
			return Convert.ToInt32(SysDatabase.ExecuteScalar(str));
		}

		public FieldStyle GetModel(string string_0)
		{
			FieldStyle model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_FieldStyle ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			FieldStyle fieldStyle = new FieldStyle();
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

		public FieldStyle GetModel(DataRow dataRow_0)
		{
			FieldStyle fieldStyle = new FieldStyle()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				fieldStyle._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				fieldStyle._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				fieldStyle._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			fieldStyle.TableName = dataRow_0["TableName"].ToString();
			if (dataRow_0["StyleIndex"].ToString() != "")
			{
				fieldStyle.StyleIndex = int.Parse(dataRow_0["StyleIndex"].ToString());
			}
			fieldStyle.FieldName = dataRow_0["FieldName"].ToString();
			if (dataRow_0["FieldInDisp"].ToString() != "")
			{
				fieldStyle.FieldInDisp = int.Parse(dataRow_0["FieldInDisp"].ToString());
			}
			if (dataRow_0["FieldRead"].ToString() != "")
			{
				fieldStyle.FieldRead = int.Parse(dataRow_0["FieldRead"].ToString());
			}
			if (dataRow_0["FieldNull"].ToString() != "")
			{
				fieldStyle.FieldNull = int.Parse(dataRow_0["FieldNull"].ToString());
			}
			fieldStyle.FieldWidth = dataRow_0["FieldWidth"].ToString();
			fieldStyle.FieldHeight = dataRow_0["FieldHeight"].ToString();
			fieldStyle.FieldDValueType = dataRow_0["FieldDValueType"].ToString();
			fieldStyle.FieldDValue = dataRow_0["FieldDValue"].ToString();
			fieldStyle.FieldInDispStyle = dataRow_0["FieldInDispStyle"].ToString();
			fieldStyle.FieldInDispStyleTxt = dataRow_0["FieldInDispStyleTxt"].ToString();
			fieldStyle.FieldInDispStyleName = dataRow_0["FieldInDispStyleName"].ToString();
			return fieldStyle;
		}

		public List<FieldStyle> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<FieldStyle> fieldStyles = new List<FieldStyle>();
			stringBuilder.Append("select *  From T_E_Sys_FieldStyle ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by FieldOdr");
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				fieldStyles.Add(this.GetModel(row));
			}
			return fieldStyles;
		}

		public List<FieldStyle> GetTableFields(string tblName, int styleIndex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<FieldStyle> fieldStyles = new List<FieldStyle>();
			stringBuilder.Append(" select *  from T_E_Sys_FieldStyle ");
			stringBuilder.Append(string.Concat(" where TableName='", tblName, "' and StyleIndex=", styleIndex.ToString()));
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
			foreach (DataRow row in dataTable.Rows)
			{
				fieldStyles.Add(this.GetModel(row));
			}
			return fieldStyles;
		}

		public List<FieldStyle> GetTableFields(string tblName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<FieldStyle> fieldStyles = new List<FieldStyle>();
			stringBuilder.Append(" select *  from T_E_Sys_FieldStyle ");
			stringBuilder.Append(string.Concat(" where TableName='", tblName, "'"));
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
			foreach (DataRow row in dataTable.Rows)
			{
				fieldStyles.Add(this.GetModel(row));
			}
			return fieldStyles;
		}

		public int Update(FieldStyle model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_FieldStyle set \r\n\t\t\t\t\t_UserName=@_UserName,\r\n\t\t\t\t\t_OrgCode=@_OrgCode,\r\n\t\t\t\t\t_CreateTime=@_CreateTime,\r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tTableName=@TableName,\r\n\t\t\t\t\tStyleIndex=@StyleIndex,\r\n\t\t\t\t\tFieldName=@FieldName,\r\n\t\t\t\t\tFieldInDisp=@FieldInDisp,\r\n\t\t\t\t\tFieldRead=@FieldRead,\r\n\t\t\t\t\tFieldNull=@FieldNull,\r\n\t\t\t\t\tFieldWidth=@FieldWidth,\r\n\t\t\t\t\tFieldHeight=@FieldHeight,\r\n\t\t\t\t\tFieldDValueType=@FieldDValueType,\r\n\t\t\t\t\tFieldDValue=@FieldDValue,\r\n\t\t\t\t\tFieldInDispStyle=@FieldInDispStyle,\r\n\t\t\t\t\tFieldInDispStyleTxt=@FieldInDispStyleTxt,\r\n\t\t\t\t\tFieldInDispStyleName=@FieldInDispStyleName\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
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
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}