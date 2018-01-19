using System;
using System.Collections;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class InstanceEventScripts : BaseElement
	{
		private IList ilist_0;

		public InstanceEventScripts()
		{
		}

		public InstanceEventScript GetEventScript(string eventName)
		{
			InstanceEventScript instanceEventScript;
			if (null == this.ilist_0)
			{
				this.GetEventScripts();
			}
			foreach (InstanceEventScript ilist0 in this.ilist_0)
			{
				if (!ilist0.GetEventName().Equals(eventName))
				{
					continue;
				}
				instanceEventScript = ilist0;
				return instanceEventScript;
			}
			instanceEventScript = null;
			return instanceEventScript;
		}

		public IList GetEventScripts()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(InstanceEventScript), "EventScript");
			return children;
		}
	}
}