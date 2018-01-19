using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class BizLogics : BaseElement
	{
		private IList ilist_0;

		public BizLogics()
		{
		}

		public BizLogic GetBizLogic(string eventName)
		{
			BizLogic bizLogic;
			if (null == this.ilist_0)
			{
				this.GetBizLogics();
			}
			foreach (BizLogic ilist0 in this.ilist_0)
			{
				if (!ilist0.GetEventName().Equals(eventName))
				{
					continue;
				}
				bizLogic = ilist0;
				return bizLogic;
			}
			bizLogic = null;
			return bizLogic;
		}

		public IList GetBizLogics()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(BizLogic), "BizLogic");
			return children;
		}
	}
}