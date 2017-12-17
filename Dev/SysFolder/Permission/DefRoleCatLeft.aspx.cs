using AjaxPro;
using EIS.AppBase;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.UI.HtmlControls;
namespace EIS.Studio.SysFolder.Permission
{
    public partial class DefRoleCatLeft : AdminPageBase
    {
        private List<RoleCatalog> list_0;

        public string treedata = "";

      

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string GetNewWbs(string catPWBS)
        {
            return RoleCatalogService.GetNewWbs(catPWBS);
        }

        private void method_0(TreeItem treeItem_0)
        {
           
            List<RoleCatalog> roleCatalogs = this.list_0.FindAll((RoleCatalog roleCatalog_0) => roleCatalog_0.CatalogPWBS == treeItem_0.id);
            if (roleCatalogs.Count > 0)
            {
                foreach (RoleCatalog roleCatalog in roleCatalogs)
                {
                    TreeItem treeItem = new TreeItem()
                    {
                        text = roleCatalog.CatalogName,
                        id = roleCatalog.CatalogWBS,
                        @value = roleCatalog._AutoID
                    };
                    treeItem_0.Add(treeItem);
                    this.method_0(treeItem);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefRoleCatLeft));
            RoleCatalog top = RoleCatalogService.GetTop();
            this.list_0 = (new _RoleCatalog()).GetListByWbs(top.CatalogWBS);
            TreeItem treeItem = new TreeItem()
            {
                id = top.CatalogWBS,
                text = top.CatalogName,
                @value = top._AutoID,
                isexpand = true
            };
            this.method_0(treeItem);
            this.treedata = treeItem.ToJsonString();
        }
    }
}