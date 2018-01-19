using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class TableStyle : AppModelBase
	{
		public string _CompanyId
		{
			get;
			set;
		}

		public string CompiledHtml
		{
			get;
			set;
		}

		public string DetailHtml
		{
			get;
			set;
		}

		public string FormHtml
		{
			get;
			set;
		}

		public string FormHtml2
		{
			get;
			set;
		}

		public string Memo
		{
			get;
			set;
		}

		public string PrintHtml
		{
			get;
			set;
		}

		public int StyleIndex
		{
			get;
			set;
		}

		public string StyleName
		{
			get;
			set;
		}

		public string TableName
		{
			get;
			set;
		}

		public TableStyle()
		{
		}

		public TableStyle(XmlElement xmlElement_0)
		{
			base._AutoID = Guid.NewGuid().ToString();
			base._UserName = "";
			base._OrgCode = "";
			base._IsDel = 0;
			base._CreateTime = DateTime.Now;
			base._UpdateTime = DateTime.Now;
			this.TableName = xmlElement_0.GetAttribute("TableName");
			this.StyleIndex = int.Parse(xmlElement_0.GetAttribute("StyleIndex"));
			this.StyleName = xmlElement_0.GetAttribute("StyleName");
			this.FormHtml = xmlElement_0.SelectSingleNode("FormHtml").InnerText;
			this.FormHtml2 = xmlElement_0.SelectSingleNode("FormHtml2").InnerText;
			this.PrintHtml = xmlElement_0.SelectSingleNode("PrintHtml").InnerText;
			this.DetailHtml = xmlElement_0.SelectSingleNode("DetailHtml").InnerText;
		}

		public XmlElement ToXml(XmlDocument xmlDoc)
		{
			XmlElement xmlElement = xmlDoc.CreateElement("TableStyle");
			xmlElement.SetAttribute("StyleIndex", this.StyleIndex.ToString());
			xmlElement.SetAttribute("StyleName", this.StyleName);
			xmlElement.SetAttribute("TableName", this.TableName);
			XmlElement xmlElement1 = xmlDoc.CreateElement("FormHtml");
			xmlElement1.InnerXml = string.Format("<![CDATA[{0}]]>", Utility.TransferCDATA(this.FormHtml));
			xmlElement.AppendChild(xmlElement1);
			XmlElement xmlElement2 = xmlDoc.CreateElement("FormHtml2");
			xmlElement2.InnerXml = string.Format("<![CDATA[{0}]]>", Utility.TransferCDATA(this.FormHtml2));
			xmlElement.AppendChild(xmlElement2);
			XmlElement xmlElement3 = xmlDoc.CreateElement("PrintHtml");
			xmlElement3.InnerXml = string.Format("<![CDATA[{0}]]>", Utility.TransferCDATA(this.PrintHtml));
			xmlElement.AppendChild(xmlElement3);
			XmlElement xmlElement4 = xmlDoc.CreateElement("DetailHtml");
			xmlElement4.InnerXml = string.Format("<![CDATA[{0}]]>", Utility.TransferCDATA(this.DetailHtml));
			xmlElement.AppendChild(xmlElement4);
			return xmlElement;
		}
	}
}