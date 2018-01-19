using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class TableScript : AppModelBase
	{
		public string Enable
		{
			get;
			set;
		}

		public string ScriptCode
		{
			get;
			set;
		}

		public string ScriptEvent
		{
			get;
			set;
		}

		public string ScriptNote
		{
			get;
			set;
		}

		public string ScriptTxt
		{
			get;
			set;
		}

		public string TableName
		{
			get;
			set;
		}

		public TableScript()
		{
		}

		public TableScript(XmlElement xmlElement_0)
		{
			base._AutoID = Guid.NewGuid().ToString();
			base._UserName = "";
			base._OrgCode = "";
			base._IsDel = 0;
			base._CreateTime = DateTime.Now;
			base._UpdateTime = DateTime.Now;
			this.TableName = xmlElement_0.GetAttribute("TableName");
			this.ScriptCode = xmlElement_0.GetAttribute("ScriptCode");
			this.Enable = xmlElement_0.GetAttribute("Enable");
			this.ScriptEvent = xmlElement_0.GetAttribute("ScriptEvent");
			this.ScriptTxt = xmlElement_0.SelectSingleNode("ScriptTxt").InnerText;
			this.ScriptNote = xmlElement_0.SelectSingleNode("ScriptNote").InnerText;
		}

		public XmlElement ToXml(XmlDocument xmlDoc)
		{
			XmlElement xmlElement = xmlDoc.CreateElement("TableScript");
			xmlElement.SetAttribute("TableName", this.TableName);
			xmlElement.SetAttribute("ScriptCode", this.ScriptCode);
			xmlElement.SetAttribute("Enable", this.Enable);
			xmlElement.SetAttribute("ScriptEvent", this.ScriptEvent);
			XmlElement xmlElement1 = xmlDoc.CreateElement("ScriptTxt");
			xmlElement1.InnerXml = string.Format("<![CDATA[{0}]]>", this.ScriptTxt);
			xmlElement.AppendChild(xmlElement1);
			XmlElement xmlElement2 = xmlDoc.CreateElement("ScriptNote");
			xmlElement2.InnerXml = string.Format("<![CDATA[{0}]]>", this.ScriptNote);
			xmlElement.AppendChild(xmlElement2);
			return xmlElement;
		}
	}
}