using AjaxPro;
using EIS.AppBase;
using EIS.AppModel;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Studio.JZY.SysFolder.DefFrame
{
    public partial class AppQuery2 : PageBase
    {


        public string tblName = "";

        public string condition = "";

        public string sIndex = "";

        public string tblHTML = "";

        public string caption = "";

     

        public string getList()
        {
            ModelBuilder modelBuilder = new ModelBuilder(this)
            {
                Sindex = base.GetParaValue("sindex"),
                DataContolFirst = false,
                ReplaceValue = base.GetParaValue("replacevalue"),
                DefaultValue = base.GetParaValue("defaultvalue")
            };
            string paraValue = base.GetParaValue("p");
            paraValue = (paraValue == "" ? "10" : paraValue);
            string str = base.GetParaValue("i");
            str = (str == "" ? "0" : str);
            string str1 = EIS.AppBase.Utility.ReplacePara(base.DecodedPara, "i", "{i}");
            string rawUrl = base.Request.RawUrl;
            char[] chrArray = new char[] { '?' };
            string str2 = EIS.AppBase.Utility.ReplacePara(rawUrl.Split(chrArray)[1], "i", "{i}");
            str2 = EIS.AppBase.Utility.TrimPara(str2, "para");
            if ((EIS.AppBase.Utility.GetPara(str1, "i") != "" ? false : EIS.AppBase.Utility.GetPara(str2, "i") == ""))
            {
                str2 = (str2.Length <= 1 ? "i={i}" : string.Concat(str2, "&i={i}"));
            }
            string[] path = new string[] { base.Request.Path, "?", str1, "&[UNCRYPT]", str2, "[/UNCRYPT]" };
            string str3 = string.Concat(path);
            path = new string[] { paraValue, "|", str, "|", base.GetParaValue("sortdir") };
            string str4 = string.Concat(path);
            string queryList = modelBuilder.GetQueryList(this.tblName, base.GetParaValue("condition"), str4, str3);
            return queryList;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(AppQuery2));
            StringBuilder stringBuilder = new StringBuilder();
            this.tblName = base.GetParaValue("tblName");
            _TableInfo __TableInfo = new _TableInfo(this.tblName);
            this.caption = __TableInfo.GetModel().TableNameCn;
            this.sIndex = base.GetParaValue("sindex");
            this.tblHTML = this.getList();
        }
    }
}