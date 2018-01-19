using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class FieldStyle : AppModelBase
	{
		public string FieldDValue
		{
			get;
			set;
		}

		public string FieldDValueType
		{
			get;
			set;
		}

		public string FieldHeight
		{
			get;
			set;
		}

		public int FieldInDisp
		{
			get;
			set;
		}

		public string FieldInDispStyle
		{
			get;
			set;
		}

		public string FieldInDispStyleName
		{
			get;
			set;
		}

		public string FieldInDispStyleTxt
		{
			get;
			set;
		}

		public string FieldName
		{
			get;
			set;
		}

		public int FieldNull
		{
			get;
			set;
		}

		public int FieldRead
		{
			get;
			set;
		}

		public string FieldWidth
		{
			get;
			set;
		}

		public int StyleIndex
		{
			get;
			set;
		}

		public string TableName
		{
			get;
			set;
		}

		public FieldStyle()
		{
		}

		public FieldStyle(XmlElement xmlElement_0)
		{
			base._AutoID = xmlElement_0.GetAttribute("_AutoID");
			base._UserName = "";
			base._OrgCode = "";
			base._IsDel = 0;
			base._CreateTime = DateTime.Now;
			base._UpdateTime = DateTime.Now;
			this.TableName = xmlElement_0.GetAttribute("TableName");
			this.StyleIndex = int.Parse(xmlElement_0.GetAttribute("StyleIndex"));
			this.FieldName = xmlElement_0.GetAttribute("FieldName");
			this.FieldInDisp = int.Parse(xmlElement_0.GetAttribute("FieldInDisp"));
			this.FieldRead = int.Parse(xmlElement_0.GetAttribute("FieldRead"));
			this.FieldNull = int.Parse(xmlElement_0.GetAttribute("FieldNull"));
			this.FieldWidth = xmlElement_0.GetAttribute("FieldWidth");
			this.FieldHeight = xmlElement_0.GetAttribute("FieldHeight");
			this.FieldDValueType = xmlElement_0.GetAttribute("FieldDValueType");
			this.FieldDValue = xmlElement_0.GetAttribute("FieldDValue");
			this.FieldInDispStyle = xmlElement_0.GetAttribute("FieldInDispStyle");
			this.FieldInDispStyleTxt = xmlElement_0.GetAttribute("FieldInDispStyleTxt");
			this.FieldInDispStyleName = xmlElement_0.GetAttribute("FieldInDispStyleName");
		}

		public XmlElement ToXml(XmlDocument xmlDoc)
		{
			XmlElement xmlElement = xmlDoc.CreateElement("Field");
			xmlElement.SetAttribute("_AutoID", base._AutoID);
			xmlElement.SetAttribute("TableName", this.TableName);
			xmlElement.SetAttribute("FieldName", this.FieldName);
			int styleIndex = this.StyleIndex;
			xmlElement.SetAttribute("StyleIndex", styleIndex.ToString());
			styleIndex = this.FieldInDisp;
			xmlElement.SetAttribute("FieldInDisp", styleIndex.ToString());
			styleIndex = this.FieldRead;
			xmlElement.SetAttribute("FieldRead", styleIndex.ToString());
			styleIndex = this.FieldNull;
			xmlElement.SetAttribute("FieldNull", styleIndex.ToString());
			xmlElement.SetAttribute("FieldWidth", this.FieldWidth);
			xmlElement.SetAttribute("FieldHeight", this.FieldHeight);
			xmlElement.SetAttribute("FieldDValueType", this.FieldDValueType);
			xmlElement.SetAttribute("FieldDValue", this.FieldDValue);
			xmlElement.SetAttribute("FieldInDispStyle", this.FieldInDispStyle);
			xmlElement.SetAttribute("FieldInDispStyleTxt", this.FieldInDispStyleTxt);
			xmlElement.SetAttribute("FieldInDispStyleName", this.FieldInDispStyleName);
			return xmlElement;
		}
	}
}