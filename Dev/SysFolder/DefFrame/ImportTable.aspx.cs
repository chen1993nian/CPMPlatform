using AjaxPro;
using EIS.AppBase;
using EIS.DataModel.Service;
using System;
using System.Web.UI.HtmlControls;
namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class ImportTable : AdminPageBase
    {
        public string othercond = "";

        public string nodewbs = "";

        public string parent = "";

       
        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public bool ImportPhyTable(string tblName, string catCode)
        {
            return TableService.ImportTable(tblName, catCode, base.EmployeeID, base.OrgCode);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ImportTable));
        }
    }
}