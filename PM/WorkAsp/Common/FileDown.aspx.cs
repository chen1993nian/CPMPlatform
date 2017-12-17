using EIS.AppBase;
using EIS.DataModel.Access;
using EIS.DataModel.Model;
using EIS.DataModel.Service;
using EIS.Permission;
using WebBase.JZY.Tools;
using NLog;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
namespace EIS.Web.WorkAsp.Common
{
    public partial class FileDown : PageBase
    {
        private int int_0 = 0x2710;

        private string string_0 = "";

        public FileDown()
        {
            this.AutoRedirect = false;
        }

        private string method_0(string string_1, string string_2)
        {
            string str = "";
            switch (EIS.Permission.Utility.LoginChk(string_1, string_2))
            {
                case LoginInfoType.Allowed:
                    {
                        str = "";
                        break;
                    }
                case LoginInfoType.NotExist:
                    {
                        str = "用户不存在";
                        break;
                    }
                case LoginInfoType.WrongPwd:
                    {
                        str = "密码不正确";
                        break;
                    }
                case LoginInfoType.IsLocked:
                    {
                        str = "帐户被锁定";
                        break;
                    }
            }
            if (str == "")
            {
                WebTools.OnAuthenticated(string_1);
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool flag = false;
            if (!(string.IsNullOrEmpty(base.Request.Form["userName"]) ? true : string.IsNullOrEmpty(base.Request.Form["loginPass"])))
            {
                if (this.method_0(base.Request.Form["userName"], base.Request.Form["loginPass"]) != "")
                {
                    FormsAuthentication.RedirectToLoginPage();
                    base.Response.End();
                }
                flag = true;
            }
            else if (base.Request["loginkey"] != null)
            {
                string str = base.Request["loginkey"].ToString();
                string str1 = Security.Decrypt(base.Server.UrlDecode(str), base.CommonKey);
                Security.GetUrlPara(str1, "u");
                string urlPara = Security.GetUrlPara(str1, "t");
                this.string_0 = Security.GetUrlPara(str1, "fileId");
                if (!string.IsNullOrEmpty(urlPara))
                {
                    flag = true;
                }
            }
            if ((this.Logged ? false : !flag))
            {
                FormsAuthentication.RedirectToLoginPage();
                base.Response.End();
            }
            if (this.string_0 == "")
            {
                this.string_0 = base.GetParaValue("fileid");
            }
            string paraValue = base.GetParaValue("appId");
            base.GetParaValue("cache");
            _AppFile __AppFile = new _AppFile();
            AppFile appFile = new AppFile();
            if (this.string_0 != "")
            {
                appFile = __AppFile.GetModel(this.string_0);
                if (appFile == null)
                {
                    appFile = FileService.GetLastFileByAppId(this.string_0);
                }
            }
            else
            {
                if (paraValue == "")
                {
                    base.Response.Write("<script type='text/javascript'>window.close();</script>");
                    return;
                }
                appFile = FileService.GetLastFileByAppId(paraValue);
            }
            if (appFile == null)
            {
                base.Response.Write("<script>alert('未找到此文件，请联系管理员！')</script>");
            }
            else
            {
                Stream fileStream = null;
                string basePath = AppFilePath.GetBasePath(appFile.BasePath);
                if (File.Exists(string.Concat(basePath, appFile.FilePath)))
                {
                    StringCollection stringCollections = new StringCollection();
                    char[] chrArray = new char[] { '|' };
                    stringCollections.AddRange(".gif|.jpg|.png|.bmp".Split(chrArray));
                    base.Response.Charset = "UTF-8";
                    base.Response.AddHeader("Content-Type", "application/octet-stream");
                    base.Response.AddHeader("Content-Disposition", string.Concat("attachment;filename=\"", base.Server.UrlEncode(appFile.FactFileName), "\""));
                    base.Response.ContentType = AppSettings.Instance.MimeMap[appFile.FileType];
                    if (stringCollections.Contains(appFile.FileType.ToLower()))
                    {
                        base.Response.Expires = 0xa8c0;
                    }
                    try
                    {
                        try
                        {
                            fileStream = new FileStream(string.Concat(basePath, appFile.FilePath), FileMode.Open, FileAccess.Read, FileShare.Read);
                            long length = fileStream.Length;
                            base.Response.AddHeader("Content-Length", length.ToString());
                            byte[] numArray = new byte[this.int_0];
                            while (length > (long)0)
                            {
                                if (!base.Response.IsClientConnected)
                                {
                                    length = (long)-1;
                                }
                                else
                                {
                                    int num = fileStream.Read(numArray, 0, this.int_0);
                                    base.Response.OutputStream.Write(numArray, 0, num);
                                    base.Response.Flush();
                                    numArray = new byte[this.int_0];
                                    length = length - (long)num;
                                }
                            }
                        }
                        catch (Exception exception1)
                        {
                            Exception exception = exception1;
                            object[] fileSize = new object[] { appFile._AutoID, appFile.FileSize, appFile.FactFileName, base.EmployeeName };
                            string str2 = string.Concat(string.Format("{3}，{0}，{1}，{2}\\r\\n", fileSize), this.FormatException(base.Server.GetLastError(), ""));
                            base.WriteExceptionLog("错误", str2);
                            base.Response.Write(string.Concat("Error : ", exception.Message));
                        }
                    }
                    finally
                    {
                        if (fileStream != null)
                        {
                            fileStream.Close();
                        }
                    }
                }
                else
                {
                    this.fileLogger.Error("文件不存在：{0}", string.Concat(basePath, appFile.FilePath));
                    base.Response.Write("<script type='text/javascript'>alert('文件不存在，请联系管理员！');</script>");
                    base.Response.End();
                }
            }
        }
    }
}