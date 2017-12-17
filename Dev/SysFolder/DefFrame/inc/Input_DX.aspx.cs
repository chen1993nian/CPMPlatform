using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EIS.AppBase;
using System.Text;
using EIS.DataModel.Access;
using System.Collections.Generic;
using EIS.DataModel.Model;

namespace Studio.JZY.SysFolder.DefFrame.inc
{
    public partial class Input_DX : PageBase
    {
        public StringBuilder sbList = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            string tblName = base.GetParaValue("tblname");
            _TableInfo dalTbl = new _TableInfo(tblName);
            List<FieldInfo> list = dalTbl.GetFields();

            for (int i = 0; i < list.Count; i++)
            {
                sbList.AppendFormat("<option value='{0}'>{1} [ {0} ]</option>"
                    , list[i].FieldName, list[i].FieldNameCn);
            }
        }
    }
}