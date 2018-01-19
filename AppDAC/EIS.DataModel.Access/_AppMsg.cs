using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.DataModel.Access
{
	public class _AppMsg
	{
		private DbTransaction dbTransaction_0 = null;

		public _AppMsg()
		{
		}

		public _AppMsg(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(AppMsg model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_App_MsgInfo (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tSender,\r\n\t\t\t\t\tRecIds,\r\n\t\t\t\t\tRecNames,\r\n\t\t\t\t\tSendTime,\r\n\t\t\t\t\tMsgType,\r\n\t\t\t\t\tMsgUrl,\r\n\t\t\t\t\tTitle,\r\n\t\t\t\t\tReplyId,\r\n\t\t\t\t\tContent\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@Sender,\r\n\t\t\t\t\t@RecIds,\r\n\t\t\t\t\t@RecNames,\r\n\t\t\t\t\t@SendTime,\r\n\t\t\t\t\t@MsgType,\r\n\t\t\t\t\t@MsgUrl,\r\n\t\t\t\t\t@Title,\r\n\t\t\t\t\t@ReplyId,\r\n\t\t\t\t\t@Content\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@Sender", DbType.String, model.Sender);
			SysDatabase.AddInParameter(sqlStringCommand, "@RecIds", DbType.String, model.RecIds);
			SysDatabase.AddInParameter(sqlStringCommand, "@RecNames", DbType.String, model.RecNames);
			SysDatabase.AddInParameter(sqlStringCommand, "@SendTime", DbType.DateTime, model.SendTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@MsgType", DbType.String, model.MsgType);
			SysDatabase.AddInParameter(sqlStringCommand, "@MsgUrl", DbType.String, model.MsgUrl);
			SysDatabase.AddInParameter(sqlStringCommand, "@Title", DbType.String, model.Title);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReplyId", DbType.String, model.ReplyId);
			SysDatabase.AddInParameter(sqlStringCommand, "@Content", DbType.String, model.Content);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string msgId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_App_MsgInfo set \r\n\t\t\t\t\t_IsDel=@_IsDel\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, msgId);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, 1);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_App_MsgInfo ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public AppMsg GetModel(string string_0)
		{
			AppMsg model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_App_MsgInfo ");
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

		public AppMsg GetModel(DataRow dataRow_0)
		{
			AppMsg appMsg = new AppMsg()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				appMsg._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				appMsg._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				appMsg._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			appMsg.Sender = dataRow_0["Sender"].ToString();
			appMsg.RecIds = dataRow_0["RecIds"].ToString();
			appMsg.RecNames = dataRow_0["RecNames"].ToString();
			if (dataRow_0["SendTime"].ToString() != "")
			{
				appMsg.SendTime = new DateTime?(DateTime.Parse(dataRow_0["SendTime"].ToString()));
			}
			appMsg.MsgType = dataRow_0["MsgType"].ToString();
			appMsg.MsgUrl = dataRow_0["MsgUrl"].ToString();
			appMsg.Title = dataRow_0["Title"].ToString();
			appMsg.ReplyId = dataRow_0["ReplyId"].ToString();
			appMsg.Content = dataRow_0["Content"].ToString();
			return appMsg;
		}

		public IList<AppMsg> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<AppMsg> appMsgs = new List<AppMsg>();
			stringBuilder.Append("select *  FROM T_E_App_MsgInfo ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				appMsgs.Add(this.GetModel(row));
			}
			return appMsgs;
		}

		public int Remove(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_App_MsgInfo ");
			stringBuilder.Append(" where _AutoID=@_AutoID ;");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Update(AppMsg model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_App_MsgInfo set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\r\n\t\t\t\t\tSender =@Sender,\r\n\t\t\t\t\tRecIds=@RecIds,\r\n\t\t\t\t\tRecNames=@RecNames,\r\n\t\t\t\t\tSendTime=@SendTime,\r\n\t\t\t\t\tMsgType=@MsgType,\r\n\t\t\t\t\tMsgUrl=@MsgUrl,\r\n\t\t\t\t\tTitle=@Title,\r\n\t\t\t\t\tReplyId=@ReplyId,\r\n\t\t\t\t\tContent=@Content\r\n\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@Sender", DbType.String, model.Sender);
			SysDatabase.AddInParameter(sqlStringCommand, "@RecIds", DbType.String, model.RecIds);
			SysDatabase.AddInParameter(sqlStringCommand, "@RecNames", DbType.String, model.RecNames);
			SysDatabase.AddInParameter(sqlStringCommand, "@SendTime", DbType.DateTime, model.SendTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@MsgType", DbType.String, model.MsgType);
			SysDatabase.AddInParameter(sqlStringCommand, "@MsgUrl", DbType.String, model.MsgUrl);
			SysDatabase.AddInParameter(sqlStringCommand, "@Title", DbType.String, model.Title);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReplyId", DbType.String, model.ReplyId);
			SysDatabase.AddInParameter(sqlStringCommand, "@Content", DbType.String, model.Content);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}