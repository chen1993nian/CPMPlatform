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
	public class _FunNode
	{
		private DbTransaction dbTransaction_0 = null;

		public _FunNode()
		{
		}

		public _FunNode(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(FunNode model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_Sys_FunNode (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tFunName,\r\n\t\t\t\t\tLinkFile,\r\n\t\t\t\t\tLinkType,\r\n\t\t\t\t\tDispState,\r\n\t\t\t\t\tDispStyle,\r\n\t\t\t\t\tEncrypt,\r\n                    IsExpand,\r\n\t\t\t\t\tFunPWBS,\r\n\t\t\t\t\tFunWBS,\r\n\t\t\t\t\tOrderId,\r\n\t\t\t\t\tWebID\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@FunName,\r\n\t\t\t\t\t@LinkFile,\r\n\t\t\t\t\t@LinkType,\r\n\t\t\t\t\t@DispState,\r\n\t\t\t\t\t@DispStyle,\r\n\t\t\t\t\t@Encrypt,\r\n                    @IsExpand,\r\n\t\t\t\t\t@FunPWBS,\r\n\t\t\t\t\t@FunWBS,\r\n\t\t\t\t\t@OrderId,\r\n\t\t\t\t\t@WebID\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "FunName", DbType.String, model.FunName);
			SysDatabase.AddInParameter(sqlStringCommand, "LinkFile", DbType.String, model.LinkFile);
			SysDatabase.AddInParameter(sqlStringCommand, "LinkType", DbType.Int32, model.LinkType);
			SysDatabase.AddInParameter(sqlStringCommand, "DispState", DbType.String, model.DispState);
			SysDatabase.AddInParameter(sqlStringCommand, "DispStyle", DbType.String, model.DispStyle);
			SysDatabase.AddInParameter(sqlStringCommand, "Encrypt", DbType.String, model.Encrypt);
			SysDatabase.AddInParameter(sqlStringCommand, "IsExpand", DbType.String, model.IsExpand);
			SysDatabase.AddInParameter(sqlStringCommand, "FunPWBS", DbType.String, model.FunPWBS);
			SysDatabase.AddInParameter(sqlStringCommand, "FunWBS", DbType.String, model.FunWBS);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderId", DbType.Int32, model.OrderId);
			SysDatabase.AddInParameter(sqlStringCommand, "WebID", DbType.String, model.WebID);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public int Delete(string string_0)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_Sys_FunNode ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_Sys_FunNode ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public List<FunNode> GetListByWbs(string PWBS, string WebId)
		{
			string[] pWBS = new string[] { "FunWbs like '", PWBS, "%' and WebId='", WebId, "'" };
			return this.GetModelList(string.Concat(pWBS));
		}

		public int GetMaxOrder(string pnodewbs, string WebId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select max(OrderId) ");
			stringBuilder.Append(" FROM T_E_Sys_FunNode ");
			string[] strArrays = new string[] { " where FunPWbs='", pnodewbs, "' and WebId='", WebId, "'" };
			stringBuilder.Append(string.Concat(strArrays));
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
			return (obj == DBNull.Value ? 0 : Convert.ToInt32(obj));
		}

		public FunNode GetModel(string string_0)
		{
			FunNode model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_Sys_FunNode ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			FunNode funNode = new FunNode();
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

		public FunNode GetModel(DataRow dataRow_0)
		{
			FunNode funNode = new FunNode()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				funNode._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				funNode._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				funNode._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			funNode.FunName = dataRow_0["FunName"].ToString();
			funNode.LinkFile = dataRow_0["LinkFile"].ToString();
			if (dataRow_0["LinkType"].ToString() != "")
			{
				funNode.LinkType = int.Parse(dataRow_0["LinkType"].ToString());
			}
			funNode.DispState = dataRow_0["DispState"].ToString();
			funNode.DispStyle = dataRow_0["DispStyle"].ToString();
			funNode.FunPWBS = dataRow_0["FunPWBS"].ToString();
			funNode.FunWBS = dataRow_0["FunWBS"].ToString();
			if (dataRow_0["OrderId"].ToString() != "")
			{
				funNode.OrderId = int.Parse(dataRow_0["OrderId"].ToString());
			}
			funNode.WebID = dataRow_0["WebID"].ToString();
			return funNode;
		}

		public List<FunNode> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<FunNode> funNodes = new List<FunNode>();
			stringBuilder.Append("select *  FROM T_E_Sys_FunNode ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by OrderID ");
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				funNodes.Add(this.GetModel(row));
			}
			return funNodes;
		}

		public string GetNewCode(string pnodewbs, string WebId)
		{
			string str;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select max(right(FunWbs,4))+1 ");
			stringBuilder.Append(" FROM T_E_Sys_FunNode ");
			string[] strArrays = new string[] { " where FunPWbs='", pnodewbs, "' and WebId='", WebId, "'" };
			stringBuilder.Append(string.Concat(strArrays));
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
			string str1 = "";
			str1 = (obj == DBNull.Value ? "0001" : obj.ToString());
			if (int.Parse(str1) <= 9999)
			{
				int num = Convert.ToInt32(str1);
				str = string.Concat(pnodewbs, num.ToString("d4"));
			}
			else
			{
				str = "";
			}
			return str;
		}

		public List<FunNode> GetTopList(string webId)
		{
			return this.GetModelList(string.Concat("dispstate='是' and FunPWBS='0' and webid='", webId, "'"));
		}

		public int Update(FunNode model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_Sys_FunNode set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tFunName=@FunName,\r\n\t\t\t\t\tLinkFile=@LinkFile,\r\n\t\t\t\t\tLinkType=@LinkType,\r\n\t\t\t\t\tDispState=@DispState,\r\n\t\t\t\t\tDispStyle=@DispStyle,\r\n\t\t\t\t\tFunPWBS=@FunPWBS,\r\n\t\t\t\t\tFunWBS=@FunWBS,\r\n\t\t\t\t\tOrderId=@OrderId,\r\n\t\t\t\t\tWebID=@WebID\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "FunName", DbType.String, model.FunName);
			SysDatabase.AddInParameter(sqlStringCommand, "LinkFile", DbType.String, model.LinkFile);
			SysDatabase.AddInParameter(sqlStringCommand, "LinkType", DbType.Int32, model.LinkType);
			SysDatabase.AddInParameter(sqlStringCommand, "DispState", DbType.String, model.DispState);
			SysDatabase.AddInParameter(sqlStringCommand, "DispStyle", DbType.String, model.DispStyle);
			SysDatabase.AddInParameter(sqlStringCommand, "FunPWBS", DbType.String, model.FunPWBS);
			SysDatabase.AddInParameter(sqlStringCommand, "FunWBS", DbType.String, model.FunWBS);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderId", DbType.Int32, model.OrderId);
			SysDatabase.AddInParameter(sqlStringCommand, "WebID", DbType.String, model.WebID);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}
	}
}