using AjaxPro;
using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web.Doc
{
	public partial class MyDocList : PageBase
	{
		public string treedata = "";

		public string folderId = "";

		public string folderPId = "";

		public string fullPath = "";

		public string PasteHide = "1";

		private List<FolderInfo> list_0 = new List<FolderInfo>();

		

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void DeleteFile(string[] idList, string[] folderList)
		{
			int i;
			string str;
			_FolderInfo __FolderInfo = new _FolderInfo();
			string[] strArrays = folderList;
			for (i = 0; i < (int)strArrays.Length; i++)
			{
				str = strArrays[i];
				if (__FolderInfo.GetFileCount(str) + __FolderInfo.GetSubFolderCount(str) > 0)
				{
					throw new Exception("文件夹下面还有对象，不能删除");
				}
			}
			_AppFile __AppFile = new _AppFile();
			strArrays = idList;
			for (i = 0; i < (int)strArrays.Length; i++)
			{
				__AppFile.Delete(strArrays[i]);
			}
			strArrays = folderList;
			for (i = 0; i < (int)strArrays.Length; i++)
			{
				str = strArrays[i];
				__FolderInfo.Delete(str);
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
			AjaxPro.Utility.RegisterTypeForAjax(typeof(MyDocList));
			this.folderId = base.GetParaValue("folderId");
			if (this.folderId != base.EmployeeID)
			{
				this.folderPId = FolderService.GetFolderModel(this.folderId).FolderPID;
				this.fullPath = FolderService.GetFullPath(this.folderId, base.EmployeeID).Replace("/", " / ");
			}
			else
			{
				this.folderPId = "";
				this.fullPath = "/";
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