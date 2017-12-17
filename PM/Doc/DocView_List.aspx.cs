using AjaxPro;
using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

namespace EIS.Web.Doc
{
	public partial class DocView_List : PageBase
	{
		public string treedata = "";

		public string folderId = "";

		public string folderPId = "";

		public string fullPath = "";

		public string treeId = "";

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
			AjaxPro.Utility.RegisterTypeForAjax(typeof(DocList));
			this.folderId = base.GetParaValue("folderId");
			this.treeId = base.GetParaValue("treeId");
			if (this.folderId != this.treeId)
			{
				this.folderPId = FolderService.GetFolderModel(this.folderId).FolderPID;
				this.fullPath = FolderService.GetFullPath(this.folderId, this.treeId).Replace("/", " / ");
			}
			else
			{
				this.folderPId = "";
				this.fullPath = "/";
			}
		}
	}
}