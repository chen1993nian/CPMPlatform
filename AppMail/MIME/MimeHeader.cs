using System;
using System.Collections;
using System.IO;
using System.Text;

namespace EIS.AppMail.MIME
{
	public class MimeHeader
	{
		protected ArrayList m_listFields = new ArrayList();

		protected MimeHeader()
		{
		}

		protected MimeField FindField(string pszFieldName)
		{
			MimeField mimeField;
			int num = 0;
			while (true)
			{
				if (num < this.m_listFields.Count)
				{
					MimeField item = (MimeField)this.m_listFields[num];
					if (item.GetName().ToLower() == pszFieldName.ToLower())
					{
						mimeField = item;
						break;
					}
					else
					{
						num++;
					}
				}
				else
				{
					mimeField = null;
					break;
				}
			}
			return mimeField;
		}

		public string GetBoundary()
		{
			return this.GetParameter("Content-Type", "boundary");
		}

		public string GetCharset()
		{
			return this.GetParameter("Content-Type", "charset");
		}

		public string GetContentMainType()
		{
			string str;
			string contentType = this.GetContentType();
			if (null == contentType)
			{
				str = "text";
			}
			else
			{
				int num = contentType.IndexOf('/', 0);
				str = (num == -1 ? contentType : contentType.Substring(0, num));
			}
			return str;
		}

		public string GetContentSubType()
		{
			string str;
			string contentType = this.GetContentType();
			if (null == contentType)
			{
				str = "text";
			}
			else
			{
				int num = contentType.IndexOf('/', 0);
				if (num == -1)
				{
					str = "";
				}
				else
				{
					int num1 = contentType.IndexOf(';', num + 1);
					if (num1 == -1)
					{
						num1 = contentType.IndexOf('\r', num + 1);
					}
					str = contentType.Substring(num + 1, num1 - num - 1);
				}
			}
			return str;
		}

		public string GetContentType()
		{
			return this.GetFieldValue("Content-Type");
		}

		public string GetDiscription()
		{
			return this.GetFieldValue("Content-Description");
		}

		public string GetDisposition()
		{
			return this.GetFieldValue("Content-Disposition");
		}

		public MimeField GetField(string pszFieldName)
		{
			MimeField mimeField;
			MimeField mimeField1 = this.FindField(pszFieldName);
			if (mimeField1 != null)
			{
				mimeField = mimeField1;
			}
			else
			{
				mimeField = null;
			}
			return mimeField;
		}

		public string GetFieldCharset(string pszFieldName)
		{
			string charset;
			MimeField field = this.GetField(pszFieldName);
			if (field != null)
			{
				charset = field.GetCharset();
			}
			else
			{
				charset = null;
			}
			return charset;
		}

		public string GetFieldValue(string pszFieldName)
		{
			string value;
			MimeField field = this.GetField(pszFieldName);
			if (field != null)
			{
				value = field.GetValue();
			}
			else
			{
				value = null;
			}
			return value;
		}

		public string GetFilename()
		{
			return this.GetParameter("Content-Disposition", "filename");
		}

		public MimeType.MediaType GetMediaType()
		{
			MimeType.MediaType mediaType;
			string lower = this.GetContentMainType().ToLower();
			int num = 0;
			while (true)
			{
				if (MimeType.TypeTable[num] == null)
				{
					mediaType = (MimeType.MediaType)num;
					break;
				}
				else if (lower.IndexOf(MimeType.TypeTable[num], 0) != -1)
				{
					mediaType = (MimeType.MediaType)num;
					break;
				}
				else
				{
					num++;
				}
			}
			return mediaType;
		}

		public string GetName()
		{
			return this.GetParameter("Content-Type", "name");
		}

		public string GetParameter(string pszFieldName, string pszAttr)
		{
			string parameter;
			MimeField field = this.GetField(pszFieldName);
			if (field != null)
			{
				parameter = field.GetParameter(pszAttr);
			}
			else
			{
				parameter = null;
			}
			return parameter;
		}

		public string GetTransferEncoding()
		{
			return this.GetFieldValue("Content-Transfer-Encoding");
		}

		protected void LoadHead(string strData)
		{
			bool flag;
			if (strData == null)
			{
				throw new ArgumentNullException();
			}
			string str = "";
			StringReader stringReader = new StringReader(strData);
			try
			{
				string str1 = stringReader.ReadLine();
				str = string.Concat(str1, "\r\n");
				while (str1 != null)
				{
					str1 = stringReader.ReadLine();
					if (str1 == null)
					{
						flag = true;
					}
					else
					{
						flag = (str1[0] == ' ' ? false : str1[0] != '\t');
					}
					if (flag)
					{
						MimeField mimeField = new MimeField();
						mimeField.LoadField(str);
						this.m_listFields.Add(mimeField);
						str = string.Concat(str1, "\r\n");
					}
					else
					{
						str = string.Concat(str, str1, "\r\n");
					}
				}
			}
			finally
			{
				stringReader.Close();
				stringReader = null;
			}
		}

		public void SetBoundary(string pszBoundary)
		{
			if (pszBoundary == null)
			{
				Random random = new Random((int)DateTime.Now.Ticks);
				string str = random.Next().ToString();
				int num = random.Next();
				pszBoundary = string.Concat("__=_Part_Boundary_", str, "_", num.ToString());
			}
			MimeField field = this.GetField("Content-Type");
			if (field == null)
			{
				field = new MimeField();
				field.SetName("Content-Type");
				field.SetValue("multipart/mixed");
				field.SetParameter("boundary", string.Concat("\"", pszBoundary, "\""));
				this.m_listFields.Add(field);
			}
			else
			{
				if (field.GetValue().IndexOf("multipart", 0, 9) == -1)
				{
					field.SetValue("multipart/mixed");
				}
				field.SetParameter("boundary", string.Concat("\"", pszBoundary, "\""));
			}
		}

		public void SetCharset(string pszCharset)
		{
			MimeField field = this.GetField("Content-Type");
			if (field != null)
			{
				field.SetParameter("charset", string.Concat("\"", pszCharset, "\""));
			}
			else
			{
				field = new MimeField();
				field.SetName("Content-Type");
				field.SetValue("text/plain");
				field.SetParameter("charset", string.Concat("\"", pszCharset, "\""));
				this.m_listFields.Add(field);
			}
		}

		public void SetContentType(string pszValue, string pszCharset)
		{
			this.SetFieldValue("Content-Type", pszValue, pszCharset);
		}

		public void SetContentType(string pszValue)
		{
			this.SetContentType(pszValue, null);
		}

		public void SetDescription(string pszValue, string pszCharset)
		{
			this.SetFieldValue("Content-Description", pszValue, pszCharset);
		}

		public void SetDisposition(string pszValue, string pszCharset)
		{
			this.SetFieldValue("Content-Disposition", pszValue, pszCharset);
		}

		public void SetFieldCharset(string pszFieldName, string pszFieldCharset)
		{
			MimeField field = this.GetField(pszFieldName);
			if (field == null)
			{
				field = new MimeField();
				field.SetCharset(pszFieldCharset);
				this.m_listFields.Add(field);
			}
			else
			{
				field.SetCharset(pszFieldCharset);
			}
		}

		public void SetFieldValue(string pszFieldName, string pszFieldValue, string pszFieldCharset)
		{
			MimeField field = this.GetField(pszFieldName);
			if (field == null)
			{
				field = new MimeField();
				field.SetName(pszFieldName);
				field.SetValue(pszFieldValue);
				if (pszFieldCharset != null)
				{
					field.SetCharset(pszFieldCharset);
				}
				this.m_listFields.Add(field);
			}
			else
			{
				field.SetValue(pszFieldValue);
				if (pszFieldCharset != null)
				{
					field.SetCharset(pszFieldCharset);
				}
			}
		}

		public void SetName(string pszName)
		{
			MimeField field = this.GetField(pszName);
			if (field != null)
			{
				field.SetParameter("name", string.Concat("\"", pszName, "\""));
			}
			else
			{
				field = new MimeField();
				int num = pszName.LastIndexOf('.');
				string typeTable = "application/octet-stream";
				string str = pszName.Substring(num + 1, pszName.Length - num - 1);
				int num1 = 0;
				while (true)
				{
					if (MimeType.TypeCvtTable[num1].nMediaType == MimeType.MediaType.MEDIA_UNKNOWN)
					{
						break;
					}
					else if (MimeType.TypeCvtTable[num1].pszFileExt == str)
					{
						typeTable = MimeType.TypeTable[(int)MimeType.TypeCvtTable[num1].nMediaType];
						typeTable = string.Concat(typeTable, '/');
						typeTable = string.Concat(typeTable, MimeType.TypeCvtTable[num1].pszSubType);
						break;
					}
					else
					{
						num1++;
					}
				}
				field.SetName("Content-Type");
				field.SetValue(typeTable);
				field.SetParameter("name", string.Concat("\"", pszName, "\""));
				this.m_listFields.Add(field);
			}
		}

		public bool SetParameter(string pszFieldName, string pszAttr, string pszValue)
		{
			bool flag;
			MimeField field = this.GetField(pszFieldName);
			if (field == null)
			{
				flag = false;
			}
			else
			{
				field.SetParameter(pszAttr, pszValue);
				flag = true;
			}
			return flag;
		}

		public void SetTransferEncoding(string pszValue)
		{
			this.SetFieldValue("Content-Transfer-Encoding", pszValue, null);
		}

		protected virtual void StoreHead(StringBuilder stringBuilder_0)
		{
			if (stringBuilder_0 == null)
			{
				throw new ArgumentNullException();
			}
			for (int i = 0; i < this.m_listFields.Count; i++)
			{
				((MimeField)this.m_listFields[i]).Store(stringBuilder_0);
			}
			stringBuilder_0.Append("\r\n");
		}
	}
}