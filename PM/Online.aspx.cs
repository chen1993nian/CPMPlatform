using EIS.AppBase;
using EIS.AppBase.Config;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web
{
    public partial class Online : PageBase
    {
        protected HtmlForm form1;

        public string refreshInterval = "120";

        protected void Page_Load(object sender, EventArgs e)
        {
            string itemValue = SysConfig.GetConfig("OnlineRefreshInterval").ItemValue;
            int num = (string.IsNullOrEmpty(itemValue) ? 120 : Convert.ToInt32(itemValue));
            this.refreshInterval = ((num < 30 ? 30 : num)).ToString();
        }
    }
}