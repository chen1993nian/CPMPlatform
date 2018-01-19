using EIS.DataAccess;
using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.AppModel.Service
{
	public class AppMsgRecService
	{
		public AppMsgRecService()
		{
		}

		public static void DeleteReadRec(string msgId, string empId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_App_MsgRec set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel = 1\r\n\r\n\t\t\t\t\twhere RecId=@RecId and MsgId=@MsgId\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
			SysDatabase.AddInParameter(sqlStringCommand, "@RecId", DbType.String, empId);
			SysDatabase.AddInParameter(sqlStringCommand, "@MsgId", DbType.String, msgId);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public static void FlagRead(string msgId, string empId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_App_MsgRec set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\r\n\t\t\t\t\tIsRead =@IsRead,\r\n\t\t\t\t\tReadTime=@ReadTime\r\n\r\n\t\t\t\t\twhere RecId=@RecId and MsgId=@MsgId\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, msgId);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, DateTime.Now);
			SysDatabase.AddInParameter(sqlStringCommand, "@RecId", DbType.String, empId);
			SysDatabase.AddInParameter(sqlStringCommand, "@MsgId", DbType.String, msgId);
			SysDatabase.AddInParameter(sqlStringCommand, "@IsRead", DbType.Int32, 1);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReadTime", DbType.DateTime, DateTime.Now);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}
	}
}