using System;
using System.Collections;

namespace EIS.AppMail.MIME
{
	public class MimeCodeManager
	{
		private static Hashtable hashtable_0;

		private readonly static MimeCodeManager mimeCodeManager_0;

		public static MimeCodeManager Instance
		{
			get
			{
				return MimeCodeManager.mimeCodeManager_0;
			}
		}

		static MimeCodeManager()
		{
			MimeCodeManager.hashtable_0 = new Hashtable();
			MimeCodeManager.mimeCodeManager_0 = new MimeCodeManager();
		}

		private MimeCodeManager()
		{
			this.method_0();
		}

		public MimeCode GetCode(string name)
		{
			return (MimeCode)MimeCodeManager.hashtable_0[name.ToLower()];
		}

		private void method_0()
		{
			MimeCode mimeFieldCodeBase = new MimeFieldCodeBase();
			this.SetCode("Subject", mimeFieldCodeBase);
			this.SetCode("Comments", mimeFieldCodeBase);
			this.SetCode("Content-Description", mimeFieldCodeBase);
			mimeFieldCodeBase = new MimeFieldCodeAddress();
			this.SetCode("From", mimeFieldCodeBase);
			this.SetCode("To", mimeFieldCodeBase);
			this.SetCode("Resent-To", mimeFieldCodeBase);
			this.SetCode("Cc", mimeFieldCodeBase);
			this.SetCode("Resent-Cc", mimeFieldCodeBase);
			this.SetCode("Bcc", mimeFieldCodeBase);
			this.SetCode("Resent-Bcc", mimeFieldCodeBase);
			this.SetCode("Reply-To", mimeFieldCodeBase);
			this.SetCode("Resent-Reply-To", mimeFieldCodeBase);
			mimeFieldCodeBase = new MimeFieldCodeParameter();
			this.SetCode("Content-Type", mimeFieldCodeBase);
			this.SetCode("Content-Disposition", mimeFieldCodeBase);
			MimeCode mimeCode = new MimeCode();
			this.SetCode("7bit", mimeCode);
			this.SetCode("8bit", mimeCode);
			mimeCode = new MimeCodeBase64();
			this.SetCode("base64", mimeCode);
			mimeCode = new MimeCodeQP();
			this.SetCode("quoted-printable", mimeCode);
		}

		public void SetCode(string name, MimeCode code)
		{
			MimeCodeManager.hashtable_0.Add(name.ToLower(), code);
		}
	}
}