using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using EIS.DataAccess;

namespace Studio.JZY.WorkAsp.RelationTree
{
    public partial class RelationDeptTree : EIS.AppBase.PageBase
    {

        public string FunNodeZTree_Script = "";
        public string Relation_url = "";
        StringBuilder sb_zTree = new StringBuilder();
        DataSet treeds = new DataSet();
        DataTable treedt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                FunNodeZTree_Script = "";
                //获取部门集合
                GetDepartmentRoleTreeDataTable();
                //获取关联URL
                GetRelationUrl();

                DataRow[] arrRow;
                string strExpr = "wbs='0'";
                arrRow = treedt.Select(strExpr);
                int arrcount = arrRow.Length;
                //循环所有节点
                if (arrcount > 0)
                {
                    DataRow KmRow = arrRow[0];
                    GetListTree("", arrRow[0]["_autoid"].ToString(),"1");
                }
                else
                {
                    strExpr = "pwbs='0'";
                    arrRow = treedt.Select(strExpr);
                    arrcount = arrRow.Length;
                    if (arrcount > 0)
                    {
                        DataRow KmRow = arrRow[0];
                        GetListTree("0", arrRow[0]["_autoid"].ToString(), "1");
                    }
                }
                this.FunNodeZTree_Script = sb_zTree.ToString();
            }
        }
        /// <summary>
        /// 获取部门集合
        /// </summary>
        private void GetDepartmentRoleTreeDataTable()
        {
            string NotLimit = "0";
            string datalimit_sql = "";
            string Fld_DeptName = "DeptName";

            Fld_DeptName = this.GetParaValue("DeptFldName");
            if (Fld_DeptName == "") Fld_DeptName = "DeptName";
            NotLimit = this.GetParaValue("NotLimit");

            if (NotLimit == "0")
            {
                datalimit_sql = @"select fd.* , fd._autoid as DeptID ,dep.DeptName as CompanyName  
                        ,fd.DeptPWBS as pwbs , fd.DeptWBS as wbs, fd." + Fld_DeptName + @" as caption
                        from T_E_Org_Department  fd  
                        left join T_E_Org_Department dep on fd.CompanyId=dep._AutoID
                        where  fd._IsDel = 0 order by fd.OrderID,fd.DeptPWBS";
            }
            else
            {
                datalimit_sql = @"select fd.* , fd._autoid as DeptID ,dep.DeptName as CompanyName  
                        ,fd.DeptPWBS as pwbs , fd.DeptWBS as wbs, fd." + Fld_DeptName + @" as caption
                        from T_E_Org_Department  fd  
                        left join T_E_Org_Department dep on fd.CompanyId=dep._AutoID
                        where  fd._IsDel = 0 order by fd.OrderID,fd.DeptPWBS";
            }

            System.Data.Common.DbCommand command = SysDatabase.GetSqlStringCommand(datalimit_sql);
            treeds = SysDatabase.ExecuteDataSet(command);
            treedt = treeds.Tables[0];

        }

        /// <summary>
        /// 获取关联URL
        /// </summary>
        private void GetRelationUrl()
        {
            string RelationID = this.GetParaValue("RelationID");
            RelationPageInfo rpage = new RelationPageInfo();
            whRelationPage objR = rpage.GetRelationPageInfoByID(RelationID, this);
            Relation_url = objR.AppUrl1;
        }



        /// <summary>
        /// 递归显示部门
        /// </summary>
        /// <param name="PIDValue">上级部门WBS</param>
        /// <param name="Pautoid">上级部门autoID</param>
        private void GetListTree(string PIDValue, string Pautoid,string dgIndex)
        {
            if (dgIndex.Length > 7) return;
            DataRow[] arrRow;
            string strExpr = "PWBS='" + PIDValue + "'";
            arrRow = treedt.Select(strExpr);
            int arrcount = arrRow.Length;
            //循环所有节点
            for (int m = 0; m < arrcount; m++)
            {
                DataRow KmRow = arrRow[m];

                ////替换Session值
                string url = Relation_url;
                url = this.ReplaceWithDataRow(url, KmRow);

                url = ",url:\"" + url + "\", target:\"RelationMain\"";

                if (FunNodeZTree_Script != "") FunNodeZTree_Script = ",";
                FunNodeZTree_Script += "\t\t\t\n{ id: \"" + KmRow["_AutoID"].ToString() + "\"" +
                    ", pId:\"" + Pautoid + "\"" +
                    ", name: \"" + KmRow["caption"].ToString() + "\"" +
                    ", t: \"" + KmRow["caption"].ToString() + "\"" +
                    ", wbs: \"" + KmRow["WBS"].ToString() + "\"" +
                    ", pwbs: \"" + KmRow["PWBS"].ToString() + "\"" +
                    ", open:" + (KmRow["wbs"].ToString() == "0" ? "true" : "false") +
                    ", icon:\"../../img/treeimages/dept.gif\"" + url;

                FunNodeZTree_Script += "}";
                sb_zTree.Append(FunNodeZTree_Script);

                //如果当前节点有子节点
                int sonnum = treedt.Select("pwbs='" + KmRow["wbs"].ToString() + "'").Length;
                if (sonnum > 0)
                {
                    GetListTree(KmRow["wbs"].ToString(), KmRow["_AutoID"].ToString(), dgIndex+"1");
                }
            }
        }

    }
}