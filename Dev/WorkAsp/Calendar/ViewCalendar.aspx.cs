using EIS.AppBase;
using EIS.DataAccess;
using EIS.Web.ModelLib.Model;
using EIS.Web.ModelLib.Service;
using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.WorkAsp.Calendar
{
    public partial class ViewCalendar : PageBase
    {
        public string Id = "";

        public string string_0 = "";



        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime startTime;
            this.Id = base.Request["Id"];
            if (!base.IsPostBack)
            {
                foreach (DataRow row in SysDatabase.ExecuteTable("select ItemCode,ItemName from T_E_Sys_DictEntry where DictID='3ccf63f7-bc91-48b9-b34e-2da0967fb7d0' order by  Itemorder").Rows)
                {
                    ListItem listItem = new ListItem(row["ItemName"].ToString(), row["ItemCode"].ToString());
                    this.rdCategory.Items.Add(listItem);
                }
                if (this.rdCategory.Items.Count > 0)
                {
                    this.rdCategory.SelectedIndex = 0;
                }
                EIS.Web.ModelLib.Model.Calendar calendar = CalendarService.GetCalendar(this.Id);
                if (calendar != null)
                {
                    this.Subject.Text = calendar.Subject;
                    this.colorvalue.Value = calendar.Category;
                    if (calendar.Category != "")
                    {
                        this.rdCategory.SelectedValue = calendar.Category;
                    }
                    this.IsAllDayEvent.Checked = calendar.IsAllDayEvent == 1;
                    if (!this.IsAllDayEvent.Checked)
                    {
                        TextBox str = this.stpartdate;
                        startTime = calendar.StartTime;
                        str.Text = startTime.ToString("yyyy-MM-dd HH:ss");
                        TextBox textBox = this.etpartdate;
                        startTime = calendar.EndTime;
                        textBox.Text = startTime.ToString("yyyy-MM-dd HH:ss");
                    }
                    else
                    {
                        TextBox str1 = this.stpartdate;
                        startTime = calendar.StartTime;
                        str1.Text = startTime.ToString("yyyy-MM-dd");
                        TextBox textBox1 = this.etpartdate;
                        startTime = calendar.EndTime;
                        textBox1.Text = startTime.ToString("yyyy-MM-dd");
                    }
                    this.Location.Text = calendar.Location;
                    this.Description.Text = calendar.Description;
                }
            }
        }
    }
}