using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace WebBase.JZY.Tools
{
    
	public  class WebTools
	{
		public WebTools()
		{
		}

		public static void ChangeDefaultPosition(string deptEmployeeId)
		{
			DeptEmployee deptEmployeeById = DeptEmployeeService.GetDeptEmployeeById(deptEmployeeId);
			DeptEmployeeService.UpdateDefaultPosition(deptEmployeeById.EmployeeID, deptEmployeeById.PositionId);
			Position defaultPositionById = EmployeeService.GetDefaultPositionById(deptEmployeeById.EmployeeID);
			Department model = DepartmentService.GetModel(defaultPositionById.DeptID);
			Department companyByDeptId = DepartmentService.GetCompanyByDeptId(model._AutoID);
			HttpContext.Current.Session["CompanyId"] = companyByDeptId._AutoID;
			HttpContext.Current.Session["CompanyCode"] = companyByDeptId.DeptCode;
			HttpContext.Current.Session["CompanyWbs"] = companyByDeptId.DeptWBS;
			HttpContext.Current.Session["CompanyName"] = companyByDeptId.DeptName;
			HttpContext.Current.Session["CompanyAbbr"] = companyByDeptId.DeptAbbr;
			HttpContext.Current.Session["CompanyTypeId"] = companyByDeptId.TypeID;
			HttpContext.Current.Session["DeptId"] = model._AutoID;
			HttpContext.Current.Session["DeptWbs"] = model.DeptWBS;
			HttpContext.Current.Session["DeptCode"] = model.DeptCode;
			HttpContext.Current.Session["DeptName"] = model.DeptName;
			if (model.DeptFullName.Trim() == "")
			{
				model.DeptFullName = model.DeptName;
			}
			HttpContext.Current.Session["DeptFullName"] = model.DeptFullName;
			Department topLevelDeptModel = (new _Department()).GetTopLevelDeptModel(model._AutoID);
			if (topLevelDeptModel == null)
			{
				HttpContext.Current.Session["TopDeptId"] = "";
				HttpContext.Current.Session["TopDeptWbs"] = "";
				HttpContext.Current.Session["TopDeptCode"] = "";
				HttpContext.Current.Session["TopDeptName"] = "";
				HttpContext.Current.Session["TopDeptFullName"] = "";
			}
			else
			{
				HttpContext.Current.Session["TopDeptId"] = topLevelDeptModel._AutoID;
				HttpContext.Current.Session["TopDeptWbs"] = topLevelDeptModel.DeptWBS;
				HttpContext.Current.Session["TopDeptCode"] = topLevelDeptModel.DeptCode;
				HttpContext.Current.Session["TopDeptName"] = topLevelDeptModel.DeptName;
				if (topLevelDeptModel.DeptFullName.Trim() == "")
				{
					topLevelDeptModel.DeptFullName = topLevelDeptModel.DeptName;
				}
				HttpContext.Current.Session["TopDeptFullName"] = topLevelDeptModel.DeptFullName;
			}
			if (defaultPositionById != null)
			{
				HttpContext.Current.Session["PositionId"] = defaultPositionById._AutoID;
				HttpContext.Current.Session["PositionCode"] = defaultPositionById.PositionCode;
				HttpContext.Current.Session["PositionName"] = defaultPositionById.PositionName;
			}
			else
			{
				HttpContext.Current.Session["PositionId"] = "";
				HttpContext.Current.Session["PositionCode"] = "";
				HttpContext.Current.Session["PositionName"] = "";
			}
		}

		public static void ChangePosition(string deptEmployeeId)
		{
			DeptEmployee deptEmployeeById = DeptEmployeeService.GetDeptEmployeeById(deptEmployeeId);
			Department model = DepartmentService.GetModel(deptEmployeeById.DeptID);
			HttpContext.Current.Session["EmployeeId"] = deptEmployeeById.EmployeeID;
			HttpContext.Current.Session["EmployeeName"] = deptEmployeeById.EmployeeName;
			Department department = DepartmentService.GetModel(deptEmployeeById.CompanyID);
			HttpContext.Current.Session["CompanyId"] = department._AutoID;
			HttpContext.Current.Session["CompanyCode"] = department.DeptCode;
			HttpContext.Current.Session["CompanyWbs"] = department.DeptWBS;
			HttpContext.Current.Session["CompanyName"] = department.DeptName;
			HttpContext.Current.Session["DeptId"] = model._AutoID;
			HttpContext.Current.Session["DeptCode"] = model.DeptCode;
			HttpContext.Current.Session["DeptWbs"] = model.DeptWBS;
			HttpContext.Current.Session["DeptName"] = model.DeptName;
			if (model.DeptFullName.Trim() == "")
			{
				model.DeptFullName = model.DeptName;
			}
			HttpContext.Current.Session["DeptFullName"] = model.DeptFullName;
			Department topLevelDeptModel = (new _Department()).GetTopLevelDeptModel(model._AutoID);
			if (topLevelDeptModel == null)
			{
				HttpContext.Current.Session["TopDeptId"] = "";
				HttpContext.Current.Session["TopDeptWbs"] = "";
				HttpContext.Current.Session["TopDeptCode"] = "";
				HttpContext.Current.Session["TopDeptName"] = "";
				HttpContext.Current.Session["TopDeptFullName"] = "";
			}
			else
			{
				HttpContext.Current.Session["TopDeptId"] = topLevelDeptModel._AutoID;
				HttpContext.Current.Session["TopDeptWbs"] = topLevelDeptModel.DeptWBS;
				HttpContext.Current.Session["TopDeptCode"] = topLevelDeptModel.DeptCode;
				HttpContext.Current.Session["TopDeptName"] = topLevelDeptModel.DeptName;
				if (topLevelDeptModel.DeptFullName.Trim() == "")
				{
					topLevelDeptModel.DeptFullName = topLevelDeptModel.DeptName;
				}
				HttpContext.Current.Session["TopDeptFullName"] = topLevelDeptModel.DeptFullName;
			}
			HttpContext.Current.Session["PositionId"] = deptEmployeeById.PositionId;
			HttpContext.Current.Session["PositionName"] = deptEmployeeById.PositionName;
		}

		public static void DeleteRemind(string empId, string appName, string appId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Delete T_OA_Reminded where appid='{0}' and appname='{1}' and employeeId='{2}'", appId, appName, empId);
			SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
		}

		public static DataTable GetAppRead(string appName, string appId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select * , e.employeeName from T_OA_Read r inner join T_E_Org_Employee e on r.EmployeeId=e._autoId\r\n                where r.appid='{0}' and r.appname='{1}'", appId, appName);
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public static int GetNewsToReadCount(string employeeId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(*) from T_OA_News ");
			stringBuilder.AppendFormat(" where _isdel=0 and NewsState='是' and _AutoId not in (select AppID from T_OA_Read r where r.AppName='T_OA_News' and r.EmployeeId='{0}')", employeeId);
			return Convert.ToInt32(SysDatabase.ExecuteScalar(stringBuilder.ToString()));
		}

		public static int GetNoteToReadCount(string employeeId, string orgcode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(*) from T_OA_Note ");
			stringBuilder.AppendFormat(" where _isdel=0 and NewsState='是' and ((datalength(ScopeId)=0 and datalength(OrgScopeId)=0) or  patIndex('%{0}%',ScopeId)>0 or patIndex('%{1}%',OrgScopeId)>0)\r\n            and _AutoId not in (select AppID from T_OA_Read r where r.AppName='T_OA_Note' and r.EmployeeId='{0}')", employeeId, orgcode);
			return Convert.ToInt32(SysDatabase.ExecuteScalar(stringBuilder.ToString()));
		}

		public static UserContext GetUserInfo(string deptEmployeeId)
		{
			UserContext userInfo = EIS.Permission.Utility.GetUserInfo(deptEmployeeId, HttpContext.Current.Session["webId"].ToString());
			return userInfo;
		}
        public static UserContext GetUserInfo(string deptEmployeeId, string webId)
        {
            UserContext userInfo = EIS.Permission.Utility.GetUserInfo(deptEmployeeId, webId);
            return userInfo;
        }

		public static void InsertComment(OAComment model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_OA_Comment  (\r\n \t\t\t\t\t    _AutoID,\r\n\t\t\t\t\t    _UserName,\r\n\t\t\t\t\t    _OrgCode,\r\n\t\t\t\t\t    _CreateTime,\r\n\t\t\t\t\t    _UpdateTime,\r\n\t\t\t\t\t    _IsDel,\r\n\t\t\t\t\t    EmployeeName,\r\n\t\t\t\t\t    AddTime,\r\n\t\t\t\t\t    Content,\r\n\t\t\t\t\t    AppID,\r\n                        AppName\r\n\t\t\t    ) values(\r\n\t\t\t\t\t    @_AutoID,\r\n\t\t\t\t\t    @_UserName,\r\n\t\t\t\t\t    @_OrgCode,\r\n\t\t\t\t\t    @_CreateTime,\r\n\t\t\t\t\t    @_UpdateTime,\r\n\t\t\t\t\t    @_IsDel,\r\n\t\t\t\t\t    @EmployeeName,\r\n\t\t\t\t\t    @AddTime,\r\n\t\t\t\t\t    @Content,\r\n\t\t\t\t\t    @AppID,\r\n\t\t\t\t\t    @AppName\r\n\t\t\t    )");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "EmployeeName", DbType.String, model.EmployeeName);
			SysDatabase.AddInParameter(sqlStringCommand, "AddTime", DbType.DateTime, model.AddTime);
			SysDatabase.AddInParameter(sqlStringCommand, "Content", DbType.String, model.Content);
			SysDatabase.AddInParameter(sqlStringCommand, "AppID", DbType.String, model.AppID);
			SysDatabase.AddInParameter(sqlStringCommand, "AppName", DbType.String, model.AppName);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public static bool IsRead(string empId, string appName, string appId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(*) from T_OA_Read ");
			stringBuilder.AppendFormat(" where _isdel=0 and AppId='{0}' and AppName='{1}' and EmployeeId='{2}'", appId, appName, empId);
			return Convert.ToInt32(SysDatabase.ExecuteScalar(stringBuilder.ToString())) > 0;
		}

		public static bool IsRemind(string empId, string appName, string appId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(*) from T_OA_Reminded ");
			stringBuilder.AppendFormat(" where _isdel=0 and AppId='{0}' and AppName='{1}' and EmployeeId='{2}'", appId, appName, empId);
			return Convert.ToInt32(SysDatabase.ExecuteScalar(stringBuilder.ToString())) > 0;
		}

		public static void OnAuthenticated(string userName)
		{
			Employee modelByLoginName = (new _Employee()).GetModelByLoginName(userName);
			Position defaultPositionById = EmployeeService.GetDefaultPositionById(modelByLoginName._AutoID);
			Department model = DepartmentService.GetModel(defaultPositionById.DeptID);
			HttpContext.Current.Session["UserName"] = userName;
			HttpContext.Current.Session["EmployeeId"] = modelByLoginName._AutoID;
			HttpContext.Current.Session["EmployeeCode"] = modelByLoginName.EmployeeCode;
			HttpContext.Current.Session["EmployeeName"] = modelByLoginName.EmployeeName;
			Department companyByDeptId = DepartmentService.GetCompanyByDeptId(model._AutoID);
			HttpContext.Current.Session["CompanyId"] = companyByDeptId._AutoID;
			HttpContext.Current.Session["CompanyCode"] = companyByDeptId.DeptCode;
			HttpContext.Current.Session["CompanyWbs"] = companyByDeptId.DeptWBS;
			HttpContext.Current.Session["CompanyName"] = companyByDeptId.DeptName;
			HttpContext.Current.Session["CompanyAbbr"] = companyByDeptId.DeptAbbr;
			HttpContext.Current.Session["CompanyTypeId"] = companyByDeptId.TypeID;
			HttpContext.Current.Session["CompanyTypeCode"] = companyByDeptId.TypeCode;
			HttpContext.Current.Session["DeptId"] = model._AutoID;
			HttpContext.Current.Session["DeptWbs"] = model.DeptWBS;
			HttpContext.Current.Session["DeptCode"] = model.DeptCode;
			HttpContext.Current.Session["DeptName"] = model.DeptName;
			if (model.DeptFullName.Trim() == "")
			{
				model.DeptFullName = model.DeptName;
			}
			HttpContext.Current.Session["DeptFullName"] = model.DeptFullName;
			Department topLevelDeptModel = (new _Department()).GetTopLevelDeptModel(model._AutoID);
			if (topLevelDeptModel == null)
			{
				HttpContext.Current.Session["TopDeptId"] = "";
				HttpContext.Current.Session["TopDeptWbs"] = "";
				HttpContext.Current.Session["TopDeptCode"] = "";
				HttpContext.Current.Session["TopDeptName"] = "";
				HttpContext.Current.Session["TopDeptFullName"] = "";
			}
			else
			{
				HttpContext.Current.Session["TopDeptId"] = topLevelDeptModel._AutoID;
				HttpContext.Current.Session["TopDeptWbs"] = topLevelDeptModel.DeptWBS;
				HttpContext.Current.Session["TopDeptCode"] = topLevelDeptModel.DeptCode;
				HttpContext.Current.Session["TopDeptName"] = topLevelDeptModel.DeptName;
				if (topLevelDeptModel.DeptFullName.Trim() == "")
				{
					topLevelDeptModel.DeptFullName = topLevelDeptModel.DeptName;
				}
				HttpContext.Current.Session["TopDeptFullName"] = topLevelDeptModel.DeptFullName;
			}
			if (defaultPositionById != null)
			{
				HttpContext.Current.Session["PositionId"] = defaultPositionById._AutoID;
				HttpContext.Current.Session["PositionCode"] = defaultPositionById.PositionCode;
				HttpContext.Current.Session["PositionName"] = defaultPositionById.PositionName;
			}
			else
			{
				HttpContext.Current.Session["PositionId"] = "";
				HttpContext.Current.Session["PositionCode"] = "";
				HttpContext.Current.Session["PositionName"] = "";
			}
			List<DeptEmployee> deptEmployeeByEmployeeId = DeptEmployeeService.GetDeptEmployeeByEmployeeId(modelByLoginName._AutoID);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DeptEmployee deptEmployee in deptEmployeeByEmployeeId)
			{
				stringBuilder.AppendFormat("'{0}',", deptEmployee.PositionId);
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length = stringBuilder.Length - 1;
			}
			HttpContext.Current.Session["PositionIdList"] = stringBuilder.ToString();
			HttpContext.Current.Session["LastLoginTime"] = modelByLoginName.LastLoginTime;
			HttpContext.Current.Session["LoginCount"] = modelByLoginName.LoginCount;
			HttpContext.Current.Session["webId"] = AppSettings.Instance.WebId;
			EmployeeService.UpdateLoginCount(modelByLoginName._AutoID);

            GetDefalutSession();
            GetDefalutProjectID();
        }

        #region

        /// <summary>
        /// 获取默认项目信息,，切换项目管理
        /// </summary>
        /// <returns></returns>
        public static void GetDefalutProjectID()
        {
            if (HttpContext.Current.Session["projectid"] == null)
            {
                HttpContext.Current.Session["projectname"] = "";
                HttpContext.Current.Session["projectcode"] = "";
                HttpContext.Current.Session["projectid"] = "";
            }
            if (HttpContext.Current.Session["projectid"].ToString() == "")
            {
                try
                {
                    DataSet ds = new DataSet();
                    //所属项目信息
                    string sql = @"select * from T_SG_Basic_ProjectInfo 
                    where projectid in (select projectid from T_SG_Basic_EmployeeDefaultProject 
                            WHERE  EmployeeID = @employeeid)  order by _CreateTime desc ";
                    System.Data.Common.DbCommand command = SysDatabase.GetSqlStringCommand(sql);
                    SysDatabase.AddInParameter(command, "@employeeid", DbType.String, HttpContext.Current.Session["employeeid"].ToString());
                    ds = SysDatabase.ExecuteDataSet(command);
                    if ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
                    {
                        //更换项目信息
                        HttpContext.Current.Session["projectname"] = ds.Tables[0].Rows[0]["projectname"].ToString();
                        HttpContext.Current.Session["projectcode"] = ds.Tables[0].Rows[0]["projectcode"].ToString();
                        HttpContext.Current.Session["projectid"] = ds.Tables[0].Rows[0]["projectid"].ToString();
                    }
                    else
                    {
                        //所属项目信息
                        string strprojectid = "select ProjectID from [dbo].[getPermissionProjectByEmployeeID]('" + HttpContext.Current.Session["employeeid"].ToString() + "')";
                        string def_sql = "select * from T_SG_Basic_ProjectInfo where projectid in (" + strprojectid + ")  order by _CreateTime desc  ";
                        System.Data.Common.DbCommand def_cmd = SysDatabase.GetSqlStringCommand(def_sql);
                        ds = SysDatabase.ExecuteDataSet(def_cmd);
                        if ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
                        {
                            HttpContext.Current.Session["projectname"] = ds.Tables[0].Rows[0]["projectname"].ToString();
                            HttpContext.Current.Session["projectcode"] = ds.Tables[0].Rows[0]["projectcode"].ToString();
                            HttpContext.Current.Session["projectid"] = ds.Tables[0].Rows[0]["projectid"].ToString();
                        }
                    }
                    ds.Dispose();
                }
                catch { }
                finally { }
            }
            #region 创建Session值
          
            //生存数据项目过滤
            if ((HttpContext.Current.Session["strprojectid"] == null)||(HttpContext.Current.Session["strprojectid"].ToString() == ""))
            {
                string strprojectid = "select ProjectID from [dbo].[getPermissionProjectByEmployeeID]('" + HttpContext.Current.Session["employeeid"].ToString() + "')";
                HttpContext.Current.Session["strprojectid"] = strprojectid;
            }
            //生存数据范围过滤
            if ((HttpContext.Current.Session["strdeptid"] == null)||(HttpContext.Current.Session["strdeptid"].ToString()==""))
            {
                string strdeptid = "select DeptID from [dbo].[getPermissionDepartmentByEmployeeID]('" + HttpContext.Current.Session["employeeid"].ToString() + "')";
                HttpContext.Current.Session["strdeptid"] = strdeptid;
                HttpContext.Current.Session["strcompid"] = strdeptid;
            }
            #endregion
        }


        /// <summary>
        /// 获取自定义Session值
        /// </summary>
        /// <returns></returns>
        public static void GetDefalutSession()
        {
            try
            {
                DataSet ds = new DataSet();
                string sql = @"select * from T_E_Sys_UserDefinedSession where Enabled=1  ";
                System.Data.Common.DbCommand command = SysDatabase.GetSqlStringCommand(sql);
                ds = SysDatabase.ExecuteDataSet(command);
                if ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string sqlSession = dr["SessionSQL"].ToString().Trim();
                        if (sqlSession != "")
                        {
                            sqlSession = sqlSession.Replace("[!UserName!]", HttpContext.Current.Session["UserName"].ToString());
                            sqlSession = sqlSession.Replace("[!EmployeeId!]", HttpContext.Current.Session["EmployeeId"].ToString());
                            sqlSession = sqlSession.Replace("[!EmployeeCode!]", HttpContext.Current.Session["EmployeeCode"].ToString());
                            sqlSession = sqlSession.Replace("[!EmployeeName!]", HttpContext.Current.Session["EmployeeName"].ToString());
                            sqlSession = sqlSession.Replace("[!CompanyId!]", HttpContext.Current.Session["CompanyId"].ToString());
                            sqlSession = sqlSession.Replace("[!CompanyCode!]", HttpContext.Current.Session["CompanyCode"].ToString());
                            sqlSession = sqlSession.Replace("[!CompanyWbs!]", HttpContext.Current.Session["CompanyWbs"].ToString());
                            sqlSession = sqlSession.Replace("[!CompanyName!]", HttpContext.Current.Session["CompanyName"].ToString());
                            sqlSession = sqlSession.Replace("[!DeptId!]", HttpContext.Current.Session["DeptId"].ToString());
                            sqlSession = sqlSession.Replace("[!DeptWbs!]", HttpContext.Current.Session["DeptWbs"].ToString());
                            sqlSession = sqlSession.Replace("[!DeptCode!]", HttpContext.Current.Session["DeptCode"].ToString());
                            sqlSession = sqlSession.Replace("[!DeptName!]", HttpContext.Current.Session["DeptName"].ToString());

                            try
                            {
                                System.Data.Common.DbCommand commandSession = SysDatabase.GetSqlStringCommand(sqlSession);
                                DataSet dsSession = SysDatabase.ExecuteDataSet(commandSession);
                                foreach (DataRow drSession in dsSession.Tables[0].Rows)
                                {
                                    foreach (DataColumn colSession in dsSession.Tables[0].Columns)
                                    {
                                        HttpContext.Current.Session[colSession.ColumnName] = "";
                                        HttpContext.Current.Session[colSession.ColumnName] = drSession[colSession.ColumnName].ToString();
                                    }
                                }
                                dsSession.Dispose();
                            }
                            catch { }
                            finally { }
                        }
                    }
                }
                ds.Dispose();
            }
            catch { }
            finally { }
        }
        #endregion


		public static void UpdateRead(string empId, string appName, string appId)
		{
			if (!WebTools.IsRead(empId, appName, appId))
			{
				StringBuilder stringBuilder = new StringBuilder();
				object[] str = new object[] { Guid.NewGuid().ToString(), empId, "", DateTime.Now, DateTime.Now, 0, appId, appName, empId };
				stringBuilder.AppendFormat("insert T_OA_Read (_autoid,_username,_orgcode,_createtime,_updatetime,_isdel,appid,appname,employeeId) values\r\n            ('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}','{8}')", str);
				SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
			}
		}

		public static void UpdateRemind(string empId, string appName, string appId)
		{
			if (!WebTools.IsRemind(empId, appName, appId))
			{
				StringBuilder stringBuilder = new StringBuilder();
				object[] str = new object[] { Guid.NewGuid().ToString(), empId, "", DateTime.Now, DateTime.Now, 0, appId, appName, empId };
				stringBuilder.AppendFormat("insert T_OA_Reminded (_autoid,_username,_orgcode,_createtime,_updatetime,_isdel,appid,appname,employeeId) values\r\n            ('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}','{8}')", str);
				SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
			}
		}
	}
}