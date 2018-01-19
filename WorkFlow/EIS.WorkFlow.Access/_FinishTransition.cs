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
	public class _FinishTransition
	{
		private DbTransaction _tran = null;

		public _FinishTransition()
		{
		}

		public _FinishTransition(DbTransaction tran)
		{
			this._tran = tran;
		}

		public int Add(FinishTransition model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_WF_FinishTransition (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tInstanceId,\r\n\t\t\t\t\tTransitionId,\r\n\t\t\t\t\tFromActivity,\r\n\t\t\t\t\tToActivity\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@InstanceId,\r\n\t\t\t\t\t@TransitionId,\r\n\t\t\t\t\t@FromActivity,\r\n\t\t\t\t\t@ToActivity\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "InstanceId", DbType.String, model.InstanceId);
			SysDatabase.AddInParameter(sqlStringCommand, "TransitionId", DbType.String, model.TransitionId);
			SysDatabase.AddInParameter(sqlStringCommand, "FromActivity", DbType.String, model.FromActivity);
			SysDatabase.AddInParameter(sqlStringCommand, "ToActivity", DbType.String, model.ToActivity);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int Delete(string key)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_WF_FinishTransition ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, key);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int DeleteByFromActId(string instanceId, string fromActId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_WF_FinishTransition ");
			stringBuilder.Append(" where instanceId=@instanceId and fromActivity=@fromActivity");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "instanceId", DbType.String, instanceId);
			SysDatabase.AddInParameter(sqlStringCommand, "fromActivity", DbType.String, fromActId);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int DeleteByInstanceId(string instanceId)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_WF_FinishTransition ");
			stringBuilder.Append(" where instanceId=@instanceId ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "instanceId", DbType.String, instanceId);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			DataTable dataTable;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_WF_FinishTransition ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			dataTable = (this._tran == null ? SysDatabase.ExecuteTable(sqlStringCommand) : SysDatabase.ExecuteTable(sqlStringCommand, this._tran));
			return dataTable;
		}

		public FinishTransition GetModel(string key)
		{
			FinishTransition model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_WF_FinishTransition ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, key);
			Catalog catalog = new Catalog();
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

		public FinishTransition GetModel(DataRow dr)
		{
			FinishTransition finishTransition = new FinishTransition()
			{
				_AutoID = dr["_AutoID"].ToString(),
				_UserName = dr["_UserName"].ToString(),
				_OrgCode = dr["_OrgCode"].ToString()
			};
			if (dr["_CreateTime"].ToString() != "")
			{
				finishTransition._CreateTime = DateTime.Parse(dr["_CreateTime"].ToString());
			}
			if (dr["_UpdateTime"].ToString() != "")
			{
				finishTransition._UpdateTime = DateTime.Parse(dr["_UpdateTime"].ToString());
			}
			if (dr["_IsDel"].ToString() != "")
			{
				finishTransition._IsDel = int.Parse(dr["_IsDel"].ToString());
			}
			finishTransition.InstanceId = dr["InstanceId"].ToString();
			finishTransition.TransitionId = dr["TransitionId"].ToString();
			finishTransition.FromActivity = dr["FromActivity"].ToString();
			finishTransition.ToActivity = dr["ToActivity"].ToString();
			return finishTransition;
		}

		public List<FinishTransition> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<FinishTransition> finishTransitions = new List<FinishTransition>();
			stringBuilder.Append("select *  FROM T_E_WF_FinishTransition ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			DataTable dataTable = new DataTable();
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			dataTable = (this._tran == null ? SysDatabase.ExecuteTable(sqlStringCommand) : SysDatabase.ExecuteTable(sqlStringCommand, this._tran));
			foreach (DataRow row in dataTable.Rows)
			{
				finishTransitions.Add(this.GetModel(row));
			}
			return finishTransitions;
		}

		public bool IsExist(string InstanceId, string TransitionId)
		{
			bool flag;
			string str = string.Format("select count(*) from T_E_WF_FinishTransition where InstanceId='{0}' and TransitionId='{1}'", InstanceId, TransitionId);
			flag = (this._tran != null ? Convert.ToInt32(SysDatabase.ExecuteScalar(str, this._tran)) > 0 : Convert.ToInt32(SysDatabase.ExecuteScalar(str)) > 0);
			return flag;
		}

		public int Update(FinishTransition model)
		{
			return 0;
		}
	}
}