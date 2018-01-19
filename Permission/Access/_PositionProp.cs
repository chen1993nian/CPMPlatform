using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace EIS.Permission.Access
{
	public class _PositionProp
	{
		private DbTransaction dbTransaction_0 = null;

		public _PositionProp()
		{
		}

		public _PositionProp(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Org_PositionProp ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			DataTable item = SysDatabase.ExecuteDataSet(stringBuilder.ToString()).Tables[0];
			return item;
		}

		public PositionProp GetModel(string string_0)
		{
			PositionProp model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Org_PositionProp ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			DataTable item = SysDatabase.ExecuteDataSet(sqlStringCommand).Tables[0];
			if (item.Rows.Count <= 0)
			{
				model = null;
			}
			else
			{
				model = this.GetModel(item.Rows[0]);
			}
			return model;
		}

		public PositionProp GetModel(DataRow dataRow_0)
		{
			PositionProp positionProp = new PositionProp()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				positionProp._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				positionProp._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				positionProp._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			positionProp.PropName = dataRow_0["PropName"].ToString();
			positionProp.SearchScope = dataRow_0["SearchScope"].ToString();
			if (dataRow_0["OrderID"].ToString() != "")
			{
				positionProp.OrderID = int.Parse(dataRow_0["OrderID"].ToString());
			}
			return positionProp;
		}

		public List<PositionProp> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<PositionProp> positionProps = new List<PositionProp>();
			stringBuilder.Append("select *  FROM T_E_Org_PositionProp ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			DataTable item = SysDatabase.ExecuteDataSet(stringBuilder.ToString()).Tables[0];
			foreach (DataRow row in item.Rows)
			{
				positionProps.Add(this.GetModel(row));
			}
			return positionProps;
		}
	}
}