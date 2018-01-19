using System;
using System.Collections;
using System.IO;
using System.Text;

namespace EIS.AppMail.MIME
{
	public class MimeBody : MimeHeader
	{
		private ArrayList arrayList_0 = null;

		private string string_0;

		protected MimeBody()
		{
		}

		public MimeBody CreatePart(MimeBody parent)
		{
			MimeBody mimeBody;
			int num;
			if (this.arrayList_0 == null)
			{
				this.arrayList_0 = new ArrayList();
			}
			MimeBody mimeBody1 = new MimeBody();
			if (parent != null)
			{
				int num1 = this.arrayList_0.IndexOf(parent);
				if (num1 == -1)
				{
					num = this.arrayList_0.Add(mimeBody1);
					mimeBody = mimeBody1;
					return mimeBody;
				}
				this.arrayList_0.Insert(num1 + 1, mimeBody1);
				mimeBody = mimeBody1;
				return mimeBody;
			}
			num = this.arrayList_0.Add(mimeBody1);
			mimeBody = mimeBody1;
			return mimeBody;
		}

		public MimeBody CreatePart()
		{
			return this.CreatePart(null);
		}

		public void DeleteAllPart()
		{
			this.arrayList_0.RemoveRange(0, this.arrayList_0.Count);
		}

		public void ErasePart(MimeBody ChildPart)
		{
			this.arrayList_0.Remove(ChildPart);
		}

		public void GetBodyPartList(ArrayList BodyList)
		{
			if (BodyList == null)
			{
				throw new ArgumentNullException();
			}
			if (base.GetMediaType() == MimeType.MediaType.MEDIA_MULTIPART)
			{
				BodyList.Add(this);
				for (int i = 0; i < this.arrayList_0.Count; i++)
				{
					((MimeBody)this.arrayList_0[i]).GetBodyPartList(BodyList);
				}
			}
			else
			{
				BodyList.Add(this);
			}
		}

		public void GetMessage(MimeMessage aMimeMessage)
		{
			if (aMimeMessage == null)
			{
				throw new ArgumentNullException();
			}
			aMimeMessage.LoadBody(this.string_0);
		}

		public string GetText()
		{
			string transferEncoding = base.GetTransferEncoding();
			if (transferEncoding == null)
			{
				transferEncoding = "7bit";
			}
			MimeCode code = MimeCodeManager.Instance.GetCode(transferEncoding);
			code.Charset = base.GetCharset();
			return code.DecodeToString(this.string_0);
		}

		public bool IsAttachment()
		{
			return base.GetName() != null;
		}

		public bool IsMessage()
		{
			return base.GetMediaType() == MimeType.MediaType.MEDIA_MESSAGE;
		}

		public bool IsMultiPart()
		{
			return base.GetMediaType() == MimeType.MediaType.MEDIA_MULTIPART;
		}

		public bool IsText()
		{
			return base.GetMediaType() == MimeType.MediaType.MEDIA_TEXT;
		}

		public void LoadBody(string strData)
		{
			if (strData == null)
			{
				throw new ArgumentNullException();
			}
			int num = strData.IndexOf("\r\n\r\n");
			base.LoadHead(strData.Substring(0, num + 2));
			int num1 = num + 4;
			if (MimeType.MediaType.MEDIA_MULTIPART != base.GetMediaType())
			{
				this.string_0 = strData.Substring(num1, strData.Length - num1);
				int num2 = this.string_0.IndexOf("\r\n\r\n");
				if (num2 > -1)
				{
					this.string_0 = this.string_0.Substring(0, num2);
				}
			}
			else
			{
				string boundary = base.GetBoundary();
				if (null != boundary)
				{
					string str = string.Concat("--", boundary);
					string str1 = string.Concat(str, "--");
					int length = strData.IndexOf(str, num1);
					if (length != -1)
					{
						int length1 = strData.IndexOf(str1, num1);
						if (length1 == -1)
						{
							length1 = strData.Length;
						}
						if (length > num1)
						{
							this.string_0 = strData.Substring(num1, length - num1);
						}
						while (length < length1)
						{
							length = length + str.Length + 2;
							int num3 = strData.IndexOf(str, length);
							if (num3 != -1)
							{
								MimeBody mimeBody = this.CreatePart();
								mimeBody.LoadBody(strData.Substring(length, num3 - length));
							}
							length = num3;
						}
					}
				}
			}
		}

		public void ReadFromFile(string filePathName)
		{
			string str;
			if (filePathName == null)
			{
				throw new ArgumentNullException();
			}
			StreamReader streamReader = new StreamReader(filePathName);
			Stream baseStream = streamReader.BaseStream;
            byte[] numArray = new byte[baseStream.Length];
			baseStream.Read(numArray, 0, (int)baseStream.Length);
			string transferEncoding = base.GetTransferEncoding();
			if (transferEncoding == null)
			{
				transferEncoding = "base64";
				base.SetTransferEncoding(transferEncoding);
			}
			MimeCode code = MimeCodeManager.Instance.GetCode(transferEncoding);
			code.Charset = base.GetCharset();
			this.string_0 = string.Concat(code.EncodeFromBytes(numArray), "\r\n");
			int num = filePathName.LastIndexOf('\\');
			str = (num == -1 ? filePathName : filePathName.Substring(num + 1, filePathName.Length - num - 1));
			base.SetName(str);
			streamReader.Close();
			streamReader = null;
		}

		public void SetMessage(MimeMessage aMimeMessage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			aMimeMessage.StoreBody(stringBuilder);
			this.string_0 = stringBuilder.ToString();
			base.SetContentType("message/rfc822");
		}

		public void SetText(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException();
			}
			string transferEncoding = base.GetTransferEncoding();
			if (transferEncoding == null)
			{
				transferEncoding = "7bit";
				base.SetTransferEncoding(transferEncoding);
			}
			MimeCode code = MimeCodeManager.Instance.GetCode(transferEncoding);
			code.Charset = base.GetCharset();
			this.string_0 = string.Concat(code.EncodeFromString(text), "\r\n");
			base.SetContentType("text/plain");
			base.SetCharset(code.Charset);
		}

		public void StoreBody(StringBuilder stringBuilder_0)
		{
			if (stringBuilder_0 == null)
			{
				throw new ArgumentNullException();
			}
			this.StoreHead(stringBuilder_0);
			stringBuilder_0.Append(this.string_0);
			if (MimeType.MediaType.MEDIA_MULTIPART == base.GetMediaType())
			{
				string boundary = base.GetBoundary();
				for (int i = 0; i < this.arrayList_0.Count; i++)
				{
					stringBuilder_0.AppendFormat("--{0}\r\n", boundary);
					((MimeBody)this.arrayList_0[i]).StoreBody(stringBuilder_0);
				}
				stringBuilder_0.AppendFormat("--{0}--\r\n", boundary);
			}
		}

		public int WriteToFile(string filePathName)
		{
			if (filePathName == null)
			{
				throw new ArgumentNullException();
			}
			StreamWriter streamWriter = new StreamWriter(filePathName);
			Stream baseStream = streamWriter.BaseStream;
			string transferEncoding = base.GetTransferEncoding();
			if (transferEncoding == null)
			{
				transferEncoding = "7bit";
			}
			MimeCode code = MimeCodeManager.Instance.GetCode(transferEncoding);
			code.Charset = base.GetCharset();
			byte[] bytes = code.DecodeToBytes(this.string_0);
			baseStream.Write(bytes, 0, (int)bytes.Length);
			streamWriter.Close();
			streamWriter = null;
			return (int)bytes.Length;
		}
	}
}