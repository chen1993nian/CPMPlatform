using EIS.DataAccess;
using EIS.Permission.Access;
using EIS.Permission.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

namespace EIS.Permission.Service
{
	public class DeptEmployeeService
	{
		public DeptEmployeeService()
		{
		}

		public static DeptEmployee GetDeptEmployee(string empId, string deptId)
		{
			DeptEmployee item;
			_DeptEmployee __DeptEmployee = new _DeptEmployee();
			List<DeptEmployee> modelList = __DeptEmployee.GetModelList(string.Format(" employeeId='{0}' and deptId='{1}' ", empId, deptId));
			if (modelList.Count <= 0)
			{
				item = null;
			}
			else
			{
				item = modelList[0];
			}
			return item;
		}

		public static List<DeptEmployee> GetDeptEmployeeByEmployeeId(string empId)
		{
			_DeptEmployee __DeptEmployee = new _DeptEmployee();
			return __DeptEmployee.GetModelList(string.Concat("EmployeeId='", empId, "' and _isDel=0"));
		}

		public static DeptEmployee GetDeptEmployeeById(string deptEmployeeId)
		{
			return (new _DeptEmployee()).GetModel(deptEmployeeId);
		}

		public static List<DeptEmployee> GetDeptEmployeeByPositionId(string positionId)
		{
			_DeptEmployee __DeptEmployee = new _DeptEmployee();
			return __DeptEmployee.GetModelList(string.Concat("positionId='", positionId, "' and _isDel=0"));
		}

		public static DeptEmployee GetDeptEmployeeByPositionId(string empId, string posId)
		{
			DeptEmployee item;
			_DeptEmployee __DeptEmployee = new _DeptEmployee();
			List<DeptEmployee> modelList = __DeptEmployee.GetModelList(string.Format(" employeeId='{0}' and positionId='{1}' and _isDel=0 ", empId, posId));
			if (modelList.Count <= 0)
			{
				item = null;
			}
			else
			{
				item = modelList[0];
			}
			return item;
		}

		public static StringCollection GetDeptList(string empId)
		{
			StringCollection stringCollections = new StringCollection();
			_DeptEmployee __DeptEmployee = new _DeptEmployee();
			DataTable list = __DeptEmployee.GetList(string.Concat("EmployeeId='", empId, "' and _isDel=0"));
			foreach (DataRow row in list.Rows)
			{
				stringCollections.Add(row["DeptId"].ToString());
			}
			return stringCollections;
		}

		public static DeptEmployee GetOrignalDeptEmployee(string empId)
		{
			DeptEmployee item;
			_DeptEmployee __DeptEmployee = new _DeptEmployee();
			List<DeptEmployee> modelList = __DeptEmployee.GetModelList(string.Format(" employeeId='{0}' and DeptEmployeeType=0 ", empId));
			if (modelList.Count <= 0)
			{
				item = null;
			}
			else
			{
				item = modelList[0];
			}
			return item;
		}

		public static StringCollection GetPositionList(string empId)
		{
			StringCollection stringCollections = new StringCollection();
			_DeptEmployee __DeptEmployee = new _DeptEmployee();
			DataTable list = __DeptEmployee.GetList(string.Concat("EmployeeId='", empId, "' and _isDel=0"));
			foreach (DataRow row in list.Rows)
			{
				stringCollections.Add(row["PositionId"].ToString());
			}
			return stringCollections;
		}

		public static void UpdateDefaultPosition(string empId, string positionId)
		{
			string str = string.Format("update T_E_Org_DeptEmployee set IsDefault = 0 where employeeId='{0}';\r\n                update T_E_Org_DeptEmployee set IsDefault =1 where employeeId='{0}' and PositionId='{1}'", empId, positionId);
			SysDatabase.ExecuteNonQuery(str);
		}
	}
}