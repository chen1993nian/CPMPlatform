using System;
using System.Collections.Generic;

namespace EIS.AppModel
{
	public class ModelDefaults
	{
		private static Dictionary<string, string> dictionary_0;

		public static Dictionary<string, string> DefaultValue
		{
			get
			{
				return ModelDefaults.dictionary_0;
			}
		}

		static ModelDefaults()
		{
			ModelDefaults.dictionary_0 = new Dictionary<string, string>()
			{
				{ "Custom", "自定义值" },
				{ "Date", "当前日期" },
				{ "DateTime", "当前日期时间" },
				{ "Session", "Session值类型" },
				{ "LoginName", "登录用户账号" },
				{ "EmployeeName", "登录用户中文名" },
				{ "EmployeeID", "登录用户ID" },
				{ "DeptID", "所属部门ID" },
				{ "DeptCode", "所属部门编码" },
				{ "DeptName", "所属部门名称" },
				{ "DeptFullName", "所属部门全称" },
				{ "TopDeptID", "一级部门ID" },
				{ "TopDeptCode", "一级部门编码" },
				{ "TopDeptName", "一级部门名称" },
				{ "TopDeptFullName", "一级部门全称" },
				{ "CompanyID", "所属公司ID" },
				{ "CompanyCode", "所属公司编码" },
				{ "CompanyName", "所属公司名称" },
				{ "PositionID", "所属岗位ID" },
				{ "PositionName", "所属岗位名称" },
				{ "LoginIP", "登录用户IP" },
				{ "DbFunction", "数据库函数" },
				{ "DbSQL", "自定义SQL" },
				{ "CurrentYear", "当前年" },
				{ "CurrentMonth", "当前月" },
				{ "CurrentDay", "当前日" }
			};
		}

		public ModelDefaults()
		{
		}
	}
}