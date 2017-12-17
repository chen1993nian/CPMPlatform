using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.AppFrame
{
    public partial class AppDirection : PageBase
    {
      
        public string TblNameCn = "";

        public string UpdateTime = "";

        public string Infos = "";

        public AppDirection()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string paraValue = base.GetParaValue("tblName");
            base.GetParaValue("sIndex");
            TableDoc modelBytableName = (new _TableDoc()).GetModelBytableName(paraValue);
            if (modelBytableName == null)
            {
                this.Infos = "暂无填报说明";
            }
            else
            {
                this.Infos = modelBytableName.ReportDoc;
                this.UpdateTime = modelBytableName._UpdateTime.ToString("yyyy年MM月dd日 HH:mm");
            }
            TableInfo model = (new _TableInfo(paraValue)).GetModel();
            if (model != null)
            {
                this.TblNameCn = model.TableNameCn;
            }
        }
    }
}