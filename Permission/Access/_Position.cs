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
	public class _Position
	{
		private DbTransaction dbTransaction_0 = null;

		public _Position()
		{
		}

		public _Position(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(Position model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Org_Position (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tPositionName,\r\n\t\t\t\t\tPositionCode,\r\n\t\t\t\t\tParentPositionId,\r\n\t\t\t\t\tParentPositionName,\r\n\t\t\t\t\tPropName,\r\n\t\t\t\t\tPropId,\r\n\t\t\t\t\tRankName,\r\n\t\t\t\t\tRankCode,\r\n\t\t\t\t\tDeptID,\r\n\t\t\t\t\tOrderID\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@PositionName,\r\n\t\t\t\t\t@PositionCode,\r\n\t\t\t\t\t@ParentPositionId,\r\n\t\t\t\t\t@ParentPositionName,\r\n\t\t\t\t\t@PropName,\r\n                    @PropId,\r\n\t\t\t\t\t@RankName,\r\n\t\t\t\t\t@RankCode,\r\n\t\t\t\t\t@DeptID,\r\n\t\t\t\t\t@OrderID\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "PositionName", DbType.String, model.PositionName);
			SysDatabase.AddInParameter(sqlStringCommand, "PositionCode", DbType.String, model.PositionCode);
			SysDatabase.AddInParameter(sqlStringCommand, "ParentPositionId", DbType.String, model.ParentPositionId);
			SysDatabase.AddInParameter(sqlStringCommand, "ParentPositionName", DbType.String, model.ParentPositionName);
			SysDatabase.AddInParameter(sqlStringCommand, "PropName", DbType.String, model.PropName);
			SysDatabase.AddInParameter(sqlStringCommand, "PropId", DbType.String, model.PropId);
			SysDatabase.AddInParameter(sqlStringCommand, "RankName", DbType.String, model.RankName);
			SysDatabase.AddInParameter(sqlStringCommand, "RankCode", DbType.String, model.RankCode);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptID", DbType.String, model.DeptID);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderID", DbType.Int32, model.OrderID);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Org_Position ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Org_Position ,(select DeptName from T_E_Org_Department d where d._autoid=p.deptId) DeptName  FROM T_E_Org_Position p ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public int GetMaxOrder(string deptId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select max(OrderId) ");
			stringBuilder.Append(" FROM T_E_Org_Position ");
			stringBuilder.Append(string.Concat(" where deptId='", deptId, "'"));
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
			return (obj == DBNull.Value ? 0 : Convert.ToInt32(obj));
		}

		public Position GetModel(string string_0)
		{
			Position model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 *,(select DeptName from T_E_Org_Department d where d._autoid=p.deptId) DeptName from T_E_Org_Position p");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			Position position = new Position();
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

		public Position GetModel(DataRow dataRow_0)
		{
			Position position = new Position()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				position._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				position._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				position._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			position.PositionName = dataRow_0["PositionName"].ToString();
			position.PositionCode = dataRow_0["PositionCode"].ToString();
			position.ParentPositionId = dataRow_0["ParentPositionId"].ToString();
			position.ParentPositionName = dataRow_0["ParentPositionName"].ToString();
			position.PropName = dataRow_0["PropName"].ToString();
			position.PropId = dataRow_0["PropId"].ToString();
			position.RankName = dataRow_0["RankName"].ToString();
			position.RankCode = dataRow_0["RankCode"].ToString();
			position.DeptID = dataRow_0["DeptID"].ToString();
			position.DeptName = dataRow_0["DeptName"].ToString();
			if (dataRow_0["OrderID"].ToString() != "")
			{
				position.OrderID = int.Parse(dataRow_0["OrderID"].ToString());
			}
			return position;
		}

		public Position GetModelByName(string positionName, string deptId)
		{
			Position model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * ,(select DeptName from T_E_Org_Department d where d._autoid=p.deptId) DeptName  FROM T_E_Org_Position p  ");
			stringBuilder.Append(" where PositionName=@PositionName and DeptId=@DeptId");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "PositionName", DbType.String, positionName);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptId", DbType.String, deptId);
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(sqlStringCommand) : SysDatabase.ExecuteTable(sqlStringCommand, this.dbTransaction_0));
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

		public List<Position> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Position> positions = new List<Position>();
			stringBuilder.Append("select *,(select DeptName from T_E_Org_Department d where d._autoid=p.deptId) DeptName  FROM T_E_Org_Position p ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by OrderID");
			DataTable dataTable = new DataTable();
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
			foreach (DataRow row in dataTable.Rows)
			{
				positions.Add(this.GetModel(row));
			}
			return positions;
		}

		public List<Position> GetModelListByDeptId(string deptId)
		{
			return this.GetModelList(string.Concat(" deptId ='", deptId, "'"));
		}

		public List<Position> GetModelListByPropId(string propId)
		{
			return this.GetModelList(string.Concat(" PropId ='", propId, "'"));
		}

		public List<Position> GetModelListByPropNameInCompany(string companyId, string propId)
		{
			string[] strArrays = new string[] { " deptId in (select _autoid from T_E_Org_Department where CompanyId = '", companyId, "') and PropId='", propId, "'" };
			return this.GetModelList(string.Concat(strArrays));
		}

		public List<Position> GetModelListByPropNameInDept(string deptId, string propId)
		{
			string[] strArrays = new string[] { " deptId ='", deptId, "' and PropId='", propId, "'" };
			return this.GetModelList(string.Concat(strArrays));
		}

		public List<Position> GetModelListByPropNameInOrg(string orgWbs, string propId)
		{
			string[] strArrays = new string[] { " deptId in (select _autoid from T_E_Org_Department where _isdel=0 and deptwbs like '", orgWbs, "%') and PropId='", propId, "'" };
			return this.GetModelList(string.Concat(strArrays));
		}

		public int GetPositionCountByDeptId(string deptId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(*)  From T_E_Org_Position ");
			stringBuilder.Append(string.Concat(" where deptId ='", deptId, "' and _IsDel=0 "));
			object obj = SysDatabase.ExecuteScalar(stringBuilder.ToString());
			if ((obj == null ? true : obj == DBNull.Value))
			{
				throw new Exception("Error in GetSubDeptCount");
			}
			return Convert.ToInt32(obj);
		}

		public int Update(Position model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Org_Position set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tPositionName=@PositionName,\r\n\t\t\t\t\tPositionCode=@PositionCode,\r\n\t\t\t\t\tParentPositionId=@ParentPositionId,\r\n\t\t\t\t\tParentPositionName=@ParentPositionName,\r\n\t\t\t\t\tDeptID=@DeptID,\r\n\t\t\t\t\tPropName=@PropName,\r\n\t\t\t\t\tPropId=@PropId,\r\n\t\t\t\t\tRankName=@RankName,\r\n\t\t\t\t\tRankCode=@RankCode,\r\n\t\t\t\t\tOrderID=@OrderID\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "PositionName", DbType.String, model.PositionName);
			SysDatabase.AddInParameter(sqlStringCommand, "PositionCode", DbType.String, model.PositionCode);
			SysDatabase.AddInParameter(sqlStringCommand, "ParentPositionId", DbType.String, model.ParentPositionId);
			SysDatabase.AddInParameter(sqlStringCommand, "ParentPositionName", DbType.String, model.ParentPositionName);
			SysDatabase.AddInParameter(sqlStringCommand, "PropName", DbType.String, model.PropName);
			SysDatabase.AddInParameter(sqlStringCommand, "PropId", DbType.String, model.PropId);
			SysDatabase.AddInParameter(sqlStringCommand, "RankName", DbType.String, model.RankName);
			SysDatabase.AddInParameter(sqlStringCommand, "RankCode", DbType.String, model.RankCode);
			SysDatabase.AddInParameter(sqlStringCommand, "DeptID", DbType.String, model.DeptID);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderID", DbType.Int32, model.OrderID);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}