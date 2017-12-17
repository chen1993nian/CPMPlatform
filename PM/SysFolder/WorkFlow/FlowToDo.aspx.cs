using AjaxPro;
using EIS.AppBase;
using EIS.Permission;
using EIS.WorkFlow.Service;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.WorkFlow
{
	public partial class FlowToDo : PageBase
	{
		

		public string EmpId = "";

		public string funId = "";

		public string bakUrl = "";


		protected void Page_Load(object sender, EventArgs e)
		{
			this.EmpId = base.GetParaValue("EmployeeId");
			this.EmpId = (string.IsNullOrEmpty(this.EmpId) ? base.EmployeeID : this.EmpId);
			AjaxPro.Utility.RegisterTypeForAjax(typeof(FlowToDo));
			this.funId = base.GetParaValue("funId");
			if (this.funId.Length > 0)
			{
				this.bakUrl = EIS.Permission.Utility.GetFunAttrById(this.funId, "FunUrl");
			}
		}

		  [AjaxMethod(HttpSessionStateRequirement.Read)]  // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void updateTaskAgent(string[] uTaskList, string agentId)
		{
			if (!string.IsNullOrEmpty(agentId))
			{
				string[] strArrays = uTaskList;
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					UserTaskService.UpdateTaskAgent(strArrays[i], agentId);
				}
			}
		}

          [AjaxMethod(HttpSessionStateRequirement.Read)]   // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void updateTaskOwner(string[] uTaskList, string ownerId, string posId)
		{
			if (!string.IsNullOrEmpty(ownerId))
			{
				string[] strArrays = uTaskList;
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					UserTaskService.UpdateTaskOwner(strArrays[i], ownerId, posId);
				}
			}
		}
	}
}