using EIS.AppBase;
using EIS.Permission;
using EIS.Permission.Access;
using EIS.Permission.Model;
using System;

namespace EIS.Permission.Service
{
	public class UserService
	{
		public UserService()
		{
		}

		public static void ChangePass(string user, string newPass)
		{
			_User __User = new _User();
			LoginUser model = __User.GetModel(user);
			if (model == null)
			{
				throw new Exception("用户不存在!");
			}
			if (model.IsLock == 1)
			{
				throw new Exception("用户被锁定，不能修改密码!");
			}
			model.LoginPass = Security.Encrypt(newPass);
			__User.ChangePass(user, model.LoginPass);
		}

		public static LoginInfoType CompanyAdminChk(string user, string string_0)
		{
			LoginInfoType loginInfoType;
			LoginUser companyAdmin = (new _User()).GetCompanyAdmin(user);
			if (companyAdmin == null)
			{
				loginInfoType = LoginInfoType.NotExist;
			}
			else if (companyAdmin.IsLock != 1)
			{
				loginInfoType = (Security.Encrypt(string_0).Trim().Equals(companyAdmin.LoginPass) ? LoginInfoType.Allowed : LoginInfoType.WrongPwd);
			}
			else
			{
				loginInfoType = LoginInfoType.IsLocked;
			}
			return loginInfoType;
		}

		public static LoginUser GetUserByCompanyId(string companyId)
		{
			return (new _User()).GetModelByCompanyId(companyId);
		}

		public static bool IsLoginUserExist(string loginName)
		{
			return (new _User()).IsExist(loginName);
		}

		public static bool IsLoginUserExist(string loginName, string companyId)
		{
			return (new _User()).IsExist(loginName, companyId);
		}

        /// <summary>
        /// 登录逻辑
        /// </summary>
        /// <param name="user"></param>
        /// <param name="string_0"></param>
        /// <returns></returns>
		public static LoginInfoType LoginChk(string user, string string_0)
		{
			LoginInfoType loginInfoType;
			LoginUser model = (new _User()).GetModel(user);
			if (model == null)
			{
				loginInfoType = LoginInfoType.NotExist;
			}
			else if (model.IsLock == 1)
			{
				loginInfoType = LoginInfoType.IsLocked;
			}
			else if (Security.Encrypt(string_0).Trim().Equals(model.LoginPass))
			{
				loginInfoType = LoginInfoType.Allowed;
			}
			else
			{
               loginInfoType =  LoginInfoType.WrongPwd;
				//loginInfoType = (!string_0.Equals(model.LoginPass) ? LoginInfoType.WrongPwd : LoginInfoType.Allowed); 后门，关闭
			}
			return loginInfoType;
		}
	}
}