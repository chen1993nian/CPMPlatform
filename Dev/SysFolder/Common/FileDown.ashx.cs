using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using EIS.DataAccess;
using System.IO;
using System.Text;


namespace Studio.JZY.SysFolder.Common
{
    /// <summary>
    /// FileDown 的摘要说明
    /// </summary>
    public class FileDown : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            GetFile(context);
        }


        /// <summary>
        /// 获取文件配置
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetFileConfig(HttpContext context)
        {

            string Company_sql = @"select top 1 *  from T_E_File_Config order by _createtime desc ";
            DataSet ds = SysDatabase.ExecuteDataSet(Company_sql);

            string file_folder = "";
            if ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
            {
                file_folder = ds.Tables[0].Rows[0]["BasePath"].ToString();
            }
            if (file_folder.Substring(file_folder.Length - 1, 1) != "\\")
            {
                file_folder = file_folder + "\\";
            }
            ds.Dispose();
            return (file_folder);
        }


        public DataSet GetFileInfoByFileId(HttpContext context, string FileId)
        {
            string folder_sql = @"select top 1 *  from T_E_File_File  where _AutoID = @FileId";
            DbCommand cmmd1 = SysDatabase.GetSqlStringCommand(folder_sql);
            SysDatabase.AddInParameter(cmmd1, "@FileId", DbType.String, FileId);
            DataSet ds = SysDatabase.ExecuteDataSet(cmmd1);
            return (ds);
        }


        public DataSet GetFileInfoByAppId(HttpContext context, string AppName, string AppId)
        {
            string folder_sql = "";
            if ((AppName != "") && (AppId != ""))
            {
                folder_sql = @"select top 1 *  from T_E_File_File  
                where _AutoID in (select _AutoID from T_E_File_File where AppName=@AppName and AppId = @AppId )
                order by _CreateTime desc ";
            }
            else 
            {
                folder_sql = @"select top 1 *  from T_E_File_File  
                where _AutoID in (select _AutoID from T_E_File_File where AppId = @AppId )
                order by _CreateTime desc ";
            }
            DbCommand cmmd1 = SysDatabase.GetSqlStringCommand(folder_sql);
            SysDatabase.AddInParameter(cmmd1, "@AppName", DbType.String, AppName);
            SysDatabase.AddInParameter(cmmd1, "@AppId", DbType.String, AppId);
            DataSet ds = SysDatabase.ExecuteDataSet(cmmd1);
            return (ds);
        }


        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="context"></param>
        private void GetFile(HttpContext context)
        {
            string AppID = "";
            string AppName = "";
            string FileID = "";
            if (context.Request["AppName"] != null) AppName = context.Request["AppName"].ToString();
            if (context.Request["AppID"] != null) AppID = context.Request["AppID"].ToString();
            if (context.Request["FileID"] != null) FileID = context.Request["FileID"].ToString();
            DataSet file_ds = null;
            string folder_str = GetFileConfig(context);
            if (FileID != "")
                file_ds = GetFileInfoByFileId(context, FileID);
            if ((AppName != "") && (AppID != ""))
                file_ds = GetFileInfoByAppId(context, AppName, AppID);
            if ((file_ds.Tables.Count > 0) && (file_ds.Tables[0].Rows.Count > 0))
            {
                string filePath = folder_str + file_ds.Tables[0].Rows[0]["FilePath"].ToString();
                string filename = file_ds.Tables[0].Rows[0]["FactFileName"].ToString();
                DownPhoto(context, filePath, filename);
            }
        }


        public void DownPhoto(HttpContext context, string path, string fileName)
        {
            Stream stream = null;
            byte[] buffer = new byte[0x2710];
            try
            {
                stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                long length = stream.Length;
                context.Response.ContentType = "application/octet-stream";
                context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + HttpUtility.UrlEncode(fileName, Encoding.UTF8) + "\";");
                while (length > 0L)
                {
                    if (context.Response.IsClientConnected)
                    {
                        int count = stream.Read(buffer, 0, 0x2710);
                        context.Response.OutputStream.Write(buffer, 0, count);
                        context.Response.Flush();
                        buffer = new byte[0x2710];
                        length -= count;
                    }
                    else
                    {
                        length = -1L;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                context.Response.End();
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}