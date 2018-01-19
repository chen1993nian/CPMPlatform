using EIS.DataAccess;
using EIS.Permission.Access;
using System;
using System.Data;

namespace EIS.Permission.Service
{
	public class FunNodeService
	{
		public FunNodeService()
		{
		}

		public static DataTable GetAllFunNodeByWebId(string webId)
		{
			_FunNode __FunNode = new _FunNode();
			return __FunNode.GetList(string.Concat("webId='", webId, "'"));
		}

		public static string GetFunIdByCode(string funCode)
		{
			string str = string.Concat("select FunCode from T_E_Sys_FunNode where _AutoId ='", funCode, "'");
			object obj = SysDatabase.ExecuteScalar(str);
			return ((obj == DBNull.Value ? true : obj == null) ? "" : obj.ToString());
		}

		public static string GetFunWbsById(string funId)
		{
			string str = string.Concat("select FunWbs from T_E_Sys_FunNode where _AutoId ='", funId, "'");
			object obj = SysDatabase.ExecuteScalar(str);
			return ((obj == DBNull.Value ? true : obj == null) ? "" : obj.ToString());
		}

		public static int GetMaxOrder(string PWBS, string WebId)
		{
			return (new _FunNode()).GetMaxOrder(PWBS, WebId);
		}

		public static string GetNewWbs(string PWBS, string WebId)
		{
			return (new _FunNode()).GetNewCode(PWBS, WebId);
		}
	}
}