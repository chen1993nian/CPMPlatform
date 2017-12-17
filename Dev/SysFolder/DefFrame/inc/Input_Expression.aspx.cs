using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System.Data;

namespace EIS.Studio.SysFolder.DefFrame.inc
{
    public partial class Input_Expression : PageBase
    {
        public string t = "1";
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Input_Expression));
            string tblName = GetParaValue("tblname");
            string fieldId= GetParaValue("fieldId");
            _TableInfo dalTbl = new _TableInfo(tblName);
            TableInfo model = dalTbl.GetModel();
            t = model.TableType.ToString();
            if (model.TableType == 1) { 
                RadioButtonList1.Items.Add(new ListItem("主表字段表达式","1"));
                RadioButtonList1.Items.Add(new ListItem("合计子表表达式","2"));
                List<TableInfo> dt = dalTbl.GetSubTable();
                if(dt.Count>0){
                    DropDownList1.Enabled=true;
                    DropDownList1.DataSource = dt;
                    DropDownList1.DataTextField = "TableNameCn";
                    DropDownList1.DataValueField = "TableName";
                    DropDownList1.DataBind();

                }
            }
            else if (model.TableType == 2) { 
            }
        }


        [AjaxPro.AjaxMethod()]
        public string[] GetFields(string tblName)
        {
            _TableInfo dalTbl = new _TableInfo(tblName);
            List<FieldInfo> list= dalTbl.GetFields();

            string[] arr = new string[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                arr[i] = list[i].FieldName + "|" + list[i].FieldNameCn;
            }

            return arr;
        }



    }
}