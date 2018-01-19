using EIS.AppBase;
using EIS.DataAccess;
using EIS.Web.ModelLib.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.Web.ModelLib.Access
{
	public class _DaPosition
	{
		private DbTransaction dbTransaction_0 = null;

		public _DaPosition()
		{
		}

		public _DaPosition(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(DaPosition model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_OA_DA_Position (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tOrderId,\r\n\t\t\t\t\tPositionPId,\r\n\t\t\t\t\tPositionName,\r\n\t\t\t\t\tNote\r\n\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@OrderId,\r\n\t\t\t\t\t@PositionPId,\r\n\t\t\t\t\t@PositionName,\r\n\t\t\t\t\t@Note\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@OrderId", DbType.Int32, model.OrderId);
			SysDatabase.AddInParameter(sqlStringCommand, "@PositionName", DbType.String, model.PositionName);
			SysDatabase.AddInParameter(sqlStringCommand, "@Note", DbType.String, model.Note);
			SysDatabase.AddInParameter(sqlStringCommand, "@PositionPId", DbType.String, model.PositionPId);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_OA_DA_Position ");
			stringBuilder.Append(" where _AutoID=@_AutoID ;");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_OA_DA_Position ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by OrderId ");
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public DaPosition GetModel(string string_0)
		{
			DaPosition model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_OA_DA_Position ");
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

		public DaPosition GetModel(DataRow dataRow_0)
		{
			DaPosition daPosition = new DaPosition()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				daPosition._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				daPosition._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				daPosition._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			if (dataRow_0["OrderId"].ToString() != "")
			{
				daPosition.OrderId = int.Parse(dataRow_0["OrderId"].ToString());
			}
			daPosition.PositionName = dataRow_0["PositionName"].ToString();
			daPosition.Note = dataRow_0["Note"].ToString();
			daPosition.PositionPId = dataRow_0["PositionPId"].ToString();
			return daPosition;
		}

		public List<DaPosition> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<DaPosition> daPositions = new List<DaPosition>();
			stringBuilder.Append("select *  FROM T_OA_DA_Position ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by OrderId ");
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				daPositions.Add(this.GetModel(row));
			}
			return daPositions;
		}

		public int GetNewOrd(string pnodewbs)
		{
			int num;
			string str = string.Concat("select max(OrderId)+1 from T_OA_DA_Position where PositionPId='", pnodewbs, "'");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(str.ToString());
			object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
			num = (obj != DBNull.Value ? int.Parse(obj.ToString()) : 1);
			return num;
		}

		public int Update(DaPosition model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_OA_DA_Position set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\r\n\t\t\t\t\tOrderId =@OrderId,\r\n\t\t\t\t\tPositionName=@PositionName,\r\n\t\t\t\t\tPositionPId=@PositionPId,\r\n\t\t\t\t\tNote=@Note\r\n\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@OrderId", DbType.Int32, model.OrderId);
			SysDatabase.AddInParameter(sqlStringCommand, "@PositionName", DbType.String, model.PositionName);
			SysDatabase.AddInParameter(sqlStringCommand, "@PositionPId", DbType.String, model.PositionPId);
			SysDatabase.AddInParameter(sqlStringCommand, "@Note", DbType.String, model.Note);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}