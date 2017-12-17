using AjaxPro;
using EIS.AppBase;
using EIS.AppModel.Service;
using EIS.DataAccess;
using EIS.Permission.Service;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.WorkAsp.Msg
{
	public partial class MsgRecList : PageBase
	{
		

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void DeleteAll()
		{
			string employeeID = base.EmployeeID;
			SysDatabase.ExecuteNonQuery(string.Concat("update T_E_App_MsgRec set _UpdateTime=getdate() , _IsDel = 1 where recId='", employeeID, "'"));
		}

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void DeleteAllReaded()
		{
			string employeeID = base.EmployeeID;
			SysDatabase.ExecuteNonQuery(string.Concat("update T_E_App_MsgRec set _UpdateTime=getdate() , _IsDel = 1 where isRead=1 and recId='", employeeID, "'"));
		}

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void DeleteReadRec(string msgId)
		{
			AppMsgRecService.DeleteReadRec(msgId, base.EmployeeID);
			int unReadMsgCount = AppMsgService.GetUnReadMsgCount(base.EmployeeID);
			EmployeeService.UpdateLastMsgCount(base.EmployeeID, unReadMsgCount);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(MsgRecList));
			int unReadMsgCount = AppMsgService.GetUnReadMsgCount(base.EmployeeID);
			EmployeeService.UpdateLastMsgCount(base.EmployeeID, unReadMsgCount);
		}

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void ReadAll(string msgId)
		{
			string employeeID = base.EmployeeID;
			SysDatabase.ExecuteNonQuery(string.Concat("update T_E_App_MsgRec set _UpdateTime=getdate() , ReadTime =getdate(),IsRead=1 where recId='", employeeID, "'"));
		}
	}
}