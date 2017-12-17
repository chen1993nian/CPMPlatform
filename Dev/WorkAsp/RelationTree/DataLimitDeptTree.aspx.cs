using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Xml;
using AjaxPro;
using System.Data;
using System.Data.SqlClient;
using EIS.DataAccess;

namespace Studio.JZY.WorkAsp.DataLimit
{
    public partial class DataLimitDeptTree : EIS.AppBase.PageBase
    {

        public string DataLimit_Type = "EmployeeID";
        public string DataLimit_Value = "";
        public string CaptionFldName = "DeptName";
        public string FunNodeZTree_Script = "";
        StringBuilder sb_zTree = new StringBuilder();
        DataSet treeds = new DataSet();
        DataTable treedt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DataLimitDeptTree),this.Page);
            if (!this.IsPostBack)
            {
                FunNodeZTree_Script = "";
                if (Request["CaptionFldName"] != null) CaptionFldName = Request["CaptionFldName"].ToString();
                //获取部门集合
                GetDepartmentTreeDataTable();
                HiddenField1.Value = DataLimit_Type;
                HiddenField2.Value = DataLimit_Value;

                DataRow[] arrRow;
                string strExpr = "DeptWBS='0'";
                arrRow = treedt.Select(strExpr);
                int arrcount = arrRow.Length;
                //循环所有节点
                if (arrcount > 0)
                {
                    DataRow KmRow = arrRow[0];
                    GetListTree("", arrRow[0]["_autoid"].ToString());
                }
                else
                {
                    strExpr = "DeptPWBS='0'";
                    arrRow = treedt.Select(strExpr);
                    arrcount = arrRow.Length;
                    if (arrcount > 0)
                    {
                        DataRow KmRow = arrRow[0];
                        GetListTree("0", arrRow[0]["_autoid"].ToString());
                    }
                }
                this.FunNodeZTree_Script = sb_zTree.ToString();
            }
        }
        /// <summary>
        /// 获取部门集合
        /// </summary>
        private void GetDepartmentTreeDataTable()
        {
            string username = Session["UserName"].ToString();
            string employeeid = Session["EmployeeID"].ToString();
            string datalimit_sql = @"select a._autoid,a._autoid as DeptID,a.DeptName,a.DeptCode
                ,a.DeptWBS,a.DeptPWBS,a.DeptAbbr as DeptJC,'false' as Limit
                from T_E_Org_Department a where _IsDel=0 ";
            //获取人员部门权限
            if (Request["personid"] != null)
            {
                DataLimit_Type = "EmployeeID";
                DataLimit_Value = Request["personid"].ToString();
                datalimit_sql = @"select a._autoid,a._autoid as DeptID,a.DeptName,a.DeptCode,a.DeptWBS,a.DeptPWBS,a.DeptAbbr as DeptJC
                    , (case when isnull(c.DeptID,'') = '' then 'false' else 'true' end) as Limit  
                    from T_E_Org_Department a  
                    left join T_E_Org_DataLimit c  
                    on a._autoid=c.DeptID and c.EmployeeID='" + Request["personid"].ToString() + @"' 
                    where a._IsDel=0";
            }
            //获取角色部门权限
            if (Request["RoleID"] != null)
            {
                DataLimit_Type = "RoleID";
                DataLimit_Value = Request["RoleID"].ToString();
                datalimit_sql = @"select a._autoid,a._autoid as DeptID,a.DeptName,a.DeptCode,a.DeptWBS,a.DeptPWBS,a.DeptAbbr as DeptJC
                    , (case when isnull(c.DeptID,'') = '' then 'false' else 'true' end) as Limit  
                    from T_E_Org_Department a  
                    left join T_E_Org_DataLimit c  
                    on a._autoid=c.DeptID and c.RoleID='" + Request["RoleID"].ToString() + @"' 
                    where a._IsDel=0 ";
            }
            //获取岗位部门权限
            if (Request["PositionID"] != null)
            {
                DataLimit_Type = "PositionID";
                DataLimit_Value = Request["PositionID"].ToString();
                datalimit_sql = @"select a._autoid,a._autoid as DeptID,a.DeptName,a.DeptCode,a.DeptWBS,a.DeptPWBS,a.DeptAbbr as DeptJC
                    , (case when isnull(c.DeptID,'') = '' then 'false' else 'true' end) as Limit  
                    from T_E_Org_Department a  
                    left join T_E_Org_DataLimit c  
                    on a._autoid=c.DeptID and c.PositionID='" + Request["PositionID"].ToString() + @"' 
                    where a._IsDel=0";
            }
            //获取部门部门权限
            if (Request["DeptID"] != null)
            {
                DataLimit_Type = "DepartmentID";
                DataLimit_Value = Request["DeptID"].ToString();

                datalimit_sql = @"select a._autoid,a._autoid as DeptID,a.DeptName,a.DeptCode,a.DeptWBS,a.DeptPWBS,a.DeptAbbr as DeptJC
                    , (case when isnull(c.DeptID,'') = '' then 'false' else 'true' end) as Limit  
                    from T_E_Org_Department a  
                    left join T_E_Org_DataLimit c  
                    on a._autoid=c.DeptID and c.DepartmentID='" + Request["DeptID"].ToString() + @"' 
                    where a._IsDel=0";
            }

            datalimit_sql = datalimit_sql + " order by a.OrderID ";

            System.Data.Common.DbCommand command = SysDatabase.GetSqlStringCommand(datalimit_sql);
            DataSet treeds = SysDatabase.ExecuteDataSet(command);
            treedt = treeds.Tables[0];
        }

        /// <summary>
        /// 递归显示菜单
        /// </summary>
        /// <param name="PIDValue">上级菜单ID</param>
        /// <param name="Pautoid">上级菜单autoID</param>
        private void GetListTree(string PIDValue, string Pautoid)
        {
            DataRow[] arrRow;
            string strExpr = "DeptPWBS='" + PIDValue + "'";
            arrRow = treedt.Select(strExpr);
            int arrcount = arrRow.Length;
            //循环所有节点
            for (int m = 0; m < arrcount; m++)
            {
                DataRow KmRow = arrRow[m];

                if (FunNodeZTree_Script != "") FunNodeZTree_Script = ",";
                FunNodeZTree_Script += "\t\t\t\n{ id:\"" + KmRow["_autoid"].ToString() + "\"" +
                    ", pId:\"" + Pautoid + "\"" +
                    ", name: \"" + KmRow[CaptionFldName].ToString() + "\"" +
                    ", t: \"" + KmRow[CaptionFldName].ToString() + "\"" +
                    ", DeptID: \"" + KmRow["DeptID"].ToString() + "\"" +
                    ", DeptCode: \"" + KmRow["DeptCode"].ToString() + "\"" +
                    ", DeptWBS: \"" + KmRow["DeptWBS"].ToString() + "\"" +
                    ", DeptPWBS: \"" + KmRow["DeptPWBS"].ToString() + "\"" +
                    ", checked:" + KmRow["Limit"].ToString() +
                    ", open:" + (KmRow["DeptWBS"].ToString() == "0" ? "true" : "false") +
                    "}";
                sb_zTree.Append(FunNodeZTree_Script);

                //如果当前节点有子节点
                int sonnum = treedt.Select("DeptPWBS='" + KmRow["DeptWBS"].ToString() + "'").Length;
                if (sonnum > 0)
                {
                    GetListTree(KmRow["DeptWBS"].ToString(), KmRow["_autoid"].ToString());
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="funid"></param>
        /// <param name="remark"></param>
        /// <param name="funOrder"></param>
        /// <returns></returns>
        [AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string SaveCheckDept(string str_detpid, string fldName, string fldValue)
        {
            try
            {
                string datalimit_sql = @"delete T_E_Org_DataLimit where {0}='{1}';";
                if (str_detpid != "")
                {
                    datalimit_sql = datalimit_sql +
                        @"insert into T_E_Org_DataLimit (DataLimitID ,DeptID, DeptCode, DeptName, DeptWBS, DeptPWBS,{0})
                        select newid() ,_AutoID, DeptCode, DeptName, DeptWBS, DeptPWBS,'{1}' as {0}
                        from T_E_Org_Department
                        where _AutoID in ({2})";
                }
                datalimit_sql = string.Format(datalimit_sql, fldName, fldValue, str_detpid);

                System.Data.Common.DbCommand command = SysDatabase.GetSqlStringCommand(datalimit_sql);
                SysDatabase.ExecuteNonQuery(command);
            }
            catch { }
            finally { }
            return ("");
        }

    }
}