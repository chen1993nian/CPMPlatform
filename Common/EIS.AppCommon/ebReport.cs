using EIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;


namespace EIS.AppCommon
{
    /// <summary>
    /// e表运行权限验证,防SQL注入
    /// </summary>
    public class EbReportLimitRun : IHttpModule
    {

        public void Dispose() { }
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication httpApp = (HttpApplication)sender;
            string pageUrl = httpApp.Request.Url.ToString();
            if ((pageUrl.Contains("ebsys/")) && (pageUrl.Contains("/ebrun.htm")))
            {
                //如果没有人员ID，不允许执行报表
                if (pageUrl.Contains("rpara="))
                {
                    string[] arr1 = pageUrl.Split('?');
                    string[] arr2 = arr1[1].Split('&');
                    string ebFileName = "";
                    string EmployeeID = "";
                    foreach (string paraStr in arr2)
                    {
                        if (paraStr.Contains("file="))
                        {
                            ebFileName = paraStr.Split('=')[1].ToString();
                        }
                        else if (paraStr.Contains("rpara="))
                        {
                            EmployeeID = paraStr.Split('=')[1].ToString();
                        }
                        else
                        {
                            if (sql_inj(paraStr))
                            {
                                ShowFileNotLimit(httpApp);
                                break;
                            }
                        }
                    }

                    DataSet ds = getReportDataSet(ebFileName);
                    if ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
                    {
                        string AutoID = ds.Tables[0].Rows[0]["_AutoID"].ToString();
                        DataSet ds_limit = getReportLimitDataSet(EmployeeID, AutoID);
                        if ((ds_limit.Tables.Count > 0) && (ds_limit.Tables[0].Rows.Count > 0))
                        {
                        }
                        else
                        {
                            ShowFileNotLimit(httpApp);
                        }
                    }
                }
                else
                {
                    ShowFileNotLimit(httpApp);
                }
            }
        }

        /// <summary>
        /// 跳转到授权提醒页面
        /// </summary>
        /// <param name="httpApp"></param>
        private void ShowFileNotLimit(HttpApplication httpApp)
        {
            Int32 p1 = httpApp.Request.Url.ToString().IndexOf("/ebsys/");
            string str = httpApp.Request.Url.ToString().Substring(0, p1);
            if (ConfigurationManager.AppSettings["WebAppRoot"] != null) str = ConfigurationManager.AppSettings["WebAppRoot"].ToString();
            httpApp.Response.Redirect(str + "/FileNotLimit.htm");
        }

        /// <summary>
        /// SQL注入检查
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private Boolean sql_inj(String str)
        {
            str = str.ToLower();
            String inj_str = "exec|insert|select|delete|update|master|truncate|declare|create|drop";
            String[] inj_arr = inj_str.Split('|');
            for (int i = 0; i < inj_arr.Length; i++)
            {
                if (str.IndexOf(inj_arr[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取报表信息
        /// </summary>
        /// <param name="ebFileName"></param>
        /// <returns></returns>
        private DataSet getReportDataSet(string ebFileName)
        {
            string sqlStr = "select * from T_E_App_Reports where CHARINDEX(ReportFile,@FilePath)>0 and ReportLimit=0";
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(sqlStr);
            SysDatabase.AddInParameter(sqlStringCommand, "@FilePath", DbType.String, ebFileName);
            DataSet ds = SysDatabase.ExecuteDataSet(sqlStringCommand);
            return (ds);
        }

        /// <summary>
        /// 获取报表权限信息
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <param name="AutoID"></param>
        /// <returns></returns>
        private DataSet getReportLimitDataSet(string EmployeeID, string AutoID)
        {
            string ret_sql = @"select b.RedpID
                from T_E_App_Reports a
                left join dbo.T_E_App_Reports_Sub2 b on a._AutoID=b._MainID
                where b.LimitType=1 and b.RedpID in (
	                select RoleID from T_E_Org_RoleEmployee 
	                where EmployeeID=@EmployeeID
                ) and a._AutoID=@AutoID
                Union all
                select b.RedpID
                from T_E_App_Reports a
                left join dbo.T_E_App_Reports_Sub2 b on a._AutoID=b._MainID
                where b.LimitType=2 and b.RedpID=@EmployeeID and a._AutoID=@AutoID
                Union all
                select b.RedpID
                from T_E_App_Reports a
                left join dbo.T_E_App_Reports_Sub2 b on a._AutoID=b._MainID
                where b.LimitType=3 and b.RedpID in (
	                select DeptID from T_E_Org_Employee where _AutoID=@EmployeeID
                ) and a._AutoID=@AutoID
                Union all
                select b.RedpID
                from T_E_App_Reports a
                left join dbo.T_E_App_Reports_Sub2 b on a._AutoID=b._MainID
                where b.LimitType=4 and b.RedpID in (
	                select PositionID from T_E_Org_Employee where _AutoID=@EmployeeID
                ) and a._AutoID=@AutoID";
            DbCommand sqlStringCommand = SysDatabase.GetSqlStringCommand(ret_sql);
            SysDatabase.AddInParameter(sqlStringCommand, "@EmployeeID", DbType.String, EmployeeID);
            SysDatabase.AddInParameter(sqlStringCommand, "@AutoID", DbType.String, AutoID);
            DataSet ds_limit = SysDatabase.ExecuteDataSet(sqlStringCommand);
            return (ds_limit);
        }
    }

    /// <summary>
    /// 禁止使用e表设计器
    /// </summary>
    public class EbReportDisenableDesign : IHttpModule
    {
        public void Dispose() { }
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication httpApp = (HttpApplication)sender;
            if ((httpApp.Request.Url.ToString().Contains("ebsys")) && (httpApp.Request.RawUrl.Contains("ebdesign.htm")))
            {
                Int32 p1 = httpApp.Request.Url.ToString().IndexOf("/ebsys/");
                string str = httpApp.Request.Url.ToString().Substring(0, p1);
                if (ConfigurationManager.AppSettings["WebAppRoot"] != null) str = ConfigurationManager.AppSettings["WebAppRoot"].ToString();
                httpApp.Response.Redirect(str + "/FileNotFound.htm");
            }
        }
    }



}
