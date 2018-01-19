using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;

namespace EIS.DataModel.Service
{
	public class FolderService
	{
		public FolderService()
		{
		}

		public static void CopyFolder(string folderId, string newParentId)
		{
			_FolderInfo __FolderInfo = new _FolderInfo();
			FolderInfo model = __FolderInfo.GetModel(folderId);
			model._AutoID = Guid.NewGuid().ToString();
			model._CreateTime = DateTime.Now;
			model._UpdateTime = DateTime.Now;
			model.FolderPID = newParentId;
			model.ShareUser = "";
			model.ShareUserId = "";
			model.OrderID = __FolderInfo.GetNewOrd(newParentId);
			_AppFile __AppFile = new _AppFile();
			foreach (AppFile filesByFolderId in FolderService.GetFilesByFolderId(folderId))
			{
				filesByFolderId._AutoID = Guid.NewGuid().ToString();
				filesByFolderId._CreateTime = DateTime.Now;
				filesByFolderId._UpdateTime = DateTime.Now;
				filesByFolderId.FolderID = model._AutoID;
				__AppFile.Add(filesByFolderId);
			}
			foreach (FolderInfo sonFolder in FolderService.GetSonFolder(folderId))
			{
				FolderService.CopyFolder(sonFolder._AutoID, model._AutoID);
			}
			__FolderInfo.Add(model);
		}

		public static List<FolderInfo> GetAllFolder()
		{
			return (new _FolderInfo()).GetModelList("");
		}

		public static StringCollection GetDeptList(string empId)
		{
			StringCollection stringCollections = new StringCollection();
			DataTable dataTable = SysDatabase.ExecuteTable(string.Concat("select * from T_E_Org_DeptEmployee where EmployeeId='", empId, "' and _isDel=0"));
			foreach (DataRow row in dataTable.Rows)
			{
				stringCollections.Add(row["DeptId"].ToString());
			}
			return stringCollections;
		}

		public static int GetFileLimit(string fileId, string employeeId)
		{
			int num;
			int folderLimit = -1;
			DataTable dataTable = SysDatabase.ExecuteTable(string.Format("select FolderID,Inherit,_UserName from T_E_File_File where _AutoId='{0}'", fileId));
			string str = dataTable.Rows[0]["FolderID"].ToString();
			string str1 = dataTable.Rows[0]["Inherit"].ToString();
			if (str1 == "")
			{
				str1 = "1";
			}
			if (dataTable.Rows[0]["_UserName"].ToString() == employeeId)
			{
				num = 9;
			}
			else if (!FolderService.IsDocAdmin(employeeId))
			{
				DataTable dataTable1 = SysDatabase.ExecuteTable(string.Format("select Limit,objId,objType from T_E_File_FileSafe where FileId='{0}'", fileId));
				DataRow[] dataRowArray = dataTable1.Select(string.Concat("objType='1' and objId='", employeeId, "'"));
				if ((int)dataRowArray.Length <= 0)
				{
					StringCollection deptList = FolderService.GetDeptList(employeeId);
					int num1 = -1;
					foreach (string str2 in deptList)
					{
						dataRowArray = dataTable1.Select(string.Concat("objType='2' and objId='", str2, "'"));
						if ((int)dataRowArray.Length <= 0 || int.Parse(dataRowArray[0]["Limit"].ToString()) <= num1)
						{
							continue;
						}
						num1 = int.Parse(dataRowArray[0]["Limit"].ToString());
					}
					if (num1 <= -1)
					{
						StringCollection roleList = FolderService.GetRoleList(employeeId);
						int num2 = -1;
						foreach (string str3 in roleList)
						{
							dataRowArray = dataTable1.Select(string.Concat("objType='3' and objId='", str3, "'"));
							if ((int)dataRowArray.Length <= 0 || int.Parse(dataRowArray[0]["Limit"].ToString()) <= num2)
							{
								continue;
							}
							num2 = int.Parse(dataRowArray[0]["Limit"].ToString());
						}
						if (num2 <= -1)
						{
							dataRowArray = dataTable1.Select("objType='0' and objId='everyone'");
							if ((int)dataRowArray.Length <= 0)
							{
								if (str != "0" && str1 == "1")
								{
									folderLimit = FolderService.GetFolderLimit(str, employeeId);
								}
								num = folderLimit;
							}
							else
							{
								folderLimit = int.Parse(dataRowArray[0]["Limit"].ToString());
								num = folderLimit;
							}
						}
						else
						{
							folderLimit = num2;
							num = folderLimit;
						}
					}
					else
					{
						folderLimit = num1;
						num = folderLimit;
					}
				}
				else
				{
					folderLimit = int.Parse(dataRowArray[0]["Limit"].ToString());
					num = folderLimit;
				}
			}
			else
			{
				num = 9;
			}
			return num;
		}

		public static List<AppFile> GetFilesByFolderId(string folderId)
		{
			_AppFile __AppFile = new _AppFile();
			return __AppFile.GetModelList(string.Concat("FolderId='", folderId, "'"));
		}

		public static string GetFolderAttr(string folderId, string attrName)
		{
			string str = string.Format("select {1} from T_E_File_Folder where _AutoId='{0}'", folderId, attrName);
			return SysDatabase.ExecuteScalar(str).ToString();
		}

		public static int GetFolderLimit(string folderId, string employeeId)
		{
			int num;
			int folderLimit = -1;

			DataTable dataTable = SysDatabase.ExecuteTable(string.Format("select FolderPID,Inherit,_UserName from T_E_File_Folder where _AutoId='{0}'", folderId));
			string str = "";
			string str1 ="";
            string _UserName="";
            if (dataTable != null && dataTable.Rows.Count>0)
            {
                str = dataTable.Rows[0]["FolderPID"].ToString();
                str1 = dataTable.Rows[0]["Inherit"].ToString();
                _UserName=dataTable.Rows[0]["_UserName"].ToString();

            } // end if 

			if (str1 == "")
			{
				str1 = "1";
			}
            if (_UserName == employeeId)
			{
				num = 9;
			}
			else if (!FolderService.IsDocAdmin(employeeId))
			{
				DataTable dataTable1 = SysDatabase.ExecuteTable(string.Format("select Limit,objId,objType from T_E_File_FolderSafe where FolderId='{0}'", folderId));
				DataRow[] dataRowArray = dataTable1.Select(string.Concat("objType='1' and objId='", employeeId, "'"));
				if ((int)dataRowArray.Length <= 0)
				{
					StringCollection deptList = FolderService.GetDeptList(employeeId);
					int num1 = -1;
					foreach (string str2 in deptList)
					{
						dataRowArray = dataTable1.Select(string.Concat("objType='2' and objId='", str2, "'"));
						if ((int)dataRowArray.Length <= 0 || int.Parse(dataRowArray[0]["Limit"].ToString()) <= num1)
						{
							continue;
						}
						num1 = int.Parse(dataRowArray[0]["Limit"].ToString());
					}
					if (num1 <= -1)
					{
						StringCollection roleList = FolderService.GetRoleList(employeeId);
						int num2 = -1;
						foreach (string str3 in roleList)
						{
							dataRowArray = dataTable1.Select(string.Concat("objType='3' and objId='", str3, "'"));
							if ((int)dataRowArray.Length <= 0 || int.Parse(dataRowArray[0]["Limit"].ToString()) <= num2)
							{
								continue;
							}
							num2 = int.Parse(dataRowArray[0]["Limit"].ToString());
						}
						if (num2 <= -1)
						{
							dataRowArray = dataTable1.Select("objType='0' and objId='everyone'");
							if ((int)dataRowArray.Length <= 0)
							{
								if (str != "0" && str1 == "1")
								{
									folderLimit = FolderService.GetFolderLimit(str, employeeId);
								}
								num = folderLimit;
							}
							else
							{
								folderLimit = int.Parse(dataRowArray[0]["Limit"].ToString());
								num = folderLimit;
							}
						}
						else
						{
							folderLimit = num2;
							num = folderLimit;
						}
					}
					else
					{
						folderLimit = num1;
						num = folderLimit;
					}
				}
				else
				{
					folderLimit = int.Parse(dataRowArray[0]["Limit"].ToString());
					num = folderLimit;
				}
			}
			else
			{
				num = 9;
			}
			return num;
		}

		public static FolderInfo GetFolderModel(string folderId)
		{
			return (new _FolderInfo()).GetModel(folderId);
		}

		public static string GetFullPath(string folderId, string rootId)
		{
			string str;
			StringBuilder stringBuilder = new StringBuilder();
			FolderInfo folderModel = FolderService.GetFolderModel(folderId);
			if (folderModel != null)
			{
				StringCollection stringCollections = new StringCollection();
				stringCollections.Add(folderModel.FolderName);
				while (folderModel.FolderPID != "0")
				{
					folderModel = FolderService.GetFolderModel(folderModel.FolderPID);
					if (folderModel == null)
					{
						break;
					}
					stringCollections.Add(folderModel.FolderName);
				}
				for (int i = stringCollections.Count - 1; i > -1; i--)
				{
					stringBuilder.AppendFormat("/{0}", stringCollections[i]);
				}
				str = stringBuilder.ToString();
			}
			else
			{
				str = "";
			}
			return str;
		}

		public static string GetNewFolderWbsCode(string pFolderId)
		{
			object obj = SysDatabase.ExecuteScalar(string.Format("select IsNull(FolderWBS,'') from T_E_File_Folder where _AutoId='{0}'", pFolderId));
			string str = "0";
			if (obj != null)
			{
				if (obj == DBNull.Value)
				{
					throw new Exception("父文件夹没有定义WBS");
				}
				str = obj.ToString();
			}
			string str1 = string.Format("select Max(cast(right(FolderWBS,len(FolderWBS)-{1}) as int)) from T_E_File_Folder where Isnull(FolderWBS,'')<>'' and FolderPId = '{0}'", pFolderId, str.Length + 1);
			obj = SysDatabase.ExecuteScalar(str1);
			int num = 1;
			if (obj != DBNull.Value)
			{
				num = int.Parse(obj.ToString()) + 1;
			}
			return string.Concat(str, ".", num.ToString());
		}

		public static string GetParentShareLimit(string folderId)
		{
			string shareLimit;
			FolderInfo model = (new _FolderInfo()).GetModel(folderId);
			if (model == null)
			{
				shareLimit = "0";
			}
			else if (model.ShareUserId.Length <= 0)
			{
				shareLimit = (model.FolderPID.Length <= 1 ? "0" : FolderService.GetParentShareLimit(model.FolderPID));
			}
			else
			{
				shareLimit = model.ShareLimit;
			}
			return shareLimit;
		}

		public static StringCollection GetRoleList(string empId)
		{
			StringCollection stringCollections = new StringCollection();
			DataTable dataTable = SysDatabase.ExecuteTable(string.Concat("select * from T_E_Org_RoleEmployee where EmployeeId='", empId, "' and _isDel=0"));
			foreach (DataRow row in dataTable.Rows)
			{
				stringCollections.Add(row["RoleId"].ToString());
			}
			return stringCollections;
		}

		public static List<FolderInfo> GetSonFolder(string pFolderId)
		{
			_FolderInfo __FolderInfo = new _FolderInfo();
			return __FolderInfo.GetModelList(string.Concat("FolderPID='", pFolderId, "'"));
		}

		public static bool IsDocAdmin(string empId)
		{
			StringCollection stringCollections = new StringCollection();
			object obj = SysDatabase.ExecuteScalar(string.Format("select COUNT(*) from T_E_Org_Role r inner join T_E_Org_RoleEmployee e on r._AutoID=e.RoleID\r\n                where r.RoleCode='Doc_Admin' and e.EmployeeID='{0}'", empId));
			return obj.ToString() != "0";
		}

		public static void MoveFolder(string folderId, string newParentId)
		{
			_FolderInfo __FolderInfo = new _FolderInfo();
			FolderInfo model = __FolderInfo.GetModel(folderId);
			model.FolderPID = newParentId;
			__FolderInfo.Update(model);
		}
	}
}