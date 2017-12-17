using AjaxPro;
using EIS.AppBase;
using EIS.WorkFlow.Engine;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.WorkFlow
{
	public partial class FlowArchive : PageBase
	{
	

		public FlowArchive()
		{
		}

        [AjaxMethod(HttpSessionStateRequirement.Read)]   // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void FetchTask(string instanceId)
		{
			EIS.WorkFlow.Engine.Utility.FetchTask(instanceId, base.EmployeeID);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(FlowArchive));
		}
	}
}