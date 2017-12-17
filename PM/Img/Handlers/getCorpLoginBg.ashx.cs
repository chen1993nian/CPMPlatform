using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using EIS.DataAccess;
using System.IO;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.AppBase;
using NLog.LogReceiverService;
using System.Text;



namespace EIS.Web.Img.Handlers
{
    /// <summary>
    /// getCorpLoginBg 的摘要说明
    /// </summary>
    public class getCorpLoginBg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/octet-stream";
            DownCorpPhotoBg(context);
        }

        public void DownCorpPhotoBg(HttpContext context)
        {
            string bg_sql = @"select * from T_E_File_File 
                where AppId in (
	                select top 1 bgFile  from (
		                select bgFile,bgType from dbo.T_E_App_CorpLoginBackGround 
		                where bgType=5 and DATEDIFF(day,bgDate,GETDATE())=0
		                union all
		                select bgFile,bgType from dbo.T_E_App_CorpLoginBackGround 
		                where bgType=4 and DATEPART(day,GETDATE()) = bgDay
		                union all
		                select bgFile,bgType from dbo.T_E_App_CorpLoginBackGround 
		                where bgType=3 and DATEPART(weekday,GETDATE()) = bgWeekDay
		                union all
		                select bgFile,bgType from dbo.T_E_App_CorpLoginBackGround 
		                where bgType=2 and DATEPART(WEEK,GETDATE()) = bgWeek
		                union all
		                select bgFile,bgType from dbo.T_E_App_CorpLoginBackGround 
		                where bgType=1 and DATEPART(MONTH,GETDATE()) = bgMonth
	                ) a order by bgType desc
                ) and AppName = 'T_E_App_CorpLoginBackGround'";

            DataTable dataTable = SysDatabase.ExecuteTable(bg_sql);
            if (dataTable.Rows.Count > 0)
            {
                DownPhotoByFileId(context, dataTable.Rows[0]["_AutoID"].ToString());
            }
            else
            {
                string filePath = context.Server.MapPath("../") + "desktop\\login_main" + System.DateTime.Now.Month.ToString() + ".jpg";
                DownPhoto(context, filePath, "loginmain.png");
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
            catch (Exception exp)
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


        public void DownPhotoByFileId(HttpContext context, string FileId)
        {
            _AppFile __AppFile = new _AppFile();
            AppFile appFile = new AppFile();
            appFile = __AppFile.GetModel(FileId);
            if (appFile != null)
            {
                string basePath = AppFilePath.GetBasePath(appFile.BasePath);
                string filePath = string.Concat(basePath, appFile.FilePath);
                if (File.Exists(filePath))
                {
                    DownPhoto(context, filePath, "logo.png");
                }
                else
                {
                    appFile = null;
                }
            }
            if (appFile == null)
            {
                string filePath = context.Server.MapPath("../") + "desktop\\login_main"+ System.DateTime.Now.Month.ToString() +".jpg";
                DownPhoto(context, filePath, "loginmain.png");
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