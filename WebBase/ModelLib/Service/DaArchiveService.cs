using EIS.DataAccess;
using System;
using System.Text;

namespace EIS.WebBase.ModelLib.Service
{
	public class DaArchiveService
	{
		public DaArchiveService()
		{
		}

		public static bool IsArchived(string AppName, string AppId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select count(*) from T_OA_DA_ArchiveInfo where  AppId='{0}' and AppName='{1}'", AppId, AppName);
			return Convert.ToInt32(SysDatabase.ExecuteScalar(stringBuilder.ToString())) > 0;
		}

		public static bool IsArchivedByInstanceId(string instanceId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select count(*) from T_OA_DA_ArchiveInfo where  instanceId='{0}'", instanceId);
			return Convert.ToInt32(SysDatabase.ExecuteScalar(stringBuilder.ToString())) > 0;
		}
	}
}