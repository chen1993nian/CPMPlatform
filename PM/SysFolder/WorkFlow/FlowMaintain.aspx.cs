using System;
using System.Collections.Generic;
using AjaxPro;
using EIS.AppBase;
using EIS.Permission;
using EIS.Permission.Model;
using EIS.Permission.Service;
using EIS.WorkFlow.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;
namespace EIS.Web.SysFolder.WorkFlow
{
    public partial class FlowMaintain :PageBase
    {
        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void FetchTask(string instanceId)
        {
            EIS.WorkFlow.Engine.Utility.FetchTask(instanceId, base.EmployeeID);
        }
        public string condition = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(FlowMaintain));
            this.condition = base.GetParaValue("condition");
        }
    }
}