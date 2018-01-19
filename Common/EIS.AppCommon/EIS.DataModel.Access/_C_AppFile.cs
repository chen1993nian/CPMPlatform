using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
//从 EIS.ApppDAC 中迁移过来，解决 Comm和DAC 相互引用问题

namespace EIS.AppCommo
{
	public class _C_AppFile
	{
		private DbTransaction dbTransaction_0 = null;

		public _C_AppFile()
		{
		}

        public _C_AppFile(DbTransaction tran)
		{
			this.dbTransaction_0 = tran;
		}

		public int Add(AppFile model)
		{
			int num;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Insert T_E_File_File (\r\n \t\t\t\t\t_AutoID,\r\n\t\t\t\t\t_UserName,\r\n\t\t\t\t\t_OrgCode,\r\n\t\t\t\t\t_CreateTime,\r\n\t\t\t\t\t_UpdateTime,\r\n\t\t\t\t\t_IsDel,\r\n\t\t\t\t\tFileName,\r\n\t\t\t\t\tFactFileName,\r\n\t\t\t\t\tFolderID,\r\n\t\t\t\t\tFileType,\r\n\t\t\t\t\tFileSize,\r\n\t\t\t\t\tFilePath,\r\n                    BasePath,\r\n                    DownCount,\r\n\t\t\t\t\tAppId,\r\n\t\t\t\t\tAppName,\r\n                    OrderId,\r\n                    FileVer,\r\n                    ValidVer,\r\n                    FileCode,\r\n                    Inherit\r\n\t\t\t) values(\r\n\t\t\t\t\t@_AutoID,\r\n\t\t\t\t\t@_UserName,\r\n\t\t\t\t\t@_OrgCode,\r\n\t\t\t\t\t@_CreateTime,\r\n\t\t\t\t\t@_UpdateTime,\r\n\t\t\t\t\t@_IsDel,\r\n\t\t\t\t\t@FileName,\r\n\t\t\t\t\t@FactFileName,\r\n\t\t\t\t\t@FolderID,\r\n\t\t\t\t\t@FileType,\r\n\t\t\t\t\t@FileSize,\r\n\t\t\t\t\t@FilePath,\r\n                    @BasePath,\r\n                    @DownCount,\r\n\t\t\t\t\t@AppId,\r\n\t\t\t\t\t@AppName,\r\n                    @OrderId,\r\n                    @FileVer,\r\n                    @ValidVer,\r\n                    @FileCode,\r\n                    @Inherit\r\n\t\t\t)");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "FileName", DbType.String, model.FileName);
			SysDatabase.AddInParameter(sqlStringCommand, "FactFileName", DbType.String, model.FactFileName);
			SysDatabase.AddInParameter(sqlStringCommand, "FolderID", DbType.String, model.FolderID);
			SysDatabase.AddInParameter(sqlStringCommand, "FileType", DbType.String, model.FileType);
			SysDatabase.AddInParameter(sqlStringCommand, "FileSize", DbType.Int32, model.FileSize);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderId", DbType.Int32, model.OrderId);
			SysDatabase.AddInParameter(sqlStringCommand, "FilePath", DbType.String, model.FilePath);
			SysDatabase.AddInParameter(sqlStringCommand, "BasePath", DbType.String, model.BasePath);
			SysDatabase.AddInParameter(sqlStringCommand, "DownCount", DbType.Int32, model.DownCount);
			SysDatabase.AddInParameter(sqlStringCommand, "AppId", DbType.String, model.AppId);
			SysDatabase.AddInParameter(sqlStringCommand, "AppName", DbType.String, model.AppName);
			SysDatabase.AddInParameter(sqlStringCommand, "FileVer", DbType.Decimal, model.FileVer);
			SysDatabase.AddInParameter(sqlStringCommand, "ValidVer", DbType.Int32, model.ValidVer);
			SysDatabase.AddInParameter(sqlStringCommand, "FileCode", DbType.String, model.FileCode);
			SysDatabase.AddInParameter(sqlStringCommand, "Inherit", DbType.String, model.Inherit);
			num = (this.dbTransaction_0 == null ? SysDatabase.ExecuteNonQuery(sqlStringCommand) : SysDatabase.ExecuteNonQuery(sqlStringCommand, this.dbTransaction_0));
			return num;
		}

		public void Delete(string string_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Update T_E_File_File set _isdel=1 ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public DataTable GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select *  FROM T_E_File_File ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by _CreateTime");
			return SysDatabase.ExecuteTable(stringBuilder.ToString());
		}

		public AppFile GetModel(string string_0)
		{
			AppFile model;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 * from T_E_File_File ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			AppFile appFile = new AppFile();
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

		public AppFile GetModel(DataRow dataRow_0)
		{
			AppFile appFile = new AppFile()
			{
				_AutoID = dataRow_0["_AutoID"].ToString(),
				_UserName = dataRow_0["_UserName"].ToString(),
				_OrgCode = dataRow_0["_OrgCode"].ToString()
			};
			if (dataRow_0["_CreateTime"].ToString() != "")
			{
				appFile._CreateTime = DateTime.Parse(dataRow_0["_CreateTime"].ToString());
			}
			if (dataRow_0["_UpdateTime"].ToString() != "")
			{
				appFile._UpdateTime = DateTime.Parse(dataRow_0["_UpdateTime"].ToString());
			}
			if (dataRow_0["_IsDel"].ToString() != "")
			{
				appFile._IsDel = int.Parse(dataRow_0["_IsDel"].ToString());
			}
			appFile.FileName = dataRow_0["FileName"].ToString();
			appFile.FactFileName = dataRow_0["FactFileName"].ToString();
			appFile.FolderID = dataRow_0["FolderID"].ToString();
			appFile.FileType = dataRow_0["FileType"].ToString();
			if (dataRow_0["FileSize"].ToString() != "")
			{
				appFile.FileSize = int.Parse(dataRow_0["FileSize"].ToString());
			}
			appFile.FilePath = dataRow_0["FilePath"].ToString();
			if (dataRow_0["DownCount"].ToString() != "")
			{
				appFile.DownCount = int.Parse(dataRow_0["DownCount"].ToString());
			}
			appFile.BasePath = dataRow_0["BasePath"].ToString();
			appFile.AppId = dataRow_0["AppId"].ToString();
			appFile.AppName = dataRow_0["AppName"].ToString();
			if (dataRow_0["OrderId"].ToString() != "")
			{
				appFile.OrderId = int.Parse(dataRow_0["OrderId"].ToString());
			}
			appFile.FileCode = dataRow_0["FileCode"].ToString();
			appFile.Inherit = dataRow_0["Inherit"].ToString();
			if (dataRow_0["ValidVer"].ToString() != "")
			{
				appFile.ValidVer = int.Parse(dataRow_0["ValidVer"].ToString());
			}
			if (dataRow_0["FileVer"].ToString() != "")
			{
				appFile.FileVer = decimal.Parse(dataRow_0["FileVer"].ToString());
			}
			return appFile;
		}

		public List<AppFile> GetModelList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<AppFile> appFiles = new List<AppFile>();
			stringBuilder.Append("select *  FROM T_E_File_File ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			stringBuilder.Append(" order by  _CreateTime");
			foreach (DataRow row in SysDatabase.ExecuteTable(stringBuilder.ToString()).Rows)
			{
				appFiles.Add(this.GetModel(row));
			}
			return appFiles;
		}

		public string GetModelListCount(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(*)  FROM T_E_File_File ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(string.Concat(" where ", strWhere));
			}
			return SysDatabase.ExecuteScalar(stringBuilder.ToString()).ToString();
		}

		public void Remove(string string_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete T_E_File_File ");
			stringBuilder.Append(" where _AutoID=@_AutoID ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, string_0);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}

		public void Update(AppFile model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update T_E_File_File set \r\n\t\t\t\t\t_UpdateTime=@_UpdateTime,\r\n\t\t\t\t\t_IsDel=@_IsDel,\r\n\t\t\t\t\tFileName=@FileName,\r\n\t\t\t\t\tFactFileName=@FactFileName,\r\n\t\t\t\t\tFolderID=@FolderID,\r\n\t\t\t\t\tFileType=@FileType,\r\n\t\t\t\t\tFileSize=@FileSize,\r\n\t\t\t\t\tFilePath=@FilePath,\r\n\t\t\t\t\tAppId=@AppId,\r\n\t\t\t\t\tAppName=@AppName,\r\n                    OrderId=@OrderId,\r\n                    FileVer=@FileVer,\r\n                    ValidVer=@ValidVer,\r\n                    FileCode=@FileCode,\r\n                    Inherit=@Inherit\r\n\t\t\t\t\twhere _AutoID=@_AutoID\r\n\t\t\t ");
			DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(stringBuilder.ToString());
			SysDatabase.AddInParameter(sqlStringCommand, "_AutoID", DbType.String, model._AutoID);
			SysDatabase.AddInParameter(sqlStringCommand, "_UserName", DbType.String, model._UserName);
			SysDatabase.AddInParameter(sqlStringCommand, "_OrgCode", DbType.String, model._OrgCode);
			SysDatabase.AddInParameter(sqlStringCommand, "_CreateTime", DbType.DateTime, model._CreateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_UpdateTime", DbType.DateTime, model._UpdateTime);
			SysDatabase.AddInParameter(sqlStringCommand, "_IsDel", DbType.Int32, model._IsDel);
			SysDatabase.AddInParameter(sqlStringCommand, "FileName", DbType.String, model.FileName);
			SysDatabase.AddInParameter(sqlStringCommand, "FactFileName", DbType.String, model.FactFileName);
			SysDatabase.AddInParameter(sqlStringCommand, "FolderID", DbType.String, model.FolderID);
			SysDatabase.AddInParameter(sqlStringCommand, "FileType", DbType.String, model.FileType);
			SysDatabase.AddInParameter(sqlStringCommand, "OrderId", DbType.Int32, model.OrderId);
			SysDatabase.AddInParameter(sqlStringCommand, "FileSize", DbType.Int32, model.FileSize);
			SysDatabase.AddInParameter(sqlStringCommand, "FilePath", DbType.String, model.FilePath);
			SysDatabase.AddInParameter(sqlStringCommand, "AppId", DbType.String, model.AppId);
			SysDatabase.AddInParameter(sqlStringCommand, "AppName", DbType.String, model.AppName);
			SysDatabase.AddInParameter(sqlStringCommand, "FileVer", DbType.Decimal, model.FileVer);
			SysDatabase.AddInParameter(sqlStringCommand, "ValidVer", DbType.Int32, model.ValidVer);
			SysDatabase.AddInParameter(sqlStringCommand, "FileCode", DbType.String, model.FileCode);
			SysDatabase.AddInParameter(sqlStringCommand, "Inherit", DbType.String, model.Inherit);
			SysDatabase.ExecuteNonQuery(sqlStringCommand);
		}
	}
}