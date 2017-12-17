using AjaxPro;
using EIS.AppBase;
using EIS.DataModel.Access;
using System;
using System.Web.UI.HtmlControls;
namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefBizList2 : AdminPageBase
    {
        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void DelRecord(string tblname)
        {
            (new _TableInfo(tblname)).DropTable();
        }
        public string othercond = "";

        public string parent = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefBizList2));
            this.parent = base.GetParaValue("parent");
            this.othercond = string.Concat("parentname='", this.parent, "'");
        }
    }
}