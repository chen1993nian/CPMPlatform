using EIS.AppBase;
using EIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EIS.Web.SysFolder.AppFrame
{
    public partial class AppReportRun : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ReportCode = base.GetParaValue("ReportCode").Trim();
            DataSet ds = getReportDataSet(ReportCode);
            if ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
            {
                //是易表
                if (ds.Tables[0].Rows[0]["ReportType"].ToString() == "1")
                {
                    string ReportUrl = "../../ebsys/fceform/ereport/ebrun.htm?";
                    //当前登录人员ID
                    ReportUrl += "rpara=" + Session["EmployeeId"].ToString();
                    //报表文件
                    string ReportFile = ds.Tables[0].Rows[0]["ReportFile"].ToString();
                    if (ReportFile.Substring(0, 1) == "/") ReportFile = ReportFile.Substring(1, ReportFile.Length);
                    ReportFile = "../../ebsys/ebfile/" + ReportFile + ".htm";
                    ReportUrl += "&file=" + ReportFile;
                    //报表需要的参数
                    DataSet ds_para = getReportParaDataSet(ReportCode);
                    foreach (DataRow dr in ds_para.Tables[0].Rows)
                    {
                        ReportUrl += "&" + dr["ParaName"].ToString() + base.GetParaValue(dr["ParaName"].ToString()).Trim();
                    }
                    //约定的默认条件参数
                    string condition = base.GetParaValue("condition").Trim();
                    if ((!ReportUrl.Contains("condition")) && (condition != ""))
                    {
                        ReportUrl += "&condition=" + condition;
                    }
                    //执行报表
                    HiddenField1.Value = ReportUrl;
                    //Response.Redirect(ReportUrl);
                }
                else if (ds.Tables[0].Rows[0]["ReportType"].ToString() == "2")
                {
                    //是帆软报表

                }

            }
        }



        /// <summary>
        /// 获取报表信息
        /// </summary>
        /// <param name="ebFileName"></param>
        /// <returns></returns>
        private DataSet getReportDataSet(string ReportCode)
        {
            string sqlStr = "select * from T_E_App_Reports where ReportCode=@ReportCode ";
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(sqlStr);
            SysDatabase.AddInParameter(sqlStringCommand, "@ReportCode", DbType.String, ReportCode);
            DataSet ds = SysDatabase.ExecuteDataSet(sqlStringCommand);
            return (ds);
        }

        /// <summary>
        /// 获取报表参数信息
        /// </summary>
        /// <param name="ebFileName"></param>
        /// <returns></returns>
        private DataSet getReportParaDataSet(string ReportCode)
        {
            string sqlStr = @"select b.* from T_E_App_Reports a 
                left join T_E_App_Reports_Sub1 b on a._AutoID=b._MainID
                where a.ReportCode=@ReportCode and b.ParaName<>'' ";
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(sqlStr);
            SysDatabase.AddInParameter(sqlStringCommand, "@ReportCode", DbType.String, ReportCode);
            DataSet ds = SysDatabase.ExecuteDataSet(sqlStringCommand);
            return (ds);
        }


    }
}