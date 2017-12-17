using AjaxPro;
using EIS.AppBase;
using EIS.AppModel.Service;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.WorkAsp.Msg
{
	public partial class MsgSendList : PageBase
	{
		

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void DeleteMessage(string msgId)
		{
			AppMsgService.DeleteMessage(msgId);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(MsgSendList));
		}
	}
}