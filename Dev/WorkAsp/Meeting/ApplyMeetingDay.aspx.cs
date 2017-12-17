using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.DataAccess;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI;


namespace EIS.Web.WorkAsp.Meeting
{
    public partial class ApplyMeetingDay : PageBase
    {
        public StringBuilder bodyHtml = new StringBuilder();

        public StringBuilder hysList = new StringBuilder();

        public string ShowDate = "";

        public string AheadDate
        {
            get
            {
                string str;
                str = (this.ViewState["AheadDate"] == null ? "" : this.ViewState["AheadDate"].ToString());
                return str;
            }
            set
            {
                this.ViewState["AheadDate"] = value;
            }
        }

       

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            if (base.GetParaValue("date") != "")
            {
                today = Convert.ToDateTime(base.GetParaValue("date"));
            }
            this.ShowDate = today.ToString("yyyy-M-d dddd");
            foreach (DataRow row in SysDatabase.ExecuteTable("select * from T_OA_HYS_Info where HysState='可用' order by OrderId,HysName").Rows)
            {
                string str = row["HysName"].ToString();
                StringBuilder stringBuilder = this.hysList;
                object[] item = new object[] { str, row["HysAddr"], row["Note"], row["HysNum"], row["_AutoId"], row["StartTime"], row["EndTime"] };
                stringBuilder.AppendFormat("{{'HysName':'{0}','HysAddr':'{1}','Note':'{2}','HysNum':'{3}','Id':'{4}',StartTime:'{5}',EndTime:'{6}'}},", item);
            }
            string itemValue = SysConfig.GetConfig("HY_AheadDays", false).ItemValue;
            if (!string.IsNullOrEmpty(itemValue))
            {
                int num = int.Parse(itemValue);
                DateTime dateTime = DateTime.Today;
                dateTime = dateTime.AddDays((double)num);
                this.AheadDate = dateTime.ToString("yyyy-MM-dd");
            }
            else
            {
                this.AheadDate = "";
            }
            if (this.hysList.Length > 0)
            {
                this.hysList.Length = this.hysList.Length - 1;
            }
        }
    }
}