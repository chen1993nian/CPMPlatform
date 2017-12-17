using AjaxPro;
using EIS.AppBase;
using EIS.WorkFlow.Engine;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.WebBase.SysFolder.WorkFlow
{
	public partial class FlowMyStart : PageBase
	{
		public string condition = "";

	

		public FlowMyStart()
		{
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string FetchTask(string instanceId)
		{
			return EIS.WorkFlow.Engine.Utility.FetchTask(instanceId, base.EmployeeID);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(FlowMyStart));
			this.condition = base.GetParaValue("condition");
			if (this.condition == "")
			{
				this.condition = string.Concat("_UserName='", base.EmployeeID, "'");
			}
			else
			{
				this.condition = string.Concat("_UserName='", base.EmployeeID, "' and ", this.condition);
			}
		}

        [AjaxMethod(HttpSessionStateRequirement.Read)]  // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void RemoveInstance(string instanceId)
		{
			EIS.WorkFlow.Engine.Utility.RemoveInstanceOnStart(instanceId, base.EmployeeID, base.UserInfo);
		}
	}
}