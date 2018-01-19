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
	public class _EmployeeRelation
	{
		private DbTransaction dbTransaction_0 = null;

		public _EmployeeRelation()
		{
		}

		public _EmployeeRelation(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(EmployeeRelation model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_App_Relation  (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tLeaderName,\r\n\t\t\t\t\tLeaderId,\r\n\t\t\t\t\tEmployeeName,\r\n\t\t\t\t\tEmployeeId,\r\n\t\t\t\t\tRelation,\r\n\t\t\t\t\tState\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@LeaderName,\r\n\t\t\t\t\t@LeaderId,\r\n\t\t\t\t\t@EmployeeName,\r\n\t\t\t\t\t@EmployeeId,\r\n\t\t\t\t\t@Relation,\r\n\t\t\t\t\t@State\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@LeaderName", DbType.String, model.LeaderName);
			SysDatabase.AddInParameter(sqlStringCommand, "@LeaderId", DbType.String, model.LeaderId);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeName", DbType.String, model.EmployeeName);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeId", DbType.String, model.EmployeeId);
			SysDatabase.AddInParameter(sqlStringCommand, "@Relation", DbType.String, model.Relation);
			SysDatabase.AddInParameter(sqlStringCommand, "@State", DbType.String, model.State);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_App_Relation  ");
			stringBuilder.Append(" where _AutoID=@_AutoID ;");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			DataTable dataTable;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_App_Relation  ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			dataTable = (this.dbTransaction_0 == null ? SysDatabase.ExecuteTable(stringBuilder.ToString()) : SysDatabase.ExecuteTable(stringBuilder.ToString(), this.dbTransaction_0));
			return dataTable;
		}

		public EmployeeRelation GetModel(string string_0)
		{
			EmployeeRelation model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_App_Relation  ");
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

		public EmployeeRelation GetModel(DataRow dataRow_0)
		{
			EmployeeRelation employeeRelation = new EmployeeRelation()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				employeeRelation._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				employeeRelation._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				employeeRelation._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			employeeRelation.LeaderName = dataRow_0["LeaderName"].ToString();
			employeeRelation.LeaderId = dataRow_0["LeaderId"].ToString();
			employeeRelation.EmployeeName = dataRow_0["EmployeeName"].ToString();
			employeeRelation.EmployeeId = dataRow_0["EmployeeId"].ToString();
			employeeRelation.Relation = dataRow_0["Relation"].ToString();
			employeeRelation.State = dataRow_0["State"].ToString();
			return employeeRelation;
		}

		public IList<EmployeeRelation> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<EmployeeRelation> employeeRelations = new List<EmployeeRelation>();
			stringBuilder.Append("select *  FROM T_E_App_Relation  ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				employeeRelations.Add(this.GetModel(row));
			}
			return employeeRelations;
		}

		public int Update(EmployeeRelation model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_App_Relation  set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\r\n\t\t\t\t\tLeaderName =@LeaderName,\r\n\t\t\t\t\tLeaderId=@LeaderId,\r\n\t\t\t\t\tEmployeeName=@EmployeeName,\r\n\t\t\t\t\tEmployeeId=@EmployeeId,\r\n\t\t\t\t\tRelation=@Relation,\r\n\t\t\t\t\tState=@State\r\n\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@LeaderName", DbType.String, model.LeaderName);
			SysDatabase.AddInParameter(sqlStringCommand, "@LeaderId", DbType.String, model.LeaderId);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeName", DbType.String, model.EmployeeName);
			SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeId", DbType.String, model.EmployeeId);
			SysDatabase.AddInParameter(sqlStringCommand, "@Relation", DbType.String, model.Relation);
			SysDatabase.AddInParameter(sqlStringCommand, "@State", DbType.String, model.State);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}