using System;
using System.Collections;

namespace EIS.WorkFlow.XPDL.Utility
{
	public interface Node
	{
		string GetAttribute(string attrName);

		IDictionary GetAttributes();

		IList GetChildren();

		string GetName();

		string GetText();

		bool IsEmpty();

		IList SelectNodes(string xPath);

		Node SelectSingleNode(string xPath);

		string ToXML();
	}
}