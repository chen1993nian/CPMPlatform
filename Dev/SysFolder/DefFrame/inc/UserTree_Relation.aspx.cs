using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EIS.DataModel.Access;
using System.Text;
using EIS.DataAccess;
using System.Collections.Specialized;
using EIS.DataModel.Model;
using System.Collections.Generic;
using EIS.AppBase;

namespace EIS.Studio.SysFolder.DefFrame.inc
{
    public partial class UserTree_Relation : PageBase
    {
        public StringBuilder fieldlist1 = new StringBuilder();
        public StringBuilder fieldlist2 = new StringBuilder();
        public StringBuilder fieldlist1in = new StringBuilder();
        public StringBuilder fieldlist2in = new StringBuilder();
        public string key = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            key = GetParaValue("key");
            string tblname = GetParaValue("tblname");
            _TableInfo tblinfo = new _TableInfo(tblname);
            List<FieldInfo> fieldList = tblinfo.GetPhyFields();

            StringCollection arrInfo = new StringCollection();
            arrInfo.Add("empid|员工ID");
            arrInfo.Add("empcode|员工编号");
            arrInfo.Add("empname|员工姓名");
            arrInfo.Add("email|电子邮件");
            arrInfo.Add("ophone|办公电话");
            arrInfo.Add("mphone|移动电话");
            arrInfo.Add("posid|岗位ID");
            arrInfo.Add("posname|岗位名称");
            arrInfo.Add("deptcode|部门编码");
            arrInfo.Add("deptname|部门名称");

            arrInfo.Add("deptcodex|一级部门编码");
            arrInfo.Add("deptnamex|一级部门名称");

            arrInfo.Add("compcode|公司编码");
            arrInfo.Add("compname|公司名称");

            StringDictionary userfields = new StringDictionary();

            foreach (string item in arrInfo)
            {
                userfields.Add(item.Split('|')[0], item.Split('|')[1]);
            }

            ArrayList al1 = new ArrayList();
            ArrayList al2 = new ArrayList();

            string strcmd = string.Format(@"select isnull(FieldInDispStyle,'') style, isnull(FieldInDispStyleTxt,'') txt from  T_E_Sys_FieldInfo where _AutoID='{0}'
                union select isnull(FieldInDispStyle,'') style, isnull(FieldInDispStyleTxt,'') txt from  T_E_Sys_FieldStyle where _AutoID='{0}'"
                , base.GetParaValue("fieldid")
                );

            DataRow style = SysDatabase.ExecuteTable(strcmd).Rows[0];

            if (style[0].ToString() == "033")
            {
                string[] arr = style[1].ToString().Split("|".ToCharArray());

                foreach (string f1 in arr[1].Split(",".ToCharArray()))
                {
                    fieldlist1in.AppendFormat("<li class='ui-state-default' id='{0}'>{1}</li>\n"
                        , f1
                        , fieldList.Find(f => f.FieldName == f1).FieldNameCn
                        );
                    al1.Add(f1);
                }

                foreach (string f2 in arr[2].Split(",".ToCharArray()))
                {
                    fieldlist2in.AppendFormat("<li class='ui-state-default' id='{0}'>{1}</li>\n"
                        , f2
                        , userfields[f2]
                        );
                    al2.Add(f2);
                }
            }


            foreach (FieldInfo r in fieldList)
            {
                if (al1.Contains(r.FieldName)) continue;

                fieldlist1.AppendFormat("<li class='ui-state-default' id='{0}'>{1}</li>\n"
                    , r.FieldName
                    , r.FieldNameCn
                    );
            }
            foreach (string item in arrInfo)
            {
                string field = item.Split('|')[0];
                if (al2.Contains(field)) continue;

                fieldlist2.AppendFormat("<li class='ui-state-highlight' id='{0}'>{1}</li>\n"
                    , field
                    , userfields[field]
                    );
            }
        }
    }
}
