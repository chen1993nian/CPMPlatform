using EIS.AppBase;
using EIS.DataAccess;
using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace EIS.Web.WorkAsp.Meeting
{
    public partial class ViewMeeting : PageBase
    {
   

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = this.DropDownList1.SelectedValue;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                foreach (DataRow row in SysDatabase.ExecuteTable("select * from T_OA_HYS_Info where HysState='可用' order by OrderId,HysName").Rows)
                {
                    string str = row["HysName"].ToString();
                    if (string.IsNullOrEmpty(base.Request["HysName"]))
                    {
                        this.DropDownList1.Items.Add(str);
                    }
                    else if (base.Request["HysName"] != str)
                    {
                        this.DropDownList1.Items.Add(str);
                    }
                    else
                    {
                        ListItemCollection items = this.DropDownList1.Items;
                        ListItem listItem = new ListItem()
                        {
                            Text = str,
                            Selected = true
                        };
                        items.Add(listItem);
                    }
                }
            }
        }
    }
}