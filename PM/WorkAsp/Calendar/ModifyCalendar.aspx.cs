using EIS.AppBase;
using EIS.DataAccess;
using EIS.Permission.Service;
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
    public partial class ModifyCalendar : PageBase
    {
     

        public string Id = "";

        public string msg = "";

        private string string_1 = "";

       

        protected void Button1_Click(object sender, EventArgs e)
        {
            JsonReturnMessages jsonReturnMessage = this.method_0();
            this.msg = string.Format("={{IsSuccess:{0},Msg:'{1}'}}", (jsonReturnMessage.IsSuccess ? "true" : "false"), jsonReturnMessage.Msg);
            base.ClientScript.RegisterStartupScript(base.GetType(), "", "success();", true);
        }

        private JsonReturnMessages method_0()
        {
            EIS.Web.ModelLib.Model.Calendar text;
            DateTime dateTime;
            JsonReturnMessages jsonReturnMessage = new JsonReturnMessages();
            try
            {
                text = (this.Id.Length <= 0 ? new EIS.Web.ModelLib.Model.Calendar(base.UserInfo)
                {
                    EmpId = this.string_1,
                    EmpName = EmployeeService.GetEmployeeName(this.string_1)
                } : CalendarService.GetCalendar(this.Id));
                text.Subject = this.Subject.Text;
                text.Location = this.Location.Text;
                text.Description = this.Description.Text;
                text.IsAllDayEvent = (this.IsAllDayEvent.Checked ? 1 : 0);
                text.Category = this.rdCategory.SelectedValue;
                text.CategoryName = this.rdCategory.SelectedItem.Text;
                string str = this.stpartdate.Text;
                string text1 = this.etpartdate.Text;
                int num = Convert.ToInt32(base.Request["timezone"]);
                int timeZone = TimeHelper.GetTimeZone() - num;
                if (text.IsAllDayEvent != 1)
                {
                    dateTime = Convert.ToDateTime(str);
                    text.StartTime = dateTime.AddHours((double)timeZone);
                    dateTime = Convert.ToDateTime(text1);
                    text.EndTime = dateTime.AddHours((double)timeZone);
                }
                else
                {
                    dateTime = Convert.ToDateTime(str);
                    text.StartTime = dateTime.AddHours((double)timeZone);
                    dateTime = Convert.ToDateTime(text1);
                    dateTime = dateTime.AddHours(23);
                    dateTime = dateTime.AddMinutes(59);
                    dateTime = dateTime.AddSeconds(59);
                    text.EndTime = dateTime.AddHours((double)timeZone);
                }
                if (text.EndTime <= text.StartTime)
                {
                    throw new Exception("结束日期小于开始日期");
                }
                text.CalendarType = 1;
                text.InstanceType = 0;
                text.MasterId = new int?(num);
                if (this.Id.Length <= 0)
                {
                    CalendarService.AddCalendar(text);
                }
                else
                {
                    CalendarService.UpdateCalendar(text);
                }
                jsonReturnMessage.IsSuccess = true;
                jsonReturnMessage.Msg = "操作成功！";
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                jsonReturnMessage.IsSuccess = false;
                jsonReturnMessage.Msg = exception.Message;
            }
            return jsonReturnMessage;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime startTime;
            this.Id = (base.Request["Id"] == "0" ? "" : base.Request["Id"]);
            this.string_1 = (string.IsNullOrEmpty(base.Request["employeeId"]) ? base.EmployeeID : base.Request["employeeId"]);
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
                if (!string.IsNullOrEmpty(this.Id))
                {
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
                else
                {
                    this.Subject.Text = base.Request["title"];
                    this.stpartdate.Text = base.Request["start"];
                    this.etpartdate.Text = base.Request["end"];
                    this.IsAllDayEvent.Checked = base.Request["isallday"] == "true";
                }
            }
        }
    }
}