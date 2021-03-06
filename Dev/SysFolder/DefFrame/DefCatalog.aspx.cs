﻿using AjaxPro;
using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.UI.HtmlControls;


namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefCatalog : AdminPageBase
    {
        private List<Catalog> list_0;

        public string treedata = "";
        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public int delnode(string nodewbs)
        {
            return (new _Catalog()).DeleteByCode(nodewbs);
        }

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
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefCatalog));
            string paraValue = base.GetParaValue("rootwbs");
            string str = base.GetParaValue("rootname");
            this.list_0 = (new _Catalog()).GetModelListByCode(paraValue);
            TreeItem treeItem = new TreeItem()
            {
                id = paraValue,
                text = str,
                isexpand = true
            };
            this.method_0(treeItem);
            this.treedata = treeItem.ToJsonString();
        }
    }
}