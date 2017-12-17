using AjaxPro;
using EIS.AppBase;
using EIS.DataModel.Access;
using System;


namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefDictList : AdminPageBase
    {
        public string NodeWbs = "";

		

		[AjaxMethod(HttpSessionStateRequirement.Read)]
		public int DelRecord(string dictid)
		{
			return (new _Dict()).Delete(dictid);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(DefDictList));
			this.NodeWbs = base.GetParaValue("nodewbs");
		}
    }
}