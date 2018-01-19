using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Condition : BaseElement
	{
		private IList ilist_0;

		private string string_0;

		private string string_1;

		private static Hashtable hashtable_0;

		public Condition()
		{
			if (Condition.hashtable_0 == null)
			{
				Condition.hashtable_0 = new Hashtable()
				{
					{ "AGREE", ConditionType.AGREE },
					{ "REJECT", ConditionType.REJECT },
					{ "CONDITION", ConditionType.CONDITION },
					{ "DEFAULTEXCEPTION", ConditionType.DEFAULTEXCEPTION },
					{ "EXCEPTION", ConditionType.EXCEPTION },
					{ "OTHERWISE", ConditionType.OTHERWISE }
				};
			}
		}

		public ConditionType GetConditionType()
		{
			if (!this.IsTypeSpecified())
			{
				throw new ObjectNotFound("ConditionType is not specified");
			}
			return (ConditionType)Condition.hashtable_0[this.string_1];
		}

		public string GetConditionTypeString()
		{
			string str;
			str = (!this.IsTypeSpecified() ? "" : base.GetAttribute(this.string_1, out this.string_1, "Type").Trim());
			return str;
		}

		public string GetConnectionId()
		{
			return base.GetAttribute(this.string_0, out this.string_0, "ConnectionId");
		}

		public new string GetText()
		{
			return base.GetText();
		}

		public IList GetXpressions()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(Xpression), "Xpression");
			return children;
		}

		public bool IsTypeSpecified()
		{
			return base.GetAttribute(this.string_1, out this.string_1, "Type") != "";
		}
	}
}