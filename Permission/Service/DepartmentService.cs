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
using System.Web.Script.Serialization;

namespace EIS.Permission.Service
{
	public class DepartmentService
	{
		private List<Department> list_0 = null;

		public DepartmentService()
		{
		}

		public static List<Department> GetAllCompanyList()
		{
			_Department __Department = new _Department();
			List<Department> modelList = __Department.GetModelList(string.Concat(" TypeID in (select _AutoId from T_E_Org_DeptType where ", string.Format(" TypeProp in ({0},{1},{2})", DeptType.CompanyTypeId, DeptType.GroupTypeId, DeptType.VirtualTypeId), ") order by orderId"));
			return modelList;
		}

		public static Department GetCompanyByDeptId(string deptId)
		{
			return DepartmentService.GetCompanyByDeptId(deptId, null);
		}

		public static Department GetCompanyByDeptId(string deptId, DbTransaction dbTran)
		{
			Department model;
			_Department __Department = new _Department(dbTran);
			string deptWBS = __Department.GetModel(deptId).DeptWBS;
			_DeptType __DeptType = new _DeptType();
			DataTable list = __DeptType.GetList(string.Format(" TypeProp in ({0},{1})", DeptType.CompanyTypeId, DeptType.GroupTypeId));
			DataTable dataTable = __Department.GetList(string.Concat("'", deptWBS, "' like deptwbs+'%' order by deptwbs desc"));
			foreach (DataRow row in dataTable.Rows)
			{
				if ((int)list.Select(string.Concat("_autoid='", row["TypeId"].ToString(), "'")).Length <= 0)
				{
					continue;
				}
				model = __Department.GetModel(row["_autoid"].ToString());
				return model;
			}
			model = null;
			return model;
		}

		public static Department GetCompanyByDeptWbs(string deptWbs)
		{
			Department model;
			_DeptType __DeptType = new _DeptType();
			DataTable list = __DeptType.GetList(string.Format(" TypeProp in ({0},{1})", DeptType.CompanyTypeId, DeptType.GroupTypeId));
			_Department __Department = new _Department();
			DataTable dataTable = __Department.GetList(string.Concat("'", deptWbs, "' like deptwbs+'%' order by deptwbs desc"));
			foreach (DataRow row in dataTable.Rows)
			{
				if ((int)list.Select(string.Concat("_autoid='", row["TypeId"].ToString(), "'")).Length <= 0)
				{
					continue;
				}
				_Department __Department1 = new _Department();
				model = __Department1.GetModel(row["_autoid"].ToString());
				return model;
			}
			model = null;
			return model;
		}

		public static string GetDepartmentName(string deptId)
		{
			string str = string.Concat("select deptName from T_E_Org_Department where _AutoId ='", deptId, "'");
			object obj = SysDatabase.ExecuteScalar(str);
			return ((obj == DBNull.Value ? true : obj == null) ? "" : obj.ToString());
		}

		public static string GetDeptWbsById(string deptId)
		{
			return (new _Department()).GetModel(deptId).DeptWBS;
		}

		public static List<Employee> GetEmployeeByDeptId(string deptId)
		{
			List<Employee> employeeByDeptId = (new _Employee()).GetEmployeeByDeptId(deptId);
			foreach (Employee employee in employeeByDeptId)
			{
				if (employee.DeType != 1)
				{
					continue;
				}
				employee.EmployeeName = string.Concat(employee.EmployeeName, "(兼)");
			}
			return employeeByDeptId;
		}

		public static List<Employee> GetEmployeeByDeptWbs(string deptWbs)
		{
			List<Employee> employeeByDeptWbs = (new _Employee()).GetEmployeeByDeptWbs(deptWbs);
			foreach (Employee employeeByDeptWb in employeeByDeptWbs)
			{
				if (employeeByDeptWb.DeType != 1)
				{
					continue;
				}
				employeeByDeptWb.EmployeeName = string.Concat(employeeByDeptWb.EmployeeName, "(兼)");
			}
			return employeeByDeptWbs;
		}

		public static string GetJsonDeptAndEmployeeByDeptId(string deptId)
		{
			TreeItem treeItem;
			List<Employee> employeeByDeptId = (new _Employee()).GetEmployeeByDeptId(deptId);
			ArrayList arrayLists = new ArrayList();
			foreach (Department sonDeptByWb in (new _Department()).GetSonDeptByWbs(DepartmentService.GetDeptWbsById(deptId)))
			{
				if (sonDeptByWb.DeptState == "停用")
				{
					continue;
				}
				treeItem = new TreeItem()
				{
					id = sonDeptByWb.DeptWBS,
					text = sonDeptByWb.DeptName,
					@value = sonDeptByWb._AutoID,
					complete = false,
					hasChildren = true
				};
				arrayLists.Add(treeItem);
			}
			foreach (Employee employee in employeeByDeptId)
			{
				treeItem = new TreeItem()
				{
					id = employee._AutoID
				};
				string str = (employee.PositionName == "未知" ? "" : string.Concat("(", employee.PositionName, ")"));
				treeItem.text = string.Concat(employee.EmployeeName.Replace(" ", ""), " ", str);
				treeItem.@value = employee.PositionId;
				arrayLists.Add(treeItem);
			}
			return (new JavaScriptSerializer()).Serialize(arrayLists);
		}

		public static int GetMaxOrder(string deptPWBS)
		{
			return (new _Department()).GetMaxOrder(deptPWBS);
		}

		public static Department GetModel(string deptId)
		{
			return (new _Department()).GetModel(deptId);
		}

		public static Department GetModelByWbs(string deptWbs)
		{
			return (new _Department()).GetModelByWbs(deptWbs);
		}

		public static string GetNewDeptWbs(string deptPWBS)
		{
			return (new _Department()).GetNewCode(deptPWBS);
		}

		public static Department GetParentOrgByDeptId(string deptId, string typeName)
		{
			Department model;
			string deptWbsById = DepartmentService.GetDeptWbsById(deptId);
			_DeptType __DeptType = new _DeptType();
			DataTable list = __DeptType.GetList(string.Format(" TypeName = '{0}'", typeName));
			_Department __Department = new _Department();
			DataTable dataTable = __Department.GetList(string.Concat("'", deptWbsById, "' like deptwbs+'%' order by deptwbs desc"));
			foreach (DataRow row in dataTable.Rows)
			{
				if ((int)list.Select(string.Concat("_autoid='", row["TypeId"].ToString(), "'")).Length <= 0)
				{
					continue;
				}
				_Department __Department1 = new _Department();
				model = __Department1.GetModel(row["_autoid"].ToString());
				return model;
			}
			model = null;
			return model;
		}

		public static Department GetTopDept()
		{
			return (new _Department()).GetModelByWbs("0");
		}

		private void method_0(string string_0, string string_1, _Department _Department_0)
		{
			foreach (Department string1 in this.list_0.FindAll((Department department_0) => department_0.DeptPWBS == string_0))
			{
				string deptWBS = string1.DeptWBS;
				string1.DeptPWBS = string_1;
				string1.DeptWBS = _Department_0.GetNewCode(string_1);
				_Department_0.Update(string1);
				this.method_0(deptWBS, string1.DeptWBS, _Department_0);
			}
		}

		public static bool RemoveDepartment(string deptId)
		{
			_Department __Department = new _Department();
			if (__Department.GetSubDeptCount(__Department.GetModel(deptId).DeptWBS) > 1)
			{
				throw new Exception("该组织下面存在部门，不能删除");
			}
			if ((new _Position()).GetPositionCountByDeptId(deptId) > 0)
			{
				throw new Exception("该组织下面存在岗位，不能删除");
			}
			if ((new _DeptEmployee()).GetEmployeeCountByDeptId(deptId) > 0)
			{
				throw new Exception("该组织下面存在员工，不能删除");
			}
			return __Department.Delete(deptId) > 0;
		}

		public static void UpdateDeptCompany(string deptPWBS, DbTransaction dbTran)
		{
			try
			{
				_Department __Department = new _Department(dbTran);
				foreach (Department sonDeptByWb in __Department.GetSonDeptByWbs(deptPWBS))
				{
					string companyByDeptId = DepartmentService.GetCompanyByDeptId(sonDeptByWb._AutoID, dbTran)._AutoID;
					__Department.Update(sonDeptByWb);
					SysDatabase.ExecuteNonQuery(string.Format("\r\n                        update T_E_ORG_Department set companyId='{1}' where _AutoId='{0}';\r\n                        update T_E_ORG_DeptEmployee set companyId='{1}' where deptId='{0}'", sonDeptByWb._AutoID, companyByDeptId), dbTran);
					DepartmentService.UpdateDeptCompany(sonDeptByWb.DeptWBS, dbTran);
				}
			}
			catch (Exception exception)
			{
				throw new Exception(string.Concat("更新部门所属公司的属性时发生异常：", exception.Message));
			}
		}

		public bool UpdateParent(string OldWbs, string NewWbs, DbTransaction dbTran)
		{
			try
			{
				_Department __Department = new _Department(dbTran);
				this.list_0 = __Department.GetSubDeptByWbs(OldWbs);
				this.method_0(OldWbs, NewWbs, __Department);
			}
			catch (Exception exception)
			{
				throw new Exception(string.Concat("在变更父级组织时发生异常：", exception.Message));
			}
			return true;
		}
	}
}