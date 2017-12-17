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
            arrInfo.Add("empid|Ա��ID");
            arrInfo.Add("empcode|Ա�����");
            arrInfo.Add("empname|Ա������");
            arrInfo.Add("email|�����ʼ�");
            arrInfo.Add("ophone|�칫�绰");
            arrInfo.Add("mphone|�ƶ��绰");
            arrInfo.Add("posid|��λID");
            arrInfo.Add("posname|��λ����");
            arrInfo.Add("deptcode|���ű���");
            arrInfo.Add("deptname|��������");

            arrInfo.Add("deptcodex|һ�����ű���");
            arrInfo.Add("deptnamex|һ����������");

            arrInfo.Add("compcode|��˾����");
            arrInfo.Add("compname|��˾����");

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
