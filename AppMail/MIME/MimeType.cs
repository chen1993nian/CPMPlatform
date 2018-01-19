using System;

namespace EIS.AppMail.MIME
{
	public class MimeType
	{
		public readonly static string[] TypeTable;

		public readonly static MimeType.MediaTypeCvt[] TypeCvtTable;

		static MimeType()
		{
			string[] strArrays = new string[] { "text", "image", "audio", "vedio", "application", "multipart", "message", null };
			MimeType.TypeTable = strArrays;
			MimeType.MediaTypeCvt[] mediaTypeCvt = new MimeType.MediaTypeCvt[] { new MimeType.MediaTypeCvt(MimeType.MediaType.MEDIA_APPLICATION, "xml", "xml"), new MimeType.MediaTypeCvt(MimeType.MediaType.MEDIA_APPLICATION, "msword", "doc"), new MimeType.MediaTypeCvt(MimeType.MediaType.MEDIA_APPLICATION, "rtf", "rtf"), new MimeType.MediaTypeCvt(MimeType.MediaType.MEDIA_APPLICATION, "vnd.ms-excel", "xls"), new MimeType.MediaTypeCvt(MimeType.MediaType.MEDIA_APPLICATION, "vnd.ms-powerpoint", "ppt"), new MimeType.MediaTypeCvt(MimeType.MediaType.MEDIA_APPLICATION, "pdf", "pdf"), new MimeType.MediaTypeCvt(MimeType.MediaType.MEDIA_APPLICATION, "zip", "zip"), new MimeType.MediaTypeCvt(MimeType.MediaType.MEDIA_IMAGE, "jpeg", "jpeg"), new MimeType.MediaTypeCvt(MimeType.MediaType.MEDIA_IMAGE, "jpeg", "jpg"), new MimeType.MediaTypeCvt(MimeType.MediaType.MEDIA_IMAGE, "gif", "gif"), new MimeType.MediaTypeCvt(MimeType.MediaType.MEDIA_IMAGE, "tiff", "tif"), new MimeType.MediaTypeCvt(MimeType.MediaType.MEDIA_IMAGE, "tiff", "tiff"), new MimeType.MediaTypeCvt(MimeType.MediaType.MEDIA_AUDIO, "basic", "wav"), new MimeType.MediaTypeCvt(MimeType.MediaType.MEDIA_AUDIO, "basic", "mp3"), new MimeType.MediaTypeCvt(MimeType.MediaType.MEDIA_VEDIO, "mpeg", "mpg"), new MimeType.MediaTypeCvt(MimeType.MediaType.MEDIA_VEDIO, "mpeg", "mpeg"), new MimeType.MediaTypeCvt(MimeType.MediaType.MEDIA_UNKNOWN, "", "") };
			MimeType.TypeCvtTable = mediaTypeCvt;
		}

		public MimeType()
		{
		}

		public enum MediaType
		{
			MEDIA_TEXT,
			MEDIA_IMAGE,
			MEDIA_AUDIO,
			MEDIA_VEDIO,
			MEDIA_APPLICATION,
			MEDIA_MULTIPART,
			MEDIA_MESSAGE,
			MEDIA_UNKNOWN
		}

		public struct MediaTypeCvt
		{
			public MimeType.MediaType nMediaType;

			public string pszSubType;

			public string pszFileExt;

			public MediaTypeCvt(MimeType.MediaType nMediaType, string pszSubType, string pszFileExt)
			{
				this.nMediaType = nMediaType;
				this.pszSubType = pszSubType;
				this.pszFileExt = pszFileExt;
			}
		}
	}
}