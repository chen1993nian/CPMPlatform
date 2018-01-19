using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class FieldInfoExt : AppModelBase
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

		public int FieldType
		{
			get;
			set;
		}

		public int IsComput
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

		public FieldInfoExt()
		{
			this.IsComput = 1;
		}

		public FieldInfoExt(XmlElement xmlElement_0)
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
			this.FieldNameCn = xmlElement_0.GetAttribute("FieldNameCn");
			this.FieldType = int.Parse(xmlElement_0.GetAttribute("FieldType"));
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
			XmlElement xmlElement = xmlDoc.CreateElement("FieldExt");
			xmlElement.SetAttribute("_AutoID", base._AutoID);
			xmlElement.SetAttribute("TableName", this.TableName);
			int styleIndex = this.StyleIndex;
			xmlElement.SetAttribute("StyleIndex", styleIndex.ToString());
			xmlElement.SetAttribute("FieldName", this.FieldName);
			xmlElement.SetAttribute("FieldNameCn", this.FieldNameCn);
			styleIndex = this.FieldType;
			xmlElement.SetAttribute("FieldType", styleIndex.ToString());
			styleIndex = this.ListDisp;
			xmlElement.SetAttribute("ListDisp", styleIndex.ToString());
			styleIndex = this.QueryDisp;
			xmlElement.SetAttribute("QueryDisp", styleIndex.ToString());
			xmlElement.SetAttribute("ColumnAlign", this.ColumnAlign);
			xmlElement.SetAttribute("ColumnWidth", this.ColumnWidth);
			styleIndex = this.ColumnOrder;
			xmlElement.SetAttribute("ColumnOrder", styleIndex.ToString());
			styleIndex = this.ColumnHidden;
			xmlElement.SetAttribute("ColumnHidden", styleIndex.ToString());
			styleIndex = this.QueryOrder;
			xmlElement.SetAttribute("QueryOrder", styleIndex.ToString());
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