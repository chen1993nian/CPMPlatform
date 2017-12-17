using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using EIS.DataAccess;

namespace Studio.JZY.WorkAsp.DataLimit
{
    public class ClsDataLimit1
    {
        /// <summary>
        /// 默认设置所有部门拥有查看本公司所有数据的权限
        /// </summary>
        public void SetAllDeptDataLimit()
        {
            string datalimit_sql = @"select a._AutoID as DeptID,a.CompanyId from dbo.T_E_Org_Department a order by a.DeptWBS";
            System.Data.Common.DbCommand command = SysDatabase.GetSqlStringCommand(datalimit_sql);
            DataSet ds = SysDatabase.ExecuteDataSet(command);

            string insert_datalimit_sql = @"delete T_E_Org_DataLimit where departmentID = '{0}';
                insert into T_E_Org_DataLimit (DataLimitID ,DeptID, DeptCode, DeptName, DeptWBS, DeptPWBS,departmentID)
                select newid() ,_AutoID, DeptCode, DeptName, DeptWBS, DeptPWBS,'{0}' as departmentID
                from T_E_Org_Department
                where CompanyId = '{1}' or  _AutoID = '{1}'";

            foreach (DataRow dr in ds.Tables[0].Rows) {
                string insert_sql = string.Format(insert_datalimit_sql, dr["DeptID"].ToString(), dr["CompanyId"].ToString());
                SysDatabase.ExecuteNonQuery(insert_sql);
            }

        }
    }
}