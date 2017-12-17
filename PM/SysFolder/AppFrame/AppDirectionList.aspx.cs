using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;

namespace EIS.Web.SysFolder.AppFrame
{
    public partial class AppDirectionList : PageBase
    {
       public string TblNameCn = "";

        public string UpdateTime = "";

        public string Infos = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string paraValue = base.GetParaValue("tblName");
            base.GetParaValue("sIndex");
            TableDoc modelBytableName = (new _TableDoc()).GetModelBytableName(paraValue);
            if (modelBytableName == null)
            {
                this.Infos = "暂无设计说明";
            }
            else
            {
                this.Infos = modelBytableName.DesignDoc;
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