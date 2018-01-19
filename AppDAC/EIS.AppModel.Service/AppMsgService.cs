using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace EIS.AppModel.Service
{
	public class AppMsgService
	{
		public AppMsgService()
		{
		}

		public static void DeleteMessage(string msgId)
		{
			(new _AppMsg()).Delete(msgId);
		}

		public static IList<AppMsgRec> GetMsgRecDel(string msgId)
		{
			_AppMsgRec __AppMsgRec = new _AppMsgRec();
			return __AppMsgRec.GetModelList(string.Concat("_isDel=1 and msgId='", msgId, "'"));
		}

		public static IList<AppMsgRec> GetMsgRecRead(string msgId, int readFlag)
		{
			_AppMsgRec __AppMsgRec = new _AppMsgRec();
			string[] str = new string[] { "isRead=", readFlag.ToString(), " and msgId='", msgId, "'" };
			return __AppMsgRec.GetModelList(string.Concat(str));
		}

		public static int GetUnReadMsgCount(string employeeId)
		{
			string str = string.Concat("select count(*) from T_E_App_MsgRec where _isDel=0  and isRead=0 and recId='", employeeId, "'");
			object obj = SysDatabase.ExecuteScalar(str);
			return ((obj == DBNull.Value ? false : obj != null) ? Convert.ToInt32(obj) : 0);
		}

		public static int GetUnReadMsgCount(string employeeId, string condition)
		{
			string str = string.Concat("select count(*) from T_E_App_MsgInfo m inner join T_E_App_MsgRec r on m._autoId=r.recId \r\n                where r._isDel=0 and r.isRead=0 and r.recId='", employeeId, "'");
			if (!string.IsNullOrEmpty(condition))
			{
				str = string.Concat(str, " and ", condition);
			}
			object obj = SysDatabase.ExecuteScalar(str);
			return ((obj == DBNull.Value ? false : obj != null) ? Convert.ToInt32(obj) : 0);
		}

		public static void SendMessage(AppMsg appMsg_0)
		{
			DbConnection dbConnection = SysDatabase.CreateConnection();
			dbConnection.Open();
			DbTransaction dbTransaction = dbConnection.BeginTransaction();
			try
			{
				try
				{
					(new _AppMsg(dbTransaction)).Add(appMsg_0);
					_AppMsgRec __AppMsgRec = new _AppMsgRec(dbTransaction);
					string[] strArrays = appMsg_0.RecIds.Split(new char[] { ',' });
					for (int i = 0; i < (int)strArrays.Length; i++)
					{
						string str = strArrays[i];
						AppMsgRec appMsgRec = new AppMsgRec()
						{
							_AutoID = Guid.NewGuid().ToString(),
							_UserName = appMsg_0._UserName,
							_OrgCode = appMsg_0._OrgCode,
							_IsDel = 0,
							_CreateTime = DateTime.Now,
							_UpdateTime = DateTime.Now,
							MsgId = appMsg_0._AutoID,
							IsRead = 0,
							ReadTime = null,
							RecId = str
						};
						__AppMsgRec.Add(appMsgRec);
					}
					dbTransaction.Commit();
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					dbTransaction.Rollback();
					throw exception;
				}
			}
			finally
			{
				dbConnection.Close();
			}
		}

		public static void SendMessage(AppMsg appMsg_0, DbTransaction Trans)
		{
			try
			{
				(new _AppMsg(Trans)).Add(appMsg_0);
				_AppMsgRec __AppMsgRec = new _AppMsgRec(Trans);
				string[] strArrays = appMsg_0.RecIds.Split(new char[] { ',' });
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					string str = strArrays[i];
					AppMsgRec appMsgRec = new AppMsgRec()
					{
						_AutoID = Guid.NewGuid().ToString(),
						_UserName = appMsg_0._UserName,
						_OrgCode = appMsg_0._OrgCode,
						_IsDel = 0,
						_CreateTime = DateTime.Now,
						_UpdateTime = DateTime.Now,
						MsgId = appMsg_0._AutoID,
						IsRead = 0,
						ReadTime = null,
						RecId = str
					};
					__AppMsgRec.Add(appMsgRec);
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}
	}
}