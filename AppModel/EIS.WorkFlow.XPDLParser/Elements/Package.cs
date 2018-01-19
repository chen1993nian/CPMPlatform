using EIS.WorkFlow.XPDL.Utility;
using System;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	public class Package : BaseElementWithIdName
	{
		private BaseElement baseElement_0;

		private BaseElement baseElement_1;

		private BaseElement baseElement_2;

		private BaseElement baseElement_3;

		private BaseElement baseElement_4;

		private BaseElement baseElement_5;

		private BaseElement baseElement_6;

		private BaseElement baseElement_7;

		private BaseElement baseElement_8;

		private BaseElement baseElement_9;

		private BaseElement baseElement_10;

		private BaseElement baseElement_11;

		private BaseElement baseElement_12;

		public Package(Node node)
		{
			base.SetNode(node);
		}

		public Applications GetApplications()
		{
			Applications child = (Applications)base.GetChild(this.baseElement_7, out this.baseElement_7, "Applications");
			return child;
		}

		public BizLogics GetBizLogics()
		{
			BizLogics child = (BizLogics)base.GetChild(this.baseElement_10, out this.baseElement_10, "BizLogics");
			return child;
		}

		public ConformanceClass GetConformanceClass()
		{
			ConformanceClass child = (ConformanceClass)base.GetChild(this.baseElement_2, out this.baseElement_2, "ConformanceClass");
			return child;
		}

		public DataFields GetDataFields()
		{
			DataFields child = (DataFields)base.GetChild(this.baseElement_8, out this.baseElement_8, "DataFields");
			return child;
		}

		public InstanceEventScripts GetEventScripts()
		{
			InstanceEventScripts child = (InstanceEventScripts)base.GetChild(this.baseElement_9, out this.baseElement_9, "EventScripts", "InstanceEventScripts");
			return child;
		}

		public ExtendedAttributes GetExtendedAttributes()
		{
			ExtendedAttributes child = (ExtendedAttributes)base.GetChild(this.baseElement_12, out this.baseElement_12, "ExtendedAttributes");
			return child;
		}

		public ExternalPackages GetExternalPackages()
		{
			ExternalPackages child = (ExternalPackages)base.GetChild(this.baseElement_4, out this.baseElement_4, "ExternalPackages");
			return child;
		}

		public PackageHeader GetPackageHeader()
		{
			PackageHeader child = (PackageHeader)base.GetChild(this.baseElement_0, out this.baseElement_0, "PackageHeader");
			return child;
		}

		public Participants GetParticipants()
		{
			Participants child = (Participants)base.GetChild(this.baseElement_6, out this.baseElement_6, "Participants");
			return child;
		}

		public RedefinableHeader GetRedefinableHeader()
		{
			RedefinableHeader child = (RedefinableHeader)base.GetChild(this.baseElement_1, out this.baseElement_1, "RedefinableHeader");
			return child;
		}

		public Script GetScript()
		{
			Script child = (Script)base.GetChild(this.baseElement_3, out this.baseElement_3, "Script");
			return child;
		}

		public TypeDeclarations GetTypeDeclarations()
		{
			TypeDeclarations child = (TypeDeclarations)base.GetChild(this.baseElement_5, out this.baseElement_5, "TypeDeclarations");
			return child;
		}

		public WorkflowProcesses GetWorkflowProcesses()
		{
			WorkflowProcesses child = (WorkflowProcesses)base.GetChild(this.baseElement_11, out this.baseElement_11, "WorkflowProcesses");
			return child;
		}
	}
}