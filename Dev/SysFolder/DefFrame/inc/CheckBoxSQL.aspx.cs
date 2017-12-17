using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EIS.Studio.SysFolder.DefFrame.inc
{
    public partial class CheckBoxSQL : PageBase
    {
        public StringBuilder sbList = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            List<FieldInfo> fields = (new _TableInfo(base.GetParaValue("tblname"))).GetFields();
            for (int i = 0; i < fields.Count; i++)
            {
                this.sbList.AppendFormat("<option value='{0}'>{1} [ {0} ]</option>", fields[i].FieldName, fields[i].FieldNameCn);
            }
        }
    }
}