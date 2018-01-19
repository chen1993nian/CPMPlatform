using EIS.AppBase;
//using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Configuration;
using EIS.AppCommo;
namespace EIS.AppCommon
{
    /// <summary>
    /// 上传图片
    /// </summary>
	public class UploadImage : IHttpHandler, IRequiresSessionState
	{
		private HttpResponse Response;

		private HttpRequest Request;

		private string AppPath
		{
			get
			{
				string str;
				str = (this.Request.ApplicationPath.EndsWith("/") ? this.Request.ApplicationPath : string.Concat(this.Request.ApplicationPath, "/"));
				return str;
			}
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public UploadImage()
		{
		}

		private void Alert(string MsgStr)
		{
			this.Response.Write("<html>");
			this.Response.Write("<head>");
			this.Response.Write("<title>error</title>");
			this.Response.Write("<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\">");
			this.Response.Write("</head>");
			this.Response.Write("<body>");
			this.Response.Write(string.Concat("<script type=\"text/javascript\">alert(\"", MsgStr, "\");history.back();</script>"));
			this.Response.Write("</body>");
			this.Response.Write("</html>");
		}

		private bool CheckExt(string[] ExtStr, string fileExt)
		{
			bool flag;
			int num = 0;
			while (true)
			{
				if (num >= (int)ExtStr.Length)
				{
					flag = false;
					break;
				}
				else if (!(ExtStr[num] == fileExt))
				{
					num++;
				}
				else
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		public void ProcessRequest(HttpContext context)
		{
			if (context.Session["UserName"] == null)
			{
				throw new Exception("会话过期！");
			}
			this.Request = context.Request;
			this.Response = context.Response;
			string appFileSavePath = AppSettings.Instance.AppFileSavePath;
			string[] strArrays = new string[] { ".gif", ".jpg", ".png", ".bmp" };


            int num = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MaxLength"].ToString());
			string[] strArrays1 = new string[] { "上传文件大小超过限制.", "上传文件扩展名是不允许的扩展名.", "上传文件失败\\n请重试." };
			HttpPostedFile item = HttpContext.Current.Request.Files["imgFile"];
			if (item.ContentLength <= num)
			{
				string fileName = Path.GetFileName(item.FileName);
				string lower = Path.GetExtension(fileName).ToLower();
				if (!this.CheckExt(strArrays, lower))
				{
					this.Alert(strArrays1[1]);
				}
				else if (item.ContentLength > 0)
				{
					DateTime now = DateTime.Now;
					string str = string.Concat(now.ToString("yyyy-MM-dd-HH-mm-ss-ffff", DateTimeFormatInfo.InvariantInfo), lower);
					now = DateTime.Now;
					string str1 = now.ToString("yyyy年MM月", DateTimeFormatInfo.InvariantInfo);
					if (!string.IsNullOrEmpty(context.Request["FolderName"]))
					{
						str1 = context.Request["FolderName"];
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
							FolderID = ""
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
							string item1 = this.Request.Form["imgWidth"];
							string item2 = this.Request.Form["imgHeight"];
							string item3 = this.Request.Form["imgBorder"];
							string str3 = this.Request.Form["imgTitle"];
							string item4 = this.Request.Form["imgAlign"];
							string str4 = this.Request.Form["imgHspace"];
							string item5 = this.Request.Form["imgVspace"];
							string str5 = string.Concat(this.AppPath, "SysFolder/Common/FileDown.aspx?fileId=", str2);
							context.Response.Write(string.Format("{{\"error\":{0},\"url\":\"{1}\"}}", 0, str5));
						}
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						context.Response.Write(string.Format("{{\"error\":{0},\"message\":\"{1}\"}}", 1, exception.Message));
					}
				}
			}
			else
			{
				this.Alert(strArrays1[0]);
			}
		}

		private void ReturnImg(string FileUrl, string FileWidth, string FileHeight, string FileBorder, string FileTitle, string HSpace, string VSpace, string Align)
		{
			this.Response.Write("<html>");
			this.Response.Write("<head>");
			this.Response.Write("<title>上传成功</title>");
			this.Response.Write("<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\">");
			this.Response.Write("</head>");
			this.Response.Write("<body>");
			HttpResponse response = this.Response;
			string[] item = new string[] { "<script type=\"text/javascript\">parent.KE.plugin['image'].insert(\"", this.Request["id"], "\",\"", FileUrl, "\",\"", FileTitle, "\",\"", FileWidth, "\",\"", FileHeight, "\",\"", FileBorder, "\",\"", HSpace, "\",\"", VSpace, "\",\"", Align, "\");</script>" };
			response.Write(string.Concat(item));
			this.Response.Write("</body>");
			this.Response.Write("</html>");
		}
	}
}