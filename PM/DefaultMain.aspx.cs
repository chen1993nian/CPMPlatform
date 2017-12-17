using AjaxPro;
using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.AppModel.Service;
using EIS.DataAccess;
using EIS.Permission;
using EIS.Permission.Model;
using EIS.Permission.Service;
using WebBase.JZY.Tools;
using EIS.WorkFlow.Service;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web
{
   
    public partial class DefaultMain : PageBase
    {


        public string mainMenu = "";

        public string refreshInterval = "120";

        public string PositionName = "";

        public string homePage = "home.aspx";

        public string MainTitle = "";

        public string customScript = "";

        public StringBuilder sbFlash = new StringBuilder();

        public StringBuilder sbPosList = new StringBuilder();



        protected void InitPage()
        {
            object[] item;

            string itemValue = SysConfig.GetConfig("OnlineRefreshInterval").ItemValue;
            int num = (string.IsNullOrEmpty(itemValue) ? 120 : Convert.ToInt32(itemValue));
            this.refreshInterval = ((num < 30 ? 30 : num)).ToString();
            if (!base.IsPostBack)
            {
                if (!string.IsNullOrEmpty(base.Request["relationId"]))
                {
                    WebTools.ChangeDefaultPosition(base.Request["relationId"]);
                }
                //this.fileLogger.Trace(string.Concat("webId=", base.Request["webId"]));
                string webid = base.GetParaValue("webId");
                if (webid != "")
                {
                    this.Session["webId"] = base.GetParaValue("webId");
                }
                else
                {
                    if (!string.IsNullOrEmpty(base.Request.QueryString["webId"]))
                    {
                        this.Session["webId"] = base.Request.QueryString["webId"];
                    }
                    else if (base.Request.Cookies["webId"] != null)
                    {
                        this.Session["webId"] = base.Request.Cookies["webId"].Value;
                    }
                }
            }
            if (EmployeeService.GetModel(base.EmployeeID).EmployeeType != "外部人员2")
            {
                string str = "1";
                string str1 = string.Format("select funValue from T_E_App_Personal where funId='DeskTop' and _userName='{0}'", base.EmployeeID);
                object obj = SysDatabase.ExecuteScalar(str1);
                str = (obj != null ? obj.ToString() : SysConfig.GetConfig("Basic_DeskTop", false).ItemValue);
                if (str == "1")
                {
                    this.homePage = "home.aspx";
                }
                else
                {
                    this.homePage = "deskTop.aspx";
                }

            }
            else
            {
                string itemValue1 = SysConfig.GetConfig("OuterEmployeeWebId").ItemValue;
                if (!string.IsNullOrEmpty(itemValue1))
                {
                    this.Session["webId"] = itemValue1;
                }
                this.homePage = "";
            }
            base.Response.Cookies.Add(new HttpCookie("webId", this.Session["webId"].ToString()));
            this.MainTitle = AppSettings.GetMainTitle(this.Session["webId"].ToString());
            StringBuilder stringBuilder = new StringBuilder();
            int num1 = 0;
            DataTable sonLimitDataByEmployeeId = EIS.Permission.Utility.GetSonLimitDataByEmployeeId(base.EmployeeID, "0", this.Session["webId"].ToString());
            for (int i = 0; i < sonLimitDataByEmployeeId.Rows.Count; i++)
            {
                DataRow dataRow = sonLimitDataByEmployeeId.Rows[i];
                if (dataRow["Limit"].ToString().StartsWith("1"))
                {
                    if ((num1 != 0 || !(dataRow["LinkFile"].ToString() != "") ? false : this.homePage == ""))
                    {
                        this.homePage = dataRow["LinkFile"].ToString();
                        num1 = 1;
                    }
                    StringBuilder stringBuilder1 = stringBuilder;
                    item = new object[] { dataRow["FunWBS"], dataRow["FunName"], dataRow["LinkFile"], null };
                    item[3] = (i == 0 ? "on " : "");
                    stringBuilder1.AppendFormat("<li id=\"_M{0}\" class=\"{3}top_menu\"><a href=\"javascript:_M('{0}','{2}')\" hidefocus=\"true\" style=\"outline:none;\">{1}</a></li>\r", item);
                }
            }
            this.mainMenu = stringBuilder.ToString();
            if (this.homePage == "")
            {
                this.homePage = "home.aspx";
            }
            if (base.UserInfo.DeptName != base.UserInfo.CompanyName)
            {
                string[] companyName = new string[] { base.UserInfo.CompanyName, " - ", base.UserInfo.DeptName, " - ", base.UserInfo.PositionName };
                this.PositionName = string.Concat(companyName);
            }
            else
            {
                this.PositionName = string.Concat(base.UserInfo.CompanyName, " - ", base.UserInfo.PositionName);
            }
            DataTable dataTable = SysDatabase.ExecuteTable("select top 1 * from T_oa_flash where enable='是' and GETDATE() between startTime and endtime order by startTime");
            if (dataTable.Rows.Count > 0)
            {
                string str2 = dataTable.Rows[0]["attachId"].ToString();
                StringBuilder stringBuilder2 = this.sbFlash;
                item = new object[] { str2, dataTable.Rows[0]["Caption"], Convert.ToInt32(dataTable.Rows[0]["Duration"]) * 1000, dataTable.Rows[0]["winWidth"], dataTable.Rows[0]["winHeight"] };
                stringBuilder2.AppendFormat("$.zxxbox($('<img style=\"width:{3}px;height:{4}px\" src=\"SysFolder/Common/FileDown.aspx?appId={0}\">'),{{title:'{1}',bgclose:true,delay:{2},width:{3},height:{4}}});", item);
            }
            foreach (DeptEmployee deptEmployeeByEmployeeId in DeptEmployeeService.GetDeptEmployeeByEmployeeId(base.EmployeeID))
            {
                string str3 = "";
                string departmentName = DepartmentService.GetDepartmentName(deptEmployeeByEmployeeId.CompanyID);
                string str4 = (deptEmployeeByEmployeeId.PositionName == "未知" ? "" : string.Concat("－", deptEmployeeByEmployeeId.PositionName));
                str3 = (departmentName != deptEmployeeByEmployeeId.DeptName ? string.Concat(departmentName, "－", deptEmployeeByEmployeeId.DeptName, str4) : string.Concat(deptEmployeeByEmployeeId.DeptName, str4));
                if (this.sbPosList.Length > 0)
                {
                    this.sbPosList.Append("<li class='smart_menu_li_separate'>&nbsp;</li>");
                }
                this.sbPosList.AppendFormat("<li class='smart_menu_li'><a class='smart_menu_a' href='DefaultMain.aspx?relationId={0}'>{1}</a></li>", deptEmployeeByEmployeeId._AutoID, str3);
            }
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
        public string OnlineFlash()
        {
            StringBuilder stringBuilder;
            string itemValue;
            int onlineCount;
            int unReadMsgCount;
            int lastMsgCount;
            string str;
            int num;
            StringBuilder stringBuilder1;
            StringBuilder stringBuilder2;
            StringBuilder stringBuilder3;
            if (HttpContext.Current.Session["lastOprationTime"] != null)
            {
                int timeout = HttpContext.Current.Session.Timeout;
                long num1 = long.Parse(HttpContext.Current.Session["lastOprationTime"].ToString());
                if (DateTime.Now.CompareTo((new DateTime(num1)).AddMinutes((double)timeout)) <= 0)
                {
                    stringBuilder = new StringBuilder();
                    EmployeeService.UpdateRefreshTime(base.EmployeeID);
                    itemValue = SysConfig.GetConfig("OnlineRefreshInterval").ItemValue;
                    num = (string.IsNullOrEmpty(itemValue) ? 120 : Convert.ToInt32(itemValue));
                    onlineCount = EmployeeService.GetOnlineCount(num);
                    stringBuilder1 = stringBuilder.AppendFormat("{0}", onlineCount);
                    onlineCount = UserTaskService.GetToDoCount(base.EmployeeID);
                    stringBuilder2 = stringBuilder.AppendFormat("|{0}", onlineCount);
                    unReadMsgCount = AppMsgService.GetUnReadMsgCount(base.EmployeeID);
                    lastMsgCount = EmployeeService.GetLastMsgCount(base.EmployeeID);
                    stringBuilder3 = stringBuilder.AppendFormat("|{0},{1}", unReadMsgCount, lastMsgCount);
                    str = stringBuilder.ToString();
                    return str;
                }
                str = null;
                return str;
            }
            stringBuilder = new StringBuilder();
            EmployeeService.UpdateRefreshTime(base.EmployeeID);
            itemValue = SysConfig.GetConfig("OnlineRefreshInterval").ItemValue;
            num = (string.IsNullOrEmpty(itemValue) ? 120 : Convert.ToInt32(itemValue));
            onlineCount = EmployeeService.GetOnlineCount(num);
            stringBuilder1 = stringBuilder.AppendFormat("{0}", onlineCount);
            onlineCount = UserTaskService.GetToDoCount(base.EmployeeID);
            stringBuilder2 = stringBuilder.AppendFormat("|{0}", onlineCount);
            unReadMsgCount = AppMsgService.GetUnReadMsgCount(base.EmployeeID);
            lastMsgCount = EmployeeService.GetLastMsgCount(base.EmployeeID);
            stringBuilder3 = stringBuilder.AppendFormat("|{0},{1}", unReadMsgCount, lastMsgCount);
            str = stringBuilder.ToString();
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //AjaxPro.Utility.RegisterTypeForAjax(this.GetType());
                AjaxPro.Utility.RegisterTypeForAjax(typeof(DefaultMain));
                this.customScript = base.GetCustomScript("ref_DefaultMain");
                this.InitPage();
            }
        }
    }
}