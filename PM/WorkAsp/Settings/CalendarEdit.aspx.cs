using EIS.AppBase;
using EIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.WorkAsp.Settings
{
    public partial class CalendarEdit : PageBase
    {

        public string calendarId = "";

        public StringBuilder MsgInfo = new StringBuilder();

        protected void Button1_Click(object sender, EventArgs e)
        {
            float single = 8f;
            int num = 0;
            if (!float.TryParse(this.txtHours.Text.Trim(), out single))
            {
                this.MsgInfo.Append("<div class=tip>每天工作小时数输入有误！</div>");
            }
            else if (int.TryParse(this.txtOrder.Text.Trim(), out num))
            {
                object[] objArray = new object[] { this.txtName.Text.Trim(), this.txtOrder.Text.Trim(), this.calendarId, this.txtHours.Text.Trim(), this.selTimeZone.SelectedValue };
                SysDatabase.ExecuteNonQuery(string.Format("update T_E_App_Calendar set calendarName='{0}',HoursOneDay={3},TimeZone='{4}',OrderId={1} where _autoId='{2}' ", objArray));
                System.Web.UI.ScriptManager.RegisterStartupScript(this, base.GetType(), "afterSuccess", "afterSuccess();", true);
            }
            else
            {
                this.MsgInfo.Append("<div class=tip>排序字段输入有误！</div>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.calendarId = base.GetParaValue("calendarId");
            if (!base.IsPostBack)
            {
                string str = string.Concat("select * from T_E_App_Calendar where _autoId='", this.calendarId, "'");
                DataTable dataTable = SysDatabase.ExecuteTable(str);
                string id = "";
                if (dataTable.Rows.Count > 0)
                {
                    this.txtName.Text = dataTable.Rows[0]["calendarName"].ToString();
                    this.txtHours.Text = dataTable.Rows[0]["HoursOneDay"].ToString();
                    id = dataTable.Rows[0]["TimeZone"].ToString();
                    this.txtOrder.Text = dataTable.Rows[0]["OrderId"].ToString();
                }
                if (id == "")
                {
                    id = TimeZoneInfo.Local.Id;
                }
                foreach (TimeZoneInfo systemTimeZone in TimeZoneInfo.GetSystemTimeZones())
                {
                    ListItem listItem = new ListItem(systemTimeZone.DisplayName, systemTimeZone.Id);
                    if (systemTimeZone.Id == id)
                    {
                        listItem.Selected = true;
                    }
                    this.selTimeZone.Items.Add(listItem);
                }
            }
        }
    }
}