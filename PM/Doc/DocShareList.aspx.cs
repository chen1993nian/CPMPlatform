using AjaxPro;
using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web.Doc
{
	public partial class DocShareList : PageBase
	{
		public string folderId = "";

		public string PasteHide = "";

		public string folderPId = "";

		public string shareLimit = "0";

		

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void DeleteFile(string[] idList, string[] folderList)
		{
			string str = HttpContext.Current.Session["EmployeeId"].ToString();
			StringBuilder stringBuilder = new StringBuilder();
			_AppFile __AppFile = new _AppFile();
			string[] strArrays = idList;
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				string str1 = strArrays[i];
				AppFile model = __AppFile.GetModel(str1);
				if (model._UserName != str)
				{
					stringBuilder.AppendFormat("{0},", model.FactFileName);
				}
				else
				{
					__AppFile.Delete(str1);
				}
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length = stringBuilder.Length - 1;
				throw new Exception(string.Concat(stringBuilder.ToString(), "\r\n，上述文件非本人所有，不能删除"));
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AjaxPro.Utility.RegisterTypeForAjax(typeof(DocShareList));
			this.folderId = base.GetParaValue("folderId");
			if (this.Session["FileSelect"] != null && this.Session["FileSelect"].ToString().Length > 0)
			{
				this.PasteHide = "0";
			}
			FolderInfo folderModel = FolderService.GetFolderModel(this.folderId);
			if (folderModel != null)
			{
				this.folderPId = folderModel.FolderPID;
				this.shareLimit = FolderService.GetParentShareLimit(this.folderId);
			}
		}

		[AjaxMethod]    // JustDecompile was unable to locate the assembly where attribute parameters types are defined. Generating parameters values is impossible.
		public void SaveSelect(string actFlag, string[] idList, string[] folderList)
		{
			StringCollection stringCollections = new StringCollection();
			stringCollections.Add(actFlag);
			stringCollections.AddRange(idList);
			stringCollections.Add("$");
			stringCollections.AddRange(folderList);
			HttpContext.Current.Session["FileSelect"] = EIS.AppBase.Utility.GetJoinString(stringCollections);
		}
	}
}