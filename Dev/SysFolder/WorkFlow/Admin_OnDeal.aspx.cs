using AjaxPro;
using EIS.AppBase;
using EIS.WorkFlow.Engine;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.WebBase.SysFolder.WorkFlow
{
    public partial class Admin_OnDeal:PageBase
    {
     


		 [AjaxMethod(HttpSessionStateRequirement.Read)]       // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void FinishInstance(string instanceId)
		{
			EIS.WorkFlow.Engine.Utility.FinishInstance(instanceId, base.UserInfo);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(Admin_OnDeal));
		}

		 [AjaxMethod(HttpSessionStateRequirement.Read)]      // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void RemoveInstance(string instanceId)
		{
			EIS.WorkFlow.Engine.Utility.RemoveInstance(instanceId, base.UserInfo);
		}

         [AjaxMethod(HttpSessionStateRequirement.Read)]       // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void StopInstance(string instanceId)
		{
            EIS.WorkFlow.Engine.Utility.StopInstance(instanceId, "", base.UserInfo);
		}
    }
}