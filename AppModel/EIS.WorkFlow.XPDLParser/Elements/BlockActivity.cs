using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class BlockActivity : BaseElement
	{
		private string string_0;

		public BlockActivity()
		{
		}

		public string GetId()
		{
			return base.GetAttribute(this.string_0, out this.string_0, "BlockId");
		}
	}
}