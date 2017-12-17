using AjaxPro;
using EIS.AppBase;
using EIS.AppEngine;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefBizCopy : AdminPageBase
    {
        public string tblName = "";

        public string string_0 = "";

        public StringBuilder tblList = new StringBuilder();


  
        protected void Button1_Click(object sender, EventArgs e)
        {
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void CopyTables(string tableDefList)
        {
            string[] strArrays = tableDefList.Split(new char[] { '|' });
            StringCollection stringCollections = new StringCollection();
            stringCollections.AddRange(strArrays);
            ModelCopy.CopyTables(stringCollections, base.UserInfo);
        }

        private void method_0()
        {
            _TableInfo __TableInfo = new _TableInfo(this.tblName);
            TableInfo model = __TableInfo.GetModel();
            this.string_0 = model.TableType.ToString();
            if (model != null)
            {
                int num = 1;
                StringBuilder stringBuilder = this.tblList;
                object[] tableName = new object[6];
                int num1 = 1;
                num = num1 + 1;
                tableName[0] = num1;
                tableName[1] = model.TableName;
                tableName[2] = model.TableNameCn;
                tableName[3] = "主表";
                tableName[4] = "<input type='text' name='mainName' id='mainName' class='TextBoxInChar'/>";
                tableName[5] = "<input type='text' name='mainCn' id='mainCn' class='TextBoxInChar'/>";
                stringBuilder.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>", tableName);
                foreach (TableInfo subTable in __TableInfo.GetSubTable())
                {
                    StringBuilder stringBuilder1 = this.tblList;
                    tableName = new object[6];
                    int num2 = num;
                    num = num2 + 1;
                    tableName[0] = num2;
                    tableName[1] = subTable.TableName;
                    tableName[2] = subTable.TableNameCn;
                    tableName[3] = "子表";
                    tableName[4] = string.Format("<input type='text' class='TextBoxInChar subName' id='subName{0}' srcTbl='{1}'/>", num, subTable.TableName);
                    tableName[5] = string.Format("<input type='text' id='subName{0}_cn' class='TextBoxInChar'/>", num);
                    stringBuilder1.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>", tableName);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefBizCopy));
            this.tblName = base.GetParaValue("tblName");
            if (!base.IsPostBack)
            {
                this.method_0();
            }
        }
    }
}