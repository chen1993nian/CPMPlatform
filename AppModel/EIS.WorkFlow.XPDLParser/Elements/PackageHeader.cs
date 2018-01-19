using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class PackageHeader : BaseElement
	{
		private string string_0;

		private string string_1;

		private string string_2;

		private string string_3;

		private string string_4;

		private string string_5;

		private string string_6;

		public PackageHeader()
		{
		}

		public string GetCostUnit()
		{
			return base.GetChildText(this.string_6, out this.string_6, "CostUnits");
		}

		public string GetCreated()
		{
			return base.GetChildText(this.string_2, out this.string_2, "Created");
		}

		public string GetDescription()
		{
			return base.GetChildText(this.string_3, out this.string_3, "Description");
		}

		public string GetDocumentation()
		{
			return base.GetChildText(this.string_4, out this.string_4, "Documentation");
		}

		public string GetPriorityUnit()
		{
			return base.GetChildText(this.string_5, out this.string_5, "PriorityUnits");
		}

		public string GetVendor()
		{
			return base.GetChildText(this.string_1, out this.string_1, "Vendor");
		}

		public string GetXPDLVersion()
		{
			return base.GetChildText(this.string_0, out this.string_0, "XPDLVersion");
		}
	}
}