using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections;
using System.Data;
using System.Text;

namespace EIS.Permission
{
	public class Utility
	{
		public Utility()
		{
		}

		public static void ChangePass(string employeeId, string newPass)
		{
			_Employee __Employee = new _Employee();
			Employee model = __Employee.GetModel(employeeId);
			if (model == null)
			{
				throw new Exception("用户不存在!");
			}
			if (model.IsLocked == "是")
			{
				throw new Exception("用户被锁定，不能修改密码!");
			}
			model.LoginPass = Security.Encrypt(newPass);
			__Employee.ChangePass(employeeId, model.LoginPass);
		}

		public static DataTable GetAllLimitDataByEmployeeId(string EmployeeId, string webId)
		{
			DataTable dataTable;
			DataRow row = null;
			string str;
			string str1;
			DataRow dataRow;
			string str2;
			DataTable dataTable1 = SysDatabase.ExecuteTable(string.Format("\r\n                    select _AutoID, FunName, LinkFile, LinkType, DispState, DispStyle, FunPWBS, FunWBS, OrderId, WebID ,'0000000000' Limit  FROM T_E_Sys_FunNode \r\n                    where DispState='是' and webId='{0}' ORDER BY FunPWBS, OrderId", webId));
			if (!RoleEmployeeService.IsEmployeeHasAllRight(EmployeeId))
			{
				string str3 = string.Format("select FunID,Limit from dbo.T_E_Org_PositionLimit where PositionId in (select PositionId from dbo.T_E_Org_DeptEmployee where EmployeeId='{0}' and _IsDel=0)", EmployeeId);
				foreach (DataRow rowa in SysDatabase.ExecuteTable(str3).Rows)
				{
                    str = rowa["FunID"].ToString();
                    str1 = rowa["Limit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format("select FunID,FunLimit from dbo.T_E_Org_RoleLimit where roleid in (select RoleId from dbo.T_E_Org_RoleEmployee where EmployeeId='{0}')", EmployeeId);
				foreach (DataRow row1 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = row1["FunID"].ToString();
					str1 = row1["FunLimit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format("select FunID,FunLimit from dbo.T_E_Org_RoleLimit where  roleId in (select p.propId from T_E_Org_Position p inner join T_E_Org_DeptEmployee de on p._AutoID = de.positionId where de.EmployeeId='{0}' and de._IsDel=0 )", EmployeeId);
				foreach (DataRow dataRow1 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = dataRow1["FunID"].ToString();
					str1 = dataRow1["FunLimit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format("select _AutoID roleId,RoleSQL from dbo.T_E_Org_Role where RoleType='动态' and IsNull(RoleSQL,'')<>''", new object[0]);
				foreach (DataRow row2 in SysDatabase.ExecuteTable(str3).Rows)
				{
					string str4 = row2["RoleId"].ToString();
					if (SysDatabase.ExecuteScalar(string.Format("select count(*) where '{0}' in ({1})", EmployeeId, row2["RoleSQL"].ToString())).ToString() == "0")
					{
						continue;
					}
					str3 = string.Format("select FunID,FunLimit from dbo.T_E_Org_RoleLimit where roleId ='{0}'", str4);
					foreach (DataRow dataRow2 in SysDatabase.ExecuteTable(str3).Rows)
					{
						str = dataRow2["FunID"].ToString();
						str1 = dataRow2["FunLimit"].ToString();
						if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
						{
							continue;
						}
						dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
						str2 = dataRow["Limit"].ToString();
						dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
					}
				}
				str3 = string.Format("select FunID,Limit from dbo.T_E_Org_DeptLimit where DeptID in (select DeptID from dbo.T_E_Org_DeptEmployee where EmployeeId='{0}')", EmployeeId);
				foreach (DataRow row3 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = row3["FunID"].ToString();
					str1 = row3["Limit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format("select FunID,Limit from dbo.T_E_Org_EmployeeLimit where EmployeeId='{0}' or EmployeeId='everyone'", EmployeeId);
				foreach (DataRow dataRow3 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = dataRow3["FunID"].ToString();
					str1 = dataRow3["Limit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format("select FunID,Limit from dbo.T_E_Org_ExcludeLimit where EmployeeId='{0}' or EmployeeId='everyone'", EmployeeId);
				foreach (DataRow row4 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = row4["FunID"].ToString();
					str1 = row4["Limit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_1(str1, str2);
				}
				dataTable = dataTable1;
			}
			else
			{
				dataTable1 = SysDatabase.ExecuteTable(string.Format("select * ,'1111111111' Limit  FROM T_E_Sys_FunNode where DispState='是' and webId='{0}' ORDER BY FunPWBS, OrderId", webId));
				dataTable = dataTable1;
			}
			return dataTable;
		}

		public static DataTable GetAllLimitDataByEmployeeId(string EmployeeId, string pwbs, string webId)
		{
			DataTable dataTable;
			DataRow row = null;
			string str;
			string str1;
			DataRow dataRow;
			string str2;
			string str3 = string.Format("select * ,'0000000000' Limit  FROM T_E_Sys_FunNode where DispState='是' and webId='{1}' and FunWbs like '{0}%' ORDER BY FunPwbs, OrderId", pwbs, webId);
			DataTable dataTable1 = SysDatabase.ExecuteTable(str3);
			if (!RoleEmployeeService.IsEmployeeHasAllRight(EmployeeId))
			{
				str3 = string.Format(string.Concat("select FunID,Limit from dbo.T_E_Org_PositionLimit where FunWbs like '", pwbs, "%' and PositionId in (select PositionId from dbo.T_E_Org_DeptEmployee where EmployeeId='{0}' and _IsDel=0)"), EmployeeId);
				foreach (DataRow rowa in SysDatabase.ExecuteTable(str3).Rows)
				{
                    str = rowa["FunID"].ToString();
                    str1 = rowa["Limit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format(string.Concat("select FunID,FunLimit from dbo.T_E_Org_RoleLimit where FunWbs like '", pwbs, "%' and roleid in (select RoleId from dbo.T_E_Org_RoleEmployee where EmployeeId='{0}')"), EmployeeId);
				foreach (DataRow row1 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = row1["FunID"].ToString();
					str1 = row1["FunLimit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format(string.Concat("select FunID,FunLimit from dbo.T_E_Org_RoleLimit where FunWbs like '", pwbs, "%' and roleId in (select p.propId from T_E_Org_Position p inner join T_E_Org_DeptEmployee de on p._AutoID = de.positionId where de.EmployeeId='{0}' and de._IsDel=0 )"), EmployeeId);
				foreach (DataRow dataRow1 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = dataRow1["FunID"].ToString();
					str1 = dataRow1["FunLimit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format("select _AutoID roleId,RoleSQL from dbo.T_E_Org_Role where RoleType='动态' and IsNull(RoleSQL,'')<>''", new object[0]);
				foreach (DataRow row2 in SysDatabase.ExecuteTable(str3).Rows)
				{
					string str4 = row2["RoleId"].ToString();
					if (SysDatabase.ExecuteScalar(string.Format("select count(*) where '{0}' in ({1})", EmployeeId, row2["RoleSQL"].ToString())).ToString() == "0")
					{
						continue;
					}
					str3 = string.Format(string.Concat("select FunID,FunLimit from dbo.T_E_Org_RoleLimit where FunWbs like '", pwbs, "%' and roleId ='{0}'"), str4);
					foreach (DataRow dataRow2 in SysDatabase.ExecuteTable(str3).Rows)
					{
						str = dataRow2["FunID"].ToString();
						str1 = dataRow2["FunLimit"].ToString();
						if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
						{
							continue;
						}
						dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
						str2 = dataRow["Limit"].ToString();
						dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
					}
				}
				str3 = string.Format(string.Concat("select FunID,Limit from dbo.T_E_Org_DeptLimit where FunWbs like '", pwbs, "%' and DeptID in (select DeptID from dbo.T_E_Org_DeptEmployee where EmployeeId='{0}')"), EmployeeId);
				foreach (DataRow row3 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = row3["FunID"].ToString();
					str1 = row3["Limit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format(string.Concat("select FunID,Limit from dbo.T_E_Org_EmployeeLimit where FunWbs like '", pwbs, "%' and (EmployeeId='{0}' or EmployeeId='everyone')"), EmployeeId);
				foreach (DataRow dataRow3 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = dataRow3["FunID"].ToString();
					str1 = dataRow3["Limit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format(string.Concat("select FunID,Limit from dbo.T_E_Org_ExcludeLimit where FunWbs like '", pwbs, "%' and (EmployeeId='{0}' or EmployeeId='everyone')"), EmployeeId);
				foreach (DataRow row4 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = row4["FunID"].ToString();
					str1 = row4["Limit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_1(str1, str2);
				}
				dataTable = dataTable1;
			}
			else
			{
				str3 = string.Format("select * ,'1111111111' Limit  FROM T_E_Sys_FunNode where DispState='是' and webId='{1}' and FunWbs like '{0}%' ORDER BY FunPwbs, OrderId", pwbs, webId);
				dataTable1 = SysDatabase.ExecuteTable(str3);
				dataTable = dataTable1;
			}
			return dataTable;
		}

		public static DataTable GetAllowedFunNode(string EmployeeId, string pwbs, string webId)
		{
			DataTable dataTable;
			string str;
			string str1;
			DataRow dataRow;
			string str2;
			string str3 = string.Format("select * ,'0000000000' Limit  FROM T_E_Sys_FunNode where DispState='是' and FunWbs like '{0}%' and webId='{1}' ORDER BY FunPWBS, OrderId", pwbs, webId);
			DataTable dataTable1 = SysDatabase.ExecuteTable(str3);
			if (!RoleEmployeeService.IsEmployeeHasAllRight(EmployeeId))
			{
				str3 = string.Format(string.Concat("select FunID,Limit from dbo.T_E_Org_PositionLimit where FunWbs like '", pwbs, "%' and PositionId in (select PositionId from dbo.T_E_Org_DeptEmployee where EmployeeId='{0}' and _IsDel=0)"), EmployeeId);
				foreach (DataRow rowa in SysDatabase.ExecuteTable(str3).Rows)
				{
                    str = rowa["FunID"].ToString();
                    str1 = rowa["Limit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format(string.Concat("select FunID,FunLimit from dbo.T_E_Org_RoleLimit where FunWbs like '", pwbs, "%' and roleid in (select RoleId from dbo.T_E_Org_RoleEmployee where EmployeeId='{0}')"), EmployeeId);
				foreach (DataRow row1 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = row1["FunID"].ToString();
					str1 = row1["FunLimit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format(string.Concat("select FunID,FunLimit from dbo.T_E_Org_RoleLimit where FunWbs like '", pwbs, "%' and roleId in (select p.propId from T_E_Org_Position p inner join T_E_Org_DeptEmployee de on p._AutoID = de.positionId where de.EmployeeId='{0}' and de._IsDel=0 )"), EmployeeId);
				foreach (DataRow dataRow1 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = dataRow1["FunID"].ToString();
					str1 = dataRow1["FunLimit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format("select _AutoID roleId,RoleSQL from dbo.T_E_Org_Role where RoleType='动态' and IsNull(RoleSQL,'')<>''", new object[0]);
				foreach (DataRow row2 in SysDatabase.ExecuteTable(str3).Rows)
				{
					string str4 = row2["RoleId"].ToString();
					if (SysDatabase.ExecuteScalar(string.Format("select count(*) where '{0}' in ({1})", EmployeeId, row2["RoleSQL"].ToString())).ToString() == "0")
					{
						continue;
					}
					str3 = string.Format(string.Concat("select FunID,FunLimit from dbo.T_E_Org_RoleLimit where FunWbs like '", pwbs, "%' and roleId ='{0}'"), str4);
					foreach (DataRow dataRow2 in SysDatabase.ExecuteTable(str3).Rows)
					{
						str = dataRow2["FunID"].ToString();
						str1 = dataRow2["FunLimit"].ToString();
						if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
						{
							continue;
						}
						dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
						str2 = dataRow["Limit"].ToString();
						dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
					}
				}
				str3 = string.Format(string.Concat("select FunID,Limit from dbo.T_E_Org_DeptLimit where FunWbs like '", pwbs, "%' and DeptID in (select DeptID from dbo.T_E_Org_DeptEmployee where EmployeeId='{0}')"), EmployeeId);
				foreach (DataRow row3 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = row3["FunID"].ToString();
					str1 = row3["Limit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format(string.Concat("select FunID,Limit from dbo.T_E_Org_EmployeeLimit where FunWbs like '", pwbs, "%' and (EmployeeId='{0}' or EmployeeId='everyone')"), EmployeeId);
				foreach (DataRow dataRow3 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = dataRow3["FunID"].ToString();
					str1 = dataRow3["Limit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format(string.Concat("select FunID,Limit from dbo.T_E_Org_ExcludeLimit where FunWbs like '", pwbs, "%' and  (EmployeeId='{0}' or EmployeeId='everyone')"), EmployeeId);
				foreach (DataRow row4 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = row4["FunID"].ToString();
					str1 = row4["Limit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_1(str1, str2);
				}
				foreach (DataRow dataRow4 in dataTable1.Rows)
				{
					if (!dataRow4["Limit"].ToString().StartsWith("0"))
					{
						continue;
					}
					dataRow4.Delete();
				}
				dataTable1.AcceptChanges();
				dataTable = dataTable1;
			}
			else
			{
				dataTable = dataTable1;
			}
			return dataTable;
		}

		public static DataTable GetAllowedFunNodeByRole(string roleId, string pwbs, string webId)
		{
			DataRow row = null;
			string str = string.Format("select * ,'0000000000' Limit  FROM T_E_Sys_FunNode where DispState='是' and FunWbs like '{0}%' and webId='{1}' ORDER BY FunPWBS, OrderId", pwbs, webId);
			DataTable dataTable = SysDatabase.ExecuteTable(str);
			str = string.Format(string.Concat("select FunID,FunLimit from dbo.T_E_Org_RoleLimit where FunWbs like '", pwbs, "%' and roleId ='{0}'"), roleId);
			foreach (DataRow rowa in SysDatabase.ExecuteTable(str).Rows)
			{
                string str1 = rowa["FunID"].ToString();
                string str2 = rowa["FunLimit"].ToString();
				if ((int)dataTable.Select(string.Concat("_AutoID='", str1, "'")).Length <= 0)
				{
					continue;
				}
				DataRow dataRow = dataTable.Select(string.Concat("_AutoID='", str1, "'"))[0];
				string str3 = dataRow["Limit"].ToString();
				dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str2, str3);
			}
			foreach (DataRow row1 in dataTable.Rows)
			{
				if (!row1["Limit"].ToString().StartsWith("0"))
				{
					continue;
				}
				row1.Delete();
			}
			dataTable.AcceptChanges();
			return dataTable;
		}

		public static string GetFunAttrById(string funId, string attr)
		{
			string str = string.Format("select {1} from T_E_Sys_FunNode where _AutoID='{0}'", funId, attr);
			return SysDatabase.ExecuteScalar(str).ToString();
		}

		public static string GetFunLimitByEmployeeId(string EmployeeId, string FunId)
		{
			string str;
			DataRow row = null;
			string str1;
			string str2 = "0000000000";
			if (!RoleEmployeeService.IsEmployeeHasAllRight(EmployeeId))
			{
				string str3 = string.Format(string.Concat("select FunID,Limit from dbo.T_E_Org_PositionLimit where FunID = '", FunId, "' and PositionId in (select PositionId from dbo.T_E_Org_DeptEmployee where EmployeeId='{0}' and _IsDel=0)"), EmployeeId);
				foreach (DataRow rowA in SysDatabase.ExecuteTable(str3).Rows)
				{
                    rowA["FunID"].ToString();
                    str1 = rowA["Limit"].ToString();
					str2 = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format(string.Concat("select FunID,FunLimit from dbo.T_E_Org_RoleLimit where FunID = '", FunId, "' and roleid in (select RoleId from dbo.T_E_Org_RoleEmployee where EmployeeId='{0}')"), EmployeeId);
				foreach (DataRow dataRow in SysDatabase.ExecuteTable(str3).Rows)
				{
					dataRow["FunID"].ToString();
					str1 = dataRow["FunLimit"].ToString();
					str2 = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format(string.Concat("select FunID,FunLimit from dbo.T_E_Org_RoleLimit where FunID= '", FunId, "' and roleId in (select p.propId from T_E_Org_Position p inner join T_E_Org_DeptEmployee de on p._AutoID = de.positionId where de.EmployeeId='{0}' and de._IsDel=0 )"), EmployeeId);
				foreach (DataRow row1 in SysDatabase.ExecuteTable(str3).Rows)
				{
					row1["FunID"].ToString();
					str1 = row1["FunLimit"].ToString();
					str2 = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format("select _AutoID roleId,RoleSQL from dbo.T_E_Org_Role where RoleType='动态' and IsNull(RoleSQL,'')<>''", new object[0]);
				foreach (DataRow dataRow1 in SysDatabase.ExecuteTable(str3).Rows)
				{
					string str4 = dataRow1["RoleId"].ToString();
					if (SysDatabase.ExecuteScalar(string.Format("select count(*) where '{0}' in ({1})", EmployeeId, dataRow1["RoleSQL"].ToString())).ToString() == "0")
					{
						continue;
					}
					str3 = string.Format(string.Concat("select FunID,FunLimit from dbo.T_E_Org_RoleLimit where FunID = '", FunId, "' and roleId ='{0}'"), str4);
					foreach (DataRow row2 in SysDatabase.ExecuteTable(str3).Rows)
					{
						row2["FunID"].ToString();
						str1 = row2["FunLimit"].ToString();
						str2 = EIS.Permission.Utility.smethod_0(str1, str2);
					}
				}
				str3 = string.Format(string.Concat("select FunID,Limit from dbo.T_E_Org_DeptLimit where FunID = '", FunId, "' and DeptID in (select DeptID from dbo.T_E_Org_DeptEmployee where EmployeeId='{0}')"), EmployeeId);
				foreach (DataRow dataRow2 in SysDatabase.ExecuteTable(str3).Rows)
				{
					dataRow2["FunID"].ToString();
					str1 = dataRow2["Limit"].ToString();
					str2 = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format(string.Concat("select FunID,Limit from dbo.T_E_Org_EmployeeLimit where FunID = '", FunId, "' and (EmployeeId='{0}' or EmployeeId='everyone')"), EmployeeId);
				foreach (DataRow row3 in SysDatabase.ExecuteTable(str3).Rows)
				{
					row3["FunID"].ToString();
					str1 = row3["Limit"].ToString();
					str2 = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format(string.Concat("select FunID,Limit from dbo.T_E_Org_ExcludeLimit where FunID = '", FunId, "' and (EmployeeId='{0}' or EmployeeId='everyone')"), EmployeeId);
				foreach (DataRow dataRow3 in SysDatabase.ExecuteTable(str3).Rows)
				{
					dataRow3["FunID"].ToString();
					str1 = dataRow3["Limit"].ToString();
					str2 = EIS.Permission.Utility.smethod_1(str1, str2);
				}
				str = str2;
			}
			else
			{
				str = "1111111111";
			}
			return str;
		}

		public static string GetFunLimitByFunCode(string EmployeeId, string FunCode)
		{
			string str;
			string funIdByCode = FunNodeService.GetFunIdByCode(FunCode);
			str = (funIdByCode != "" ? EIS.Permission.Utility.GetFunLimitByEmployeeId(EmployeeId, funIdByCode) : "0000000000");
			return str;
		}

		public static DataTable GetFunLimitByFunId(string FunId)
		{
			DataRow row = null;
			string str;
			string str1;
			DataRow dataRow;
			string str2;
			DataTable dataTable = SysDatabase.ExecuteTable("select EmployeeID,'1111111111' Limit from T_E_Org_RoleEmployee where RoleID='allright'");
			string str3 = "0000000000";
			string str4 = string.Format(string.Concat("select EmployeeId, Limit from dbo.T_E_Org_EmployeeLimit where EmployeeId='everyone' and FunID = '", FunId, "'"), new object[0]);
			DataTable dataTable1 = SysDatabase.ExecuteTable(str4);
			if (dataTable1.Rows.Count > 0)
			{
				str3 = dataTable1.Rows[0]["Limit"].ToString();
				if (str3 != "0000000000")
				{
					dataTable.LoadDataRow(dataTable1.Rows[0].ItemArray, true);
				}
			}
			str4 = string.Format("select r.EmployeeID,Limit from T_E_Org_PositionLimit d inner join T_E_Org_DeptEmployee r\r\n                    on d.PositionID=r.PositionId and r.DeptEmployeeType=0 where d.FunID='{0}' and r._IsDel=0", FunId);
			foreach (DataRow rowA in SysDatabase.ExecuteTable(str4).Rows)
			{
                str = rowA["EmployeeID"].ToString();
                str1 = rowA["Limit"].ToString();
				if ((int)dataTable.Select(string.Concat("EmployeeID='", str, "'")).Length <= 0)
				{
					dataTable.LoadDataRow(row.ItemArray, true);
				}
				else
				{
					dataRow = dataTable.Select(string.Concat("EmployeeID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
			}
			str4 = string.Format("select r.EmployeeID,d.FunLimit Limit from T_E_Org_RoleLimit d inner join T_E_Org_RoleEmployee r\r\n                on d.RoleID = r.RoleID where FunID='{0}'", FunId);
			foreach (DataRow row1 in SysDatabase.ExecuteTable(str4).Rows)
			{
				str = row1["EmployeeID"].ToString();
				str1 = row1["Limit"].ToString();
				if ((int)dataTable.Select(string.Concat("EmployeeID='", str, "'")).Length <= 0)
				{
					dataTable.LoadDataRow(row1.ItemArray, true);
				}
				else
				{
					dataRow = dataTable.Select(string.Concat("EmployeeID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
			}
			str4 = string.Format("select d.EmployeeID,r.FunLimit Limit from T_E_Org_RoleLimit r inner join T_E_Org_Position p \r\n                on r.RoleID= p.PropId and r.FunID='{0}'\r\n                inner join T_E_Org_DeptEmployee d on p._AutoID=d.PositionId", FunId);
			foreach (DataRow dataRow1 in SysDatabase.ExecuteTable(str4).Rows)
			{
				str = dataRow1["EmployeeID"].ToString();
				str1 = dataRow1["Limit"].ToString();
				if ((int)dataTable.Select(string.Concat("EmployeeID='", str, "'")).Length <= 0)
				{
					dataTable.LoadDataRow(dataRow1.ItemArray, true);
				}
				else
				{
					dataRow = dataTable.Select(string.Concat("EmployeeID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
			}
			str4 = string.Format("select r.EmployeeID,d.Limit from T_E_Org_DeptLimit d inner join T_E_Org_DeptEmployee r\r\n                        on d.DeptID=r.DeptID and r.DeptEmployeeType=0 where d.FunID='{0}'", FunId);
			foreach (DataRow row2 in SysDatabase.ExecuteTable(str4).Rows)
			{
				str = row2["EmployeeID"].ToString();
				str1 = row2["Limit"].ToString();
				if ((int)dataTable.Select(string.Concat("EmployeeID='", str, "'")).Length <= 0)
				{
					dataTable.LoadDataRow(row2.ItemArray, true);
				}
				else
				{
					dataRow = dataTable.Select(string.Concat("EmployeeID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
			}
			str4 = string.Format(string.Concat("select EmployeeId,Limit from dbo.T_E_Org_EmployeeLimit where EmployeeId<>'everyone' and FunID = '", FunId, "'"), new object[0]);
			foreach (DataRow dataRow2 in SysDatabase.ExecuteTable(str4).Rows)
			{
				str = dataRow2["EmployeeID"].ToString();
				str1 = dataRow2["Limit"].ToString();
				if ((int)dataTable.Select(string.Concat("EmployeeID='", str, "'")).Length <= 0)
				{
					dataTable.LoadDataRow(dataRow2.ItemArray, true);
				}
				else
				{
					dataRow = dataTable.Select(string.Concat("EmployeeID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
			}
			if (str3 != "0000000000")
			{
				foreach (DataRow row3 in dataTable.Rows)
				{
					str = row3["EmployeeID"].ToString();
					str1 = row3["Limit"].ToString();
					row3["Limit"] = EIS.Permission.Utility.smethod_0(str3, str1);
				}
			}
			str4 = string.Format(string.Concat("select EmployeeId,Limit from dbo.T_E_Org_ExcludeLimit where FunID = '", FunId, "'"), new object[0]);
			foreach (DataRow dataRow3 in SysDatabase.ExecuteTable(str4).Rows)
			{
				str = dataRow3["EmployeeID"].ToString();
				str1 = dataRow3["Limit"].ToString();
				if ((int)dataTable.Select(string.Concat("EmployeeID='", str, "'")).Length <= 0)
				{
					continue;
				}
				dataRow = dataTable.Select(string.Concat("EmployeeID='", str, "'"))[0];
				str2 = dataRow["Limit"].ToString();
				dataRow["Limit"] = EIS.Permission.Utility.smethod_1(str1, str2);
			}
			return dataTable;
		}

		public static DataTable GetSonLimitDataByEmployeeId(string EmployeeId, string pwbs, string webId)
		{
			DataTable dataTable;
			DataRow row = null;
			string str;
			string str1;
			DataRow dataRow;
			string str2;
			string str3 = string.Format("select * ,'0000000000' Limit  FROM T_E_Sys_FunNode where DispState='是' and FunPWBS='{0}' and webId='{1}' ORDER BY FunPwbs, OrderId", pwbs, webId);
			DataTable dataTable1 = SysDatabase.ExecuteTable(str3);
			if (!RoleEmployeeService.IsEmployeeHasAllRight(EmployeeId))
			{
				str3 = string.Format(string.Concat("select FunID,Limit from dbo.T_E_Org_PositionLimit where left(FunWbs,len(FunWbs)-4) = '", pwbs, "' and PositionId in (select PositionId from dbo.T_E_Org_DeptEmployee where EmployeeId='{0}' and _IsDel=0)"), EmployeeId);
				foreach (DataRow rowA in SysDatabase.ExecuteTable(str3).Rows)
				{
                    str = rowA["FunID"].ToString();
                    str1 = rowA["Limit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format(string.Concat("select FunID,FunLimit from dbo.T_E_Org_RoleLimit where left(FunWbs,len(FunWbs)-4) = '", pwbs, "' and roleid in (select RoleId from dbo.T_E_Org_RoleEmployee where EmployeeId='{0}')"), EmployeeId);
				foreach (DataRow row1 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = row1["FunID"].ToString();
					str1 = row1["FunLimit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format(string.Concat("select FunID,FunLimit from dbo.T_E_Org_RoleLimit where left(FunWbs,len(FunWbs)-4) = '", pwbs, "' and roleId in (select p.propId from T_E_Org_Position p inner join T_E_Org_DeptEmployee de on p._AutoID = de.positionId where de.EmployeeId='{0}' and de._IsDel=0 )"), EmployeeId);
				foreach (DataRow dataRow1 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = dataRow1["FunID"].ToString();
					str1 = dataRow1["FunLimit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format("select _AutoID roleId,RoleSQL from dbo.T_E_Org_Role where RoleType='动态' and IsNull(RoleSQL,'')<>''", new object[0]);
				foreach (DataRow row2 in SysDatabase.ExecuteTable(str3).Rows)
				{
					string str4 = row2["RoleId"].ToString();
					if (SysDatabase.ExecuteScalar(string.Format("select count(*) where '{0}' in ({1})", EmployeeId, row2["RoleSQL"].ToString())).ToString() == "0")
					{
						continue;
					}
					str3 = string.Format(string.Concat("select FunID,FunLimit from dbo.T_E_Org_RoleLimit where left(FunWbs,len(FunWbs)-4) = '", pwbs, "' and roleId ='{0}'"), str4);
					foreach (DataRow dataRow2 in SysDatabase.ExecuteTable(str3).Rows)
					{
						str = dataRow2["FunID"].ToString();
						str1 = dataRow2["FunLimit"].ToString();
						if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
						{
							continue;
						}
						dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
						str2 = dataRow["Limit"].ToString();
						dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
					}
				}
				str3 = string.Format(string.Concat("select FunID,Limit from dbo.T_E_Org_DeptLimit where left(FunWbs,len(FunWbs)-4) = '", pwbs, "' and DeptID in (select DeptID from dbo.T_E_Org_DeptEmployee where EmployeeId='{0}')"), EmployeeId);
				foreach (DataRow row3 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = row3["FunID"].ToString();
					str1 = row3["Limit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format(string.Concat("select FunID,Limit from dbo.T_E_Org_EmployeeLimit where left(FunWbs,len(FunWbs)-4) = '", pwbs, "' and (EmployeeId='{0}' or EmployeeId='everyone')"), EmployeeId);
				foreach (DataRow dataRow3 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = dataRow3["FunID"].ToString();
					str1 = dataRow3["Limit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str1, str2);
				}
				str3 = string.Format(string.Concat("select FunID,Limit from dbo.T_E_Org_ExcludeLimit where left(FunWbs,len(FunWbs)-4) = '", pwbs, "' and (EmployeeId='{0}' or EmployeeId='everyone')"), EmployeeId);
				foreach (DataRow row4 in SysDatabase.ExecuteTable(str3).Rows)
				{
					str = row4["FunID"].ToString();
					str1 = row4["Limit"].ToString();
					if ((int)dataTable1.Select(string.Concat("_AutoID='", str, "'")).Length <= 0)
					{
						continue;
					}
					dataRow = dataTable1.Select(string.Concat("_AutoID='", str, "'"))[0];
					str2 = dataRow["Limit"].ToString();
					dataRow["Limit"] = EIS.Permission.Utility.smethod_1(str1, str2);
				}
				dataTable = dataTable1;
			}
			else
			{
				str3 = string.Format("select * ,'1111111111' Limit  FROM T_E_Sys_FunNode where DispState='是' and FunPWBS='{0}' and webId='{1}' ORDER BY FunPwbs, OrderId", pwbs, webId);
				dataTable1 = SysDatabase.ExecuteTable(str3);
				dataTable = dataTable1;
			}
			return dataTable;
		}

		public static DataTable GetSonLimitDataByRole(string roleId, string pwbs, string webId)
		{
			DataRow row = null;
			string str = string.Format("select * ,'0000000000' Limit  FROM T_E_Sys_FunNode where DispState='是' and FunPWBS='{0}' and webId='{1}' ORDER BY FunPwbs, OrderId", pwbs, webId);
			DataTable dataTable = SysDatabase.ExecuteTable(str);
			str = string.Format(string.Concat("select FunID,FunLimit from dbo.T_E_Org_RoleLimit where left(FunWbs,len(FunWbs)-4) = '", pwbs, "' and roleid ='{0}'"), roleId);
			foreach (DataRow rowA in SysDatabase.ExecuteTable(str).Rows)
			{
                string str1 = rowA["FunID"].ToString();
                string str2 = rowA["FunLimit"].ToString();
				if ((int)dataTable.Select(string.Concat("_AutoID='", str1, "'")).Length <= 0)
				{
					continue;
				}
				DataRow dataRow = dataTable.Select(string.Concat("_AutoID='", str1, "'"))[0];
				string str3 = dataRow["Limit"].ToString();
				dataRow["Limit"] = EIS.Permission.Utility.smethod_0(str2, str3);
			}
			foreach (DataRow row1 in dataTable.Rows)
			{
				if (!row1["Limit"].ToString().StartsWith("0"))
				{
					continue;
				}
				row1.Delete();
			}
			dataTable.AcceptChanges();
			return dataTable;
		}

		public static UserContext GetUserInfo(string deptEmployeeId, string webId)
		{
			UserContext userContext = new UserContext();
			DeptEmployee deptEmployeeById = DeptEmployeeService.GetDeptEmployeeById(deptEmployeeId);
			Employee model = EmployeeService.GetModel(deptEmployeeById.EmployeeID);
			userContext.LoginName = model.LoginName;
			userContext.EmployeeId = deptEmployeeById.EmployeeID;
			userContext.EmployeeName = model.EmployeeName;
			Department department = DepartmentService.GetModel(deptEmployeeById.DeptID);
			userContext.DeptId = department._AutoID;
			userContext.DeptWbs = department.DeptWBS;
			userContext.DeptName = department.DeptName;
			Department model1 = DepartmentService.GetModel(department.CompanyID);
			userContext.CompanyId = deptEmployeeById.CompanyID;
			userContext.CompanyCode = model1.DeptCode;
			userContext.CompanyWbs = model1.DeptWBS;
			userContext.CompanyName = model1.DeptName;
			userContext.PositionId = deptEmployeeById.PositionId;
			userContext.PositionName = deptEmployeeById.PositionName;
			userContext.WebId = webId;
			return userContext;
		}

		public static LoginInfoType LoginChk(string user, string string_0)
		{
			LoginInfoType loginInfoType;
			Employee modelByLoginName = (new _Employee()).GetModelByLoginName(user);
			if (!(modelByLoginName == null ? false : modelByLoginName._IsDel != 1))
			{
				loginInfoType = LoginInfoType.NotExist;
			}
			else if (modelByLoginName.IsLocked != "是")
			{
				loginInfoType = (Security.Encrypt(string_0).Trim().Equals(modelByLoginName.LoginPass) ? LoginInfoType.Allowed : LoginInfoType.WrongPwd);
			}
			else
			{
				loginInfoType = LoginInfoType.IsLocked;
			}
			return loginInfoType;
		}

		private static string smethod_0(string limit1, string limit2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 10; i++)
			{
				if (Convert.ToInt16(limit1.Substring(i, 1)) + Convert.ToInt16(limit2.Substring(i, 1)) <= 0)
				{
					stringBuilder.Append("0");
				}
				else
				{
					stringBuilder.Append("1");
				}
			}
			return stringBuilder.ToString();
		}

		private static string smethod_1(string limit1, string limit2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 10; i++)
			{
				int num = Convert.ToInt16(limit1.Substring(i, 1));
				int num1 = Convert.ToInt16(limit2.Substring(i, 1));
				if (num != 1)
				{
					stringBuilder.Append(num1.ToString());
				}
				else
				{
					stringBuilder.Append("0");
				}
			}
			return stringBuilder.ToString();
		}
	}
}