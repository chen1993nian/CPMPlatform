using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Activity : BaseElementWithIdName
	{
		private string string_2;

		private string string_3;

		private BaseElement baseElement_0;

		private static string[] string_4;

		private BaseElement baseElement_1;

		private BaseElement baseElement_2;

		private BaseElement baseElement_3;

		private string string_5;

		private IList ilist_0;

		private BaseElement baseElement_4;

		private string string_6;

		private string string_7;

		private BaseElement baseElement_5;

		private BaseElement baseElement_6;

		private string string_8;

		private string string_9;

		private string string_10;

		private string string_11;

		private string string_12;

		private string string_13;

		private string string_14;

		private string string_15;

		private string string_16;

		private BaseElement baseElement_7;

		private BaseElement baseElement_8;

		private BaseElement baseElement_9;

		public string SelectPath
		{
			get;
			set;
		}

		static Activity()
		{
			Activity.string_4 = new string[] { "Route", "Implementation", "BlockActivity" };
		}

		public Activity()
		{
		}

		public object GetActivityType()
		{
			object obj = base.ChoiceChild(this.baseElement_0, out this.baseElement_0, Activity.string_4);
			return obj;
		}

		public AlertBack GetAlertBack()
		{
			AlertBack child = (AlertBack)base.GetChild(this.baseElement_9, out this.baseElement_9, "AlertBack");
			return child;
		}

		public AlertIn GetAlertIn()
		{
			AlertIn child = (AlertIn)base.GetChild(this.baseElement_7, out this.baseElement_7, "AlertIn");
			return child;
		}

		public AlertOut GetAlertOut()
		{
			AlertOut child = (AlertOut)base.GetChild(this.baseElement_8, out this.baseElement_8, "AlertOut");
			return child;
		}

		public string GetAppHtml()
		{
			return base.GetChildText(this.string_8, out this.string_8, "AppHtml");
		}

		public string GetBusiSql()
		{
			return base.GetChildText(this.string_9, out this.string_9, "BusiSql");
		}

		public bool GetCanReturn()
		{
			return this.GetSafeOption("CanReturn") == "1";
		}

		public string GetCode()
		{
			return base.GetAttribute(this.string_11, out this.string_11, "Code");
		}

		public IList GetDeadlines()
		{
			IList children = base.GetChildren(this.ilist_0, out this.ilist_0, typeof(Deadline), "Deadline");
			return children;
		}

		public string GetDealStrategy()
		{
			if (this.string_13 == null)
			{
				Performers performers = this.GetPerformers();
				if (performers != null)
				{
					this.string_13 = performers.GetStrategy();
				}
				this.string_13 = (string.IsNullOrEmpty(this.string_13) ? "2|2" : this.string_13);
			}
			return this.string_13;
		}

		public string GetDescription()
		{
			return base.GetChildText(this.string_2, out this.string_2, "Description");
		}

		public string GetDirectScope()
		{
			string str = "";
			try
			{
				ExtendedAttribute extendedAttributeByName = this.GetExtendedAttributes().GetExtendedAttributeByName("DirectScope");
				str = (extendedAttributeByName != null ? extendedAttributeByName.GetValue() : "");
			}
			catch
			{
			}
			return str;
		}

		public string GetDocumentation()
		{
			return base.GetChildText(this.string_7, out this.string_7, "Documentation");
		}

		public string GetExtendedAttribute(string attrName)
		{
			string str = "";
			try
			{
				ExtendedAttribute extendedAttributeByName = this.GetExtendedAttributes().GetExtendedAttributeByName(attrName);
				str = (extendedAttributeByName != null ? extendedAttributeByName.GetValue() : "");
			}
			catch
			{
			}
			return str;
		}

		public ExtendedAttributes GetExtendedAttributes()
		{
			ExtendedAttributes child = (ExtendedAttributes)base.GetChild(this.baseElement_6, out this.baseElement_6, "ExtendedAttributes");
			return child;
		}

		public FinishMode GetFinishMode()
		{
			FinishMode child = (FinishMode)base.GetChild(this.baseElement_3, out this.baseElement_3, "FinishMode");
			return child;
		}

		public string GetIcon()
		{
			return base.GetChildText(this.string_6, out this.string_6, "Icon");
		}

		public string GetJoinType()
		{
			if (this.string_15 == null)
			{
				TransitionRestriction item = (TransitionRestriction)this.GetTransitionRestrictions().GetTransitionRestrictions()[0];
				if (item.GetJoin().IsTypeSpecified())
				{
					this.string_15 = item.GetJoin().GetJoinType().ToString();
				}
			}
			return this.string_15;
		}

		public string GetLimit()
		{
			return base.GetChildText(this.string_3, out this.string_3, "Limit");
		}

		public ActivityType GetNodeType()
		{
			this.string_16 = base.GetChildText(this.string_16, out this.string_16, "Icon");
			return (ActivityType)int.Parse(this.string_16);
		}

		public string GetPerformer()
		{
			return "";
		}

		public Performers GetPerformers()
		{
			Performers child = (Performers)base.GetChild(this.baseElement_1, out this.baseElement_1, "Performers");
			return child;
		}

		public string GetPriority()
		{
			return base.GetChildText(this.string_5, out this.string_5, "Priority");
		}

		public string GetRollBackScope()
		{
			string str = "";
			try
			{
				ExtendedAttribute extendedAttributeByName = this.GetExtendedAttributes().GetExtendedAttributeByName("RollBackScope");
				str = (extendedAttributeByName != null ? extendedAttributeByName.GetValue() : "");
			}
			catch
			{
			}
			return str;
		}

		public string GetSafeOption(string optionName)
		{
			string str = "";
			try
			{
				ExtendedAttribute extendedAttributeByName = this.GetExtendedAttributes().GetExtendedAttributeByName(optionName);
				str = (extendedAttributeByName != null ? extendedAttributeByName.GetValue() : "");
			}
			catch
			{
			}
			return str;
		}

		public string GetSignStrategy()
		{
			if (this.string_12 == null)
			{
				this.string_12 = this.GetExtendedAttributes().GetExtendedAttributeByName("SignStrategy").GetValue();
			}
			return this.string_12;
		}

		public SimulationInformation GetSimulationInformation()
		{
			SimulationInformation child = (SimulationInformation)base.GetChild(this.baseElement_4, out this.baseElement_4, "SimulationInformation");
			return child;
		}

		public string GetSplitType()
		{
			if (this.string_14 == null)
			{
				TransitionRestriction item = (TransitionRestriction)this.GetTransitionRestrictions().GetTransitionRestrictions()[0];
				if (item.GetSplit().IsTypeSpecified())
				{
					this.string_14 = item.GetSplit().GetSplitType().ToString();
				}
			}
			return this.string_14;
		}

		public StartMode GetStartMode()
		{
			StartMode child = (StartMode)base.GetChild(this.baseElement_2, out this.baseElement_2, "StartMode");
			return child;
		}

		public string GetStyleName()
		{
			return base.GetChildText(this.string_10, out this.string_10, "StyleName");
		}

		public TransitionRestrictions GetTransitionRestrictions()
		{
			TransitionRestrictions child = (TransitionRestrictions)base.GetChild(this.baseElement_5, out this.baseElement_5, "TransitionRestrictions");
			return child;
		}

		public bool IsDecisionNode()
		{
			bool flag;
			if (this.GetNodeType() == ActivityType.Sign)
			{
				flag = true;
			}
			else if (this.GetNodeType() != ActivityType.Normal)
			{
				flag = false;
			}
			else
			{
				string extendedAttribute = this.GetExtendedAttribute("Decision");
				flag = ((extendedAttribute == "" ? false : !(extendedAttribute == "1")) ? false : true);
			}
			return flag;
		}
	}
}