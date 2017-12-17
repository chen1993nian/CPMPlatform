using AjaxPro;
using EIS.AppBase;
using EIS.AppCommon;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.UI.HtmlControls;

namespace Studio.JZY.Doc
{
	public partial class DocView_Tree : PageBase
	{
		

		public string treedata = "";

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

		private void method_0(zTreeNode zTreeNode_0)
		{
			List<FolderInfo> folderInfos = this.list_0.FindAll((FolderInfo folderInfo_0) => folderInfo_0.FolderPID == zTreeNode_0.id);
			if (folderInfos.Count > 0)
			{
				foreach (FolderInfo folderInfo in folderInfos)
				{
					if (folderInfo._IsDel == 1)
					{
						continue;
					}
					zTreeNode _zTreeNode = new zTreeNode()
					{
						name = folderInfo.FolderName,
						id = folderInfo._AutoID,
						@value = ""
					};
					zTreeNode_0.Add(_zTreeNode);
					this.method_0(_zTreeNode);
				}
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
			AjaxPro.Utility.RegisterTypeForAjax(typeof(DocTree));
			this.treeId = base.GetParaValue("treeId");
			this.list_0 = FolderService.GetAllFolder();
			zTreeNode _zTreeNode = new zTreeNode();
			if (this.treeId != "")
			{
				FolderInfo model = (new _FolderInfo()).GetModel(this.treeId);
				if (model != null)
				{
					_zTreeNode.id = this.treeId;
					_zTreeNode.name = model.FolderName;
					_zTreeNode.@value = "";
					_zTreeNode.open = true;
				}
				else
				{
					string paraValue = base.GetParaValue("treeName");
					_zTreeNode.id = this.treeId;
					_zTreeNode.name = paraValue;
					_zTreeNode.@value = "";
					_zTreeNode.open = true;
				}
			}
			else
			{
				this.treeId = "0";
				_zTreeNode.id = "0";
				_zTreeNode.name = "公司文档";
				_zTreeNode.@value = "";
				_zTreeNode.open = true;
			}
			this.method_0(_zTreeNode);
			this.treedata = _zTreeNode.ToJsonString(true);
		}
	}
}