using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class FieldEvent : AppModelBase
	{
		public string EventScript
		{
			get;
			set;
		}

		public string EventType
		{
			get;
			set;
		}

		public string FieldID
		{
			get;
			set;
		}

		public string FieldName
		{
			get;
			set;
		}

		public string TableName
		{
			get;
			set;
		}

		public FieldEvent()
		{
		}

		public FieldEvent(XmlElement xmlElement_0)
		{
			base._AutoID = Guid.NewGuid().ToString();
			base._UserName = "";
			base._OrgCode = "";
			base._IsDel = 0;
			base._CreateTime = DateTime.Now;
			base._UpdateTime = DateTime.Now;
			this.FieldID = xmlElement_0.GetAttribute("FieldId");
			this.EventType = xmlElement_0.GetAttribute("EventType");
			this.TableName = xmlElement_0.GetAttribute("TableName");
			this.FieldName = xmlElement_0.GetAttribute("FieldName");
			this.EventScript = xmlElement_0.SelectSingleNode("EventScript").InnerText;
		}

		public XmlElement ToXml(XmlDocument xmlDoc)
		{
			XmlElement xmlElement = xmlDoc.CreateElement("FieldEvent");
			xmlElement.SetAttribute("FieldId", this.FieldID);
			xmlElement.SetAttribute("TableName", this.TableName);
			xmlElement.SetAttribute("FieldName", this.FieldName);
			xmlElement.SetAttribute("EventType", this.EventType);
			XmlElement xmlElement1 = xmlDoc.CreateElement("EventScript");
			xmlElement1.InnerXml = string.Format("<![CDATA[{0}]]>", this.EventScript);
			xmlElement.AppendChild(xmlElement1);
			return xmlElement;
		}
	}
}