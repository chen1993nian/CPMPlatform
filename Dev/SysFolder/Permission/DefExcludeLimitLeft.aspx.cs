using AjaxPro;
using EIS.AppBase;
using EIS.DataModel.Access;
using System;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EIS.Studio.SysFolder.Permission
{
    public partial class DefExcludeLimitLeft : AdminPageBase
    {
       


        public string othercond = "";
        public StringBuilder condition = new StringBuilder();
        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void DelRecord(string tblname)
        {
            (new _TableInfo(tblname)).DropTable();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefExcludeLimitLeft));
            string paraValue = base.GetParaValue("nodewbs");
            if (base.LoginType != "2")
            {
                this.condition.AppendFormat("TableCat like '{0}%' and TableType=3", paraValue);
            }
            else
            {
                this.condition.AppendFormat("TableCat like '{0}%' and TableType=3 and _UserName='{1}'", paraValue, base.EmployeeID);
            }
        }
}
}