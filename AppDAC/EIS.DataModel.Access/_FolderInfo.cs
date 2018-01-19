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
	public class _FolderInfo
	{
		private DbTransaction dbTransaction_0 = null;

		public _FolderInfo(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public _FolderInfo()
		{
		}

		public void Add(FolderInfo model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_File_Folder (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tFolderPID,\r\n\t\t\t\t\tFolderName,\r\n\t\t\t\t\tShareUser,\r\n\t\t\t\t\tShareUserId,\r\n                    ShareLimit,\r\n\t\t\t\t\tOrderID,\r\n                    Inherit,\r\n                    FolderWBS\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@FolderPID,\r\n\t\t\t\t\t@FolderName,\r\n\t\t\t\t\t@ShareUser,\r\n\t\t\t\t\t@ShareUserId,\r\n                    @ShareLimit,\r\n\t\t\t\t\t@OrderID,\r\n                    @Inherit,\r\n                    @FolderWBS\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "FolderPID", DbType.String, model.FolderPID);
			SysDatabase.AddInParameter(sqlStringCommand, "FolderName", DbType.String, model.FolderName);
			SysDatabase.AddInParameter(sqlStringCommand, "ShareUser", DbType.String, model.ShareUser);
			SysDatabase.AddInParameter(sqlStringCommand, "ShareUserId", DbType.String, model.ShareUserId);
			SysDatabase.AddInParameter(sqlStringCommand, "ShareLimit", DbType.String, model.ShareLimit);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderID", DbType.Int32, model.OrderID);
			SysDatabase.AddInParameter(sqlStringCommand, "Inherit", DbType.Int32, int.Parse(model.Inherit));
			SysDatabase.AddInParameter(sqlStringCommand, "FolderWBS", DbType.String, model.FolderWBS);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public void Delete(string string_0)
		{
			if (this.GetFileCount(string_0) > 0)
			{
				throw new Exception("下面存在文件，不能删除");
			}
			if (this.GetSubFolderCount(string_0) > 0)
			{
				throw new Exception("下面存在文件夹，不能删除");
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_File_Folder set _isdel=1 where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public int GetFileCount(string string_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(*) from T_E_File_File ");
			stringBuilder.Append(" where _isdel=0 and FolderID=@FolderID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "FolderID", DbType.String, string_0);
			return Convert.ToInt32(SysDatabase.ExecuteScalar(sqlStringCommand));
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  From T_E_File_Folder ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public FolderInfo GetModel(string string_0)
		{
			FolderInfo model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_File_Folder ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			FieldInfo fieldInfo = new FieldInfo();
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

		public FolderInfo GetModel(DataRow dataRow_0)
		{
			FolderInfo folderInfo = new FolderInfo()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				folderInfo._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				folderInfo._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				folderInfo._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			folderInfo.FolderPID = dataRow_0["FolderPID"].ToString();
			folderInfo.FolderName = dataRow_0["FolderName"].ToString();
			folderInfo.ShareUser = dataRow_0["ShareUser"].ToString();
			folderInfo.ShareUserId = dataRow_0["ShareUserId"].ToString();
			folderInfo.ShareLimit = dataRow_0["ShareLimit"].ToString();
			if (dataRow_0["OrderID"].ToString() != "")
			{
				folderInfo.OrderID = int.Parse(dataRow_0["OrderID"].ToString());
			}
			folderInfo.Inherit = dataRow_0["Inherit"].ToString();
			folderInfo.FolderWBS = dataRow_0["FolderWBS"].ToString();
			return folderInfo;
		}

		public List<FolderInfo> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<FolderInfo> folderInfos = new List<FolderInfo>();
			stringBuilder.Append("select *  FROM T_E_File_Folder ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by OrderID");
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				folderInfos.Add(this.GetModel(row));
			}
			return folderInfos;
		}

		public int GetNewOrd(string pFolderId)
		{
			int num;
			string str = string.Concat("select max(OrderID)+1 from T_E_File_Folder where FolderPID='", pFolderId, "'");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(str.ToString());
			object obj = SysDatabase.ExecuteScalar(sqlStringCommand);
			num = (obj != DBNull.Value ? int.Parse(obj.ToString()) : 1);
			return num;
		}

		public int GetSubFolderCount(string string_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(*) from T_E_File_Folder ");
			stringBuilder.Append(" where _isdel=0 and FolderPID=@FolderPID");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "FolderPID", DbType.String, string_0);
			return Convert.ToInt32(SysDatabase.ExecuteScalar(sqlStringCommand));
		}

		public void Remove(string string_0)
		{
			if (this.GetFileCount(string_0) > 0)
			{
				throw new Exception("下面存在文件，不能删除");
			}
			if (this.GetSubFolderCount(string_0) > 0)
			{
				throw new Exception("下面存在文件夹，不能删除");
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from T_E_File_Folder ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public void Update(FolderInfo model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_File_Folder set \r\n\t\t\t\t\t_UserName=@_UserName,\r\n\t\t\t\t\t_OrgCode=@_OrgCode,\r\n\t\t\t\t\t_CreateTime=@_CreateTime,\r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tFolderPID=@FolderPID,\r\n\t\t\t\t\tFolderName=@FolderName,\r\n\t\t\t\t\tShareUser=@ShareUser,\r\n\t\t\t\t\tShareUserId=@ShareUserId,\r\n\t\t\t\t\tShareLimit=@ShareLimit,\r\n\t\t\t\t\tOrderID=@OrderID,\r\n\t\t\t\t\tInherit=@Inherit,\r\n\t\t\t\t\tFolderWBS=@FolderWBS\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "FolderPID", DbType.String, model.FolderPID);
			SysDatabase.AddInParameter(sqlStringCommand, "FolderName", DbType.String, model.FolderName);
			SysDatabase.AddInParameter(sqlStringCommand, "ShareUser", DbType.String, model.ShareUser);
			SysDatabase.AddInParameter(sqlStringCommand, "ShareLimit", DbType.String, model.ShareLimit);
			SysDatabase.AddInParameter(sqlStringCommand, "ShareUserId", DbType.String, model.ShareUserId);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderID", DbType.Int32, model.OrderID);
			SysDatabase.AddInParameter(sqlStringCommand, "Inherit", DbType.Int32, int.Parse(model.Inherit));
			SysDatabase.AddInParameter(sqlStringCommand, "FolderWBS", DbType.String, model.FolderWBS);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}
	}
}