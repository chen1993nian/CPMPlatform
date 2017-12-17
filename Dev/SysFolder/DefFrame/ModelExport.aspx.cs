using EIS.AppBase;
using System;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class ModelExport : AdminPageBase
    {
        public StringBuilder tblHtml = new StringBuilder();

        public string view = "display:none;";

        public StringBuilder TipMessage = new StringBuilder();

   

        protected void Page_Load(object sender, EventArgs e)
        {
            string paraValue = base.GetParaValue("tblName");
            string[] strArrays = paraValue.Split(new char[] { ',' });
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                string str = strArrays[i];
                StringBuilder stringBuilder = this.tblHtml;
                object[] objArray = new object[] { string.Concat("<input type='checkbox' class='chkTbl' checked  value='", str, "' />"), str, string.Concat("<input type='checkbox' class='chkDict' checked  value='", str, "' />"), string.Concat("<input type='checkbox' class='chkData' value='", str, "' />") };
                stringBuilder.AppendFormat("<tr><td class='center'>{0}</td><td>{1}</td><td class='center'>{2}</td><td class='center'>{3}</td></tr>", objArray);
            }
        }
    }
}