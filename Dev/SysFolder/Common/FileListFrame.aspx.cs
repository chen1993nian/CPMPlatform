using AjaxPro;
using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using EIS.Permission.Access;
using EIS.Permission.Model;
using SevenZip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.WebBase.SysFolder.Common
{
	public partial class FileListFrame : PageBase
	{
		public string fileList = "";

		public string appId = "";

		public string appName = "";

		public string read = "";

		public string limit = "0";

        public string ext = "*.*";

		public string multi = "true";

		public string folder = "";

		public string fromDoc = "";

	

		[AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string CopyFiles(string fileIdList, string appId, string appName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] strArrays = fileIdList.Split(new char[] { ',' });
			_AppFile __AppFile = new _AppFile();
			string[] strArrays1 = strArrays;
			for (int i = 0; i < (int)strArrays1.Length; i++)
			{
				string str = strArrays1[i];
				if (str.Length != 0)
				{
					AppFile model = __AppFile.GetModel(str);
					model._AutoID = Guid.NewGuid().ToString();
					model._UserName = HttpContext.Current.Session["EmployeeId"].ToString();
					model._CreateTime = DateTime.Now;
					model._UpdateTime = DateTime.Now;
					model.AppId = appId;
					model.AppName = appName;
					model.FileCode = "";
					model.FolderID = "";
					model.Inherit = "1";
					__AppFile.Add(model);
					stringBuilder.AppendFormat("{0}|{1},", model._AutoID, model.FactFileName);
				}
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length = stringBuilder.Length - 1;
			}
			return stringBuilder.ToString();
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void DeleteFile(string fileId)
		{
			_AppFile __AppFile = new _AppFile();
			if (__AppFile.GetModel(fileId)._UserName != base.EmployeeID)
			{
				throw new Exception("只有附件上传本人才能删除附件");
			}
			__AppFile.Delete(fileId);
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void PackFiles(string appId)
		{
			List<AppFile> files = (new FileService()).GetFiles("", appId);
			string str = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "\\5LianSoft");
			if (!Directory.Exists(string.Concat(str, "\\", appId)))
			{
				Directory.CreateDirectory(string.Concat(str, "\\", appId));
			}
			foreach (AppFile file in files)
			{
				string basePath = AppFilePath.GetBasePath(file.BasePath);
				if (!File.Exists(string.Concat(basePath, file.FilePath)))
				{
					continue;
				}
				string str1 = string.Concat(basePath, file.FilePath);
				string[] strArrays = new string[] { str, "\\", appId, "\\", file.FactFileName };
				File.Copy(str1, string.Concat(strArrays), true);
			}
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			SevenZipCompressor sevenZipCompressor = new SevenZipCompressor();
			if (IntPtr.Size != 8)
			{
				SevenZipBase.SetLibraryPath(string.Concat(baseDirectory, "\\bin\\SevenZip_7z_x86.dll"));
			}
			else
			{
				SevenZipBase.SetLibraryPath(string.Concat(baseDirectory, "\\bin\\SevenZip_7z_x64.dll"));
			}
			sevenZipCompressor.CompressDirectory(string.Concat(str, "\\", appId), string.Concat(str, "\\", appId, ".7z"));
		}

		[AjaxMethod(HttpSessionStateRequirement.Read)]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public string PackFilesPercent(string appId)
		{
			return (HttpContext.Current.Session["PackedPercent"] == null ? "0" : HttpContext.Current.Session["PackedPercent"].ToString());
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			object[] fileSize;
			AjaxPro.Utility.RegisterTypeForAjax(typeof(FileListFrame));
			this.appId = base.GetParaValue("appId");
			this.appName = base.GetParaValue("appName");
			this.fromDoc = (base.GetParaValue("fromDoc") == "0" ? "display:none;" : "");
			string paraValue = base.GetParaValue("set");
			if (paraValue.Length > 0)
			{
				string[] strArrays = paraValue.Split("|".ToCharArray());
                this.ext = strArrays[0];
				this.limit = (strArrays[1] == "" ? "0" : string.Concat(strArrays[1], "MB"));
				this.multi = (strArrays[2] == "1" ? "true" : "false");
				if ((int)strArrays.Length > 3 && strArrays[3] == "1")
				{
					this.folder = this.appName;
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(base.GetParaValue("read")) && base.GetParaValue("read") != "0")
			{
				this.read = "display:none;";
			}
			foreach (AppFile file in (new FileService()).GetFiles(this.appName, this.appId))
			{
				Employee model = (new _Employee()).GetModel(file._UserName);
				if (base.Request.Path.ToLower().IndexOf("filelistmini.aspx") != -1)
				{
					StringBuilder stringBuilder1 = stringBuilder;
					fileSize = new object[] { base.CryptPara(string.Concat("fileId=", file._AutoID)), file.FactFileName, null, null, null, null };
					fileSize[2] = (model == null ? "" : model.EmployeeName);
					fileSize[3] = (file._CreateTime.Year == DateTime.Now.Year ? file._CreateTime.ToString("M月d日 HH:mm") : file._CreateTime.ToString("yy年M月d日 HH:mm"));
					fileSize[4] = file.FileSize / 0x400;
					fileSize[5] = (this.read == "" ? string.Concat("&nbsp;<a class='dellink' href=\"javascript:delFile('", file._AutoID, "')\">[删除]</a>") : "");
					stringBuilder1.AppendFormat("<a class='filelink' title='点击文件名即可下载〔{2}&nbsp;&nbsp;{3}&nbsp;&nbsp;{4}K〕' href='FileDown.aspx?para={0}' target='_blank'>{1}</a>{5}&nbsp;", fileSize);
				}
				else
				{
					StringBuilder stringBuilder2 = stringBuilder;
					fileSize = new object[] { base.CryptPara(string.Concat("fileId=", file._AutoID)), file.FactFileName, null, null, null, null };
					fileSize[2] = (model == null ? "" : model.EmployeeName);
					fileSize[3] = (file._CreateTime.Year == DateTime.Now.Year ? file._CreateTime.ToString("M月d日 HH:mm") : file._CreateTime.ToString("yy年M月d日 HH:mm"));
					fileSize[4] = file.FileSize / 0x400;
					fileSize[5] = (this.read == "" ? string.Concat("&nbsp;<a class='dellink' href=\"javascript:delFile('", file._AutoID, "')\">[删除]</a>") : "");
					stringBuilder2.AppendFormat("<div class='fileitem'><a class='filelink' title='点击文件名即可下载' href='FileDown.aspx?para={0}' target='_blank'>{1}</a>{5}&nbsp;〔{2}&nbsp;&nbsp;{3}&nbsp;&nbsp;{4}K〕</div>", fileSize);
				}
			}
			this.fileList = stringBuilder.ToString();
		}
	}
}