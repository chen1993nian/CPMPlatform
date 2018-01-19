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
	public class _Employee
	{
		private DbTransaction dbTransaction_0 = null;

		public _Employee()
		{
		}

		public _Employee(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(Employee model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Insert T_E_Org_Employee (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tEmployeeCode,\r\n\t\t\t\t\tEmployeeName,\r\n\t\t\t\t\tSysUser,\r\n\t\t\t\t\tUserType,\r\n                    LoginName,\r\n                    LoginPass,\r\n                    IsLocked,\r\n                    LockReason,\r\n\t\t\t\t\tSex,\r\n\t\t\t\t\tBirthday,\r\n\t\t\t\t\tNationality,\r\n\t\t\t\t\tMarriage,\r\n\t\t\t\t\tIDcard,\r\n                    SID,\r\n\t\t\t\t\tAddress,\r\n\t\t\t\t\tCity,\r\n\t\t\t\t\tZipCode,\r\n\t\t\t\t\tEmployeeType,\r\n\t\t\t\t\tEmployeeState,\r\n\t\t\t\t\tEMail,\r\n\t\t\t\t\tHomephone,\r\n\t\t\t\t\tCellphone,\r\n\t\t\t\t\tOfficephone,\r\n\t\t\t\t\tGraduateSchool,\r\n\t\t\t\t\tRemark,\r\n                    OutList,\r\n                    SignId,\r\n                    Photo,\r\n\t\t\t\t\tOrderID\r\n\t\t\t,DefaultLogin) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@EmployeeCode,\r\n\t\t\t\t\t@EmployeeName,\r\n\t\t\t\t\t@SysUser,\r\n\t\t\t\t\t@UserType,\r\n                    @LoginName,\r\n                    @LoginPass,\r\n                    @IsLocked,\r\n                    @LockReason,\r\n\t\t\t\t\t@Sex,\r\n\t\t\t\t\t@Birthday,\r\n\t\t\t\t\t@Nationality,\r\n\t\t\t\t\t@Marriage,\r\n\t\t\t\t\t@IDcard,\r\n\t\t\t\t\t@SID,\r\n\t\t\t\t\t@Address,\r\n\t\t\t\t\t@City,\r\n\t\t\t\t\t@ZipCode,\r\n\t\t\t\t\t@EmployeeType,\r\n\t\t\t\t\t@EmployeeState,\r\n\t\t\t\t\t@EMail,\r\n\t\t\t\t\t@Homephone,\r\n\t\t\t\t\t@Cellphone,\r\n\t\t\t\t\t@Officephone,\r\n\t\t\t\t\t@GraduateSchool,\r\n\t\t\t\t\t@Remark,\r\n                    @OutList,\r\n                    @SignId,\r\n                    @Photo,\r\n\t\t\t\t\t@OrderID\r\n\t\t\t,@DefaultLogin)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeCode", DbType.String, model.EmployeeCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeName", DbType.String, model.EmployeeName);
			SysDatabase.AddInParameter(sqlStringCommand, "@SysUser", DbType.Int32, model.SysUser);
			SysDatabase.AddInParameter(sqlStringCommand, "@UserType", DbType.String, model.UserType);
			SysDatabase.AddInParameter(sqlStringCommand, "@LoginName", DbType.String, model.LoginName);
			SysDatabase.AddInParameter(sqlStringCommand, "@LoginPass", DbType.String, model.LoginPass);
			SysDatabase.AddInParameter(sqlStringCommand, "@IsLocked", DbType.String, model.IsLocked);
			SysDatabase.AddInParameter(sqlStringCommand, "@LockReason", DbType.String, model.LockReason);
			SysDatabase.AddInParameter(sqlStringCommand, "@Sex", DbType.String, model.Sex);
			SysDatabase.AddInParameter(sqlStringCommand, "@Birthday", DbType.DateTime, model.Birthday);
			SysDatabase.AddInParameter(sqlStringCommand, "@Nationality", DbType.String, model.Nationality);
			SysDatabase.AddInParameter(sqlStringCommand, "@Marriage", DbType.String, model.Marriage);
			SysDatabase.AddInParameter(sqlStringCommand, "@IDcard", DbType.String, model.IdCard);
			SysDatabase.AddInParameter(sqlStringCommand, "@SID", DbType.String, model.SID);
			SysDatabase.AddInParameter(sqlStringCommand, "@Address", DbType.String, model.Address);
			SysDatabase.AddInParameter(sqlStringCommand, "@City", DbType.String, model.City);
			SysDatabase.AddInParameter(sqlStringCommand, "@ZipCode", DbType.String, model.ZipCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeType", DbType.String, model.EmployeeType);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeState", DbType.String, model.EmployeeState);
			SysDatabase.AddInParameter(sqlStringCommand, "@EMail", DbType.String, model.EMail);
			SysDatabase.AddInParameter(sqlStringCommand, "@Homephone", DbType.String, model.Homephone);
			SysDatabase.AddInParameter(sqlStringCommand, "@Cellphone", DbType.String, model.Cellphone);
			SysDatabase.AddInParameter(sqlStringCommand, "@Officephone", DbType.String, model.Officephone);
			SysDatabase.AddInParameter(sqlStringCommand, "@GraduateSchool", DbType.String, model.GraduateSchool);
			SysDatabase.AddInParameter(sqlStringCommand, "@Remark", DbType.String, model.Remark);
			SysDatabase.AddInParameter(sqlStringCommand, "@OutList", DbType.String, model.OutList);
			SysDatabase.AddInParameter(sqlStringCommand, "@SignId", DbType.String, model.SignId);
			SysDatabase.AddInParameter(sqlStringCommand, "@Photo", DbType.String, model.PhotoId);
			SysDatabase.AddInParameter(sqlStringCommand, "@OrderID", DbType.Int32, model.OrderID);
            SysDatabase.AddInParameter(sqlStringCommand, "@DefaultLogin", DbType.String, model.DefaultLogin);
            
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int ChangePass(string employeeId, string newPass)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update  T_E_Org_Employee set ");
			stringBuilder.Append(" LoginPass=@LoginPass ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, employeeId);
			SysDatabase.AddInParameter(sqlStringCommand, "@LoginPass", DbType.String, newPass);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string employeeId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Org_Employee set _IsDel=1");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, employeeId);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public List<Employee> GetAllEmployee(string deptId)
		{
			List<Employee> employees = new List<Employee>();
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand("select d.PositionId,d.OrderID as DeOrderID,d.DeptEmployeeType as DeType,d.PositionName,e.* from dbo.T_E_Org_Employee e inner join dbo.T_E_Org_DeptEmployee d\r\n                    on e._AutoID=d.EmployeeID where  d._IsDel=0");
			SysDatabase.AddInParameter(sqlStringCommand, "@DeptId", DbType.String, deptId);
			foreach (DataRow row in SysDatabase.ExecuteTable(sqlStringCommand).Rows)
			{
				employees.Add(this.GetModel(row));
			}
			return employees;
		}

		public List<Employee> GetEmployeeByDeptId(string deptId)
		{
			List<Employee> employees = new List<Employee>();
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand("select d.PositionId,d.OrderID as DeOrderID,d.DeptEmployeeType as DeType,d.PositionName,e.* from dbo.T_E_Org_Employee e inner join dbo.T_E_Org_DeptEmployee d\r\n                    on e._AutoID=d.EmployeeID where d.DeptId=@DeptId and d._IsDel=0 order by d.OrderID");
			SysDatabase.AddInParameter(sqlStringCommand, "@DeptId", DbType.String, deptId);
			foreach (DataRow row in SysDatabase.ExecuteTable(sqlStringCommand).Rows)
			{
				employees.Add(this.GetModel(row));
			}
			return employees;
		}

		public List<Employee> GetEmployeeByDeptWbs(string deptWbs)
		{
			List<Employee> employees = new List<Employee>();
			string modelByWbs = (new _Department()).GetModelByWbs(deptWbs)._AutoID;
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand("select d.PositionId,d.OrderID as DeOrderID,d.DeptEmployeeType as DeType,d.PositionName,e.* from dbo.T_E_Org_Employee e inner join dbo.T_E_Org_DeptEmployee d\r\n                    on e._AutoID=d.EmployeeID where d.DeptId=@DeptId and d._IsDel=0 order by d.OrderID");
			SysDatabase.AddInParameter(sqlStringCommand, "@DeptId", DbType.String, modelByWbs);
			foreach (DataRow row in SysDatabase.ExecuteTable(sqlStringCommand).Rows)
			{
				employees.Add(this.GetModel(row));
			}
			return employees;
		}

		public static DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  From T_E_Org_Employee ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public Employee GetModel(string string_0)
		{
			Employee model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select e.*,d._AutoID as DeId,d.PositionId,d.OrderID as DeOrderID,d.DeptEmployeeType as DeType,d.PositionName,d.DeptName \r\n                    from dbo.T_E_Org_Employee e left join dbo.T_E_Org_DeptEmployee d\r\n                    on e._AutoID=d.EmployeeID where d.DeptEmployeeType=0 and d._IsDel=0 and d.EmployeeId=@EmployeeId ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeId", DbType.String, string_0);
			Employee employee = new Employee();
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

		public Employee GetModel(DataRow dataRow_0)
		{
			Employee employee = new Employee()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				employee._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				employee._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				employee._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			employee.EmployeeCode = dataRow_0["EmployeeCode"].ToString();
			employee.EmployeeName = dataRow_0["EmployeeName"].ToString();
			employee.Sex = dataRow_0["Sex"].ToString();
			if (dataRow_0["Birthday"].ToString() != "")
			{
				employee.Birthday = new DateTime?(DateTime.Parse(dataRow_0["Birthday"].ToString()));
			}
			employee.Nationality = dataRow_0["Nationality"].ToString();
			employee.Marriage = dataRow_0["Marriage"].ToString();
			employee.IdCard = dataRow_0["IDcard"].ToString();
			employee.Address = dataRow_0["Address"].ToString();
			employee.City = dataRow_0["City"].ToString();
			employee.ZipCode = dataRow_0["ZipCode"].ToString();
			employee.EmployeeType = dataRow_0["EmployeeType"].ToString();
			employee.EmployeeState = dataRow_0["EmployeeState"].ToString();
			employee.EMail = dataRow_0["EMail"].ToString();
			employee.Homephone = dataRow_0["Homephone"].ToString();
			employee.Cellphone = dataRow_0["Cellphone"].ToString();
			employee.Officephone = dataRow_0["Officephone"].ToString();
			employee.GraduateSchool = dataRow_0["GraduateSchool"].ToString();
			employee.Remark = dataRow_0["Remark"].ToString();
			employee.LoginName = dataRow_0["LoginName"].ToString();
			employee.LoginPass = dataRow_0["LoginPass"].ToString();
			employee.IsLocked = dataRow_0["IsLocked"].ToString();
			employee.LockReason = dataRow_0["LockReason"].ToString();
			employee.OutList = dataRow_0["OutList"].ToString();
			employee.SignId = dataRow_0["SignId"].ToString();
			employee.PhotoId = dataRow_0["Photo"].ToString();
			employee.BasePath = dataRow_0["BasePath"].ToString();
            employee.DefaultLogin = "1";
            if (dataRow_0.Table.Columns.Contains("DefaultLogin"))
            {
                employee.DefaultLogin = dataRow_0["DefaultLogin"].ToString();
            }
            
			if (dataRow_0["LastLoginTime"].ToString() != "")
			{
				employee.LastLoginTime = new DateTime?(DateTime.Parse(dataRow_0["LastLoginTime"].ToString()));
			}
			if (dataRow_0["LoginCount"].ToString() != "")
			{
				employee.LoginCount = int.Parse(dataRow_0["LoginCount"].ToString());
			}
			if (dataRow_0["HideMobile"].ToString() != "")
			{
				employee.HideMobile = int.Parse(dataRow_0["HideMobile"].ToString());
			}
			if (dataRow_0["OrderID"].ToString() != "")
			{
				employee.OrderID = int.Parse(dataRow_0["OrderID"].ToString());
			}
			if (dataRow_0.Table.Columns.Contains("PositionId"))
			{
				employee.PositionId = dataRow_0["PositionId"].ToString();
			}
			if (dataRow_0.Table.Columns.Contains("PositionName"))
			{
				employee.PositionName = dataRow_0["PositionName"].ToString();
			}
			if (dataRow_0.Table.Columns.Contains("DeptName"))
			{
				employee.DeptName = dataRow_0["DeptName"].ToString();
			}
			if (dataRow_0.Table.Columns.Contains("DeOrderID") && dataRow_0["DeOrderID"].ToString() != "")
			{
				employee.DeOrderID = int.Parse(dataRow_0["DeOrderID"].ToString());
			}
			if (dataRow_0.Table.Columns.Contains("DeType") && dataRow_0["DeType"].ToString() != "")
			{
				employee.DeType = int.Parse(dataRow_0["DeType"].ToString());
			}
			if (dataRow_0.Table.Columns.Contains("DeID"))
			{
				employee.DeId = dataRow_0["DeID"].ToString();
			}
			employee.ReAuthType = dataRow_0["ReAuthType"].ToString();
			employee.ReAuthPass = dataRow_0["ReAuthPass"].ToString();
			return employee;
		}

		public Employee GetModelByLoginName(string loginName)
		{
			Employee model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Org_Employee ");
			stringBuilder.Append(" where LoginName=@LoginName ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@LoginName", DbType.String, loginName);
			Employee employee = new Employee();
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

		public List<Employee> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Employee> employees = new List<Employee>();
			stringBuilder.Append("select *  FROM T_E_Org_Employee ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				employees.Add(this.GetModel(row));
			}
			return employees;
		}

		public bool IsADUserExist(string SID)
		{
			bool flag;
			string str = string.Concat("select count(*) from T_E_Org_Employee where sid='", SID, "'");
			SysDatabase.GetSqlStringCommand(str);
			flag = (this.dbTransaction_0 == null ? Convert.ToInt32(SysDatabase.ExecuteScalar(str)) > 0 : Convert.ToInt32(SysDatabase.ExecuteScalar(str, this.dbTransaction_0)) > 0);
			return flag;
		}

		public bool IsExistByMobile(string phone)
		{
			object obj;
			bool flag;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select count(*)  From T_E_Org_Employee where Cellphone = '{0}'", phone);
			obj = (this.dbTransaction_0 == null ? SysDatabase.ExecuteScalar(stringBuilder.ToString()) : SysDatabase.ExecuteScalar(stringBuilder.ToString(), this.dbTransaction_0));
			flag = ((obj == DBNull.Value ? true : obj == null) ? true : Convert.ToInt32(obj) > 0);
			return flag;
		}

		public bool IsLoginExist(string LoginName)
		{
			bool flag;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select count(*)  FROM T_E_Org_Employee where LoginName = '{0}'", LoginName);
			object obj = null;
			obj = (this.dbTransaction_0 == null ? SysDatabase.ExecuteScalar(stringBuilder.ToString()) : SysDatabase.ExecuteScalar(stringBuilder.ToString(), this.dbTransaction_0));
			flag = (obj == DBNull.Value ? true : Convert.ToInt32(obj) > 0);
			return flag;
		}

		public bool IsLoginExist(string LoginName, string EmployeeId)
		{
			object obj;
			bool flag;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select count(*)  From T_E_Org_Employee where LoginName = '{0}' and isnull(_AutoId,'')<>'{1}'", LoginName, EmployeeId);
			obj = (this.dbTransaction_0 == null ? SysDatabase.ExecuteScalar(stringBuilder.ToString()) : SysDatabase.ExecuteScalar(stringBuilder.ToString(), this.dbTransaction_0));
			flag = ((obj == DBNull.Value ? true : obj == null) ? true : Convert.ToInt32(obj) > 0);
			return flag;
		}

		public int Remove(string employeeId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Org_Employee ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, employeeId);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Update(Employee model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("update T_E_Org_Employee set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tEmployeeCode=@EmployeeCode,\r\n\t\t\t\t\tEmployeeName=@EmployeeName,\r\n                    LoginName=@LoginName,\r\n\t\t\t\t\tSex=@Sex,\r\n\t\t\t\t\tBirthday=@Birthday,\r\n\t\t\t\t\tNationality=@Nationality,\r\n\t\t\t\t\tMarriage=@Marriage,\r\n\t\t\t\t\tIDcard=@IDcard,\r\n\t\t\t\t\tAddress=@Address,\r\n\t\t\t\t\tCity=@City,\r\n\t\t\t\t\tZipCode=@ZipCode,\r\n\t\t\t\t\tEmployeeType=@EmployeeType,\r\n\t\t\t\t\tEmployeeState=@EmployeeState,\r\n\t\t\t\t\tEMail=@EMail,\r\n\t\t\t\t\tHomephone=@Homephone,\r\n\t\t\t\t\tCellphone=@Cellphone,\r\n\t\t\t\t\tOfficephone=@Officephone,\r\n\t\t\t\t\tGraduateSchool=@GraduateSchool,\r\n\t\t\t\t\tRemark=@Remark,\r\n\t\t\t\t\tOutList=@OutList,\r\n\t\t\t\t\tHideMobile=@HideMobile,\r\n\t\t\t\t\tSignId=@SignId,\r\n\t\t\t\t\tBasePath=@BasePath,\r\n\t\t\t\t\tPhoto=@Photo,\r\n\t\t\t\t\tOrderID=@OrderID,\r\n                    IsLocked=@IsLocked,\r\n                    LockReason=@LockReason\r\n\t\t\t\t\t ,DefaultLogin=@DefaultLogin  where _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeCode", DbType.String, model.EmployeeCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeName", DbType.String, model.EmployeeName);
			SysDatabase.AddInParameter(sqlStringCommand, "@LoginName", DbType.String, model.LoginName);
			SysDatabase.AddInParameter(sqlStringCommand, "@Sex", DbType.String, model.Sex);
			SysDatabase.AddInParameter(sqlStringCommand, "@Birthday", DbType.DateTime, model.Birthday);
			SysDatabase.AddInParameter(sqlStringCommand, "@Nationality", DbType.String, model.Nationality);
			SysDatabase.AddInParameter(sqlStringCommand, "@Marriage", DbType.String, model.Marriage);
			SysDatabase.AddInParameter(sqlStringCommand, "@IDcard", DbType.String, model.IdCard);
			SysDatabase.AddInParameter(sqlStringCommand, "@Address", DbType.String, model.Address);
			SysDatabase.AddInParameter(sqlStringCommand, "@City", DbType.String, model.City);
			SysDatabase.AddInParameter(sqlStringCommand, "@ZipCode", DbType.String, model.ZipCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeType", DbType.String, model.EmployeeType);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeState", DbType.String, model.EmployeeState);
			SysDatabase.AddInParameter(sqlStringCommand, "@EMail", DbType.String, model.EMail);
			SysDatabase.AddInParameter(sqlStringCommand, "@Homephone", DbType.String, model.Homephone);
			SysDatabase.AddInParameter(sqlStringCommand, "@Cellphone", DbType.String, model.Cellphone);
			SysDatabase.AddInParameter(sqlStringCommand, "@Officephone", DbType.String, model.Officephone);
			SysDatabase.AddInParameter(sqlStringCommand, "@GraduateSchool", DbType.String, model.GraduateSchool);
			SysDatabase.AddInParameter(sqlStringCommand, "@Remark", DbType.String, model.Remark);
			SysDatabase.AddInParameter(sqlStringCommand, "@OutList", DbType.String, model.OutList);
			SysDatabase.AddInParameter(sqlStringCommand, "@HideMobile", DbType.Int32, model.HideMobile);
			SysDatabase.AddInParameter(sqlStringCommand, "@SignId", DbType.String, model.SignId);
			SysDatabase.AddInParameter(sqlStringCommand, "@Photo", DbType.String, model.PhotoId);
			SysDatabase.AddInParameter(sqlStringCommand, "@BasePath", DbType.String, model.BasePath);
			SysDatabase.AddInParameter(sqlStringCommand, "@OrderID", DbType.Int32, model.OrderID);
			SysDatabase.AddInParameter(sqlStringCommand, "@IsLocked", DbType.String, model.IsLocked);
			SysDatabase.AddInParameter(sqlStringCommand, "@LockReason", DbType.String, model.LockReason);
            SysDatabase.AddInParameter(sqlStringCommand, "@DefaultLogin", DbType.String, model.DefaultLogin);
            
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int UpdateName(string EmpId, string EmpName)
		{
			int num;
			string str = string.Format("Update T_E_Org_DeptEmployee set EmployeeName='{1}' where EmployeeId='{0}'", EmpId, EmpName);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(str) : SysDatabase.ExecuteNonQuery(str, this.dbTransaction_0));
			return num;
		}

		public int UpdateReAuth(string employeeId, string reType, string rePass)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Org_Employee set ");
			stringBuilder.Append(" ReAuthType=@ReAuthType, ");
			stringBuilder.Append(" ReAuthPass=@ReAuthPass ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, employeeId);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReAuthType", DbType.String, reType);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReAuthPass", DbType.String, rePass);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}