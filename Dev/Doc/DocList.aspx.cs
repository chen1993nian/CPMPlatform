using AjaxPro;
using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Studio.JZY.Doc
{
	public partial class DocList : PageBase
	{
	

		public string treedata = "";

		public string folderId = "";

		public string folderPId = "";

		public string fullPath = "";

		public string treeId = "";

		public string limit = "0";

		public string PasteHide = "1";

		private List<FolderInfo> list_0 = new List<FolderInfo>();

	

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void DeleteFile(string[] idList, string[] folderList)
		{
			int i;
			_FolderInfo __FolderInfo = new _FolderInfo();
			string[] strArrays = folderList;
			for (i = 0; i < (int)strArrays.Length; i++)
			{
				string str = strArrays[i];
				if (__FolderInfo.GetFileCount(str) + __FolderInfo.GetSubFolderCount(str) > 0)
				{
					throw new Exception("文件夹下面还有对象，不能删除");
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			_AppFile __AppFile = new _AppFile();
			string str1 = HttpContext.Current.Session["EmployeeID"].ToString();
			strArrays = idList;
			for (i = 0; i < (int)strArrays.Length; i++)
			{
				string str2 = strArrays[i];
				if (FolderService.GetFileLimit(str2, str1) < 9)
				{
					string fileAttr = FileService.GetFileAttr(str2, "FactFileName");
					stringBuilder.AppendFormat(string.Concat(fileAttr, "，"), new object[0]);
				}
				else
				{
					__AppFile.Delete(str2);
				}
			}
			strArrays = folderList;
			for (i = 0; i < (int)strArrays.Length; i++)
			{
				string str3 = strArrays[i];
				if (FolderService.GetFolderLimit(str3, str1) < 9)
				{
					string folderAttr = FolderService.GetFolderAttr(str3, "FolderName");
					stringBuilder.AppendFormat(string.Concat(folderAttr, "，"), new object[0]);
				}
				else
				{
					__FolderInfo.Delete(str3);
				}
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length = stringBuilder.Length - 1;
				stringBuilder.Insert(0, "您没有权限删除下列对象：");
				throw new Exception(stringBuilder.ToString());
			}
		}

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string NewFolder(string folderName, string folderPId)
		{
			_FolderInfo __FolderInfo = new _FolderInfo();
			FolderInfo folderInfo = new FolderInfo(base.UserInfo)
			{
				FolderName = folderName,
				FolderPID = folderPId,
				OrderID = 0
			};
			__FolderInfo.Add(folderInfo);
			return folderInfo._AutoID;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(DocList));
			this.folderId = base.GetParaValue("folderId");
			this.treeId = base.GetParaValue("treeId");
			if (this.folderId != this.treeId)
			{
				FolderInfo folderModel = FolderService.GetFolderModel(this.folderId);
				this.folderPId = folderModel.FolderPID;
				this.fullPath = folderModel.FolderName;
			}
			else
			{
				this.folderPId = "";
				this.fullPath = "/";
			}
			if (this.folderId != "0")
			{
				int folderLimit = FolderService.GetFolderLimit(this.folderId, base.EmployeeID);
				this.limit = folderLimit.ToString();
			}
			else if (FolderService.IsDocAdmin(base.EmployeeID))
			{
				this.limit = "9";
			}
			if (this.Session["FileSelect"] != null && this.Session["FileSelect"].ToString().Length > 0)
			{
				this.PasteHide = "0";
			}
		}

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void PasteSelect(string folderId)
		{
			int i;
			_AppFile __AppFile;
			string str;
			AppFile model;
			string[] strArrays;
			if (HttpContext.Current.Session["FileSelect"] != null)
			{
				string str1 = HttpContext.Current.Session["FileSelect"].ToString();
				if (str1.Length != 0)
				{
					string str2 = str1.Substring(2);
					char[] chrArray = new char[] { '$' };
					string[] strArrays1 = str2.Split(chrArray);
					if (str1.StartsWith("1"))
					{
						__AppFile = new _AppFile();
						string str3 = strArrays1[0];
						chrArray = new char[] { ',' };
						strArrays = str3.Split(chrArray);
						for (i = 0; i < (int)strArrays.Length; i++)
						{
							str = strArrays[i];
							if (str.Length != 0)
							{
								model = __AppFile.GetModel(str);
								model._AutoID = Guid.NewGuid().ToString();
								model._UserName = HttpContext.Current.Session["EmployeeId"].ToString();
								model._CreateTime = DateTime.Now;
								model._UpdateTime = DateTime.Now;
								model.FolderID = folderId;
								__AppFile.Add(model);
							}
						}
						_FolderInfo __FolderInfo = new _FolderInfo();
						string str4 = strArrays1[1];
						chrArray = new char[] { ',' };
						strArrays = str4.Split(chrArray);
						for (i = 0; i < (int)strArrays.Length; i++)
						{
							str = strArrays[i];
							if (str.Length != 0)
							{
								FolderService.CopyFolder(str, folderId);
							}
						}
					}
					else if (str1.StartsWith("2"))
					{
						__AppFile = new _AppFile();
						string str5 = strArrays1[0];
						chrArray = new char[] { ',' };
						strArrays = str5.Split(chrArray);
						for (i = 0; i < (int)strArrays.Length; i++)
						{
							str = strArrays[i];
							if (str.Length != 0)
							{
								model = __AppFile.GetModel(str);
								model._UpdateTime = DateTime.Now;
								model.FolderID = folderId;
								__AppFile.Update(model);
							}
						}
						_FolderInfo __FolderInfo1 = new _FolderInfo();
						string str6 = strArrays1[1];
						chrArray = new char[] { ',' };
						strArrays = str6.Split(chrArray);
						for (i = 0; i < (int)strArrays.Length; i++)
						{
							str = strArrays[i];
							if (str.Length != 0)
							{
								FolderService.MoveFolder(str, folderId);
							}
						}
					}
					HttpContext.Current.Session["FileSelect"] = "";
				}
			}
		}

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void SaveSelect(string actFlag, string[] idList, string[] folderList)
		{
			StringCollection stringCollections = new StringCollection();
			stringCollections.Add(actFlag);
			stringCollections.AddRange(idList);
			stringCollections.Add("$");
			stringCollections.AddRange(folderList);
            HttpContext.Current.Session["FileSelect"] = EIS.AppBase.Utility.GetJoinString(stringCollections);
		}
	}
}