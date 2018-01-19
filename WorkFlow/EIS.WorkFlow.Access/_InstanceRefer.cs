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
	public class _InstanceRefer
	{
		private DbTransaction _tran = null;

		public _InstanceRefer()
		{
		}

		public _InstanceRefer(DbTransaction tran)
		{
			this._tran = tran;
		}

		public int Add(InstanceRefer model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_WF_InstanceRefer (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tInstanceId,\r\n\t\t\t\t\tReferId,\r\n\t\t\t\t\tReferName,\r\n\t\t\t\t\tOrderID\r\n\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@InstanceId,\r\n\t\t\t\t\t@ReferId,\r\n\t\t\t\t\t@ReferName,\r\n\t\t\t\t\t@OrderID\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "InstanceId", DbType.String, model.InstanceId);
			SysDatabase.AddInParameter(sqlStringCommand, "ReferId", DbType.String, model.ReferId);
			SysDatabase.AddInParameter(sqlStringCommand, "ReferName", DbType.String, model.ReferName);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderID", DbType.Int32, model.OrderID);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int Delete(string key)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_WF_InstanceRefer ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, key);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  From T_E_WF_InstanceRefer ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by orderId ");
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public InstanceRefer GetModel(DataRow dr)
		{
			InstanceRefer instanceRefer = new InstanceRefer()
			{
				_AutoID = dr["_AutoID"].ToString(),
				_UserName = dr["_UserName"].ToString(),
				_OrgCode = dr["_OrgCode"].ToString()
			};
			if (dr["_CreateTime"].ToString() != "")
			{
				instanceRefer._CreateTime = DateTime.Parse(dr["_CreateTime"].ToString());
			}
			if (dr["_UpdateTime"].ToString() != "")
			{
				instanceRefer._UpdateTime = DateTime.Parse(dr["_UpdateTime"].ToString());
			}
			if (dr["_IsDel"].ToString() != "")
			{
				instanceRefer._IsDel = int.Parse(dr["_IsDel"].ToString());
			}
			instanceRefer.InstanceId = dr["InstanceId"].ToString();
			instanceRefer.ReferId = dr["ReferId"].ToString();
			instanceRefer.ReferName = dr["ReferName"].ToString();
			if (dr["OrderID"].ToString() != "")
			{
				instanceRefer.OrderID = int.Parse(dr["OrderID"].ToString());
			}
			return instanceRefer;
		}

		public List<InstanceRefer> GetModelList(string strWhere)
		{
			List<InstanceRefer> instanceRefers = new List<InstanceRefer>();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  From T_E_WF_InstanceRefer ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by orderId ");
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				instanceRefers.Add(this.GetModel(row));
			}
			return instanceRefers;
		}

		public int Update(InstanceRefer model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_WF_InstanceRefer set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tInstanceId=@InstanceId,\r\n\t\t\t\t\tReferId=@ReferId,\r\n\t\t\t\t\tReferName=@ReferName,\r\n\t\t\t\t\tOrderID=@OrderID\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "InstanceId", DbType.String, model.InstanceId);
			SysDatabase.AddInParameter(sqlStringCommand, "ReferId", DbType.String, model.ReferId);
			SysDatabase.AddInParameter(sqlStringCommand, "ReferName", DbType.String, model.ReferName);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderID", DbType.Int32, model.OrderID);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}
	}
}