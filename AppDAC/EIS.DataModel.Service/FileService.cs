using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace EIS.DataModel.Service
{
	public class FileService
	{
		public FileService()
		{
		}

		public static string GetFileAttr(string fileId, string attrName)
		{
			string str = string.Format("select {1} from T_E_File_File where _AutoId='{0}'", fileId, attrName);
			return SysDatabase.ExecuteScalar(str).ToString();
		}

		public List<AppFile> GetFiles(string AppName, string AppId)
		{
			_AppFile __AppFile = new _AppFile();
			string str = "";
			str = (AppName.Trim() != "" ? string.Format("_isDel=0 and AppName='{0}' and AppId='{1}'", AppName, AppId) : string.Format("_isDel=0 and AppId='{0}'", AppId));
			return __AppFile.GetModelList(str);
		}

		public string GetFilesCount(string AppName, string AppId)
		{
			_AppFile __AppFile = new _AppFile();
			return __AppFile.GetModelListCount(string.Format("AppName='{0}' and AppId='{1}'", AppName, AppId));
		}

		public static AppFile GetFirstFile(string AppId)
		{
			AppFile item;
			_AppFile __AppFile = new _AppFile();
			IList<AppFile> modelList = __AppFile.GetModelList(string.Format(" AppId='{0}' ", AppId));
			foreach (AppFile appFile in modelList)
			{
				appFile.FilePath = string.Concat(AppSettings.Instance.AppFileSavePath, appFile.FilePath);
			}
			if (modelList.Count <= 0)
			{
				item = null;
			}
			else
			{
				item = modelList[0];
			}
			return item;
		}

		public static AppFile GetLastFileByAppId(string AppId)
		{
			AppFile item;
			_AppFile __AppFile = new _AppFile();
			IList<AppFile> modelList = __AppFile.GetModelList(string.Format("_isDel=0 and  AppId='{0}'", AppId));
			if (modelList.Count <= 0)
			{
				item = null;
			}
			else
			{
				item = modelList[modelList.Count - 1];
			}
			return item;
		}

		public bool RemoveFiles(string AppName, string AppId)
		{
			bool flag = true;
			_AppFile __AppFile = new _AppFile();
			DataTable list = __AppFile.GetList(string.Format("AppName='{0}' and AppId='{1}'", AppName, AppId));
			foreach (DataRow row in list.Rows)
			{
				string str = row["FilePath"].ToString();
				string str1 = string.Concat(AppSettings.Instance.AppFileSavePath, str);
				File.Delete(str1);
			}
			return flag;
		}
	}
}