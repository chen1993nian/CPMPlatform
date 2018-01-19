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
	public class _RoleLimit
	{
		private DbTransaction dbTransaction_0 = null;

		public _RoleLimit()
		{
		}

		public _RoleLimit(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(RoleLimit model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Org_RoleLimit (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tFunID,\r\n\t\t\t\t\tRoleID,\r\n\t\t\t\t\tFunLimit,\r\n\t\t\t\t\tDeptLimit,\r\n\t\t\t\t\tIsDealOwen\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@FunID,\r\n\t\t\t\t\t@RoleID,\r\n\t\t\t\t\t@FunLimit,\r\n\t\t\t\t\t@DeptLimit,\r\n\t\t\t\t\t@IsDealOwen\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "FunID", DbType.String, model.FunID);
			SysDatabase.AddInParameter(sqlStringCommand, "RoleID", DbType.String, model.RoleID);
			SysDatabase.AddInParameter(sqlStringCommand, "FunLimit", DbType.String, model.FunLimit);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptLimit", DbType.Int32, model.DeptLimit);
			SysDatabase.AddInParameter(sqlStringCommand, "IsDealOwen", DbType.Int32, model.IsDealOwen);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(int int_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Org_RoleLimit ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, int_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Org_RoleLimit ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public RoleLimit GetModel(string string_0)
		{
			RoleLimit model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Org_RoleLimit ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			RoleLimit roleLimit = new RoleLimit();
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

		public RoleLimit GetModel(DataRow dataRow_0)
		{
			RoleLimit roleLimit = new RoleLimit()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				roleLimit._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				roleLimit._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				roleLimit._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			roleLimit.FunID = dataRow_0["FunID"].ToString();
			roleLimit.RoleID = dataRow_0["RoleID"].ToString();
			roleLimit.FunLimit = dataRow_0["FunLimit"].ToString();
			if (dataRow_0["DeptLimit"].ToString() != "")
			{
				roleLimit.DeptLimit = int.Parse(dataRow_0["DeptLimit"].ToString());
			}
			if (dataRow_0["IsDealOwen"].ToString() != "")
			{
				roleLimit.IsDealOwen = int.Parse(dataRow_0["IsDealOwen"].ToString());
			}
			return roleLimit;
		}

		public List<RoleLimit> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<RoleLimit> roleLimits = new List<RoleLimit>();
			stringBuilder.Append("select *  FROM T_E_Org_RoleLimit ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				roleLimits.Add(this.GetModel(row));
			}
			return roleLimits;
		}

		public int Update(RoleLimit model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Org_RoleLimit set \r\n\t\t\t\t\t_UserName=@_UserName,\r\n\t\t\t\t\t_OrgCode=@_OrgCode,\r\n\t\t\t\t\t_CreateTime=@_CreateTime,\r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tFunID=@FunID,\r\n\t\t\t\t\tRoleID=@RoleID,\r\n\t\t\t\t\tFunLimit=@FunLimit,\r\n\t\t\t\t\tDeptLimit=@DeptLimit,\r\n\t\t\t\t\tIsDealOwen=@IsDealOwen\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "FunID", DbType.String, model.FunID);
			SysDatabase.AddInParameter(sqlStringCommand, "RoleID", DbType.String, model.RoleID);
			SysDatabase.AddInParameter(sqlStringCommand, "FunLimit", DbType.String, model.FunLimit);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptLimit", DbType.Int32, model.DeptLimit);
			SysDatabase.AddInParameter(sqlStringCommand, "IsDealOwen", DbType.Int32, model.IsDealOwen);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}