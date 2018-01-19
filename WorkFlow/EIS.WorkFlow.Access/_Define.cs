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
	public class _Define
	{
		private DbTransaction _tran = null;

		public _Define()
		{
		}

		public _Define(DbTransaction tran)
		{
			this._tran = tran;
		}

		public int Add(Define model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_WF_Define (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tCatalogCode,\r\n                    CompanyId,\r\n\t\t\t\t\tWorkflowCode,\r\n\t\t\t\t\tWorkflowName,\r\n\t\t\t\t\tVersion,\r\n\t\t\t\t\tDescription,\r\n\t\t\t\t\tEnabled,\r\n\t\t\t\t\tAppNames,\r\n\t\t\t\t\tXPDL,\r\n                    OrderId,\r\n                    BizType,\r\n                    PrintStyle,\r\n                    RememberUser,\r\n                    StartDate,\r\n                    EndDate\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@CatalogCode,\r\n                    @CompanyId,\r\n\t\t\t\t\t@WorkflowCode,\r\n\t\t\t\t\t@WorkflowName,\r\n\t\t\t\t\t@Version,\r\n\t\t\t\t\t@Description,\r\n\t\t\t\t\t@Enabled,\r\n\t\t\t\t\t@AppNames,\r\n\t\t\t\t\t@XPDL,\r\n                    @OrderId,\r\n                    @BizType,\r\n                    @PrintStyle,\r\n                    @RememberUser,\r\n                    @StartDate,\r\n                    @EndDate\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model.WorkflowId);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "CatalogCode", DbType.String, model.CatalogCode);
			SysDatabase.AddInParameter(sqlStringCommand, "CompanyId", DbType.String, model.CompanyId);
			SysDatabase.AddInParameter(sqlStringCommand, "WorkflowCode", DbType.String, model.WorkflowCode);
			SysDatabase.AddInParameter(sqlStringCommand, "WorkflowName", DbType.String, model.WorkflowName);
			SysDatabase.AddInParameter(sqlStringCommand, "Version", DbType.String, model.Version);
			SysDatabase.AddInParameter(sqlStringCommand, "Description", DbType.String, model.Description);
			SysDatabase.AddInParameter(sqlStringCommand, "Enabled", DbType.String, model.Enabled);
			SysDatabase.AddInParameter(sqlStringCommand, "AppNames", DbType.String, model.AppNames);
			SysDatabase.AddInParameter(sqlStringCommand, "XPDL", DbType.String, model.XPDL);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderId", DbType.Int32, model.OrderId);
			SysDatabase.AddInParameter(sqlStringCommand, "PrintStyle", DbType.String, model.PrintStyle);
			SysDatabase.AddInParameter(sqlStringCommand, "BizType", DbType.String, model.BizType);
			SysDatabase.AddInParameter(sqlStringCommand, "RememberUser", DbType.String, model.RememberUser);
			SysDatabase.AddInParameter(sqlStringCommand, "StartDate", DbType.DateTime, model.StartDate);
			SysDatabase.AddInParameter(sqlStringCommand, "EndDate", DbType.DateTime, model.EndDate);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public int Delete(string key)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_WF_Define ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, key);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_WF_Define ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by orderId");
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public Define GetModel(string key)
		{
			Define model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_WF_Define ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, key);
			Define define = new Define();
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

		public Define GetModel(DataRow dr)
		{
			Define define = new Define()
			{
				WorkflowId = dr["_AutoID"].ToString(),
				_UserName = dr["_UserName"].ToString(),
				_OrgCode = dr["_OrgCode"].ToString()
			};
			if (dr["_CreateTime"].ToString() != "")
			{
				define._CreateTime = DateTime.Parse(dr["_CreateTime"].ToString());
			}
			if (dr["_UpdateTime"].ToString() != "")
			{
				define._UpdateTime = DateTime.Parse(dr["_UpdateTime"].ToString());
			}
			if (dr["_IsDel"].ToString() != "")
			{
				define._IsDel = int.Parse(dr["_IsDel"].ToString());
			}
			define.CatalogCode = dr["CatalogCode"].ToString();
			define.CompanyId = dr["CompanyId"].ToString();
			define.WorkflowCode = dr["WorkflowCode"].ToString();
			define.WorkflowName = dr["WorkflowName"].ToString();
			define.Version = dr["Version"].ToString();
			define.Description = dr["Description"].ToString();
			define.Enabled = dr["Enabled"].ToString();
			define.AppNames = dr["AppNames"].ToString();
			define.XPDL = dr["XPDL"].ToString();
			define.BizType = dr["BizType"].ToString();
			define.RememberUser = dr["RememberUser"].ToString();
			define.PrintStyle = dr["PrintStyle"].ToString();
			if (dr["OrderId"].ToString() != "")
			{
				define.OrderId = int.Parse(dr["OrderId"].ToString());
			}
			if (dr["StartDate"].ToString() != "")
			{
				define.StartDate = new DateTime?(DateTime.Parse(dr["StartDate"].ToString()));
			}
			if (dr["EndDate"].ToString() != "")
			{
				define.EndDate = new DateTime?(DateTime.Parse(dr["EndDate"].ToString()));
			}
			return define;
		}

		public List<Define> GetModelList(string strWhere, bool withXpdl)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Define> defines = new List<Define>();
			if (!withXpdl)
			{
				stringBuilder.Append("select [_AutoID]\r\n                      ,[_UserName]\r\n                      ,[_OrgCode]\r\n                      ,[_CreateTime]\r\n                      ,[_UpdateTime]\r\n                      ,[_IsDel]\r\n                      ,[CatalogCode]\r\n                      ,[WorkflowCode]\r\n                      ,[WorkflowName]\r\n                      ,[Version]\r\n                      ,[Description]\r\n                      ,[Enabled]\r\n                      ,[AppNames]\r\n                      ,'' [XPDL]\r\n                      ,[OrderId]\r\n                      ,[PrintStyle]\r\n                      ,[BizType]\r\n                      ,StartDate\r\n                      ,EndDate\r\n                      ,[RememberUser]\r\n                      ,[CompanyId] from T_E_WF_Define ");
			}
			else
			{
				stringBuilder.Append("select [_AutoID]\r\n                      ,[_UserName]\r\n                      ,[_OrgCode]\r\n                      ,[_CreateTime]\r\n                      ,[_UpdateTime]\r\n                      ,[_IsDel]\r\n                      ,[CatalogCode]\r\n                      ,[WorkflowCode]\r\n                      ,[WorkflowName]\r\n                      ,[Version]\r\n                      ,[Description]\r\n                      ,[Enabled]\r\n                      ,[AppNames]\r\n                      ,[XPDL]\r\n                      ,[OrderId]\r\n                      ,[BizType]\r\n                      ,[PrintStyle]\r\n                      ,StartDate\r\n                      ,EndDate\r\n                      ,[RememberUser]\r\n                      ,[CompanyId] from T_E_WF_Define ");
			}
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by orderId");
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				defines.Add(this.GetModel(row));
			}
			return defines;
		}

		public List<Define> GetModelListByCatCode(string catCode)
		{
			List<Define> modelList = this.GetModelList(string.Concat(" CatalogCode ='", catCode, "' "), false);
			return modelList;
		}

		public int Update(Define model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_WF_Define set \r\n\t\t\t\t\t_UserName=@_UserName,\r\n\t\t\t\t\t_OrgCode=@_OrgCode,\r\n\t\t\t\t\t_CreateTime=@_CreateTime,\r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tCatalogCode=@CatalogCode,\r\n                    CompanyId = @CompanyId,\r\n\t\t\t\t\tWorkflowCode=@WorkflowCode,\r\n\t\t\t\t\tWorkflowName=@WorkflowName,\r\n\t\t\t\t\tVersion=@Version,\r\n\t\t\t\t\tDescription=@Description,\r\n\t\t\t\t\tEnabled=@Enabled,\r\n\t\t\t\t\tAppNames=@AppNames,\r\n\t\t\t\t\tXPDL=@XPDL,\r\n                    OrderId=@OrderId,\r\n                    BizType=@BizType,\r\n                    PrintStyle=@PrintStyle,\r\n                    RememberUser=@RememberUser,\r\n                    StartDate=@StartDate,\r\n                    EndDate=@EndDate\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model.WorkflowId);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "CatalogCode", DbType.String, model.CatalogCode);
			SysDatabase.AddInParameter(sqlStringCommand, "CompanyId", DbType.String, model.CompanyId);
			SysDatabase.AddInParameter(sqlStringCommand, "WorkflowCode", DbType.String, model.WorkflowCode);
			SysDatabase.AddInParameter(sqlStringCommand, "WorkflowName", DbType.String, model.WorkflowName);
			SysDatabase.AddInParameter(sqlStringCommand, "Version", DbType.String, model.Version);
			SysDatabase.AddInParameter(sqlStringCommand, "Description", DbType.String, model.Description);
			SysDatabase.AddInParameter(sqlStringCommand, "Enabled", DbType.String, model.Enabled);
			SysDatabase.AddInParameter(sqlStringCommand, "AppNames", DbType.String, model.AppNames);
			SysDatabase.AddInParameter(sqlStringCommand, "XPDL", DbType.String, model.XPDL);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderId", DbType.Int32, model.OrderId);
			SysDatabase.AddInParameter(sqlStringCommand, "BizType", DbType.String, model.BizType);
			SysDatabase.AddInParameter(sqlStringCommand, "PrintStyle", DbType.String, model.PrintStyle);
			SysDatabase.AddInParameter(sqlStringCommand, "RememberUser", DbType.String, model.RememberUser);
			SysDatabase.AddInParameter(sqlStringCommand, "StartDate", DbType.DateTime, model.StartDate);
			SysDatabase.AddInParameter(sqlStringCommand, "EndDate", DbType.DateTime, model.EndDate);
			num = (this._tran == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this._tran));
			return num;
		}
	}
}