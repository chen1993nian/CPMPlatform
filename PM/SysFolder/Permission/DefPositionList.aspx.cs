using AjaxPro;
using EIS.AppBase;
using EIS.Permission.Service;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.Permission
{
    public partial  class DefPositionList : AdminPageBase
    {
        public string DeptID = "";

    

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefPositionList));
            this.DeptID = base.GetParaValue("DeptID");
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void RemovePosition(string positionId)
        {
            PositionService.RemovePosition(positionId);
        }
    }
}