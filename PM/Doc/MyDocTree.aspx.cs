using EIS.AppBase;
using EIS.AppCommon;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

namespace EIS.Web.Doc
{
    public partial class MyDocTree : PageBase
    {
        

        public string treedata = "";

        public string treeId = "";

        private List<FolderInfo> list_0 = new List<FolderInfo>();

    

        private void method_0(zTreeNode zTreeNode_0)
        {
            List<FolderInfo> sonFolder = FolderService.GetSonFolder(zTreeNode_0.id);
            if (sonFolder.Count > 0)
            {
                foreach (FolderInfo folderInfo in sonFolder)
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

        protected void Page_Load(object sender, EventArgs e)
        {
            this.treeId = base.GetParaValue("treeId");
            zTreeNode _zTreeNode = new zTreeNode()
            {
                id = base.EmployeeID,
                name = "我的文档",
                @value = "",
                icon = "../img/doc/home.png",
                open = true
            };
            zTreeNode _zTreeNode1 = new zTreeNode()
            {
                name = "收藏夹",
                id = "myFavorite",
                @value = "",
                icon = "../img/doc/star.png"
            };
            _zTreeNode.Add(_zTreeNode1);
            _zTreeNode1 = new zTreeNode()
            {
                name = "收件夹",
                id = "myReceive",
                @value = "",
                icon = "../img/doc/folder_go.png"
            };
            _zTreeNode.Add(_zTreeNode1);
            _zTreeNode1 = new zTreeNode()
            {
                name = "共享文档",
                id = "myShare",
                @value = "",
                icon = "../img/doc/folder_user.png"
            };
            _zTreeNode.Add(_zTreeNode1);
            this.method_0(_zTreeNode);
            this.treedata = _zTreeNode.ToJsonString(true);
        }
    }
}