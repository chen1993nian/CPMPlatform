using EIS.AppBase;
using EIS.AppCommon;
using EIS.DataAccess;
using EIS.Permission.Access;
using EIS.Permission.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Web.UI.HtmlControls;

namespace EIS.Studio.SysFolder.Limit
{
    public partial class FunLimitLeft : AdminPageBase
    {
        private List<FunNode> list_0;

        public string treedata = "";

        public string webId = "";

     

        private void method_0(zTreeNode zTreeNode_0)
        {
            List<FunNode> funNodes = this.list_0.FindAll((FunNode funNode_0) => funNode_0.FunPWBS == zTreeNode_0.id);
            if (funNodes.Count > 0)
            {
                foreach (FunNode funNode in funNodes)
                {
                    zTreeNode _zTreeNode = new zTreeNode()
                    {
                        name = funNode.FunName,
                        id = funNode.FunWBS,
                        @value = funNode._AutoID
                    };
                    zTreeNode_0.Add(_zTreeNode);
                    this.method_0(_zTreeNode);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.webId = base.GetParaValue("webId");
            _FunNode __FunNode = new _FunNode();
            this.list_0 = __FunNode.GetModelList(string.Concat("webId='", this.webId, "'"));
            string str = string.Format("select _AutoID,WebName from T_E_Sys_WebId where webId='{0}'", this.webId);
            DataTable dataTable = SysDatabase.ExecuteTable(str);
            zTreeNode _zTreeNode = new zTreeNode()
            {
                id = "0",
                name = dataTable.Rows[0]["WebName"].ToString(),
                @value = dataTable.Rows[0]["_AutoID"].ToString(),
                open = true
            };
            this.method_0(_zTreeNode);
            this.treedata = _zTreeNode.ToJsonString(true);
        }
    }
}