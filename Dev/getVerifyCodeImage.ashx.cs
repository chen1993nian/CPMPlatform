using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace EIS.Studio
{
    /// <summary>
    /// getVerifyCodeImage 的摘要说明
    /// </summary>
    public class getVerifyCodeImage : IHttpHandler ,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/gif";
            int intLength = 6;                          //长度
            string strIdentify = "Validate_Identify";   //随机字串存储键值，以便存储到Session中

            Bitmap b = new Bitmap(180, 50);
            Graphics g = Graphics.FromImage(b);
            g.FillRectangle(new SolidBrush(Color.LightGreen), 0, 0, 180, 50);
            Font font = new Font(FontFamily.GenericSerif, 32, FontStyle.Bold, GraphicsUnit.Pixel);
            Random r = new Random();

            Color[] arrColor = { Color.Blue, Color.Green, Color.SeaShell, Color.MidnightBlue, Color.Black, Color.DarkGray };

            //合法随机显示字符列表
            string strLetters = "1234567890";
            StringBuilder s = new StringBuilder();

            //将随机生成的字符串绘制到图片上
            for (int i = 0; i < intLength; i++)
            {
                s.Append(strLetters.Substring(r.Next(0, strLetters.Length - 1), 1));
                int y = r.Next(0, 20);
                g.DrawString(s[s.Length - 1].ToString(), font, new SolidBrush(arrColor[r.Next(0, 5)]), i * 26, y);
            }

            //生成干扰线条
            Pen pen = new Pen(new SolidBrush(Color.Red), 1);
            for (int i = 0; i < 10; i++)
            {
                g.DrawLine(pen, new Point(r.Next(-10, 180), r.Next(-10, 50)), new Point(r.Next(-10, 180), r.Next(-10, 50)));
            }
            b.Save(context.Response.OutputStream, ImageFormat.Gif);
            context.Session[strIdentify] = s.ToString(); //先保存在Session中，验证与用户输入是否一致
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}