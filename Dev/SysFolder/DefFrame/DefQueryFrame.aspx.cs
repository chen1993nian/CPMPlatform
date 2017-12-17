using AjaxPro;
using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefQueryFrame : AdminPageBase
    {

        public string CryptStr = "";
        public string CryptStr2 = "";

        public string subtbl = "";

        public string tblname = "";

        public string tblNameCn = "";

     

        protected void Page_Load(object sender, EventArgs e)
        {
            //AjaxPro.Utility.RegisterTypeForAjax(typeof(DefTableFrame));
            this.tblname = base.GetParaValue("tblname");
            this.CryptStr = base.CryptPara("tblname=" + this.tblname);
            this.CryptStr2 = base.CryptPara("tblname=T_E_Sys_TableScript&bizName=" + this.tblname);

            _TableInfo __TableInfo = new _TableInfo(this.tblname);
            this.tblNameCn = __TableInfo.GetModel().TableNameCn;
            foreach (TableInfo subTable in __TableInfo.GetSubTable())
            {
                DefQueryFrame defQueryFrame = this;
                defQueryFrame.subtbl = string.Concat(defQueryFrame.subtbl, ",", subTable.TableName);
            }
            if (this.subtbl.Length > 0)
            {
                this.subtbl = this.subtbl.Substring(1);
            }
        }
    }
}