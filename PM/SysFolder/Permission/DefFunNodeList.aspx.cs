using AjaxPro;
using EIS.AppBase;
using EIS.Permission.Access;
using EIS.Permission.Service;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.Permission
{
    public partial class DefFunNodeList : AdminPageBase
    {
        public string webId = "";

        public string funpwbs = "";



        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void DelRecord(string funId)
        {
            (new _FunNode()).Delete(funId);
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public string GetNewWbs(string PWBS, string webId)
        {
            return FunNodeService.GetNewWbs(PWBS, webId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefFunNodeList));
            this.webId = base.GetParaValue("webId");
            this.funpwbs = base.GetParaValue("funpwbs");
        }
    }
}