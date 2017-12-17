using EIS.AppBase;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.Limit
{
    public partial class FunLimitTop : AdminPageBase
    {
    

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["DefaultWebSite"] = this.DropDownList1.SelectedValue;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                foreach (WebCataLog cataLogList in WebCataLogService.GetCataLogList(WebCataLog.BizSystem))
                {
                    ListItem listItem = new ListItem(cataLogList.WebName, cataLogList.WebId);
                    if (this.Session["DefaultWebSite"] != null && listItem.Value == this.Session["DefaultWebSite"].ToString())
                    {
                        listItem.Selected = true;
                    }
                    this.DropDownList1.Items.Add(listItem);
                }
            }
        }
    }
}