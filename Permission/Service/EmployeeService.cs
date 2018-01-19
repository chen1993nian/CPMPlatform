using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Access;
using EIS.Permission.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.Script.Serialization;

namespace EIS.Permission.Service
{
	public class EmployeeService
	{
		public EmployeeService()
		{
		}

		public static bool CheckedValid(string employeeId)
		{
			bool flag;
			string str = string.Concat("select count(*) from T_E_Org_Employee  where IsNull(isLocked,'') <> '是' and _isdel=0 and  _AutoId='", employeeId, "'");
			object obj = SysDatabase.ExecuteScalar(str);
			flag = ((obj == DBNull.Value ? false : obj != null) ? Convert.ToInt32(obj) > 0 : false);
			return flag;
		}

		public static bool DeleteEmployee(string employeeId)
		{
			DbConnection dbConnection = SysDatabase.CreateConnection();
			dbConnection.Open();
			DbTransaction dbTransaction = dbConnection.BeginTransaction();
			try
			{
				try
				{
					(new _Employee(dbTransaction)).Delete(employeeId);
					_DeptEmployee __DeptEmployee = new _DeptEmployee(dbTransaction);
					foreach (DeptEmployee modelList in __DeptEmployee.GetModelList(string.Concat("EmployeeId='", employeeId, "' and _IsDel=0")))
					{
						__DeptEmployee.Delete(modelList._AutoID);
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
				if (dbConnection.State == ConnectionState.Open)
				{
					dbConnection.Close();
				}
			}
			return true;
		}

		public static Department GetDefaultDeptById(string employeeId)
		{
			Department model;
			_DeptEmployee __DeptEmployee = new _DeptEmployee();
			List<DeptEmployee> modelList = __DeptEmployee.GetModelList(string.Concat("EmployeeId='", employeeId, "' and _IsDel=0"));
			_Department __Department = new _Department();
			foreach (DeptEmployee deptEmployee in modelList)
			{
				if (deptEmployee.DeptEmployeeType != 0)
				{
					continue;
				}
				model = __Department.GetModel(deptEmployee.DeptID);
				return model;
			}
			model = null;
			return model;
		}

		public static Position GetDefaultPositionById(string employeeId)
		{
			DeptEmployee deptEmployee = null;
			Position model;
			_DeptEmployee __DeptEmployee = new _DeptEmployee();
			List<DeptEmployee> modelList = __DeptEmployee.GetModelList(string.Concat("EmployeeId='", employeeId, "' and _IsDel=0 and isnull(PositionId,'')<>''"));
			_Position __Position = new _Position();
			foreach (DeptEmployee deptEmployee1 in modelList)
			{
                if (deptEmployee1.IsDefault != 1)
				{
					continue;
				}
                model = __Position.GetModel(deptEmployee1.PositionId);
				return model;
			}
			foreach (DeptEmployee deptEmployee1 in modelList)
			{
				if (deptEmployee1.DeptEmployeeType != 0)
				{
					continue;
				}
				model = __Position.GetModel(deptEmployee1.PositionId);
				return model;
			}
			model = null;
			return model;
		}

		public static List<Department> GetDeptsByEmployeeId(string employeeId)
		{
			_DeptEmployee __DeptEmployee = new _DeptEmployee();
			List<DeptEmployee> modelList = __DeptEmployee.GetModelList(string.Concat("EmployeeId='", employeeId, "' and _IsDel=0"));
			List<Department> departments = new List<Department>();
			_Department __Department = new _Department();
			foreach (DeptEmployee deptEmployee in modelList)
			{
				departments.Add(__Department.GetModel(deptEmployee.DeptID));
			}
			return departments;
		}

		public static string GetEmployeeAttrByCode(string empCode, string fldName)
		{
			string[] strArrays = new string[] { "select isnull(", fldName, ",'') from T_E_Org_Employee where EmployeeCode ='", empCode, "'" };
			object obj = SysDatabase.ExecuteScalar(string.Concat(strArrays));
			return ((obj == DBNull.Value ? true : obj == null) ? "" : obj.ToString());
		}

		public static string GetEmployeeAttrById(string employeeId, string fldName)
		{
			string[] strArrays = new string[] { "select isnull(", fldName, ",'') from T_E_Org_Employee where _AutoId ='", employeeId, "'" };
			object obj = SysDatabase.ExecuteScalar(string.Concat(strArrays));
			return ((obj == DBNull.Value ? true : obj == null) ? "" : obj.ToString());
		}

		public static string GetEmployeeName(string employeeId)
		{
			string str = string.Concat("select EmployeeName from T_E_Org_Employee where _AutoId ='", employeeId, "'");
			object obj = SysDatabase.ExecuteScalar(str);
			return ((obj == DBNull.Value ? true : obj == null) ? "" : obj.ToString());
		}

		public static string GetEmployeeNameList(IList empIdList)
		{
			string str;
			if (empIdList.Count != 0)
			{
				string splitQuoteString = EIS.AppBase.Utility.GetSplitQuoteString(empIdList);
				string str1 = string.Concat("select EmployeeName from T_E_Org_Employee where _AutoId  in (", splitQuoteString, ")");
				StringBuilder stringBuilder = new StringBuilder();
				foreach (DataRow row in SysDatabase.ExecuteTable(str1).Rows)
				{
					stringBuilder.AppendFormat("{0},", row["EmployeeName"]);
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Length = stringBuilder.Length - 1;
				}
				str = stringBuilder.ToString();
			}
			else
			{
				str = "";
			}
			return str;
		}

		public static string GetJsonEmployeeByDeptId(string deptId)
		{
			List<Employee> employeeByDeptId = (new _Employee()).GetEmployeeByDeptId(deptId);
			ArrayList arrayLists = new ArrayList();
			foreach (Employee employee in employeeByDeptId)
			{
				TreeItem treeItem = new TreeItem()
				{
					id = employee._AutoID,
					text = string.Concat(employee.EmployeeName.Replace(" ", ""), " (", employee.PositionName, ")"),
					@value = employee.PositionId
				};
				arrayLists.Add(treeItem);
			}
			return (new JavaScriptSerializer()).Serialize(arrayLists);
		}

		public static int GetLastMsgCount(string employeeId)
		{
			string str = string.Concat("select isnull(LastMsgCount,0) from T_E_Org_Employee  where _AutoId='", employeeId, "'");
			object obj = SysDatabase.ExecuteScalar(str);
			return ((obj == DBNull.Value ? false : obj != null) ? Convert.ToInt32(obj) : 0);
		}

		public static int GetLoginCount(string employeeId)
		{
			string str = string.Concat("select isnull(LoginCount,0) from T_E_Org_Employee where _AutoId='", employeeId, "'");
			object obj = SysDatabase.ExecuteScalar(str);
			return ((obj == DBNull.Value ? false : obj != null) ? Convert.ToInt32(obj) : 0);
		}

		public static string GetMail(string employeeId)
		{
			string str = string.Concat("select isnull(EMail,'') from T_E_Org_Employee where _AutoId ='", employeeId, "'");
			object obj = SysDatabase.ExecuteScalar(str);
			return ((obj == DBNull.Value ? true : obj == null) ? "" : obj.ToString());
		}

		public static string GetMobilePhone(string employeeId)
		{
			string str = string.Concat("select isnull(CellPhone,'') from T_E_Org_Employee where _AutoId ='", employeeId, "'");
			object obj = SysDatabase.ExecuteScalar(str);
			return ((obj == DBNull.Value ? true : obj == null) ? "" : obj.ToString());
		}

		public static string GetMobilePhoneList(IList empIdList)
		{
			string str;
			if (empIdList.Count != 0)
			{
				string splitQuoteString = EIS.AppBase.Utility.GetSplitQuoteString(empIdList);
				string str1 = string.Concat("select isnull(CellPhone,'') from T_E_Org_Employee where _AutoId  in (", splitQuoteString, ")");
				StringBuilder stringBuilder = new StringBuilder();
				foreach (DataRow row in SysDatabase.ExecuteTable(str1).Rows)
				{
					stringBuilder.AppendFormat("{0},", row[0]);
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Length = stringBuilder.Length - 1;
				}
				str = stringBuilder.ToString();
			}
			else
			{
				str = "";
			}
			return str;
		}

		public static Employee GetModel(string employeeId)
		{
			return (new _Employee()).GetModel(employeeId);
		}

		public static Employee GetModelByLoginName(string loginName)
		{
			return (new _Employee()).GetModelByLoginName(loginName);
		}

		public static int GetOnlineCount(int interval)
		{
			string str = string.Concat("select isnull(count(*), 0) as online from T_E_Org_Employee where  datediff(s, RefreshTime, getdate()) < ", interval.ToString());
			return Convert.ToInt32(SysDatabase.ExecuteScalar(str));
		}

		public static bool IsADUserExist(string LoginName)
		{
			string str = string.Concat("select count(*) from T_E_Org_Employee where userType='AD' and loginName='", LoginName, "'");
			return Convert.ToInt32(SysDatabase.ExecuteScalar(str)) > 0;
		}

		public static bool RemoveEmployee(string employeeId)
		{
			DbConnection dbConnection = SysDatabase.CreateConnection();
			dbConnection.Open();
			DbTransaction dbTransaction = dbConnection.BeginTransaction();
			try
			{
				try
				{
					(new _Employee(dbTransaction)).Remove(employeeId);
					_DeptEmployee __DeptEmployee = new _DeptEmployee(dbTransaction);
					foreach (DeptEmployee modelList in __DeptEmployee.GetModelList(string.Concat("EmployeeId='", employeeId, "' and _IsDel=0")))
					{
						__DeptEmployee.Remove(modelList._AutoID);
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
				if (dbConnection.State == ConnectionState.Open)
				{
					dbConnection.Close();
				}
			}
			return true;
		}

		public static void UpdateLastMsgCount(string employeeId, int msgCount)
		{
			string[] str = new string[] { "update T_E_Org_Employee set LastMsgCount=", msgCount.ToString(), " where _AutoId='", employeeId, "'" };
			SysDatabase.ExecuteNonQuery(string.Concat(str));
		}

		public static void UpdateLoginCount(string employeeId)
		{
			string str = string.Concat("update T_E_Org_Employee set LastLoginTime=getdate(),LoginCount=isnull(LoginCount,0)+1 where _AutoId='", employeeId, "'");
			SysDatabase.ExecuteNonQuery(str);
		}

		public static void UpdateRefreshTime(string employeeId)
		{
			string str = string.Concat("update T_E_Org_Employee set RefreshTime=getdate() where _AutoId='", employeeId, "'");
			SysDatabase.ExecuteNonQuery(str);
		}

        public static void UpdateCellphone(string EmployeeId, string Cellphone, string Officephone, string EMail)
        {
            try
            {
                string sql_save = @"update T_E_Org_Employee  
                    set Cellphone=@Cellphone,Officephone=@Officephone,EMail=@EMail
                    ,_UpdateTime = getdate()
                    where _AutoID=@AutoID  ";
                DbCommand cmmd = SysDatabase.GetSqlStringCommand(sql_save);
                SysDatabase.AddInParameter(cmmd, "@Cellphone", DbType.String, Cellphone);
                SysDatabase.AddInParameter(cmmd, "@Officephone", DbType.String, Officephone);
                SysDatabase.AddInParameter(cmmd, "@EMail", DbType.String, EMail);
                SysDatabase.AddInParameter(cmmd, "@AutoID", DbType.String, EmployeeId);
                SysDatabase.ExecuteNonQuery(cmmd);
            }
            catch { }
            finally { }
        }


	}
}