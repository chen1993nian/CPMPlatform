using EIS.WorkFlow.XPDL.Utility;
using System;
using System.Collections;
using System.Reflection;

namespace EIS.WorkFlow.XPDLParser.Elements
{
	[Serializable]
	public class BaseElement
	{
		private Node node = NodeFactory.CreateNode();

		public BaseElement()
		{
		}

		protected BaseElement ChoiceChild(BaseElement child, out BaseElement refChild, string[] xPath)
		{
			BaseElement baseElement;
			if (child != null)
			{
				refChild = child;
				baseElement = refChild;
			}
			else
			{
				string[] strArrays = xPath;
				int num = 0;
				while (num < (int)strArrays.Length)
				{
					string str = strArrays[num];
					if (!this.node.SelectSingleNode(str).IsEmpty())
					{
						refChild = (BaseElement)Type.GetType(string.Concat("EIS.WorkFlow.XPDLParser.Elements.", str)).GetConstructor(new Type[0]).Invoke(new object[0]);
						refChild.SetNode(this.node.SelectSingleNode(str));
						baseElement = refChild;
						return baseElement;
					}
					else
					{
						num++;
					}
				}
				throw new ObjectNotFound(string.Concat("Object Not Found In ", xPath.ToString()));
			}
			return baseElement;
		}

		protected string GetAttribute(string attr, out string refAttr, string xPath)
		{
			return this.GetAttribute(attr, out refAttr, xPath, "");
		}

		protected string GetAttribute(string attr, out string refAttr, string xPath, string defaultValue)
		{
			if (attr != null)
			{
				refAttr = attr;
			}
			else
			{
				refAttr = (string)this.node.GetAttributes()[xPath];
				if (refAttr == null)
				{
					refAttr = defaultValue;
				}
			}
			return refAttr;
		}

		protected BaseElement GetChild(BaseElement child, out BaseElement refChild, string xPath)
		{
			if (child != null)
			{
				refChild = child;
			}
			else
			{
				refChild = (BaseElement)Type.GetType(string.Concat("EIS.WorkFlow.XPDLParser.Elements.", xPath)).GetConstructor(new Type[0]).Invoke(new object[0]);
				refChild.SetNode(this.node.SelectSingleNode(xPath));
			}
			return refChild;
		}

		protected BaseElement GetChild(BaseElement child, out BaseElement refChild, string xPath, string typeName)
		{
			if (child != null)
			{
				refChild = child;
			}
			else
			{
				refChild = (BaseElement)Type.GetType(string.Concat("EIS.WorkFlow.XPDLParser.Elements.", typeName)).GetConstructor(new Type[0]).Invoke(new object[0]);
				refChild.SetNode(this.node.SelectSingleNode(xPath));
			}
			return refChild;
		}

		protected IList GetChildren(IList children, out IList refChildren, Type type, string xPath)
		{
			if (children != null)
			{
				refChildren = children;
			}
			else
			{
				IList lists = this.node.SelectNodes(xPath);
				ArrayList arrayLists = new ArrayList();
				foreach (Node node in lists)
				{
					BaseElement baseElement = (BaseElement)type.GetConstructor(new Type[0]).Invoke(new object[0]);
					baseElement.SetNode(node);
					arrayLists.Add(baseElement);
				}
				refChildren = arrayLists;
			}
			return refChildren;
		}

		protected IList GetChildren(IList children, out IList refChildren)
		{
			if (children != null)
			{
				refChildren = children;
			}
			else
			{
				refChildren = this.node.GetChildren();
			}
			return refChildren;
		}

		protected IList GetChildrenTexts()
		{
			IList lists = null;
			this.GetChildren(lists, out lists);
			IList arrayLists = new ArrayList();
			foreach (Node node in lists)
			{
				arrayLists.Add(node.GetText());
			}
			return arrayLists;
		}

		protected string GetChildText(string text, out string refText, string childName)
		{
			if (text != null)
			{
				refText = text;
			}
			else
			{
				refText = this.node.SelectSingleNode(string.Concat(childName, "/text()[1]")).GetText();
			}
			return refText;
		}

		protected string GetText()
		{
			return this.node.GetText();
		}

		protected void SetNode(Node node)
		{
			this.node = node;
		}
	}
}