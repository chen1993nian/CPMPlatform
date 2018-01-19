using System;

namespace EIS.AppMail.MIME
{
	public class MimeFieldCodeAddress : MimeFieldCodeBase
	{
		public MimeFieldCodeAddress()
		{
		}

		protected override char[] GetDelimeterChars()
		{
			return new char[] { '(', ')', '<', '>', '\"' };
		}

		protected override char[] GetFoldChars()
		{
			return new char[] { ',', ':' };
		}

		protected override bool IsAutoFold()
		{
			return true;
		}

		protected override bool IsNeedDelimeter()
		{
			return true;
		}
	}
}