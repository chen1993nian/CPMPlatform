﻿using EIS.AppBase;
using System;


namespace EIS.Studio.SysFolder.Permission
{
    public partial class DefFunNodeFrame : AdminPageBase
    {
        public string webId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.webId = base.GetParaValue("webId");
        }
    }
}