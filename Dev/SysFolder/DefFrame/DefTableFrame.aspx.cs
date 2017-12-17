using AjaxPro;
using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefTableFrame : AdminPageBase
    {
        public string CryptStr = "";
        public string CryptStr1 = "";
        public string CryptStr2 = "";
        public string CryptStr3 = "";
        public string CryptStr4 = "";
        public string CryptStr5 = "";

        public string subtbl = "";

        public string tblName = "";

        public string t = "";

        public string tblNameCn = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            this.tblName = base.GetParaValue("tblname");
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefTableFrame));
            _TableInfo __TableInfo = new _TableInfo(this.tblName);
            TableInfo model = __TableInfo.GetModel();
            this.tblNameCn = model.TableNameCn;
            this.t = model.TableType.ToString();
            foreach (TableInfo subTable in __TableInfo.GetSubTable())
            {
                DefTableFrame defTableFrame = this;
                defTableFrame.subtbl = string.Concat(defTableFrame.subtbl, ",", subTable.TableName);
            }
            if (this.subtbl.Length > 0)
            {
                this.subtbl = this.subtbl.Substring(1);
            }

            //tblname=<%=tblName %>&sindex=&t=<%=t %>
            this.CryptStr = base.CryptPara("tblname=" + this.tblName);
            this.CryptStr1 = base.CryptPara("tblname=" + this.tblName + "&sindex=&t=1");
            this.CryptStr2 = base.CryptPara("tblname=" + this.tblName + "&sindex=&t=" + this.t);
            this.CryptStr3 = base.CryptPara("tblname=T_E_Sys_TableScript&bizName=" + this.tblName + "&sindex=&t=" + this.t);
            this.CryptStr4 = base.CryptPara("tblname=T_E_Sys_TableDll&Condition=TableName='" + this.tblName + "'&cpro=TableName=" + this.tblName + "^1&ext=700|400");
            this.CryptStr5 = base.CryptPara("tblname=" + this.tblName + "&t=" + this.t + "&admin=1");


        }
    }
}