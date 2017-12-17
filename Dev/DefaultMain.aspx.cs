using EIS.AppBase;
using EIS.Permission;
using EIS.Permission.Access;
using EIS.Permission.Model;
using EIS.Permission.Service;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission;
using System;
using System.Data;
using System.Text;
using System.Web.SessionState;
using System.Web.UI;
using AjaxPro;

namespace EIS.Studio
{
    public partial class DefaultMain : AdminPageBase
    {
        public string mainMenu = "";

        public StringBuilder leftMenu = new StringBuilder();


        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            _FunNode __FunNode = new _FunNode();
            if ((base.LoginType == "1") || (base.LoginType == "2"))
            {
                string str = "";
                if (base.LoginType == "1")
                {
                    str = "_GroupAdmin";
                }
                else if (base.LoginType == "2")
                {
                    str = "_CompanyAdmin";
                }
                string roleId = RoleService.GetRoleId(str);
                if (roleId != "")
                {
                    foreach (DataRow row in EIS.Permission.Utility.GetSonLimitDataByRole(roleId, "0", "0").Rows)
                    {
                        this.leftMenu.AppendFormat("<a href='#' wbs='{0}'>{1}</a><div id='menuTree_{0}'></div>", row["FunWBS"], row["FunName"]);
                    }
                }
            }
            else
            {
                foreach (FunNode topList in __FunNode.GetTopList(AppSettings.Instance.WebId))
                {
                    this.leftMenu.AppendFormat("<a href='#' wbs='{0}'>{1}</a><div id='menuTree_{0}'></div>", topList.FunWBS, topList.FunName);
                }
            }
        }
    }
}