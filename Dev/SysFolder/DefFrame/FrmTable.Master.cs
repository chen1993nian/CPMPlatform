using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class FrmTable : MasterPage
    {

        public string TblName = "";

        public string TblNameCn = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.TblName = base.Request["TblName"];
            if (!string.IsNullOrEmpty(this.TblName))
            {
                TableInfo model = (new _TableInfo(this.TblName)).GetModel();
                if (model != null)
                {
                    this.TblNameCn = model.TableNameCn;
                }
            }
        }
    }
}