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
	public class _AppTree
	{
		private DbTransaction dbTransaction_0 = null;

		public _AppTree()
		{
		}

		public _AppTree(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(AppTree model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Sys_Tree  (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tTreeName,\r\n\t\t\t\t\tConnectionId,\r\n\t\t\t\t\tConnection,\r\n\t\t\t\t\tCatCode,\r\n                    TreeSQL,\r\n                    TreeScript,\r\n                    RootPara,\r\n                    RootValue\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@TreeName,\r\n\t\t\t\t\t@ConnectionId,\r\n\t\t\t\t\t@Connection,\r\n\t\t\t\t\t@CatCode,\r\n                    @TreeSQL,\r\n                    @TreeScript,\r\n                    @RootPara,\r\n                    @RootValue\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "@_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@TreeName", DbType.String, model.TreeName);
			SysDatabase.AddInParameter(sqlStringCommand, "@ConnectionId", DbType.String, model.ConnectionId);
			SysDatabase.AddInParameter(sqlStringCommand, "@Connection", DbType.String, model.Connection);
			SysDatabase.AddInParameter(sqlStringCommand, "@CatCode", DbType.String, model.CatCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@TreeSQL", DbType.String, model.TreeSQL);
			SysDatabase.AddInParameter(sqlStringCommand, "@TreeScript", DbType.String, model.TreeScript);
			SysDatabase.AddInParameter(sqlStringCommand, "@RootPara", DbType.String, model.RootPara);
			SysDatabase.AddInParameter(sqlStringCommand, "@RootValue", DbType.String, model.RootValue);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Sys_Tree ");
			stringBuilder.Append(" where _AutoID=@_AutoID ;");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Sys_Tree ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public AppTree GetModel(string string_0)
		{
			AppTree model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_Tree ");
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

		public AppTree GetModel(DataRow dataRow_0)
		{
			AppTree appTree = new AppTree()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				appTree._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				appTree._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				appTree._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			appTree.TreeName = dataRow_0["TreeName"].ToString();
			appTree.ConnectionId = dataRow_0["ConnectionId"].ToString();
			appTree.Connection = dataRow_0["Connection"].ToString();
			appTree.TreeSQL = dataRow_0["TreeSQL"].ToString();
			appTree.TreeScript = dataRow_0["TreeScript"].ToString();
			appTree.CatCode = dataRow_0["CatCode"].ToString();
			appTree.RootPara = dataRow_0["RootPara"].ToString();
			appTree.RootValue = dataRow_0["RootValue"].ToString();
			return appTree;
		}

		public IList<AppTree> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<AppTree> appTrees = new List<AppTree>();
			stringBuilder.Append("select *  FROM T_E_Sys_Tree ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				appTrees.Add(this.GetModel(row));
			}
			return appTrees;
		}

		public int Update(AppTree model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_Tree  set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\r\n\t\t\t\t\tTreeName =@TreeName,\r\n\t\t\t\t\tConnectionId=@ConnectionId,\r\n\t\t\t\t\tConnection=@Connection,\r\n\t\t\t\t\tCatCode=@CatCode,\r\n\t\t\t\t\tTreeSQL=@TreeSQL,\r\n\t\t\t\t\tTreeScript=@TreeScript,\r\n\t\t\t\t\tRootPara=@RootPara,\r\n\t\t\t\t\tRootValue=@RootValue\r\n\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "@_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "@_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "@_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "@TreeName", DbType.String, model.TreeName);
			SysDatabase.AddInParameter(sqlStringCommand, "@ConnectionId", DbType.String, model.ConnectionId);
			SysDatabase.AddInParameter(sqlStringCommand, "@Connection", DbType.String, model.Connection);
			SysDatabase.AddInParameter(sqlStringCommand, "@CatCode", DbType.String, model.CatCode);
			SysDatabase.AddInParameter(sqlStringCommand, "@TreeSQL", DbType.String, model.TreeSQL);
			SysDatabase.AddInParameter(sqlStringCommand, "@TreeScript", DbType.String, model.TreeScript);
			SysDatabase.AddInParameter(sqlStringCommand, "@RootPara", DbType.String, model.RootPara);
			SysDatabase.AddInParameter(sqlStringCommand, "@RootValue", DbType.String, model.RootValue);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}