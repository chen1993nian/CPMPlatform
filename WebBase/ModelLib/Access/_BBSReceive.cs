using EIS.AppBase;
using EIS.DataAccess;
using EIS.Web.ModelLib.Model;
using System;
using System.Data;
using System.Data.Common;
using System.Text;
using EIS.WebBase.ModelLib.Model;
namespace EIS.WebBase.ModelLib.Access
{
	public class _BBSReceive
	{
		private DbTransaction dbTransaction_0 = null;

		public _BBSReceive()
		{
		}

		public _BBSReceive(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(BBSReceive model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_BBS_Receive   (\r\n \t\t\t\t\t    _AutoID,\r\n\t\t\t\t\t    _UserName,\r\n\t\t\t\t\t    _OrgCode,\r\n\t\t\t\t\t    _CreateTime,\r\n\t\t\t\t\t    _UpdateTime,\r\n\t\t\t\t\t    _IsDel,\r\n                        _CompanyId,\r\n                        SubjectId,\r\n\t\t\t\t\t    ReadTime,\r\n                        IsRead,\r\n                        RecId,\r\n                        SubScribe\r\n\t\t\t    ) values(\r\n\t\t\t\t\t    @_AutoID,\r\n\t\t\t\t\t    @_UserName,\r\n\t\t\t\t\t    @_OrgCode,\r\n\t\t\t\t\t    @_CreateTime,\r\n\t\t\t\t\t    @_UpdateTime,\r\n\t\t\t\t\t    @_IsDel,\r\n\t\t\t\t\t    @_CompanyId,\r\n\r\n                        @SubjectId,\r\n\t\t\t\t\t    @ReadTime,\r\n                        @IsRead,\r\n                        @RecId,\r\n                        @SubScribe\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CompanyId", DbType.String, model._CompanyID);
			SysDatabase.AddInParameter(sqlStringCommand, "@SubjectId", DbType.String, model.SubjectId);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReadTime", DbType.DateTime, model.ReadTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@IsRead", DbType.Int32, model.IsRead);
			SysDatabase.AddInParameter(sqlStringCommand, "@RecId", DbType.String, model.RecId);
			SysDatabase.AddInParameter(sqlStringCommand, "@SubScribe", DbType.String, model.SubScribe);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public bool Exists(string topicId, string recId)
		{
			bool flag;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from T_BBS_Receive");
			stringBuilder.Append(" where SubjectId=@SubjectId and  RecId=@RecId");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@SubjectId", DbType.String, topicId);
			SysDatabase.AddInParameter(sqlStringCommand, "@RecId", DbType.String, recId);
			flag = (this.dbTransaction_0 == null ? Convert.ToInt32(SysDatabase.ExecuteScalar(sqlStringCommand)) > 0 : Convert.ToInt32(SysDatabase.ExecuteScalar(sqlStringCommand, this.dbTransaction_0)) > 0);
			return flag;
		}

		public BBSReceive GetModel(string topicId, string recId)
		{
			BBSReceive model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_BBS_Receive  ");
			stringBuilder.Append(" where SubjectId=@SubjectId and  RecId=@RecId ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@SubjectId", DbType.String, topicId);
			SysDatabase.AddInParameter(sqlStringCommand, "@RecId", DbType.String, recId);
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

		public BBSReceive GetModel(DataRow dataRow_0)
		{
			BBSReceive bBSReceive = new BBSReceive()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				bBSReceive._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				bBSReceive._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				bBSReceive._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			bBSReceive._CompanyID = dataRow_0["_CompanyID"].ToString();
			bBSReceive.SubjectId = dataRow_0["SubjectId"].ToString();
			bBSReceive.RecId = dataRow_0["RecId"].ToString();
			bBSReceive.SubScribe = dataRow_0["SubScribe"].ToString();
			if (dataRow_0["IsRead"].ToString() != "")
			{
				bBSReceive.IsRead = int.Parse(dataRow_0["IsRead"].ToString());
			}
			if (dataRow_0["ReadTime"].ToString() != "")
			{
				bBSReceive.ReadTime = new DateTime?(DateTime.Parse(dataRow_0["ReadTime"].ToString()));
			}
			return bBSReceive;
		}

		public int Update(BBSReceive model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_BBS_Receive   set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n                    ReadTime=@ReadTime,\r\n                    IsRead=@IsRead,\r\n                    SubScribe=@SubScribe\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReadTime", DbType.DateTime, model.ReadTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@IsRead", DbType.Int32, model.IsRead);
			SysDatabase.AddInParameter(sqlStringCommand, "@SubScribe", DbType.String, model.SubScribe);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}