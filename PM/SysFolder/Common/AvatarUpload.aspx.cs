using AjaxPro;
using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.Common
{
	public partial class AvatarUpload : PageBase
	{
		
		public string fileList = "";

		public string appId = "";

		public string appName = "";

		public string read = "";

		public string limit = "0";

        public string ext = "*.*";

		public string multi = "true";

		public string folder = "";

		public string fileId = "";

		public string imgAlt = "";

		public string imgCss = "";

		public string filePath = "";

		
		[AjaxMethod(HttpSessionStateRequirement.Read)]   // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void DeleteFile(string fileId)
		{
			_AppFile __AppFile = new _AppFile();
			if (__AppFile.GetModel(fileId)._UserName != base.EmployeeID)
			{
				throw new Exception("只有附件上传本人才能删除附件");
			}
			__AppFile.Delete(fileId);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(FileListFrame));
			this.appId = base.GetParaValue("appId");
			this.appName = base.GetParaValue("appName");
			string paraValue = base.GetParaValue("set");
			string str = "";
			if (paraValue.Length > 0)
			{
				string[] strArrays = paraValue.Split("|".ToCharArray());
                this.ext = strArrays[0];
				this.limit = (strArrays[1] == "" ? "0" : string.Concat(strArrays[1], "MB"));
				this.imgAlt = strArrays[2];
				this.imgCss = strArrays[3];
				if ((int)strArrays.Length > 4 && strArrays[4] == "1")
				{
					this.folder = this.appName;
				}
				if ((int)strArrays.Length > 5)
				{
					str = strArrays[5];
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(base.GetParaValue("read")) && base.GetParaValue("read") != "0")
			{
				this.read = "display:none;";
			}
			IList<AppFile> files = (new FileService()).GetFiles(this.appName, this.appId);
			if (files.Count > 0)
			{
				AppFile item = files[files.Count - 1];
				if (File.Exists(string.Concat(AppFilePath.GetBasePath(item.BasePath), item.FilePath)))
				{
					this.fileId = item._AutoID;
					this.filePath = string.Concat("FileDown.aspx?fileId=", this.fileId);
				}
			}
			if ((this.fileId != "" ? false : str.Length > 0))
			{
				this.filePath = string.Concat(base.AppPath, str);
			}
		}
	}
}