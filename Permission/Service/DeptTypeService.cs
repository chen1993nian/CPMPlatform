using EIS.DataAccess;
using System;

namespace EIS.Permission.Service
{
	public class DeptTypeService
	{
		public DeptTypeService()
		{
		}

		public static string GetTypeIdByCode(string typeCode)
		{
			string str = SysDatabase.ExecuteScalar(string.Concat("select top 1 _AutoId from T_E_Org_DeptType where TypeCode='", typeCode, "'")).ToString();
			return str;
		}
	}
}