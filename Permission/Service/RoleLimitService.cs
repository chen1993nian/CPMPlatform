using EIS.DataAccess;
using System;
using System.Data;
using System.Data.Common;

namespace EIS.Permission.Service
{
	public class RoleLimitService
	{
		public RoleLimitService()
		{
		}

		public static DataTable GetRoleLimitDataById(string webId, string roleId)
		{
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand("\r\n            SELECT node._AutoID FunID, node.FunName, node.WebID, node.FunpWBS, \r\n            node.FunWBS, node.OrderId, node.DispState, limit._AutoID RoleFunID, limit.RoleID, limit.FunLimit, \r\n            limit.DeptLimit, limit.IsDealOwen \r\n            FROM T_E_Sys_FunNode AS node LEFT OUTER JOIN \r\n            T_E_Org_RoleLimit AS limit ON node._AutoID = limit.FunID AND (limit.RoleID =@RoleID)\r\n            WHERE (node.WebID = @WebID)  and node.DispState='是' \r\n            ORDER BY FunPWBS, OrderId");
			SysDatabase.AddInParameter(sqlStringCommand, "@WebID", DbType.String, webId);
			SysDatabase.AddInParameter(sqlStringCommand, "@RoleID", DbType.String, roleId);
			return SysDatabase.ExecuteTable(sqlStringCommand);
		}
	}
}