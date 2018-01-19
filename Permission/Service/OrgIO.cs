using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Access;
using EIS.Permission.Model;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;

namespace EIS.Permission.Service
{
	public class OrgIO
	{
		public readonly static string DeptTypeId;

		static OrgIO()
		{
			OrgIO.DeptTypeId = "7C2F6B38-EDE8-4EB4-B667-6EABE32A1EEF";
		}

		public OrgIO()
		{
		}

		public static void ClearAllEmployee()
		{
			SysDatabase.ExecuteNonQuery("\r\n                    delete dbo.T_E_Org_DeptEmployee\r\n                    delete dbo.T_E_Org_Employee\r\n                    delete dbo.T_E_Org_Position\r\n                    delete dbo.T_E_Org_RoleEmployee");
		}

		public static void ClearAllOrgAndEmployee()
		{
			SysDatabase.ExecuteNonQuery("\r\n                delete dbo.T_E_Org_Department where DeptWBS<>'0'\r\n                delete dbo.T_E_Org_DeptEmployee\r\n                delete dbo.T_E_Org_Employee\r\n                delete dbo.T_E_Org_Group\r\n                delete dbo.T_E_Org_OrgGroup\r\n                delete dbo.T_E_Org_Position\r\n                delete dbo.T_E_Org_PositionProp\r\n                delete dbo.T_E_Org_RoleEmployee\r\n\r\n                delete dbo.T_E_Org_User where not (LoginName='SuperAdmin' or LoginName='groupadmin')");
		}

		public static string EmpImport(DataTable data)
		{
			data.Columns.Add("flag", typeof(int));
			DbConnection dbConnection = SysDatabase.CreateConnection();
			dbConnection.Open();
			DbTransaction dbTransaction = dbConnection.BeginTransaction();
			_Department __Department = new _Department(dbTransaction);
			Department department = new Department();
			_Position __Position = new _Position(dbTransaction);
			_Employee __Employee = new _Employee(dbTransaction);
			int num = 0;
			int num1 = 0;
			int num2 = 0;
			try
			{
				try
				{
					foreach (DataRow row in data.Rows)
					{
						string str = row[0].ToString();
						string str1 = row[1].ToString();
						string str2 = row[2].ToString();
						string str3 = row[3].ToString().Replace(" ", "");
						string str4 = row[4].ToString().ToLower().Replace(" ", "");
						string str5 = row[5].ToString();
						string str6 = row[6].ToString();
						string str7 = row[7].ToString();
						string str8 = row[8].ToString();
						string str9 = row[9].ToString();
						if ((string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str3) ? false : !string.IsNullOrEmpty(str4)))
						{
							bool flag = true;
							string[] strArrays = str.Split(new char[] { '/' });
							Department subDeptByName = null;
							string deptWBS = "0";
							int num3 = 0;
							while (true)
							{
								if (num3 < (int)strArrays.Length)
								{
									subDeptByName = __Department.GetSubDeptByName(strArrays[num3], deptWBS);
									if (subDeptByName == null)
									{
										flag = false;
										break;
									}
									else
									{
										deptWBS = subDeptByName.DeptWBS;
										num3++;
									}
								}
								else
								{
									break;
								}
							}
							if (!flag)
							{
								continue;
							}
							if (__Department.GetCompanyByDeptWbs(deptWBS) == null)
							{
								num1++;
								row["flag"] = 1;
							}
							else
							{
								Position modelByName = __Position.GetModelByName(str1, subDeptByName._AutoID);
								if ((modelByName != null ? false : !string.IsNullOrEmpty(str1)))
								{
									modelByName = new Position()
									{
										_AutoID = Guid.NewGuid().ToString(),
										_CreateTime = DateTime.Now,
										_UpdateTime = DateTime.Now,
										_OrgCode = "",
										_UserName = "",
										_IsDel = 0,
										PositionCode = "",
										PositionName = str1,
										OrderID = 0,
										DeptID = subDeptByName._AutoID
									};
									__Position.Add(modelByName);
									num2++;
								}
								if (__Employee.IsLoginExist(str4))
								{
									row["flag"] = 1;
									num1++;
								}
								else
								{
									_DeptEmployee __DeptEmployee = new _DeptEmployee(dbTransaction);
									Employee employee = new Employee()
									{
										_AutoID = Guid.NewGuid().ToString(),
										_OrgCode = "",
										_UserName = "",
										_CreateTime = DateTime.Now,
										_UpdateTime = DateTime.Now,
										_IsDel = 0,
										EmployeeName = str3,
										EmployeeCode = str2,
										Sex = str5,
										Officephone = str6,
										EMail = str8,
										Cellphone = str7,
										EmployeeType = "正式",
										EmployeeState = "在职",
										IdCard = str9,
										IsLocked = "否",
										LockReason = "",
										LoginName = str4,
										LoginPass = Security.Encrypt(AppSettings.Instance.EmployeeDefaultPass)
									};
									__Employee.Add(employee);
									DeptEmployee deptEmployee = new DeptEmployee()
									{
										_AutoID = Guid.NewGuid().ToString(),
										_OrgCode = "",
										_UserName = "",
										_CreateTime = DateTime.Now,
										_UpdateTime = DateTime.Now,
										_IsDel = 0,
										DeptID = subDeptByName._AutoID,
										DeptName = subDeptByName.DeptName,
										CompanyID = subDeptByName.CompanyID,
										EmployeeID = employee._AutoID,
										EmployeeName = employee.EmployeeName,
										OrderID = 0
									};
									if (modelByName == null)
									{
										deptEmployee.PositionId = "";
										deptEmployee.PositionName = "";
									}
									else
									{
										deptEmployee.PositionId = modelByName._AutoID;
										deptEmployee.PositionName = modelByName.PositionName;
									}
									__DeptEmployee.Add(deptEmployee);
									num++;
								}
							}
						}
						else
						{
							row["flag"] = 1;
							num1++;
						}
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
			object[] count = new object[] { data.Rows.Count, num2, num, num1 };
			return string.Format("共{0}条数据，创建岗位{1}，创建人员{2}，跳过{3}", count);
		}

		public static string OrgImport(DataTable data)
		{
			data.Columns.Add("flag", typeof(int));
			DbConnection dbConnection = SysDatabase.CreateConnection();
			dbConnection.Open();
			DbTransaction dbTransaction = dbConnection.BeginTransaction();
			DataTable dataTable = SysDatabase.ExecuteTable("select _AutoId typeId,TypeName,TypeProp from T_E_Org_DeptType");
			_Department __Department = new _Department(dbTransaction);
			Department department = new Department();
			StringDictionary stringDictionaries = new StringDictionary();
			int num = 0;
			int num1 = 0;
			try
			{
				try
				{
					foreach (DataRow row in data.Rows)
					{
						string str = row[0].ToString();
						string str1 = row[1].ToString();
						string str2 = row[2].ToString();
						string str3 = "";
						if ((string.IsNullOrEmpty(str) ? false : !string.IsNullOrEmpty(str2)))
						{
							if ((int)dataTable.Select(string.Concat("typeName='", str2, "'")).Length == 0)
							{
								continue;
							}
							str3 = dataTable.Select(string.Concat("typeName='", str2, "'"))[0]["TypeId"].ToString();
							string[] strArrays = str.Split(new char[] { '/' });
							string deptWBS = "0";
							for (int i = 0; i < (int)strArrays.Length; i++)
							{
								string str4 = strArrays[i];
								Department subDeptByName = __Department.GetSubDeptByName(str4, deptWBS);
								if (subDeptByName == null)
								{
									subDeptByName = new Department()
									{
										_AutoID = Guid.NewGuid().ToString(),
										_CreateTime = DateTime.Now,
										_UpdateTime = DateTime.Now,
										_OrgCode = "",
										_UserName = "",
										_IsDel = 0
									};
									string str5 = "";
									str5 = ((int)dataTable.Select(string.Concat("typeName='", str2, "' and (TypeProp=0 or TypeProp=1)")).Length <= 0 ? __Department.GetCompanyByDeptWbs(deptWBS)._AutoID : subDeptByName._AutoID);
									if (i != (int)strArrays.Length - 1)
									{
										subDeptByName.DeptCode = "";
										subDeptByName.DeptName = str4;
										subDeptByName.OrderID = __Department.GetMaxOrder(deptWBS) + 1;
										subDeptByName.DeptAbbr = "";
										subDeptByName.DeptPWBS = deptWBS;
										subDeptByName.DeptWBS = __Department.GetNewCode(subDeptByName.DeptPWBS);
										subDeptByName.TypeID = OrgIO.DeptTypeId;
										subDeptByName.DeptState = "正常";
										subDeptByName.LdapPath = "";
										subDeptByName.CompanyID = str5;
									}
									else
									{
										subDeptByName.DeptCode = str1;
										subDeptByName.DeptName = str4;
										subDeptByName.OrderID = __Department.GetMaxOrder(deptWBS) + 1;
										subDeptByName.DeptAbbr = "";
										subDeptByName.DeptPWBS = deptWBS;
										subDeptByName.DeptWBS = __Department.GetNewCode(subDeptByName.DeptPWBS);
										subDeptByName.TypeID = str3;
										subDeptByName.DeptState = "正常";
										subDeptByName.LdapPath = "";
										subDeptByName.CompanyID = str5;
									}
									__Department.Add(subDeptByName);
									num++;
								}
								deptWBS = subDeptByName.DeptWBS;
							}
						}
						else
						{
							row["flag"] = 1;
							num1++;
						}
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
			string str6 = string.Format("共{0}条数据，创建部门{1}，跳过{2}", data.Rows.Count, num, num1);
			return str6;
		}
	}
}