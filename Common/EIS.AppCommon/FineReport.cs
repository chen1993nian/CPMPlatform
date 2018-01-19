using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace EIS.AppCommon
{
    public class FineReport : IHttpModule, IRequiresSessionState
    {
        public void Dispose() { }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication httpApp = (HttpApplication)sender;
        }

    }
}
