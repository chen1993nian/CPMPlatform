﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EIS.AppBase;
namespace EIS.WebBase.SysFolder.WorkFlow
{
    public partial class FlowMaintain :PageBase
    {
        public string condition = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.condition = base.GetParaValue("condition");
        }
    }
}