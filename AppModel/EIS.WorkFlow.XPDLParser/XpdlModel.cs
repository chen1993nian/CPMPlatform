using EIS.WorkFlow.XPDL.Utility;
using EIS.WorkFlow.XPDLParser.Elements;
using System;

namespace EIS.WorkFlow.XPDLParser
{
	public class XpdlModel
	{
		public static Package GetPackage(string fileName)
		{
			return new Package(NodeFactory.CreateNode(fileName));
		}

		public static Package GetPackageFromText(string xpdlText)
		{
			return new Package(NodeFactory.CreateNodeFromText(xpdlText));
		}
	}
}