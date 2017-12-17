using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EIS.Web.SysFolder.AppFrame
{
    [Serializable]
    public class AppInputAlertException : Exception
    {
        public AppInputAlertException() { }
 
        public AppInputAlertException(string message)
        : base(message)
        { }

        public AppInputAlertException(string message, Exception inner)
        : base(message, inner)
        { }
    }
}