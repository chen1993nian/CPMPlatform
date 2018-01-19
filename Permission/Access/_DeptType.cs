using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.Permission.Access
{
	public class _DeptType
	{
		private DbTransaction dbTransaction_0 = null;

		public _DeptType()
		{
		}

		public _DeptType(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(DeptType model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Org_DeptType (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tTypeName,\r\n\t\t\t\t\tTypeState,\r\n\t\t\t\t\tTypeProp,\r\n\t\t\t\t\tOrderID\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@TypeName,\r\n\t\t\t\t\t@TypeState,\r\n\t\t\t\t\t@TypeProp,\r\n\t\t\t\t\t@OrderID\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "TypeName", DbType.String, model.TypeName);
			SysDatabase.AddInParameter(sqlStringCommand, "TypeState", DbType.Int32, model.TypeState);
			SysDatabase.AddInParameter(sqlStringCommand, "TypeProp", DbType.Int32, model.TypeProp);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderID", DbType.Int32, model.OrderID);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(int int_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Org_DeptType ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, int_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Org_DeptType ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by OrderId");
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public DeptType GetModel(string string_0)
		{
			DeptType model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Org_DeptType ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			DeptType deptType = new DeptType();
			DataTable dataTable = SysDatabase.ExecuteTable(sqlStringCommand);
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

		public DeptType GetModel(DataRow dataRow_0)
		{
			DeptType deptType = new DeptType()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				deptType._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				deptType._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				deptType._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			deptType.TypeName = dataRow_0["TypeName"].ToString();
			if (dataRow_0["TypeState"].ToString() != "")
			{
				deptType.TypeState = int.Parse(dataRow_0["TypeState"].ToString());
			}
			if (dataRow_0["TypeProp"].ToString() != "")
			{
				deptType.TypeProp = int.Parse(dataRow_0["TypeProp"].ToString());
			}
			if (dataRow_0["OrderID"].ToString() != "")
			{
				deptType.OrderID = int.Parse(dataRow_0["OrderID"].ToString());
			}
			return deptType;
		}

		public List<DeptType> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<DeptType> deptTypes = new List<DeptType>();
			stringBuilder.Append("select *  FROM T_E_Org_DeptType ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by OrderId");
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				deptTypes.Add(this.GetModel(row));
			}
			return deptTypes;
		}

		public int Update(DeptType model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Org_DeptType set \r\n\t\t\t\t\t_UserName=@_UserName,\r\n\t\t\t\t\t_OrgCode=@_OrgCode,\r\n\t\t\t\t\t_CreateTime=@_CreateTime,\r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tTypeName=@TypeName,\r\n\t\t\t\t\tTypeState=@TypeState,\r\n\t\t\t\t\tTypeProp=@TypeProp,\r\n\t\t\t\t\tOrderID=@OrderID\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "TypeName", DbType.String, model.TypeName);
			SysDatabase.AddInParameter(sqlStringCommand, "TypeState", DbType.Int32, model.TypeState);
			SysDatabase.AddInParameter(sqlStringCommand, "TypeProp", DbType.Int32, model.TypeProp);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderID", DbType.Int32, model.OrderID);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}