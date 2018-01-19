using System;
using System.Collections.Specialized;

namespace EIS.AppModel
{
	public class AppFields
	{
		public const string SysKeyField = "_autoid";

		public static StringCollection SysFields;

		static AppFields()
		{
			AppFields.SysFields = new StringCollection();
			AppFields.SysFields.Add("_autoid");
			AppFields.SysFields.Add("_username");
			AppFields.SysFields.Add("_orgcode");
			AppFields.SysFields.Add("_createtime");
			AppFields.SysFields.Add("_updatetime");
			AppFields.SysFields.Add("_isdel");
			AppFields.SysFields.Add("_companyid");
			AppFields.SysFields.Add("_wfstate");
			AppFields.SysFields.Add("_gdstate");
		}

		public AppFields()
		{
		}

		public static bool Contains(string fieldName)
		{
			return AppFields.SysFields.Contains(fieldName.ToLower());
		}
	}
}