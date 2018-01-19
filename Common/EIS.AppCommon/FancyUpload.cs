using EIS.AppBase;
//using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.SessionState;
using EIS.AppCommo;
namespace EIS.AppCommon
{
    /// <summary>
    /// 上传文件
    /// </summary>
	public class FancyUpload : IHttpHandler, IRequiresSessionState
	{
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public FancyUpload()
		{
		}

		public void ProcessRequest(HttpContext context)
		{
			if (context.Session["UserName"] == null)
			{
				throw new Exception("会话过期！");
			}
			string appFileSavePath = AppSettings.Instance.AppFileSavePath;
			if (context.Request.Files.Count > 0)
			{
				for (int i = 0; i < context.Request.Files.Count; i++)
				{
					HttpPostedFile item = context.Request.Files[i];
					if (item.ContentLength > 0)
					{
						string fileName = Path.GetFileName(item.FileName);
						string lower = Path.GetExtension(fileName).ToLower();
						DateTime now = DateTime.Now;
						string str = string.Concat(now.ToString("yyyy-MM-dd-HH-mm-ss-ffff", DateTimeFormatInfo.InvariantInfo), lower);
						now = DateTime.Now;
						string str1 = now.ToString("yyyy年MM月", DateTimeFormatInfo.InvariantInfo);
						if (!string.IsNullOrEmpty(context.Request["FolderName"]))
						{
							str1 = context.Request["FolderName"];
						}
						else if (!string.IsNullOrEmpty(context.Request["Folder"]))
						{
							str1 = context.Request["Folder"];
						}
						try
						{
							if (!Directory.Exists(string.Concat(appFileSavePath, str1)))
							{
								Directory.CreateDirectory(string.Concat(appFileSavePath, str1));
							}
							item.SaveAs(string.Concat(appFileSavePath, str1, "\\", str));
							AppFile appFile = new AppFile()
							{
								_AutoID = Guid.NewGuid().ToString(),
								_UserName = Utility.GetSession("EmployeeID").ToString(),
								_OrgCode = Utility.GetSession("DeptWbs").ToString(),
								_CreateTime = DateTime.Now,
								_UpdateTime = DateTime.Now,
								_IsDel = 0,
								FileName = str,
								FactFileName = fileName,
								FilePath = string.Concat(str1, "\\", str),
								BasePath = AppSettings.Instance.AppFileBaseCode,
								DownCount = 0,
								FileSize = item.ContentLength,
								FileType = lower,
								FolderID = context.Request["FolderID"]
							};
							if (context.Request["AppID"] != null)
							{
								appFile.AppId = context.Request["AppID"];
								appFile.AppName = context.Request["AppName"];
							}
							(new _C_AppFile()).Add(appFile);
							string str2 = appFile._AutoID;
							if (str2 != "")
							{
								context.Response.Write(str2);
							}
						}
						catch (Exception exception1)
						{
							Exception exception = exception1;
							context.Response.Write(string.Format("{{\"status\":\"{0}\",\"error\":\"{1}\"}}", 0, exception.Message));
						}
					}
				}
			}
		}
	}
}