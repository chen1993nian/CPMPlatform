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
	public class _Department
	{
		private DbTransaction dbTransaction_0 = null;

		public _Department()
		{
		}

		public _Department(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(Department model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Org_Department (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tDeptWBS,\r\n\t\t\t\t\tDeptPWBS,\r\n\t\t\t\t\tDeptCode,\r\n\t\t\t\t\tDeptAbbr,\r\n\t\t\t\t\tDeptName,\r\n\t\t\t\t\tDeptFullName,\r\n\t\t\t\t\tDeptProp,\r\n\t\t\t\t\tDeptArea,\r\n\t\t\t\t\tCostCode,\r\n\t\t\t\t\tTypeID,\r\n\t\t\t\t\tDeptFlag,\r\n\t\t\t\t\tDeptDuty,\r\n\t\t\t\t\tDeptState,\r\n\t\t\t\t\tRemark,\r\n\t\t\t\t\tOrderID,\r\n\t\t\t\t\tCompanyID,\r\n\t\t\t\t\tCalendarId,\r\n\t\t\t\t\tPicPositionId,\r\n\t\t\t\t\tPicPosition,\t\t\t\t\t\r\n                    \r\n                    UpPositionId,\r\n\t\t\t\t\tUpPosition,\r\n\r\n                    UpEmployeeId,\r\n                    UpEmployeeName,\r\n                    PicEmployeeId2,\r\n                    PicEmployeeName2,\r\n                    LdapPath\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@DeptWBS,\r\n\t\t\t\t\t@DeptPWBS,\r\n\t\t\t\t\t@DeptCode,\r\n\t\t\t\t\t@DeptAbbr,\r\n\t\t\t\t\t@DeptName,\r\n\t\t\t\t\t@DeptFullName,\r\n\t\t\t\t\t@DeptProp,\r\n\t\t\t\t\t@DeptArea,\r\n\t\t\t\t\t@CostCode,\r\n\t\t\t\t\t@TypeID,\r\n\t\t\t\t\t@DeptFlag,\r\n\t\t\t\t\t@DeptDuty,\r\n\t\t\t\t\t@DeptState,\r\n\t\t\t\t\t@Remark,\r\n\t\t\t\t\t@OrderID,\r\n\t\t\t\t\t@CompanyId,\r\n\t\t\t\t\t@CalendarId,\r\n\t\t\t\t\t@PicPositionId,\r\n\t\t\t\t\t@PicPosition,\r\n                    @UpPositionId,\r\n\t\t\t\t\t@UpPosition,\r\n                    @UpEmployeeId,\r\n                    @UpEmployeeName,\r\n                    @PicEmployeeId2,\r\n                    @PicEmployeeName2,\r\n                    @LdapPath\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptWBS", DbType.String, model.DeptWBS);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptPWBS", DbType.String, model.DeptPWBS);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptCode", DbType.String, model.DeptCode);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptAbbr", DbType.String, model.DeptAbbr);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptName", DbType.String, model.DeptName);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptFullName", DbType.String, model.DeptFullName);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptProp", DbType.String, model.DeptProp);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptArea", DbType.String, model.DeptArea);
			SysDatabase.AddInParameter(sqlStringCommand, "CostCode", DbType.String, model.CostCode);
			SysDatabase.AddInParameter(sqlStringCommand, "TypeID", DbType.String, model.TypeID);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptFlag", DbType.Int32, model.DeptFlag);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptDuty", DbType.String, model.DeptDuty);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptState", DbType.String, model.DeptState);
			SysDatabase.AddInParameter(sqlStringCommand, "Remark", DbType.String, model.Remark);
			SysDatabase.AddInParameter(sqlStringCommand, "CompanyID", DbType.String, model.CompanyID);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderID", DbType.Int32, model.OrderID);
			SysDatabase.AddInParameter(sqlStringCommand, "PicPositionId", DbType.String, model.PicPositionId);
			SysDatabase.AddInParameter(sqlStringCommand, "PicPosition", DbType.String, model.PicPosition);
			SysDatabase.AddInParameter(sqlStringCommand, "UpPosition", DbType.String, model.UpPosition);
			SysDatabase.AddInParameter(sqlStringCommand, "UpPositionId", DbType.String, model.UpPositionId);
			SysDatabase.AddInParameter(sqlStringCommand, "UpEmployeeId", DbType.String, model.UpEmployeeId);
			SysDatabase.AddInParameter(sqlStringCommand, "UpEmployeeName", DbType.String, model.UpEmployeeName);
			SysDatabase.AddInParameter(sqlStringCommand, "PicEmployeeId2", DbType.String, model.PicEmployeeId2);
			SysDatabase.AddInParameter(sqlStringCommand, "PicEmployeeName2", DbType.String, model.PicEmployeeName2);
			SysDatabase.AddInParameter(sqlStringCommand, "LdapPath", DbType.String, model.LdapPath);
			SysDatabase.AddInParameter(sqlStringCommand, "CalendarId", DbType.String, model.CalendarId);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Org_Department ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public Department GetCompanyByDeptWbs(string deptWbs)
		{
			Department model;
			_DeptType __DeptType = new _DeptType();
			DataTable list = __DeptType.GetList(string.Format(" TypeProp in ({0},{1})", DeptType.CompanyTypeId, DeptType.GroupTypeId));
			DataTable dataTable = this.GetList(string.Concat("'", deptWbs, "' like deptwbs+'%' order by deptwbs desc"));
			foreach (DataRow row in dataTable.Rows)
			{
				if ((int)list.Select(string.Concat("_autoid='", row["TypeId"].ToString(), "'")).Length <= 0)
				{
					continue;
				}
				model = this.GetModel(row);
				return model;
			}
			model = null;
			return model;
		}

		public DataTable GetList(string strWhere)
		{
			DataTable dataTable;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  From T_E_Org_Department ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
			return dataTable;
		}

		public int GetMaxOrder(string pnodewbs)
		{
			object obj;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select max(OrderId) ");
			stringBuilder.Append(" From T_E_Org_Department ");
			stringBuilder.Append(string.Concat(" where DeptPWbs='", pnodewbs, "'"));
			SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			obj = (this.dbTransaction_0 == null ? SysDatabase.ExecuteScalar(stringBuilder.ToString()) : SysDatabase.ExecuteScalar(stringBuilder.ToString(), this.dbTransaction_0));
			return (obj == DBNull.Value ? 0 : Convert.ToInt32(obj));
		}

		public Department GetModel(string DeptId)
		{
			Department model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 d.*,t.TypeCode,t.TypeName from T_E_Org_Department d inner join T_E_Org_DeptType t on d.TypeID=t._AutoId");
			stringBuilder.Append(" where d._AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, DeptId);
			Department department = new Department();
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

		public Department GetModel(DataRow dataRow_0)
		{
			Department department = new Department()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				department._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				department._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				department._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			department.DeptWBS = dataRow_0["DeptWBS"].ToString();
			department.DeptPWBS = dataRow_0["DeptPWBS"].ToString();
			department.DeptCode = dataRow_0["DeptCode"].ToString();
			department.DeptAbbr = dataRow_0["DeptAbbr"].ToString();
			department.DeptName = dataRow_0["DeptName"].ToString();
			department.DeptFullName = dataRow_0["DeptFullName"].ToString();
			department.DeptProp = dataRow_0["DeptProp"].ToString();
			department.DeptArea = dataRow_0["DeptArea"].ToString();
			department.CostCode = dataRow_0["CostCode"].ToString();
			department.TypeID = dataRow_0["TypeID"].ToString();
			if (dataRow_0["DeptFlag"].ToString() != "")
			{
				department.DeptFlag = int.Parse(dataRow_0["DeptFlag"].ToString());
			}
			department.DeptDuty = dataRow_0["DeptDuty"].ToString();
			department.DeptState = dataRow_0["DeptState"].ToString();
			department.Remark = dataRow_0["Remark"].ToString();
			if (dataRow_0["OrderID"].ToString() != "")
			{
				department.OrderID = int.Parse(dataRow_0["OrderID"].ToString());
			}
			department.PicPositionId = dataRow_0["PicPositionId"].ToString();
			department.PicPosition = dataRow_0["PicPosition"].ToString();
			department.UpPositionId = dataRow_0["UpPositionId"].ToString();
			department.UpPosition = dataRow_0["UpPosition"].ToString();
			department.DeptAdminId = dataRow_0["DeptAdminId"].ToString();
			department.DeptAdminCn = dataRow_0["DeptAdminCn"].ToString();
			department.DeptSfwId = dataRow_0["DeptSfwId"].ToString();
			department.DeptSfwCn = dataRow_0["DeptSfwCn"].ToString();
			department.UpEmployeeId = dataRow_0["UpEmployeeId"].ToString();
			department.UpEmployeeName = dataRow_0["UpEmployeeName"].ToString();
			department.PicEmployeeId2 = dataRow_0["PicEmployeeId2"].ToString();
			department.PicEmployeeName2 = dataRow_0["PicEmployeeName2"].ToString();
			department.LdapPath = dataRow_0["LdapPath"].ToString();
			department.CompanyID = dataRow_0["CompanyID"].ToString();
			department.CalendarId = dataRow_0["CalendarId"].ToString();
			if (dataRow_0.Table.Columns.Contains("TypeCode"))
			{
				department.TypeCode = dataRow_0["TypeCode"].ToString();
			}
			return department;
		}

		public Department GetModelByCode(string DeptCode)
		{
			Department model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Org_Department ");
			stringBuilder.Append(" where DeptCode=@DeptCode ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "DeptCode", DbType.String, DeptCode);
			Department department = new Department();
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

		public Department GetModelByDeptName(string deptName, string companyId)
		{
			Department model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Org_Department ");
			stringBuilder.Append(" where DeptName=@DeptName and CompanyId=@CompanyId");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "DeptName", DbType.String, deptName);
			SysDatabase.AddInParameter(sqlStringCommand, "CompanyId", DbType.String, companyId);
			DataTable dataTable = new DataTable();
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

		public Department GetModelByName(string DeptName)
		{
			Department model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Org_Department ");
			stringBuilder.Append(" where DeptName=@DeptName ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "DeptName", DbType.String, DeptName);
			Department department = new Department();
			DataTable dataTable = new DataTable();
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

		public Department GetModelByWbs(string DeptWbs)
		{
			Department model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Org_Department ");
			stringBuilder.Append(" where DeptWBS=@DeptWBS ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "DeptWBS", DbType.String, DeptWbs);
			Department department = new Department();
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

		public List<Department> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Department> departments = new List<Department>();
			stringBuilder.Append("select *  FROM T_E_Org_Department ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				departments.Add(this.GetModel(row));
			}
			return departments;
		}

		public string GetNewCode(string pnodewbs)
		{
			object obj;
			string str;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select max(right(DeptWbs,4))+1 ");
			stringBuilder.Append(" From T_E_Org_Department ");
			stringBuilder.Append(string.Concat(" where DeptPWbs='", pnodewbs, "'"));
			obj = (this.dbTransaction_0 == null ? SysDatabase.ExecuteScalar(stringBuilder.ToString()) : SysDatabase.ExecuteScalar(stringBuilder.ToString(), this.dbTransaction_0));
			string str1 = "";
			str1 = (obj == DBNull.Value ? "0001" : obj.ToString());
			if (int.Parse(str1) <= 9999)
			{
				int num = Convert.ToInt32(str1);
				str = string.Concat(pnodewbs, num.ToString("d4"));
			}
			else
			{
				str = "";
			}
			return str;
		}

		public Department GetParentDeptModel(string deptId)
		{
			Department model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select * from T_E_Org_Department where DeptWBS=\r\n            (select DeptPWBS from T_E_Org_Department where _AutoID='{0}') ", deptId);
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
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

		public List<Department> GetSonDeptByWbs(string DeptWbs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Department> departments = new List<Department>();
			stringBuilder.Append("select *  From T_E_Org_Department ");
			stringBuilder.Append(string.Concat(" where DeptPWBS = '", DeptWbs, "' and _IsDel=0 order by orderId"));
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
			foreach (DataRow row in dataTable.Rows)
			{
				departments.Add(this.GetModel(row));
			}
			return departments;
		}

		public Department GetSubDeptByName(string deptName, string pwbs)
		{
			Department model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Org_Department ");
			stringBuilder.Append(" where DeptName=@DeptName and DeptPWbs=@DeptPWBS");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "DeptName", DbType.String, deptName);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptPWBS", DbType.String, pwbs);
			DataTable dataTable = new DataTable();
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

		public List<Department> GetSubDeptByWbs(string DeptWbs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Department> departments = new List<Department>();
			stringBuilder.Append("select *  From T_E_Org_Department ");
			stringBuilder.Append(string.Concat(" where DeptWBS like '", DeptWbs, "%' and _IsDel=0 order by DeptPWBS,orderId"));
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
			foreach (DataRow row in dataTable.Rows)
			{
				departments.Add(this.GetModel(row));
			}
			return departments;
		}

		public int GetSubDeptCount(string DeptWbs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(*)  From T_E_Org_Department ");
			stringBuilder.Append(string.Concat(" where DeptWBS like '", DeptWbs, "%' and _IsDel=0 "));
			object obj = SysDatabase.ExecuteScalar(stringBuilder.ToString());
			if ((obj == null ? true : obj == DBNull.Value))
			{
				throw new Exception("Error in GetSubDeptCount");
			}
			return Convert.ToInt32(obj);
		}

		public Department GetTopLevelDeptModel(string deptId)
		{
			Department model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("declare @deptwbs varchar(50)\r\n            select @deptwbs=deptwbs from T_E_Org_Department where _autoId='{0}'\r\n            select Top 1 d.* from T_E_Org_Department d inner join T_E_Org_DeptType t\r\n            on d.TypeID=t._AutoID where t.TypeCode='BM' and  @deptwbs like d.DeptWBS+'%' order by deptwbs desc", deptId);
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
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

		public bool IsCompanyExist(string companyName)
		{
			object obj;
			bool flag;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select count(*)  From T_E_Org_Department where DeptName = '{0}'", companyName);
			obj = (this.dbTransaction_0 == null ? SysDatabase.ExecuteScalar(stringBuilder.ToString()) : SysDatabase.ExecuteScalar(stringBuilder.ToString(), this.dbTransaction_0));
			flag = ((obj == DBNull.Value ? true : obj == null) ? true : Convert.ToInt32(obj) > 0);
			return flag;
		}

		public bool IsDeptExist(string deptName, string companyId)
		{
			object obj;
			bool flag;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select count(*)  From T_E_Org_Department where DeptName = '{0}' and CompanyId='{1}'", deptName, companyId);
			obj = (this.dbTransaction_0 == null ? SysDatabase.ExecuteScalar(stringBuilder.ToString()) : SysDatabase.ExecuteScalar(stringBuilder.ToString(), this.dbTransaction_0));
			flag = ((obj == DBNull.Value ? true : obj == null) ? true : Convert.ToInt32(obj) > 0);
			return flag;
		}

		public int Update(Department model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Org_Department set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\tDeptCode=@DeptCode,\r\n\t\t\t\t\tDeptWBS=@DeptWBS,\r\n\t\t\t\t\tDeptPWBS=@DeptPWBS,\r\n\t\t\t\t\tDeptAbbr=@DeptAbbr,\r\n\t\t\t\t\tDeptName=@DeptName,\r\n\t\t\t\t\tDeptFullName=@DeptFullName,\r\n\t\t\t\t\tDeptProp=@DeptProp,\r\n\r\n\t\t\t\t\tDeptArea=@DeptArea,\r\n\t\t\t\t\tCostCode=@CostCode,\r\n\t\t\t\t\tTypeID=@TypeID,\r\n\t\t\t\t\tDeptFlag=@DeptFlag,\r\n\t\t\t\t\tDeptDuty=@DeptDuty,\r\n\t\t\t\t\tDeptState=@DeptState,\r\n\t\t\t\t\tRemark=@Remark,\r\n\t\t\t\t\tOrderID=@OrderID,\r\n\r\n\t\t\t\t\tPicPositionId=@PicPositionId,\r\n\t\t\t\t\tPicPosition=@PicPosition,\r\n\r\n\t\t\t\t\tUpPositionId=@UpPositionId,\r\n\t\t\t\t\tUpPosition=@UpPosition,\r\n\r\n\t\t\t\t\tUpEmployeeId=@UpEmployeeId,\r\n\t\t\t\t\tUpEmployeeName=@UpEmployeeName,\r\n\r\n\t\t\t\t\tPicEmployeeId2=@PicEmployeeId2,\r\n\t\t\t\t\tPicEmployeeName2=@PicEmployeeName2,\r\n\r\n                    DeptSfwId=@DeptSfwId,\r\n                    DeptSfwCn=@DeptSfwCn,\r\n\r\n\t\t\t\t\tLdapPath=@LdapPath,\r\n\t\t\t\t\tCompanyID=@CompanyID,\r\n\t\t\t\t\tCalendarId=@CalendarId\r\n\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptAbbr", DbType.String, model.DeptAbbr);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptWBS", DbType.String, model.DeptWBS);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptPWBS", DbType.String, model.DeptPWBS);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptCode", DbType.String, model.DeptCode);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptName", DbType.String, model.DeptName);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptFullName", DbType.String, model.DeptFullName);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptProp", DbType.String, model.DeptProp);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptArea", DbType.String, model.DeptArea);
			SysDatabase.AddInParameter(sqlStringCommand, "CostCode", DbType.String, model.CostCode);
			SysDatabase.AddInParameter(sqlStringCommand, "TypeID", DbType.String, model.TypeID);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptFlag", DbType.Int32, model.DeptFlag);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptDuty", DbType.String, model.DeptDuty);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptState", DbType.String, model.DeptState);
			SysDatabase.AddInParameter(sqlStringCommand, "Remark", DbType.String, model.Remark);
			SysDatabase.AddInParameter(sqlStringCommand, "CompanyID", DbType.String, model.CompanyID);
			SysDatabase.AddInParameter(sqlStringCommand, "CalendarId", DbType.String, model.CalendarId);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderID", DbType.Int32, model.OrderID);
			SysDatabase.AddInParameter(sqlStringCommand, "PicPositionId", DbType.String, model.PicPositionId);
			SysDatabase.AddInParameter(sqlStringCommand, "PicPosition", DbType.String, model.PicPosition);
			SysDatabase.AddInParameter(sqlStringCommand, "UpPosition", DbType.String, model.UpPosition);
			SysDatabase.AddInParameter(sqlStringCommand, "UpPositionId", DbType.String, model.UpPositionId);
			SysDatabase.AddInParameter(sqlStringCommand, "UpEmployeeId", DbType.String, model.UpEmployeeId);
			SysDatabase.AddInParameter(sqlStringCommand, "UpEmployeeName", DbType.String, model.UpEmployeeName);
			SysDatabase.AddInParameter(sqlStringCommand, "PicEmployeeId2", DbType.String, model.PicEmployeeId2);
			SysDatabase.AddInParameter(sqlStringCommand, "PicEmployeeName2", DbType.String, model.PicEmployeeName2);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptSfwId", DbType.String, model.DeptSfwId);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptSfwCn", DbType.String, model.DeptSfwCn);
			SysDatabase.AddInParameter(sqlStringCommand, "LdapPath", DbType.String, model.LdapPath);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}