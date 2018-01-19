using System;
using System.Drawing;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace EIS.AppCommon
{
	public class VryImgGen
	{
		private const double PI = 3.14159265358979;

		private const double PI2 = 6.28318530717959;

		public static string ChineseChars;

		protected readonly static string EnglishOrNumChars;

		private Random rnd;

		private int length = 6;

		private int fontSize = 32;

		private int padding = 2;

		private bool chaos = true;

		private Color chaosColor = Color.LightGray;

		private Color backgroundColor = Color.White;

		private Color[] colors = new Color[] { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

		private string[] fonts = new string[] { "Arial", "Georgia" };

		public Color BackgroundColor
		{
			get
			{
				return this.backgroundColor;
			}
			set
			{
				this.backgroundColor = value;
			}
		}

		public bool Chaos
		{
			get
			{
				return this.chaos;
			}
			set
			{
				this.chaos = value;
			}
		}

		public Color ChaosColor
		{
			get
			{
				return this.chaosColor;
			}
			set
			{
				this.chaosColor = value;
			}
		}

		public Color[] Colors
		{
			get
			{
				return this.colors;
			}
			set
			{
				this.colors = value;
			}
		}

		public string[] Fonts
		{
			get
			{
				return this.fonts;
			}
			set
			{
				this.fonts = value;
			}
		}

		public int FontSize
		{
			get
			{
				return this.fontSize;
			}
			set
			{
				this.fontSize = value;
			}
		}

		public int Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}

		public int Padding
		{
			get
			{
				return this.padding;
			}
			set
			{
				this.padding = value;
			}
		}

		static VryImgGen()
		{
			VryImgGen.ChineseChars = string.Empty;
			VryImgGen.EnglishOrNumChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
		}

		public VryImgGen()
		{
			this.rnd = new Random((int)DateTime.Now.Ticks);
		}

		protected char CreateEnOrNumChar()
		{
			char englishOrNumChars = VryImgGen.EnglishOrNumChars[this.rnd.Next(0, VryImgGen.EnglishOrNumChars.Length)];
			return englishOrNumChars;
		}

		public Bitmap CreateImage(string code)
		{
			int i;
			int fontSize = this.FontSize;
			int padding = fontSize + this.Padding;
			int length = code.Length * padding + 4 + this.Padding * 2;
			int num = fontSize * 2 + this.Padding * 2;
			Bitmap bitmap = new Bitmap(215, 37);
			Graphics graphic = Graphics.FromImage(bitmap);
            graphic.FillRectangle(new SolidBrush(Color.LightGreen), 0, 0, 215, 37);
            Font font = new Font(FontFamily.GenericSerif, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
            Color[] arrColor = { Color.Blue, Color.Red, Color.Yellow, Color.Orange, Color.Black, Color.DarkGray };
            if (this.Chaos)
			{
				Pen pen = new Pen(this.ChaosColor, 0f);
				int length1 = this.Length * 12;
				for (i = 0; i < length1; i++)
				{
					int num1 = this.rnd.Next(bitmap.Width);
					int num2 = this.rnd.Next(bitmap.Height);
					graphic.DrawRectangle(pen, num1, num2, 1, 1);
				}
			}

            Random r = new Random();
			for (i = 0; i < code.Length; i++)
			{
				int x = i * padding+5;
                int y = r.Next(-5, 5);
                graphic.DrawString(code.Substring(i, 1), font, new SolidBrush(arrColor[r.Next(0, 5)]), x, y);
			}
			graphic.Dispose();
			return bitmap;
		}

		public string CreateNumCharCode(int codeLen)
		{
            char[] englishOrNumChars = new char[codeLen];
            for (int i = 0; i < codeLen; i++)
            {
                englishOrNumChars[i] = VryImgGen.EnglishOrNumChars[this.rnd.Next(0, 9)];
            }
            return new string(englishOrNumChars, 0, (int)englishOrNumChars.Length);
		}

		public string CreateVerifyCode(int codeLen, int zhCharsCount)
		{
			int i;
			char[] chrArray = new char[codeLen];
			for (i = 0; i < zhCharsCount; i++)
			{
				int num = this.rnd.Next(0, codeLen);
				if (chrArray[num] != '\0')
				{
					i--;
				}
				else
				{
					chrArray[num] = this.CreateZhChar();
				}
			}
			for (i = 0; i < codeLen; i++)
			{
				if (chrArray[i] == '\0')
				{
					chrArray[i] = this.CreateEnOrNumChar();
				}
			}
			return new string(chrArray, 0, (int)chrArray.Length);
		}

		public string CreateVerifyCode()
		{
			return this.CreateVerifyCode(this.Length, 0);
		}

		protected char CreateZhChar()
		{
			char chineseChars;
			if (VryImgGen.ChineseChars.Length <= 0)
			{
				byte[] numArray = new byte[] { (byte)this.rnd.Next(176, 248), (byte)this.rnd.Next(161, 255) };
				string str = Encoding.GetEncoding("gb2312").GetString(numArray);
				chineseChars = str[0];
			}
			else
			{
				chineseChars = VryImgGen.ChineseChars[this.rnd.Next(0, VryImgGen.ChineseChars.Length)];
			}
			return chineseChars;
		}

		public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
		{
			Bitmap bitmap = new Bitmap(srcBmp.Width, srcBmp.Height);
			Graphics graphic = Graphics.FromImage(bitmap);
			graphic.FillRectangle(new SolidBrush(Color.White), 0, 0, bitmap.Width, bitmap.Height);
			graphic.Dispose();
			double num = (bXDir ? (double)bitmap.Height : (double)bitmap.Width);
			for (int i = 0; i < bitmap.Width; i++)
			{
				for (int j = 0; j < bitmap.Height; j++)
				{
					double num1 = Math.Sin((bXDir ? 6.28318530717959 * (double)j / num : 6.28318530717959 * (double)i / num) + dPhase);
					int num2 = 0;
					int num3 = 0;
					num2 = (bXDir ? i + (int)(num1 * dMultValue) : i);
					num3 = (bXDir ? j : j + (int)(num1 * dMultValue));
					Color pixel = srcBmp.GetPixel(i, j);
					if ((num2 < 0 || num2 >= bitmap.Width || num3 < 0 ? false : num3 < bitmap.Height))
					{
						bitmap.SetPixel(num2, num3, pixel);
					}
				}
			}
			return bitmap;
		}
	}
}