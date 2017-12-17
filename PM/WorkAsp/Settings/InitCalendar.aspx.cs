using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.AppModel.Service;
using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.WorkAsp.Settings
{
    public partial class InitCalendar : PageBase
    {
        public string calendarId = "";

        public string MsgInfo = "";


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(this.TextBox1.Text, "[0-2]?\\d:[0-5]\\d"))
            {
                this.MsgInfo = "<div class='ErrorMsg'>上班时间格式不正确,如09:30</div>";
            }
            else if (Regex.IsMatch(this.TextBox2.Text, "[0-2]?\\d:[0-5]\\d"))
            {
                bool[] selected = new bool[] { this.CheckBoxList1.Items[6].Selected, this.CheckBoxList1.Items[0].Selected, this.CheckBoxList1.Items[1].Selected, this.CheckBoxList1.Items[2].Selected, this.CheckBoxList1.Items[3].Selected, this.CheckBoxList1.Items[4].Selected, this.CheckBoxList1.Items[5].Selected };
                bool[] flagArray = selected;
                try
                {
                    AppWorkDayService.InitWorkDay(int.Parse(this.DropDownList1.SelectedValue), this.TextBox1.Text, this.TextBox2.Text, this.TextBox3.Text, this.TextBox4.Text, flagArray, this.calendarId);
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, base.GetType(), "afterSuccess", "afterSuccess();", true);
                }
                catch (Exception exception)
                {
                    this.MsgInfo = "<div class='ErrorMsg'>初始化数据出错</div>";
                }
            }
            else
            {
                this.MsgInfo = "<div class='ErrorMsg'>下班时间格式不正确,如17:30</div>";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int i;
            this.calendarId = base.GetParaValue("calendarId");
            if (!base.IsPostBack)
            {
                for (i = DateTime.Now.Year; i < DateTime.Now.Year + 10; i++)
                {
                    this.DropDownList1.Items.Add(i.ToString());
                }
                this.TextBox1.Text = SysConfig.GetConfig("AM_StartTime", false).ItemValue;
                this.TextBox2.Text = SysConfig.GetConfig("AM_EndTime", false).ItemValue;
                this.TextBox3.Text = SysConfig.GetConfig("PM_StartTime", false).ItemValue;
                this.TextBox4.Text = SysConfig.GetConfig("PM_EndTime", false).ItemValue;
                for (i = 0; i < 5; i++)
                {
                    this.CheckBoxList1.Items[i].Selected = true;
                }
            }
        }
    }
}