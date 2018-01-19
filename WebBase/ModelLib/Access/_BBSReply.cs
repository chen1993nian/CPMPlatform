using EIS.AppBase;
using EIS.DataAccess;
using EIS.WebBase.ModelLib.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.WebBase.ModelLib.Access
{
	public class _BBSReply
	{
		private DbTransaction dbTransaction_0 = null;

		public _BBSReply()
		{
		}

		public _BBSReply(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(BBSReply model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert BBSReply  (\r\n \t\t\t\t\t    _AutoID,\r\n\t\t\t\t\t    _UserName,\r\n\t\t\t\t\t    _OrgCode,\r\n\t\t\t\t\t    _CreateTime,\r\n\t\t\t\t\t    _UpdateTime,\r\n\t\t\t\t\t    _IsDel,\r\n                        _CompanyId,\r\n                        TopicId,\r\n\t\t\t\t\t    ReferId,\r\n                        ReplyText,\r\n                        ReplyOrder\r\n\t\t\t    ) values(\r\n\t\t\t\t\t    @_AutoID,\r\n\t\t\t\t\t    @_UserName,\r\n\t\t\t\t\t    @_OrgCode,\r\n\t\t\t\t\t    @_CreateTime,\r\n\t\t\t\t\t    @_UpdateTime,\r\n\t\t\t\t\t    @_IsDel,\r\n\t\t\t\t\t    @_CompanyId,\r\n\r\n                        @TopicId,\r\n\t\t\t\t\t    @ReferId,\r\n                        @ReplyText,\r\n                        @ReplyOrder\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CompanyId", DbType.String, model._CompanyID);
			SysDatabase.AddInParameter(sqlStringCommand, "@TopicId", DbType.String, model.TopicId);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReferId", DbType.String, model.ReferId);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReplyText", DbType.String, model.ReplyText);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReplyOrder", DbType.Int32, model.ReplyOrder);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BBSReply  ");
			stringBuilder.Append(" where _AutoID=@_AutoID ;");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  From BBSReply  ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public BBSReply GetModel(string string_0)
		{
			BBSReply model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from BBSReply  ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
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

		public BBSReply GetModel(DataRow dataRow_0)
		{
			BBSReply bBSReply = new BBSReply()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				bBSReply._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				bBSReply._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				bBSReply._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			bBSReply._CompanyID = dataRow_0["_CompanyID"].ToString();
			bBSReply.TopicId = dataRow_0["TopicId"].ToString();
			bBSReply.ReferId = dataRow_0["ReferId"].ToString();
			bBSReply.ReplyText = dataRow_0["ReplyText"].ToString();
			if (dataRow_0["ReplyOrder"].ToString() != "")
			{
				bBSReply._IsDel = int.Parse(dataRow_0["ReplyOrder"].ToString());
			}
			return bBSReply;
		}

		public List<BBSReply> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<BBSReply> bBSReplies = new List<BBSReply>();
			stringBuilder.Append("select *  From BBSReply  ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				bBSReplies.Add(this.GetModel(row));
			}
			return bBSReplies;
		}

		public int Update(BBSReply model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update BBSReply  set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n                    ReplyText=@ReplyText\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReplyText", DbType.String, model.ReplyText);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}