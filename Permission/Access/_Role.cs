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
	public class _Role
	{
		private DbTransaction dbTransaction_0 = null;

		public _Role()
		{
		}

		public _Role(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(Role model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Org_Role (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tRoleName,\r\n\t\t\t\t\tRoleState,\r\n\t\t\t\t\tRoleNotes,\r\n\t\t\t\t\tCatalogID,\r\n\t\t\t\t\tCompanyId,\r\n\t\t\t\t\tSearchScope,\r\n\t\t\t\t\tRoleType,\r\n\t\t\t\t\tOrderID\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@RoleName,\r\n\t\t\t\t\t@RoleState,\r\n\t\t\t\t\t@RoleNotes,\r\n\t\t\t\t\t@CatalogID,\r\n\t\t\t\t\t@CompanyId,\r\n\t\t\t\t\t@SearchScope,\r\n\t\t\t\t\t@RoleType,\r\n\t\t\t\t\t@OrderID\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "RoleName", DbType.String, model.RoleName);
			SysDatabase.AddInParameter(sqlStringCommand, "RoleState", DbType.Int32, model.RoleState);
			SysDatabase.AddInParameter(sqlStringCommand, "RoleNotes", DbType.String, model.RoleNotes);
			SysDatabase.AddInParameter(sqlStringCommand, "CatalogID", DbType.String, model.CatalogID);
			SysDatabase.AddInParameter(sqlStringCommand, "CompanyId", DbType.String, model.CompanyId);
			SysDatabase.AddInParameter(sqlStringCommand, "SearchScope", DbType.String, model.SearchScope);
			SysDatabase.AddInParameter(sqlStringCommand, "RoleType", DbType.String, model.RoleType);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderID", DbType.Int32, model.OrderID);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Org_Role ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int GetEmployeeCountByRoleId(string roleId)
		{
			string str = string.Format("select count(*) from T_E_Org_RoleEmployee where _isDel=0 and  RoleID='{0}'", roleId);
			return Convert.ToInt32(SysDatabase.ExecuteScalar(str));
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Org_Role ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public Role GetModel(string string_0)
		{
			Role model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Org_Role ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			Role role = new Role();
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

		public Role GetModel(DataRow dataRow_0)
		{
			Role role = new Role()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				role._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				role._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				role._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			role.RoleName = dataRow_0["RoleName"].ToString();
			if (dataRow_0["RoleState"].ToString() != "")
			{
				role.RoleState = int.Parse(dataRow_0["RoleState"].ToString());
			}
			role.RoleNotes = dataRow_0["RoleNotes"].ToString();
			role.CatalogID = dataRow_0["CatalogID"].ToString();
			role.CompanyId = dataRow_0["CompanyId"].ToString();
			role.SearchScope = dataRow_0["SearchScope"].ToString();
			role.RoleType = dataRow_0["RoleType"].ToString();
			if (dataRow_0["OrderID"].ToString() != "")
			{
				role.OrderID = int.Parse(dataRow_0["OrderID"].ToString());
			}
			return role;
		}

		public List<Role> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Role> roles = new List<Role>();
			stringBuilder.Append("select *  From T_E_Org_Role ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				roles.Add(this.GetModel(row));
			}
			return roles;
		}

		public List<Role> GetModelListByCatId(string catId)
		{
			return this.GetModelList(string.Concat(" CatalogID ='", catId, "'"));
		}

		public int GetPositionCountByRoleId(string roleId)
		{
			string str = string.Format("select count(*) from T_E_Org_Position where  PropId='{0}'", roleId);
			return Convert.ToInt32(SysDatabase.ExecuteScalar(str));
		}

		public int Update(Role model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Org_Role set \r\n\t\t\t\t\t_UserName=@_UserName,\r\n\t\t\t\t\t_OrgCode=@_OrgCode,\r\n\t\t\t\t\t_CreateTime=@_CreateTime,\r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tRoleName=@RoleName,\r\n\t\t\t\t\tRoleState=@RoleState,\r\n\t\t\t\t\tRoleNotes=@RoleNotes,\r\n\t\t\t\t\tCatalogID=@CatalogID,\r\n\t\t\t\t\tSearchScope=@SearchScope,\r\n\t\t\t\t\tRoleType=@RoleType,\r\n\t\t\t\t\tOrderID=@OrderID\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "RoleName", DbType.String, model.RoleName);
			SysDatabase.AddInParameter(sqlStringCommand, "RoleState", DbType.Int32, model.RoleState);
			SysDatabase.AddInParameter(sqlStringCommand, "RoleNotes", DbType.String, model.RoleNotes);
			SysDatabase.AddInParameter(sqlStringCommand, "CatalogID", DbType.String, model.CatalogID);
			SysDatabase.AddInParameter(sqlStringCommand, "SearchScope", DbType.String, model.SearchScope);
			SysDatabase.AddInParameter(sqlStringCommand, "RoleType", DbType.String, model.RoleType);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderID", DbType.Int32, model.OrderID);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}