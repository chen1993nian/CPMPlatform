using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class RedefinableHeader : BaseElement
	{
		private string string_0;

		private string string_1;

		private string string_2;

		private string string_3;

		private BaseElement baseElement_0;

		private string string_4;

		private static Hashtable hashtable_0;

		public RedefinableHeader()
		{
			if (RedefinableHeader.hashtable_0 == null)
			{
				RedefinableHeader.hashtable_0 = new Hashtable()
				{
					{ "UNDER_REVISION", RedefinableHeaderPublicationStatus.UNDER_REVISION },
					{ "RELEASED", RedefinableHeaderPublicationStatus.RELEASED },
					{ "UNDER_TEST", RedefinableHeaderPublicationStatus.UNDER_TEST }
				};
			}
		}

		public string GetAuthor()
		{
			return base.GetChildText(this.string_0, out this.string_0, "Author");
		}

		public string GetCodepage()
		{
			return base.GetChildText(this.string_2, out this.string_2, "Codepage");
		}

		public string GetCountrykey()
		{
			return base.GetChildText(this.string_3, out this.string_3, "Countrykey");
		}

		public RedefinableHeaderPublicationStatus GetRedefinableHeaderPublicationStatus()
		{
			if (!this.IsPublicationStatusSpecified())
			{
				throw new ObjectNotFound("PublicationStatus is not specified");
			}
			return (RedefinableHeaderPublicationStatus)RedefinableHeader.hashtable_0[this.string_4];
		}

		public Responsibles GetResponsibles()
		{
			Responsibles child = (Responsibles)base.GetChild(this.baseElement_0, out this.baseElement_0, "Responsibles");
			return child;
		}

		public string GetVersion()
		{
			return base.GetChildText(this.string_1, out this.string_1, "Version");
		}

		public bool IsPublicationStatusSpecified()
		{
			return base.GetAttribute(this.string_4, out this.string_4, "PublicationStatus") != "";
		}
	}
}