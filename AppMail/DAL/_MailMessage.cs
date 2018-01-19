using EIS.AppBase;
using EIS.AppMail.Model;
using EIS.DataAccess;
using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.AppMail.DAL
{
	public class _MailMessage
	{
		private DbTransaction dbTransaction_0 = null;

		public _MailMessage()
		{
		}

		public _MailMessage(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(MailInfo model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Mail_Message (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\r\n\t\t\t\t\tSubject,\r\n\t\t\t\t\tPriority,\r\n\t\t\t\t\tBody,\r\n\t\t\t\t\tSender,\r\n                    SenderName,\r\n\t\t\t\t\tCreateTime,\r\n\t\t\t\t\tFolderId,\r\n\t\t\t\t\tReceivers,\r\n\t\t\t\t\tReceiverIDs,\r\n\t\t\t\t\tCC,\r\n\t\t\t\t\tCCIDS,\r\n\t\t\t\t\tBCC,\r\n\t\t\t\t\tBCCIDS,\r\n\t\t\t\t\tOutReceivers,\r\n\t\t\t\t\tOutReceiverIDs,\r\n                    SendFlag\r\n\t\t\t) values(\r\n \t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\r\n\t\t\t\t\t@Subject,\r\n\t\t\t\t\t@Priority,\r\n\t\t\t\t\t@Body,\r\n\t\t\t\t\t@Sender,\r\n                    @SenderName,\r\n\t\t\t\t\t@CreateTime,\r\n\t\t\t\t\t@FolderId,\r\n\t\t\t\t\t@Receivers,\r\n\t\t\t\t\t@ReceiverIDs,\r\n\t\t\t\t\t@CC,\r\n\t\t\t\t\t@CCIDS,\r\n\t\t\t\t\t@BCC,\r\n\t\t\t\t\t@BCCIDS,\r\n\t\t\t\t\t@OutReceivers,\r\n\t\t\t\t\t@OutReceiverIDs,\r\n                    @SendFlag\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@Subject", DbType.String, model.Subject);
			SysDatabase.AddInParameter(sqlStringCommand, "@Priority", DbType.Int32, model.Priority);
			SysDatabase.AddInParameter(sqlStringCommand, "@Body", DbType.String, model.Body);
			SysDatabase.AddInParameter(sqlStringCommand, "@Sender", DbType.String, model.Sender);
			SysDatabase.AddInParameter(sqlStringCommand, "@SenderName", DbType.String, model.SenderName);
			SysDatabase.AddInParameter(sqlStringCommand, "@CreateTime", DbType.DateTime, model.CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@FolderId", DbType.String, model.FolderId);
			SysDatabase.AddInParameter(sqlStringCommand, "@Receivers", DbType.String, model.Receivers);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReceiverIDs", DbType.String, model.ReceiverIDs);
			SysDatabase.AddInParameter(sqlStringCommand, "@CC", DbType.String, model.CC);
			SysDatabase.AddInParameter(sqlStringCommand, "@CCIDS", DbType.String, model.CCIDS);
			SysDatabase.AddInParameter(sqlStringCommand, "@BCC", DbType.String, model.BCC);
			SysDatabase.AddInParameter(sqlStringCommand, "@BCCIDS", DbType.String, model.BCCIDS);
			SysDatabase.AddInParameter(sqlStringCommand, "@OutReceivers", DbType.String, model.OutReceivers);
			SysDatabase.AddInParameter(sqlStringCommand, "@OutReceiverIDs", DbType.String, model.OutReceiverIDs);
			SysDatabase.AddInParameter(sqlStringCommand, "@SendFlag", DbType.Int32, model.SendFlag);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0, int flag)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Concat("Update T_E_Mail_Message set _IsDel= ", flag));
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Mail_Message ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public MailInfo GetModel(string string_0)
		{
			MailInfo mailInfo;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Mail_Message ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			MailInfo str = new MailInfo();
			DataTable dataTable = SysDatabase.ExecuteTable(sqlStringCommand);
			if (dataTable.Rows.Count <= 0)
			{
				mailInfo = null;
			}
			else
			{
				str.MailID = dataTable.Rows[0]["_AutoID"].ToString();
				str._AutoID = dataTable.Rows[0]["_AutoID"].ToString();
				str.Subject = dataTable.Rows[0]["Subject"].ToString();
				if (dataTable.Rows[0]["Priority"].ToString() != "")
				{
					str.Priority = int.Parse(dataTable.Rows[0]["Priority"].ToString());
				}
				str.Body = dataTable.Rows[0]["Body"].ToString();
				str.Sender = dataTable.Rows[0]["Sender"].ToString();
				str.SenderName = dataTable.Rows[0]["SenderName"].ToString();
				if (dataTable.Rows[0]["CreateTime"].ToString() != "")
				{
					str.CreateTime = DateTime.Parse(dataTable.Rows[0]["CreateTime"].ToString());
				}
				str.FolderId = dataTable.Rows[0]["FolderId"].ToString();
				if (dataTable.Rows[0]["SendFlag"].ToString() != "")
				{
					str.SendFlag = int.Parse(dataTable.Rows[0]["SendFlag"].ToString());
				}
				str.Receivers = dataTable.Rows[0]["Receivers"].ToString();
				str.ReceiverIDs = dataTable.Rows[0]["ReceiverIDs"].ToString();
				str.CC = dataTable.Rows[0]["CC"].ToString();
				str.CCIDS = dataTable.Rows[0]["CCIDS"].ToString();
				str.BCC = dataTable.Rows[0]["BCC"].ToString();
				str.BCCIDS = dataTable.Rows[0]["BCCIDS"].ToString();
				str.OutReceivers = dataTable.Rows[0]["OutReceivers"].ToString();
				str.OutReceiverIDs = dataTable.Rows[0]["OutReceiverIDs"].ToString();
				mailInfo = str;
			}
			return mailInfo;
		}

		public bool IsExist(string mailId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(*)  FROM T_E_Mail_Message ");
			stringBuilder.Append(string.Concat(" where _AutoID='", mailId, "'"));
			return int.Parse(SysDatabase.ExecuteScalar(stringBuilder.ToString()).ToString()) > 0;
		}

		public int Remove(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Mail_Message ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Update(MailInfo model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Mail_Message set \r\n\t\t\t\t\t_UpdateTime = @_UpdateTime,\r\n\t\t\t\t\tSubject=@Subject,\r\n\t\t\t\t\tPriority=@Priority,\r\n\t\t\t\t\tBody=@Body,\r\n\t\t\t\t\tSender=@Sender,\r\n                    SenderName=@SenderName,\r\n\t\t\t\t\tCreateTime=@CreateTime,\r\n\t\t\t\t\tFolderId=@FolderId,\r\n\t\t\t\t\tReceivers=@Receivers,\r\n\t\t\t\t\tReceiverIDs=@ReceiverIDs,\r\n\t\t\t\t\tCC=@CC,\r\n\t\t\t\t\tCCIDS=@CCIDS,\r\n\t\t\t\t\tBCC=@BCC,\r\n\t\t\t\t\tBCCIDS=@BCCIDS,\r\n\t\t\t\t\tOutReceivers=@OutReceivers,\r\n\t\t\t\t\tOutReceiverIDs=@OutReceiverIDs,\r\n                    SendFlag=@SendFlag\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@Subject", DbType.String, model.Subject);
			SysDatabase.AddInParameter(sqlStringCommand, "@Priority", DbType.Int32, model.Priority);
			SysDatabase.AddInParameter(sqlStringCommand, "@Body", DbType.String, model.Body);
			SysDatabase.AddInParameter(sqlStringCommand, "@Sender", DbType.String, model.Sender);
			SysDatabase.AddInParameter(sqlStringCommand, "@SenderName", DbType.String, model.SenderName);
			SysDatabase.AddInParameter(sqlStringCommand, "@CreateTime", DbType.DateTime, model.CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@FolderId", DbType.Int32, model.FolderId);
			SysDatabase.AddInParameter(sqlStringCommand, "@Receivers", DbType.String, model.Receivers);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReceiverIDs", DbType.String, model.ReceiverIDs);
			SysDatabase.AddInParameter(sqlStringCommand, "@CC", DbType.String, model.CC);
			SysDatabase.AddInParameter(sqlStringCommand, "@CCIDS", DbType.String, model.CCIDS);
			SysDatabase.AddInParameter(sqlStringCommand, "@BCC", DbType.String, model.BCC);
			SysDatabase.AddInParameter(sqlStringCommand, "@BCCIDS", DbType.String, model.BCCIDS);
			SysDatabase.AddInParameter(sqlStringCommand, "@OutReceivers", DbType.String, model.OutReceivers);
			SysDatabase.AddInParameter(sqlStringCommand, "@OutReceiverIDs", DbType.String, model.OutReceiverIDs);
			SysDatabase.AddInParameter(sqlStringCommand, "@SendFlag", DbType.Int32, model.SendFlag);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int UpdateFolder(string string_0, string FolderId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Mail_Message set FolderId=@FolderId ");
			stringBuilder.Append(" where _AutoID=@_AutoID");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			SysDatabase.AddInParameter(sqlStringCommand, "@FolderId", DbType.String, FolderId);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}