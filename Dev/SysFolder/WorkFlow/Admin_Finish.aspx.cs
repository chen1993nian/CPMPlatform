using AjaxPro;
using EIS.AppBase;
using EIS.WorkFlow.Engine;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.WebBase.SysFolder.WorkFlow
{
    public partial class Admin_Finish : PageBase
    {
     
        [AjaxMethod(HttpSessionStateRequirement.Read)]   // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void FetchInstance(string instanceId)
		{
			EIS.WorkFlow.Engine.Utility.FetchInstance(instanceId);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(Admin_Finish));
		}

		 [AjaxMethod(HttpSessionStateRequirement.Read)]   // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void RemoveInstance(string instanceId)
		{
			EIS.WorkFlow.Engine.Utility.RemoveInstance(instanceId, base.UserInfo);
		}
    }
}