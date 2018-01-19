using System;
using System.Xml;

namespace EIS.WorkFlow.XPDL.Utility
{
	public class NodeFactory
	{
		private static Node node_0;

		static NodeFactory()
		{
			NodeFactory.node_0 = new EmptyNode();
		}

		public static Node CreateNode(string fileName)
		{
			Node node0;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(fileName);
			if (xmlDocument.DocumentElement == null)
			{
				node0 = NodeFactory.node_0;
			}
			else
			{
				node0 = new NonEmptyNode(xmlDocument.DocumentElement);
			}
			return node0;
		}

		public static Node CreateNode(XmlNode xmlNode)
		{
			Node node0;
			if (xmlNode == null)
			{
				node0 = NodeFactory.node_0;
			}
			else
			{
				node0 = new NonEmptyNode(xmlNode);
			}
			return node0;
		}

		public static Node CreateNode()
		{
			return NodeFactory.node_0;
		}

		public static Node CreateNodeFromText(string xmlText)
		{
			Node node0;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xmlText);
			if (xmlDocument.DocumentElement == null)
			{
				node0 = NodeFactory.node_0;
			}
			else
			{
				node0 = new NonEmptyNode(xmlDocument.DocumentElement);
			}
			return node0;
		}

		public static Node CreateNodeFromXmlDocument(XmlDocument xmlDocument_0)
		{
			Node node0;
			if (xmlDocument_0.DocumentElement == null)
			{
				node0 = NodeFactory.node_0;
			}
			else
			{
				node0 = new NonEmptyNode(xmlDocument_0.DocumentElement);
			}
			return node0;
		}
	}
}