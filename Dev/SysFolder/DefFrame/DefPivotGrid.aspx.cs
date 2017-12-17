using AjaxPro;
using EIS.AppBase;
using EIS.DataAccess;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class DefPivotGrid : AdminPageBase
    {
        

        public StringBuilder sbFieldList = new StringBuilder();

        public StringBuilder sbFilterArea = new StringBuilder();

        public StringBuilder sbRowArea = new StringBuilder();

        public StringBuilder sbColumnArea = new StringBuilder();

        public StringBuilder sbDataArea = new StringBuilder();

        public string tblName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string[] strArrays;
            int i;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefPivotGrid));
            StringBuilder stringBuilder = new StringBuilder();
            this.tblName = base.GetParaValue("tblName");
            List<FieldInfo> fields = (new _TableInfo(this.tblName)).GetFields();
            ArrayList arrayLists = new ArrayList();
            string str = string.Concat("select * from  T_E_Sys_PivotGrid where queryCode='", this.tblName, "'");
            DataTable dataTable = SysDatabase.ExecuteTable(str);
            if (dataTable.Rows.Count > 0)
            {
                DataRow item = dataTable.Rows[0];
                string str1 = item["FilterArea"].ToString();
                string str2 = item["RowArea"].ToString();
                string str3 = item["ColumnArea"].ToString();
                string str4 = item["DataArea"].ToString();
                if (str1.Length > 0)
                {
                    strArrays = str1.Split(",".ToCharArray());
                    for (i = 0; i < (int)strArrays.Length; i++)
                    {
                        string str5 = strArrays[i];
                        this.sbFilterArea.AppendFormat("<li class='ui-state-default' id='{0}'>{1}</li>\n", str5, fields.Find((FieldInfo fieldInfo_0) => fieldInfo_0.FieldName == str5).FieldNameCn);
                        arrayLists.Add(str5);
                    }
                }
                if (str2.Length > 0)
                {
                    strArrays = str2.Split(",".ToCharArray());
                    for (i = 0; i < (int)strArrays.Length; i++)
                    {
                        string str6 = strArrays[i];
                        this.sbRowArea.AppendFormat("<li class='ui-state-default' id='{0}'>{1}</li>\n", str6, fields.Find((FieldInfo fieldInfo_0) => fieldInfo_0.FieldName == str6).FieldNameCn);
                        arrayLists.Add(str6);
                    }
                }
                if (str3.Length > 0)
                {
                    strArrays = str3.Split(",".ToCharArray());
                    for (i = 0; i < (int)strArrays.Length; i++)
                    {
                        string str7 = strArrays[i];
                        this.sbColumnArea.AppendFormat("<li class='ui-state-default' id='{0}'>{1}</li>\n", str7, fields.Find((FieldInfo fieldInfo_0) => fieldInfo_0.FieldName == str7).FieldNameCn);
                        arrayLists.Add(str7);
                    }
                }
                if (str4.Length > 0)
                {
                    strArrays = str4.Split(",".ToCharArray());
                    for (i = 0; i < (int)strArrays.Length; i++)
                    {
                        string str8 = strArrays[i];
                        this.sbDataArea.AppendFormat("<li class='ui-state-default' id='{0}'>{1}</li>\n", str8, fields.Find((FieldInfo fieldInfo_0) => fieldInfo_0.FieldName == str8).FieldNameCn);
                        arrayLists.Add(str8);
                    }
                }
            }
            foreach (FieldInfo field in fields)
            {
                if (arrayLists.Contains(field.FieldName))
                {
                    continue;
                }
                this.sbFieldList.AppendFormat("<li class='ui-state-default' id='{0}'>{1}</li>\n", field.FieldName, field.FieldNameCn);
            }
        }

        [AjaxMethod(HttpSessionStateRequirement.Read)]
        public void saveQuery(string tblName, string string_0, string string_1, string string_2, string string_3)
        {
            object[] objArray;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("select * from T_E_Sys_PivotGrid where queryCode='{0}'", tblName);
            DataTable dataTable = SysDatabase.ExecuteTable(stringBuilder.ToString());
            stringBuilder.Length = 0;
            if (dataTable.Rows.Count != 0)
            {
                objArray = new object[] { tblName, string_0, string_1, string_2, string_3 };
                stringBuilder.AppendFormat("update T_E_Sys_PivotGrid set FilterArea='{1}' ,RowArea='{2}',ColumnArea='{3}',DataArea='{4}' where queryCode='{0}'", objArray);
            }
            else
            {
                objArray = new object[] { tblName, string_0, string_1, string_2, string_3 };
                stringBuilder.AppendFormat("insert T_E_Sys_PivotGrid (_AutoID,_UserName,_OrgCode,_CreateTime,_UpdateTime,_IsDel,queryCode ,FilterArea,RowArea,ColumnArea,DataArea) \r\n                    values(newid(),'','',getdate(),getdate(),0,'{0}','{1}','{2}','{3}','{4}' )", objArray);
            }
            SysDatabase.ExecuteNonQuery(stringBuilder.ToString());
        }
    }
}