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
	public class _User
	{
		private DbTransaction dbTransaction_0 = null;

		public _User()
		{
		}

		public _User(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(LoginUser model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Org_User (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tLoginName,\r\n\t\t\t\t\tLoginPass,\r\n\t\t\t\t\tCompanyId,\r\n\t\t\t\t\tIsOnline,\r\n\t\t\t\t\tIsLock,\r\n\t\t\t\t\tLockReason,\r\n\t\t\t\t\tLoginType,\r\n\t\t\t\t\tCanMultiLogin,\r\n\t\t\t\t\tRemark\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@LoginName,\r\n\t\t\t\t\t@LoginPass,\r\n\t\t\t\t\t@CompanyId,\r\n\t\t\t\t\t@IsOnline,\r\n\t\t\t\t\t@IsLock,\r\n\t\t\t\t\t@LockReason,\r\n\t\t\t\t\t@LoginType,\r\n\t\t\t\t\t@CanMultiLogin,\r\n\t\t\t\t\t@Remark\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "LoginName", DbType.String, model.LoginName);
			SysDatabase.AddInParameter(sqlStringCommand, "LoginPass", DbType.String, model.LoginPass);
			SysDatabase.AddInParameter(sqlStringCommand, "CompanyId", DbType.String, model.CompanyId);
			SysDatabase.AddInParameter(sqlStringCommand, "IsOnline", DbType.Int32, model.IsOnline);
			SysDatabase.AddInParameter(sqlStringCommand, "IsLock", DbType.Int32, model.IsLock);
			SysDatabase.AddInParameter(sqlStringCommand, "LockReason", DbType.String, model.LockReason);
			SysDatabase.AddInParameter(sqlStringCommand, "LoginType", DbType.Int32, model.LoginType);
			SysDatabase.AddInParameter(sqlStringCommand, "CanMultiLogin", DbType.Int32, model.CanMultiLogin);
			SysDatabase.AddInParameter(sqlStringCommand, "Remark", DbType.String, model.Remark);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int ChangePass(string LoginName, string newPass)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update  T_E_Org_User set ");
			stringBuilder.Append(" LoginPass=@LoginPass ");
			stringBuilder.Append(" where LoginName=@LoginName ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@LoginName", DbType.String, LoginName);
			SysDatabase.AddInParameter(sqlStringCommand, "@LoginPass", DbType.String, newPass);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Org_User ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public LoginUser GetCompanyAdmin(string userName)
		{
			LoginUser model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Org_User ");
			stringBuilder.Append(" where LoginName=@LoginName and LoginType=2");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@LoginName", DbType.String, userName);
			LoginUser loginUser = new LoginUser();
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

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Org_User ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public LoginUser GetModel(string userName)
		{
			LoginUser model;
			StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select *  from T_E_Org_User where LoginName=@LoginName ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@LoginName", DbType.String, userName);
			LoginUser loginUser = new LoginUser();
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


        public string GetFileId(string AppId)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select top 1 _AutoID from T_E_File_File ");
            stringBuilder.Append(" where AppId=@AppId and AppName='T_E_Org_User' ");
            stringBuilder.Append(" order by _createtime desc ");
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
            SysDatabase.AddInParameter(sqlStringCommand, "@AppId", DbType.String, AppId);
            LoginUser loginUser = new LoginUser();
            DataTable dataTable = SysDatabase.ExecuteTable(sqlStringCommand);
            string fileid = "";
            if (dataTable.Rows.Count > 0)
            {
                fileid = dataTable.Rows[0][0].ToString();
            }
            return fileid;
        }

		public LoginUser GetModel(DataRow dataRow_0)
		{
			LoginUser loginUser = new LoginUser()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				loginUser._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				loginUser._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				loginUser._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			loginUser.LoginName = dataRow_0["LoginName"].ToString();
			loginUser.LoginPass = dataRow_0["LoginPass"].ToString();
			loginUser.CompanyId = dataRow_0["CompanyId"].ToString();
			if (dataRow_0["IsOnline"].ToString() != "")
			{
				loginUser.IsOnline = int.Parse(dataRow_0["IsOnline"].ToString());
			}
			if (dataRow_0["IsLock"].ToString() != "")
			{
				loginUser.IsLock = int.Parse(dataRow_0["IsLock"].ToString());
			}
			loginUser.LockReason = dataRow_0["LockReason"].ToString();
			if (dataRow_0["LoginType"].ToString() != "")
			{
				loginUser.LoginType = int.Parse(dataRow_0["LoginType"].ToString());
			}
            loginUser.PhotoId = "";
            if (dataRow_0.Table.Columns.Contains("PhotoId"))
            {
                if (dataRow_0["PhotoId"].ToString() != "")
                {
                    loginUser.PhotoId = GetFileId(dataRow_0["PhotoId"].ToString());
                }
            }

            if (dataRow_0["CanMultiLogin"].ToString() != "")
			{
				loginUser.CanMultiLogin = int.Parse(dataRow_0["CanMultiLogin"].ToString());
			}

            loginUser.EmployeeName = dataRow_0["LoginName"].ToString();
            if (dataRow_0.Table.Columns.Contains("EmployeeName"))
            {
                loginUser.EmployeeName = dataRow_0["EmployeeName"].ToString();
            }

            loginUser.Cellphone = "";
            if (dataRow_0.Table.Columns.Contains("Cellphone"))
            {
                loginUser.Cellphone = dataRow_0["Cellphone"].ToString();
            }

			loginUser.Remark = dataRow_0["Remark"].ToString();
			return loginUser;
		}

		public LoginUser GetModelByCompanyId(string companyId)
		{
			LoginUser model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Org_User ");
			stringBuilder.Append(" where companyId=@companyId ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@companyId", DbType.String, companyId);
			LoginUser loginUser = new LoginUser();
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

		public List<LoginUser> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<LoginUser> loginUsers = new List<LoginUser>();
			stringBuilder.Append("select *  FROM T_E_Org_User ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				loginUsers.Add(this.GetModel(row));
			}
			return loginUsers;
		}

		public bool IsExist(string LoginName)
		{
			bool flag;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select count(*)  FROM T_E_Org_User where LoginName = '{0}'", LoginName);
			object obj = SysDatabase.ExecuteScalar(stringBuilder.ToString());
			flag = (obj == DBNull.Value ? true : Convert.ToInt32(obj) > 0);
			return flag;
		}

		public bool IsExist(string LoginName, string CompanyId)
		{
			bool flag;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select count(*)  FROM T_E_Org_User where LoginName = '{0}' and isnull(CompanyId,'')<>'{1}'", LoginName, CompanyId);
			object obj = SysDatabase.ExecuteScalar(stringBuilder.ToString());
			flag = (obj == DBNull.Value ? true : Convert.ToInt32(obj) > 0);
			return flag;
		}

		public int Update(LoginUser model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Org_User set \r\n\r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\r\n\t\t\t\t\tLoginName=@LoginName,\r\n                    LoginPass=@LoginPass,\r\n\t\t\t\t\tCompanyId=@CompanyId,\r\n\t\t\t\t\tIsOnline=@IsOnline,\r\n\t\t\t\t\tIsLock=@IsLock,\r\n\t\t\t\t\tLockReason=@LockReason,\r\n\t\t\t\t\tLoginType=@LoginType,\r\n\t\t\t\t\tCanMultiLogin=@CanMultiLogin,\r\n\t\t\t\t\tRemark=@Remark\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "LoginName", DbType.String, model.LoginName);
			SysDatabase.AddInParameter(sqlStringCommand, "LoginPass", DbType.String, model.LoginPass);
			SysDatabase.AddInParameter(sqlStringCommand, "CompanyId", DbType.String, model.CompanyId);
			SysDatabase.AddInParameter(sqlStringCommand, "IsOnline", DbType.Int32, model.IsOnline);
			SysDatabase.AddInParameter(sqlStringCommand, "IsLock", DbType.Int32, model.IsLock);
			SysDatabase.AddInParameter(sqlStringCommand, "LockReason", DbType.String, model.LockReason);
			SysDatabase.AddInParameter(sqlStringCommand, "LoginType", DbType.Int32, model.LoginType);
			SysDatabase.AddInParameter(sqlStringCommand, "CanMultiLogin", DbType.Int32, model.CanMultiLogin);
			SysDatabase.AddInParameter(sqlStringCommand, "Remark", DbType.String, model.Remark);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}