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
using EIS.DataModel.Model;
using System.Collections.Generic;
using EIS.AppBase;
namespace EIS.Studio.SysFolder.DefFrame
{
    public partial class Input_Outlist_Relation : PageBase
    {
        public StringBuilder fieldlist1 = new StringBuilder();
        public StringBuilder fieldlist2 = new StringBuilder();
        public StringBuilder fieldlist1in = new StringBuilder();
        public StringBuilder fieldlist2in = new StringBuilder();
        public string fieldId = "", queryId = "", query = "", queryName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            fieldId = GetParaValue("fieldid");

            ArrayList al1 = new ArrayList();
            ArrayList al2 = new ArrayList();

            string strcmd = string.Format(@"select TableName, isnull(FieldInDispStyle,'') style, isnull(FieldInDispStyleTxt,'') txt from  T_E_Sys_FieldInfo where _AutoID='{0}'
                union select TableName, isnull(FieldInDispStyle,'') style, isnull(FieldInDispStyleTxt,'') txt from  T_E_Sys_FieldStyle where _AutoID='{0}'"
                , fieldId
                );

            DataRow style = SysDatabase.ExecuteTable(strcmd).Rows[0];

            string tblname = style["TableName"].ToString();

            _TableInfo tblinfo = new _TableInfo(tblname);
            List<FieldInfo> dt = tblinfo.GetPhyFields();
            List<FieldInfo> qy = new List<FieldInfo>();

            queryId = GetParaValue("queryId");
            if (string.IsNullOrEmpty(queryId))
            {
                if (style["style"].ToString() == "032")
                {
                    string[] arr = style["txt"].ToString().Split("|".ToCharArray());
                    queryId = arr[0];
                    TableInfo queryModel = new _TableInfo().GetModelById(queryId);
                    query = queryModel.TableName;
                    queryName = queryModel.TableNameCn;
                    qy = new _TableInfo().GetFieldsByTableId(queryId);

                    foreach (string f1 in arr[1].Split(",".ToCharArray()))
                    {
                        fieldlist1in.AppendFormat("<li class='ui-state-default' id='{0}'>{1}</li>\n"
                            , f1
                            , dt.Find(f => f.FieldName == f1).FieldNameCn
                            );
                        al1.Add(f1);
                    }

                    foreach (string f2 in arr[2].Split(",".ToCharArray()))
                    {
                        if (qy.FindIndex(f => f.FieldName == f2) > -1)
                        {
                            fieldlist2in.AppendFormat("<li class='ui-state-default' id='{0}'>{1}</li>\n"
                                , f2
                                , qy.Find(f => f.FieldName == f2).FieldNameCn
                                );
                            al2.Add(f2);
                        }
                    }
                    if (arr.Length > 3)
                    {
                        txtWhere.Text = arr[3];
                    }


                }
                else
                {
                    Response.Redirect("Input_OutList.aspx?key=032&fieldId=" + fieldId);
                }
            }
            else
            {
                //设置对应关系
                TableInfo queryModel = new _TableInfo().GetModelById(queryId);
                query = queryModel.TableName;
                queryName = queryModel.TableNameCn;
                qy = new _TableInfo().GetFieldsByTableId(queryId);

                if (style["style"].ToString() == "032")
                {
                    string[] arr = style["txt"].ToString().Split("|".ToCharArray());
                    if (queryId == arr[0])
                    {
                        foreach (string f1 in arr[1].Split(",".ToCharArray()))
                        {
                            fieldlist1in.AppendFormat("<li class='ui-state-default' id='{0}'>{1}</li>\n"
                                , f1
                                , dt.Find(f => f.FieldName == f1).FieldNameCn
                                );
                            al1.Add(f1);
                        }

                        foreach (string f2 in arr[2].Split(",".ToCharArray()))
                        {
                            if (qy.FindIndex(f => f.FieldName == f2) > -1)
                            {
                                fieldlist2in.AppendFormat("<li class='ui-state-default' id='{0}'>{1}</li>\n"
                                    , f2
                                    , qy.Find(f => f.FieldName == f2).FieldNameCn
                                    );
                                al2.Add(f2);
                            }
                        }
                    }


                }

            }

            foreach (FieldInfo r in dt)
            {
                if (al1.Contains(r.FieldName)) continue;

                fieldlist1.AppendFormat("<li class='ui-state-default' id='{0}'>{1}</li>\n"
                    , r.FieldName
                    , r.FieldNameCn
                    );
            }
            foreach (FieldInfo r in qy)
            {
                if (al2.Contains(r.FieldName)) continue;
                fieldlist2.AppendFormat("<li class='ui-state-highlight' id='{0}'>{1}</li>\n"
                    , r.FieldName
                    , r.FieldNameCn
                    );
            }
        }
    }
}