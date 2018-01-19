using EIS.AppBase;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class DictEntry : AppModelBase
	{
		[Description("DictID")]
		public string DictID
		{
			get;
			set;
		}

		[Description("ItemCode")]
		public string ItemCode
		{
			get;
			set;
		}

		[Description("ItemName")]
		public string ItemName
		{
			get;
			set;
		}

		[Description("ItemOrder")]
		public int ItemOrder
		{
			get;
			set;
		}

		public DictEntry()
		{
		}

		public DictEntry(XmlElement xmlElement_0)
		{
			base._AutoID = Guid.NewGuid().ToString();
			base._UserName = "";
			base._OrgCode = "";
			base._IsDel = 0;
			base._CreateTime = DateTime.Now;
			base._UpdateTime = DateTime.Now;
			this.ItemName = xmlElement_0.GetAttribute("ItemName");
			this.ItemCode = xmlElement_0.GetAttribute("ItemCode");
			this.ItemOrder = int.Parse(xmlElement_0.GetAttribute("ItemOrder"));
			this.DictID = xmlElement_0.GetAttribute("DictID");
		}

		public XmlElement ToXml(XmlDocument xmlDoc)
		{
			XmlElement xmlElement = xmlDoc.CreateElement("DictEntry");
			xmlElement.SetAttribute("ItemName", this.ItemName);
			xmlElement.SetAttribute("ItemCode", this.ItemCode);
			xmlElement.SetAttribute("ItemOrder", this.ItemOrder.ToString());
			xmlElement.SetAttribute("DictID", this.DictID);
			return xmlElement;
		}
	}
}