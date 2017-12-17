using EIS.AppBase;
using EIS.DataAccess;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Studio.JZY.PlugIn
{
    public   partial class FusionChart : PageBase
    {
        public string chartId = "";

        public string flashPath = "";

        public string w = "";

        public string h = "";

        public string query = "";

        public string chartSWF = "";

        public StringBuilder chartJson = new StringBuilder();

   

        protected void Page_Load(object sender, EventArgs e)
        {
            this.chartId = base.GetParaValue("chartId");
            if (!base.IsPostBack)
            {
                string strcmd = string.Concat("select QueryDefine, ChartName,ChartCaption,ChartSWF,OtherSWF,ChartWidth,ChartHeight from T_E_App_FusionChartsXT where chartId='", this.chartId, "'");
                DataTable dt = SysDatabase.ExecuteTable(strcmd);
                if (dt.Rows.Count > 0)
                {
                    this.chartSWF = dt.Rows[0]["ChartSWF"].ToString();
                    string chartList = this.chartSWF;
                    string otherSWF = dt.Rows[0]["OtherSWF"].ToString();
                    if (otherSWF.Length > 0)
                    {
                        chartList = string.Concat(chartList, ",", otherSWF);
                    }
                    chartList = chartList.TrimEnd(new char[] { ',' });
                    string chartIn = Utility.GetSplitQuoteString(chartList, ",");
                    strcmd = string.Concat("select * from T_E_App_FusionChartsXTSWF where swfCode in (", chartIn, ")");
                    DataTable dtTypes = SysDatabase.ExecuteTable(strcmd);
                    foreach (DataRow chart in dtTypes.Rows)
                    {
                        this.chartJson.AppendFormat("{0}|{1}|{2}$", chart["swfCode"], chart["swfFileName"], chart["swfFilePath"]);
                    }
                    this.flashPath = dtTypes.Select(string.Concat("swfCode='", this.chartSWF, "'"))[0]["swfFilePath"].ToString();
                    this.w = dt.Rows[0]["ChartWidth"].ToString();
                    this.h = dt.Rows[0]["ChartHeight"].ToString();
                    this.query = dt.Rows[0]["QueryDefine"].ToString().Replace("\r\n", "$");
                }
            }
        }
    }
}