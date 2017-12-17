using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EIS.Web.SysFolder.Common
{
    public partial class WebOfficeSave : PageBase
    {
     

        protected void Page_Load(object sender, EventArgs e)
        {
            string appFileSavePath = AppSettings.Instance.AppFileSavePath;
            if (base.Request.Files.Count > 0)
            {
                for (int i = 0; i < base.Request.Files.Count; i++)
                {
                    HttpPostedFile item = base.Request.Files[i];
                    if (item.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(item.FileName);
                        string lower = Path.GetExtension(fileName).ToLower();
                        if ((lower != "" ? false : !string.IsNullOrEmpty(base.Request["filedot"])))
                        {
                            lower = base.Request["filedot"];
                        }
                        DateTime now = DateTime.Now;
                        string str = string.Concat(now.ToString("yyyy-MM-dd-HH-mm-ss-ffff", DateTimeFormatInfo.InvariantInfo), lower);
                        now = DateTime.Now;
                        string str1 = now.ToString("yyyy年MM月", DateTimeFormatInfo.InvariantInfo);
                        if (!string.IsNullOrEmpty(base.Request["FolderName"]))
                        {
                            str1 = base.Request["FolderName"];
                        }
                        try
                        {
                            _AppFile __AppFile = new _AppFile();
                            AppFile model = __AppFile.GetModel(base.Request["FileId"]);
                            if (model != null)
                            {
                                model._UpdateTime = DateTime.Now;
                                model.FileSize = item.ContentLength;
                                model.FileType = lower;
                                __AppFile.Update(model);
                                item.SaveAs(string.Concat(appFileSavePath, model.FilePath));
                            }
                            else
                            {
                                model = new AppFile()
                                {
                                    _AutoID = base.Request["FileId"],
                                    _UserName = Utility.GetSession("EmployeeID").ToString(),
                                    _OrgCode = Utility.GetSession("DeptWbs").ToString(),
                                    _CreateTime = DateTime.Now,
                                    _UpdateTime = DateTime.Now,
                                    _IsDel = 0,
                                    FileName = str,
                                    FactFileName = str,
                                    FilePath = string.Concat(str1, "\\", str),
                                    BasePath = AppSettings.Instance.AppFileBaseCode,
                                    FileSize = item.ContentLength,
                                    FileType = lower,
                                    FolderID = base.Request["FileId"],
                                    AppName = base.Request["FolderName"]
                                };
                                __AppFile.Add(model);
                                if (!Directory.Exists(string.Concat(appFileSavePath, str1)))
                                {
                                    Directory.CreateDirectory(string.Concat(appFileSavePath, str1));
                                }
                                item.SaveAs(string.Concat(appFileSavePath, str1, "\\", str));
                            }
                            if (model._AutoID != "")
                            {
                                base.Response.Write(string.Format("{{\"status\":\"{0}\",\"name\":\"{1}\",\"hash\":\"{2}\"}}", 1, fileName, "0"));
                            }
                        }
                        catch (Exception exception1)
                        {
                            Exception exception = exception1;
                            base.Response.Write(string.Format("{{\"status\":\"{0}\",\"error\":\"{1}\"}}", 0, exception.Message));
                        }
                    }
                }
            }
        }
    }
}