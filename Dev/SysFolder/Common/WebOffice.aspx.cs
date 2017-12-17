using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.WebBase.SysFolder.Common
{
    public partial class WebOffice : PageBase
    {
        public string fileid = "";

        public string filename = "";

        public string foldername = "";

        public string newofficetype = "";

       


        public void getBookMark()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if ((this.foldername != "" ? true : this.foldername != null))
            {
                string str = string.Concat("select BookMark,TblName,FldName from T_OA_GW_BookMark where TblName='", this.foldername, "'");
                foreach (DataRow row in SysDatabase.ExecuteTable(str).Rows)
                {
                    stringBuilder.AppendFormat("{0}${1}${2}|", row["BookMark"], row["TblName"], row["FldName"]);
                }
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Length = stringBuilder.Length - 1;
                    this.jstxt.Value = stringBuilder.ToString();
                }
            }
        }

        public void getYzth()
        {
            int i;
            DataRow row = null;
            string str;
            AppFile lastFileByAppId;
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder length = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder();
            string str1 = string.Concat("select * from T_OA_GW_DZYZ where companyId='", base.CompanyId, "'");
            DataTable dataTable = SysDatabase.ExecuteTable(str1);
            foreach (DataRow rowA in dataTable.Rows)
            {
                str = rowA["FileId"].ToString();
                lastFileByAppId = FileService.GetLastFileByAppId(str);
                if (lastFileByAppId == null)
                {
                    continue;
                }
                stringBuilder.AppendFormat("{0},{1}|", row["YzName"], lastFileByAppId._AutoID);
            }
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Length = stringBuilder.Length - 1;
                this.hiddenYz.Value = string.Concat(dataTable.Rows.Count, "$", stringBuilder.ToString());
            }
            str1 = string.Concat("select * from T_OA_GW_THMB where companyId='", base.CompanyId, "' and IsEnabled='启用'");
            dataTable = SysDatabase.ExecuteTable(str1);
            DataRow[] dataRowArray = dataTable.Select("ThOrMb='套红'");
            for (i = 0; i < (int)dataRowArray.Length; i++)
            {
                row = dataRowArray[i];
                str = row["FileId"].ToString();
                lastFileByAppId = FileService.GetLastFileByAppId(str);
                if (lastFileByAppId != null)
                {
                    length.AppendFormat("{0},{1}|", row["FileName"], lastFileByAppId._AutoID);
                }
            }
            if (length.Length > 0)
            {
                length.Length = length.Length - 1;
                this.hiddenTh.Value = string.Concat((int)dataTable.Select("ThOrMb='套红'").Length, "$", length.ToString());
            }
            dataRowArray = dataTable.Select("ThOrMb='模板'");
            for (i = 0; i < (int)dataRowArray.Length; i++)
            {
                row = dataRowArray[i];
                str = row["FileId"].ToString();
                lastFileByAppId = FileService.GetLastFileByAppId(str);
                if (lastFileByAppId != null)
                {
                    stringBuilder1.AppendFormat("{0},{1}|", row["FileName"], lastFileByAppId._AutoID);
                }
            }
            if (stringBuilder1.Length > 0)
            {
                stringBuilder1.Length = stringBuilder1.Length - 1;
                this.hiddenMb.Value = string.Concat((int)dataTable.Select("ThOrMb='模板'").Length, "$", stringBuilder1.ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.fileid = base.GetParaValue("fileid");
            AppFile model = (new _AppFile()).GetModel(this.fileid);
            if (model == null)
            {
                string paraValue = base.GetParaValue("mburl");
                if (paraValue != "")
                {
                    this.hiddenMbUrl.Value = paraValue;
                    this.hiddenMbLoad.Value = "1";
                }
            }
            else
            {
                this.filename = model.FileName;
            }
            this.foldername = base.GetParaValue("foldername");
            this.newofficetype = base.GetParaValue("newofficetype");
            if (this.newofficetype == "")
            {
                this.newofficetype = "1";
            }
            this.hiddenControl.Value = "1";
            this.hiddenPrint.Value = "1";
            this.hiddenReadonly.Value = base.GetParaValue("read");
            this.hiddenMark.Value = "0";
            this.hiddenCopy.Value = "1";
            this.hiddenFilenew.Value = "1";
            this.hiddenSaveas.Value = "1";
            this.hiddenFullScrn.Value = base.GetParaValue("fullscrn");
            if (base.Request["crt"] != null)
            {
                this.hiddenControl.Value = base.Request["crt"].ToString();
            }
            if (base.Request["print"] != null)
            {
                this.hiddenPrint.Value = base.Request["print"].ToString();
            }
            if (base.Request["mark"] != null)
            {
                this.hiddenMark.Value = base.Request["mark"].ToString();
            }
            if (base.Request["copy"] != null)
            {
                this.hiddenCopy.Value = base.Request["copy"].ToString();
            }
            if (base.Request["filenew"] != null)
            {
                this.hiddenFilenew.Value = base.Request["filenew"].ToString();
            }
            if (base.Request["saveas"] != null)
            {
                this.hiddenSaveas.Value = base.Request["saveas"].ToString();
            }
            if (this.hiddenReadonly.Value == "1")
            {
                this.hiddenControl.Value = "0";
                this.hiddenMark.Value = "0";
                this.hiddenFilenew.Value = "0";
                this.hiddenSaveas.Value = "0";
                this.hiddenCopy.Value = "0";
            }
            this.getYzth();
            this.getBookMark();
        }
    }
}