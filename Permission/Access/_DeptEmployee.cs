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
	public class _DeptEmployee
	{
		private DbTransaction dbTransaction_0 = null;

		public _DeptEmployee()
		{
		}

		public _DeptEmployee(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(DeptEmployee model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Org_DeptEmployee (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tDeptID,\r\n\t\t\t\t\tEmployeeID,\r\n\t\t\t\t\tCompanyID,\r\n\t\t\t\t\tPositionId,\r\n\t\t\t\t\tDeptName,\r\n\t\t\t\t\tEmployeeName,\r\n\t\t\t\t\tPositionName,\r\n\t\t\t\t\tDataRegion,\r\n\t\t\t\t\tDeptEmployeeType,\r\n\t\t\t\t\tOrderID,\r\n                    IsDefault\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@DeptID,\r\n\t\t\t\t\t@EmployeeID,\r\n\t\t\t\t\t@CompanyID,\r\n\t\t\t\t\t@PositionId,\r\n\t\t\t\t\t@DeptName,\r\n\t\t\t\t\t@EmployeeName,\r\n\t\t\t\t\t@PositionName,\r\n\t\t\t\t\t@DataRegion,\r\n\t\t\t\t\t@DeptEmployeeType,\r\n\t\t\t\t\t@OrderID,\r\n                    @IsDefault\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptID", DbType.String, model.DeptID);
			SysDatabase.AddInParameter(sqlStringCommand, "EmployeeID", DbType.String, model.EmployeeID);
			SysDatabase.AddInParameter(sqlStringCommand, "CompanyID", DbType.String, model.CompanyID);
			SysDatabase.AddInParameter(sqlStringCommand, "PositionId", DbType.String, model.PositionId);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptName", DbType.String, model.DeptName);
			SysDatabase.AddInParameter(sqlStringCommand, "EmployeeName", DbType.String, model.EmployeeName);
			SysDatabase.AddInParameter(sqlStringCommand, "PositionName", DbType.String, model.PositionName);
			SysDatabase.AddInParameter(sqlStringCommand, "DataRegion", DbType.Int32, model.DataRegion);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptEmployeeType", DbType.Int32, model.DeptEmployeeType);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderID", DbType.Int32, model.OrderID);
			SysDatabase.AddInParameter(sqlStringCommand, "IsDefault", DbType.Int32, model.IsDefault);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Org_DeptEmployee set _isDel=1 ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int GetEmployeeCountByDeptId(string deptId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(*)  From T_E_Org_DeptEmployee ");
			stringBuilder.Append(string.Concat(" where deptId ='", deptId, "' and _IsDel=0 "));
			object obj = SysDatabase.ExecuteScalar(stringBuilder.ToString());
			if ((obj == null ? true : obj == DBNull.Value))
			{
				throw new Exception("Error in GetSubDeptCount");
			}
			return Convert.ToInt32(obj);
		}

		public int GetEmployeeCountByPositionId(string positionId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(*)  From T_E_Org_DeptEmployee ");
			stringBuilder.Append(string.Concat(" where positionId ='", positionId, "' and _IsDel=0 "));
			object obj = SysDatabase.ExecuteScalar(stringBuilder.ToString());
			if ((obj == null ? true : obj == DBNull.Value))
			{
				throw new Exception("Error in GetSubDeptCount");
			}
			return Convert.ToInt32(obj);
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  From T_E_Org_DeptEmployee ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public DeptEmployee GetModel(string string_0)
		{
			DeptEmployee model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Org_DeptEmployee ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			DeptEmployee deptEmployee = new DeptEmployee();
			DataTable dataTable = null;
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

		public DeptEmployee GetModel(DataRow dataRow_0)
		{
			DeptEmployee deptEmployee = new DeptEmployee()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				deptEmployee._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				deptEmployee._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				deptEmployee._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			deptEmployee.DeptID = dataRow_0["DeptID"].ToString();
			deptEmployee.DeptName = dataRow_0["DeptName"].ToString();
			deptEmployee.EmployeeID = dataRow_0["EmployeeID"].ToString();
			deptEmployee.EmployeeName = dataRow_0["EmployeeName"].ToString();
			deptEmployee.PositionId = dataRow_0["PositionId"].ToString();
			deptEmployee.PositionName = dataRow_0["PositionName"].ToString();
			deptEmployee.CompanyID = dataRow_0["CompanyID"].ToString();
			if (dataRow_0["IsDefault"].ToString() != "")
			{
				deptEmployee.IsDefault = int.Parse(dataRow_0["IsDefault"].ToString());
			}
			if (dataRow_0["DataRegion"].ToString() != "")
			{
				deptEmployee.DataRegion = int.Parse(dataRow_0["DataRegion"].ToString());
			}
			if (dataRow_0["DeptEmployeeType"].ToString() != "")
			{
				deptEmployee.DeptEmployeeType = int.Parse(dataRow_0["DeptEmployeeType"].ToString());
			}
			if (dataRow_0["OrderID"].ToString() != "")
			{
				deptEmployee.OrderID = int.Parse(dataRow_0["OrderID"].ToString());
			}
			return deptEmployee;
		}

		public List<DeptEmployee> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<DeptEmployee> deptEmployees = new List<DeptEmployee>();
			stringBuilder.Append("select *  From T_E_Org_DeptEmployee ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				deptEmployees.Add(this.GetModel(row));
			}
			return deptEmployees;
		}

		public int Remove(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Org_DeptEmployee ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Update(DeptEmployee model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Org_DeptEmployee set \r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tDeptID=@DeptID,\r\n\t\t\t\t\tDeptName=@DeptName,\r\n\t\t\t\t\tCompanyID=@CompanyID,\r\n\t\t\t\t\tEmployeeID=@EmployeeID,\r\n\t\t\t\t\tEmployeeName=@EmployeeName,\r\n\t\t\t\t\tPositionId=@PositionId,\r\n\t\t\t\t\tPositionName=@PositionName,\r\n\t\t\t\t\tDataRegion=@DataRegion,\r\n\t\t\t\t\tDeptEmployeeType=@DeptEmployeeType,\r\n\t\t\t\t\tOrderID=@OrderID,\r\n\t\t\t\t\tIsDefault=@IsDefault\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, DateTime.Now);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptID", DbType.String, model.DeptID);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptName", DbType.String, model.DeptName);
			SysDatabase.AddInParameter(sqlStringCommand, "CompanyID", DbType.String, model.CompanyID);
			SysDatabase.AddInParameter(sqlStringCommand, "EmployeeID", DbType.String, model.EmployeeID);
			SysDatabase.AddInParameter(sqlStringCommand, "EmployeeName", DbType.String, model.EmployeeName);
			SysDatabase.AddInParameter(sqlStringCommand, "PositionId", DbType.String, model.PositionId);
			SysDatabase.AddInParameter(sqlStringCommand, "PositionName", DbType.String, model.PositionName);
			SysDatabase.AddInParameter(sqlStringCommand, "DataRegion", DbType.Int32, model.DataRegion);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptEmployeeType", DbType.Int32, model.DeptEmployeeType);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderID", DbType.Int32, model.OrderID);
			SysDatabase.AddInParameter(sqlStringCommand, "IsDefault", DbType.Int32, model.IsDefault);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}