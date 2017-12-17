using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

using AjaxPro;
using System.Data;
using System.Data.SqlClient;
using EIS.DataAccess;


namespace EIS.Web.Doc
{
    /// <summary>
    /// ChangeProjectListXML 的摘要说明
    /// </summary>
    public class ChangeProjectListXML : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}