using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Studio.JZY
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {







            //HttpContext.Current.Session["UserName"] = "superadmin";
            //HttpContext.Current.Session["LoginType"] = "0";
            //HttpContext.Current.Session["CompanyId"] = "";
            //HttpContext.Current.Session["CompanyCode"] = "";
            //HttpContext.Current.Session["CompanyWbs"] = "";
            //HttpContext.Current.Session["CompanyAbbr"] = "";
            //HttpContext.Current.Session["CompanyName"] = "";



            //HttpContext.Current.Session["DeptId"] = "";
            //HttpContext.Current.Session["DeptCode"] = "";
            //HttpContext.Current.Session["DeptName"] = "";
            //HttpContext.Current.Session["DeptWbs"] = "";
            //HttpContext.Current.Session["TopDeptId"] = "";
            //HttpContext.Current.Session["TopDeptCode"] = "";
            //HttpContext.Current.Session["TopDeptName"] = "";
            //HttpContext.Current.Session["TopDeptWbs"] = "";
            //HttpContext.Current.Session["EmployeeId"] = "8F2D3994-6582-47EA-B362-FA495D28DDC9";
            //HttpContext.Current.Session["EmployeeCode"] = "";
            //HttpContext.Current.Session["EmployeeName"] = "superadmin";
            //HttpContext.Current.Session["PositionId"] = "";
            //HttpContext.Current.Session["PositionName"] = "";
            //HttpContext.Current.Session["webId"] = "0";


            //FormsAuthentication.RedirectFromLoginPage("superadmin", true);
            







        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}