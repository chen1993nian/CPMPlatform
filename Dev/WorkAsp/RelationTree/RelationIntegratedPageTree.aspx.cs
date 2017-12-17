using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Xml;
using EIS.DataAccess;

namespace Studio.JZY.WorkAsp.RelationTree
{
    public partial class RelationIntegratedPageTree : EIS.AppBase.PageBase
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
                //获取物资类别集合
                GetMaterialsClassifyTreeDataTable();
                //获取关联URL
                GetRelationUrl();

                DataRow[] arrRow;
                string strExpr = "pwbs='0'";
                arrRow = treedt.Select(strExpr);
                int arrcount = arrRow.Length;
                //循环所有节点
                if (arrcount > 0)
                {
                    DataRow KmRow = arrRow[0];
                    GetListTree("0", "0", "1");
                }
                this.FunNodeZTree_Script = sb_zTree.ToString();
            }
        }

        /// <summary>
        /// 获取物资类别集合
        /// </summary>
        private void GetMaterialsClassifyTreeDataTable()
        {
            string datalimit_sql = "";

            datalimit_sql = @"select *  
	                ,classpcode as pwbs , classcode as wbs, classname as caption
	                from T_E_App_IntegratedPageInfo  order by classpcode,orderid,classcode";

            System.Data.Common.DbCommand comm = SysDatabase.GetSqlStringCommand(datalimit_sql);
            treeds = SysDatabase.ExecuteDataSet(comm);
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
        private void GetListTree(string PIDValue, string Pautoid, string dgIndex)
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
                FunNodeZTree_Script += "\t\t\t\n{ id:\"" + KmRow["classcode"].ToString() + "\"" +
                    ", pId: \"" + Pautoid + "\"" +
                    ", name: \"" + KmRow["caption"].ToString() + "\"" +
                    ", t: \"" + KmRow["caption"].ToString() + "\"" +
                    ", wbs: \"" + KmRow["WBS"].ToString() + "\"" +
                    ", pwbs: \"" + KmRow["PWBS"].ToString() + "\"" +
                    ", open:true" +
                    ", icon:\"../../img/treeimages/dept.gif\"" + url;

                FunNodeZTree_Script += "}";
                sb_zTree.Append(FunNodeZTree_Script);

                //如果当前节点有子节点
                int sonnum = treedt.Select("pwbs='" + KmRow["wbs"].ToString() + "'").Length;
                if (sonnum > 0)
                {
                    GetListTree(KmRow["wbs"].ToString(), KmRow["classcode"].ToString(), dgIndex + "1");
                }
            }
        }
    }
}