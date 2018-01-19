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
	public class _BBSTopic
	{
		private DbTransaction dbTransaction_0 = null;

		public _BBSTopic()
		{
		}

		public _BBSTopic(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(BBSTopic model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_BBS_Topic  (\r\n \t\t\t\t\t    _AutoID,\r\n\t\t\t\t\t    _UserName,\r\n\t\t\t\t\t    _OrgCode,\r\n\t\t\t\t\t    _CreateTime,\r\n\t\t\t\t\t    _UpdateTime,\r\n\t\t\t\t\t    _IsDel,\r\n                        _CompanyId,\r\n                        Title,\r\n\t\t\t\t\t    EmpName,\r\n                        Content,\r\n                        AttachId,\r\n                        Enable,\r\n                        NmEnable,\r\n                        StartTime,\r\n                        EndTime,\r\n                        ToUserId,\r\n                        ToUserName,\r\n                        DeptName,\r\n                        BBSType,\r\n                        LastUpdateTime,\r\n                        State,\r\n                        ToDeptId,\r\n                        ToDeptName,\r\n                        ReplyCount,\r\n                        BizId,\r\n                        BizName\r\n\t\t\t    ) values(\r\n\t\t\t\t\t    @_AutoID,\r\n\t\t\t\t\t    @_UserName,\r\n\t\t\t\t\t    @_OrgCode,\r\n\t\t\t\t\t    @_CreateTime,\r\n\t\t\t\t\t    @_UpdateTime,\r\n\t\t\t\t\t    @_IsDel,\r\n\t\t\t\t\t    @_CompanyId,\r\n\r\n                        @Title,\r\n\t\t\t\t\t    @EmpName,\r\n                        @Content,\r\n                        @AttachId,\r\n                        @Enable,\r\n                        @NmEnable,\r\n                        @StartTime,\r\n                        @EndTime,\r\n                        @ToUserId,\r\n                        @ToUserName,\r\n                        @DeptName,\r\n                        @BBSType,\r\n                        @LastUpdateTime,\r\n                        @State,\r\n                        @ToDeptId,\r\n                        @ToDeptName,\r\n                        @ReplyCount,\r\n                        @BizId,\r\n                        @BizName\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CompanyId", DbType.String, model._CompanyID);
			SysDatabase.AddInParameter(sqlStringCommand, "@Title", DbType.String, model.Title);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmpName", DbType.String, model.EmpName);
			SysDatabase.AddInParameter(sqlStringCommand, "@Content", DbType.String, model.Content);
			SysDatabase.AddInParameter(sqlStringCommand, "@AttachId", DbType.String, model.AttachId);
			SysDatabase.AddInParameter(sqlStringCommand, "@Enable", DbType.String, model.Enable);
			SysDatabase.AddInParameter(sqlStringCommand, "@NmEnable", DbType.String, model.NmEnable);
			SysDatabase.AddInParameter(sqlStringCommand, "@StartTime", DbType.DateTime, model.StartTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@EndTime", DbType.DateTime, model.EndTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@ToUserId", DbType.String, model.ToUserId);
			SysDatabase.AddInParameter(sqlStringCommand, "@ToUserName", DbType.String, model.ToUserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@DeptName", DbType.String, model.DeptName);
			SysDatabase.AddInParameter(sqlStringCommand, "@BBSType", DbType.String, model.BBSType);
			SysDatabase.AddInParameter(sqlStringCommand, "@LastUpdateTime", DbType.DateTime, model.LastUpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@State", DbType.String, model.State);
			SysDatabase.AddInParameter(sqlStringCommand, "@ToDeptId", DbType.String, model.ToDeptId);
			SysDatabase.AddInParameter(sqlStringCommand, "@ToDeptName", DbType.String, model.ToDeptName);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReplyCount", DbType.Int32, model.ReplyCount);
			SysDatabase.AddInParameter(sqlStringCommand, "@BizId", DbType.String, model.BizId);
			SysDatabase.AddInParameter(sqlStringCommand, "@BizName", DbType.String, model.BizName);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_BBS_Topic  ");
			stringBuilder.Append(" where _AutoID=@_AutoID ;");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  From T_BBS_Topic  ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public BBSTopic GetModel(string string_0)
		{
			BBSTopic model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_BBS_Topic  ");
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

		public BBSTopic GetModel(DataRow dataRow_0)
		{
			BBSTopic bBSTopic = new BBSTopic()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				bBSTopic._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				bBSTopic._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				bBSTopic._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			bBSTopic._CompanyID = dataRow_0["_CompanyID"].ToString();
			bBSTopic.Title = dataRow_0["Title"].ToString();
			bBSTopic.EmpName = dataRow_0["EmpName"].ToString();
			bBSTopic.Content = dataRow_0["Content"].ToString();
			bBSTopic.AttachId = dataRow_0["AttachId"].ToString();
			bBSTopic.Enable = dataRow_0["Enable"].ToString();
			bBSTopic.NmEnable = dataRow_0["NmEnable"].ToString();
			bBSTopic.ToUserId = dataRow_0["ToUserId"].ToString();
			bBSTopic.ToUserName = dataRow_0["ToUserName"].ToString();
			bBSTopic.DeptName = dataRow_0["DeptName"].ToString();
			bBSTopic.BBSType = dataRow_0["BBSType"].ToString();
			bBSTopic.State = dataRow_0["State"].ToString();
			bBSTopic.ToDeptId = dataRow_0["ToDeptId"].ToString();
			bBSTopic.ToDeptName = dataRow_0["ToDeptName"].ToString();
			bBSTopic.BizId = dataRow_0["BizId"].ToString();
			bBSTopic.BizName = dataRow_0["BizName"].ToString();
			if (dataRow_0["StartTime"].ToString() != "")
			{
				bBSTopic.StartTime = new DateTime?(DateTime.Parse(dataRow_0["StartTime"].ToString()));
			}
			if (dataRow_0["EndTime"].ToString() != "")
			{
				bBSTopic.EndTime = new DateTime?(DateTime.Parse(dataRow_0["EndTime"].ToString()));
			}
			if (dataRow_0["LastUpdateTime"].ToString() != "")
			{
				bBSTopic.LastUpdateTime = new DateTime?(DateTime.Parse(dataRow_0["LastUpdateTime"].ToString()));
			}
			if (dataRow_0["ReplyCount"].ToString() != "")
			{
				bBSTopic._IsDel = int.Parse(dataRow_0["ReplyCount"].ToString());
			}
			return bBSTopic;
		}

		public List<BBSTopic> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<BBSTopic> bBSTopics = new List<BBSTopic>();
			stringBuilder.Append("select *  From T_BBS_Topic  ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				bBSTopics.Add(this.GetModel(row));
			}
			return bBSTopics;
		}

		public int Update(BBSTopic model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_BBS_Topic  set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\r\n                    Content=@Content ,\r\n                    Enable=@Enable ,\r\n                    NmEnable=@NmEnable ,\r\n                    EndTime=@EndTime ,\r\n                    ToUserId=@ToUserId,\r\n                    ToUserName=@ToUserName ,\r\n                    LastUpdateTime=@LastUpdateTime ,\r\n                    State=@State ,\r\n                    ToDeptId=@ToDeptId ,\r\n                    ToDeptName=@ToDeptName ,\r\n                    ReplyCount=@ReplyCount\r\n\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@Title", DbType.String, model.Title);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmpName", DbType.String, model.EmpName);
			SysDatabase.AddInParameter(sqlStringCommand, "@Content", DbType.String, model.Content);
			SysDatabase.AddInParameter(sqlStringCommand, "@AttachId", DbType.String, model.AttachId);
			SysDatabase.AddInParameter(sqlStringCommand, "@Enable", DbType.String, model.Enable);
			SysDatabase.AddInParameter(sqlStringCommand, "@NmEnable", DbType.String, model.NmEnable);
			SysDatabase.AddInParameter(sqlStringCommand, "@StartTime", DbType.DateTime, model.StartTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@EndTime", DbType.DateTime, model.EndTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@ToUserId", DbType.String, model.ToUserId);
			SysDatabase.AddInParameter(sqlStringCommand, "@ToUserName", DbType.String, model.ToUserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@DeptName", DbType.String, model.DeptName);
			SysDatabase.AddInParameter(sqlStringCommand, "@BBSType", DbType.String, model.BBSType);
			SysDatabase.AddInParameter(sqlStringCommand, "@LastUpdateTime", DbType.DateTime, model.LastUpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@State", DbType.String, model.State);
			SysDatabase.AddInParameter(sqlStringCommand, "@ToDeptId", DbType.String, model.ToDeptId);
			SysDatabase.AddInParameter(sqlStringCommand, "@ToDeptName", DbType.String, model.ToDeptName);
			SysDatabase.AddInParameter(sqlStringCommand, "@ReplyCount", DbType.Int32, model.ReplyCount);
			SysDatabase.AddInParameter(sqlStringCommand, "@BizId", DbType.String, model.BizId);
			SysDatabase.AddInParameter(sqlStringCommand, "@BizName", DbType.String, model.BizName);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}