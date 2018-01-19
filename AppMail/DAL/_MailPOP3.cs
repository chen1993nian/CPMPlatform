using EIS.AppBase;
using EIS.AppMail.Model;
using EIS.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.AppMail.DAL
{
	public class _MailPOP3
	{
		public _MailPOP3()
		{
		}

		public int Add(MailPOP3 model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Mail_POP3 (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tEmailAdrr,\r\n\t\t\t\t\tPOP3Adrr,\r\n\t\t\t\t\tPOP3Port,\r\n\t\t\t\t\tPOP3SSL,\r\n\t\t\t\t\tSMTPAdrr,\r\n\t\t\t\t\tSMTPPort,\r\n\t\t\t\t\tSMTPSSL,\r\n\t\t\t\t\tAccount,\r\n\t\t\t\t\tPassWD,\r\n\t\t\t\t\tCredentialRequired,\r\n\t\t\t\t\tAutoReceive,\r\n\t\t\t\t\tMaxSize,\r\n\t\t\t\t\tIsDefault,\r\n\t\t\t\t\tDelAfterRec,\r\n\t\t\t\t\tOwner\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@EmailAdrr,\r\n\t\t\t\t\t@POP3Adrr,\r\n\t\t\t\t\t@POP3Port,\r\n\t\t\t\t\t@POP3SSL,\r\n\t\t\t\t\t@SMTPAdrr,\r\n\t\t\t\t\t@SMTPPort,\r\n\t\t\t\t\t@SMTPSSL,\r\n\t\t\t\t\t@Account,\r\n\t\t\t\t\t@PassWD,\r\n\t\t\t\t\t@CredentialRequired,\r\n\t\t\t\t\t@AutoReceive,\r\n\t\t\t\t\t@MaxSize,\r\n\t\t\t\t\t@IsDefault,\r\n\t\t\t\t\t@DelAfterRec,\r\n\t\t\t\t\t@Owner\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmailAdrr", DbType.String, model.EmailAdrr);
			SysDatabase.AddInParameter(sqlStringCommand, "@POP3Adrr", DbType.String, model.POP3Adrr);
			SysDatabase.AddInParameter(sqlStringCommand, "@POP3Port", DbType.Int32, model.POP3Port);
			SysDatabase.AddInParameter(sqlStringCommand, "@POP3SSL", DbType.Int32, model.POP3SSL);
			SysDatabase.AddInParameter(sqlStringCommand, "@SMTPAdrr", DbType.String, model.SMTPAdrr);
			SysDatabase.AddInParameter(sqlStringCommand, "@SMTPPort", DbType.Int32, model.SMTPPort);
			SysDatabase.AddInParameter(sqlStringCommand, "@SMTPSSL", DbType.Int32, model.SMTPSSL);
			SysDatabase.AddInParameter(sqlStringCommand, "@Account", DbType.String, model.Account);
			SysDatabase.AddInParameter(sqlStringCommand, "@PassWD", DbType.String, model.PassWD);
			SysDatabase.AddInParameter(sqlStringCommand, "@CredentialRequired", DbType.Int32, model.CredentialRequired);
			SysDatabase.AddInParameter(sqlStringCommand, "@AutoReceive", DbType.Int32, model.AutoReceive);
			SysDatabase.AddInParameter(sqlStringCommand, "@MaxSize", DbType.Int32, model.MaxSize);
			SysDatabase.AddInParameter(sqlStringCommand, "@IsDefault", DbType.Int32, model.IsDefault);
			SysDatabase.AddInParameter(sqlStringCommand, "@DelAfterRec", DbType.Int32, model.DelAfterRec);
			SysDatabase.AddInParameter(sqlStringCommand, "@Owner", DbType.String, model.Owner);
			return SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public int Delete(string string_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Mail_POP3 ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			return SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Mail_POP3 ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public MailPOP3 GetModel(string string_0)
		{
			MailPOP3 model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Mail_POP3 ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			MailPOP3 mailPOP3 = new MailPOP3();
			DataTable dataTable = SysDatabase.ExecuteTable(sqlStringCommand);
			if (dataTable.Rows.Count <= 0)
			{
				model = null;
			}
			else
			{
				model = this.GetModel(dataTable.Rows[0]);
			}
			return model;
		}

		public MailPOP3 GetModel(DataRow dataRow_0)
		{
			MailPOP3 mailPOP3 = new MailPOP3()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				EmailAdrr = dataRow_0["EmailAdrr"].ToString(),
				POP3Adrr = dataRow_0["POP3Adrr"].ToString()
			};
			if (dataRow_0["POP3Port"].ToString() != "")
			{
				mailPOP3.POP3Port = int.Parse(dataRow_0["POP3Port"].ToString());
			}
			if (dataRow_0["POP3SSL"].ToString() != "")
			{
				mailPOP3.POP3SSL = int.Parse(dataRow_0["POP3SSL"].ToString());
			}
			mailPOP3.SMTPAdrr = dataRow_0["SMTPAdrr"].ToString();
			if (dataRow_0["SMTPPort"].ToString() != "")
			{
				mailPOP3.SMTPPort = int.Parse(dataRow_0["SMTPPort"].ToString());
			}
			if (dataRow_0["SMTPSSL"].ToString() != "")
			{
				mailPOP3.SMTPSSL = int.Parse(dataRow_0["SMTPSSL"].ToString());
			}
			mailPOP3.Account = dataRow_0["Account"].ToString();
			mailPOP3.PassWD = dataRow_0["PassWD"].ToString();
			if (dataRow_0["CredentialRequired"].ToString() != "")
			{
				mailPOP3.CredentialRequired = int.Parse(dataRow_0["CredentialRequired"].ToString());
			}
			if (dataRow_0["AutoReceive"].ToString() != "")
			{
				mailPOP3.AutoReceive = int.Parse(dataRow_0["AutoReceive"].ToString());
			}
			if (dataRow_0["MaxSize"].ToString() != "")
			{
				mailPOP3.MaxSize = int.Parse(dataRow_0["MaxSize"].ToString());
			}
			if (dataRow_0["IsDefault"].ToString() != "")
			{
				mailPOP3.IsDefault = int.Parse(dataRow_0["IsDefault"].ToString());
			}
			if (dataRow_0["DelAfterRec"].ToString() != "")
			{
				mailPOP3.DelAfterRec = int.Parse(dataRow_0["DelAfterRec"].ToString());
			}
			mailPOP3.Owner = dataRow_0["Owner"].ToString();
			return mailPOP3;
		}

		public IList<MailPOP3> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<MailPOP3> mailPOP3s = new List<MailPOP3>();
			stringBuilder.Append("select *  FROM T_E_Mail_POP3 ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				mailPOP3s.Add(this.GetModel(row));
			}
			return mailPOP3s;
		}

		public int Update(MailPOP3 model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Mail_POP3 set \r\n\t\t\t\t\t_UpdateTime = @_UpdateTime,\r\n\t\t\t\t\tEmailAdrr=@EmailAdrr,\r\n\t\t\t\t\tPOP3Adrr=@POP3Adrr,\r\n\t\t\t\t\tPOP3Port=@POP3Port,\r\n\t\t\t\t\tPOP3SSL=@POP3SSL,\r\n\t\t\t\t\tSMTPAdrr=@SMTPAdrr,\r\n\t\t\t\t\tSMTPPort=@SMTPPort,\r\n\t\t\t\t\tSMTPSSL=@SMTPSSL,\r\n\t\t\t\t\tAccount=@Account,\r\n\t\t\t\t\tPassWD=@PassWD,\r\n\t\t\t\t\tCredentialRequired=@CredentialRequired,\r\n\t\t\t\t\tAutoReceive=@AutoReceive,\r\n\t\t\t\t\tMaxSize=@MaxSize,\r\n\t\t\t\t\tIsDefault=@IsDefault,\r\n\t\t\t\t\tDelAfterRec=@DelAfterRec,\r\n\t\t\t\t\tOwner=@Owner\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmailAdrr", DbType.String, model.EmailAdrr);
			SysDatabase.AddInParameter(sqlStringCommand, "@POP3Adrr", DbType.String, model.POP3Adrr);
			SysDatabase.AddInParameter(sqlStringCommand, "@POP3Port", DbType.Int32, model.POP3Port);
			SysDatabase.AddInParameter(sqlStringCommand, "@POP3SSL", DbType.Int32, model.POP3SSL);
			SysDatabase.AddInParameter(sqlStringCommand, "@SMTPAdrr", DbType.String, model.SMTPAdrr);
			SysDatabase.AddInParameter(sqlStringCommand, "@SMTPPort", DbType.Int32, model.SMTPPort);
			SysDatabase.AddInParameter(sqlStringCommand, "@SMTPSSL", DbType.Int32, model.SMTPSSL);
			SysDatabase.AddInParameter(sqlStringCommand, "@Account", DbType.String, model.Account);
			SysDatabase.AddInParameter(sqlStringCommand, "@PassWD", DbType.String, model.PassWD);
			SysDatabase.AddInParameter(sqlStringCommand, "@CredentialRequired", DbType.Int32, model.CredentialRequired);
			SysDatabase.AddInParameter(sqlStringCommand, "@AutoReceive", DbType.Int32, model.AutoReceive);
			SysDatabase.AddInParameter(sqlStringCommand, "@MaxSize", DbType.Int32, model.MaxSize);
			SysDatabase.AddInParameter(sqlStringCommand, "@IsDefault", DbType.Int32, model.IsDefault);
			SysDatabase.AddInParameter(sqlStringCommand, "@DelAfterRec", DbType.Int32, model.DelAfterRec);
			SysDatabase.AddInParameter(sqlStringCommand, "@Owner", DbType.String, model.Owner);
			return SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}
	}
}