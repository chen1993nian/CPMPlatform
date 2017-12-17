using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using EIS.DataAccess;
using System.IO;



namespace Studio.JZY.WorkAsp.RelationTree
{
    /// <summary>
    /// 
    /// </summary>
    public class whRelationPage {
        public string RelationID = "";
        public string RelationName = "";
        public string RelationTree = "";
        public string RelationTreeUrl = "";
        public string RelationUIType = "";
        public string RelationUIWidth = "";
        public string RelationUIUrl = "";
        public string AppUrl1 = "";
        public string AppUrl2 = "";
        public string LevelUrl = "";
        public string LevelUrl1 = "";
        public string LevelUrl2 = "";
        public string LevelUrl3 = "";
        public string LevelUrl4 = "";
        public string MainUrl = "";
    }

    public class RelationPageInfo
    {

        public DataRow GetRelationPageInfo_Data(string RelationID)
        {
            DataSet ds = new DataSet();
            System.Data.Common.DbCommand comm = SysDatabase.GetSqlStringCommand("select * from T_E_App_RelationPageInfo where RelationID=@RelationID");
            SysDatabase.AddInParameter(comm, "@RelationID", DbType.String, RelationID);
            ds = SysDatabase.ExecuteDataSet(comm);
            #region 从XML文件中读取关联页面信息
            //如果没有找到记录，就到XML文件中去找
            bool IsNotExt = false;
            if (ds.Tables.Count == 0) {
                IsNotExt = true;
            }
            else if (ds.Tables[0].Rows.Count == 0)
            {
                IsNotExt = true;
            }
            if (IsNotExt)
            {
                ds = GetRelationPageInfoToXML();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow[] arr_t = ds.Tables[0].Select("RelationID='" + RelationID + "'");
                    if (arr_t.Length > 0)
                        return (arr_t[0]);
                    else
                        return (null);
                }
                else
                {
                    return (null);
                }
            }
            else
            {
                return (ds.Tables[0].Rows[0]);
            }
            #endregion
        }

        public whRelationPage GetRelationPageInfoByID(string RelationID,EIS.AppBase.PageBase objPage)
        {
            whRelationPage objRpage = new whRelationPage();
            
            DataRow dr = GetRelationPageInfo_Data(RelationID);
            if (dr != null )
            {
                objRpage.RelationID = dr["RelationID"].ToString();
                objRpage.RelationName = dr["RelationName"].ToString();
                objRpage.RelationUIType = dr["RelationUIType"].ToString();
                objRpage.RelationUIWidth = dr["RelationUIWidth"].ToString();
                objRpage.AppUrl1 = dr["AppUrl1"].ToString();
                objRpage.AppUrl2 = dr["AppUrl2"].ToString();
                objRpage.LevelUrl = dr["LevelUrl"].ToString();
                objRpage.LevelUrl1 = dr["LevelUrl1"].ToString();
                objRpage.LevelUrl2 = dr["LevelUrl2"].ToString();
                objRpage.LevelUrl3 = dr["LevelUrl3"].ToString();
                objRpage.LevelUrl4 = dr["LevelUrl4"].ToString();
                objRpage.MainUrl = dr["MainUrl"].ToString();

                if (objRpage.LevelUrl != "")
                {
                    objRpage.LevelUrl = objPage.ReplaceContext(objRpage.LevelUrl);
                    objRpage.LevelUrl = ReplaceQueryString(objRpage.LevelUrl);
                }
                if (objRpage.MainUrl != "")
                {
                    objRpage.MainUrl = objPage.ReplaceContext(objRpage.MainUrl);
                    objRpage.MainUrl = ReplaceQueryString(objRpage.MainUrl);
                }
                if (objRpage.LevelUrl1 != "")
                {
                    objRpage.LevelUrl1 = objPage.ReplaceContext(objRpage.LevelUrl1);
                    objRpage.LevelUrl1 = ReplaceQueryString(objRpage.LevelUrl1);
                }
                if (objRpage.LevelUrl2 != "")
                {
                    objRpage.LevelUrl2 = objPage.ReplaceContext(objRpage.LevelUrl2);
                    objRpage.LevelUrl2 = ReplaceQueryString(objRpage.LevelUrl2);
                }
                if (objRpage.LevelUrl3 != "")
                {
                    objRpage.LevelUrl3 = objPage.ReplaceContext(objRpage.LevelUrl3);
                    objRpage.LevelUrl3 = ReplaceQueryString(objRpage.LevelUrl3);
                }
                if (objRpage.LevelUrl4 != "")
                {
                    objRpage.LevelUrl4 = objPage.ReplaceContext(objRpage.LevelUrl4);
                    objRpage.LevelUrl4 = ReplaceQueryString(objRpage.LevelUrl4);
                }

                if (objRpage.AppUrl2 == "")
                {
                    objRpage.AppUrl2 = "../../welcome.htm";
                }
                else
                {
                    objRpage.AppUrl2 = objPage.ReplaceContext(objRpage.AppUrl2);
                }

                objRpage.AppUrl1 = objPage.ReplaceContext(objRpage.AppUrl1);
                objRpage.AppUrl1 = ReplaceQueryString(objRpage.AppUrl1);
                objRpage.AppUrl2 = ReplaceQueryString(objRpage.AppUrl2);

                string funid = objPage.GetParaValue("funid");
                if (objRpage.AppUrl1.IndexOf("?") > 0)
                {
                    objRpage.AppUrl1 = objRpage.AppUrl1 + "&funid=" + funid;
                }
                else
                {
                    objRpage.AppUrl1 = objRpage.AppUrl1 + "?funid=" + funid;
                }

                if (objRpage.AppUrl2.IndexOf("?") > 0)
                {
                    objRpage.AppUrl2 = objRpage.AppUrl2 + "&funid=" + funid;
                }
                else
                {
                    objRpage.AppUrl2 = objRpage.AppUrl2 + "?funid=" + funid;
                }

                //左树右列表|1,右树左列表|2,上下型结构|3,左右型结构|4,下上型结构|5,右左型结构|6
                if ((objRpage.RelationUIWidth == "*,*") || (objRpage.RelationUIWidth == ""))
                {
                    switch (objRpage.RelationUIType)
                    {
                        case "1":
                            objRpage.RelationUIWidth = "300,*";
                            break;
                        case "2":
                            objRpage.RelationUIWidth = "*,300";
                            break;
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                            objRpage.RelationUIWidth = "60,*";
                            break;
                    }
                }

                switch (objRpage.RelationUIType)
                {
                    case "1":
                        objRpage.RelationUIUrl = "./RelationRightLeft.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "2":
                        objRpage.RelationUIUrl = "./RelationLeftRight.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "3":
                        objRpage.RelationUIUrl = "./RelationLayoutTopBottom.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "4":
                        objRpage.RelationUIUrl = "./RelationLayoutRightLeft.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "5":
                        objRpage.RelationUIUrl = "./RelationLayoutBottomTop.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "6":
                        objRpage.RelationUIUrl = "./RelationLayoutLeftRight.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                }
                //单位树|1,部门树|2,岗位树|3,部门人员树|4,角色人员树|5,部门角色树|6
                //物资分类树|7,周转材料分类树|8,机械设备分类树|9,成本科目树|10
                //档案分类树|11,档案存放位置树|12,档案国标分类树|13,档案重大项目树|14,档案WBS结构树|15
                //流程分类树|16，办公用品分类树|17,供应商类别树|18,预算科目分类树|19,资金类别树|20
                //日常费用类别树|21,综合业务节点树|22
                //项目列表|23,单位工程树|24,分包工程树|25
                //预算科目树|26,合同类型树|27,工种树|28
                //安全资料分类模板|29,自定义链接|30,规范目录树|31,质量资料分类模板树|32
                //项目树|33,项目楼栋树|34,
                string RelationTree = dr["RelationTree"].ToString();
                switch (RelationTree)
                {
                    case "1":
                        objRpage.RelationTreeUrl = "./RelationCompanyTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "2":
                        objRpage.RelationTreeUrl = "./RelationDeptTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "3":
                        objRpage.RelationTreeUrl = "./RelationPostitionTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "4":
                        objRpage.RelationTreeUrl = "./RelationDeptPersonTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "5":
                        objRpage.RelationTreeUrl = "./RelationRolePersonTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "6":
                        objRpage.RelationTreeUrl = "./RelationDeptRoleTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "7":
                        objRpage.RelationTreeUrl = "./RelationMaterialsClassifyTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "8":
                        objRpage.RelationTreeUrl = "./RelationSupMaterialsClassifyTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "9":
                        objRpage.RelationTreeUrl = "./RelationEquipmentClassifyTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "10":
                        objRpage.RelationTreeUrl = "./RelationCostAccountClassifyTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "11":
                        string TreeID = "8eddc812-ab7e-4eaf-8398-16e70cf02dc4";
                        //依据关联名称查询目录树ID
                        TreeID = GetTreeIDByRelationName(objRpage.RelationName, TreeID);
                        objRpage.RelationTreeUrl = "./RelationArchiveClassifyTree.aspx?" + objPage.Request.QueryString.ToString() + "&TreeID=" + TreeID;
                        break;
                    case "12":
                        objRpage.RelationTreeUrl = "./RelationArchivePostionClassifyTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "13":
                        objRpage.RelationTreeUrl = "./RelationArchiveGBT50328ClassifyTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "14":
                        objRpage.RelationTreeUrl = "./RelationArchiveT282002ClassifyTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "15":
                        objRpage.RelationTreeUrl = "./RelationArchiveWBSClassifyTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "16":
                        objRpage.RelationTreeUrl = "./RelationWorkFlowClassifyTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "17":
                        objRpage.RelationTreeUrl = "./RelationOfficeSuppliesClassifyTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "18":
                        objRpage.RelationTreeUrl = "./RelationSupplierTypeClassifyTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "19":
                        objRpage.RelationTreeUrl = "./RelationBudgetCostAccountClassifyTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "20":
                        objRpage.RelationTreeUrl = "./RelationCapitalClassifyTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "21":
                        objRpage.RelationTreeUrl = "./RelationDailyExpenseClassifyTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "22":
                        objRpage.RelationTreeUrl = "./RelationIntegratedPageTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "23":
                        objRpage.RelationTreeUrl = "./ListSGBasicProjectInfo.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "24":
                        objRpage.RelationTreeUrl = "./ListSGBasicUnitProjectInfo.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "25":
                        objRpage.RelationTreeUrl = "./ListSGBasicBidProjectInfo.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "26":
                        objRpage.RelationTreeUrl = "./RelationBudgetCostAccountTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "27":
                        objRpage.RelationTreeUrl = "./RelationContractTypeTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "28":
                        objRpage.RelationTreeUrl = "./RelationWordSortTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "29":
                        objRpage.RelationTreeUrl = "./RelationSafeDataClassTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "30":
                        objRpage.RelationTreeUrl = objRpage.MainUrl;
                        break;
                    case "31":
                        objRpage.RelationTreeUrl = "./RelationStandardChapterTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "32":
                        objRpage.RelationTreeUrl = "./RelationQualityDataClassTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "33":
                        objRpage.RelationTreeUrl = "./RelationProjectTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                    case "34":
                        objRpage.RelationTreeUrl = "./RelationBuildingMessageTree.aspx?" + objPage.Request.QueryString.ToString();
                        break;
                }
                objRpage.RelationName = objPage.GetParaValue("Caption");
                if (objRpage.RelationName == "")
                {
                    objRpage.RelationName = dr["RelationName"].ToString();
                }
            }
            return (objRpage);
        }

        public string GetTreeIDByRelationName(string rName, string tid)
        {
            string TreeID = tid;
            DataSet ds = new DataSet();
            string sqlstr = @"select _AutoID  
                    from T_E_File_Folder
                    where (FolderPID='' or FolderPID in (select _AutoID from T_E_File_Folder where FolderPID='')) 
                    and FolderName=@FolderName";
            System.Data.Common.DbCommand comm = SysDatabase.GetSqlStringCommand(sqlstr);
            SysDatabase.AddInParameter(comm, "@FolderName", DbType.String, rName);
            ds = SysDatabase.ExecuteDataSet(comm);
            if ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
            {
                TreeID = ds.Tables[0].Rows[0][0].ToString();
            }
            ds.Dispose();
            return (TreeID);
        }

        //生成XML文件
        public DataSet SaveRelationPageInfoToXML(string str_path)
        {
            DataSet ds = new DataSet();
            System.Data.Common.DbCommand comm = SysDatabase.GetSqlStringCommand("select * from T_E_App_RelationPageInfo order by _CreateTime");
            ds = SysDatabase.ExecuteDataSet(comm);
            string filepath1 = HttpContext.Current.Server.MapPath(str_path) + "\\WorkAsp\\RelationTree\\T_E_App_RelationPageInfo.xml";
            ds.WriteXml(filepath1,XmlWriteMode.IgnoreSchema);
            string filepath2 = HttpContext.Current.Server.MapPath(str_path) + "\\WorkAsp\\RelationTree\\T_E_App_RelationPageInfo.xsd";
            ds.WriteXmlSchema(filepath2);
            return (ds);
        }


        /// <summary>
        /// 从XML文件中，获取数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetRelationPageInfoToXML()
        {
            DataSet ds = new DataSet("T_E_App_RelationPageInfo");
            string spath = HttpContext.Current.Server.MapPath("");
            string filepath1 = spath + "\\T_E_App_RelationPageInfo.xml";
            string filepath2 = spath + "\\T_E_App_RelationPageInfo.xsd";
            if (!System.IO.File.Exists(filepath1))
            {
                filepath1 = spath + "\\WorkAsp\\RelationTree\\T_E_App_RelationPageInfo.xml";
                filepath2 = spath + "\\WorkAsp\\RelationTree\\T_E_App_RelationPageInfo.xsd";
                if (!System.IO.File.Exists(filepath1))
                {
                    filepath1 = spath + "\\..\\RelationTree\\T_E_App_RelationPageInfo.xml";
                    filepath2 = spath + "\\..\\RelationTree\\T_E_App_RelationPageInfo.xsd";
                }
            }
            ds.ReadXmlSchema(filepath2);
            ds.ReadXml(filepath1, XmlReadMode.IgnoreSchema);
            return (ds);
        }

        public string ReplaceQueryString(string url)
        {

            for (int qIndex = 0; qIndex < HttpContext.Current.Request.QueryString.Count; qIndex++)
            {
                string keyname =  "@" + HttpContext.Current.Request.QueryString.Keys[qIndex].ToString();
                string keyvalue = HttpContext.Current.Request.QueryString[qIndex].ToString();

                url = url.Replace(keyname, keyvalue);
            }
            url = url.Replace("'", "%27");
            return (url);
        }

    }
}