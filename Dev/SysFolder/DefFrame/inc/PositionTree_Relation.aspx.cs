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
    public partial class PositionTree_Relation : PageBase
    {
        public StringBuilder fieldlist1 = new StringBuilder();
        public StringBuilder fieldlist2 = new StringBuilder();
        public StringBuilder fieldlist1in = new StringBuilder();
        public StringBuilder fieldlist2in = new StringBuilder();
        public string key = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            string tblname = GetParaValue("tblname");
            key = GetParaValue("key");
            _TableInfo tblinfo = new _TableInfo(tblname);
            List<FieldInfo> fieldList = tblinfo.GetPhyFields();

            StringDictionary userfields=new StringDictionary();

            userfields.Add("PositionId", "岗位ID");
            userfields.Add("PositionName", "岗位名称");
            userfields.Add("DeptId", "部门ID");
            userfields.Add("DeptName", "部门名称");

            ArrayList al1 = new ArrayList();
            ArrayList al2 = new ArrayList();

            string strcmd = string.Format(@"select isnull(FieldInDispStyle,'') style, isnull(FieldInDispStyleTxt,'') txt from  T_E_Sys_FieldInfo where _AutoID='{0}'
                union select isnull(FieldInDispStyle,'') style, isnull(FieldInDispStyleTxt,'') txt from  T_E_Sys_FieldStyle where _AutoID='{0}'"
                , base.GetParaValue("fieldid")
                );

            DataRow style = SysDatabase.ExecuteTable(strcmd).Rows[0];

            if (style[0].ToString() == "034")
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
            foreach (string field in userfields.Keys)
            {
                if (al2.Contains(field)) continue;
                fieldlist2.AppendFormat("<li class='ui-state-highlight' id='{0}'>{1}</li>\n"
                    , field
                    , userfields[field]
                    );
            }
        }
    }
}
