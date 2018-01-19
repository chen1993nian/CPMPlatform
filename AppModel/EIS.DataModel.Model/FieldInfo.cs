using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class FieldInfo : AppModelBase
	{
		public string ColFormatExp
		{
			get;
			set;
		}

		public string ColTotalExp
		{
			get;
			set;
		}

		public string ColumnAlign
		{
			get;
			set;
		}

		public int ColumnHidden
		{
			get;
			set;
		}

		public int ColumnOrder
		{
			get;
			set;
		}

		public string ColumnRender
		{
			get;
			set;
		}

		public string ColumnWidth
		{
			get;
			set;
		}

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

		public string FieldInFilter
		{
			get;
			set;
		}

		public string FieldLength
		{
			get;
			set;
		}

		public string FieldName
		{
			get;
			set;
		}

		public string FieldNameCn
		{
			get;
			set;
		}

		public string FieldNote
		{
			get;
			set;
		}

		public int FieldNull
		{
			get;
			set;
		}

		public int FieldOdr
		{
			get;
			set;
		}

		public int FieldRead
		{
			get;
			set;
		}

		public string FieldStyleId
		{
			get;
			set;
		}

		public int FieldType
		{
			get;
			set;
		}

		public string FieldWidth
		{
			get;
			set;
		}

		public int IsComput
		{
			get;
			set;
		}

		public int IsUnique
		{
			get;
			set;
		}

		public int ListDisp
		{
			get;
			set;
		}

		public string QueryDefaultType
		{
			get;
			set;
		}

		public string QueryDefaultValue
		{
			get;
			set;
		}

		public int QueryDisp
		{
			get;
			set;
		}

		public string QueryMatchMode
		{
			get;
			set;
		}

		public int QueryOrder
		{
			get;
			set;
		}

		public string QueryStyle
		{
			get;
			set;
		}

		public string QueryStyleName
		{
			get;
			set;
		}

		public string QueryStyleTxt
		{
			get;
			set;
		}

		public string TableName
		{
			get;
			set;
		}

		public FieldInfo()
		{
		}

		public FieldInfo(XmlElement xmlElement_0)
		{
			base._AutoID = xmlElement_0.GetAttribute("_AutoID");
			base._UserName = "";
			base._OrgCode = "";
			base._IsDel = 0;
			base._CreateTime = DateTime.Now;
			base._UpdateTime = DateTime.Now;
			this.TableName = xmlElement_0.GetAttribute("TableName");
			this.FieldName = xmlElement_0.GetAttribute("FieldName");
			this.FieldNameCn = xmlElement_0.GetAttribute("FieldNameCn");
			this.FieldOdr = int.Parse(xmlElement_0.GetAttribute("FieldOdr"));
			this.FieldType = int.Parse(xmlElement_0.GetAttribute("FieldType"));
			this.FieldLength = xmlElement_0.GetAttribute("FieldLength");
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
			this.FieldInFilter = xmlElement_0.GetAttribute("FieldInFilter");
			this.IsComput = int.Parse(xmlElement_0.GetAttribute("IsComput"));
			this.IsUnique = int.Parse(xmlElement_0.GetAttribute("IsUnique"));
			this.ListDisp = int.Parse(xmlElement_0.GetAttribute("ListDisp"));
			this.QueryDisp = int.Parse(xmlElement_0.GetAttribute("QueryDisp"));
			this.ColumnAlign = xmlElement_0.GetAttribute("ColumnAlign");
			this.ColumnWidth = xmlElement_0.GetAttribute("ColumnWidth");
			this.ColumnOrder = int.Parse(xmlElement_0.GetAttribute("ColumnOrder"));
			this.ColumnHidden = int.Parse(xmlElement_0.GetAttribute("ColumnHidden"));
			this.QueryOrder = int.Parse(xmlElement_0.GetAttribute("QueryOrder"));
			this.ColumnRender = xmlElement_0.GetAttribute("ColumnRender");
			this.ColFormatExp = xmlElement_0.GetAttribute("ColFormatExp");
			this.ColTotalExp = xmlElement_0.GetAttribute("ColTotalExp");
			this.QueryMatchMode = xmlElement_0.GetAttribute("QueryMatchMode");
			this.QueryDefaultType = xmlElement_0.GetAttribute("QueryDefaultType");
			this.QueryDefaultValue = xmlElement_0.GetAttribute("QueryDefaultValue");
			this.QueryStyle = xmlElement_0.GetAttribute("QueryStyle");
			this.QueryStyleName = xmlElement_0.GetAttribute("QueryStyleName");
			this.QueryStyleTxt = xmlElement_0.GetAttribute("QueryStyleTxt");
		}

		public XmlElement ToXml(XmlDocument xmlDoc)
		{
			XmlElement xmlElement = xmlDoc.CreateElement("Field");
			xmlElement.SetAttribute("_AutoID", base._AutoID);
			xmlElement.SetAttribute("TableName", this.TableName);
			xmlElement.SetAttribute("FieldName", this.FieldName);
			xmlElement.SetAttribute("FieldNameCn", this.FieldNameCn);
			int fieldOdr = this.FieldOdr;
			xmlElement.SetAttribute("FieldOdr", fieldOdr.ToString());
			fieldOdr = this.FieldType;
			xmlElement.SetAttribute("FieldType", fieldOdr.ToString());
			xmlElement.SetAttribute("FieldLength", this.FieldLength);
			fieldOdr = this.FieldInDisp;
			xmlElement.SetAttribute("FieldInDisp", fieldOdr.ToString());
			fieldOdr = this.FieldRead;
			xmlElement.SetAttribute("FieldRead", fieldOdr.ToString());
			fieldOdr = this.FieldNull;
			xmlElement.SetAttribute("FieldNull", fieldOdr.ToString());
			xmlElement.SetAttribute("FieldWidth", this.FieldWidth);
			xmlElement.SetAttribute("FieldHeight", this.FieldHeight);
			xmlElement.SetAttribute("FieldDValueType", this.FieldDValueType);
			xmlElement.SetAttribute("FieldDValue", this.FieldDValue);
			xmlElement.SetAttribute("FieldInDispStyle", this.FieldInDispStyle);
			xmlElement.SetAttribute("FieldInDispStyleTxt", this.FieldInDispStyleTxt);
			xmlElement.SetAttribute("FieldInDispStyleName", this.FieldInDispStyleName);
			xmlElement.SetAttribute("FieldInFilter", this.FieldInFilter);
			fieldOdr = this.IsComput;
			xmlElement.SetAttribute("IsComput", fieldOdr.ToString());
			fieldOdr = this.IsUnique;
			xmlElement.SetAttribute("IsUnique", fieldOdr.ToString());
			fieldOdr = this.ListDisp;
			xmlElement.SetAttribute("ListDisp", fieldOdr.ToString());
			fieldOdr = this.QueryDisp;
			xmlElement.SetAttribute("QueryDisp", fieldOdr.ToString());
			xmlElement.SetAttribute("ColumnAlign", this.ColumnAlign);
			xmlElement.SetAttribute("ColumnWidth", this.ColumnWidth);
			fieldOdr = this.ColumnOrder;
			xmlElement.SetAttribute("ColumnOrder", fieldOdr.ToString());
			fieldOdr = this.ColumnHidden;
			xmlElement.SetAttribute("ColumnHidden", fieldOdr.ToString());
			fieldOdr = this.QueryOrder;
			xmlElement.SetAttribute("QueryOrder", fieldOdr.ToString());
			xmlElement.SetAttribute("ColumnRender", this.ColumnRender);
			xmlElement.SetAttribute("ColFormatExp", this.ColFormatExp);
			xmlElement.SetAttribute("ColTotalExp", this.ColTotalExp);
			xmlElement.SetAttribute("QueryMatchMode", this.QueryMatchMode);
			xmlElement.SetAttribute("QueryDefaultType", this.QueryDefaultType);
			xmlElement.SetAttribute("QueryDefaultValue", this.QueryDefaultValue);
			xmlElement.SetAttribute("QueryStyle", this.QueryStyle);
			xmlElement.SetAttribute("QueryStyleName", this.QueryStyleName);
			xmlElement.SetAttribute("QueryStyleTxt", this.QueryStyleTxt);
			return xmlElement;
		}
	}
}