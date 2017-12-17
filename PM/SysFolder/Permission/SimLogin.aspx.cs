using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.Permission
{
    public partial class SimLogin : AdminPageBase
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            bool flag;
            Employee model = EmployeeService.GetModel(base.Request["empId"]);
            if (model == null)
            {
                flag = true;
            }
            else
            {
                flag = (base.LoginType == "0" ? false : !(base.LoginType == "3"));
            }
            if (!flag)
            {
                string loginName = model.LoginName;
                long ticks = DateTime.Now.Ticks;
                string str = Security.EncryptStr(string.Concat("u=", loginName, "&t=", ticks.ToString()));
                string itemValue = SysConfig.GetConfig("loginPage").ItemValue;
                base.Response.Redirect(string.Concat(itemValue, "?loginKey=", base.Server.UrlEncode(str)));
            }
        }
    }
}