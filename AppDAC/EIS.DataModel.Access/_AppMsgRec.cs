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
	public class _AppMsgRec
	{
		private DbTransaction dbTransaction_0 = null;

		public _AppMsgRec()
		{
		}

		public _AppMsgRec(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(AppMsgRec model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_App_MsgRec (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tMsgId,\r\n\t\t\t\t\tRecId,\r\n\t\t\t\t\tIsRead,\r\n\t\t\t\t\tReadTime\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@MsgId,\r\n\t\t\t\t\t@RecId,\r\n\t\t\t\t\t@IsRead,\r\n\t\t\t\t\t@ReadTime\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@MsgId", DbType.String, model.MsgId);
			SysDatabase.AddInParameter(sqlStringCommand, "@RecId", DbType.String, model.RecId);
			SysDatabase.AddInParameter(sqlStringCommand, "@IsRead", DbType.Int32, model.IsRead);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReadTime", DbType.DateTime, model.ReadTime);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_App_MsgRec ");
			stringBuilder.Append(" where _AutoID=@_AutoID ;");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_App_MsgRec ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public AppMsgRec GetModel(string string_0)
		{
			AppMsgRec model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_App_MsgRec ");
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

		public AppMsgRec GetModel(DataRow dataRow_0)
		{
			AppMsgRec appMsgRec = new AppMsgRec()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				appMsgRec._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				appMsgRec._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				appMsgRec._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			appMsgRec.MsgId = dataRow_0["MsgId"].ToString();
			appMsgRec.RecId = dataRow_0["RecId"].ToString();
			if (dataRow_0["IsRead"].ToString() != "")
			{
				appMsgRec.IsRead = int.Parse(dataRow_0["IsRead"].ToString());
			}
			if (dataRow_0["ReadTime"].ToString() != "")
			{
				appMsgRec.ReadTime = new DateTime?(DateTime.Parse(dataRow_0["ReadTime"].ToString()));
			}
			return appMsgRec;
		}

		public IList<AppMsgRec> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<AppMsgRec> appMsgRecs = new List<AppMsgRec>();
			stringBuilder.Append("select *  FROM T_E_App_MsgRec ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				appMsgRecs.Add(this.GetModel(row));
			}
			return appMsgRecs;
		}

		public int Update(AppMsgRec model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_App_MsgRec set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\r\n\t\t\t\t\tMsgId =@MsgId,\r\n\t\t\t\t\tRecId=@RecId,\r\n\t\t\t\t\tIsRead=@IsRead,\r\n\t\t\t\t\tReadTime=@ReadTime\r\n\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@MsgId", DbType.String, model.MsgId);
			SysDatabase.AddInParameter(sqlStringCommand, "@RecId", DbType.String, model.RecId);
			SysDatabase.AddInParameter(sqlStringCommand, "@IsRead", DbType.Int32, model.IsRead);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReadTime", DbType.DateTime, model.ReadTime);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}