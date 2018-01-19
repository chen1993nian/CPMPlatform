using EIS.AppBase.Config;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.SessionState;

namespace EIS.AppCommon
{
	public class VerifyCode : IHttpHandler, IRequiresSessionState
	{
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public VerifyCode()
		{
		}

		public void ProcessRequest(HttpContext context)
		{
			VryImgGen vryImgGen = new VryImgGen();
			string str = vryImgGen.CreateVerifyCode(6, 0);
			if (SysConfig.GetConfig("VerifyCode_Show").ItemValue == "数字")
			{
				str = vryImgGen.CreateNumCharCode(6);
			}
			context.Session["VerifyCode"] = str;
			Bitmap bitmap = vryImgGen.CreateImage(str);
			MemoryStream memoryStream = new MemoryStream();
			bitmap.Save(memoryStream, ImageFormat.Png);
			context.Response.Clear();
			context.Response.ContentType = "image/Png";
			context.Response.BinaryWrite(memoryStream.GetBuffer());
			bitmap.Dispose();
			memoryStream.Dispose();
			memoryStream.Close();
			context.Response.End();
		}
	}
}