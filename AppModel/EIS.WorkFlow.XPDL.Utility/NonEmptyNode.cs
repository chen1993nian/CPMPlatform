using System;
using System.Collections;
using System.Xml;

namespace EIS.WorkFlow.XPDL.Utility
{
	[Serializable]
	public class NonEmptyNode : Node
	{
		private IDictionary attrs;

		private IList children;

		private string text;

		private XmlNode xml_node;

		private XmlNamespaceManager xmlns;

		private string nsprefix;

		public NonEmptyNode(XmlNode xmlNode)
		{
			this.xml_node = xmlNode;
			this.nsprefix = "owmpre";
			this.xmlns = new XmlNamespaceManager(new NameTable());
			this.xmlns.AddNamespace(this.nsprefix, this.xml_node.NamespaceURI);
		}

		public string GetAttribute(string attrName)
		{
			object item = this.GetAttributes()[attrName];
			return (item == null ? "" : (string)item);
		}

		public IDictionary GetAttributes()
		{
			if (this.attrs == null)
			{
				this.attrs = this.method_1(this.xml_node.Attributes);
			}
			return this.attrs;
		}

		public IList GetChildren()
		{
			if (this.children == null)
			{
				this.children = this.method_0(this.xml_node.ChildNodes);
			}
			return this.children;
		}

		public string GetName()
		{
			return this.xml_node.Name;
		}

		public string GetText()
		{
			if (this.text == null)
			{
				this.text = this.xml_node.InnerText ?? "";
			}
			return this.text;
		}

		public bool IsEmpty()
		{
			return false;
		}

		private IList method_0(XmlNodeList xmlNodeList_0)
		{
			ArrayList arrayLists = new ArrayList();
			foreach (XmlNode xmlNodeList0 in xmlNodeList_0)
			{
				arrayLists.Add(NodeFactory.CreateNode(xmlNodeList0));
			}
			return arrayLists;
		}

		private IDictionary method_1(XmlAttributeCollection xmlAttributeCollection_0)
		{
			IDictionary hashtables = new Hashtable();
			foreach (XmlNode xmlAttributeCollection0 in xmlAttributeCollection_0)
			{
				hashtables.Add(xmlAttributeCollection0.Name, xmlAttributeCollection0.Value);
			}
			return hashtables;
		}

		public IList SelectNodes(string xPath)
		{
			IList lists = this.method_0(this.xml_node.SelectNodes(string.Concat(this.nsprefix, ":", xPath), this.xmlns));
			return lists;
		}

		public Node SelectSingleNode(string xPath)
		{
			Node node = NodeFactory.CreateNode(this.xml_node.SelectSingleNode(string.Concat(this.nsprefix, ":", xPath), this.xmlns));
			return node;
		}

		public string ToXML()
		{
			return this.xml_node.OuterXml;
		}
	}
}