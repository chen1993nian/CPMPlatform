using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Web.UI.HtmlControls;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefTableStyleList : AdminPageBase
    {
      

        public string othercond = "";

        public string tblName = "";

        public string parent = "";


        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void AddStyle(string string_0)
        {
            TableInfo model = (new _TableInfo(string_0)).GetModel();
            int maxIndex = _TableStyle.GetMaxIndex(string_0) + 1;
            TableStyle tableStyle = new TableStyle()
            {
                _AutoID = Guid.NewGuid().ToString(),
                _OrgCode = base.OrgCode,
                _UserName = base.EmployeeID,
                _CreateTime = DateTime.Now,
                _UpdateTime = DateTime.Now,
                _IsDel = 0,
                _CompanyId = "",
                TableName = string_0,
                StyleIndex = maxIndex,
                StyleName = string.Concat("样式", maxIndex),
                FormHtml = model.FormHtml,
                FormHtml2 = model.FormHtml2,
                PrintHtml = model.PrintHtml,
                DetailHtml = model.DetailHtml
            };
            (new _TableStyle()).Add(tableStyle);
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void DelRecord(string styleId)
        {
            if (styleId != "")
            {
                (new _TableStyle()).Delete(styleId);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefTableStyleList));
            this.tblName = base.GetParaValue("tblName");
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void UpdateName(string styleId, string sName)
        {
            string[] strArrays = new string[] { "Update T_E_Sys_TableStyle set StyleName='", sName, "' where _AutoID='", styleId, "'" };
            SysDatabase.ExecuteNonQuery(string.Concat(strArrays));
        }
    }
}