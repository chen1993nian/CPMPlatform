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
	public class _AppSms
	{
		private DbTransaction dbTransaction_0 = null;

		public _AppSms()
		{
		}

		public _AppSms(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(AppSms model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_App_Sms (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tSenderId,\r\n\t\t\t\t\tSenderName,\r\n\t\t\t\t\tRecIds,\r\n\t\t\t\t\tRecNames,\r\n\t\t\t\t\tRecPhone,\r\n\t\t\t\t\tState,\r\n\t\t\t\t\tSendTime,\r\n\t\t\t\t\tAppId,\r\n\t\t\t\t\tAppName,\r\n\t\t\t\t\tCompanyId,\r\n\t\t\t\t\tContent\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@SenderId,\r\n\t\t\t\t\t@SenderName,\r\n\t\t\t\t\t@RecIds,\r\n\t\t\t\t\t@RecNames,\r\n\t\t\t\t\t@RecPhone,\r\n\t\t\t\t\t@State,\r\n\t\t\t\t\t@SendTime,\r\n\t\t\t\t\t@AppId,\r\n\t\t\t\t\t@AppName,\r\n\t\t\t\t\t@CompanyId,\r\n\t\t\t\t\t@Content\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@SenderId", DbType.String, model.SenderId);
			SysDatabase.AddInParameter(sqlStringCommand, "@SenderName", DbType.String, model.SenderName);
			SysDatabase.AddInParameter(sqlStringCommand, "@RecIds", DbType.String, model.RecIds);
			SysDatabase.AddInParameter(sqlStringCommand, "@RecNames", DbType.String, model.RecNames);
			SysDatabase.AddInParameter(sqlStringCommand, "@RecPhone", DbType.String, model.RecPhone);
			SysDatabase.AddInParameter(sqlStringCommand, "@State", DbType.String, model.State);
			SysDatabase.AddInParameter(sqlStringCommand, "@SendTime", DbType.String, model.SendTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@AppId", DbType.String, model.AppId);
			SysDatabase.AddInParameter(sqlStringCommand, "@AppName", DbType.String, model.AppName);
			SysDatabase.AddInParameter(sqlStringCommand, "@CompanyId", DbType.String, model.CompanyId);
			SysDatabase.AddInParameter(sqlStringCommand, "@Content", DbType.String, model.Content);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_App_Sms ");
			stringBuilder.Append(" where _AutoID=@_AutoID ;");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_App_Sms ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public AppSms GetModel(string string_0)
		{
			AppSms model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_App_Sms ");
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

		public AppSms GetModel(DataRow dataRow_0)
		{
			AppSms appSm = new AppSms()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				appSm._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				appSm._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				appSm._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			appSm.SenderId = dataRow_0["SenderId"].ToString();
			appSm.SenderName = dataRow_0["SenderName"].ToString();
			appSm.RecIds = dataRow_0["RecIds"].ToString();
			appSm.RecNames = dataRow_0["RecNames"].ToString();
			appSm.RecPhone = dataRow_0["RecPhone"].ToString();
			if (dataRow_0["SendTime"].ToString() != "")
			{
				appSm.SendTime = new DateTime?(DateTime.Parse(dataRow_0["SendTime"].ToString()));
			}
			appSm.State = dataRow_0["State"].ToString();
			appSm.AppId = dataRow_0["AppId"].ToString();
			appSm.AppName = dataRow_0["AppName"].ToString();
			appSm.CompanyId = dataRow_0["CompanyId"].ToString();
			appSm.Content = dataRow_0["Content"].ToString();
			return appSm;
		}

		public IList<AppSms> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<AppSms> appSms = new List<AppSms>();
			stringBuilder.Append("select *  FROM T_E_App_Sms ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				appSms.Add(this.GetModel(row));
			}
			return appSms;
		}

		public int Update(AppSms model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_App_Sms set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\r\n\t\t\t\t\tSenderId =@SenderId,\r\n\t\t\t\t\tSenderName=@SenderName,\r\n\t\t\t\t\tRecIds=@RecIds,\r\n\t\t\t\t\tRecNames=@RecNames,\r\n\t\t\t\t\tRecPhone=@RecPhone,\r\n\t\t\t\t\tState=@State,\r\n\t\t\t\t\tSendTime=@SendTime,\r\n\t\t\t\t\tAppId=@AppId,\r\n\t\t\t\t\tAppName=@AppName,\r\n\t\t\t\t\tCompanyId=@CompanyId,\r\n\t\t\t\t\tContent=@Content\r\n\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@SenderId", DbType.String, model.SenderId);
			SysDatabase.AddInParameter(sqlStringCommand, "@SenderName", DbType.String, model.SenderName);
			SysDatabase.AddInParameter(sqlStringCommand, "@RecIds", DbType.String, model.RecIds);
			SysDatabase.AddInParameter(sqlStringCommand, "@RecNames", DbType.String, model.RecNames);
			SysDatabase.AddInParameter(sqlStringCommand, "@RecPhone", DbType.String, model.RecPhone);
			SysDatabase.AddInParameter(sqlStringCommand, "@State", DbType.String, model.State);
			SysDatabase.AddInParameter(sqlStringCommand, "@SendTime", DbType.String, model.SendTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@AppId", DbType.String, model.AppId);
			SysDatabase.AddInParameter(sqlStringCommand, "@AppName", DbType.String, model.AppName);
			SysDatabase.AddInParameter(sqlStringCommand, "@CompanyId", DbType.String, model.CompanyId);
			SysDatabase.AddInParameter(sqlStringCommand, "@Content", DbType.String, model.Content);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}