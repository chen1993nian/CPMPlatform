using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.WebBase.SysFolder.AppFrame
{
    public partial class  AppChartView : PageBase
    {
        public string tblName = "";

        public string flashPath = "";

        public string string_0 = "";

        public string string_1 = "";

     
     

        protected void Page_Load(object sender, EventArgs e)
        {
            this.tblName = base.GetParaValue("tblName");
            if (!base.IsPostBack)
            {
                AppChart modelByQueryCode = (new _AppChart()).GetModelByQueryCode(this.tblName);
                if (modelByQueryCode != null)
                {
                    string str = string.Concat("select ChartPath from T_E_Sys_ChartConfig where _autoid='", modelByQueryCode.ChartType, "'");
                    this.flashPath = SysDatabase.ExecuteScalar(str).ToString();
                    this.string_0 = modelByQueryCode.ChartWidth;
                    this.string_1 = modelByQueryCode.ChartHeight;
                }
            }
        }
    }
}