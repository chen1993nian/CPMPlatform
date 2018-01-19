using EIS.AppBase;
using EIS.AppMail.Model;
using EIS.DataAccess;
using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.AppMail.DAL
{
	public class _MailReceiver
	{
		private DbTransaction dbTransaction_0 = null;

		public _MailReceiver()
		{
		}

		public _MailReceiver(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(MailReceiver model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Mail_Receiver (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\r\n\t\t\t\t\tMailID,\r\n\t\t\t\t\tReceiverID,\r\n\t\t\t\t\tReceiveName,\r\n\t\t\t\t\tSendTime,\r\n\t\t\t\t\tReadTime,\r\n\t\t\t\t\tState,\r\n\t\t\t\t\tFolderID,\r\n\t\t\t\t\tSendType,\r\n                    ReceiveType\r\n\t\t\t) values(\r\n \t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\r\n\t\t\t\t\t@MailID,\r\n\t\t\t\t\t@ReceiverID,\r\n\t\t\t\t\t@ReceiveName,\r\n\t\t\t\t\t@SendTime,\r\n\t\t\t\t\t@ReadTime,\r\n\t\t\t\t\t@State,\r\n\t\t\t\t\t@FolderID,\r\n\t\t\t\t\t@SendType,\r\n                    @ReceiveType\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@MailID", DbType.String, model.MailID);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReceiverID", DbType.String, model.ReceiverID);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReceiveName", DbType.String, model.ReceiveName);
			SysDatabase.AddInParameter(sqlStringCommand, "@SendTime", DbType.DateTime, model.SendTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReadTime", DbType.DateTime, model.ReadTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@State", DbType.Int32, model.State);
			SysDatabase.AddInParameter(sqlStringCommand, "@FolderID", DbType.String, model.FolderID);
			SysDatabase.AddInParameter(sqlStringCommand, "@SendType", DbType.String, model.SendType);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReceiveType", DbType.String, model.ReceiveType);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			return this.Delete(string_0, 1);
		}

		public int Delete(string string_0, int flag)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Concat("Update T_E_Mail_Receiver set _IsDel= ", flag));
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string mailId, string recId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Mail_Receiver set _isDel=1 ");
			stringBuilder.Append(" where mailId=@mailId and ReceiverId=@ReceiverId ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@mailId", DbType.String, mailId);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReceiverId", DbType.String, recId);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  From T_E_Mail_Receiver ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public MailReceiver GetModel(string string_0)
		{
			MailReceiver mailReceiver;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Mail_Receiver ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			MailReceiver str = new MailReceiver();
			DataTable dataTable = SysDatabase.ExecuteTable(sqlStringCommand);
			if (dataTable.Rows.Count <= 0)
			{
				mailReceiver = null;
			}
			else
			{
				str._AutoID = dataTable.Rows[0]["_AutoID"].ToString();
				str.MailID = dataTable.Rows[0]["MailID"].ToString();
				str.ReceiverID = dataTable.Rows[0]["ReceiverID"].ToString();
				str.ReceiveName = dataTable.Rows[0]["ReceiveName"].ToString();
				if (dataTable.Rows[0]["SendTime"].ToString() != "")
				{
					str.SendTime = new DateTime?(DateTime.Parse(dataTable.Rows[0]["SendTime"].ToString()));
				}
				if (dataTable.Rows[0]["ReadTime"].ToString() != "")
				{
					str.ReadTime = new DateTime?(DateTime.Parse(dataTable.Rows[0]["ReadTime"].ToString()));
				}
				if (dataTable.Rows[0]["State"].ToString() != "")
				{
					str.State = int.Parse(dataTable.Rows[0]["State"].ToString());
				}
				str.FolderID = dataTable.Rows[0]["FolderID"].ToString();
				str.SendType = dataTable.Rows[0]["SendType"].ToString();
				str.ReceiveType = dataTable.Rows[0]["ReceiveType"].ToString();
				mailReceiver = str;
			}
			return mailReceiver;
		}

		public DataTable GetReceiveList(string employeeId, string folderId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select r._AutoID, r.State,r.FolderID,m._AutoId MailID,m.Subject,m.Priority,m.Sender,(select employeeName from T_E_Org_Employee e where e._AutoID=m.Sender) SenderName\r\n                ,cast(m.body as varchar(8000)) Body\r\n                ,m.CreateTime from dbo.T_E_Mail_Receiver r inner join dbo.T_E_Mail_Message m\r\n                on r.MailID=m._AutoId where r.ReceiverID='{0}' and r.state<>3 and r.folderId='{1}' order by m.CreateTime desc", employeeId, folderId);
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public DataTable GetReceiveList(string employeeId, string folderId, string sortdir)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(string.Concat("select * from (select r.*,m.Subject,m.Priority,m.SenderName \r\n\t\t\tfrom T_E_Mail_Message m inner join T_E_Mail_Receiver r on m._autoid=r.mailid\r\n\t\t\t) t where _isdel=0 and receiverid='{0}' and folderId='{1}' order by ", sortdir), employeeId, folderId);
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public int Remove(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Mail_Receiver ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Remove(string mailId, string recId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Mail_Receiver ");
			stringBuilder.Append(" where mailId=@mailId and ReceiverId=@ReceiverId ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@mailId", DbType.String, mailId);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReceiverId", DbType.String, recId);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Update(MailReceiver model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Mail_Receiver set \r\n\t\t\t\t\tMailID=@MailID,\r\n\t\t\t\t\tReceiverID=@ReceiverID,\r\n\t\t\t\t\tReceiveName=@ReceiveName,\r\n\t\t\t\t\tSendTime=@SendTime,\r\n\t\t\t\t\tReadTime=@ReadTime,\r\n\t\t\t\t\tState=@State,\r\n\t\t\t\t\tFolderID=@FolderID,\r\n\t\t\t\t\tSendType=@SendType\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@MailID", DbType.String, model.MailID);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReceiverID", DbType.String, model.ReceiverID);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReceiveName", DbType.String, model.ReceiveName);
			SysDatabase.AddInParameter(sqlStringCommand, "@SendTime", DbType.DateTime, model.SendTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReadTime", DbType.DateTime, model.ReadTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@State", DbType.Int32, model.State);
			SysDatabase.AddInParameter(sqlStringCommand, "@FolderID", DbType.String, model.FolderID);
			SysDatabase.AddInParameter(sqlStringCommand, "@SendType", DbType.String, model.SendType);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int UpdateFolder(string string_0, string FolderId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update  T_E_Mail_Receiver set FolderId=@FolderId ");
			stringBuilder.Append(" where _AutoID=@_AutoID");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			SysDatabase.AddInParameter(sqlStringCommand, "@FolderId", DbType.String, FolderId);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int UpdateState(string string_0, int state)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update  T_E_Mail_Receiver set state=@state ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			SysDatabase.AddInParameter(sqlStringCommand, "@state", DbType.Int32, state);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int UpdateState(string MailId, string ReceiverID, int state)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update  T_E_Mail_Receiver set state=@state ");
			stringBuilder.Append(" where MailId=@MailId and ReceiverID=@ReceiverID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@MailId", DbType.String, MailId);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReceiverID", DbType.String, ReceiverID);
			SysDatabase.AddInParameter(sqlStringCommand, "@state", DbType.Int32, state);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}