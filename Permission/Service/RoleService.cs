using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Access;
using EIS.Permission.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;

namespace EIS.Permission.Service
{
	public class RoleService
	{
		public RoleService()
		{
		}

		public static int AddRoleRelation(string roleCode, string empId, UserContext userInfo)
		{
			int num;
			if (RoleService.IsRole(roleCode, empId))
			{
				num = 0;
			}
			else
			{
				string str = string.Format("select top 1 _AutoId from T_E_Org_Role where RoleCode='{0}'", roleCode);
				string str1 = SysDatabase.ExecuteScalar(str).ToString();
				_RoleEmployee __RoleEmployee = new _RoleEmployee();
				RoleEmployee roleEmployee = new RoleEmployee(userInfo)
				{
					RoleID = str1,
					EmployeeID = empId,
					RoleEmployeeType = 0
				};
				num = __RoleEmployee.Add(roleEmployee);
			}
			return num;
		}

		public static string GetJsonRoleByCatId(string catId)
		{
			List<Role> modelListByCatId = (new _Role()).GetModelListByCatId(catId);
			ArrayList arrayLists = new ArrayList();
			foreach (Role role in modelListByCatId)
			{
				TreeItem treeItem = new TreeItem()
				{
					id = role._AutoID,
					text = role.RoleName,
					@value = role.CatalogID
				};
				arrayLists.Add(treeItem);
			}
			return (new JavaScriptSerializer()).Serialize(arrayLists);
		}

		public static Role GetModelById(string roleId)
		{
			return (new _Role()).GetModel(roleId);
		}

		public static string GetRoleId(string roleCode)
		{
			string str = string.Concat("select _AutoId from T_E_Org_Role where RoleCode ='", roleCode, "'");
			object obj = SysDatabase.ExecuteScalar(str);
			return ((obj == DBNull.Value ? true : obj == null) ? "" : obj.ToString());
		}

		public static string GetRoleName(string roleId)
		{
			string str = string.Concat("select RoleName from T_E_Org_Role where _AutoId ='", roleId, "'");
			object obj = SysDatabase.ExecuteScalar(str);
			return ((obj == DBNull.Value ? true : obj == null) ? "" : obj.ToString());
		}

		public static bool IsRole(string roleCode, string empId)
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select count(*) from T_E_Org_RoleEmployee where EmployeeID='{0}' and RoleID in \r\n                (select _AutoID from T_E_Org_Role where RoleCode='{1}')", empId, roleCode);
			object obj = SysDatabase.ExecuteScalar(stringBuilder.ToString());
			if (obj != null)
			{
				flag = (obj == DBNull.Value ? false : Convert.ToInt32(obj) > 0);
			}
			else
			{
				flag = false;
			}
			if (!flag)
			{
				DataTable dataTable = SysDatabase.ExecuteTable(string.Format("select RoleSQL from dbo.T_E_Org_Role where RoleType='动态' and IsNull(RoleSQL,'')<>'' and RoleCode='{0}'", roleCode));
				if (dataTable.Rows.Count > 0)
				{
					string str = dataTable.Rows[0]["RoleSQL"].ToString();
					string str1 = string.Format("select count(*) where '{0}' in ({1})", empId, str);
					string str2 = SysDatabase.ExecuteScalar(str1).ToString();
					flag = str2 != "0";
				}
			}
			return flag;
		}

		public static void RemoveRole(string roleId)
		{
			_Role __Role = new _Role();
			if (__Role.GetPositionCountByRoleId(roleId) > 0)
			{
				throw new Exception("该角色存在关联岗位，不能删除");
			}
			if (__Role.GetEmployeeCountByRoleId(roleId) > 0)
			{
				throw new Exception("该角色存在关联员工，不能删除");
			}
			__Role.Delete(roleId);
		}

		public static void RemoveRoleRelation(string roleCode, string empId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("delete from T_E_Org_RoleEmployee where EmployeeID='{0}' and RoleID in \r\n                (select _AutoID from T_E_Org_Role where RoleCode='{1}')", empId, roleCode);
			SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
		}
	}
}