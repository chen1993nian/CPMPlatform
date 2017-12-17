using EIS.AppBase;
using EIS.DataModel.Access;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefSelectStyleLeft : AdminPageBase
    {
        private _Catalog _Catalog_0 = new _Catalog();

        public string treedata = "";

        public string fieldId = "";

        public string tblName = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefSelectStyleLeft));
            this.fieldId = base.GetParaValue("fieldId");
            this.tblName = base.GetParaValue("tblName");
        }
    }
}