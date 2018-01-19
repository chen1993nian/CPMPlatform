using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Access;
using EIS.Permission.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace EIS.Permission.Service
{
	public class RoleEmployeeService
	{
		public RoleEmployeeService()
		{
		}

		public static List<RoleEmployee> GetRoleEmployeeList(string roleId)
		{
			_RoleEmployee __RoleEmployee = new _RoleEmployee();
			List<RoleEmployee> modelList = __RoleEmployee.GetModelList(string.Concat("RoleId='", roleId, "'"));
			DataTable dataTable = SysDatabase.ExecuteTable(string.Format("select RoleSQL from dbo.T_E_Org_Role where RoleType='动态' and IsNull(RoleSQL,'')<>'' and _AutoID='{0}'", roleId));
			if (dataTable.Rows.Count > 0)
			{
				string str = dataTable.Rows[0]["RoleSQL"].ToString();
				foreach (DataRow row in SysDatabase.ExecuteTable(str).Rows)
				{
					string str1 = row[0].ToString();
					if (modelList.FindIndex((RoleEmployee roleEmployee_0) => roleEmployee_0.EmployeeID == str1) != -1)
					{
						continue;
					}
					RoleEmployee roleEmployee = new RoleEmployee()
					{
						EmployeeID = str1,
						RoleID = roleId,
						RoleEmployeeType = 0
					};
					modelList.Add(roleEmployee);
				}
			}
			return modelList;
		}

		public static bool IsEmployeeHasAllRight(string empId)
		{
			string str = string.Format("select count(*) from t_e_org_roleEmployee re where re.EmployeeID='{0}' \r\n                and re.RoleID='allright'", empId);
			return Convert.ToInt32(SysDatabase.ExecuteScalar(str)) > 0;
		}

		public static void SaveEmployeeRoleSet(IList roleList, string empId)
		{
			DbConnection dbConnection = SysDatabase.CreateConnection();
			dbConnection.Open();
			DbTransaction dbTransaction = dbConnection.BeginTransaction();
			try
			{
				try
				{
					string str = "";
					str = string.Format("delete T_E_Org_RoleEmployee where employeeId='{0}'", empId);
					SysDatabase.ExecuteNonQuery(str, dbTransaction);
					_RoleEmployee __RoleEmployee = new _RoleEmployee(dbTransaction);
					foreach (object obj in roleList)
					{
						RoleEmployee roleEmployee = new RoleEmployee()
						{
							_AutoID = Guid.NewGuid().ToString(),
							_OrgCode = "",
							_UserName = "",
							_IsDel = 0
						};
						DateTime now = DateTime.Now;
						DateTime dateTime = now;
						roleEmployee._UpdateTime = now;
						roleEmployee._CreateTime = dateTime;
						roleEmployee.EmployeeID = empId;
						roleEmployee.RoleEmployeeType = 0;
						roleEmployee.RoleID = obj.ToString();
						__RoleEmployee.Add(roleEmployee);
					}
					dbTransaction.Commit();
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					dbTransaction.Rollback();
					throw exception;
				}
			}
			finally
			{
				dbConnection.Close();
			}
		}

		public static void SaveRoleEmployeeSet(IList empList, string roleId)
		{
			DbConnection dbConnection = SysDatabase.CreateConnection();
			dbConnection.Open();
			DbTransaction dbTransaction = dbConnection.BeginTransaction();
			try
			{
				try
				{
					string str = "";
					str = string.Format("delete T_E_Org_RoleEmployee where roleId='{0}'", roleId);
					SysDatabase.ExecuteNonQuery(str, dbTransaction);
					_RoleEmployee __RoleEmployee = new _RoleEmployee(dbTransaction);
					foreach (object obj in empList)
					{
						if (__RoleEmployee.IsExist(roleId, obj.ToString(), dbTransaction))
						{
							continue;
						}
						RoleEmployee roleEmployee = new RoleEmployee()
						{
							_AutoID = Guid.NewGuid().ToString(),
							_OrgCode = "",
							_UserName = "",
							_IsDel = 0
						};
						DateTime now = DateTime.Now;
						DateTime dateTime = now;
						roleEmployee._UpdateTime = now;
						roleEmployee._CreateTime = dateTime;
						roleEmployee.EmployeeID = obj.ToString();
						roleEmployee.RoleEmployeeType = 0;
						roleEmployee.RoleID = roleId;
						__RoleEmployee.Add(roleEmployee);
					}
					dbTransaction.Commit();
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					dbTransaction.Rollback();
					throw exception;
				}
			}
			finally
			{
				dbConnection.Close();
			}
		}
	}
}