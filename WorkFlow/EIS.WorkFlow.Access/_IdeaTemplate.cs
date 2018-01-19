using EIS.AppBase;
using EIS.DataAccess;
using EIS.WorkFlow.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.WorkFlow.Access
{
	public class _IdeaTemplate
	{
		private DbTransaction _tran = null;

		public _IdeaTemplate()
		{
		}

		public _IdeaTemplate(DbTransaction tran)
		{
			this._tran = tran;
		}

		public int Add(IdeaTemplate model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_WF_IdeaTemplate (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tIdeaUser,\r\n\t\t\t\t\tIdeaName,\r\n\t\t\t\t\tOrderId\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@IdeaUser,\r\n\t\t\t\t\t@IdeaName,\r\n\t\t\t\t\t@OrderId\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "IdeaUser", DbType.String, model.IdeaUser);
			SysDatabase.AddInParameter(sqlStringCommand, "IdeaName", DbType.String, model.IdeaName);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderId", DbType.Int32, model.OrderId);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int Delete(string key)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_WF_IdeaTemplate ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, key);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_WF_IdeaTemplate ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public IdeaTemplate GetModel(string key)
		{
			IdeaTemplate model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_WF_IdeaTemplate ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, key);
			IdeaTemplate ideaTemplate = new IdeaTemplate();
            DataTable dataTable = (this._tran == null ? SysDatabase.ExecuteTable(sqlStringCommand) : SysDatabase.ExecuteTable(sqlStringCommand,this._tran));
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

		public IdeaTemplate GetModel(DataRow dr)
		{
			IdeaTemplate ideaTemplate = new IdeaTemplate()
			{
				_AutoID = dr["_AutoID"].ToString(),
				_UserName = dr["_UserName"].ToString(),
				_OrgCode = dr["_OrgCode"].ToString()
			};
			if (dr["_CreateTime"].ToString() != "")
			{
				ideaTemplate._CreateTime = DateTime.Parse(dr["_CreateTime"].ToString());
			}
			if (dr["_UpdateTime"].ToString() != "")
			{
				ideaTemplate._UpdateTime = DateTime.Parse(dr["_UpdateTime"].ToString());
			}
			if (dr["_IsDel"].ToString() != "")
			{
				ideaTemplate._IsDel = int.Parse(dr["_IsDel"].ToString());
			}
			ideaTemplate.IdeaUser = dr["IdeaUser"].ToString();
			ideaTemplate.IdeaName = dr["IdeaName"].ToString();
			if (dr["OrderId"].ToString() != "")
			{
				ideaTemplate.OrderId = int.Parse(dr["OrderId"].ToString());
			}
			return ideaTemplate;
		}

		public List<IdeaTemplate> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<IdeaTemplate> ideaTemplates = new List<IdeaTemplate>();
			stringBuilder.Append("select *  FROM T_E_WF_IdeaTemplate ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by OrderId");

            DataTable tbl = (this._tran == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this._tran));

            foreach (DataRow row in tbl.Rows)
			{
				ideaTemplates.Add(this.GetModel(row));
			}
			return ideaTemplates;
		}

		public int Update(IdeaTemplate model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_WF_IdeaTemplate set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tIdeaUser=@IdeaUser,\r\n\t\t\t\t\tIdeaName=@IdeaName,\r\n\t\t\t\t\tOrderId=@OrderId\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "IdeaUser", DbType.String, model.IdeaUser);
			SysDatabase.AddInParameter(sqlStringCommand, "IdeaName", DbType.String, model.IdeaName);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderId", DbType.Int32, model.OrderId);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}
	}
}