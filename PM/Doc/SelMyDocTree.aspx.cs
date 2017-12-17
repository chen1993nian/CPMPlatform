using EIS.AppBase;
using EIS.AppCommon;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

namespace EIS.Web.Doc
{
    public partial class SelMyDocTree : PageBase
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
            this.method_0(_zTreeNode);
            this.treedata = _zTreeNode.ToJsonString(true);
        }
    }
}