using AjaxPro;
using EIS.AppBase;
using EIS.Permission.Service;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Studio.SysFolder.Permission
{
    public partial class GroupRoleList : AdminPageBase
    {
      
        public string DeptPWBS = "";

       

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(GroupRoleList));
            this.DeptPWBS = base.GetParaValue("DeptPWBS");
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void RemoveRole(string roleId)
        {
            RoleService.RemoveRole(roleId);
        }
    }
}