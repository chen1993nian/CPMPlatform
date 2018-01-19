using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.SessionState;
using ThoughtWorks.QRCode.Codec;

namespace EIS.AppCommon
{
	public class QRCode : IHttpHandler, IRequiresSessionState
	{
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public QRCode()
		{
		}

		public void ProcessRequest(HttpContext context)
		{
			string item = "";
			if (string.IsNullOrEmpty(context.Request["c"]))
			{
				if (context.Session["QRCode"] != null)
				{
					goto Label1;
				}
				return;
			}
			else
			{
				item = context.Request["c"];
			}
		Label2:
			int num = 2;
			int num1 = 7;
			if (!string.IsNullOrEmpty(context.Request["s"]))
			{
				num = Convert.ToInt32(context.Request["s"]);
			}
			if (!string.IsNullOrEmpty(context.Request["v"]))
			{
				num1 = Convert.ToInt32(context.Request["v"]);
			}
			QRCodeEncoder qRCodeEncoder = new QRCodeEncoder()
			{
				QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
				QRCodeScale = num,
				QRCodeVersion = num1,
				QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
			};
			Bitmap bitmap = qRCodeEncoder.Encode(item);
			MemoryStream memoryStream = new MemoryStream();
			bitmap.Save(memoryStream, ImageFormat.Png);
			context.Response.Clear();
			context.Response.ContentType = "image/Png";
			context.Response.BinaryWrite(memoryStream.GetBuffer());
			bitmap.Dispose();
			memoryStream.Dispose();
			memoryStream.Close();
			context.Response.End();
			return;
		Label1:
			item = context.Session["QRCode"].ToString();
			goto Label2;
		}
	}
}