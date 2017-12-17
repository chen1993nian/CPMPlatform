using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using System;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefBizList : AdminPageBase
    {
        public string othercond = "";

        public string nodewbs = "";

        public string parent = "";

        public StringBuilder condition = new StringBuilder();

        

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void DelRecord(string tblname)
        {
            if (SysDatabase.ExecuteScalar(string.Format("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.[{0}]') AND type in (N'U')) \r\n                select count(*) from {0} \r\n                else \r\n                select 0", tblname)).ToString() != "0")
            {
                throw new Exception("数据库物理表中已经存在数据，不能删除");
            }
            (new _TableInfo(tblname)).DropTable();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefBizList));
            this.nodewbs = base.GetParaValue("nodewbs");
            this.parent = base.GetParaValue("parent");
            this.othercond = "TableType=1";
            if (base.LoginType != "2")
            {
                this.condition.AppendFormat("TableCat like '{0}%' and TableType=1", this.nodewbs);
            }
            else
            {
                this.condition.AppendFormat("TableCat like '{0}%' and TableType=1 and _UserName='{1}'", this.nodewbs, base.EmployeeID);
            }
        }
    }
}