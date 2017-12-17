using EIS.AppBase;
using EIS.Permission.Model;
using EIS.Permission.Service;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.UI;

namespace EIS.Web.SysFolder.Common
{
    public partial class SignImage : PageBase
    {
        private int int_0 = 0x2710;

        public SignImage()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Employee model = EmployeeService.GetModel(base.GetParaValue("uid"));
            if (model != null && model.SignId != "")
            {
                string basePath = AppFilePath.GetBasePath(model.BasePath);
                string str = string.Concat(basePath, model.SignId);
                Stream fileStream = null;
                try
                {
                    try
                    {
                        string fileName = Path.GetFileName(model.SignId);
                        string lower = Path.GetExtension(fileName).ToLower();
                        StringCollection stringCollections = new StringCollection();
                        char[] chrArray = new char[] { '|' };
                        stringCollections.AddRange(".gif|.jpg|.png|.bmp".Split(chrArray));
                        base.Response.Charset = "UTF-8";
                        base.Response.AddHeader("Content-Type", "application/octet-stream");
                        base.Response.AddHeader("Content-Disposition", string.Concat("attachment;filename=\"", base.Server.UrlEncode(fileName), "\""));
                        base.Response.ContentType = AppSettings.Instance.MimeMap[lower];
                        if (stringCollections.Contains(lower))
                        {
                            base.Response.Expires = 0xa8c0;
                        }
                        fileStream = new FileStream(str, FileMode.Open, FileAccess.Read, FileShare.Read);
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
                        base.WriteExceptionLog("错误", "读取个人签名进出错！");
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
        }
    }
}