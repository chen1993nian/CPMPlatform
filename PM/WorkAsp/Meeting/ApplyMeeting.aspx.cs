using EIS.AppBase;
using EIS.AppBase.Config;
using EIS.DataAccess;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Web.WorkAsp.Meeting
{
    public partial class ApplyMeeting : PageBase
    {
        public string view = "week";

        public StringBuilder hysList = new StringBuilder();


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

        public ApplyMeeting()
        {
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = this.DropDownList1.SelectedValue;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DataRow row = null;
            string str;
            string paraValue = base.GetParaValue("hysId");
            this.view = base.GetParaValue("view");
            if (this.view == "")
            {
                this.view = "week";
            }
            if (!base.IsPostBack)
            {
                string itemValue = SysConfig.GetConfig("HY_AheadDays", false).ItemValue;
                if (!string.IsNullOrEmpty(itemValue))
                {
                    int num = int.Parse(itemValue);
                    DateTime today = DateTime.Today;
                    today = today.AddDays((double)num);
                    this.AheadDate = today.ToString("yyyy-MM-dd");
                }
                else
                {
                    this.AheadDate = "";
                }
                foreach (DataRow roAw in SysDatabase.ExecuteTable("select * from T_OA_HYS_Info where HysState='可用' order by OrderId, HysName").Rows)
                {
                    str = roAw["HysName"].ToString();
                    if (string.IsNullOrEmpty(paraValue))
                    {
                        this.DropDownList1.Items.Add(str);
                    }
                    else if (paraValue != roAw["_AutoId"].ToString())
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
            foreach (DataRow dataRow in SysDatabase.ExecuteTable("select * from T_OA_HYS_Info where HysState='可用' order by OrderId,HysName").Rows)
            {
                str = dataRow["HysName"].ToString();
                StringBuilder stringBuilder = this.hysList;
                object[] item = new object[] { str, dataRow["HysAddr"], dataRow["Note"], dataRow["HysNum"], dataRow["_AutoId"], dataRow["StartTime"], dataRow["EndTime"] };
                stringBuilder.AppendFormat("{{'HysName':'{0}','HysAddr':'{1}','Note':'{2}','HysNum':'{3}','Id':'{4}',StartTime:'{5}',EndTime:'{6}'}},", item);
            }
            if (this.hysList.Length > 0)
            {
                this.hysList.Length = this.hysList.Length - 1;
            }
        }
    }
}