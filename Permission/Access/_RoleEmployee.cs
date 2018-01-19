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
	public class _RoleEmployee
	{
		private DbTransaction dbTransaction_0 = null;

		public _RoleEmployee()
		{
		}

		public _RoleEmployee(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(RoleEmployee model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Org_RoleEmployee (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tRoleID,\r\n\t\t\t\t\tEmployeeID,\r\n\t\t\t\t\tRoleEmployeeType\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@RoleID,\r\n\t\t\t\t\t@EmployeeID,\r\n\t\t\t\t\t@RoleEmployeeType\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "RoleID", DbType.String, model.RoleID);
			SysDatabase.AddInParameter(sqlStringCommand, "EmployeeID", DbType.String, model.EmployeeID);
			SysDatabase.AddInParameter(sqlStringCommand, "RoleEmployeeType", DbType.Int32, model.RoleEmployeeType);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(int int_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Org_RoleEmployee ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, int_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Org_RoleEmployee ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public RoleEmployee GetModel(string string_0)
		{
			RoleEmployee model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Org_RoleEmployee ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			RoleEmployee roleEmployee = new RoleEmployee();
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

		public RoleEmployee GetModel(DataRow dataRow_0)
		{
			RoleEmployee roleEmployee = new RoleEmployee()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				roleEmployee._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				roleEmployee._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				roleEmployee._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			roleEmployee.RoleID = dataRow_0["RoleID"].ToString();
			roleEmployee.EmployeeID = dataRow_0["EmployeeID"].ToString();
			if (dataRow_0["RoleEmployeeType"].ToString() != "")
			{
				roleEmployee.RoleEmployeeType = int.Parse(dataRow_0["RoleEmployeeType"].ToString());
			}
			return roleEmployee;
		}

		public List<RoleEmployee> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<RoleEmployee> roleEmployees = new List<RoleEmployee>();
			stringBuilder.Append("select *  FROM T_E_Org_RoleEmployee ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				roleEmployees.Add(this.GetModel(row));
			}
			return roleEmployees;
		}

		public bool IsExist(string roleId, string employeeId, DbTransaction tran)
		{
			bool flag;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from T_E_Org_RoleEmployee");
			stringBuilder.Append(" where RoleID=@RoleID and EmployeeID=@EmployeeID");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@RoleID", DbType.String, roleId);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeID", DbType.String, employeeId);
			flag = (tran != null ? Convert.ToInt32(SysDatabase.ExecuteScalar(sqlStringCommand, tran)) > 0 : Convert.ToInt32(SysDatabase.ExecuteScalar(sqlStringCommand)) > 0);
			return flag;
		}

		public int Update(RoleEmployee model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Org_RoleEmployee set \r\n\t\t\t\t\t_UserName=@_UserName,\r\n\t\t\t\t\t_OrgCode=@_OrgCode,\r\n\t\t\t\t\t_CreateTime=@_CreateTime,\r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tRoleID=@RoleID,\r\n\t\t\t\t\tEmployeeID=@EmployeeID,\r\n\t\t\t\t\tRoleEmployeeType=@RoleEmployeeType\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "RoleID", DbType.String, model.RoleID);
			SysDatabase.AddInParameter(sqlStringCommand, "EmployeeID", DbType.String, model.EmployeeID);
			SysDatabase.AddInParameter(sqlStringCommand, "RoleEmployeeType", DbType.Int32, model.RoleEmployeeType);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}