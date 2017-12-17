using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefTableRelation : AdminPageBase
    {
        public string tblName = "";

        public string string_0 = "";

        public StringBuilder tblList = new StringBuilder();


        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void AddRelation(string mainTbl, string subTbl)
        {
            if (SysDatabase.ExecuteScalar(string.Format("select count(*) from T_E_Sys_Relation where mainTable='{0}' and subTable='{1}'", mainTbl, subTbl)).ToString() == "0")
            {
                string str = string.Format("insert T_E_Sys_Relation (mainTable,mainKey,subTable,subKey,tableName) values (\r\n                '{0}','_AutoID','{1}','_MainID','{0}');\r\n                IF NOT EXISTS(select * from syscolumns where id = object_id(N'{1}') and name = N'_MainID')\r\n                ALTER TABLE {1} ADD _MainID varchar(50) NULL", mainTbl, subTbl, mainTbl);
                SysDatabase.ExecuteNonQuery(str);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefTableRelation));
            if (!IsPostBack)
            {
                this.tblName = base.GetParaValue("tblName");
            }
           
        }
    }
}