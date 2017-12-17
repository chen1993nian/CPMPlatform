using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.AppModel.Service;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.WebBase.WorkAsp.Settings
{
    public partial class EditCalendar : PageBase
    {
      
        public string MsgInfo = "";

        public string calendarId = "";

        public string string_0 = "";

        public string times = "";

    

        protected void Button1_Click(object sender, EventArgs e)
        {
            DateTime workDate;
            if (!(this.TextBox1.Text == "" ? true : Regex.IsMatch(this.TextBox1.Text, "[0-2]\\d:[0-5]\\d")))
            {
                this.MsgInfo = "<div class='ErrorMsg'>上班时间格式不正确,如09:30</div>";
            }
            else if ((this.TextBox2.Text == "" ? true : Regex.IsMatch(this.TextBox2.Text, "[0-2]\\d:[0-5]\\d")))
            {
                try
                {
                    _AppWorkDay __AppWorkDay = new _AppWorkDay();
                    List<AppWorkDay> dataByDay = AppWorkDayService.GetDataByDay(DateTime.Parse(this.string_0), this.calendarId);
                    if (dataByDay.Count == 2)
                    {
                        if (this.RadioButtonList1.SelectedValue != "工作日")
                        {
                            dataByDay[0].DayType = this.RadioButtonList1.SelectedValue;
                            dataByDay[0].Holiday = this.HolidayName.Text;
                            dataByDay[1].DayType = this.RadioButtonList1.SelectedValue;
                            dataByDay[1].Holiday = this.HolidayName.Text;
                            dataByDay[0].IsWorkDay = "否";
                            dataByDay[1].IsWorkDay = "否";
                        }
                        else
                        {
                            dataByDay[0].DayType = "工作日";
                            dataByDay[1].DayType = "工作日";
                            dataByDay[0].Holiday = this.HolidayName.Text;
                            dataByDay[1].Holiday = this.HolidayName.Text;
                            if ((this.TextBox1.Text.Trim() == "" ? false : !(this.TextBox2.Text.Trim() == "")))
                            {
                                dataByDay[0].IsWorkDay = "是";
                                AppWorkDay item = dataByDay[0];
                                workDate = dataByDay[0].WorkDate;
                                item.StartTime = Convert.ToDateTime(string.Concat(workDate.ToString("yyyy-MM-dd "), this.TextBox1.Text));
                                AppWorkDay dateTime = dataByDay[0];
                                workDate = dataByDay[0].WorkDate;
                                dateTime.EndTime = Convert.ToDateTime(string.Concat(workDate.ToString("yyyy-MM-dd "), this.TextBox2.Text));
                            }
                            else
                            {
                                dataByDay[0].IsWorkDay = "否";
                            }
                            if ((this.TextBox3.Text.Trim() == "" ? false : !(this.TextBox4.Text.Trim() == "")))
                            {
                                dataByDay[1].IsWorkDay = "是";
                                AppWorkDay appWorkDay = dataByDay[1];
                                workDate = dataByDay[1].WorkDate;
                                appWorkDay.StartTime = Convert.ToDateTime(string.Concat(workDate.ToString("yyyy-MM-dd "), this.TextBox3.Text));
                                AppWorkDay item1 = dataByDay[1];
                                workDate = dataByDay[1].WorkDate;
                                item1.EndTime = Convert.ToDateTime(string.Concat(workDate.ToString("yyyy-MM-dd "), this.TextBox4.Text));
                            }
                            else
                            {
                                dataByDay[1].IsWorkDay = "否";
                            }
                        }
                        __AppWorkDay.Update(dataByDay[0]);
                        __AppWorkDay.Update(dataByDay[1]);
                    }
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

        protected void Button2_Click(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime startTime;
            string str;
            string str1;
            string str2;
            string str3;
            this.calendarId = base.GetParaValue("calId");
            this.string_0 = base.GetParaValue("day");
            this.times = string.Concat(SysConfig.GetConfig("AM_StartTime", false).ItemValue, "|");
            EditCalendar editCalendar = this;
            editCalendar.times = string.Concat(editCalendar.times, SysConfig.GetConfig("AM_EndTime", false).ItemValue, "|");
            EditCalendar editCalendar1 = this;
            editCalendar1.times = string.Concat(editCalendar1.times, SysConfig.GetConfig("PM_StartTime", false).ItemValue, "|");
            EditCalendar editCalendar2 = this;
            editCalendar2.times = string.Concat(editCalendar2.times, SysConfig.GetConfig("PM_EndTime", false).ItemValue);
            if (!base.IsPostBack)
            {
                _AppWorkDay __AppWorkDay = new _AppWorkDay();
                List<AppWorkDay> dataByDay = AppWorkDayService.GetDataByDay(DateTime.Parse(this.string_0), this.calendarId);
                if (dataByDay.Count != 2)
                {
                    this.MsgInfo = "<div class='ErrorMsg'>recId有误，查询不能对应的日期</div>";
                }
                else
                {
                    if ((dataByDay[0].IsWorkDay == "是" ? false : !(dataByDay[1].IsWorkDay == "是")))
                    {
                        if (dataByDay[0].DayType != "")
                        {
                            this.RadioButtonList1.SelectedValue = dataByDay[0].DayType;
                        }
                        else
                        {
                            this.RadioButtonList1.SelectedValue = "周末";
                        }
                        this.HolidayName.Text = dataByDay[0].Holiday;
                        this.TextBox1.Text = "";
                        this.TextBox2.Text = "";
                        this.TextBox3.Text = "";
                        this.TextBox4.Text = "";
                    }
                    else
                    {
                        if (dataByDay[0].DayType != "")
                        {
                            this.RadioButtonList1.SelectedValue = dataByDay[0].DayType;
                        }
                        else
                        {
                            this.RadioButtonList1.SelectedValue = "工作日";
                        }
                        TextBox textBox1 = this.TextBox1;
                        if (dataByDay[0].IsWorkDay == "是")
                        {
                            startTime = dataByDay[0].StartTime;
                            str = startTime.ToString("HH:mm");
                        }
                        else
                        {
                            str = "";
                        }
                        textBox1.Text = str;
                        TextBox textBox2 = this.TextBox2;
                        if (dataByDay[0].IsWorkDay == "是")
                        {
                            startTime = dataByDay[0].EndTime;
                            str1 = startTime.ToString("HH:mm");
                        }
                        else
                        {
                            str1 = "";
                        }
                        textBox2.Text = str1;
                        TextBox textBox3 = this.TextBox3;
                        if (dataByDay[1].IsWorkDay == "是")
                        {
                            startTime = dataByDay[1].StartTime;
                            str2 = startTime.ToString("HH:mm");
                        }
                        else
                        {
                            str2 = "";
                        }
                        textBox3.Text = str2;
                        TextBox textBox4 = this.TextBox4;
                        if (dataByDay[1].IsWorkDay == "是")
                        {
                            startTime = dataByDay[1].EndTime;
                            str3 = startTime.ToString("HH:mm");
                        }
                        else
                        {
                            str3 = "";
                        }
                        textBox4.Text = str3;
                    }
                    this.EditDate.Text = this.string_0;
                }
            }
        }
    }
}