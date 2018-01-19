using EIS.AppBase;
using EIS.AppMail.Model;
using EIS.DataAccess;
using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.AppMail.DAL
{
	public class _MailFolder
	{
		public _MailFolder()
		{
		}

		public void Add(MailFolder model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Mail_Folder (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tFolderName,\r\n\t\t\t\t\tOwner,\r\n\t\t\t\t\tSN\r\n\t\t\t) values(\r\n \t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@FolderName,\r\n\t\t\t\t\t@Owner,\r\n\t\t\t\t\t@SN\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@FolderName", DbType.String, model.FolderName);
			SysDatabase.AddInParameter(sqlStringCommand, "@Owner", DbType.String, model.Owner);
			SysDatabase.AddInParameter(sqlStringCommand, "@SN", DbType.Int32, model.SN);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public int Delete(string string_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Mail_Folder ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			return SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Mail_Folder ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public MailFolder GetModel(string string_0)
		{
			MailFolder mailFolder;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Mail_Folder ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			MailFolder str = new MailFolder();
			DataTable dataTable = SysDatabase.ExecuteTable(sqlStringCommand);
			if (dataTable.Rows.Count <= 0)
			{
				mailFolder = null;
			}
			else
			{
				DataRow item = dataTable.Rows[0];
				str._AutoID = item["_AutoID"].ToString();
				str._UserName = item["_UserName"].ToString();
				str._OrgCode = item["_OrgCode"].ToString();
				if (item["_CreateTime"].ToString() != "")
				{
					str._CreateTime = DateTime.Parse(item["_CreateTime"].ToString());
				}
				if (item["_UpdateTime"].ToString() != "")
				{
					str._UpdateTime = DateTime.Parse(item["_UpdateTime"].ToString());
				}
				if (item["_IsDel"].ToString() != "")
				{
					str._IsDel = int.Parse(item["_IsDel"].ToString());
				}
				str.FolderName = item["FolderName"].ToString();
				str.Owner = item["Owner"].ToString();
				if (item["SN"].ToString() != "")
				{
					str.SN = int.Parse(item["SN"].ToString());
				}
				mailFolder = str;
			}
			return mailFolder;
		}

		public void Update(MailFolder model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Mail_Folder set \r\n\t\t\t\t\tFolderName=@FolderName,\r\n\t\t\t\t\tOwner=@Owner,\r\n\t\t\t\t\tSN=@SN\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@FolderName", DbType.String, model.FolderName);
			SysDatabase.AddInParameter(sqlStringCommand, "@Owner", DbType.String, model.Owner);
			SysDatabase.AddInParameter(sqlStringCommand, "@SN", DbType.Int32, model.SN);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}
	}
}