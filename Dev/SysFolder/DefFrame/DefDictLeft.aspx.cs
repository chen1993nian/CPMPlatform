using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.UI.HtmlControls;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefDictLeft : AdminPageBase
    {
       

        private List<Catalog> list_0;

        private _Catalog _Catalog_0 = new _Catalog();

        public string treedata = "";


        private void method_0(TreeItem treeItem_0)
        {
       
            List<Catalog> catalogs = this.list_0.FindAll((Catalog catalog_0) => catalog_0.PCatCode == treeItem_0.id);
            if (catalogs.Count > 0)
            {
                foreach (Catalog catalog in catalogs)
                {
                    TreeItem treeItem = new TreeItem()
                    {
                        text = catalog.CatName,
                        id = catalog.CatCode
                    };
                    treeItem_0.Add(treeItem);
                    this.method_0(treeItem);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string paraValue = base.GetParaValue("rootwbs");
            this.list_0 = this._Catalog_0.GetModelListByCode(paraValue);
            TreeItem treeItem = new TreeItem()
            {
                id = base.GetParaValue("rootwbs"),
                text = base.GetParaValue("rootname"),
                isexpand = true
            };
            this.method_0(treeItem);
            this.treedata = treeItem.ToJsonString();
        }
    }
}